// Generated by IBatisNetModel
	
using System;
using System.Collections.Generic;
using GModel.Basic;

namespace GInterfaceDAL.Basic
{
    public interface IDeptInfoViewDao
    {
        DeptInfoView GetDeptInfoView(object userinfoId);
        IList<DeptInfoView> GetDeptInfoViewPage(object o);
        int GetDeptInfoViewCount(object o);

        IList<DeptInfoView> GetGroupDeptInfoPage(object o);
        int GetGroupDeptInfoCount(object o);
    }
}	
	
	