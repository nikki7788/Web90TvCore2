using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models.ViewModels
{
    /// <summary>
    /// مدل و وویومدل برای ویوی اصلی سایت
    /// این وبو مدل حاوی چندین کلاس دیگر است
    /// </summary>
    /// این ویو مدل برای ویوی اصلی سایت است که باید از چندین کلاس تغذیه واستفاده کند
    public class IndexViewModel
    {

        /// <summary>
        /// ویومدل لاگین
        /// </summary>
        /// برای اینکه ویو اصی از لاگین ویو مدل باید تغذیه کند
        public LoginViewModel LoginVM { get; set; }




        /// <summary>
        /// خبرهای اسلایر
        /// </summary>
        /// دارند newsPlace=0 خبرهایی که 
        public List<News> SliderNews { get; set; }


        /// <summary>
        /// خبرهای ویژه
        /// </summary>
        /// دارند newsPlace=1 خبرهایی که 
        public List<News> SpecialNews { get; set; }




        /// <summary>
        /// آخرین ویدوها
        /// </summary>
        /// دارند newsPlace=1 خبرهایی که 
        public List<News> LastVideos { get; set; }




        /// <summary>
        /// آخرین خبرها
        /// </summary>
        /// چمدتا خبر اخری که ثبت شده را در این تب نشان میدهیم 
        public List<News> LastNews { get; set; }



        /// <summary>
        /// خبرهای داخلی
        /// </summary>
        /// انها 0 باشد NewsType خبرهایی که پراپرتی 
        public List<News> DomesticNews { get; set; }



        /// <summary>
        /// خبرهای خارجی
        /// </summary>
        /// انها 1 باشد NewsType خبرهایی که پراپرتی 
        public List<News> ForeignNews { get; set; }




        /// <summary>
        /// خبرهای اختصاصی
        /// </summary>
        /// انها 2 باشد NewsType خبرهایی که پراپرتی 
        public List<News> ExclusiveNews { get; set; }

        /// <summary>
        /// جزییات خبر
        /// </summary>
        /// نمایش جزییات خبر
        public News NewsDetails { get; set; }

        /// <summary>
        /// تبلیغات 
        /// </summary>
        /// نمایش تبلیغات
        public List<Advertise> Advertises { get; set; }

        /// <summary>
        /// نظر سنجی
        /// </summary>
        public Poll Poll { get; set; }

        /// <summary>
        /// متاتگ
        /// </summary>
        public string Metatag { get; set; }



        /// <summary>
        /// متای توضیحات
        /// </summary>

        public string MetaDescription { get; set; }


    }
}
