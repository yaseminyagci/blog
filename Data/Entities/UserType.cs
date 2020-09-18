using Core.BaseModels;
using Core.Constants;
using Core.Interfaces;
using Data.Entities.Public;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    public class UserType : HelperModel, IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(GeneralConstants.StringLengthMd)]
        public string Type { get; set; }
        public ICollection<UserDetail> UserDetails { get; set; } = new Collection<UserDetail>();

    }
}
