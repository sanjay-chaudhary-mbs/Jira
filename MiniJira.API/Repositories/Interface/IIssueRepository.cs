using MiniJira.API.Models;

namespace MiniJira.API.Repositories.Interface
{
    public interface IIssueRepository
    {
        Task<IEnumerable<Issue>> GetAllAsync();
        Task<Issue?> GetByIdAsync(int id);
        Task<Issue> AddAsync(Issue issue);
        Task<Issue> UpdateAsync(Issue issue);
        Task<bool> DeleteAsync(int id);
    }
}
