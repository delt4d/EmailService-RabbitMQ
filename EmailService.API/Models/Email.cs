namespace EmailService.API.Models;

public class Email
{
    public string Id { get; set; } = "";
    public string From { get; set; } = "";
    public string Recipient { get; set; } = "";
    public string Body { get; set; } = "";
    public string Subject { get; set; } = "";
}