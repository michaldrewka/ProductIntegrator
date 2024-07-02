namespace ProductIntegrator.Models;

public class ErrorViewModel
{
    public ErrorViewModel(object requestId)
    {
        RequestId = requestId;
    }

    public bool ShowRequestId { get; set; }
    public object RequestId { get; set; }
}