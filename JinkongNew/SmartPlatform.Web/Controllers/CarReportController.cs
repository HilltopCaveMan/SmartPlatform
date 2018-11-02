using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GBLL.Car;
using GModel;
using GModel.Car;
using GModel.Basic;

using GBLL;
using GBLL.Basic;

using System.Text;
using System.Collections;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using GModel.RoleRight;
using SuperGPS.App_Start;
using GCommon;
using GBLL.Location;

namespace SuperGPS.Controllers
{
    public class CarReportController : BaseController
    {

        CarReportBLL ReportBll = new CarReportBLL();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();
        // GET: /CarReport/
        [Log(LogMessage = "报表统计")]
        [UserFilter]
        public ActionResult Index(string flag)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            new LogMessage().Save("进入报表统计。");

            if (user != null)
            {
                ViewBag.Deptid = user.EnterId;
                ViewBag.Deptcode = user.UserDeptcode;
            }
            if (flag != null)
            {
                ViewBag.Flag = flag;
            }
            return View();
        }

        [UserFilter]
        public ActionResult GetReportMenuTree()
        {
            UserInfo user = new UserInfo();

            user = (UserInfo)Session["LoginUser"];

            List<TreeMode> mTreeNode = new List<TreeMode>();

            if (user != null)
            {
                List<TreeMode> mTreeNodeNew = ReportBll.GetReportMenuTree();

                return Json(mTreeNodeNew, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        #region 报表统计
        public ActionResult Index_TJBB()
        {
            return View();
        }

        public string GetTerCharData(string ObjType, string ObjValue, string ProId, string StateId, string DeptId, string ChildrenSel)
        {
            List<ChartData> cdlist = new List<ChartData>();

            if (StateId == "0")
            {
                for (int i = 1; i < 8; i++)
                {
                    ChartData cd = this.GetCharDataByStateId(ObjType, ObjValue, ProId, i.ToString(), DeptId, ChildrenSel);
                    cdlist.Add(cd);
                }
            }
            else
            {
                for (int i = 1; i < 8; i++)
                {
                    if (i.ToString() == StateId)
                    {
                        ChartData cd = this.GetCharDataByStateId(ObjType, ObjValue, ProId, i.ToString(), DeptId, ChildrenSel);
                        cdlist.Add(cd);
                    }
                    else
                    {
                        string name = "";
                        switch (i)
                        {
                            case 1:
                                name = "在线";
                                break;
                            case 2:
                                name = "离线";
                                break;
                            case 3:
                                name = "失联";
                                break;
                            case 4:
                                name = "到期";
                                break;
                            case 5:
                                name = "库存";
                                break;
                            case 6:
                                name = "休眠";
                                break;
                            case 7:
                                name = "其他";
                                break;
                        }
                        ChartData cd = new ChartData(i.ToString(),"0",name);
                        cdlist.Add(cd);
                    }
                }
            }
            return ConvertToJson(cdlist);
        }

        public class ChartData 
        {
            public string id { set; get; }
            public string value { set; get; }
            public string name { set; get; }

            public ChartData()
            { }

            public ChartData(string id, string value, string name)
            {
                this.id = id;
                this.value = value;
                this.name = name;
            }
        }

        [UserFilter]
        public ChartData GetCharDataByStateId(string ObjType, string ObjValue, string ProId, string StateId, string DeptId, string ChildrenSel)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            Hashtable ht = new Hashtable();
            ht.Add("EndData", 0);
            if (DeptId != null && DeptId.Trim() != "")
            {
                ht.Add("DeptId", DeptId);
            }
            else
            {
                if (user.EnterId != null)
                {
                    ht.Add("DeptId", user.EnterId);
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }
            if (ChildrenSel != null && ChildrenSel.Trim() == "true")
            {
                if (user.EnterId != null)
                {
                    DeptInfo di = deptInfoBll.GetDeptInfo(ht["DeptId"].ToString());
                    ht.Add("Businessdivisioncode", di.Businessdivisioncode);
                    ht["DeptId"] = "";
                }
            }
            else
            {
                ht.Add("Businessdivisioncode", "");
            }
            if (ObjType == "1" && ObjValue != "" && ObjValue != null)
            {
                ht.Add("TerNo", ObjValue.Trim());
            }
            else
            {
                ht.Add("TerNo", "");
            }

            if (ObjType == "2" && ObjValue != "" && ObjValue != null)
            {
                ht.Add("CarNo", ObjValue.Trim());
            }
            else
            {
                ht.Add("CarNo", "");
            }

            if (ProId != null && ProId.Trim() != "")
            {
                ht.Add("ProId", ProId.Trim());
            }
            else
            {
                ht.Add("ProId", "");
            }


            ChartData cd = new ChartData();
            string name = "";
            switch (StateId)
            {
                case "1":
                    name = "在线";
                    break;
                case "2":
                    name = "离线";
                    break;
                case "3":
                    name = "失联";
                    break;
                case "4":
                    name = "到期";
                    break;
                case "5":
                    name = "库存";
                    break;
                case "6":
                    name = "休眠";
                    break;
                case "7":
                    name = "其他";
                    break;
            }
            ht.Add("StateId", StateId);
            int count = ReportBll.GetTjbbTerDataCount(ht);
            cd.id = StateId.ToString();
            cd.value = count.ToString();
            cd.name = name;
            return cd;
        }

        [Log(LogMessage = "查看统计报表信息列表")]
        [UserFilter]
        public string GetTjbbTerData(string ObjType, string ObjValue, string ProId, string StateId, int rows, int page, string DeptId, string ChildrenSel, string StartTerNo, string EndTerNo)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            Hashtable ht = new Hashtable();
            ht.Add("StartData", (page - 1) * rows + 1);
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);
            if (DeptId != null && DeptId.Trim() != "")
            {
                ht.Add("DeptId", DeptId);
            }
            else
            {
                if (user.EnterId != null)
                {
                    ht.Add("DeptId", user.EnterId);
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }

            if (ChildrenSel != null && ChildrenSel.Trim() == "true")
            {
                if (user.EnterId != null)
                {
                    DeptInfo di = deptInfoBll.GetDeptInfo(ht["DeptId"].ToString());
                    ht.Add("Businessdivisioncode", di.Businessdivisioncode);
                    ht["DeptId"] = "";
                }
            }
            else
            {
                ht.Add("Businessdivisioncode", "");
            }


            if (ObjType == "1" && ObjValue != "" && ObjValue != null)
            {
                ht.Add("TerNo", ObjValue.Trim());
            }
            else
            {
                ht.Add("TerNo", "");
            }

            if (ObjType == "2" && ObjValue != "" && ObjValue != null)
            {
                ht.Add("CarNo", ObjValue.Trim());
            }
            else
            {
                ht.Add("CarNo", "");
            }

            if (ProId != null && ProId.Trim() != "")
            {
                ht.Add("ProId", ProId.Trim());
            }
            else
            {
                ht.Add("ProId", "");
            }

            if (StateId != null && StateId.Trim() != "")
            {
                ht.Add("StateId", StateId.Trim());
            }
            else
            {
                ht.Add("StateId", "");
            }

            if (StartTerNo != null && StartTerNo.Trim() != "")
            {
                ht.Add("StartTerNo", StartTerNo.Trim());
            }
            else
            {
                ht.Add("StartTerNo", "");
            }

            if (EndTerNo != null && EndTerNo.Trim() != "")
            {
                ht.Add("EndTerNo", EndTerNo.Trim());
            }
            else
            {
                ht.Add("EndTerNo", "");
            }
            IList<CarReport_TerData> iltd = ReportBll.GetTjbbTerData(ht);
            int count = ReportBll.GetTjbbTerDataCount(ht);
            return ConvertToJson(iltd, count);
        }

        [Log(LogMessage = "下载终端报表数据")]
        [UserFilter]
        public string GetReportTerData(string ObjType, string ObjValue, string ProId, string StateId, string DeptId, string ChildrenSel)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            Hashtable ht = new Hashtable();
            ht.Add("StartData", 0);
            ht.Add("EndData", 0);
            if (DeptId != null && DeptId.Trim() != "")
            {
                ht.Add("DeptId", DeptId);
            }
            else
            {
                if (user.EnterId != null)
                {
                    ht.Add("DeptId", user.EnterId);
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }
            if (ChildrenSel != null && ChildrenSel.Trim() == "true")
            {
                if (user.EnterId != null)
                {
                    DeptInfo di = deptInfoBll.GetDeptInfo(ht["DeptId"].ToString());
                    ht.Add("Businessdivisioncode", di.Businessdivisioncode);
                    ht["DeptId"] = "";
                }
            }
            else
            {
                ht.Add("Businessdivisioncode", "");
            }
            if (ObjType == "1" && ObjValue != "" && ObjValue != null)
            {
                ht.Add("TerNo", ObjValue.Trim());
            }
            else
            {
                ht.Add("TerNo", "");
            }

            if (ObjType == "2" && ObjValue != "" && ObjValue != null)
            {
                ht.Add("CarNo", ObjValue.Trim());
            }
            else
            {
                ht.Add("CarNo", "");
            }

            if (ProId != null && ProId.Trim() != "")
            {
                ht.Add("ProId", ProId.Trim());
            }
            else
            {
                ht.Add("ProId", "");
            }

            if (StateId != null && StateId.Trim() != "")
            {
                ht.Add("StateId", StateId.Trim());
            }
            else
            {
                ht.Add("StateId", "");
            }
            ht.Add("StartTerNo", "");
            ht.Add("EndTerNo", "");
            IList<CarReport_TerData> iltd = ReportBll.GetTjbbTerData(ht);
            ExcelUpLoad eu = new ExcelUpLoad();

            MemoryStream ms = eu.CreateExcelTerReportData(iltd);

            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/终端报表数据信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/终端报表数据信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/终端报表数据信息" + xlsName + ".xlsx";

                new LogMessage().Save("文件:" + ppphhh + "。");

                return ppphhh;
            }
            else
            {
                new LogMessage().Save("生成文件出错，请重新导出！");

                return "生成文件出错，请重新导出！";
            }
        }
        #endregion 

        #region 终端统计-有/无线

        /// <summary>
        /// 有/无线
        /// </summary>
        /// <returns></returns>
        [UserFilter]
        public ActionResult Index_ZDTJ()
        {
            return View();
        }

        [UserFilter]
        //终端统计
        [Log(LogMessage = "终端统计")]
        public string GetData_ZDTJ(

                            int rows,
                            int page,

                            string TerType, //终端类型：有线、无线

                            string DeptId, //部门代码
                            string ShowChild,//加载子企业

                            string TerTypeNo, //设备型号

                            string ObjTypeNo, //关键字类型
                            string KeyText,   //关键字值

                            string KSRQ,//开始时间
                            string JSRQ //结束时间
        )
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            new LogMessage().Save("终端类型：" + (TerType == "yxzd" ? "有线" : "无线") + "。");

            Hashtable ht = new Hashtable();

            ht.Add("StartData", (page - 1) * rows + 1);
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);

