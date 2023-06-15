using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Lunchs.Queries
{
    
    public class GetAllLunchQuery : IRequest<IQueryable<LunchGetDto>>
    {

    }

    public class GetAllLunchQueryHandler : IRequestHandler<GetAllLunchQuery, IQueryable<LunchGetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllLunchQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<LunchGetDto>> Handle(GetAllLunchQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Lunchs;
            var res = _mapper.ProjectTo<LunchGetDto>(result);

            return await Task.FromResult(res);
        }
    }
}
