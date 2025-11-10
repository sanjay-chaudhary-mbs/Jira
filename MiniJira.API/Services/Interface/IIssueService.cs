using MiniJira.API.Models;

namespace MiniJira.API.Services.Interface
{
    public interface IIssueService
    {
        Task<IEnumerable<Issue>> GetAllAsync();
        Task<Issue?> GetByIdAsync(int id);
        Task<Issue> CreateAsync(Issue issue);
        Task<Issue> UpdateAsync(Issue issue);
        Task<bool> DeleteAsync(int id);
    }
}
