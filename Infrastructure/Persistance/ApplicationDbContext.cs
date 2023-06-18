using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
    {
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<BreakFast> BreakFasts { get; set; }
        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<Lunch> Lunchs { get; set; }
        public DbSet<NewsEvent> NewsEvents { get; set; }
        public DbSet<Reserve> Reserves { get; set; }
        public DbSet<SpecialMenu> SpecialMenus { get; set; }

     
    }
        
}

