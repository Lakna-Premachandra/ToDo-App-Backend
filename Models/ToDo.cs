namespace ToDo_App_Backend.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string TodoName { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;  // Default value
    }
}
