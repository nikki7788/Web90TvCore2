using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models.Repository;
using Web90TvCore2.Models.Service;

namespace Web90TvCore2.Models.UnitOfWork
{

    /// <summary>
    ///برای  جداول مورد نظر CRUD تعریف پراپرتی و متد ها برای پیاده سازی کلاس  
    /// 
    /// در کنترلر و پروژه unitofwork  اینترفیس برای استفاده از کلاس
    /// </summary>
    public interface IUnitOfWork
    {

        /// <summary>
        /// دسته بندی اخبار
        /// </summary>
        CrudRepGeneric<Category> CategoryRepUW { get; }


        /// <summary>
        /// خبر
        /// </summary>
        CrudRepGeneric<News> NewsRepUW { get; }




        /// <summary>
        /// نظرات کاربران
        /// </summary>
        CrudRepGeneric<Comment> CommentRepUW { get; }



        /// <summary>
        /// تبلیغات
        /// </summary>
        CrudRepGeneric<Advertise> AdveriseRepUW { get; }



        /// <summary>
        /// یوزر
        /// </summary>
        CrudRepGeneric<ApplicationUsers> UserManagerUW { get; }



        /// <summary>
        /// متن نطرسنجی
        /// </summary>
        CrudRepGeneric<PollOption> PollOptionRepoUW { get; }



        /// <summary>
        /// گزینه های نطرسنحی
        /// </summary>
        CrudRepGeneric<Poll> PollRepoUW { get; }

        /// <summary>
        /// تنظیمات سایت
        /// </summary>
        CrudRepGeneric<SiteSetting> SiteSettingRepoUW { get; }


        /// <summary>
        /// مدیریت تراکنش
        /// Transaction
        /// </summary>
        IEntityDataBaseTransaction BeginTransaction();


        Task Save();

         //void Save();
    }
}
