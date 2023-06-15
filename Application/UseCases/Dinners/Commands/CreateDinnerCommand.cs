using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Dinners.Commands
{
    public class CreateDinnerCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Uri Img { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }
  

    public class CreateDinnerCommandHandler : IRequestHandler<CreateDinnerCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        public CreateDinnerCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateDinnerCommand request, CancellationToken cancellationToken)
        {
            var breakFast = new Dinner
            {
                Name = request.Name,
                Img = request.Img,
                Price = request.Price,
                Rewievs = request.Rewievs,
                Quality = request.Quality
            };

            var entity = await _context.Dinners.AddAsync(breakFast);
            await _context.SaveChangesAsync();

            return breakFast.Id;
        }
    }
}
