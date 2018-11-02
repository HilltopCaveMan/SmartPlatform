using System;

namespace GModel.Car
{
    [Serializable]
    public class CarReport_LCTJView
    {
        #region Private Members

		private bool _isChanged;
		private bool _isDeleted;
		private int _startdata=0;
        private int _enddata=0;
		
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
        private double _car_lc;

        private string _fxjg;

        private string _position;
        private string _rtime;

		#endregion
		
		#region Default ( Empty ) Class Constuctor

		/// <summary>
		/// default constructor
		/// </summary>
        public CarReport_LCTJView()
		{
            _ter_no = ""; 
			_car_no = "";
            _ter_typeid = "";
            _car_lc = 0;
            _fxjg = "";
            _position = "";
            _rtime = "";
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
        public string TerTypeid
        {
            get { return _ter_typeid; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for TerTypeid", value, value.ToString());

                _isChanged |= (_ter_typeid != value); _ter_typeid = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double LCTJ
        {
            get { return _car_lc; }
            set
            {
                _isChanged |= (_car_lc != value); _car_lc = value;
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

        public string Rtime
        {
            get { return _rtime; }
            set
            {
                _isChanged |= (_rtime != value); _rtime = value;
            }
        }
        public string Position
        {
            get { return _position; }
            set
            {
                _isChanged |= (_position != value); _position = value;
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
