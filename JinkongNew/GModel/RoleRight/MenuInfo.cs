// Generated by IBatisNetModel
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace GModel.RoleRight
{
	//[Serializable]
    [Authorize]
	public class MenuInfo
    {
        #region
        //#region Private Members
        //private bool _isChanged;
        //private bool _isDeleted;
        //private int _startdata=0;
        //private int _enddata=0;
        //private int _newRow = 0;
		
        //private string _menu_id; 
        //private string _menu_name; 
        //private string _menu_url; 
        //private string _menu_parent; 
        //private DateTime _menu_createtime; 
        //private string _menu_icon; 
        //private string _menu_sortorder; 
        //private string _menu_type; 		
        //#endregion
		
        //#region Default ( Empty ) Class Constuctor
        ///// <summary>
        ///// default constructor
        ///// </summary>
        //public MenuInfo()
        //{
        //    _menu_id = ""; 
        //    _menu_name = ""; 
        //    _menu_url = ""; 
        //    _menu_parent = ""; 
        //    _menu_createtime = new DateTime(); 
        //    _menu_icon = ""; 
        //    _menu_sortorder = ""; 
        //    _menu_type = ""; 
        //}
        //#endregion // End of Default ( Empty ) Class Constuctor
        #endregion

        #region Public Properties

        /// <summary>
		/// 
		/// </summary>		
        public string MenuId
        {
            get;
            set;
        }

         [Required(ErrorMessage = "必须输入菜单名称")]
		public string MenuName
		{
            get;
            set;
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string MenuUrl
		{
            get;
            set;
		}


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "父菜单")]
        [Required(ErrorMessage = "必填")]
		public string MenuParent
		{
            get;
            set;
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public DateTime MenuCreatetime
		{
            get;
            set;
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string MenuIcon
		{
            get;
            set;
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string MenuSortorder
		{
            get;
            set;
		}

         [Required(ErrorMessage = "必须选择菜单类型")]
		/// <summary>
		/// 类型1：菜单，2：按钮
		/// </summary>		
		public string MenuType
		{
            get;
            set;
		}
			
		
		public int StartData
        {
            get;
            set;
        }
		
        public int EndData
        {
            get;
            set;
        }
		public int NewRow 
        {
            get;
            set;
        }

        public List<MenuInfo> MenuListSub
        {
            get;
            set;
        }

        public string RmId 
        { 
            get; 
            set; 
        }

        public MenuInfo FatherMenuObj { get; set; }
		/// <summary>
		/// Returns whether or not the object has changed it's values.
		/// </summary>
        //public bool IsChanged
        //{
        //    get { return _isChanged; }
        //}
		
		/// <summary>
		/// Returns whether or not the object has changed it's values.
		/// </summary>
        //public bool IsDeleted
        //{
        //    get { return _isDeleted; }
        //}
		
		#endregion 
		
		
		#region Public Functions
		
		/// <summary>
		/// mark the item as deleted
		/// </summary>
        //public void MarkAsDeleted()
        //{
        //    _isDeleted = true;
        //    _isChanged = true;
        //}


		
		#endregion
		
		
	}
}