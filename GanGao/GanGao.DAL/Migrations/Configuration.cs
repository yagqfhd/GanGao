using GanGao.Common;
using GanGao.Common.Data;
using GanGao.Common.Model.Systems;
using GanGao.MEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
                RegisgterMEF.regisgter().ComposeParts(this);
            if (PasswordValidator != null)
                _defaultPassword = PasswordValidator.HashPassword("123456");
            /// 初始化角色
            List<SysRole> roles = new List<SysRole>
            {
                new SysRole{ Name = "系统管理", Description = "系统管理角色，拥有整个系统的管理权限。"},
                new SysRole{ Name = "蓝钻", Description = "蓝钻会员角色"},
                new SysRole{ Name = "红钻", Description = "红钻会员角色"},
                new SysRole{ Name = "黄钻", Description = "黄钻会员角色"},
                new SysRole{ Name = "绿钻", Description = "绿钻会员角色"}
            };
            DbSet<SysRole> roleSet = context.Set<SysRole>();
            roleSet.AddOrUpdate(m => new { m.Name }, roles.ToArray());
            context.SaveChanges();
            /// 初始化部门
            List<SysDepartment> departments = new List<SysDepartment>
            {
                new SysDepartment { Name = "甘泉县高级中学" },
                new SysDepartment { Name = "办公室" },
                new SysDepartment { Name = "教务处" },
                new SysDepartment { Name = "德育处" },
                new SysDepartment { Name = "宣招办" },
                new SysDepartment { Name = "党办" },
                new SysDepartment { Name = "团委" },
                new SysDepartment { Name = "工会" },
                new SysDepartment { Name = "总务处" }
            };
            departments.FirstOrDefault(d => d.Name == "办公室").Parent =
                departments.FirstOrDefault(d => d.Name == "甘泉县高级中学");
            departments.FirstOrDefault(d => d.Name == "总务处").Parent =
                departments.FirstOrDefault(d => d.Name == "甘泉县高级中学");

            DbSet<SysDepartment> departmentSet = context.Set<SysDepartment>();
            departmentSet.AddOrUpdate(m => new { m.Name }, departments.ToArray());
            context.SaveChanges();
            /// 初始化用户
            List<SysUser> users = new List<SysUser>
            {
                new SysUser { Name = "admin",PasswordHash = _defaultPassword  },
                new SysUser { Name = "fhd",PasswordHash = _defaultPassword }
            };
            users.FirstOrDefault(d => d.Name == "admin")
                .Departments.Add(new UserDepartment
                {
                    DepartmentId = departments.FirstOrDefault(d => d.Name == "办公室").Id,
                    UserId = users.FirstOrDefault(d => d.Name == "admin").Id
                });

            DbSet<SysUser> userSet = context.Set<SysUser>();
            userSet.AddOrUpdate(m => new { m.Name }, users.ToArray());
            context.SaveChanges();
            /// 初始化权限

        }
    }
}