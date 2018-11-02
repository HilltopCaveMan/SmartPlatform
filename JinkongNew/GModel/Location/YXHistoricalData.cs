using System;

namespace GModel.Location
{
    [Serializable]
    public class YXHistoricalData
    {
        private bool _isChanged;
        private bool _isDeleted;
        private int _startdata = 0;
        private int _enddata = 0;
        private int _newRow = 0;

        private string _id;
        private string _ter_no;
        private DateTime _rtime;
        private string _settype;
        private string _replydatacode;
        private string _replydataname;
        private string _ifposition;
        private string _accstate;
        private string _northorsouth;
        private string _eastorwest;
        private string _latitude;
        private string _longitude;
        private string _baidu_latitude;
        private string _baidu_longitude;
        private string _google_latitude;
        private string _google_longitude;
        private string _position;
        private string _speed;
        private string _direction;
        private string _ter_vbatt;
        private string _gsmrssi;
        private string _temperature;
        private string _remainlpct;
        private string _protocolversion;
        private string _programverson;
        private string _gpsverson;
        private string _TerSimcard;
        private DateTime _Ter_Innettime;

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public YXHistoricalData()
        {
            _id = "";
            _ter_no = "";
            _rtime = new DateTime();
            _settype = "";
            _replydatacode = "";
            _replydataname = "";
            _ifposition = "";
            _accstate = "";
            _northorsouth = "";
            _eastorwest = "";
            _latitude = "";
            _longitude = "";
            _baidu_latitude = "";
            _baidu_longitude = "";
            _google_latitude = "";
            _google_longitude = "";
            _position = "";
            _speed = "";
            _direction = "";
            _ter_vbatt = "";
            _gsmrssi = "";
            _temperature = "";
            _remainlpct = "";
            _protocolversion = "";
            _programverson = "";
            _gpsverson = "";
            //_TerSimcard = "";
            //_Ter_Innettime = new DateTime();
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// 主键
        /// </summary>
        public string Id
        {
            get { return _id; }
            set { _isChanged |= (_id != value); _id = value; }
        }

        /// <summary>
        /// 终端号
        /// </summary>
        public string TerNo
        {
            get { return _ter_no; }
            set { _isChanged |= (_ter_no != value); _ter_no = value; }
        }
        
        /// <summary>
        /// 回传时间
        /// </summary>
        public DateTime Rtime
        {
            get { return _rtime; }
            set { _isChanged |= (_rtime != value); _rtime = value; }
        }

        /// <summary>
        /// 设备类型（101 有线 102 无线）
        /// </summary>
        public string SetType
        {
            get { return _settype; }
            set { _isChanged |= (_settype != value); _settype = value; }
        }

        /// <summary>
        /// 报警代码
        /// </summary>
        public string ReplydataCode
        {
            get { return _replydatacode; }
            set { _isChanged |= (_replydatacode != value); _replydatacode = value; }
        }

        /// <summary>
        /// 报警描述
        /// </summary>
        public string ReplydataName
        {
            get { return _replydataname; }
            set { _isChanged |= (_replydataname != value); _replydataname = value; }
        }

        /// <summary>
        /// 是否定位
        /// </summary>
        public string Ifposition
        {
            get { return _ifposition; }
            set { _isChanged |= (_ifposition != value); _ifposition = value; }
        }

        /// <summary>
        /// 点火状态
        /// </summary>
        public string AccState
        {
            get { return _accstate; }
            set { _isChanged |= (_accstate != value); _accstate = value; }
        }

        /// <summary>
        /// 南北半球
        /// </summary>
        public string Northorsouth
        {
            get { return _northorsouth; }
            set { _isChanged |= (_northorsouth != value); _northorsouth = value; }
        }

        /// <summary>
        /// 东西半球
        /// </summary>
        public string Eastorwest
        {
            get { return _eastorwest; }
            set { _isChanged |= (_eastorwest != value); _eastorwest = value; }
        }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude
        {
            get { return _latitude; }
            set { _isChanged |= (_latitude != value); _latitude = value; }
        }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude
        {
            get { return _longitude; }
            set { _isChanged |= (_longitude != value); _longitude = value; }
        }

        /// <summary>
        /// 百度纬度
        /// </summary>
        public string BaiduLatitude
        {
            get { return _baidu_latitude; }
            set { _isChanged |= (_baidu_latitude != value); _baidu_latitude = value; }
        }

        /// <summary>
        /// 百度经度
        /// </summary>
        public string BaiduLongitude
        {
            get { return _baidu_longitude; }
            set { _isChanged |= (_baidu_longitude != value); _baidu_longitude = value; }
        }

        /// <summary>
        /// 谷歌纬度
        /// </summary>
        public string GoogleLatitude
        {
            get { return _google_latitude; }
            set { _isChanged |= (_google_latitude != value); _google_latitude = value; }
        }

        /// <summary>
        /// 谷歌经度
        /// </summary>
        public string GoogleLongitude
        {
            get { return _google_longitude; }
            set { _isChanged |= (_google_longitude != value); _google_longitude = value; }
        }

        /// <summary>
        /// 地理位置
        /// </summary>
        public string Position
        {
            get { return _position; }
            set { _isChanged |= (_position != value); _position = value; }
        }

        /// <summary>
        /// GPS速度
        /// </summary>
        public string Speed
        {
            get { return _speed; }
            set { _isChanged |= (_speed != value); _speed = value; }
        }

        /// <summary>
        /// GPS方向
        /// </summary>
        public string Direction
        {
            get { return _direction; }
            set { _isChanged |= (_direction != value); _direction = value; }
        }

        /// <summary>
        /// 设备电池电压
        /// </summary>
        public string TerVbatt
        {
            get { return _ter_vbatt; }
            set { _isChanged |= (_ter_vbatt != value); _ter_vbatt = value; }
        }

        /// <summary>
        /// GSM信号强度
        /// </summary>
        public string Gsmrssi
        {
            get { return _gsmrssi; }
            set { _isChanged |= (_gsmrssi != value); _gsmrssi = value; }
        }

        /// <summary>
        /// 温度
        /// </summary>
        public string Temperature
        {
            get { return _temperature; }
            set { _isChanged |= (_temperature != value); _temperature = value; }
        }

        /// <summary>
        /// 里程
        /// </summary>
        public string Remainlpct
        {
            get { return _remainlpct; }
            set { _isChanged |= (_remainlpct != value); _remainlpct = value; }
        }

        /// <summary>
        /// 本协议版本号
        /// </summary>
        public string Protocolversion
        {
            get { return _protocolversion; }
            set { _isChanged |= (_protocolversion != value); _protocolversion = value; }
        }

        /// <summary>
        /// 设备软件版本
        /// </summary>
        public string Programverson
        {
            get { return _programverson; }
            set { _isChanged |= (_programverson != value); _programverson = value; }
        }

        /// <summary>
        /// 设备硬件版本
        /// </summary>
        public string Gpsverson
        {
            get { return _gpsverson; }
            set { _isChanged |= (_gpsverson != value); _gpsverson = value; }
        }

        public string TerSimcard
        {
            get { return _TerSimcard; }
            set { _isChanged |= (_TerSimcard != value); _TerSimcard = value; }
        }

        public DateTime Ter_Innettime
        {
            get { return _Ter_Innettime;}
            set { _isChanged |= (_Ter_Innettime != value); _Ter_Innettime = value; }
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