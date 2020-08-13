using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SilverHorseTech.WebApi.Interfaces.Services;
using SilverHorseTech.WebApi.Models;

namespace SilverHorseTech.WebApi.Services
{
    public class PostsService : ServiceBase<Post>, IPostsService
    {
        private const string URL_PATH = "/posts";

        /// <summary>
        /// Returns a list of posts
        /// </summary>
        /// <returns></returns>
        public async Task<List<Post>> GetPostsAsync()
        {
            return await GetListAsync(URL_PATH);
        }

        /// <summary>
        /// Returns a specific post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Post> GetPostAsync(int id)
        {
            return await GetAsync($"{URL_PATH}/{id}");
        }

        /// <summary>
        /// Save a new post entry
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Post> SavePostAsync(Post post)
        {
            return await PostJsonAsync(URL_PATH, post);
        }

        /// <summary>
        /// Update an existing post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Post> UpdatePostAsync(int id, Post post)
        {
            return await PutJsonAsync($"{URL_PATH}/{id}", post);
        }

        /// <summary>
        /// Delete a post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeletePostAsync(int id)
        {
            return await DeleteAsync($"{URL_PATH}/{id}");
        }

    }
}