using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models;
using Web90TvCore2.Models.Service;
using Web90TvCore2.Models.UnitOfWork;
using Web90TvCore2.Models.ViewModels;
using Web90TvCore2.PublicClass;


namespace Web90TvCore2.Areas.AdminPanel.Controllers
{
    /// <summary>
    /// نطرسنجی
    /// </summary>
    [Area("AdminPanel")]
    [Authorize(Roles = "Poll")]
    public class PollController : Controller
    {

        #region ######## Dependencies  ##########################
        private readonly IUnitOfWork _unitOfWork;

        private readonly IPollService _pollService;

        public PollController(IUnitOfWork unitOfWork, IPollService pollService)
        {
            _unitOfWork = unitOfWork;
            _pollService = pollService;
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
                            PollStartDate = PersianDateAndTime.PersianDateNow().Item2,
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
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PollResult(int Id)
        {
            ///------------ آوردن اطلاعات نظرسنجی ----------------------
            var pollResult = _unitOfWork.PollRepoUW.Get(p => p.PollId == Id).Result.Single();


            //نمایش نتایج به صورت نمودار
            List<PollResultViewModel> pollRes = new List<PollResultViewModel>();
            foreach (PollOption vr in await _unitOfWork.PollOptionRepoUW.Get(p => p.PollID == pollResult.PollId))
            {
                ///-------  مقدار دهی ویو مدل نمایش نایج نظر سنجی -------
                PollResultViewModel pRes = new PollResultViewModel()
                {
                    /// حتما باید با حروف کوچک باشد نام پاپرتی ها تا خطا ندهد در نمایش
                    label = vr.Answer ,
                    data = vr.VouteCount
                };
                ///-------- افزودن هرگزینه و نتیجه ان به لیستی از ویو مدل برای ارسال به ویو برای نمایش نتایج -------
                pollRes.Add(pRes);
            }

            ///------- برای اینکه نال به ویو ارسال نشود و باکس نمایش داده شود .نظر سنجی فعال را به ویو ارسال گردیم -------------
            ///---------------  تبدیل لیست ویو مدل نمایش نتایج به فرمت جیسان بای ارسال به ویو  پارشال نظر سنجی ----------------
            ViewBag.getListOfAnswer = JsonConvert.SerializeObject(pollRes);
            return PartialView("_PollResultPartial", pollResult);
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
        /// نمایش مودال بستن نظرسنجی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ClosePoll(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Poll pl = await _unitOfWork.PollRepoUW.GetById(Id);
            if (pl == null)
            {
                return NotFound();
            }
            return PartialView("_ClosePollPatial", pl);
        }


        /// <summary>
        /// بستن نظرسنجی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("ClosePoll")]
        public IActionResult ClosePollConfirm(int Id)
        {
            _pollService.ClosePoll(Id);
            return RedirectToAction("Index");
        }

        #endregion#########################
    }
}

