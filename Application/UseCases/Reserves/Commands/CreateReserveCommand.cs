using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Reserves.Commands
{
    public class CreateReserveCommand : IRequest<Guid>
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int NumberOfPerson { get; set; }
        public string? MyData { get; set; }
        public string? Time { get; set; }
        public string? Description { get; set; }
    }

    public class CreateReserveCommandHandler : IRequestHandler<CreateReserveCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        public CreateReserveCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateReserveCommand request, CancellationToken cancellationToken)
        {
            var reserve = new Reserve
            {
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                NumberOfPerson = request.NumberOfPerson,
                MyData = request.MyData,
                Time = request.Time,
                Description = request.Description
            };

            var entity = await _context.Reserves.AddAsync(reserve);
            await _context.SaveChangesAsync();

            return reserve.Id;
        }
    }
}
