using GanGao.Common;
using GanGao.Common.Data;
using GanGao.DAL.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GanGao.DAL.Initialize
{
    /// <summary>
    /// 数据库初始化操作类
    /// </summary>
    public static class DatabaseInitializer
    {
        
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize(string defaultPassword)
        {
            if (Database.Exists("default")) return;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, Configuration>());
        }
    }
}