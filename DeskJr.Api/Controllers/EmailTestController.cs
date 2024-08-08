using DeskJr.Common;
using DeskJr.Service.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTestController : ControllerBase
    {
        private readonly EmailSender _emailSender;

        public EmailTestController(EmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail()
        {
            var request = new EmailRequest
            {
                toEmail = "aliaksu347@gmail.com",
                leaderName = "Arif Uslu",
                employeeName = "Ali Aksu",
                startDate = new DateTime(2024, 8, 15),
                endDate = new DateTime(2024, 8, 20)
            };

            await SendLeaveRequestNotificationAsync(request.toEmail, request.leaderName, request.employeeName, request.startDate, request.endDate);
            return Ok("Email sent successfully");
        }
        private async Task SendLeaveRequestNotificationAsync(string toEmail, string teamLeaderName, string employeeName, DateTime startDate, DateTime endDate)
        {
            string template = EmailTemplates.LeaveRequestNotificationTemplate;
            var variables = new Dictionary<string, string>
            {
                { "TeamLeaderName", teamLeaderName },
                { "EmployeeName", employeeName },
                { "StartDate", startDate.ToString("yyyy-MM-dd") },
                { "EndDate", endDate.ToString("yyyy-MM-dd") }
            };
            await _emailSender.SendEmailAsync(toEmail, "İzin Talebi Bildirimi", template, variables);
        }
    }
    public class EmailRequest
    {
        public string toEmail { get; set; }
        public string leaderName { get; set; }
        public string employeeName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}

