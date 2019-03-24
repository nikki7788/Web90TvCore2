using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using InsertShowImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web90TvCore2.Models;
using Web90TvCore2.Models.UnitOfWork;
using Web90TvCore2.Models.ViewModels;

namespace Web90TvCore2.Areas.AdminPanel.Controllers
{

    [Area("AdminPanel")]
    [Authorize(Roles ="User")]
    public class UserController : Controller
    {
        #region ############## depenencies ###############################################

        private readonly IUnitOfWork _iUintOfWork;

        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<ApplicationUsers> _userManager;

        public UserController(IUnitOfWork iUnitOfWork, IHostingEnvironment appEnvironment, UserManager<ApplicationUsers> userManager)
        {
            _iUintOfWork = iUnitOfWork;

            _appEnvironment = appEnvironment;

            _userManager = userManager;
        }

        #endregion ################################

        #region ###################### actions  ###############################################

        /// <summary>
        /// نمایش لیست کاربران
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.viewTitle = "  لیست کاربران";
            var model = await _iUintOfWork.UserManagerUW.Get();
            return View(model);
        }



        /// <summary>
        /// اپلود کردن تصویر پروفایل کاربر
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<IActionResult> UploadFile(IEnumerable<IFormFile> files)
        {
            //todo:catch - مدیریت خطا به درستی انجام شود
            //todo:using -try catch - ایا هنگام استفاده از یوزینگ ترای کچ هم نیاز است
            if (files.Count() != 0)
            {


                try
                {
                    var upload = Path.Combine(_appEnvironment.WebRootPath, "upload//userImage//normalImage//");
                    var fileName = "";

                    foreach (var file in files)
                    {

                        fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

                        try
                        {
                            using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                        }
                        catch (NotSupportedException)
                        {

                            throw;
                        }
                        catch (SecurityException)
                        {

                            throw;
                        }
                        catch (FileNotFoundException)
                        {

                            throw;
                        }
                        catch (DirectoryNotFoundException)
                        {

                            throw;
                        }
                        catch (PathTooLongException)
                        {

                            throw;
                        }

                        catch (IOException ex)
                        {

                            throw ex;
                        }
                        catch (ArgumentNullException)
                        {

                            throw;
                        }

                        ////---------------------- تغییر سایز عکس و ذخیره برای حالت  بندانگشتی ----------------------------////

                        ImageResizer imgThumb = new ImageResizer();
                        imgThumb.Resize(upload + fileName, Path.Combine(_appEnvironment.WebRootPath, "upload//userImage//thumbnailImage//") + fileName);

                        //----------------------------------------------//
                    }

                    return Json(new { status = "success", message = "تصویر با موفقیت آپلود شد", imageName = fileName });
                }



                catch (ArgumentNullException)
                {

                    throw;
                }
                catch (ArgumentException)
                {

                    throw;
                }
                catch (Exception)
                {
                    //ModelState.AddModelError("UserImage", "خطایی رخ داده است");
                    throw;
                }
            }

            //اگر تصویری برای اپلود انتخاب نشود
            return Json(new { status = "empty", message = "تصویری برای آپلود انتخاب نشده است" });
        }





