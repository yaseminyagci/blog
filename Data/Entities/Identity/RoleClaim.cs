using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities.Identity
{
    public class RoleClaim : IdentityRoleClaim<string>, IEntity { }
}
