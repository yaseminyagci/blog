using Shared.Resources.Post;
using Shared.Resources.Tag;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Resources.TagPost
{
    public class TagPostGetData
    {
        public int Id { get; set; }     
        public PostGetData Post { get; set; }
        public TagGetData Tag { get; set; }
        public bool Selected { get; set; }

    }
}
