// Generated by IBatisNetModel
	
using System;
using System.Collections.Generic;
using GModel.RoleRight;
using GModel;

namespace GInterfaceDAL.RoleRight
{
    public interface IMenuInfoDao
    {
        //IList<MenuInfo> GetMenuInfoList(object o);
        int Update(MenuInfo entity);
        int Delete(object condition);
        object Insert(MenuInfo entity);

        MenuInfo GetMenuInfo(object userinfoId);

        IList<MenuInfo> GetMenuInfoPage(object o);
        int GetMenuInfoCount(object o);
        IList<MenuInfo> GetMenuByParentOrMenuName(object o);  
        //IList<MenuInfo> GetMenuTreeAll(); 
        List<TreeMode> GetMenuTreeByRoleId(string RoleId); 
    }
}	
	
	