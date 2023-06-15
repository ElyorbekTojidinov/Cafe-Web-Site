using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.UseCases.NewsEvents.Queries
{
    public class GetAllNewsEventQuery : IRequest<IQueryable<NewsEventGetDto>>
    {

    }

    public class GetAllNewsEventQueryHandler : IRequestHandler<GetAllNewsEventQuery, IQueryable<NewsEventGetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllNewsEventQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<NewsEventGetDto>> Handle(GetAllNewsEventQuery request, CancellationToken cancellationToken)
        {
            var result = _context.NewsEvents;
            var res = _mapper.ProjectTo<NewsEventGetDto>(result);

            return await Task.FromResult(res);
        }
    }
}
