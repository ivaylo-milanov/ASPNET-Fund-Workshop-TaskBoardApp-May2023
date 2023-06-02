namespace TaskBoardApp.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using TaskBoardApp.Core.Services.Contracts;
    using TaskBoardApp.Core.ViewModels.Board;
    using TaskBoardApp.Core.ViewModels.Task;
    using TaskBoardApp.Infrastructure.Common;
    using TaskBoardApp.Infrastructure.Models;

    public class BoardService : IBoardService
    {
        private readonly IRepository repository;

        public BoardService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<BoardViewModel>> GetBoardTasks()
            => await this.repository.Set<Board>()
                .Select(b => new BoardViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks
                        .Select(t => new TaskViewModel
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            Owner = t.Owner.UserName
                        })
                })
                .ToListAsync();
    }
}
