using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.NewsEvents.Queries
{
    public class GetByIdNewsEventQuery : IRequest<NewsEventGetDto>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdNewsEventQueryHandler : IRequestHandler<GetByIdNewsEventQuery, NewsEventGetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdNewsEventQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NewsEventGetDto> Handle(GetByIdNewsEventQuery request, CancellationToken cancellationToken)
        {
            var result = _context.NewsEvents.FindAsync(request.Id);
            if (result == null)
            {
                throw new NotFoundException(nameof(NewsEvent), request.Id);
            }
            var res = _mapper.Map<NewsEventGetDto>(result);

            return res;
        }
    }
}
