using System;
using Common.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Data
{
    public static class EntityTypeConfigurationExtensions
    {
        public static void ConfigureEntity<T>(this EntityTypeBuilder<T> builder, string tableName, string schema, bool useSequenceHilo = true)
            where T : EntityBase
        {
            builder.ToTable(tableName, schema);

            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.CreationDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder
                .Property(e => e.UpdateDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            if (useSequenceHilo)
                builder.Property(b => b.Id)
                    .ForSqlServerUseSequenceHiLo($"{tableName}Hilo", schema)
                    .HasDefaultValueSql($"NEXT VALUE FOR {schema}.{tableName}Hilo")
                    .IsRequired();

        }
    }
}