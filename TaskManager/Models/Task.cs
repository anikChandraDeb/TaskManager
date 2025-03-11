using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
