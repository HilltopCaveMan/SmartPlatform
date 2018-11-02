using System;

namespace GModel.Car
{
    /// <summary>
    /// 终端统计
    /// </summary>
    [Serializable]
    public class CarReport_ZDTJView
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
		private string _ter_state; 
		private string _ter_simcard; 
		private DateTime _ter_innettime; 
		private string _ter_typeid; 
		private string _pro_id; 
		private string _pro_model; 
        private string _pro_name; 
        
		private string _car_id; 
		private string _car_no;

        //里程
        private Int32 _car_lc;

        //回传时间
        private string _hcsj;

        //累计离线天数
        private string _ljlxts;

        private string _fxjg;

        //当前报警状态
        private string _dqbjzt;
        //标准比例
        private string _ms_bzbl;
        //精准比例
        private string _ms_jzbl;
        //追车比例
        private string _ms_zcbl;
        //回传次数
        private string _ljhccs;
        //累计工时
        private string _ljgs;
        //统计时间
        private string _tjsj;

		#endregion
		
		#region Default ( Empty ) Class Constuctor

		/// <summary>
		/// default constructor
		/// </summary>
        public CarReport_ZDTJView()
		{
            //_ter_guid = ""; 
            //_dept_id = ""; 
            //_businessdivisionname = ""; 
            //_businessdivisioncode = ""; 
            //_ter_softver = ""; 
            //_ter_hardver = ""; 
            //_ter_state = ""; 
            //_ter_simcard = ""; 
            //_ter_innettime = new DateTime(); 
            //_ter_typeid = ""; 
            //_pro_id = ""; 
            //_pro_model = "";
            //_car_id = "";

            _ter_no = ""; 
			_car_no = "";
            _car_lc = 0;

            _hcsj = new DateTime().ToString("yyyy-MM-dd");
            _ljlxts = "0";

            _fxjg = "";

            //当前报警状态
            _dqbjzt = "";
            //标准比例
            _ms_bzbl = "";
            //精准比例
            _ms_jzbl = "";
            //追车比例
            _ms_zcbl = "";
            //回传次数
            _ljhccs = "";
            //累计工时
            _ljgs = "";
            //统计时间
            _tjsj = "";

		}

		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
        //public string TerGuid
        //{
        //    get { return _ter_guid; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 40)
        //            throw new ArgumentOutOfRangeException("Invalid value for TerGuid", value, value.ToString());
				
        //        _isChanged |= (_ter_guid != value); _ter_guid = value;
        //    }
        //}
			
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
        /// 里程统计
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
        /// 当前报警状态
        /// </summary>
        public string DQBJZT
        {
            get { return _dqbjzt; }
            set
            {
                _isChanged |= (_dqbjzt != value); _dqbjzt = value;
            }
        }

        /// <summary>
        /// 标准比例
        /// </summary>
        public string MS_BZBL
        {
            get { return _ms_bzbl; }
            set
            {
                _isChanged |= (_ms_bzbl != value); _ms_bzbl = value;
            }
        }
        /// <summary>
        /// 精准比例
        /// </summary>
        public string MS_JZBL
        {
            get { return _ms_jzbl; }
            set
            {
                _isChanged |= (_ms_jzbl != value); _ms_jzbl = value;
            }
        }
        /// <summary>
        /// 追车比例
        /// </summary>
        public string MS_ZCBL
        {
            get { return _ms_zcbl; }
            set
            {
                _isChanged |= (_ms_zcbl != value); _ms_zcbl = value;
            }
        }
        /// <summary>
        /// 累计回传次数
        /// </summary>
        public string LJHCCS
        {
            get { return _ljhccs; }
            set
            {
                _isChanged |= (_ljhccs != value); _ljhccs = value;
            }
        }
        /// <summary>
        /// 累计工时
        /// </summary>
        public string LJGS
        {
            get { return _ljgs; }
            set
            {
                _isChanged |= (_ljgs != value); _ljgs = value;
            }
        }
        /// <summary>
        /// 统计时间
        /// </summary>
        public string TJSJ
        {
            get { return _tjsj; }
            set
            {
                _isChanged |= (_tjsj != value); _tjsj = value;
            }
        }
        /*
        //回传次数
        private string _ljhccs;
        //累计工时
        private string _ljgs;
        //统计时间
        private string _tjsj;
        */

