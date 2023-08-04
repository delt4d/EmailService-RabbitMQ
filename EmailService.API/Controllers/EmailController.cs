using EmailService.API.Models;
using EmailService.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly IMailProducer _mailProducer;
    public static readonly List<Email> _emails = new();

    public EmailController(
        ILogger<EmailController> logger,
        IMailProducer mailProducer
        )
    {
        _logger = logger;
        _mailProducer = mailProducer;
    }

    [HttpGet("/")]
    public RedirectResult HealthCheck()
    {
        return Redirect("/swagger");
    }

    [HttpPost]
    public IActionResult SendEmail(Email email)
    {
        if (!ModelState.IsValid) return BadRequest();

        _emails.Add(email);
        _mailProducer.SendEmailMessage(email);

        return Ok();
    }
}
