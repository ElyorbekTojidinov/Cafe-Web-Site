using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.NewsEvents.Commands
{
   
    public class CreateNewsEventCommand : IRequest<Guid>
    {
        public string Description { get; set; }
        public Uri Img { get; set; }
        public string Time { get; set; }
        public string Priority { get; set; }
    }

    public class CreateNewsEventCommandHandler : IRequestHandler<CreateNewsEventCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        public CreateNewsEventCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateNewsEventCommand request, CancellationToken cancellationToken)
        {
            var events = new NewsEvent
            {
                Description = request.Description,
                Img = request.Img,
                Time = request.Time,
                Priority = request.Priority
            };

            var entity = await _context.NewsEvents.AddAsync(events);
            await _context.SaveChangesAsync();

            return events.Id;
        }
    }
}
