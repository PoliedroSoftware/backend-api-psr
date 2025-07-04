namespace Poliedro.Psr.Domain.Wrapper;

public class ApiResponse<T>
{
    public string Status { get; set; } = "OK";
    public string Message { get; set; } = "Operation completed successfully.";
    public string CorrelationId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public T Data { get; set; }
}


