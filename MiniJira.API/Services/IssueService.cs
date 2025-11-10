using MiniJira.API.Models;
using MiniJira.API.Repositories.Interface;
using MiniJira.API.Services.Interface;

namespace MiniJira.API.Services
{
    public class IssueService: IIssueService
    {
        private readonly IIssueRepository _repo;

        public IssueService(IIssueRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Issue>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Issue?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Issue> CreateAsync(Issue issue)
        {
            // Add any business validation here
            if (string.IsNullOrWhiteSpace(issue.Title))
                throw new ArgumentException("Issue title cannot be empty.");

            return await _repo.AddAsync(issue);
        }

        public async Task<Issue> UpdateAsync(Issue issue)
        {
            var existing = await _repo.GetByIdAsync(issue.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Issue with ID {issue.Id} not found.");

            existing.Title = issue.Title;
            existing.Description = issue.Description;
            existing.Status = issue.Status;
            existing.ProjectId = issue.ProjectId;

            return await _repo.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
