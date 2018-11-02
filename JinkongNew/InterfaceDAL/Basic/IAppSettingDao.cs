using GModel.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDAL.Basic
{
    public interface IAppSettingDao
    {
        AppSetting GetAppSettingData(string username);

        object Insert(AppSetting entity);

        int Update(AppSetting entity);
    }
}
