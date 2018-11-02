using System;

namespace GModel.Car
{
    [Serializable]
    public class CarReport_BJView
    {
        #region Private Members

		private bool _isChanged;
		private bool _isDeleted;
		private int _startdata=0;
        private int _enddata=0;
		private int _newRow = 0;
		
		private string _ter_guid; 
		private string _ter_no; 
		private string _dept_id; 
		private string _businessdivisionname; 
		private string _businessdivisioncode; 
		private string _ter_softver; 
		private string _ter_hardver; 
		private string _ter_status;
        private string _replydataname;
		private string _car_no;

        //里程
        private Int32 _car_lc;

        //回传时间
        private string _hcsj;
        private string _position;
        private string _postbacktimes;
        //累计离线天数
        private string _ljlxts;

        private string _fxjg;

		#endregion
		
		#region Default ( Empty ) Class Constuctor

		/// <summary>
		/// default constructor
		/// </summary>
        public CarReport_BJView()
		{
            _ter_status = "";
            _replydataname = "";
            _ter_no = ""; 
			_car_no = "";
            _car_lc = 0;

            _hcsj = new DateTime().ToString("yyyy-MM-dd");
            _position = "";
            _postbacktimes = "";
            _ljlxts = "0";
            _fxjg = "";

		}

		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public string TerNo
		{
			get { return _ter_no; }
			set	
			{
				if( value!= null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TerNo", value, value.ToString());
				
				_isChanged |= (_ter_no != value); _ter_no = value;
			}
		}

        /// <summary>
        /// 
        /// </summary>		
        public string CarNo
        {
            get { return _car_no; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CarNo", value, value.ToString());

                _isChanged |= (_car_no != value); _car_no = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public int LCTJ
        {
            get { return _car_lc; }
            set
            {
                _isChanged |= (_car_lc != value); _car_lc = value;
            }
        }

        /// <summary>
        /// 累计离线天数
        /// </summary>
        public string LJLXTS
        {
            get { return _ljlxts; }
            set
            {
                _isChanged |= (_ljlxts != value); _ljlxts = value;
            }
        }

        /// <summary>
        /// 回传时间
        /// </summary>
        public string HCSJ
        {
            get { return _hcsj; }
            set
            {
                _isChanged |= (_hcsj != value); _hcsj = value;
            }
        }

        /// <summary>
        /// 回传位置
        /// </summary>
        public string Position
        {
            get { return _position; }
            set {
                _isChanged |= (_position != value); _position = value;
            }
        }

        /// <summary>
        /// 回传次数
        /// </summary>
        public string Postbacktimes
        {
            get { return _postbacktimes; }
            set
            {
                _isChanged |= (_postbacktimes != value); _postbacktimes = value;
            }
        }

        /// <summary>
        /// 分析结果
        /// </summary>
        public string FXJG
        {
            get { return _fxjg; }
            set
            {
                _isChanged |= (_fxjg != value); _fxjg = value;
            }
        }
			
		/// <summary>
		/// 
		/// </summary>		
        public string TerStatus
        {
            get { return _ter_status; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for TerStatus", value, value.ToString());

                _isChanged |= (_ter_status != value); _ter_status = value;
            }
        }
			
		/// <summary>
		/// 
		/// </summary>		
        public string ReplyDataName
        {
            get { return _replydataname; }
            set
            {
                _isChanged |= (_replydataname != value); _replydataname = value;
            }
        }
				
		public int StartData
        {
            get { return _startdata;}
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

        //public int NewRow 
        //{
        //    get { return _newRow; }
        //    set { _newRow = value; }
        //}

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
