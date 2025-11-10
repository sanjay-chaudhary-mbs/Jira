using Microsoft.EntityFrameworkCore;
using MiniJira.API.Data;
using MiniJira.API.Models;
using MiniJira.API.Repositories.Interface;

namespace MiniJira.API.Repositories
{
    public class IssueRepository: IIssueRepository
    {
        private readonly ApplicationDbContext _context;

        public IssueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Issue>> GetAllAsync()
        {
            return await _context.Issues.Include(i => i.Project).ToListAsync();
        }

        public async Task<Issue?> GetByIdAsync(int id)
        {
            return await _context.Issues.Include(i => i.Project).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Issue> AddAsync(Issue issue)
        {
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();
            return issue;
        }

        public async Task<Issue> UpdateAsync(Issue issue)
        {
            _context.Issues.Update(issue);
            await _context.SaveChangesAsync();
            return issue;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null) return false;

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
