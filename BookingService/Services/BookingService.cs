using BookingService.Interfaces;

namespace BookingService.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService (IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
    }
}
