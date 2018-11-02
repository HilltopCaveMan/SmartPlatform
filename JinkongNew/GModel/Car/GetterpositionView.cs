// Generated by IBatisNetModel
using System;

namespace GModel.Car
{
    [Serializable]
    public sealed class GetterpositionView
    {
        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private int _startdata = 0;
        private int _enddata = 0;
        private int _newRow = 0;

        private string _ter_guid; 
        private string _ter_no;
        private string _ter_simcard;
        private string _pro_version;
        private string _pro_id;
        private string _pro_name;
        private string _businessdivisionname;
        private string _businessdivisionid;
        private string _businessdivisioncode;
        private double _worktime;
        private DateTime _rtime;
        private string _protocolversion;
        private DateTime _positioningtime;
        private string _workstate;
        private string _ter_model;
        private string _ter_statrtimes;
        private string _blinddatanum;
        private double _totalworktime;
        private string _ter_vbatt;
        private string _ntervalltime;
        private string _sleeptime;
        private DateTime _ter_innettime;
        private string _ter_softver;
        private string _ter_hardver;
        private string _position;
        private string _ter_status; 
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public GetterpositionView()
        {
            _ter_guid = ""; 
            _ter_no = "";
            _ter_simcard = "";
            _pro_version = "";
            _pro_id = "";
            _pro_name = "";
            _businessdivisionname = "";
            _businessdivisionid = "";
            _businessdivisioncode = "";
            _worktime = new double();
            _rtime = new DateTime();
            _protocolversion = "";
            _positioningtime = new DateTime();
            _workstate = "";
            _ter_model = "";
            _ter_statrtimes = "";
            _blinddatanum = "";
            _totalworktime = new double();
            _ter_vbatt = "";
            _ntervalltime = "";
            _sleeptime = "";
            _ter_innettime = new DateTime();
            _ter_softver = "";
            _ter_hardver = "";
            _position = "";
            _ter_status = "";
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties

        /// <summary>
        /// �ն�GUID
        /// </summary>	
        public string TerGuid
        {
            get { return _ter_guid; }
            set
            {
                _ter_guid = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TerNo
        {
            get { return _ter_no; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TerNo", value, value.ToString());

                _isChanged |= (_ter_no != value); _ter_no = value;
            }
        }

        /// <summary>
        /// SIM����
        /// </summary>	

        public string TerSimcard
        {
            get { return _ter_simcard; }
            set
            {
                if (value != null && value.Length > 13)
                    throw new ArgumentOutOfRangeException("Invalid value for TerSimcard", value, value.ToString());

                _isChanged |= (_ter_simcard != value); _ter_simcard = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string ProVersion
        {
            get { return _pro_version; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ProVersion", value, value.ToString());

                _isChanged |= (_pro_version != value); _pro_version = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string ProId
        {
            get { return _pro_id; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for ProId", value, value.ToString());

                _isChanged |= (_pro_id != value); _pro_id = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string ProName
        {
            get { return _pro_name; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ProName", value, value.ToString());

                _isChanged |= (_pro_name != value); _pro_name = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Businessdivisionname
        {
            get { return _businessdivisionname; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Businessdivisionname", value, value.ToString());

                _isChanged |= (_businessdivisionname != value); _businessdivisionname = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Businessdivisionid
        {
            get { return _businessdivisionid; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for Businessdivisionid", value, value.ToString());

                _isChanged |= (_businessdivisionid != value); _businessdivisionid = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Businessdivisioncode
        {
            get { return _businessdivisioncode; }
            set
            {
                if (value != null && value.Length > 80)
                    throw new ArgumentOutOfRangeException("Invalid value for Businessdivisioncode", value, value.ToString());

                _isChanged |= (_businessdivisioncode != value); _businessdivisioncode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double Worktime
        {
            get { return _worktime; }
            set { _isChanged |= (_worktime != value); _worktime = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime Rtime
        {
            get { return _rtime; }
            set { _isChanged |= (_rtime != value); _rtime = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Protocolversion
        {
            get { return _protocolversion; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Protocolversion", value, value.ToString());

                _isChanged |= (_protocolversion != value); _protocolversion = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime Positioningtime
        {
            get { return _positioningtime; }
            set { _isChanged |= (_positioningtime != value); _positioningtime = value; }
        }

        /// <summary>
        /// �豸״̬
        /// </summary>		
        public string TerStatus
        {
            get { return _ter_status; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for TerState", value, value.ToString());

                _isChanged |= (_ter_status != value); _ter_status = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Workstate
        {
            get { return _workstate; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Workstate", value, value.ToString());

                _isChanged |= (_workstate != value); _workstate = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TerModel
        {
            get { return _ter_model; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TerModel", value, value.ToString());

                _isChanged |= (_ter_model != value); _ter_model = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TerStatrtimes
        {
            get { return _ter_statrtimes; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TerStatrtimes", value, value.ToString());

                _isChanged |= (_ter_statrtimes != value); _ter_statrtimes = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Blinddatanum
        {
            get { return _blinddatanum; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Blinddatanum", value, value.ToString());

                _isChanged |= (_blinddatanum != value); _blinddatanum = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double Totalworktime
        {
            get { return _totalworktime; }
            set { _isChanged |= (_totalworktime != value); _totalworktime = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TerVbatt
        {
            get { return _ter_vbatt; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TerVbatt", value, value.ToString());

                _isChanged |= (_ter_vbatt != value); _ter_vbatt = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Ntervalltime
        {
            get { return _ntervalltime; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Ntervalltime", value, value.ToString());

                _isChanged |= (_ntervalltime != value); _ntervalltime = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Sleeptime
        {
            get { return _sleeptime; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Sleeptime", value, value.ToString());

                _isChanged |= (_sleeptime != value); _sleeptime = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime TerInnettime
        {
            get { return _ter_innettime; }
            set { _isChanged |= (_ter_innettime != value); _ter_innettime = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TerSoftver
        {
            get { return _ter_softver; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for TerSoftver", value, value.ToString());

                _isChanged |= (_ter_softver != value); _ter_softver = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TerHardver
        {
            get { return _ter_hardver; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for TerHardver", value, value.ToString());

                _isChanged |= (_ter_hardver != value); _ter_hardver = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Position
        {
            get { return _position; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Position", value, value.ToString());

                _isChanged |= (_position != value); _position = value;
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