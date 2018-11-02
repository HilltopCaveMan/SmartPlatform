using System;

using GInterfaceDAL.InterFace;
using System.Collections.Generic;
using GModel.InterFace;
using IBatisNet.DataMapper;

namespace GDAL.InterFace
{
    public class SendcmdDao : BaseSqlMapDao, ISendcmdDao
    {
        public IList<Sendcmd> GetSendcmdPage(object o)
        {
            return ExecuteQueryForList<Sendcmd>("Sendcmd.SelectSendcmdPage", o);
        }

        public int GetSendcmdCount(object o)
        {
            object count = ExecuteQueryForObject("Sendcmd.SelectSendcmdCount", o);
            return (int)count;
        }

        public IList<Sendcmd> SelectSendcmdListByTerNos(object o)
        {
            return ExecuteQueryForList<Sendcmd>("Sendcmd.SelectSendcmdListByTerNos", o);
        }

        public int SelectSendcmdCountByTerNos(object o)
        {
            object count = ExecuteQueryForObject("Sendcmd.SelectSendcmdCountByTerNos", o);
            return (int)count;
        }


        public IList<Sendcmd> SelectSendcmdList(object o)
        {
            return ExecuteQueryForList<Sendcmd>("Sendcmd.SelectSendcmdList", o);
        }

        public int SelectSendcmdListCount(object o)
        {
            object count = ExecuteQueryForObject("Sendcmd.SelectSendcmdListCount", o);
            return (int)count;
        }

        public IList<Sendcmd> SelectSendcmdHistoryList(object o)
        {
            return ExecuteQueryForList<Sendcmd>("Sendcmd.SelectSendcmdHistoryList", o);
        }

        public int SelectSendcmdHistoryListCount(object o)
        {
            object count = ExecuteQueryForObject("Sendcmd.SelectSendcmdHistoryListCount", o);
            return (int)count;
        }
    }
}
