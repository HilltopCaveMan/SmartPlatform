// Generated by IBatisNetModel
	
using System;
using System.Collections.Generic;
using GModel.Basic;

namespace GInterfaceDAL.Basic
{
    public interface IUserFieldsDao
    {
        //IList<UserFields> GetUserFieldsList(object o);
        int Update(UserFields entity);
        int Delete(object condition);
        object Insert(UserFields entity);

        UserFields GetUserFields(object userinfoId);

        IList<UserFields> GetUserFieldsPage(object o);
        int GetUserFieldsCount(object o);
    }
}	
	
	