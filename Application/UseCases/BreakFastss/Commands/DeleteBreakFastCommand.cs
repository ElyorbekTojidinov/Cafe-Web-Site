using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.BreakFasts.Commands
{
    public class DeleteBreakFastCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteBreakFastCommandHandler : IRequestHandler<DeleteBreakFastCommand, bool>
    {

        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IDeleteImg _deleteImg;
        public DeleteBreakFastCommandHandler(IApplicationDbContext applicationDbContext, IDeleteImg deleteImg)
        {
            _applicationDbContext = applicationDbContext;
            _deleteImg = deleteImg;
        }

        public async Task<bool> Handle(DeleteBreakFastCommand request, CancellationToken cancellationToken)
        {
            BreakFast? breakFast = await _applicationDbContext.BreakFasts.FindAsync(request.Id);
           
            if (breakFast == null)
            {
                throw new NotFoundException(nameof(BreakFasts), request.Id);
            }

            if (breakFast.ImgFileName is not null)
            {
                _deleteImg.Delete_Img(breakFast.ImgFileName);
            }

            _applicationDbContext.BreakFasts.Remove(breakFast);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
