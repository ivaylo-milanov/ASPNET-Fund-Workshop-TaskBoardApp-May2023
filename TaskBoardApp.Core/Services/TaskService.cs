namespace TaskBoardApp.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Linq.Expressions;
    using TaskBoardApp.Core.Services.Contracts;
    using TaskBoardApp.Core.ViewModels.Task;
    using TaskBoardApp.Infrastructure.Common;
    using TaskBoardApp.Infrastructure.Models;

    public class TaskService : ITaskService
    {
        private readonly IRepository repository;

        public TaskService(IRepository repository)
        {
            this.repository = repository;
        }

        public async System.Threading.Tasks.Task AddTaskAsync(TaskFormModel model, string userId)
        {
            Task task = new Task
            {
                Title = model.Title,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                BoardId = model.BoardId,
                OwnerId = userId
            };

            await this.repository.AddAsync(task);
            await this.repository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(Task task)
        {
            this.repository.Delete(task);
            await this.repository.SaveChangesAsync();
        }

        public IQueryable<Task> GetAllTasks(Expression<Func<Task, bool>> search)
            => this.repository
                .All(search)
                .AsQueryable();

        public int GetAllTasksCount()
            => this.repository.AllReadonly<Task>().Count();

        public async Task<IEnumerable<TaskBoardModel>> GetBoards()
            => await this.repository
                .AllReadonly<Board>()
                .Select(b => new TaskBoardModel
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToListAsync();

        public async Task<Task> GetTaskById(int id)
            => await this.repository.FindAsync<Task>(id);

        public async Task<TaskDetailsViewModel> GetTaskDetails(int id)
            => await this.repository.All<Task>(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName
                })
                .FirstOrDefaultAsync()!;

        public async System.Threading.Tasks.Task UpdateAsync(Task task, TaskFormModel model)
        {
            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;

            await this.repository.SaveChangesAsync();
        }
    }
}
