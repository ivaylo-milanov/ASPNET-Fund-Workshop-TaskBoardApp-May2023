namespace TaskBoardApp.Infrastructure.Common
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using TaskBoardApp.Infrastructure.Data;

    public class Repository : IRepository
    {
        private readonly TaskBoardAppDbContext context;

        public Repository(TaskBoardAppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync<T>(T model) where T : class
            => await this.Set<T>().AddAsync(model);

        public IQueryable<T> All<T>() where T : class
            => this.Set<T>()
                .AsQueryable();

        public IQueryable<T> All<T>(Expression<Func<T, bool>> search) where T : class
            => this.Set<T>()
                .Where(search)
                .AsQueryable();

        public IQueryable<T> AllReadonly<T>() where T : class
            => this.Set<T>()
                .AsQueryable()
                .AsNoTracking();

        public void Delete<T>(T model) where T : class
            => this.context.Remove(model);

        public async Task<T> FindAsync<T>(int id) where T : class
            => await this.Set<T>().FindAsync(id);

        public async Task SaveChangesAsync()
            => await this.context.SaveChangesAsync();

        public DbSet<T> Set<T>() where T : class
            => this.context.Set<T>();
    }
}
