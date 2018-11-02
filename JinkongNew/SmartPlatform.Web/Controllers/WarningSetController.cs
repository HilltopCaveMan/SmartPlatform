using GModel.RoleRight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GModel.Basic;
using SuperGPS.App_Start;

namespace SuperGPS.Controllers
{
    public class WarningSetController : Controller
    {
        // GET: Warning
        [Log(LogMessage = "")]
        public ActionResult WarningSetIndex()
        {
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.EditWarningVal = "false";
            ViewBag.AddWarningVal = "false";
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "修改报警参数":
                        ViewBag.EditWarningVal = "true";
                        break;
                    case "添加报警参数":
                        ViewBag.AddWarningVal = "true";
                        break;
                }
            }
            return View();
        }

        public WarnParam GetWarnParam() 
        {



            return null;
        }
    }
}