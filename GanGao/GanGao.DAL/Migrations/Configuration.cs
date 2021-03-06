﻿using GanGao.Common;
using GanGao.Common.Data;
using GanGao.Common.Model.Systems;
using GanGao.DAL.Initialize;
using GanGao.MEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace GanGao.DAL.Migrations
{
    //internal sealed
    internal sealed class Configuration : DbMigrationsConfiguration<EFDbContext>
    {

        string _defaultPassword="123456";

        /// <summary>
        /// 密码校验生成对象
        /// </summary>
        [Import]
        IPasswordValidator PasswordValidator { get; set; }

        public Configuration()
        {
            
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;            
        }

        

        protected override void Seed(EFDbContext context)
        {
            if(PasswordValidator==null)
            {
                RegisgterMEF.regisgter().ComposeParts(this);
            }
            if (PasswordValidator != null)
                _defaultPassword = PasswordValidator.HashPassword("123456");
            #region /// 初始化角色
            List<SysRole> roles = new List<SysRole>
            {
                new SysRole{ Name = "系统管理", Description = "系统管理角色，拥有整个系统的管理权限。"},
                new SysRole{ Name = "校长", Description = "正校长"},
                new SysRole{ Name = "副校长", Description = "副校长"},
                new SysRole{ Name = "主任", Description = "正主任"},
                new SysRole{ Name = "副主任", Description = "副主任"},
                new SysRole{ Name = "班主任", Description = "带班教师"},
                new SysRole{ Name = "教师", Description = "任课教师"},
            };
            DbSet<SysRole> roleSet = context.Set<SysRole>();
            roleSet.AddOrUpdate(m => new { m.Name }, roles.ToArray());
            context.SaveChanges();

            var adminRole = roles.FirstOrDefault(d => d.Name == "系统管理");
            #endregion

            #region /// 初始化部门
            List<SysDepartment> departments = new List<SysDepartment>
            {
                new SysDepartment { Name = "甘泉高级中学" },
                new SysDepartment { Name = "办公室" },
                new SysDepartment { Name = "教务处" },
                new SysDepartment { Name = "德育处" },
                new SysDepartment { Name = "宣招办" },
                new SysDepartment { Name = "保卫科" },
                new SysDepartment { Name = "党办" },
                new SysDepartment { Name = "团委" },
                new SysDepartment { Name = "工会" },
                new SysDepartment { Name = "年级组(连亏)" },
                new SysDepartment { Name = "年级组(张宏斌)" },
                new SysDepartment { Name = "年级组(李志斌)" },
                new SysDepartment { Name = "总务处" }
            };
            var xxBm = departments.FirstOrDefault(d => d.Name == "甘泉高级中学");
            departments.FirstOrDefault(d => d.Name == "办公室").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "总务处").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "教务处").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "德育处").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "宣招办").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "保卫科").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "党办").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "团委").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "工会").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "年级组(连亏)").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "年级组(张宏斌)").Parent = xxBm;
            departments.FirstOrDefault(d => d.Name == "年级组(李志斌)").Parent = xxBm;

            DbSet<SysDepartment> departmentSet = context.Set<SysDepartment>();
            departmentSet.AddOrUpdate(m => new { m.Name }, departments.ToArray());
            context.SaveChanges();
            #endregion

            #region /// 初始化用户
            List<SysUser> users = new List<SysUser>
            {
                new SysUser { Name = "admin",Email="admin@qq.com",Nick="管理员",TrueName="管理员",PasswordHash = _defaultPassword  },
                new SysUser { Name = "fhd",Email="fhd@qq.com",Nick="无所谓",TrueName="付宏达",PasswordHash = _defaultPassword }
            };
            var admin = users.FirstOrDefault(d => d.Name == "admin");

            var adminUserDepartment = new UserDepartment
            {
                DepartmentId = xxBm.Id,
                UserId = admin.Id
            };
            adminUserDepartment.Roles.Add(new UserDepartmentRole
            {
                RoleId = adminRole.Id,
                UserId = admin.Id,
                DepartmentId = xxBm.Id
            });
            admin.Departments.Add(adminUserDepartment);

            DbSet<SysUser> userSet = context.Set<SysUser>();
            userSet.AddOrUpdate(m => new { m.Name }, users.ToArray());
            context.SaveChanges();

            #endregion
            /// 初始化权限
            /// 
            Console.WriteLine("开始初始化权限");
            var permissions = AutoCreatePermissions.GetPermissions("APIBaseController", "GanGao.WebAPI");
            Console.WriteLine("获取权限数量:{0}",permissions.Count);
            DbSet<SysPermission> permissionSet = context.Set<SysPermission>();
            permissionSet.AddOrUpdate(m => new { m.Name }, permissions.ToArray());
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                ex.EntityValidationErrors.ToList().ForEach((err) => {
                    err.ValidationErrors.ToList().ForEach((ve) => {
                        Console.WriteLine("PropertyName:{0},Msg={1}", ve.PropertyName, ve.ErrorMessage);
                    });
                });
            }
            
        }
    }
}