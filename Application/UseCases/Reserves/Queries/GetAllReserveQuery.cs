using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Reserves.Queries
{
    public class GetAllReserveQuery : IRequest<IQueryable<ReserveGetDto>>
    {

    }

    public class GetAllReserveQueryHandler : IRequestHandler<GetAllReserveQuery, IQueryable<ReserveGetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllReserveQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<ReserveGetDto>> Handle(GetAllReserveQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Reserves;
            var res = _mapper.ProjectTo<ReserveGetDto>(result);

            return await Task.FromResult(res);
        }
    }
}
