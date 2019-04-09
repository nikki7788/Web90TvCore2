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
    /// این ویو مدل برای ویوی اصلی سایت است که باید ز چندین کلاس تغذیه کند و استفاده کند
    public class IndexViewModel
    {

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
        /// ویومدل لاگین
        /// </summary>
        /// برای اینکه ویو اصی از لاگین ویو مدل باید تغذیه کند
        public LoginViewModel loginVM { get; set; }

    }
}
