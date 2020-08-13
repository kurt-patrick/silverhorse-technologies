using System.Threading.Tasks;
using System.Web.Http;
using SilverHorseTech.WebApi.Services;

namespace SilverHorseTech.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly UsersService _usersService = new UsersService();

        /// <summary>
        /// Return a list of users
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _usersService.GetUsersAsync());
        }

    }
}
