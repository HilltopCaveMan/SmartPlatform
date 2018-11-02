﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GModel.Car
{
    [Serializable]
    public class CarReport_TerData
    {
                #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private int _startdata = 0;
        private int _enddata = 0;
        private int _newRow = 0;

        private string _id;
        private string _rawdataid;
        private string _ter_no;
        private string _car_no;
        private string _pro_name;
        private string _state_name;
        private string _generation_num;
        private string _factorynum;
        private DateTime _rtime;
        private string _protocolversion;
        private string _programverson;
        private string _gpsverson;
        private DateTime _positioningtime;
        private string _ter_status;
        private string _ter_model;
        private double _worktime;
        private string _sleeptime;
        private string _ntervalltime;
        private string _ter_vbatt;
        private double _totalworktime;
        private string _blinddatanum;
        private string _ter_statrtimes;
        private string _ifblinddata;
        private string _ifposition;
        private string _northorsouth;
        private string _eastorwest;
        private double _latitude;
        private double _longitude;
        private double _baidu_latitude;
        private double _baidu_longitude;
        private double _google_latitude;
        private double _google_longitude;
        private string _position;
        private string _speed;
        private string _direction;
        private string _height;
        private string _gpsant;
        private double _usesatellite;
        private double _visualsatellite;
        private string _gpsrssi;
        private string _gsmrssi;
        private string _lca;
        private string _cell;
        private string _gsmrssi1;
        private string _lca1;
        private string _cell1;
        private string _gsmrssi2;
        private string _lca2;
        private string _cell2;
        private string _gsmrssi3;
        private string _lca3;
        private string _cell3;
        private string _province;
        private string _city;
        private string _county;
        private string _workstate;
        private double _postbacktimes;
        private string _gsmrssi4;
        private string _lca4;
        private string _cell4;
        private string _gsmrssi5;
        private string _lca5;
        private string _cell5;
        private string _gsmrssi6;
        private string _lca6;
        private string _cell6;
        private string  _lat_;
        private string  _lng_;
        private double _carworkvmp;
        private double _carworktemp ;
        private string _remainl_pct;
        private double _addup_dist;
        private string _ter_simcard;
        private DateTime _ter_innettime;
        private DateTime _ter_activetime;
        private string _businessdivisionname;
        private string _businessdivisionid;
        private string _replydatacode;
        private string _replydataname;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public CarReport_TerData()
        {
            _id = "";
            _businessdivisionname = "";
            _businessdivisionid = "";
            _rawdataid = "";
            _ter_no = "";
            _car_no = "";
            _pro_name = "";
            _state_name = "";
            _generation_num = "";
            _factorynum = "";
            _rtime = new DateTime();
            _protocolversion = "";
            _programverson = "";
            _gpsverson = "";
            _positioningtime = new DateTime();
            _ter_status = "";
            _ter_model = "";
            _worktime = new double();
            _sleeptime = "";
            _ntervalltime = "";
            _ter_vbatt = "";
            _totalworktime = new double();
            _blinddatanum = "";
            _ter_statrtimes = "";
            _ifblinddata = "";
            _ifposition = "";
            _northorsouth = "";
            _eastorwest = "";
            _latitude = new double();
            _longitude = new double();
            _baidu_latitude = new double();
            _baidu_longitude = new double();
            _google_latitude = new double();
            _google_longitude = new double();
            _position = "";
            _speed = "";
            _direction = "";
            _height = "";
            _gpsant = "";
            _usesatellite = new double();
            _visualsatellite = new double();
            _gpsrssi = "";
            _gsmrssi = "";
            _lca = "";
            _cell = "";
            _gsmrssi1 = "";
            _lca1 = "";
            _cell1 = "";
            _gsmrssi2 = "";
            _lca2 = "";
            _cell2 = "";
            _gsmrssi3 = "";
            _lca3 = "";
            _cell3 = "";
            _province = "";
            _city = "";
            _county = "";
            _workstate = "";
            _postbacktimes = new double();
            _gsmrssi4 = "";
            _lca4 = "";
            _cell4 = "";
            _gsmrssi5 = "";
            _lca5 = "";
            _cell5 = "";
            _gsmrssi6 = "";
            _lca6 = "";
            _cell6 = "";
            _lat_="";
            _lng_="";
            _carworkvmp=new double();
            _carworktemp =new double();
            _remainl_pct="";
            _addup_dist=new double();
            _ter_simcard = "";
            _ter_innettime = new DateTime();
            _ter_activetime = new DateTime();
            _replydatacode="";
            _replydataname="";
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties


        public DateTime Ter_Innettime
        {
            get { return _ter_innettime; }
            set { _isChanged |= (_ter_innettime != value); _ter_innettime = value; }
        }

        public DateTime Ter_Activetime
        {
            get { return _ter_activetime; }
            set { _isChanged |= (_ter_activetime != value); _ter_activetime = value; }
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
                    throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());

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
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());

                _isChanged |= (_businessdivisionid != value); _businessdivisionid = value;
            }
        }
        

        /// <summary>
        /// 
        /// </summary>		
        public string Id
        {
            get { return _id; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());

                _isChanged |= (_id != value); _id = value;
            }
        }
        /// <summary>
		/// SIM卡号
		/// </summary>		
		public string TerSimcard
		{
			get { return _ter_simcard; }
			set	
			{
				if( value!= null && value.Length > 13)
					throw new ArgumentOutOfRangeException("Invalid value for TerSimcard", value, value.ToString());
				
				_isChanged |= (_ter_simcard != value); _ter_simcard = value;
			}
		}

        /// <summary>
        /// 
        /// </summary>		
        public string Rawdataid
        {
            get { return _rawdataid; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Rawdataid", value, value.ToString());

                _isChanged |= (_rawdataid != value); _rawdataid = value;
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

        public string CarNo
        {
            get { return _car_no; }
            set
            {
                _isChanged |= (_car_no != value); _car_no = value;
            }
        }

        public string ProName
        {
            get { return _pro_name; }
            set
            {
                _isChanged |= (_pro_name != value); _pro_name = value;
            }
        }

        public string StateName
        {
            get { return _state_name; }
            set
            {
                _isChanged |= (_state_name != value); _state_name = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string GenerationNum
        {
            get { return _generation_num; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for GenerationNum", value, value.ToString());

                _isChanged |= (_generation_num != value); _generation_num = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Factorynum
        {
            get { return _factorynum; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Factorynum", value, value.ToString());

                _isChanged |= (_factorynum != value); _factorynum = value;
            }
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
        /// 本协议版本号
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
        public string Programverson
        {
            get { return _programverson; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Programverson", value, value.ToString());

                _isChanged |= (_programverson != value); _programverson = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Gpsverson
        {
            get { return _gpsverson; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Gpsverson", value, value.ToString());

                _isChanged |= (_gpsverson != value); _gpsverson = value;
            }
        }

        /// <summary>
        /// 终端设备时间
        /// </summary>		
        public DateTime Positioningtime
        {
            get { return _positioningtime; }
            set { _isChanged |= (_positioningtime != value); _positioningtime = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TerStatus
        {
            get { return _ter_status; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TerStatus", value, value.ToString());

                _isChanged |= (_ter_status != value); _ter_status = value;
            }
        }

        /// <summary>
        /// 设备工作模式
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
        public double Worktime
        {
            get { return _worktime; }
            set { _isChanged |= (_worktime != value); _worktime = value; }
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
        public double Totalworktime
        {
            get { return _totalworktime; }
            set { _isChanged |= (_totalworktime != value); _totalworktime = value; }
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
        /// 设备启动次数
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
        public string Ifblinddata
        {
            get { return _ifblinddata; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Ifblinddata", value, value.ToString());

                _isChanged |= (_ifblinddata != value); _ifblinddata = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Ifposition
        {
            get { return _ifposition; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Ifposition", value, value.ToString());

                _isChanged |= (_ifposition != value); _ifposition = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Northorsouth
        {
            get { return _northorsouth; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Northorsouth", value, value.ToString());

                _isChanged |= (_northorsouth != value); _northorsouth = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Eastorwest
        {
            get { return _eastorwest; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Eastorwest", value, value.ToString());

                _isChanged |= (_eastorwest != value); _eastorwest = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double Latitude
        {
            get { return _latitude; }
            set { _isChanged |= (_latitude != value); _latitude = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double Longitude
        {
            get { return _longitude; }
            set { _isChanged |= (_longitude != value); _longitude = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double BaiduLatitude
        {
            get { return _baidu_latitude; }
            set { _isChanged |= (_baidu_latitude != value); _baidu_latitude = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double BaiduLongitude
        {
            get { return _baidu_longitude; }
            set { _isChanged |= (_baidu_longitude != value); _baidu_longitude = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double GoogleLatitude
        {
            get { return _google_latitude; }
            set { _isChanged |= (_google_latitude != value); _google_latitude = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double GoogleLongitude
        {
            get { return _google_longitude; }
            set { _isChanged |= (_google_longitude != value); _google_longitude = value; }
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

        /// <summary>
        /// 
        /// </summary>		
        public string Speed
        {
            get { return _speed; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Speed", value, value.ToString());

                _isChanged |= (_speed != value); _speed = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Direction
        {
            get { return _direction; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Direction", value, value.ToString());

                _isChanged |= (_direction != value); _direction = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Height
        {
            get { return _height; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Height", value, value.ToString());

                _isChanged |= (_height != value); _height = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Gpsant
        {
            get { return _gpsant; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Gpsant", value, value.ToString());

                _isChanged |= (_gpsant != value); _gpsant = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double Usesatellite
        {
            get { return _usesatellite; }
            set { _isChanged |= (_usesatellite != value); _usesatellite = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double Visualsatellite
        {
            get { return _visualsatellite; }
            set { _isChanged |= (_visualsatellite != value); _visualsatellite = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Gpsrssi
        {
            get { return _gpsrssi; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Gpsrssi", value, value.ToString());

                _isChanged |= (_gpsrssi != value); _gpsrssi = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Gsmrssi
        {
            get { return _gsmrssi; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Gsmrssi", value, value.ToString());

                _isChanged |= (_gsmrssi != value); _gsmrssi = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Lca
        {
            get { return _lca; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Lca", value, value.ToString());

                _isChanged |= (_lca != value); _lca = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Cell
        {
            get { return _cell; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Cell", value, value.ToString());

                _isChanged |= (_cell != value); _cell = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Gsmrssi1
        {
            get { return _gsmrssi1; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Gsmrssi1", value, value.ToString());

                _isChanged |= (_gsmrssi1 != value); _gsmrssi1 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Lca1
        {
            get { return _lca1; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Lca1", value, value.ToString());

                _isChanged |= (_lca1 != value); _lca1 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Cell1
        {
            get { return _cell1; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Cell1", value, value.ToString());

                _isChanged |= (_cell1 != value); _cell1 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Gsmrssi2
        {
            get { return _gsmrssi2; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Gsmrssi2", value, value.ToString());

                _isChanged |= (_gsmrssi2 != value); _gsmrssi2 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Lca2
        {
            get { return _lca2; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Lca2", value, value.ToString());

                _isChanged |= (_lca2 != value); _lca2 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Cell2
        {
            get { return _cell2; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Cell2", value, value.ToString());

                _isChanged |= (_cell2 != value); _cell2 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Gsmrssi3
        {
            get { return _gsmrssi3; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Gsmrssi3", value, value.ToString());

                _isChanged |= (_gsmrssi3 != value); _gsmrssi3 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Lca3
        {
            get { return _lca3; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Lca3", value, value.ToString());

                _isChanged |= (_lca3 != value); _lca3 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Cell3
        {
            get { return _cell3; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Cell3", value, value.ToString());

                _isChanged |= (_cell3 != value); _cell3 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Province
        {
            get { return _province; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Province", value, value.ToString());

                _isChanged |= (_province != value); _province = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string City
        {
            get { return _city; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for City", value, value.ToString());

                _isChanged |= (_city != value); _city = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string County
        {
            get { return _county; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for County", value, value.ToString());

                _isChanged |= (_county != value); _county = value;
            }
        }

        /// <summary>
        /// 工作状态
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
        public double Postbacktimes
        {
            get { return _postbacktimes; }
            set { _isChanged |= (_postbacktimes != value); _postbacktimes = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Gsmrssi4
        {
            get { return _gsmrssi4; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Gsmrssi4", value, value.ToString());

                _isChanged |= (_gsmrssi4 != value); _gsmrssi4 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Lca4
        {
            get { return _lca4; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Lca4", value, value.ToString());

                _isChanged |= (_lca4 != value); _lca4 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Cell4
        {
            get { return _cell4; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Cell4", value, value.ToString());

                _isChanged |= (_cell4 != value); _cell4 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Gsmrssi5
        {
            get { return _gsmrssi5; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Gsmrssi5", value, value.ToString());

                _isChanged |= (_gsmrssi5 != value); _gsmrssi5 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Lca5
        {
            get { return _lca5; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Lca5", value, value.ToString());

                _isChanged |= (_lca5 != value); _lca5 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Cell5
        {
            get { return _cell5; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Cell5", value, value.ToString());

                _isChanged |= (_cell5 != value); _cell5 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Gsmrssi6
        {
            get { return _gsmrssi6; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Gsmrssi6", value, value.ToString());

                _isChanged |= (_gsmrssi6 != value); _gsmrssi6 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Lca6
        {
            get { return _lca6; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Lca6", value, value.ToString());

                _isChanged |= (_lca6 != value); _lca6 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Cell6
        {
            get { return _cell6; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Cell6", value, value.ToString());

                _isChanged |= (_cell6 != value); _cell6 = value;
            }
        }

        public string ReplydataCode
        {
            get { return _replydatacode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ReplydataCode", value, value.ToString());

                _isChanged |= (_replydatacode != value); _replydatacode = value;
            }
        }

        public string ReplydataName
        {
            get { return _replydataname; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ReplydataName", value, value.ToString());

                _isChanged |= (_replydataname != value); _replydataname = value;
            }
        }

        public DateTime ActTime
        {
            get;
            set;
        }
        public string ActCount
        {
            get;
            set;
        }

        public string Tertypeid
        {
            get;
            set;
        }

        public string Accstate
        {
            get;
            set;
        }
        public string Lat
        {
            get;
            set;
        }
        public string Lng
        {
            get;
            set;
        }
        public double Carworkvmp
        {
            get;
            set;
        }
        public double Carworktemp
        {
            get;
            set;
        }
        public string RemainlPct
        {
            get;
            set;
        }
        public double AddupDist
        {
            get;
            set;
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
