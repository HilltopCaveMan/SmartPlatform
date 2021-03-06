// Generated by IBatisNetModel

using System;

using GInterfaceDAL.Location;
using System.Collections.Generic;
using GModel.Location;

namespace GDAL.Location
{
    public class RealtimeViewDao : BaseSqlMapDao, IRealtimeViewDao
    {

        RealtimeView IRealtimeViewDao.GetRealtimeView(object userinfoId)
        {
            return (RealtimeView)ExecuteQueryForObject("RealtimeView.SelectRealtimeView", userinfoId);
        }

        public IList<RealtimeView> GetRealtimeViewPage(object o) 
        {
            return ExecuteQueryForList<RealtimeView>("RealtimeView.SelectRealtimeViewPage", o);
        }

		public int GetRealtimeViewCount(object o) 
        {
            object count = ExecuteQueryForObject("RealtimeView.SelectRealtimeViewCount", o);
            return (int)count;
        }
    }
}
	