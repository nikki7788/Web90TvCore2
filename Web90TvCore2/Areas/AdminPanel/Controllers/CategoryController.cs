using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web90TvCore2.Areas.AminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        public CategoryController()
        {
                
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}