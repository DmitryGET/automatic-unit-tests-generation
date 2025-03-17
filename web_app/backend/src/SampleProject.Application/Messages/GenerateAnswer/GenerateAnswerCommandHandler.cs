using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.AIModels.Dto.Requests;
using SampleProject.Application.AIModels.Interfaces;
using SampleProject.Application.Exceptions;
using SampleProject.Application.Messages.Constants;
using SampleProject.Application.Messages.Dto;
using SampleProject.Application.Messages.GenerateAnswer.Dto.Responses;
using SampleProject.Application.Tokens;
using SampleProject.Domain.Chats;
using SampleProject.Domain.Messages;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Application.Messages.GenerateAnswer;

public class GenerateAnswerCommandHandler : IRequestHandler<GenerateAnswerCommand, GenerateAnswerResponse>
{
    private readonly IAiModelService _aiModelService;
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;

    public GenerateAnswerCommandHandler(
        IAiModelService aiModelService, 
        IMessageRepository messageRepository, 
        IChatRepository chatRepository, 
        IUnitOfWork unitOfWork,
        IJwtTokenService jwtTokenService)
    {
        _aiModelService = aiModelService;
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<GenerateAnswerResponse> Handle(GenerateAnswerCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var currentUserId = _jwtTokenService.GetCurrentUserIdFromJwtToken();
        
            var existingChat = await _chatRepository.GetChatByChatIdAndUserId(request.ChatId, currentUserId);
        
            if (existingChat is null)
            {
                throw new EntityNotFoundException($"Chat with id '{request.ChatId}' was not found for user with id '{currentUserId}'");
            }
        
            var messagesByChat = await _messageRepository.GetMessagesByChatIdAsync(request.ChatId);
            var maxOrderChat = messagesByChat.Count != 0 ? messagesByChat.Max(x => x.Order) : 0;
                
            var userMessage = new Message
            {
                Id = Guid.NewGuid(),
                ChatId = request.ChatId, 
                Order = maxOrderChat + 1,
                Role = LlamaMessageRole.User,
                Content = request.Message
            };
                
            await _messageRepository.AddAsync(userMessage);
        
            maxOrderChat += 1;
                
            var updatedMessagesByChat = messagesByChat.Concat([userMessage]);
                
            var messageInfoList = updatedMessagesByChat.Select(m => new MessageInfo
            {
                Role = m.Role,
                Content = m.Content
            }).ToList();
                
            var response = await _aiModelService.GenerateAiAnswerWithChatContextAsync(new GenerateAiAnswerWithChatContextRequest
            {
                Model = "llama3",
                Messages = messageInfoList,
                Stream = false
            });
        
            var aiResponseMessage = new Message
            {
                Id = Guid.NewGuid(),
                ChatId = request.ChatId,
                Order = maxOrderChat + 1,
                Role = LlamaMessageRole.Assistant,
                Content = response.Message.Content
            };
        
            await _messageRepository.AddAsync(aiResponseMessage);
                
            await transaction.CommitAsync(cancellationToken);
        
            return new GenerateAnswerResponse
            {
                Content = response.Message.Content
            };
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}