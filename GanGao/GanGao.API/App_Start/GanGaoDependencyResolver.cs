using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using System.Web.Http.Dependencies;

namespace GanGao.API.App_Start
{
    public class GanGaoDependencyResolver : GanGaoDependencyScope, IDependencyResolver
    {
        readonly ExportFactory<CompositionContext> _requestScopeFactory;

        /// <summary>
        /// Construct a <see cref="StandaloneDependencyResolver"/> for the provided
        /// root composition scope.
        /// </summary>
        /// <param name="rootCompositionScope">The scope to provide application-level services to
        /// the program.</param>
        public GanGaoDependencyResolver(CompositionHost rootCompositionScope)
            : base(new Export<CompositionContext>(rootCompositionScope, rootCompositionScope.Dispose))
        {
            if (rootCompositionScope == null) throw new ArgumentNullException();
            var factoryContract = new CompositionContract(typeof(ExportFactory<CompositionContext>), null, new Dictionary<string, object> {
                { "SharingBoundaryNames", new[] { Boundaries.HttpRequest, Boundaries.DataConsistency, Boundaries.UserIdentity }}
            });

            _requestScopeFactory = (ExportFactory<CompositionContext>)rootCompositionScope.GetExport(factoryContract);
        }

        /// <summary>
        /// Create a new request-specific scope.
        /// </summary>
        /// <returns>A new scope.</returns>
        public IDependencyScope BeginScope()
        {
            return new GanGaoDependencyScope(_requestScopeFactory.CreateExport());
        }
    }


    public class MefConfig
    {
        GanGaoDependencyResolver CreateResolver(params Type[] parts)
        {
            var container = new ContainerConfiguration()
                .WithParts(parts)
                .CreateContainer();

            return new GanGaoDependencyResolver(container);
        }

        public static void RegisterMef()
        {
            

            var container = CompositionHost.CreateCompositionHost(export);
            var resolver = new GanGaoDependencyResolver(container);
            // Install MEF dependency resolver for MVC
            //DependencyResolver.SetResolver(resolver);
            // Install MEF dependency resolver for Web API
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}