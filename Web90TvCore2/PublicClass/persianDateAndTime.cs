using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.PublicClass
{
    /// <summary>
    /// تاریخ و زمان شمسی
    /// --item1=currentDate--
    /// --item2=currentTime--
    /// </summary>
    /// برای اینکه کد های تکراری در اکشن ها ننویسیم
    public class PersianDateAndTime
    {


        /// <summary>
        /// تاریخ شمسی و زمان حال را باهم برمیکرداند
        ///--item1=currentDate--
        /// --item2=currentTime--
        /// </summary>
        ///بااین روش کیتوان چندین خروجی را ارسال کرد
        public static Tuple<string, string> PersianDateNow()
        {
            //-------------  بدست اوردن تایخ شمسی و زمان کنونی   -------------

            PersianCalendar persianCalendar = new PersianCalendar();
            var currentDate = DateTime.Now;
            int year = persianCalendar.GetYear(currentDate);
            int month = persianCalendar.GetMonth(currentDate);
            int day = persianCalendar.GetDayOfMonth(currentDate);

            string pCalendar = String.Format("{00:yyyy/MM/dd}", Convert.ToDateTime(year + "/" + month + "/" + day));

            string currentTime = String.Format("{0:hh:mm}", Convert.ToDateTime(currentDate.Hour + ":" + currentDate.Minute));
            return new Tuple<string, string>(currentTime, pCalendar);

        }








        /// <summary>
        /// تاریخ شمسی و زمان حال را باهم برمیکرداند
        /// </summary>
        /// <param name="currentTime"> خروجی اول تابع که باید یک رشه برای ان بفرستیم زمن حال را برمیگرداند</param>
        /// متغیری از توع استرینگ ایجاد میکنیم در اکشن و ان را به
        /// این متد پاس میدهیم تابع زمان حال را روی این متغیر میریدو برمیکرداند
        /// <returns name="pCalendar" > تاریخ شمسی را برمیکرداند</returns>
        /// روش استفاده به صورت زیر است از این مد
        ///  string TimeNow;
        ///string shamsiDate = PersianDateAndTime.PersianDateNow(out TimeNow);

        //public static string PersianDateNow(out string currentTime)
        //{
        //    //-------------  بدست اوردن تایخ شمسی و زمان کنونی   -------------

        //    PersianCalendar persianCalendar = new PersianCalendar();
        //    var currentDate = DateTime.Now;
        //    int year = persianCalendar.GetYear(currentDate);
        //    int month = persianCalendar.GetMonth(currentDate);
        //    int day = persianCalendar.GetDayOfMonth(currentDate);

        //    string pCalendar = String.Format("{00:yyyy/MM/dd}", Convert.ToDateTime(year + "/" + month + "/" + day));

        //    currentTime = String.Format("{0:hh:mm}", Convert.ToDateTime(currentDate.Hour + ":" + currentDate.Minute));

        //    return pCalendar;

        //}


        ///به روش زیر هم میتوان نوشت دو متد برای زمان وتاریخ 

        //public static string DateShamsi()
        //{
        //    var currentDate = DateTime.Now;
        //    PersianCalendar pcCalender = new PersianCalendar();
        //    int year = pcCalender.GetYear(currentDate);
        //    int month = pcCalender.GetMonth(currentDate);
        //    int day = pcCalender.GetDayOfMonth(currentDate);

        //    string shamsiDate = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(day + "/" + month + "/" + year));
        //    return shamsiDate;
        //}

        //public static string MyTime()
        //{
        //    var currentDate = DateTime.Now;
        //    PersianCalendar pcCalender = new PersianCalendar();
        //    int year = pcCalender.GetYear(currentDate);
        //    int month = pcCalender.GetMonth(currentDate);
        //    int day = pcCalender.GetDayOfMonth(currentDate);

        //    string NowTime = string.Format("{0:hh:mm}", Convert.ToDateTime(pcCalender.GetHour(currentDate) + ":" + pcCalender.GetMinute(currentDate)));
        //    return NowTime;
        //}

    }
}
