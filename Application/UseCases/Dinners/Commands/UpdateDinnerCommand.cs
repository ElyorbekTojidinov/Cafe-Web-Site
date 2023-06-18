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
        public IFormFile Img { get; set; }
        public string ImgName { get; set; }
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
            var breakFast = await _context.Dinners.FindAsync(request.Id);
            if (breakFast != null)
            {
                throw new NotFoundException(nameof(Dinner), request.Id);
            }

            string ImgSource = request.ImgName;
            if (request.Img != null)
            {
                ImgSource = _saveImg.SaveImage(request.Img);
            }

            breakFast.Name = request.Name;
            breakFast.Img = ImgSource;
            breakFast.Price = request.Price;
            breakFast.Rewievs = request.Rewievs;
            breakFast.Quality = request.Quality;

            await _context.SaveChangesAsync(cancellationToken);

            return breakFast.Id;
        }
    }
}
