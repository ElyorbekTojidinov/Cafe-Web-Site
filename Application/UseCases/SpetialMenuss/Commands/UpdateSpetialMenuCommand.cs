using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.SpetialMenuss.Commands
{
    public class UpdateSpetialMenuCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Uri Img { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class UpdateSpetialMenuCommandHandler : IRequestHandler<UpdateSpetialMenuCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSpetialMenuCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(UpdateSpetialMenuCommand request, CancellationToken cancellationToken)
        {
            var breakFast = await _context.SpecialMenus.FindAsync(request.Id);
            if (breakFast != null)
            {
                throw new NotFoundException(nameof(SpecialMenu), request.Id);
            }

            breakFast.Id = request.Id;
            breakFast.Name = request.Name;
            breakFast.Img = request.Img;
            breakFast.Type = request.Type;
            breakFast.Price = request.Price;
            breakFast.Rewievs = request.Rewievs;
            breakFast.Quality = request.Quality;

            await _context.SaveChangesAsync(cancellationToken);

            return breakFast.Id;
        }
    }
}
