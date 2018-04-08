using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Park.EntityFramework.Repositories
{
    public abstract class ParkRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<ParkDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ParkRepositoryBase(IDbContextProvider<ParkDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class ParkRepositoryBase<TEntity> : ParkRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ParkRepositoryBase(IDbContextProvider<ParkDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
