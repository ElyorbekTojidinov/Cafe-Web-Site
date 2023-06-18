using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.UseCases.BreakFasts.Commands
{
    public class UpdateBreakFastCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImgFile { get; set; }
        public string ImgFileName { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class UpdateBreakFastCommandHandler : IRequestHandler<UpdateBreakFastCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISaveImg _saveImg;
        public UpdateBreakFastCommandHandler(IApplicationDbContext context, ISaveImg saveImg)
        {
            _context = context;
            _saveImg = saveImg;
        }

        public async Task<Guid> Handle(UpdateBreakFastCommand request, CancellationToken cancellationToken)
        {
            BreakFast breakFast = await _context.BreakFasts.FindAsync(request.Id);
            if (breakFast != null)
            {
                throw new NotFoundException(nameof(BreakFast), request.Id);
            }

            string imgSource = request.ImgFileName; 
            if(request.ImgFile != null)
            {
                imgSource = _saveImg.SaveImage(request.ImgFile);
            }

            breakFast.Name = request.Name;
            breakFast.ImgFileName = imgSource; 
            breakFast.Price = request.Price;
            breakFast.Rewievs = request.Rewievs;
            breakFast.Quality = request.Quality;

            await _context.SaveChangesAsync(cancellationToken);

            return breakFast.Id;

        }
    }
}
