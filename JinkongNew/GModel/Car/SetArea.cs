using System;

namespace GModel.Car
{
    [Serializable]
    public class SetArea
    {
        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private int _startdata = 0;
        private int _enddata = 0;
        private int _newRow = 0;

        private string _areaId;
        private string _businessdivisionCode;
        private string _area_Terno;
        private string _area_Type;
        private string _area_Data;
        private string _area_Maptype;
        private DateTime _createDate;
        private string _userName;
        private string _centerloglatzoom;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public SetArea()
        {
            _areaId = "";
            _businessdivisionCode = "";
            _area_Terno = "";
            _area_Type = "";
            _area_Data = "";
            _area_Maptype = "";
            _createDate = new DateTime();
            _userName = "";
            _centerloglatzoom = "";
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// 区域ID
        /// </summary>		
        public string AreaId
        {
            get { return _areaId; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for AreaId", value, value.ToString());

                _isChanged |= (_areaId != value); _areaId = value;
            }
        }

        /// <summary>
        /// 部门编码
        /// </summary>		
        public string BusinessdivisionCode
        {
            get { return _businessdivisionCode; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for BusinessdivisionCode", value, value.ToString());

                _isChanged |= (_businessdivisionCode != value); _businessdivisionCode = value;
            }
        }

        /// <summary>
        /// 终端编号
        /// </summary>		
        public string Area_Terno
        {
            get { return _area_Terno; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Area_Terno", value, value.ToString());

                _isChanged |= (_area_Terno != value); _area_Terno = value;
            }
        }

        /// <summary>
        /// 区域类型（0 方形 1 圆 2 多边形）
        /// </summary>		
        public string Area_Type
        {
            get { return _area_Type; }
            set
            {
                if (value != null && value.Length > 3)
                    throw new ArgumentOutOfRangeException("Invalid value for Area_Type", value, value.ToString());
                _isChanged |= (_area_Type != value); _area_Type = value;
            }
        }

        /// <summary>
        /// 区域位置信息
        /// </summary>		
        public string Area_Data
        {
            get { return _area_Data; }
            set
            {
                if (value != null && value.Length > 4000)
                    throw new ArgumentOutOfRangeException("Invalid value for Area_Data", value, value.ToString());

                _isChanged |= (_area_Data != value); _area_Data = value;
            }
        }

        /// <summary>
        /// 地图类型（0 经纬 1 百度 2 谷歌）
        /// </summary>		
        public string Area_Maptype
        {
            get { return _area_Maptype; }
            set
            {
                if (value != null && value.Length > 3)
                    throw new ArgumentOutOfRangeException("Invalid value for Area_Maptype", value, value.ToString());
                _isChanged |= (_area_Maptype != value); _area_Maptype = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _isChanged |= (_createDate != value); _createDate = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
                _isChanged |= (_userName != value); _userName = value;
            }
        }

        public string CenterloglatZoom
        {
            get { return _centerloglatzoom; }
            set {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CenterloglatZoom", value, value.ToString());
                _isChanged |= (_centerloglatzoom != value); _centerloglatzoom = value;            
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

