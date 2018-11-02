using GBLL.Basic;
using GModel.Basic;
using SuperGPS.App_Start;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperGPS.Controllers
{
    public class QuestionReplyController : BaseController
    {
        QuestionReplyBLL questionreplybll = new QuestionReplyBLL();
        //
        // GET: /QuestionReply/
        [UserFilter]
        public ActionResult QuestionReplyIndex()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user.UserName == "bcadmin")
            {
                ViewBag.IsAdmin = "true";
            }
            else
            {
                ViewBag.IsAdmin = "false";
            }
            return View();
        }

        /// <summary>
        /// 得到问题列表
        /// </summary>
        /// <returns></returns>
        [UserFilter]
        public string GetQuestionList(QuestionInfo questioninfo,string BeginTime,string EndTime)
        {
            Hashtable ht = new Hashtable();
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (questioninfo.QuestionTitle!=null)
            {
                ht.Add("QuestionTitle", questioninfo.QuestionTitle);
            }
            if (user.UserName == "bcadmin")
            {
                if (questioninfo.QuestionMan != null)
                {
                    ht.Add("QuestionMan", questioninfo.QuestionMan);
                }
            }
            else
            {
                ht.Add("QuestionMan",user.UserName);
            }
            ht.Add("EndData", 31);
            ht.Add("StartData", 0);
            if (BeginTime != null)
            {
                ht.Add("st", BeginTime);
            }
            if (EndTime != null)
            {
                ht.Add("ed", EndTime);
            }
            IList<QuestionInfo> iqi = questionreplybll.GetQuestionInfoPage(ht);
            int total = questionreplybll.GetQuestionInfoCount(ht);
            return ConvertToJson(iqi, total);
        }

        public string GetReplyList(string questionid)
        {
            IList<ReplyInfo> iri = questionreplybll.GetReplyInfoList(questionid);
            // int total = questionreplybll.GetReplyInfoCount(questionid); ,total
            return ConvertToJson(iri);
        }

        public ActionResult AddQuestion()
        {
            return View();
        }

        [UserFilter]
        public ActionResult AddQuestionForm(QuestionInfo questioninfo)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            questioninfo.QuestionId = Guid.NewGuid().ToString();
            questioninfo.QuestionTime = DateTime.Now;
            questioninfo.QuestionMan = user.UserName;
            int k = questionreplybll.InsertQuestion(questioninfo);
            if (k == 0)
            {
                return JavaScript("submitFormSucceed();");
            }
            else
            {
                return JavaScript("submitFormError();");
            }
        }

        public ActionResult EditQuestion(string questionid)
        {
            QuestionInfo questioninfo = questionreplybll.GetQuestionInfo(questionid);
            return View(questioninfo);
        }

        [UserFilter]
        public ActionResult EditQuestionForm(QuestionInfo questioninfo)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            int k = questionreplybll.UpdateQuestion(questioninfo);
            if (k > 0)
            {
                return JavaScript("submitFormSucceed();");
            }
            else
            {
                return JavaScript("submitFormError();");
            }
        }

        public string DelQuestion(string questionid)
        {
          int k = questionreplybll.DeleteQuestion(questionid);
          if (k > 0)
          {
              return "true";
          }
          else
          {
              return "false";
          }
        }

        public ActionResult AddReply(string questionid)
        {
            QuestionInfo questioninfo = questionreplybll.GetQuestionInfo(questionid);
            return View(questioninfo);
        }

        [UserFilter]
        public ActionResult AddReplyForm(ReplyInfo replyinfo)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            replyinfo.ReplyId = Guid.NewGuid().ToString();
            replyinfo.ReplyTime = DateTime.Now;
            replyinfo.ReplyMan = user.UserName;
            int k = questionreplybll.InsertReply(replyinfo);
            if (k == 0)
            {
                return JavaScript("submitFormSucceed();");
            }
            else
            {
                return JavaScript("submitFormError();");
            }
        }

        public ActionResult LookQuestion(string questionid)
        {
            QuestionInfo questioninfo = questionreplybll.GetQuestionInfo(questionid);
            return View(questioninfo);
        }
	}
}