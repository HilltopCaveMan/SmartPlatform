// Generated by IBatisNetModel

using System;

using GInterfaceDAL.Basic;
using System.Collections.Generic;
using GModel.Basic;

namespace GDAL.Basic
{
    public class UserRoleViewDao : BaseSqlMapDao, IUserRoleViewDao
    {

        UserRoleView IUserRoleViewDao.GetUserRoleView(object userinfoId)
        {
            return (UserRoleView)ExecuteQueryForObject("UserRoleView.SelectUserRoleView", userinfoId);
        }

        public IList<UserRoleView> GetUserRoleViewPage(object o) 
        {
            return ExecuteQueryForList<UserRoleView>("UserRoleView.SelectUserRoleViewPage", o);
        }

		public int GetUserRoleViewCount(object o) 
        {
            object count = ExecuteQueryForObject("UserRoleView.SelectUserRoleViewCount", o);
            return (int)count;
        }
    }
}
	