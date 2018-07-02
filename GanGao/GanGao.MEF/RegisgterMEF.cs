using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;

namespace GanGao.MEF
{
    public class RegisgterMEF
    {
        private static object obj = new object();
        private static CompositionContainer _container;
        public static CompositionContainer regisgter()
        {
            lock (obj)
            {
                try
                {
                    if (_container != null)
                    {
                        return _container;
                    }
                    AggregateCatalog aggregateCatalog = new AggregateCatalog();
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    var thisAssembly = new DirectoryCatalog(path, "*.dll");
                    //if (thisAssembly.Count() == 0)
                    //{
                    //    path = path + "\\bin\\";
                    //    thisAssembly = new DirectoryCatalog(path, "*.dll");
                    //}
                    aggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
                    aggregateCatalog.Catalogs.Add(thisAssembly);
                    _container = new CompositionContainer(aggregateCatalog);
                    return _container;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("错误：{0}",ex.Message);
                    return null;
                }
            }
        }
    }
}