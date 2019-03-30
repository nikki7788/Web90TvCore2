using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web90TvCore2.Models;
using Web90TvCore2.Models.Service;
using Web90TvCore2.Models.ViewModels;

namespace Web90TvCore2.Areas.AdminPanel.Controllers
{
    /// <summary>
    /// کنترلر مربوط به رل و نقش ها
    /// </summary>

    [Area("AdminPanel")]
    public class RoleController : Controller
    {
        #region ################### dependencies #############################

        private readonly RoleManager<ApplicationRoles> _roleManager;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IAspNetUserRolesRepo _iAspNetUserRoleRepo;

        public RoleController(RoleManager<ApplicationRoles> roleManager, UserManager<ApplicationUsers> userManager,
            IAspNetUserRolesRepo iAspNetUserRoleRepo)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _iAspNetUserRoleRepo = iAspNetUserRoleRepo;
        }
        #endregion ########################


        /// <summary>
        /// نمایش درختواره و جز های آن
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<TreeViewNode> nodes = new List<TreeViewNode>();

                //جز اصلی و ردیف اول را خودمان به صورت دستی وارد میکنیم
                nodes.Add(new TreeViewNode
                {
                    id = "asd",
                    parent = "#",   //اولین نود و ردیف پدر ندارد و پدر ان را # میگذاریم
                    text = "اجزای سیستم"
                });

                //بقیه ردیف ها ا ازدیتابیس میخواند و از جدول نقش ها
                foreach (ApplicationRoles subNode in _roleManager.Roles.Where(r => r.RoleLevel != "0"))
                {
                    //تمام ردیف ها به جز ردیف اول را از دیتابیس میخواند و برمیگرداند

                    nodes.Add(new TreeViewNode
                    {
                        id = subNode.Id.ToString(),
                        text = subNode.Description,
                        parent = subNode.RoleLevel.ToString()
                    });

                }

                // دریافت میکند و ما اطالاعات را تبدیل به جیسان کرده و میفرستیم به ویو json اطلاعات را به صورت jstree
                ViewBag.json = JsonConvert.SerializeObject(nodes);
                ViewBag.viewTitle = "  نمایش درختی اجزای سیستم";

