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
        public string FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی کاربر
        /// </summary>
        [Display(Name = "نام خانوادگی")]
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
        public override string PhoneNumber { get; set; }

        [Display(Name = "تصویر")]
        public string UserImagePath { get; set; }

        [Display(Name = "تاریخ تولد")]
        public string BirthDayDate { get; set; }




        ///// <summary>
        ///// شماره تماس کاربر
        ///// </summary>
        /////  اگر در ویو مدل ان را تعریف کریدم و از ویو مدل در ویو استفاده میکنیم نام پراپرتی ان در ویو مدل نمایش داده میشود و نیازی ب اوورراید نیست در اینجا
        //[Display(Name = "تلفن")]
        //public string PhoneNumber { get; set; }
        #endregion#################


    }


    /// <summary>
    /// 
    /// </summary>
    public enum GenderSelect : byte
    {
        male = 0,
        femal = 1
    }

}
