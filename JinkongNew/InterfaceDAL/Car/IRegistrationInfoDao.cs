// Generated by IBatisNetModel
	
using System;
using System.Collections.Generic;
using GModel.Car;

namespace GInterfaceDAL.Car
{
    public interface IRegistrationInfoDao
    {
        //IList<RegistrationInfo> GetRegistrationInfoList(object o);
        int Update(RegistrationInfo entity);
        int Delete(object condition);
        object Insert(RegistrationInfo entity);

        RegistrationInfo GetRegistrationInfo(object userinfoId);

        IList<RegistrationInfo> GetRegistrationInfoPage(object o);
        int GetRegistrationInfoCount(object o);
    }
}	
	
	