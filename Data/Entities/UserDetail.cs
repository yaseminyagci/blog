using Core.BaseModels;
using Core.Interfaces;
using Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Constants;

namespace Data.Entities.Public
{
    public class UserDetail : HelperModel, IEntity
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("UserType")]
        public int UserTypeId { get; set; }
        [Required]
        [StringLength(GeneralConstants.StringLengthXs)]
        public string FullName { get; set; }
        public UserType UserType { get; set; }
        public User User { get; set; }

        public ICollection<Likes> Likes { get; set; } = new Collection<Likes>();
        public ICollection<Comment> Comments { get; set; } = new Collection<Comment>();
        public ICollection<Post> Posts { get; set; } = new Collection<Post>();


    }
}
