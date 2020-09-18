using System;
using System.Collections.Generic;
using Data.Entities.Identity;
using Data.Entities.Public;

namespace Data.Initialize.Data
{
    public static partial class InitializeData
    {

        public static List<User> BuildUserList()
        {
            var list = new List<User>()
            {
                new User { Id = Guid.NewGuid().ToString(), UserName = "admin", Email = "admin@gmail.com",IsEditable = false,
                    Detail = new UserDetail()
                {
                    //Departman = "admin",
                    FullName = "admin"
                    //Title = "admin",
                }},
                new User { Id = Guid.NewGuid().ToString(), UserName = "user", Email = "user@gmail.com", Detail = new UserDetail()
                {
                    //Departman = "user",
                    FullName = "user"
                    //Title = "user",
                }},
            };
            return list;

        }

    }
}
