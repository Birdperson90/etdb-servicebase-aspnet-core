﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETDB.API.ServiceBase.Generics.Base
{
    public abstract class EntityMappingConfiguration<TEntity> : IEntityMappingConfiguration where TEntity : class, IEntity, new()
    {
        protected readonly ModelBuilder ModelBuilder;

        protected EntityMappingConfiguration(ModelBuilder modelBuilder)
        {
            this.ModelBuilder = modelBuilder;
        }

        protected virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var name = typeof(TEntity).Name;
            builder.ToTable($"{name}s");
        }

        public virtual void ConfigureEntity()
        {
            this.AutoAddGuidPrimaryKey();
            this.EnableConccurentTracking();
            this.Configure(this.ModelBuilder.Entity<TEntity>());
        }

        protected virtual void AutoAddGuidPrimaryKey()
        {
            this.ModelBuilder.Entity<TEntity>(builder =>
            {
                builder.HasKey(entity => entity.Id);

                builder.Property(entity => entity.Id)
                    .HasDefaultValueSql("newid()");
            });
        }

        protected virtual void EnableConccurentTracking()
        {
            this.ModelBuilder.Entity<TEntity>(builder =>
            {
                builder.Property(entity => entity.RowVersion)
                    .ValueGeneratedOnAddOrUpdate()
                    .IsConcurrencyToken();
            });
        }
    }
}
