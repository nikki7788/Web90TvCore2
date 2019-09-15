using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models;
using Web90TvCore2.Models.UnitOfWork;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web90TvCore2.Areas.AdminPanel.Controllers
{

    /// <summary>
    /// تنظیمات سایت
    /// </summary>
    [Area("AdminPanel")]
    [Authorize(Roles ="SiteSetting")]
    public class SiteSettingController : Controller
    {
        #region ###################### Dependencies ################################

        private readonly IUnitOfWork _unitOfWork;
        public SiteSettingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion #############
        #region ####################### Actions ###############################


        /// <summary>
        /// نمایش صفحه تنظیمات سایت
        /// </summary>
        /// <returns></returns>
        /// فقط یک رکورد دارد-
        [HttpGet]
        public IActionResult Index()
        {
            SiteSetting setting = _unitOfWork.SiteSettingRepoUW.Get().Result.SingleOrDefault();

            return View(setting);
        }


        /// <summary>
        /// ویرایش یا ایجاد تنظیمات سایت
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ActionName("SetSetting")]
        public async Task<IActionResult> SetSettingConfirm(SiteSetting model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    ///------ اگر درحالت ویرایش بودیم --------
                  await  _unitOfWork.SiteSettingRepoUW.Create(model);
                    await _unitOfWork.Save();
                }
                else
                {
                    ///-----------  درحالت ایجاد رکورد جدید هستیم -----------
                    _unitOfWork.SiteSettingRepoUW.Update(model);
                    await _unitOfWork.Save();

                }

                ViewBag.SuccessMessage = "اطلاعات با موفقیت ویرایش شد";
            }

            return View("Index",model);
        }
        #endregion #############
    }
}
