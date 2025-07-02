using Microsoft.EntityFrameworkCore;
using score_management_be.Data;
using score_management_be.Models;
using System.Data;

namespace score_management_be.Repositories
{
    public interface IConductRepository : IBaseRepository<Conduct>
    {
        Task<Conduct?> CreateConductAsync(Conduct conduct);
        Task<Conduct> GetConductByNameAsync(string conductName);
    }
    public class ConductRepository : BaseRepository<Conduct>, IConductRepository
    {
        public ConductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Conduct?> CreateConductAsync(Conduct conduct)
        {
            var existingConduct = await GetConductByNameAsync(conduct.ConductName);
            if (existingConduct != null)
            {
                throw new InvalidOperationException($"Conduct with name '{conduct.ConductName}' already exists.");
            }
            return await base.CreateAsync(conduct);
        }

        public async Task<Conduct> GetConductByNameAsync(string conductName)
        {
            return await _context.Conducts.FirstOrDefaultAsync(c => c.ConductName == conductName);
        }
    }
}
