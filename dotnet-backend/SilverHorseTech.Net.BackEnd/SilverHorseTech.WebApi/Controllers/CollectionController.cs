using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SilverHorseTech.WebApi.Models;
using SilverHorseTech.WebApi.Services;

namespace SilverHorseTech.WebApi.Controllers
{
    public class CollectionController : ApiController
    {
        private readonly AlbumsService _albumsService = new AlbumsService();
        private readonly PostsService _postsService = new PostsService();
        private readonly UsersService _usersService = new UsersService();

        /// <summary>
        /// Return a random aggregrated list of albums, posts, users
        /// Results limited to 30 records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var albums = await _albumsService.GetAlbumsAsync();
            var posts = await _postsService.GetPostsAsync();
            var users = await _usersService.GetUsersAsync();
            var response = new List<Aggregate>();
            var random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < 30; i++)
            {
                var aggregate = new Aggregate(
                    albums[random.Next(albums.Count)],
                    posts[random.Next(posts.Count)],
                    users[random.Next(users.Count)]
                );
                response.Add(aggregate);
            }

            return Ok(response);
        }

    }
}
