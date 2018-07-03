using GanGao.API.App_Start;
using GanGao.MEF;
using System;
using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using System.ComponentModel.Composition.Hosting;
//using System.ComponentModel.Composition.Primitives;
using System.Composition.Hosting;
//using System.Composition.Hosting.Core;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Dependencies;

namespace GanGao.API
{
    /// <summary>
    /// MEF IOC
    /// </summary>
    public class MefDependencySolver : IDependencyResolver
    {
        //private readonly ComposablePartCatalog _catalog;
        //private const string MefContainerKey = "JuCheap_MefContainerKey";

        /// <summary>
        /// MefDependencySolver
        /// </summary>
        /// <param name="catalog"></param>
        //public MefDependencySolver(ComposablePartCatalog catalog)
        //{
        //    _catalog = catalog;
        //}
        public MefDependencySolver()
        {
            //_catalog = catalog;
        }
        /// <summary>
        /// Container
        /// </summary>
        public CompositionContainer Container
        {
            get
            {
                return RegisgterMEF.regisgter();
                //if (!HttpContext.Current.Items.Contains(MefContainerKey))
                //{
                //    HttpContext.Current.Items.Add(MefContainerKey, new CompositionContainer(_catalog));
                //}
                //CompositionContainer container = (CompositionContainer)HttpContext.Current.Items[MefContainerKey];
                //HttpContext.Current.Application["Container"] = container;
                //return container;
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
            var contractName = serviceType.FullName;
            // AttributedModelServices.GetContractName(serviceType)
            var result =  Container.GetExportedValues<object>(contractName);
            return result;
        }

        /// <summary>
        /// BeginScope
        /// </summary>
        /// <returns></returns>
        public IDependencyScope BeginScope()
        {
            //return new MefDependencySolver(_catalog);
            return this;
            //return new MefDependencySolver();
        }

        #endregion

        public void Dispose()
        {
            //ToDo
        }
    }

    
}