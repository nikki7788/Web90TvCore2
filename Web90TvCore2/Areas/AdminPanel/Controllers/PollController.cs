using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models;
using Web90TvCore2.Models.UnitOfWork;
using Web90TvCore2.Models.ViewModels;
using Web90TvCore2.PublicClass;


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
            IEnumerable<Poll> model = await _unitOfWork.PollRepoUW.Get();
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
        /// ایجاد نظر سنجی
        /// </summary>
        /// <param name="model">مدل دریافتی از ویو</param>
        /// <returns></returns>
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateConfirm(AddPollViewModel model)
        {
            if (ModelState.IsValid)
            {
                ///-------------کنترل اینکه از قبل نظرسنجی فعالی موجود نباشد--------------
                IEnumerable<Poll> activePoll = await _unitOfWork.PollRepoUW.Get(p => p.Active == true);
                if (activePoll.Count() > 0)
                {
                    ViewBag.message = "از قبل یک نظرسنجی فعال وجود دارد.";
                    return View();
                }
                ///-----------------------------------------------------------
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {

                        ////////////////----- ایجاد  متن نطرسنجی---------//////////////////////-
                        Poll poll = new Poll
                        {
                            Question = model.Question,
                            PollStartDate = PersianDateAndTime.PersianDateNow().Item1,
                            Active = true
                        };
                        await _unitOfWork.PollRepoUW.Create(poll);
                        await _unitOfWork.Save();

                        /////////////////////// ثبت گزینه های نطرسنجی /////////////////////////

                        ///هرجا در قسمت گزینه های نطر سنحی اینتر زده شد وبه خط جدید رفت با دستور زیر یک گزینه در نطر گرفته میشود
                        string[] pollopt = model.Answer.Split(new string[] { Environment.NewLine },
                            StringSplitOptions.RemoveEmptyEntries);

                        ///ثبت  گزینه های نطرسنجی در دیتابیس
                        foreach (var item in pollopt)
                        {
                            PollOption pollOption = new PollOption();
                            pollOption.Answer = item;
                            pollOption.VouteCount = 0;
                            pollOption.PollID = poll.PollId;
                            await _unitOfWork.PollOptionRepoUW.Create(pollOption);
                            await _unitOfWork.Save();
                        }
                        ///-----------------------------------------------------------
                        ///درصورت خطا ندادن در طول عملیات این خط کد اجرا و تغییرات در دیتابیس اعمال میشود
                        transaction.Commit();

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ///اگر در هنکام عملیات بالا خطایی بوجود امد تمام عملیات برشگت داده میشود
                        transaction.Rollback();

                        throw ex;

                    }
                }
            }
            ///در صورت رعایت نشدن ولیدیشن ها
            return View(model);

        }


        /// <summary>
        /// نمایش مودال حذف نطر سنجی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Poll poll = await _unitOfWork.PollRepoUW.GetById(Id);
            if (poll == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", poll.Question);

        }



        /// <summary>
        /// حذف نطر سنجی
        /// </summary>
        /// <param name="Id">شناسه نطرسنجی</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int Id)
        {
            using (var transacation = _unitOfWork.BeginTransaction())
            {
                try
                {
                    ///حذف پاسخ ها
                    List<PollOption> pollOption = _unitOfWork.PollOptionRepoUW.Get(po => po.PollID == Id).Result.ToList();

                    foreach (var item in pollOption)
                    {
                        await _unitOfWork.PollOptionRepoUW.DeletById(item.PolloptionID);
                        await _unitOfWork.Save();
                    }


                    //حذف نظرسنجی
                    await _unitOfWork.PollRepoUW.DeletById(Id);
                    await _unitOfWork.Save();

                    ///درصورت خطا ندادن در طول عملیات این خط کد اجرا و تغییرات در دیتابیس اعمال میشود
                    transacation.Commit();

                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    transacation.Rollback();
                    throw ex;
                }


            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task< IActionResult> ClosePoll(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Poll pl =await _unitOfWork.PollRepoUW.GetById(Id);
            if (pl == null)
            {
                return NotFound();
            }
            return PartialView("_closepoll", pl);
        }

        public IActionResult ClosePoll(int id)
        {
            _ipr.ClosePoll(id);
            return RedirectToAction("Index");
        }

        #endregion#########################
    }
}

