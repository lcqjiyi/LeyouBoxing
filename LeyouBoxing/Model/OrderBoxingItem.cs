using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeyouBoxing.Model
{
    public class OrderBoxingItem
    {
        private OrderBoxingItem() { }
        public OrderBoxingItem( string sKU, string style, string color, int size, string goodName, int poQty, int shipQty, int boxNumber, decimal pOprice, decimal money)
        {
            SKU = sKU;
            Style = style;
            Color = color;
            Size = size;
            GoodName = goodName;
            PoQty = poQty;
            ShipQty = shipQty;
            BoxNumber = boxNumber;
            POprice = pOprice;
            Money = money;
        }

        [Key]
        public int Id { get; set; }
        public int OrderBoxingId { get; set; }

        [Display(Name = "SKU")]
        public string SKU { get; set; }

        [Display(Name = "款号")]
        public string Style { get; set; }

        [Display(Name = "颜色")]
        public string Color { get; set; }

        [Display(Name = "尺码")]
        public int Size { get; set; }

        [Display(Name = "品名")]
        public string GoodName { get; set; }

        [Display(Name = "PO数量")]
        public int PoQty { get; set; }

        [Display(Name = "出货数")]
        public int ShipQty { get; set; }

        [Display(Name = "箱号")]
        public int BoxNumber { get; set; }

        [Display(Name = "PO单价")]
        public Decimal POprice { get; set; }

        [Display(Name = "金额")]
        public Decimal Money { get; set; }

    }
}
