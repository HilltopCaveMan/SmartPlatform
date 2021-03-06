// Generated by IBatisNetModel

using System;

using GInterfaceDAL.Basic;
using System.Collections;
using System.Collections.Generic;
using GModel.Basic;
using IBatisNet.DataMapper;
using System.Data;
using System.IO;
using System.Text;

using Autofac;
using Autofac.Configuration;

namespace GDAL.Basic
{
    public class LogInfoDao : BaseSqlMapDao, ILogInfoDao
    {
        public object Insert(LogInfo log)
        {
            bool flg = false;
            try
            {
                string sql = "";

                switch (log.LogType)
                {
                    case LogType.Operation:
                        sql = (new Log2DB()).GetOperationSQL(log);
                        break;
                    case LogType.Exception:
                        sql = (new Log2DB()).GetExceptionSQL(log);
                        break;
                }

                if (sql.Trim().Length > 0)
                {
                    (new ColligateQueryDao()).UpdateColligateQuery(sql);
                }
                flg = true;
            }
            catch (Exception e)
            {
                
            }

            return flg;
        }

        public object Save(IList<LogInfo> list)
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<LogManager>();

            builder.RegisterType<Log2DB>().As<ILog>();
            
            using (var container = builder.Build())
            {
                var manager = container.Resolve<LogManager>();
                manager.Save(list);
            }

            return null;
        }

        public List<LogInfo> GetLogInfoListPage(Hashtable o)
        {
            List<LogInfo> list = new List<LogInfo>();

            string sql = GetLogList_SQL(o);
            if (sql.Trim().Length == 0) return null;

            sql = GetSQL_FY(o, sql);

            System.Data.DataSet ds = this.QueryForDataSet("ColligateQuery.ProteanQuery", sql);

            if (ds != null
                && ds.Tables.Count > 0
                && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    LogInfo node = new LogInfo();

                    if (dr["GNMC"] != DBNull.Value)
                    {
                        node.GNMC = dr["GNMC"].ToString();
                    }
                    if (dr["GNDZ"] != DBNull.Value)
                    {
                        node.GNDZ = dr["GNDZ"].ToString();
                    }
                    if (dr["YCMS"] != DBNull.Value)
                    {
                        node.YCMS = dr["YCMS"].ToString();
                    }

                    if (dr["YCSJ"] != DBNull.Value)
                    {
                        node.YCSJ = Convert.ToDateTime(dr["YCSJ"]);
                    }

                    if (dr["CZR"] != DBNull.Value)
                    {
                        node.CZR = dr["CZR"].ToString();
                    }

                    if (dr["CZSJ"] != DBNull.Value)
                    {
                        node.CZSJ = Convert.ToDateTime(dr["CZSJ"]);
                    }

                    if (dr["LYLX"] != DBNull.Value)
                    {
                        node.LYLX = dr["LYLX"].ToString();
                    }
                    if (dr["WLBS"] != DBNull.Value)
                    {
                        node.WLBS = dr["WLBS"].ToString();
                    }
                    if (dr["BZ"] != DBNull.Value)
                    {
                        node.BZ = dr["BZ"].ToString();
                    }

                    list.Add(node);
                }
            }
            else
            {
                return null;
            }

