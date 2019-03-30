using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.PublicClass;

namespace Web90TvCore2.Models.ViewModels
{
    /// <summary>
    /// ویومدل برای افزودن کاربر
    /// </summary>
    /// عمل میکند ApplicationUser روی کلاس 
    public class AddUserViewModel
    {
        /// <summary>
        /// شناسه کاربری
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// نام کاربری
        /// </summary>
        [Display(Name = "نام کاربری")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = PublicConst.LengthMessage)]
        public string UserName { get; set; }


        /// <summary>
        /// رمز عبور
        /// </summary>
        [Display(Name = "رمز عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = PublicConst.LengthMessage)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "رمز عبور باید ترکیبی از حروف کوچک و بزرگ و عدد و علامت باشد")]
        public string Password { get; set; }


        /// <summary>
        /// تکرار رمز عبور
        /// </summary>
        [Display(Name = "تکرار رمز عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = PublicConst.LengthMessage)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور با تکرار آن یکسان نیست")]
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "رمز عبور باید ترکیبی از حروف کوچک و بزرگ و عدد و علامت باشد")]
        public string ConfirmPassword { get; set; }


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
        /// شماره تماس کاربر
        /// </summary>
        [Display(Name = "تلفن")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "شماره تماس 11 رقمی می باشد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "شماره تماس شامل حرف نمی تواند باشد")]
        public string PhoneNumber { get; set; }


        /// <summary>
        /// ایمیل کاربر
        /// </summary>
        [Display(Name = "ایمیل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "ایمیل معتبر وارد کنید")]
        public string Email { get; set; }


        /// <summary>
        /// تصویر پروفایل کاربر
        /// </summary>
        [Display(Name = "تصویر")]
        public string UserImage { get; set; }


        /// <summary>
        /// تاریخ تولد کاربر
        /// </summary>
        [Display(Name = "تاریخ تولد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string BirthDayDate { get; set; }
    }


    /// <summary>
    /// ویومدل اینترنال برای ویرایش کاربران
    ///---- ویومدل اینترنال  داخل ویو مدل افزودن
    /// </summary>
    public class EditUserViewModel
    {

        /// <summary>
        /// شناسه کاربری
        /// </summary>
        public string Id { get; set; }

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
        /// شماره تماس کاربر
        /// </summary>
        [Display(Name = "تلفن")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "شماره تماس 11 رقمی می باشد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "شماره تماس شامل حرف نمی تواند باشد")]
        public string PhoneNumber { get; set; }


        /// <summary>
        /// ایمیل کاربر
        /// </summary>
        [Display(Name = "ایمیل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "ایمیل معتبر وارد کنید")]
        public string Email { get; set; }


        /// <summary>
        /// تصویر پروفایل کاربر
        /// </summary>
        [Display(Name = "تصویر")]
        public string UserImage { get; set; }


        /// <summary>
        /// تاریخ تولد کاربر
        /// </summary>
        [Display(Name = "تاریخ تولد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string BirthDayDate { get; set; }

        /// <summary>
        /// chekbox---
        /// ریست کردن رمز عبور توسط ادمین سیستم
        /// </summary>
        /// ادمین سیستم ی رمز ثابت مه همیشه ثابت است را به عنوان رمز به کاربر میدهد و کاربر باید خودش عوض کند

        [Display(Name = "ریست رمز عبور")]
        public bool ResetPassword { get; set; }


    }
}
