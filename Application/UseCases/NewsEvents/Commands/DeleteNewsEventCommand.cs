using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.NewsEvents.Commands
{

    public class DeleteNewsEventCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteNewsEventCommandHandler : IRequestHandler<DeleteNewsEventCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteNewsEventCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteNewsEventCommand request, CancellationToken cancellationToken)
        {
            var events = await _applicationDbContext.NewsEvents.FindAsync(request.Id);
            if (events == null)
            {
                throw new NotFoundException(nameof(NewsEvent), request.Id);
            }
            _applicationDbContext.NewsEvents.Remove(events);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
