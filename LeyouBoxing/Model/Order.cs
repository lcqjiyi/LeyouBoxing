using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeyouBoxing.Model
{
    public class Order
    {
        private Order() { }
        public Order(string jcNo, string name)
        {
            JcNo = jcNo;
            Name = name;
            InputDate = DateTime.Now;
            OrderItems = new List<OrderItem>();
            OrderRows = new List<OrderRow>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name ="货号")]
        public string JcNo { get; set; }
        [Display(Name = "款式")]
        public string Style { get; set; }
        [Display(Name = "描述")]
        public string Name { get; set; }
        [Display(Name = "添加时间")]
        public DateTime InputDate { get; set; }
        [Display(Name = "已生成")]
        public bool GenerateData { get; set; }

        public OrderSizeGroup OrderSizeGroup { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<OrderRow> OrderRows { get; set; }
        public ICollection<OrderBoxing> OrderSummarys { get; set; }

    }
}
