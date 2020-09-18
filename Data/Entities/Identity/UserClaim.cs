using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities.Identity
{
    public class UserClaim : IdentityUserClaim<string>, IEntity { }
}
