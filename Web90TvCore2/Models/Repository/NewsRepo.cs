using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models.Service;

namespace Web90TvCore2.Models.Repository
{
    /// <summary>
    /// سرویس ها و متد های خاص برای اخبار
    /// </summary>
    public class NewsRepo:INewsService
    {
        #region############################### Dependencies ###############################################

        private readonly ApplicationDbContext _context;


        public NewsRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion##################################################################################

        #region############################### Methods ###############################################

        /// <summary>
        /// به روز رسانی تعداد بازدید
        /// </summary>
        /// <param name="id">آیدی خبر</param>
        /// <returns></returns>
        public async Task RefreshVisitCounter(int id)
        {
            try
            {
                var result = _context.News.Where(n => n.NewsId == id).FirstOrDefault();

                if (result != null)
                {
                    result.VisitCount++;
                    //آپدیت یک ستون ازیک سظر جدول
                    _context.News.Attach(result);
                    _context.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();

                    }
                    catch (DbUpdateConcurrencyException ex)
                    {

                        throw ex;
                    }
                    catch (DbUpdateException ex)
                    {

                        throw ex;
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }


                }
            }
            catch (ArgumentNullException)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }

        }


        #endregion################################################################################

    }
}
