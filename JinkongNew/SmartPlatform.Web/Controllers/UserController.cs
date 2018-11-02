using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GModel.Basic;
using GBLL.Basic;
using GModel.RoleRight;
using System.Web.Security;
using SuperGPS.App_Start;

namespace SuperGPS.Controllers
{
    public class UserController : BaseController
    {
        UserDeptViewBLL userDeptViewBll = new UserDeptViewBLL();

        UserInfoBLL userInfoBll = new UserInfoBLL();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();

        // GET: User
        //[OutputCache(CacheProfile = "ActionCacheProfile")]
        [UserFilter]
        public ActionResult UserList()
        {
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.AddDept = "false";
            ViewBag.EditDept = "false";
            ViewBag.DelDept = "false";

            ViewBag.AddUser = "false";
            ViewBag.EditUser = "false";
            ViewBag.DelUser = "false";
            ViewBag.PwdReste = "false";

            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            ViewBag.Deptid = user.EnterId;
            DeptInfo di = deptInfoBll.GetDeptInfo(user.EnterId);
            ViewBag.Deptname = di.Businessdivisionname;
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "添加企业":
                        ViewBag.AddDept = "true";
                        break;
                    case "删除企业":
                        ViewBag.DelDept = "true";
                        break;
                    case "编辑企业":
                        ViewBag.EditDept = "true";
                        break;
                    case "添加用户":
                        ViewBag.AddUser = "true";
                        break;
                    case "删除用户":
                        ViewBag.DelUser = "true";
                        break;
                    case "修改用户":
                        ViewBag.EditUser = "true";
                        break;
                    case "密码初始化":
                        ViewBag.PwdReste = "true";
                        break;
                }
            }
            return View();
        }

        [UserFilter]
        public ActionResult TerUserList()
        {
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.AddDept = "false";
            ViewBag.EditDept = "false";
            ViewBag.DelDept = "false";

            ViewBag.AddUser = "false";
            ViewBag.EditUser = "false";
            ViewBag.DelUser = "false";
            ViewBag.PwdReste = "false";

            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            ViewBag.Deptid = user.EnterId;
            DeptInfo di = deptInfoBll.GetDeptInfo(user.EnterId);
            ViewBag.Deptname = di.Businessdivisionname;
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "添加企业":
                        ViewBag.AddDept = "true";
                        break;
                    case "删除企业":
                        ViewBag.DelDept = "true";
                        break;
                    case "编辑企业":
                        ViewBag.EditDept = "true";
                        break;
                    case "添加用户":
                        ViewBag.AddUser = "true";
                        break;
                    case "删除用户":
                        ViewBag.DelUser = "true";
                        break;
                    case "修改用户":
                        ViewBag.EditUser = "true";
                        break;
                    case "密码初始化":
                        ViewBag.PwdReste = "true";
                        break;
                }
            }
            return View();
        }

        public string GetUserData()
        {
            return "";
        }

        [Log(LogMessage = "查看企业用户列表")]
        //根据所选组织机构给员工列表加载数据
        [UserFilter]
        public string GetygListByzzjgId(UserDeptView ud, int rows, int page, string ChildrenSel)
        {
            ud.StartData = (page - 1) * rows + 1;
            ud.EndData = ud.StartData + rows;
            if (ud.Businessdivisionid == null || ud.Businessdivisionid.Trim() == "") 
            { 
                UserInfo user = new UserInfo();
                user = (UserInfo)Session["LoginUser"];
                ud.Businessdivisionid = user.EnterId;
            }

            if (ChildrenSel == "true") 
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(ud.Businessdivisionid);
                ud.Businessdivisioncode = di.Businessdivisioncode;
                ud.Businessdivisionid = "";
            }

            IList<UserDeptView> lud = userDeptViewBll.GetUserDeptViewPage(ud);
            int a = userDeptViewBll.GetUserDeptViewCount(ud);
            string str = ConvertToJson(lud, a);
            return str;
        }

        [Log(LogMessage = "查看设备用户列表")]
        //根据所选组织机构给员工列表加载数据
        [UserFilter]
        public string GetTerUserListByCode(UserDeptView ud, int rows, int page)
        {
            ud.StartData = (page - 1) * rows + 1;
            ud.EndData = ud.StartData + rows;
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            ud.Businessdivisioncode = user.UserDeptcode;


            IList<UserDeptView> lud = userDeptViewBll.GetTerUserViewPage(ud);
            int a = userDeptViewBll.GetTerUserViewCount(ud);
            string str = ConvertToJson(lud, a);
            return str;
        }

        public ActionResult AddUser(UserInfo userInfo, string DEPARTMENTNAME)
        {
            return View(userInfo);
        }

        public ActionResult AddTerUser(UserInfo userInfo, string DEPARTMENTNAME)
        {
            return View(userInfo);
        }

        [Log(LogMessage = "企业用户添加")]
        public ActionResult AddUserForm(UserInfo ui)
        {
            ui.UserId = System.Guid.NewGuid().ToString();
            
            if(ui.RoleId==null || ui.RoleId=="")
            {
                return JavaScript("submitFormError('用户角色不能为空');");
            }
            if (ui.EnterId != null && ui.EnterId != "")
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(ui.EnterId);
                ui.UserDeptcode = di.Businessdivisioncode;
            }
            else
            {
                return JavaScript("submitFormError('所属企业不正确或者为空');");
            }
            if (ui.UserPasswrd != null && ui.UserPasswrd != "")
            {
                ui.UserPasswrd = FormsAuthentication.HashPasswordForStoringInConfigFile(ui.UserPasswrd, "md5");
            }
            else
            {
                return JavaScript("submitFormError('用户密码不能为空');");
            }
            ui.UserFunct="0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,";
            string[] arr=ui.UserFunct.Split(',');
            if (ui.UserFunct1 == "InstallCar")
            {
                arr[19] = "1";
            }
            if (ui.UserFunct2 == "SendCmd")
            {
                arr[18] = "1";
            }
            ui.UserFunct = string.Join(",",arr);
            ui.UserLname = ui.UserLname.Trim();
            int i = userInfoBll.Insert(ui);
            new LogMessage().Save("名称:" + ui.UserName + "。");

            if (i == 0)
            {
                //刷新用户
                Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "1", "", "", "", "", "", "");
                return JavaScript("submitFormSucceed();");
            }
            else
            {
                if (i == 2)
                {
                    return JavaScript("submitFormError('用户名已存在');");
                }
                else
                {
                    return JavaScript("submitFormError();");
                }
            }
        }

        [Log(LogMessage = "设备用户添加")]
        public ActionResult AddTerUserForm(UserInfo ui,string userters)
        {
            ui.UserId = System.Guid.NewGuid().ToString();
            if (ui.UserPasswrd != null && ui.UserPasswrd != "")
            {
                ui.UserPasswrd = FormsAuthentication.HashPasswordForStoringInConfigFile(ui.UserPasswrd, "md5");
            }
            else
            {
                return JavaScript("submitFormError('用户密码不能为空');");
            }
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            ui.UserLname = ui.UserLname.Trim();
            ui.UserBuildcode = user.UserDeptcode;
            int i = userInfoBll.Insert(ui);
            //循环加入SETMANAGER 
            string[] terno = userters.Trim().Split(',');
            string terstr = "";
            foreach(string ter in terno){
                terstr += ter + "&";
            }
            if (i == 0)
            {
                //添加SETMANAGER表的指令
                Transfers.ClintSendCommData(1108, "1", "6", ui.UserLname, "", "", "", "", "", "", "", terstr, "", "", "", "", "", "", user.UserName);
                //刷新用户
                Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "1", "", "", "", "", "", "");
                System.Threading.Thread.Sleep(2000);
                return JavaScript("submitFormSucceed();");
            }
            else
            {
                if (i == 2)
                {
                    return JavaScript("submitFormError('用户名已存在');");
                }
                else
                {
                    return JavaScript("submitFormError();");
                }
            }
        }

        [Log(LogMessage = "用户删除")]
        public string DelUser(string UserId)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (UserId != null && UserId.Trim() != "")
            {
                UserInfo ui = userInfoBll.GetUserInfo(UserId);
                //删除用户
                int k = userInfoBll.Delete(UserId);

                new LogMessage().Save("ID:" + UserId + "。");

                if (k > 0)
                {
                    //删除设备关联SETMANAGER
                    Transfers.ClintSendCommData(1108, "1", "7", ui.UserLname, "", "", "", "", "", "", "", "", "", "", "", "", "", "",user.UserName);
                    //刷新用户
                    Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "1", "", "", "", "", "", "");

                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }

        [Log(LogMessage = "用户初始化密码")]
        public string ResetUser(string UserId) 
        {
            UserInfo ui = new UserInfo();
            ui.UserId = UserId;
            ui.UserPasswrd = FormsAuthentication.HashPasswordForStoringInConfigFile("123456", "md5");
            if (UserId != null && UserId.Trim() != "")
            {
                int k = userInfoBll.ResetUser(ui);
                if (k > 0)
                {
                    //刷新用户
                    Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "1", "", "", "", "", "", "");
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }

        public ActionResult EditUser(UserInfo ui) 
        {
           UserInfo user= userInfoBll.GetUserInfo(ui);
           ViewBag.EnterId = user.EnterId;
           ViewBag.RoleId = user.RoleId;

           if (user.UserFunct != "" && user.UserFunct != null)
           {
               string[] arr = user.UserFunct.Split(',');
               if (arr[19] == "1")
               {
                   user.UserFunct1 = "InstallCar";
               }
               if (arr[18] == "1")
               {
                   user.UserFunct2 = "SendCmd";
               }
           }
            return View(user);
        }

        [Log(LogMessage = "企业用户修改")]
        public ActionResult EditUserForm(UserInfo ui,string oldname) 
        {
            if (ui.RoleId == null || ui.RoleId == "")
            {
                return JavaScript("editFormError('用户角色不能为空');");
            }
            if (ui.EnterId != null && ui.EnterId != "")
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(ui.EnterId);
                ui.UserDeptcode = di.Businessdivisioncode;
            }
            else
            {
                return JavaScript("editFormError('所属企业不正确或者为空');");
            }
            ui.UserFunct = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,";
            string[] arr = ui.UserFunct.Split(',');
            if (ui.UserFunct1 == "InstallCar")
            {
                arr[19] = "1";
            }
            if (ui.UserFunct2 == "SendCmd")
            {
                arr[18] = "1";
            }
            ui.UserFunct = string.Join(",", arr);
            if (ui.UserLname != oldname)
            {
                UserInfoBLL userbll = new UserInfoBLL();
                ui.UserLname = ui.UserLname.Trim();
                UserInfo uiObj = userbll.GetUserByLName(ui, out uiObj);
                if (uiObj!=null)
                {
                    return JavaScript("editFormError('该用户名已经存在');");
                }
            }

           int k = userInfoBll.Update(ui);

           new LogMessage().Save("ID:" + ui.UserId + "。");

           if (k > 0)
           {
               //刷新用户
               Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "1", "", "", "", "", "", "");
               return JavaScript("editFormSucceed();");
           }
           else 
           {
               return JavaScript("editFormError();");
           }
        }

        public ActionResult EditTerUser(UserInfo ui)
        {
            UserInfo user = userInfoBll.GetTerUserInfo(ui);
            DeptInfo di = deptInfoBll.GetDeptInfoByCode(user.UserBuildcode);
            ViewBag.EnterId = di.Businessdivisionid;
            return View(user);
        }

        [Log(LogMessage = "设备用户修改")]
        public ActionResult EditTerUserForm(UserInfo ui, string oldname,string userters)
        {
            if (ui.UserLname != oldname)
            {
                UserInfoBLL userbll = new UserInfoBLL();
                ui.UserLname = ui.UserLname.Trim();
                UserInfo uiObj = userbll.GetUserByLName(ui, out uiObj);
                if (uiObj != null)
                {
                    return JavaScript("editFormError('该用户名已经存在');");
                }
            }
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            string[] terno =userters.Trim().Split(',');
            string terstr = "";
            foreach (string ter in terno)
            {
                terstr += ter + "&";
            }
            int k = userInfoBll.Update(ui);

            new LogMessage().Save("ID:" + ui.UserId + "。");

            if (k > 0)
            {
                //修改SETMANAGER表的指令
                Transfers.ClintSendCommData(1108, "1", "6", ui.UserLname, "", "", "", "", "", "", "", terstr, "", "", "", "", "", "",user.UserName);
                //刷新用户
                Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "1", "", "", "", "", "", "");
                System.Threading.Thread.Sleep(2000);
                return JavaScript("editFormSucceed();");
            }
            else
            {
                return JavaScript("editFormError();");
            }
        }
    }
}