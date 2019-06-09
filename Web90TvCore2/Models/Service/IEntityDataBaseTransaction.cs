using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models.Service
{
    /// <summary>
    /// لایه سرویس تراکنش
    /// </summary>
    public interface IEntityDataBaseTransaction : IDisposable
    {
        /// <summary>
        /// اجرای عملیات
        /// </summary>
        void Commit();


        /// <summary>
        /// رگشت عملیات
        /// </summary>
        void Rollback();


    }
}
