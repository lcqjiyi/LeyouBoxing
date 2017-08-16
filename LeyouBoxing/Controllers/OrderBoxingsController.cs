using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeyouBoxing;
using LeyouBoxing.Model;
using LeyouBoxing.ViewModels;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace LeyouBoxing.Controllers
{
    public class OrderBoxingsController : Controller
    {
        private readonly DataContext _context;
        private readonly OrderServer _orderServer;
        private IHostingEnvironment _hostingEnvironment;

        public OrderBoxingsController(DataContext context, OrderServer orderServer, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _orderServer = orderServer;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: OrderBoxings
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            var orderSummarysList = await _context.OrderBoxings
                .Where(o => o.OrderId == id)
                .OrderBy(o => o.TotalQty)
                .ThenBy(o => o.Hashcode)
                .ThenBy(o => o.LeyouNo).ToListAsync();
            if (orderSummarysList == null)
            {
                return NotFound();
            }

            ViewData["orderId"] = id;

            ViewData["styleList"]  = await _context.OrderItems.Where(o => o.OrderId == id).Select(o => o.Style).Distinct().ToListAsync();

            ViewData["BatchBoxingItem"] = await _context.OrderItems.Where(o => o.OrderId == id)
                .GroupBy(o=>new { o.Style, o.Color, o.Size })
                .Select(g =>new BatchBoxingItemViewModel {  Color=g.Key.Color, Style=g.Key.Style, Size=g.Key.Size})
                .OrderBy(o=>o.Style)
                .ThenBy(o=>o.Color)
                .ToListAsync();

            return View(orderSummarysList);
        }
        //批量装箱按款式装
        public  IActionResult BatchBoxing(string[] style, int[] boxNumber, int TotalMini,int TotalMax,int orderId) {
            if (orderId < 0) throw new Exception("没有订单号");
            if (TotalMini < 0) throw new  Exception("最小件数不能小于0");
            if (TotalMax < TotalMini) throw new Exception("最大件数数不能大于最少件数");
            if (style.Length != boxNumber.Length) throw new Exception("参数错误");

            var BoxingsList = _context.OrderBoxings.Include(o=>o.OrderBoxingItems).Where(o => o.OrderId == orderId && o.TotalQty >=TotalMini && o.TotalQty <=TotalMax).ToList();
            List<string> styleList = new List<string>(style);
            List<int> boxNumberList = new List<int>(boxNumber);
            foreach (var boxing in BoxingsList)
            {
                if (boxing.OrderBoxingItems.Count>0)
                {
                    _context.OrderBoxingItems.RemoveRange(boxing.OrderBoxingItems);
                }
                boxing.OrderBoxingItems = _orderServer.GenerateOrderBoxingItem(styleList, boxNumberList, boxing);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", new { id = orderId } );

        }
        //批量装箱按按码装
        public IActionResult BatchBoxingBySize(string[] style, string[] Color, string[] Size,int[] MaxQty, int TotalMini, int TotalMax, int orderId)
        {
            if (orderId < 0) throw new Exception("没有订单号");
            if (TotalMini < 0) throw new Exception("最小件数不能小于0");
            if (TotalMax < TotalMini) throw new Exception("最大件数数不能大于最少件数");
            if (style.Length != Color.Length) throw new Exception("参数错误");
            if (style.Length != Size.Length) throw new Exception("参数错误");
            if (style.Length != MaxQty.Length) throw new Exception("参数错误");

            var BoxingsList = _context.OrderBoxings.Include(o => o.OrderBoxingItems).Where(o => o.OrderId == orderId && o.TotalQty >= TotalMini && o.TotalQty <= TotalMax).ToList();

            Dictionary<string,int> KeyWork = new Dictionary<string, int>();
            for (int i = 0; i < style.Length; i++)
            {
                KeyWork.Add(style[i] + "-" + Color[i] + "-" + Size[i], MaxQty[i]);
            }

            foreach (var boxing in BoxingsList)
            {
                if (boxing.OrderBoxingItems.Count > 0)
                {
                    _context.OrderBoxingItems.RemoveRange(boxing.OrderBoxingItems);
                }
                boxing.OrderBoxingItems = _orderServer.GenerateOrderBoxingItemBySize(KeyWork, boxing);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", new { id = orderId });

        }
        /// <summary>
        /// 导出装箱单
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OutputExcel(int? id) {
            if (id == null)
            {
                return NotFound();
            }
            var orderSummarysList = await _context.OrderBoxings.Include(o=>o.OrderBoxingItems)
                .Where(o => o.OrderId == id)
                .OrderBy(o => o.TotalQty)
                .ThenBy(o => o.Hashcode)
                .ThenBy(o => o.LeyouNo).ToListAsync();
            if (orderSummarysList == null)
            {
                return NotFound();
            }


            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = $"{Guid.NewGuid()}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            using (ExcelPackage package = new ExcelPackage(file))
            {
                int rowindex = 1;
                // 添加worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                foreach (var order in orderSummarysList)
                {
                   
                    foreach (int box in order.OrderBoxingItems.GroupBy(o => o.BoxNumber).OrderBy(o => o.Key).Select(o => o.Key))
                    {
                        int Total = 0;
                        worksheet.Column(1).Width = 8; worksheet.Column(2).Width = 8; worksheet.Column(3).Width = 29; worksheet.Column(4).Width = 11; worksheet.Column(5).Width = 12; worksheet.Column(6).Width = 5; worksheet.Column(7).Width =60; worksheet.Column(8).Width = 5;
                        worksheet.Cells[rowindex, 1].Value = "装箱单"; worksheet.Row(rowindex).Height=40;worksheet.Cells[$"A{rowindex}:H{rowindex}"].Merge = true;rowindex++;
                        worksheet.Cells[rowindex, 1].Value = "送货单位：中山坚成制衣厂有限公司(供货商编号：10032)"; worksheet.Cells[$"A{rowindex}:H{rowindex}"].Merge = true; rowindex++;
                        worksheet.Cells[rowindex, 1].Value = $"箱号：{box}  总箱数： {order.TotalBox}";  worksheet.Cells[$"A{rowindex}:H{rowindex}"].Merge = true; rowindex++;
                        worksheet.Cells[rowindex, 1].Value = "乐友PO";
                        worksheet.Cells[rowindex, 2].Value = "启迪PO";
                        worksheet.Cells[rowindex, 3].Value = "门店库房";
                        worksheet.Cells[rowindex, 4].Value = "SKU";
                        worksheet.Cells[rowindex, 5].Value = "款号";
                        worksheet.Cells[rowindex, 6].Value = "尺码";
                        worksheet.Cells[rowindex, 7].Value = "品名";
                        worksheet.Cells[rowindex, 8].Value = "数量";
                        rowindex++;
                        foreach (var item in order.OrderBoxingItems.Where(o => o.BoxNumber == box).OrderBy(o => o.Style).ThenBy(o => o.Color).ThenBy(o => o.Size))
                        {
                            worksheet.Cells[rowindex, 1].Value = order.LeyouNo;
                            worksheet.Cells[rowindex, 2].Value = order.QidiNo;
                            worksheet.Cells[rowindex, 3].Value = order.ShopName;
                            worksheet.Cells[rowindex, 4].Value = item.SKU;
                            worksheet.Cells[rowindex, 5].Value = item.Style;
                            worksheet.Cells[rowindex, 6].Value = item.Size;
                            worksheet.Cells[rowindex, 7].Value = item.GoodName;
                            worksheet.Cells[rowindex, 8].Value = item.PoQty;
                            Total+= item.PoQty;
                            rowindex++;
                        }
                        worksheet.Cells[rowindex, 7].Value = "合计";
                        worksheet.Cells[rowindex, 8].Value =Total ;
                        worksheet.Row(rowindex).PageBreak = true;
                        rowindex++;
                    }
                }
                package.Save();

            }
            
            return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

    }
}
