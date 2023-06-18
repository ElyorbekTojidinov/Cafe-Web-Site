using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.NewsEvents.Commands
{

    public class UpdateNewsEventCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public IFormFile ImgFile { get; set; }
        public string ImgFileName { get; set; }
        public string Time { get; set; }
        public string Priority { get; set; }
    }

    public class UpdateNewsEventCommandHandler : IRequestHandler<UpdateNewsEventCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISaveImg _saveImg;
        public UpdateNewsEventCommandHandler(IApplicationDbContext context, ISaveImg saveImg)
        {
            _context = context;
            _saveImg = saveImg;
        }

        public async Task<Guid> Handle(UpdateNewsEventCommand request, CancellationToken cancellationToken)
        {
            var news = await _context.NewsEvents.FindAsync(request.Id);
            if (news != null)
            {
                throw new NotFoundException(nameof(NewsEvent), request.Id);
            }

            string ImgSource = request.ImgFileName;
            if(request.ImgFile != null)
            {
                ImgSource = _saveImg.SaveImage(request.ImgFile);
            }
            news.Id = news.Id;
            news.ImgFileName = ImgSource;
            news.Description = news.Description;
            news.Time = news.Time;
            news.Priority = request.Priority;

            await _context.SaveChangesAsync(cancellationToken);

            return news.Id;
        }
    }
}
