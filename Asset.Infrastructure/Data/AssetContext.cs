using System;
using Asset.Core.Entities;
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

        public AssetContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
