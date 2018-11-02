using GModel.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GModel.RoleRight;

namespace GModel
{
    public class TreeModeCheck
    {
        //private bool Checked;
        public string id { get; set; }

        public string text { get; set; }
        //checked是C#关键字，要加@
        public bool @checked
        {
            get;
            set;
        }

        public string RmId { get; set; }
        public List<TreeModeCheck> children { get; set; }

        //public string icon { get; set; }
        //public string url { get; set; }
        private TreeModeCheck()
        {
            children = new List<TreeModeCheck>();
        }
        public TreeModeCheck(string isTree) { }
        public static TreeModeCheck CreateRole(MenuInfo node)
        {
            TreeModeCheck treeNode = new TreeModeCheck
            {
                id = node.MenuId,
                text = node.MenuName,
                RmId=node.RmId
            };
            if (treeNode.RmId != null && treeNode.RmId.Trim() != "")
            {
                treeNode.@checked = true;
            }
            else 
            { 
                treeNode.@checked = false; 
            }
            if (node.MenuListSub != null)
            {
                foreach (var item in node.MenuListSub)
                {
                    treeNode.children.Add(TreeModeCheck.CreateRole(item));
                }
            }
            return treeNode;
        }
       
    }
}