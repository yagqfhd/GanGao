using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GanGao.IDAL.ISystems;
using System.ComponentModel.Composition;
using GanGao.MEF;
using GanGao.DAL.Initialize;
using GanGao.Common;
using System.Linq;

namespace GanGao.DAL.Test
{
    [TestClass]
    public class DALTest
    {
        /// <summary>
        /// 密码校验生成对象
        /// </summary>
        [Import]
        IPasswordValidator PasswordValidator { get; set; }

        [Import]
        IUserRepository userRepository { get; set; }
        private void Compose()
        {
            RegisgterMEF.regisgter().ComposeParts(this);
        }
        public DALTest()
        {            
            Compose();
            var defaultPassword = PasswordValidator.HashPassword("123456");
            Console.WriteLine("Password [{0}]", defaultPassword);
            DatabaseInitializer.Initialize();
        }
        [TestMethod]
        public void TestUserRepository()
        {
            Console.WriteLine("UserRepository Import ={0}", userRepository == null);
            userRepository.Insert(new Common.Model.Systems.SysUser { Name = "gangao" , PasswordHash = PasswordValidator.HashPassword("123456") });
            var verify = PasswordValidator.VerifyHashedPassword(
                userRepository.Entities.FirstOrDefault(d => d.Name.Equals("admin")).PasswordHash,
                "123456");
            Console.WriteLine("User Admin Password [{0}] : HashPassword[{1}]  is [{2}]", "123456", userRepository.Entities.FirstOrDefault(d => d.Name.Equals("admin")).PasswordHash, verify);
        }
    }
}
