using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GBLL.Location;
using GModel.Location;
using System.Collections;
using GBLL;
using System.Data;
using SuperGPS.App_Start;
using GModel.Basic;
using GCommon;
using System.IO;
using System.Text;

namespace SuperGPS.Controllers
{
    public class HistoryTrailController : BaseController
    {
        HistoricalDataBLL historicalDataBll = new HistoricalDataBLL();
        ColligateQueryService c = new ColligateQueryService();
        RealtimeDataBLL realtimeDataBll = new RealtimeDataBLL();

        // GET: HistoryTrail
        public ActionResult TrailIndex()
        {
            return View();
        }

        public ActionResult TrailNewIndex(string CarId, string TerNo)
        {
            ViewBag.CarId = CarId;
            ViewBag.TerNo = TerNo;
            return View();
        }

        public ActionResult BDTrailNewIndex(string CarId, string TerNo)
        {
            ViewBag.CarId = CarId;
            ViewBag.TerNo = TerNo;
            return View();
        }

        public ActionResult YXTrailNewIndex(string CarId, string TerNo)
        {
            ViewBag.CarId = CarId;
            ViewBag.TerNo = TerNo;
            return View();
        }

        public ActionResult BDYXTrailNewIndex(string CarId, string TerNo)
        {
            ViewBag.CarId = CarId;
            ViewBag.TerNo = TerNo;
            return View();
        }

        /// <summary>
        /// 查询历史轨迹
        /// </summary>
        /// <param name="CarId"></param>
        /// <param name="TerNo"></param>
        /// <param name="isStart"></param>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <returns></returns>
        [Log(LogMessage = "查看历史轨迹信息")]
        public string GetTrailList(string CarId, string TerNo, bool isStart, DateTime st, DateTime ed)
        {
            //得到本车最新上传数据的终端编号
            //string NewEastTerNo = "";
            //if (CarId != "null" && CarId.Trim() != "")
            //{
            //    DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("SELECT TI.TER_NO FROM REALTIME_DATA RD JOIN TERMINAL_INFO TI ON RD.TER_NO = TI.TER_NO JOIN CAR_INFO CI ON TI.CAR_ID = CI.CAR_ID and ti.car_id='{0}' order by rd.rtime desc", CarId));
            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        NewEastTerNo = ds.Tables[0].Rows[0][0].ToString();
            //    }
            //}
            //else
            //{
            //    NewEastTerNo = TerNo;
            //}

            Hashtable ht = new Hashtable();
            ht.Add("TerNo", TerNo);
            if (isStart == true)
            {
                ht.Add("EndData",31);
                ht.Add("StartData", 0);
            }
            else
            {
                if (ed != null)
                {
                    ht.Add("EndData", 0);
                    ht.Add("st", st.ToString("yyyy-MM-dd HH:mm:ss"));
                    ht.Add("ed", ed.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            IList<HistoricalData> ihrd = historicalDataBll.GetHistorical(ht);
            return ConvertToJson(ihrd);
        }

        /// <summary>
        /// 查询有线历史轨迹
        /// </summary>
        /// <param name="CarId"></param>
        /// <param name="TerNo"></param>
        /// <param name="isStart"></param>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <returns></returns>
        [Log(LogMessage = "查看历史轨迹信息")]
        public string GetYXTrailList(string CarId, string TerNo,DateTime st, DateTime ed)
        {
            //得到本车最新上传数据的终端编号
            //string NewEastTerNo = "";
            //if (CarId != "null" && CarId.Trim() != "")
            //{
            //    DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("SELECT TI.TER_NO FROM REALTIME_DATA RD JOIN TERMINAL_INFO TI ON RD.TER_NO = TI.TER_NO JOIN CAR_INFO CI ON TI.CAR_ID = CI.CAR_ID and ti.car_id='{0}' order by rd.rtime desc", CarId));
            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        NewEastTerNo = ds.Tables[0].Rows[0][0].ToString();
            //    }
            //}
            //else
            //{
            //    NewEastTerNo = TerNo;
            //}

            string strTrackName = "";
            string StartDate = "";
            string EndDate = "";
            List<HistoricalData> Listhrd = new List<HistoricalData>();
            do
            {
                if (st.ToString("yyyyMMdd") == ed.ToString("yyyyMMdd"))
                {
                     strTrackName = "ZTRACK" + st.ToString("yyyyMMdd");
                     StartDate = st.ToString("yyyy-MM-dd HH:mm:ss");
                     EndDate = ed.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                     strTrackName = "ZTRACK" + st.ToString("yyyyMMdd");
                     StartDate = st.ToString("yyyy-MM-dd HH:mm:ss");
                     EndDate = st.ToString("yyyy-MM-dd") + " " + " 23:59:59";
                }
                DataSet dsYXTrailCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select * from user_tab_cols where table_name='" + strTrackName + "' and column_name='TER_NO'"));
                if (dsYXTrailCount != null && dsYXTrailCount.Tables[0].Rows.Count > 0)
                {
                    DataSet dsYXTrail = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select ID AS Id,RTIME AS Rtime,nvl(POSITION,'设备周边无地理信息描述') AS Position,TER_NO AS TerNo,"
                        + "REPLYDATANAME as ReplydataName,IFPOSITION AS Ifposition,BAIDU_LATITUDE AS BaiduLatitude,BAIDU_LONGITUDE AS BaiduLongitude,"
                        + "GOOGLE_LATITUDE AS GoogleLatitude,GOOGLE_LONGITUDE AS GoogleLongitude from " + strTrackName + " where (GOOGLE_LATITUDE>15 and GOOGLE_LATITUDE<55) and (GOOGLE_LONGITUDE>70 and GOOGLE_LONGITUDE<140)"
                        + " and TER_NO='" + TerNo + "' and RTIME BETWEEN to_date('" + StartDate + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + EndDate + "','yyyy-mm-dd hh24:mi:ss') order by RTIME desc"));
                    if (dsYXTrail != null && dsYXTrail.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsYXTrail.Tables[0].Rows)
                        {
                            HistoricalData hd = new HistoricalData();
                            hd.Rtime = DateTime.Parse(dr["Rtime"].ToString());
                            hd.Position = dr["Position"].ToString();
                            hd.TerNo = dr["TerNo"].ToString();
                            hd.ReplydataName = dr["ReplydataName"].ToString();
                            hd.Ifposition = dr["Ifposition"].ToString();
                            hd.GoogleLatitude = double.Parse(dr["GoogleLatitude"].ToString());
                            hd.GoogleLongitude = double.Parse(dr["GoogleLongitude"].ToString());
                            hd.BaiduLatitude = double.Parse(dr["BaiduLatitude"].ToString());
                            hd.BaiduLongitude = double.Parse(dr["BaiduLongitude"].ToString());
                            Listhrd.Add(hd);
                        }
                    }
                }
                else
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("TerNo", TerNo);
                    ht.Add("EndData", 0);
                    ht.Add("st", StartDate);
                    ht.Add("ed", EndDate);
                    IList<HistoricalData> ihrd = historicalDataBll.GetHistorical(ht);
                    foreach(HistoricalData hid in ihrd)
                    {
                        Listhrd.Add(hid);
                    }
                }
                st = st.AddDays(1);
            }
            while (st <= ed);
            //string strTrackName = "ZTRACK" + st.ToString("yyyyMMdd");
            //string StartDate = st.ToString("yyyy-MM-dd HH:mm:ss");
            //string EndDate = ed.ToString("yyyy-MM-dd HH:mm:ss");

