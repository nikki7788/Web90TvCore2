using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models.UnitOfWork;

namespace Web90TvCore2.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class NewsController : Controller
    {
        #region ############## depenencies ###############################################

        private readonly IUnitOfWork _iUintOfWork;
        public NewsController(IUnitOfWork iUnitOfWork)
        {
            _iUintOfWork = iUnitOfWork;
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
        public IActionResult Create()
        {
            ViewBag.viewTitle = "  افزودن خبر";
            return View();
        }










        #endregion #############################
    }
}