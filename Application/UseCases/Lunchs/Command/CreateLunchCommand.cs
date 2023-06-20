using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Lunchs.Command
{

    public class CreateLunchCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImgFile { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class CreateLunchCommandHandler : IRequestHandler<CreateLunchCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISaveImg _saveImg;
        public CreateLunchCommandHandler(IApplicationDbContext context, ISaveImg saveImg)
        {
            _context = context;
            _saveImg = saveImg;
        }

        public async Task<Guid> Handle(CreateLunchCommand request, CancellationToken cancellationToken)
        {
            var lunch = new Lunch
            {
                Id = request.Id,
                Name = request.Name,
                ImgFileName = _saveImg.SaveImage(request.ImgFile),
                Price = request.Price,
                Rewievs = request.Rewievs,
                Quality = request.Quality
            };

            var entity = await _context.Lunchs.AddAsync(lunch);
            await _context.SaveChangesAsync();

            return lunch.Id;
        }
    }
}
