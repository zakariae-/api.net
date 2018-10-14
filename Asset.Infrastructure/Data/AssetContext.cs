using System;
using Asset.Core.Entities;
using Asset.Infrastructure.Data.Configurations;
using Conquistador.Common.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Asset.Infrastructure.Data
{
    public class AssetContext : DbContext, IUnitOfWork, IDesignTimeDbContextFactory<AssetContext>
    {
        public const string DEFAULT_SCHEMA = "Asset";

        public DbSet<User> Users { get; set; }

        public AssetContext(DbContextOptions<AssetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());

        }

        public AssetContext()
        {
        }

        public AssetContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AssetContext>();
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=MyDataBase;User=SA;Password=<YourStrong!Passw0rd>");

            return new AssetContext(optionsBuilder.Options);
        }
    }
}
