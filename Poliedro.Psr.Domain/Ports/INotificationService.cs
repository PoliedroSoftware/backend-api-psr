namespace Poliedro.Psr.Domain.Ports
{
    public interface INotificationService
    {
        Task NotifyAsync(string data);
    }
}
