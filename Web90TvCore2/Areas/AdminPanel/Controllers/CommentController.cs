﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web90TvCore2.Models;
using Web90TvCore2.Models.Service;
using Web90TvCore2.Models.UnitOfWork;

namespace Web90TvCore2.Areas.AdminPanel.Controllers
{

    /// <summary>
    /// کنترلر برای عملیات روی نظرات کاربران
    /// </summary>
    [Area("AdminPanel")]
    [Authorize(Roles = "Comment")]
    public class CommentController : Controller
    {

        #region ################################## Dependencies ###################################################


        private readonly IUnitOfWork _unitOfWork;

        private readonly ICommentService _commentService;

        public CommentController(IUnitOfWork unitOfWork, ICommentService commentService)
        {
            _unitOfWork = unitOfWork;
            _commentService = commentService;
        }

        #endregion #################################

        #region ################################## Dependencies ###################################################



        /// <summary>
        /// نمایش نطرات کاربران برای خبر ها
        /// </summary>
        /// <returns></returns>
        /// نام جدول کلید خارجی در جدول نظرات TblNews
        /// یک جوین با جدول خبر زده میشود تا شناسه خبر را هم بدست اوریم (eager loding ===include)
        public async Task<IActionResult> Index()
        {
            ViewBag.ViewTitle = "لیست نظرات";

            //var model=  await _unitOfWork.CommentRepUW.Get(null, null,"TblNews");
            //return View(model);
            return View(await _unitOfWork.CommentRepUW.Get(null,c=>c.OrderByDescending(cm=>cm.Id), "TblNews"));
        }




        /// <summary>
        /// نمایش تایید یا رد نمایش نظر در سایت
        /// </summary>
        /// <param name="Id">شناسه خبر</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AcceptOrReject(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Comment cm = await _unitOfWork.CommentRepUW.GetById(Id);
            if (cm == null)
            {
                return NotFound();
            }
            if (cm.status == true)
            {
                ViewBag.Title = " رد و جلوگیری از نمایش نظر";
            }
            else
            {
                ViewBag.Title = "تایید و نمایش نظر";

            }
            return PartialView("_AcceptOrRejectPartial", cm);
        }





        /// <summary>
        /// متد پست تایید یارد نمایش نظرات
        /// </summary>
        /// <param name="Id">شناسه نظر</param>
        /// <returns></returns>
        [HttpPost,ActionName("AcceptOrReject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptOrRejectConfirm(int Id)
        {

            await _commentService.AcceptOrReject(Id);
            return RedirectToAction(nameof(Index));

        }





        /// <summary>
        /// نمایش پارشال ویو حذف که حاوی پیام نطر است
        /// </summary>
        /// <param name="id">شناسه نطر</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment cm = await _unitOfWork.CommentRepUW.GetById(id);

            if (cm == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", cm);
        }




        /// <summary>
        /// متد پست حذف
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _unitOfWork.CommentRepUW.DeletById(id);
            await _unitOfWork.CommentRepUW.Save();

            return RedirectToAction(nameof(Index));

            //todo:وقتی نظری را حذف میکنیم اگر نظر اصلی باشد تمام نظرات پاسخ به ان خودکار و بهصو.رت ابشاری حذف شوند
        }
        #endregion ##############################
    }
}