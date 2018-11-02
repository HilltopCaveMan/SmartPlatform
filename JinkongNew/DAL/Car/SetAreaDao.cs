using System;
using GInterfaceDAL.Car;
using System.Collections.Generic;
using GModel.Car;

namespace GDAL.Car
{
    public class SetAreaDao : BaseSqlMapDao,ISetAreaDao
    {
        public object Insert(SetArea entity)
        {
            return ExecuteInsert("SetArea.InsertSetArea", entity);
        }

        public SetArea GetSetAreaData(object terno)
        {
            return (SetArea)ExecuteQueryForObject("SetArea.SelectSetAreaData", terno);
        }

        public SetArea GetSetAreaDataById(object areaid)
        {
            return (SetArea)ExecuteQueryForObject("SetArea.SelectSetAreaDataById", areaid);
        }

        public IList<SetArea> GetSetAreaPage(object o)
        {
            return ExecuteQueryForList<SetArea>("SetArea.SelectSetAreaDataPage", o);
        }

        public int Delete(string condition)
        {
            return ExecuteUpdate("SetArea.DeleteSetArea", condition);
        }
    }
}
