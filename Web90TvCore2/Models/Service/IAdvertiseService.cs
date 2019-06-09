using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models.Service
{
    /// <summary>
    /// سرویس پیاده سازی متدهای خاص جدول تبلیغات
    /// </summary>
   public interface IAdvertiseService
    {
        /// <summary>
        /// متد و تابع تغییر وضعیت نمایش تبلیغ در سایت
        /// </summary>
        /// <param name="Id">شناسه تبلیغ مورد نظر</param>
        /// <returns></returns>
        Task ChangeStatus(int Id);
        Task DeleteRootFile(int Id);
    }
}
