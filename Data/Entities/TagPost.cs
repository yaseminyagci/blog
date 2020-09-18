using Core.BaseModels;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class TagPost : HelperModel, IEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
 
        public Post Post { get; set; }

        public Tag Tag { get; set; }
        public byte Selected { get; set; }
    }
}
