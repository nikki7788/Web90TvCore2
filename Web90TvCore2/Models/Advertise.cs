using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.PublicClass;

namespace Web90TvCore2.Models
{
    /// <summary>
    /// تبلیغات
    /// </summary>
    public class Advertise
    {
        [Key]
        public int AdId { get; set; }

        /// <summary>
        /// نام فایل کیف تبلیغ
        /// </summary>
        [Display(Name = "تصویر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string GifPath { get; set; }


        /// <summary>
        /// تاریخ شروع تبلیغ
        /// </summary>
        [Display(Name = "از تاریخ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string FromDate { get; set; }


        /// <summary>
        /// تاریخ اتمام تبلیغ
        /// </summary>
        [Display(Name = "تا تاریخ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string ToDate { get; set; }



        /// <summary>
        /// لینک تبلیغ
        /// </summary>
        /// لینکی که وقتی روی تبلیغ کلیک میکنیم برود به ادرس مورد نظر
        [Display(Name = "لینک تبلیغ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string Link { get; set; }


        /// <summary>
        /// وضعیت نمایش  تبلیغ
        /// </summary>
        [Display(Name = "وضعیت")]
        public byte Flag { get; set; }



        /// <summary>
        /// محل نمایش تبلیغ
        /// </summary>
        /// 
        [Display(Name = "محل نمایش")]
        public byte AdvLocation { get; set; }

    }


    /// <summary>
    /// تعریف   محل نمایش برای عر تبلیغ
    /// </summary>
    /// کلاس برای مقداردهی محل نمایش تبلیغات
    public class AdvertisePlace
    {

        /// <summary>
        /// شناسه هر محل نمایش تبلیغ
        /// </summary>
        /// مقادیر داده شده در پایین را میگیرد
        public int AdvId { get; set; }



        /// <summary>
        /// نام محل نمایش تبلیغ
        /// </summary>
        public string AdvLocationName { get; set; }



        /// <summary>
        /// لیست محل های نمایش تبلیغ
        /// </summary>
        /// <returns></returns>
        public List<AdvertisePlace> AdvertiseDescription()
        {
            var model = new List<AdvertisePlace>
            {
                new AdvertisePlace {AdvId = 1, AdvLocationName = "هدر سایت"},
                new AdvertisePlace {AdvId = 2, AdvLocationName = "زیر اسلایدر"},
                new AdvertisePlace {AdvId = 3, AdvLocationName = "زیر اخبار ویژه"},
                new AdvertisePlace {AdvId = 4, AdvLocationName = "زیر آخرین ویدیوها"},
            };
            return model;
        }
    }
}
