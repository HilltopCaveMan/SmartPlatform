using GBLL;
using GBLL.Basic;
using GModel.Basic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperGPS.Controllers
{
    public class AppSettingController : BaseController
    {
        private ColligateQueryService query = new ColligateQueryService();

        public ActionResult Index()
        {
            return View();
        }

        public string GetAppSetting(string username)
        {

            string val = "{\"status\":\"0\",\"message\":\"获取失败\",\"result\":{}}";

            if (username != "")
            {
                string sql = @"
                              SELECT t.username     as UserName,
                              nvl(t.total_setting,'')       as totalSetting,
                              nvl(t.online_setting,'')      as onlineSetting,
                              nvl(t.offline_setting,'')     as offlineSetting,
                              nvl(t.other_setting,'')       as otherSetting,
                              nvl(t.demolition_alarm,'')    as demolitionAlarm,
                              nvl(t.overspeed_alarm,'')     as overspeedAlarm,
                              nvl(t.zone_alarm,'')          as zoneAlarm,
                              nvl(t.power_off_alarm,'')     as poweroffAlarm,
                              nvl(t.push_setting,'')        as pushSetting,
                              nvl(t.stock_setting,'')       as stockSetting,
                              nvl(t.expired_setting,'')     as expiredSetting,
                              nvl(t.warn_setting,'')        as warnSetting
                              FROM AppSetting t
                              WHERE t.username='" + username + "'";

                System.Data.DataSet ds = query.GetColligateQuery("ColligateQuery.ProteanQuery", sql);

                if (ds != null
                    && ds.Tables.Count > 0
                    && ds.Tables[0].Rows.Count > 0)
                {

                    val = Dtb2Json(ds.Tables[0]);
                    if(val.Length>=3)
                    {
                        string result=val.Substring(1,val.Length-2);
                        val = "{\"status\":\"1\",\"message\":\"获取成功\",\"result\":" + result + "}";
                    }
                }
            }

            return val;
        }

        public string AddEditAppSetting(string uploadString)
        {
            //--设置结果  1 开 0 关 
            int result = 0;
            string SuccessResult = "{\"status\":\"1\",\"message\":\"操作成功\",\"result\":{}}";
            string ErrorResult = "{\"status\":\"0\",\"message\":\"操作失败\",\"result\":{}}";
            if (uploadString == null || uploadString=="")
            {
                return ErrorResult;
            }
            AppSettingBLL appsetbll = new AppSettingBLL();
            AppSetting appset = JsonConvert.DeserializeObject<AppSetting>(uploadString); 
            appset.settingTime = DateTime.Now;
            AppSetting IsAppSet = appsetbll.GetAppSettingData(appset.UserName);
            if (IsAppSet == null)
            {
                result = appsetbll.Insert(appset);
            }
            else
            {
                result = appsetbll.Update(appset);
            }

            if (result >= 0)
            {
                return SuccessResult;
            }
            else
            {
                return ErrorResult;
            }
        }
	}
}