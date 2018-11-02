using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;

namespace GenrerConfigWeb.Util
{
    public class UtilityTools
    {
        //****************//        

        //*******************//
        public static readonly string appSettings = "appSettings";

        public static string GetAppSettingUnity(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string connectionStrings { get { return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; } }

        /// <summary>
        /// 获取Cookie中RoleID分隔符
        /// </summary>
        /// <returns></returns>
        public static char[] UserInfoSeparator
        {
            get
            {
                return GetAppSettingUnity("Separator").ToCharArray();
            }
        }

        /// <summary>
        /// 车辆编号取反
        /// </summary>
        /// <param name="strcar"></param>
        /// <returns></returns>
        public static string convert(string strcar)//字符串反转
        {
            string strtemp = "";
            for (int i = strcar.Length - 1; i >= 0; i--)
            {
                if (i % 2 == 0)
                    strtemp = strtemp + strcar.Substring(i + 1, 1);
                else
                    strtemp = strtemp + strcar.Substring(i - 1, 1);
            }
            return strtemp;
        }

        //时间转换

        #region  获得认证
        /// <summary>
        /// 获得认证
        /// </summary>
        /// <returns></returns>
        //public static string GetPower()
        //{
        //    if (null == HttpRuntime.Cache.Get("power"))
        //    {
        //        string files = string.Empty;
        //        try
        //        {
        //            files = DESEncrypt.ReadFile(GetAppSettingUnity("desFilePath"));
        //            string orDes = DESEncrypt.DesDecrypt(DESEncrypt.BinaryStringToByteArray(files));
        //            if (orDes.IndexOf(DESEncrypt.getHostIpName()) != 0)
        //            {
        //                HttpRuntime.Cache.Insert("power", "");
        //            }
        //            else
        //            {
        //                HttpRuntime.Cache.Insert("power", orDes);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            HttpRuntime.Cache.Insert("power", "");
        //        }

        //    }
        //    return HttpRuntime.Cache.Get("power").ToString();
        //}
        #endregion

        public static string GetTimeSet(string TimeM)
        {
            string i = TimeM;
            string k = Convert.ToString(Convert.ToInt32(i, 10), 16).ToUpper();
            string m = new string('0', 4 - k.Length);
            string Time = m + k;
            string TimeNew = Time.Substring(2, 2) + Time.Substring(0, 2) + "F";
            return TimeNew;
        }

        /// <summary>
        /// 获取Cookie中RoleID分隔符
        /// </summary>
        /// <returns></returns>
        public static char[] UserInfoSmallSeparator
        {
            get
            {
                return GetAppSettingUnity("SmallSeparator").ToCharArray();
            }
        }

        public static int GetUerID()
        {
            string id = HttpContext.Current.User.Identity.Name;
            int idI = 0;
            int.TryParse(id, out idI);
            return idI;
        }

        internal static string GetCookie(string currentUserName)
        {
            if (HttpContext.Current.Request.Cookies[currentUserName] != null)
            {
                string useCookie = HttpContext.Current.Request.Cookies[currentUserName].Value;
                useCookie = Encoding.UTF8.GetString(Convert.FromBase64String(useCookie));//解码
                return useCookie;
            }
            return string.Empty;
        }

        internal static void SetCookie(string useID, string cookieString, HttpContextBase HttpContext)
        {
            HttpCookie cookie = new HttpCookie(useID);
            //string domain = "";// GetConfigUnity("CookieDomain");
            //if (!string.IsNullOrEmpty(domain))
            //{
            //    cookie.Domain = domain;
            //}
            cookie.Value = cookieString;
            HttpContext.Response.AppendCookie(cookie);
        }

        public static void RemoveCookie(HttpContextBase httpContext)
        {
            httpContext.Response.SetCookie(new HttpCookie(httpContext.User.Identity.Name, null) { Expires = DateTime.Now.AddDays(-1) });
        }

        /// <summary>
        /// dataset转json  
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public  string DataTableToJson(DataTable dt, int Total)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{");
                jsonBuilder.Append("\"Rows\"");
                jsonBuilder.Append(":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    jsonBuilder.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        //jsonBuilder.Append("\"");
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(dt.Columns[j].ColumnName);
                        jsonBuilder.Append("\":\"");

                        jsonBuilder.Append(dt.Rows[i][j].ToString());

                        jsonBuilder.Append("\",");
                    }
                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("},");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("],");
                jsonBuilder.Append("\"Total\":");
                jsonBuilder.Append(Total);
                jsonBuilder.Append("}");
                return jsonBuilder.ToString();
            }
            else
            {
                return "{\"Rows\":[{}],\"Total\":0}";
            }
        }
        /// <summary>
        /// 通用表格查询
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="strSortName"></param>
        /// <param name="sortorder"></param>
        /// <param name="strParam"></param>
        /// <param name="nPagesize"></param>
        /// <param name="nPage"></param>
        /// <param name="nRowsCount"></param>
        /// <returns></returns>
        //public static string QueryToTable(string spName, string strSortName, string sortorder, string strParam, int nPagesize, int nPage, ref int nRowsCount)
        //{
        //    DataSet ds = new DataSet();
        //    ProcedureBuilder pb = new ProcedureBuilder();
        //    DataProvider dataProvider = new DataProvider();
        //    try
        //    {
        //        // 指定存储过程名称
        //        pb.SetProcName(spName);
        //        pb.Command.Parameters.Clear();
        //        pb.PutPara(strSortName);      // 排序字段
        //        pb.PutPara(sortorder);      // 排序方式
        //        pb.PutPara(nPagesize);      // 每页记录数
        //        pb.PutPara(nPage);     // 当前页码 
        //        pb.PutPara(strParam);      // 查询条件     
        //        ds = dataProvider.ExcCmdDataSetEx(pb, out nRowsCount);
        //        DataTable dt = ds.Tables[0];
        //        return DataTableToJson(dt, nRowsCount);
        //        //  return ds;
        //    }
        //    catch (Exception e)
        //    {
        //        // 记录错误日志
        //        string strErr = e.Message;
        //    }

