using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models.Repository;

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

        private  CrudRepGeneric<ApplicationUsers> _userManagerUW;


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
        /// کاربران سایت
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  ApplicationUser برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudRepGeneric<ApplicationUsers> UserManagerUW {
            get
            {
                if (_userManagerUW==null)
                {
                    _userManagerUW = new CrudRepGeneric<ApplicationUsers>(_context);
                }
                return _userManagerUW;
            }
                }

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
