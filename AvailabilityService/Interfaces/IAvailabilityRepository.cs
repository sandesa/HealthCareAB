﻿using AvailabilityService.DTO;
using AvailabilityService.Models;

namespace AvailabilityService.Interfaces
{
    public interface IAvailabilityRepository
    {
        Task<IEnumerable<Availability>> GetAvailabilitiesDevAsync();
        Task<IEnumerable<AvailabilityDTO>> GetAvailabilitiesByCaregiverIdAsync(int caregiverId);
        Task<IEnumerable<AvailabilityDTO>> GetAvailabilitiesByDateIdAsync(string date);
        Task<IEnumerable<AvailabilityDTO>> GetAvailabilitiesOneMonthFromNow(string startDate);
        Task<AvailabilityDTO?> GetAvailabilityByIdAsync(int id);
        Task<AvailabilityDTO?> CreateAvailabilityAsync(AvailabilityCreate newAvailability, int caregiverId);
        Task<AvailabilityDTO?> UpdateAvailabilityAsync(int id, AvailabilityUpdate availabilityUpdate);
        Task<AvailabilityDTO?> DeleteAvailabilityAsync(int id);
    }
}