        //    return null;
        //}
        ///// <summary>
        ///// 通用删除
        ///// </summary>
        ///// <param name="dID"></param>
        ///// <returns></returns>
        //internal bool Delete(string spName, string dID)
        //{
        //    ProcedureBuilder pb = new ProcedureBuilder();
        //    DataProvider dataProvider = new DataProvider();
        //    // 指定存储过程名称
        //    pb.SetProcName(spName);
        //    pb.Command.Parameters.Clear();
        //    // 构造存储过程参数
        //    pb.PutPara(dID);

        //    int nRet = 0;
        //    try
        //    {
        //        dataProvider.ExcCmdNoRecordEx(pb, out nRet);
        //        if (nRet > 0)
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        // 记录错误日志
        //        string strErr = e.Message;
        //    }

        //    return false;
        //}

        /// <summary>
        /// 获得客户端IP
        /// </summary>
        /// <returns></returns>
        public static string getClientIP()
        {
            //string client_IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //if (client_IP == null || client_IP == String.Empty)
            //    client_IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //if (client_IP == null || client_IP == String.Empty)
            //    client_IP = HttpContext.Current.Request.UserHostAddress;
            //return client_IP;
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理 
                if (result.IndexOf(".") == -1) //没有“.”肯定是非IPv4格式 
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。 
                        result = result.Replace(" ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i]; //找到不是内网的地址 
                            }
                        }
                    }
                    else if (IsIPAddress(result)) //代理即是IP格式 
                        return result;
                    else
                        result = null; //代理中的内容 非IP，取IP 
                }
            }
            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (result == null || result == String.Empty)
                result = HttpContext.Current.Request.UserHostAddress;
            if (result.Equals("::1"))
            {
                result = "127.0.0.1";
            }
            return result;


        }

        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }


        internal static string converStrHex(string correctionTime, int p)
        {
            int i = -1;
            int.TryParse(correctionTime, out i);
            return Convert.ToString(i, 16).ToUpper().PadLeft(8, '0');
        }

        /// <summary>
        /// 高低位转为正常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String RenH2L(string str)
        {
            int isInt = str.Length % 2;
            if (isInt != 0)
            {
                str = str.Insert(str.Length - 1, "0");// str.PadLeft(str.Length + isInt, '0');
            }
            StringBuilder strTem = new StringBuilder("");
            int len = str.Length;

            for (int i = 0; i < len; i += 2)
            {
                strTem.Append(str.Substring(len - 2 - i, 2));
            }
            return strTem.ToString();
        }
    }
}