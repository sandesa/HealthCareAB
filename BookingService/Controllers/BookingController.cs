using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly Services.BookingService _bookingService;

        public BookingController(Services.BookingService bookingService)
        {
            _bookingService = bookingService;
        }
    }
}
