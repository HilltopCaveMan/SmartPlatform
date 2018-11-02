using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using GBLL.Car;
using GModel.Car;
using GBLL;
using System.IO;
using System.Text;
using System.Data;
using System.Collections;
using GModel.RoleRight;
using GModel.Basic;
using GBLL.Basic;
using SuperGPS.App_Start;

namespace SuperGPS.Controllers
{
    public class CarTypeController : BaseController
    {
        CarTypeBLL carTypeBll = new CarTypeBLL();
        DeptInfoBLL deptInfoBll = new DeptInfoBLL();

        // GET: CarType
        //[OutputCache(CacheProfile = "ActionCacheProfile")]
        [UserFilter]
        public ActionResult CarTypeIndex(CarTypeView ct)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            IList<MenuInfo> imi = (IList<MenuInfo>)Session["Right"];
            ViewBag.AddCarType = "false";
            //ViewBag.EditDept = "false";
            ViewBag.DelCarType = "false";
            for (int i = 0; i < imi.Count; i++)
            {
                switch (imi[i].MenuName)
                {
                    case "添加车辆类型":
                        ViewBag.AddCarType = "true";
                        break;
                    case "删除车辆类型":
                        ViewBag.DelCarType = "true";
                        break;
                }
            }
            ct.Businessdivisionid = user.EnterId;
            IList<CarTypeView> ict = carTypeBll.GetCarTypePage(ct);
            return View(ict);
        }
        public ActionResult AddCarType() 
        {
            return View();
        }

        [Log(LogMessage = "车辆类型添加")]
        [HttpPost]
        public ActionResult CarTypeForm(CarType ct, HttpPostedFileBase file) 
        {
            if (ct != null)
            {
                if (ct.TypeName == null || ct.TypeName.Trim() == "") 
                {
                    ViewBag.Result= "类型名称不能为空！";
                }
                else if (ct.DeptId == null || ct.DeptId.Trim() == "")
                {
                    ViewBag.Result = "所属企业不能为空！";
                }
                else
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        byte[] bytes = new byte[file.ContentLength];
                        using (BinaryReader reader = new BinaryReader(file.InputStream, Encoding.UTF8))
                        {
                            bytes = reader.ReadBytes(file.ContentLength);
                        }
                        ct.TypePicture = bytes;
                        ct.TypePictype = file.ContentType;
                    }

                    new LogMessage().Save("名称：" + ct.TypeName + "。");

                    object i = carTypeBll.Insert(ct);

                    if (i == null)
                    {
                        //更新缓存
                        #region
                        this.RefreshCache(ct.DeptId);
                        #endregion

                        ViewBag.Result = "true";
                    }
                    else
                    {
                        ViewBag.Result = "false";
                    }                
                }
            }
            else 
            {
                ViewBag.Result = "false";
            }
           return View();
        }

        public FileContentResult GetCarTypeImage(CarType carType)
        {
            ArrayList al = carTypeBll.getCarTypeImage(carType);
            if (al != null && al.Count > 0)
            {
                Hashtable ht = (Hashtable)al[0];
                byte[] b = (byte[])(ht["Picture"]);
                if (b != null)
                {
                    return new FileContentResult(b, ht["PicType"].ToString());
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        [UserFilter]
        public string GetCarTypeList(string TypeMode, string DeptId)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                DeptInfo di = deptInfoBll.GetDeptInfo(user.EnterId);
                CarTypeList ct = new CarTypeList();
                if (DeptId != null && DeptId.Trim() != "")
                {
                    ct.Businessdivisionid = DeptId;
                }
                else
                {
                    ct.Businessdivisioncode = di.Businessdivisioncode;
                }

                IList<CarTypeList> ictlist = carTypeBll.GetCarTypeList(ct);
                if (TypeMode == "true")
                {
                    CarTypeList c = new CarTypeList();
                    c.TypeId = "";
                    c.TypeName = "--选择车辆类型--";
                    ictlist.Insert(0, c);
                }
                string json = ConvertToJson(ictlist);
                return json;
            }
            else
            {
                return "";
            }
        }

        /*
        [UserFilter]
        public string GetCarTypeList(string TypeMode,string DeptId) 
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            IList<CarTypeList> ictlist = null;

            if (user != null)
            {
                string cache_name = "CurUserCarTypeList_" + user.EnterId;

                if (CacheHelper.Get(cache_name) != null)
                {
                    ictlist = (IList<CarTypeList>)CacheHelper.Get(cache_name);
                }
                else
                {
                    DeptInfo di = deptInfoBll.GetDeptInfo(user.EnterId);

                    string deptid = di.Businessdivisionid;
                    if (DeptId != null && DeptId.Trim() != "")
                    {
                        deptid = DeptId;
                    }

                    ictlist = this.RefreshCache(deptid);
                }

                IList<CarTypeList> ret_list = new List<CarTypeList>();

                if (TypeMode == "true")
                {
                    CarTypeList c = new CarTypeList();
                    c.TypeId = "";
                    c.TypeName = "--选择车辆类型--";
                    ret_list.Insert(0, c);
                }

                if(ictlist!=null
                    && ictlist.Count>0)
                {
                    foreach (CarTypeList ctl in ictlist)
                    {
                        ret_list.Add(ctl);
                    }
                }

                return ConvertToJson(ret_list); 
            }
            else {
                return "";
            }                        
        }
        */

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [Log(LogMessage = "车辆类型删除")]
        public string DelCarType(string TypeId)
        {
            return carTypeBll.Delete(TypeId);
        }

        private IList<CarTypeList> RefreshCache(string deptid)
        {
            UserInfo user = new UserInfo();
            user = (UserInfo)Session["LoginUser"];
            IList<CarTypeList> ictlist = null;

            string cache_name = "CurUserCarTypeList_" + user.EnterId;

            CarTypeList ct = new CarTypeList();

            ct.Businessdivisionid = deptid;

            ictlist = carTypeBll.GetCarTypeList(ct);

            CacheHelper.Remove(cache_name);
            CacheHelper.Insert(cache_name, ictlist, 365 * 24 * 60);

            return ictlist;
        }

    }
}