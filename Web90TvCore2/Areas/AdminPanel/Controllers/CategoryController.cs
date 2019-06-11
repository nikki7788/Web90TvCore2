using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web90TvCore2.Models;
using Web90TvCore2.Models.UnitOfWork;

namespace Web90TvCore2.Areas.AminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Category")]
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
                //todo: به صورت اصولی catch درست کردن قسمت
                try
                {
                    await _iUintOfWork.CategoryRepUW.Create(model);
                    await _iUintOfWork.Save();

                    ViewBag.viewTitle = " ایجاد دسته بندی";
                    ViewBag.SuccessMessage = "اطلاعات با موفقیت ثبت شد";
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
        public async Task<IActionResult> Edit(Category model, int categoryId)
        {

            if (categoryId != model.CategoryId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //todo: به صورت اصولی catch درست کردن قسمت
                try
                {
                    _iUintOfWork.CategoryRepUW.Update(model);
                    await _iUintOfWork.Save();

                    ViewBag.SuccessMessage = "اطلاعات با موفقیت ویرایش شد";
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
            }

            return View(model);
        }



        /// <summary>
        /// حذف دسته بندی متد خواندنی
        /// نمایش اطلاعات موردی  که    میخوااهیم  حذف کنیم
        /// </summary>
        /// <param name="id">آی دی موردی که می خواهیم حذف کنیم</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category category = await _iUintOfWork.CategoryRepUW.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            //برای فرستادن ورژن بوت استرپ به مودال که باتجه به ورژن بوت استرپ دستورات را بیاورد
            //ModalHedear class روش ۲ در داخل 
            //ViewBag.bootstrapVers = 4;
            return PartialView("_DeletePartial", category);
        }


        /// <summary>
        /// حذف دسته بندی متد نوشتنی
        /// 
        /// </summary>
        /// <param name="id">آیدی را از متد خواندنی دریافت میکنیم</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                await _iUintOfWork.CategoryRepUW.DeletById(id);
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
        #endregion#################################

    }
}