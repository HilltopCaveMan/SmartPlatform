// Generated by IBatisNetModel
using System;

namespace GModel.Basic
{
	[Serializable]
	public class UserRoleView
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private int _startdata=0;
        private int _enddata=0;
		private int _newRow = 0;
		
		private string _user_id; 
		private string _user_name; 
		private string _user_lname; 
		private string _user_passwrd; 
		private string _user_phone; 
		private string _user_email; 
		private DateTime _user_date; 
		private string _role_id; 
		private string _role_name; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public UserRoleView()
		{
			_user_id = ""; 
			_user_name = ""; 
			_user_lname = ""; 
			_user_passwrd = ""; 
			_user_phone = ""; 
			_user_email = ""; 
			_user_date = new DateTime(); 
			_role_id = ""; 
			_role_name = ""; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public string UserId
		{
			get { return _user_id; }
			set	
			{
				if( value!= null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for UserId", value, value.ToString());
				
				_isChanged |= (_user_id != value); _user_id = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string UserName
		{
			get { return _user_name; }
			set	
			{
				if( value!= null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
				
				_isChanged |= (_user_name != value); _user_name = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string UserLname
		{
			get { return _user_lname; }
			set	
			{
				if( value!= null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for UserLname", value, value.ToString());
				
				_isChanged |= (_user_lname != value); _user_lname = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string UserPasswrd
		{
			get { return _user_passwrd; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for UserPasswrd", value, value.ToString());
				
				_isChanged |= (_user_passwrd != value); _user_passwrd = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string UserPhone
		{
			get { return _user_phone; }
			set	
			{
				if( value!= null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for UserPhone", value, value.ToString());
				
				_isChanged |= (_user_phone != value); _user_phone = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string UserEmail
		{
			get { return _user_email; }
			set	
			{
				if( value!= null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UserEmail", value, value.ToString());
				
				_isChanged |= (_user_email != value); _user_email = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public DateTime UserDate
		{
			get { return _user_date; }
			set { _isChanged |= (_user_date != value); _user_date = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string RoleId
		{
			get { return _role_id; }
			set	
			{
				if( value!= null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for RoleId", value, value.ToString());
				
				_isChanged |= (_role_id != value); _role_id = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string RoleName
		{
			get { return _role_name; }
			set	
			{
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for RoleName", value, value.ToString());
				
				_isChanged |= (_role_name != value); _role_name = value;
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