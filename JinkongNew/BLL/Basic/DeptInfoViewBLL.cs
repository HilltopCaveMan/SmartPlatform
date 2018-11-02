// Generated by IBatisNetModel

using System;
using System.Collections;

using GModel.Basic;
using GInterfaceDAL.Basic;
using GDAL.Basic;
using System.Collections.Generic;
using GModel.Car;
using GBLL.Car;

namespace GBLL.Basic
{
    public class DeptInfoViewBLL
    {
        private IDeptInfoViewDao _iDeptInfoViewDao = null;
        private IDeptInfoDao _iDeptInfoDao = null;
        public DeptInfoViewBLL()
        {
            _iDeptInfoViewDao = new DeptInfoViewDao();
            _iDeptInfoDao = new DeptInfoDao();
        }

        public DeptInfoView GetDeptInfoView(object o)
        {
            return _iDeptInfoViewDao.GetDeptInfoView(o);
        }

        public IList<DeptInfoView> GetDeptInfoViewPage(object o)
        {
            return _iDeptInfoViewDao.GetDeptInfoViewPage(o);
        }

        public int GetDeptInfoViewCount(object o)
        {
            return _iDeptInfoViewDao.GetDeptInfoViewCount(o);
        }

        public IList<DeptInfoView> GetGroupDeptInfoPage(object o)
        {
            return _iDeptInfoViewDao.GetGroupDeptInfoPage(o);
        }

        public int GetGroupDeptInfoCount(object o)
        {
            return _iDeptInfoViewDao.GetGroupDeptInfoCount(o);
        }

        /// <summary>
        /// ������λ��Ŀ¼������
        /// </summary>
        /// <param name="LoginUser"></param>
        /// <returns></returns>
        public List<GModel.TreeMode> GetDepartmentListByUser2(UserInfo LoginUser,string DeptId)
        {
            List<GModel.TreeMode> TreeNode = new List<GModel.TreeMode>();

            GModel.TreeMode tm = new GModel.TreeMode("false");
            tm.id = "";
            tm.text = "---ѡ����ҵ---";
            tm.code = "0";
            TreeNode.Add(tm);

            List<DeptInfo> dep = new List<DeptInfo>();
            
            if (LoginUser != null)
            {
                dep = this.GetDepartmentListByUser(LoginUser, DeptId);
            }
            
            if (dep != null)
            {
                foreach (DeptInfo DepartmentObj in dep)
                {
                    TreeNode.Add(GModel.TreeMode.CreateDepartment(DepartmentObj));
                }
            }

            //���»���
            string cache_name = "CurUserTreeModeList_" + LoginUser.EnterId;
            if (CacheHelper.Get(cache_name) != null)
            {
                CacheHelper.Remove(cache_name);
            }
            CacheHelper.Insert(cache_name, TreeNode, 365 * 24 * 60);

            return TreeNode;
        }

        public List<GModel.TreeMode> GetGroupDeptList(string DeptId)
        {
            List<GModel.TreeMode> TreeNode = new List<GModel.TreeMode>();

            GModel.TreeMode tm = new GModel.TreeMode("false");
            tm.id = "";
            tm.text = "���岿��";
            tm.code = "0";
            TreeNode.Add(tm);

            List<DeptInfo> dep = new List<DeptInfo>();

            if (DeptId != null)
            {
                dep = this.GetGroupDeptList2(DeptId);
            }

            if (dep != null)
            {
                foreach (DeptInfo DepartmentObj in dep)
                {
                    TreeNode.Add(GModel.TreeMode.CreateDepartment(DepartmentObj));
                }
            }
            return TreeNode;
        }

