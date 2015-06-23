using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using NLog;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;

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

        protected class FetchResult<T>
            where T : class, IBaseEntity
        {
            public T Result { get; set; }
            public IHttpActionResult HttpResult { get; set;}
        }

        protected async Task<FetchResult<T>> Fetch<T, TProperty>(IQueryable<T> set, int id, Expression<Func<T, TProperty>> include = null) 
            where T : class, IBaseEntity
        {
            var result = new FetchResult<T>();
            
            if (include != null)
            {
                set.Include(include);
            }

            result.Result = await set.SingleOrDefaultAsync(e => e.Id == id);

            if (result.Result == null)
            {
                result.HttpResult = new MessageActionResult(Request, HttpStatusCode.NotFound,
                    "No " + typeof (T).Name + " with id '{0}' exists", id);
            }
            else
            {
                result.HttpResult = null;
            }

            return result;
        }

        protected async Task<FetchResult<TSubordinate>> FetchSubordinate<TParent, TNavigationProp, TSubordinate>(
            IDbSet<TParent> dbSet,
            int parentId,
            Expression<Func<TParent, TNavigationProp>> include,
            int subId)
            where TParent : class, IBaseEntity
            where TNavigationProp : IList<TSubordinate>
            where TSubordinate : class, IBaseEntity
        {

            var result = new FetchResult<TSubordinate>();

            var parentFetch = await Fetch(dbSet, parentId, include);
            
            if (parentFetch.Result == null)
            {
                result.HttpResult = parentFetch.HttpResult;
                return result;
            }

            result.Result = include.Compile().Invoke(parentFetch.Result).FirstOrDefault(s => s.Id == subId);

            if (result.Result == null)
            {
                result.HttpResult = new MessageActionResult(Request, HttpStatusCode.NotFound,
                    "No " + typeof (TSubordinate).Name + " with id '{0}' exists for " + typeof (TParent).Name +
                    " with id '{1}'", subId, parentId);
            }

            return result;
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