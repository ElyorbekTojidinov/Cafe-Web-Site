using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Dinners.Commands
{

    public class DeleteDinnerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteDinnerCommandHandler : IRequestHandler<DeleteDinnerCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteDinnerCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteDinnerCommand request, CancellationToken cancellationToken)
        {
            var breakFast = await _applicationDbContext.Dinners.FindAsync(request.Id);
            if (breakFast == null)
            {
                throw new NotFoundException(nameof(Dinner), request.Id);
            }
            _applicationDbContext.Dinners.Remove(breakFast);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
