using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models;
using Web90TvCore2.Models.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Web90TvCore2.Controllers
{

    /// <summary>
    ///  کنترلر برای لاگین کردن ومدیریت ان
    /// </summary>
    public class AccountController : Controller
    {

        #region #################### Dependencies ##############################

        private readonly SignInManager<ApplicationUsers> _signInManager;

        private readonly UserManager<ApplicationUsers> _userManager;


        public AccountController(SignInManager<ApplicationUsers> signInManager, UserManager<ApplicationUsers> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #endregion###########################



        /// <summary>
        /// Post Method
        /// اکشن لاگین برای بررسی هویت و دسترسی کاربر و لاگین کردن کاربر
        /// </summary>
        /// <param name="model">ویو مدل لاگین که مقادیر فرم پرشده لاگین توسز کاربر را همراه خود دارد</param>
        /// <param name="returnUrl"> ادرس صفحه ای که کاربروارد آن شده و نیاز به لاگین دارد</param>
        ///  و یا به صفحه ای برود که نیاز به لاگین دارداین ادرس از طریق این ورودی از متد فرم پارشال ویو لاگین ارسال میشود اینجا Url اگر کاربر آدرسی وارد کند در 
        /// <returns></returns>
        ///   نداریم برای لاگین چون به صورت پارشال ویو ان را در صفحه اصلی تعریف کردیم و نیازی به نمایش ان نداریم Get متد      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            // را به ویو میفرستیم به تگ فرم Url  توسط این مقدار
            ViewData["returnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                //بررسی و تایید هویت کاربر و لاگین کردن او
                // var result= SignInResult result مقدار بولین برمیکرداند
                SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    //Success Login

                    // کاربر را براساس نام کاربری او که منحصر بفرد است پیدا کن
                    ApplicationUsers user = await _userManager.FindByNameAsync(model.UserName);

                    //نقش ها و دسترسی های کاربر را برکردان
                    //var roles=string []roles
                   string[] userRroles = _userManager.GetRolesAsync(user).Result.ToArray();


                    //اگرلاگین موفق بود
                    return Json(new { status="success" });

                    // اگر وجود داشت به متد زیر Url ارسال نقش ها و 
                   // return RedirectToLocal(userRroles, returnUrl);

                }

                //اگراطالعات درست نبود
                return Json(new { status = "failInput" });

            }

            // اگر ورودی های فرم لاگین  خالی بود
            return Json(new { status = "failEmptyInput" });
            // return PartialView(model);
        }


        /// <summary>
        ///  Private Action = Method   
        ///    ----این متد پس از موفقیت امیز بودن لاگین کاربر را به صفحه مورد نظر هدایت میکند
        /// </summary>
        /// <param name="userRoles">نقش هاودسترسی های کاربر را میگیرد</param>
        /// از اکشن لاگین ارسال میشود
        /// <param name="returnUrl">در وجود ادرس صفحه ای کاربر ان را وارد کرده و نیاز به لاگین داشته است را میگیرد</param>
        /// از اکشن لاگین ارسال میشود
        /// <returns></returns>
        private IActionResult RedirectToLocal(string[] userRoles, string returnUrl)
        {
            // جود داشت Url اگر 
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                // بودبرو مسیر زیر User اگر نقش های و دسترسی ها کاربر شامل 
                // را داشت User اگر کاربر دسترسی و نقش 
                if (userRoles.Contains("User"))
                {
                    return Redirect("/AdminPanel/User");
                }

            }
            return null;
        }


        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {

          await  _signInManager.SignOutAsync();
            return Redirect("/Home");
        }

    }
}