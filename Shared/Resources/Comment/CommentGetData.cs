using Core.Constants;
using Shared.Resources.Post;
using Shared.Resources.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Resources.Comment
{
    public  class CommentGetData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(GeneralConstants.StringLengthLg)]
        public string Content { get; set; }

        public UserDetailGetData UserDetail { get; set; }

        public PostGetData Post { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
