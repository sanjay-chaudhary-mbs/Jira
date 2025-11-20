using Microsoft.AspNetCore.Mvc;
using MiniJira.MVC.Models;
using Newtonsoft.Json;

namespace MiniJira.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly HttpClient _client;

        public DashboardController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("MiniJiraAPI");
        }

        public async Task<IActionResult> Index()
        {
            // Fetch projects (not strictly necessary but useful for counts)
            var projectsResponse = await _client.GetAsync("api/projects");
            var projects = new List<ProjectViewModel>();
            if (projectsResponse.IsSuccessStatusCode)
            {
                var pjJson = await projectsResponse.Content.ReadAsStringAsync();
                projects = JsonConvert.DeserializeObject<List<ProjectViewModel>>(pjJson) ?? new List<ProjectViewModel>();
            }

            // Fetch all issues (we assume API has GET /api/issues)
            var issuesResponse = await _client.GetAsync("api/issues");
            var issues = new List<IssueViewModel>();
            if (issuesResponse.IsSuccessStatusCode)
            {
                var isJson = await issuesResponse.Content.ReadAsStringAsync();
                issues = JsonConvert.DeserializeObject<List<IssueViewModel>>(isJson) ?? new List<IssueViewModel>();
            }

            // Group issues into columns
            var todo = issues.Where(i => string.Equals(i.Status, "Open", StringComparison.OrdinalIgnoreCase)).ToList();
            var inProgress = issues.Where(i => string.Equals(i.Status, "In Progress", StringComparison.OrdinalIgnoreCase) ||
                                               string.Equals(i.Status, "InProgress", StringComparison.OrdinalIgnoreCase)).ToList();
            var done = issues.Where(i => string.Equals(i.Status, "Resolved", StringComparison.OrdinalIgnoreCase) ||
                                          string.Equals(i.Status, "Closed", StringComparison.OrdinalIgnoreCase)).ToList();

            // Recent activity (latest issues)
            var recentActivity = issues.OrderByDescending(i => i.Id).Take(8).ToList();

            var vm = new DashboardViewModel
            {
                Projects = projects,
                ToDo = todo,
                InProgress = inProgress,
                Done = done,
                RecentActivity = recentActivity
            };

            return View(vm);
        }
    }
}
