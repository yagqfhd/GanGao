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
        //导出私有方法(有参数)
        //[Export(typeof(Func<>))]
        public static void Initialize()
        {
#if DEBUG
            Console.WriteLine("开始 Database init...");
#endif 
            if (Database.Exists("default")) return;
#if DEBUG
            Console.WriteLine("开始 Database init...OKKK");
#endif 
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, Configuration>());            
        }
    }
}