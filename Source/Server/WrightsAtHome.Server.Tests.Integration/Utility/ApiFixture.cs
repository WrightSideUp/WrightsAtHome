using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Owin.Testing;
using WrightsAtHome.Server;

namespace WrightsAtHome.Server.Tests.Integration.Utility
{
    public class ApiFixture : IDisposable
    {
        public string BaseAddress { get {  return "http://localhost/api/"; } }
        
        public TestServer Server { get; private set; }

        public HttpClient Client
        {
            get
            {
                return Server.HttpClient;
            }
        }

        public HttpRequestMessage CreateRequest(string url, HttpMethod method)
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(BaseAddress + url),
                Method = method
            };

            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return req;
        }

        public ApiFixture()
        {
            Server = TestServer.Create<Startup>();
        }

        public void Dispose()
        {
            if (Server != null)
            {
                Server.Dispose();
            }
        }
    }
}
