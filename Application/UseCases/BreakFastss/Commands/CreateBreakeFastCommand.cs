using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BreakFasts.Commands
{
    public class CreateBreakeFastCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public IFormFile ImgFile { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class CreateBreakeFastCommandHandler : IRequestHandler<CreateBreakeFastCommand, Guid>
    {
        private readonly ISaveImg _saveImg;
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        public CreateBreakeFastCommandHandler(IApplicationDbContext context, IMediator mediator, ISaveImg saveImg)
        {
            _context = context;
            _mediator = mediator;
            _saveImg = saveImg;
        }

        public async Task<Guid> Handle(CreateBreakeFastCommand request, CancellationToken cancellationToken)
        {
            var breakFast = new BreakFast
            {
                Id = request.Id,
                Name = request.Name,
                ImgFileName = _saveImg.SaveImage(request.ImgFile),
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
