using AvailabilityService.DTO;
using AvailabilityService.Interfaces;
using AvailabilityService.Models;

namespace AvailabilityService.Services
{
    public class AvailabilityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;

        public AvailabilityService(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        public async Task<ResponseDTO<IEnumerable<Availability>>> GetAvailabilitiesDevAsync()
        {
            try
            {
                var availabilities = await _availabilityRepository.GetAvailabilitiesDevAsync();
                if (availabilities == null || !availabilities.Any())
                {
                    return new ResponseDTO<IEnumerable<Availability>>
                    {
                        Message = "No DEV availabilities found.",
                        IsSuccess = true
                    };
                }
                return new ResponseDTO<IEnumerable<Availability>>
                {
                    Data = availabilities,
                    Message = "Availabilities DEV retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<Availability>>
                {
                    Message = $"An error occurred while retrieving availabilities: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<AvailabilityDTO>>> GetAvailabilitiesByCaregiverIdAsync(int caregiverId)
        {
            try
            {
                var availabilities = await _availabilityRepository.GetAvailabilitiesByCaregiverIdAsync(caregiverId);
                if (availabilities == null || !availabilities.Any())
                {
                    return new ResponseDTO<IEnumerable<AvailabilityDTO>>
                    {
                        Message = "No availabilities found for the specified caregiver.",
                        IsSuccess = true
                    };
                }
                return new ResponseDTO<IEnumerable<AvailabilityDTO>>
                {
                    Data = availabilities,
                    Message = "Availabilities retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<AvailabilityDTO>>
                {
                    Message = $"An error occurred while retrieving availabilities: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<AvailabilityDTO>>> GetAvailabilitiesByDateIdAsync(string date)
        {
            try
            {
                var availabilities = await _availabilityRepository.GetAvailabilitiesByDateIdAsync(date);
                if (!availabilities.Any())
                {
                    return new ResponseDTO<IEnumerable<AvailabilityDTO>>
                    {
                        Message = "No availabilities found for the specified date.",
                        IsSuccess = true
                    };
                }
                return new ResponseDTO<IEnumerable<AvailabilityDTO>>
                {
                    Data = availabilities,
                    Message = "Availabilities retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<AvailabilityDTO>>
                {
                    Message = $"An error occurred while retrieving availabilities: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<AvailabilityDTO>>> GetAvailabilitiesOneMonthFromNow(string startDate)
        {
            try
            {
                var availabilities = await _availabilityRepository.GetAvailabilitiesOneMonthFromNow(startDate);
                if (!availabilities.Any())
                {
                    return new ResponseDTO<IEnumerable<AvailabilityDTO>>
                    {
                        Message = "No availabilities found for the next month.",
                        IsSuccess = true
                    };
                }
                return new ResponseDTO<IEnumerable<AvailabilityDTO>>
                {
                    Data = availabilities,
                    Message = "Availabilities retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<AvailabilityDTO>>
                {
                    Message = $"An error occurred while retrieving availabilities: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<AvailabilityDTO>> GetAvailabilityByIdAsync(int id)
        {
            try
            {
                var availability = await _availabilityRepository.GetAvailabilityByIdAsync(id);
                if (availability == null)
                {
                    return new ResponseDTO<AvailabilityDTO>
                    {
                        Message = "Availability not found.",
                    };
                }
                return new ResponseDTO<AvailabilityDTO>
                {
                    Data = availability,
                    Message = "Availability retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<AvailabilityDTO>
                {
                    Message = $"An error occurred while retrieving the availability: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<AvailabilityDTO>> CreateAvailabilityAsync(AvailabilityCreate newAvailability)
        {
            try
            {
                var availability = await _availabilityRepository.CreateAvailabilityAsync(newAvailability);
                if (availability == null)
                {
                    return new ResponseDTO<AvailabilityDTO>
                    {
                        Message = "Failed to create availability.",
                    };
                }
                return new ResponseDTO<AvailabilityDTO>
                {
                    Data = availability,
                    Message = "Availability created successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<AvailabilityDTO>
                {
                    Message = $"An error occurred while creating the availability: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<AvailabilityDTO>> UpdateAvailabilityAsync(int id, AvailabilityUpdate availabilityUpdate)
        {
            try
            {
                var availability = await _availabilityRepository.UpdateAvailabilityAsync(id, availabilityUpdate);
                if (availability == null)
                {
                    return new ResponseDTO<AvailabilityDTO>
                    {
                        Message = "Could not find an existing availability to update.",
                    };
                }
                return new ResponseDTO<AvailabilityDTO>
                {
                    Data = availability,
                    Message = "Availability updated successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<AvailabilityDTO>
                {
                    Message = $"An error occurred while updating the availability: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<AvailabilityDTO>> DeleteAvailabilityAsync(int id)
        {
            try
            {
                var availability = await _availabilityRepository.DeleteAvailabilityAsync(id);
                if (availability == null)
                {
                    return new ResponseDTO<AvailabilityDTO>
                    {
                        Message = "Could not find an existing availability to delete.",
                    };
                }
                return new ResponseDTO<AvailabilityDTO>
                {
                    Data = availability,
                    Message = "Availability deleted successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<AvailabilityDTO>
                {
                    Message = $"An error occurred while deleting the availability: {ex.Message}",
                };
            }
        }
    }
}
