using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web90TvCore2.Models;

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

        public RoleController(RoleManager<ApplicationRoles> roleManager)
        {
            _roleManager = roleManager;
        }
        #endregion ########################


        /// <summary>
        /// نمایش درختواره و جز های آن
        /// </summary>
        /// <returns></returns>
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
            catch (Exception ex)
            {
                //todo:مدیریت خطاها در لاگمانده
                throw ex;
            }

        }
    }
}