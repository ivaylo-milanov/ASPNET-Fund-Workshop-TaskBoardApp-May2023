namespace TaskBoardApp.Infrastructure.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TaskBoardApp.Common;

    [Comment("Task table")]
    public class Task
    {
        [Comment("Task Id")]
        [Key]
        public int Id { get; set; }

        [Comment("Task title")]
        [MaxLength(GlobalConstants.TaskTitleMaxLength)]
        [Required]
        public string Title { get; set; } = null!;

        [Comment("Task description")]
        [MaxLength(GlobalConstants.TaskDescriptionMaxLength)]
        [Required]
        public string Description { get; set; } = null!;

        [Comment("Task createdOn")]
        public DateTime CreatedOn { get; set; }

        [Comment("Task board id")]
        [ForeignKey(nameof(Board))]
        public int BoardId { get; set; }

        [Comment("Task board")]
        public virtual Board? Board { get; set; }

        [Comment("Task owner id")]
        [ForeignKey(nameof(Owner))]
        [Required]
        public string OwnerId { get; set; } = null!;

        [Comment("Task owner")]
        public virtual IdentityUser Owner { get; set; } = null!;
    }
}
