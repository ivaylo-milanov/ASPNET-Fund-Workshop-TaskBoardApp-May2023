namespace TaskBoardApp.Core.ViewModels.Home
{
    public class HomeViewModel
    {
        public int AllTasksCount { get; set; }

        public IEnumerable<HomeBoardModel> BoardsWithTasksCount { get; set; } = null!;

        public int UserTasksCount { get; set; }
    }
}
