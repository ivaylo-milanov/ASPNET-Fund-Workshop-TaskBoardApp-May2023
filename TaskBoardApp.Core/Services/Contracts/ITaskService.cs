namespace TaskBoardApp.Core.Services.Contracts
{
    using System.Linq.Expressions;
    using TaskBoardApp.Core.ViewModels.Task;
    using TaskBoardApp.Infrastructure.Models;

    public interface ITaskService
    {
        Task<IEnumerable<TaskBoardModel>> GetBoards();

        System.Threading.Tasks.Task AddTaskAsync(TaskFormModel model, string userId);

        Task<TaskDetailsViewModel> GetTaskDetails(int id);

        Task<Task> GetTaskById(int id);

        System.Threading.Tasks.Task UpdateAsync(Task task, TaskFormModel model);

        System.Threading.Tasks.Task DeleteAsync(Task task);

        int GetAllTasksCount();

        IQueryable<Task> GetAllTasks(Expression<Func<Task, bool>> search);
    }
}
