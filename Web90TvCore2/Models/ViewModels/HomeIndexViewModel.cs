using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models.ViewModels
{
    /// <summary>
    /// محل نمایش خبر ها در صفحه اصلی سایت
    /// </summary>
    /// این کلاس ثابت است وپ در دیتابیس ثبت نمیشود
    /// محل هایی که در صفحه اصلی خبر در انها قرار میکیرد را در اینجا ثبت میکنیم
    /// 
    public class HomeIndexViewModel
    {

        #region############### Properties ############################

        /// <summary>
        /// شناسه محل نمایش خبر
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// عنوان محل نمایش خبر
        /// </summary>
        public string Title { get; set; }

        #endregion############################



        #region##################### Methods #######################

        /// <summary>
        /// متد برای مقدار دهی پراپرتی های بالا
        /// </summary>
        /// <returns></returns>
        /// محل هایی که در صفحه اصلی خبر در انها قرار میکیرد را در اینجا ثبت میکنیم
        public List<HomeIndexViewModel> GetPlaceInIndex()
        {
            var model = new List<HomeIndexViewModel>
            {
                new HomeIndexViewModel {Id = 0,Title = "اسلایدر"},
                new HomeIndexViewModel {Id = 1,Title = "اخبار ویژه"},
                new HomeIndexViewModel {Id = 2,Title = "آخرین مطالب"},
                new HomeIndexViewModel {Id = 3,Title = "آخرین ویدیوها"},
            };
            return model;
        }

        #endregion#############

    }
}
