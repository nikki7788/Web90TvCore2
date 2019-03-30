using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.PublicClass;

namespace Web90TvCore2.Models.ViewModels
{

    /// <summary>
    /// ویو مدل برای لاگین کردن
    /// </summary>
    public class LoginViewModel
    {

        /// <summary>
        /// نام کاربری 
        /// </summary>
        /// نام کاربری کاربری که میخواهد لاگین کند
        [Display(Name = "نام کاربری")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string UserName { get; set; }


        /// <summary>
        /// رمزعبور
        /// </summary>
        /// رمز عبور کاربری کاربری که میخواهد لاگین کند
        [Display(Name = "رمز عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        /// <summary>
        /// مرا به خاطر بسپار-ذخیره رمز عبور و پسورد برای ورود های بعدی
        /// </summary>
        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }

    }
}
