using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.PublicClass
{
    /// <summary>
    ///متن پیام های خطای جدول ها 
    /// </summary>
    public class PublicConst
    {

        /// <summary>
        /// خطای وارد نکردن فیلد و پراپرتی
        /// </summary>
        public const string EnterMessage="لطفا {0} را وارد نمایید";


        /// <summary>
        /// خطای رعایت نکردن طول رشته حداقل و حداکثر
        /// </summary>
        public const string LengthMessage = "{0} باید بین {2} تا {1} کاراکتر باشد";


        /// <summary>
        /// خطای رعایت نکردن عبارات باقاعده پراپرتی
        /// </summary>
        public const string DangrouseMessageForBadCharachter = "در {0} کاراکترهای نامعتبر وارد شده است.";

    }
}
