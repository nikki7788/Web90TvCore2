using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models;
using Web90TvCore2.Models.Service;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUsers> _signInManager;
        private readonly INewsService _newsService;

        public HomeController(SignInManager<ApplicationUsers> signInManager, IUnitOfWork UnitOfWork,
            UserManager<ApplicationUsers> userManager, INewsService newsService)
        {
            _userManager = userManager;
            _unitOfWork = UnitOfWork;
            _signInManager = signInManager;
            _newsService = newsService;
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
                //var query =  _unitOfWork.UserManagerUW.GetById(_userManager.GetUserId(HttpContext.User));

                // از کوکی مرورکر اطلاعات کاربر را میخواند  HttpContext.User اشاره به خود مرورگر خود کاربر لاگین کرده دارد  و HttpContext   
                var query = await _unitOfWork.UserManagerUW.GetById(_userManager.GetUserAsync(HttpContext.User).Result.Id);
                //ارسال اطلاعات کاربر به ویو اصلی جهت نمایش
                ViewBag.FullName = query.FirstName + " " + query.LastName;

                //todo: UserManagerUW  برای چی از دستور بالا استفاده کردیم؟ 
            }

            var model = new IndexViewModel();

            //   استقاده میکردیم result باید از  Crud در  Get نوشته شده متد   async چون به  صورت غیر همزمان 
            //  نبود نیاز نداشت async اگر 


            model.SliderNews = _unitOfWork.NewsRepUW.Get(n => n.NewsPlace == 0).Result.Take(4).ToList();


            model.SpecialNews = _unitOfWork.NewsRepUW.Get(n => n.NewsPlace == 1).Result.Take(8).ToList();


            model.LastVideos = _unitOfWork.NewsRepUW.Get(n => n.NewsPlace == 2).Result.Take(8).ToList();

            //ازبین تمامی خبر ها 15 تا خبر اخر را در تب نمایش میدهد
            model.LastNews = _unitOfWork.NewsRepUW.Get().Result.Take(15).ToList();

            //خبرهای داخلی NewsType==0
            model.DomesticNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 0).Result.Take(15).ToList();

            //خبرهای خارجی n => n.NewsType == 1
            model.ForeignNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 1).Result.Take(15).ToList();

            //خبرهای اختصاصی n => n.NewsType == 2
            model.ExclusiveNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 2).Result.Take(15).ToList();


            //model.loginVM=
            return View(model);


        }




        /// <summary>
        /// نمایش جزییات خبر
        /// متد خواندنی Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> NewsDetails(int id)
        {


            var model = new IndexViewModel();

            //   استقاده میکردیم result باید از  Crud در  Get نوشته شده متد   async چون به  صورت غیر همزمان 
            //  نبود نیاز نداشت async اگر 

            //ازبین تمامی خبر ها 15 تا خبر اخر را در تب نمایش میدهد
            model.LastNews = _unitOfWork.NewsRepUW.Get().Result.Take(15).ToList();
            //خبرهای داخلی NewsType==0
            model.DomesticNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 0).Result.Take(15).ToList();
            //خبرهای خارجی n => n.NewsType == 1
            model.ForeignNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 1).Result.Take(15).ToList();
            //خبرهای اختصاصی n => n.NewsType == 2
            model.ExclusiveNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 2).Result.Take(15).ToList();
            model.NewsDetails = await _unitOfWork.NewsRepUW.GetById(id);

            //به روز رسانی تعدا بازدید خبر
            await _newsService.RefreshVisitCounter(id);
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
