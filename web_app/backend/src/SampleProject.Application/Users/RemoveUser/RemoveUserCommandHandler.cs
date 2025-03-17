using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Users;

namespace SampleProject.Application.Users.RemoveUser;

public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.RemoveAsync(request.UserId);
        return Unit.Value;
    }
}