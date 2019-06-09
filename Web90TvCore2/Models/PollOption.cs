using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models
{
    /// <summary>
    /// جدول گزینه های نطرسنحی
    /// </summary>
//    [Table("PollOptions")]
    public class PollOption
    {

        #region ################### properties #######################

        /// <summary>
        /// شناسه پاسخ نطرسنجی
        /// </summary>
        public int PolloptionID { get; set; }

        
        /// <summary>
        /// متن پاسخ نطرسنحی
        /// </summary>
        public string Answer { get; set; }


        /// <summary>
        /// تعداد انتخاب هر گزینه
        /// </summary>
        public int VouteCount { get; set; }


        /// <summary>
        /// شناسهمتن نطر سنحی
        /// </summary>
        /// کلید خارجی
        public int PollID { get; set; }



        #endregion #################

        #region ############## NavigationProperties ########################

        /// <summary>
        /// ارتباط یک به چند - سمت چند
        /// </summary>
        /// کلیدخارجی راهم قید نکنیم خدوش میفهمد کدام کلید خارحی است براساس نام
        [ForeignKey("PollID")]
        public virtual Poll Poll { get; set; }

        #endregion ##########
    }
}
