using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IBatisNet.Common.Pagination;
using GModel;

namespace GInterfaceDAL
{
    public interface IColligateQueryDao
    {
        DataSet GetColligateQuery(string queryName, string condition);

        DataSet GetColligateQuery(string queryName, string condition, int currentPage, int pageSize);

        //int GetQueryListCount(string queryName, string condition);

        int UpdateColligateQuery(string condition);

        int UpdateColligateQuery(string queryName, string condition);
    }
}
