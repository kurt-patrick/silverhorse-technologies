using System.Collections.Generic;
using System.Threading.Tasks;
using SilverHorseTech.WebApi.Models;

namespace SilverHorseTech.WebApi.Services
{
    public class UsersService : ServiceBase<User>
    {
        /// <summary>
        /// Returns a list of users
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync() => await GetListAsync("/users");
    }
}