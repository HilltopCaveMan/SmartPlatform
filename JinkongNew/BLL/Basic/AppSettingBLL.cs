using GDAL.Basic;
using GModel.Basic;
using InterfaceDAL.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBLL.Basic
{
    public class AppSettingBLL
    {
        private IAppSettingDao _iAppSettingDao = null;
        public AppSettingBLL()
        {
            _iAppSettingDao = new AppSettingDao();
        }

        public AppSetting GetAppSettingData(string username)
        {
            return _iAppSettingDao.GetAppSettingData(username);
        }

        public int Insert(AppSetting entity)
        {
            return Convert.ToInt32(_iAppSettingDao.Insert(entity));
        }

        public int Update(AppSetting entity)
        {
            return _iAppSettingDao.Update(entity);
        }
    }
}