            return list;
        }

        public int GetLogInfoCount(Hashtable o)
        {
            int count = 0;

            string sql = GetLogListCnt_SQL(o);

            if (sql.Trim().Length == 0) return 0;

            System.Data.DataSet ds = this.QueryForDataSet("ColligateQuery.ProteanQuery", sql);

            if (ds != null
                && ds.Tables.Count > 0
                && ds.Tables[0].Rows.Count > 0)
            {
                count = Convert.ToInt16(ds.Tables[0].Rows[0][0]);
            }

            return count;
        }

        public string GetLogListCnt_SQL(Hashtable ht)
        {
            string LogTypeNo = "";

            if (ht["LogTypeNo"] != null)
            {
                LogTypeNo = ht["LogTypeNo"].ToString();
            }

            string KSRQ = "";
            if (ht["KSRQ"] != null)
            {
                KSRQ = ht["KSRQ"].ToString();
            }
            string JSRQ = "";
            if (ht["JSRQ"] != null)
            {
                JSRQ = ht["JSRQ"].ToString();
            }

            string DeptID = "";
            if (ht["DeptID"] != null)
            {
                DeptID = ht["DeptID"].ToString();
            }

            string tabs = GetTablesSQL(KSRQ, JSRQ);

            string sql = "";

            // == 1 操作日志  
            if (LogTypeNo == "1" && tabs!="")
            {
                sql = @" select count(*)
                              from ( " + tabs + @" ) t
                              where 
                                t.dept_id in
                                       (select d.businessdivisionid dept_id
                                          from (select * from dept_info t where t.isdel = 0) d 　　
                                         start with d.businessdivisionid = '" + DeptID + @"' 　　
                                        connect by prior d.businessdivisionid = d.fatherid)

                                and to_char(t.czsj,'yyyy-MM-dd') >= '" + KSRQ + @"'
                                and to_char(t.czsj,'yyyy-MM-dd') <= '" + JSRQ + @"' ";
            }
            // == 2  
            if (LogTypeNo == "2")
            {
                sql = @" select count(*)
                              from LOG_EXCEPTION t
                              where to_char(t.ycsj,'yyyy-MM-dd') >= '" + KSRQ + @"'
                                and to_char(t.ycsj,'yyyy-MM-dd') <= '" + JSRQ + @"' ";
            }

            return sql;
        }

        public string GetLogList_SQL(Hashtable ht)
        {
            string LogTypeNo = "";

            if (ht["LogTypeNo"] != null)
            {
                LogTypeNo = ht["LogTypeNo"].ToString();
            }

            string KSRQ = "";
            if (ht["KSRQ"] != null)
            {
                KSRQ = ht["KSRQ"].ToString();
            }
            string JSRQ = "";
            if (ht["JSRQ"] != null)
            {
                JSRQ = ht["JSRQ"].ToString();
            }

            string DeptID = "";
            if (ht["DeptID"] != null)
            {
                DeptID = ht["DeptID"].ToString();
            }

            string tabs = GetTablesSQL(KSRQ, JSRQ);

            string sql = "";

            // == 1 操作日志  
            if (LogTypeNo == "1" && tabs!="")
            {
                //

                sql = @" SELECT T.GNMC,
                                   T.GNDZ,
                                   '' YCMS,
                                   '' YCSJ,
                                   T.LYLX,
                                   T.WLBS,
                                   T.CZR,
                                   T.CZSJ,
                                   T.BZ
                              FROM ( " + tabs + @" ) T
                              where 
                                t.dept_id in
                                       (select d.businessdivisionid dept_id
                                          from (select * from dept_info t where t.isdel = 0) d 　　
                                         start with d.businessdivisionid = '" + DeptID + @"' 　　
                                        connect by prior d.businessdivisionid = d.fatherid)

                                and to_char(t.czsj,'yyyy-MM-dd') >= '" + KSRQ + @"'
                                and to_char(t.czsj,'yyyy-MM-dd') <= '" + JSRQ + @"' order by t.czsj desc ";
            }
            // == 2  
            if (LogTypeNo == "2")
            {
                sql = @" select t.gnmc,
                                   t.gndz,
                                   t.ycms,
                                   t.ycsj,
                                   '' lylx,
                                   t.wlbs,
                                   '' czr,
                                   '' czsj,
                                   t.bz
                              from LOG_EXCEPTION t
                              where to_char(t.ycsj,'yyyy-MM-dd') >= '" + KSRQ + @"'
                                and to_char(t.ycsj,'yyyy-MM-dd') <= '" + JSRQ + @"' order by t.ycsj desc ";
            }

            return sql;
        }

        private string GetTablesSQL(string KSRQ, string JSRQ)
        {
            DateTime ks = Convert.ToDateTime(KSRQ);
            DateTime js = Convert.ToDateTime(JSRQ);
            string tabs = ",'LOG_OPERATION" + ks.ToString("yyyyMM") + "'";

            while (ks < js)
            {
                ks = ks.AddMonths(1);
                tabs += ",'" + ks.ToString("yyyyMM") + "'";
            }

            tabs += ",'" + js.ToString("yyyyMM") + "'";

            string sql = @"
                    select distinct t.TABLE_NAME
                        from user_tables t
                        where t.TABLE_NAME in (" + tabs.Substring(1) + ") ";

            System.Data.DataSet ds = this.QueryForDataSet("ColligateQuery.ProteanQuery", sql);

            sql = "";

            if (ds != null
                && ds.Tables.Count > 0
                && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sql += "union select * from " + dr["TABLE_NAME"].ToString() + " ";
                }
            }
            if (sql.Trim().Length > 0)
            {
                sql = sql.Substring(5);
            }

            return sql;

        }

        /// <summary>
        /// 添加分页
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private string GetSQL_FY(Hashtable ht, string sql)
        {
            //识别分页
            //*********************************************************************
            int StartData = 0;
            int EndData = 15;

            if (ht["StartData"] != null
                && ht["StartData"].ToString().Trim().Length > 0)
            {
                StartData = Convert.ToInt16(ht["StartData"]);
            }
            if (ht["EndData"] != null
                && ht["EndData"].ToString().Trim().Length > 0)
            {
                EndData = Convert.ToInt16(ht["EndData"]);
            }

            sql = @" select p.*
                       from (select w.*, rownum rn from (" + sql + @") w) p
                      where p.rn >= " + StartData + @"
                        and p.rn < " + EndData + @" ";

            //*********************************************************************

            return sql;
        }

        public int CreateLogTable()
        {
            int flg = 0;
            string ny = System.DateTime.Now.ToString("yyyyMM");
            object obj_lock = new object();

            lock (obj_lock)
            {
                string sql = @"
                    select t.TABLE_NAME
                        from user_tables t
                        where t.TABLE_NAME = 'LOG_OPERATION" + ny + "'";

                System.Data.DataSet ds = this.QueryForDataSet("ColligateQuery.ProteanQuery", sql);

                if (ds != null
                    && ds.Tables.Count > 0
                    && ds.Tables[0].Rows.Count > 0)
                {
                    //count = Convert.ToInt16(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    //创建
                    sql = @"
                    create table LOG_OPERATION" + ny + @"
                    (
                        log_id VARCHAR2(100) not null,
                        gnmc   VARCHAR2(50) not null,
                        gndz   VARCHAR2(100),
                        lylx   VARCHAR2(50),
                        wlbs   VARCHAR2(50),
                        czr    VARCHAR2(50),
                        czsj   DATE,
                        bz     VARCHAR2(100),
                        dept_id     VARCHAR2(100)
                    )
                ";

                    flg = (new ColligateQueryDao()).UpdateColligateQuery(sql);
                }
            }

            return flg;
        }
    }

    public interface ILog
    {
        object Save(IList<LogInfo> list);
    }

    public class Log2DB : ILog
    {
        public object Save(IList<LogInfo> list)
        {
            bool flg = false;
            try
            {
                string sql = "";

                if (list.Count > 0)
                {
                    foreach (LogInfo log in list)
                    {
                        switch (log.LogType)
                        {
                            case LogType.Operation:
                                sql += this.GetOperationSQL(log) + ";";
                                break;

                            case LogType.Exception:
                                sql += this.GetExceptionSQL(log) + ";";
                                break;
                        }
                    }
                }

                if (sql.Trim().Length > 0)
                {
                    sql = " BEGIN " + sql + " END COMMIT; ";

                    (new ColligateQueryDao()).UpdateColligateQuery(sql);
                }

                flg = true;

            }
            catch (Exception e)
            {

            }

            return flg;
        }

        public string GetOperationSQL(LogInfo log)
        {
            string ny = System.DateTime.Now.ToString("yyyyMM");

            string sql = @"INSERT INTO LOG_OPERATION" + ny + @"( log_id ,
                                                        GNMC,
                                                        GNDZ, 
                                                        CZR,
                                                        CZSJ, 
                                                        LYLX, 
                                                        WLBS,
                                                        BZ,DEPT_ID ) 
                                    VALUES(
                                        '" + log.ID + @"',
                                        '" + log.GNMC + @"',
                                        '" + log.GNDZ + @"',

                                        ( select t.businessdivisionname || '(" + log.CZR + @")' 
                                              from DEPT_INFO t, user_info u
                                             where u.user_lname = '" + log.CZR + @"'
                                               and u.enter_id = t.businessdivisionid ),

                                        to_date('" + log.CZSJ + @"','yyyy-mm-dd hh24:mi:ss'),
                                        '" + log.LYLX + @"',
                                        '" + log.WLBS + @"',
                                        '" + log.BZ + @"','" + log.DeptId + @"')";

            return sql;
        }

        public string GetExceptionSQL(LogInfo log)
        {
            string sql = @"Insert into log_exception( log_id ,
                                                        gnmc,
                                                        gndz, 
                                                        ycms,
                                                        ycsj, 
                                                        wlbs,
                                                        bz ) 
                                    values(
                                        '" + log.ID + @"',
                                        '" + log.GNMC + @"',
                                        '" + log.GNDZ + @"',
                                        '" + log.YCMS + @"',
                                        to_date('" + log.YCSJ + @"','yyyy-mm-dd hh24:mi:ss'),
                                        '" + log.WLBS + @"',
                                        '" + log.BZ + @"')";

            return sql;
        }
    }

    public class Log2File : ILog
    {
        public object Save(IList<LogInfo> list)
        {
            StringBuilder _log = new StringBuilder();

            if (list.Count > 0)
            {
                foreach (LogInfo log in list)
                {
                    _log.Append("\r\n");
                    _log.Append("------------------------------------------------------------------------" + "\r\n");

                    if (log.LogType == LogType.Exception)
                    {
                        _log.Append("异常时间：" + log.YCSJ.ToString("yyyy-MM-dd HH:mm:ss") + "：" + "\r\n");
                        _log.Append("异常描述：" + log.YCMS.ToString() + "\r\n");
                    }

                    if (log.LogType == LogType.Operation)
                    {
                        _log.Append("操作时间：" + log.CZSJ.ToString("yyyy-MM-dd HH:mm:ss") + "：" + "\r\n");
                        _log.Append("来源类型：" + log.LYLX.ToString() + "\r\n");
                        if (log.CZR != null)
                        {
                            _log.Append("操 作 人：" + log.CZR.ToString() + "\r\n");
                        }
                    }

                    _log.Append("操作功能：" + log.GNMC.ToString() + "\r\n");
                    _log.Append("功能地址：" + log.GNDZ.ToString() + "\r\n");

                    _log.Append("网络标识：" + log.WLBS.ToString() + "\r\n");

                    if (log.BZ != null)
                    {
                        _log.Append("备注：" + log.BZ.ToString() + "\r\n");
                    }
                }
            }

            string path = AppDomain.CurrentDomain.BaseDirectory + "logfiles\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string file = path + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            if (!File.Exists(file))
            {
                using (File.Create(file)) { }
            }

            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine(_log.ToString());
                sw.Close();
            }

            return null;
        }
    }

    public class LogManager
    {
        ILog _logService;

        public LogManager(ILog logService)
        {
            _logService = logService;
        }

        public void Save(IList<LogInfo> list)
        {
            _logService.Save(list);
        }
    }
}
