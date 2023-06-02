namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TaskBoardApp.Core.Services.Contracts;

    [Authorize]
    public class BoardsController : Controller
    {
        private readonly IBoardService boardService;

        public BoardsController(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<IActionResult> Index()
        {
            var boardTasks = await this.boardService.GetBoardTasks();

            return View(boardTasks);
        }
    }
}
