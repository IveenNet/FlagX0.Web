using FlagX0.Web.Application.Interface.UseCases;
using FlagX0.Web.Application.UseCases.Flags;
using FlagX0.Web.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlagX0.Web.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<FlagEntity> Flags { get; set; }
        private readonly IFlagUserDetails _flagUserDetails;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IFlagUserDetails flagUserDetails)
            : base(options)
        {
            _flagUserDetails = flagUserDetails;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FlagEntity>().HasQueryFilter(a => !a.IsDeleted && a.UserId == _flagUserDetails.UserId);
            base.OnModelCreating(builder);
        }
    }

}
