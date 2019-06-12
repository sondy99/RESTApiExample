using SPTestUsersRankingAPI.DataAccessLayer;
using SPTestUsersRankingAPI.DTO;
using SPTestUsersRankingAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPTestUsersRankingAPI.BusinessLayer
{
    public class GameBusiness : BusinessBase
    {
        public void Create(GameDTO gameDTO)
        {
            Logger.Trace("GameBusiness", "Create", "Starting the method");

            try
            {
                var unitOfWork = Database.Context.Instance;

                var gameRepository = new GameRepository(unitOfWork);

                gameRepository.Create(new Game()
                {
                    Name = gameDTO.Name
                });
            }
            catch (Exception e)
            {
                Logger.Error("GameBusiness", "Create", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameBusiness", "Create", "Finishing method");
            }
        }

        public GameDTO Delete(int gameId)
        {
            Logger.Trace("GameBusiness", "Delete", "Starting the method");

            GameDTO result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var gameRepository = new GameRepository(unitOfWork);

                var game = gameRepository.GetById(gameId);

                if(game != null)
                {
                    game = gameRepository.Delete(game);

                    result = GameToGameDTO(game);
                }
            }
            catch (Exception e)
            {
                Logger.Error("GameBusiness", "Delete", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameBusiness", "Delete", "Finishing method");
            }

            return result;
        }

        public GameDTO GetById(int gameId)
        {
            Logger.Trace("GameBusiness", "GetById", "Starting the method");

            GameDTO result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var gameRepository = new GameRepository(unitOfWork);

                var game = gameRepository.GetById(gameId);

                result = GameToGameDTO(game);

            }
            catch (Exception e)
            {
                Logger.Error("GameBusiness", "GetById", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameBusiness", "GetById", "Finishing method");
            }

            return result;
        }

        public List<GameDTO> GetAll()
        {
            Logger.Trace("GameBusiness", "GetAll", "Starting the method");

            List<GameDTO> result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var gameRepository = new GameRepository(unitOfWork);

                var game = gameRepository.GetAll();

                result = game.Select(_ => new GameDTO()
                {
                    Id = _.Id,
                    Name = _.Name
                }).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("GameBusiness", "GetAll", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameBusiness", "GetAll", "Finishing method");
            }

            return result;
        }

        public GameDTO Update(GameDTO gameDTO)
        {
            Logger.Trace("GameBusiness", "Update", "Starting the method");

            GameDTO result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var gameRepository = new GameRepository(unitOfWork);

                var game = gameRepository.Update(new Game()
                {
                    Id = gameDTO.Id,
                    Name = gameDTO.Name
                });
                
                result = GameToGameDTO(game);

            }
            catch (Exception e)
            {
                Logger.Error("GameBusiness", "Update", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameBusiness", "Update", "Finishing method");
            }

            return result;
        }

        public List<UserTopDTO> GetAbsoluteRanking(int absoluteNumber)
        {

            Logger.Trace("GameBusiness", "GetAbsoluteRanking", "Starting the method");
            List<UserTopDTO> result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var userGameRepository = new UserGameRepository(unitOfWork);

                var userGames = userGameRepository.GetAbsoluteRanking(absoluteNumber);

                result = ListUserGaMeToListUserTopDTO(userGames, 1);
            }
            catch (Exception e)
            {
                Logger.Error("GameBusiness", "GetAbsoluteRanking", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameBusiness", "GetAbsoluteRanking", "Finishing method");
            }

            return result;
        }

        public List<UserTopDTO> GetRelativeRanking(int atNumber, int quantity)
        {

            Logger.Trace("GameBusiness", "GetRelativeRanking", "Starting the method");
            List<UserTopDTO> result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var userGameRepository = new UserGameRepository(unitOfWork);

                var userGames = userGameRepository.GetRelativeRanking(atNumber, quantity);

                result = ListUserGaMeToListUserTopDTO(userGames, atNumber - quantity);
            }
            catch (Exception e)
            {
                Logger.Error("GameBusiness", "GetRelativeRanking", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameBusiness", "GetRelativeRanking", "Finishing method");
            }

            return result;
        }

        private List<UserTopDTO> ListUserGaMeToListUserTopDTO(List<UserGame> userGames, int atNumber)
        {
            Logger.Trace("GameBusiness", "ListUserGaMeToListUserTopDTO", "Starting the method");
            List<UserTopDTO> result = null;

            try
            {
                if (userGames != null && userGames.Count() > 0)
                {
                    result = new List<UserTopDTO>();

                    for (int i = 0; i < userGames.Count(); i++)
                    {
                        result.Add(new UserTopDTO()
                        {
                            Id = userGames[i].UserId,
                            FirstName = userGames[i].User.FirstName,
                            LastName = userGames[i].User.FirstName,
                            Score = userGames[i].Score,
                            Position = i + atNumber
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("GameBusiness", "ListUserGaMeToListUserTopDTO", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("GameBusiness", "ListUserGaMeToListUserTopDTO", "Finishing method");
            }

            return result;
        }

        private GameDTO GameToGameDTO(Game game)
        {
            GameDTO result = null;

            if (game != null)
            {
                result = new GameDTO()
                {
                    Id = game.Id,
                    Name = game.Name
                };
            }

            return result;
        }
    }
}
