using Core.Constants;
using Shared.Resources.TagPost;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Resources.Post
{
    public class PostData
    {

        public int Id { get; set; }
        [Required]
        [StringLength(GeneralConstants.StringLengthXs)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        //[Required]
        [StringLength(GeneralConstants.StringLengthMd)]
        public string Img { get; set; }
        public string UserId { get; set; }
        public int[] Tags { get; set; }

    }
}
