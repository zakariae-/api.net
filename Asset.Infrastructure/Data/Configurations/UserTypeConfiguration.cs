using System;
using Asset.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Common.Infrastructure.Data;

namespace Asset.Infrastructure.Data.Configurations
{
    internal class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ConfigureEntity("User", AssetContext.DEFAULT_SCHEMA);
        }
    }
}
