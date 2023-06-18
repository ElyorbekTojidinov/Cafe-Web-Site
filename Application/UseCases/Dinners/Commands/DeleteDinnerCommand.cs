using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Application.UseCases.Dinners.Commands
{

    public class DeleteDinnerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteDinnerCommandHandler : IRequestHandler<DeleteDinnerCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IDeleteImg _deleteImg;

        public DeleteDinnerCommandHandler(IApplicationDbContext applicationDbContext, IDeleteImg deleteImg)
        {
            _applicationDbContext = applicationDbContext;
            _deleteImg = deleteImg;
        }

        public async Task<bool> Handle(DeleteDinnerCommand request, CancellationToken cancellationToken)
        {
           Dinner breakFast = await _applicationDbContext.Dinners.FindAsync(request.Id);
            if (breakFast == null)
            {
                throw new NotFoundException(nameof(Dinner), request.Id);
            }

            if(breakFast.ImgFileName is not null)
            {
                _deleteImg.Delete_Img(breakFast.ImgFileName);
            }

            _applicationDbContext.Dinners.Remove(breakFast);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
