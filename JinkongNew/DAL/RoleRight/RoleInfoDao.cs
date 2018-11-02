// Generated by IBatisNetModel

using System;

using GInterfaceDAL.RoleRight;
using System.Collections.Generic;
using GModel.RoleRight;
using GModel.Basic;
using IBatisNet.DataMapper;

namespace GDAL.RoleRight
{
    public class RoleInfoDao : BaseSqlMapDao, IRoleInfoDao
    {
		 public object Insert(RoleInfo entity)
        {
            return ExecuteInsert("RoleInfo.InsertRoleInfo", entity);
        }

        public int Update(RoleInfo entity)
        {
            return ExecuteUpdate("RoleInfo.UpdateRoleInfo", entity);
        }

		public int Delete(object condition)
        {
            ISqlMapper NewMap = SqlMapper.Instance();
            NewMap.BeginTransaction();
            try
            {
               object o = ExecuteQueryForObjectTrans("UserInfo.SelectUserInfoCount", condition,NewMap);
               if (Convert.ToInt32(o) > 0)
               {
                   NewMap.CommitTransaction();
                   return -2;
               }
               else 
               {
                   ExecuteUpdateTrans("RoleInfo.DeleteRoleInfo", condition,NewMap);
                   NewMap.CommitTransaction();
                   return 1;
               }
            }
            catch (Exception e)
            {
                NewMap.RollBackTransaction();
                return -1;
            }
        }

        public RoleInfo GetRoleInfo(object userinfoId)
        {
            return (RoleInfo)ExecuteQueryForObject("RoleInfo.SelectRoleInfo", userinfoId);
        }

        public IList<RoleView> GetRoleInfoPage(object o) 
        {
            return ExecuteQueryForList<RoleView>("RoleInfo.SelectRoleInfoPage", o);
        }

        public IList<RoleView> GetRoleInfoDownList(string DeptId) 
        {
            return ExecuteQueryForList<RoleView>("RoleInfo.SelectRoleInfoDownList", DeptId);
        }

        public int GetRoleInfoCount(RoleView o) 
        {
            object count = ExecuteQueryForObject("RoleInfo.SelectRoleInfoCount", o);
            return (int)count;
        }

        public RoleInfo GetRoleObjByRoleId(string RoleId)
        {
            return (RoleInfo)ExecuteQueryForObject("RoleInfo.SelectRoleInfo", RoleId);      
        }
    }
}
	