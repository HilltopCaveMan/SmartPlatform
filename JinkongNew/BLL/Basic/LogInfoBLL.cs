using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using GModel.Car;

using GInterfaceDAL.Car;
using GDAL.Car;
using GModel.Basic;
using GModel;

using GDAL.Basic;
using GInterfaceDAL.Basic;

namespace GBLL.Basic
{
    public class LogInfoBLL
    {
        private static object obj_lock = new object();

        private static ILogInfoDao _iLogInfoDao = new LogInfoDao();

        private static IList<LogInfo> LogInfoList = new List<LogInfo>();

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="gnmc">功能名称</param>
        /// <param name="gndz">功能地址</param>
        /// <param name="czr">操作人</param>
        /// <param name="bz">日志内容描述</param>
        public static void Insert(string gnmc,string gndz,string czr,string bz)
        {
            LogInfo log = new GModel.Basic.LogInfo();

            log.LogType = LogType.Operation;

            log.GNMC = gnmc;
            log.GNDZ = gndz;
            log.CZR = czr;
            log.BZ = bz;

            log.CZSJ = DateTime.Now;
            log.WLBS = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();

            UserInfo user = new UserInfo();
            user = (UserInfo)System.Web.HttpContext.Current.Session["LoginUser"];
            log.DeptId = user.EnterId;

            LogInfoBLL.Add(log);
        }

        public static void Add(LogInfo log){

            lock (obj_lock)
            {
                LogInfoList.Add(log);

                if (log.LogType == LogType.Exception)
                {
                    Save();
                }
                else
                {
                    if (LogInfoList.Count > 100)
                    {
                        Save();
                    }
                }
            }
        }

        public static void Save()
        {
            //写库并清空
            _iLogInfoDao.Save(LogInfoList);

            LogInfoList.Clear();
        }

        public static List<LogInfo> GetLogInfoListPage(Hashtable ht, out int rowCount)
        {
            List<LogInfo> list = _iLogInfoDao.GetLogInfoListPage(ht);

            if (list == null) list = new List<GModel.Basic.LogInfo>();

            rowCount = _iLogInfoDao.GetLogInfoCount(ht);

            return list;
        }

        public static int CreateLogTable()
        {
            return _iLogInfoDao.CreateLogTable();
        }
    }
}
