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
            return webClient.DownloadString("http://180.218.133.164:50888/Parking/ParkingSpace");
        }
    }
}
