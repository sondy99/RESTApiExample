using SPTestUsersRankingAPI.Model;
namespace SPTestUsersRankingAPI.DataAccessLayer
{
    interface IRepository<T> where T : EntityBase
    {
        T GetById(int id);

        void Create(T entity);

        T Delete(T entity);

        T Update(T entity);
    }
}
