
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Web.Http;
using System.Web.Http.Dependencies;
using GanGao.MEF;

namespace GanGao.WebAPI
{
    /// <summary>
    /// MEF IOC
    /// </summary>
    public class MefDependencySolver :  IDependencyResolver
    {
        private readonly ComposablePartCatalog _catalog;
        private static CompositionContainer _compositionContainer=null;
        public MefDependencySolver()//(ComposablePartCatalog catalog)
        {
            //_catalog = catalog;
#if DEBUG
            
            //_compositionContainer = new CompositionContainer(_catalog, CompositionOptions.DisableSilentRejection);
            Console.WriteLine("MefDependncySolver Create");
#else
            _compositionContainer = new CompositionContainer(_catalog);
#endif
            //_compositionContainer = new CompositionContainer(_catalog, CompositionOptions.DisableSilentRejection);

        }
        /// <summary>
        /// Container
        /// </summary>
        public CompositionContainer Container
        {
            get
            {

                //#if DEBUG
                //Console.WriteLine("Get CompositionContainer Container ={0}", _compositionContainer == null);
                //#endif 
                //                if(_compositionContainer==null)
                //                {
                //#if DEBUG
                //                    _compositionContainer = new CompositionContainer(_catalog, CompositionOptions.DisableSilentRejection);
                //                    Console.WriteLine("MefDependncySolver Create");
                //#else
                //                    _compositionContainer = new CompositionContainer(_catalog);
                //#endif
                //                }
                // return _compositionContainer;
                return RegisgterMEF.regisgter();
            }
        }

        #region IDependencyResolver Members

        /// <summary>
        /// GetService
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
                string contractName = AttributedModelServices.GetContractName(serviceType);
                var result = Container.GetExportedValueOrDefault<object>(contractName);
                
#if DEBUG
                Console.WriteLine("Solver GetService One typeContract {0} = {1}", contractName, result == null ? "null" : result.ToString());
#endif
                return result;
        }

        /// <summary>
        /// GetServices
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            //Console.WriteLine("Solver GetService ALL typeContract");
            //var contractName = serviceType.FullName;
            var contractName = AttributedModelServices.GetContractName(serviceType);
            var result =  Container.GetExportedValues<object>(contractName);
#if DEBUG
            Console.WriteLine("Solver GetService List typeContract {0} = {1}", contractName, result == null ? "null" : result.ToString());
#endif
            return result;
        }

        /// <summary>
        /// BeginScope
        /// </summary>
        /// <returns></returns>
        public IDependencyScope BeginScope()
        {
            //return new MefDependencySolver(_catalog);
#if DEBUG
            Console.WriteLine("BeginScope()");
#endif
            return this;
            //return new MefDependencySolver(_catalog);
        }

        #endregion

        public void Dispose()
        {
            //ToDo
            //Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class MefConfig
    {

        public static void RegisterMef(HttpConfiguration cfg)
        {
#if DEBUG
            Console.WriteLine("RegisterMef....cfg");
#endif
            //AggregateCatalog aggregateCatalog = new AggregateCatalog();
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            //var thisAssembly = new DirectoryCatalog(path, "*.dll");
            //if (thisAssembly.Count() == 0)
            //{
            //    path = path + "bin\\";
            //    thisAssembly = new DirectoryCatalog(path, "*.dll");
            //}
            //aggregateCatalog.Catalogs.Add(thisAssembly);

            var resolver = new MefDependencySolver(); // new MefDependencySolver(aggregateCatalog);
            // Install MEF dependency resolver for MVC
            //DependencyResolver.SetResolver(resolver);
            // Install MEF dependency resolver for Web API
            cfg.DependencyResolver = resolver;
        }
    }

}