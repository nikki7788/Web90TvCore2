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

        private CrudRepGeneric<Category> _CategoryRepUW;

        #endregion #######################




        #region ############################ properties #############################################
        /// <summary>
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  category برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudRepGeneric<Category> CategoryRepUW
        {
            //فقط خواندنی
            get
            {
                if (_CategoryRepUW == null)
                {
                    _CategoryRepUW = new CrudRepGeneric<Category>(_context);
                }
                return _CategoryRepUW;
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
