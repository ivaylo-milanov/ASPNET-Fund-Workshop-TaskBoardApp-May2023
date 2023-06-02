namespace TaskBoardApp.Core.ViewModels.Task
{
    using System.ComponentModel.DataAnnotations;
    using TaskBoardApp.Common;

    public class TaskFormModel
    {
        [Required]
        [StringLength(GlobalConstants.TaskTitleMaxLength, MinimumLength = GlobalConstants.TaskTitleMinLength, ErrorMessage = ExceptionMessages.TaskTitleInvalidLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(GlobalConstants.TaskDescriptionMaxLength, MinimumLength = GlobalConstants.TaskDescriptionMinLength, ErrorMessage = ExceptionMessages.TaskDescriptionInvalidLength)]
        public string Description { get; set; } = null!;

        [Display(Name = "Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel>? Boards { get; set; }
    }
}
