using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using NLog;
using WrightsAtHome.Server.DataAccess;

namespace WrightsAtHome.Server.API.Common
{
    public class BaseApiController : ApiController
    {
        protected readonly IAtHomeDbContext dbContext;
        protected readonly Logger logger;

        public BaseApiController(IAtHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
            logger = LogManager.GetLogger(GetType().Name);
        }

        protected MessageActionResult Conflict(string message, params object[] formatParams)
        {
            return new MessageActionResult(Request, HttpStatusCode.Conflict, message, formatParams);
        }

        protected MessageActionResult NotFound(string message, params object[] formatParams)
        {
            return new MessageActionResult(Request, HttpStatusCode.NotFound,  message, formatParams);
        }

        protected MessageActionResult BadRequest(string message, params object[] formatParams)
        {
            return new MessageActionResult(Request, HttpStatusCode.BadRequest, message, formatParams);
        }

        protected MessageActionResult InternalServerError(string message, params object[] formatParams)
        {
            return new MessageActionResult(Request, HttpStatusCode.InternalServerError, message, formatParams);
        }

        protected IHttpActionResult NoContent()
        {
            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

    }
}