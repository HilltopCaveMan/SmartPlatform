using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using System.Text;

using GBLL.Basic;
using GModel.Basic;

namespace SuperGPS.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    //public class MyAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    public class UserFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            ActionDescriptor d = filterContext.ActionDescriptor;
            var loginUser = filterContext.HttpContext.Session["LoginUser"];
            var right = filterContext.HttpContext.Session["Right"];
            var na = filterContext.HttpContext.Session["UserName"];
            if (loginUser == null || right == null || na == null)
            {
                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    //public class MyAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    public class IsLoginFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            ActionDescriptor d = filterContext.ActionDescriptor;

            var loginUser = filterContext.HttpContext.Session["LoginUser"];
            var right = filterContext.HttpContext.Session["Right"];
            var UserName = filterContext.HttpContext.Session["UserName"];

            if (loginUser != null
                || right != null
                || UserName != null)
            {
                filterContext.Result = new RedirectToRouteResult("Default",
                    new RouteValueDictionary(new { controller = "CarMonitor", action = "MonitorIndex_511" }));
            }
        }
    }

    /// <summary>
    /// 禁止页面缓存
    /// </summary>
    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();
        }
    }

    /// <summary>
    /// 操作及异常日志记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class LogAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public string LogMessage { get; set; }

        public void OnException(ExceptionContext filterContext)
        {
            LogInfo log = CreateLogInfo(filterContext.RouteData, filterContext.HttpContext.Request);
                
            log.YCMS = filterContext.Exception.Message;
            log.LogType = LogType.Exception;

            LogInfoBLL.Add(log);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LogInfo log = CreateLogInfo(filterContext.RouteData, filterContext.HttpContext.Request);

            log.LogType = LogType.Operation;

            if (filterContext.HttpContext.Session["logmsg"] != null)
            {
                LogMessage logMsg = (LogMessage)filterContext.HttpContext.Session["logmsg"];
                log.BZ = logMsg.message.ToString();
                filterContext.HttpContext.Session["logmsg"] = null;
            }

            if (filterContext.HttpContext.Session["UserName"] != null)
            {
                log.CZR = filterContext.HttpContext.Session["UserName"].ToString();

                UserInfo user = new UserInfo();
                user = (UserInfo)System.Web.HttpContext.Current.Session["LoginUser"];
                log.DeptId = user.EnterId;
            }

            LogInfoBLL.Add(log);

            base.OnActionExecuted(filterContext);
        }

        private string GetGNMC(RouteData rd)
        {
            string controllerNamer = rd.Values["controller"].ToString();
            string actionName = rd.Values["action"].ToString();

            return controllerNamer + "." + actionName;
        }

        private LogInfo CreateLogInfo(RouteData rd, HttpRequestBase req)
        {
            LogInfo log = new LogInfo();

            log.ID = System.Guid.NewGuid().ToString();

            log.GNMC = GetGNMC(rd);
            if (LogMessage != null)
            {
                log.GNMC = LogMessage.ToString();
            }

            log.GNDZ = req.RawUrl.ToString();
            log.WLBS = req.UserHostAddress.ToString();

            log.LYLX = "Web";
            log.YCSJ = DateTime.Now;
            log.CZSJ = DateTime.Now;

            return log;
        }
    }
}