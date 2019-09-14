using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.PublicClass
{
    /// <summary>
    /// تبدیل اعداد فارسی به انگکلیسی
    /// </summary>
    /// این متد را برای تبدیل فونت فارسی اععداد به انگلییسی استفاده
    /// میکنیم تا در دیتابیس اعداد به صورت لاتین ذخیره شود نه فارسی
    
    public class ConvertFaToEnDigit
    {
          
        /// <summary>
        /// متد بدیل اعدا د فارسی به انگلیسی
        /// </summary>
        /// <param name="input">متغیر موردپنطر</param>
        /// دراینجا متغیر مورد نطر را دریافت میکنیم
        /// <returns name="input">از نوع رشته برمیگرداند و عدد انگلیسی را برمیگرداند</returns>
        /// این متد را برای تبدیل فونت فارسی اععداد به انگلییسی استفاده میکنیم تا
        /// در دیتابیس اعداد به صورت لاتین ذخیره شود نه فارسی
        public static string ToEnDigit(string input)
        {
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
                input = input.Replace(persian[j], j.ToString());

            return input;
        }

        /// <summary>
        ///تبدیل اعداد انگلیسی به فارسی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EnToFaDigit(string input)
        {
            string[] persian = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            for (int j = 0; j < persian.Length; j++)
                input = input.Replace(persian[j], j.ToString());

            return input;
        }



    }
}
