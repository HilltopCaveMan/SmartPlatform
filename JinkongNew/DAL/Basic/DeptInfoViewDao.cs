// Generated by IBatisNetModel

using System;

using GInterfaceDAL.Basic;
using System.Collections.Generic;
using GModel.Basic;

namespace GDAL.Basic
{
    public class DeptInfoViewDao : BaseSqlMapDao, IDeptInfoViewDao
    {

        DeptInfoView IDeptInfoViewDao.GetDeptInfoView(object userinfoId)
        {
            return (DeptInfoView)ExecuteQueryForObject("DeptInfoView.SelectDeptInfoView", userinfoId);
        }

        public IList<DeptInfoView> GetDeptInfoViewPage(object o) 
        {
            return ExecuteQueryForList<DeptInfoView>("DeptInfoView.SelectDeptInfoViewPage", o);
        }

		public int GetDeptInfoViewCount(object o) 
        {
            object count = ExecuteQueryForObject("DeptInfoView.SelectDeptInfoViewCount", o);
            return (int)count;
        }

        public IList<DeptInfoView> GetGroupDeptInfoPage(object o)
        {
            return ExecuteQueryForList<DeptInfoView>("DeptInfoView.SelectGroupDeptInfoPage", o);
        }

        public int GetGroupDeptInfoCount(object o)
        {
            object count = ExecuteQueryForObject("DeptInfoView.SelectGroupDeptInfoCount", o);
            return (int)count;
        }
    }
}
	