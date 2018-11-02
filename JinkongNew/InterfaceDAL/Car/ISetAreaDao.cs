using System;
using System.Collections.Generic;
using GModel.Car;

namespace GInterfaceDAL.Car
{
    public interface ISetAreaDao
    {
        object Insert(SetArea entity);

        SetArea GetSetAreaData(object terno);

        SetArea GetSetAreaDataById(object areaid);

        IList<SetArea> GetSetAreaPage(object o);

        int Delete(string condition);
    }
}

