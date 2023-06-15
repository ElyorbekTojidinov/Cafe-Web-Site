using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.BreakFasts.Commands
{
    public class UpdateBreakFastCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Uri Img { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class UpdateBreakFastCommandHandler : IRequestHandler<UpdateBreakFastCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBreakFastCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(UpdateBreakFastCommand request, CancellationToken cancellationToken)
        {
            var breakFast = await _context.BreakFasts.FindAsync(request.Id);
            if (breakFast != null)
            {
                throw new NotFoundException(nameof(BreakFast), request.Id);
            }

            breakFast.Name = request.Name;
            breakFast.Img = request.Img; 
            breakFast.Price = request.Price;
            breakFast.Rewievs = request.Rewievs;
            breakFast.Quality = request.Quality;

            await _context.SaveChangesAsync(cancellationToken);

            return breakFast.Id;
        }
    }
}
