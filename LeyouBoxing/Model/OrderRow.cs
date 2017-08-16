using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LeyouBoxing.Model
{
    public class OrderRow
    {
        private OrderRow() { }
        public OrderRow( int leyouNo, int qidiNo, string shopName, string sylte, string color)
        {
            LeyouNo = leyouNo;
            QidiNo = qidiNo;
            ShopName = shopName;
            Sylte = sylte;
            Color = color;
        }

        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        [Display(Name = "乐友PO")]
        public int LeyouNo { get; set; }
        [Display(Name = "启迪PO")]
        public int QidiNo { get; set; }
        [Display(Name = "门店库房")]
        public string ShopName { get; set; }

        [Display(Name = "款号")]
        public string Sylte { get; set; }

        [Display(Name = "颜色")]
        public string Color { get; set; }

        public int Qty1 { get; set; }
        public int Qty2 { get; set; }
        public int Qty3 { get; set; }
        public int Qty4 { get; set; }
        public int Qty5 { get; set; }
        public int Qty6 { get; set; }
        public int Qty7 { get; set; }
        public int Qty8 { get; set; }
        public int Qty9 { get; set; }

        public int Total() {

            return Qty1 + Qty2 + Qty3 + Qty4 + Qty5 + Qty6 + Qty7 + Qty8 + Qty9;
        }

        public void AddQty(OrderItem item , OrderSizeGroup sizeGroup) {
            int sizeIndex = sizeGroup.GetSizeIndex(item.Size.ToString());
            if (sizeIndex < 0) new Exception("没有找到" + item.LeyouNo + "  " + item.Style + "  " + item.Size + "码数");
            Type t = typeof(OrderRow);
            var a = t.GetProperty("Qty" + sizeIndex);
            a.SetValue(this, item.PoQty);//通过名称对属性 
        }
        public override string ToString()
        {
            return Sylte + Color + Qty1 + Qty2 + Qty3 + Qty4 + Qty5 + Qty6 + Qty7 + Qty8 + Qty9;
        }

    }
}
