using SPTestUsersRankingAPI.Database;
using SPTestUsersRankingAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPTestUsersRankingAPI.DataAccessLayer
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(IContext context) : base(context) { }

        public override void Create(User entity)
        {
            Logger.Trace("UserRepository", "Create", "Starting the method");
            
            try
            {
                entity.Id = Context.Users.Count + 1;
                if (!Context.Users.TryAdd(entity.Id, entity))
                {
                    Logger.Info("UserRepository", "Create", "Error creating entity");
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserRepository", "Create", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserRepository", "Create", "Finishing method");
            }
        }

        public override User Delete(User entity)
        {
            Logger.Trace("UserRepository", "Delete", "Starting the method");

            User result = null;

            try
            {
                if (!Context.Users.TryRemove(entity.Id, out result))
                {
                    Logger.Info("UserRepository", "Delete", "Error deleting entity");
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserRepository", "Delete", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserRepository", "Delete", "Finishing method");
            }

            return result;
        }

        public override User GetById(int id)
        {
            Logger.Trace("UserRepository", "GetById", "Starting the method");

            User result = null;

            try
            {
                if (!Context.Users.TryGetValue(id, out result))
                {
                    Logger.Info("UserRepository", "GetById", "Error getting entity");
                }

            }
            catch (Exception e)
            {
                Logger.Error("UserRepository", "GetById", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserRepository", "GetById", "Finishing method");
            }

            return result;
        }
                
        public override User Update(User entity)
        {
            Logger.Trace("UserRepository", "Update", "Starting the method");

            User result = null;

            try
            {
                if (!Context.Users.TryGetValue(entity.Id, out result))
                { 
                    Logger.Info("UserRepository", "Update", "Error getting entity");
                }
                else
                {
                    if (!Context.Users.TryUpdate(entity.Id, entity, result))
                    {
                        Logger.Info("UserRepository", "Update", "Error updating entity");
                    }
                    else
                    {
                        result = entity;
                    }

                }
            }
            catch (Exception e)
            {
                Logger.Error("UserRepository", "Update", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserRepository", "Update", "Finishing method");
            }

            return result;
        }

        public List<User> GetAll()
        {
            Logger.Trace("UserRepository", "GetAll", "Starting the method");
            List<User> result;
            try
            {
                result = Context.Users.Select(_ => _.Value).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("UserRepository", "GetAll", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserRepository", "GetAll", "Finishing method");
            }

            return result;
        }
    }
}
