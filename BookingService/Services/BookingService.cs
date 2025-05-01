using BookingService.DTO;
using BookingService.Interfaces;
using BookingService.Models;

namespace BookingService.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<ResponseDTO<IEnumerable<Booking>>> GetBookingsDevAsync()
        {
            try
            {
                var bookings = await _bookingRepository.GetBookingsDevAsync();
                if (!bookings.Any())
                {
                    return new ResponseDTO<IEnumerable<Booking>>
                    {
                        Message = "No bookings found.",
                        IsSuccess = true
                    };
                }
                return new ResponseDTO<IEnumerable<Booking>>
                {
                    Data = bookings,
                    Message = "Bookings retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting DEV Booking data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<IEnumerable<Booking>>
                {
                    Message = "An error occurred when getting DEV Booking data.",
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<BookingDTO>>> GetBookingsByUserIdAsync(int? caregiverId, int? patientId)
        {
            try
            {
                IEnumerable<BookingDTO> bookingDtos = [];

                if (caregiverId != null)
                {
                    bookingDtos = await _bookingRepository.GetBookingsByUserIdAsync(caregiverId: caregiverId, patientId: null);
                }

                if (patientId != null)
                {
                    bookingDtos = await _bookingRepository.GetBookingsByUserIdAsync(caregiverId: null, patientId: patientId);
                }

                if (!bookingDtos.Any())
                {
                    return new ResponseDTO<IEnumerable<BookingDTO>>
                    {
                        Message = "No bookings found.",
                        IsSuccess = true
                    };
                }
                return new ResponseDTO<IEnumerable<BookingDTO>>
                {
                    Data = bookingDtos,
                    Message = "Bookings retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting Booking data by Caregiver ID. Error message: \"{ex.Message}\"");
                return new ResponseDTO<IEnumerable<BookingDTO>>
                {
                    Message = "An error occurred when getting Booking data by Caregiver ID.",
                };
            }
        }

        public async Task<ResponseDTO<BookingDTO?>> GetBookingByIdAsync(int id)
        {
            try
            {
                var bookingDto = await _bookingRepository.GetBookingByIdAsync(id);
                if (bookingDto == null)
                {
                    return new ResponseDTO<BookingDTO?>
                    {
                        Message = "No booking found with this ID: " + id,
                    };
                }
                return new ResponseDTO<BookingDTO?>
                {
                    Data = bookingDto,
                    Message = "Booking retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting Booking data by ID. Error message: \"{ex.Message}\"");
                return new ResponseDTO<BookingDTO?>
                {
                    Message = "An error occurred when getting Booking data by ID.",
                };
            }
        }

        public async Task<ResponseDTO<BookingDTO?>> CreateBookingAsync(BookingCreation bookingCreation, int patientId)
        {
            try
            {
                var bookingDto = await _bookingRepository.CreateBookingAsync(bookingCreation, patientId);
                if (bookingDto == null)
                {
                    return new ResponseDTO<BookingDTO?>
                    {
                        Message = "Failed to create booking.",
                    };
                }
                return new ResponseDTO<BookingDTO?>
                {
                    Data = bookingDto,
                    Message = "Booking created successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when creating Booking data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<BookingDTO?>
                {
                    Message = "An error occurred when creating Booking data.",
                };
            }
        }

        public async Task<ResponseDTO<BookingDTO?>> UpdateBookingAsync(int id, BookingUpdate? bookingUpdate, bool cancel)
        {
            try
            {
                var bookingDto = await _bookingRepository.UpdateBookingAsync(id, bookingUpdate, cancel);
                if (bookingDto == null)
                {
                    return new ResponseDTO<BookingDTO?>
                    {
                        Message = "Could not find the existing booking.",
                    };
                }
                return new ResponseDTO<BookingDTO?>
                {
                    Data = bookingDto,
                    Message = "Booking updated successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when updating Booking data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<BookingDTO?>
                {
                    Message = "An error occurred when updating Booking data.",
                };
            }
        }

        public async Task<ResponseDTO<BookingDTO?>> DeleteBookingAsync(int id)
        {
            try
            {
                var bookingDto = await _bookingRepository.DeleteBookingAsync(id);
                if (bookingDto == null)
                {
                    return new ResponseDTO<BookingDTO?>
                    {
                        Message = "Failed to delete booking.",
                    };
                }
                return new ResponseDTO<BookingDTO?>
                {
                    Data = bookingDto,
                    Message = "Booking deleted successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when deleting Booking data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<BookingDTO?>
                {
                    Message = "An error occurred when deleting Booking data.",
                };
            }
        }
    }
}
