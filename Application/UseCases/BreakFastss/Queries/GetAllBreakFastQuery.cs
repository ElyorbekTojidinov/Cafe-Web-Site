using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.UseCases.BreakFasts.Queries
{
    public class GetAllBreakFastQuery : IRequest<IQueryable<BreakFastGetDto>>
    {

    }

    public class GetAllBreakFastQueryHandler : IRequestHandler<GetAllBreakFastQuery, IQueryable<BreakFastGetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllBreakFastQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<BreakFastGetDto>> Handle(GetAllBreakFastQuery request, CancellationToken cancellationToken)
        {
            var result = _context.BreakFasts;
            var res = _mapper.ProjectTo<BreakFastGetDto>(result);

            return await Task.FromResult(res);
        }
    }
}
