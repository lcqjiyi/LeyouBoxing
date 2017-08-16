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
using System.IO;
using OfficeOpenXml;

namespace LeyouBoxing.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DataContext _context;
        private readonly OrderServer _orderServer;

        public OrdersController(DataContext context, OrderServer orderServer)
        {
            _context = context;
            _orderServer = orderServer;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        // GET: Orders/Create
        public IActionResult InputExcel()
        {
            return View();
        }
        //导入文件
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InputExcel(InputOrderViewModel InputOrderViewModel)
        {
            if (InputOrderViewModel.ExcelFile != null) {
                if (!InputOrderViewModel.ExcelFile.FileName.Contains(".xlsx"))
                {
                    ModelState.AddModelError("ExcelFile", "文件不是.xlsx文件");
                } 
            }

            if (ModelState.IsValid)
            {

                using (MemoryStream fs = new MemoryStream())
                {
                    InputOrderViewModel.ExcelFile.CopyTo(fs);
                    var newOrder = _orderServer.GetOrderForExcel(fs, InputOrderViewModel.JcNo, InputOrderViewModel.Name);
                    _context.Orders.Add(newOrder);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            return View(InputOrderViewModel);
        }

        public async Task<ActionResult> GetOrderRow(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderRowList = await _context.OrderRows.Where(o => o.OrderId == id).ToListAsync();
            if (orderRowList == null)
            {
                return NotFound();
            }
            ViewData["OrderSizeGroup"] = await _context.OrderSizeGroup.Where(o => o.OrderId == id).FirstOrDefaultAsync();
            return View(orderRowList);
        }

        public async Task<ActionResult> GetOrderSummary(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderSummarysList = await _context.OrderBoxings
                .Where(o => o.OrderId == id)
                .OrderBy(o=> o.TotalQty )
                .ThenBy(o=>o.Hashcode)
                .ThenBy(o=>o.LeyouNo).ToListAsync();
            if (orderSummarysList == null)
            {
                return NotFound();
            }
            return View(orderSummarysList);
        }
        /// <summary>
        /// 生成数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> GenerateData(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            var orderItemsList = await _context.OrderItems.Where(o => o.OrderId == id).ToListAsync();
            if (orderItemsList == null)
            {
                return NotFound();
            }
            if (order.GenerateData == false)
            {
                order.OrderSizeGroup = _orderServer.GetOrderSizeGroup(orderItemsList);
                order.OrderRows = _orderServer.GenerateOrderRowList(orderItemsList, order.OrderSizeGroup);
                await _context.SaveChangesAsync();
                order.OrderSummarys = _orderServer.GenerateOrderSummary(orderItemsList);
                order.GenerateData = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,jcNo,Style,Name,InputDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
