namespace TaskBoardApp.Core.Services.Contracts
{
    using TaskBoardApp.Core.ViewModels.Home;

    public interface IHomeService
    {
        Task<IEnumerable<HomeBoardModel>> GetBoards();
    }
}
