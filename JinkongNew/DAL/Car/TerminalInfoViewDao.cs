// Generated by IBatisNetModel

using System;

using GInterfaceDAL.Car;
using System.Collections.Generic;
using GModel.Car;

namespace GDAL.Car
{
    public class TerminalInfoViewDao : BaseSqlMapDao, ITerminalInfoViewDao
    {

        TerminalInfoView ITerminalInfoViewDao.GetTerminalInfoView(object userinfoId)
        {
            return (TerminalInfoView)ExecuteQueryForObject("TerminalInfoView.SelectTerminalInfoView", userinfoId);
        }

        public IList<TerminalInfoView> SelectTerminalInfoViewByTerNos(object ternos)
        {
            return ExecuteQueryForList<TerminalInfoView>("TerminalInfoView.SelectTerminalInfoViewByTerNos", ternos);
        }

        public int SelectTerminalInfoViewByTerNosCount(object ternos)
        {
            object count = ExecuteQueryForObject("TerminalInfoView.SelectTerminalInfoViewByTerNosCount", ternos);
            return (int)count;
        }

        public IList<TerminalInfoView> GetTerminalInfoViewPage(object o) 
        {
            return ExecuteQueryForList<TerminalInfoView>("TerminalInfoView.SelectTerminalInfoViewPage", o);
        }

        public IList<TerminalInfoView> GetNotBindTerminalInfoViewPage(object o)
        {
            return ExecuteQueryForList<TerminalInfoView>("TerminalInfoView.SelectNotBindTerminalInfoViewPage", o);
        }

		public int GetTerminalInfoViewCount(object o) 
        {
            object count = ExecuteQueryForObject("TerminalInfoView.SelectTerminalInfoViewCount", o);
            return (int)count;
        }

        public int GetNotBindTerminalInfoViewCount(object o)
        {
            object count = ExecuteQueryForObject("TerminalInfoView.SelectNotBindTerminalInfoViewCount", o);
            return (int)count;
        }
    }
}
	