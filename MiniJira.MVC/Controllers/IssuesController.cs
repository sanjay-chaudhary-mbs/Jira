using Microsoft.AspNetCore.Mvc;
using MiniJira.MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace MiniJira.MVC.Controllers
{
    public class IssuesController : Controller
    {
        private readonly HttpClient _client;

        public IssuesController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("MiniJiraAPI");
        }

        // GET: Issues/Create
        public IActionResult Create(int projectId)
        {
            var model = new IssueViewModel { ProjectId = projectId };
            return View(model);
        }

        // POST: Issues/Create
        [HttpPost]
        public async Task<IActionResult> Create(IssueViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/issues", content);

            if (response.IsSuccessStatusCode)
            {
                // Redirect back to Project Details page
                return RedirectToAction("Details", "Projects", new { id = model.ProjectId });
            }

            ViewBag.Error = "Failed to create issue.";
            return View(model);
        }
    }
}
