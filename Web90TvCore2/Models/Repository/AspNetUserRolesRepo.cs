using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models.Service;

namespace Web90TvCore2.Models.Repository
{
    /// <summary>
    /// سرویس و کلاس برای نمایش سطح دسترسی (=نقش های) کاربر انتخاب شده
    /// </summary>
    public class AspNetUserRolesRepo:IAspNetUserRolesRepo
    {
        #region ################ dependencies ####################### 
        
        private readonly ApplicationDbContext _context;

        public AspNetUserRolesRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion ########################### 

        #region ######################### Methods #############################

        /// <summary>
        /// بدست آوردن سطح دسترسی (=نقش های)کاربرانتخاب شده
        /// </summary>
        /// <param name="userId">آیدی کاربری که روی  دکمه دسترسی آن کلیک شده را از اکشن دریافت میکند</param>
        /// <returns name="getRoleArray">خروجی از نوع رشته که به صورت آرایه ساخته شده است را 
        /// برمیگرداند که در واقع همان سطح دسترسی و آیدی نقش های کاربر است </returns>
        public  string GetRoleId(string userId)
        {
            //برگرداندن آیدی نقش های(سزح دسترسی) کاربرانتخاب شده
            var getRoleId = _context.UserRoles.Where(ur => ur.UserId == userId).ToList();

            //برای  ذخیره آیدی نقش ها به صورت آرایه
            string getRoleArray = "";

            for (int i = 0; i < getRoleId.Count; i++)
            {
                //هر آدی نقش را به متغیر از نوع رشته اضافه میکنیم و یک ویرکول بعد از هر آیدی میگذاریم تا چیزی شبیه آرایه بسازیم
                //e.g.:{"asdfads","fasdfasd",}
                getRoleArray += getRoleId[i].RoleId.ToString() + ",";
            }

            return getRoleArray;

        }

        //public Array GetRoleId(string userId)
        //{
        //    var getRoleId = _context.UserRoles.Where(ur => ur.UserId == userId).ToArray();

        //    return getRoleId;
        //}


        #endregion###################


    }
}
