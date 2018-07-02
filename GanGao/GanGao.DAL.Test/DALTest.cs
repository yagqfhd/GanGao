using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GanGao.IDAL.ISystems;
using System.ComponentModel.Composition;
using GanGao.MEF;

namespace GanGao.DAL.Test
{
    [TestClass]
    public class DALTest
    {

        [Import]
        IUserRepository userRepository { get; set; }
        private void Compose()
        {
            RegisgterMEF.regisgter().ComposeParts(this);
        }
        public DALTest()
        {
           
        }
        [TestMethod]
        public void TestUserRepository()
        {
            Compose();
            Console.WriteLine("UserRepository Import ={0}", userRepository == null);
        }
    }
}
