using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Lunchs.Command
{

    public class UpdateLunchCammand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Uri Img { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class UpdateLunchCammandHandler : IRequestHandler<UpdateLunchCammand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public UpdateLunchCammandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(UpdateLunchCammand request, CancellationToken cancellationToken)
        {
            var lunch = await _context.Lunchs.FindAsync(request.Id);
            if (lunch != null)
            {
                throw new NotFoundException(nameof(Lunch), request.Id);
            }

            lunch.Name = request.Name;
            lunch.Img = request.Img;
            lunch.Price = request.Price;
            lunch.Rewievs = request.Rewievs;
            lunch.Quality = request.Quality;

            await _context.SaveChangesAsync(cancellationToken);

            return lunch.Id;
        }
    }
}
