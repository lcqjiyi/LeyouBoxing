using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using System.Reflection;

namespace LeyouBoxing.Model
{
    public class OrderServer
    {
        private readonly DataContext _context;
        public OrderServer(DataContext context) {
            _context = context;
        }
        /// <summary>
        /// 从EXCEL生成订单
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="jcno"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Order GetOrderForExcel(Stream stream, string jcno, string name) {

            ExcelPackage Excel = new ExcelPackage(stream);
            ExcelWorksheet sheet = Excel.Workbook.Worksheets[1];
            int rowCount = sheet.Dimension.Rows;
            List<string> stylelist = new List<string>();
            Order Order = new Order(jcno, name);
            for (int rowIndex = 2; rowIndex < rowCount; rowIndex++)
            {
                string style = sheet.Cells[rowIndex, 12].GetValue<string>();
                if (!stylelist.Contains(style))
                {
                    stylelist.Add(style);
                }
                var NewOrderitme = GetOrderItemForExcelRow(sheet,rowIndex);
                Order.OrderItems.Add(NewOrderitme);
               

            }
            Order.Style = string.Join("/", stylelist);
            return Order;
        }

        private OrderItem GetOrderItemForExcelRow(ExcelWorksheet sheet, int rowIndex)
        {

            OrderItem item = new OrderItem();

            item.OrderNo = sheet.Cells[rowIndex, 1].GetValue<string>();
            item.Suppliercode = sheet.Cells[rowIndex, 2].GetValue<string>();
            item.Quarterly = sheet.Cells[rowIndex, 3].GetValue<string>();
            item.ApprovalNo = sheet.Cells[rowIndex, 4].GetValue<string>();
            item.LeyouNo =sheet.Cells[rowIndex, 5].GetValue<int>();
            item.QidiNo = sheet.Cells[rowIndex, 6].GetValue<int>();
            item.ShopName = sheet.Cells[rowIndex, 7].GetValue<string>();
            item.PoInfo = sheet.Cells[rowIndex, 8].GetValue<string>();
            item.StartTime = sheet.Cells[rowIndex, 9].GetValue<string>();
            item.EndTime = sheet.Cells[rowIndex, 10].GetValue<string>();
            item.SKU = sheet.Cells[rowIndex, 11].GetValue<string>();
            item.Style = sheet.Cells[rowIndex, 12].GetValue<string>();
            item.Color = sheet.Cells[rowIndex, 13].GetValue<string>();
            item.Size = sheet.Cells[rowIndex, 14].GetValue<int>();
            item.GoodName = sheet.Cells[rowIndex, 15].GetValue<string>();
            item.PoQty = sheet.Cells[rowIndex, 16].GetValue<int>();
            item.POprice = sheet.Cells[rowIndex, 17].GetValue<Decimal>();
            item.Money = item.PoQty * item.POprice;

            return item;

        }

        /// <summary>
        /// 生成行列表
        /// </summary>
        /// <param name="orderItems"></param>
        /// <param name="sizeGroup"></param>
        /// <returns></returns>
        public List<OrderRow> GenerateOrderRowList(List<OrderItem> orderItems,OrderSizeGroup sizeGroup) {
            Dictionary<string, OrderRow> OrderRowList = new Dictionary<string, OrderRow>();
            OrderRow TempOrderRow;
            foreach (OrderItem orderItem in orderItems)
            {
                string key = orderItem.LeyouNo + orderItem.Style + orderItem.Color;
                if (!OrderRowList.ContainsKey(key)) {
                    OrderRowList.Add(key, new OrderRow(orderItem.LeyouNo, orderItem.QidiNo, orderItem.ShopName, orderItem.Style, orderItem.Color));
                }
                OrderRowList.TryGetValue(key, out TempOrderRow);
                TempOrderRow.AddQty(orderItem, sizeGroup);

            }
            return OrderRowList.Values.ToList();
        }
        /// <summary>
        /// 生成尺码组
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        public OrderSizeGroup GetOrderSizeGroup(List<OrderItem> orderItems) {
            OrderSizeGroup sizegroup = new OrderSizeGroup();
            List<int> sizeList = orderItems.GroupBy(o=>o.Size).OrderBy(o => o.Key).Select(g=>g.Key).ToList();
            sizegroup.Nmae = string.Join("/", sizeList);
            Type type = typeof(OrderSizeGroup);
            for (int i = 0; i < sizeList.Count; i++)
            {
                var a = type.GetProperty("Size" + (i + 1));
                if (a == null) new Exception("Size" + i + 1 + "找不到。");
                 a .SetValue(sizegroup, sizeList[i].ToString());//通过名称对属性
            }
            return sizegroup;
        }

       
        /// <summary>
        /// 生成汇总
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        public List<OrderBoxing> GenerateOrderSummary(List<OrderItem> orderItems) {
            List< OrderBoxing> OrderSummaryList = new  List<OrderBoxing>();
            var ItemList = orderItems.GroupBy(o => new { o.LeyouNo, o.QidiNo,o.ShopName}).Select(o=>o.Key).ToList();
            foreach (var Item in ItemList)
            {
                string Sid = "";
                int Total = 0;
                var sortList =_context.OrderRows.Where(o => o.LeyouNo == Item.LeyouNo)
                    .OrderBy(o => new {o.Sylte,o.Color}).ToList();
                foreach (var item in sortList)
                {
                    Total += item.Total();
                    Sid += item.ToString();
                }
                if (Sid.Length > 255) Sid = Sid.Substring(0, 255);
                OrderSummaryList.Add(new OrderBoxing(Item.LeyouNo, Item.QidiNo, Item.ShopName, Total, Sid.GetHashCode()));
            }
            return OrderSummaryList;
        }

