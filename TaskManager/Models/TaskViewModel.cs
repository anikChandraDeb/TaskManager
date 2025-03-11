namespace TaskManager.Models
{
    public class TaskViewModel
    {
        public TaskManager.Models.Task NewTask { get; set; } = new TaskManager.Models.Task();
        public IList<TaskManager.Models.Task> Tasks { get; set; } = new List<TaskManager.Models.Task>();
    }
}
