using SPTestUsersRankingAPI.BusinessLayer.Exceptions;
using SPTestUsersRankingAPI.DataAccessLayer;
using SPTestUsersRankingAPI.DTO;
using SPTestUsersRankingAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPTestUsersRankingAPI.BusinessLayer
{
    public class UserBusiness : BusinessBase
    {
        public void Create(UserDTO userDTO)
        {
            Logger.Trace("UserBusiness", "Create", "Starting the method");

            try
            {
                var unitOfWork = Database.Context.Instance;

                var userRepository = new UserRepository(unitOfWork);

                userRepository.Create(new User()
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName
                });
            }
            catch (Exception e)
            {
                Logger.Error("UserBusiness", "Create", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserBusiness", "Create", "Finishing method");
            }
        }

        public UserDTO Delete(int userId)
        {
            Logger.Trace("UserBusiness", "Delete", "Starting the method");

            UserDTO result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var userRepository = new UserRepository(unitOfWork);
                var userGameRepository = new UserGameRepository(unitOfWork);

                var user = userRepository.GetById(userId);

                if (user != null)
                {
                    userRepository.Delete(user);

                    if (user.Games != null)
                    {
                        foreach (var game in user.Games)
                        {
                            userGameRepository.Delete(game);
                        }
                    }

                    result = UserToUserDTO(user);
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserBusiness", "Delete", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserBusiness", "Delete", "Finishing method");
            }

            return result;
        }

        public List<UserDTO> GetAll()
        {
            Logger.Trace("UserBusiness", "GetAll", "Starting the method");

            List<UserDTO> result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var userRepository = new UserRepository(unitOfWork);

                var users = userRepository.GetAll();

                result = users.Select(_ => new UserDTO()
                {
                    Id = _.Id,
                    FirstName = _.FirstName,
                    LastName = _.LastName
                }).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("UserBusiness", "GetAll", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserBusiness", "GetAll", "Finishing method");
            }

            return result;
        }

        public UserDTO GetById(int userId)
        {
            Logger.Trace("UserBusiness", "GetById", "Starting the method");

            UserDTO result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var userRepository = new UserRepository(unitOfWork);

                var user = userRepository.GetById(userId);

                result = UserToUserDTO(user);

            }
            catch (Exception e)
            {
                Logger.Error("UserBusiness", "GetById", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserBusiness", "GetById", "Finishing method");
            }

            return result;
        }

        public UserDTO Update(UserDTO userDTO)
        {
            Logger.Trace("UserBusiness", "Update", "Starting the method");

            UserDTO result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var userRepository = new UserRepository(unitOfWork);

                var userResult = userRepository.Update(new User()
                {
                    Id = userDTO.Id,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName
                });

                result = UserToUserDTO(userResult);

            }
            catch (Exception e)
            {
                Logger.Error("UserBusiness", "Update", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserBusiness", "Update", "Finishing method");
            }

            return result;
        }

        public UserDTO SubscribeUserToGame(UserGameDTO userGameDTO)
        {
            Logger.Trace("UserBusiness", "SubscribeUserToGame", "Starting the method");

            UserDTO result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var userRepository = new UserRepository(unitOfWork);
                var userGameRepository = new UserGameRepository(unitOfWork);

                var user = userRepository.GetById(userGameDTO.UserId);

                if(user != null)
                {
                    user.Games = user.Games ?? new List<UserGame>();

                    //This will be always game 1 because I'm not implementing several games, but the code is
                    //prepared to support it with minimal changes.
                    var game = new Game()
                    {
                        Id = userGameDTO.GameId,
                        Name = "Game"
                    };

                    var userGame = new UserGame()
                    {
                        UserId = user.Id,
                        User = user,
                        GameId = 1,
                        Game = game,
                        Score = 0
                    };

                    user.Games.Add(userGame);

                    userGameRepository.Create(userGame);
                    user = userRepository.Update(user);

                    result = UserToUserDTO(user);
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserBusiness", "SubscribeUserToGame", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserBusiness", "SubscribeUserToGame", "Finishing method");
            }

            return result;
        }

        public UserDTO SubmitAbsoluteScore(AbsoluteScoreDTO absoluteScoreDTO)
        {
            Logger.Trace("UserBusiness", "SubmitAbsoluteScore", "Starting the method");

            UserDTO result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var userRepository = new UserRepository(unitOfWork);

                var user = userRepository.GetById(absoluteScoreDTO.User);

                if (user != null && absoluteScoreDTO.Total >= 0 && absoluteScoreDTO.Total <= 1000)
                {
                    if (user.Games != null)
                    {
                        //this is because for this specific test I only have used one game
                        user.Games[0].Score = absoluteScoreDTO.Total;
                    }
                    else
                    {
                        throw new BusinessCustomException("The user has not subscribed to any game");
                    }

                    user = userRepository.Update(user);

                    result = UserToUserDTO(user);
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserBusiness", "SubmitAbsoluteScore", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserBusiness", "SubmitAbsoluteScore", "Finishing method");
            }

            return result;
        }

        public UserDTO SubmitRelativeScore(RelativeScoreDTO relativeScoreDTO)
        {
            Logger.Trace("UserBusiness", "SubmitRelativeScore", "Starting the method");

            UserDTO result = null;

            try
            {
                var unitOfWork = Database.Context.Instance;

                var userRepository = new UserRepository(unitOfWork);

                var user = userRepository.GetById(relativeScoreDTO.User);

                if (user != null)
                {

                    if (user.Games != null)
                    {
                        //this is because for this specific test I only have used one game
                        user.Games[0].Score = user.Games[0].Score + Int32.Parse(relativeScoreDTO.Score);
                    }
                    else
                    {
                        throw new BusinessCustomException("The user has not subscribed to any game");
                    }

                    user = userRepository.Update(user);

                    result = UserToUserDTO(user);
                }

            }
            catch (Exception e)
            {
                Logger.Error("UserBusiness", "SubmitRelativeScore", e.Message);
                throw e;
            }
            finally
            {
                Logger.Trace("UserBusiness", "SubmitRelativeScore", "Finishing method");
            }

            return result;
        }

        private UserDTO UserToUserDTO(User user)
        {
            UserDTO result = null;

            if (user != null)
            {
                result = new UserDTO()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }

            return result;
        }
    }
}
