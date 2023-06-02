namespace TaskBoardApp.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskBoardApp.Core.Services.Contracts;
    using TaskBoardApp.Core.ViewModels.Home;
    using TaskBoardApp.Infrastructure.Common;
    using TaskBoardApp.Infrastructure.Models;

    public class HomeService : IHomeService
    {
        private readonly IRepository repository;

        public HomeService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<HomeBoardModel>> GetBoards()
            => await this.repository.AllReadonly<Board>()
                .Select(b => new HomeBoardModel
                {
                    BoardName = b.Name,
                    TasksCount = b.Tasks.Count
                })
                .ToListAsync();
    }
}