        /// <summary>
        /// Get Method
        /// نمایش صفحه ایجادکاربر
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.viewTitle = "  افزودن کاربر جدید";
            return View();
        }



        /// <summary>
        /// post method
        ///ثبت و ایجاد کاربر در دیتابیس 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model, string imageName)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //-------بررسی آپلود شدن عکس  ---------
                    //-------بررسی آپلود شدن عکس ---------
                    if (imageName != null)
                    {
                        //اگر عکس اپلود شده بود نام ان را در پراپرتی نام عکس یوزر بریز
                        model.UserImage = imageName;
                    }
                    else if (Request.Cookies["ImgCreate"] != null)
                    {
                        //عکس  وقتی که  ولیدیشن رعایت نشده بود  را می اورد
                        string cookieImg = Request.Cookies["ImgCreate"].ToString();
                        model.UserImage = cookieImg;
                        Response.Cookies.Delete("ImgCreate");
                    }
                    else if (imageName == null)
                    {
                        //اگر عکس اپلود نشده بود نام تصویر پیش فرض را در پراپرتی نام عکس یوزر بریز
                        model.UserImage = "closedEyes.jpg";
                    }

                    //-------------------------------------

                    ApplicationUsers user = new ApplicationUsers
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        UserName = model.UserName,
                        Email = model.Email,
                        BirthDayDate = model.BirthDayDate,
                        UserImagePath = model.UserImage,
                        Gender = model.Gender,
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //return Json(new {status });
                        return RedirectToAction(nameof(Index));
                    }
                    if(!result.Succeeded)
                    {
                        //دو نوع خطا ممکن است بجود بیاید یکی برای رمز بور ویکی نام کاربری
                        //خطای رمز عبور را با ولیدیشن گذاشتن از طریق عبارات باقاعده  میدیریت کردیم
                        //خطای تکراری بودن نام کاربری را از طریق زیر
                        ModelState.AddModelError("UserName", "نام کاربی از قبل وجود دارد");
                     
                    }
                }
                catch (Exception)
                {
                    throw;
                    //ModelState.AddModelError("Exception","خطایی بوجود آمده است");

                }


            }
            //------------------------** استفاده از کوکی برای ذخیره نام تصویر برای بعد از رفرش شدن صفحه بخاطر ولیدیشن ----------------------
            if (Request.Cookies["ImgCreate"] == null)
            {
                //برای اولین بار لیدیشن ها رعایت نمیشوود
                if (imageName != null)
                {

                    //اگر عکسی اپدیت شده بود عکس جدید را نمایش دهد
                    model.UserImage = imageName;

                    string cookieImageName = imageName;
                    Response.Cookies.Append("ImgCreate", cookieImageName, new CookieOptions { Expires = DateTime.Now.AddMinutes(30) });

                }
            }
            else if (Request.Cookies["ImgCreate"] != null)
            {
                //وقتی برای بار چندم ولیدیشن ها رعایت نمیشود 

                if (imageName != null)
                {

                    //اگر عکسی اپدیت شده بود عکس جدید را نمایش دهد
                    model.UserImage = imageName;

                    Response.Cookies.Delete("ImgCreate");
                    string cookieImageName = imageName;
                    Response.Cookies.Append("ImgCreate", cookieImageName, new CookieOptions { Expires = DateTime.Now.AddMinutes(30) });

                }
                else
                {

                    //-----اگر تصویر ویرایش نشده بود و عکسی اپلود نشده بود همان عکس موجود در دیتابیس و قبلی را نمایش دهد---------
                    string cookieImg = Request.Cookies["ImgCreate"].ToString();
                    model.UserImage = cookieImg;
                }

            }
            //----------------،***-----------------------
            ViewBag.viewTitle = "  افزودن کاربر جدید";
            return View(model);
        }



        /// <summary>
        /// Get Method
        ///---- نمایش صفحه ویرایش کاربر
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id != null)
            {
                //finding the user
                ApplicationUsers user = await _userManager.FindByIdAsync(id);

                if (user != null)
                {
                    //Initializing the viewmodel of user by user variable  and showing the user's information in the view
                    EditUserViewModel model = new EditUserViewModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        BirthDayDate = user.BirthDayDate,
                        Gender = user.Gender,
                        UserImage = user.UserImagePath
                    };

                    ViewBag.viewTitle = "  ویرایش کاربر";
                    return View(model);
                }
                else
                {
                    return NotFound();

                }
            }
            else
            {
                return NotFound();

            }

        }



        /// <summary>
        /// post method
        ///ویرایش کاربر در دیتابیس
        /// </summary>
        /// <param name="model">ویومدل حاوی اطلاعات ویرایش شده کاربر را برمیگرداند</param>
        /// <param name="id">شناسه کاربر مورد ویرایش را برمیگرداند</param>
        /// <param name="chekinput">چک بودن یا نبودن چک باکس را برمیگرداند</param>
        /// <param name="imagename">نام عکس اپلود شده را در صورت وجود برمیگرداند</param>
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirm(EditUserViewModel model, string id, string chekinput, string imageName)
        {
            try
            {
                ApplicationUsers user = await _userManager.FindByIdAsync(id);
                if (ModelState.IsValid)
                {
                    //-------بررسی آپلود شدن عکس ---------

                    if (Request.Cookies["ImgEdit"] != null)
                    {
                        //عکس  وقتی که  ولیدیشن رعایت نشده بود  را می اورد
                        string cookieImg = Request.Cookies["ImgEdit"].ToString();
                        model.UserImage = cookieImg;
                        Response.Cookies.Delete("ImgEdit");

                    }
                    if (imageName != null)
                    {
                        //اگر عکس اپلود شده بود نام ان را در پارپتی نام عکس یوزر بریز
                        model.UserImage = imageName;
                    }

                    //----------------------------------------------

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.BirthDayDate = model.BirthDayDate;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Gender = model.Gender;
                    user.UserImagePath = model.UserImage;

                    //-------- بررسی انتخاب شدن و چک وشدن و تیک خوردن چک باکس پلاگین----------------------- 
                    //on=چک بودن و تیک خوردن
                    if (chekinput == "on")
                    {
                        //اگر تیک خورده بود رمز را ریست کنیم
                        //Reset Password
                        //123d@F
                        user.PasswordHash = "AQAAAAEAACcQAAAAEAabKLaDOcVF55N+pqYxMKEsctUlZmrmudfUurx8DtbxZcPv0wXbujbbfg3g2LrYrg==";
                    }
                    //---------------------------------------------------------------

                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                }

                //------------------------** استفاده از کوکی برای ذخیره نام تصویر برای بعد از رفرش شدن صفحه بخاطر ولیدیشن ----------------------
                if (Request.Cookies["ImgEdit"] == null)
                {
                    //برای اولین بار لیدیشن ها رعایت نمیشوود
                    if (imageName == null)
                    {

                        //-----اگر تصویر ویرایش نشده بود و عکسی اپلود نشده بود همان عکس موجود در دیتابیس و قبلی را نمایش دهد---------

                        model.UserImage = user.UserImagePath;

                        string cookieImageName = model.UserImage;
                        Response.Cookies.Append("ImgEdit", cookieImageName, new CookieOptions { Expires = DateTime.Now.AddMinutes(30) });
                    }
                    else
                    {
                        //اگر عکسی اپدیت شده بود عکس جدید را نمایش دهد
                        model.UserImage = imageName;

                        string cookieImageName = imageName;
                        Response.Cookies.Append("ImgEdit", cookieImageName, new CookieOptions { Expires = DateTime.Now.AddMinutes(30) });


                    }
                }
                else if (Request.Cookies["ImgEdit"] != null)
                {
                    //وقتی برای بار چندم ولیدیشن ها رعایت نمیشود 

                    if (imageName != null)
                    {
                        //-----اگر تصویر ویرایش نشده بود و عکسی اپلود نشده بود همان عکس موجود در دیتابیس و قبلی را نمایش دهد---------

                        model.UserImage = imageName;
                        Response.Cookies.Delete("ImgEdit");
                        string cookieImageName = imageName;
                        Response.Cookies.Append("ImgEdit", cookieImageName, new CookieOptions { Expires = DateTime.Now.AddMinutes(30) });

                    }
                    else
                    {
                        //اگر عکسی اپدیت شده بود عکس جدید را نمایش دهد

                        string cookieImg = Request.Cookies["ImgEdit"].ToString();
                        model.UserImage = cookieImg;
                    }

                }
                //----------------،***-----------------------
                ViewBag.viewTitle = "  ویرایش کاربر ";
                return View(model);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        #endregion ################################


    }
}