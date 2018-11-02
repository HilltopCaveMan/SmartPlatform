using GModel.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GModel.RoleRight;

namespace GModel
{
    public class TreeMode
    {
        public string id { get; set; }

        public string text { get; set; }

        public string code { get; set; }
        public List<TreeMode> children { get; set; }

        public string icon { get; set; }
        public string url { get; set; }
        private TreeMode()
        {
            children = new List<TreeMode>();
        }
        public TreeMode(string isTree) { }

        public static TreeMode CreateMenu(MenuInfo node)
        {
            TreeMode treeNode = new TreeMode
            {
                id = node.MenuId,
                text = node.MenuName,
                icon = node.MenuIcon,
                url = node.MenuUrl
            };
            if (node.MenuListSub != null)
            {

                foreach (var item in node.MenuListSub)
                {
                    treeNode.children.Add(TreeMode.CreateMenu(item));
                }
            }
            return treeNode;
        }
        public static TreeMode CreateDepartment(DeptInfo node)
        {
            TreeMode treeNode = new TreeMode
            {
                id = node.Businessdivisionid,
                text = node.Businessdivisionname,
                code=node.Businessdivisioncode
            };
            if (node.ListDepartmentSub != null)
            {
                foreach (var item in node.ListDepartmentSub)
                {
                    treeNode.children.Add(TreeMode.CreateDepartment(item));
                }
            }
            return treeNode;
        }
        
        public static TreeMode CreateRole(RoleInfo node)
        {
            TreeMode treeNode = new TreeMode
            {
                id = node.RoleId,
                text = node.RoleName
            };
            if (node.ListRoleSub != null)
            {

                foreach (var item in node.ListRoleSub)
                {
                    treeNode.children.Add(TreeMode.CreateRole(item));
                }
            }
            return treeNode;
        }
       
    }
}