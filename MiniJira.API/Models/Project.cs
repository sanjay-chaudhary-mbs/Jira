namespace MiniJira.API.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property (list of issues related to this project)
        public ICollection<Issue>? Issues { get; set; }
    }
}