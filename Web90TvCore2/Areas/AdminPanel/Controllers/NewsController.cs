using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web90TvCore2.Models;
using Web90TvCore2.Models.UnitOfWork;
using Web90TvCore2.Models.ViewModels;
using Web90TvCore2.PublicClass;
using Web90TvCore2.services;

namespace Web90TvCore2.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "News")]
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
            //برای ورودی که جوین مارا در لایه سرویسانجام دهد باید نام جدولی که میخاهیم با ان جوین صورت گیرد بنویسیم و این نام حتما باید 
            //    دادیم باشد.دراینجا در جدول در جدول خبر در قسمت مرجع کلید خارجی navigation property  نامی که در   
            //در اینجا شرطی اعمال میگنیم وفیلتر میکنیم که کاربرخبرهای ایجاد شده توسط خود ش را ببیند نه تمام خبرها
            //اگر آيدی کاربر برابر کاربری که که الان لاگین است خبرهایش رابیاور
            var model = await _iUintOfWork.NewsRepUW.Get(n => n.UserId == _userManager.GetUserId(User), null, "Category");
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
        ///  /// (News model, string indexImage, string userId)
        ///  ///  هم وجود دارد تگ هارا روی ان ست میکنیم و در پراپرتی های ان ذخیره میشود و از ان استفاده میکنیم News ورودی های زیر را هم میشتد در نطر گرفت ولی چون تگی که این ها در ان ذخیره شده در کلاس 
        /// <param name="model">مدل دریافتی از فرم داخل ویوکه توسط کاربر پر شده است</param>
        /// <param name="r1">نوع خبر را برمیگرداند</param>
        /// مقدار دکمه های رادیو را برمیگرداند---
        /// شده اند name="r1" که در ویو با  
        /// رادیو باتن ها اطلاعاتشان توسط مدل برگردانده نمیشود و در مدل ذیره نمیشوند باید دستی مقدار دهی کنیم
        /// <returns></returns>
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateConfirm(News model, byte r1)
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
                        NewsPlace = model.NewsPlace,
                        Abstract = model.Abstract,
                        Content = model.Content,
                        NewsDate = model.NewsDate,
                        NewsTime = model.NewsTime,
                        CategoryId = model.CategoryId,
                        UserId = model.UserId,
                        IndexImage = model.IndexImage,
                        NewsType = r1
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
                ViewBag.ImgNM = model.IndexImage;
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
        /// <param name="r1">نوع خبر را برمیگرداند</param>
        /// مقدار دکمه های رادیو را برمیگرداند---
        /// شده اند name="r1" که در ویو با  
        /// رادیو باتن ها اطلاعاتشان توسط مدل برگردانده نمیشود و در مدل ذیره نمیشوند باید دستی مقدار دهی کنیم
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirm(News model, int id, byte r1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //مقدار دهی نوع خبر
                    //رادیو باتن ها اطلاعاتشان توسط مدل برگردانده نمیشود و در مدل ذیره نمیشوند باید دستی مقدار دهی کنیم
                    model.NewsType = r1;
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



        /// <summary>
        /// حذف خبر--  متد خواندنی
        /// نمایش اطلاعات موردی  که    میخوااهیم  حذف کنیم
        /// </summary>
        /// <param name="id">آی دی موردی که می خواهیم حذف کنیم</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var model = await _iUintOfWork.NewsRepUW.GetById(id);
                if (model != null)
                {
                    return PartialView("_DeletePartial", model);
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
        /// حذف خبر متد نوشتنی
        /// </summary>
        /// <param name="id">آیدی را از متد خواندنی دریافت میکنیم</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                await _iUintOfWork.NewsRepUW.DeletById(id);
                await _iUintOfWork.CategoryRepUW.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw ex;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction(nameof(Index));

        }

 
        #endregion #############################
    }
}