namespace Services;

public interface IEMailService
{
    public Task SendMail(string destination, string message, string? messageHeader = null);
}