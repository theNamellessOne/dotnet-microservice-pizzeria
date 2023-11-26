using AutoMapper;
using Grpc.Net.Client;
using UserService;

namespace FavoriteService.SyncDataServices.Grpc.User;

public class UserDataClient(IConfiguration configuration, IMapper mapper) : IUserDataClient
{
    public IEnumerable<Models.User>? ReturnAllUsers()
    {
        Console.WriteLine($"---> Calling GRPC User Service {configuration["GrpcUser"]}");
        var channel = GrpcChannel.ForAddress(configuration["GrpcUser"]!);
        var client = new GrpcUser.GrpcUserClient(channel);
        var request = new GetAllRequest();

        try
        {
            var response = client.GetAllUsers(request);
            return mapper.Map<IEnumerable<Models.User>>(response.User);
        }
        catch (Exception e)
        {
            Console.WriteLine($"---> Could not call GRPC User Service {configuration["GrpcUser"]}");
            Console.WriteLine($"---> Error {e.Message}");
            return null;
        }
    }
}