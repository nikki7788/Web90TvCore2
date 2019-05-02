using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// متد افزایش تعدادلایک  کامنت
        /// </summary>
        /// <param name="Id">شناسه کامنتی که لایک میشود</param>
        /// <returns></returns>
        Task IncreaseLike(int Id);


        /// <summary>
        /// متد افزایش تعداد دیسلایک کامنت
        /// 
        /// </summary>
        /// <param name="Id">شناسه کامنتی که دیسلایک میشود</param>
        /// <returns></returns>
        Task IncreaseDislike(int Id);




        /// <summary>
        /// متد کاهش تعداد لایک کامنت
        /// وقتی کاربری خبری را لایک کرده و سپس میخواهد ان را دیسلایک کند باید از لایک یگی گم کنیم
        /// </summary>
        /// <param name="Id">شناسه کامنتی که لایک میشود</param>
        /// <returns></returns>
        Task DecreaseLike(int Id);



        /// <summary>
        /// متد کاهش تعداد دیسلایک کامنت
        /// وقتی کاربری خبری را دیسلایک کرده و سپس میخواهد ان را لایک کند باید از دیسلایک یگی گم کنیم
        /// </summary>
        /// </summary>
        /// <param name="Id">شناسه کامنتی که لایک میشود</param>
        /// <returns></returns>
        Task DecreaseDislike(int Id);
    }
}
