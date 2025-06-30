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

        public async Task UpdateAsync(long id, Specification updatedSpec)
        {
            var existingSpec = await _context.Specifications
                .Include(s => s.phoneDetails)
                .Include(s => s.gsmLaunchDetails)
                .Include(s => s.gsmBodyDetails)
                .Include(s => s.gsmDisplayDetails)
                .Include(s => s.gsmMemoryDetails)
                .Include(s => s.gsmSoundDetails)
                .Include(s => s.gsmBatteryDetails)
                .FirstOrDefaultAsync(s => s.customId == id);

            if (existingSpec == null)
                throw new KeyNotFoundException("Specification not found.");

            existingSpec.phoneDetails.yearValue = updatedSpec.phoneDetails.yearValue;
            existingSpec.phoneDetails.brandValue = updatedSpec.phoneDetails.brandValue;
            existingSpec.phoneDetails.modelValue = updatedSpec.phoneDetails.modelValue;

            existingSpec.gsmLaunchDetails.launchAnnounced = updatedSpec.gsmLaunchDetails.launchAnnounced;
            existingSpec.gsmLaunchDetails.launchStatus = updatedSpec.gsmLaunchDetails.launchStatus;

            existingSpec.gsmBodyDetails.bodyDimensions = updatedSpec.gsmBodyDetails.bodyDimensions;
            existingSpec.gsmBodyDetails.bodyWeight = updatedSpec.gsmBodyDetails.bodyWeight;
            existingSpec.gsmBodyDetails.bodySim = updatedSpec.gsmBodyDetails.bodySim;
            existingSpec.gsmBodyDetails.bodyBuild = updatedSpec.gsmBodyDetails.bodyBuild;
            existingSpec.gsmBodyDetails.bodyOther1 = updatedSpec.gsmBodyDetails.bodyOther1;
            existingSpec.gsmBodyDetails.bodyOther2 = updatedSpec.gsmBodyDetails.bodyOther2;
            existingSpec.gsmBodyDetails.bodyOther3 = updatedSpec.gsmBodyDetails.bodyOther3;

            existingSpec.gsmDisplayDetails.displayType = updatedSpec.gsmDisplayDetails.displayType;
            existingSpec.gsmDisplayDetails.displaySize = updatedSpec.gsmDisplayDetails.displaySize;
            existingSpec.gsmDisplayDetails.displayResolution = updatedSpec.gsmDisplayDetails.displayResolution;
            existingSpec.gsmDisplayDetails.displayProtection = updatedSpec.gsmDisplayDetails.displayProtection;
            existingSpec.gsmDisplayDetails.displayOther1 = updatedSpec.gsmDisplayDetails.displayOther1;

            existingSpec.gsmMemoryDetails.memoryCardSlot = updatedSpec.gsmMemoryDetails.memoryCardSlot;
            existingSpec.gsmMemoryDetails.memoryInternal = updatedSpec.gsmMemoryDetails.memoryInternal;
            existingSpec.gsmMemoryDetails.memoryOther1 = updatedSpec.gsmMemoryDetails.memoryOther1;

            existingSpec.gsmSoundDetails.sound35MmJack = updatedSpec.gsmSoundDetails.sound35MmJack;
            existingSpec.gsmSoundDetails.soundLoudspeaker = updatedSpec.gsmSoundDetails.soundLoudspeaker;
            existingSpec.gsmSoundDetails.soundOther1 = updatedSpec.gsmSoundDetails.soundOther1;
            existingSpec.gsmSoundDetails.soundOther2 = updatedSpec.gsmSoundDetails.soundOther2;

            existingSpec.gsmBatteryDetails.batteryCharging = updatedSpec.gsmBatteryDetails.batteryCharging;
            existingSpec.gsmBatteryDetails.batteryType = updatedSpec.gsmBatteryDetails.batteryType;

            await _context.SaveChangesAsync();
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

