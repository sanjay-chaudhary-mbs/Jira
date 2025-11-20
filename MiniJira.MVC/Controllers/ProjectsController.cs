using Microsoft.AspNetCore.Mvc;
using MiniJira.MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace MiniJira.MVC.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly HttpClient _client;

        public ProjectsController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("MiniJiraAPI");
        }
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("api/projects");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load projects.";
                return View(new List<ProjectViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var projects = JsonConvert.DeserializeObject<List<ProjectViewModel>>(json);
            return View(projects);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"api/projects/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var project = JsonConvert.DeserializeObject<ProjectViewModel>(json);
            return View(project);
        }
        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/projects", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Failed to create project.";
            return View(model);
        }

    }
}
