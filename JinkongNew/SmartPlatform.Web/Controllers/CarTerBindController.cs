using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GModel.Car;
using GModel.Basic;
using GBLL.Basic;
using GBLL.Car;
using System.Collections;
using SuperGPS.App_Start;

namespace SuperGPS.Controllers
{
    public class CarTerBindController : Controller
    {
        CarInfoBLL carInfoBll = new CarInfoBLL();
        UserFieldsBLL userFieldsBll = new UserFieldsBLL();
        
        [UserFilter]
        public ActionResult BindIndex()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            UserFields uf = new UserFields();
            uf.DeptId = user.EnterId;
            IList<UserFields> iuf = userFieldsBll.GetUserFieldsPage(uf);
            ArrayList arr = new ArrayList();
            for (int i = 0; i < iuf.Count; i++)
            {
                arr.Add(iuf[i].UfName.ToUpper());
            }
            return View(arr);
        }
    }
}