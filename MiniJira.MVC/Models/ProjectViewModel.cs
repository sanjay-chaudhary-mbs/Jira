namespace MiniJira.MVC.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<IssueViewModel> Issues { get; set; } = new();
    }
}
