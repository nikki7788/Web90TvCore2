using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.PublicClass;
namespace Web90TvCore2.Models
{
    /// <summary>
    /// مدیریت و افزوودن و ویرایش فیلد و پارپرتی ها برای یوزر
    /// </summary>

    public class ApplicationUsers : IdentityUser
    {
        #region  ########################### Constructor ################

        #endregion#######

        #region######################### Properties ########################################

        /// <summary>
        /// نام کاربر
        /// </summary>
        [Display(Name = "نام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(90, MinimumLength = 4, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public string FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی کاربر
        /// </summary>
        [Display(Name = "نام خانوادگی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(100, MinimumLength = 4, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public string LastName { get; set; }

        /// <summary>
        /// جنسیت کاربر
        /// </summary>
        [Display(Name = "جنسیت")]
        public GenderSelect Gender { get; set; }


        /// <summary>
        ///شماره تلفن کاربر
        ///override prop
        /// </summary>
        /// این متد از روی پراپرتی خود ایدنتیتی اورراید شده است برای فارسی کردن نام پرارپرتی
        [Display(Name = "موبایل")]
        [StringLength(20, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[0-9]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public override string PhoneNumber { get; set; }

        #endregion#################


    }


    /// <summary>
    /// 
    /// </summary>
    public enum GenderSelect : byte
    {
        mail = 0,
        femail = 1
    }

}
