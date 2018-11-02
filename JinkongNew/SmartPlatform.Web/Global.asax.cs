using System;
using SuperGPS;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Timers;

namespace SuperGPS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //定时任务
            System.Timers.Timer CreateLogTable = new System.Timers.Timer();

            CreateLogTable.Interval = 1000 * 60 * 60 ; // 一小时 
            //CreateLogTable.Interval = 1000 * 30 ; // 一分钟
            
            CreateLogTable.Enabled = true;
            CreateLogTable.Elapsed += new ElapsedEventHandler(CreateLogTable_Elapsed); ;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }

        //每月一号检查表并创建
        public void CreateLogTable_Elapsed(Object sender, ElapsedEventArgs e)
        {
            DateTime dt = e.SignalTime;

            if (dt.Day == 1)
            {
                if (dt.Hour == 0)
                {
                    //判断并创建;
                    GBLL.Basic.LogInfoBLL.CreateLogTable();
                }
            }
        }
    }
}
