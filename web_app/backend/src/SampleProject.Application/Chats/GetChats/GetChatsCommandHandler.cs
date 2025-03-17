using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Chats.GetChats.Dto.Responses;
using SampleProject.Application.Tokens;
using SampleProject.Domain.Chats;

namespace SampleProject.Application.Chats.GetChats;

public class GetChatsCommandHandler : IRequestHandler<GetChatsCommand, GetChatsResponse>
{
    private readonly IChatRepository _chatRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public GetChatsCommandHandler(IChatRepository chatRepository, IJwtTokenService jwtTokenService)
    {
        _chatRepository = chatRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<GetChatsResponse> Handle(GetChatsCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _jwtTokenService.GetCurrentUserIdFromJwtToken();
        
        var chats = await _chatRepository.GetChatsByUserId(currentUserId);

        return new GetChatsResponse
        {
            Chats = chats
        };
    }
}