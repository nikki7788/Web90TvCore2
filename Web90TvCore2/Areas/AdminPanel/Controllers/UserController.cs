using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using InsertShowImage;
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
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model, string imageName)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //-----------------اگر کاربر تصویری وارد نکرد-----------------------------

                    if (imageName == null)
                    {
                        model.UserImage = "closedEyes.jpg";
                    }
                    else
                    {
                        model.UserImage = imageName;
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
                        Gender = model.Gender
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //return Json(new {status });
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    throw;
                    //ModelState.AddModelError("Exception","خطایی بوجود آمده است");

                }


            }
            ViewBag.viewTitle = "  افزودن کاربر جدید";
            return View(model);
        }
        #endregion ################################


    }
}