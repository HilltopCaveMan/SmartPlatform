using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GBLL.Car;
using GModel.Car;
using GModel.Basic;
using GBLL;
using GBLL.Basic;
using System.Text;
using System.Collections;
using System.Data;
using System.Web.Script.Serialization;
using GModel.RoleRight;
using SuperGPS.App_Start;
using GCommon;
using System.IO;
using GBLL.Location;
using GModel.Location;

namespace SuperGPS.Controllers
{
    public class CarInfoController : BaseController
    {
        // GET: CarInfo
        ColligateQueryService c = new ColligateQueryService();
        UserFieldsBLL userFieldsBll = new UserFieldsBLL();
        CarInfoBLL carInfoBll = new CarInfoBLL();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();
        TerminalInfoBLL terbll = new TerminalInfoBLL();
        RealtimeDataBLL realtimedatabll = new RealtimeDataBLL();
        /// <summary>
        /// 主查询页面
        /// </summary>
        /// <returns></returns>
        [Log(LogMessage = "车辆管理")]
        [UserFilter]
        public ActionResult CarInfoIndex()
        {
            new LogMessage().Save("加载车辆信息列表;");

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

            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];

            ViewBag.AddCar = "false";
            ViewBag.EditCar = "false";
            ViewBag.DelCar = "false";
            ViewBag.XSZ = "false";
            ViewBag.DeptId = user.EnterId;
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "添加车辆":
                        ViewBag.AddCar = "true";
                        break;
                    case "删除车辆":
                        ViewBag.DelCar = "true";
                        break;
                    case "修改车辆":
                        ViewBag.EditCar = "true";
                        break;
                    case "添加行驶证信息":
                        ViewBag.XSZ = "true";
                        break;
                }
            }

            return View(arr);
        }

        /// <summary>
        /// 添加车辆窗口
        /// </summary>
        /// <param name="uf"></param>
        /// <returns></returns>
        [UserFilter]
        public ActionResult AddCar(string Businessdivisionid)
        {
            UserFields uf = new UserFields();
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            uf.DeptId = user.EnterId;
            ViewBag.DeptId = Businessdivisionid;
            IList<UserFields> iuf = userFieldsBll.GetUserFieldsPage(uf);
            return View(iuf);
        }

        [HttpPost]
        [UserFilter]
        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <param name="carInfo"></param>
        /// <param name="Hid_FieldsVal"></param>
        /// <returns></returns>
        [Log(LogMessage = "车辆添加")]
        public ActionResult CarInfoForm(CarInfo carInfo, string Hid_FieldsVal, string TerGuid, string TypeName)
        {
            if (carInfo.CarNo != null && carInfo.CarNo.Trim() != "")
            {
                new LogMessage().Save("CarNo:" + carInfo.CarNo + ";");

                ArrayList arr = new ArrayList();
                carInfo.CarId = System.Guid.NewGuid().ToString();
                if (carInfo.Businessdivisionid != null || carInfo.Businessdivisionid.Trim() != "")
                {
                    DeptInfo di = deptInfoBll.GetDeptInfo(carInfo.Businessdivisionid);
                    carInfo.CarDeptcode = di.Businessdivisioncode;
                }
                else
                {
                    ViewBag.Result = "所属企业不正确或者为空";
                    return View();
                }
                arr.Add(carInfo);

                if (TerGuid != null && TerGuid.Trim() != "")
                {
                    arr.Add(TerGuid);
                }
                else
                {
                    ViewBag.Result = "终端号不正确或者为空";
                    return View();
                }

                string kku = carInfoBll.InsertCarInfo(arr, TypeName);
                ViewBag.Result = kku;

                //绑定车辆终端
                UserInfo user = (UserInfo)Session["LoginUser"];
                string carhbinfo = "";
                if (carInfo.CarColor != "" || TypeName != "")
                {
                    string cartypename = "";
                    if (TypeName.Split('-').Length == 3)
                    {
                        cartypename = TypeName.Split('-')[2];
                    }
                    else
                    {
                        cartypename = TypeName;
                    }

                    carhbinfo = cartypename + "|" + carInfo.CarColor + "|||||||||||||||||||||||||||||";
                }
                string[] terguids = TerGuid.Split(',');
                foreach (string terguid in terguids)
                {
                    TerminalInfoBLL terbll = new TerminalInfoBLL();
                    TerminalInfo terinfo = terbll.GetTerminalInfo(terguid);
                    if (terinfo != null && terinfo.TerNo != "")
                    {
                        Transfers.ClintSendCommData(1107, "50", "", terinfo.TerNo, "", "", "", "", "", "", "", carInfo.CarDeptcode, carInfo.CarNo, carhbinfo, carInfo.CarAdminName, carInfo.CarFrame, "", "", user.UserName);
                    }
                }
                //刷新车辆
                //Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "2", "", "", "", "", "", "");
                return View();
            }
            else
            {
                ViewBag.Result = "false";
                return View();
            }
        }

        /// <summary>
        /// 查询车辆（包括动态字段）
        /// </summary>
        /// <param name="carInfo"></param>
        /// <returns></returns>
        [UserFilter]
        public string GetCarList_bak(CarInfo carInfo, int rows, int page, string ChildrenSel)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            if (user != null)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sbCount = new StringBuilder();

                sb.Append("select ci.car_id,ci.car_no,ct.type_name,di.businessdivisionname,ti.ter_no,ci.car_adminname,ci.car_admincardid");
                sbCount.Append("select count(*) ");

                UserFields uf = new UserFields();

                if (carInfo.DeptId != "" && carInfo.DeptId != null)
                {
                    uf.DeptId = carInfo.Businessdivisionid;
                }
                else
                {
                    uf.DeptId = user.EnterId;
                }

                IList<UserFields> iuf = userFieldsBll.GetUserFieldsPage(uf);

                if (iuf.Count > 0)
                {
                    sb.Append(",");
                }

                for (int i = 0; i < iuf.Count; i++)
                {
                    if (i < (iuf.Count - 1))
                        sb.Append("tt." + iuf[i].UfName + ",");
                    else
                        sb.Append("tt." + iuf[i].UfName);
                }

                sb.Append(" from Car_Info ci left join car_type ct on ci.type_id=ct.type_id join dept_info di on ci.businessdivisionid=di.businessdivisionid  left join terminal_info ti on ci.car_id=ti.car_id left join");
                sbCount.Append(" from Car_Info ci left join car_type ct on ci.type_id=ct.type_id join dept_info di on ci.businessdivisionid=di.businessdivisionid  left join terminal_info ti on ci.car_id=ti.car_id left join");

                sb.Append(" (SELECT CAR_ID ");
                sbCount.Append(" (SELECT CAR_ID ");
                if (iuf.Count > 0)
                {
                    sb.Append(",");
                    sbCount.Append(",");
                }
                for (int i = 0; i < iuf.Count; i++)
                {
                    if (i < (iuf.Count - 1))
                    {
                        sb.Append(string.Format("max(CASE UF_ID WHEN '{0}' THEN FIELD_VALUE ELSE '' END) as {1},", iuf[i].UfId, iuf[i].UfName));
                        sbCount.Append(string.Format("max(CASE UF_ID WHEN '{0}' THEN FIELD_VALUE ELSE '' END) as {1},", iuf[i].UfId, iuf[i].UfName));
                    }
                    else
                    {
                        sb.Append(string.Format("max(CASE UF_ID WHEN '{0}' THEN FIELD_VALUE ELSE '' END) as {1} ", iuf[i].UfId, iuf[i].UfName));
                        sbCount.Append(string.Format("max(CASE UF_ID WHEN '{0}' THEN FIELD_VALUE ELSE '' END) as {1} ", iuf[i].UfId, iuf[i].UfName));
                    }
                }
                sb.Append(" FROM field_values GROUP BY CAR_ID) tt on ci.car_id=tt.car_id where 1=1");
                sbCount.Append(" FROM field_values GROUP BY CAR_ID) tt on ci.car_id=tt.car_id where 1=1");
                if (carInfo.CarNo != null && carInfo.CarNo.Trim() != "")
                {
                    sb.Append(string.Format(" and ci.car_no like '%{0}%'", carInfo.CarNo));
                    sbCount.Append(string.Format(" and ci.car_no like '%{0}%'", carInfo.CarNo));
                }
                if (carInfo.TypeId != null && carInfo.TypeId.Trim() != "")
                {
                    sb.Append(string.Format(" and ct.type_id='{0}'", carInfo.TypeId));
                    sbCount.Append(string.Format(" and ct.type_id='{0}'", carInfo.TypeId));
                }
                if (ChildrenSel != "true")
                {
                    if (carInfo.Businessdivisionid != null && carInfo.Businessdivisionid.Trim() != "")
                    {
                        sb.Append(string.Format(" and di.businessdivisionid='{0}'", carInfo.Businessdivisionid));
                        sbCount.Append(string.Format(" and di.businessdivisionid='{0}'", carInfo.Businessdivisionid));
                    }
                    else
                    {
                        sb.Append(string.Format(" and di.businessdivisionid='{0}'", user.EnterId));
                        sbCount.Append(string.Format(" and di.businessdivisionid='{0}'", user.EnterId));
                    }
                }
                else
                {
                    DeptInfo di = null;
                    if (carInfo.Businessdivisionid != null || carInfo.Businessdivisionid.Trim() != "")
                    {
                        di = deptInfoBll.GetDeptInfo(carInfo.Businessdivisionid);
                    }
                    else
                    {
                        di = deptInfoBll.GetDeptInfo(user.EnterId);
                    }
                    sb.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
                    sbCount.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
                }
                sb.Append(" order by car_no ");
                DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString(), page, rows);
                DataSet dsCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", sbCount.ToString());
                if (dsCount != null && ds != null && dsCount.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return DtbConvertToJson(ds.Tables[0], Convert.ToInt32(dsCount.Tables[0].Rows[0][0].ToString()));
                }
                else
                {
                    return DtbConvertToJson(ds.Tables[0], 0);
                }
            }
            else
            {
                return "";
            }
        }

        [UserFilter]
        [Log(LogMessage = "车辆列表")]
        public string GetCarList(CarInfo carInfo, int rows, int page, string ChildrenSel, string TerNo)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            new LogMessage().Save("加载车辆列表;");

            string TmpDeptID = "";

            if (user != null)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sbCount = new StringBuilder();

                sb.Append("select ci.car_id,ci.car_no,ct.type_name,di.businessdivisionname,ti.ter_no,ti.ter_guid,ti.ter_typeid,rd.rawdataid,ci.car_adminname,ci.car_admincardid,ci.car_color,");
                sb.Append("ci.oweraddress,ci.owerphone,ci.installname,ci.installaddress,ci.installphone,ci.installplace,ci.installtime,ci.entryname,ci.entryphone,");
                sb.Append("ci.safeorder,ci.loanmoney,ci.loanyear,ci.carmodel,ci.enginenumber,ci.car_frame,ci.description,ci.contractnum");
                sbCount.Append("select count(*) ");

                sb.Append(" from Car_Info ci left join car_type ct on ci.type_id=ct.type_id left join terminal_info ti on ci.car_id=ti.car_id left join realtime_data rd on rd.ter_no=ti.ter_no join dept_info di on di.businessdivisionid=ti.dept_id ");
                sbCount.Append(" from Car_Info ci left join car_type ct on ci.type_id=ct.type_id  left join terminal_info ti on ci.car_id=ti.car_id left join realtime_data rd on rd.ter_no=ti.ter_no join dept_info di on di.businessdivisionid=ti.dept_id ");

                sb.Append(" where 1=1");
                sbCount.Append(" where 1=1");
                if (TerNo != "" && TerNo != null)
                {
                    sb.Append(string.Format(" and ti.ter_no like '%{0}%'", TerNo));
                    sbCount.Append(string.Format(" and ti.ter_no like '%{0}%'", TerNo));
                }
                if (carInfo.CarNo != null && carInfo.CarNo.Trim() != "")
                {
                    sb.Append(string.Format(" and ci.car_no like '%{0}%'", carInfo.CarNo));
                    sbCount.Append(string.Format(" and ci.car_no like '%{0}%'", carInfo.CarNo));
                }
                if (carInfo.CarAdminName != null && carInfo.CarAdminName != "")
                {
                    sb.Append(string.Format(" and ci.car_adminname like '%{0}%'", carInfo.CarAdminName));
                    sbCount.Append(string.Format(" and ci.car_adminname like '%{0}%'", carInfo.CarAdminName));
                }
                if (carInfo.TypeId != null && carInfo.TypeId.Trim() != "")
                {
                    sb.Append(string.Format(" and ct.type_id='{0}'", carInfo.TypeId));
                    sbCount.Append(string.Format(" and ct.type_id='{0}'", carInfo.TypeId));
                }

                if (ChildrenSel != "true")
                {
                    if (carInfo.Businessdivisionid != null && carInfo.Businessdivisionid.Trim() != "")
                    {
                        TmpDeptID = carInfo.Businessdivisionid;
                    }
                    else
                    {
                        TmpDeptID = user.EnterId;
                    }

                    sb.Append(string.Format(" and ti.dept_id='{0}'", TmpDeptID));
                    sbCount.Append(string.Format(" and ti.dept_id='{0}'", TmpDeptID));
                }
                else
                {
                    DeptInfo di = null;

                    if (string.IsNullOrEmpty(carInfo.Businessdivisionid) == false)
                    {
                        TmpDeptID = carInfo.Businessdivisionid;
                    }
                    else
                    {
                        TmpDeptID = user.EnterId;
                    }

                    di = deptInfoBll.GetDeptInfo(TmpDeptID);

                    sb.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
                    sbCount.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
                }

                sb.Append(" order by car_no ");

                DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString(), page, rows);

                DataSet dsCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", sbCount.ToString());

                if (dsCount != null && ds != null && dsCount.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return DtbConvertToJson(ds.Tables[0], Convert.ToInt32(dsCount.Tables[0].Rows[0][0].ToString()));
                }
                else
                {
                    return DtbConvertToJson(ds.Tables[0], 0);
                }
            }
            else
            {
                return "";
            }
        }

        [UserFilter]
        public int GetCarCount(CarInfo carInfo, string ChildrenSel, string TerNo)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("select count(*) ");
            sbCount.Append(" from Car_Info ci left join car_type ct on ci.type_id=ct.type_id left join terminal_info ti on ci.car_id=ti.car_id join dept_info di on di.businessdivisionid=ti.dept_id ");
            sbCount.Append(" where 1=1");

            if (TerNo != "" && TerNo != null)
            {
                sbCount.Append(string.Format(" and ti.ter_no like '%{0}%'", TerNo));
            }
            if (carInfo.CarNo != null && carInfo.CarNo.Trim() != "")
            {
                sbCount.Append(string.Format(" and ci.car_no like '%{0}%'", carInfo.CarNo));
            }
            if (carInfo.CarAdminName != null && carInfo.CarAdminName != "")
            {
                sbCount.Append(string.Format(" and ci.car_adminname like '%{0}%'", carInfo.CarAdminName));
            }
            if (carInfo.TypeId != null && carInfo.TypeId.Trim() != "")
            {
                sbCount.Append(string.Format(" and ct.type_id='{0}'", carInfo.TypeId));
            }

            if (ChildrenSel != "true")
            {
                if (carInfo.Businessdivisionid != null && carInfo.Businessdivisionid.Trim() != "")
                {
                    sbCount.Append(string.Format(" and ti.dept_id='{0}'", carInfo.Businessdivisionid));
                }
                else
                {
                    sbCount.Append(string.Format(" and ti.dept_id='{0}'", user.EnterId));
                }
            }
            else
            {
                DeptInfo di = null;
                if (string.IsNullOrEmpty(carInfo.Businessdivisionid) == false)
                {
                    di = deptInfoBll.GetDeptInfo(carInfo.Businessdivisionid);
                }
                else
                {
                    di = deptInfoBll.GetDeptInfo(user.EnterId);
                }
                sbCount.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
            }

            DataSet dsCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", sbCount.ToString());
            if (dsCount != null)
            {
                return Convert.ToInt32(dsCount.Tables[0].Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 编辑窗口
        /// </summary>
        /// <returns></returns>
        [UserFilter]
        public ActionResult EditCarInfo(CarInfo carInfo,string TerNo, string flag)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            StringBuilder sb = new StringBuilder();
            sb.Append("select ci.car_id,ci.car_no,ci.type_id,ct.type_name,di.businessdivisionid,di.businessdivisionname,ci.CAR_ADMINNAME,ci.CAR_ADMINCARDID,ci.CAR_COLOR,");
            sb.Append("ci.oweraddress,ci.owerphone,ci.installname,ci.installaddress,ci.installphone,ci.installplace,ci.installtime,ci.entryname,ci.entryphone,");
            sb.Append("ci.safeorder,ci.loanmoney,ci.loanyear,ci.carmodel,ci.enginenumber,ci.car_frame,ci.description,ci.contractnum");
            sb.Append(" from Car_Info ci left join car_type ct on ci.type_id=ct.type_id join dept_info di on ci.businessdivisionid=di.businessdivisionid left join");
            sb.Append(" (SELECT CAR_ID");
            sb.Append(" FROM field_values GROUP BY CAR_ID) tt on ci.car_id=tt.car_id where 1=1");
            if (carInfo.CarId.Trim() != "")
            {
                sb.Append(string.Format(" and ci.car_id='{0}'", carInfo.CarId));
            }
            DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString());
            ViewBag.Terno = TerNo;
            ViewBag.Deptid = ds.Tables[0].Rows[0]["BUSINESSDIVISIONID"].ToString();
            ViewBag.Typeid = ds.Tables[0].Rows[0]["TYPE_ID"].ToString();
            ViewBag.flag = flag;
            return View(ds);
        }

        /// <summary>
        /// 修改车辆
        /// </summary>
        /// <param name="carInfo"></param>
        /// <param name="Hid_CarInfoEdit"></param>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        [Log(LogMessage = "车辆修改")]
        [HttpPost]
        [UserFilter]
        public ActionResult EditCarInfoForm(CarInfo carInfo, string Hid_TerNo,string Hid_OldCarno, string TypeName, string flag)
        {
            new LogMessage().Save("CarNo:" + carInfo.CarNo + ";");
            UserInfo user = (UserInfo)Session["LoginUser"];
            if (carInfo.Businessdivisionid != null || carInfo.Businessdivisionid.Trim() != "")
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(carInfo.Businessdivisionid);
                carInfo.CarDeptcode = di.Businessdivisioncode;
            }
            carInfo.Description = carInfo.Description.Trim();

            string carhbinfo = "";
            if (carInfo.CarColor != "" || TypeName != "")
            {
                string cartypename = "";
                if (TypeName.Split('-').Length == 1)
                {
                    cartypename = TypeName.Split('-')[0];
                }
                else
                {
                    cartypename = TypeName.Split('-')[2] == null ? "" : TypeName.Split('-')[2];
                }
                carhbinfo = cartypename + "|" + carInfo.CarColor + "|||||||||||||||||||||||||||||";
            }

            RealtimeData rdinfo = realtimedatabll.GetRealtimeData(Hid_TerNo);
            if (rdinfo.Rawdataid != null && rdinfo.Rawdataid != "")
            {
                string NewCarno = carInfo.CarNo;
                carInfo.CarNo = Hid_OldCarno;
                string kku = carInfoBll.UpdateCarType(carInfo, TypeName);
                ViewBag.Result = kku;

                //修改联动车辆发送的指令
                Transfers.ClintSendCommData(1107, "551", "", Hid_OldCarno, "", "", "", "", "", "", "", carInfo.CarDeptcode, NewCarno, carhbinfo, carInfo.CarAdminName, carInfo.CarFrame, "", "", user.UserName);
                System.Threading.Thread.Sleep(2000);
            }
            else
            {
                ArrayList arr = new ArrayList();
                arr.Add(carInfo);
                string kku = carInfoBll.UpdateCarInfo(arr, TypeName);
                ViewBag.Result = kku;
                //修改普通车辆发送的指令
                TerminalInfoBLL terbll = new TerminalInfoBLL();
                IList<TerminalInfo> terinfolist = terbll.GetTerminalInfoByCarId(carInfo.CarId);
                foreach (TerminalInfo terinfo in terinfolist)
                {
                    if (terinfo != null && terinfo.TerNo != "")
                    {
                        Transfers.ClintSendCommData(1107, "50", "", terinfo.TerNo, "", "", "", "", "", "", "", carInfo.CarDeptcode, carInfo.CarNo, carhbinfo, carInfo.CarAdminName, carInfo.CarFrame, "", "", user.UserName);
                    }
                }
            }
            ViewBag.flag = flag;
            return View();
        }

        [UserFilter]
        [Log(LogMessage = "车辆删除")]
        public string DelCarInfo(string carId, string terGuid)
        {
            new LogMessage().Save("CarNo:" + carId + ";");
            UserInfo user = (UserInfo)Session["LoginUser"];
            TerminalInfo terinfo = terbll.GetTerminalInfo(terGuid);
            string result = "";
            int cnt = terbll.GetTerminalCountByCarId(carId);
            if (cnt > 1)
            {
                result = carInfoBll.Delete(terGuid,true);
            }
            else
            {
                result = carInfoBll.Delete(carId,false);
            }

            //解绑接口
            if (terGuid != "")
            {
                Transfers.ClintSendCommData(1107, "501", "", terinfo.TerNo, "", "", "", "", "", "", "", "", "", "", "", "", "", "", user.UserName);
            }
            return result;
        }

        [UserFilter]
        //无线设备的解绑
        public string DelWxLdTer(string carId, string terGuid,string DelStyle)
        {
            UserInfo user = (UserInfo)Session["LoginUser"];
            TerminalInfo terinfo = terbll.GetTerminalInfo(terGuid);
            CarInfo ci = carInfoBll.GetCarInfoByCarId(carId);
            if (DelStyle == "1")    //全部解绑
            {
                Transfers.ClintSendCommData(1107, "63", "", "", "", "", "", "", "", "", "", ci.CarNo, "", "", "", "", "", "", user.UserName);
            }
            else                    //单台解绑
            {
                Transfers.ClintSendCommData(1107, "631", "", terinfo.TerNo, "", "", "", "", "", "", "", ci.CarNo, "", "", "", "", "", "", user.UserName);
            }
            System.Threading.Thread.Sleep(2000);
            return "true";
        }

        public ActionResult ImpCarInfo()
        {
            return View();
        }

        [UserFilter]
        [Log(LogMessage = "车辆导入")]
        public ActionResult UpLoadCarInfoForm(string DeptId, HttpPostedFileBase file)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (file != null && file.ContentLength > 0 && DeptId != null && DeptId.Trim() != "")
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Files"), System.Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

                file.SaveAs(filePath);

                new LogMessage().Save("文件:" + filePath + ";");

                string val = "";
                string msg = "";

                ExcelUpLoad eu = new ExcelUpLoad();

                List<UpLoadTerBind> lut = eu.ReadCarInfo_ZNX(filePath, ref msg);
                if (lut != null && lut.Count > 0)
                {
                    val = carInfoBll.insertCarInfoMul(DeptId, lut);
                    if (val.LastIndexOf(",") > 0)
                    {
                        val = val.Substring(0, val.Length - 1);
                    }
                    ViewBag.ReturnVal = val;

                    //绑车接口
                    for (int i = 0; i < lut.Count; i++)
                    {
                        string CarDeptcode = "";
                        string TypeName = "";
                        if (lut[i].BusinessDivisionId != "" && lut[i].BusinessDivisionId != null)
                        {
                            DeptInfo di = deptInfoBll.GetDeptInfo(lut[i].BusinessDivisionId);
                            CarDeptcode = di.Businessdivisioncode;
                        }
                        if (lut[i].CarType != "" && lut[i].CarType != null)
                        {
                            CarTypeBLL cartypebll = new CarTypeBLL();
                            CarType ct = cartypebll.GetCarType(lut[i].CarType);
                            if (ct != null)
                            {
                                TypeName = ct.TypeName;
                            }
                        }

                        string carinfostr = TypeName + "|" + lut[i].CarColor + "|||||||||||||||||||||||||||||";

                        Transfers.ClintSendCommData(1107, "50", "", lut[i].TerNo, "", "", "", "", "", "", "", CarDeptcode, lut[i].CarNo, carinfostr, lut[i].CarAdminName, lut[i].CarFrame, "", "", user.UserName);
                    }

                }
                else if (msg.Trim() != "")
                {
                    if (msg.LastIndexOf(",") > 0)
                    {
                        msg = msg.Substring(0, msg.Length - 1);
                    }
                    ViewBag.ReturnVal = msg;
                }
                else
                {
                    ViewBag.ReturnVal = "false";
                }
            }
            else if (DeptId == null || DeptId.Trim() == "")
            {
                ViewBag.ReturnVal = "请选择导入车辆的所属企业！";
            }
            else if (file == null || file.ContentLength <= 0)
            {
                ViewBag.ReturnVal = "请选择要导入的文件！";
            }
            //刷新车辆
            Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "2", "", "", "", "", "", "");
            return View();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportCarInfo(string CarNo, string DeptId, string TypeId, string ChildrenSel, string Businessdivisionid)
        {
            ViewBag.CarNo = CarNo;
            ViewBag.DeptId = DeptId;
            ViewBag.TypeId = TypeId;
            ViewBag.ChildrenSel = ChildrenSel;
            ViewBag.Businessdivisionid = Businessdivisionid;
            return View();
        }

        [Log(LogMessage = "车辆导出")]
        [HttpPost]
        [UserFilter]
        public string ExportCarInfo(CarInfo tiv, string ShowFieldString, string CarNo, string DeptId, string TypeId, string ChildrenSel, string Businessdivisionid)
        {
            string[] Filed = null;
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            DataSet ds = null;
            DataSet dsCount = null;
            DataTable dt = null;

            #region
            tiv.StartData = 0;
            tiv.EndData = 0;
            if (!string.IsNullOrEmpty(ShowFieldString))
            {
                ShowFieldString = ShowFieldString.Substring(0, ShowFieldString.Length - 1);
                if (ShowFieldString.IndexOf("|") != -1)
                {
                    Filed = ShowFieldString.Split(new char[] { '|' });
                }
            }
            else
            {
                return "请选择导出的列,再进行导出！";
            }
            tiv.CarNo = CarNo;
            tiv.DeptId = DeptId;
            tiv.TypeId = TypeId;
            tiv.Businessdivisionid = Businessdivisionid;
            Hashtable ht = new Hashtable();
            ht.Add("CarNo", tiv.CarNo);
            ht.Add("TypeId", tiv.TypeId);
            ht.Add("Businessdivisionid", DeptId);
            ht.Add("DeptId", tiv.Businessdivisionid);
            ht.Add("StartData", tiv.StartData);
            ht.Add("EndData", tiv.EndData);
            if (tiv.Businessdivisionid == null || tiv.Businessdivisionid.Trim() == "")
            {
                ht["DeptId"] = user.EnterId;
            }
            if (ChildrenSel == "true")
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(tiv.Businessdivisionid);
                if (di != null)
                {
                    ht["Businessdivisioncode"] = di.Businessdivisioncode;
                    ht["DeptId"] = "";
                }
            }
            #endregion

            #region
            if (user != null)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sbCount = new StringBuilder();
                sb.Append("select ci.car_no as 车牌号,ct.type_name as 车辆类型,ci.car_color as 车辆颜色,di.businessdivisionname as 所属企业,ci.car_adminname as 车主姓名,ci.car_admincardid as 身份证号,ti.ter_no as 终端编号,");
                sb.Append("ci.oweraddress as 车主地址,ci.owerphone as 车主电话,ci.installname as 安装人,ci.installaddress as 安装位置,ci.installphone as 安装人联系电话,ci.installplace as 安装地点,ci.car_frame as 车架号,");
                sb.Append("ci.installtime as 安装时间,ci.entryname as 录入人,ci.entryphone as 录入人电话,ci.contractnum as 合同号,ci.safeorder as 保险单号,ci.loanmoney as 融资金额,ci.loanyear as 贷款年限,ci.carmodel as 车辆型号,ci.enginenumber as 发动机号,ci.car_frame as 车架号,ci.description as 备注");
                sbCount.Append("select count(*) ");
                sb.Append(" from Car_Info ci left join car_type ct on ci.type_id=ct.type_id left join terminal_info ti on ci.car_id=ti.car_id join dept_info di on ti.dept_id=di.businessdivisionid where 1=1");
                sbCount.Append(" from Car_Info ci left join car_type ct on ci.type_id=ct.type_id  left join terminal_info ti on ci.car_id=ti.car_id join dept_info di on ti.dept_id=di.businessdivisionid where 1=1");
                if (tiv.CarNo != null && tiv.CarNo.Trim() != "" && tiv.CarNo != "undefined")
                {
                    sb.Append(string.Format(" and ci.car_no like '%{0}%'", tiv.CarNo));
                    sbCount.Append(string.Format(" and ci.car_no like '%{0}%'", tiv.CarNo));
                }
                if (tiv.TypeId != null && tiv.TypeId.Trim() != "")
                {
                    sb.Append(string.Format(" and ct.type_id='{0}'", tiv.TypeId));
                    sbCount.Append(string.Format(" and ct.type_id='{0}'", tiv.TypeId));
                }
                if (ChildrenSel != "true")
                {
                    if (tiv.Businessdivisionid != null && tiv.Businessdivisionid.Trim() != "")
                    {
                        sb.Append(string.Format(" and di.businessdivisionid='{0}'", tiv.Businessdivisionid));
                        sbCount.Append(string.Format(" and di.businessdivisionid='{0}'", tiv.Businessdivisionid));
                    }
                    else
                    {
                        sb.Append(string.Format(" and di.businessdivisionid='{0}'", user.EnterId));
                        sbCount.Append(string.Format(" and di.businessdivisionid='{0}'", user.EnterId));
                    }
                }
                else
                {
                    DeptInfo di = null;
                    if (tiv.Businessdivisionid != null && tiv.Businessdivisionid.Trim() != "")
                    {
                        di = deptInfoBll.GetDeptInfo(tiv.Businessdivisionid);
                    }
                    else
                    {
                        di = deptInfoBll.GetDeptInfo(user.EnterId);
                    }
                    sb.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
                    sbCount.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
                }
                sb.Append(" order by car_no ");
                ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString());
                dsCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", sbCount.ToString());
            }
            else
            {
                return "";
            }
            #endregion

            if (dsCount != null && ds != null && dsCount.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                IList<CarInfo> ltiv = new List<CarInfo>();
                List<string> filedlist = new List<string>();
                ExcelUpLoad eu = new ExcelUpLoad();
                MemoryStream ms = eu.CreateCarInfoExcel(dt, Filed);
                string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                // 输出Excel
                using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/车辆信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
                if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/车辆信息") + xlsName + ".xlsx"))
                {
                    string ppphhh = "../../Files/车辆信息" + xlsName + ".xlsx";

                    new LogMessage().Save("文件:" + ppphhh + ";");

                    return ppphhh;
                }
                else
                {
                    new LogMessage().Save("生成文件出错，请重新导出！");

                    return "生成文件出错，请重新导出！";
                }
            }
            else
            {
                new LogMessage().Save("生成文件出错，请重新导出！");

                return "生成文件出错，请重新导出！";
            }
        }
    }
}
