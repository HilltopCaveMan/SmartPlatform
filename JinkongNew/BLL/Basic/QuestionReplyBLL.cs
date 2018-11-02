using GDAL.Basic;
using GModel.Basic;
using InterfaceDAL.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBLL.Basic
{
    public class QuestionReplyBLL
    {
        private IQuestionReplyDao _iQuestionReplyDao = null;
        public QuestionReplyBLL()
        {
            _iQuestionReplyDao = new QuestionReplyDao();
        }

        public QuestionInfo GetQuestionInfo(object o)
        {
            return _iQuestionReplyDao.GetQuestionInfo(o);
        }

        public IList<QuestionInfo> GetQuestionInfoPage(Hashtable ht)
        {
            return _iQuestionReplyDao.GetQuestionInfoPage(ht);
        }

        public int GetQuestionInfoCount(Hashtable ht)
        {
            return _iQuestionReplyDao.GetQuestionInfoCount(ht);
        }

        public IList<ReplyInfo> GetReplyInfoList(string questionid)
        {
            return _iQuestionReplyDao.GetReplyInfoList(questionid);
        }

        public int GetReplyInfoCount(string questionid)
        {
            return _iQuestionReplyDao.GetReplyInfoCount(questionid);
        }

        public int InsertReply(ReplyInfo entity)
        {
            return Convert.ToInt32(_iQuestionReplyDao.InsertReply(entity));
        }

        public int InsertQuestion(QuestionInfo entity)
        {
            return Convert.ToInt32(_iQuestionReplyDao.InsertQuestion(entity));
        }

        public int UpdateQuestion(QuestionInfo entity)
        {
            return _iQuestionReplyDao.UpdateQuestion(entity);
        }

        public int DeleteQuestion(string questionid)
        {
            return _iQuestionReplyDao.DeleteQuestion(questionid);
        }
    }
}
