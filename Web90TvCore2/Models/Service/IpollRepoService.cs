using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models.Service
{
    /// <summary>
    /// سرویس پیاده سازی متدهای خاص جدول تبلیغات
    /// </summary>
    public interface IpollRepoService
    {
        /// <summary>
        ///بستن نطرسنجی
        /// </summary>
        /// <param name="id">شناسه نطرسنجی</param>
        void ClosePoll(int id);

        /// <summary>
        /// ثبت رای 
        /// </summary>
        /// <param name="id">شناسه نطرسنجی</param>
        void SetVote(int id);
    }
}
