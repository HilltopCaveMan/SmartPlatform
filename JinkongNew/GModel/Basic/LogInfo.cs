using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GModel.Basic
{
    /// <summary>
    /// 操作/异常日志
    /// </summary>
    public class LogInfo
    {
        private LogType log_type;

        private string log_id="";
        private string gnmc = "";
        private string gndz = "";

        private string ycms = "";
        private DateTime ycsj = DateTime.Now;

        private string czr = "";
        private DateTime czsj = DateTime.Now;

        private string lylx = "";
        private string wlbs = "";

        private string bz = "";

        private string dept_id = "";
        public string DeptId
        {
            get { return dept_id; }
            set
            {
                dept_id = value;
            }
        }

        /// <summary>
        /// 日志类型 Operation / Exception 
        /// </summary>
        public LogType LogType
        {
            get { return log_type; }
            set
            {
                log_type = value;
            }
        }

        /// <summary>
        /// 日志ID
        /// </summary>
        public string ID
        {
            get { return log_id; }
            set
            {
                log_id = value;
            }
        }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string GNMC
        {
            get
            {
                return gnmc;
            }
            set
            {
                gnmc = value;
            }
        }
        /// <summary>
        /// 功能URL地址
        /// </summary>
        public string GNDZ
        {
            get { return gndz; }
            set { gndz = value; }
        }
        /// <summary>
        /// 异常描述
        /// </summary>
        public string YCMS
        {
            get { return ycms; }
            set { ycms = value; }
        }
        /// <summary>
        /// 导常时间
        /// </summary>
        public DateTime YCSJ
        {
            get { return ycsj; }
            set { ycsj = value; }
        }
        /// <summary>
        /// 来源类型 平台、IOS、android 
        /// </summary>
        public string LYLX
        {
            get { return lylx; }
            set { lylx = value; }
        }
        /// <summary>
        /// 网络标识 IP、手机号、
        /// </summary>
        public string WLBS
        {
            get { return wlbs; }
            set { wlbs = value; }
        }


        /// <summary>
        /// 操作人
        /// </summary>
        public string CZR
        {
            get { return czr; }
            set { czr = value; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CZSJ
        {
            get { return czsj; }
            set { czsj = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ
        {
            get { return bz; }
            set { bz = value; }
        }
    }

    public class LogMessage
    {
        public string message { get; set; }

        public void Save(string message)
        {
            LogMessage logmsg = new LogMessage();
            logmsg.message = message;
            System.Web.HttpContext.Current.Session.Add("logmsg", logmsg);
        }
    }

    public enum LogType
    {
        Operation,
        Exception
    }
}
