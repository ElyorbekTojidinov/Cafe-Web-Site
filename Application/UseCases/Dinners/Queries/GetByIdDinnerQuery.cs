using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Dinners.Queries
{

    public class GetByIdDinnerQuery : IRequest<DinnerGetDto>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdDinnerQueryyHandler : IRequestHandler<GetByIdDinnerQuery, DinnerGetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdDinnerQueryyHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DinnerGetDto> Handle(GetByIdDinnerQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Dinners.FindAsync(request.Id);
            if (result == null)
            {
                throw new NotFoundException(nameof(Dinner), request.Id);
            }
            var res = _mapper.Map<DinnerGetDto>(result);

            return res;
        }
    }
}
