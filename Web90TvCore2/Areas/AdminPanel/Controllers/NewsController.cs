using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


        public NewsController(IUnitOfWork iUnitOfWork, IUploadingFileService uploadingFile)
        {
            _iUintOfWork = iUnitOfWork;
            _uploadingFile = uploadingFile;
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

            ViewBag.CategoryList =await _iUintOfWork.CategoryRepUW.Get();

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
                string fileName = await _uploadingFile.UploadFiles(files, imagePath,null);

                return Json(new { status = "success", message = "تصویر با موفقیت آپلود شد", imageName = fileName });
            }

            //اگر تصویری برای اپلود انتخاب نشود
            return Json(new { status = "empty", message = "تصویری برای آپلود انتخاب نشده است" });
        }




        [HttpPost,ActionName("Create")]
        public IActionResult CreateConfirm()
        {
            return View();
        }




        #endregion #############################
    }
}