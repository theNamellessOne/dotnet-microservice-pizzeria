namespace FavoriteService.EventProcessing;

public interface IEventProcessor
{
    void ProcessEvent(string msg);
}