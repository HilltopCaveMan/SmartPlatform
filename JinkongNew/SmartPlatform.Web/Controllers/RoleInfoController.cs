using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GBLL.RoleRight;
using GModel.RoleRight;
using SuperGPS.App_Start;
using GModel.Basic;
using GBLL.Basic;

namespace SuperGPS.Controllers
{
    public class RoleInfoController : BaseController
    {
        RoleInfoBLL roleInfoBll = new RoleInfoBLL();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();

        // GET: RoleInfo
        //主页面
        //[OutputCache(CacheProfile = "ActionCacheProfile")]
        public ActionResult RoleIndex()
        {
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.AddRole = "false";
            ViewBag.EditRole = "false";
            ViewBag.DelRole = "false";
            ViewBag.RightManager = "false";
            
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "添加角色":
                        ViewBag.AddRole = "true";
                        break;
                    case "删除角色":
                        ViewBag.DelRole = "true";
                        break;
                    case "修改角色":
                        ViewBag.EditRole = "true";
                        break;
                    case "权限分配":
                        ViewBag.RightManager = "true";
                        break;
                }
            }
            return View();
        }

         [Log(LogMessage = "查看角色列表信息")]
        //查询角色列表
         [UserFilter]
        public string GetRoleList(RoleView ri, string ChildrenSel,int rows,int page)
        {
            //RoleView ri = new RoleView();
            if (ri.DeptId == null || ri.DeptId.Trim() == "")
            { 
                UserInfo user = new UserInfo();
                user = (UserInfo)Session["LoginUser"];
                ri.DeptId = user.EnterId;
            }

            if (ChildrenSel == "true")
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(ri.DeptId);
                if (di != null)
                {
                    ri.Businessdivisioncode = di.Businessdivisioncode;
                    ri.DeptId = "";
                }
            }
            ri.StartData = (page - 1) * rows + 1;
            ri.EndData = ri.StartData + rows;
            IList<RoleView> iri = roleInfoBll.GetRoleInfoPage(ri);
            int total = roleInfoBll.GetRoleInfoCount(ri);
            return ConvertToJson(iri,total);
        }

        //查询角色树，用于下拉框
        [UserFilter]
        public string GetRoleTree()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            DeptInfo di = deptInfoBll.GetDeptInfo(user.EnterId);
            IList<RoleView> iri = roleInfoBll.GetRoleInfoDownList(di.Businessdivisioncode);
            return ConvertToJson(iri);
        }

        //添加角色视图
        public ActionResult AddRoleInfo()
        {
            return View();
        }

        //添加角色
        public ActionResult AddRoleForm(RoleInfo ri)
        {
            ri.RoleId = System.Guid.NewGuid().ToString();
            ri.RoleDate = DateTime.Now;
            int k = roleInfoBll.Insert(ri);

            new LogMessage().Save("ID:" + ri.RoleId + "。");

            if (k == 0)
            {
                return JavaScript("submitFormSucceed();");
            }
            else 
            {
                return JavaScript("submitFormError();");
            }
        }
        //编辑角色视图
        public ActionResult EditRoleInfo(string RoleId)
        {
            //RoleInfo ri = new RoleInfo();
            //ri.RoleId = RoleId;
            RoleInfo ri = roleInfoBll.GetRoleInfo(RoleId);
            return View(ri);
        }

        [Log(LogMessage = "角色修改")]
        //编辑角色
        public ActionResult EditRoleForm(RoleInfo ri)
        {
            ri.RoleDate = DateTime.Now;
            int k = roleInfoBll.Update(ri);

            new LogMessage().Save("ID:" + ri.RoleId + "。");

            if (k > 0)
            {
                return JavaScript("submitFormSucceed();");
            }
            else 
            {
                return JavaScript("submitFormError();");
            }           
        }

        [Log(LogMessage = "角色删除")]
        //删除角色
        public int DelRoleInfo(string RoleId)
        {
           int k = roleInfoBll.Delete(RoleId);

           new LogMessage().Save("ID:" + RoleId + "。");

           return k;
        }

        //权限分配 
        public ActionResult RightManager(RoleInfo ri)
        {
            ViewBag.RoleID = ri.RoleId;
            ViewBag.RoleName = ri.RoleName;
            return View();
        }
    }
}