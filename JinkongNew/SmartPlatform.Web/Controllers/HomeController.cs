using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.ApplicationServices;

using GModel.Basic;
using GModel.RoleRight;
using GBLL.RoleRight;
using GModel;
using SuperGPS.App_Start;

namespace SuperGPS.Controllers
{
    public class HomeController : BaseController
    {
        MenuViewBLL menuViewBll = new MenuViewBLL();
        MenuInfoBLL menuInfoBll = new MenuInfoBLL();

        public HomeController()
        {
        }

        [Log(LogMessage = "系统管理")]
        [OutputCache(CacheProfile = "ActionCacheProfile")]
        public ActionResult Index()
        {
            //terminalInfoBll.TerExChange(tif);
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            new LogMessage().Save("登陆系统管理。");

            if (user != null)
            {
                ViewBag.Deptid = user.EnterId;
                IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
                //车辆监控
                //ViewBag.CLJK = "false";
                //终端监控
                ViewBag.ZDJK = "false";
                //统计报表
                //ViewBag.TJBB = "false";
                //系统管理按钮
                //ViewBag.XTGLAN = "false";
                for (int i = 0; i < imi.Count; i++)
                {
                    switch (imi[i].MenuName)
                    {
                        case "终端监控":
                            ViewBag.ZDJK = "true";
                            break;
                    }
                }
            }


            return View();
        }
        public ActionResult BodyIframe()
        {
            return View();
        }

        [UserFilter]
        public string GetSession()
        {
            UserInfo LoginUser = (UserInfo)Session["LoginUser"];
            string SessionValue = null;
            if (LoginUser == null)
            {
                SessionValue = "登录失效";
            }
            else
            {
                SessionValue = LoginUser.UserLname;
            }
            return SessionValue;
        }

        [UserFilter]
        public ActionResult GetUserMenuList()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            List<TreeMode> mTreeNode = new List<TreeMode>();
            if (user != null)
            {    
                List<TreeMode> mTreeNodeNew = menuInfoBll.GetMenuTreeByRoleId(user.RoleId);
                return Json(mTreeNodeNew, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 操作/异常日志列表
        /// </summary>
        /// <returns></returns>
        public ActionResult LogIndex()
        {

            GBLL.Basic.LogInfoBLL.Save();

            ViewBag.KSRQ = GetKsrq();
            ViewBag.JSRQ = GetJsrq();
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user.EnterId != null && user.EnterId != "")
            {
                ViewBag.DeptId = user.EnterId;
            }
            return View();
        }

        [UserFilter]
        public string GetLogList(

                        int rows,
                        int page,
                        
                        string DeptID,
                        string ArgsLogTypeNo, //日志类型 1 = 操作日志
                        string ArgsKSRQ,      //开始时间
                        string ArgsJSRQ       //结束时间
            )
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            if (Request.QueryString["ArgsKSRQ"] != null)
            {
                ArgsKSRQ = Request.QueryString["ArgsKSRQ"].ToString();
            }
            if (Request["ArgsJSRQ"] != null)
            {
                ArgsJSRQ = Request["ArgsJSRQ"].ToString();
            }

            Hashtable ht = new Hashtable();

            ht.Add("StartData", (page - 1) * rows + 1);
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);

            ht.Add("LogTypeNo", ArgsLogTypeNo);

            if (ArgsKSRQ == null)
            {
                ArgsKSRQ = GetKsrq();
            }
            if (ArgsJSRQ == null)
            {
                ArgsJSRQ = GetJsrq();
            }

            ht.Add("KSRQ", ArgsKSRQ);
            ht.Add("JSRQ", ArgsJSRQ);

            if (DeptID == null)
            {
                DeptID = user.EnterId;
            }

            ht.Add("DeptID", DeptID);

            if (ht["LogTypeNo"] == null 
                || ht["LogTypeNo"].ToString().Trim() == "")
            {
                ht["LogTypeNo"] = "1";
            }

            int count = 0;
            IList<LogInfo> ltiv = GBLL.Basic.LogInfoBLL.GetLogInfoListPage(ht, out count);

            return ConvertToJson(ltiv, count);
        }

        private string GetKsrq()
        {
            return DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
        }
        private string GetJsrq()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}