using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Resources.Likes
{
  public class LikesData
    {
        [Key]
        public int Id { get; set; }
        public bool Liked { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
    
    }
}
