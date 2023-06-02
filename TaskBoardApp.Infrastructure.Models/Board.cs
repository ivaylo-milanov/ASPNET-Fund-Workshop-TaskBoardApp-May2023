namespace TaskBoardApp.Infrastructure.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using TaskBoardApp.Common;

    [Comment("Board table")]
    public class Board
    {
        public Board()
        {
            this.Tasks = new HashSet<Task>();
        }

        [Comment("Board id")]
        [Key]
        public int Id { get; set; }

        [Comment("Board name")]
        [MaxLength(GlobalConstants.BoardNameMaxLength)]
        [Required]
        public string Name { get; set; } = null!;

        [Comment("Board tasks")]
        public virtual ICollection<Task> Tasks { get; set; } = null!;
    }
}