using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models.Service;

namespace Web90TvCore2.Models.Repository
{

    /// <summary>
    /// متد های خاص جدول کامنت
    /// </summary>
    public class CommentRepo : ICommentService
    {
        #region ########################ctor Dependencies ######################
        private readonly ApplicationDbContext _context;

        public CommentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion ############


        #region ################################## Methods ##################################################

        /// <summary>
        /// افزایش تعداد لایک
        /// </summary>
        /// <param name="Id">شناسه کامنتی که لایک میشود</param>
        /// <returns> </returns>
        /// اگر کامنتی با شناسه دریافتی از اکشن دریافت پیداشد اولین رکورد را برممیگرداند وگرنه نال برمیگرداند
        /// اگر کامنتی یافت شد ستون لایک را یکی افزایش میدهیم و سپس تغییرات  را ذخیره میکنیم 
        public async Task IncreaseLike(int Id)
        {
            //var result = (from c in _context.Comments where c.Id == Id select c);
            //var currentComment = result.FirstOrDefault();

            var result = _context.Comments.FirstOrDefault(c => c.Id == Id);
            if (result != null)
            {

                result.LikeCount++;
                _context.Comments.Attach(result);
                _context.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }



        /// <summary>
        /// کاهش تعداد لایک
        /// وقتی کاربری خبری را لایک کرده و سپس میخواهد ان را دیسلایک کند باید از لایک یگی گم کنیم
        /// </summary>
        /// <param name="Id">شناسه کامنتی که لایک میشود</param>
        /// <returns> </returns>
        /// اگر کامنتی با شناسه دریافتی از اکشن دریافت پیداشد اولین رکورد را برممیگرداند وگرنه نال برمیگرداند
        /// اگر کامنتی یافت شد ستون لایک را یکی کاهش میدهیم و سپس تغییرات  را ذخیره میکنیم 
        /// وقتی کاربری خبری را لایک کرده و سپس میخواهد ان را دیسلایک کند باید از لایک یگی گم کنیم
        public async Task DecreaseLike(int Id)
        {
            //var result = (from c in _context.Comments where c.Id == Id select c);
            //var currentComment = result.FirstOrDefault();

            var result = _context.Comments.FirstOrDefault(c => c.Id == Id);
            if (result != null)
            {

                result.LikeCount--;
                _context.Comments.Attach(result);
                _context.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }





        /// <summary>
        /// افزایش تعداد دیسلایک
        /// </summary>
        /// <param name="Id">شناسه کامنتی که دیسلایک میشود</param>
        /// <returns> </returns>
        /// اگر کامنتی با شناسه دریافتی از اکشن دریافت پیداشد اولین رکورد را برممیگرداند وگرنه نال برمیگرداند
        /// اگر کامنتی یافت شد ستون لایک را یکی افزایش میدهیم و سپس تغییرات  را ذخیره میکنیم 
        public async Task IncreaseDislike(int Id)
        {
            //var result = (from c in _context.Comments where c.Id == Id select c);
            //var currentComment = result.FirstOrDefault();

            var result = _context.Comments.FirstOrDefault(c => c.Id == Id);
            if (result != null)
            {
                result.DisLikeCount++;
                _context.Comments.Attach(result);
                _context.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }





        /// <summary>
        /// کاهش تعداد دیسلایک
        ///  وقتی کاربری خبری را دیسلایک کرده و سپس میخواهد ان را لایک کند باید از دیسلایک یگی گم کنیم
        /// </summary>
        /// <param name="Id">شناسه کامنتی که دیسلایک میشود</param>
        /// <returns> </returns>
        /// اگر کامنتی با شناسه دریافتی از اکشن دریافت پیداشد اولین رکورد را برممیگرداند وگرنه نال برمیگرداند
        /// اگر کامنتی یافت شد ستون لایک را یکی کاهش میدهیم و سپس تغییرات  را ذخیره میکنیم 
        public async Task DecreaseDislike(int Id)
        {
            //var result = (from c in _context.Comments where c.Id == Id select c);
            //var currentComment = result.FirstOrDefault();

            var result = _context.Comments.FirstOrDefault(c => c.Id == Id);
            if (result != null)
            {
                result.DisLikeCount--;
                _context.Comments.Attach(result);
                _context.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// تایید یا رد نمایش نطر در سایت
        /// </summary>
        /// <param name="Id">شناسه نطر</param>
        /// <returns></returns>
        public async Task AcceptOrReject(int Id)
        {
            var result = _context.Comments.FirstOrDefault(c => c.Id == Id);
            if (result != null)
            {
                if (result.status == true)
                {
                    result.status = false;
                }
                else
                {
                    result.status = true;
                }
                _context.Comments.Attach(result);
                _context.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
            }

        }




        #endregion ######

    }
}
