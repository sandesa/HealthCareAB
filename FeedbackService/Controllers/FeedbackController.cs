using Microsoft.AspNetCore.Mvc;

namespace FeedbackService.Controllers
{
    [ApiController]
    [Route("api/feedback")]
    public class FeedbackController
    {
        private readonly Services.FeedbackService _feedbackService;

        public FeedbackController(Services.FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }


    }
}
