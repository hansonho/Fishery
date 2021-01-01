using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Fishery.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ParkingSpaceController : ControllerBase
    {
        public string Get()
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadString("http://49.158.145.38:50888/Parking/ParkingSpace");
        }
    }
}
