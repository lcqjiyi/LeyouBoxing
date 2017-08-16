using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeyouBoxing.Model
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        [Display(Name = "预订单号")]
        public string OrderNo{ get; set; }
        [Display(Name = "供应商编码")]
        public string Suppliercode { get; set; }
        [Display(Name = "波段名")]
        public string Quarterly { get; set; }
        [Display(Name = "审批单号")]
        public string ApprovalNo { get; set; }
        [Display(Name = "乐友PO")] 
        public int LeyouNo { get; set; }
        [Display(Name = "启迪PO")] 
        public int QidiNo { get; set; }
        [Display(Name = "门店库房")] 
        public string ShopName { get; set; }
        [Display(Name = "PO信息")]
        public string PoInfo { get; set; }
        [Display(Name = "开始到店日期")]
        public string StartTime  { get; set; }
        [Display(Name = "截止到店日期")]
        public string EndTime  { get; set; }

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

        [Display(Name = "PO单价")]
        public Decimal Money { get; set; }
    }
}
