// Generated by IBatisNetModel

using GModel.Basic;
using System;
using System.Collections.Generic;

namespace GInterfaceDAL.Basic
{
    public interface IVersionInfoDao 
    {
        IList<VersionInfo> GetVersionViewPage(object o);
        int GetVersionViewCount(object o);

        int Update(VersionInfo entity);
        int Delete(object condition);
        object Insert(VersionInfo entity);

        VersionInfo GetVersionInfo(VersionInfo vi);
    }
}	
	
	