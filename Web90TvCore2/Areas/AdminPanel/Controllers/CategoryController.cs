using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models;
using Web90TvCore2.Models.UnitOfWork;

namespace Web90TvCore2.Areas.AminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        #region ############## Dependencies ###########################


        private readonly IUnitOfWork _iUintOfWork;

        public CategoryController(IUnitOfWork iUintOfWork)
        {
            _iUintOfWork = iUintOfWork;
        }

        #endregion #################

        #region ################################Actions ###########################################


        /// <summary>
        /// Get Method
        /// نمایش لیست رکورد ها
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.viewTitle = "  لیست دسته بندی";
            return View(await _iUintOfWork.CategoryRepUW.Get());
        }




        /// <summary>
        /// Get Method
        /// نمایش صفحه ایجاد
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.viewTitle = " ایجاد دسته بندی";
            return View();
        }


        /// <summary>
        /// Post Method
        /// اکشن ارسال به دیتابیس و ساخت رکورد
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                await _iUintOfWork.CategoryRepUW.Create(model);
                await _iUintOfWork.Save();

                ViewBag.viewTitle = " ایجاد دسته بندی";
                ViewBag.SuccessMessage = "اطلاعات با موفقیت ثبت شد";
            }

            return View(model);

        }

        /// <summary>
        /// Get Method
        /// نمایش صفحه نمایش ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.viewTitle = " ویرایش دسته بندی";

            return View(await _iUintOfWork.CategoryRepUW.GetById(id));

        }

        /// <summary>
        /// Post Method
        /// اکشن ارسال به دیتابیس و ویرایش رکورد
        /// </summary>
        /// <param name="model"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model, int categoryId)
        {

            if (categoryId != model.CategoryId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _iUintOfWork.CategoryRepUW.Update(model);
                    _iUintOfWork.Save();

                    ViewBag.SuccessMessage = "اطلاعات با موفقیت ویرایش شد";
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return View(model);
        }
        #endregion#################################

    }
}