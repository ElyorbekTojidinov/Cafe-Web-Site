using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Dinners.Commands
{

    public class UpdateDinnerCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Uri Img { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class UpdateDinnerCommandHandler : IRequestHandler<UpdateDinnerCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDinnerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(UpdateDinnerCommand request, CancellationToken cancellationToken)
        {
            var breakFast = await _context.Dinners.FindAsync(request.Id);
            if (breakFast != null)
            {
                throw new NotFoundException(nameof(Dinner), request.Id);
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
