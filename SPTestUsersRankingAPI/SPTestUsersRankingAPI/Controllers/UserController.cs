using SPTestUsersRankingAPI.BusinessLayer;
using SPTestUsersRankingAPI.DTO;
using SPTestUsersRankingAPI.Util;
using SPTestUsersRankingAPI.App_Start;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SPTestUsersRankingAPI.Controllers
{
    [AuthenticationFilter]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        protected Logger Logger = Logger.Instance;

        // GET api/User
        [Route("")]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                UserBusiness userBusiness = new UserBusiness();

                var result = userBusiness.GetAll();

                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                Logger.Error("UserController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // GET api/User/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                UserBusiness userBusiness = new UserBusiness();

                var result = userBusiness.GetById(id);

                if (result != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // POST api/User
        [Route("")]
        public HttpResponseMessage Post(UserDTO userDTO)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                if (ModelState.IsValid)
                {
                    UserBusiness userBusiness = new UserBusiness();

                    userBusiness.Create(userDTO);

                    response = Request.CreateResponse(HttpStatusCode.OK, "User successfully created");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "The request object doesn't match with the requirement");
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // PUT api/User
        [Route("")]
        public HttpResponseMessage Put(UserDTO userDTO)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                if (ModelState.IsValid)
                {
                    UserBusiness userBusiness = new UserBusiness();

                    var result = userBusiness.Update(userDTO);

                    if (result != null)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, "User successfully updated");
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "The request object doesn't match with the requirement");
                }

            }
            catch (Exception e)
            {
                Logger.Error("UserController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // PUT api/User
        [Route("SubscribeUserToGame/")]
        public HttpResponseMessage PutSubscribeUserToGame(UserGameDTO userGameDTO)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                if (ModelState.IsValid)
                {
                    UserBusiness userBusiness = new UserBusiness();

                    var result = userBusiness.SubscribeUserToGame(userGameDTO);

                    if (result != null)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, "User successfully subscribe to the game");
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "The request object doesn't match with the requirement");
                }

            }
            catch (Exception e)
            {
                Logger.Error("UserController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // PUT api/User/AbsoluteScore
        [Route("AbsoluteScore/")]
        public HttpResponseMessage PutSubmitAbsoluteScore(AbsoluteScoreDTO absoluteScoreDTO)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                if (ModelState.IsValid)
                {
                    UserBusiness userBusiness = new UserBusiness();

                    var result = userBusiness.SubmitAbsoluteScore(absoluteScoreDTO);

                    if (result != null)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, "Absolute score successfully updated");
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "The request object doesn't match with the requirement");
                }

            }
            catch (Exception e)
            {
                Logger.Error("UserController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // PUT api/User/RelativeScore
        [Route("RelativeScore/")]
        public HttpResponseMessage PutSubmitRelativeScore(RelativeScoreDTO relativeScoreDTO)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                if (ModelState.IsValid)
                {
                    UserBusiness userBusiness = new UserBusiness();

                    var result = userBusiness.SubmitRelativeScore(relativeScoreDTO);

                    if (result != null)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, "Absolute score successfully updated");
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "The request object doesn't match with the requirement");
                }

            }
            catch (Exception e)
            {
                Logger.Error("UserController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // DELETE api/User/5
        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                UserBusiness userBusiness = new UserBusiness();

                var result = userBusiness.Delete(id);

                if (result != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, "User successfully deleted");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
                }
            }
            catch (Exception e)
            {
                Logger.Error("UserController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }
    }
}
