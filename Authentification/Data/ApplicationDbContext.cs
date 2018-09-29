using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Authentification.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentification.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public const string IdentitySchema = "authentification";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(IdentitySchema);
        }
    }
}
