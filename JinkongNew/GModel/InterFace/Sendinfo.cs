// Generated by IBatisNetModel
using System;

namespace GModel.InterFace
{
	[Serializable]
	public class Sendinfo
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private int _startdata=0;
        private int _enddata=0;
		private int _newRow = 0;
		
		private double _id; 
		private string _device_id; 
		private string _carinfo_sim; 
		private string _sendinfo_command; 
		private DateTime _sendinfo_ptime; 
		private double _sendinfo_status; 
		private string _sendinfo_userid; 
		private string _sendinfo_description; 
		private string _receipt_flag; 
		private DateTime _receipt_time; 
		private string _receipt_succflag; 
		private string _wanguan; 
		private string _sendmethod; 
		private string _resultstr; 
		private string _formvalue; 
		private double _isonline; 
		private DateTime _sendtotime; 
		private string _sendtocmd; 
		private double _setcount; 
		private double _type; 
		private string _comandtype;
        private string _sendguid;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Sendinfo()
		{
			_id = new double(); 
			_device_id = ""; 
			_carinfo_sim = ""; 
			_sendinfo_command = ""; 
			_sendinfo_ptime = new DateTime(); 
			_sendinfo_status = new double(); 
			_sendinfo_userid = ""; 
			_sendinfo_description = ""; 
			_receipt_flag = ""; 
			_receipt_time = new DateTime(); 
			_receipt_succflag = ""; 
			_wanguan = ""; 
			_sendmethod = ""; 
			_resultstr = ""; 
			_formvalue = ""; 
			_isonline = new double(); 
			_sendtotime = new DateTime(); 
			_sendtocmd = ""; 
			_setcount = new double(); 
			_type = new double(); 
			_comandtype = "";
            _sendguid = "";
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// ID
		/// </summary>		
		public double Id
		{
			get { return _id; }
			set { _isChanged |= (_id != value); _id = value; }
		}
			
		/// <summary>
		/// 终端编号
		/// </summary>		
		public string DeviceId
		{
			get { return _device_id; }
			set	
			{
                //if( value!= null && value.Length > 50)
                //    throw new ArgumentOutOfRangeException("Invalid value for DeviceId", value, value.ToString());
				
				_isChanged |= (_device_id != value); _device_id = value;
			}
		}
			
		/// <summary>
		/// sim卡号
		/// </summary>		
		public string CarinfoSim
		{
			get { return _carinfo_sim; }
			set	
			{
				if( value!= null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CarinfoSim", value, value.ToString());
				
				_isChanged |= (_carinfo_sim != value); _carinfo_sim = value;
			}
		}
			
		/// <summary>
		/// 发送命令码
		/// </summary>		
		public string SendinfoCommand
		{
			get { return _sendinfo_command; }
			set	
			{
				if( value!= null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for SendinfoCommand", value, value.ToString());
				
				_isChanged |= (_sendinfo_command != value); _sendinfo_command = value;
			}
		}
			
		/// <summary>
		/// 发送命令时间
		/// </summary>		
		public DateTime SendinfoPtime
		{
			get { return _sendinfo_ptime; }
			set { _isChanged |= (_sendinfo_ptime != value); _sendinfo_ptime = value; }
		}
			
		/// <summary>
		/// 发送命令状态
		/// </summary>		
		public double SendinfoStatus
		{
			get { return _sendinfo_status; }
			set { _isChanged |= (_sendinfo_status != value); _sendinfo_status = value; }
		}
			
		/// <summary>
		/// 发送人
		/// </summary>		
		public string SendinfoUserid
		{
			get { return _sendinfo_userid; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for SendinfoUserid", value, value.ToString());
				
				_isChanged |= (_sendinfo_userid != value); _sendinfo_userid = value;
			}
		}
			
		/// <summary>
		/// 发送信息描述
		/// </summary>		
		public string SendinfoDescription
		{
			get { return _sendinfo_description; }
			set	
			{
				if( value!= null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for SendinfoDescription", value, value.ToString());
				
				_isChanged |= (_sendinfo_description != value); _sendinfo_description = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string ReceiptFlag
		{
			get { return _receipt_flag; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiptFlag", value, value.ToString());
				
				_isChanged |= (_receipt_flag != value); _receipt_flag = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public DateTime ReceiptTime
		{
			get { return _receipt_time; }
			set { _isChanged |= (_receipt_time != value); _receipt_time = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string ReceiptSuccflag
		{
			get { return _receipt_succflag; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiptSuccflag", value, value.ToString());
				
				_isChanged |= (_receipt_succflag != value); _receipt_succflag = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Wanguan
		{
			get { return _wanguan; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Wanguan", value, value.ToString());
				
				_isChanged |= (_wanguan != value); _wanguan = value;
			}
		}
			
		/// <summary>
		/// 发送方式
		/// </summary>		
		public string Sendmethod
		{
			get { return _sendmethod; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Sendmethod", value, value.ToString());
				
				_isChanged |= (_sendmethod != value); _sendmethod = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Resultstr
		{
			get { return _resultstr; }
			set	
			{
				if( value!= null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Resultstr", value, value.ToString());
				
				_isChanged |= (_resultstr != value); _resultstr = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Formvalue
		{
			get { return _formvalue; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Formvalue", value, value.ToString());
				
				_isChanged |= (_formvalue != value); _formvalue = value;
			}
		}
			
		/// <summary>
		/// 是否在线
		/// </summary>		
		public double Isonline
		{
			get { return _isonline; }
			set { _isChanged |= (_isonline != value); _isonline = value; }
		}
			
		/// <summary>
		/// 执行时间
		/// </summary>		
		public DateTime Sendtotime
		{
			get { return _sendtotime; }
			set { _isChanged |= (_sendtotime != value); _sendtotime = value; }
		}
			
		/// <summary>
		/// 执行命令码
		/// </summary>		
		public string Sendtocmd
		{
			get { return _sendtocmd; }
			set	
			{
				if( value!= null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for Sendtocmd", value, value.ToString());
				
				_isChanged |= (_sendtocmd != value); _sendtocmd = value;
			}
		}

        public string SendGuid
        {
            get { return _sendguid; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SendGuid", value, value.ToString());

                _isChanged |= (_sendguid != value); _sendguid = value;
            }
        }

		/// <summary>
		/// 
		/// </summary>		
		public double Setcount
		{
			get { return _setcount; }
			set { _isChanged |= (_setcount != value); _setcount = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public double Type
		{
			get { return _type; }
			set { _isChanged |= (_type != value); _type = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Comandtype
		{
			get { return _comandtype; }
			set	
			{
				if( value!= null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Comandtype", value, value.ToString());
				
				_isChanged |= (_comandtype != value); _comandtype = value;
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
