using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using Core.Enums;
using Core.Extensions;
using Data.Entities;
using Data.Entities.Identity;
using Data.Entities.Public;
using Shared.Resources.Comment;
using Shared.Resources.Likes;
using Shared.Resources.Permission;
using Shared.Resources.PermissionCategory;
using Shared.Resources.Post;
using Shared.Resources.Role;
using Shared.Resources.Tag;
using Shared.Resources.TagPost;
using Shared.Resources.User;
using Shared.Resources.UserType;
using static Core.Constants.GeneralConstants;
namespace Process
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<User, UserGetData>()
                .ForMember(er => er.Roles,
                    opt => opt.MapFrom(e => e.Roles.Select(c => c.Role)))
                .ForMember(c => c.Detail, opt => opt.MapFrom(c => new UserDetailGetData()
                    {
                    FullName=c.Detail.FullName,
                    UserType=c.Detail.UserTypeId
                    }));


            CreateMap<UserDetail, UserDetailGetData>();
            CreateMap<UserRegisterData, User>();


            CreateMap<UserAddData, User>()
                .ForMember(c => c.Roles, opt => opt.Ignore())
                .ForMember(c => c.DirectivePermissions, opt => opt.Ignore())
                .ForMember(c => c.Detail, opt => opt.MapFrom(c => new UserDetail()
                {
                    FullName = c.FullName,
                    UserTypeId = c.UserTypeId
                }))
                .AfterMap((userAdd, user) =>
                {

                    //var addedRoles = userAdd.Roles.Where(id => user.Roles.All(f => f.RoleId != id)).Select(c => new UserRole() { RoleId = c, DateCreated = DateTime.Now });
                    //foreach (var item in addedRoles)
                    //{
                    //    user.Roles.Add(item);
                    //}

                });
            CreateMap<User, UserEditData>().ForMember(c => c.FullName, opt => opt.MapFrom(c => c.Detail.FullName));
          
            CreateMap<UserEditData, User>()
                .ForMember(c => c.Roles, opt => opt.Ignore())
                .ForMember(c => c.DirectivePermissions, opt => opt.Ignore())
                .AfterMap((userAdd, user) =>
                {
                    //var removedRoles = user.Roles.Where(f => !userAdd.Roles.Contains(f.RoleId)).ToList();
                    //foreach (var item in removedRoles)
                    //{
                    //    user.Roles.Remove(item);
                    //}

                    //if (userAdd.Roles != null && userAdd.Roles.Any())
                    //{
                    //    var addedRoles = userAdd.Roles.Where(id => user.Roles.All(f => f.RoleId != id)).Select(c => new UserRole() { RoleId = c, DateCreated = DateTime.Now }).ToList();
                    //    foreach (var item in addedRoles)
                    //    {
                    //        user.Roles.Add(item);
                    //    }
                    //}
                });
            #endregion

            #region Role
            CreateMap<Role, RoleGetData>()
                .ForMember(c => c.Permissions, opt => opt.MapFrom(m => m.PermissionCategory.Select(c => c.PermissionCategoryPermission)))
                .ForMember(c => c.Users, opt => opt.MapFrom(m => m.Users.Select(c => c.User)))
                .ForMember(c => c.DateCreated, opt => opt.MapFrom(m => m.DateCreated.ToString(DefaultDateFormat, CultureInfo.InvariantCulture)));
            CreateMap<RoleData, Role>()
             .ForMember(c => c.PermissionCategory, opt => opt.Ignore())
             .AfterMap((roleAdd, role) =>
             {
                 var removedPermissions = role.PermissionCategory.Where(f => !roleAdd.PermissionCategories.Contains(f.PermissionCategoryPermissionId)).ToList();
                 foreach (var item in removedPermissions)
                 {
                     role.PermissionCategory.Remove(item);
                 }

                 if (roleAdd.PermissionCategories != null && roleAdd.PermissionCategories.Any())
                 {
                     var addedPermissions = roleAdd.PermissionCategories.Where(id => role.PermissionCategory.All(f => f.PermissionCategoryPermissionId != id)).Select(c => new RolePermissionCategory() { PermissionCategoryPermissionId = c, DateCreated = DateTime.Now }).ToList();
                     foreach (var item in addedPermissions)
                     {
                         role.PermissionCategory.Add(item);
                     }
                 }

             });

            #endregion

            #region Permission

            CreateMap<Permission, PermissionGetData>()
                .ForMember(c => c.DateCreated, opt => opt.MapFrom(m => m.DateCreated.ToString(DefaultDateFormat, CultureInfo.InvariantCulture)));
            #endregion   

            #region Permission Category

            CreateMap<PermissionCategory, PermissionCategoryGetData>()
                .ForMember(c => c.DateCreated, opt => opt.MapFrom(m => m.DateCreated.ToCustomFormatString(false)));


            CreateMap<PermissionCategoryPermission, RolePermissionCategoryGetData>()
                .ForMember(c => c.RelationId, opt => opt.MapFrom(m => m.Id))
                .ForMember(c => c.Category, opt => opt.MapFrom(m => m.Category))
                .ForMember(c => c.Permission, opt => opt.MapFrom(m => m.Permission))
                ;
            #endregion

            #region Tag
            CreateMap<Tag, TagGetData>();
            CreateMap<TagData, Tag>();
            //CreateMap<List<Tag>, List<TagGetData>>();
            #endregion

            #region Post
            CreateMap<Post, PostGetData>().ForMember(er => er.User,
                    opt => opt.MapFrom(e => new UserDetailGetData
                    {
                        UserType = e.UserDetail.UserTypeId,
                        FullName = e.UserDetail.FullName
                    }));

            CreateMap<PostData, Post>().ForMember(c => c.Tags, opt => opt.Ignore());
            #endregion

            #region Comment
            CreateMap<Comment, CommentGetData>().ForMember(er => er.UserDetail,
                    opt => opt.MapFrom(e => new UserDetailGetData
                    {
                        FullName=e.UserDetail.FullName,
                        UserType=e.UserDetail.UserTypeId
                    })).ForMember(er => er.Post,
                    opt => opt.MapFrom(e => new PostGetData
                    {
                        Id = e.PostId,
                        Title=e.Post.Title,
                        DateCreated=e.DateCreated,
                        Content=e.Content

                     }));
            CreateMap<CommentData, Comment>();

            #endregion

            #region Likes
            CreateMap<Likes, LikesGetData>();
            CreateMap<LikesData, Likes>();
            #endregion 

            #region UserType
            CreateMap<UserType, UserTypeGetData>();
            CreateMap<UserTypeData, UserType>();
            #endregion

            #region TagPost
            CreateMap<TagPost, TagPostGetData>().ForMember(c => c.Selected, opt => opt.Ignore());
            CreateMap<TagPostData,TagPost>();
            #endregion
        }
    }
}
