namespace TaskBoardApp.Core.Services.Contracts
{
    using TaskBoardApp.Core.ViewModels.Board;

    public interface IBoardService
    {
        Task<IEnumerable<BoardViewModel>> GetBoardTasks();
    }
}
