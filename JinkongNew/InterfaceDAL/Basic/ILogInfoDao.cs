// Generated by IBatisNetModel
	
using System;
using System.Collections;
using System.Collections.Generic;
using GModel.Basic;

namespace GInterfaceDAL.Basic
{
    public interface ILogInfoDao
    {
        object Insert(LogInfo entity);

        object Save(IList<LogInfo> list);

        List<LogInfo> GetLogInfoListPage(Hashtable o);

        int GetLogInfoCount(Hashtable o);

        int CreateLogTable();
    }
}	
	
	