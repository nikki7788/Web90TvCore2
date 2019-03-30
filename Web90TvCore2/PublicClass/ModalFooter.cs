using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.PublicClass
{
    /// <summary>
    /// footer Modal
    /// مقدار دهی فوتر مودال
    /// </summary>
    public class ModalFooter
    {
        /// <summary>
        /// آی دی دکمه کنسل را مقدار دهی میکنیم چون در کل پروژه ثابت هستند
        /// </summary>
        public string CancelbuttonId { get; set; } = "btn-cancel";

        /// <summary>
        /// متن دکمه کنسل 
        /// </summary>
        ///چون متن دکمه کنسل ثابت است مقدار ان را همینجا تعریف میکنیم 
        public string CancelbuttonText { get; set; } = "برگشت";

        /// <summary>
        /// آی دی دکمه ارسال را مقدار دهی میکنیم
        /// </summary>
        /// آیدی دکمه ثابت است برای همه یکسان در نظر میکیریم
        public string SubmitButtonId { get; set; } = "btn-submit";

        /// <summary>
        /// متن و نام دکمه ارسال
        /// </summary>
        /// توسط کاربر در مقدار دهی میشودوبه صورت ویش فرض اگر مقدار دهی  نکند مقدار زیر و پیش فرض را میگیرد
        public string SubmitButtonText { get; set; } = "submit";

        /// <summary>
        /// برای مودال هایی که فقط یک دکمه بیشتر ندارند
        /// </summary>
        ///برای مودال هایی که فقط یک دکمه بیشتر ندارند مثلا فقط برگشت. مودال اطلاع رسانی و خبری هستند چیزی ارسال نمیکنند
        public bool OnlyButton { get; set; }


    }
}
