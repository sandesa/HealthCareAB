using AvailabilityService.DTO;
using AvailabilityService.Models;

namespace AvailabilityService.Interfaces
{
    public interface IAvailabilityRepository
    {
        Task<IEnumerable<Availability>> GetAvailabilitiesDevAsync();
        Task<IEnumerable<AvailabilityDTO>> GetAvailabilitiesByCaregiverIdAsync(int caregiverId);
        Task<IEnumerable<AvailabilityDTO>> GetAvailabilitiesByDateIdAsync(string date);
        Task<AvailabilityDTO?> GetAvailabilityByIdAsync(int id);
        Task<AvailabilityDTO?> CreateAvailabilityAsync(AvailabilityCreate newAvailability);
        Task<AvailabilityDTO?> UpdateAvailabilityAsync(int id, AvailabilityUpdate availabilityUpdate);
        Task<AvailabilityDTO?> DeleteAvailabilityAsync(int id);
    }
}
