using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IEmailSender _emailSender;

        public TestController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/test/send-email")]
        public async Task<IActionResult> SendTestEmail()
        {
            try
            {
                await _emailSender.SendEmailAsync("ahmedrefaatsenger@gmail.com", "Test Subject", "<p>This is a test email.</p>");
                return Ok("Test email sent successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error sending test email: {ex.Message}");
            }
        }
    }
}