        public List<DeptInfo> GetDepartmentListByUser(UserInfo LoginUser, string DeptId)
        {
            List<DeptInfoView> DepList = new List<DeptInfoView>();
            List<DeptInfo> DepListAll = new List<DeptInfo>();
            List<DeptInfo> TreeLists = new List<DeptInfo>();
            List<DeptInfoView> TreeListAll = new List<DeptInfoView>();

            if (LoginUser.UserDeptcode != null)
            {
                DeptInfo di = _iDeptInfoDao.GetDeptInfoById(LoginUser.EnterId);
                DeptInfoView d = new DeptInfoView();
                d.Isdel = "0";
                d.Businessdivisioncode = di.Businessdivisioncode;
                DepList = (List<DeptInfoView>)GetDeptInfoViewPage(d);

                if (DepList != null)
                {
                    foreach (DeptInfoView div in DepList)
                    {
                        DeptInfo DepObj = new DeptInfo();
                        DepObj.Businessdivisionid = div.Businessdivisionid;
                        DepObj.Businessdivisionname = div.Businessdivisionname;
                        DepObj.Businessdivisioncode = div.Businessdivisioncode;
                        DepObj.Fatherid = div.Fatherid;
                        DeptInfo fa = new DeptInfo();
                        fa.Businessdivisionid = div.Fatherid;
                        fa.Businessdivisionname = div.Fbusinessdivisionname;
                        fa.Businessdivisioncode = div.Fbusinessdivisioncode;
                        DepObj.FatherDepartmentObj = fa;
                        DepListAll.Add(DepObj);
                    }
                    TreeLists = DepListAll.FindAll(x => x.Businessdivisioncode == di.Businessdivisioncode);

                    foreach (DeptInfo deptInfo in TreeLists)
                    {
                        deptInfo.ListDepartmentSub = CreateDepartment(deptInfo, DepListAll);
                    }
                }
                return TreeLists;

            }
            else
            {
                if (DeptId != null)
                {
                    DeptInfo di = _iDeptInfoDao.GetDeptInfoById(DeptId);
                    TreeLists.Add(di);
                    return TreeLists;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<DeptInfo> GetGroupDeptList2(string DeptId)
        {
            List<DeptInfoView> DepList = new List<DeptInfoView>();
            List<DeptInfo> DepListAll = new List<DeptInfo>();
            List<DeptInfo> TreeLists = new List<DeptInfo>();
            List<DeptInfoView> TreeListAll = new List<DeptInfoView>();
            TerminalInfoBLL terbll = new TerminalInfoBLL();
            if (DeptId != null)
            {
                DeptInfo di = _iDeptInfoDao.GetDeptInfoById(DeptId);
                DeptInfoView d = new DeptInfoView();
                d.Isdel = "0";
                d.DepType = "1";
                d.Fatherid = DeptId;
                d.Businessdivisioncode = di.Businessdivisioncode;
                DepList = (List<DeptInfoView>)GetGroupDeptInfoPage(d);

                if (DepList != null)
                {
                    foreach (DeptInfoView div in DepList)
                    {
                        DeptInfo DepObj = new DeptInfo();
                        DepObj.Businessdivisionid = div.Businessdivisionid;
                        DepObj.Businessdivisionname = div.Businessdivisionname;
                        DepObj.Businessdivisioncode = div.Businessdivisioncode;
                        DepObj.Fatherid = div.Fatherid;
                        DeptInfo fa = new DeptInfo();
                        fa.Businessdivisionid = div.Fatherid;
                        fa.Businessdivisionname = div.Fbusinessdivisionname;
                        fa.Businessdivisioncode = div.Fbusinessdivisioncode;
                        DepObj.FatherDepartmentObj = fa;
                        DepListAll.Add(DepObj);
                    }
                    TreeLists = DepListAll.FindAll(x => x.Fatherid == di.Businessdivisionid);

                    foreach (DeptInfo deptInfo in TreeLists)
                    {
                        List<DeptInfo> deplist = new List<DeptInfo>();
                        IList<TerminalInfo> terlist = terbll.GetTerminalInfoByDeptId(deptInfo.Businessdivisionid);
                        if (terlist != null)
                        {
                            foreach (TerminalInfo terinfo in terlist)
                            {
                                DeptInfo TerDep = new DeptInfo();
                                TerDep.Businessdivisionid = terinfo.DeptId;
                                TerDep.Businessdivisionname = terinfo.TerNo;
                                TerDep.Businessdivisioncode = terinfo.TerDeptcode;
                                deplist.Add(TerDep);
                            }
                        }
                        deptInfo.ListDepartmentSub = deplist;
                    }
                }
                return TreeLists;

            }
            else
            {
                return null;
            }
        }

        private List<DeptInfo> CreateDepartment(DeptInfo DepartmentObj, List<DeptInfo> DepartmentListTotal)
        {
            List<DeptInfo> lists = DepartmentListTotal.FindAll(x => x.FatherDepartmentObj.Businessdivisionid == DepartmentObj.Businessdivisionid);
            if (lists != null)
            {
                foreach (DeptInfo DepartmentObjTmp in lists)
                {
                    DepartmentObjTmp.ListDepartmentSub = new List<DeptInfo>();
                    List<DeptInfo> ListTmp = CreateDepartment(DepartmentObjTmp, DepartmentListTotal);
                    if (ListTmp != null)
                    {
                        DepartmentObjTmp.ListDepartmentSub = ListTmp;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return lists;
        }
    }
}