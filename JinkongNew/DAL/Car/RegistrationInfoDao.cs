// Generated by IBatisNetModel

using System;

using GInterfaceDAL.Car;
using System.Collections.Generic;
using GModel.Car;

namespace GDAL.Car
{
    public class RegistrationInfoDao : BaseSqlMapDao, IRegistrationInfoDao
    {
		 public object Insert(RegistrationInfo entity)
        {
            return ExecuteInsert("RegistrationInfo.InsertRegistrationInfo", entity);
        }

        public int Update(RegistrationInfo entity)
        {
            return ExecuteUpdate("RegistrationInfo.UpdateRegistrationInfo", entity);
        }

		public int Delete(object condition)
        {
            //删除跟插入调同一个方法。
            return ExecuteUpdate("RegistrationInfo.DeleteRegistrationInfo", condition);
        }

        RegistrationInfo IRegistrationInfoDao.GetRegistrationInfo(object userinfoId)
        {
            return (RegistrationInfo)ExecuteQueryForObject("RegistrationInfo.SelectRegistrationInfo", userinfoId);
        }

		//public IList<RegistrationInfo> GetRegistrationInfoList(object o)
        //{
            //return ExecuteQueryForList<RegistrationInfo>("RegistrationInfo.SelectRegistrationInfo", o);  
        //}

        public IList<RegistrationInfo> GetRegistrationInfoPage(object o) 
        {
            return ExecuteQueryForList<RegistrationInfo>("RegistrationInfo.SelectRegistrationInfoPage", o);
        }

		public int GetRegistrationInfoCount(object o) 
        {
            object count = ExecuteQueryForObject("RegistrationInfo.SelectRegistrationInfoCount", o);
            return (int)count;
        }
    }
}
	