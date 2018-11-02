using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GBLL.Car;
using GModel.Car;
using GBLL.Location;
using GModel.Location;
using GModel.InterFace;
using GBLL.InterFace;
using GBLL.Basic;
using GModel.Basic;
using GBLL.RoleRight;
using System.Collections;
using GModel.RoleRight;

using System.Configuration;
using ConsoleApplicationGPS.Models;
using RedisDBInterface;
using SuperGPS.App_Start;
using SuperGPS.Models;
using GCommon;
using System.IO;
using System.Data;
using GBLL;

namespace SuperGPS.Controllers
{
    public class CarMonitorController : BaseController
    {
        MonitorViewBLL monitorViewBll = new MonitorViewBLL();
        RealtimeViewBLL realTimeViewBll = new RealtimeViewBLL();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();
        RealtimeDataBLL realtimeDataBll = new RealtimeDataBLL();
        MenuInfoBLL menuInfoBll = new MenuInfoBLL();
        HistoricalDataBLL hdb = new HistoricalDataBLL();
        CarReportBLL carReportBll = new CarReportBLL();
        RawDataBLL rawDataBll = new RawDataBLL();
        OldterPostbacktimesBLL ttt = new OldterPostbacktimesBLL();
        SendinfoLastBLL silb = new SendinfoLastBLL();
        SendinfoBLL sif = new SendinfoBLL();
        SendinfoyxLastBLL sixl = new SendinfoyxLastBLL();

