using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GModel.Car;
using GBLL.Car;
using GModel.RoleRight;
using GBLL.Location;
using GModel.Basic;
using GBLL.Basic;
using SuperGPS.App_Start;
using System.Collections;
using GCommon;
using System.IO;
using GModel.Location;
using GModel;
using System.Data;
using GBLL;
using System.Diagnostics;

namespace SuperGPS.Controllers
{
    [UserFilter]
    public class TerminalController : BaseController
    {
        // GET: Terminal
        ColligateQueryService c = new ColligateQueryService();
        TerminalInfoBLL terminalInfoBll = new TerminalInfoBLL();
        TerminalInfoViewBLL terminalInfoViewBll = new TerminalInfoViewBLL();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();
        RealtimeDataBLL realtimeDataBll = new RealtimeDataBLL();

        //[OutputCache(CacheProfile = "ActionCacheProfile")]
        public ActionResult TerminalIndex()
        {
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.AddTer = "false";
            ViewBag.EditTer = "false";
            ViewBag.DelTer = "false";
            ViewBag.ExChange = "false";
            ViewBag.SetPct = "false";
            ViewBag.SendCmd = "false";
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "添加终端":
                        ViewBag.AddTer = "true";
                        break;
                    case "删除终端":
                        ViewBag.DelTer = "true";
                        break;
                    case "修改终端":
                        ViewBag.EditTer = "true";
                        break;
                    case "终端流转":
                        ViewBag.ExChange = "true";
                        break;
                    case "设置里程":
                        ViewBag.SetPct = "true";
                        break;
                    case "发送命令":
                        ViewBag.SendCmd = "true";
                        break;

                }
            }
            return View();
        }

        public ActionResult HistoryData(string TerNo, string DeptId)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            //bcadmin 超级管理员
            if (user.UserId == "2bd01c98-15db-4543-9f47-1eafd6999078")
            {
                ViewBag.admin = true;
            }
            else
            {
                ViewBag.admin = false;
            }
            ViewBag.TerNo = TerNo;
            ViewBag.DeptId = DeptId;

            return View();
        }

        public ActionResult YXHistoryData(string TerNo, string DeptId)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            //bcadmin 超级管理员
            if (user.UserId == "2bd01c98-15db-4543-9f47-1eafd6999078")
            {
                ViewBag.admin = true;
            }
            else
            {
                ViewBag.admin = false;
            }
            ViewBag.TerNo = TerNo;
            ViewBag.DeptId = DeptId;
            return View();
        }

        public ActionResult AllData()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.SendCmd = "false";
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "发送命令":
                        ViewBag.SendCmd = "true";
                        break;
                }
            }

            //bcadmin 超级管理员      bcsccs 生产测试
            if (user.UserId == "2bd01c98-15db-4543-9f47-1eafd6999078" || user.UserId == "9386458a-c5a7-4560-a5c4-b798570fa7f5")
            {
                ViewBag.admin = true;
            }
            else
            {
                ViewBag.admin = false;
            }
            return View();
        }

        public ActionResult MilageData(string TerNo, string DeptId)
        {
            ViewBag.TerNo = TerNo;
            ViewBag.DeptId = DeptId;
            return View();
        }

        public ActionResult TercensusData()
        {
            return View();
        }

        public ActionResult TerInfo(string TerNo)
        {
            Hashtable ht = new Hashtable();
            ht.Add("TerNo",TerNo);
            ht.Add("EndData", 0);
            ht.Add("StateId", "");
            ht.Add("TerModel", "");
            TerData terinfo = (TerData)realtimeDataBll.GetTerData(ht)[0];
            return View(terinfo);
        }

        [Log(LogMessage = "查看终端上传信息列表")]
        [UserFilter]
        public string GetTerData(string ObjType, string ObjValue, string ProId, string StateId, int rows, int page, string DeptId, string ChildrenSel, string StartTerNo, string EndTerNo, string DayNumber, string DaySpanNumber, string TerModel, string AddTerMonth, string OverTerMonth)
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

            if (ObjType == "1" && ObjValue != "" && ObjValue!=null)
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

            if (ProId != null && ProId.Trim() != "" && ProId.Trim() != "-1")
            {
                if (ProId == "6")
                {
                    ht.Add("Rawdataid", ProId.Trim());
                }
                else
                {
                    ht.Add("ProId", ProId.Trim());
                }
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

            if (TerModel != null && TerModel.Trim() != "")
            {
                ht.Add("TerModel", TerModel.Trim());
            }
            else
            {
                ht.Add("TerModel", "");
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

            if (AddTerMonth != null && AddTerMonth != "" && AddTerMonth != "0")
            {
                if(int.Parse(AddTerMonth)<10)
                {
                    ht.Add("AddTerMonth", DateTime.Now.Year + "0" + AddTerMonth);
                }
                else
                {
                    ht.Add("AddTerMonth", DateTime.Now.Year + AddTerMonth);
                }
            }
            else
            {
                ht.Add("AddTerMonth", "");
            }

            if (OverTerMonth != null && OverTerMonth != "" && OverTerMonth != "0")
            {
                if (user.EnterId != null)
                {
                    DeptInfo di = deptInfoBll.GetDeptInfo(ht["DeptId"].ToString());
                    ht.Add("Businessdivisioncode", di.Businessdivisioncode);
                    ht["DeptId"] = "";
                }
                if (int.Parse(OverTerMonth) < 10)
                {
                    ht.Add("OverTerMonth", DateTime.Now.Year + "0" + OverTerMonth);
                }
                else
                {
                    ht.Add("OverTerMonth", DateTime.Now.Year + OverTerMonth);
                }
            }
            else
            {
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
                ht.Add("OverTerMonth", "");
            }

            ht.Add("DayNumber", DayNumber==null?7.1:Double.Parse(DayNumber));

            if (StateId == "6")
            {
                ht["StartData"]=0;
                ht["EndData"]=0;
                //var sw = new Stopwatch();
                //sw.Start();
                IList<TerData> iltdall = realtimeDataBll.GetTerData(ht); 

                //System.IO.File.AppendAllText("D:\\log.txt","查库时间:"+sw.ElapsedMilliseconds+Environment.NewLine);
                //sw.Restart();

                foreach (TerData td in iltdall)
                {
                    DateTime time1 = DateTime.Now;
                    DateTime time2 = Convert.ToDateTime(td.Sleeptime);
                    string dateDiff = null;
                    TimeSpan ts1 = new TimeSpan(time1.Ticks);
                    TimeSpan ts2 = new TimeSpan(time2.Ticks);
                    TimeSpan ts = ts1.Subtract(ts2).Duration();          //显示时间  

                    dateDiff = ts.Days.ToString() + "天"
                    + ts.Hours.ToString() + "小时"
                    + (ts.Minutes+1).ToString() + "分钟";

                    td.StopLong = dateDiff; 
                }

                //System.IO.File.AppendAllText("D:\\log.txt", "循环时间:" + sw.ElapsedMilliseconds);
                //sw.Restart();
                //System.IO.File.AppendAllText("D:\\log.txt", "返回前时间:" + sw.ElapsedMilliseconds);

                //遍历有停车时长  
                IList<TerData> iltdNew = iltdall.Where(s => !String.IsNullOrEmpty(s.StopLong)).ToList();
                if (DaySpanNumber != null && DaySpanNumber != "")
                {
                    if (DaySpanNumber == "5") 
                    {
                        iltdNew = iltdNew.Where(s => int.Parse(s.StopLong.Substring(0, s.StopLong.IndexOf('天'))) >= int.Parse(DaySpanNumber)).ToList();
                    }
                    else if (DaySpanNumber != "6") 
                    {
                        iltdNew = iltdNew.Where(s => s.StopLong.Substring(0, s.StopLong.IndexOf('天')) == DaySpanNumber).ToList();
                    }
                }
                int cnt = iltdNew.Count;
                IList<TerData> iltdPager = iltdNew.Skip((page - 1) * rows).Take(rows).ToList();
                return ConvertToJson(iltdPager, cnt); 
            }
            else
            {
                IList<TerData> iltd = realtimeDataBll.GetTerData(ht);
                foreach (TerData td in iltd)
                {
                    //遍历添加激活时间、激活次数
                    string sql = "select acttime from realtime_data where TER_NO='" + td.TerNo + "'";
                    DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string acttime = ds.Tables[0].Rows[0]["acttime"].ToString();
                        if (acttime != null && acttime != "")
                        {
                            string[] actarr = acttime.Split('|');
                            td.ActTime = DateTime.Parse(actarr[actarr.Length - 2].Split(',')[1].ToString());
                            td.ActCount = (actarr.Length - 1).ToString();
                        }
                    }

                    //添加停车时长
                    if (td.Tertypeid == "2" || td.Tertypeid == "3")
                    {
                        if (!String.IsNullOrEmpty(td.Sleeptime))
                        {
                            DateTime time1 = DateTime.Now;
                            DateTime time2 = Convert.ToDateTime(td.Sleeptime);
                            string dateDiff = null;
                            TimeSpan ts1 = new TimeSpan(time1.Ticks);
                            TimeSpan ts2 = new TimeSpan(time2.Ticks);
                            TimeSpan ts = ts1.Subtract(ts2).Duration();          //显示时间  

                            dateDiff = ts.Days.ToString() + "天"
                            + ts.Hours.ToString() + "小时"
                            + (ts.Minutes+1).ToString() + "分钟";

                            td.StopLong = dateDiff;
                        }
                    }
                }
                int count = realtimeDataBll.GetTerDataCount(ht);
                return ConvertToJson(iltd, count);
            }
        }

        public ActionResult TerActData(string TerNo)
        {
            ViewBag.TerNo = TerNo;
            return View();
        }

        public string GetActData(string TerNo)
        {
            string sql = "select acttime from realtime_data where TER_NO='" + TerNo + "'";
            DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sql);
            IList<ActData> ilad = new List<ActData>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string acttime = ds.Tables[0].Rows[0]["acttime"].ToString();
                if (acttime != null && acttime != "")
                {
                    string[] actarr = acttime.Split('|');
                    for (int i = 0; i < actarr.Length - 1; i++)
                    {
                        ActData ad = new ActData();
                        string state=actarr[i].Split(',')[0].ToString();
                        if(state=="1")
                        {
                            ad.ProState = "测试";
                        }
                        else if (state == "2")
                        {
                            ad.ProState = "待激活";
                        }
                        else if(state == "3")
                        {
                            ad.ProState = "已激活";
                        }
                        else if (state == "4")
                        {
                            ad.ProState = "已拆除";
                        }
                        else if (state == "5")
                        {
                            ad.ProState = "到期";
                        }
                        else
                        {
                            ad.ProState = "其他";
                        }
                        ad.ActTime = DateTime.Parse(actarr[i].Split(',')[1].ToString());
                        ilad.Add(ad);
                    }
                }
            }
            return ConvertToJson(ilad);
        }

        public class ActData
        {
            public string ProState
            {
                get;
                set;
            }
            public DateTime ActTime
            {
                get;
                set;
            }
        }

        [Log(LogMessage = "下载终端上传数据")]
        [UserFilter]
        public string GetUpLoadTerData(string ObjType, string ObjValue, string ProId, string StateId, string DeptId, string ChildrenSel, string StartTerNo, string EndTerNo, string DayNumber, string DaySpanNumber, string TerModel, string AddTerMonth, string OverTerMonth)
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

            if (ProId != null && ProId.Trim() != "" && ProId.Trim() != "-1")
            {
                if (ProId == "6")
                {
                    ht.Add("Rawdataid", ProId.Trim());
                }
                else
                {
                    ht.Add("ProId", ProId.Trim());
                }
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

            if (TerModel != null && TerModel.Trim() != "")
            {
                ht.Add("TerModel", TerModel.Trim());
            }
            else
            {
                ht.Add("TerModel", "");
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
            if (AddTerMonth != null && AddTerMonth != "" && AddTerMonth != "0")
            {
                if (int.Parse(AddTerMonth) < 10)
                {
                    ht.Add("AddTerMonth", DateTime.Now.Year + "0" + AddTerMonth);
                }
                else
                {
                    ht.Add("AddTerMonth", DateTime.Now.Year + AddTerMonth);
                }
            }
            else
            {
                ht.Add("AddTerMonth", "");
            }

            if (OverTerMonth != null && OverTerMonth != "" && OverTerMonth != "0")
            {
                if (user.EnterId != null)
                {
                    DeptInfo di = deptInfoBll.GetDeptInfo(ht["DeptId"].ToString());
                    ht.Add("Businessdivisioncode", di.Businessdivisioncode);
                    ht["DeptId"] = "";
                }
                if (int.Parse(OverTerMonth) < 10)
                {
                    ht.Add("OverTerMonth", DateTime.Now.Year + "0" + OverTerMonth);
                }
                else
                {
                    ht.Add("OverTerMonth", DateTime.Now.Year + OverTerMonth);
                }
            }
            else
            {
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
                ht.Add("OverTerMonth", "");
            }
            ht.Add("DayNumber", DayNumber == null ? 7.1 : Double.Parse(DayNumber));
            IList<TerData> iltd = realtimeDataBll.GetTerData(ht);
      
            foreach (TerData td in iltd)
            {
                // 遍历添加激活时间、激活次数
                string sql = "select acttime from realtime_data where TER_NO='" + td.TerNo + "'";
                DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string acttime = ds.Tables[0].Rows[0]["acttime"].ToString();
                    if (acttime != null && acttime != "")
                    {
                        string[] actarr = acttime.Split('|');
                        td.ActTime = DateTime.Parse(actarr[actarr.Length - 2].Split(',')[1].ToString());
                        td.ActCount = (actarr.Length - 1).ToString();
                    }
                }

                //添加停车时长
                if (td.Tertypeid == "2" || td.Tertypeid == "3")
                {
                    if (!String.IsNullOrEmpty(td.Sleeptime))
                    {
                        DateTime time1 = DateTime.Now;
                        DateTime time2 = Convert.ToDateTime(td.Sleeptime);
                        string dateDiff = null;
                        TimeSpan ts1 = new TimeSpan(time1.Ticks);
                        TimeSpan ts2 = new TimeSpan(time2.Ticks);
                        TimeSpan ts = ts1.Subtract(ts2).Duration();          //显示时间  

                        dateDiff = ts.Days.ToString() + "天"
                        + ts.Hours.ToString() + "小时"
                        + (ts.Minutes+1).ToString() + "分钟";

                        td.StopLong = dateDiff;
                    }
                }
            }

            if (StateId == "6")
            {
                iltd = iltd.Where(s => !String.IsNullOrEmpty(s.StopLong)).ToList();
                if (DaySpanNumber != null && DaySpanNumber != "")
                {
                    if (DaySpanNumber == "5")
                    {
                        iltd = iltd.Where(s => int.Parse(s.StopLong.Substring(0, s.StopLong.IndexOf('天'))) >= int.Parse(DaySpanNumber)).ToList();
                    }
                    else if (DaySpanNumber != "6")
                    {
                        iltd = iltd.Where(s => s.StopLong.Substring(0, s.StopLong.IndexOf('天')) == DaySpanNumber).ToList();
                    }
                }
            }

            ExcelUpLoad eu = new ExcelUpLoad();
            string ppphhh = "";
            if (iltd.Count > 60000)
            {
                int pagSize = 60000;
                int pageCount = (iltd.Count - 1) / pagSize + 1;
                List<TerData>[] result = new List<TerData>[pageCount];
                for (var i = 0; i < pageCount; i++)
                {
                    result[i] = iltd.Skip(i * pagSize).Take(pagSize).ToList();
                    MemoryStream ms = new MemoryStream();
                    if (user.UserId == "2bd01c98-15db-4543-9f47-1eafd6999078" || user.UserId == "9386458a-c5a7-4560-a5c4-b798570fa7f5")
                    {
                         ms = eu.CreateExcelTerData(result[i]);
                    }
                    else
                    {
                         ms = eu.CreateExcelTerDataByUser(result[i]);
                    }

                    string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-" + (i + 1);

                    // 输出Excel
                    using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/终端上传数据信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                    if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/终端上传数据信息") + xlsName + ".xlsx"))
                    {
                        ppphhh += "../../Files/终端上传数据信息" + xlsName + ".xlsx,";
                    }
                    else
                    {
                        ppphhh = "";
                        break;
                    }
                }
            }
            else
            { 
                MemoryStream ms = new MemoryStream();
                if (user.UserId == "2bd01c98-15db-4543-9f47-1eafd6999078" || user.UserId == "9386458a-c5a7-4560-a5c4-b798570fa7f5")
                {
                     ms = eu.CreateExcelTerData(iltd);
                }
                else
                {
                    ms = eu.CreateExcelTerDataByUser(iltd);                
                }
                string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                // 输出Excel
                using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/终端上传数据信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
                if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/终端上传数据信息") + xlsName + ".xlsx"))
                {
                    ppphhh = "../../Files/终端上传数据信息" + xlsName + ".xlsx,";
                }  
            }
            if (!string.IsNullOrEmpty(ppphhh))
            {
                new LogMessage().Save("文件:" + ppphhh + "。");
                return ppphhh;
            }
            else
            {
                new LogMessage().Save("生成文件出错，请重新导出！");
                return "生成文件出错，请重新导出！";
            }
        }


        [UserFilter]
        public string GetTercensusData(string TerNo, int rows, int page, string DeptId, string ChildrenSel)
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
                ht.Add("DeptId", user.EnterId);
            }

            if (ChildrenSel != null && ChildrenSel.Trim() == "true")
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(ht["DeptId"].ToString());
                ht.Add("Businessdivisioncode", di.Businessdivisioncode);
                ht["DeptId"] = "";
            }
            else
            {
                ht.Add("Businessdivisioncode", "");
            }


            if (TerNo != null && TerNo.Trim() != "")
            {
                ht.Add("TerNo", TerNo.Trim());
            }
            else
            {
                ht.Add("TerNo", "");
            }
            IList<TercensusData> iltcd = terminalInfoBll.GetTercensusData(ht);
            int count = terminalInfoBll.GetTercensusDataCount(ht);
            return ConvertToJson(iltcd, count);
        }

        [UserFilter]
        public string GetloadTercensusData(string TerNo, string DeptId, string ChildrenSel)
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
                ht.Add("DeptId", user.EnterId);
            }

            if (ChildrenSel != null && ChildrenSel.Trim() == "true")
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(ht["DeptId"].ToString());
                ht.Add("Businessdivisioncode", di.Businessdivisioncode);
                ht["DeptId"] = "";
            }
            else
            {
                ht.Add("Businessdivisioncode", "");
            }


            if (TerNo != null && TerNo.Trim() != "")
            {
                ht.Add("TerNo", TerNo.Trim());
            }
            else
            {
                ht.Add("TerNo", "");
            }
            IList<TercensusData> iltcd = terminalInfoBll.GetTercensusData(ht);
            ExcelUpLoad eu = new ExcelUpLoad();
            MemoryStream ms = eu.CreateTercensusExcel(iltcd);
            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/统计分析信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/统计分析信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/统计分析信息" + xlsName + ".xlsx";
                return ppphhh;
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }

        [Log(LogMessage = "查看终端信息列表")]
        [UserFilter]
        public string GetTerList(
            TerminalInfoView tiv, 
            int rows, 
            int page, 
            string ChildrenSel,
            string TerBindType, string IsCheckNos, string CheckTernos)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            Hashtable ht = new Hashtable();

            ht.Add("StartData", (page - 1) * rows + 1);
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);
            ht.Add("Businessdivisioncode", tiv.Businessdivisioncode);
            ht.Add("DeptId", tiv.DeptId);
            ht.Add("TerNo", tiv.TerNo);
            ht.Add("CarAdminName", tiv.CarAdminName);
            ht.Add("ProId", tiv.ProId);
            ht.Add("Bind", "");
            ht.Add("NotBind", "");

            switch (TerBindType)
            {
                case "2":
                    ht["Bind"] = "true";
                    break;
                case "3":
                    ht["NotBind"] = "true";
                    break;
            }

            if (ht["DeptId"] == null || ht["DeptId"].ToString().Trim() == "")
            {
                ht["DeptId"] = user.EnterId;
            }
            if (ChildrenSel == "true")
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(ht["DeptId"].ToString());
                if (di != null)
                {
                    ht["Businessdivisioncode"] = di.Businessdivisioncode;
                    ht["DeptId"] = "";
                }
            }
            if (IsCheckNos == "true" && CheckTernos != null && CheckTernos!="")
            {
                string[] TerNos = CheckTernos.Split(',');
                string checknos = "";
                foreach(string ternostr in TerNos)
                {
                    if (!string.IsNullOrEmpty(ternostr))
                    {
                        checknos += "'" + ternostr + "',";
                    }
                }
                ht["CheckTernos"] = checknos.Trim(',');
            }
            IList<TerminalInfoView> ltiv = terminalInfoViewBll.GetTerminalInfoViewPage(ht);
            int count = terminalInfoViewBll.GetTerminalInfoViewCount(ht);
            return ConvertToJson(ltiv, count);
        }

        public string GetSelectTerList(string SelectTers)
        {
            if (SelectTers == "")
            {
                SelectTers = "''";
            }
            else
            {
                string[] arrters = SelectTers.Split(',');
                string terstr = "";
                foreach(string ter in arrters)
                {
                    if (!string.IsNullOrEmpty(ter))
                    {
                        terstr += "'" + ter + "',";
                    }
                }
                SelectTers = terstr.Trim(',');
            }
            IList<TerminalInfoView> ltiv = terminalInfoViewBll.SelectTerminalInfoViewByTerNos(SelectTers);
            int count = terminalInfoViewBll.SelectTerminalInfoViewByTerNosCount(SelectTers);
            return ConvertToJson(ltiv,count);
        }


        [Log(LogMessage = "下载终端信息数据")]
        [UserFilter]
        public string GetUpLoadTer(TerminalInfoView tiv, string ChildrenSel, string TerBindType, string IsCheckNos, string CheckTernos)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            tiv.StartData = 0;
            tiv.EndData = 0;
            Hashtable ht = new Hashtable();
            ht.Add("Businessdivisioncode", tiv.Businessdivisioncode);
            ht.Add("DeptId", tiv.DeptId);
            ht.Add("TerNo", tiv.TerNo);
            ht.Add("CarAdminName", tiv.CarAdminName);
            ht.Add("ProId", tiv.ProId);
            ht.Add("Bind", "");
            ht.Add("NotBind", "");
            switch (TerBindType)
            {
                case "2":
                    ht["Bind"] = "true";
                    break;
                case "3":
                    ht["NotBind"] = "true";
                    break;
            }
            ht.Add("StartData", tiv.StartData);
            ht.Add("EndData", tiv.EndData);
            if (tiv.DeptId == null || tiv.DeptId.Trim() == "")
            {
                ht["DeptId"] = user.EnterId;
            }
            if (ChildrenSel == "true")
            {
                string deptid = tiv.DeptId;
                if (tiv.DeptId == null || tiv.DeptId.Trim() == "")
                {
                    deptid = user.EnterId;
                }
                DeptInfo di = deptInfoBll.GetDeptInfo(deptid);
                if (di != null)
                {
                    ht["Businessdivisioncode"] = di.Businessdivisioncode;
                    ht["DeptId"] = "";
                }
            }
            if (IsCheckNos == "true" && CheckTernos != null && CheckTernos != "")
            {
                string[] TerNos = CheckTernos.Split(',');
                string checknos = "";
                foreach (string ternostr in TerNos)
                {
                    if (!string.IsNullOrEmpty(ternostr))
                    {
                        checknos += "'" + ternostr + "',";
                    }
                }
                ht["CheckTernos"] = checknos.Trim(',');
            }
            IList<TerminalInfoView> ltiv = terminalInfoViewBll.GetTerminalInfoViewPage(ht);

            ExcelUpLoad eu = new ExcelUpLoad();
            MemoryStream ms = eu.CreateTerExcel(ltiv);
            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/终端信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/终端信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/终端信息" + xlsName + ".xlsx";
                return ppphhh;
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }

        public string GetTerSelList(TerminalInfoView tiv)
        {
            IList<TerminalInfoView> ltiv = terminalInfoViewBll.GetTerminalInfoViewPage(tiv);
            return ConvertToJson(ltiv);
        }

        public string GetNotBindTerList(TerminalInfoView tiv, int? rows, int? page)
        {
            if (rows != null && page != null)
            {
                tiv.StartData = ((int)page - 1) * (int)rows + 1;
                tiv.EndData = tiv.StartData + (int)rows;
            }

            if (tiv.DeptId != null && tiv.DeptId.Trim() != "")
            {
                IList<TerminalInfoView> ltiv = terminalInfoViewBll.GetNotBindTerminalInfoViewPage(tiv);
                int count = terminalInfoViewBll.GetNotBindTerminalInfoViewCount(tiv);
                TerminalInfoView tivnull = new TerminalInfoView();
                ltiv.Add(tivnull);
                return ConvertToJson(ltiv, count);
            }
            else
            {
                return "[]";
            }
        }

        public string GetNotBindTerListTree(TerminalInfoView tiv)
        {
            if (tiv.DeptId != null && tiv.DeptId.Trim() != "")
            {
                List<TreeMode> TreeNode = new List<TreeMode>();
                IList<TerminalInfoView> ltiv = terminalInfoViewBll.GetNotBindTerminalInfoViewPage(tiv);
                if (ltiv != null)
                {
                    foreach (TerminalInfoView terinfoview in ltiv)
                    {
                        TreeMode tm = new TreeMode("");
                        tm.id = terinfoview.TerGuid;
                        tm.text = terinfoview.TerNo;
                        TreeNode.Add(tm);
                    }
                    return ConvertToJson(TreeNode);
                }
                else
                {
                    return "[]";
                }
            }
            else
            {
                return "[]";
            }
        }

        public string GetDeptTerList(string DeptId)
        {
            if (DeptId != null && DeptId.Trim() != "")
            {
                List<TreeMode> TreeNode = new List<TreeMode>();
                IList<TerminalInfo> ltiv = terminalInfoBll.GetTerminalInfoByDeptId(DeptId.Trim());
                if (ltiv != null)
                {
                    foreach (TerminalInfo terinfo in ltiv)
                    {
                        TreeMode tm = new TreeMode("");
                        tm.id = terinfo.TerGuid;
                        tm.text = terinfo.TerNo;
                        TreeNode.Add(tm);
                    }
                    return ConvertToJson(TreeNode);
                }
                else
                {
                    return "[]";
                }
            }
            else
            {
                return "[]";
            }
        }

        [HttpGet]
        public ActionResult addTerminal()
        {
            return View();
        }

        [Log(LogMessage = "添加终端信息")]
        [HttpPost]
        public ActionResult TerminalForm(TerminalInfo ti)
        {
            if (ti != null)
            {
                ti.TerGuid = System.Guid.NewGuid().ToString();
                if (ti.TerNo == null || ti.TerNo.Trim() == "")
                {
                    ModelState.AddModelError("", "添加失败");
                    return JavaScript("editFormError('终端编号不能为空，');");
                }
                else if (ti.TerTypeid == null || ti.TerTypeid.Trim() == "")
                {
                    ModelState.AddModelError("", "添加失败");
                    return JavaScript("editFormError('终端类型不能为空，');");
                }
                else if (ti.DeptId == null || ti.DeptId.Trim() == "")
                {
                    ModelState.AddModelError("", "添加失败");
                    return JavaScript("editFormError('所属企业不能为空，');");
                }
                else if (ti.TerSimcard == null || ti.TerSimcard.Trim() == "")
                {
                    ModelState.AddModelError("", "添加失败");
                    return JavaScript("editFormError('SIM卡不能为空，');");
                }
                else
                {
                    ti.TerNo = ti.TerNo.Trim();
                    DeptInfo di = deptInfoBll.GetDeptInfo(ti.DeptId);
                    ti.TerDeptcode = di.Businessdivisioncode;
                    ti.TerInnettime = DateTime.Now;
                    int k = terminalInfoBll.Insert(ti);
                    if (k == 0)
                    {
                        UserInfo user = (UserInfo)Session["LoginUser"];
                        string tertype = "";
                        if (ti.TerTypeid == "0")   //一代无线GPS
                        {
                            tertype = "104";
                        }
                        else if (ti.TerTypeid == "1") //二代无线GPS
                        {
                            tertype = "102";
                        }
                        else if (ti.TerTypeid == "2" || ti.TerTypeid == "3")  //Homer3M 和 Homer3B-2
                        {
                            tertype = "101";
                        }
                        else if (ti.TerTypeid == "4") //五代无线GPS
                        {
                            tertype = "112";
                        }
                        else if (ti.TerTypeid == "5") //五代有线GPS
                        {
                            tertype = "111";
                        }
                        //添加终端的接口
                        Transfers.ClintSendCommData(1108, "2", "1", ti.TerNo, "", "", "", ti.TerInnettime.ToString("yyyy-MM-dd HH:mm:ss"), "", "", "", ti.TerDeptcode, ti.TerSimcard, tertype, "", "", "", "", user.UserName);

                        //刷新车辆
                        Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "2", "", "", "", "", "", "");
                        return JavaScript("submitTerminalSucceed();");
                    }
                    else if (k == -2)
                    {
                        ModelState.AddModelError("", "添加失败");
                        return JavaScript("editFormError('终端编号重复');");
                    }
                    else
                    {
                        ModelState.AddModelError("", "添加失败");
                        return JavaScript("editFormError();");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "添加失败");
                return JavaScript("editFormError('信息不完整');");
            }
        }

        /// <summary>
        /// 编辑窗口
        /// </summary>
        /// <param name="uf"></param>
        /// <returns></returns>
        public ActionResult EditTerminal(string TerGuid,string flag)
        {
            TerminalInfo ti = terminalInfoBll.GetTerminalInfo(TerGuid);
            ViewBag.Deptid = ti.DeptId;
            ViewBag.Type = ti.TerTypeid;
            ViewBag.flag = flag;
            return View(ti);
        }

        [Log(LogMessage = "修改终端信息")]
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="uf"></param>
        /// <returns></returns>
        public ActionResult EditTerForm(TerminalInfo ti)
        {
            ti.TerNo = ti.TerNo.Trim();

            if (ti.DeptId != null && ti.DeptId != "")
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(ti.DeptId);
                ti.TerDeptcode = di.Businessdivisioncode;
            }
            if (string.IsNullOrEmpty(ti.TerSimcard))
            {
                ModelState.AddModelError("", "修改失败");
                return JavaScript("editFormError('SIM卡号不能为空!');");
            }
            TerminalInfo oldter = terminalInfoBll.GetTerminalInfo(ti.TerGuid);
            int a = terminalInfoBll.Update(ti);
            if (a > 0)
            {
                UserInfo user = (UserInfo)Session["LoginUser"];
                string tertype = "";
                if (ti.TerTypeid == "0") //一代无线GPS
                {
                    tertype = "104";
                }
                else if (ti.TerTypeid == "1") //二代无线GPS
                {
                    tertype = "102";
                }
                else if (ti.TerTypeid == "2" || ti.TerTypeid == "3")  //Homer3M 和 Homer3B-2
                {
                    tertype = "101";
                }
                else if (ti.TerTypeid == "4")  //五代无线GPS
                {
                    tertype = "112";
                }
                else if (ti.TerTypeid == "5")  //五代有线GPS
                {
                    tertype = "111";
                }
                //修改终端接口
                Transfers.ClintSendCommData(1108, "2", "2", oldter.TerNo, ti.TerNo, "", "", "", "", "", "", ti.TerDeptcode, ti.TerSimcard, tertype, "", "", "", "", user.UserName);

                //刷新车辆
                Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "2", "", "", "", "", "", "");
                return JavaScript("editFormSucceed();");
            }
            else if (a == -1)
            {
                //TerNo 不能重复
                ModelState.AddModelError("", "修改失败");
                return JavaScript("editFormError('终端号["+ ti.TerNo +"]已存在!');");
            }
            else
            {
                ModelState.AddModelError("", "修改失败");
                return JavaScript("editFormError();");
            }
        }

        [Log(LogMessage = "删除终端信息")]
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="FieldId"></param>
        /// <returns></returns>
        public string DeleteTer(string TerGuid)
        {
            TerGuid = TerGuid.Trim(',');
            if (TerGuid != null && TerGuid.Trim() != "")
            {
                UserInfo user = (UserInfo)Session["LoginUser"];
                string[] terguids = TerGuid.Split(',');
                string ternos = "";
                foreach (string terguid in terguids)
                {
                    TerminalInfo terinfo = terminalInfoBll.GetTerminalInfo(terguid);
                    if (terinfo.TerNo != "")
                    {
                        ternos += terinfo.TerNo + "&";
                    }
                }
                string result = terminalInfoBll.Delete(TerGuid);

                new LogMessage().Save("ID:" + TerGuid + "。");

                //删除终端接口
                Transfers.ClintSendCommData(1108, "2", "0", ternos, "", "", "", "", "", "", "", "", "", "", "", "", "", "", user.UserName);
                //刷新车辆
                Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "2", "", "", "", "", "", "");
                return result;
            }
            else
            {
                return "Error";
            }
        }

        public ActionResult TerPct(string TerNos)
        {
            ViewBag.TerNos = TerNos;
            return View();
        }

        [Log(LogMessage = "设置里程信息")]
        [HttpPost]
        public string SetPct(string TerNos,string Pct)
        {
            TerNos = TerNos.Replace(',', '&');
            Transfers.ClintSendCommData(1107, "47", "", TerNos, "", "", "", "", "", "", "", Pct, "", "", "", "", "", "", "");
            return "Success";
        }

        [Log(LogMessage = "绑定终端车辆信息")]
        public string BindTerCar(string TerIds, string CarId)
        {
            string result = terminalInfoBll.BindTerCar(TerIds, CarId);

            new LogMessage().Save("Ids:" + TerIds + "。");

            //刷新车辆
            Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "2", "", "", "", "", "", "");
            
            return result;
        }

        public ActionResult TerExChange(string TerGuid, string DeptId)
        {
            ViewBag.TerGuid = TerGuid;
            ViewBag.DeptId = DeptId;
            return View();
        }

        public ActionResult TerChange()
        {
            return View();
        }



        [Log(LogMessage = "操作终端企业流转")]
        /// <summary>
        /// 终端企业流转
        /// </summary>
        /// <returns></returns>
        public int TerExChangeForm(string TerGuid, string DeptId)
        {
            TerminalInfo entity = new TerminalInfo();
            if (TerGuid != null && TerGuid.Trim() != "" && DeptId != null && DeptId.Trim() != "")
            {
                entity.TerGuid = TerGuid;
                entity.DeptId = DeptId;
                DeptInfo di = deptInfoBll.GetDeptInfo(DeptId);
                entity.TerDeptcode = di.Businessdivisioncode;
                if (di.DepType == "1")
                {
                    entity.TerOvertime = DateTime.Now;
                }
                else
                {
                    entity.TerOvertime = DateTime.Parse("1900-01-01");
                }
                UserInfo user = (UserInfo)Session["LoginUser"];
                string ternos = "";
                string[] terguids = TerGuid.Trim(',').Split(',');
                foreach (string terguid in terguids)
                {
                    TerminalInfo terinfo = terminalInfoBll.GetTerminalInfo(terguid);
                    if (terinfo.TerNo != "")
                    {
                        ternos += terinfo.TerNo + "&";
                    }
                }

                DeptInfo scdi = deptInfoBll.GetDeptInfo("dabf8b57-75a3-43f8-b540-03fefc9e43c3"); //生产测试的类
                if (entity.TerDeptcode.StartsWith(scdi.Businessdivisioncode))
                {
                    int c = terminalInfoBll.TerExChange(entity, true);
                    //终端流转调用的接口
                    Transfers.ClintSendCommData(1108, "2", "3", ternos, "", "", "", "", "", "", "", di.Businessdivisioncode, "", "", "", "", "", "", user.UserName);
                    //刷新车辆和终端
                    Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "2", "", "", "", "", "", "");
                    return c;
                }
                else
                {
                    int c = terminalInfoBll.TerExChange(entity, false);
                    //终端流转调用的接口
                    Transfers.ClintSendCommData(1108, "2", "3", ternos, "", "", "", "", "", "", "", di.Businessdivisioncode, "", "", "", "", "", "", user.UserName);
                    //刷新车辆和终端
                    Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "2", "", "", "", "", "", "");
                    return c;
                }
            }
            else
            {
                return 0;
            }
        }

        public ActionResult TerminalMonitor(TerminalInfo tif)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
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

        public int GetTerminalCountByCarId(string CarId)
        {
            return terminalInfoBll.GetTerminalCountByCarId(CarId);
        }

        [Log(LogMessage = "解除终端绑定")]
        /// <summary>
        /// 解除绑定
        /// </summary>
        /// <param name="TerGuids"></param>
        /// <returns></returns>
        public string RemoveBind(string TerGuids)
        {
            string result = terminalInfoBll.RemoveBind(TerGuids);

            new LogMessage().Save("TerGuids:" + TerGuids + "。");

            //解绑接口
            string[] guids = TerGuids.Split(',');
            TerminalInfoBLL terbll = new TerminalInfoBLL();
            UserInfo user = (UserInfo)Session["LoginUser"];
            foreach (string terGuid in guids)
            {
                if (terGuid != "")
                {
                    TerminalInfo terinfo = terbll.GetTerminalInfo(terGuid);
                    Transfers.ClintSendCommData(1107, "501", "", terinfo.TerNo, "", "", "", "", "", "", "", "", "", "", "", "", "", "", user.UserName);
                }
            }
            //刷新车辆
            Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "2", "", "", "", "", "", "");
            return result;
        }
    }
}