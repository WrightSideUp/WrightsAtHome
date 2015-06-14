using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.API.Common;

namespace WrightsAtHome.Server.API.Devices
{
    [RoutePrefix("api/[controller]")]
    public class DevicesController : ApiController
    {
        private readonly IAtHomeDbContext dbContext;
        
        public DevicesController(IAtHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        // GET: api/values
        public async Task<IEnumerable<DeviceInfo>> Get()
        {
            var devices = await dbContext.Devices
                .Include(s => s.PossibleStates)
                .ToListAsync();

            return devices.Select(d => new DeviceInfo
                {
                    Id = d.Id,
                    Name = d.Name,
                    LargeImageUrl = this.DeviceImageUrlLarge(d.ImageName),
                    SmallImageUrl = this.DeviceImageUrlSmall(d.ImageName),
                    PossibleStates = d.PossibleStates.Select(ds => new DeviceStateInfo
                                        {
                                            Id = ds.Id,
                                            Name = ds.Name,
                                            Sequence = ds.Sequence
                                        }).ToArray(),
                    CurrentStateId = d.PossibleStates[0].Id,
                    NextEvent = ""
                }).ToList();
        }
    }
}
