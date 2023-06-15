using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Lunchs.Queries
{

    public class GetByIdLunchQuery : IRequest<LunchGetDto>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdLunchQueryHandler : IRequestHandler<GetByIdLunchQuery, LunchGetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdLunchQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LunchGetDto> Handle(GetByIdLunchQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Lunchs.FindAsync(request.Id);
            if (result == null)
            {
                throw new NotFoundException(nameof(Lunch), request.Id);
            }
            var res = _mapper.Map<LunchGetDto>(result);

            return res;
        }
    }
}
