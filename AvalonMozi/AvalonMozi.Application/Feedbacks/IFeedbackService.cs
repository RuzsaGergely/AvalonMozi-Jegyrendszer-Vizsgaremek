using AvalonMozi.Application.Feedbacks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Feedbacks
{
    public interface IFeedbackService
    {
        Task AddNewFeedback(FeedbackDto feedback);
    }
}
