using SPTestUsersRankingAPI.Database;
using SPTestUsersRankingAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPTestUsersRankingAPI.DataAccessLayer
{
    public class GameRepository : RepositoryBase<Game>
    {
        public GameRepository(IContext context) : base(context) { }

        public override void Create(Game entity)
        {
            Logger.Trace("GameRepository", "Create", "Starting the method");
            
            try
            {
                entity.Id = Context.Games.Count + 1;
                if (!Context.Games.TryAdd(entity.Id, entity))
                {
                    Logger.Info("GameRepository", "Create", "Error creating entity");
                }
            }
            catch (Exception e)
            {
                Logger.Error("GameRepository", "Create", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameRepository", "Create", "Finishing method");
            }
        }

        public override Game Delete(Game entity)
        {
            Logger.Trace("GameRepository", "Delete", "Starting the method");

            Game result = null;

            try
            {
                if (!Context.Games.TryRemove(entity.Id, out result))
                {
                    Logger.Info("GameRepository", "Delete", "Error deleting entity");
                }
            }
            catch (Exception e)
            {
                Logger.Error("GameRepository", "Delete", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameRepository", "Delete", "Finishing method");
            }

            return result;
        }

        public override Game GetById(int id)
        {
            Logger.Trace("GameRepository", "GetById", "Starting the method");

            try
            {
                if (!Context.Games.TryGetValue(id, out Game result))
                {
                    Logger.Info("GameRepository", "GetById", "Error getting entity");
                }

                return result;
            }
            catch (Exception e)
            {
                Logger.Error("GameRepository", "GetById", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameRepository", "GetById", "Finishing method");
            }
        }

        public override Game Update(Game entity)
        {
            Logger.Trace("GameRepository", "Update", "Starting the method");

            Game result = null;

            try
            {
                if (!Context.Games.TryGetValue(entity.Id, out result))
                { 
                    Logger.Info("GameRepository", "Update", "Error getting entity");
                }
                else
                {
                    if (!Context.Games.TryUpdate(entity.Id, entity, result))
                    {
                        Logger.Info("GameRepository", "Update", "Error updating entity");
                    }
                    else
                    {
                        result = entity;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("GameRepository", "Update", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameRepository", "Update", "Finishing method");
            }

            return result;
        }
        
        public List<Game> GetAll()
        {
            Logger.Trace("GameRepository", "GetAll", "Starting the method");
            List<Game> result;
            try
            {
                result = Context.Games.Select(_ => _.Value).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("GameRepository", "GetAll", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameRepository", "GetAll", "Finishing method");
            }

            return result;
        }
    }
}
