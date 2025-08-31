using AvalonMozi.Application.Feedbacks;
using AvalonMozi.Application.Feedbacks.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvalonMozi.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN,EMPLOYEE")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost("NewFeedback")]
        [AllowAnonymous]
        public async Task<IActionResult> NewFeedback(FeedbackDto data)
        {
            await _feedbackService.AddNewFeedback(data);
            return Ok();
        }
    }
}
