namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using TaskBoardApp.Common;
    using TaskBoardApp.Core.Services.Contracts;
    using TaskBoardApp.Core.ViewModels.Task;
    using TaskBoardApp.Infrastructure.Models;

    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        public async Task<IActionResult> Create()
        {
            var boards = await this.taskService.GetBoards();

            TaskFormModel model = new TaskFormModel
            {
                Boards = boards
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            var boards = await this.taskService.GetBoards();

            if (!boards.Any(b => b.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), ExceptionMessages.BoardDoesNotExist);
            }

            string userId = this.GetUserId();

            if (!ModelState.IsValid)
            {
                model.Boards = boards;

                return View(model);
            }

            await this.taskService.AddTaskAsync(model, userId);

            return RedirectToAction("Index", "Boards");
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await this.taskService.GetTaskDetails(id);

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var boards = await this.taskService.GetBoards();
            Task task = await this.taskService.GetTaskById(id);

            if (task == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (userId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskFormModel model = new TaskFormModel
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = id,
                Boards = boards
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskFormModel model)
        {
            var task = await this.taskService.GetTaskById(id);

            if (task == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (userId != task.OwnerId)
            {
                return Unauthorized();
            }

            var boards = await this.taskService.GetBoards();

            if (!boards.Any(b => b.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), ExceptionMessages.BoardDoesNotExist);
            }

            await this.taskService.UpdateAsync(task, model);

            return RedirectToAction("Index", "Boards");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await this.taskService.GetTaskById(id);

            if (task == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (userId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel model = new TaskViewModel
            {
                Id = id,
                Title = task.Title,
                Description = task.Description
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel model)
        {
            var task = await this.taskService.GetTaskById(model.Id);

            if (task == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (userId != task.OwnerId)
            {
                return Unauthorized();
            }

            await this.taskService.DeleteAsync(task);
            return RedirectToAction("Index", "Boards");
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
