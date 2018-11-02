using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GModel;
using GModel.Basic;
using GBLL.Basic;
using SuperGPS.App_Start;
using GBLL;
using GModel.RoleRight;

namespace SuperGPS.Controllers
{
    public class DeptController : Controller
    {
        DeptInfoViewBLL deptInfoViewBll = new DeptInfoViewBLL();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();

        // GET: Dept
        public ActionResult Index()
        {
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];

            ViewBag.AddDept = "false";
            ViewBag.EditDept = "false";
            ViewBag.DelDept = "false";

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
                }
            }

            return View();
        }

        //企业树
        [UserFilter]
        public ActionResult GetOrgNode(string isTree, string DeptId)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            List<TreeMode> TreeNode = new List<TreeMode>();
            if (user.EnterId != null)
            {
                string cache_name = "CurUserTreeModeList_" + user.EnterId;

                if (CacheHelper.Get(cache_name) != null)
                {
                    TreeNode = (List<TreeMode>)CacheHelper.Get(cache_name);
                }
                else
                {
                    TreeNode = deptInfoViewBll.GetDepartmentListByUser2(user, DeptId);
                }
            }
            else
            {
                TreeNode = deptInfoViewBll.GetDepartmentListByUser2(user, DeptId);
            }

            return Json(TreeNode, JsonRequestBehavior.AllowGet); 
        }

        //结清企业树
        [UserFilter]
        public ActionResult GetGroupNode(string fatherid)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            List<TreeMode> TreeNode = new List<TreeMode>();
            string DeptId = fatherid;
            if (fatherid == "" || fatherid==null)
            {
                DeptId = user.EnterId;
            }
            TreeNode = deptInfoViewBll.GetGroupDeptList(DeptId);
            
            return Json(TreeNode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加机构窗口
        /// </summary>
        /// <param name="FatherNodeId"></param>
        /// <returns></returns>
        public ActionResult ANode(string FatherNodeId,string DepType)
        {
            //ViewData["FatherID"] = FatherNodeId;
            //ViewBag.FatherID = FatherNodeId;
            if (FatherNodeId != null)
            {
                ViewBag.FatherID = FatherNodeId;
            }
            else
            {
                ViewBag.FatherID = "";
            }
            if (DepType != null)
            {
                ViewBag.DepType = DepType;
            }
            else
            {
                ViewBag.DepType = "";
            }
            return View();
        }

        [Log(LogMessage = "组织机构添加")]
        public string AddEnter(DeptInfo deptInfo)
        {
            try
            {
                object o = deptInfoBll.Insert(deptInfo);

                //刷新缓存
                UserInfo user = new UserInfo();
                user = (UserInfo)Session["LoginUser"];

                new LogMessage().Save("名称：" + deptInfo.Businessdivisionname + "。");

                string cache_name = "CurUserTreeModeList_" + user.EnterId;
                if (CacheHelper.Get(cache_name) != null)
                {
                    CacheHelper.Remove(cache_name);
                }

                //刷新部门
                Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "0", "", "", "", "", "", "");
                return o.ToString();
            }
            catch (Exception)
            {
                return "false";
            }
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="Businessdivisionid"></param>
        /// <returns></returns>
        [Log(LogMessage = "组织机构删除")]
        public int DelEnter(string Businessdivisionid, string ExistChildren)
        {
            int aa = 0;
            if (ExistChildren == "false")
            {
                aa = deptInfoBll.Delete(Businessdivisionid);
            }
            else if (ExistChildren == "true")
            {
                aa = deptInfoBll.DeleteChildren(Businessdivisionid);
            }

            new LogMessage().Save("ID：" + Businessdivisionid + "。");

            //刷新缓存
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            string cache_name = "CurUserTreeModeList_" + user.EnterId;
            if (CacheHelper.Get(cache_name) != null)
            {
                CacheHelper.Remove(cache_name);
            }

            //刷新部门
            Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "0", "", "", "", "", "", "");
            return aa;
        }

        //编辑企业窗口
        [UserFilter]
        public ActionResult ENode(string DeptId, string DepType)
        {
            DeptInfo df = deptInfoBll.GetDeptInfo(DeptId);
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (DeptId.Equals(user.EnterId))
            {
                df.Fatherid = user.EnterId;
            }
            if (DepType != null)
            {
                ViewBag.DepType = DepType;
            }
            else
            {
                ViewBag.DepType = "";
            }
            return View(df);
        }

        //组织机构修改
        [Log(LogMessage = "组织机构修改")]
        public ActionResult EditNode(DeptInfo Dept)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];

            if (Dept.Businessdivisionid == user.EnterId) //"52c96c61-8532-4ace-ab6e-2be214289280"
            {
                ModelState.AddModelError("", "修改失败");
                return JavaScript("editFormError('不能修改根企业');");
            }
            if (Dept.Fatherid == "" || Dept.Fatherid == null)
            {
                ModelState.AddModelError("", "修改失败");
                return JavaScript("editFormError('上级企业不能为空');");
            }
            else
            {
                DeptInfo di= deptInfoBll.GetDeptInfo(Dept.Fatherid);
                if (di.Businessdivisioncode.StartsWith(Dept.Businessdivisioncode))
                {
                    ModelState.AddModelError("", "修改失败");
                    return JavaScript("editFormError('所选企业不能把自己的子企业修改为父企业');");
                }
            }

            if (string.IsNullOrEmpty(Dept.Businessdivisionname))
            {
                ModelState.AddModelError("", "修改失败");
                return JavaScript("editFormError('企业名称不能为空');");
            }
            else
            {
                List<string> OldDeptCode=new List<string>();
                List<string> NewDeptCode=new List<string>();
                int k = deptInfoBll.Update(Dept, OldDeptCode, NewDeptCode);
                if (k > 0)
                {
                    UpdateState();

                    new LogMessage().Save("ID：" + Dept.Businessdivisionid + "。");

                    string cache_name = "CurUserTreeModeList_" + user.EnterId;
                    if (CacheHelper.Get(cache_name) != null)
                    {
                        CacheHelper.Remove(cache_name);
                    }
                    if (OldDeptCode.Count > 0 && NewDeptCode.Count>0)
                    {
                        for (int i = 0; i < OldDeptCode.Count;i++)
                        {
                            //部门流转修改终端部门
                            Transfers.ClintSendCommData(1108, "0", "3", "", "", "", "", "", "", "", "", OldDeptCode[i], NewDeptCode[i], "", "", "", "", "",user.UserName);
                        }
                    }
                    //刷新部门
                    Transfers.ClintSendCommData(1160, "1108", "", "", "", "", "", "", "", "", "", "1", "0", "", "", "", "", "", "");
                    ModelState.AddModelError("", "修改成功");
                    return JavaScript("editFormSucceed();");
                }
                else
                {
                    ModelState.AddModelError("", "修改失败");
                    return JavaScript("editFormError();");
                }
            }
        }

        public void UpdateState()
        {

            ColligateQueryService c = new ColligateQueryService();

            string sql1 = "UPDATE CAR_INFO A SET A.CAR_DEPTCODE=(SELECT B.BUSINESSDIVISIONCODE FROM dept_info B WHERE A.BUSINESSDIVISIONID = B.BUSINESSDIVISIONID) WHERE EXISTS(SELECT 1 FROM dept_info C WHERE C.BUSINESSDIVISIONID=A.BUSINESSDIVISIONID)";
            //int num1 = c.UpdateQuery("ColligateQuery.ProteanUpdate", sql1);
            string sql2 = "UPDATE User_info A SET A.user_deptcode=(SELECT B.BUSINESSDIVISIONCODE FROM dept_info B WHERE A.Enter_id = B.BUSINESSDIVISIONID) WHERE EXISTS(SELECT 1 FROM dept_info C WHERE C.BUSINESSDIVISIONID=A.Enter_id)";
            //int num2 = c.UpdateQuery("ColligateQuery.ProteanUpdate", sql2);
            string sql3 = "UPDATE terminal_info A SET A.ter_deptcode=(SELECT B.BUSINESSDIVISIONCODE FROM dept_info B WHERE A.dept_id = B.BUSINESSDIVISIONID) WHERE EXISTS(SELECT 1 FROM dept_info C WHERE C.BUSINESSDIVISIONID=A.dept_id)";
            //int num3 = c.UpdateQuery("ColligateQuery.ProteanUpdate", sql3);
            //string sql4 = "UPDATE terminal_info A SET A.ter_carno=(SELECT B.car_no FROM car_info B WHERE A.car_id = B.CAR_ID) WHERE EXISTS(SELECT 1 FROM car_info C WHERE C.car_id=A.car_id)";
            //int num4 = c.UpdateQuery("ColligateQuery.ProteanUpdate", sql4);
            string sqlsum = "begin "+sql1 +";"+ sql2 +";"+ sql3+";"+" commit; end;";
            int num0 = c.UpdateQuery("ColligateQuery.ProteanUpdate", sqlsum);

        }

        public int GetDeptByFatherId(string DeptId)
        {
            try
            {
                return deptInfoBll.GetDeptCountByFatherId(DeptId);
            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}