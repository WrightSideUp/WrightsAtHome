using System.Web.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WrightsAtHome.Server.API.Devices
{
    [RoutePrefix("api/[controller]")]
    public class DeviceStateController : ApiController
    {
        // POST api/values
        [HttpPost]
        public IHttpActionResult CreateStateRequest([FromBody]DeviceStateRequest request)
        {
            return Ok();
        }
    }
}
