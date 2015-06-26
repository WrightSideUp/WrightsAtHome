using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.ModelConfiguration;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace WrightsAtHome.Server.API.Common
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly Dictionary<Type, HttpStatusCode> returnTypes = new Dictionary<Type, HttpStatusCode>
        {
            {typeof (ObjectNotFoundException), HttpStatusCode.NotFound},
            {typeof (ModelValidationException), HttpStatusCode.BadRequest},
            {typeof (DBConcurrencyException), HttpStatusCode.Conflict}
        };

        public void Handle(ExceptionHandlerContext context)
        {

            if (context.ExceptionContext != null &&
                context.ExceptionContext.CatchBlock == ExceptionCatchBlocks.IExceptionFilter)
            {
                return;
            }

            var ex = context.Exception;
            var type = context.Exception.GetType();

            context.Result = returnTypes.ContainsKey(type) ? 
                new MessageActionResult(context.Request, returnTypes[type], ex.Message) : 
                new MessageActionResult(context.Request, HttpStatusCode.InternalServerError, ex.Message);
        }

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            Handle(context);
            return Task.FromResult(true);
        }
    }
}