using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Dinners.Commands
{
    public class CreateDinnerCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImgFile { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }


    public class CreateDinnerCommandHandler : IRequestHandler<CreateDinnerCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly ISaveImg _saveImg;
        public CreateDinnerCommandHandler(IApplicationDbContext context, IMediator mediator, ISaveImg saveImg)
        {
            _context = context;
            _mediator = mediator;
            _saveImg = saveImg;
        }

        public async Task<Guid> Handle(CreateDinnerCommand request, CancellationToken cancellationToken)
        {
            var breakFast = new Dinner
            {
                Name = request.Name,
                ImgFileName = _saveImg.SaveImage(request.ImgFile),
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
