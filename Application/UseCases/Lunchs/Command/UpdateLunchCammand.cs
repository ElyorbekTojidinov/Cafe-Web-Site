using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Lunchs.Command
{

    public class UpdateLunchCammand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImgFileName { get; set; }
        public IFormFile ImgFile { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class UpdateLunchCammandHandler : IRequestHandler<UpdateLunchCammand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISaveImg _saveImg;
        public UpdateLunchCammandHandler(IApplicationDbContext context, ISaveImg saveImg = null)
        {
            _context = context;
            _saveImg = saveImg;
        }

        public async Task<Guid> Handle(UpdateLunchCammand request, CancellationToken cancellationToken)
        {
            var lunch = await _context.Lunchs.FindAsync(request.Id);
            if (lunch != null)
            {
                throw new NotFoundException(nameof(Lunch), request.Id);
            }

            string ImgSource = request.ImgFileName;
            if(request.ImgFile != null)
            {
                ImgSource = _saveImg.SaveImage(request.ImgFile);
            }

            lunch.Name = request.Name;
            lunch.ImgFileName = ImgSource;
            lunch.Price = request.Price;
            lunch.Rewievs = request.Rewievs;
            lunch.Quality = request.Quality;

            await _context.SaveChangesAsync(cancellationToken);

            return lunch.Id;
        }
    }
}
