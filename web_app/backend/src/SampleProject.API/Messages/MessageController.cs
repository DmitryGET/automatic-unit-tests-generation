using System;
using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Messages.DeleteMessage;
using SampleProject.Application.Messages.GenerateAnswer;
using SampleProject.Application.Messages.GenerateAnswer.Dto.Requests;
using SampleProject.Application.Messages.GenerateAnswer.Dto.Responses;

namespace SampleProject.API.Messages;

[Route("api/v1/messages")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class MessageController : Controller
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Generate answer to user question
    /// </summary>
    [HttpPost]
    [ProducesResponseType<GenerateAnswerResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GenerateAnswer([FromBody] GenerateAnswerRequest request)
    {
        var response = await _mediator.Send(new GenerateAnswerCommand(request.Model, request.ChatId, request.Message));
        return Ok(response);
    }

    /// <summary>
    /// Delete message
    /// </summary>
    [HttpDelete("{messageId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteMessage([FromRoute] Guid messageId)
    {
        var response = await _mediator.Send(new DeleteMessageCommand(messageId));
        return Ok(response);
    }
}