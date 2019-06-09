using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models.ViewModels
{

    /// <summary>
    /// ویومدل متن و گزینه های نظرسنجی
    /// </summary>
    public class AddPollViewModel
    {

        /// <summary>
        /// متن نطرسنجی
        /// </summary>
        [Display(Name = "سوال نظرسنجی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicClass.PublicConst.EnterMessage)]
        public string Question { get; set; }




        /// <summary>
        /// گزینه های نطرسنجی
        /// </summary>
        [Display(Name = "پاسخ ها")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicClass.PublicConst.EnterMessage)]
        public string Answer { get; set; }
    }
}
