using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.SpetialMenuss.Commands
{
    public class UpdateSpetialMenuCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImgFile { get; set; }
        public string ImgFileName { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class UpdateSpetialMenuCommandHandler : IRequestHandler<UpdateSpetialMenuCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISaveImg _saveImg;

        public UpdateSpetialMenuCommandHandler(IApplicationDbContext context, ISaveImg saveImg)
        {
            _context = context;
            _saveImg = saveImg;
        }

        public async Task<Guid> Handle(UpdateSpetialMenuCommand request, CancellationToken cancellationToken)
        {
            SpecialMenu? breakFast = await _context.SpecialMenus.FindAsync(request.Id);
            if (breakFast != null)
            {
                throw new NotFoundException(nameof(SpecialMenu), request.Id);
            }

            string ImgSource = request.ImgFileName;
            if(request.ImgFile  != null)
            {
                ImgSource = _saveImg.SaveImage(request.ImgFile);
            }

            breakFast.Id = request.Id;
            breakFast.Name = request.Name;
            breakFast.ImgFileName = ImgSource;
            breakFast.Type = request.Type;
            breakFast.Price = request.Price;
            breakFast.Rewievs = request.Rewievs;
            breakFast.Quality = request.Quality;

            await _context.SaveChangesAsync(cancellationToken);

            return breakFast.Id;
        }
    }
}
