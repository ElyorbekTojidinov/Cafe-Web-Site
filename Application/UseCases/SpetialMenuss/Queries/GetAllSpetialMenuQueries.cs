using Application.Common.Interfaces;
using Application.Common.Models;
using Application.UseCases.BreakFasts.Queries;
using AutoMapper;
using MediatR;

namespace Application.UseCases.SpetialMenuss.Queries
{
    public class GetAllSpetialMenuQueries : IRequest<IQueryable<SpetialMenuGetDto>>
    {
    }
    public class GetAllSpetialMenuQueriesHandler : IRequestHandler<GetAllSpetialMenuQueries, IQueryable<SpetialMenuGetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllSpetialMenuQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<SpetialMenuGetDto>> Handle(GetAllSpetialMenuQueries request, CancellationToken cancellationToken)
        {
            var result = _context.SpecialMenus;
            var res = _mapper.ProjectTo<SpetialMenuGetDto>(result);

            return await Task.FromResult(res);
        }
    }
}
