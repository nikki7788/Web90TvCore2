using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models.Repository;

namespace Web90TvCore2.Models.UnitOfWork
{
    /// <summary>
    ///برای  جداول مورد نظر CRUD تعریف پراپرتی و متد ها برای پیاده سازی کلاس  
    /// 
    /// در کنترلر و پروژه unitofwork  اینترفیس برای استفاده از کلاس
    /// </summary>
    public interface IUnitOfWork
    {
       CrudRepGeneric<Category> CategoryRepUW { get;}

        Task Save();

         //void Save();
    }
}
