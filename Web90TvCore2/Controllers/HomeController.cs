using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Web90TvCore2.Models;
using Web90TvCore2.Models.Service;
using Web90TvCore2.Models.UnitOfWork;
using Web90TvCore2.Models.ViewModels;
using Web90TvCore2.PublicClass;

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
        private readonly ICommentService _comentService;
        private readonly IPollService _pollService;


        public HomeController(SignInManager<ApplicationUsers> signInManager, IUnitOfWork UnitOfWork,
            UserManager<ApplicationUsers> userManager, INewsService newsService, ICommentService commentService, IPollService pollService)
        {
            _userManager = userManager;
            _unitOfWork = UnitOfWork;
            _signInManager = signInManager;
            _newsService = newsService;
            _comentService = commentService;
            _pollService = pollService;
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


            model.SliderNews = _unitOfWork.NewsRepUW.Get(n => n.NewsPlace == 0, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(4).ToList();


            model.SpecialNews = _unitOfWork.NewsRepUW.Get(n => n.NewsPlace == 1, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(8).ToList();


            model.LastVideos = _unitOfWork.NewsRepUW.Get(n => n.NewsPlace == 2, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(8).ToList();

            //ازبین تمامی خبر ها 15 تا خبر اخر را در تب نمایش میدهد
            model.LastNews = _unitOfWork.NewsRepUW.Get(null, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(15).ToList();

            //خبرهای داخلی NewsType==0
            model.DomesticNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 0, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(15).ToList();

            //خبرهای خارجی n => n.NewsType == 1
            model.ForeignNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 1, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(15).ToList();

            ///خبرهای اختصاصی n => n.NewsType == 2
            model.ExclusiveNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 2, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(15).ToList();

            string pDate = PersianDateAndTime.PersianDateNow().Item2;
            ///تمام تبلیغاتی که تاریخ انها در بازه تاریخ ذکر شده قرار داشته باشدو وضعیت نمایش انها روی نمایش یعنی صفر باشد را میاورد
            model.Advertises = _unitOfWork.AdveriseRepUW.Get(
                a => (a.FromDate.CompareTo(pDate) <= 0
                && a.ToDate.CompareTo(pDate) >= 0
                && a.Flag == 0)).Result.ToList();

            ///model.loginVM=
            ///ارسال اطلاعات متاتگ ها
            var siteSetting = _unitOfWork.SiteSettingRepoUW.Get().Result.SingleOrDefault();
            if (siteSetting!=null)
            {
                ViewData["metaKey"] = siteSetting.MetaTag;
                ViewData["metadescription"] = siteSetting.MetaDescription;
                //ViewData["Title"] = siteSetting.SiteTitle;
            }


            ///---------------- نظر سنجی --------------
            if (_unitOfWork.PollRepoUW.Get(p => p.Active == true).Result.Count() == 1)
            {
                ///------------نظرسنجی فعالی وجود داشت--------------------------
                ///------------ آوردن اطلاعات نظرسنجی ----------------------
                var pollResult = _unitOfWork.PollRepoUW.Get(p => p.Active == true).Result.Single();

                if (Request.Cookies["poll" + pollResult.PollId] == null)
                {
                    ///کابر رای نداده است 
                    ///نمایش نظرسنجی
                    model.Poll = pollResult;

                }
                else
                {
                    //کاربر در نظرسنجی شرکت کرده است
                    //نمایش نتایج به صورت نمودار
                    List<PollResultViewModel> pollRes = new List<PollResultViewModel>();
                    foreach (PollOption vr in await _unitOfWork.PollOptionRepoUW.Get(p => p.PollID == pollResult.PollId))
                    {
                        ///-------  مقدار دهی ویو مدل نمایش نایج نظر سنجی -------
                        PollResultViewModel pRes = new PollResultViewModel()
                        {
                            ///todo:مهم
                            /// حتما باید با حروف کوچک باشد نام پاپرتی ها تا خطا ندهد در نمایش
                            data = vr.VouteCount,
                            label = vr.Answer
                        };
                        ///-------- افزودن هرگزینه و نتیجه ان به لیستی از ویو مدل برای ارسال به ویو برای نمایش نتایج -------
                        pollRes.Add(pRes);

                    }
                    ///------- برای اینکه نال به ویو ارسال نشود و باکس نمایش داده شود .نظر سنجی فعال را به ویو ارسال گردیم -------------
                    model.Poll = pollResult;
                    ///---------------  تبدیل لیست ویو مدل نمایش نتایج به فرمت جیسان بای ارسال به ویو  پارشال نظر سنجی ----------------
                    ViewBag.getListOfAnswer = JsonConvert.SerializeObject(pollRes);

                }

            }
            else
            {
                ///نظر سنجی فعالی وجود ندارد
                ///۰---- برای ویو نال ارسال میشود و به خاطر شرط در ویو چیزی نمایش داده نمیشود-----
                model.Poll = null;
            }
            return View(model);
        }


        /// <summary>
        /// نمایش جزییات خبر
        /// متد خواندنی Get
        /// </summary>
        /// <param name="id">شناسه خبر</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> NewsDetails(int id)
        {


            var model = new IndexViewModel();

            //   استقاده میکردیم result باید از  Crud در  Get نوشته شده متد   async چون به  صورت غیر همزمان 
            //  نبود نیاز نداشت async اگر 

            //ازبین تمامی خبر ها 15 تا خبر اخر را در تب نمایش میدهد
            model.LastNews = _unitOfWork.NewsRepUW.Get(null, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(15).ToList();
            //خبرهای داخلی NewsType==0
            model.DomesticNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 0, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(15).ToList();
            //خبرهای خارجی n => n.NewsType == 1
            model.ForeignNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 1, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(15).ToList();
            //خبرهای اختصاصی n => n.NewsType == 2
            model.ExclusiveNews = _unitOfWork.NewsRepUW.Get(n => n.NewsType == 2, ne => ne.OrderByDescending(n => n.NewsId)).Result.Take(15).ToList();



            //به روز رسانی تعدا بازدید خبر
            await _newsService.RefreshVisitCounter(id);

            //ارسال مدل متن و جزییات خبر 
            //model.NewsDetails = await _unitOfWork.NewsRepUW.GetById(id);
            var newsInfo = await _unitOfWork.NewsRepUW.GetById(id);
            ViewBag.newsContext = newsInfo;


            //ارسال اطلاعات متاتگ ها
            ViewData["metaKey"] = newsInfo.MetaTag;
            ViewData["metadescription"] = newsInfo.MetaDescription;
            ViewData["Title"] = newsInfo.Title;

            model.Advertises = _unitOfWork.AdveriseRepUW.Get(a => (a.FromDate.CompareTo(PersianDateAndTime.PersianDateNow().Item2)) <= 0
            && a.ToDate.CompareTo(PersianDateAndTime.PersianDateNow().Item2) >= 0 && a.Flag == 0).Result.ToList();


            //ارسال مدل نطرات 
            ViewBag.comments = await _unitOfWork.CommentRepUW.Get(n => n.NewsId == id);

            return View(model);
        }


        /// <summary>
        /// نمایش نطرات - متد خواندنی
        /// </summary>
        /// <param name="id">شناسه خبر</param>
        /// شناسه خبری که درمورد ان نظرات ثبت میشود
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertComment(string txtEmail, string txtFullname, string txtComment, int newsId, int cmId)
        {


            if (txtEmail == null || txtFullname == null || txtComment == null)
            {
                return Json(new { status = "failValidation" });
            }
            try
            {

                //-------------  بدست اوردن تایخ شمسی و زمان کنونی   -------------

                PersianCalendar persianCalendar = new PersianCalendar();
                var currentDate = DateTime.Now;
                int year = persianCalendar.GetYear(currentDate);
                int month = persianCalendar.GetMonth(currentDate);
                int day = persianCalendar.GetDayOfMonth(currentDate);

                string pCalendar = String.Format("{00:yyyy/MM/dd}", Convert.ToDateTime(year + "/" + month + "/" + day));
                string currentTime = String.Format("{0:hh:mm}", Convert.ToDateTime(currentDate.Hour + ":" + currentDate.Minute));
                // string NowTime = string.Format("{0:hh:mm}", Convert.ToDateTime(persianCalendar.GetHour(currentDate) + ":" + persianCalendar.GetMinute(currentDate)));
                //----------------------------------------------------

                //مقدار دهی جدول نطرات از روی اطلاعات ارسالی نطر
                Comment model = new Comment()
                {
                    //بدست اوردن آی پی کاربر
                    IP = HttpContext.Connection.RemoteIpAddress.ToString(),
                    //تغییر میکند و درسایت نمایش داده میشود true وضعیت خبر بعد از تاید مدیر سایت به 
                    status = false,
                    Email = txtEmail,
                    FullName = txtFullname,
                    Message = txtComment,
                    commentDate = pCalendar,
                    commentTime = currentTime,
                    NewsId = newsId,
                    //آیدی کامنتی که برای آن پاسخ ارسال میشود-اگر پاسخ به نطری نباشد کامنت -1از سمت جیکويری میاید.
                    //  است بهصورت پیش فرض getCommentId=-1
                    ReplyID = cmId
                };

                await _unitOfWork.CommentRepUW.Create(model);
                await _unitOfWork.Save();

                return Json(new { status = "success" });
            }
            catch (Exception)
            {

                return Json(new { status = "failSystem" });
                throw;
            }
        }


        /// <summary>
        /// لایک کامنت
        /// </summary>
        /// <param name="cmId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Like(int cmId)
        {
            var IsExistCm = await _unitOfWork.CommentRepUW.GetById(cmId);

            if (IsExistCm == null)
            {

                //اگر آی دی کامنت اشتباه بود
                //return null;  بادستور زیر تقریبا یکی است
                //برمیگردد به اکشن قبلی 
                return Redirect(Request.Headers["Referer"].ToString());
            }
            try
            {
                //چک کردن اینکه آیا از قبل کوکی ایجاد شده است یا نه
                if (Request.Cookies["_cm"] == null)
                {
                    //------------------*****************-------------------------------
                    //اگرکوکی برای دیسلایک وجود داشته باشد و خبر مورد نطردیسلایک شده باشد  تعداد تعداد دیسلایک را یکی کم میکند را کم میکند
                    if (Request.Cookies["_cmD"] != null)
                    {
                        if (Request.Cookies["_cmD"].Contains("," + cmId + ","))
                        {
                            await _comentService.DecreaseDislike(cmId);
                        }

                    }
                    //------------------*****************-------------------------------
                    //کوکی وجود نداشته است
                    //پس کوکی باید ایجاد شود
                    Response.Cookies.Append("_cm", "," + cmId + ",",
                        new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddYears(1) });


                    await _comentService.IncreaseLike(cmId);
                    //اگر کامنت دیسلایک شده باشد قبلا توسط این کابر و الان لایک کند از تعداد دیسلایک کم میشود و به لایک اضافه میشود
                    return Json(new { status = "success", result = IsExistCm.LikeCount, result2 = IsExistCm.DisLikeCount, backId = cmId });
                }
                else
                {
                    //اگر کوکی از قبل وجود داشت

                    string cookieContent = Request.Cookies["_cm"].ToString();

                    if (cookieContent.Contains("," + cmId + ","))
                    {
                        //اگر کاربر خواست یک کامنت را 2 بار لایک کند

                        return Redirect(Request.Headers["Referer"].ToString());
                    }
                    else
                    {
                        //اگر کاربر قبلا کامنتی را لایک کرده است و حالا می خواهد کامنت دیگری را لایک کند

                        //------------------*****************-------------------------------
                        //اگرکوکی برای دیسلایک وجود داشته باشد و خبر مورد نطردیسلایک شده باشد  تعداد تعداد دیسلایک را یکی کم میکند را کم میکند
                        if (Request.Cookies["_cmD"] != null)
                        {
                            if (Request.Cookies["_cmD"].Contains("," + cmId + ","))
                            {
                                await _comentService.DecreaseDislike(cmId);
                            }

                        }
                        //------------------*****************-------------------------------
                        cookieContent += "," + cmId + ",";
                        Response.Cookies.Append("_cm", cookieContent,
                             new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddYears(1) });

                        await _comentService.IncreaseLike(cmId);
                        //اگر کامنت دیسلایک شده باشد قبلا توسط این کابر و الان لایک کند از تعداد دیسلایک کم میشود و به لایک اضافه میشود
                        return Json(new { status = "success", result = IsExistCm.LikeCount, result2 = IsExistCm.DisLikeCount, backId = cmId });
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw ex;
            }
            catch (DbUpdateException ex)
            {

                throw ex;
            }

            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// دیسلایک کامنت
        /// </summary>
        /// <param name="cmId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Dislike(int cmId)
        {
            var IsExistCm = await _unitOfWork.CommentRepUW.GetById(cmId);

            if (IsExistCm == null)
            {

                //اگر آی دی کامنت اشتباه بود
                //return null;  بادستور زیر تقریبا یکی است
                //برمیگردد به اکشن قبلی 
                return Redirect(Request.Headers["Referer"].ToString());
            }
            try
            {
                //چک کردن اینکه آیا از قبل کوکی ایجاد شده است یا نه
                if (Request.Cookies["_cmD"] == null)
                {
                    //------------------*****************-------------------------------
                    //اگرکوکی برای لایک وجود داشته باشد و خبر مورد نطرلایک شده باشد  تعداد تعداد لایک را یکی کم میکند 
                    if (Request.Cookies["_cm"] != null)
                    {
                        if (Request.Cookies["_cm"].Contains("," + cmId + ","))
                        {
                            await _comentService.DecreaseLike(cmId);
                        }

                    }
                    //------------------*****************-------------------------------
                    //کوکی وجود نداشته است
                    //پس کوکی باید ایجاد شود
                    Response.Cookies.Append("_cmD", "," + cmId + ",",
                        new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddYears(1) });


                    await _comentService.IncreaseDislike(cmId);
                    //اگر کامنت لایک شده باشد قبلا توسط این کابر و الان دیسلایک کند از تعداد لایک کم میشود و به دیسلایم اضافه میشود
                    return Json(new { status = "success", result = IsExistCm.DisLikeCount, result2 = IsExistCm.LikeCount, backId = cmId });
                }
                else
                {
                    //اگر کوکی از قبل وجود داشت

                    string cookieContent = Request.Cookies["_cmD"].ToString();

                    if (cookieContent.Contains("," + cmId + ","))
                    {
                        //اگر کاربر خواست یک کامنت را 2 بار لایک کند

                        return Redirect(Request.Headers["Referer"].ToString());
                    }
                    else
                    {
                        //اگر کاربر قبلا کامنتی را لایک کرده است و حالا می خواهد کامنت دیگری را لایک کند

                        //------------------*****************-------------------------------
                        //اگرکوکی برای لایک وجود داشته باشد و خبر مورد نطرلایک شده باشد  تعداد تعداد لایک را یکی کم میکند 
                        if (Request.Cookies["_cm"] != null)
                        {
                            if (Request.Cookies["_cm"].Contains("," + cmId + ","))
                            {
                                await _comentService.DecreaseLike(cmId);
                            }

                        }
                        //------------------*****************-------------------------------
                        cookieContent += "," + cmId + ",";
                        Response.Cookies.Append("_cmD", cookieContent,
                             new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddYears(1) });

                        await _comentService.IncreaseDislike(cmId);
                        //اگر کامنت لایک شده باشد قبلا توسط این کابر و الان دیسلایک کند از تعداد لایک کم میشود و به دیسلایم اضافه میشود
                        return Json(new { status = "success", result = IsExistCm.DisLikeCount, result2 = IsExistCm.LikeCount, backId = cmId });
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw ex;
            }
            catch (DbUpdateException ex)
            {

                throw ex;
            }

            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// ثبت رای نظرسنجی
        /// </summary>
        /// <param name="answerId"></param>
        /// <param name="pollId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetVote(int answerId, int pollId)
        {
            if (answerId != 0 && pollId != 0)
            {
                //یعنی کاربر یک گزینه را انتخاب کرده است
                if (Request.Cookies["poll" + pollId] == null)
                {
                    //یعنی کاربر اولین بار است که می خواهد در این رای گیری شرکت نماید
                    Response.Cookies.Append("poll" + pollId, "kjdhsfkshfsdkjfhs",
                        new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddMonths(1) });

                    ///ثبت رای
                    _pollService.SetVote(answerId);

                    ///-------------------*****************نمایش نتایج به صورت نمودار-*****************----------------------------
                    var pollResult = _unitOfWork.PollRepoUW.Get(p => p.Active == true).Result.Single();

                    List<PollResultViewModel> pollRes = new List<PollResultViewModel>();
                    foreach (PollOption vr in await _unitOfWork.PollOptionRepoUW.Get(p => p.PollID == pollResult.PollId))
                    {
                        ///-------  مقدار دهی ویو مدل نمایش نایج نظر سنجی -------
                        PollResultViewModel pRes = new PollResultViewModel()
                        {
                            ///todo:مهم
                            /// حتما باید با حروف کوچک باشد نام پاپرتی ها تا خطا ندهد در نمایش
                            data = vr.VouteCount,
                            label = vr.Answer
                        };
                        ///-------- افزودن هرگزینه و نتیجه ان به لیستی از ویو مدل برای ارسال به ویو برای نمایش نتایج -------
                        pollRes.Add(pRes);

                    }
                    ///------- برای اینکه نال به ویو ارسال نشود و باکس نمایش داده شود .نظر سنجی فعال را به ویو ارسال گردیم -------------
                    ///---------------  تبدیل لیست ویو مدل نمایش نتایج به فرمت جیسان بای ارسال به ویو  پارشال نظر سنجی ----------------
                    return Json(new { status = "success", getListOfAnswer = JsonConvert.SerializeObject(pollRes) });

                }
                else
                {
                    //کاربر قبلا رای داده است
                    return Json(new { status = "duplicate" });
                }
            }
            else
            {
                //کاربر هیچ گزینه ای را انتخاب نکرده است و روی ثبت نظر کلیک کرده است
                return Json(new { status = "fail" });
            }
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
