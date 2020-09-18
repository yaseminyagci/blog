using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Data.Entities.Identity
{
    public class UserRole : IdentityUserRole<string>, IEntity, IHelperModel
    {

        public User User { get; set; }
        public Role Role { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public byte Status { get; set; }
    }

}
