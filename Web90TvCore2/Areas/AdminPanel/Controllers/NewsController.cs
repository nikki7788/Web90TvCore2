using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models;
using Web90TvCore2.Models.UnitOfWork;
using Web90TvCore2.services;

namespace Web90TvCore2.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class NewsController : Controller
    {
        #region ############## depenencies ###############################################

        private readonly IUnitOfWork _iUintOfWork;

        private readonly IUploadingFileService _uploadingFile;

        private readonly UserManager<ApplicationUsers> _userManager;


        public NewsController(IUnitOfWork iUnitOfWork, IUploadingFileService uploadingFile, UserManager<ApplicationUsers> userManager)
        {
            _iUintOfWork = iUnitOfWork;
            _uploadingFile = uploadingFile;
            _userManager = userManager;
        }

        #endregion ################################

        #region ################################ Actions ###################################### 



        /// <summary>
        /// نمایش لیست خبر ها
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.viewTitle = "  لیست خبر ها";
            var model = await _iUintOfWork.NewsRepUW.Get();
            return View(model);
        }




        /// <summary>
        /// get method ----
        /// نمایش صفحه ایجاد خبر جدید
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //برای برگرداندن مقادیر دسته بندی برای نمایش در کمبو باکس
            ViewBag.CategoryList = await _iUintOfWork.CategoryRepUW.Get();

            //بدست اوردن کاربری (و آیدی او) که لاگین است و خبر را ایجاد کرده
            ViewBag.UsersId = _userManager.GetUserId(User);

            ViewBag.viewTitle = "  افزودن خبر";
            return View();
        }





        /// <summary>
        /// اپلود کردن تصویر شاخص برای خبر
        /// </summary>
        /// <param name="files">فایل دریافتی از کاربر برای آپلودکردن</param>
        /// <returns></returns>
        public async Task<IActionResult> UploadFile(IEnumerable<IFormFile> files)
        {
            if (files.Count() != 0)
            {
                //مسر ذخیره تصویر عادی
                string imagePath = "upload//indexImage//";

                //دریافت نام فایل آپلود شده از لایه سرویس و متد آپلود کردن تصویر
                string fileName = await _uploadingFile.UploadFiles(files, imagePath, null);

                return Json(new { status = "success", message = "تصویر با موفقیت آپلود شد", imageName = fileName });
            }

            //اگر تصویری برای اپلود انتخاب نشود
            return Json(new { status = "empty", message = "تصویری برای آپلود انتخاب نشده است" });
        }





        /// <summary>
        /// ایجاد خبر جدید----
        /// متد پست
        /// </summary>
        /// <returns></returns>
        ///  هم وجود دارد تگ هارا روی ان ست میکنیم و در پراپرتی های ان ذخیره میشود و از ان استفاده میکنیم News ورودی های زیر را هم میشتد در نطر گرفت ولی چون تگی که این ها در ان ذخیره شده در کلاس 
        /// (News model, string indexImage, string userId)
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateConfirm(News model)
        {

            if (ModelState.IsValid)
            {
                //if (imageName == null)
                if (model.IndexImage == null)
                {
                    //عکس پیش فرض
                    model.IndexImage = "defaultpic.jpg";
                }
                //else
                //{
                //    model.IndexImage = imageName;
                //}
                try
                {
                    News news = new News
                    {
                        NewsId = model.NewsId,
                        Title = model.Title,
                        Abstract = model.Abstract,
                        Content = model.Content,
                        NewsDate = model.NewsDate,
                        NewsTime = model.NewsTime,
                        CategoryId = model.CategoryId,
                        UserId = model.UserId,
                        IndexImage = model.IndexImage
                    };
                    await _iUintOfWork.NewsRepUW.Create(news);
                    await _iUintOfWork.Save();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            // ---------------اگر ولیدیشن رعایت نشده بود-------

            //برای برگرداندن مقادیر دسته بندی برای نمایش در کمبو باکس
            ViewBag.CategoryList = await _iUintOfWork.CategoryRepUW.Get();

            //بدست اوردن کاربری (و آیدی او) که لاگین است و خبر را ایجاد کرده
            ViewBag.UsersId = _userManager.GetUserId(User);

            //-----اگر ولیدیشن رعایت نشده بود عکس آپلود شده دوباره نمایش داده شود ------------------
            //و نام ان برای ذخیره در دیتابیس بماند و نیاز ب اپدیت مجدد نباشد
            if (model.IndexImage != null)
            {
                // این روش بدون کوکی است
                //در کنترلر یوزر و اکشن ایجاد از کوکی استفاده کرده ام
                ViewBag.ImgNM= model.IndexImage;
            }
            //---------------------------------------------------------

            return View(model);
        }






        /// <summary>
        /// نمایش ویرایش خبر
        /// متد Get
        /// </summary>
        /// <param name="id">وقتی روی ویرایش کیلیک میکنیم آیدی خبر ارسال میشود به اکشن و اینجا دریافت میکنیم</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //پیداکردن خبر در دیتابیس
            News model = await _iUintOfWork.NewsRepUW.GetById(id);
            if (model != null)
            {
                //برای برگرداندن مقادیر دسته بندی برای نمایش در کمبو باکس
                ViewBag.CategoryList = await _iUintOfWork.CategoryRepUW.Get();

                //بدست اوردن کاربری (و آیدی او) که لاگین است و خبر را ایجاد کرده
                ViewBag.UsersId = _userManager.GetUserId(User);

                ViewBag.viewTitle = "  ویرایش خبر";
                return View(model);

            }
            else
            {
                return NotFound();
            }
        }




        /// <summary>
        /// ارسال اطلاعات خبر و ثبت در دیتابیس
        ///  Post
        /// </summary>
        /// <param name="model">مودل واطالاعات  دریافتی از ویو </param>
        /// را به صورت مخفی ایجاد کنیم تا مقدار دهی شود NewsId  نال نباشد و مقدار داشته باشد باید در ویو تگ  model  در NewsId برای اینکه مقدار  
        /// <param name="id">آیدی خبر</param>
        ///  دریافت میشود get ايدی بطور خودکار از اکشن
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirm(News model, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // +* را به صورت مخفی در ویو ننویسیم میتوانیم اینگنه مقدار آیدی خبر را داخل مودل بریزیم NewsId اگر تگ 
                    // model.NewsId = id;

                    _iUintOfWork.NewsRepUW.Update(model);
                    await _iUintOfWork.Save();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    //  برای برگرداندن مقادیر دسته بندی برای نمایش در کمبو باکس
                    ViewBag.CategoryList = await _iUintOfWork.CategoryRepUW.Get();

                    // بدست اوردن کاربری(و آیدی او) که لاگین است و خبر را ایجاد کرده
                    ViewBag.UsersId = _userManager.GetUserId(User);

                    ViewBag.viewTitle = "  ویرایش خبر";
                    return View(model);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion #############################
    }
}