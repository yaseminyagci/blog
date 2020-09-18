using Core.BaseModels;
using Core.Interfaces;
using Data.Entities.Public;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Likes : HelperModel, IEntity
    {
        [Key]
        public int Id { get; set; }
        public bool Liked { get; set; }
        [ForeignKey("UserDetail")]
        public string UserId { get; set; }

        public UserDetail UserDetail { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
     
        public Post Post { get; set; }
    }
}
