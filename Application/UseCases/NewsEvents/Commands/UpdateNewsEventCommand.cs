using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.NewsEvents.Commands
{

    public class UpdateNewsEventCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Uri Img { get; set; }
        public string Time { get; set; }
        public string Priority { get; set; }
    }

    public class UpdateNewsEventCommandHandler : IRequestHandler<UpdateNewsEventCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public UpdateNewsEventCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(UpdateNewsEventCommand request, CancellationToken cancellationToken)
        {
            var news = await _context.NewsEvents.FindAsync(request.Id);
            if (news != null)
            {
                throw new NotFoundException(nameof(NewsEvent), request.Id);
            }

            news.Id = news.Id;
            news.Img = request.Img;
            news.Description = news.Description;
            news.Time = news.Time;
            news.Priority = request.Priority;

            await _context.SaveChangesAsync(cancellationToken);

            return news.Id;
        }
    }
}
