using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Core.Constants;

namespace Data.Entities.Identity
{
    public class Role : IdentityRole<string>, IEntity, IHelperModel
    {
        public ICollection<UserRole> Users { get; set; }
        public ICollection<RolePermissionCategory> PermissionCategory { get; set; }

        [StringLength(GeneralConstants.StringLengthLg)]
        public string Description { get; set; }
        public bool IsEditable { get; set; } = true;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public byte Status { get; set; }
        public Role()
        {
            Users = new Collection<UserRole>();
            PermissionCategory = new Collection<RolePermissionCategory>();
        }


    }
}