        [UserFilter]
        public ActionResult MonitorIndex()
        { 
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                ViewBag.Deptid = user.EnterId;
                ViewBag.Deptcode = user.UserDeptcode;
                if (user.UserDeptcode != null)
                {
                    IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];

                    ViewBag.CKXQ = "false";
                    ViewBag.CKGJ = "false";
                    ViewBag.FSML = "false";
                    ViewBag.CROLE = "";

                    //终端监控
                    ViewBag.ZDJK = "false";

                    for (int i = 0; i < imi.Count; i++)
                    {
                        switch (imi[i].MenuName)
                        {
                            case "发送命令":
                                ViewBag.FSML = "true";
                                break;
                            case "查看详情":
                                ViewBag.CKXQ = "true";
                                break;
                            case "查看轨迹":
                                ViewBag.CKGJ = "true";
                                break;
                            case "终端监控":
                                ViewBag.ZDJK = "true";
                                break;
                        }
                    }
                }
                else
                {
                    ViewBag.CKXQ = "true";
                    ViewBag.CKGJ = "true";
                    ViewBag.FSML = "true";
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        [UserFilter]
        public ActionResult MonitorIndex_511()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            new LogMessage().Save("加载系统首页;");

            if (user != null)
            {
                ViewBag.Deptid = user.EnterId;
                ViewBag.Deptcode = user.UserDeptcode;
                if (user.UserDeptcode != null)
                {
                    IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];

                    ViewBag.CKXQ = "false";
                    ViewBag.CKGJ = "false";
                    ViewBag.FSML = "false";
                    ViewBag.DZWL = "true";
                    ViewBag.CROLE = "";

                    //终端监控
                    ViewBag.ZDJK = "false";

                    //超里程车辆
                    ViewBag.Vis_Carlist_clc = carReportBll.GetVis_Carlist_clc(user.UserDeptcode);

                    for (int i = 0; i < imi.Count; i++)
                    {
                        switch (imi[i].MenuName)
                        {
                            case "发送命令":
                                ViewBag.FSML = "true";
                                break;
                            case "查看详情":
                                ViewBag.CKXQ = "true";
                                break;
                            case "查看轨迹":
                                ViewBag.CKGJ = "true";
                                break;
                            case "终端监控":
                                ViewBag.ZDJK = "true";
                                break;
                        }
                    }
                }
                else
                {
                    ViewBag.CKXQ = "true";
                    ViewBag.CKGJ = "true";
                    ViewBag.FSML = "false";
                    ViewBag.DZWL = "false";
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        [UserFilter]
        public ActionResult BDMonitorIndex()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                ViewBag.Deptid = user.EnterId;
                ViewBag.Deptcode = user.UserDeptcode;
                if (user.UserDeptcode != null)
                {
                    IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];

                    ViewBag.CKXQ = "false";
                    ViewBag.CKGJ = "false";
                    ViewBag.FSML = "false";
                    ViewBag.CROLE = "";
                    //终端监控
                    ViewBag.ZDJK = "false";
                    for (int i = 0; i < imi.Count; i++)
                    {
                        switch (imi[i].MenuName)
                        {
                            case "发送命令":
                                ViewBag.FSML = "true";
                                break;
                            case "查看详情":
                                ViewBag.CKXQ = "true";
                                break;
                            case "查看轨迹":
                                ViewBag.CKGJ = "true";
                                break;
                            case "终端监控":
                                ViewBag.ZDJK = "true";
                                break;
                        }
                    }
                }
                else
                {
                    ViewBag.CKXQ = "true";
                    ViewBag.CKGJ = "true";
                    ViewBag.FSML = "true";               
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        [UserFilter]
        public ActionResult BDMonitorIndex_511()
        {
            UserInfo user = new UserInfo();

            user = (UserInfo)Session["LoginUser"];

            new LogMessage().Save("加载系统首页;");

            if (user != null)
            {
                ViewBag.Deptid = user.EnterId;
                ViewBag.Deptcode = user.UserDeptcode;
                if (user.UserDeptcode != null)
                {
                    IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];

                    ViewBag.CKXQ = "false";
                    ViewBag.CKGJ = "false";
                    ViewBag.FSML = "false";
                    ViewBag.CROLE = "";

                    //终端监控
                    ViewBag.ZDJK = "false";

                    //超里程车辆(是否显示)
                    //ViewBag.Vis_Carlist_clc = carReportBll.GetVis_Carlist_clc(user.UserDeptcode); 
                    IList<CarReport_LCTJView> irt = carReportBll.GetVis_Carlist_ViewPage(user.UserDeptcode);
                    if (irt.Count > 0)
                    {
                        ViewBag.Vis_Carlist_clc = true;
                    }
                    else
                    {
                        ViewBag.Vis_Carlist_clc = false;
                    }

                    for (int i = 0; i < imi.Count; i++)
                    {
                        switch (imi[i].MenuName)
                        {
                            case "发送命令":
                                ViewBag.FSML = "true";
                                break;
                            case "查看详情":
                                ViewBag.CKXQ = "true";
                                break;
                            case "查看轨迹":
                                ViewBag.CKGJ = "true";
                                break;
                            case "终端监控":
                                ViewBag.ZDJK = "true";
                                break;
                        }
                    }
                }
                else
                {
                    ViewBag.CKXQ = "true";
                    ViewBag.CKGJ = "true";
                    ViewBag.FSML = "true";                   
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        [UserFilter]
        public ActionResult VersionInfo()
        {
            return View();
        }

        public string GetCarList()
        {
            MonitorView mv = new MonitorView();
            IList<MonitorView> imv = monitorViewBll.GetMonitorViewPage(mv);
            int k = monitorViewBll.GetMonitorViewCount(mv);
            string uu = ConvertToJson(imv, k);
            return uu;
        }

        #region 超里程车辆统计列表
        [UserFilter]
        public string ShowVisCarListInfo()
        {
            UserInfo user = new UserInfo();

            user = (UserInfo)Session["LoginUser"];

            IList<CarReport_LCTJView> irt = carReportBll.GetVis_Carlist_ViewPage(user.UserDeptcode);
            
            return ConvertToJson(irt);
        }

        [UserFilter]
        public string ShowVisCarListInfo_ToExcel()
        {
            UserInfo user = new UserInfo();

            user = (UserInfo)Session["LoginUser"];

            IList<CarReport_LCTJView> irt = carReportBll.GetVis_Carlist_ViewPage(user.UserDeptcode);
            
            ExcelUpLoad eu = new ExcelUpLoad();

            MemoryStream ms = eu.CreateTerExcel(irt);

            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // 输出Excel
            using (FileStream fs = new FileStream(
                                            HttpContext.Server.MapPath("../Files/超里程车辆信息") + xlsName + ".xlsx", 
                                            FileMode.Create, 
                                            FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }

            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/超里程车辆信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/超里程车辆信息" + xlsName + ".xlsx";

                new LogMessage().Save("文件:" + ppphhh + "");

                return ppphhh;
            }
            else
            {
                new LogMessage().Save("生成文件出错，请重新导出！");

                return "生成文件出错，请重新导出！";
            }
        }

        #endregion

        [UserFilter]
        public string GetList(Selectcarmonitor rtv, string DeptCode,string SelType,int RowNumber)
        {
            UserInfo user = new UserInfo();

            user = (UserInfo)Session["LoginUser"];

            DeptInfo dif = new DeptInfo();
            Hashtable ht = new Hashtable();

            ht.Add("TerStatus", rtv.TerStatus);
            ht.Add("TerNo", rtv.TerNo == null ? "" : rtv.TerNo.Trim().ToUpper());
            //ht.Add("CarNo", rtv.CarNo == null ? "" : rtv.CarNo.Trim());
            //ht.Add("CarAdminName", rtv.CarAdminName == null ? "" : rtv.CarAdminName.Trim());
            ht.Add("StartData", rtv.StartData);
            ht.Add("EndData", 0); // RowNumber * 20 + 1
            ht.Add("ReplydataCode", rtv.ReplydataCode==null?"":rtv.ReplydataCode.Trim());
            ht.Add("Northorsouth", rtv.Northorsouth);
            if (rtv.Businessdivisionid!=null && rtv.Businessdivisionid.Trim() == "")
            {
                if (user.EnterId != null)
                {
                    ht.Add("Businessdivisionid", user.EnterId);
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }
            else {
                ht.Add("Businessdivisionid", rtv.Businessdivisionid);
            }

            if (DeptCode == "true")
            {
                if (user.EnterId != null)
                {
                    dif = deptInfoBll.GetDeptInfo(ht["Businessdivisionid"].ToString());
                    ht.Add("Businessdivisioncode", dif.Businessdivisioncode);
                    ht["Businessdivisionid"] = "";
                }
            }

            if (SelType == "Ter" || SelType == "Dept")
            {
                ht.Add("SelType", "true");
            }
            else
            {
                ht.Add("SelType", "");
            }

            IList<Selectcarmonitor> irt = realtimeDataBll.SelectCarMonitor(ht);
            string uu = ConvertToJson(irt);

            return uu;
        }

        public string GetTerList(int st, int pageSize)
        {
            RedisDB OBj = new RedisDB();
            OBj.SetRedisPos(ConfigurationManager.ConnectionStrings["IpportString"].ConnectionString);
            bool a = OBj.CreateManager();
            bool b = a;
            try
            {
                int count = 0;
                TerCount tc = new TerCount();
                tc.list = new List<DataList>();
                tc.list = OBj.List_Table_GetDataByPage<DataList>(st, pageSize, ref count);
                tc.TCount = count.ToString();
                return ConvertToJson(tc);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string GetTerSingleByTerNo(string TerNo)
        {
            RedisDB OBj = new RedisDB();
            OBj.SetRedisPos(ConfigurationManager.ConnectionStrings["IpportString"].ConnectionString);
            bool a = OBj.CreateManager();
            bool b = a;
            try
            {
                Func<DataList, bool> wheres = x => x.realtime_dataObj.TerNo.Contains(TerNo);
                var list2 = OBj.List_Table_GetData<DataList>(wheres);
                List<DataList> ReturnList = new List<DataList>();
                foreach (DataList curData in list2)
                {
                    DataList dd = new DataList();
                    dd = curData;
                    ReturnList.Add(curData);
                }
                string uu = ConvertToJsonNew(ReturnList);
                return uu;
            }
            catch (Exception e)
            {
                return "[]";
            }
        }

        public string test()
        {

            string aaa = sixl.GetSendPageByTerNoAndCommand("aaa", "kkk", 0, 0);

            //DataSet ds = cg.GetColligateQuery("ProteanQuery", "SELECT T.SWIFTNUMBER FROM SENDINFOYX_LAST T WHERE T.DEVICE_ID={0}", "aaa");
            //RealtimeData rd = new RealtimeData();
            //rd.Id = "aaa";
            //rd.Height = "23";
            //rd.Ifblinddata = "mmnb";
            //rd.Rtime = DateTime.Now;
            //string kk = realtimeDataBll.InsertData(rd);
            //RealtimeData hd = new RealtimeData();
            //hd.Id = "mm";
            //hd.Ifblinddata = "kdjfs";
            //hd.Lat = "aaa";
            //hd.Rtime = DateTime.Now;
            //string mm = hdb.Insert(hd);

            ////SendinfoLast sib = silb.GetCmd("test1111");
            //string msf="";
            //int k= ttt.GetPostbackTimes("test1111", ref msf);
            //int b = k + 1;

            //Sendinfo ssss = new Sendinfo();
            //ssss.Id = 12345;
            //ssss.DeviceId = "test";
            //sif.InsertData(ssss, 22);

            //GBLL.Location.RealtimeDataBLL rtd = new GBLL.Location.RealtimeDataBLL();
            //RealtimeData rd = new RealtimeData();
            //rd.TerNo = "M1051234";
            //rd.Rtime = DateTime.Now;
            //string result = rtd.InsertData(rd);
            //return result;

            return "true";

        }

        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="rtv"></param>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        [UserFilter]
        [Log(LogMessage = "导出车辆信息")]
        public string DownLoadExcel(Selectcarmonitor rtv, string DeptCode, string SelType,string ZLflag)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            DeptInfo dif = new DeptInfo();
            Hashtable ht = new Hashtable();
            ht.Add("TerStatus", rtv.TerStatus);
            ht.Add("TerNo", rtv.TerNo);
            ht.Add("CarNo", rtv.CarNo);
            ht.Add("StartData", rtv.StartData);
            ht.Add("EndData", rtv.EndData);
            ht.Add("ReplydataCode", rtv.ReplydataCode);
            ht.Add("Northorsouth", rtv.Northorsouth);
            if (rtv.Businessdivisionid==null || rtv.Businessdivisionid.Trim() == "")
            {
                if (user.EnterId != null)
                {
                    ht.Add("Businessdivisionid", user.EnterId);
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }
            else
            {
                ht.Add("Businessdivisionid", rtv.Businessdivisionid);
            }
            if (DeptCode == "true")
            {
                if (user.EnterId != null)
                {
                    dif = deptInfoBll.GetDeptInfo(ht["Businessdivisionid"].ToString());
                    ht.Add("Businessdivisioncode", dif.Businessdivisioncode);
                    ht["Businessdivisionid"] = "";
                }
            }
            if (rtv.CarWorkVMP > 0)
            {
                ht.Add("SelType", "");
            }
            else
            {
                if (SelType == "Ter" || SelType == "Dept")
                {
                    ht.Add("SelType", "true");
                }
                else
                {
                    ht.Add("SelType", "");
                }
            }

            IList<Selectcarmonitor> irt = realtimeDataBll.SelectCarMonitor(ht);
            IList<Selectcarmonitor> lxirt = new List<Selectcarmonitor>();
            IList<Selectcarmonitor> zxirt = new List<Selectcarmonitor>();
            for(int i=0;i<irt.Count;i++)
            {
                if (irt[i].StateName == "lx")
                {
                    lxirt.Add(irt[i]);
                }
                else if (irt[i].StateName == "zx")
                {
                    zxirt.Add(irt[i]);
                }
            }

            ExcelUpLoad eu = new ExcelUpLoad();
            MemoryStream ms = new MemoryStream();
            if (ZLflag == "ZX")
            {
                ms = eu.CreateExcel(zxirt);
            }
            else if (ZLflag == "LX")
            {
                ms = eu.CreateExcel(lxirt);
            }
            else
            {
                ms = eu.CreateExcel(irt);
            }
            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/车辆位置信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/车辆位置信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/车辆位置信息" + xlsName + ".xlsx";

                new LogMessage().Save("文件:" + ppphhh + "");

                return ppphhh;
            }
            else
            {
                new LogMessage().Save("生成文件出错，请重新导出！");

                return "生成文件出错，请重新导出！";
            }
        }
        
        [UserFilter]
        public int GetCXWaringCount(Selectcarmonitor sm) 
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
           
            if (sm.Businessdivisionid==null || sm.Businessdivisionid.Trim() == "")
            {
                sm.Businessdivisionid = user.EnterId;
            }
            if (sm.Businessdivisioncode == "true")
            {
                DeptInfo dif = new DeptInfo();
                dif = deptInfoBll.GetDeptInfo(sm.Businessdivisionid);
                sm.Businessdivisioncode = dif.Businessdivisioncode;
                sm.Businessdivisionid = "";
            }
            else {
                sm.Businessdivisioncode = "";
            }
           int c = realtimeDataBll.GetCXWaringCount(sm);
            return c;
        }

        [UserFilter]
        public int GetDDWaringCount(Selectcarmonitor sm)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            if (sm.Businessdivisionid == null || sm.Businessdivisionid.Trim() == "")
            {
                sm.Businessdivisionid = user.EnterId;
            }
            if (sm.Businessdivisioncode == "true")
            {
                DeptInfo dif = new DeptInfo();
                dif = deptInfoBll.GetDeptInfo(sm.Businessdivisionid);
                sm.Businessdivisioncode = dif.Businessdivisioncode;
                sm.Businessdivisionid = "";
            }
            else
            {
                sm.Businessdivisioncode = "";
            }
            int c = realtimeDataBll.GetDDWaringCount(sm);
            return c;
        }

        [UserFilter]
        public int GetQYWaringCount(Selectcarmonitor sm)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            if (sm.Businessdivisionid == null || sm.Businessdivisionid.Trim() == "")
            {
                sm.Businessdivisionid = user.EnterId;
            }
            if (sm.Businessdivisioncode == "true")
            {
                DeptInfo dif = new DeptInfo();
                dif = deptInfoBll.GetDeptInfo(sm.Businessdivisionid);
                sm.Businessdivisioncode = dif.Businessdivisioncode;
                sm.Businessdivisionid = "";
            }
            else
            {
                sm.Businessdivisioncode = "";
            }
            int c = realtimeDataBll.GetQYWaringCount(sm);
            return c;
        }
    }
}