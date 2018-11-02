using GModel.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDAL.Basic
{
    public interface IQuestionReplyDao
    {
        object InsertReply(ReplyInfo entity);
        object InsertQuestion(QuestionInfo entity);
        int UpdateQuestion(QuestionInfo entity);
        int DeleteQuestion(string questionId);

        QuestionInfo GetQuestionInfo(object questionId);

        IList<QuestionInfo> GetQuestionInfoPage(Hashtable ht);

        int GetQuestionInfoCount(Hashtable ht);

        IList<ReplyInfo> GetReplyInfoList(object o);

        int GetReplyInfoCount(object o);

    }
}
