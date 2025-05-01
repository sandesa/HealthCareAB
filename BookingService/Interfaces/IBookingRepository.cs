using BookingService.DTO;
using BookingService.Models;

namespace BookingService.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsDevAsync();
        Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(int? caregiverId, int? patientId);
        Task<IEnumerable<BookingDTO>> GetBookingsByCaregiverIdAsync(int caregiverId);
        Task<IEnumerable<BookingDTO>> GetBookingsByPatientIdAsync(int patientId);
        Task<BookingDTO?> GetBookingByIdAsync(int id);
        Task<BookingDTO?> CreateBookingAsync(BookingCreation bookingCreation, int patientId);
        Task<BookingDTO?> UpdateBookingAsync(int id, BookingUpdate? bookingUpdate, bool cancel);
        Task<BookingDTO?> DeleteBookingAsync(int id);
    }
}
