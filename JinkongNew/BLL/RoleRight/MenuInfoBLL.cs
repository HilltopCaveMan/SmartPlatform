// Generated by IBatisNetModel

using System;
using System.Collections;

using GModel.RoleRight;
using GInterfaceDAL.RoleRight;
using GDAL.RoleRight;
using System.Collections.Generic;
using GModel;

namespace GBLL.RoleRight
{
    public class MenuInfoBLL
    {
        private IMenuInfoDao _iMenuInfoDao = null;
        public MenuInfoBLL()
        {
            _iMenuInfoDao = new MenuInfoDao();
        }

		public MenuInfo GetMenuInfo(object o)
        {
            return _iMenuInfoDao.GetMenuInfo( o);
        }

        public IList<MenuInfo> GetMenuInfoPage(object o)
        {
            return _iMenuInfoDao.GetMenuInfoPage(o);
        }

		public int GetMenuInfoCount(object o)
        {
            return _iMenuInfoDao.GetMenuInfoCount(o);
        }

        //public IList<MenuInfo> GetMenuInfoList(object o)
        //{
            //return _iMenuInfoDao.GetMenuInfoList(o);
        //}

		public int Insert(MenuInfo entity)
        {
            return  Convert.ToInt32(_iMenuInfoDao.Insert(entity));
        }

        public int Update(MenuInfo entity)
        {
            return _iMenuInfoDao.Update(entity);
        }
		
        public int Delete(string condition)
        {
            return _iMenuInfoDao.Delete(condition);
        }

        public IList<MenuInfo> GetMenuByRoleId(string RoleId) 
        {
            return _iMenuInfoDao.GetMenuByParentOrMenuName(RoleId);
        }

        public List<TreeMode> GetMenuTreeByRoleId(string RoleId) 
        {
           return _iMenuInfoDao.GetMenuTreeByRoleId(RoleId);
        }


    }
}
	