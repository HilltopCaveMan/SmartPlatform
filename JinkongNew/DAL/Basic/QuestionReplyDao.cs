using GModel.Basic;
using InterfaceDAL.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAL.Basic
{
    public class QuestionReplyDao:BaseSqlMapDao, IQuestionReplyDao
    {
        public object InsertReply(ReplyInfo entity)
        {
            return ExecuteInsert("QuestionReply.InsertReplyInfo", entity);
        }

        public object InsertQuestion(QuestionInfo entity)
        {
            return ExecuteInsert("QuestionReply.InsertQuestionInfo", entity);
        }

        public int UpdateQuestion(QuestionInfo entity)
        {
            return ExecuteUpdate("QuestionReply.UpdateQuestionInfo", entity);
        }

        public int DeleteQuestion(string questionid)
        {
            return ExecuteUpdate("QuestionReply.DeleteQuestionInfo", questionid);
        }

        QuestionInfo IQuestionReplyDao.GetQuestionInfo(object questionid)
        {
            return (QuestionInfo)ExecuteQueryForObject("QuestionReply.SelectQuestionInfo", questionid);
        }

        public IList<QuestionInfo> GetQuestionInfoPage(Hashtable ht)
        {
            return ExecuteQueryForList<QuestionInfo>("QuestionReply.SelectQuestionInfoPage", ht);
        }

        public int GetQuestionInfoCount(Hashtable ht)
        {
            object count = ExecuteQueryForObject("QuestionReply.SelectQuestionCount", ht);
            return (int)count;
        }

        public IList<ReplyInfo> GetReplyInfoList(object o)
        {
            return ExecuteQueryForList<ReplyInfo>("QuestionReply.GetReplyInfoByQuestionId", o);
        }

        public int GetReplyInfoCount(object o)
        {
            object count = ExecuteQueryForObject("QuestionReply.GetReplyCountByQuestionId", o);
            return (int)count;
        }
    }
}
