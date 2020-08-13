using System.Threading.Tasks;
using System.Web.Http;
using SilverHorseTech.WebApi.Interfaces.Services;
using SilverHorseTech.WebApi.Models;
using SilverHorseTech.WebApi.Services;

namespace SilverHorseTech.WebApi.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IPostsService _postsService;

        public PostsController()
        {
            _postsService = new PostsService();
        }

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        /// <summary>
        /// Return a list of posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _postsService.GetPostsAsync());
        }

        /// <summary>
        /// Returns a specific post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest("invalid id");
            }

            Post post = await _postsService.GetPostAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        /// <summary>
        /// Save a new post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Post(Post post)
        {
            if (post == null)
            {
                return BadRequest("post body required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Post newPost = await _postsService.SavePostAsync(post);
            if (newPost == null)
            {
                return InternalServerError();
            }

            return Ok(newPost);
        }

        /// <summary>
        /// Update a post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, Post post)
        {
            if (id < 1)
            {
                return BadRequest("invalid id");
            }

            if (post == null)
            {
                return BadRequest("post body required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Post updatedPost = await _postsService.UpdatePostAsync(id, post);
            if (updatedPost == null)
            {
                return InternalServerError();
            }

            return Ok(updatedPost);
        }

        /// <summary>
        /// Delete a post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("invalid id");
            }

            return Ok(await _postsService.DeletePostAsync(id));
        }

    }
}
