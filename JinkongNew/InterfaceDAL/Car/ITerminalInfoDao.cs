// Generated by IBatisNetModel
	
using System;
using System.Collections.Generic;
using GModel.Car;
using GModel.Basic;
using System.Collections;
using GModel.Location;

namespace GInterfaceDAL.Car
{
    public interface ITerminalInfoDao
    {
        //IList<TerminalInfo> GetTerminalInfoList(object o);
        int Update(TerminalInfo entity);
        string Delete(string condition);
        object Insert(TerminalInfo entity);

        TerminalInfo GetTerminalInfo(object userinfoId);

        TerminalInfo GetTerminalInfoByTerNo(object terno);

        IList<TerminalInfo> GetTerminalInfoByCarId(object carid);

        IList<TerminalInfo> GetTerminalInfoByDeptId(object o);
        int GetTerminalInfoCount(object o);

        string BindTerCar(string TerIds,string CarId);
        IList<GetterpositionView> GetterpositionViewPage(GetterpositionView gpv);
        int GetterpositionViewCount(GetterpositionView gpv);
        string AddCarAndTer(TerminalBind tb);

        IList<TerminalBind> GetTerminalBindPage(object o);
        int GetTerminalBindCount(object o);
        int GetTerminalByCarId(string CarId);
        string RemoveBind(string TerGuids);
        string upLoadData(UpLoadTerBind ut, string DeptId);

        int CarExChange(TerminalInfo entity);

        int TerExChange(TerminalInfo entity);

        int SetReplyRealdata(TerminalInfo entity);

        IList<TercensusData> GetTercensusData(Hashtable ht);
        int GetTercensusDataCount(Hashtable ht);
    }
}	
	
	