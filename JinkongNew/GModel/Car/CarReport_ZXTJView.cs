using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GModel.Car
{
    [Serializable]
    public class CarReport_ZXTJView
    {
        #region Private Members

		private bool _isChanged;
		private bool _isDeleted;
		private int _startdata=0;
        private int _enddata=0;
		
		private string _ter_no;
        private string _car_no;
        private DateTime _rtime; 
        private string _position;
		#endregion
		
		#region Default ( Empty ) Class Constuctor

		/// <summary>
		/// default constructor
		/// </summary>
        public CarReport_ZXTJView()
		{
            _ter_no = ""; 
			_car_no = "";
            _rtime = new DateTime();
            _position = "";
		}

		#endregion 
		
		#region Public Properties
			
		public string TerNo
		{
			get { return _ter_no; }
			set	
			{
				_isChanged |= (_ter_no != value); _ter_no = value;
			}
		}
	
        public string CarNo
        {
            get { return _car_no; }
            set
            {
                _isChanged |= (_car_no != value); _car_no = value;
            }
        }

        public DateTime Rtime
        {
            get { return _rtime; }
            set {
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
