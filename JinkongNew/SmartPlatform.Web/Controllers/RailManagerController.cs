using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GBLL.Car;
using GModel.Car;
using System.Text.RegularExpressions;
using System.Text;
using GModel.Basic;
using SuperGPS.App_Start;
using System.Data;
using GBLL.Basic;
using GBLL;
using Newtonsoft.Json;

namespace SuperGPS.Controllers
{
    public class RailManagerController : BaseController
    {
        RailPropertyBLL railPropertyBll = new RailPropertyBLL();
        SetAreaBLL saBll = new SetAreaBLL();
        ColligateQueryService c = new ColligateQueryService();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();

        // GET: RailManager
        //[OutputCache(CacheProfile = "ActionCacheProfile")]
        public ActionResult RailIndex()
        {
            return View();
        }

        public ActionResult BDRailIndex()
        {
            return View();
        }

        public ActionResult GGRailView(string terno)
        {
            ViewBag.TerNo = terno;
            SetArea sa = saBll.GetSetAreaData(terno);
            if (sa == null)
            {
                sa = new SetArea();
            }
            return View(sa);
        }

        public ActionResult BDRailView(string terno)
        {
            ViewBag.TerNo = terno;
            SetArea sa = saBll.GetSetAreaData(terno);
            if (sa == null)
            {
                sa = new SetArea();
            }
            return View(sa);
        }

        public int InsertRail(RailProperty entity) 
        {
            entity.RailId = System.Guid.NewGuid().ToString();
             UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                entity.Businessdivisionid = user.EnterId;
            }
            entity.RailState = 1;
            Regex regex = new Regex("(?<=\\()[^\\(\\)]*(?=\\))");
            //entity.Positiondata = "ksdfsdfkfaa";
            if (entity.RailData != null && entity.RailData.Trim() != "")
            {
                StringBuilder sb = new StringBuilder();
                char[] c = new char[3];
                c[0] = '(';
                c[1] = ')';
                c[2] = ' ';
                if (entity.RailShapetype == 1)
                {
                    MatchCollection m = regex.Matches(entity.RailData.Trim());
                    for (int i = 0; i < m.Count; i++)
                    {
                        //s = m[i].ToString().Trim(c);
                        string[] str = m[i].ToString().Split(',');
                        //计算最大X,最大Y，最小X，最小Y
                        if (entity.RailMaxx == 0 || entity.RailMaxx < Convert.ToDouble(str[0]))
                        {
                            entity.RailMaxx = Convert.ToDouble(str[0]);
                        }
                        if (entity.RailMinx == 0 || entity.RailMinx > Convert.ToDouble(str[0]))
                        {
                            entity.RailMinx = Convert.ToDouble(str[0]);
                        }
                        if (entity.RailMaxy == 0 || entity.RailMaxy < Convert.ToDouble(str[1]))
                        {
                            entity.RailMaxy = Convert.ToDouble(str[1]);
                        }
                        if (entity.RailMiny == 0 || entity.RailMiny > Convert.ToDouble(str[1]))
                        {
                            entity.RailMiny = Convert.ToDouble(str[1]);
                        }
                        //sb.Append(m[i].ToString().Replace('(','{').Replace(')','}')+",");
                        sb.Append("{x:" + str[0] + ",y:" + str[1] + "},");
                    }
                    //将电子围栏坐标以js数组的格式保存在数据库。
                    entity.RailData = "[" + sb.ToString().Trim(',') + "]";
                    entity.RailCenter = "[{x:" + ((entity.RailMaxx + entity.RailMinx) / 2).ToString() + ",y:" + ((entity.RailMaxy + entity.RailMiny) / 2).ToString() + "}]";
                }
                else if (entity.RailShapetype == 0)
                {
                    string[] strdata = entity.RailData.Split('$');
                    MatchCollection m = regex.Matches(entity.RailData.Trim());
                    if (m.Count > 0)
                    {
                        string[] str = m[0].ToString().Split(',');
                        double degree = Convert.ToDouble( strdata[1]) / (2 * Math.PI * 6378137.0) * 360;

                        entity.RailMaxx = Convert.ToDouble(str[0])+degree;
                        entity.RailMinx = Convert.ToDouble(str[0])-degree;
                        entity.RailMaxy = Convert.ToDouble(str[1])+degree;
                        entity.RailMiny = Convert.ToDouble(str[1])+degree;
                        entity.RailData = "[{x:" + str[0] + ",y:" + str[1] + ",Radius:" + strdata[1] + "}]";
                        entity.RailCenter = "[{x:" + str[0] + ",y:" + str[1] + "}]";
                    }
                }
                else
                {
                    MatchCollection m = regex.Matches(entity.RailData.Trim());
                    if (m.Count == 2)
                    {
                        string[] str = m[0].ToString().Split(',');
                        entity.RailMinx = Convert.ToDouble(str[0]);
                        entity.RailMiny = Convert.ToDouble(str[1]);
                        sb.Append("{x:" + str[0] + ",y:" + str[1] + "},");

                        str = m[1].ToString().Split(',');
                        entity.RailMaxx = Convert.ToDouble(str[0]);
                        entity.RailMaxy = Convert.ToDouble(str[1]);
                        sb.Append("{x:" + str[0] + ",y:" + str[1] + "},");
                        entity.RailData = "[" + sb.ToString().Trim(',') + "]";
                        entity.RailCenter = "[{x:" + ((entity.RailMaxx + entity.RailMinx) / 2).ToString() + ",y:" + ((entity.RailMaxy + entity.RailMiny) / 2).ToString() + "}]";
                    }
                }
            }
            entity.RailCdate = DateTime.Now;
            int a = Convert.ToInt32(railPropertyBll.Insert(entity));
            return a;
        }

