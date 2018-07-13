using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GanGao.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API 配置和服务
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            
            //跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // WebAPI 特征路由
            config.MapHttpAttributeRoutes();
            //Web API 路由            
            config.Routes.MapHttpRoute(
                name: "DefaultApi", // {action}/
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    //Version = 1,
                    id = RouteParameter.Optional
                }
            );

            
            //// 干掉XML序列化器
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //修改App_Start文件夹中的WebApiConfig.cs文件，实现api数据通过json格式返回
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            //jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // 对 JSON 数据使用混合大小写。跟属性名同样的大小.输出
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();
            
            // 对Null 返回空字符串
            //config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }
    }
}