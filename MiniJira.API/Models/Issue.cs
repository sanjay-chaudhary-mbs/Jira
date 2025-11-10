namespace MiniJira.API.Models
{
    public class Issue
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "Open";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationship
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
