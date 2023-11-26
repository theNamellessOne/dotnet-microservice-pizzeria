namespace OrderService.EventProcessing;

public interface IEventProcessor
{
    void ProcessEvent(string msg);
}