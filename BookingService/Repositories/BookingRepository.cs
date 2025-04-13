using BookingService.Interfaces;
using BookingService.Services;

namespace BookingService.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingMappingService _mapper;

        public BookingRepository(BookingMappingService mapper)
        {
            _mapper = mapper;
        }
    }
}
