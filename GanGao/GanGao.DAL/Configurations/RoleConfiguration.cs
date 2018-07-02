using GanGao.Common.Model.Systems;
using GanGao.IDAL;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace GanGao.DAL.Configurations
{
    /// <summary>
    /// 角色表配置类
    /// </summary>
    public class RoleConfiguration : EntityTypeConfiguration<SysRole>, IEntityMapper
    {
        public RoleConfiguration()
        {
            // 表名次定义
            this.ToTable("Sys_Roles");
            //主键定义
            this.HasKey(m => m.Id);
            //索引定义
            this.HasIndex(m => m.Name);
            //属性定义
            this.Property(m => m.Id).HasMaxLength(64);
            this.Property(m => m.Name).HasMaxLength(16);
            this.Property(m => m.Description).HasMaxLength(128);
            //关系映射
            // 角色无关系映射            
        }

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}