using Core.BaseModels;
using Core.Constants;
using Core.Interfaces;
using Data.Entities.Public;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
   public class Post: HelperModel, IEntity
    {
        public Post()
        {
          
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(GeneralConstants.StringLengthXs)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        //[Required]
        [StringLength(GeneralConstants.StringLengthMd)]
        public string Image { get; set; }

        [ForeignKey("UserDetail")]
        public string UserId { get; set; }
      
        public UserDetail UserDetail { get; set; }
        public ICollection<Likes> Likes { get; set; } = new Collection<Likes>();
        public ICollection<Comment> Comments { get; set; } = new Collection<Comment>();
        public ICollection<TagPost> Tags { get; set; } = new Collection<TagPost>();
    }
}
