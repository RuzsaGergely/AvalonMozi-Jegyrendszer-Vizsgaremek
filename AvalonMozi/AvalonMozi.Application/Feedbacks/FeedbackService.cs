using AvalonMozi.Application.Feedbacks.Dto;
using AvalonMozi.Domain.Feedbacks;
using AvalonMozi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Feedbacks
{
    public class FeedbackService : IFeedbackService
    {
        private readonly AvalonContext _context;
        public FeedbackService(AvalonContext context)
        {
            _context = context;
        }
        public async Task AddNewFeedback(FeedbackDto feedback)
        {
            var newObject = new Feedback()
            {
                Deleted = false,
                Email = feedback.Email ?? "",
                Message = feedback.Message,
                Name = feedback.Name ?? "",
                Phone = feedback.Phone ?? "",
            };
            await _context.Feedbacks.AddAsync(newObject);
            await _context.SaveChangesAsync();
        }
    }
}
