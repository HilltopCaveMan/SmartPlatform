// Generated by IBatisNetModel
	
using System;
using System.Collections.Generic;
using GModel.RoleRight;

namespace GInterfaceDAL.RoleRight
{
    public interface IRoleManuDao
    {
        //IList<RoleManu> GetRoleManuList(object o);
        int Update(RoleManu entity);
        int Delete(object condition);
        object Insert(RoleManu entity);

        RoleManu GetRoleManu(object userinfoId);

        IList<RoleManu> GetRoleManuPage(object o);
        int GetRoleManuCount(object o);
        string AddRM(RoleManu rm);
        int DelRM(RoleManu rm);
    }
}	
	
	