using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<BreakFast> BreakFasts { get; set; }
        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<Lunch> Lunchs { get; set; }
        public DbSet<NewsEvent> NewsEvents { get; set; }
        public DbSet<Reserve> Reserves { get; set; }
        public DbSet<SpecialMenu> SpecialMenus { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
