using System;
using System.Collections;
using GModel.Car;
using GInterfaceDAL.Car;
using GDAL.Car;
using System.Collections.Generic;

namespace GBLL.Car
{
    public class SetAreaBLL
    {
        private ISetAreaDao _iSetAreaDao = null;
        public SetAreaBLL()
        {
            _iSetAreaDao = new SetAreaDao();
        }

        public int Insert(SetArea entity)
        {
            return Convert.ToInt32(_iSetAreaDao.Insert(entity));
        }

        public SetArea GetSetAreaData(object o)
        {
            return _iSetAreaDao.GetSetAreaData(o);
        }

        public SetArea GetSetAreaDataById(object o)
        {
            return _iSetAreaDao.GetSetAreaDataById(o);
        }

        public IList<SetArea> GetSetAreaPage(object o)
        {
            return _iSetAreaDao.GetSetAreaPage(o);
        }

        public int Delete(string condition)
        {
            return _iSetAreaDao.Delete(condition);
        }
    }
}
