using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WrightsAtHome.Server.API.Common
{
    public class MessageActionResult : IHttpActionResult
    {
        private readonly HttpStatusCode statusCode;
        private readonly HttpRequestMessage request;
        private readonly string message;
        private readonly object[] formatParams;
        
        public MessageActionResult(HttpRequestMessage request, HttpStatusCode statusCode, string message, params object[] formatParams)
        {
            if (request == null)
            {
                throw new ArgumentException("request");
            }

            if (message == null)
            {
                throw new ArgumentException("message");
            }

            this.request = request;
            this.statusCode = statusCode;
            this.message = message;
            this.formatParams = formatParams;
        }


        public HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(statusCode);

            string msg;
            if (formatParams != null && formatParams.Length > 0)
            {
                msg = string.Format(message, formatParams);
            }
            else
            {
                msg = message;
            }

            response.Content = new StringContent(msg); // Put the message in the response body (text/plain content).
            response.RequestMessage = request;
            return response;            
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
    }
}