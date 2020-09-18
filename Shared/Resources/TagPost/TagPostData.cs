using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Resources.TagPost
{
    public class TagPostData
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int PostId { get; set; }
        public byte Selected { get; set; }

    }
}
