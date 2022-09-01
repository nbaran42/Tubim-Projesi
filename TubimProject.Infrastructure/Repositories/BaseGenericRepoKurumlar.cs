using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories;

namespace TubimProject.Infrastructure.Repositories
{
    public class BaseGenericRepoKurumlar<T> : IBaseGenericRepo<T> where T : class
    {
        private readonly KurumlarContext _context;
        public DbSet<T> dbset;

        public BaseGenericRepoKurumlar(KurumlarContext context)
        {
            _context=context;
            dbset = context.Set<T>();
        }
        public bool IsDisposed { get; set; }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            IsDisposed=true;
        }


        public void Delete(T entity)
        {
            dbset.Remove(entity);

        }



        public List<T> GetAll()
        {
            return dbset.ToList();
        }

        public List<T> GetAll(Func<T, bool> predicate)
        {

            return predicate==null ? dbset.ToList() : dbset.Where(predicate).ToList();

        }

        public IQueryable<T> GetAllAsQueryable(Func<T, bool> predicate)
        {

            return predicate==null ? dbset.AsQueryable() : dbset.Where(predicate).AsQueryable();

        }



        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {

            return predicate==null ? await dbset.ToListAsync() : await dbset.Where(predicate).AsQueryable().ToListAsync();

        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {

            return predicate==null ? dbset.FirstOrDefault() : dbset.FirstOrDefault(predicate);

        }


        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {

            return predicate==null ? await dbset.FirstOrDefaultAsync() : await dbset.FirstOrDefaultAsync(cancellationToken: cancellationToken, predicate: predicate);

        }
        public T GetById(int id)
        {

            return dbset.Find(id);

        }

        public async Task<T> GetByIdAsync(int id)
        {

            return await dbset.FindAsync(id);

        }

        public void Insert(T entity)
        {

            dbset.Add(entity);


        }

        public async Task InsertAsync(T entity)
        {

            await dbset.AddAsync(entity);


        }

        public void Update(T entity)
        {

            _context.Entry(entity).State = EntityState.Modified;


        }

    }
}
