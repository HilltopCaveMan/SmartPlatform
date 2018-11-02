using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GModel.Car
{
     [Serializable]
    public class CarReport_LCBBView
    {
        #region Private Members

        private bool _isChanged;
        private bool _isDeleted;
        private int _startdata = 0;
        private int _enddata = 0;

        private string _id;
        private string _ter_no;
        private string _depcode;
        private string _statdate; 
        private string _runtime;
        private string _stoptime;
        private string _stopcount;
        private string _daymile;
        private string _daysummile;

        #endregion

        #region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public CarReport_LCBBView()
		{
            _id = "";
            _ter_no = "";
            _depcode = "";
            _statdate = "";
            _runtime = "";
            _stoptime = "";
            _stopcount = "";
            _daymile = "";
            _daysummile = "";
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// 主键
        /// </summary>
        public string ID
        {
            get { return _id; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ID", value, value.ToString());

                _isChanged |= (_id != value); _id = value;
            }
        }

        /// <summary>
        /// 终端编号
        /// </summary>		
        public string TerNo
        {
            get { return _ter_no; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for TerNo", value, value.ToString());

                _isChanged |= (_ter_no != value); _ter_no = value;
            }
        }

        /// <summary>
        /// 部门编码
        /// </summary>		
        public string DepCode
        {
            get { return _depcode; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for DepCode", value, value.ToString());

                _isChanged |= (_depcode != value); _depcode = value;
            }
        }

        /// <summary>
        /// 统计时间
        /// </summary>		
        public string StatDate
        {
            get { return _statdate; }
            set
            {
                _isChanged |= (_statdate != value); _statdate = value;
            }
        }

        /// <summary>
        /// 行驶时间
        /// </summary>
        public string RunTime
        {
            get { return _runtime; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for RunTime", value, value.ToString());

                _isChanged |= (_runtime != value); _runtime = value;
            }
        }

        /// <summary>
        /// 停止时间
        /// </summary>
        public string StopTime
        {
            get { return _stoptime; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for StopTime", value, value.ToString());

                _isChanged |= (_stoptime != value); _stoptime = value;
            }
        }

        /// <summary>
        /// 停车次数
        /// </summary>
        public string StopCount
        {
            get { return _stopcount; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for StopCount", value, value.ToString());

                _isChanged |= (_stopcount != value); _stopcount = value;
            }
        }

        /// <summary>
        /// 当天里程
        /// </summary>
        public string DayMile
        {
            get { return _daymile; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DayMile", value, value.ToString());

                _isChanged |= (_daymile != value); _daymile = value;
            }
        }


        /// <summary>
        /// 总里程
        /// </summary>
        public string DaySumMile
        {
            get { return _daysummile; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DaySumMile", value, value.ToString());

                _isChanged |= (_daysummile != value); _daysummile = value;
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
