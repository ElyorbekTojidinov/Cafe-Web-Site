using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Dinners.Queries
{

    public class GetAllDinnerQuery : IRequest<IQueryable<DinnerGetDto>>
    {

    }

    public class GetAllDinnerQueryHandler : IRequestHandler<GetAllDinnerQuery, IQueryable<DinnerGetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllDinnerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<DinnerGetDto>> Handle(GetAllDinnerQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Dinners;
            var res = _mapper.ProjectTo<DinnerGetDto>(result);

            return await Task.FromResult(res);
        }
    }
}
