using FeedbackService.DTO;
using FeedbackService.Models;

namespace FeedbackService.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> GetFeedbacksDevAsync();
        Task<FeedbackDTO?> GetFeedbackByIdAsync(int id);
        Task<FeedbackDTO?> GetFeedbackByBookingIdAsync(int bookingId);
        Task<FeedbackDTO?> CreateFeedbackAsync(FeedbackCreate feedbackCreate);
        Task<FeedbackDTO?> UpdateFeedbackAsync(int id, FeedbackUpdate feedbackUpdate);
        Task<FeedbackDTO?> DeleteFeedbackAsync(int id);
    }
}
