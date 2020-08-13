using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SilverHorseTech.WebApi.Models;

namespace SilverHorseTech.WebApi.Interfaces.Services
{
    public interface IPostsService
    {
        Task<HttpResponseMessage> DeletePostAsync(int id);
        Task<Post> GetPostAsync(int id);
        Task<List<Post>> GetPostsAsync();
        Task<Post> SavePostAsync(Post post);
        Task<Post> UpdatePostAsync(int id, Post post);
    }
}