using LeyouBoxing.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeyouBoxing
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
        public DbSet<OrderSizeGroup> OrderSizeGroup { get; set; }
        public DbSet<OrderBoxing> OrderBoxings { get; set; }
        public DbSet<OrderBoxingItem> OrderBoxingItems { get; set; }
    }
}
