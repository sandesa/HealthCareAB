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
            var availDtos = avails.Select(a => _mapper.AvailToDto(a));
            return availDtos;
        }

        public async Task<IEnumerable<AvailabilityDTO>> GetAvailabilitiesByDateIdAsync(string date)
        {
            if (!DateTime.TryParse(date, out var parsedDate))
            {
                return [];
            }
            var avails = await _context.Availabilities.Where(a => a.StartTime.Date == parsedDate.Date).ToListAsync();

            var availDtos = avails.Select(a => _mapper.AvailToDto(a));
            return availDtos;
        }

        public async Task<IEnumerable<AvailabilityDTO>> GetAvailabilitiesOneMonthFromNow(string startDate)
        {
            var parsedDate = DateTime.Parse(startDate);
            var monthBreakPoint = parsedDate;

            while (monthBreakPoint.Month == parsedDate.Month && monthBreakPoint.AddDays(1).Month == parsedDate.Month)
            {
                monthBreakPoint = monthBreakPoint.AddDays(1);
            }

            var avails = await _context.Availabilities.Where(a => a.StartTime <= monthBreakPoint && a.StartTime >= parsedDate).ToListAsync();
            avails.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
            var availDtos = avails.Select(a => _mapper.AvailToDto(a));
            return availDtos;
        }

        public async Task<AvailabilityDTO?> GetAvailabilityByIdAsync(int id)
        {
            var avail = await _context.Availabilities.FindAsync(id);
            if (avail == null)
            {
                return null;
            }
            var availDto = _mapper.AvailToDto(avail);
            return availDto;
        }

        public async Task<AvailabilityDTO?> CreateAvailabilityAsync(AvailabilityCreate newAvailability)
        {
            if (newAvailability == null || newAvailability.StartTime > newAvailability.EndTime)
            {
                return null;
            }
            var avail = _mapper.CreateToAvail(newAvailability);
            avail.CreatedAt = DateTime.UtcNow;

            await _context.Availabilities.AddAsync(avail);
            await _context.SaveChangesAsync();

            var availDto = _mapper.AvailToDto(avail);
            return availDto;
        }

        public async Task<AvailabilityDTO?> UpdateAvailabilityAsync(int id, AvailabilityUpdate availabilityUpdate)
        {
            var existingAvail = await _context.Availabilities.FindAsync(id);
            if (existingAvail == null)
            {
                return null;
            }
            var avail = _mapper.UpdateToAvail(existingAvail, availabilityUpdate);
            avail.UpdatedAt = DateTime.UtcNow;

            if (avail.StartTime > avail.EndTime)
            {
                return null;
            }

            _context.Availabilities.Update(avail);
            await _context.SaveChangesAsync();

            var availDto = _mapper.AvailToDto(avail);
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

            var availDto = _mapper.AvailToDto(avail);
            return availDto;
        }
    }
}
