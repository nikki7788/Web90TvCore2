using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models.UnitOfWork;

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
        public AdvertisingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion #############


        #region ################## Actions ##################################

        /// <summary>
        /// نمایش همه تبلیغات
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var model = await _unitOfWork.AdveriseRepUW.Get();
            return View(model);
        }

        #endregion ########################

    }
}