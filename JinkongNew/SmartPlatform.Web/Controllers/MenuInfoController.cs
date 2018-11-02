using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GModel.RoleRight;
using GBLL.RoleRight;
using GModel;
using GModel.Basic;
using SuperGPS.App_Start;
using System.Collections;

namespace SuperGPS.Controllers
{
    public class MenuInfoController : BaseController
    {
        MenuViewBLL menuViewBll = new MenuViewBLL();
        MenuInfoBLL menuInfoBll = new MenuInfoBLL();
        RoleManuBLL roleManuBll = new RoleManuBLL();
        NotselMenuViewBLL notSelMenuViewBll = new NotselMenuViewBLL();
        SelMenuViewBLL selMenuViewBll = new SelMenuViewBLL();

        //[OutputCache(CacheProfile = "ActionCacheProfile")]
        public ActionResult MenuInfoIndex()
        {
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.AddMenu = "false";
            ViewBag.EditMenu = "false";
            ViewBag.DelMenu = "false";

            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "添加菜单":
                        ViewBag.AddMenu = "true";
                        break;
                    case "修改菜单":
                        ViewBag.DelMenu = "true";
                        break;
                    case "删除菜单":
                        ViewBag.EditMenu = "true";
                        break;
                }
            }
            return View();
        }

        [Log(LogMessage = "菜单管理")]
        //菜单管理-查询菜单
        public string GetMenuData(MenuView mv, int rows, int page)
        {
            new LogMessage().Save("加载菜单列表。");

            mv.StartData = (page - 1) * rows + 1;
            mv.EndData = mv.StartData + rows;
            IList<MenuView> imv = menuViewBll.GetMenuViewPage(mv);

            int count = menuViewBll.GetMenuViewCount(mv);
            return ConvertToJson(imv, count);
        }

        [HttpGet]
        public ActionResult AddMenuForm()
        {
            return View();
        }

        [Log(LogMessage = "添加菜单")]
        [HttpPost]
        public ActionResult AddMenuForm(MenuInfo mi)
        {
            mi.MenuId = System.Guid.NewGuid().ToString();
            int k = menuInfoBll.Insert(mi);

            new LogMessage().Save("ID:" + mi.MenuId + "。");

            if (k == 0)
            {
                return JavaScript("submitFormSucceed();");
            }
            else
            {
                return JavaScript("submitFormError();");
            }
        }

        [HttpGet]
        public ActionResult EditMenuInfo(string MenuId)
        {
            MenuInfo mi = new MenuInfo();
            mi.MenuId = MenuId;

            mi = menuInfoBll.GetMenuInfo(mi);
            ViewBag.MenuParent = mi.MenuParent;
            ViewBag.MenuType = mi.MenuType;
            return View(mi);
        }

        [Log(LogMessage = "修改菜单")]
        [HttpPost]
        public ActionResult EditMenuForm(MenuInfo mi)
        {
            int k = menuInfoBll.Update(mi);

            new LogMessage().Save("ID:" + mi.MenuId + "。");

            if (k == 1)
            {
                return JavaScript("editFormSucceed();");
            }
            else
            {
                return JavaScript("editFormError();");
            }
        }

        [Log(LogMessage = "删除菜单")]
        public string DelMenuInfo(string MenuId)
        {
            int k = menuInfoBll.Delete(MenuId);

            new LogMessage().Save("ID:" + MenuId + "。");

            if (k == 1)
            {
                return "删除成功！";
            }
            else
            {
                return "删除失败！";
            }
        }

        //菜单树
        [UserFilter]
        public ActionResult GetMenuNode(string isTree)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            List<TreeMode> TreeNode = new List<TreeMode>();

            List<MenuInfo> dep = new List<MenuInfo>();
            if (user != null)
            {
                dep = menuViewBll.GetMenuViewListByUser(user);
            }
            if (dep != null)
            {
                foreach (MenuInfo DepartmentObj in dep)
                {
                    TreeNode.Add(TreeMode.CreateMenu(DepartmentObj));
                }
            }
            return Json(TreeNode, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 权限树，带复选框
        /// </summary>
        /// <returns></returns>
        [UserFilter]
        public string GetMenuTree(string UserRole)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            List<TreeModeCheck> TreeNode = new List<TreeModeCheck>();
            
            //if (isTree != null && isTree.Trim() != "")
            //{
            //    TreeMode tm = new TreeMode("false");
            //    tm.id = "";
            //    tm.text = "---选择企业---";
            //    TreeNode.Add(tm);
            //}

            if (user.RoleId != null && user.RoleId.Trim() != "" && UserRole != null && UserRole.Trim() != "")
            {
                Hashtable ht = new Hashtable();
                ht.Add("LocalRoleId", user.RoleId);
                ht.Add("UserRoleId", UserRole);
                List<MenuInfo> dep = new List<MenuInfo>();
                if (user != null)
                {
                    dep = menuViewBll.GetMenuPowerListByRoleId(ht);
                }
                if (dep != null)
                {
                    foreach (MenuInfo DepartmentObj in dep)
                    {
                        TreeNode.Add(TreeModeCheck.CreateRole(DepartmentObj));
                    }
                }
                return ConvertToJson(TreeNode);
            }
            else {
                return "[]";
            }
           
            

        }

        //权限查询未选权限树
        [UserFilter]
        public ActionResult GetNotSelMenu(string RoleId)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            List<TreeMode> TreeNode = new List<TreeMode>();

            List<MenuInfo> dep = new List<MenuInfo>();
            if (user != null)
            {
                dep = notSelMenuViewBll.GetMenuViewListByUser(user);
            }
            if (dep != null)
            {
                foreach (MenuInfo DepartmentObj in dep)
                {
                    TreeNode.Add(TreeMode.CreateMenu(DepartmentObj));
                }
            }
            return Json(TreeNode, JsonRequestBehavior.AllowGet);
        }

        //权限查询已选权限树
        //public ActionResult GetSelMenu(string RoleId)
        //{
        //    UserInfo user = new UserInfo();
        //    user = (UserInfo)Session["LoginUser"];
        //    List<TreeMode> TreeNode = new List<TreeMode>();

        //    List<MenuInfo> dep = new List<MenuInfo>();
        //    if (user != null)
        //    {
        //        dep = selMenuViewBll.GetMenuViewListByUser(user);
        //    }
        //    if (dep != null)
        //    {
        //        foreach (MenuInfo DepartmentObj in dep)
        //        {
        //            TreeNode.Add(TreeMode.CreateMenu(DepartmentObj));
        //        }
        //    }
        //    return Json(TreeNode, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="Id">菜单ID</param>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        [Log(LogMessage = "添加权限")]
        public string AddRight(RoleManu rm)
        {
            if (rm != null && rm.RoleId != null && rm.RoleId.Trim() != "" && rm.MenuId != null && rm.MenuId.Trim() != "")
            {
                return roleManuBll.AddRM(rm);
            }
            else
            {
                return "添加失败，请重新操作";
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <returns></returns>
        [Log(LogMessage = "删除权限")]
        public int CancelRight(RoleManu rm)
        {
            if (rm !=null && rm.MenuId != null && rm.MenuId.Trim() != "" && rm.RoleId != null && rm.RoleId.Trim() != "")
            {
                return roleManuBll.DelRM(rm);
            }
            else
            {
                return -1;
            }
        }
       
        
    }
}