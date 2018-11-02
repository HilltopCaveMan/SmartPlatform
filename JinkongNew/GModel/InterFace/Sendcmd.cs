using System;

namespace GModel.InterFace
{
    [Serializable]
    public class Sendcmd
    {
        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private int _startdata = 0;
        private int _enddata = 0;
        private int _newRow = 0;

        private string _tr_id;
        private DateTime _tr_opdate;
        private string _tr_settype;
        private string _tr_setsn;
        private string _tr_setsn1;
        private string _tr_cmdtype;
        private string _tr_cmdtype1;
        private string _tr_param1;
        private string _tr_param2;
        private string _tr_param3;
        private string _tr_param4;
        private string _tr_param5;
        private string _tr_param6;
        private string _tr_param7;
        private string _tr_param8;
        private string _tr_userseq;
        private string _tr_state;
        private DateTime _tr_senddate;
        private DateTime _tr_exedate;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public Sendcmd()
        {
            _tr_id = "";
            _tr_opdate = new DateTime();
            _tr_settype = "";
            _tr_setsn = "";
            _tr_setsn1 = "";
            _tr_cmdtype = "";
            _tr_cmdtype1 = "";
            _tr_param1 = "";
            _tr_param2 = "";
            _tr_param3 = "";
            _tr_param4 = "";
            _tr_param5 = "";
            _tr_param6 = "";
            _tr_param7 = "";
            _tr_param8 = "";
            _tr_userseq = "";
            _tr_state = "";
            _tr_senddate = new DateTime();
            _tr_exedate = new DateTime();
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// 主键
        /// </summary>		
        public string TrId
        {
            get { return _tr_id; }
            set { _isChanged |= (_tr_id != value); _tr_id = value; }
        }

        /// <summary>
        /// 操作时间
        /// </summary>		
        public DateTime TrOpdate
        {
            get { return _tr_opdate; }
            set
            {
                _isChanged |= (_tr_opdate != value); _tr_opdate = value;
            }
        }

        /// <summary>
        /// 设备类型 （101 有线 102 无线）
        /// </summary>		
        public string TrSettype
        {
            get { return _tr_settype; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for TrSettype", value, value.ToString());

                _isChanged |= (_tr_settype != value); _tr_settype = value;
            }
        }

        /// <summary>
        /// 终端编号（&连接编号串）
        /// </summary>		
        public string TrSetsn
        {
            get { return _tr_setsn; }
            set
            {
                //if (value != null && value.Length > 30)
                //    throw new ArgumentOutOfRangeException("Invalid value for Tr_setsn", value, value.ToString());

                _isChanged |= (_tr_setsn != value); _tr_setsn = value;
            }
        }

        /// <summary>
        /// 预留字段
        /// </summary>		
        public string TrSetsn1
        {
            get { return _tr_setsn1; }
            set { _isChanged |= (_tr_setsn1 != value); _tr_setsn1 = value; }
        }

        /// <summary>
        /// 命令类型（101 熄火 102 点火 201 定时呼叫 206 定距呼叫 306 限速设置 307 取消限速）
        /// </summary>		
        public string TrCmdtype
        {
            get { return _tr_cmdtype; }
            set { _isChanged |= (_tr_cmdtype != value); _tr_cmdtype = value; }
        }

        /// <summary>
        /// 预留字段
        /// </summary>		
        public string TrCmdtype1
        {
            get { return _tr_cmdtype1; }
            set
            {
                _isChanged |= (_tr_cmdtype1 != value); _tr_cmdtype1 = value;
            }
        }

        /// <summary>
        /// 参数1
        /// </summary>		
        public string TrParam1
        {
            get { return _tr_param1; }
            set
            {
                _isChanged |= (_tr_param1 != value); _tr_param1 = value;
            }
        }

        /// <summary>
        /// 参数2
        /// </summary>		
        public string TrParam2
        {
            get { return _tr_param2; }
            set
            {
                _isChanged |= (_tr_param2 != value); _tr_param2 = value;
            }
        }

        /// <summary>
        /// 参数3
        /// </summary>		
        public string TrParam3
        {
            get { return _tr_param3; }
            set
            {
                _isChanged |= (_tr_param3 != value); _tr_param3 = value;
            }
        }

        /// <summary>
        /// 参数4
        /// </summary>		
        public string TrParam4
        {
            get { return _tr_param4; }
            set
            {
                _isChanged |= (_tr_param4 != value); _tr_param4 = value;
            }
        }

        /// <summary>
        /// 参数5
        /// </summary>		
        public string TrParam5
        {
            get { return _tr_param5; }
            set
            {
                _isChanged |= (_tr_param5 != value); _tr_param5 = value;
            }
        }

        /// <summary>
        /// 参数6
        /// </summary>		
        public string TrParam6
        {
            get { return _tr_param6; }
            set
            {
                _isChanged |= (_tr_param6!= value); _tr_param6 = value;
            }
        }

        /// <summary>
        /// 参数7
        /// </summary>		
        public string TrParam7
        {
            get { return _tr_param7; }
            set
            {
                _isChanged |= (_tr_param7 != value); _tr_param7 = value;
            }
        }

        /// <summary>
        /// 参数8
        /// </summary>		
        public string TrParam8
        {
            get { return _tr_param8; }
            set
            {
                _isChanged |= (_tr_param8 != value); _tr_param8 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TrUserseq
        {
            get { return _tr_userseq; }
            set { _isChanged |= (_tr_userseq != value); _tr_userseq = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TrState
        {
            get { return _tr_state; }
            set { _isChanged |= (_tr_state != value); _tr_state = value; }
        }

        /// <summary>
        /// 发送时间
        /// </summary>		
        public DateTime TrSenddate
        {
            get { return _tr_senddate; }
            set { _isChanged |= (_tr_senddate != value); _tr_senddate = value; }
        }

        /// <summary>
        /// 执行时间
        /// </summary>		
        public DateTime TrExedate
        {
            get { return _tr_exedate; }
            set { _isChanged |= (_tr_exedate != value); _tr_exedate = value; }
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