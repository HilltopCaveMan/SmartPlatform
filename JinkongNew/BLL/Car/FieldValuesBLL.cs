// Generated by IBatisNetModel

using System;
using System.Collections;

using GModel.Car;
using GInterfaceDAL.Car;
using GDAL.Car;
using System.Collections.Generic;

namespace GBLL.Car
{
    public class FieldValuesBLL
    {
        private IFieldValuesDao _iFieldValuesDao = null;
        public FieldValuesBLL()
        {
            _iFieldValuesDao = new FieldValuesDao();
        }

		public FieldValues GetFieldValues(object o)
        {
            return _iFieldValuesDao.GetFieldValues( o);
        }

        public IList<FieldValues> GetFieldValuesPage(object o)
        {
            return _iFieldValuesDao.GetFieldValuesPage(o);
        }

		public int GetFieldValuesCount(object o)
        {
            return _iFieldValuesDao.GetFieldValuesCount(o);
        }

        //public IList<FieldValues> GetFieldValuesList(object o)
        //{
            //return _iFieldValuesDao.GetFieldValuesList(o);
        //}

		public int Insert(FieldValues entity)
        {
            return  Convert.ToInt32(_iFieldValuesDao.Insert(entity));
        }

        public int Update(FieldValues entity)
        {
            return _iFieldValuesDao.Update(entity);
        }
		
        public int Delete(string condition)
        {
            return _iFieldValuesDao.Delete(condition);
        }
    }
}
	