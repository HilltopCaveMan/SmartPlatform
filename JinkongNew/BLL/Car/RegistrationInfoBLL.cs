// Generated by IBatisNetModel

using System;
using System.Collections;

using GModel.Car;
using GInterfaceDAL.Car;
using GDAL.Car;
using System.Collections.Generic;

namespace GBLL.Car
{
    public class RegistrationInfoBLL
    {
        private IRegistrationInfoDao _iRegistrationInfoDao = null;
        public RegistrationInfoBLL()
        {
            _iRegistrationInfoDao = new RegistrationInfoDao();
        }

		public RegistrationInfo GetRegistrationInfo(object o)
        {
            return _iRegistrationInfoDao.GetRegistrationInfo( o);
        }

        public IList<RegistrationInfo> GetRegistrationInfoPage(object o)
        {
            return _iRegistrationInfoDao.GetRegistrationInfoPage(o);
        }

		public int GetRegistrationInfoCount(object o)
        {
            return _iRegistrationInfoDao.GetRegistrationInfoCount(o);
        }

        //public IList<RegistrationInfo> GetRegistrationInfoList(object o)
        //{
            //return _iRegistrationInfoDao.GetRegistrationInfoList(o);
        //}

		public int Insert(RegistrationInfo entity)
        {
            return  Convert.ToInt32(_iRegistrationInfoDao.Insert(entity));
        }

        public int Update(RegistrationInfo entity)
        {
            return _iRegistrationInfoDao.Update(entity);
        }
		
        public int Delete(string condition)
        {
            return _iRegistrationInfoDao.Delete(condition);
        }
    }
}
	