using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models;
using Web90TvCore2.Models.UnitOfWork;
using Web90TvCore2.Models.ViewModels;

namespace Web90TvCore2.Controllers
{
    /// <summary>
    /// کنترلر اصلی سایت
    /// </summary>
    public class HomeController : Controller
    {
        #region ################### Dependencies ###################

        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly SignInManager<ApplicationUsers> _signInManager;
        public HomeController(SignInManager<ApplicationUsers> signInManager, IUnitOfWork UnitOfWork, UserManager<ApplicationUsers> userManager)
        {
            _userManager = userManager;
            _UnitOfWork = UnitOfWork;
            _signInManager = signInManager;

        }

        #endregion###########

        #region ############## Actions #######################

        
        /// <summary>
        /// نمایش صفحه اصلی سایت
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            // اشاره به کاربری دارد که لاگین کرده است User 

            //اگر کاربر لاگین کرده بود
            if (_signInManager.IsSignedIn(User))
            {
                //روش 1
                //var query =  _UnitOfWork.UserManagerUW.GetById(_userManager.GetUserId(HttpContext.User));

                // از کوکی مرورکر اطلاعات کاربر را میخواند  HttpContext.User اشاره به خود مرورگر خود کاربر لاگین کرده دارد  و HttpContext   
                var query = await _UnitOfWork.UserManagerUW.GetById(_userManager.GetUserAsync(HttpContext.User).Result.Id);
                //ارسال اطلاعات کاربر به ویو اصلی جهت نمایش
                ViewBag.FullName = query.FirstName + " " + query.LastName;

                //todo: UserManagerUW  برای چی از دستور بالا استفاده کردیم؟ 
            }

            var model = new IndexViewModel();

            //   استقاده میکردیم result باید از  Crud در  Get نوشته شده متد   async چون به  صورت غیر همزمان 
            //  نبود نیاز نداشت async اگر 


            model.SliderNews =  _UnitOfWork.NewsRepUW.Get(n => n.NewsPlace == 0).Result.Take(4).ToList();


            model.SpecialNews=_UnitOfWork.NewsRepUW.Get(n=>n.NewsPlace==1).Result.Take(8).ToList();


            model.LastVideos = _UnitOfWork.NewsRepUW.Get(n => n.NewsPlace == 2).Result.Take(8).ToList();

            //ازبین تمامی خبر ها 15 تا خبر اخر را در تب نمایش میدهد
            model.LastNews = _UnitOfWork.NewsRepUW.Get().Result.Take(15).ToList();

            //خبرهای داخلی NewsType==0
            model.DomesticNews = _UnitOfWork.NewsRepUW.Get(n=>n.NewsType==0).Result.Take(15).ToList();

            //خبرهای خارجی n => n.NewsType == 1
            model.ForeignNews = _UnitOfWork.NewsRepUW.Get(n => n.NewsType == 1).Result.Take(15).ToList();

            //خبرهای اختصاصی n => n.NewsType == 2
            model.ExclusiveNews = _UnitOfWork.NewsRepUW.Get(n => n.NewsType == 2).Result.Take(15).ToList();

            
            //model.loginVM=
            return View(model);
              

        }




        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #endregion #####################
    }
}
