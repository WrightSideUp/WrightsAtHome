using System.Web.Http;

namespace WrightsAtHome.Server.API.Common
{
    public static class ControllerHelpers
    {
        public static string DeviceImageUrlLarge(string imageName)
        {
            return "images/devices_large/" + imageName;
        }
        public static string DeviceImageUrlSmall(string imageName)
        {
            return "images/devices_small/" + imageName;
        }
    }
}