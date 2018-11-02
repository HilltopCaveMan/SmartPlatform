using GBLL.Car;
using GBLL.InterFace;
using GenrerConfigWeb.Util;
using GModel.Basic;
using GModel.Car;
using GModel.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GModel.RoleRight;
using SuperGPS.App_Start;
using GBLL.Basic;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Security;
using System.Data;
using GBLL;

namespace SuperGPS.Controllers
{
    public class SendCMDController : BaseController
    {
        SendinfoBLL sendinfoBll = new SendinfoBLL();
        SendcmdBLL sendcmdBll = new SendcmdBLL();
        SendinfoLastBLL sendinfoLastBLL = new SendinfoLastBLL();
        TerminalInfoBLL terbll = new TerminalInfoBLL();
        CarInfoBLL carInfobll = new CarInfoBLL();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();
        SendinfoyxBLL sendinfoyxbll = new SendinfoyxBLL();
        ColligateQueryService c = new ColligateQueryService();
        SendinfoyxLastBLL yxbll = new SendinfoyxLastBLL();
        //
        // GET: /SendCMD/
        //[OutputCache(CacheProfile = "ActionCacheProfile")]
        [HttpGet]
        public ActionResult Index()
        {
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.SendCMD = "false";
            ViewBag.WaitSend = "false";
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "发送命令":
                        ViewBag.SendCMD = "true";
                        break;
                    case "单个待发送命令":
                        ViewBag.WaitSend = "true";
                        break;
                }
            }
            return View();
        }
        [HttpPost]
        [UserFilter]
        public string GetTerListData(GetterpositionView carinfoObj, int rows, int page, string ChildrenSel)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (carinfoObj.Businessdivisionid == null || carinfoObj.Businessdivisionid.Trim() == "")
            {
                carinfoObj.Businessdivisionid = user.EnterId;
            }
            carinfoObj.StartData = (page - 1) * rows + 1;
            carinfoObj.EndData = carinfoObj.StartData + rows;
            if (ChildrenSel == "true")
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(carinfoObj.Businessdivisionid);
                if (di != null)
                {
                    carinfoObj.Businessdivisioncode = di.Businessdivisioncode;
                    carinfoObj.Businessdivisionid = "";
                }
            }
            IList<GetterpositionView> CarInfolist = terbll.GetterpositionViewPage(carinfoObj);
            int count = terbll.GetterpositionViewCount(carinfoObj);
            return ConvertToJson(CarInfolist, count);
        }

        //[OutputCache(CacheProfile = "ActionCacheProfile")]
        [UserFilter]
        public ActionResult Index_Multi()
        {
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.MutilSendCMD = "false";
            ViewBag.MutilWaitSend = "false";
            ViewBag.MutilWaitExe = "false";
            ViewBag.MutilHisSend = "false";
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "批量发送命令":
                        ViewBag.MutilSendCMD = "true";
                        break;
                    case "批量待发送命令":
                        ViewBag.MutilWaitSend = "true";
                        break;
                    case "发送未执行":
                        ViewBag.MutilWaitExe = "true";
                        break;
                    case "历史命令":
                        ViewBag.MutilHisSend = "true";
                        break;
                }
            }
            return View();
        }
        //有线gps发送命令视图
        [UserFilter]
        [HttpGet]
        public ActionResult WiredDevCMD(string str,bool flag)
        {
            bool ISAdmin = false;
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                string RoleId = userinfo.RoleId;
                if (RoleId != null && RoleId.Trim() == "d2f78a0d-d375-4a20-a29d-13b011c94ff4")
                {
                    ISAdmin = true;
                }
            }
            ViewBag.IsAdmin = ISAdmin;
            ViewData["Ter_ID"] = str;
            ViewBag.flag = flag;
            return View();
        }


        //有线gps新版发送命令视图
        [UserFilter]
        [HttpGet]
        public ActionResult WiredDevCMD_New(string str, bool flag)
        {
            bool ISAdmin = false;
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                string RoleId = userinfo.RoleId;
                if (RoleId != null && RoleId.Trim() == "d2f78a0d-d375-4a20-a29d-13b011c94ff4")
                {
                    ISAdmin = true;
                }
            }
            ViewBag.IsAdmin = ISAdmin;
            ViewData["Ter_ID"] = str;
            ViewBag.flag = flag;
            return View();
        }

        [Log(LogMessage = "终端发送命令")]
        [UserFilter]
        [HttpPost]
        public string WiredDevCMD(string devid, string cmd, string strparam,string terflag)
        {
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            string username = "";
            if (userinfo != null)
            {
                username = userinfo.UserName;
            }
            //dshj,djlhj,hcjg,cjxs,tzbj,tzhj，xsjl
            string tertype = "";
            if (terflag == "False")
            {
                tertype = "111";
            }
            else
            {
                tertype = "101";
            }
            switch (cmd)
            {
                case "dshj":
                    //定时设置
                    Transfers.ClintSendCommData(1160, "0005", tertype, devid.Trim() + "&", "", "201", "", "", "", "", "", "", strparam.ToString(), "", "", "", username, "", "");
                    break;
                case "djlhj":
                    //定距呼叫
                    Transfers.ClintSendCommData(1160, "0005", tertype, devid.Trim() + "&", "", "206", "", "", "", "", "", "", strparam.ToString(), "", "", "", username, "", "");
                    break;
                //case "hcjg"://GPRS链接维持报文的回传时间间隔
                //    Transfers.ClintSendCommData(1160, "0005", "101", devid.Trim() + "&", "", "201", "", "", "", "", "", "", strparam.ToString(), "", "", "", username, "", "");
                //    break;
                case "cjxs"://车机限速
                    Transfers.ClintSendCommData(1160, "0005", tertype, devid.Trim() + "&", "", "306", "", "", "", "", "", "", strparam.ToString(), "", "", "", username, "", "");
                    break;
                case "qxxs"://取消限速
                    Transfers.ClintSendCommData(1160, "0005", tertype, devid.Trim() + "&", "", "307", "", "", "", "", "", "", "", "", "", "", username, "", "");
                    break;
                case "ykxh": //遥控熄火
                    Transfers.ClintSendCommData(1160, "0005", tertype, devid.Trim() + "&", "", "101", "", "", "", "", "", "", "", "", "", "", username, "", "");
                    break;
                case "hfsd": //恢复上电
                    Transfers.ClintSendCommData(1160, "0005", tertype, devid.Trim() + "&", "", "102", "", "", "", "", "", "", "", "", "", "", username, "", "");
                    break;
                case "lybd": //蓝牙绑定
                    Transfers.ClintSendCommData(1160, "0005", tertype, devid.Trim() + "&", "", "702", "", "", "", "", "", "", strparam.ToString(), "", "", "", username, "", "");
                    break;
                case "jcbd": //解除绑定
                    Transfers.ClintSendCommData(1160, "0005", tertype, devid.Trim() + "&", "", "702", "", "", "", "", "", "", strparam.ToString(), "", "", "", username, "", "");
                    break;
            }

            string reslut = @"true;" + cmd;
            return reslut;
        }
        [UserFilter]
        [HttpGet]
        public ActionResult Homer3B2CMD(string str)
        {
            bool ISAdmin = false;
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                string RoleId = userinfo.RoleId;
                if (RoleId != null && RoleId.Trim() == "d2f78a0d-d375-4a20-a29d-13b011c94ff4")
                {
                    ISAdmin = true;
                }
            }
            ViewBag.IsAdmin = ISAdmin;
            ViewData["Ter_ID"] = str;
            return View();
        }
        [HttpPost]
        public string Homer3B2CMD(string devid, string cmd, int strparam)
        {
            //dshj,djlhj,hcjg,cjxs,tzbj,tzhj，xsjl
            string reslut = "false;";
            string SendCMD = "";
            Sendinfoyx SendinfoyxObj = new Sendinfoyx();
            switch (cmd)
            {
                case "dshj":
                    //定时呼叫
                    SendCMD = SendCMD + "030103" + Convert.ToString(strparam, 16).PadLeft(4, '0');
                    SendinfoyxObj.SendinfoDescription = "定时呼叫命令，参数：" + strparam;
                    break;
                case "djlhj":
                    //定距呼叫
                    SendCMD = SendCMD + "030105" + Convert.ToString(strparam / 50, 16).PadLeft(2, '0');
                    SendinfoyxObj.SendinfoDescription = "定距呼叫命令，参数：" + strparam;
                    break;
                case "hcjg"://GPRS链接维持报文的回传时间间隔
                    SendCMD = SendCMD + "030106" + Convert.ToString(strparam / 10, 16).PadLeft(2, '0');
                    SendinfoyxObj.SendinfoDescription = "GPRS链接维持报文的回传时间间隔，参数：" + strparam;
                    break;
                case "cjxs"://车机限速
                    if (strparam > 0 && strparam <= 99)
                    {
                        SendCMD = SendCMD + "030004" + Convert.ToString(strparam);
                        SendinfoyxObj.SendinfoDescription = "车机限速，参数：" + strparam;
                    }
                    else
                    {
                        return "fasle;";
                    }
                    break;
                case "csbj"://超速报警
                    SendCMD = SendCMD + "02030B";
                    SendinfoyxObj.SendinfoDescription = "超速报警";
                    break;
                case "tzbj"://停止报警
                    SendCMD = SendCMD + "03020100";
                    SendinfoyxObj.SendinfoDescription = "停止报警";
                    break;
                case "tzhj":
                    SendCMD = SendCMD + "030104";
                    SendinfoyxObj.SendinfoDescription = "停止呼叫";
                    break;
                case "xsjl":
                    SendinfoyxObj.SendinfoDescription = "行驶距离";
                    break;
                case "xh"://熄火
                    SendCMD = SendCMD + "030202" + "00";
                    SendinfoyxObj.SendinfoDescription = "熄火";
                    break;
                case "sd"://上电
                    SendCMD = SendCMD + "030202" + "01";
                    SendinfoyxObj.SendinfoDescription = "上电";
                    break;
                case "jt"://监听
                    SendCMD = SendCMD + "030400" + "00";
                    SendinfoyxObj.SendinfoDescription = "监听";
                    break;

            }

            int a = getSwiftnumber(devid);
            if (a != -1)
            {
                SendinfoyxObj.Swiftnumber = a;
            }
            else
            {
                return "false;";
            }
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                SendinfoyxObj.SendinfoUserid = userinfo.UserId;
            }
            else
            {
                return "false;";
            }
            SendCMD = "@SJHX," + UtilityTools.convert(devid.Trim()) + "7E7E" + Convert.ToString(a, 16).PadLeft(4, '0') + "7E7E" + SendCMD;
            SendinfoyxObj.Id = Guid.NewGuid().ToString();
            SendinfoyxObj.DeviceId = devid;
            SendinfoyxObj.SendinfoCommand = SendCMD.ToUpper();
            SendinfoyxObj.SendinfoPtime = DateTime.Now;
            string str = sendinfoyxbll.InsertData(SendinfoyxObj);
            if (str == "true") { reslut = @"true;" + SendCMD; }
            else
            {
                reslut = "false;";
            }
            return reslut;
        }
        public int getSwiftnumber(string devid)
        {
            System.Collections.Generic.IList<SendinfoyxLast> list = new List<SendinfoyxLast>();
            list = yxbll.GetSendinfoyxLastPageByTerNo(devid, 0, 0);
            Random rd = new Random();
            int num = rd.Next(0, 65535);
            if (list != null)
            {
                foreach (SendinfoyxLast obj in list)
                {
                    if (obj.Swiftnumber == num)
                    {
                        getSwiftnumber(devid);
                    }
                }
            }
            else
            {
                num = -1;
            }
            return num;
        }
        public string IfSendSuccess(string devid, string sendCMD,string terflag)
        {
            string tertype = "";
            if (terflag == "False")
            {
                tertype = "111";
            }
            else
            {
                tertype = "101";
            }
            string cmd = "";
            switch (sendCMD)
            {
                case "dshj"://定时呼叫
                    cmd = "201";
                    break;
                case "djlhj"://定距呼叫
                    cmd = "206";
                    break;
                case "hcjg"://GPRS链接维持报文的回传时间间隔
                    cmd = "201";
                    break;
                case "cjxs"://车机限速
                    cmd = "306";
                    break;
                case "qxxs"://取消限速
                    cmd = "307";
                    break;
                case "ykxh"://遥控熄火
                    cmd = "101";
                    break;
                case "hfsd"://恢复上电
                    cmd = "102";
                    break;
                case "lybd"://蓝牙绑定
                    cmd = "702";
                    break;
                case "jcbd"://解除绑定
                    cmd = "702";
                    break;
            }
            string stateSQL = string.Format("select tr_state from( select * from SENDCMD where tr_setsn='{0}' and tr_cmdtype='{1}' and tr_settype='{2}' order by TR_OPDATE desc) a where rownum=1", devid.Trim(), cmd, tertype);
            DataSet ds = c.GetColligateQuery("ColligateQuery.ProteanQuery", stateSQL);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count!=0 && ds.Tables[0].Rows[0][0].ToString() != "003")
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        //地图选择行驶距离
        public ActionResult MapSelect(string str)
        {
            ViewData["Ter_ID"] = str;
            return View();
        }
        /// <summary>
        /// 一代设备发送命令view
        /// </summary>
        /// <param name="str">设备编号</param>
        /// <returns></returns>
        [UserFilter]
        public ActionResult DevSendCMD(string str)
        {
            bool ISAdmin = false;
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                string RoleId = userinfo.RoleId;
                if (RoleId != null && RoleId.Trim() == "d2f78a0d-d375-4a20-a29d-13b011c94ff4")
                {
                    ISAdmin = true;
                }
            }
            ViewBag.IsAdmin = ISAdmin;
            ViewData["Ter_ID"] = str;
            return View();
        }
        /// <summary>
        /// 一代设备发送命令
        /// </summary>
        /// <param name="devid"></param>
        /// <param name="ips"></param>
        /// <param name="ports"></param>
        /// <param name="gzmss"></param>
        /// <param name="gzsjs"></param>
        /// <param name="dssjs"></param>
        /// <param name="xmsjs"></param>
        /// <param name="scsjs"></param>
        /// <param name="sends"></param>
        /// <param name="dwcs"></param>
        /// <param name="CmandType"></param>
        /// <returns></returns>
        [HttpPost]
        [UserFilter]
        public String DevSendCMD(string devid, string ips, string ports, string gzmss, string gzsjs, string dssjs, string xmsjs, string scsjs, string sends, string dwcs, string CmandType)
        {
            var dssjsStr = string.Empty;
            if (dssjs == "0")
            {
                dssjsStr = "未设置";
            }
            else
            {
                dssjsStr = (Convert.ToInt32(dssjs) / 60).ToString();
            }
            string sendStr = "模式：" + Model(gzmss) + ",工作时间：" + gzsjs + "(分钟),启动时间：" + dssjsStr + "(点),间隔时间：" + xmsjs + "(分钟),定位次数：" + dwcs + "(次/天)";
            string uid = HttpContext.User.Identity.Name;
            string setValHex = string.Empty;
            if (sends == "1")
            {
                var re = ips.Split('.');
                if (re.Length == 4)
                {
                    setValHex = Convert.ToString(Convert.ToInt32(re[0]), 16).PadLeft(2, '0').ToUpper() +
                                Convert.ToString(Convert.ToInt32(re[1]), 16).PadLeft(2, '0').ToUpper() +
                                Convert.ToString(Convert.ToInt32(re[2]), 16).PadLeft(2, '0').ToUpper() +
                                Convert.ToString(Convert.ToInt32(re[3]), 16).PadLeft(2, '0').ToUpper();
                }
                setValHex += Convert.ToString(Convert.ToInt32(ports), 16).PadLeft(4, '0').ToUpper();
                setValHex += Convert.ToString(Convert.ToInt32(0), 16).PadLeft(4, '0').ToUpper();
                setValHex += Convert.ToString(Convert.ToInt32(dssjs), 16).PadLeft(4, '0').ToUpper();
                setValHex += Convert.ToString(Convert.ToInt32(xmsjs), 16).PadLeft(4, '0').ToUpper();
                setValHex += Convert.ToString(Convert.ToInt32(scsjs), 16).PadLeft(8, '0').ToUpper();
            }
            else
            {
                setValHex = "09";
                sendStr = "时间修正";
            }
            Sendinfo SendinfoObj = new Sendinfo();
            SendinfoObj.DeviceId = devid.Trim();
            SendinfoObj.SendinfoDescription = sendStr;
            SendinfoObj.SendinfoCommand = UtilityTools.convert(devid.Trim()) + setValHex.ToUpper();
            SendinfoObj.SendinfoPtime = DateTime.Now;
            SendinfoObj.SendinfoStatus = 0;
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                SendinfoObj.SendinfoUserid = userinfo.UserId;
            }
            string result = "";
            result = sendinfoBll.InsertData(SendinfoObj);
            return result;
        }

        [UserFilter]
        public ActionResult DevSendCMD_Multi(string str)
        {
            bool ISAdmin = false;
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                string RoleId = userinfo.RoleId;
                if (RoleId != null && RoleId.Trim() == "d2f78a0d-d375-4a20-a29d-13b011c94ff4")
                {
                    ISAdmin = true;
                }
            }
            ViewBag.IsAdmin = ISAdmin;
            ViewData["Ter_ID"] = str;
            return View();
        }
        /// <summary>
        /// 一代批量发送
        /// </summary>
        /// <param name="devid"></param>
        /// <param name="ips"></param>
        /// <param name="ports"></param>
        /// <param name="gzmss"></param>
        /// <param name="gzsjs"></param>
        /// <param name="dssjs"></param>
        /// <param name="xmsjs"></param>
        /// <param name="scsjs"></param>
        /// <param name="sends"></param>
        /// <param name="dwcs"></param>
        /// <param name="CmandType"></param>
        /// <returns></returns>
        [HttpPost]
        [UserFilter]
        public String DevSendCMD_Multi(string devid, string ips, string ports, string gzmss, string gzsjs, string dssjs, string xmsjs, string scsjs, string sends, string dwcs, string CmandType)
        {
            var dssjsStr = string.Empty;
            if (dssjs == "0")
            {
                dssjsStr = "未设置";
            }
            else
            {
                dssjsStr = (Convert.ToInt32(dssjs) / 60).ToString();
            }
            string sendStr = "模式：" + Model(gzmss) + ",工作时间：" + gzsjs + "(分钟),启动时间：" + dssjsStr + "(点),间隔时间：" + xmsjs + "(分钟),定位次数：" + dwcs + "(次/天)";
            string uid = HttpContext.User.Identity.Name;
            string setValHex = string.Empty;
            if (sends == "1")
            {
                var re = ips.Split('.');
                if (re.Length == 4)
                {
                    setValHex = Convert.ToString(Convert.ToInt32(re[0]), 16).PadLeft(2, '0').ToUpper() +
                                Convert.ToString(Convert.ToInt32(re[1]), 16).PadLeft(2, '0').ToUpper() +
                                Convert.ToString(Convert.ToInt32(re[2]), 16).PadLeft(2, '0').ToUpper() +
                                Convert.ToString(Convert.ToInt32(re[3]), 16).PadLeft(2, '0').ToUpper();
                }
                setValHex += Convert.ToString(Convert.ToInt32(ports), 16).PadLeft(4, '0').ToUpper();
                setValHex += Convert.ToString(Convert.ToInt32(0), 16).PadLeft(4, '0').ToUpper();
                setValHex += Convert.ToString(Convert.ToInt32(gzsjs), 16).PadLeft(4, '0').ToUpper();
                setValHex += Convert.ToString(Convert.ToInt32(dssjs), 16).PadLeft(4, '0').ToUpper();
                setValHex += Convert.ToString(Convert.ToInt32(xmsjs), 16).PadLeft(4, '0').ToUpper();
                setValHex += Convert.ToString(Convert.ToInt32(scsjs), 16).PadLeft(8, '0').ToUpper();
            }
            else
            {
                setValHex = "09";
                sendStr = "时间修正";
            }
            Sendinfo SendinfoObj = new Sendinfo();
            SendinfoObj.SendinfoDescription = sendStr;
            SendinfoObj.SendinfoCommand = UtilityTools.convert(devid.Trim()) + setValHex.ToUpper();
            SendinfoObj.SendinfoPtime = DateTime.Now;
            SendinfoObj.SendinfoStatus = 0;
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                SendinfoObj.SendinfoUserid = userinfo.UserId;
            }
            List<Sendinfo> listSendinfo = new List<Sendinfo>();
            string[] devidarr = devid.Trim(',').Split(',');
            for (int i = 0; i < devidarr.Length; i++)
            {
                SendinfoObj.DeviceId = devidarr[i];
                listSendinfo.Add(SendinfoObj);
            }
            //插入数据表SendInfo与SendInfo_Last--------------------------------------------
            //成功返回true，失败返回失败的设备编号
            string result = sendinfoBll.InsertDataList(listSendinfo);
            return result;
        }

        [HttpGet]
        [UserFilter]
        public ActionResult DevSendCMD_New(string str)
        {
            bool ISAdmin = false;
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                string userid = userinfo.UserId;
                if ((userid.ToLower() == "0".ToLower()) ||
                userid.ToLower() == "9386458A-C5A7-4560-A5C4-B798570FA7F5".ToLower())
                {
                    ISAdmin = true;
                }
            }
            ViewBag.IsAdmin = ISAdmin;
            ViewData["Ter_ID"] = str;
            return View();
        }

        [HttpGet]
        [UserFilter]
        public ActionResult DevSendCMD_Wudai(string str)
        {
            ViewData["TerNo"] = str;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devid"></param>
        /// <param name="ips"></param>
        /// <param name="ports"></param>
        /// <param name="gzmss">工作模式</param>
        /// <param name="gzsjs">工作时间</param>
        /// <param name="dssjs">定位时间</param>
        /// <param name="xmsjs">休眠时间（间隔时间）</param>
        /// <param name="scsjs"></param>
        /// <param name="sends"></param>
        /// <param name="dwcs">定位次数</param>
        /// <param name="CmandType">命令类型</param>
        /// <param name="DomainName">域名</param>
        /// <param name="addressType">发送地址类型（01:ip;02:域名；00不设置）</param>
        /// <param name="Activation">激活状态 00：不设置；01：测试；02待激活；03已激活</param>
        /// <returns></returns>
        [Log(LogMessage = "终端发送命令")]
        [UserFilter]
        public String DevSendCMD_New(string devid, string ips, string ports, string gzmss,
            string gzsjs, string dssjs, string xmsjs, string scsjs, string dwcs,
            string CmandType, string DomainName, string addressType, string Activation,
            string Workhours,string LanyaState)
        {
            List<string> setValue =new List<string>();
            //设备状态
            switch (Activation)
            {
                case "00":
                    setValue.Add("V");
                    break;
                case "01"://测试
                    setValue.Add("1");
                    break;
                case "02"://待激活
                    setValue.Add("2");
                    break;
                case "03"://已激活
                    setValue.Add("3");
                    break;
                default:
                    setValue.Add("V");
                    break;
            }
            //模式
            switch (gzmss)
            {
                case "01"://标准
                    setValue.Add("1");
                    break;
                case "02"://精准
                    setValue.Add("2");
                    break;
                case "03"://追车
                    setValue.Add("3");
                    break;
                case "00"://不设置
                    setValue.Add("V");
                    break;
                default:
                    setValue.Add("V");
                    break;
            }
            //工作时间
            setValue.Add(gzsjs);
            //休眠时间
            if (dwcs == "0")
            {
                setValue.Add("V");
            }
            else
            {
                setValue.Add(Convert.ToInt32(24 * 60 / int.Parse(dwcs)).ToString());
            }

            //定时时间
            var dssjsStr = string.Empty;
            if (dssjs == "0")
            {
                setValue.Add("V");
            }
            else
            {
                setValue.Add((Convert.ToInt32(dssjs) / 60).ToString());
            }

            //间隔时间
            if (xmsjs == "0")
            {
                setValue.Add("V");
            }
            else
            {
                setValue.Add(xmsjs.Trim());
            }
            //累计工时
            if (Workhours == "00" || Workhours == "0")
            {
                setValue.Add("V");
            }
            else
            {
                setValue.Add(Workhours);
            }
            //IP或域名选择
            switch (addressType)
            {
                case "00":
                    setValue.Add("V");
                    setValue.Add("V");
                    break;
                case "01":
                    //IP转换 端口
                    setValue.Add("I*" + ips);
                    setValue.Add(ports);
                    break;
                case "02":
                    //域名转换 端口
                    setValue.Add("Y*" + DomainName);
                    setValue.Add(ports);
                    break;
            }
            //蓝牙设置
            if (LanyaState == "0")
            {
                setValue.Add("V");
            }
            else
            {
                setValue.Add(LanyaState);
            }
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            string username = "";
            if (userinfo != null)
            {
                username = userinfo.UserName;
            }
            var strparam = String.Join("`", setValue.ToArray());
            Transfers.ClintSendCommData(1160, "0005", "102", devid.Trim() + "&", "", "705", "", "", "", "", "", "", strparam.ToString(), "", "", "", username, "", "");

            #region 旧代码
            //string IsTimetype = "分钟";
            //if (gzmss == "03")
            //{
            //    IsTimetype = "秒";
            //}
            //string dd = (dwcs == "0") ? "不设置" : dwcs;
            //string sendStr = "模式：" + Model(gzmss) + ",工作时间：" + gzsjs + "(分钟),启动时间：" + dssjsStr
            //    + "(点),间隔时间：" + xmsjs + "(" + IsTimetype + "),定位次数：" + dd + "(次/天)";
            //string uid = HttpContext.User.Identity.Name;
            //string setValHex = "FFFFFFFFFF4D010200000000";
            //var type = 0;//
            //IP或域名选择
            //switch (addressType)
            //{
            //    case "00":
            //        setValHex += "00";
            //        string addr = "";
            //        setValHex += addr.PadLeft(58, '0');
            //        break;
            //    case "01":
            //        //IP转换
            //        var re = ips.Split('.');
            //        if (re.Length == 4)
            //        {
            //            setValHex += "01";
            //            string address = Convert.ToString(Convert.ToInt32(re[0]), 16).PadLeft(2, '0').ToUpper() +
            //                        Convert.ToString(Convert.ToInt32(re[1]), 16).PadLeft(2, '0').ToUpper() +
            //                        Convert.ToString(Convert.ToInt32(re[2]), 16).PadLeft(2, '0').ToUpper() +
            //                        Convert.ToString(Convert.ToInt32(re[3]), 16).PadLeft(2, '0').ToUpper();
            //            setValHex += address.PadLeft(58, '0');//位数不够补齐29位
            //        }
            //        break;
            //    case "02":
            //        //域名转换
            //        setValHex += "02";
            //        string yuming = byteToHexStr(System.Text.Encoding.Default.GetBytes(DomainName));
            //        setValHex += yuming.PadLeft(58, '0');//位数不够补齐29位
            //        break;
            //}
            //端口号
            //setValHex += Convert.ToString(Convert.ToInt32(ports), 16).PadLeft(4, '0').ToUpper();
            //北京时间默认
            //setValHex += "000000000000";
           
           
            //累计工时
            //if (Workhours == "00")
            //{
            //    setValHex += "FFFFFFFF";
            //}
            //else
            //{
            //    setValHex += Convert.ToString(Convert.ToInt32(Workhours), 16).PadLeft(8, '0').ToUpper();
            //}
            ////备用字节20
            //setValHex += "0000000000000000000000000000000000000000";
            ////type = 1;
            //Sendinfo SendinfoObj = new Sendinfo();
            //SendinfoObj.DeviceId = devid.Trim();
            //SendinfoObj.SendinfoDescription = sendStr;
            //SendinfoObj.SendinfoCommand = setValHex.ToUpper();
            //SendinfoObj.SendinfoPtime = DateTime.Now;
            //SendinfoObj.SendinfoStatus = 0;
            //UserInfo userinfo = (UserInfo)Session["LoginUser"];
            //if (userinfo != null)
            //{
            //    SendinfoObj.SendinfoUserid = userinfo.UserId;
            //}
            //string result = "";
            //string sss = GetWebService(devid, sendStr, setValHex, "2bd01c98-15db-4543-9f47-1eafd6999078",
            // dwcs, CmandType);
            ////调用Webservice接口默认UserID全部传入旧数据库管理员ID
            //if (sss != "error" && sss != "notExist")
            //{
            //    SendinfoObj.SendGuid = sss;
            //    result = sendinfoBll.InsertData(SendinfoObj, 0);//将流水号去掉，增加
            //}
            //else if (sss == "notExist")
            //{
            //    result = sendinfoBll.InsertData(SendinfoObj, -1);
            //}
            #endregion

            return "true";
        }

        [Log(LogMessage = "终端发送命令")]
        [UserFilter]
        public String DevSendCMD_Wudai(string devid, string sendtype, string jgtime, string dstime, string weekstr)
        {
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            string username = "";
            if (userinfo != null)
            {
                username = userinfo.UserName;
            }
            if (sendtype == "1")
            {
                if (int.Parse(jgtime) == 0)
                {
                    Transfers.ClintSendCommData(1160, "0005", "112", devid.Trim() + "&", "", "715", "", "", "", "", "", "", "<SPBSJ*P:BSJGPS*D:000>", "", "", "", username, "", "");
                }
                else
                {
                    if (int.Parse(jgtime) > 0 && int.Parse(jgtime) < 10)
                    {
                        Transfers.ClintSendCommData(1160, "0005", "112", devid.Trim() + "&", "", "715", "", "", "", "", "", "", "<SPBSJ*P:BSJGPS*D:00" + int.Parse(jgtime).ToString() + ">", "", "", "", username, "", "");
                    }
                    else if (int.Parse(jgtime) > 9 && int.Parse(jgtime) < 100)
                    {
                        Transfers.ClintSendCommData(1160, "0005", "112", devid.Trim() + "&", "", "715", "", "", "", "", "", "", "<SPBSJ*P:BSJGPS*D:0" + int.Parse(jgtime).ToString() + ">", "", "", "", username, "", "");
                    }
                    else if (int.Parse(jgtime) >= 100)
                    {
                        Transfers.ClintSendCommData(1160, "0005", "112", devid.Trim() + "&", "", "715", "", "", "", "", "", "", "<SPBSJ*P:BSJGPS*D:" + int.Parse(jgtime).ToString() + ">", "", "", "", username, "", "");
                    }
                }
            }
            else
            {
                string dstimestr = dstime.Substring(0, 2) + dstime.Substring(3, 2);
                Transfers.ClintSendCommData(1160, "0005", "112", devid.Trim() + "&", "", "715", "", "", "", "", "", "", "<SPBSJ*P:BSJGPS*W:1," + weekstr + "," + dstimestr + ">", "", "", "", username, "", "");
            }
            System.Threading.Thread.Sleep(1000);
            return "true";
        }

        public static string GetWebService(string devid, string sendStr, string setValHex, string useID,
             string dwcs, string CmandType)
        {
            ServiceReference1.InfoCollectionManagerSoapClient Inser = new ServiceReference1.InfoCollectionManagerSoapClient();
            string aaa = Inser.InsertCMDGetGUID(devid, sendStr, setValHex, useID, dwcs, CmandType);
            return aaa;
        }

        /// <summary>
        /// 新版无线GPS批量发送命令
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UserFilter]
        public ActionResult DevSendCMD_MultiNew(string str)
        {
            bool ISAdmin = false;
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                string userid = userinfo.UserId;
                if ((userid.ToLower() == "0".ToLower()) ||
                userid.ToLower() == "9386458A-C5A7-4560-A5C4-B798570FA7F5".ToLower()
                    || (userid.ToLower() == "2bd01c98-15db-4543-9f47-1eafd6999078".ToLower())
                    || userid.ToLower() == "c52cecbd-9268-4922-a823-b016aa993ba9".ToLower())
                {
                    ISAdmin = true;
                }
            }
            ViewBag.IsAdmin = ISAdmin;
            ViewBag.Ter_ID = str;
            return View();
        }

         [Log(LogMessage = "终端发送命令")]
        [HttpPost]
        [UserFilter]
        public String DevSendCMD_MultiNew(string devid, string ips, string ports, string gzmss,
            string gzsjs, string dssjs, string xmsjs, string scsjs, string dwcs,
            string CmandType, string DomainName, string addressType, string Activation,
            string Workhours,string LanyaState)
        {
            #region 旧代码
            //var dssjsStr = string.Empty;
            //if (dssjs == "0")
            //{
            //    dssjsStr = "未设置";
            //}
            //else
            //{
            //    dssjsStr = (Convert.ToInt32(dssjs) / 60).ToString();
            //}
            //string IsTimetype = "分钟";
            //if (gzmss == "03")
            //{
            //    IsTimetype = "秒";
            //}
            //string dd = (dwcs == "0") ? "不设置" : dwcs;
            //string sendStr = "模式：" + Model(gzmss) + ",工作时间：" + gzsjs + "(分钟),启动时间：" + dssjsStr
            //    + "(点),间隔时间：" + xmsjs + "(" + IsTimetype + "),定位次数：" + dd + "(次/天)";
            //string uid = HttpContext.User.Identity.Name;
            //string setValHex = "FFFFFFFFFF4D010200000000";
            //var type = 0;//
            ////IP或域名选择
            //switch (addressType)
            //{
            //    case "00":
            //        setValHex += "00";
            //        string addr = "";
            //        setValHex += addr.PadLeft(58, '0');
            //        break;
            //    case "01":
            //        //IP转换
            //        var re = ips.Split('.');
            //        if (re.Length == 4)
            //        {
            //            setValHex += "01";
            //            string address = Convert.ToString(Convert.ToInt32(re[0]), 16).PadLeft(2, '0').ToUpper() +
            //                        Convert.ToString(Convert.ToInt32(re[1]), 16).PadLeft(2, '0').ToUpper() +
            //                        Convert.ToString(Convert.ToInt32(re[2]), 16).PadLeft(2, '0').ToUpper() +
            //                        Convert.ToString(Convert.ToInt32(re[3]), 16).PadLeft(2, '0').ToUpper();
            //            setValHex += address.PadLeft(58, '0');//位数不够补齐29位
            //        }

            //        break;
            //    case "02":
            //        //域名转换
            //        setValHex += "02";
            //        string yuming = byteToHexStr(System.Text.Encoding.Default.GetBytes(DomainName));
            //        setValHex += yuming.PadLeft(58, '0');//位数不够补齐29位
            //        break;
            //}
            ////端口号
            //setValHex += Convert.ToString(Convert.ToInt32(ports), 16).PadLeft(4, '0').ToUpper();
            ////北京时间默认
            //setValHex += "000000000000";
            ////激活状态
            //switch (Activation)
            //{
            //    case "00":
            //        setValHex += "00";
            //        break;
            //    case "01"://测试
            //        setValHex += "01";
            //        break;
            //    case "02"://待激活
            //        setValHex += "02";
            //        break;
            //    case "03"://已激活
            //        setValHex += "03";
            //        break;
            //    default:
            //        setValHex += "00";
            //        break;
            //}
            ////模式
            //switch (gzmss)
            //{
            //    case "01"://标准
            //        //模式，工作，休眠（间隔时间），定位时间，间隔时间
            //        setValHex += "01";
            //        setValHex += Convert.ToString(Convert.ToInt32(gzsjs), 16).PadLeft(2, '0').ToUpper();
            //        setValHex += Convert.ToString(Convert.ToInt32(xmsjs), 16).PadLeft(4, '0').ToUpper();
            //        if (dssjs == "0")
            //        {
            //            setValHex += "FFFF";
            //        }
            //        else
            //        {
            //            setValHex += Convert.ToString(Convert.ToInt32(dssjs), 16).PadLeft(4, '0').ToUpper();
            //        }
            //        setValHex += "0000";
            //        break;
            //    case "02"://精准
            //        //模式，工作，休眠（间隔时间），定时时间，间隔时间
            //        setValHex += "02";
            //        setValHex += Convert.ToString(Convert.ToInt32(gzsjs), 16).PadLeft(2, '0').ToUpper();
            //        setValHex += Convert.ToString(Convert.ToInt32(xmsjs), 16).PadLeft(4, '0').ToUpper();
            //        if (dssjs == "0")
            //        {
            //            setValHex += "FFFF";
            //        }
            //        else
            //        {
            //            setValHex += Convert.ToString(Convert.ToInt32(dssjs), 16).PadLeft(4, '0').ToUpper();
            //        }
            //        setValHex += "0000";
            //        break;
            //    case "03"://追车
            //        //模式，工作，休眠（间隔时间），
            //        setValHex += "03" + "00" + "0000";
            //        //定时时间，
            //        if (dssjs == "0")
            //        {
            //            setValHex += "FFFF";
            //        }
            //        else
            //        {
            //            setValHex += Convert.ToString(Convert.ToInt32(dssjs), 16).PadLeft(4, '0').ToUpper();
            //        }
            //        //间隔时间
            //        setValHex += Convert.ToString(Convert.ToInt32(xmsjs), 16).PadLeft(4, '0').ToUpper();
            //        break;
            //    case "00"://不设置
            //        //模式，工作，休眠（间隔时间），定时时间，间隔时间
            //        setValHex += "00";
            //        setValHex += Convert.ToString(Convert.ToInt32(gzsjs), 16).PadLeft(2, '0').ToUpper();
            //        setValHex += Convert.ToString(Convert.ToInt32(xmsjs), 16).PadLeft(4, '0').ToUpper();
            //        if (dssjs == "0")
            //        {
            //            setValHex += "FFFF";
            //        }
            //        else
            //        {
            //            setValHex += Convert.ToString(Convert.ToInt32(dssjs), 16).PadLeft(4, '0').ToUpper();
            //        }
            //        setValHex += "0000";
            //        break;
            //    default:
            //        //gongzuo模式，工作shijian，休眠（间隔时间），定时时间，间隔时间
            //        setValHex += "00" + "00" + "0000" + "FFFF" + "0000";
            //        break;
            //}
            ////累计工时
            //if (Workhours == "00")
            //{
            //    setValHex += "FFFFFFFF";
            //}
            //else
            //{
            //    setValHex += Convert.ToString(Convert.ToInt32(Workhours), 16).PadLeft(8, '0').ToUpper();
            //}
            ////备用字节20
            //setValHex += "0000000000000000000000000000000000000000";
            //type = 1;
            //string result = "";
            //Sendinfo SendinfoObj = null;
            ////new Sendinfo();
            ////SendinfoObj.SendinfoDescription = sendStr;
            ////SendinfoObj.SendinfoCommand = setValHex.ToUpper();
            ////SendinfoObj.SendinfoPtime = DateTime.Now;
            ////SendinfoObj.SendinfoStatus = 0;
            //UserInfo userinfo = (UserInfo)Session["LoginUser"];
            ////if (userinfo != null)
            ////{
            ////    SendinfoObj.SendinfoUserid = userinfo.UserId;
            ////}
            //List<Sendinfo> listSendinfo = new List<Sendinfo>();
            //string[] devidarr = devid.Trim(',').Split(',');
            //for (int i = 0; i < devidarr.Length; i++)
            //{
            //    SendinfoObj = new Sendinfo();
            //    SendinfoObj.SendinfoDescription = sendStr;
            //    SendinfoObj.SendinfoCommand = setValHex.ToUpper();
            //    SendinfoObj.SendinfoPtime = DateTime.Now;
            //    SendinfoObj.SendinfoStatus = 0;

            //    if (userinfo != null)
            //    {
            //        SendinfoObj.SendinfoUserid = userinfo.UserId;
            //    }

            //    SendinfoObj.DeviceId = devidarr[i];
            //    listSendinfo.Add(SendinfoObj);
            //}
            ////插入数据表SendInfo与SendInfo_Last--------------------------------------------
            ////成功返回true，失败返回失败的设备编号
            //result = sendinfoBll.InsertDataList(listSendinfo);
            #endregion

            List<string> setValue = new List<string>();
            //设备状态
            switch (Activation)
            {
                case "00":
                    setValue.Add("V");
                    break;
                case "01"://测试
                    setValue.Add("1");
                    break;
                case "02"://待激活
                    setValue.Add("2");
                    break;
                case "03"://已激活
                    setValue.Add("3");
                    break;
                default:
                    setValue.Add("V");
                    break;
            }
            //模式
            switch (gzmss)
            {
                case "01"://标准
                    setValue.Add("1");
                    break;
                case "02"://精准
                    setValue.Add("2");
                    break;
                case "03"://追车
                    setValue.Add("3");
                    break;
                case "00"://不设置
                    setValue.Add("V");
                    break;
                default:
                    setValue.Add("V");
                    break;
            }
            //工作时间
            setValue.Add(gzsjs);
            //休眠时间
            if (dwcs == "0")
            {
                setValue.Add("V");
            }
            else
            {
                setValue.Add(Convert.ToInt32(24 * 60 / int.Parse(dwcs)).ToString());
            }

            //定时时间
            var dssjsStr = string.Empty;
            if (dssjs == "0")
            {
                setValue.Add("V");
            }
            else
            {
                setValue.Add((Convert.ToInt32(dssjs) / 60).ToString());
            }

            //间隔时间
            if (xmsjs == "0")
            {
                setValue.Add("V");
            }
            else
            {
                setValue.Add(xmsjs);
            }
            //累计工时
            if (Workhours == "00" || Workhours == "0")
            {
                setValue.Add("V");
            }
            else
            {
                setValue.Add(Workhours);
            }

            //IP或域名选择
            switch (addressType)
            {
                case "00":
                    setValue.Add("V");
                    setValue.Add("V");
                    break;
                case "01":
                    //IP转换 端口
                    setValue.Add("I*"+ips);
                    setValue.Add(ports);
                    break;
                case "02":
                    //域名转换 端口
                    setValue.Add("Y*"+DomainName);
                    setValue.Add(ports);
                    break;
            }

            //蓝牙设置
            if(LanyaState=="0")
            {
                setValue.Add("V");
            }
            else
            {
                setValue.Add(LanyaState);
            }

            var strparam = String.Join("`", setValue.ToArray());
            string[] terNoArr = devid.Trim(',').Split(',');
            var NewTer = "";
            for (int i = 0; i < terNoArr.Length; i++)
            {
                NewTer += terNoArr[i] + "&";
            }
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            string username = "";
            if (userinfo != null)
            {
                username = userinfo.UserName;
            }
            Transfers.ClintSendCommData(1160, "0005", "102", NewTer, "", "705", "", "", "", "", "", "", strparam.ToString(), "", "", "", username, "", "");
            return "true";
        }
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        public string Model(string m)
        {
            switch (m)
            {
                case "0":
                    return "标准";
                case "1":
                    return "精准";
                case "2":
                    return "追车";
                case "01":
                    return "标准";
                case "02":
                    return "精准";
                case "03":
                    return "追车";
                case "00":
                    return "不设置";
                default:
                    return "设置错误";
            }
        }

        public string GetSendCMDList(Sendcmd sendCmd, int rows, int page)
        {
            //sendInfo.StartData = (page - 1) * rows + 1;

            //sendInfo.EndData = sendInfo.StartData + rows;
            //IList<Sendinfo> isi = sendinfoBll.GetSendinfoListByTerNo(sendInfo);
            //int count = sendinfoBll.GetSendinfoCountByTerNo(sendInfo.DeviceId);
            //return ConvertToJson(isi, count);


            sendCmd.StartData = (page - 1) * rows + 1;
            sendCmd.EndData = sendCmd.StartData + rows;
            IList<Sendcmd> isi = sendcmdBll.GetSendcmdPage(sendCmd);
            int count = sendcmdBll.GetSendcmdCount(sendCmd.TrSetsn);
            return ConvertToJson(isi, count);
        }

        [ValidateInput(false)]
        public string GetSendCMDListByTerNos(Sendcmd sendcmd, int rows, int page)
        {
            sendcmd.StartData = (page - 1) * rows + 1;
            sendcmd.EndData = sendcmd.StartData + rows;
            string[] terNoArr = sendcmd.TrSetsn.Trim(',').Split(',');
            var NewTer = "";
            for (int i = 0; i < terNoArr.Length; i++)
            {
                NewTer += "'" + terNoArr[i] + "',";
            }
            sendcmd.TrSetsn = NewTer.Trim(',');
            IList<Sendcmd> isi = sendcmdBll.SelectSendcmdHistoryList(sendcmd);
            int count = sendcmdBll.SelectSendcmdHistoryListCount(sendcmd.TrSetsn);
            return ConvertToJson(isi, count);
        }

        /// <summary>
        /// 获取个终端最新的一条命令
        /// </summary>
        /// <param name="sendInfo"></param>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public string GetSendCMDListNeweastByTerNos(Sendcmd sendcmd, int rows, int page)
        {
            sendcmd.StartData = (page - 1) * rows + 1;
            sendcmd.EndData = sendcmd.StartData + rows;
            string[] terNoArr = sendcmd.TrSetsn.Trim(',').Split(',');
            var NewTer = "";
            for (int i = 0; i < terNoArr.Length; i++)
            {
                NewTer += "'" + terNoArr[i] + "',";
            }
            sendcmd.TrSetsn = NewTer.Trim(',');
            IList<Sendcmd> isi = sendcmdBll.SelectSendcmdListByTerNos(sendcmd);
            int count = sendcmdBll.SelectSendcmdCountByTerNos(sendcmd.TrSetsn);
            return ConvertToJson(isi, count);
        }

        /// <summary>
        /// 待发送命令和历史命令
        /// </summary>
        /// <returns></returns>
        public ActionResult CMDList(string TerGuid,string state)
        {
            ViewBag.TerGuid = TerGuid;
            ViewBag.TerState = state;
            return View();
        }

        public string CMDListData(string TerGuid, int rows, int page,string state)
        {
            Sendcmd sil = new Sendcmd();
            string TerNos = "";
            if (TerGuid != null && TerGuid.Trim() != "")
            {
                string[] TerArr = TerGuid.Trim(',').Split(',');
                if (TerArr.Length > 0)
                {
                    for (int i = 0; i < TerArr.Length; i++)
                    {
                        TerNos += "'" + TerArr[i] + "',";
                    }
                }
                sil.TrSetsn = TerNos.Trim(',');
                sil.StartData = (page - 1) * rows + 1;
                sil.EndData = sil.StartData + rows;
                if (state == "000")
                {
                    sil.TrState = "000";
                }
                else
                {
                    sil.TrState = "001";
                }
                IList<Sendcmd> isil = sendcmdBll.SelectSendcmdList(sil);
                int count = sendcmdBll.SelectSendcmdListCount(sil);
                return ConvertToJson(isil, count);
            }
            else
            {
                return "[]";
            }
        }

        public ActionResult CMDHistoryList(string TerNos)
        {
            ViewBag.TerNos = TerNos;
            return View();
        }

        [HttpPost]
        [UserFilter]
        public string GetPwdDC(string pwd)
        {
            UserInfo userinfo = (UserInfo)Session["LoginUser"];
            if (userinfo != null)
            {
                var PwdStr = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "md5");
                if (PwdStr == userinfo.UserPasswrd)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            return "false";
        }
    }
}