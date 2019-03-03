using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Web90TvCore2.Models.Service;

namespace Web90TvCore2.Models.Repository
{
    /// <summary>
    /// CRUD
    ///  در پروژه و استفاده از ان در سرتاسر پروژه CRUD کلاس جنریک برای یکبار نوشتن متد های تکراری    
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class CrudRepGeneric<TEntity> where TEntity : class
    {
        #region############################### Dependencies ###############################################

        private readonly ApplicationDbContext _context;

        private readonly DbSet<TEntity> _table;    //e.g. _context.categories....
       
        public CrudRepGeneric(ApplicationDbContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        #endregion##################################################################################


        /// <summary>
        /// ایجاد یک رکورد
        /// </summary>
        /// <param name="entity"></param>
        public virtual async void Create(TEntity entity)
        {
            await _table.AddAsync(entity);
        }


        /// <summary>
        /// آپدیت یک رکورد
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }


        /// <summary>
        /// یک رکورد مربوط ای دی مورد نظر را برمیکرداند
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetById(object id)
        {
            return await _table.FindAsync(id);
        }


        /// <summary>
        /// لیستی  از کورد ها را میاورد
        /// </summary>
        /// <param name="whereIf"></param>
        /// <param name="orderByIf"></param>
        /// <param name="joinString"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> whereIf = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByIf = null,
           string joinString = "")
        {

            IQueryable<TEntity> query = _table;

            if (whereIf != null)
            {
                query = query.Where(whereIf);
            }

            if (orderByIf != null)
            {

                query = orderByIf(query);
            }

            if (joinString != "")
            {
                foreach (var item in joinString.Split(','))
                {
                    query = query.Include(item);
                }

            }

            return await query.ToListAsync();
        }


        /// <summary>
        /// حذف یک کورد
        /// مثلا میگوییم دسته بندی فوتبال را حذف کن
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _table.Attach(entity);
            }
            _table.Remove(entity);
        }


        /// <summary>
        /// حذف یک رکورد براساس آیدی
        /// </summary>
        /// <param name="id"></param>
        public virtual async void DeletById(object id)
        {
            var entity = await GetById(id);
            Delete(entity);

        }




        /// <summary>
        /// ذخیره اطلاعات و تغییرات در دیتابیس
        /// </summary>
        public virtual async void Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
