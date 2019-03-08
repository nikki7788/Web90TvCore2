using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models;
using Web90TvCore2.Models.UnitOfWork;

namespace Web90TvCore2.Areas.AdminPanel.Controllers
{

    [Area("AdminPanel")]
    public class UserController : Controller
    {
        #region ############## depenencies ###############################################

        private readonly IUnitOfWork _iUintOfWork;
        public UserController(IUnitOfWork iUnitOfWork)
        {
            _iUintOfWork = iUnitOfWork;
        }

        #endregion ################################

        #region ###################### actions  ###############################################

        public IActionResult Index()
        {
            ViewBag.viewTitle = "  لیست کاربران";
            var model = _iUintOfWork.UserManagerUW.Get();
            return View();
        }

        #endregion ################################


    }
}