using System;
using System.Threading.Tasks;
using MediatR;

namespace SampleProject.Infrastructure.Processing.InternalCommands
{
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IMediator _mediator;

        public CommandsDispatcher(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchCommandAsync(Guid id)
        {
            /*var internalCommand = await this._ordersContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            Type type = Assembly.GetAssembly(typeof(MarkCustomerAsWelcomedCommand)).GetType(internalCommand.Type);
            dynamic command = JsonConvert.DeserializeObject(internalCommand.Data, type);

            internalCommand.ProcessedDate = DateTime.UtcNow;

            await this._mediator.Send(command);*/
        }
    }
}