        public string getRailList(SetArea sa,string Area_Terno) 
        {
            //rpt = new RailProperty();
            //IList<RailProperty> irp = railPropertyBll.GetRailPropertyPage(rpt);
            //string aa = ConvertToJson(irp);
            //return aa;
            sa = new SetArea();
            sa.Area_Maptype = "2";
            if (Area_Terno != "" && Area_Terno != "终端号(为空时,点回车显示全部)...")
            {
                sa.Area_Terno = Area_Terno;
            }
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                sa.BusinessdivisionCode = user.UserDeptcode;
            }
            IList<SetArea> isa = saBll.GetSetAreaPage(sa);
            string aa = ConvertToJson(isa);
            return aa;
        }

        public string getBDRailList(SetArea sa, string Area_Terno)
        {
            sa = new SetArea();
            sa.Area_Maptype = "1";
            if (Area_Terno != "" && Area_Terno != "终端号(为空时,点回车显示全部)...")
            {
                sa.Area_Terno = Area_Terno;
            }
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                sa.BusinessdivisionCode = user.UserDeptcode;
            }
            IList<SetArea> isa = saBll.GetSetAreaPage(sa);
            string aa = ConvertToJson(isa);
            return aa;
        }

        public string getRail(string Area_Terno)
        {
            SetArea sa = new SetArea();
            sa.Area_Terno = Area_Terno;
            IList<SetArea> isa = saBll.GetSetAreaPage(sa);
            string aa = ConvertToJson(isa);
            return aa;
        }

        #region 坐标点的类
        public class RailPoint
        {
            public double X { get; set; }

            public double Y { get; set; }
        }
        #endregion

        #region 谷歌和百度坐标互转
        private const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
        /// <summary>
        /// 中国正常坐标系GCJ02协议的坐标，转到 百度地图对应的 BD09 协议坐标
        /// </summary>
        /// <param name="lat">维度</param>
        /// <param name="lng">经度</param>
        public static void Convert_GCJ02_To_BD09(ref RailPoint rp)
        {
            double x = rp.X, y = rp.Y;
	        double z =Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
	        double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
	        rp.Y = z * Math.Cos(theta) + 0.0065;
	        rp.X = z * Math.Sin(theta) + 0.006;
        }
        /// <summary>
        /// 百度地图对应的 BD09 协议坐标，转到 中国正常坐标系GCJ02协议的坐标
        /// </summary>
        /// <param name="lat">维度</param>
        /// <param name="lng">经度</param>
        public static void Convert_BD09_To_GCJ02(ref double lat, ref double lng)
        {
	        double x = lng - 0.0065, y = lat - 0.006;
	        double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
	        double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
	        lng = z * Math.Cos(theta);
	        lat = z * Math.Sin(theta);
        }
        #endregion

        [Log(LogMessage = "删除围栏")]
        [HttpPost]
        public string Delete(string AreaId)
        {
            //int num = saBll.Delete("AREA_ID='" + AreaId + "'");
            SetArea sa = saBll.GetSetAreaDataById(AreaId);
            Transfers.ClintSendCommData(1107, "36", "", sa.Area_Terno+"&", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            System.Threading.Thread.Sleep(1000);
            return "删除成功";
        }


        #region 获取已设置的电子围栏区域信息
        [HttpGet]
        public ActionResult TaMap()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                ViewBag.Deptid = user.EnterId;
            }
            return View();
        }
        #endregion

        #region 获取已设置的电子围栏里面车辆信息
        [HttpGet]
        public ActionResult LoadCarOnmap(string Rid)
        {
            ViewBag.RailId = Rid;
            return View();
        }
        #endregion

        /// <summary>
        /// 查询车辆
        /// </summary>
        /// <param name="carInfo"></param>
        /// <returns></returns>
        [UserFilter]
        public string GetCarList(int page,int rows)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                string CarNo = Request.Params["CarNo"].ToString();
                string TerNo = Request.Params["TerNo"].ToString();
                string DeptSelId = Request.Params["DeptSelId"].ToString();
                string ChildrenSel = Request.Params["ChildrenSel"].ToString();

                StringBuilder sb = new StringBuilder();
                StringBuilder sbCount = new StringBuilder();
                sb.Append("select ci.car_id,ci.car_no,ct.type_name,di.businessdivisionname,ti.ter_no,ti.TER_SIMCARD");
                sb.Append(" from terminal_info ti left join Car_Info ci on ci.car_id=ti.car_id left join car_type ct on ci.type_id=ct.type_id left join dept_info di on ti.dept_id=di.businessdivisionid");
                sb.Append("  where 1=1");
                sbCount.Append("select count(*) from terminal_info ti left join Car_Info ci on ci.car_id=ti.car_id  left join car_type ct on ci.type_id=ct.type_id left join dept_info di on ti.dept_id=di.businessdivisionid  where 1=1");
                if (TerNo != null && TerNo.Trim() != "")
                {
                    sb.Append(string.Format(" and ti.ter_no like '%{0}%'", TerNo));
                    sbCount.Append(string.Format(" and ti.ter_no like '%{0}%'", TerNo));
                }
                if (CarNo != null && CarNo.Trim() != "")
                {
                    sb.Append(string.Format(" and ci.car_no like '%{0}%'",CarNo));
                    sbCount.Append(string.Format(" and ci.car_no like '%{0}%'", CarNo));
                }
                if (DeptSelId != null && DeptSelId.Trim() != "")
                {
                    if (ChildrenSel != "true")
                    {
                        sb.Append(string.Format(" and di.businessdivisionid='{0}'", DeptSelId));
                        sbCount.Append(string.Format(" and di.businessdivisionid='{0}'", DeptSelId));
                    }
                    else
                    {
                        DeptInfo di = null;
                        di = deptInfoBll.GetDeptInfo(DeptSelId);
                        sb.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
                        sbCount.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
                    }
                }
                else
                {
                    if (ChildrenSel != "true")
                    {
                        sb.Append(string.Format(" and di.businessdivisionid='{0}'", user.EnterId));
                        sbCount.Append(string.Format(" and di.businessdivisionid='{0}'", user.EnterId));

                    }
                    else
                    {
                        DeptInfo di = null;
                        di = deptInfoBll.GetDeptInfo(user.EnterId);
                        sb.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
                        sbCount.Append(string.Format(" and di.businessdivisioncode like '{0}%'", di.Businessdivisioncode));
                    }
                }
                sb.Append(" order by car_no ");
                DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString(), page, rows);
                DataSet dsCount = c.GetColligateQuery("ColligateQuery.ProteanQuery",sbCount.ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return DtbConvertToJson(ds.Tables[0], int.Parse(dsCount.Tables[0].Rows[0][0].ToString()));
                }
                else
                {
                    return DtbConvertToJson(new DataTable(), 0);
                }
            }
            else
            {
                return DtbConvertToJson(new DataTable(), 0);
            }
        }



        public string GetCarlistByRid(string RailId)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                RailProperty rpt = railPropertyBll.GetRailProperty(RailId);
                string CaridStr = "";
                if (rpt.RailCaridstr != null && rpt.RailCaridstr != "")
                {
                    string[] RailCaridArr = rpt.RailCaridstr.Split(',');
                    string CarStr = "";
                    foreach (var Carid in RailCaridArr)
                    {
                        if (Carid != null && Carid != "")
                        {
                            CarStr += "'" + Carid + "',";
                        }
                    }
                    CaridStr = CarStr.Substring(0, CarStr.Length - 1);
                }
                else
                {
                    return DtbConvertToJson(new DataTable(), 0);
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("select ci.car_id,ci.car_no,ct.type_name,di.businessdivisionname,ti.ter_no,ti.TER_SIMCARD");
                sb.Append(" from Car_Info ci join car_type ct on ci.type_id=ct.type_id join dept_info di on ci.businessdivisionid=di.businessdivisionid");
                sb.Append(" left terminal_info ti on ci.car_id=ti.car_id left join");
                sb.Append(" (SELECT CAR_ID ");
                sb.Append(" FROM field_values GROUP BY CAR_ID) tt on ci.car_id=tt.car_id where 1=1 and ci.car_id in (" + CaridStr + ")");
                DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return DtbConvertToJson(ds.Tables[0], ds.Tables[0].Rows.Count);
                }
                else
                {
                    return DtbConvertToJson(ds.Tables[0],0);
                }
            }
            else
            {
                return DtbConvertToJson(new DataTable(), 0);
            }
        }


        [Log(LogMessage = "添加围栏")]
        [HttpPost]
        [UserFilter]
        public string AddCarIdstr(string SelectTerNoStr, string AreaType, string AreaData, string AreaMaptype, string CenterloglatZoom)
        {
            string result = "";
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            var DeptCode = "";
            var UserName = "";
            if (user != null)
            {
                UserName = user.UserName;
                DeptCode = user.UserDeptcode;
            }

            string[] TerNoStr = SelectTerNoStr.Trim(',').Split(',');
            Regex regex = new Regex("(?<=\\()[^\\(\\)]*(?=\\))");
            MatchCollection m = regex.Matches(AreaData);
            StringBuilder sb = new StringBuilder();
            if (AreaType == "0") //方形
            {
                if (m.Count == 2)
                {
                    string[] str = m[0].ToString().Split(',');
                    sb.Append(str[1].Trim() + "," + str[0].Trim() + "|");
                    str = m[1].ToString().Split(',');
                    sb.Append(str[1].Trim() + "," + str[0].Trim());
                }
            }
            else if (AreaType == "1") //圆形
            {
                string[] strdata = AreaData.Split('$');
                if (m.Count > 0)
                {
                    string[] str = m[0].ToString().Split(',');
                    sb.Append(str[1].Trim() + "," + str[0].Trim() + "|" + strdata[1].Trim());
                }
            }
            else if (AreaType == "2") //多边
            {
                for (int i = 0; i < m.Count; i++)
                {
                    string[] str = m[i].ToString().Split(',');
                    if (i != m.Count - 1)
                    {
                        sb.Append(str[1].Trim() + "," + str[0].Trim() + "|");
                    }
                    else
                    {
                        sb.Append(str[1].Trim() + "," + str[0].Trim());
                    }
                }
            }
            foreach (var terno in TerNoStr)
            {
               result += terno + "&";
            }
            if (result != "")
            {
                Transfers.ClintSendCommData(1107, "35", "", result, AreaMaptype, AreaType, "", "", "", "", "", DeptCode, UserName, sb.ToString(), CenterloglatZoom, "", "", "", "");
                System.Threading.Thread.Sleep(1000);
                return "成功";
            }
            else
            {
                return "未选择设备！";
            }
        }



        public ActionResult Regin()
        {
            return View();
        }



        [HttpPost]
        public string getRailCarList(string CaridStr)
        {
            DataSet ds = null;
            if (CaridStr != null && CaridStr != "")
            {
                string[] RailCaridArr = CaridStr.Split(',');
                string CarStr = "";
                foreach (var Carid in RailCaridArr)
                {
                    if (Carid != null && Carid != "")
                    {
                        CarStr += "'" + Carid + "',";
                    }
                }
                CaridStr = CarStr.Substring(0, CarStr.Length - 1);

                StringBuilder sb = new StringBuilder();
                sb.Append("select ci.car_id,ci.car_no,ct.type_name,ti.ter_no,ti.TER_SIMCARD,rd.LATITUDE,rd.LONGITUDE,rd.BAIDU_LATITUDE,rd.BAIDU_LONGITUDE,rd.GOOGLE_LATITUDE,rd.GOOGLE_LONGITUDE,rd.POSITION");
                sb.Append(" from Car_Info ci left join car_type ct on ci.type_id=ct.type_id");
                sb.Append(" left join terminal_info ti on ci.car_id=ti.car_id ");
                sb.Append(" left join REALTIME_DATA rd on rd.ter_no=ti.ter_no");
                sb.Append(" where 1=1 and ci.car_id in (" + CaridStr + ")");
                sb.Append(" order by car_no");
                ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", sb.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ConvertToJson(ds.Tables[0]);
                }
                else
                {
                    return "";
                }
            }
            else
            { 
                return "";
            }

        }
    }
}