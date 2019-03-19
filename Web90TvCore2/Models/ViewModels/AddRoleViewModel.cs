using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.PublicClass;

namespace Web90TvCore2.Models.ViewModels
{
    /// <summary>
    ///   (ویو مدل برای افزودن نقش(=بخش  
    /// </summary>
    /// عمل میکند ApplicationRole روی کلاس 
    public class AddApplicationRoleViewModel
    {

        /// <summary>
        /// (شناسه بخش (=نقش
        /// </summary>
        public string Id { get; set; }



        /// <summary>
        /// (نام بخش(=نقش  
        /// </summary>
        /// حرف اول باید به صورت بزرگ وارد شود
        [Display(Name ="نام نقش (انگلیسی)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string Name { get; set; }




        /// <summary>
        /// زیر دسته ها - پدر نقش را مشخص میکند
        /// </summary>
        [Display(Name ="زیر دسته ها")]
        [Required(AllowEmptyStrings =false,ErrorMessage =PublicConst.EnterMessage)]
        public string RoleLevel { get; set; }


        /// <summary>
        /// نام بخش (=نقش) به صورت پارسی
        /// </summary>
        [Display(Name = " نام بخش(=نقش) - پارسی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string Description { get; set; }
    }
}