        /// <summary>
        /// 生成装箱列表
        /// </summary>
        /// <param name="style"></param>
        /// <param name="boxNumber"></param>
        /// <param name="LeyouNo"></param>
        /// <returns></returns>
        public List<OrderBoxingItem> GenerateOrderBoxingItem(List<string> styleList, List<int> boxNumberList, OrderBoxing OrderBoxings) {
            List<OrderBoxingItem> OrderBoxingItems = new List<OrderBoxingItem>();
            var itemlist= _context.OrderItems.Where(o => o.OrderId == OrderBoxings.OrderId && o.LeyouNo == OrderBoxings.LeyouNo).ToList();
            
            foreach (var item in itemlist)
            {
                int index = styleList.IndexOf(item.Style);
                OrderBoxingItems.Add(new OrderBoxingItem(item.SKU, item.Style, item.Color, item.Size, item.GoodName, item.PoQty, item.PoQty, boxNumberList[index], item.POprice, item.Money));
            }
            OrderBoxings.TotalBox = OrderBoxingItems.GroupBy(o => o.BoxNumber).Count();
            return OrderBoxingItems;


        }

        public ICollection<OrderBoxingItem> GenerateOrderBoxingItemBySize(Dictionary<string, int> KeyWork, OrderBoxing OrderBoxings)
        {
            List<OrderBoxingItem> OrderBoxingItems = new List<OrderBoxingItem>();
            var itemlist = _context.OrderItems.Where(o => o.OrderId == OrderBoxings.OrderId && o.LeyouNo == OrderBoxings.LeyouNo).ToList();
            var styleColorList = itemlist.GroupBy(o => new { o.Style, o.Color }).ToList();
            int BoxNumber = 1;
            foreach (var styleColor in styleColorList)//分款式分色
            {
                foreach (var item in itemlist.Where(o=>o.Style==styleColor.Key.Style && o.Color == styleColor.Key.Color))
                {
                    int MaxQty = 0;
                    string key = item.Style + "-" + item.Color + "-" + item.Size;
                    if (!KeyWork.TryGetValue(key, out MaxQty)) throw new Exception(key + "的最大装箱数值无法获取");
                    if (item.PoQty >= MaxQty)//大于1箱
                    {
                        for (int i = 0; i < item.PoQty / MaxQty; i++)
                        {
                            OrderBoxingItems.Add(new OrderBoxingItem(item.SKU, item.Style, item.Color, item.Size, item.GoodName, MaxQty, MaxQty, BoxNumber, item.POprice, item.Money));
                            BoxNumber++;
                        }
                    }
                }
                int tempQty = 0;
                foreach (var item in itemlist.Where(o => o.Style == styleColor.Key.Style && o.Color == styleColor.Key.Color))
                {
                    int MaxQty = 0;
                    string key = item.Style + "-" + item.Color + "-" + item.Size;
                    if (!KeyWork.TryGetValue(key, out MaxQty)) throw new Exception(key + "的最大装箱数值无法获取");
                    int RemainingQty = item.PoQty % MaxQty;
                    if (RemainingQty > 0)
                    {
                        if (tempQty + RemainingQty > MaxQty) {
                            //装满一箱
                            OrderBoxingItems.Add(new OrderBoxingItem(item.SKU, item.Style, item.Color, item.Size, item.GoodName, MaxQty - tempQty, MaxQty - tempQty, BoxNumber, item.POprice, item.Money));
                            BoxNumber++;
                            if (RemainingQty - (MaxQty - tempQty)>0) //装满一箱剩的数
                            {
                                tempQty = RemainingQty - (MaxQty - tempQty);
                                OrderBoxingItems.Add(new OrderBoxingItem(item.SKU, item.Style, item.Color, item.Size, item.GoodName, tempQty, tempQty, BoxNumber, item.POprice, item.Money));

                            }
                          
                        }
                        else
                        {
                            tempQty += RemainingQty;
                            OrderBoxingItems.Add(new OrderBoxingItem(item.SKU, item.Style, item.Color, item.Size, item.GoodName, item.PoQty % MaxQty, item.PoQty % MaxQty, BoxNumber, item.POprice, item.Money));
                        }
                       
                    }
                }
                BoxNumber++;
            }

            OrderBoxings.TotalBox = OrderBoxingItems.GroupBy(o => o.BoxNumber).Count();
            return OrderBoxingItems;
        }


    }
}
