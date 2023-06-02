namespace TaskBoardApp.Infrastructure.Common
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;

    public interface IRepository
    {
        IQueryable<T> All<T>() where T : class;

        IQueryable<T> All<T>(Expression<Func<T, bool>> search) where T : class;

        DbSet<T> Set<T>() where T : class;

        IQueryable<T> AllReadonly<T>() where T : class;

        Task SaveChangesAsync();

        Task AddAsync<T>(T model) where T : class;

        Task<T> FindAsync<T>(int id) where T : class;

        void Delete<T>(T model) where T : class;
    }
}
