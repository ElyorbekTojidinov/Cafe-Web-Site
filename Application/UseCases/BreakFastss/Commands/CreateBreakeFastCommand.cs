using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.BreakFasts.Commands
{
    public class CreateBreakeFastCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Uri Img { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class CreateBreakeFastCommandHandler : IRequestHandler<CreateBreakeFastCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        public CreateBreakeFastCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateBreakeFastCommand request, CancellationToken cancellationToken)
        {
            var breakFast = new BreakFast
            {
                Name = request.Name,
                Img = request.Img,
                Price = request.Price,
                Rewievs = request.Rewievs,
                Quality = request.Quality
            };

            var entity = await _context.BreakFasts.AddAsync(breakFast);
            await _context.SaveChangesAsync();

            return breakFast.Id;
        }
    }

}