            return ConvertToJson(Listhrd);

        }

        public string GetRealData(string TerNo, DateTime st)
        {
            Hashtable ht = new Hashtable();
            ht.Add("TerNo", TerNo);
            ht.Add("st", st.ToString("yyyy-MM-dd HH:mm:ss"));

            historicalDataBll.GetHistorical(ht);


            return "";
        }

        [Log(LogMessage = "查看历史轨迹信息")]
        [UserFilter]
        public string GetTerHistoryData(string TerNo, string DeptId, int rows, int page, string st, string ed)
        {
            Hashtable ht = new Hashtable();
            if (DeptId != null && DeptId.Trim() != "")
            {
                ht.Add("DeptId", DeptId);
            }
            else
            {
                UserInfo user = new UserInfo();
                user = (UserInfo)Session["LoginUser"];
                ht.Add("DeptId", user.EnterId);
            }

            ht.Add("StartData", (page - 1) * rows + 1);
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);

            if (TerNo != null && TerNo.Trim() != "")
            {
                ht.Add("TerNo", TerNo);
            }
            else
            {
                ht.Add("TerNo", "");
            }

            if (st != "")
            {
                ht.Add("st", st);
            }
            if (ed != "")
            {
                ht.Add("ed", ed);
            }

            IList<TerData> iltd = historicalDataBll.GetTerHistoryData(ht);
            int count = historicalDataBll.GetTerHistoryDataCount(ht);
            return ConvertToJson(iltd, count);
        }

        [Log(LogMessage = "查看终端里程信息")]
        [UserFilter]
        public string GetTerMilageData(string TerNo, string DeptId, int rows, int page, string st, string ed)
        {
            Hashtable ht = new Hashtable();
            if (DeptId != null && DeptId.Trim() != "")
            {
                ht.Add("DeptId", DeptId);
            }
            else
            {
                UserInfo user = new UserInfo();
                user = (UserInfo)Session["LoginUser"];
                ht.Add("DeptId", user.EnterId);
            }

            ht.Add("StartData", (page - 1) * rows + 1);
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);

            if (TerNo != null && TerNo.Trim() != "")
            {
                ht.Add("TerNo", TerNo);
            }
            else
            {
                ht.Add("TerNo", "");
            }
            var StartMilage = "0";
            var EndMilage = "0";
            if (st != "")
            {
                var StartTableName = "ZTRACK" + DateTime.Parse(st).ToString("yyyyMMdd");
                var StartDate = DateTime.Parse(st).ToString("yyyy-MM-dd HH:mm:ss");
                DataSet dsMilageTableCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select * from user_tab_cols where table_name='" + StartTableName + "' and column_name='TER_NO'"));
                if (dsMilageTableCount != null && dsMilageTableCount.Tables[0].Rows.Count > 0)
                { 
                    if (ht["TerNo"].ToString() != "")
                    {
                        DataSet dsMilageData = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select REMAINL_PCT from (select * from " + StartTableName + " where TER_NO='" + ht["TerNo"].ToString() + "' and RTIME>to_date('" + StartDate + "','yyyy-mm-dd hh24:mi:ss') order by RTIME asc) where rownum=1"));
                        if (dsMilageData != null && dsMilageData.Tables[0].Rows.Count > 0)
                        {
                            StartMilage = dsMilageData.Tables[0].Rows[0][0].ToString();
                        }
                    }
                }
            }
            if (ed != "")
            {
                var EndtableName = "ZTRACK" + DateTime.Parse(ed).ToString("yyyyMMdd");
                var EndDate = DateTime.Parse(ed).ToString("yyyy-MM-dd HH:mm:ss");
                DataSet dsMilageTableCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select * from user_tab_cols where table_name='" + EndtableName + "' and column_name='TER_NO'"));
                if (dsMilageTableCount != null && dsMilageTableCount.Tables[0].Rows.Count > 0)
                {
                    if (ht["TerNo"].ToString() != "")
                    {
                        DataSet dsMilageData = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select REMAINL_PCT from (select * from " + EndtableName + " where TER_NO='" + ht["TerNo"].ToString() + "' and RTIME<to_date('" + EndDate + "','yyyy-mm-dd hh24:mi:ss') order by RTIME desc) where rownum=1"));
                        if (dsMilageData != null && dsMilageData.Tables[0].Rows.Count > 0)
                        {
                            EndMilage = dsMilageData.Tables[0].Rows[0][0].ToString();
                        }
                    }
                }
            }
            //计算里程
            int intDist = int.Parse(EndMilage, System.Globalization.NumberStyles.HexNumber) - int.Parse(StartMilage, System.Globalization.NumberStyles.HexNumber);
            if (intDist < 0)
            {
                intDist = 0;
            }
            string strDist = string.Format("{0,8:0.00}", (float)intDist / 1000);

            StringBuilder sb = new StringBuilder();
            StringBuilder sbCount = new StringBuilder();
            if ((int)ht["EndData"] > 0)
            {
                sb.Append("select * from (select op_a.*, rownum rn from (");
            }
            sb.Append("select TER_NO as TerNo,TER_CARNO as CarNo," + strDist + " as Milage from terminal_info where 1=1");
            sbCount.Append("select count(*) from terminal_info where 1=1");
            if (ht["TerNo"].ToString() != "")
            {
                sb.Append(" and TER_NO='" + ht["TerNo"].ToString() + "'");
                sbCount.Append(" and TER_NO='" + ht["TerNo"].ToString() + "'");
            }
            if ((int)ht["EndData"] > 0)
            {
                sb.Append(") op_a) op_b Where op_b.rn < " + ht["EndData"] + " And op_b.rn >=" + ht["StartData"]);
            }
            List<MilageData> Listmd = new List<MilageData>();
            DataSet dsMilage = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString());
            DataSet dsMilageNum = c.GetColligateQuery("ColligateQuery.ProteanQuery", sbCount.ToString());
            if (dsMilage != null && dsMilage.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsMilage.Tables[0].Rows)
                {
                    MilageData md = new MilageData();
                    md.TerNo = dr["TerNo"].ToString();
                    md.CarNo = dr["CarNo"].ToString();
                    md.Milage = dr["Milage"].ToString();
                    Listmd.Add(md);
                }
                int num = Convert.ToInt32(dsMilageNum.Tables[0].Rows[0][0].ToString());
                return ConvertToJson(Listmd, num);
            }
            else
            {
                return ConvertToJson(Listmd,0);
            }
        }

        public string GetMilageByEnddate(string TerNo,string ed)
        {
            var EndMilage = "0";
            var EndtableName = "ZTRACK" + DateTime.Parse(ed).ToString("yyyyMMdd");
            var EndDate = DateTime.Parse(ed).ToString("yyyy-MM-dd") + " 23:59:59";
            DataSet dsMilageTableCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select * from user_tab_cols where table_name='" + EndtableName + "' and column_name='TER_NO'"));
            if (dsMilageTableCount != null && dsMilageTableCount.Tables[0].Rows.Count > 0)
            {
                DataSet dsMilageData = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select REMAINL_PCT from (select * from " + EndtableName + " where TER_NO='" + TerNo.Trim() + "' and RTIME<to_date('" + EndDate + "','yyyy-mm-dd hh24:mi:ss') order by RTIME desc) where rownum=1"));
                if (dsMilageData != null && dsMilageData.Tables[0].Rows.Count > 0)
                {
                    EndMilage = dsMilageData.Tables[0].Rows[0][0].ToString();
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }

        public class MilageData
        {
            public string TerNo{ set; get;}
            public string CarNo { set; get; }
            public string Milage { set; get; }
        }

        [Log(LogMessage = "查看历史轨迹信息")]
        [UserFilter]
        public string GetYXHistoryData(string TerNo, string DeptId, int rows, int page, string st, string ed)
        {
            Hashtable ht = new Hashtable();
            if (DeptId != null && DeptId.Trim() != "")
            {
                ht.Add("DeptId", DeptId);
            }
            else
            {
                UserInfo user = new UserInfo();
                user = (UserInfo)Session["LoginUser"];
                ht.Add("DeptId", user.EnterId);
            }

            if (TerNo != null && TerNo.Trim() != "")
            {
                ht.Add("TerNo", TerNo);
            }
            else
            {
                ht.Add("TerNo", "");
            }
            ht.Add("StartData", (page - 1) * rows + 1); 
            ht.Add("EndData", Convert.ToInt32(ht["StartData"]) + rows);

            string strTrackName = "";
            string NewStartDate = "";
            string NewEndDate = "";
            if (st == "")
            {
                 strTrackName = "ZTRACK" + DateTime.Now.ToString("yyyyMMdd");
                 NewStartDate=DateTime.Now.ToString("yyyyMMdd") + " 00:00:00";
                 NewEndDate = DateTime.Now.ToString("yyyyMMdd") + " 23:59:59";
            }
            else
            {
                 strTrackName = "ZTRACK" + DateTime.Parse(st).ToString("yyyyMMdd");
                 NewStartDate = DateTime.Parse(st).ToString("yyyy-MM-dd HH:mm:ss");
                 NewEndDate = DateTime.Parse(ed).ToString("yyyy-MM-dd HH:mm:ss");
            }
            ht.Add("table", strTrackName);
            ht.Add("st", NewStartDate);
            ht.Add("ed", NewEndDate);
            List<YXHistoricalData> Listhrd = new List<YXHistoricalData>();
            DataSet dsYXTrailCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select * from user_tab_cols where table_name='" + strTrackName + "' and column_name='TER_NO'"));
            if (dsYXTrailCount != null && dsYXTrailCount.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sbCount = new StringBuilder();

                if ((int)ht["EndData"] > 0)
                {
                    sb.Append("select * from (select op_a.*, rownum rn from (");
                }
                sb.Append("select ZTK.ID AS Id,ZTK.RTIME AS Rtime,nvl(ZTK.POSITION,'设备周边无地理信息描述') AS Position,ZTK.TER_NO AS TerNo,ZTK.REPLYDATANAME as ReplydataName,ZTK.IFPOSITION AS Ifposition,");
                sb.Append("ZTK.NORTHORSOUTH AS Northorsouth,ZTK.EASTORWEST AS Eastorwest,ZTK.SPEED AS Speed,ZTK.DIRECTION AS Direction,ZTK.TER_VBATT AS TerVbatt,ZTK.GSMRSSI AS Gsmrssi,ZTK.PROGRAMVERSON AS Programverson,ZTK.GPSVERSON AS Gpsverson,ZTK.PROTOCOLVERSION AS Protocolversion,ZTK.ACCSTATE AS Accstate,");
                sb.Append("ZTK.LATITUDE AS Latitude,ZTK.LONGITUDE AS Longitude,ZTK.BAIDU_LATITUDE AS BaiduLatitude,ZTK.BAIDU_LONGITUDE AS BaiduLongitude,ZTK.GOOGLE_LATITUDE AS GoogleLatitude,ZTK.GOOGLE_LONGITUDE AS GoogleLongitude,TI.TER_SIMCARD AS TerSimcard,TI.TER_INNETTIME AS Ter_Innettime from " + strTrackName + " ZTK JOIN TERMINAL_INFO TI ON ZTK.TER_NO=TI.TER_NO where 1=1");
                sbCount.Append("select count(*) from " + strTrackName + " ZTK JOIN TERMINAL_INFO TI ON ZTK.TER_NO=TI.TER_NO where 1=1");
                if (ht["TerNo"].ToString() != "")
                {
                    sb.Append(" and TI.TER_NO='" + ht["TerNo"].ToString() + "'");
                    sbCount.Append(" and TI.TER_NO='" + ht["TerNo"].ToString() + "'");
                }
                if (ht["DeptId"].ToString() != "")
                {
                    sb.Append(" and TI.DEPT_ID ='" + ht["DeptId"].ToString() + "'");
                    sbCount.Append(" and TI.DEPT_ID ='" + ht["DeptId"].ToString() + "'");
                }
                if (NewStartDate != "")
                {
                    sb.Append(" and ZTK.RTIME >=to_date('" + NewStartDate + "','yyyy-mm-dd hh24:mi:ss')");
                    sbCount.Append(" and ZTK.RTIME >=to_date('" + NewStartDate + "','yyyy-mm-dd hh24:mi:ss')");
                }
                if (NewEndDate != "")
                {
                    sb.Append(" and ZTK.RTIME <=to_date('" + NewEndDate + "','yyyy-mm-dd hh24:mi:ss')");
                    sbCount.Append(" and ZTK.RTIME <=to_date('" + NewEndDate + "','yyyy-mm-dd hh24:mi:ss')");
                }
                sb.Append(" order by ZTK.RTIME desc");
                if ((int)ht["EndData"] > 0)
                {
                    sb.Append(") op_a) op_b Where op_b.rn < " + ht["EndData"] + " And op_b.rn >=" + ht["StartData"]);
                }
                DataSet dsYXTrail = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString());
                DataSet dsYXTrailNum = c.GetColligateQuery("ColligateQuery.ProteanQuery", sbCount.ToString());
                if (dsYXTrail != null && dsYXTrail.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsYXTrail.Tables[0].Rows)
                    {
                        YXHistoricalData yxhd = new YXHistoricalData();
                        yxhd.Rtime = DateTime.Parse(dr["Rtime"].ToString());
                        yxhd.Position = dr["Position"].ToString();
                        yxhd.TerNo = dr["TerNo"].ToString();
                        yxhd.ReplydataName = dr["ReplydataName"].ToString();
                        yxhd.Ifposition = dr["Ifposition"].ToString();
                        yxhd.Northorsouth = dr["Northorsouth"].ToString();
                        yxhd.Eastorwest = dr["Eastorwest"].ToString();
                        yxhd.Speed = dr["Speed"].ToString();
                        yxhd.Direction = dr["Direction"].ToString();
                        yxhd.TerVbatt = dr["TerVbatt"].ToString();
                        yxhd.Gsmrssi = dr["Gsmrssi"].ToString();
                        yxhd.Programverson = dr["Programverson"].ToString();
                        yxhd.Gpsverson = dr["Gpsverson"].ToString();
                        yxhd.Latitude = dr["Latitude"].ToString();
                        yxhd.Longitude = dr["Longitude"].ToString();
                        yxhd.GoogleLatitude = dr["GoogleLatitude"].ToString();
                        yxhd.GoogleLongitude = dr["GoogleLongitude"].ToString();
                        yxhd.BaiduLatitude = dr["BaiduLatitude"].ToString();
                        yxhd.BaiduLongitude = dr["BaiduLongitude"].ToString();
                        yxhd.TerSimcard = dr["TerSimcard"].ToString();
                        yxhd.Ter_Innettime = DateTime.Parse(dr["Ter_Innettime"].ToString());
                        Listhrd.Add(yxhd);
                    }
                    int num=Convert.ToInt32(dsYXTrailNum.Tables[0].Rows[0][0].ToString());
                    return ConvertToJson(Listhrd, num);
                }
                else
                {
                    return ConvertToJson(Listhrd, Listhrd.Count);
                }
            }
            else
            {
                IList<TerData> iltd = historicalDataBll.GetTerHistoryData(ht);
                int count = historicalDataBll.GetTerHistoryDataCount(ht);
                return ConvertToJson(iltd, count);
            }
        }

        [UserFilter]
        public ActionResult RealTrail(string CarNo, string TerNo)
        {
            ViewBag.TerNo = TerNo;
            return View();
        }

        [UserFilter]
        public ActionResult BDRealTrail(string TerNo)
        {
            ViewBag.TerNo = TerNo;
            return View();
        }

        public string GetRealtimeData(string TerNo)
        {
            RealtimeData rd = realtimeDataBll.GetRealtimeData(TerNo);
            return ConvertToJson(rd);

        }

        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="rtv"></param>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
         [Log(LogMessage = "导出历史轨迹信息")]
        public string DownLoadExcel(string CarId, string TerNo, bool isStart, DateTime st, DateTime ed)
        {
            //得到本车最新上传数据的终端编号
            //string NewEastTerNo = "";
            //if (CarId != "null" && CarId.Trim() != "")
            //{
            //    DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("SELECT TI.TER_NO FROM REALTIME_DATA RD JOIN TERMINAL_INFO TI ON RD.TER_NO = TI.TER_NO JOIN CAR_INFO CI ON TI.CAR_ID = CI.CAR_ID and ti.car_id='{0}' order by rd.rtime desc", CarId));
            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        NewEastTerNo = ds.Tables[0].Rows[0][0].ToString();
            //    }
            //}
            //else
            //{
            //    NewEastTerNo = TerNo;
            //}

            Hashtable ht = new Hashtable();
            ht.Add("TerNo", TerNo);
            if (isStart == true)
            {
                ht.Add("EndData", 31);
                ht.Add("StartData", 0);
            }
            else
            {
                if (ed != null)
                {
                    ht.Add("EndData", 0);
                    ht.Add("st", st.ToString("yyyy-MM-dd HH:mm:ss"));
                    ht.Add("ed", ed.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            IList<HistoricalData> ihrd = historicalDataBll.GetHistorical(ht);

            ExcelUpLoad eu = new ExcelUpLoad();
            MemoryStream ms = eu.CreateExcel(ihrd,0);
            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/历史轨迹信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/历史轨迹信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/历史轨迹信息" + xlsName + ".xlsx";

                new LogMessage().Save("文件:" + ppphhh + "。");

                return ppphhh;
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }

        /// <summary>
        /// 导出EXCEL（有线历史轨迹）
        /// </summary>
        /// <param name="CarId"></param>
        /// <param name="TerNo"></param>
        /// <param name="isStart"></param>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <returns></returns>
        [Log(LogMessage = "导出历史轨迹信息")]
        public string YXDownLoadExcel(string CarId, string TerNo, DateTime st, DateTime ed)
        {
            //得到本车最新上传数据的终端编号
            //string NewEastTerNo = "";
            //if (CarId != "null" && CarId.Trim() != "")
            //{
            //    DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("SELECT TI.TER_NO FROM REALTIME_DATA RD JOIN TERMINAL_INFO TI ON RD.TER_NO = TI.TER_NO JOIN CAR_INFO CI ON TI.CAR_ID = CI.CAR_ID and ti.car_id='{0}' order by rd.rtime desc", CarId));
            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        NewEastTerNo = ds.Tables[0].Rows[0][0].ToString();
            //    }
            //}
            //else
            //{
            //    NewEastTerNo = TerNo;
            //}

            string strTrackName = "ZTRACK" + st.ToString("yyyyMMdd");
            string StartDate = st.ToString("yyyy-MM-dd HH:mm:ss");
            string EndDate = ed.ToString("yyyy-MM-dd HH:mm:ss");
            List<HistoricalData> Listhrd = new List<HistoricalData>();
            DataSet dsYXTrailCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select * from user_tab_cols where table_name='" + strTrackName + "' and column_name='TER_NO'"));
            if (dsYXTrailCount != null && dsYXTrailCount.Tables[0].Rows.Count > 0)
            {
                DataSet dsYXTrail = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select ID AS Id,RTIME AS Rtime,nvl(POSITION,'设备周边无地理信息描述') AS Position,TER_NO AS TerNo,REPLYDATANAME as ReplydataName,IFPOSITION AS Ifposition,BAIDU_LATITUDE AS BaiduLatitude,BAIDU_LONGITUDE AS BaiduLongitude,GOOGLE_LATITUDE AS GoogleLatitude,GOOGLE_LONGITUDE AS GoogleLongitude from " + strTrackName + " where TER_NO='" + TerNo + "' and RTIME BETWEEN to_date('" + StartDate + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + EndDate + "','yyyy-mm-dd hh24:mi:ss') order by RTIME desc"));
                if (dsYXTrail != null && dsYXTrail.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in dsYXTrail.Tables[0].Rows)
                    {
                        HistoricalData hd = new HistoricalData();
                        hd.Rtime = DateTime.Parse(dr["Rtime"].ToString());
                        hd.Position = dr["Position"].ToString();
                        hd.TerNo = dr["TerNo"].ToString();
                        hd.ReplydataName = dr["ReplydataName"].ToString();
                        hd.Ifposition = dr["Ifposition"].ToString();
                        hd.GoogleLatitude = double.Parse(dr["GoogleLatitude"].ToString());
                        hd.GoogleLongitude = double.Parse(dr["GoogleLongitude"].ToString());
                        hd.BaiduLatitude = double.Parse(dr["BaiduLatitude"].ToString());
                        hd.BaiduLongitude = double.Parse(dr["BaiduLongitude"].ToString());
                        Listhrd.Add(hd);
                    }
                }
            }
            else
            {
                Hashtable ht = new Hashtable();
                ht.Add("TerNo", TerNo);
                ht.Add("EndData", 0);
                ht.Add("st", StartDate);
                ht.Add("ed", EndDate);
                IList<HistoricalData> ihrd = historicalDataBll.GetHistorical(ht);
                Listhrd = ihrd.ToList();
            }
           
            ExcelUpLoad eu = new ExcelUpLoad();
            MemoryStream ms = eu.CreateExcel(Listhrd,1);
            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/历史轨迹信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/历史轨迹信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/历史轨迹信息" + xlsName + ".xlsx";

                new LogMessage().Save("文件:" + ppphhh + "。");

                return ppphhh;
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }

        [Log(LogMessage = "导出历史轨迹信息")]
        public string DownLoadExcel2(string TerNo, string DeptId,string st, string ed)
        {
            Hashtable ht = new Hashtable();
            if (DeptId != null && DeptId.Trim() != "")
            {
                ht.Add("DeptId", DeptId);
            }
            else
            {
                UserInfo user = new UserInfo();
                user = (UserInfo)Session["LoginUser"];
                ht.Add("DeptId", user.EnterId);
            }
            ht.Add("EndData",0);
            if (TerNo != null && TerNo.Trim() != "")
            {
                ht.Add("TerNo", TerNo);
            }
            else
            {
                ht.Add("TerNo", "");
            }

            if (st != "")
            {
                ht.Add("st", st);
            }
            if (ed != "")
            {
                ht.Add("ed", ed);
            }
            IList<TerData> iltd = historicalDataBll.GetTerHistoryData(ht);
            ExcelUpLoad eu = new ExcelUpLoad();
            MemoryStream ms = eu.CreateExcel(iltd);
            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/历史轨迹信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/历史轨迹信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/历史轨迹信息" + xlsName + ".xlsx";

                new LogMessage().Save("文件:" + ppphhh + "。");

                return ppphhh;
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }

        [Log(LogMessage = "导出历史轨迹信息")]
        public string YXDownLoadExcel2(string TerNo, string DeptId, string st, string ed)
        {
            Hashtable ht = new Hashtable();
            if (DeptId != null && DeptId.Trim() != "")
            {
                ht.Add("DeptId", DeptId);
            }
            else
            {
                UserInfo user = new UserInfo();
                user = (UserInfo)Session["LoginUser"];
                ht.Add("DeptId", user.EnterId);
            }

            if (TerNo != null && TerNo.Trim() != "")
            {
                ht.Add("TerNo", TerNo);
            }
            else
            {
                ht.Add("TerNo", "");
            }

            string strTrackName = "";
            string NewStartDate = "";
            string NewEndDate = "";
            if (st == "")
            {
                strTrackName = "ZTRACK" + DateTime.Now.ToString("yyyyMMdd");
                NewStartDate = DateTime.Now.ToString("yyyyMMdd") + " 00:00:00";
                NewEndDate = DateTime.Now.ToString("yyyyMMdd") + " 23:59:59";
            }
            else
            {
                strTrackName = "ZTRACK" + DateTime.Parse(st).ToString("yyyyMMdd");
                NewStartDate = DateTime.Parse(st).ToString("yyyy-MM-dd HH:mm:ss");
                NewEndDate = DateTime.Parse(ed).ToString("yyyy-MM-dd HH:mm:ss");
            }
            ht.Add("table", strTrackName);
            ht.Add("st", NewStartDate);
            ht.Add("ed", NewEndDate);
            List<YXHistoricalData> Listhrd = new List<YXHistoricalData>();
            DataSet dsYXTrailCount = c.GetColligateQuery("ColligateQuery.ProteanQuery", string.Format("select * from user_tab_cols where table_name='" + strTrackName + "' and column_name='TER_NO'"));
            if (dsYXTrailCount != null && dsYXTrailCount.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sbCount = new StringBuilder();
                sb.Append("select ZTK.ID AS Id,ZTK.RTIME AS Rtime,nvl(ZTK.POSITION,'设备周边无地理信息描述') AS Position,ZTK.TER_NO AS TerNo,ZTK.REPLYDATANAME as ReplydataName,ZTK.IFPOSITION AS Ifposition,");
                sb.Append("ZTK.NORTHORSOUTH AS Northorsouth,ZTK.EASTORWEST AS Eastorwest,ZTK.SPEED AS Speed,ZTK.DIRECTION AS Direction,ZTK.TER_VBATT AS TerVbatt,ZTK.GSMRSSI AS Gsmrssi,ZTK.PROGRAMVERSON AS Programverson,ZTK.GPSVERSON AS Gpsverson,ZTK.PROTOCOLVERSION AS Protocolversion,ZTK.ACCSTATE AS Accstate,");
                sb.Append("ZTK.LATITUDE AS Latitude,ZTK.LONGITUDE AS Longitude,ZTK.BAIDU_LATITUDE AS BaiduLatitude,ZTK.BAIDU_LONGITUDE AS BaiduLongitude,ZTK.GOOGLE_LATITUDE AS GoogleLatitude,ZTK.GOOGLE_LONGITUDE AS GoogleLongitude,TI.TER_SIMCARD AS TerSimcard,TI.TER_INNETTIME AS Ter_Innettime from " + strTrackName + " ZTK JOIN TERMINAL_INFO TI ON ZTK.TER_NO=TI.TER_NO where 1=1");
                sbCount.Append("select count(*) from " + strTrackName + " ZTK JOIN TERMINAL_INFO TI ON ZTK.TER_NO=TI.TER_NO where 1=1");
                if (ht["TerNo"].ToString() != "")
                {
                    sb.Append(" and TI.TER_NO='" + ht["TerNo"].ToString() + "'");
                    sbCount.Append(" and TI.TER_NO='" + ht["TerNo"].ToString() + "'");
                }
                if (ht["DeptId"].ToString() != "")
                {
                    sb.Append(" and TI.DEPT_ID ='" + ht["DeptId"].ToString() + "'");
                    sbCount.Append(" and TI.DEPT_ID ='" + ht["DeptId"].ToString() + "'");
                }
                if (NewStartDate != "")
                {
                    sb.Append(" and ZTK.RTIME >=to_date('" + NewStartDate + "','yyyy-mm-dd hh24:mi:ss')");
                    sbCount.Append(" and ZTK.RTIME >=to_date('" + NewStartDate + "','yyyy-mm-dd hh24:mi:ss')");
                }
                if (NewEndDate != "")
                {
                    sb.Append(" and ZTK.RTIME <=to_date('" + NewEndDate + "','yyyy-mm-dd hh24:mi:ss')");
                    sbCount.Append(" and ZTK.RTIME <=to_date('" + NewEndDate + "','yyyy-mm-dd hh24:mi:ss')");
                }
                sb.Append(" order by ZTK.RTIME desc");
                DataSet dsYXTrail = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString());
                DataSet dsYXTrailNum = c.GetColligateQuery("ColligateQuery.ProteanQuery", sbCount.ToString());
                if (dsYXTrail != null && dsYXTrail.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsYXTrail.Tables[0].Rows)
                    {
                        YXHistoricalData yxhd = new YXHistoricalData();
                        yxhd.Rtime = DateTime.Parse(dr["Rtime"].ToString());
                        yxhd.Position = dr["Position"].ToString();
                        yxhd.TerNo = dr["TerNo"].ToString();
                        yxhd.ReplydataName = dr["ReplydataName"].ToString();
                        yxhd.Ifposition = dr["Ifposition"].ToString();
                        yxhd.Northorsouth = dr["Northorsouth"].ToString();
                        yxhd.Eastorwest = dr["Eastorwest"].ToString();
                        yxhd.Speed = dr["Speed"].ToString();
                        yxhd.Direction = dr["Direction"].ToString();
                        yxhd.TerVbatt = dr["TerVbatt"].ToString();
                        yxhd.Gsmrssi = dr["Gsmrssi"].ToString();
                        yxhd.Programverson = dr["Programverson"].ToString();
                        yxhd.Gpsverson = dr["Gpsverson"].ToString();
                        yxhd.Latitude = dr["Latitude"].ToString();
                        yxhd.Longitude = dr["Longitude"].ToString();
                        yxhd.GoogleLatitude = dr["GoogleLatitude"].ToString();
                        yxhd.GoogleLongitude = dr["GoogleLongitude"].ToString();
                        yxhd.BaiduLatitude = dr["BaiduLatitude"].ToString();
                        yxhd.BaiduLongitude = dr["BaiduLongitude"].ToString();
                        yxhd.TerSimcard = dr["TerSimcard"].ToString();
                        yxhd.Ter_Innettime = DateTime.Parse(dr["Ter_Innettime"].ToString());
                        Listhrd.Add(yxhd);
                    }
                }
            }
            else
            {
                ht.Add("EndData", 0);
                IList<TerData> iltd = historicalDataBll.GetTerHistoryData(ht);
                Listhrd = iltd.ToList().Select(m => new YXHistoricalData { 
                    Rtime = m.Rtime,
                    Position = m.Position,
                    TerNo = m.TerNo,
                    ReplydataName = m.ReplydataName,
                    Ifposition = m.Ifposition,
                    Northorsouth = m.Northorsouth,
                    Eastorwest = m.Eastorwest,
                    Speed = m.Speed,
                    Direction = m.Direction,
                    TerVbatt = m.TerVbatt,
                    Gsmrssi = m.Gsmrssi,
                    Programverson = m.Programverson,
                    Gpsverson = m.Gpsverson,
                    Latitude = m.Latitude.ToString() == null ? "" : m.Latitude.ToString(),
                    Longitude = m.Longitude.ToString() == null ? "" : m.Longitude.ToString(),
                    GoogleLatitude = m.GoogleLatitude.ToString() == null ? "" : m.GoogleLatitude.ToString(),
                    GoogleLongitude = m.GoogleLongitude.ToString() == null ? "" : m.GoogleLongitude.ToString(),
                    BaiduLatitude = m.BaiduLatitude.ToString() == null ? "" : m.BaiduLatitude.ToString(),
                    BaiduLongitude = m.BaiduLongitude.ToString() == null ? "" : m.BaiduLongitude.ToString(),
                    TerSimcard = m.TerSimcard,
                    Ter_Innettime = m.Ter_Innettime
                }).ToList();
            }

            ExcelUpLoad eu = new ExcelUpLoad();
            MemoryStream ms = eu.CreateExcel(Listhrd);
            string xlsName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // 输出Excel
            using (FileStream fs = new FileStream(HttpContext.Server.MapPath("../Files/历史轨迹信息") + xlsName + ".xlsx", FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            if (System.IO.File.Exists(HttpContext.Server.MapPath("../Files/历史轨迹信息") + xlsName + ".xlsx"))
            {
                string ppphhh = "../../Files/历史轨迹信息" + xlsName + ".xlsx";

                new LogMessage().Save("文件:" + ppphhh + "。");

                return ppphhh;
            }
            else
            {
                return "生成文件出错，请重新导出！";
            }
        }
    }
}