using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeyouBoxing.ViewModels
{
    public class InputOrderViewModel
    {
        [Required]
        [Display(Name = "货号")]
        public string JcNo { get; set; }
        [Required]
        [Display(Name = "描述")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Excel文件")]
        public IFormFile ExcelFile { get; set; }
    }
}
