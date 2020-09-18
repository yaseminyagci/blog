using System;
using System.Collections.Generic;
using Core.Enums;
using Data.Entities.Identity;

namespace Data.Initialize.Data
{
    public static partial class InitializeData
    {

        private static string GetPermissionVisibleName(string permission)
        {
            var visibleName = permission switch
            {
                nameof(PermissionEnum.Add) => "Ekle",
                nameof(PermissionEnum.Edit) => "Düzenle",
                nameof(PermissionEnum.Delete) => "Sil",
                nameof(PermissionEnum.List) => "Görüntüle",
                _ => ""
            };
            return visibleName;
        }
        public static List<Permission> BuildPermissionsList()
        {
            var list = new List<Permission>();
            foreach (var permission in Enum.GetNames(typeof(PermissionEnum)))
            {
                list.Add(new Permission()
                {
                    Label = permission,
                    VisibleLabel = GetPermissionVisibleName(permission),
                    DateCreated = DateTime.Now,
                    Description = "",
                    Status = (byte)RecordStatus.Active,

                });

            }

            return list;
        }


    }
}
