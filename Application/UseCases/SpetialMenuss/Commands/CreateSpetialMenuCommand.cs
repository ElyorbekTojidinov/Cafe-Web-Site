using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.SpetialMenus.Commands
{
    public class CreateSpetialMenuCommand  : IRequest<Guid>
    {
        public string Name { get; set; }

        public IFormFile ImgFile { get; set; }
        public string ImgFileName { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class CreateSpetialMenuCommandHandler : IRequestHandler<CreateSpetialMenuCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly ISaveImg _saveImg;
        public CreateSpetialMenuCommandHandler(IApplicationDbContext context, IMediator mediator, ISaveImg saveImg)
        {
            _context = context;
            _mediator = mediator;
            _saveImg = saveImg;
        }

        public async Task<Guid> Handle(CreateSpetialMenuCommand request, CancellationToken cancellationToken)
        {
            var spMenu = new SpecialMenu
            {
                Name = request.Name,
                ImgFileName =_saveImg.SaveImage( request.ImgFile),
                Type = request.Type,
                Price = request.Price,
                Rewievs = request.Rewievs,
                Quality = request.Quality
            };

            var result = await _context.SpecialMenus.AddAsync(spMenu);
            await _context.SaveChangesAsync();

            return spMenu.Id;
        }
    }
}
