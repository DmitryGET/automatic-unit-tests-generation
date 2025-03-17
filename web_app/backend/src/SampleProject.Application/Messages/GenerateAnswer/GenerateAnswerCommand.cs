using System;
using System.Collections.Generic;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Messages.Dto;
using SampleProject.Application.Messages.GenerateAnswer.Dto.Responses;

namespace SampleProject.Application.Messages.GenerateAnswer;

public class GenerateAnswerCommand : CommandBase<GenerateAnswerResponse>
{
    public GenerateAnswerCommand(string model, Guid chatId, string message)
    {
        Model = model;
        ChatId = chatId;
        Message = message;
    }
    
    public string Model { get; set; }

    public Guid ChatId { get; set; }

    public string Message { get; set; }
}