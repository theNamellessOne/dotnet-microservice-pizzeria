using AutoMapper;
using Grpc.Core;
using UserService.Data.Repositories;

namespace UserService.SyncDataServices.Grpc;

//GrpcUserBase генериться автоматично через user.proto
public class GrpcUserService(IUsersRepository usersRepository, IMapper mapper) : GrpcUser.GrpcUserBase
{
    //отримати всіх юзерів (цей метод зможуть викликати інші сервіси)
    public override Task<UserResponse> GetAllUsers(GetAllRequest request, ServerCallContext context)
    {
        var users = usersRepository.GetAllUsers();
        var response = new UserResponse();

        foreach (var user in users) response.User.Add(mapper.Map<GrpcUserModel>(user));

        return Task.FromResult(response);
    }
}