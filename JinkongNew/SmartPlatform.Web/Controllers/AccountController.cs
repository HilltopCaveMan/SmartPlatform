using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Owin.Security;

using GModel;
using GModel.Basic;
using GBLL.Basic;
using GBLL.RoleRight;
using GModel.RoleRight;
using SuperGPS.App_Start;

namespace SuperGPS.Controllers
{
    public class AccountController : Controller
    {

        UserInfoBLL userInfoBll = null;
        MenuInfoBLL menuInfoBll = new MenuInfoBLL();

        public AccountController()
        {
            userInfoBll = new UserInfoBLL();
        }

        [Log(LogMessage = "启动用户登陆")]
        [IsLoginFilter]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //第二登录界面
        [IsLoginFilter]
        [HttpGet]
        public ActionResult GXTLogin()
        {
            return View();
        }

        [Log(LogMessage = "用户登陆")]
        [HttpPost]
        public ActionResult Login(UserInfo user)
        {
            UserInfo UserObj = new UserInfo();
            if (user.UserLname != null && user.UserPasswrd != null)
            {
                user.UserLname = user.UserLname.Trim();
                user.UserPasswrd = user.UserPasswrd.Trim();
                user.UserPasswrd = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UserPasswrd, "md5");

                ResultState result = userInfoBll.UserLogin(user, out UserObj);

                if (result == ResultState.SUCCEED)
                {
                    if (UserObj != null)
                    {
                        Session["LoginUser"] = UserObj;
                        Session["UserName"] = UserObj.UserLname;

                        IList<MenuInfo> imi = menuInfoBll.GetMenuByRoleId(UserObj.RoleId);
                        Session["Right"] = imi;

                        FormsAuthentication.SetAuthCookie(UserObj.UserLname, false);

                        new LogMessage().Save("登陆成功;");

                        //更新用户目录树缓存
                        string cache_name = "CurUserTreeModeList_" + UserObj.EnterId;
                        if (CacheHelper.Get(cache_name) != null)
                        {
                            CacheHelper.Remove(cache_name);
                        }

                        return RedirectToAction("BDMonitorIndex_511", "CarMonitor");
                    }
                }
                else if (result == ResultState.LNAMEERROR)
                {
                    new LogMessage().Save("用户名错误;");
                    ModelState.AddModelError("", "用户名错误");
                }
                else if (result == ResultState.PWDERROR)
                {
                    new LogMessage().Save("密码错误;");
                    ModelState.AddModelError("", "密码错误");
                }
                else
                {
                    new LogMessage().Save("登陆失败;");
                    ModelState.AddModelError("", "登陆失败");
                }

                return View();
            }
            else
            {
                return View();
            }
        }

        [Log(LogMessage = "退出用户登陆")]
        [HttpPost]
        public ActionResult UserCance()
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            new LogMessage().Save("用户：" + user.UserLname + "退出登陆;");

            Session["LoginUser"] = null;
            Session["Right"] = null;
            Session["UserName"] = null;

            FormsAuthentication.SignOut();

            return Json("");
        }

        ///密码修改
        [Log(LogMessage = "启动用户密码")]
        [UserFilter]
        public ActionResult EditPWD()
        {
            UserInfo userObj = new UserInfo();
            userObj = (UserInfo)Session["LoginUser"];

            UserInfo user = new UserInfo();
            user.UserLname = userObj.UserLname;

            return View(user);
        }

        [Log(LogMessage = "修改用户密码")]
        [HttpPost]
        public ActionResult EditPWD(UserInfo clsUserInfo)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            if (FormsAuthentication.HashPasswordForStoringInConfigFile(clsUserInfo.OldPasswrd.Trim(), "md5") != user.UserPasswrd)
            {
                return JavaScript("editFormError('修改失败,旧密码不正确！');");
            }
            else if (clsUserInfo.UserPasswrd.Trim() != clsUserInfo.UserRePasswrd.Trim())
            {
                return JavaScript("editFormError('修改失败,新密码不一致！');");
            }
            else
            {
                UserInfo Userobj = new UserInfo();

                Userobj.UserPasswrd = FormsAuthentication.HashPasswordForStoringInConfigFile(clsUserInfo.UserPasswrd, "md5");
                Userobj.UserLname = clsUserInfo.UserLname;
                ResultState result = userInfoBll.ModifyUserPWD(Userobj);

                new LogMessage().Save("用户：" + user.UserLname + "修改密码;");

                switch (result)
                {
                    case ResultState.SUCCEED:
                        //刷新用户
                        Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "1", "", "", "", "", "", "");
                        return JavaScript("editFormSucceed();");
                    case ResultState.ERROR:
                        return JavaScript("editFormError('');");
                    case ResultState.EXIST:
                        return JavaScript("editFormExist();");
                    default:
                        return JavaScript("editFormSucceed();");
                }
            }
        }
    }
}