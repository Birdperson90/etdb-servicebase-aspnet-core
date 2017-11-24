using ETDB.API.ServiceBase.Domain.Abstractions.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETDB.API.ServiceBase.Entities
{
    public abstract class EntityMapBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity, new()
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.AutoAddGuidPrimaryKey(builder);
            this.EnableConccurentTracking(builder);
            var name = typeof(TEntity).Name;
            builder.ToTable($"{name}s");
        }

        protected virtual void AutoAddGuidPrimaryKey(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .HasDefaultValueSql("newid()");
        }

        protected virtual void EnableConccurentTracking(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(entity => entity.RowVersion)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
        }
    }
}
