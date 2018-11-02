using System;
using System.Collections;

using GModel.InterFace;
using GInterfaceDAL.InterFace;
using GDAL.InterFace;
using System.Collections.Generic;

namespace GBLL.InterFace
{
    public class SendcmdBLL
    {
        private ISendcmdDao _iSendcmdDao = null;
        public SendcmdBLL()
        {
            _iSendcmdDao = new SendcmdDao();
        }

        public IList<Sendcmd> GetSendcmdPage(object o)
        {
            return _iSendcmdDao.GetSendcmdPage(o);
        }

        public int GetSendcmdCount(object o)
        {
            return _iSendcmdDao.GetSendcmdCount(o);
        }

        public IList<Sendcmd> SelectSendcmdListByTerNos(object o)
        {
            return _iSendcmdDao.SelectSendcmdListByTerNos(o);
        }

        public int SelectSendcmdCountByTerNos(object o)
        {
            return _iSendcmdDao.SelectSendcmdCountByTerNos(o);
        }

        public IList<Sendcmd> SelectSendcmdList(object o)
        {
            return _iSendcmdDao.SelectSendcmdList(o);
        }

        public int SelectSendcmdListCount(object o)
        {
            return _iSendcmdDao.SelectSendcmdListCount(o);
        }

        public IList<Sendcmd> SelectSendcmdHistoryList(object o)
        {
            return _iSendcmdDao.SelectSendcmdHistoryList(o);
        }

        public int SelectSendcmdHistoryListCount(object o)
        {
            return _iSendcmdDao.SelectSendcmdHistoryListCount(o);
        }
    }
}
