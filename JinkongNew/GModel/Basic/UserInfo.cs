// Generated by IBatisNetModel
using System;

namespace GModel.Basic
{
    [Serializable]
    public class UserInfo
    {
        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private int _startdata = 0;
        private int _enddata = 0;
        private int _newRow = 0;

        private string _user_id;
        private string _user_name;
        private string _user_lname;
        private string old_Passwrd;
        
        private string _user_passwrd;
        private string _user_repasswrd;
        private string _user_phone;
        private string _user_email;
        private string _enter_id;
        private string _role_id;
        private DateTime _user_date;
        private string _user_isdel;
        private string _user_deptcode;
        private string _user_buildcode;
        private string _user_funct;

        private string _user_funct1;
        private string _user_funct2;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public UserInfo()
        {
            _user_id = "";
            _user_name = "";
            _user_lname = "";
            _user_passwrd = "";
            _user_repasswrd="";
            _user_phone = "";
            _user_email = "";
            _enter_id = "";
            _role_id = "";
            _user_date = new DateTime();
            _user_isdel = "";
            _user_deptcode = "";
            _user_buildcode = "";
            _user_funct = "";

            _user_funct1 = "";
            _user_funct2 = "";
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties

        /// <summary>
        /// 用户ID
        /// </summary>		
        public string UserId
        {
            get { return _user_id; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for UserId", value, value.ToString());

                _isChanged |= (_user_id != value); _user_id = value;
            }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>		
        public string UserName
        {
            get { return _user_name; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());

                _isChanged |= (_user_name != value); _user_name = value;
            }
        }

        /// <summary>
        /// 登录名
        /// </summary>		
        public string UserLname
        {
            get { return _user_lname; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for UserLname", value, value.ToString());

                _isChanged |= (_user_lname != value); _user_lname = value;
            }
        }

           /// <summary>
        /// 旧密码
        /// </summary>		
        public string OldPasswrd
        {
            get { return old_Passwrd; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for UserPasswrd", value, value.ToString());

                _isChanged |= (_user_passwrd != value); old_Passwrd = value;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>		
        public string UserPasswrd
        {
            get { return _user_passwrd; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for UserPasswrd", value, value.ToString());

                _isChanged |= (_user_passwrd != value); _user_passwrd = value;
            }
        }

        /// <summary>
        /// 确认密码
        /// </summary>		
        public string UserRePasswrd
        {
            get { return _user_repasswrd; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for UserPasswrd", value, value.ToString());

                _isChanged |= (_user_repasswrd != value); _user_repasswrd = value;
            }
        }



        /// <summary>
        /// 联系电话
        /// </summary>		
        public string UserPhone
        {
            get { return _user_phone; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UserPhone", value, value.ToString());

                _isChanged |= (_user_phone != value); _user_phone = value;
            }
        }

        /// <summary>
        /// 用户邮箱
        /// </summary>		
        public string UserEmail
        {
            get { return _user_email; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UserEmail", value, value.ToString());

                _isChanged |= (_user_email != value); _user_email = value;
            }
        }

        /// <summary>
        /// 所属企业ID
        /// </summary>		
        public string EnterId
        {
            get { return _enter_id; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for EnterId", value, value.ToString());

                _isChanged |= (_enter_id != value); _enter_id = value;
            }
        }

        /// <summary>
        /// 角色ID
        /// </summary>		
        public string RoleId
        {
            get { return _role_id; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for RoleId", value, value.ToString());

                _isChanged |= (_role_id != value); _role_id = value;
            }
        }

        /// <summary>
        /// 添加时间
        /// </summary>		
        public DateTime UserDate
        {
            get { return _user_date; }
            set { _isChanged |= (_user_date != value); _user_date = value; }
        }

        /// <summary>
        /// 是否已删除  0：未删除，  1：已删除
        /// </summary>		
        public string UserIsdel
        {
            get { return _user_isdel; }
            set
            {
                if (value != null && value.Length > 1)
                    throw new ArgumentOutOfRangeException("Invalid value for UserIsdel", value, value.ToString());

                _isChanged |= (_user_isdel != value); _user_isdel = value;
            }
        }

        /// <summary>
        /// 用户部门编码
        /// </summary>
        public string UserDeptcode
        {
            get { return _user_deptcode; }
            set {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for UserDeptcode",value,value.ToString());
                _isChanged |= (_user_deptcode != value); _user_deptcode = value;
            }
        }

        /// <summary>
        /// 创建者编码
        /// </summary>
        public string UserBuildcode
        {
            get { return _user_buildcode; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for UserBuildcode", value, value.ToString());
                _isChanged |= (_user_buildcode != value); _user_buildcode = value;
            }
        }

        /// <summary>
        /// 是否只限手机装车（0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1, 是限制 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,否限制）
        /// </summary>
        public string UserFunct
        {
            get { return _user_funct; }
            set
            {
                //if (value != null && value.Length > 50)
                //    throw new ArgumentOutOfRangeException("Invalid value for UserFunct", value, value.ToString());
                _isChanged |= (_user_funct != value); _user_funct = value;
            }
        }

        public string UserFunct1
        {
            get { return _user_funct1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UserFunct1", value, value.ToString());
                _isChanged |= (_user_funct1 != value); _user_funct1 = value;
            }
        }

        public string UserFunct2
        {
            get { return _user_funct2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UserFunct2", value, value.ToString());
                _isChanged |= (_user_funct2 != value); _user_funct2 = value;
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
