using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LeyouBoxing.Controllers
{
    public class OrderBoxingItemsController : Controller
    {

        private readonly DataContext _context;

        public OrderBoxingItemsController(DataContext context)
        {
            _context = context;

        }

        public IActionResult Index(int? id)
        {
            var orderboxing =  _context.OrderBoxings.Include(o => o.OrderBoxingItems).SingleOrDefault(o=>o.Id== id);
            return View(orderboxing);
        }
    }
}