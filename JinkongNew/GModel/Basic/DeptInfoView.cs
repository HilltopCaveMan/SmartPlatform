// Generated by IBatisNetModel
using System;

namespace GModel.Basic
{
	[Serializable]
	public class DeptInfoView
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private int _startdata=0;
        private int _enddata=0;
		private int _newRow = 0;
		
		private string _businessdivisionid; 
		private string _businessdivisionname; 
		private string _businessdivisioncode; 
		private string _fatherid; 
		private string _isdel;
        private string _deptype;
		private string _fbusinessdivisionname; 
		private string _fbusinessdivisioncode; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public DeptInfoView()
		{
			_businessdivisionid = ""; 
			_businessdivisionname = ""; 
			_businessdivisioncode = ""; 
			_fatherid = ""; 
			_isdel = ""; 
			_fbusinessdivisionname = ""; 
			_fbusinessdivisioncode = "";
            _deptype = "";
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public string Businessdivisionid
		{
			get { return _businessdivisionid; }
			set	
			{
				if( value!= null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Businessdivisionid", value, value.ToString());
				
				_isChanged |= (_businessdivisionid != value); _businessdivisionid = value;
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
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Businessdivisionname", value, value.ToString());
				
				_isChanged |= (_businessdivisionname != value); _businessdivisionname = value;
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
				if( value!= null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for Businessdivisioncode", value, value.ToString());
				
				_isChanged |= (_businessdivisioncode != value); _businessdivisioncode = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Fatherid
		{
			get { return _fatherid; }
			set	
			{
				if( value!= null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Fatherid", value, value.ToString());
				
				_isChanged |= (_fatherid != value); _fatherid = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Isdel
		{
			get { return _isdel; }
			set	
			{
				if( value!= null && value.Length > 1)
					throw new ArgumentOutOfRangeException("Invalid value for Isdel", value, value.ToString());
				
				_isChanged |= (_isdel != value); _isdel = value;
			}
		}

        public string DepType
        {
            get { return _deptype; }
            set
            {
                if (value != null && value.Length > 3)
                    throw new ArgumentOutOfRangeException("Invalid value for DepType", value, value.ToString());

                _isChanged |= (_deptype != value); _deptype = value;
            }
        }

		/// <summary>
		/// 
		/// </summary>		
		public string Fbusinessdivisionname
		{
			get { return _fbusinessdivisionname; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Fbusinessdivisionname", value, value.ToString());
				
				_isChanged |= (_fbusinessdivisionname != value); _fbusinessdivisionname = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Fbusinessdivisioncode
		{
			get { return _fbusinessdivisioncode; }
			set	
			{
				if( value!= null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for Fbusinessdivisioncode", value, value.ToString());
				
				_isChanged |= (_fbusinessdivisioncode != value); _fbusinessdivisioncode = value;
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
            set { _isDeleted = value; }
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