using System;
using System.Collections;
using IBatisNet.Common.Pagination;
//using IBatisNet.DataAccess;
using GModel;
using GInterfaceDAL;
using GDAL;
using System.Data;

namespace GBLL
{
    public class ColligateQueryService
    {
        //private static ColligateQueryService _instance = new ColligateQueryService();
        //private IDaoManager _daoManager = null;
        private IColligateQueryDao _iColligateQueryDao = null;

        public ColligateQueryService()
        {
            //_daoManager = ServiceConfig.GetInstance().DaoManager;
            _iColligateQueryDao = new ColligateQueryDao();
        }

        //public static ColligateQueryService GetInstance()
        //{
        //    return _instance;
        //}

        /// <summary>
        /// �Զ���SQL��ѯ  Ĭ��ʹ��ProteanQuery
        /// </summary>
        /// <param name="condition">����sql</param>
        /// <returns></returns>
        public DataSet GetColligateQuery(string condition)
        {
            return GetColligateQuery("ProteanQuery", condition);
        }

        /// <summary>
        /// �Զ���SQL��ѯ
        /// </summary>
        /// <param name="queryName">xmlӳ������</param>
        /// <param name="condition">sql</param>
        /// <returns></returns>
        public DataSet GetColligateQuery(string queryName, string condition) {
            return _iColligateQueryDao.GetColligateQuery(queryName,condition);
        }

        public DataSet GetColligateQuery(string condition, int currentPage, int pageSize)
        {
            return _iColligateQueryDao.GetColligateQuery("ProteanQuery", condition, currentPage, pageSize);
        }

        public DataSet GetColligateQuery(string queryName, string condition, int currentPage, int pageSize) {
            return _iColligateQueryDao.GetColligateQuery(queryName, condition, currentPage, pageSize);
        }

        //public int GetQueryListCount(string condition)
        //{
        //    return _iColligateQueryDao.GetQueryListCount("ProteanQuery", condition);
        //}

        //public int GetQueryListCount(string queryName, string condition) {
        //    return _iColligateQueryDao.GetQueryListCount(queryName, condition);
        //}

        public int UpdateQuery(string queryName, string condition)
        {
           return _iColligateQueryDao.UpdateColligateQuery(queryName, condition);
        }
    }
}
	