            //终端类型：有线、无线
            ht.Add("TerType", TerType);

            ht.Add("DeptId", DeptId);
            ht.Add("ShowChild", ShowChild);

            ht.Add("TerTypeNo", TerTypeNo);

            ht.Add("ObjTypeNo", ObjTypeNo);
            ht.Add("KeyText", KeyText);

            ht.Add("KSRQ", KSRQ);
            ht.Add("JSRQ", JSRQ);

            if (ht["DeptId"] == null || ht["DeptId"].ToString().Trim() == "")
            {
                if (user.EnterId != null)
                {
                    ht["DeptId"] = user.EnterId;
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }

            }

            int count = 0;

            IList<CarReport_ZDTJView> ltiv = ReportBll.GetCarReport_ZDTJViewPage(ht, out count);

            return ConvertToJson(ltiv, count);
        }

        [UserFilter]
        [Log(LogMessage = "终端统计导出")]
        public string GetData_ZDTJ_ToExcel(

                            string TerType, //终端类型：有线、无线

                            string DeptId, //部门代码
                            string ShowChild,//加载子企业

                            string TerTypeNo, //设备型号

                            string ObjTypeNo, //关键字类型
                            string KeyText,   //关键字值

                            string KSRQ,//开始时间
                            string JSRQ //结束时间
        )
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            Hashtable ht = new Hashtable();

            new LogMessage().Save("终端类型：" + (TerType == "yxzd" ? "有线" : "无线") + "。");

            ht.Add("TerType", TerType);

            ht.Add("DeptId", DeptId);
            ht.Add("ShowChild", ShowChild);

            ht.Add("TerTypeNo", TerTypeNo);

            ht.Add("ObjTypeNo", ObjTypeNo);
            ht.Add("KeyText", KeyText);

            ht.Add("KSRQ", KSRQ);
            ht.Add("JSRQ", JSRQ);

            if (ht["DeptId"] == null || ht["DeptId"].ToString().Trim() == "")
            {
                if (user.EnterId != null)
                {
                    ht["DeptId"] = user.EnterId;
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }

            IList<CarReport_ZDTJView> ltiv = ReportBll.GetCarReport_ZDTJToExcel(ht);

            ExcelUpLoad eu = new ExcelUpLoad();
            MemoryStream ms = eu.CreateRptZdtjToExcel(ltiv, TerType);
            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            string fileName = "有线";
            switch (TerType)
            {
                case "yxzd":
                    fileName = "有线";
                    break;

                case "wxzd":
                    fileName = "无线";
                    break;
            }

            fileName = "终端统计_" + fileName;

            // 输出Excel
            string xlsFileName = HttpContext.Server.MapPath("../Files/" + fileName) + xlsName + ".xlsx";

            using (FileStream fs = new FileStream(xlsFileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }

            if (System.IO.File.Exists(xlsFileName))
            {
                return "../../Files/" + fileName + xlsName + ".xlsx";
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }

        #endregion

        #region 报警统计
        [UserFilter]
        public ActionResult Index_BJ()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            if (user != null)
            {
                ViewBag.DeptId = user.EnterId;
                ViewBag.Deptcode = user.UserDeptcode;
            }

            ViewBag.KSRQ = GetKsrq();
            ViewBag.JSRQ = GetJsrq();

            string WarningType = "qb";
            if (Request.QueryString["type"] != null)
            {
                WarningType = Request.QueryString["type"].ToString();
            }
            ViewBag.WarningType = WarningType;

            return View();
        }

        [UserFilter]
        public string GetWarnCharData(
                            string WarningType,//报警类型
                            string DeptId, //部门代码
                            string ShowChild,//加载子企业
                            string TerTypeNo, //设备型号
                            string ObjTypeNo, //关键字类型
                            string KeyText) 
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            List<ChartData> cdlist = new List<ChartData>();
            List<string> WarnType = new List<string>() { "断电报警", "拆除报警", "超速报警", "区域报警", "低电预警", "疑似掉落", "疑似拆除" }; //, "疑似掉落", "疑似拆除" 
            foreach (string Item in WarnType)
            {

                if (WarningType == "qb")
                {
                    string WarnTypeCode = "";
                    switch (Item)
                    {
                        case "断电报警":
                            WarnTypeCode = "ddbj";
                            break;
                        case "拆除报警":
                            WarnTypeCode = "ccbj";
                            break;
                        case "超速报警":
                            WarnTypeCode = "csbj";
                            break;
                        case "区域报警":
                            WarnTypeCode = "qybj";
                            break;
                        case "低电预警":
                            WarnTypeCode = "ddyj";
                            break;
                        case "疑似掉落":
                            WarnTypeCode = "ysdl";
                            break;
                        case "疑似拆除":
                            WarnTypeCode = "yscc";
                            break;
                    }
                    ChartData cd = this.GetChartDataByWarnType(WarnTypeCode, DeptId, ShowChild, TerTypeNo, ObjTypeNo, KeyText);
                    cdlist.Add(cd);
                }
                else
                {
                    ChartData cd = new ChartData(WarningType, "0", Item);
                    switch (Item)
                    {
                        case "断电报警":
                            if (WarningType == "ddbj")
                            {
                                cd = this.GetChartDataByWarnType(WarningType, DeptId, ShowChild, TerTypeNo, ObjTypeNo, KeyText);
                            }
                            break;
                        case "拆除报警":
                            if (WarningType == "ccbj")
                            {
                                cd = this.GetChartDataByWarnType(WarningType, DeptId, ShowChild, TerTypeNo, ObjTypeNo, KeyText);
                            }
                            break;
                        case "超速报警":
                            if (WarningType == "csbj")
                            {
                                cd = this.GetChartDataByWarnType(WarningType, DeptId, ShowChild, TerTypeNo, ObjTypeNo, KeyText);
                            }
                            break;
                        case "区域报警":
                            if (WarningType == "qybj")
                            {
                                cd = this.GetChartDataByWarnType(WarningType, DeptId, ShowChild, TerTypeNo, ObjTypeNo, KeyText);
                            }
                            break;
                        case "低电预警":
                            if (WarningType == "ddyj")
                            {
                                cd = this.GetChartDataByWarnType(WarningType, DeptId, ShowChild, TerTypeNo, ObjTypeNo, KeyText);
                            }
                            break;
                        case "疑似掉落":
                            if (WarningType == "ysdl")
                            {
                                cd = this.GetChartDataByWarnType(WarningType, DeptId, ShowChild, TerTypeNo, ObjTypeNo, KeyText);
                            }
                            break;
                        case "疑似拆除":
                            if (WarningType == "yscc")
                            {
                                cd = this.GetChartDataByWarnType(WarningType, DeptId, ShowChild, TerTypeNo, ObjTypeNo, KeyText);
                            }
                            break;
                    }
                    cdlist.Add(cd);
                }
            }

            return ConvertToJson(cdlist);
        }

        [UserFilter]
        public ChartData GetChartDataByWarnType(string WarningType,//报警类型
                            string DeptId, //部门代码
                            string ShowChild,//加载子企业
                            string TerTypeNo, //设备型号
                            string ObjTypeNo, //关键字类型
                            string KeyText)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            Hashtable ht = new Hashtable();
            ht.Add("WarningType", WarningType);
            string bjlx = "全部";
            switch (WarningType)
            {
                case "ddbj":
                    bjlx = "断电报警";
                    break;
                case "ccbj":
                    bjlx = "拆除报警";
                    break;
                case "csbj":
                    bjlx = "超速报警";
                    break;
                case "qybj":
                    bjlx = "区域报警";
                    break;
                case "ddyj":
                    bjlx = "低电预警";
                    break;
                case "ysdl":
                    bjlx = "疑似掉落";
                    break;
                case "yscc":
                    bjlx = "疑似拆除";
                    break;
            }
            ht.Add("DeptId", DeptId);
            ht.Add("ShowChild", ShowChild);

            ht.Add("TerTypeNo", TerTypeNo);

            ht.Add("ObjTypeNo", ObjTypeNo);
            ht.Add("KeyText", KeyText);

            if (ht["DeptId"] == null || ht["DeptId"].ToString().Trim() == "")
            {
                if (user.EnterId != null)
                {
                    ht["DeptId"] = user.EnterId;
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }

            int count = 0;
            IList<CarReport_BJView> ltiv = ReportBll.GetCarReport_BJViewPage(ht, out count);
            ChartData cd = new ChartData();
            cd.id = WarningType;
            cd.value = count.ToString();
            cd.name = bjlx;
            return cd;
        }

        [UserFilter]
        //报警统计
        [Log(LogMessage = "报警统计")]
        public string GetData_BJTJ(

                            int rows,
                            int page,

                            string WarningType,

                            string DeptId, //部门代码
                            string ShowChild,//加载子企业

                            string TerTypeNo, //设备型号

                            string ObjTypeNo, //关键字类型
                            string KeyText,   //关键字值

                            string KSRQ,//开始时间
                            string JSRQ //结束时间
        )
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            Hashtable ht = new Hashtable();

            ht.Add("StartData", (page - 1) * rows + 1);
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);

            string bjlx = "全部";
            switch (WarningType)
            {
                case "ddbj":
                    bjlx = "断电报警";
                    break;
                case "ccbj":
                    bjlx = "拆除报警";
                    break;
                case "csbj":
                    bjlx = "超速报警";
                    break;
                case "qybj":
                    bjlx = "区域报警";
                    break;
                case "ddyj":
                    bjlx = "低电预警";
                    break;
                case "ysdl":
                    bjlx = "疑似掉落";
                    break;
                case "yscc":
                    bjlx = "疑似拆除";
                    break;
            }
            new LogMessage().Save("报警类型：" + bjlx + "。");

            ht.Add("WarningType", WarningType);

            ht.Add("DeptId", DeptId);
            ht.Add("ShowChild", ShowChild);

            ht.Add("TerTypeNo", TerTypeNo);

            ht.Add("ObjTypeNo", ObjTypeNo);
            ht.Add("KeyText", KeyText);

            ht.Add("KSRQ", KSRQ);
            ht.Add("JSRQ", JSRQ);

            if (ht["DeptId"] == null || ht["DeptId"].ToString().Trim() == "")
            {
                if (user.EnterId != null)
                {
                    ht["DeptId"] = user.EnterId;
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }

            int count = 0;

            IList<CarReport_BJView> ltiv = ReportBll.GetCarReport_BJViewPage(ht, out count);

            return ConvertToJson(ltiv, count);
        }

        public int GetData_BjCount()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            Hashtable ht = new Hashtable();
            ht["WarningType"] = "qb";
            if (ht["DeptId"] == null || ht["DeptId"].ToString().Trim() == "")
            {
                if (user.EnterId != null)
                {
                    ht["DeptId"] = user.EnterId;
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }
            ht.Add("ShowChild","true");
            int count = ReportBll.GetCarReport_BJViewPageCount(ht);
            return count;
        }
     

        [UserFilter]
        [Log(LogMessage = "报警统计导出")]
        public string GetData_BJTJ_ToExcel(

                            string WarningType,

                            string DeptId, //部门代码
                            string ShowChild,//加载子企业

                            string TerTypeNo, //设备型号

                            string ObjTypeNo, //关键字类型
                            string KeyText,   //关键字值

                            string KSRQ,//开始时间
                            string JSRQ //结束时间
        )
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            Hashtable ht = new Hashtable();

            string bjlx = "全部";
            switch (WarningType)
            {
                case "ddbj":
                    bjlx = "断电报警";
                    break;
                case "ccbj":
                    bjlx = "拆除报警";
                    break;
                case "csbj":
                    bjlx = "超速报警";
                    break;
                case "qybj":
                    bjlx = "区域报警";
                    break;
                case "ddyj":
                    bjlx = "低电预警";
                    break;
                case "ysdl":
                    bjlx = "疑似掉落";
                    break;
                case "yscc":
                    bjlx = "疑似拆除";
                    break;
            }
            new LogMessage().Save("报警类型：" + bjlx + "。");

            ht.Add("WarningType", WarningType);

            ht.Add("DeptId", DeptId);
            ht.Add("ShowChild", ShowChild);

            ht.Add("TerTypeNo", TerTypeNo);

            ht.Add("ObjTypeNo", ObjTypeNo);
            ht.Add("KeyText", KeyText);

            ht.Add("KSRQ", KSRQ);
            ht.Add("JSRQ", JSRQ);

            if (ht["DeptId"] == null || ht["DeptId"].ToString().Trim() == "")
            {
                if (user.EnterId != null)
                {
                    ht["DeptId"] = user.EnterId;
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }

            IList<CarReport_BJView> ltiv = ReportBll.GetCarReport_BJViewToExcel(ht);

            ExcelUpLoad eu = new ExcelUpLoad();
            MemoryStream ms = eu.CreateRptBjtjToExcel(ltiv);
            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            string fileName = "全部";
            switch(WarningType)
            {
                case "ddbj":
                    fileName = "断电报警";
                    break;

                case "ccbj":
                    fileName = "拆除报警";
                    break;

                case "qybj":
                    fileName = "区域报警";
                    break;

                case "csbj":
                    fileName = "超速报警";
                    break;

                case "ddyj":
                    fileName = "低电预警";
                    break;

                case "ysdl":
                    fileName = "疑似掉落";
                    break;

                case "yscc":
                    fileName = "疑似拆除";
                    break;
            }
            fileName = "报警统计_" + fileName;

            // 输出Excel
            string xlsFileName = HttpContext.Server.MapPath("../Files/" + fileName) + xlsName + ".xlsx";

            using (FileStream fs = new FileStream(xlsFileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }

            if (System.IO.File.Exists(xlsFileName))
            {
                return "../../Files/" + fileName + xlsName + ".xlsx";
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }

        #endregion

        #region 里程统计

        //里程统计
        [UserFilter]
        public ActionResult Index_LCTJ()
        {
            ViewBag.KSRQ = GetKsrq();
            ViewBag.JSRQ = GetJsrq();

            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            ViewBag.DeptId = user.EnterId;

            return View();
        }

        [UserFilter]
        [Log(LogMessage = "里程统计")]
        public string GetData_LCTJ(

                                    int rows,
                                    int page,

                                    string DeptId, //部门代码
                                    string ShowChild,//加载子企业

                                    string TerTypeNo, //设备类型

                                    string ObjTypeNo, //关键字类型
                                    string KeyText,   //关键字值

                                    string KSRQ,//开始时间
                                    string JSRQ //结束时间
            )
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            Hashtable ht = new Hashtable();
            
            ht.Add("StartData", (page - 1) * rows + 1);
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);

            ht.Add("DeptId", DeptId);
            ht.Add("ShowChild", ShowChild);

            ht.Add("TerTypeNo", TerTypeNo);

            ht.Add("ObjTypeNo", ObjTypeNo);
            ht.Add("KeyText", KeyText);

            ht.Add("KSRQ", KSRQ);
            ht.Add("JSRQ", JSRQ);

            if (ht["DeptId"] == null || ht["DeptId"].ToString().Trim() == "")
            {
                if (user.EnterId != null)
                {
                    ht["DeptId"] = user.EnterId;
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }

            int count = 0;

            IList<CarReport_LCTJView> ltiv = ReportBll.GetCarReport_LCTJViewPage(ht, out count);
            
            return ConvertToJson(ltiv, count);
        }

        [UserFilter]
        [Log(LogMessage = "里程统计导出")]
        public string GetData_LCTJ_ToExcel(

                                    string DeptId, //部门代码
                                    string ShowChild,//加载子企业

                                    string TerTypeNo, //设备型号

                                    string ObjTypeNo, //关键字类型
                                    string KeyText,   //关键字值

                                    string KSRQ,//开始时间
                                    string JSRQ //结束时间
            )
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            Hashtable ht = new Hashtable();

            ht.Add("DeptId", DeptId);
            ht.Add("ShowChild", ShowChild);

            ht.Add("TerTypeNo", TerTypeNo);

            ht.Add("ObjTypeNo", ObjTypeNo);
            ht.Add("KeyText", KeyText);

            ht.Add("KSRQ", KSRQ);
            ht.Add("JSRQ", JSRQ);

            if (ht["DeptId"] == null || ht["DeptId"].ToString().Trim() == "")
            {
                if (user.EnterId != null)
                {
                    ht["DeptId"] = user.EnterId;
                }
                else
                {
                    ht.Add("UserLname", user.UserLname);
                }
            }


            IList<CarReport_LCTJView> ltiv = ReportBll.GetCarReport_LCTJViewToExcel(ht);

            ExcelUpLoad eu = new ExcelUpLoad();
            MemoryStream ms = eu.CreateRptLctjToExcel(ltiv);
            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // 输出Excel
            string xlsFileName = HttpContext.Server.MapPath("../Files/里程统计") + xlsName + ".xlsx";

            using (FileStream fs = new FileStream(xlsFileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }

            if (System.IO.File.Exists(xlsFileName))
            {
                return "../../Files/里程统计" + xlsName + ".xlsx"; 
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }

        #endregion

        #region 里程报表
        public ActionResult Index_LCBB(string TerNo,string TerTypeid)
        {
            ViewBag.TerNo = TerNo;
            ViewBag.TerTypeid = TerTypeid;
            return View();
        }

        [Log(LogMessage = "里程报表")]
        public string GetData_LCBB(string TerNo,string JZRQ,string KSRQ)
        {
            IList<CarReport_LCBBView> ltiv = new List<CarReport_LCBBView>();
            int tianshu = (Convert.ToDateTime(JZRQ) - Convert.ToDateTime(KSRQ)).Days;
            double summile = 0;
            for (int i=tianshu; i>=0; i--)
            {
                Hashtable ht = new Hashtable();
                ht.Add("TerNo", TerNo);
                DateTime jzdate = Convert.ToDateTime(KSRQ).AddDays(i);
                ht.Add("StatDate", jzdate.ToString());
                IList<CarReport_LCBBView> lcbbs=ReportBll.GetLcbbData(ht);
                if (jzdate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    Transfers.ClintSendCommData(1107, "57", "", TerNo, "", "", "", "", "", "", "", jzdate.ToString("yyyy-MM-dd"), "", "", "", "", "", "", "");
                    System.Threading.Thread.Sleep(2000);
                    lcbbs = ReportBll.GetLcbbTodayData(ht);
                }
                CarReport_LCBBView lcbbview = this.GetLcbbViewByList(lcbbs, TerNo, jzdate.ToString());
                summile += Convert.ToDouble(lcbbview.DayMile == "-" ? "0" : lcbbview.DayMile);
                ltiv.Add(lcbbview);
            }

            CarReport_LCBBView hjlcbbview = new CarReport_LCBBView();
            hjlcbbview.TerNo = TerNo;
            hjlcbbview.StatDate = "合计";
            hjlcbbview.RunTime = "-";
            hjlcbbview.StopTime = "-";
            hjlcbbview.StopCount = "-";
            hjlcbbview.DayMile = summile.ToString();
            hjlcbbview.DaySumMile = "-";
            ltiv.Add(hjlcbbview);
            return ConvertToJson(ltiv);
        }

        public CarReport_LCBBView GetLcbbViewByList(IList<CarReport_LCBBView> lcbblist, string TerNo, string JZRQ)
        {
            CarReport_LCBBView lcbbview = new CarReport_LCBBView();
            if (lcbblist == null || lcbblist.Count == 0)
            {
                lcbbview = new CarReport_LCBBView();
                lcbbview.TerNo = TerNo;
                lcbbview.StatDate = Convert.ToDateTime(JZRQ).ToString();
                lcbbview.RunTime = "-";
                lcbbview.StopTime = "-";
                lcbbview.StopCount = "-";
                lcbbview.DayMile = "-";
                lcbbview.DaySumMile = "-";
            }
            else
            {
                lcbbview = lcbblist[0];
                if (lcbbview.RunTime != "0")
                {
                    string runtimestr = "";
                    if (Convert.ToInt32(lcbbview.RunTime) / 60 > 0)
                    {
                        runtimestr += Convert.ToInt32(lcbbview.RunTime) / 60 + "小时";
                    }
                    if (Convert.ToInt32(lcbbview.RunTime) % 60 > 0)
                    {
                        runtimestr += Convert.ToInt32(lcbbview.RunTime) % 60 + "分钟";
                    }
                    lcbbview.RunTime = runtimestr;
                }

                if (lcbbview.StopTime != "0")
                {
                    string stoptimestr = "";
                    if (Convert.ToInt32(lcbbview.StopTime) / 60 > 0)
                    {
                        stoptimestr += Convert.ToInt32(lcbbview.StopTime) / 60 + "小时";
                    }
                    if (Convert.ToInt32(lcbbview.StopTime) % 60 > 0)
                    {
                        stoptimestr += Convert.ToInt32(lcbbview.StopTime) % 60 + "分钟";
                    }
                    lcbbview.StopTime = stoptimestr;
                }
            }
            return lcbbview;
        }

        public string LcdataToExcel(string TerNo, string JZRQ, string KSRQ)
        {
            IList<CarReport_LCBBView> ltiv = new List<CarReport_LCBBView>();
            int tianshu = (Convert.ToDateTime(JZRQ) - Convert.ToDateTime(KSRQ)).Days;
            double summile = 0;
            for (int i = tianshu; i >= 0; i--)
            {
                Hashtable ht = new Hashtable();
                ht.Add("TerNo", TerNo);
                DateTime jzdate = Convert.ToDateTime(KSRQ).AddDays(i);
                ht.Add("StatDate", jzdate.ToString());
                IList<CarReport_LCBBView> lcbbs = ReportBll.GetLcbbData(ht);
                if (jzdate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    lcbbs = ReportBll.GetLcbbTodayData(ht);
                }
                CarReport_LCBBView lcbbview = this.GetLcbbViewByList(lcbbs, TerNo, jzdate.ToString());
                summile += Convert.ToDouble(lcbbview.DayMile == "-" ? "0" : lcbbview.DayMile);
                ltiv.Add(lcbbview);
            }

            CarReport_LCBBView hjlcbbview = new CarReport_LCBBView();
            hjlcbbview.TerNo = TerNo;
            hjlcbbview.StatDate = "合计";
            hjlcbbview.RunTime = "-";
            hjlcbbview.StopTime = "-";
            hjlcbbview.StopCount = "-";
            hjlcbbview.DayMile = summile.ToString();
            hjlcbbview.DaySumMile = "-";
            ltiv.Add(hjlcbbview);

            ExcelUpLoad eu = new ExcelUpLoad();

            MemoryStream ms = eu.CreatePctdataExcel(ltiv);

            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/里程统计信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/里程统计信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/里程统计信息" + xlsName + ".xlsx";
                return ppphhh;
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }

        #endregion 

        #region 在线统计
        [UserFilter]
        public ActionResult Index_ZXTJ()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            ViewBag.DeptId = user.EnterId;
            return View();
        }

        [Log(LogMessage = "在线统计")]
        [UserFilter]
        public string GetData_ZXTJ(int rows, int page, string DeptId, string TerTypeId, string TerNo, string CarNo, string KSRQ, string JSRQ, string ShowChild)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            Hashtable ht = new Hashtable();

            ht.Add("StartData", (page - 1) * rows + 1);
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);

            if (ShowChild == "true")
            {
                string userDeptId = "";
                if (DeptId == null || DeptId.ToString().Trim() == "")
                {
                    userDeptId = user.EnterId;
                }
                else
                {
                    userDeptId = DeptId;
                }
                DeptInfoBLL dbll = new DeptInfoBLL();
                DeptInfo di = dbll.GetDeptInfo(userDeptId);
                ht.Add("DeptCode", di.Businessdivisioncode);
                ht.Add("DeptId", "");
            }
            else
            {
                if (DeptId == null || DeptId.ToString().Trim() == "")
                {
                    ht["DeptId"] = user.EnterId;
                }
                else
                {
                    ht.Add("DeptId", DeptId);
                }
            }
            ht.Add("TerTypeId", TerTypeId);

            ht.Add("TerNo", TerNo);
            ht.Add("CarNo", CarNo);

            ht.Add("st", KSRQ);
            ht.Add("ed", JSRQ == "" || JSRQ == null ? "" : Convert.ToDateTime(JSRQ).AddDays(1).ToString());

            IList<CarReport_ZXTJView> ltiv = ReportBll.GetZxtjViewPage(ht);
            int count = ReportBll.GetZxtjViewCount(ht);
            return ConvertToJson(ltiv, count);
        }

        [Log(LogMessage = "在线统计导出")]
        [UserFilter]
        public string GetData_ZXTJ_ToExcel(string DeptId, string TerTypeId, string TerNo, string CarNo, string KSRQ, string JSRQ, string ShowChild)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            Hashtable ht = new Hashtable();
            ht.Add("EndData", 0);
            if (ShowChild == "true")
            {
                string userDeptId = "";
                if (DeptId == null || DeptId.ToString().Trim() == "")
                {
                    userDeptId = user.EnterId;
                }
                else
                {
                    userDeptId = DeptId;
                }
                DeptInfoBLL dbll = new DeptInfoBLL();
                DeptInfo di = dbll.GetDeptInfo(userDeptId);
                ht.Add("DeptCode", di.Businessdivisioncode);
                ht.Add("DeptId", "");
            }
            else
            {
                if (DeptId == null || DeptId.ToString().Trim() == "")
                {
                    ht.Add("DeptId", user.EnterId);
                }
                else
                {
                    ht.Add("DeptId", DeptId);
                }
            }
            ht.Add("TerTypeId", TerTypeId);
            ht.Add("TerNo", TerNo);
            ht.Add("CarNo", CarNo);
            ht.Add("st", KSRQ);
            ht.Add("ed", JSRQ == "" || JSRQ == null ? "" : Convert.ToDateTime(JSRQ).AddDays(1).ToString());

            IList<CarReport_ZXTJView> ltiv = ReportBll.GetZxtjViewPage(ht);
            ExcelUpLoad eu = new ExcelUpLoad();

            MemoryStream ms = eu.CreateRptZxtjToExcel(ltiv);

            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/在线统计信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/在线统计信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/在线统计信息" + xlsName + ".xlsx";
                return ppphhh;
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }
        #endregion

        private string GetKsrq()
        {
            return DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
        }
        private string GetJsrq()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        #region 离线统计
        [UserFilter]
        public ActionResult Index_LXTJ()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            ViewBag.DeptId = user.EnterId;
            return View();
        }

        [Log(LogMessage = "离线统计")]
        [UserFilter]
        public string GetData_LXTJ(int rows, int page, string DeptId, string TerTypeId, string TerNo, string CarNo, string KSRQ, string JSRQ, string ShowChild)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            Hashtable ht = new Hashtable();

            ht.Add("StartData", (page - 1) * rows + 1);
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);
            if (ShowChild == "true")
            {
                string userDeptId = "";
                if (DeptId == null || DeptId.ToString().Trim() == "")
                {
                    userDeptId = user.EnterId;
                }
                else
                {
                    userDeptId = DeptId;
                }
                DeptInfoBLL dbll = new DeptInfoBLL();
                DeptInfo di = dbll.GetDeptInfo(userDeptId);
                ht.Add("DeptCode", di.Businessdivisioncode);
                ht.Add("DeptId", "");
            }
            else
            {
                if (DeptId == null || DeptId.ToString().Trim() == "")
                {
                    ht.Add("DeptId", user.EnterId);
                }
                else
                {
                    ht.Add("DeptId", DeptId);
                }
            }
            ht.Add("TerTypeId", TerTypeId);

            ht.Add("TerNo", TerNo);
            ht.Add("CarNo", CarNo);

            ht.Add("st", KSRQ);
            ht.Add("ed", JSRQ == "" || JSRQ==null ? "" : Convert.ToDateTime(JSRQ).AddDays(1).ToString());

            IList<CarReport_LXTJView> ltiv = ReportBll.GetLxtjViewPage(ht);
            int count = ReportBll.GetLxtjViewCount(ht);
            return ConvertToJson(ltiv, count);
        }

        [Log(LogMessage = "离线统计导出")]
        [UserFilter]
        public string GetData_LXTJ_ToExcel(string DeptId, string TerTypeId, string TerNo, string CarNo, string KSRQ, string JSRQ, string ShowChild)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            Hashtable ht = new Hashtable();
            ht.Add("EndData", 0);
            if (ShowChild == "true")
            {
                string userDeptId = "";
                if (DeptId == null || DeptId.ToString().Trim() == "")
                {
                    userDeptId = user.EnterId;
                }
                else
                {
                    userDeptId = DeptId;
                }
                DeptInfoBLL dbll = new DeptInfoBLL();
                DeptInfo di = dbll.GetDeptInfo(userDeptId);
                ht.Add("DeptCode", di.Businessdivisioncode);
                ht.Add("DeptId", "");
            }
            else
            {
                if (DeptId == null || DeptId.ToString().Trim() == "")
                {
                    ht.Add("DeptId", user.EnterId);
                }
                else
                {
                    ht.Add("DeptId", DeptId);
                }
            }
            ht.Add("TerTypeId", TerTypeId);
            ht.Add("TerNo", TerNo);
            ht.Add("CarNo", CarNo);
            ht.Add("st", KSRQ);
            ht.Add("ed", JSRQ == "" || JSRQ == null ? "" : Convert.ToDateTime(JSRQ).AddDays(1).ToString());

            IList<CarReport_LXTJView> ltiv = ReportBll.GetLxtjViewPage(ht);
            ExcelUpLoad eu = new ExcelUpLoad();

            MemoryStream ms = eu.CreateRptLxtjToExcel(ltiv);

            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/离线统计信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/离线统计信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/离线统计信息" + xlsName + ".xlsx";
                return ppphhh;
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }
        #endregion 

    }
}