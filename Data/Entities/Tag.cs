using Core.BaseModels;
using Core.Constants;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    public class Tag: HelperModel, IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(GeneralConstants.StringLengthMd)]
        public string TagName { get; set; }

        public ICollection<TagPost> Posts { get; set; } = new Collection<TagPost>();
    }
}
