using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Users.GetAllUsers.Dto.Responses;
using SampleProject.Application.Users.Mappings;
using SampleProject.Domain.Users;

namespace SampleProject.Application.Users.GetAllUsers;

public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, GetAllUsersResponse>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetAllUsersResponse> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        var usersResponse = users.Select(u => u.ToGetUserResponse());
        
        return new GetAllUsersResponse
        {
            Users = usersResponse
        };
    }
}