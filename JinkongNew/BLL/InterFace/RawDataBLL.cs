// Generated by IBatisNetModel

using System;
using System.Collections;

using GModel.InterFace;
using GInterfaceDAL.InterFace;
using GDAL.InterFace;
using System.Collections.Generic;

namespace GBLL.InterFace
{
    public class RawDataBLL
    {
        private IRawDataDao _iRawDataDao = null;
        public RawDataBLL()
        {
            _iRawDataDao = new RawDataDao();
        }

        //public RawData GetRawData(object o)
        //{
        //    return _iRawDataDao.GetRawData( o);
        //}

        //public IList<RawData> GetRawDataPage(object o)
        //{
        //    return _iRawDataDao.GetRawDataPage(o);
        //}

        //public int GetRawDataCount(object o)
        //{
        //    return _iRawDataDao.GetRawDataCount(o);
        //}

        /// <summary>
        /// 1��	����ԭʼ���ݱ�
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string insertHistroy(RawData entity)
        {
            try
            {
                _iRawDataDao.Insert(entity);
                return "true";
            }
            catch (Exception e) 
            {
                return e.Message;    
            }          
        }

        //public int Update(RawData entity)
        //{
        //    return _iRawDataDao.Update(entity);
        //}
		
        //public int Delete(string condition)
        //{
        //    return _iRawDataDao.Delete(condition);
        //}
    }
}
	