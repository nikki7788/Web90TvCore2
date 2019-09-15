using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models.Repository;
using Web90TvCore2.Models.Service;

namespace Web90TvCore2.Models.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region ################## Dependencies #####################################
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion #######################



        #region############################# Fields ################################### 

        //نباید باشد readonly  
        private CrudRepGeneric<Category> _categoryRepUW;

        private CrudRepGeneric<News> _newsRepUW;

        private CrudRepGeneric<ApplicationUsers> _userManagerUW;

        private CrudRepGeneric<Comment> _commentUW;

        private CrudRepGeneric<Advertise> _advertiseUW;

        private CrudRepGeneric<Poll> _pollUW;

        private CrudRepGeneric<PollOption> _pollOptionUW;

        private CrudRepGeneric<SiteSetting> _siteSettingUW;



        #endregion #######################




        #region ############################ properties #############################################

        /// <summary>
        /// دسته بندی های سایت
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  category برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudRepGeneric<Category> CategoryRepUW
        {
            //فقط خواندنی
            get
            {
                if (_categoryRepUW == null)
                {
                    //اگر نمونه ای از دیتا بیس وجود نداشت یک نمونه از روی دیتابیس دسته بندی بسازد  
                    _categoryRepUW = new CrudRepGeneric<Category>(_context);
                }
                return _categoryRepUW;
            }
        }




        /// <summary>
        ///  اخبار
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  News برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudRepGeneric<News> NewsRepUW
        {

            //فقط خواندنی
            get
            {
                if (_newsRepUW == null)
                {
                    _newsRepUW = new CrudRepGeneric<News>(_context);
                }
                return _newsRepUW;
            }

        }




        /// <summary>
        /// نطرات کاربران
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  Comment برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudRepGeneric<Comment> CommentRepUW
        {
            get
            {
                if (_commentUW == null)
                {
                    _commentUW = new CrudRepGeneric<Comment>(_context);
                }
                return _commentUW;
            }
        }



        /// <summary>
        /// تبلیغات
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  Advertise برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudRepGeneric<Advertise> AdveriseRepUW
        {
            get
            {
                if (_advertiseUW == null)
                {
                    _advertiseUW = new CrudRepGeneric<Advertise>(_context);
                }
                return _advertiseUW;
            }
        }



        /// <summary>
        /// متن نطرسنجی
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  Poll برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudRepGeneric<Poll> PollRepoUW
        {
            get
            {
                if (_pollUW == null)
                {
                    _pollUW = new CrudRepGeneric<Poll>(_context);
                }
                return _pollUW;
            }
        }




        /// <summary>
        /// گزینه های نطرسنجی
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  PollOption برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudRepGeneric<PollOption> PollOptionRepoUW
        {
            get
            {
                if (_pollOptionUW == null)
                {
                    _pollOptionUW = new CrudRepGeneric<PollOption>(_context);
                }
                return _pollOptionUW;
            }
        }


        /// <summary>
        /// تنظیمات سایت
        /// </summary>
        /// IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  sitesetting برای کلاس و جدول CRUD پیاده سازی کلاس 
        public CrudRepGeneric<SiteSetting> SiteSettingRepoUW
        {
            get
            {
                if (_siteSettingUW == null)
                {
                    _siteSettingUW = new CrudRepGeneric<SiteSetting>(_context);
                }
                return _siteSettingUW;
            }
        }



        /// <summary>
        ///مدیریت تراکنش
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  Transaction پیاده سازی لایه سرویس 
        /// </summary>
        public IEntityDataBaseTransaction BeginTransaction()
        {
            return new EntityDataBaseTransaction(_context);
        }




        /// <summary>
        /// کاربران سایت
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  ApplicationUser برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudRepGeneric<ApplicationUsers> UserManagerUW
        {
            get
            {
                if (_userManagerUW == null)
                {
                    _userManagerUW = new CrudRepGeneric<ApplicationUsers>(_context);
                }
                return _userManagerUW;
            }
        }
        //todo: UserManagerUW  برای چی از دستور بالا استفاده کردیم؟   




        #endregion ##########################



        #region ############## methods #############################

        /// <summary>
        /// متد ذخیره کردن در دیتابیس--
        ///  IUnitOfWork پیاده سازی اعضای اینترفیس
        /// </summary>
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        //public  void Save()
        //{
        //     _context.SaveChanges();
        //}



        /// <summary>
        ///  بعد از اتمام کار کلاس ارتباط با دیتابیس را ازبین میبرد و قطع میکند 
        /// IDisposable  متد مربوط به اینترفیس  
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion ##########################
    }
}