        /// <summary>
		/// 
		/// </summary>		
        //public string DeptId
        //{
        //    get { return _dept_id; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 40)
        //            throw new ArgumentOutOfRangeException("Invalid value for DeptId", value, value.ToString());
				
        //        _isChanged |= (_dept_id != value); _dept_id = value;
        //    }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public string Businessdivisionname
        //{
        //    get { return _businessdivisionname; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 100)
        //            throw new ArgumentOutOfRangeException("Invalid value for Businessdivisionname", value, value.ToString());
				
        //        _isChanged |= (_businessdivisionname != value); _businessdivisionname = value;
        //    }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public string Businessdivisioncode
        //{
        //    get { return _businessdivisioncode; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 80)
        //            throw new ArgumentOutOfRangeException("Invalid value for Businessdivisioncode", value, value.ToString());
				
        //        _isChanged |= (_businessdivisioncode != value); _businessdivisioncode = value;
        //    }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public string TerSoftver
        //{
        //    get { return _ter_softver; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 10)
        //            throw new ArgumentOutOfRangeException("Invalid value for TerSoftver", value, value.ToString());
				
        //        _isChanged |= (_ter_softver != value); _ter_softver = value;
        //    }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public string TerHardver
        //{
        //    get { return _ter_hardver; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 10)
        //            throw new ArgumentOutOfRangeException("Invalid value for TerHardver", value, value.ToString());
				
        //        _isChanged |= (_ter_hardver != value); _ter_hardver = value;
        //    }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public string TerState
        //{
        //    get { return _ter_state; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 10)
        //            throw new ArgumentOutOfRangeException("Invalid value for TerState", value, value.ToString());
				
        //        _isChanged |= (_ter_state != value); _ter_state = value;
        //    }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public string TerSimcard
        //{
        //    get { return _ter_simcard; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 13)
        //            throw new ArgumentOutOfRangeException("Invalid value for TerSimcard", value, value.ToString());
				
        //        _isChanged |= (_ter_simcard != value); _ter_simcard = value;
        //    }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public DateTime TerInnettime
        //{
        //    get { return _ter_innettime; }
        //    set { _isChanged |= (_ter_innettime != value); _ter_innettime = value; }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public string TerTypeid
        //{
        //    get { return _ter_typeid; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 40)
        //            throw new ArgumentOutOfRangeException("Invalid value for TerTypeid", value, value.ToString());
				
        //        _isChanged |= (_ter_typeid != value); _ter_typeid = value;
        //    }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public string ProId
        //{
        //    get { return _pro_id; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 40)
        //            throw new ArgumentOutOfRangeException("Invalid value for ProId", value, value.ToString());
				
        //        _isChanged |= (_pro_id != value); _pro_id = value;
        //    }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public string ProModel
        //{
        //    get { return _pro_model; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 100)
        //            throw new ArgumentOutOfRangeException("Invalid value for ProModel", value, value.ToString());
				
        //        _isChanged |= (_pro_model != value); _pro_model = value;
        //    }
        //}

        //public string ProName {
        //    get { return _pro_name; }
        //    set { _pro_name = value; }
        //}
			
		/// <summary>
		/// 
		/// </summary>		
        //public string CarId
        //{
        //    get { return _car_id; }
        //    set	
        //    {
        //        if( value!= null && value.Length > 40)
        //            throw new ArgumentOutOfRangeException("Invalid value for CarId", value, value.ToString());
				
        //        _isChanged |= (_car_id != value); _car_id = value;
        //    }
        //}
			
		
			
		
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
