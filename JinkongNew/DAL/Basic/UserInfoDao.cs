// Generated by IBatisNetModel

using System;

using GInterfaceDAL.Basic;
using System.Collections.Generic;
using GModel.Basic;
using IBatisNet.DataMapper;

namespace GDAL.Basic
{
    public class UserInfoDao : BaseSqlMapDao, IUserInfoDao
    {
		 public object Insert(UserInfo entity)
        {
            ISqlMapper NewMap = SqlMapper.Instance();
            NewMap.BeginTransaction();
            try {
                UserInfo uiObj = (UserInfo)ExecuteQueryForObjectTrans("UserInfo.SelectUserInfoByLoginName", entity, NewMap);
                if (uiObj != null)
                {
                    NewMap.CommitTransaction();
                    return 2;
                }
                else { 
                    ExecuteInsertTrans("UserInfo.InsertUserInfo", entity,NewMap);
                    NewMap.CommitTransaction();
                    return 0;
                }
            }
            catch (Exception e) { 
                NewMap.RollBackTransaction();
                return 3;
            }
        }

        public int Update(UserInfo entity)
        {
            return ExecuteUpdate("UserInfo.UpdateUserInfo", entity);
        }

		public int Delete(string condition)
        {
            //删除跟插入调同一个方法。
            return ExecuteUpdate("UserInfo.DeleteUserInfo", condition);
        }

        public int ResetUser(UserInfo condition)
        {
            //删除跟插入调同一个方法。
            return ExecuteUpdate("UserInfo.UserInfoReset", condition);
        }
        

        public UserInfo GetUserInfo(object userinfoId)
        {
            return (UserInfo)ExecuteQueryForObject("UserInfo.SelectUserInfo", userinfoId);
        }

        public UserInfo GetTerUserInfo(object userinfoId)
        {
            return (UserInfo)ExecuteQueryForObject("UserInfo.SelectTerUserInfo", userinfoId);
        }

        public IList<UserInfo> GetUserInfoPage(object o) 
        {
            return ExecuteQueryForList<UserInfo>("UserInfo.SelectUserInfoPage", o);
        }

		public int GetUserInfoCount(object o) 
        {
            object count = ExecuteQueryForObject("UserInfo.SelectUserInfoCount", o);
            return (int)count;
        }

        public UserInfo GetUserObjByLoginName(UserInfo ui, out UserInfo uiObj) 
        {
            uiObj = (UserInfo)ExecuteQueryForObject("UserInfo.SelectUserInfoByLoginName", ui);
            return uiObj;
        }

        public int ModifyUserPWD(UserInfo ui)
        {
            return ExecuteUpdate("UserInfo.UpdateUserPWD", ui);
        }
        
    }
}
	