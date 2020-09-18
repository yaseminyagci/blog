using Shared.Resources.Post;
using Shared.Resources.User;
using System.ComponentModel.DataAnnotations;

namespace Shared.Resources.Likes
{
    public class LikesGetData
    {
        [Key]
        public int Id { get; set; }
        public bool Liked { get; set; }
        public int LikeCount { get; set; }

        //public UserGetData User { get; set; } 
        //public PostGetData Post { get; set; }
    }
}
