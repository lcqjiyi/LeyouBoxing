using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace LeyouBoxing.Model
{
    public class OrderSizeGroup
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Nmae { get; set; }

        public string Size1 { get; set; }
        public string Size2 { get; set; }
        public string Size3 { get; set; }
        public string Size4 { get; set; }
        public string Size5 { get; set; }
        public string Size6 { get; set; }
        public string Size7 { get; set; }
        public string Size8 { get; set; }
        public string Size9 { get; set; }

        public int GetSizeIndex(string size) {
            Type t = typeof(OrderSizeGroup);
            for (int i = 1; i <= 9; i++)
            {
               string sizestring = t.GetProperty("Size" + i).GetValue(this).ToString();
                if (sizestring == size)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
