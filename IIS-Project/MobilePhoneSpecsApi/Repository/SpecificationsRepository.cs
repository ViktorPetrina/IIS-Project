using Microsoft.EntityFrameworkCore;
using MobilePhoneSpecsApi.Models;

namespace MobilePhoneSpecsApi.Repository
{
    public class SpecificationsRepository : IRepository<Specification>
    {
        private readonly SpecificationsDbContext _context;
        private readonly DbSet<Specification> _specificationSet;

        public SpecificationsRepository(SpecificationsDbContext context)
        {
            _context = context;
            _specificationSet = context.Set<Specification>();
        }

        public async Task<IEnumerable<Specification>> GetAllAsync()
        {
            return await _specificationSet
                .Include(s => s.phoneDetails)
                .Include(s => s.gsmLaunchDetails)
                .Include(s => s.gsmBodyDetails)
                .Include(s => s.gsmDisplayDetails)
                .Include(s => s.gsmMemoryDetails)
                .Include(s => s.gsmSoundDetails)
                .Include(s => s.gsmBatteryDetails)
                .ToListAsync();
        }

        public async Task<Specification> GetByIdAsync(long id)
        {
            return await _specificationSet
                .Include(s => s.phoneDetails)
                .Include(s => s.gsmLaunchDetails)
                .Include(s => s.gsmBodyDetails)
                .Include(s => s.gsmDisplayDetails)
                .Include(s => s.gsmMemoryDetails)
                .Include(s => s.gsmSoundDetails)
                .Include(s => s.gsmBatteryDetails)
                .FirstOrDefaultAsync(s => s.customId == id) 
                ?? throw new Exception("Non existant Specification");
        }

        public async Task AddAsync(Specification specification)
        {
            await _specificationSet.AddAsync(specification);
            await SaveAsync();
        }

        public async Task UpdateAsync(Specification specification)
        {
            _specificationSet.Update(specification);
            await SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var specification = await _specificationSet.FindAsync(id);
            if (specification != null)
            {
                _specificationSet.Remove(specification);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

