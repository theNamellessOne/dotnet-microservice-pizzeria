namespace FavoriteService.SyncDataServices.Grpc.User;

public interface IUserDataClient
{
    IEnumerable<Models.User>? ReturnAllUsers();
}