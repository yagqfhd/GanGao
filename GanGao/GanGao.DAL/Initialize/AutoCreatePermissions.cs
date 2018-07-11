using GanGao.Common.Model.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GanGao.DAL.Initialize
{
    public class AutoCreatePermissions
    {
        /// <summary>
        /// // 自动生成API权限
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="space"></param>
        /// <param name="areaName"></param>
        /// <returns></returns>
        public static IList<SysPermission> GetPermissions(string typeName, string space, string areaName = null)
        {
            var result = new List<SysPermission>();
            //加载程序集
            var assembly = Assembly.Load(space);
            List<Type> controllerTypes = null;
            if (areaName == null)
            {
                //获取区域名为NoName下的所有Controller类型
                controllerTypes = assembly.GetTypes().Where(type => type.BaseType.Name == typeName && type.Namespace.Contains("Areas") == false).ToList();
            }
            else
            {
                //获取区域名为areaName下的所有Controller类型
                controllerTypes = assembly.GetTypes().Where(type => type.BaseType.Name == typeName && type.Namespace.Contains(areaName)).ToList();
            }
            Console.WriteLine("获取命名空间下控制器数：{0}",controllerTypes.Count);
            controllerTypes.ForEach((controller) =>
            {
                var permissionGroup = controller.GetCustomAttribute<DescriptionAttribute>();
                var controllName = controller.Name;
                var actions = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName).ToList();
                actions.ForEach((a) =>
                {
                    var des = a.GetCustomAttribute<DescriptionAttribute>();
                    if (des != null)
                    {
                        var permissionName = "";
                        var para = a.GetParameters();
                        if (para.Any())
                        {
                            para.ToList().ForEach((p) =>
                            {
                                permissionName = permissionName + p.ParameterType.Name;
                            });
                        }
                        var permission = new SysPermission
                        {
                            Name = controllName + a.Name+permissionName,
                            ControllerName = controllName,
                            ActionName = a.Name,
                            Parameters = permissionName,
                            Description = des.Description
                        };
                        result.Add(permission);
                    }
                });
            });
            return result;
        }
    }
}