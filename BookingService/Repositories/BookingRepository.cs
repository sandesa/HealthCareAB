using BookingService.Database;
using BookingService.DTO;
using BookingService.Interfaces;
using BookingService.Models;
using BookingService.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingMappingService _mapper;
        private readonly BookingDbContext _context;

        public BookingRepository(BookingMappingService mapper, BookingDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetBookingsDevAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByCaregiverIdAsync(int caregiverId)
        {
            var bookings = await _context.Bookings.Where(b => b.CaregiverId == caregiverId).ToListAsync();
            var bookingDTOs = bookings.Select(b => _mapper.BookingToDto(b));
            return bookingDTOs;
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByPatientIdAsync(int patientId)
        {
            var bookings = await _context.Bookings.Where(b => b.PatientId == patientId).ToListAsync();
            var bookingDTOs = bookings.Select(b => _mapper.BookingToDto(b));
            return bookingDTOs;
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(int? caregiverId, int? patientId)
        {
            List<Booking> bookings = [];

            if (caregiverId != null)
            {
                bookings = await _context.Bookings.Where(b => b.CaregiverId == caregiverId).ToListAsync();
            }

            if (patientId != null)
            {
                bookings = await _context.Bookings.Where(b => b.PatientId == patientId).ToListAsync();
            }

            var bookingDTOs = bookings.Select(b => _mapper.BookingToDto(b));
            return bookingDTOs;
        }

        public async Task<BookingDTO?> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null)
            {
                return null;
            }
            var bookingDTO = _mapper.BookingToDto(booking);
            return bookingDTO;
        }

        public async Task<BookingDTO?> CreateBookingAsync(BookingCreation bookingCreation)
        {
            var booking = _mapper.CreationToBooking(bookingCreation);
            booking.Created = DateTime.Now;

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            var bookingDTO = _mapper.BookingToDto(booking);
            return bookingDTO;
        }

        public async Task<BookingDTO?> UpdateBookingAsync(int id, BookingUpdate? bookingUpdate, bool cancel)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return null;
            }
            if (bookingUpdate != null)
            {
                booking = _mapper.UpdateToBooking(booking, bookingUpdate);
            }
            if (cancel)
            {
                booking.IsCancelled = true;
                booking.CancelDate = DateTime.Now;
            }
            booking.Updated = DateTime.Now;
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
            var bookingDTO = _mapper.BookingToDto(booking);
            return bookingDTO;
        }

        public async Task<BookingDTO?> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return null;
            }
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            var bookingDTO = _mapper.BookingToDto(booking);
            return bookingDTO;
        }
    }
}
