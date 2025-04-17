using AvailabilityService.Database;
using AvailabilityService.DTO;
using AvailabilityService.Interfaces;
using AvailabilityService.Models;
using AvailabilityService.Services;
using Microsoft.EntityFrameworkCore;

namespace AvailabilityService.Repositories
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly AvailabilityDbContext _context;
        private readonly AvailabilityMappingService _mapper;

        public AvailabilityRepository(AvailabilityDbContext context, AvailabilityMappingService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Availability>> GetAvailabilitiesDevAsync()
        {
            return await _context.Availabilities.ToListAsync();
        }

        public async Task<IEnumerable<AvailabilityDTO>> GetAvailabilitiesByCaregiverIdAsync(int caregiverId)
        {
            var avails = await _context.Availabilities.Where(a => a.CaregiverId == caregiverId).ToListAsync();

            var availDtos = avails.Select(a => _mapper.Map<AvailabilityDTO>(a));
            return availDtos;
        }

        public async Task<IEnumerable<AvailabilityDTO>> GetAvailabilitiesByDateIdAsync(DateTime date)
        {
            var avails = await _context.Availabilities.Where(a => a.StartTime.Date == date.Date).ToListAsync();

            var availDtos = avails.Select(a => _mapper.Map<AvailabilityDTO>(a));
            return availDtos;
        }

        public async Task<AvailabilityDTO?> GetAvailabilityByIdAsync(int id)
        {
            var avail = await _context.Availabilities.FindAsync(id);
            if (avail == null)
            {
                return null;
            }
            var availDto = _mapper.Map<AvailabilityDTO>(avail);
            return availDto;
        }

        public async Task<AvailabilityDTO?> CreateAvailabilityAsync(AvailabilityCreate newAvailability)
        {
            if (newAvailability == null)
            {
                return null;
            }
            var avail = _mapper.Map<Availability>(newAvailability);
            avail.CreatedAt = DateTime.Now;
            await _context.Availabilities.AddAsync(avail);
            await _context.SaveChangesAsync();

            var availDto = _mapper.Map<AvailabilityDTO>(avail);
            return availDto;
        }

        public async Task<AvailabilityDTO?> UpdateAvailabilityAsync(int id, AvailabilityUpdate availabilityUpdate)
        {
            var existingAvail = await _context.Availabilities.FindAsync(id);
            if (existingAvail == null)
            {
                return null;
            }
            var avail = _mapper.Map<Availability>(availabilityUpdate);
            avail.UpdatedAt = DateTime.Now;
            _context.Availabilities.Update(avail);
            await _context.SaveChangesAsync();

            var availDto = _mapper.Map<AvailabilityDTO>(avail);
            return availDto;
        }

        public async Task<AvailabilityDTO?> DeleteAvailabilityAsync(int id)
        {
            var avail = _context.Availabilities.Find(id);
            if (avail == null)
            {
                return null;
            }
            _context.Availabilities.Remove(avail);
            await _context.SaveChangesAsync();
            var availDto = _mapper.Map<AvailabilityDTO>(avail);
            return availDto;
        }
    }
}
