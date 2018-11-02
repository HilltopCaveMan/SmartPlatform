using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GModel.Basic
{
    [Serializable]
    public class AppSetting
    {
        #region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private int _startdata=0;
        private int _enddata=0;
		private int _newRow = 0;
		
		private string username;
        private DateTime setting_time; 
		private string total_setting; 
		private string online_setting; 
        private string offline_setting;
        private string other_setting;
        private string demolition_alarm;
        private string overspeed_alarm;
        private string zone_alarm;
        private string power_off_alarm;
        private string push_setting;
        private string stock_setting;
        private string expired_setting;
        private string warn_setting;
        private string sleep_setting;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public AppSetting()
		{
            username = "";
            setting_time = new DateTime();
            total_setting = "";
            online_setting = "";
            offline_setting = "";
            other_setting = "";
            demolition_alarm = "";
            overspeed_alarm = "";
            zone_alarm = "";
            power_off_alarm = "";
            push_setting = "";
            warn_setting = "";
            sleep_setting = "";
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties

		/// <summary>
		/// 用户名
		/// </summary>		
        public string UserName
		{
            get { return username; }
			set	
			{
				if( value!= null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());

                _isChanged |= (username != value); username = value;
			}
		}
			
		/// <summary>
		/// 设置时间
		/// </summary>		
        public DateTime settingTime
		{
            get { return setting_time; }
			set	
			{
                _isChanged |= (setting_time != value); setting_time = value;
			}
		}

        /// <summary>
        /// 总数设置
        /// </summary>		
        public string totalSetting
        {
            get { return total_setting; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for totalSetting", value, value.ToString());

                _isChanged |= (total_setting != value); total_setting = value;
            }
        }

			
		/// <summary>
		/// 在线设置
		/// </summary>		
        public string onlineSetting
		{
            get { return online_setting; }
			set	
			{
				if( value!= null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for onlineSetting", value, value.ToString());

                _isChanged |= (online_setting != value); online_setting = value;
			}
		}
			
		/// <summary>
		/// 离线设置
		/// </summary>		
        public string offlineSetting
		{
            get { return offline_setting; }
			set	
			{
				if( value!= null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for offlineSetting", value, value.ToString());

                _isChanged |= (offline_setting != value); offline_setting = value;
			}
		}

        /// <summary>
        /// 其他设置
        /// </summary>
        public string otherSetting
        {
            get { return other_setting; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for otherSetting", value, value.ToString());

                _isChanged |= (other_setting != value); other_setting = value;
            }
        }

        /// <summary>
        /// 拆除报警
        /// </summary>
        public string demolitionAlarm
        {
            get { return demolition_alarm; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for demolitionAlarm", value, value.ToString());

                _isChanged |= (demolition_alarm != value); demolition_alarm = value;
            }
        }

        /// <summary>
        /// 超速报警
        /// </summary>
        public string overspeedAlarm
        {
            get { return overspeed_alarm; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for overspeedAlarm", value, value.ToString());

                _isChanged |= (overspeed_alarm != value); overspeed_alarm = value;
            }
        }

        /// <summary>
        /// 区域报警
        /// </summary>
        public string zoneAlarm
        {
            get { return zone_alarm; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for zoneAlarm", value, value.ToString());

                _isChanged |= (zone_alarm != value); zone_alarm = value;
            }
        }

        /// <summary>
        /// 断电报警
        /// </summary>
        public string poweroffAlarm
        {
            get { return power_off_alarm; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for poweroffAlarm", value, value.ToString());

                _isChanged |= (power_off_alarm != value); power_off_alarm = value;
            }
        }

        /// <summary>
        /// 推送设置
        /// </summary>
        public string pushSetting
        {
            get { return push_setting; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for pushSetting", value, value.ToString());

                _isChanged |= (push_setting != value); push_setting = value;
            }
        }

        /// <summary>
        /// 库存设置
        /// </summary>
        public string stockSetting
        {
            get { return stock_setting; }
            set {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for stockSetting", value, value.ToString());

                _isChanged |= (stock_setting != value); stock_setting = value;
            }
        }

        /// <summary>
        /// 过期设置
        /// </summary>
        public string expiredSetting
        {
            get { return expired_setting; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for expiredSetting", value, value.ToString());

                _isChanged |= (expired_setting != value); expired_setting = value;
            }
        }

        /// <summary>
        /// 接警设置
        /// </summary>
        public string warnSetting
        {
            get { return warn_setting; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for warnSetting", value, value.ToString());

                _isChanged |= (warn_setting != value); warn_setting = value;
            }
        }

        /// <summary>
        /// 休眠设置
        /// </summary>
        public string sleepSetting
        {
            get { return sleep_setting; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for sleepSetting", value, value.ToString());

                _isChanged |= (sleep_setting != value); sleep_setting = value;
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
            set {_isDeleted = value; }
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
