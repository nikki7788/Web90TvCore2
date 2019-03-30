using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.Models.Service;

namespace Web90TvCore2.Models.Repository
{
    /// <summary>
    /// دسته بندی
    /// </summary>
    public class CategoryRepo:ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async void Create(Category model)
        //{
        //  await  _context.Categories.AddAsync(model);

        //}
    }
}


