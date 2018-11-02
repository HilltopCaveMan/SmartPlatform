// Generated by IBatisNetModel

using System;
using System.Collections;

using GModel.Basic;
using GInterfaceDAL.Basic;
using GDAL.Basic;
using System.Collections.Generic;

namespace GBLL.Basic
{
    public class UserRoleViewBLL
    {
        private IUserRoleViewDao _iUserRoleViewDao = null;
        public UserRoleViewBLL()
        {
            _iUserRoleViewDao = new UserRoleViewDao();
        }

		public UserRoleView GetUserRoleView(object o)
        {
            return _iUserRoleViewDao.GetUserRoleView( o);
        }

        public IList<UserRoleView> GetUserRoleViewPage(object o)
        {
            return _iUserRoleViewDao.GetUserRoleViewPage(o);
        }

		public int GetUserRoleViewCount(object o)
        {
            return _iUserRoleViewDao.GetUserRoleViewCount(o);
        }

        //public IList<UserRoleView> GetUserRoleViewList(object o)
        //{
            //return _iUserRoleViewDao.GetUserRoleViewList(o);
        //}
    }
}
	