// Generated by IBatisNetModel

using System;

using GInterfaceDAL.Basic;
using System.Collections.Generic;
using GModel.Basic;

namespace GDAL.Basic
{
    public class UserDeptViewDao : BaseSqlMapDao, IUserDeptViewDao
    {

        UserDeptView IUserDeptViewDao.GetUserDeptView(object userinfoId)
        {
            return (UserDeptView)ExecuteQueryForObject("UserDeptView.SelectUserDeptView", userinfoId);
        }

        public IList<UserDeptView> GetUserDeptViewPage(object o) 
        {
            return ExecuteQueryForList<UserDeptView>("UserDeptView.SelectUserDeptViewPage", o);
        }

		public int GetUserDeptViewCount(object o) 
        {
            object count = ExecuteQueryForObject("UserDeptView.SelectUserDeptViewCount", o);
            return (int)count;
        }

        public IList<UserDeptView> GetTerUserViewPage(object o)
        {
            return ExecuteQueryForList<UserDeptView>("UserDeptView.SelectTerUserViewPage", o);
        }

        public int GetTerUserViewCount(object o)
        {
            object count = ExecuteQueryForObject("UserDeptView.SelectTerUserViewCount", o);
            return (int)count;
        }
    }
}
	