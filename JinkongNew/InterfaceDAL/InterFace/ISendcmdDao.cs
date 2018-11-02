using System;
using System.Collections.Generic;
using GModel.InterFace;

namespace GInterfaceDAL.InterFace
{
    public interface ISendcmdDao
    {
        IList<Sendcmd> GetSendcmdPage(object o);

        int GetSendcmdCount(object o);

        IList<Sendcmd> SelectSendcmdListByTerNos(object o);

        int SelectSendcmdCountByTerNos(object o);

        IList<Sendcmd> SelectSendcmdList(object o);

        int SelectSendcmdListCount(object o);

        IList<Sendcmd> SelectSendcmdHistoryList(object o);

        int SelectSendcmdHistoryListCount(object o);
    }
}	
	
	