// Generated by IBatisNetModel
using System;

namespace GModel.InterFace
{
	[Serializable]
	public class RawData
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private int _startdata=0;
        private int _enddata=0;
		private int _newRow = 0;
		
		private string _rawdataid; 
		private string _ter_no; 
		private string _information; 
		private DateTime _information_time; 
		private string _remark; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public RawData()
		{
			_rawdataid = ""; 
			_ter_no = ""; 
			_information = ""; 
			_information_time = new DateTime(); 
			_remark = ""; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public string Rawdataid
		{
			get { return _rawdataid; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Rawdataid", value, value.ToString());
				
				_isChanged |= (_rawdataid != value); _rawdataid = value;
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
				if( value!= null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TerNo", value, value.ToString());
				
				_isChanged |= (_ter_no != value); _ter_no = value;
			}
		}
			
		/// <summary>
		/// 原始数据
		/// </summary>		
		public string Information
		{
			get { return _information; }
			set	
			{
				if( value!= null && value.Length > 700)
					throw new ArgumentOutOfRangeException("Invalid value for Information", value, value.ToString());
				
				_isChanged |= (_information != value); _information = value;
			}
		}
			
		/// <summary>
		/// 原始数据接收时间
		/// </summary>		
		public DateTime InformationTime
		{
			get { return _information_time; }
			set { _isChanged |= (_information_time != value); _information_time = value; }
		}
			
		/// <summary>
		/// 备注
		/// </summary>		
		public string Remark
		{
			get { return _remark; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Remark", value, value.ToString());
				
				_isChanged |= (_remark != value); _remark = value;
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