using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Data.Entities.Public;

namespace Data.Entities.Identity
{

    public class User : IdentityUser<string>, IEntity, IHelperModel
    {

        public ICollection<UserRole> Roles { get; set; }
        public ICollection<UserPermission> DirectivePermissions { get; set; }

        public bool IsEditable { get; set; } = true;
        public UserDetail Detail { get; set; }


        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public byte Status { get; set; }
        public User()
        {
            Roles = new Collection<UserRole>();
            DirectivePermissions = new Collection<UserPermission>();
        }
    }
}
