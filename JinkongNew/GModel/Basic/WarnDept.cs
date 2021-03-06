// Generated by IBatisNetModel
using System;

namespace GModel.Basic
{
	[Serializable]
	public sealed class WarnDept
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private double _online_time; 
		private string _iswarning; 
		private string _dept_id;
        private string _businessdivisionname; 
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public WarnDept()
		{
			_online_time = new double(); 
			_iswarning = ""; 
			_dept_id = "";
            _businessdivisionname = "";  
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 车辆在线时间(单位：小时)
		/// </summary>		
		public double OnlineTime
		{
			get { return _online_time; }
			set { _isChanged |= (_online_time != value); _online_time = value; }
		}
			
		/// <summary>
		/// 电子围栏报警(0:不报警，1：报警)
		/// </summary>		
		public string Iswarning
		{
			get { return _iswarning; }
			set	
			{
				if( value!= null && value.Length > 1)
					throw new ArgumentOutOfRangeException("Invalid value for Iswarning", value, value.ToString());
				
				_isChanged |= (_iswarning != value); _iswarning = value;
			}
		}
			
		/// <summary>
		/// 所属企业ID
		/// </summary>		
		public string DeptId
		{
			get { return _dept_id; }
			set	
			{
				if( value!= null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for DeptId", value, value.ToString());
				
				_isChanged |= (_dept_id != value); _dept_id = value;
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
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Businessdivisionname", value, value.ToString());

                _isChanged |= (_businessdivisionname != value); _businessdivisionname = value;
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
