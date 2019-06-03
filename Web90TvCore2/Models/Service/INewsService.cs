﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models.Service
{
    /// <summary>
    /// وجود ندارد CRUD  که در کلاس 
    ///  category تعریف  متد های خاص برای جدول
    ///----Service اینترفیس لایه سرویس 
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// به روز رسانی تعداد بازدید
        /// </summary>
        /// <param name="id">آیدی خبر</param>
        /// <returns></returns>
        Task RefreshVisitCounter(int id);
    }
}
