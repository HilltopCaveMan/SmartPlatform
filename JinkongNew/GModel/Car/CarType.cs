// Generated by IBatisNetModel
using System;

namespace GModel.Car
{
    [Serializable]
    public class CarType
    {
        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private int _startdata = 0;
        private int _enddata = 0;
        private int _newRow = 0;

        private string _type_id;
        private string _type_name;
        private byte[] _type_picture;
        private string _type_pictype;
        private string _dept_id;
        private string _type_isdel;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public CarType()
        {
            _type_id = "";
            _type_name = "";
            _type_picture = new byte[] { };
            _type_pictype = "";
            _dept_id = "";
            _type_isdel = "";
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties

        /// <summary>
        /// 类型ID
        /// </summary>		
        public string TypeId
        {
            get { return _type_id; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for TypeId", value, value.ToString());

                _isChanged |= (_type_id != value); _type_id = value;
            }
        }

        /// <summary>
        /// 类型名称
        /// </summary>		
        public string TypeName
        {
            get { return _type_name; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());

                _isChanged |= (_type_name != value); _type_name = value;
            }
        }

         /// <summary>
        /// 是否删除
        /// </summary>		
        public string TypeIsdel
        {
            get { return _type_isdel; }
            set { _type_isdel = value;}
        }

        /// <summary>
        /// 图片
        /// </summary>		
        public byte[] TypePicture
        {
            get { return _type_picture; }
            set { _isChanged |= (_type_picture != value); _type_picture = value; }
        }

        /// <summary>
        /// 图片类型
        /// </summary>		
        public string TypePictype
        {
            get { return _type_pictype; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TypePictype", value, value.ToString());

                _isChanged |= (_type_pictype != value); _type_pictype = value;
            }
        }

        /// <summary>
        /// 企业ID
        /// </summary>		
        public string DeptId
        {
            get { return _dept_id; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptId", value, value.ToString());

                _isChanged |= (_dept_id != value); _dept_id = value;
            }
        }


        public int StartData
        {
            get { return _startdata; }
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
              set {  _isDeleted=value; }
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
