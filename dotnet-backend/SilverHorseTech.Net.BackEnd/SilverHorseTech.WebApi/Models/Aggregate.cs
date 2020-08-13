using Newtonsoft.Json;

namespace SilverHorseTech.WebApi.Models
{
    public class Aggregate
    {
        [JsonProperty("album")]
        public Album Album { get; set; }
        [JsonProperty("post")]
        public Post Post { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        public Aggregate() { }

        public Aggregate(Album album, Post post, User user)
        {
            this.Album = album;
            this.Post = post;
            this.User = user;
        }
    }
}