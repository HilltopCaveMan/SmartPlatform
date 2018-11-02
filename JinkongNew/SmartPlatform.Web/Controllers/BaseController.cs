using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ConsoleApplicationGPS.Models;
using GModel.Location;

namespace SuperGPS.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public string Dtb2Json(DataTable dtb)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            
            foreach (DataRow dr in dtb.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> 
                    drow = new System.Collections.Generic.Dictionary<string, object>();

                foreach (DataColumn dc in dtb.Columns)
                {
                    drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                dic.Add(drow);
            }

            //序列化  
            return jss.Serialize(dic);
        }

        public string ConvertToJson(object obj)
        {
             IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                    timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string JsonStr = "";
            
            JsonStr = JsonConvert.SerializeObject(obj,Newtonsoft.Json.Formatting.Indented,timeFormat);
            return JsonStr;
        }

        public string ConvertToJsonNew(object obj)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string JsonStr = "";

            List<RealtimeData> list = new List<RealtimeData>();
            for (int i = 0; i < ((List<DataList>)obj).Count;i++)
            {
                list.Add(((List<DataList>)obj)[i].realtime_dataObj);               
            }
            JsonStr = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented, timeFormat);
            return JsonStr;
        }

        public string ConvertToJsonSingle(object obj)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string JsonStr = "";

            List<RealtimeData> list = new List<RealtimeData>();
            for (int i = 0; i < ((List<DataList>)obj).Count; i++)
            {
                list.Add(((List<DataList>)obj)[i].realtime_dataObj);
            }
            JsonStr = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented, timeFormat);
            return JsonStr;
        }
        public string ConvertToJson(object obj, int total)
        {
            string uu ="{\"total\":" + total + ", \"rows\":" + ConvertToJson(obj) + "}";
            return uu;
        }

        public string DtbConvertToJson(DataTable dtb,int total) 
        {
            return "{\"total\": " + total + ", \"rows\":" + Dtb2Json(dtb) + "}";
        }  


       
    }
}