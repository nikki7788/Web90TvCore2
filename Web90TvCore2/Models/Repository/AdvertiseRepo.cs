using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models.Service;

namespace Web90TvCore2.Models.Repository
{

    /// <summary>
    /// پیاده سازی   متد های خاص جدول تبلیغات 
    /// </summary>
    public class AdvertiseRepo:IAdvertiseService
    {
        #region ############# Dependencies ######################


        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment    _iHosting;

        public AdvertiseRepo(ApplicationDbContext context,IHostingEnvironment iHosting)
        {
            _context = context;
            _iHosting = iHosting;
        }
        #endregion #####################

        #region ############# Dependencies ######################


        /// <summary>
        /// متد و تابع تغییر وضعیت نمایش تبلیغ در سایت
        /// </summary>
        /// <param name="Id">شناسه تبلیغ مورد نظر</param>
        /// <returns></returns>
        public async Task ChangeStatus(int Id)
        {
            //var result = (from c in _context.Advertises where c.AdId == Id select c);
            var result =  _context.Advertises.Where(a => a.AdId == Id);
            var currentAdvertise =await result.FirstOrDefaultAsync();

            if (result.Count() != 0)
            {
                if (currentAdvertise.Flag == 0)
                {
                    currentAdvertise.Flag = 1;
                }
                else
                {
                    currentAdvertise.Flag = 0;
                }

                _context.Advertises.Attach(currentAdvertise);
                _context.Entry(currentAdvertise).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
               await _context.SaveChangesAsync();
            }
        }



        /// <summary>
        /// حذف فایل از روت سایت
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteRootFile(int Id)
        {
            //حذف فایل از روت سایت
            var query =await _context.Advertises.FindAsync(Id);
            if (query != null)
            {
                string gifName = query.GifPath;
                var dirPath = Path.Combine(_iHosting.WebRootPath + "\\upload\\advImage\\" + gifName);
                File.Delete(dirPath);
            }
        }
        #endregion #####################


    }
}
