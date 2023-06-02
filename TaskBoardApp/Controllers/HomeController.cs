namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using TaskBoardApp.Core.Services.Contracts;
    using TaskBoardApp.Core.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly ITaskService taskService;

        public HomeController(IHomeService homeService, ITaskService taskService)
        {
            this.homeService = homeService;
            this.taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            var boards = await this.homeService.GetBoards();

            int userTasksCount = -1;
            if (User.Identity.IsAuthenticated)
            {
                var userId = this.GetUserId();
                userTasksCount = this.taskService.GetAllTasks(t => t.OwnerId == userId).Count();
            }

            var model = new HomeViewModel
            {
                AllTasksCount = this.taskService.GetAllTasksCount(),
                BoardsWithTasksCount = boards,
                UserTasksCount = userTasksCount
            };

            return View(model);
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}