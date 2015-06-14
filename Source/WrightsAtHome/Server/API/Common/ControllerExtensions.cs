using System.Web.Http;

namespace WrightsAtHome.Server.API.Common
{
    public static class ControllerExtensions
    {
        public static string DeviceImageUrlLarge(this ApiController controller, string imageName)
        {
            return "images/devices_large/" + imageName;
        }
        public static string DeviceImageUrlSmall(this ApiController controller, string imageName)
        {
            return "images/devices_small/" + imageName;
        }
    }
}