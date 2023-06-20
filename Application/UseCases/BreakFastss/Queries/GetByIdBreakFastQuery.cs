using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Domain.Entities;

namespace Application.UseCases.BreakFasts.Queries
{
    public class GetByIdBreakFastQuery : IRequest<BreakFastGetDto>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdBreakFastQueryHandler : IRequestHandler<GetByIdBreakFastQuery, BreakFastGetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdBreakFastQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BreakFastGetDto> Handle(GetByIdBreakFastQuery request, CancellationToken cancellationToken)
        {
             BreakFast? result = _context.BreakFasts.FirstOrDefault(res=> res.Id == request.Id);
            if (result == null)
            {
                throw new NotFoundException(nameof(BreakFast), request.Id);
            }
            var res = _mapper.Map<BreakFastGetDto>(result);

            return res;
        }
    }
}
