using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.PublicClass;

namespace Web90TvCore2.Models
{
    /// <summary>
    /// تنظیمات سایت
    /// </summary>
    public class SiteSetting
    {
        /// <summary>
        /// کلید اصلی و شناسه
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// متا تگ ها
        /// </summary>
        [Display(Name = "متا تگ ها")]
        public string MetaTag { get; set; }


        /// <summary>
        /// متای توضیحات
        /// </summary>
        [Display(Name = "متای توضیحات")]
        public string MetaDescription { get; set; }

        /// <summary>
        /// عنوان سایت
        /// </summary>
        [Display(Name = "عنوان سایت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string SiteTitle { get; set; }
    }
}
