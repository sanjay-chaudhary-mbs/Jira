namespace MiniJira.MVC.Models
{
    public class DashboardViewModel
    {
        public List<ProjectViewModel> Projects { get; set; } = new();
        public List<IssueViewModel> ToDo { get; set; } = new();
        public List<IssueViewModel> InProgress { get; set; } = new();
        public List<IssueViewModel> Done { get; set; } = new();
        public List<IssueViewModel> RecentActivity { get; set; } = new();
    }
}
