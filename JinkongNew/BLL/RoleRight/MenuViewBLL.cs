// Generated by IBatisNetModel

using System;
using System.Collections;

using GModel.RoleRight;
using GInterfaceDAL.RoleRight;
using GDAL.RoleRight;
using System.Collections.Generic;
using GModel.Basic;

namespace GBLL.RoleRight
{
    public class MenuViewBLL
    {
        private IMenuViewDao _iMenuViewDao = null;
        public MenuViewBLL()
        {
            _iMenuViewDao = new MenuViewDao();
        }

		public MenuView GetMenuView(object o)
        {
            return _iMenuViewDao.GetMenuView( o);
        }

        public IList<MenuView> GetMenuViewPage(object o)
        {
            return _iMenuViewDao.GetMenuViewPage(o);
        }

        public IList<MenuPower> GetMenuPowerTree(object o) 
        {
            return _iMenuViewDao.GetMenuPower(o);
        }

		public int GetMenuViewCount(object o)
        {
            return _iMenuViewDao.GetMenuViewCount(o);
        }
        public List<MenuInfo> GetMenuViewListByUser(UserInfo LoginUser)
        {
            List<MenuView> DepList = new List<MenuView>();
            List<MenuInfo> DepListAll = new List<MenuInfo>();
            List<MenuInfo> TreeLists = new List<MenuInfo>();
            List<MenuView> TreeListAll = new List<MenuView>();
            MenuView d = new MenuView();
            DepList = (List<MenuView>)GetMenuViewPage(d);
            //if (LoginUser.UserId == "0")
            //{
                if (DepList != null)
                {
                    foreach (MenuView div in DepList)
                    {
                        MenuInfo DepObj = new MenuInfo();
                        DepObj.MenuId = div.MenuId;
                        DepObj.MenuName = div.MenuName;
                        DepObj.MenuParent = div.MenuParent;
                        DepObj.MenuUrl = div.MenuUrl;
                        MenuInfo fa = new MenuInfo();
                        fa.MenuId = div.MenuParent;
                        fa.MenuName = div.Fmenuname;
                        DepObj.FatherMenuObj = fa;
                        DepListAll.Add(DepObj);
                    }
                    TreeLists = DepListAll.FindAll(x => x.MenuParent == null || x.MenuParent.Trim() == "");

                    foreach (MenuInfo deptInfo in TreeLists)
                    {
                        deptInfo.MenuListSub = CreateMenu(deptInfo, DepListAll);
                    }
                }
            //}
            return TreeLists;
        }

        private List<MenuInfo> CreateMenu(MenuInfo DepartmentObj, List<MenuInfo> DepartmentListTotal)
        {
            List<MenuInfo> lists = DepartmentListTotal.FindAll(x => x.FatherMenuObj.MenuId == DepartmentObj.MenuId);
            if (lists != null)
            {
                foreach (MenuInfo DepartmentObjTmp in lists)
                {
                    DepartmentObjTmp.MenuListSub = new List<MenuInfo>();
                    List<MenuInfo> ListTmp = CreateMenu(DepartmentObjTmp, DepartmentListTotal);
                    if (ListTmp != null)
                    {
                        DepartmentObjTmp.MenuListSub = ListTmp;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return lists;
        }

        public List<MenuInfo> GetMenuPowerListByRoleId(Hashtable ht)
        {
            List<MenuPower> DepList = new List<MenuPower>();
            List<MenuInfo> DepListAll = new List<MenuInfo>();
            List<MenuInfo> TreeLists = new List<MenuInfo>();
            List<MenuPower> TreeListAll = new List<MenuPower>();
            //MenuView d = new MenuView();
            DepList = (List<MenuPower>)GetMenuPowerTree(ht);
            //if (LoginUser.UserId == "0")
            //{
            if (DepList != null)
            {
                foreach (MenuPower div in DepList)
                {
                    MenuInfo DepObj = new MenuInfo();
                    DepObj.MenuId = div.MenuId;
                    DepObj.MenuName = div.MenuName;
                    DepObj.MenuParent = div.MenuParent;
                    DepObj.MenuUrl = div.MenuUrl;
                    DepObj.RmId = div.RmId;
                    MenuInfo fa = new MenuInfo();
                    fa.MenuId = div.MenuParent;
                    fa.MenuName = div.Fmenuname;
                    DepObj.FatherMenuObj = fa;
                    DepListAll.Add(DepObj);
                }
                TreeLists = DepListAll.FindAll(x => x.MenuParent == null || x.MenuParent.Trim() == "");

                foreach (MenuInfo deptInfo in TreeLists)
                {
                    deptInfo.MenuListSub = CreateMenu(deptInfo, DepListAll);
                }
            }
            //}
            return TreeLists;
        }






        
    }
}
	