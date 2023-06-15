using Application.Common.Exceptions;
using Application.Common.Interfaces;
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

        public DeleteBreakFastCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteBreakFastCommand request, CancellationToken cancellationToken)
        {
             var breakFast = await _applicationDbContext.BreakFasts.FindAsync(request.Id);
            if (breakFast == null)
            {
                throw new NotFoundException(nameof(BreakFasts), request.Id);
            }
            _applicationDbContext.BreakFasts.Remove(breakFast);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
