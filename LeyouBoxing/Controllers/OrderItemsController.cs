using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeyouBoxing;
using LeyouBoxing.Model;

namespace LeyouBoxing.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly DataContext _context;

        public OrderItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: OrderItems
        public async Task<IActionResult> Index(int id)
        {
            var list = await _context.OrderItems.Where(o => o.OrderId == id).ToListAsync();
            return View(list);
        }



    }
}
