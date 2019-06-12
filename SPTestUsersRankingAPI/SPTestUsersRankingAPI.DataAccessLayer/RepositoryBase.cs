using SPTestUsersRankingAPI.Model;
using SPTestUsersRankingAPI.Database;
using SPTestUsersRankingAPI.Util;

namespace SPTestUsersRankingAPI.DataAccessLayer
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        protected Logger Logger = Logger.Instance;
        protected IContext Context;

        public RepositoryBase(IContext context)
        {
            Context = context;
        }

        public abstract void Create(T entity);

        public abstract T Delete(T entity);

        public abstract T GetById(int id);

        public abstract T Update(T entity);
    }
}
