using GanGao.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace GanGao.WebAPI.Controllers
{
    /// <summary>
    /// API控件的基类
    /// </summary>
    public class APIBaseController : ApiController
    {
        #region ///////// 内部使用方法
        /// <summary>
        /// 获取指定属性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private object GetProperties(string key)
        {
            object result = null;
            Request.Properties.TryGetValue(key, out result);
            return result;
        }

        /// <summary>
        /// 获取模型验证错误信息
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetModelStateError(ModelStateDictionary status)
        {
            var errstr = status.Keys.AsEnumerable().ExpandAndToString(";");
            //string error = string.Empty;
            //foreach (var key in status.Keys)
            //{
            //    var state = status[key];
            //    if (state.Errors.Any())
            //    {
            //        error = state.Errors.First().ErrorMessage;
            //        break;
            //    }
            //}
            //return error;
            return errstr;
        }
        #endregion
    }
}
