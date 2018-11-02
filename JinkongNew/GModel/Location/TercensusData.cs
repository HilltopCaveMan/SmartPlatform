using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GModel.Location
{
    [Serializable]
    public sealed class TercensusData
    {
        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private int _startdata = 0;
        private int _enddata = 0;
        private int _newRow = 0;

        private string _id;
        private string _terno;
        private string _deptid;
        private string _deptcode;
        private string _deptname;
        private string _tertypeid;
        private string _tertypename;
        private string _programverson;
        private string _ter_status;
        private DateTime _censustime;
        private DateTime _newrtime;
        private string _notrtimeday;
        private string _ter_vbatt;
        private string _ter_statrtimes;
        private string _postbacktimes;
        private string _totalworktime;
        private string _ntervalltime;
        private string _analysisresult;
        private string _curalarmstate;
        private string _standardscale;
        private string _precisescale;
        private string _chasecarscale;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public TercensusData()
        {
            _id = "";
            _terno = "";
            _deptid="";
            _deptcode="";
            _deptname = "";
            _tertypeid="";
            _tertypename = "";
            _programverson = "";
            _ter_status="";
            _censustime = new DateTime();
            _newrtime = new DateTime();
            _notrtimeday = "";
            _ter_vbatt="";
            _ter_statrtimes = "";
            _postbacktimes = "" ;
            _totalworktime="";
            _ntervalltime="";
            _analysisresult = "";
            _curalarmstate="";
            _standardscale="";
            _precisescale="";
            _chasecarscale="";
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties
        /// <summary>
        /// 主键
        /// </summary>		
        public string Id
        {
            get { return _id; }
            set
            {
                _isChanged |= (_id != value); _id = value;
            }
        }


        /// <summary>
        /// 终端编号
        /// </summary>		
        public string TerNo
        {
            get { return _terno; }
            set
            {
                _isChanged |= (_terno != value); _terno = value;
            }
        }

        /// <summary>
        /// 企业ID
        /// </summary>
        public string DeptId
        {
            get { return _deptid; }
            set {
                _isChanged |= (_deptid != value); _deptid = value;
            }
        }

        /// <summary>
        /// 企业code
        /// </summary>
        public string DeptCode
        {
            get { return _deptcode; }
            set
            {
                _isChanged |= (_deptcode != value); _deptcode = value;
            }
        }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string DeptName
        {
            get { return _deptname; }
            set
            {
                _isChanged |= (_deptname != value); _deptname = value;
            }
        }

        /// <summary>
        /// 设备类型
        /// </summary>		
        public string TerTypeid
        {
            get { return _tertypeid; }
            set
            {
                _isChanged |= (_tertypeid != value); _tertypeid = value;
            }
        }

        /// <summary>
        /// 设备类型
        /// </summary>		
        public string TerTypename
        {
            get { return _tertypename; }
            set
            {
                _isChanged |= (_tertypename != value); _tertypename = value;
            }
        }

        /// <summary>
        /// 设备软件版本
        /// </summary>		
        public string Programverson
        {
            get { return _programverson; }
            set
            {
                _isChanged |= (_programverson != value); _programverson = value;
            }
        }

        /// <summary>
        /// 设备状态
        /// </summary>		
        public string TerStatus
        {
            get { return _ter_status; }
            set
            {
                _isChanged |= (_ter_status != value); _ter_status = value;
            }
        }

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime CensusTime
        {
            get { return _censustime; }
            set
            {
                _isChanged |= (_censustime != value); _censustime = value;
            }
        }

        /// <summary>
        /// 最新回传时间
        /// </summary>
        public DateTime NewRtime
        {
            get { return _newrtime; }
            set
            {
                _isChanged |= (_newrtime != value); _newrtime = value;
            }
        }

        /// <summary>
        /// 未回传天数
        /// </summary>
        public string NotrtimeDay
        {
            get { return _notrtimeday; }
            set
            {
                _isChanged |= (_notrtimeday != value); _notrtimeday = value;
            }
        }

        /// <summary>
        /// 设备电池电压
        /// </summary>		
        public string TerVbatt
        {
            get { return _ter_vbatt; }
            set
            {
                _isChanged |= (_ter_vbatt != value); _ter_vbatt = value;
            }
        }

        /// <summary>
        /// 设备启动次数
        /// </summary>		
        public string TerStatrtimes
        {
            get { return _ter_statrtimes; }
            set
            {
                _isChanged |= (_ter_statrtimes != value); _ter_statrtimes = value;
            }
        }

        /// <summary>
        /// 回传次数
        /// </summary>		
        public string PostbackTimes
        {
            get { return _postbacktimes; }
            set
            {
                _isChanged |= (_postbacktimes != value); _postbacktimes = value;
            }
        }

        /// <summary>
        /// 累计工时
        /// </summary>		
        public string TotalworkTime
        {
            get { return _totalworktime; }
            set
            {
                _isChanged |= (_totalworktime != value); _totalworktime = value;
            }
        }

        /// <summary>
        /// 设备间隔时间
        /// </summary>		
        public string Ntervalltime
        {
            get { return _ntervalltime; }
            set
            {
                _isChanged |= (_ntervalltime != value); _ntervalltime = value;
            }
        }

        /// <summary>
        /// 分析结果
        /// </summary>
        public string AnalysisResult
        {
            get { return _analysisresult; }
            set
            {
                _isChanged |= (_analysisresult != value); _analysisresult = value;
            }
        }

        /// <summary>
        /// 当前报警状态
        /// </summary>
        public string CuralarmState
        {
            get { return _curalarmstate; }
            set
            {
                _isChanged |= (_curalarmstate != value); _curalarmstate = value;
            }
        }

        /// <summary>
        /// 标准比例
        /// </summary>
        public string StandardScale
        {
            get { return _standardscale; }
            set
            {
                _isChanged |= (_standardscale != value); _standardscale = value;
            }
        }

        /// <summary>
        /// 精准比例
        /// </summary>
        public string PreciseScale
        {
            get { return _precisescale; }
            set
            {
                _isChanged |= (_precisescale != value); _precisescale = value;
            }
        }

        /// <summary>
        /// 追车比例
        /// </summary>
        public string ChasecarScale
        {
            get { return _chasecarscale; }
            set
            {
                _isChanged |= (_chasecarscale != value); _chasecarscale = value;
            }
        }

        public int StartData
        {
            get { return _startdata; }
            set
            {
                _startdata = value;
            }
        }

        public int EndData
        {
            get { return _enddata; }
            set
            {
                _enddata = value;
            }
        }
        public int NewRow
        {
            get { return _newRow; }
            set { _newRow = value; }
        }
        /// <summary>
        /// Returns whether or not the object has changed it's values.
        /// </summary>
        public bool IsChanged
        {
            get { return _isChanged; }
        }

        /// <summary>
        /// Returns whether or not the object has changed it's values.
        /// </summary>
        public bool IsDeleted
        {
            get { return _isDeleted; }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// mark the item as deleted
        /// </summary>
        public void MarkAsDeleted()
        {
            _isDeleted = true;
            _isChanged = true;
        }

        #endregion
    }
}
