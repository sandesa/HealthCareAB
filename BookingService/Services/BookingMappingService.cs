using BookingService.DTO;
using BookingService.Models;

namespace BookingService.Services
{
    public class BookingMappingService
    {
        public BookingDTO BookingToDto(Booking booking)
        {
            return new BookingDTO
            {
                Id = booking.Id,
                CaregiverId = booking.CaregiverId,
                PatientId = booking.PatientId,
                MeetingDate = booking.MeetingDate,
                MeetingType = booking.MeetingType,
                Clinic = booking.Clinic,
                Address = booking.Address,
                IsCancelled = booking.IsCancelled,
                CancelDate = booking.CancelDate,
                Created = booking.Created,
                Updated = booking.Updated
            };
        }

        public Booking DtoToBooking(BookingDTO bookingDto)
        {
            return new Booking
            {
                Id = bookingDto.Id,
                CaregiverId = bookingDto.CaregiverId,
                PatientId = bookingDto.PatientId,
                MeetingDate = bookingDto.MeetingDate,
                MeetingType = bookingDto.MeetingType,
                Clinic = bookingDto.Clinic,
                Address = bookingDto.Address,
                IsCancelled = bookingDto.IsCancelled
            };
        }

        public Booking CreationToBooking(BookingCreation bookingCreation)
        {
            return new Booking
            {
                CaregiverId = bookingCreation.CaregiverId,
                MeetingDate = bookingCreation.MeetingDate,
                MeetingType = bookingCreation.MeetingType,
                Clinic = bookingCreation.Clinic,
                Address = bookingCreation.Address,
                Created = DateTime.UtcNow
            };
        }

        public Booking UpdateToBooking(Booking existingBooking, BookingUpdate bookingUpdate)
        {
            existingBooking.MeetingDate = bookingUpdate.MeetingDate ?? existingBooking.MeetingDate;
            existingBooking.MeetingType = bookingUpdate.MeetingType ?? existingBooking.MeetingType;
            existingBooking.Clinic = bookingUpdate.Clinic ?? existingBooking.Clinic;
            existingBooking.Address = bookingUpdate.Address ?? existingBooking.Address;
            return existingBooking;
        }
    }
}
