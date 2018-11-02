// Generated by IBatisNetModel

using System;

using GInterfaceDAL.Car;
using System.Collections.Generic;
using GModel.Car;

namespace GDAL.Car
{
    public class MonitorViewDao : BaseSqlMapDao, IMonitorViewDao
    {

        MonitorView IMonitorViewDao.GetMonitorView(object userinfoId)
        {
            return (MonitorView)ExecuteQueryForObject("MonitorView.SelectMonitorView", userinfoId);
        }

        public IList<MonitorView> GetMonitorViewPage(object o) 
        {
            return ExecuteQueryForList<MonitorView>("MonitorView.SelectMonitorViewPage", o);
        }

		public int GetMonitorViewCount(object o) 
        {
            object count = ExecuteQueryForObject("MonitorView.SelectMonitorViewCount", o);
            return (int)count;
        }
    }
}
	