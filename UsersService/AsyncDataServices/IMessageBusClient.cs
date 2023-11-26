using UserService.Dtos;

namespace UserService.AsyncDataServices;

public interface IMessageBusClient
{
    void PublishUser(UserPublishDto userPublishDto);
}