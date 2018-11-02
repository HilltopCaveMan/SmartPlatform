using GModel.Basic;
using InterfaceDAL.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAL.Basic
{
    public class AppSettingDao : BaseSqlMapDao, IAppSettingDao
    {
        public AppSetting GetAppSettingData(string username)
        {
            return (AppSetting)ExecuteQueryForObject("AppSetting.SelectAppSetting", username);
        }

        public object Insert(AppSetting entity)
        {
            return ExecuteInsert("AppSetting.InsertAppSetting", entity);
        }

        public int Update(AppSetting entity)
        {
            return ExecuteUpdate("AppSetting.UpdateAppSetting", entity);
        }
    }
}
