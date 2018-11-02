// Generated by IBatisNetModel

using System;
using System.Collections;

using GModel.Car;
using GInterfaceDAL.Car;
using GDAL.Car;
using System.Collections.Generic;

namespace GBLL.Car
{
    public class TerminalInfoViewBLL
    {
        private ITerminalInfoViewDao _iTerminalInfoViewDao = null;
        public TerminalInfoViewBLL()
        {
            _iTerminalInfoViewDao = new TerminalInfoViewDao();
        }

		public TerminalInfoView GetTerminalInfoView(object o)
        {
            return _iTerminalInfoViewDao.GetTerminalInfoView( o);
        }

        public IList<TerminalInfoView> SelectTerminalInfoViewByTerNos(object ternos)
        {
            return _iTerminalInfoViewDao.SelectTerminalInfoViewByTerNos(ternos);
        }

        public int SelectTerminalInfoViewByTerNosCount(object ternos)
        {
            return _iTerminalInfoViewDao.SelectTerminalInfoViewByTerNosCount(ternos);
        }

        public IList<TerminalInfoView> GetTerminalInfoViewPage(object o)
        {
            return _iTerminalInfoViewDao.GetTerminalInfoViewPage(o);
        }

        public IList<TerminalInfoView> GetNotBindTerminalInfoViewPage(object o)
        {
            return _iTerminalInfoViewDao.GetNotBindTerminalInfoViewPage(o);
        }

		public int GetTerminalInfoViewCount(object o)
        {
            return _iTerminalInfoViewDao.GetTerminalInfoViewCount(o);
        }

        public int GetNotBindTerminalInfoViewCount(object o)
        {
            return _iTerminalInfoViewDao.GetNotBindTerminalInfoViewCount(o);
        }
    }
}
	