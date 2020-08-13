using System.Collections.Generic;
using System.Threading.Tasks;
using SilverHorseTech.WebApi.Models;

namespace SilverHorseTech.WebApi.Services
{
    public class AlbumsService : ServiceBase<Album>
    {
        /// <summary>
        /// Returns all albums
        /// </summary>
        /// <returns></returns>
        public async Task<List<Album>> GetAlbumsAsync() => await GetListAsync("/albums");
    }

}