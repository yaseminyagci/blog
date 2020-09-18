using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Data.Entities.Identity;

namespace Data.Initialize.Data
{
    public static partial class InitializeData
    {

        public static List<PermissionCategory> BuildPermissionCategories(ApplicationDbContext context)
        {
            var permissions = context.Permissions.ToList();
            List<PermissionCategory> categories = new List<PermissionCategory>();
            if (permissions.Any())
            {
                var add = permissions.First(c => c.Label == nameof(PermissionEnum.Add));
                var edit = permissions.First(c => c.Label == nameof(PermissionEnum.Edit));
                var delete = permissions.First(c => c.Label == nameof(PermissionEnum.Delete));
                var list = permissions.First(c => c.Label == nameof(PermissionEnum.List));
                var now = DateTime.Now;
                categories = new List<PermissionCategory>()
                {
                    new PermissionCategory()
                {
                    Label = "User",
                    VisibleLabel = "Panel Kullanıcıları",
                    DateCreated = now ,
                    Description = "",
                    Status = (byte) RecordStatus.Active,
                    PossiblePermissions = new List<PermissionCategoryPermission>()
                    {
                        new PermissionCategoryPermission()
                        {
                            Permission = add,
                            DateCreated = now ,

                        },
                        new PermissionCategoryPermission()
                        {
                            Permission = edit,
                            DateCreated = now ,

                        },
                        new PermissionCategoryPermission()
                        {
                            Permission =delete,
                            DateCreated = now ,

                        },
                        new PermissionCategoryPermission()
                        {
                            Permission = list,
                            DateCreated = now ,

                        },

                    }

                },
                    new PermissionCategory()
                    {
                        Label = "Vehicle",
                        VisibleLabel = "Araçlar",
                        DateCreated = now ,
                        Description = "",
                        Status = (byte) RecordStatus.Active,
                        PossiblePermissions = new List<PermissionCategoryPermission>()
                        {
                            new PermissionCategoryPermission()
                            {
                                Permission = add,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = edit,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission =delete,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = list,
                                DateCreated = now ,

                            },

                        }

                    },
                    new PermissionCategory()
                    {
                        Label = "Device",
                        VisibleLabel = "Cihazlar",
                        DateCreated = now ,
                        Description = "",
                        Status = (byte) RecordStatus.Active,
                        PossiblePermissions = new List<PermissionCategoryPermission>()
                        {
                            new PermissionCategoryPermission()
                            {
                                Permission = add,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = edit,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission =delete,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = list,
                                DateCreated = now ,

                            },

                        }

                    },
                    new PermissionCategory()
                    {
                        Label = "Question",
                        VisibleLabel = "Sorular",
                        DateCreated = now ,
                        Description = "",
                        Status = (byte) RecordStatus.Active,
                        PossiblePermissions = new List<PermissionCategoryPermission>()
                        {
                            new PermissionCategoryPermission()
                            {
                                Permission = add,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = edit,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission =delete,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = list,
                                DateCreated = now ,

                            },

                        }

                    },
                    new PermissionCategory()
                    {
                        Label = "Category",
                        VisibleLabel = "Kategoriler",
                        DateCreated = now ,
                        Description = "",
                        Status = (byte) RecordStatus.Active,
                        PossiblePermissions = new List<PermissionCategoryPermission>()
                        {
                            new PermissionCategoryPermission()
                            {
                                Permission = add,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = edit,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission =delete,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = list,
                                DateCreated = now ,

                            },

                        }

                    },
                    new PermissionCategory()
                    {
                        Label = "Driver",
                        VisibleLabel = "Cihaz Kullanıcıları",
                        DateCreated = now ,
                        Description = "",
                        Status = (byte) RecordStatus.Active,
                        PossiblePermissions = new List<PermissionCategoryPermission>()
                        {
                            new PermissionCategoryPermission()
                            {
                                Permission = add,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = edit,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission =delete,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = list,
                                DateCreated = now ,

                            },

                        }

                    },
                    new PermissionCategory()
                    {
                        Label = "VehicleType",
                        VisibleLabel = "Araç Tipi Tanımlama",
                        DateCreated = now ,
                        Description = "",
                        Status = (byte) RecordStatus.Active,
                        PossiblePermissions = new List<PermissionCategoryPermission>()
                        {
                            new PermissionCategoryPermission()
                            {
                                Permission = add,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = edit,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission =delete,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = list,
                                DateCreated = now ,

                            },

                        }

                    },
                    new PermissionCategory()
                    {
                        Label = "NotificationSettings",
                        VisibleLabel = "Bildirim Ayarları",
                        DateCreated = now ,
                        Description = "",
                        Status = (byte) RecordStatus.Active,
                        PossiblePermissions = new List<PermissionCategoryPermission>()
                        {
                            new PermissionCategoryPermission()
                            {
                                Permission = add,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = edit,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission =delete,
                                DateCreated = now ,

                            },
                            new PermissionCategoryPermission()
                            {
                                Permission = list,
                                DateCreated = now ,

                            },

                        }

                    },
                    new PermissionCategory()
                    {

                        Label = "ButtonSettings",
                        VisibleLabel = "Buton Ayarları",
                        DateCreated = now ,
                        Description = "",
                        Status = (byte) RecordStatus.Active,
                        PossiblePermissions = new List<PermissionCategoryPermission>()
                        {

                            new PermissionCategoryPermission()
                            {
                                Permission = edit,
                                DateCreated = now ,

                            },

                            new PermissionCategoryPermission()
                            {
                                Permission = list,
                                DateCreated = now ,

                            },

                        }

                    },
                    new PermissionCategory()
                    {
                        Label = "SurveyReport",
                        VisibleLabel = "Anket Raporu",
                        DateCreated = now ,
                        Description = "",
                        Status = (byte) RecordStatus.Active,
                        PossiblePermissions = new List<PermissionCategoryPermission>()
                        {
                     new PermissionCategoryPermission()
                            {
                                Permission = list,
                                DateCreated = now ,

                            },

                        }

                    },
                    new PermissionCategory()
                    {
                        Label = "VehicleReport",
                        VisibleLabel = "Araç Raporu",
                        DateCreated = now ,
                        Description = "",
                        Status = (byte) RecordStatus.Active,
                        PossiblePermissions = new List<PermissionCategoryPermission>()
                        {
                            new PermissionCategoryPermission()
                            {
                                Permission = list,
                                DateCreated = now ,

                            },

                        }

                    },
                new PermissionCategory()
                {
                    Label = "Role",
                    VisibleLabel = "Yetki Grupları",
                    DateCreated = now ,
                    Description = "",
                    Status = (byte) RecordStatus.Active,
                    PossiblePermissions = new List<PermissionCategoryPermission>()
                    {
                        new PermissionCategoryPermission()
                        {
                            Permission = add,
                            DateCreated = now ,

                        },
                        new PermissionCategoryPermission()
                        {
                            Permission = list ,
                            DateCreated = now ,

                        },
                        new PermissionCategoryPermission()
                        {
                            Permission = edit,
                            DateCreated = now ,

                        },
                        new PermissionCategoryPermission()
                        {
                            Permission = delete,
                            DateCreated = now ,

                        },

                    }

                },
                new PermissionCategory()
                {
                    Label = "ReportSettings",
                    VisibleLabel = "Rapor Ayarları",
                    DateCreated = now ,
                    Description = "",
                    Status = (byte) RecordStatus.Active,
                    PossiblePermissions = new List<PermissionCategoryPermission>()
                    {
                        new PermissionCategoryPermission()
                        {
                            Permission = add,
                            DateCreated = now ,

                        },
                        new PermissionCategoryPermission()
                        {
                            Permission = list ,
                            DateCreated = now ,

                        },
                        new PermissionCategoryPermission()
                        {
                            Permission = edit,
                            DateCreated = now ,

                        },
                        new PermissionCategoryPermission()
                        {
                            Permission = delete,
                            DateCreated = now ,

                        },

                    }

                },

            };
            }




            return categories;
        }


    }
}