                return View();
            }
            catch (ArgumentNullException ex)
            {

                throw ex;
            }
            catch (NotSupportedException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                //todo:مدیریت خطاها در لاگمانده
                throw ex;
            }

        }







        /// <summary>
        /// Get Method
        /// نمایش ویوایجاد نقش(=بخش) جدید
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            //تمام نقش ها را برمیکراند و در ویو دریافت میکنیم برای نمایش در کمیو باکس
            ViewBag.systemPart = _roleManager.Roles.ToList();

            ViewBag.viewTitle = "  ایجاد اجزای جدید";
            return View();
        }



        /// <summary>
        /// Post Method
        /// ایجاد نقش (=بخش)جدید
        /// </summary>
        /// <param name="model">مدل دریافتی از ویو</param>
        /// <returns></returns>
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirm(AddApplicationRoleViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //------- برای بزرگ کردن حرف اول متن دریافتی -------
                    //string ms = model.Name;
                    // ms = ms.Substring(0, 1).ToUpper() + ms.Substring(1);
                    //-------------------




                    ApplicationRoles role = new ApplicationRoles
                    {
                        Id = model.Id,
                        RoleLevel = model.RoleLevel, //دریافتی از کمبوباکس را میکیرد id
                        Description = model.Description,
                        Name = model.Name

                    };
                    //AspnetRoles ثبت اطالعات و نقش در جدول نقش ها
                    IdentityResult result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {

                        return RedirectToAction("Index");
                    }
                }

                //*************########## درصورت خطا درولیدیشن ها باید دوباره کمبوباکس و تایتل بالا ویورو را به ویو ارسال و نمایش دهیم##### ***************************
                //تمام نقش ها را برمیکراند و در ویو دریافت میکنیم برای نمایش در کمیو باکس
                ViewBag.systemPart = _roleManager.Roles.ToList();
                ViewBag.viewTitle = "  ایجاد اجزای جدید";
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }








        /// <summary>
        /// Get Method
        /// نمایش درختواره و جز های آن برای مشخص کردن سطح دسترسی هر کاربر
        /// </summary>
        /// <param name="Id"> از ویو ارسال شده ست asp-route-id آی دی کاربری که روی  دکمه دسترسی آن کلیک شده است را بااین ورودی دریافت میکنیم که از طریق   </param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AccessRight(string Id)
        {
            try
            {
                List<TreeViewNode> nodes = new List<TreeViewNode>();

                //جز اصلی و ردیف اول را خودمان به صورت دستی وارد میکنیم
                nodes.Add(new TreeViewNode
                {
                    id = "asd",
                    parent = "#",   //اولین نود و ردیف پدر ندارد و پدر ان را # میگذاریم
                    text = "اجزای سیستم"
                });

                //بقیه ردیف ها ا ازدیتابیس میخواند و از جدول نقش ها
                foreach (ApplicationRoles subNode in _roleManager.Roles.Where(r => r.RoleLevel != "0"))
                {
                    //تمام ردیف ها به جز ردیف اول را از دیتابیس میخواند و برمیگرداند

                    nodes.Add(new TreeViewNode
                    {
                        id = subNode.Id.ToString(),
                        text = subNode.Description,
                        parent = subNode.RoleLevel.ToString()
                    });

                }

                //#######################################################
                // دریافت میکند و ما اطالاعات را تبدیل به جیسان کرده و میفرستیم به ویو json اطلاعات را به صورت jstree
                ViewBag.json = JsonConvert.SerializeObject(nodes);

                ApplicationUsers user = await _userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    ViewBag.viewTitle = "ثبت دسترسی برای " + user.FirstName + " " + user.LastName; 

                    //دریافت نقش هاودسترسی های کاربر
                    string getRoleId = _iAspNetUserRoleRepo.GetRoleId(Id);

                    //اگر کاربر نقش و دسترسی داشت
                    if (getRoleId.Length > 0)
                    {
                        //  ویرگول انتهای رشته نقش های کاربر را حذف میکند-یک زیررشته از رشته اصلی تولید میکند ","
                        //ارسال نقش ها و دسترسی های کاربر به ویوبرای نمایش ان(تیک خوردن و انتخاب بودن دسترسی هایی که کاربر داشته از قبل)د 
                        ViewBag.roleList = getRoleId.Substring(0, getRoleId.Length - 1);
                    }
                    return View();

                }
                return NotFound();
            }
            catch (ArgumentNullException ex)
            {

                throw ex;
            }
            catch (NotSupportedException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                //todo:مدیریت خطاها در لاگمانده
                throw ex;
            }
        }



        /// <summary>
        ///  AspNetUserRoles ثبت و ویرایش دسترسی های (=نقش های)کاربر در جدول 
        /// </summary>
        /// <param name="selectedItems">دسترسی های(=نقش های)انتخاب شده و تیک خورده کاربرراتوسط این ورودی دیافت میکنیم</param>
        /// <param name="Id"> از ویو ارسال شده ست asp-route-id آی دی کاربری که روی  دکمه دسترسی آن کلیک شده است را بااین ورودی دریافت میکنیم که از طریق   </param>
        /// <returns></returns>
        /// ابتدا همه نقش های کاربر اگر وجود داشته باشد  را پاک میکنیم سپس نقش هایی که انتخاب شده و تیک خورده را دوباره در دیتابیس ثبت میکنیم
        /// این کار برای ویرایش است
        [HttpPost, ActionName("AccessRight")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccessRightConfirm(string selectedItems, string Id)
        {
            // وقتی روی دسترسی هرکاربرکلیک میکنیم ارسال و دراینجا دریافت میکنیم  asp-route-id کاربر را از طریق دستور  Id
            try
            {
                // کردیم deserialize دریافت سطح دسترسی کاربراز ویو که به صورت جیسون است و ان را  
                //  به صورت ارایه و جیسوندرواقع هرکدام از درختواره ها ک تیک خورده باشد آی دی ان را که همان آی دی نقش است را برمیگرداند
                List<TreeViewNode> items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);

                //کاربر را براساس آی دی دریافتی از ورودی پیدامیکند
                ApplicationUsers user = await _userManager.FindByIdAsync(Id);

                //###############------------- حذف همه دسترسی هاونقش ها در دیتابیس----------####################
                //ایتدا همه دسترسی ها را حذف میکنیم پس دسترسی ها انخاب شده را ثبت بااین کار وییرایش انجام دادیم
                var delRole = await _userManager.GetRolesAsync(user);
                IdentityResult delRoleResult = await _userManager.RemoveFromRolesAsync(user, delRole);

                //####################-----------

                if (delRoleResult.Succeeded)
                {
                    if (user != null)
                    {
                        for (int i = 0; i < items.Count; i++)
                        {

                            //###############------------- ثبت دسترسی هاونقش ها در دیتابیس----------####################
                            ApplicationRoles role = await _roleManager.FindByIdAsync(items[i].id);

                            if (role != null)
                            {
                                // اطلاعات را ثبت میکند AspNetUserRoles در جدول 
                                //آی دی یوزر را به همراه آی دی   نقش ها آن
                                //یوزر و نام نقش را میگیرد این دستور
                                IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name);
                                if (!result.Succeeded)
                                {
                                    return NotFound();
                                }
                            }

                        }
                        
                    }
                }
                //RedirectToAction (action,Controller)
                return RedirectToAction("Index", "User"); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }




        }



    }
}