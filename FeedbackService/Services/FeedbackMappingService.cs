using FeedbackService.DTO;
using FeedbackService.Models;

namespace FeedbackService.Services
{
    public class FeedbackMappingService
    {
        public FeedbackDTO FeedbackToDto(Feedback feedback)
        {
            return new FeedbackDTO
            {
                BookingId = feedback.BookingId,
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                Created = feedback.Created
            };
        }

        public Feedback CreateToFeedback(FeedbackCreate feedbackCreate)
        {
            return new Feedback
            {
                BookingId = feedbackCreate.BookingId,
                Rating = feedbackCreate.Rating,
                Comment = feedbackCreate.Comment,
            };
        }

        public Feedback UpdateToFeedback(Feedback existingFeedback, FeedbackUpdate feedbackUpdate)
        {
            existingFeedback.Id = existingFeedback.Id;
            existingFeedback.BookingId = existingFeedback.BookingId;
            existingFeedback.Rating = feedbackUpdate.Rating ?? existingFeedback.Rating;
            existingFeedback.Comment = feedbackUpdate.Comment ?? existingFeedback.Comment;
            existingFeedback.Created = existingFeedback.Created;
            return existingFeedback;
        }

        public Feedback DtoToFeedback(FeedbackDTO feedbackDTO)
        {
            return new Feedback
            {
                BookingId = feedbackDTO.BookingId,
                Rating = feedbackDTO.Rating,
                Comment = feedbackDTO.Comment,
                Created = feedbackDTO.Created,
                Updated = feedbackDTO.Updated
            };
        }

    }
}
