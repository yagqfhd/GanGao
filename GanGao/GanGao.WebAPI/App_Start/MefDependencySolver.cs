
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
        public MefDependencySolver()//(ComposablePartCatalog catalog)
        {
        }
        /// <summary>
        /// Container
        /// </summary>
        public CompositionContainer Container
        {
            get
            {
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
                return result;
        }

        /// <summary>
        /// GetServices
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            var contractName = AttributedModelServices.GetContractName(serviceType);
            var result =  Container.GetExportedValues<object>(contractName);
            return result;
        }

        /// <summary>
        /// BeginScope
        /// </summary>
        /// <returns></returns>
        public IDependencyScope BeginScope()
        {
            return this;
        }

        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class MefConfig
    {

        public static void RegisterMef(HttpConfiguration cfg)
        {
            var resolver = new MefDependencySolver();
            // Install MEF dependency resolver for MVC
            //DependencyResolver.SetResolver(resolver);
            // Install MEF dependency resolver for Web API
            cfg.DependencyResolver = resolver;
        }
    }

}