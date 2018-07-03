using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using System.ComponentModel.Composition;
using System;
using GanGao.MEF;
using GanGao.BLL.OAuthProvider;

[assembly: OwinStartup(typeof(GanGao.WebAPI.StartupWebAPI))]

namespace GanGao.WebAPI
{
    public class StartupWebAPI
    {

        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();

            ConfiureOAuth(app);

            app.UseCors(CorsOptions.AllowAll);
            //这一行代码必须放在ConfiureOAuth(app)之后
            app.UseWebApi(config);
            WebApiConfig.Register(config);
        }

        /// <summary>
        /// 刷新Token驱动
        /// </summary>
        [Import]
        GanGaoRefreshTokenProvider  refreshProvider { get; set; }
        /// <summary>
        /// 生成Token驱动
        /// </summary>
        [Import]
        GanGaoAuthorizationServerProvider serverProvider { get; set; }

        /// <summary>
        /// OAuth验证服务配置
        /// </summary>
        /// <param name="app"></param>
        public void ConfiureOAuth(IAppBuilder app)
        {
            RegisgterMEF.regisgter().ComposeParts(this);

            /// OAuth 验证服务器配置信息
            var serverOptions = new OAuthAuthorizationServerOptions
            {                
                TokenEndpointPath = new PathString("API/Token"),
                RefreshTokenProvider = refreshProvider,
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                AllowInsecureHttp = true,
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                Provider = serverProvider
            };
            ///打开 OAuth服务
            app.UseOAuthAuthorizationServer(serverOptions);
            // 打开 OAuth验证
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
