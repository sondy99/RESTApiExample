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
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        protected Logger Logger = Logger.Instance;

        // GET api/Game
        [Route("")]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                GameBusiness gameBusiness = new GameBusiness();

                var result = gameBusiness.GetAll();

                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                Logger.Error("GameController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // GET api/Game/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                GameBusiness gameBusiness = new GameBusiness();

                var result = gameBusiness.GetById(id);

                if (result != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Game not found");
                }
            }
            catch (Exception e)
            {
                Logger.Error("GameController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // GET api/Game/5
        [Route("Top{absoluteNumber:int}")]
        public HttpResponseMessage GetAbsoluteRanking(int absoluteNumber)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                GameBusiness gameBusiness = new GameBusiness();

                var result = gameBusiness.GetAbsoluteRanking(absoluteNumber);

                if (result != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Not ranking found");
                }
            }
            catch (Exception e)
            {
                Logger.Error("GameController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // GET api/Game/5
        [Route("At{atNumber:int}/{quantity:int}")]
        public HttpResponseMessage GetRelativeRanking(int atNumber, int quantity)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                GameBusiness gameBusiness = new GameBusiness();

                var result = gameBusiness.GetRelativeRanking(atNumber, quantity);

                if (result != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Not ranking found");
                }
            }
            catch (Exception e)
            {
                Logger.Error("GameController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // POST api/Game
        [Route("")]
        public HttpResponseMessage Post(GameDTO gameDTO)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                if (ModelState.IsValid)
                {
                    GameBusiness gameBusiness = new GameBusiness();

                    gameBusiness.Create(gameDTO);

                    response = Request.CreateResponse(HttpStatusCode.OK, "Game successfully created");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "The request object doesn't match with the requirement");
                }
            }
            catch (Exception e)
            {
                Logger.Error("GameController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // PUT api/Game
        [Route("")]
        public HttpResponseMessage Put(GameDTO gameDTO)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                if (ModelState.IsValid)
                {
                    GameBusiness gameBusiness = new GameBusiness();

                    var result = gameBusiness.Update(gameDTO);

                    if (result != null)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, "Game successfully updated");
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotFound, "Game not found");
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "The request object doesn't match with the requirement");
                }

            }
            catch (Exception e)
            {
                Logger.Error("GameController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }

        // DELETE api/Game/5
        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                GameBusiness gameBusiness = new GameBusiness();

                var result = gameBusiness.Delete(id);

                if (result != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, "Game successfully deleted");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Game not found");
                }
            }
            catch (Exception e)
            {
                Logger.Error("GameController", "Get", e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error");
            }

            return response;
        }
    }
}
