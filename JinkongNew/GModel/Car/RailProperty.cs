// Generated by IBatisNetModel
using System;

namespace GModel.Car
{
	[Serializable]
	public class RailProperty
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private int _startdata=0;
        private int _enddata=0;
		private int _newRow = 0;
		
		private string _rail_id; 
		private string _businessdivisionid; 
		private string _rail_data; 
		private DateTime _rail_cdate; 
		private double _rail_state; 
		private string _rail_rname; 
		private double _rail_maxx; 
		private double _rail_minx; 
		private double _rail_maxy; 
		private double _rail_miny; 
		private double _rail_shapetype; 
		private string _rail_center;
        private string _rail_caridstr;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public RailProperty()
		{
			_rail_id = ""; 
			_businessdivisionid = ""; 
			_rail_data = ""; 
			_rail_cdate = new DateTime(); 
			_rail_state = new double(); 
			_rail_rname = ""; 
			_rail_maxx = new double(); 
			_rail_minx = new double(); 
			_rail_maxy = new double(); 
			_rail_miny = new double(); 
			_rail_shapetype = new double(); 
			_rail_center = "";
            _rail_caridstr = "";
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 围栏ID
		/// </summary>		
		public string RailId
		{
			get { return _rail_id; }
			set	
			{
				if( value!= null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for RailId", value, value.ToString());
				
				_isChanged |= (_rail_id != value); _rail_id = value;
			}
		}
			
		/// <summary>
		/// 企业ID
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
		/// 围栏位置
		/// </summary>		
		public string RailData
		{
			get { return _rail_data; }
			set	
			{
				if( value!= null && value.Length > 4000)
					throw new ArgumentOutOfRangeException("Invalid value for RailData", value, value.ToString());
				
				_isChanged |= (_rail_data != value); _rail_data = value;
			}
		}
			
		/// <summary>
		/// 创建时间
		/// </summary>		
		public DateTime RailCdate
		{
			get { return _rail_cdate; }
			set { _isChanged |= (_rail_cdate != value); _rail_cdate = value; }
		}
			
		/// <summary>
		/// 围栏状�?
		/// </summary>		
		public double RailState
		{
			get { return _rail_state; }
			set { _isChanged |= (_rail_state != value); _rail_state = value; }
		}
			
		/// <summary>
		/// 围栏名称
		/// </summary>		
		public string RailRname
		{
			get { return _rail_rname; }
			set	
			{
				if( value!= null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for RailRname", value, value.ToString());
				
				_isChanged |= (_rail_rname != value); _rail_rname = value;
			}
		}
			
		/// <summary>
		/// 最大X
		/// </summary>		
		public double RailMaxx
		{
			get { return _rail_maxx; }
			set { _isChanged |= (_rail_maxx != value); _rail_maxx = value; }
		}
			
		/// <summary>
		/// 最小X
		/// </summary>		
		public double RailMinx
		{
			get { return _rail_minx; }
			set { _isChanged |= (_rail_minx != value); _rail_minx = value; }
		}
			
		/// <summary>
		/// 最大Y
		/// </summary>		
		public double RailMaxy
		{
			get { return _rail_maxy; }
			set { _isChanged |= (_rail_maxy != value); _rail_maxy = value; }
		}
			
		/// <summary>
		/// 最小Y
		/// </summary>		
		public double RailMiny
		{
			get { return _rail_miny; }
			set { _isChanged |= (_rail_miny != value); _rail_miny = value; }
		}
			
		/// <summary>
		/// 围栏类型
		/// </summary>		
		public double RailShapetype
		{
			get { return _rail_shapetype; }
			set { _isChanged |= (_rail_shapetype != value); _rail_shapetype = value; }
		}
			
		/// <summary>
		/// 中心�?
		/// </summary>		
		public string RailCenter
		{
			get { return _rail_center; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for RailCenter", value, value.ToString());
				
				_isChanged |= (_rail_center != value); _rail_center = value;
			}
		}


        public string RailCaridstr
        {
            get { return _rail_caridstr; }
            set {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for RailCaridstr", value, value.ToString());

                _isChanged |= (_rail_caridstr != value); _rail_caridstr = value;
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
