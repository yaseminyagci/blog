using Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Resources.Comment
{
   public class CommentData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(GeneralConstants.StringLengthLg)]
        public string Content { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
 
    }
}
