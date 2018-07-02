using GanGao.Common.IModels.IBase;
using GanGao.Common.IModels.ISystems;
using GanGao.Common.Model.Bases;
using System;
using System.Collections.Generic;

namespace GanGao.Common.Model.Systems
{
    /// <summary>
    /// 用户类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class SysUser : EntityBase, IUser<string>,
        IEntityDepartmentCollection<UserDepartment>
    {
        #region /////// 数据字段属性定义
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nick { get; set; }
        /// <summary>
        /// 用户联系邮箱
        /// </summary>  
        public string Email { get; set; }
        /// <summary>
        /// 加密保存的密码
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 用户的真实姓名
        /// </summary>
        public string TrueName { get; set; }
        #endregion

        ///// <summary>
        ///// // 用户部门集合 UserDepartment 表中满足条件的集合
        ///// </summary>
        //public virtual ICollection<UserDepartment<TKey>> Departments { get; }

        ////public SysUser() { Departments = new HashSet<UserDepartment<TKey>>(); }
        /// <summary>
        /// // 用户部门集合 UserDepartment 表中满足条件的集合
        /// </summary>
        public virtual ICollection<UserDepartment> Departments { get; }

        public SysUser()
        {
            Id = Guid.NewGuid().ToString();
            //Departments = new HashSet<UserDepartment>();
        }
    }
    

}