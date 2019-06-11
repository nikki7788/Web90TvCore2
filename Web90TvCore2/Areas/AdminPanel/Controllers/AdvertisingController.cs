using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models;
using Web90TvCore2.Models.Service;
using Web90TvCore2.Models.UnitOfWork;
using Web90TvCore2.PublicClass;
using Web90TvCore2.services;

namespace Web90TvCore2.Areas.AdminPanel.Controllers
{
    /// <summary>
    /// تبلیغات
    /// </summary>

    [Area("AdminPanel")]
    public class AdvertisingController : Controller
    {

        #region ################ Dependencies ############################

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadingFileService _uploadingFile;
        private readonly IAdvertiseService _advService;



        public AdvertisingController(IUnitOfWork unitOfWork, IUploadingFileService uploadingFile
            , IAdvertiseService advService)
        {

            _unitOfWork = unitOfWork;
            _uploadingFile = uploadingFile;
            _advService = advService;
        }

        #endregion #############


        #region ################## Actions ##################################

        /// <summary>
        /// نمایش همه تبلیغات
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.ViewTitle = "تبلیغات";
            var model = await _unitOfWork.AdveriseRepUW.Get();
            return View(model);
        }



        /// <summary>
        /// اپلود کردن تصویر شاخص برای تبلیغ
        /// </summary>
        /// <param name="files">فایل دریافتی از کاربر برای آپلودکردن</param>
        /// <returns></returns>
        public async Task<IActionResult> UploadFile(IEnumerable<IFormFile> files)
        {
            if (files.Count() != 0)
            {
                //مسر ذخیره تصویر عادی
                string imagePath = "upload//advImage//";

                //دریافت نام فایل آپلود شده از لایه سرویس و متد آپلود کردن تصویر
                string fileName = await _uploadingFile.UploadFiles(files, imagePath, null);

                return Json(new { status = "success", message = "تصویر با موفقیت آپلود شد", imageName = fileName });
            }

            //اگر تصویری برای اپلود انتخاب نشود
            return Json(new { status = "empty", message = "تصویری برای آپلود انتخاب نشده است" });
        }




        /// <summary>
        /// ایجاد تبلیغ متد خواندنی - نمایش ویو ایجاد
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ViewTitle = "ایجاد تبلیغ جدید";
            return View();
        }




        /// <summary>
<<<<<<< HEAD
        /// ایجاد تبلیغ
=======
        /// ایجاد تبلیغ متد پست
>>>>>>> 42a28b29056cd534ef456fc291d73933b2826a4d
        /// </summary>
        /// <param name="model">مدل دریافتی از ویو </param>
        /// <returns></returns>
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateConfirm(Advertise model)
        {

            if (ModelState.IsValid)
            {

                ///تبدیل فونت و ارقام فارسی تاریخ به انگلیسی برای ذخیره در دیتابیس
                model.FromDate = ConvertFaToEnDigit.ToEnDigit(model.FromDate);
                model.ToDate = ConvertFaToEnDigit.ToEnDigit(model.ToDate);


                await _unitOfWork.AdveriseRepUW.Create(model);
                await _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                // ---------------اگر ولیدیشن رعایت نشده بود-------

                //-----اگر ولیدیشن رعایت نشده بود عکس آپلود شده دوباره نمایش داده شود ------------------
                //و نام ان برای ذخیره در دیتابیس بماند و نیاز ب اپدیت مجدد نباشد
                if (model.GifPath != null)
                {
                    // این روش بدون کوکی است
                    //در کنترلر یوزر و اکشن ایجاد از کوکی استفاده کرده ام
                    ViewBag.ImgNM = model.GifPath;
                }
                //---------------------------------------------------------
                //ViewBag.ViewTitle = "ایجاد تبلیغ جدید";
                return View(model);
            }


            //todo: و تغییر وضعیت تبلیغ به نمایش یا عدم نمایش و حذف تبایغ  و حذف فاییل از روت سایت



        }


        /// <summary>
        /// تغییر وضعیت نمای تبلیغ در سایت - متد خواندنی
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ChangeStatus()
        {
            return PartialView("_ChangeStatusPartial");
        }

        /// <summary>
        /// تغییر وضعیت نمای تبلیغ در سایت - متد پست
        /// </summary>
        /// <param name="id">شناسه تبلیغ</param>
        /// <returns></returns>
        [HttpPost, ActionName("ChangeStatus")]
        public async Task<IActionResult> ChangeStatusConfirm(int Id)
        {
            await _advService.ChangeStatus(Id);
            return RedirectToAction("Index");

        }



        /// <summary>
        ///نمایش مودال حذف تبلیغ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete()
        {
            return PartialView("_DeletePartial");
        }



        /// <summary>
        /// حذف تبلیغ - متد پست
        /// </summary>
        /// <param name="id">شناسه تبلیغ</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int Id)
        {
            ///todo: transaction پیاده سازی 
            ///برای اینکه اگر حذف از روت یا حذف تبلیغ هردوانجام شوپ
           
         
            
            ///حذف تصویر تبلیغ از روت سایت
            await _advService.DeleteRootFile(Id);

            ///حذف تبلیغ از سایت
            await _unitOfWork.AdveriseRepUW.DeletById(Id);
            await _unitOfWork.AdveriseRepUW.Save();

          

            return RedirectToAction(nameof(Index));
        }



        #endregion ########################

    }
}