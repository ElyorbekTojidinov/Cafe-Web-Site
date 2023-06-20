using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Dinners.Commands
{

    public class UpdateDinnerCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImgFile { get; set; }
        public string ImgFileName { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class UpdateDinnerCommandHandler : IRequestHandler<UpdateDinnerCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISaveImg _saveImg;
        public UpdateDinnerCommandHandler(IApplicationDbContext context, ISaveImg saveImg)
        {
            _context = context;
            _saveImg = saveImg;
        }

        public async Task<Guid> Handle(UpdateDinnerCommand request, CancellationToken cancellationToken)
        {
            var dinner = await _context.Dinners.FindAsync(request.Id);
            if (dinner == null)
            {
                throw new NotFoundException(nameof(Dinner), request.Id);
            }

            string ImgSource = request.ImgFileName;
            if (request.ImgFile != null)
            {
                ImgSource = _saveImg.SaveImage(request.ImgFile);
            }
            dinner.Id = request.Id;
            dinner.Name = request.Name;
            dinner.ImgFileName = ImgSource;
            dinner.Price = request.Price;
            dinner.Rewievs = request.Rewievs;
            dinner.Quality = request.Quality;

            await _context.SaveChangesAsync(cancellationToken);

            return dinner.Id;
        }
    }
}
