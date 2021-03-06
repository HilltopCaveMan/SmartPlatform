// Generated by IBatisNetModel

using System;

using GInterfaceDAL.Basic;
using System.Collections.Generic;
using GModel.Basic;

namespace GDAL.Basic
{
    public class UserFieldsDao : BaseSqlMapDao, IUserFieldsDao
    {
		 public object Insert(UserFields entity)
        {
            return ExecuteInsert("UserFields.InsertUserFields", entity);
        }

        public int Update(UserFields entity)
        {
            return ExecuteUpdate("UserFields.UpdateUserFields", entity);
        }

		public int Delete(object condition)
        {
            //删除跟插入调同一个方法。
            return ExecuteUpdate("UserFields.DeleteUserFields", condition);
        }

        UserFields IUserFieldsDao.GetUserFields(object userinfoId)
        {
            return (UserFields)ExecuteQueryForObject("UserFields.SelectUserFields", userinfoId);
        }

		//public IList<UserFields> GetUserFieldsList(object o)
        //{
            //return ExecuteQueryForList<UserFields>("UserFields.SelectUserFields", o);  
        //}

        public IList<UserFields> GetUserFieldsPage(object o) 
        {
            return ExecuteQueryForList<UserFields>("UserFields.SelectUserFieldsPage", o);
        }

		public int GetUserFieldsCount(object o) 
        {
            object count = ExecuteQueryForObject("UserFields.SelectUserFieldsCount", o);
            return (int)count;
        }
    }
}
	