using ETDB.API.ServiceBase.Domain.Abstractions.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETDB.API.ServiceBase.Repositories.Abstractions.Base
{
    public abstract class EntityMapBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity, new()
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var name = typeof(TEntity).Name;
            builder.ToTable($"{name}s");
            this.EnableConccurentTracking(builder);
        }

        protected virtual void EnableConccurentTracking(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(entity => entity.RowVersion)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
        }
    }
}
