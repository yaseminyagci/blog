using System;
using System.Collections.Generic;
using Data.Entities.Identity;

namespace Data.Initialize.Data
{
    public static partial class InitializeData
    {

        public static List<Role> BuildRoleList()
        {
            var list = new List<Role>()
            {
                new Role()
                {
                    Id = Guid.NewGuid().ToString(), Name = "ADMIN",IsEditable = false

                }

            };
            return list;

        }

    }
}
