using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeyouBoxing.Model
{
    public class OrderBoxing
    {
        private OrderBoxing() { }
        public OrderBoxing(int leyouNo, int qidiNo, string shopName, int totalQty, int hashcode)
        {
            LeyouNo = leyouNo;
            QidiNo = qidiNo;
            ShopName = shopName;
            TotalQty = totalQty;
            Hashcode = hashcode;
            TotalBox = 0;
        }

        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        [Display(Name = "乐友PO")]
        public int LeyouNo { get; set; }
        [Display(Name = "启迪PO")]
        public int QidiNo { get; set; }
        [Display(Name ="城市")]
        public string City { get; set; }
        [Display(Name = "门店库房")]
        public string ShopName { get; set; }
        [Display(Name = "总件数")]
        public int TotalQty { get; set; }
        [Display(Name = "总箱数")]
        public int TotalBox { get; set; }
        [Display(Name = "哈希值")]
        public int Hashcode { get; set; }

        public ICollection<OrderBoxingItem> OrderBoxingItems { get; set; }
    }
}
