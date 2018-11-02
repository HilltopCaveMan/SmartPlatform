using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GModel.Car;
using GModel.Basic;
using GBLL.Car;
using GCommon;
using System.IO;
using System.Text;
using SuperGPS.App_Start;
using GBLL.Basic;

namespace SuperGPS.Controllers
{
    public class TerminalBindController : BaseController
    {
        TerminalInfoBLL tib = new TerminalInfoBLL();

        [UserFilter]
        public ActionResult Index()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                ViewBag.Deptid = user.EnterId;
            }
            return View();
        }

        [HttpPost]
        public string GPSSelect()
        {
            List<TerminalBind> ltb = new List<TerminalBind>();
            TerminalBind tb = new TerminalBind();
            ltb.Add(tb);
            ltb.Add(tb);
            ltb.Add(tb);
            ltb.Add(tb);
            ltb.Add(tb);
            ltb.Add(tb);
            ltb.Add(tb);
            ltb.Add(tb);
            ltb.Add(tb);
            ltb.Add(tb);
            return ConvertToJson(ltb, 10);
        }

        [Log(LogMessage = "绑定终端车辆信息")]
        [UserFilter]
        public string AddCarAndTer(TerminalBind tb)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                if (tb.DeptId != null && tb.DeptId.Trim() == "")
                {
                    tb.DeptId = user.EnterId;
                }
                DeptInfoBLL deptInfoBll = new DeptInfoBLL();
                DeptInfo di = deptInfoBll.GetDeptInfo(tb.DeptId);
                string deptcode = di.Businessdivisioncode;
                tb.Businessdivisioncode = deptcode;
                string carinfostr = "";
                CarTypeBLL cartypebll = new CarTypeBLL();
                CarType ct = cartypebll.GetCarType(tb.TypeId);
                if (ct != null)
                {
                    carinfostr = ct.TypeName + "||||||||||||||||||||||||||||||";
                }
                string result=tib.AddCarAndTer(tb);

                new LogMessage().Save("TerNo:" + tb.TerNo + "。");

                if (result=="true")
                {
                    //绑车接口
                    Transfers.ClintSendCommData(1107, "50", "", tb.CarNo, "", "", "", "", "", "", "", deptcode, tb.CarNo, carinfostr, "", "", "", "", user.UserName);

                    //刷新车辆
                    Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "2", "", "", "", "", "", "");
                }
                return result;
            }
            else
            {
                return "false";
            }
        }

        [Log(LogMessage = "查看终端绑定列表")]
        [UserFilter]
        public string GetTerBindList(TerminalBind tb, int rows, int page)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                tb.StartData = (page - 1) * rows + 1;
                tb.EndData = tb.StartData + rows;
                if (tb.DeptId == null || tb.DeptId.Trim() == "") 
                {
                    tb.DeptId = user.EnterId;
                }

                IList<TerminalBind> itb = tib.GetTerminalBindPage(tb);
                int c = tib.GetTerminalBindCount(tb);
                return ConvertToJson(itb, c);
            }
            else 
            {
                return "[]";
            } 
        }

        [UserFilter]
        public ActionResult UpLoad()
        {
            return View();
        }

        [Log(LogMessage = "上传导入终端信息")]
        [UserFilter]
        public ActionResult UpLoadForm(string DeptId, HttpPostedFileBase file)
        {
            //CreateExcel();
            if (file != null && file.ContentLength > 0 && DeptId != null && DeptId.Trim() != "")
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Files"), System.Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                file.SaveAs(filePath);

                new LogMessage().Save("文件:" + filePath + "。");

                string val = "";
                ExcelUpLoad eu = new ExcelUpLoad();
                string msg = "";
                List<UpLoadTerBind> lut = eu.ReadExcel(filePath,ref msg);
                
                if (lut != null && lut.Count > 0)
                {
                    val = tib.InsertExcelData(DeptId, lut);
                     DeptInfoBLL deptInfoBll = new DeptInfoBLL();
                     DeptInfo di = deptInfoBll.GetDeptInfo(DeptId);
                     string deptcode = di.Businessdivisioncode;
                    UserInfo user = (UserInfo)Session["LoginUser"];
                    for (int m = 0; m < lut.Count; m++)
                    {
                        if (!val.Contains(lut[m].TerNo) && val!="false")
                        {
                            string tertype = "";
                            if (lut[m].TerType == "一代无线GPS")
                            {
                                tertype = "104";
                            }
                            else if (lut[m].TerType == "二代无线GPS")
                            {
                                tertype = "102";
                            }
                            else if (lut[m].TerType == "Homer3M" || lut[m].TerType == "Homer3B-2")
                            {
                                tertype = "101";
                            }
                            else if (lut[m].TerType == "五代无线GPS")
                            {
                                tertype = "112";
                            }
                            else if (lut[m].TerType == "五代有线GPS")
                            {
                                tertype = "111";
                            }
                            //添加终端的接口
                            Transfers.ClintSendCommData(1108, "2", "1", lut[m].TerNo, "", "", "", lut[m].TerInnettime.ToString("yyyy-MM-dd HH:mm:ss"), "", "", "", deptcode, lut[m].SimCard, tertype, "", "", "", "", user.UserName);
                        }
                    }

                    ViewBag.ReturnVal = val.Trim(',');
                }
                else if (msg.Trim() != "")
                {
                    ViewBag.ReturnVal = msg;
                }
                else
                {
                    ViewBag.ReturnVal = "false";
                }
            }
            else if (DeptId == null || DeptId.Trim() == "")
            {
                ViewBag.ReturnVal = "请选择导入终端的企业";
            }
            else if (file == null || file.ContentLength <= 0)
            {
                ViewBag.ReturnVal = "请选择导入的文件！";
            }
            return View();
        }

       
    }
}