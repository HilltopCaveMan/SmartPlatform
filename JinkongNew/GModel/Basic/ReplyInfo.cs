using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GModel.Basic
{
    public class ReplyInfo
    {
        private string replyid = "";
        private string questionid = "";
        private string replycontent = "";
        private string replyman = "";
        private DateTime replytime = new DateTime();

        public string ReplyId
        {
            get { return replyid; }
            set { replyid = value; }
        }
        public string QuestionId
        {
            get { return questionid; }
            set { questionid = value; }
        }
        public string ReplyContent
        {
            get { return replycontent; }
            set { replycontent = value; }
        }
        public string ReplyMan
        {
            get { return replyman; }
            set { replyman = value; }
        }
        public DateTime ReplyTime
        {
            get { return replytime; }
            set { replytime = value; }
        }
    }
}
