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

namespace SuperGPS.Controllers
{
	public class VersionInfoController : BaseController
    {
        private ColligateQueryService query = new ColligateQueryService();
        private VersionInfoBLL VerInfoBll = new VersionInfoBLL();

        [UserFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Log(LogMessage = "查看了版本管理")]
        //版本管理-查询版本
        public string GetList(VersionInfo vi, int rows, int page)
        {
            vi.StartData = (page - 1) * rows + 1;
            vi.EndData = vi.StartData + rows;

            IList<VersionInfo> imv = VerInfoBll.GetVersionViewPage(vi);

            int count = VerInfoBll.GetVersionViewCount(vi);

            return ConvertToJson(imv, count);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [Log(LogMessage = "版本添加")]
        [HttpPost]
        public ActionResult Add(VersionInfo vi)
        {
            vi.VerId = System.Guid.NewGuid().ToString();
            int k = VerInfoBll.Insert(vi);

            new LogMessage().Save("ID:" + vi.VerId + "。");

            if (k == 0)
            {
                return JavaScript("submitFormSucceed();");
            }
            else
            {
                return JavaScript("submitFormError();");
            }
        }

        [HttpGet]
        public ActionResult Edit(string VerId)
        {
            VersionInfo vi = new VersionInfo();

            vi.VerId = VerId;

            vi = VerInfoBll.GetVersionInfo(vi);

            ViewBag.VerId = vi.VerId;
            ViewBag.AppType = vi.AppType;
            ViewBag.PublishDate = vi.PublishDate;

            return View(vi);
        }

        [Log(LogMessage = "版本编辑")]
        [HttpPost]
        public ActionResult Edit(VersionInfo vi)
        {
            int k = VerInfoBll.Update(vi);

            new LogMessage().Save("ID:" + vi.VerId + "。");

            if (k == 1)
            {
                return JavaScript("editFormSucceed();");
            }
            else
            {
                return JavaScript("editFormError();");
            }
        }

        [Log(LogMessage = "版本删除")]
        public string Delete(string VerId)
        {
            int k = VerInfoBll.Delete(VerId);

            new LogMessage().Save("ID:" + VerId + "。");

            if (k == 1)
            {
                return "删除成功！";
            }
            else
            {
                return "删除失败！";
            }
        }

        [Log(LogMessage = "读取版本信息")]
        public string Config(string AppType)
        {
            string val = "[{'err':'参数错误或无值'}]";

            if (AppType == null)
            {
                AppType = "'android','iso'";
            }
            else
            {
                AppType = "'" + AppType + "'";
            }

            string sql = @"
                        select t.ver_id,
                               t.ver_name,
                               t.ver_number,
                               t.app_type,
                               t.app_url,
                               t.publisher,
                               to_char(t.publish_date, 'yyyy-mm-dd hh24:mi:ss') publish_date,
                               t.description
                          from (select t.ver_id,
                                       t.ver_name,
                                       t.ver_number,
                                       t.app_type,
                                       t.app_url,
                                       t.publisher,
                                       t.publish_date,
                                       t.description,
                                       row_number() over(partition by t.app_type order by t.publish_date desc) as rnum
                                  from VERSION_INFO t
                                 where lower(t.app_type) in ( " + AppType.Trim().ToLower() + @" ) ) t
                         where t.rnum = 1
                        ";

            sql = @"
                        select 
                               t.ver_name,
                               t.ver_number,
                               t.app_url

                          from (select t.ver_id,
                                       t.ver_name,
                                       t.ver_number,
                                       t.app_type,
                                       t.app_url,
                                       t.publisher,
                                       t.publish_date,
                                       t.description,
                                       row_number() over(partition by t.app_type order by t.publish_date desc) as rnum
                                  from VERSION_INFO t
                                 where lower(t.app_type) in ( " + AppType.Trim().ToLower() + @" ) ) t
                         where t.rnum = 1
                        ";

            System.Data.DataSet ds = query.GetColligateQuery("ColligateQuery.ProteanQuery", sql);

            if (ds != null 
                && ds.Tables.Count > 0 
                && ds.Tables[0].Rows.Count > 0)
            {
                val = Dtb2Json(ds.Tables[0]);
            }

            if (val.Trim().Length > 0)
            {
                val = val.Substring(1, (val.Length - 2));
            }

            return val;
        }
	}
}
