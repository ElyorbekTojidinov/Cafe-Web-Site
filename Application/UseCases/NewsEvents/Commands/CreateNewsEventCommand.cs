using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.NewsEvents.Commands
{
   
    public class CreateNewsEventCommand : IRequest<Guid>
    {
        public string Description { get; set; }
        public IFormFile ImgFile { get; set; }
        public string Time { get; set; }
        public string Priority { get; set; }
    }

    public class CreateNewsEventCommandHandler : IRequestHandler<CreateNewsEventCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly ISaveImg _saveImg;
        public CreateNewsEventCommandHandler(IApplicationDbContext context, IMediator mediator, ISaveImg saveImg)
        {
            _context = context;
            _mediator = mediator;
            _saveImg = saveImg;
        }

        public async Task<Guid> Handle(CreateNewsEventCommand request, CancellationToken cancellationToken)
        {
            var events = new NewsEvent
            {
                Description = request.Description,
                ImgFileName =_saveImg.SaveImage(request.ImgFile),
                Time = request.Time,
                Priority = request.Priority
            };

            var entity = await _context.NewsEvents.AddAsync(events);
            await _context.SaveChangesAsync();

            return events.Id;
        }
    }
}
