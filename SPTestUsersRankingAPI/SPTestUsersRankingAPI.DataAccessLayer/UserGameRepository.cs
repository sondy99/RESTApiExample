using SPTestUsersRankingAPI.Database;
using SPTestUsersRankingAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPTestUsersRankingAPI.DataAccessLayer
{
    public class UserGameRepository : RepositoryBase<UserGame>
    {
        public UserGameRepository(IContext context) : base(context) { }

        public override void Create(UserGame entity)
        {
            Logger.Trace("UserGameRepository", "Create", "Starting the method");
            
            try
            {
                entity.Id = Context.UserGames.Count + 1;
                if (!Context.UserGames.TryAdd(entity.Id, entity))
                {
                    Logger.Info("UserGameRepository", "Create", "Error creating entity");
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserGameRepository", "Create", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserGameRepository", "Create", "Finishing method");
            }
        }

        public override UserGame Delete(UserGame entity)
        {
            Logger.Trace("UserGameRepository", "Delete", "Starting the method");

            UserGame result = null;

            try
            {
                if (!Context.UserGames.TryRemove(entity.Id, out result))
                {
                    Logger.Info("UserGameRepository", "Delete", "Error deleting entity");
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserGameRepository", "Delete", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserGameRepository", "Delete", "Finishing method");
            }

            return result;
        }

        public override UserGame GetById(int id)
        {
            Logger.Trace("UserGameRepository", "GetById", "Starting the method");

            try
            {
                if (!Context.UserGames.TryGetValue(id, out UserGame result))
                {
                    Logger.Info("UserGameRepository", "GetById", "Error getting entity");
                }

                return result;
            }
            catch (Exception e)
            {
                Logger.Error("UserGameRepository", "GetById", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserGameRepository", "GetById", "Finishing method");
            }
        }

        public override UserGame Update(UserGame entity)
        {
            Logger.Trace("UserGameRepository", "Update", "Starting the method");

            UserGame result = null;

            try
            {
                if (!Context.UserGames.TryGetValue(entity.Id, out result))
                { 
                    Logger.Info("UserGameRepository", "Update", "Error getting entity");
                }
                else
                {
                    if (!Context.UserGames.TryUpdate(entity.Id, entity, result))
                    {
                        Logger.Info("UserGameRepository", "Update", "Error updating entity");
                    }
                    else
                    {
                        result = entity;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserGameRepository", "Update", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserGameRepository", "Update", "Finishing method");
            }

            return result;
        }

        public List<UserGame> GetAbsoluteRanking(int absoluteNumber)
        {
            Logger.Trace("UserGameRepository", "GetTop", "Starting the method");
            List<UserGame> result = null;

            try
            {
                result = Context.UserGames.OrderByDescending(_ => _.Value.Score).Take(absoluteNumber).Select(_ => _.Value).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("UserGameRepository", "GetTop", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserGameRepository", "Update", "Finishing method");
            }

            return result;
        }

        public List<UserGame> GetRelativeRanking(int atNumber, int quantity)
        {
            Logger.Trace("UserGameRepository", "GetTop", "Starting the method");
            List<UserGame> result = null;

            try
            {
                result = Context.UserGames.OrderByDescending(_ => _.Value.Score).Skip((atNumber - quantity)-1).Take((quantity*2) + 1).Select(_ => _.Value).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("UserGameRepository", "GetTop", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserGameRepository", "Update", "Finishing method");
            }

            return result;
        }
    }
}
