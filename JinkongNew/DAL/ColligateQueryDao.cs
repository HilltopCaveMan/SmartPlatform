using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IBatisNet.Common.Pagination;
using GInterfaceDAL;

namespace GDAL
{
    public class ColligateQueryDao : BaseSqlMapDao, IColligateQueryDao
    {
        #region IColligateQuery 成员

        public DataSet GetColligateQuery(string queryName,string condition)
        {
            DataSet ds = QueryForDataSet(queryName, condition);
            return ds;
        }

        public DataSet GetColligateQuery(string queryName, string condition, int currentPage, int pageSize)
        {
            DataSet ds = QueryForDataSet(queryName, condition, currentPage, pageSize);
            return ds;
        }

        //public int GetQueryListCount(string queryName, string condition)
        //{
        //    int result = QueryForListCount(queryName, condition);
        //    return result;
        //}

        public int UpdateColligateQuery(string condition) {
            return ExecuteUpdate("ColligateQuery.ProteanUpdate", condition);
        }

        public int UpdateColligateQuery(string queryName,string condition)
        {
            return ExecuteUpdate(queryName,condition);
        }

        #endregion
    }
}
