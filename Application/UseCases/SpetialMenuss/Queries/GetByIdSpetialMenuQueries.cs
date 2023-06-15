using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.SpetialMenuss.Queries
{
    public class GetByIdSpetialMenuQueries : IRequest<SpetialMenuGetDto>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdSpetialMenuQueriesHandler : IRequestHandler<GetByIdSpetialMenuQueries, SpetialMenuGetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdSpetialMenuQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SpetialMenuGetDto> Handle(GetByIdSpetialMenuQueries request, CancellationToken cancellationToken)
        {
            var result = _context.SpecialMenus.FindAsync(request.Id);
            if (result == null)
            {
                throw new NotFoundException(nameof(BreakFast), request.Id);
            }
            var res = _mapper.Map<SpetialMenuGetDto>(result);

            return res;
        }
    }
}
