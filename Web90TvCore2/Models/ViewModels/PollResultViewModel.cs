using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models.ViewModels
{
    /// <summary>
    /// ویو مدل برای نمایش نتایج نظر سنجی
    /// </summary>
    /// حتما باید با حروف کوچک باشد نام پاپرتی ها تا خطا ندهد در نمایش
    public class PollResultViewModel
    {
        /// <summary>
        /// تعداد آرای هر گزینه
        /// </summary>
        public int data { get; set; }

        /// <summary>
        /// نمایش گزینه نطر سنجی
        /// </summary>
        public string label { get; set; }


    }
}
