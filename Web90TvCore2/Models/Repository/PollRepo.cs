using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models.Service;
using Web90TvCore2.PublicClass;

namespace Web90TvCore2.Models.Repository
{
    public class PollRepo: IPollService
    {

        #region ############## Dependncies ###########################


        private readonly ApplicationDbContext _Context;

        public PollRepo(ApplicationDbContext context)
        {
            _Context = context;
        }
        #endregion #####################################
        #region ################## Methods #####################



        /// <summary>
        /// بستن نظرسنجی
        /// </summary>
        /// <param name="id"></param>
        public void ClosePoll(int id)
        {
            //var result = (from p in _Context.Polls where p.PollId == id select p);
            var result = _Context.Polls.Where(p=>p.PollId==id);
            var currentPoll = result.FirstOrDefault();

            if (result.Count() != 0)
            {
                currentPoll.Active = false;
                currentPoll.PollEndDate = PersianDateAndTime.PersianDateNow().Item2;

                _Context.Polls.Attach(currentPoll);
                _Context.Entry(currentPoll).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _Context.SaveChanges();
            }
        }

        /// <summary>
        /// ثبت رای نظر سنجی
        /// </summary>
        /// <param name="id">شناسه گزینه نظرسنجی</param>
        public void SetVote(int id)
        {
            // var result = (from p in _Context.PollOptions where p.PolloptionID == id select p);
            var result = _Context.PollOptions.Where(p => p.PolloptionID == id);
            var currentPolloption = result.FirstOrDefault();

            if (result.Count() != 0)
            {
                currentPolloption.VouteCount++;
                _Context.PollOptions.Attach(currentPolloption);
                _Context.Entry(currentPolloption).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _Context.SaveChanges();
            }
        }
        #endregion  #####################################
    }
}
