using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models
{
    /// <summary>
    /// جدول و کلاس برای شخصی سازی جدول نقش ها
    /// </summary>
    ///  عمل میکند و به آن برای مثال پراپرتی هایی را اضافه یا اورراید میکند AspNetRoles این جدول روی جدول
    ///است.که مابرای کاروایجاد تغییرات و شخصی سازی ان باید این کلاس را ایجاد کنیم  AspNetRoles درواقع همان جدول 
    public class ApplicationRoles :IdentityRole
    {

        /// <summary>
        /// است parent سطح و پدر جز را مشخص میکند -- همان  
        /// </summary>
        /// اگر صفر باشد یعنی  جز اصلی است و پدر ندارد.زیر مجموعه نیست
        public string RoleLevel { get; set; }


        /// <summary>
        /// نام فارسی نقش و جز را در خود نگه میدارد
        /// </summary>
        /// مثلا اجزای سیسنم
        public string  Description { get; set; }
    }
}
