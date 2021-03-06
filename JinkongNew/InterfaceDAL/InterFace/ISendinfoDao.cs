// Generated by IBatisNetModel
	
using System;
using System.Collections.Generic;
using GModel.InterFace;

namespace GInterfaceDAL.InterFace
{
    public interface ISendinfoDao
    {
        //IList<Sendinfo> GetSendinfoList(object o);
        int Update(Sendinfo entity);
        int Delete(object condition);
        object Insert(Sendinfo entity);
        Sendinfo GetSendinfo(string Id);
        IList<Sendinfo> GetSendinfoPage(object o);
        int GetSendinfoCount(object o);
        string UpdateSendInfoTable(Sendinfo si);
        string UpdateSendInfoTable_Devid(string devid, string UpdatePara, string cmdResult);
        IList<Sendinfo> GetSendinfoPageByDeviceId(string DeviceId);
        string InsertData(Sendinfo entity);

        string InsertDataNew(Sendinfo entity,int oldid);
        IList<Sendinfo> GetNewestSendinfo(string DeviceIds);

        IList<Sendinfo> SelectSendinfoListByTerNo(object o);
        IList<Sendinfo> SelectSendinfoListByTerNos(object o);
        IList<Sendinfo> SelectSendinfoListNeweastByTerNos(object o);
        int GetSendinfoCountByTerNo(object o);
        int GetSendinfoCountByTerNos(object o);
        int GetSendinfoNeweastCountByTerNos(object o);
    }
}	
	
	