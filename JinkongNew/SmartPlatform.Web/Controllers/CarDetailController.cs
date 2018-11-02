using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GBLL;
using GBLL.Car;
using GModel.Car;
using SuperGPS.App_Start;
using System.Data;
using GModel.Basic;
using System.Text;
using GBLL.Basic;

namespace SuperGPS.Controllers
{
    public class CarDetailController : Controller
    {
        ColligateQueryService cgq = new ColligateQueryService();
        CarInfoBLL carInfoBll = new CarInfoBLL();

        UserFieldsBLL userFieldsBll = new UserFieldsBLL();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();
        ColligateQueryService c = new ColligateQueryService();

        // GET: /CarDetail/
        [Log(LogMessage = "查看车辆信息")]
        public ActionResult Index(string TerNo)
        {
            CarDetialInfo cdi = null;
            if (TerNo == "null")
            {
                TerNo = "";
            }

            cdi = carInfoBll.GetCarDetial(TerNo);
            
            //cdi.InstallInfo = this.GetFiledsInfo(CarId, TerNo, "1");  //安装信息

            //cdi.CreditInfo = this.GetFiledsInfo(CarId, TerNo, "2");  //信贷信息

            //cdi.CarOwnerInfo = this.GetFiledsInfo(CarId, TerNo, "3"); //车主信息

            return View(cdi);
        }

        [UserFilter]
        public DataTable GetFiledsInfo(string CarId, string TerNo,string InfoType)
        {
            UserInfo user = new UserInfo();
            DataTable dt = new DataTable();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                StringBuilder sb = new StringBuilder();
                UserFields uf = new UserFields();
                uf.DeptId = user.EnterId;
                uf.InfoType = InfoType;
                IList<UserFields> iuf = userFieldsBll.GetUserFieldsPage(uf);
                if (iuf.Count > 0)
                {
                    sb.Append("select ");
                    for (int i = 0; i < iuf.Count; i++)
                    {
                        if (i < (iuf.Count - 1))
                            sb.Append("tt." + iuf[i].UfName + ",");
                        else
                            sb.Append("tt." + iuf[i].UfName);
                    }
                    sb.Append(" from Car_Info ci join dept_info di on ci.businessdivisionid=di.businessdivisionid left join terminal_info ti on ci.car_id=ti.car_id left join");
                    sb.Append(" (SELECT CAR_ID,");
                    for (int i = 0; i < iuf.Count; i++)
                    {
                        if (i < (iuf.Count - 1))
                        {
                            sb.Append(string.Format("max(CASE UF_ID WHEN '{0}' THEN FIELD_VALUE ELSE '' END) as {1},", iuf[i].UfId, iuf[i].UfName));
                        }
                        else
                        {
                            sb.Append(string.Format("max(CASE UF_ID WHEN '{0}' THEN FIELD_VALUE ELSE '' END) as {1} ", iuf[i].UfId, iuf[i].UfName));
                        }
                    }
                    sb.Append(" FROM field_values GROUP BY CAR_ID) tt on ci.car_id=tt.car_id where 1=1");
                    sb.Append(string.Format(" and ci.car_id='{0}'", CarId));
                    sb.Append(string.Format(" and ti.TER_NO='{0}'", TerNo));

                    DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString());
                    dt=ds.Tables[0];
                }
            }
            return dt;
        }
    }
}