using FeedbackService.DTO;
using FeedbackService.Interfaces;
using FeedbackService.Models;

namespace FeedbackService.Services
{
    public class FeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<ResponseDTO<IEnumerable<Feedback>>> GetFeedbacksDevAsync()
        {
            try
            {
                var feedbacks = await _feedbackRepository.GetFeedbacksDevAsync();
                if (feedbacks == null || !feedbacks.Any())
                {
                    return new ResponseDTO<IEnumerable<Feedback>>
                    {
                        IsSuccess = true,
                        Message = "No feedbacks found."
                    };
                }
                return new ResponseDTO<IEnumerable<Feedback>>
                {
                    Data = feedbacks,
                    IsSuccess = true,
                    Message = "Feedbacks successfully found."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<Feedback>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while fetching feedbacks: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO<FeedbackDTO>> GetFeedbackByIdAsync(int id)
        {
            try
            {
                var feedback = await _feedbackRepository.GetFeedbackByIdAsync(id);
                if (feedback == null)
                {
                    return new ResponseDTO<FeedbackDTO>
                    {
                        IsSuccess = false,
                        Message = "Feedback not found."
                    };
                }
                return new ResponseDTO<FeedbackDTO>
                {
                    Data = feedback,
                    IsSuccess = true,
                    Message = "Feedback successfully fetched."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<FeedbackDTO>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while fetching feedback: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO<FeedbackDTO>> GetFeedbackByBookingIdAsync(int bookingId)
        {
            try
            {
                var feedback = await _feedbackRepository.GetFeedbackByBookingIdAsync(bookingId);
                if (feedback == null)
                {
                    return new ResponseDTO<FeedbackDTO>
                    {
                        IsSuccess = false,
                        Message = "Feedback not found."
                    };
                }
                return new ResponseDTO<FeedbackDTO>
                {
                    Data = feedback,
                    IsSuccess = true,
                    Message = "Feedback successfully fetched."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<FeedbackDTO>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while fetching feedback: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO<FeedbackDTO>> CreateFeedbackAsync(FeedbackCreate feedbackCreate)
        {
            try
            {
                var feedback = await _feedbackRepository.CreateFeedbackAsync(feedbackCreate);
                if (feedback == null)
                {
                    return new ResponseDTO<FeedbackDTO>
                    {
                        IsSuccess = false,
                        Message = "Failed to create feedback."
                    };
                }
                return new ResponseDTO<FeedbackDTO>
                {
                    Data = feedback,
                    IsSuccess = true,
                    Message = "Feedback successfully created."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<FeedbackDTO>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while creating feedback: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO<FeedbackDTO>> UpdateFeedbackAsync(int id, FeedbackUpdate feedbackUpdate)
        {
            try
            {
                var feedback = await _feedbackRepository.UpdateFeedbackAsync(id, feedbackUpdate);
                if (feedback == null)
                {
                    return new ResponseDTO<FeedbackDTO>
                    {
                        IsSuccess = false,
                        Message = "Couldn't find existing feedback."
                    };
                }
                return new ResponseDTO<FeedbackDTO>
                {
                    Data = feedback,
                    IsSuccess = true,
                    Message = "Feedback successfully updated."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<FeedbackDTO>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while updating feedback: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO<FeedbackDTO>> DeleteFeedbackAsync(int id)
        {
            try
            {
                var feedback = await _feedbackRepository.DeleteFeedbackAsync(id);
                if (feedback == null)
                {
                    return new ResponseDTO<FeedbackDTO>
                    {
                        IsSuccess = false,
                        Message = "Couldn't find existing feedback."
                    };
                }
                return new ResponseDTO<FeedbackDTO>
                {
                    Data = feedback,
                    IsSuccess = true,
                    Message = "Feedback successfully deleted."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<FeedbackDTO>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while deleting feedback: {ex.Message}"
                };
            }
        }
    }
}
