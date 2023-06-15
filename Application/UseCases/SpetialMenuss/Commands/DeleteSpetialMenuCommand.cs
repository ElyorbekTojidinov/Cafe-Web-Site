﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.UseCases.BreakFasts.Commands;
using MediatR;

namespace Application.UseCases.SpetialMenuss.Commands
{
    public class DeleteSpetialMenuCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteSpetialMenuCommandHandler : IRequestHandler<DeleteSpetialMenuCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteSpetialMenuCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteSpetialMenuCommand request, CancellationToken cancellationToken)
        {
            var spMenu = await _applicationDbContext.SpecialMenus.FindAsync(request.Id);
            if (spMenu == null)
            {
                throw new NotFoundException(nameof(BreakFasts), request.Id);
            }
            _applicationDbContext.SpecialMenus.Remove(spMenu);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
