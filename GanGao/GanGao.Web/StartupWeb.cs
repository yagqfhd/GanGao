using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;

[assembly: OwinStartup(typeof(GanGao.Web.StartupWeb))]

namespace GanGao.Web
{
    public class StartupWeb
    {
        public void Configuration(IAppBuilder app)
        {
#if DEBUG
            var webHostPath = @"E:\Web\GanGao\GanGao\GanGao.Web\";
#else
            var webHostPath = AppDomain.CurrentDomain.BaseDirectory;
#endif

            Console.WriteLine(webHostPath);
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseFileServer(new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = true,
                FileSystem = new PhysicalFileSystem(webHostPath)
            });
        }
    }
}
