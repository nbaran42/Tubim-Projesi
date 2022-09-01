using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.Interfaces.Repositories
{
    public interface IBaseGenericRepo<T> where T : class
    {
        List<T> GetAll();   
        List<T> GetAll(Func<T,bool> predicate); 
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>> predicate=null);
        IQueryable<T> GetAllAsQueryable(Func<T, bool> predicate); 
        T FirstOrDefault(Func<T,bool> predicate);    
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken  ); 
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void  Insert(T entity);
        Task InsertAsync(T entity);
        void Update(T entity);  
        void Delete(T entity);   
       


    
    }
}
