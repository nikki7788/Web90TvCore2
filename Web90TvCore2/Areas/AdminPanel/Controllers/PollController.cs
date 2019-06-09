using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models;
using Web90TvCore2.Models.UnitOfWork;
using Web90TvCore2.Models.ViewModels;

namespace Web90TvCore2.Areas.AdminPanel.Controllers
{
    /// <summary>
    /// نطرسنجی
    /// </summary>
    public class PollController : Controller
    {

        #region ######## Dependencies  ##########################
        private readonly IUnitOfWork _unitOfWork;

        public PollController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion #####################

        #region ######## actions  ##########################   
        
        /// <summary>
        /// نمایش تمام نطرسنجی ها
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
           IEnumerable<Poll> model=await _unitOfWork.pollRepoUW.Get();
            return View(model);
        }



        /// <summary>
        /// نمایش ویوی ایجاد نطرسنجی
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost,ActionName("Create")]
        public async Task<IActionResult> CreateConfirm(AddPollViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
        #endregion#########################
    }
}
