using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models
{
    /// <summary>
    /// جدول متن پرسش نطرسنجی
    /// </summary>
    public class Poll
    {

        #region ################### properties #######################

        /// <summary>
        /// شناسه نطرسنحی
        /// </summary>
        public int PollId { get; set; }

        /// <summary>
        /// متن نطرسنحی
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// تاریخ شروع نطرسنجی
        /// </summary>
        public string PollStartDate { get; set; }

        /// <summary>
        /// تاریخ اتمام و یستن نطرسنحی
        /// </summary>
        public string PollEndDate { get; set; }


        /// <summary>
        /// وصعیت فعال بودن نطرسنجی
        /// </summary>
        public bool Active { get; set; }
        #endregion #################

        #region ############## NavigationProperties ########################

        /// <summary>
        /// ارتباط یک به چن - سمت یک
        /// </summary>
        public virtual ICollection<PollOption> PollOptions { get; set; }

        #endregion ##########
  
    }
}
