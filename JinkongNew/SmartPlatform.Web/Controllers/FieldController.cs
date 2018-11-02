using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GBLL.Basic;
using GModel.Basic;
using GModel.RoleRight;
using SuperGPS.App_Start;
namespace SuperGPS.Controllers
{
    public class FieldController : BaseController
    {
        UserFieldsBLL userFieldsBll = new UserFieldsBLL();

        // GET: Field
        //[OutputCache(CacheProfile = "ActionCacheProfile")]
        public ActionResult FieldsIndex()
        {
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.AddFields = "false";
            ViewBag.EditFields = "false";
            ViewBag.DelFields = "false";
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "添加字段":
                        ViewBag.AddFields = "true";
                        break;
                    case "修改字段":
                        ViewBag.EditFields = "true";
                        break;
                    case "删除字段":
                        ViewBag.DelFields = "true";
                        break;
                }
            }
            return View();
        }

        [UserFilter]
        [Log(LogMessage = "查看用户字段列表")]
        public string GetFields(UserFields uf,int rows,int page) 
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            uf.DeptId = user.EnterId;
            uf.StartData = (page - 1) * rows + 1;
            uf.EndData = uf.StartData + rows;
            IList<UserFields> iuf = userFieldsBll.GetUserFieldsPage(uf);
            int a = userFieldsBll.GetUserFieldsCount(uf);
            string json = ConvertToJson(iuf, a);
            return json;
        }

        public ActionResult addField(UserFields uf) 
        {
            return View();
        }

        [Log(LogMessage = "用户字段添加")]
        public ActionResult FieldForm(UserFields uf)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            uf.DeptId = user.EnterId;
            uf.UfId=System.Guid.NewGuid().ToString();
            var reg = new System.Text.RegularExpressions.Regex(@"[\u4e00-\u9fa5]");
            if (reg.IsMatch(uf.UfName) && uf.UfName!=null)
            {
                int k = userFieldsBll.Insert(uf);

                new LogMessage().Save("ID:" + uf.UfId + ",添加成功。");

                if (k == 0)
                {
                    ModelState.AddModelError("", "添加成功");
                    return JavaScript("submitFormSucceed();");
                }
                else
                {
                    ModelState.AddModelError("", "添加失败");
                    return JavaScript("editFormError();");
                }
            }
            else
            {
                ModelState.AddModelError("", "添加失败");
                return JavaScript("editFormError('用户字段只能输入汉字类型且不能为空');");
            }
        }

        /// <summary>
        /// 编辑窗口
        /// </summary>
        /// <param name="uf"></param>
        /// <returns></returns>
        public ActionResult EditFields(UserFields uf) 
        {
            uf = userFieldsBll.GetUserFields(uf);
            return View(uf) ;
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="uf"></param>
        /// <returns></returns>
        [Log(LogMessage = "用户字段修改")]
        public ActionResult EditForm(UserFields uf) 
        {
             var reg = new System.Text.RegularExpressions.Regex(@"[\u4e00-\u9fa5]");
             if (reg.IsMatch(uf.UfName) && uf.UfName != null)
             {
                 int a = userFieldsBll.Update(uf);

                 new LogMessage().Save("ID:" + uf.UfId + "。");

                 if (a > 0)
                 {
                     return JavaScript("editFormSucceed();");
                 }
                 else
                 {
                     ModelState.AddModelError("", "修改失败");
                     return JavaScript("editFormError();");
                 }
             }
             else
             {
                 ModelState.AddModelError("", "修改失败");
                 return JavaScript("editFormError('用户字段只能输入汉字类型且不能为空');");
             }
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="FieldId"></param>
        /// <returns></returns>
        [Log(LogMessage = "用户字段删除")]
        public int DeleteFields(string UfId) 
        {
            if (UfId != null && UfId.Trim() != null)
            {
                new LogMessage().Save("ID:" + UfId + "。");

                return userFieldsBll.Delete(UfId);                
            }
            else 
            { 
                return 0;
            }           
        }
    }
}