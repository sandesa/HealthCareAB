using AvailabilityService.DTO;
using AvailabilityService.Models;

namespace AvailabilityService.Services
{
    public class AvailabilityMappingService
    {
        public AvailabilityDTO AvailToDto(Availability availability)
        {
            return new AvailabilityDTO
            {
                CaregiverId = availability.CaregiverId,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime,
                Notes = availability.Notes,
            };
        }

        public Availability CreateToAvail(AvailabilityCreate availabilityCreate)
        {
            return new Availability
            {
                CaregiverId = availabilityCreate.CaregiverId,
                StartTime = availabilityCreate.StartTime,
                EndTime = availabilityCreate.EndTime,
                Notes = availabilityCreate.Notes,
            };
        }

        public Availability UpdateToAvail(Availability existingAvailability, AvailabilityUpdate availabilityUpdate)
        {
            existingAvailability.StartTime = availabilityUpdate.StartTime;
            existingAvailability.EndTime = availabilityUpdate.EndTime;
            existingAvailability.Notes = availabilityUpdate.Notes ?? existingAvailability.Notes;
            return existingAvailability;
        }
    }
}
