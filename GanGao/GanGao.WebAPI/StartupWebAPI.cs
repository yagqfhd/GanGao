using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using System;
using GanGao.WebAPI.OAuthProvider;
using GanGao.Common.DToMap;
using GanGao.DAL.Initialize;
using System.Linq;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(GanGao.WebAPI.StartupWebAPI))]

namespace GanGao.WebAPI
{
    public class StartupWebAPI
    {

        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();
            /// 配置MEF IOC
            MefConfig.RegisterMef(config);
            // 配置数据库初始化
            DatabaseInitializer.Initialize();
            //配置AutoMapper DToM转化
            AutoMapperProfileRegister.Register();
            //配置Token验证
            ConfiureOAuth(app);
            //这一行代码必须放在ConfiureOAuth(app)之后
            app.UseWebApi(config);
            app.UseCors(CorsOptions.AllowAll);

            WebApiConfig.Register(config);
            //GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        

        /// <summary>
        /// OAuth验证服务配置
        /// </summary>
        /// <param name="app"></param>
        public void ConfiureOAuth(IAppBuilder app)
        {
            /// OAuth 验证服务器配置信息
            var serverOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/API/Token"),
                RefreshTokenProvider = new GanGaoRefreshTokenProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                AllowInsecureHttp = true,
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                Provider = new GanGaoAuthorizationServerProvider()
            };
            ///打开 OAuth服务
            app.UseOAuthAuthorizationServer(serverOptions);
            // 打开 OAuth验证
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
