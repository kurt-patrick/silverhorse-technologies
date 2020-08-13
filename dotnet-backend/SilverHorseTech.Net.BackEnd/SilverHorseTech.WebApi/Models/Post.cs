using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SilverHorseTech.WebApi.Models
{
    public class Post
    {
        [Required]
        [Range(1, int.MaxValue)]
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("title")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("body")]
        public string Body { get; set; }

        public Post() { }

        public Post(int userId, string title, string body)
        {
            this.UserId = userId;
            this.Title = title;
            this.Body = body;
        }
    }
}