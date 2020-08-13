using System.Threading.Tasks;
using System.Web.Http;
using SilverHorseTech.WebApi.Services;

namespace SilverHorseTech.WebApi.Controllers
{
    public class AlbumsController : ApiController
    {
        private readonly AlbumsService _albumsService = new AlbumsService();

        /// <summary>
        /// Return a list of albums
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _albumsService.GetAlbumsAsync());
        }
    }
}