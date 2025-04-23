using FeedbackService.Database;
using FeedbackService.DTO;
using FeedbackService.Interfaces;
using FeedbackService.Models;
using FeedbackService.Services;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedbackDbContext _context;
        private readonly FeedbackMappingService _mapper;

        public FeedbackRepository(FeedbackDbContext context, FeedbackMappingService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksDevAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<FeedbackDTO?> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
            if (feedback == null)
            {
                return null;
            }

            var feedbackDTO = _mapper.FeedbackToDto(feedback);
            return feedbackDTO;
        }

        public async Task<FeedbackDTO?> GetFeedbackByBookingIdAsync(int bookingId)
        {
            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(f => f.BookingId == bookingId);
            if (feedback == null)
            {
                return null;
            }

            var feedbackDTO = _mapper.FeedbackToDto(feedback);
            return feedbackDTO;
        }

        public async Task<FeedbackDTO?> CreateFeedbackAsync(FeedbackCreate feedbackCreate)
        {
            var feedback = _mapper.CreateToFeedback(feedbackCreate);

            feedback.Created = DateTime.UtcNow;
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            var feedbackDTO = _mapper.FeedbackToDto(feedback);
            return feedbackDTO;
        }

        public async Task<FeedbackDTO?> UpdateFeedbackAsync(int id, FeedbackUpdate feedbackUpdate)
        {
            var existingFeedback = await _context.Feedbacks.FindAsync(id);
            if (existingFeedback == null)
            {
                return null;
            }
            if (feedbackUpdate != null)
            {
                existingFeedback = _mapper.UpdateToFeedback(existingFeedback, feedbackUpdate);
            }
            existingFeedback.Updated = DateTime.UtcNow;

            _context.Feedbacks.Update(existingFeedback);
            await _context.SaveChangesAsync();

            var feedbackDTO = _mapper.FeedbackToDto(existingFeedback);
            return feedbackDTO;
        }

        public async Task<FeedbackDTO?> DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return null;
            }
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            var feedbackDTO = _mapper.FeedbackToDto(feedback);
            return feedbackDTO;
        }
    }
}
