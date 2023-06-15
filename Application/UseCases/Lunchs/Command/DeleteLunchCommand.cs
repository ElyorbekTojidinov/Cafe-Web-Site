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

        public DeleteLunchCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteLunchCommand request, CancellationToken cancellationToken)
        {
            var lunch = await _applicationDbContext.Lunchs.FindAsync(request.Id);
            if (lunch == null)
            {
                throw new NotFoundException(nameof(Lunch), request.Id);
            }
            _applicationDbContext.Lunchs.Remove(lunch);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
