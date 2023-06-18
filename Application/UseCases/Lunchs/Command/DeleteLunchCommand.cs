using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Lunchs.Command
{

    public class DeleteLunchCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteLunchCommandHandler : IRequestHandler<DeleteLunchCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IDeleteImg _daleteImg;
        public DeleteLunchCommandHandler(IApplicationDbContext applicationDbContext, IDeleteImg daleteImg)
        {
            _applicationDbContext = applicationDbContext;
            _daleteImg = daleteImg;
        }

        public async Task<bool> Handle(DeleteLunchCommand request, CancellationToken cancellationToken)
        {
            Lunch? lunch = await _applicationDbContext.Lunchs.FindAsync(request.Id);
            if (lunch == null)
            {
                throw new NotFoundException(nameof(Lunch), request.Id);
            }

            if(lunch.ImgFileName is not null)
            {
                _daleteImg.Delete_Img(lunch.ImgFileName);
            }

            _applicationDbContext.Lunchs.Remove(lunch);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
