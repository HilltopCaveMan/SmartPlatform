using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using GInterfaceDAL.Car;
using GModel.Car;
using IBatisNet.DataMapper;
using GModel.Basic;
using GModel.InterFace;
using System.Collections;
using GModel.Location;


namespace GDAL.Car
{
    public class TerminalInfoDao : BaseSqlMapDao, ITerminalInfoDao
    {
        public object Insert(TerminalInfo entity)
        {
            ISqlMapper NewMap = SqlMapper.Instance();
            NewMap.BeginTransaction();
            try
            {
                object Tercnt = ExecuteQueryForObjectTrans("TerminalInfo.SelectTerminalCountByTerNo", entity.TerNo, NewMap);
                if ((int)Tercnt == 0)
                {
                    ExecuteInsertTrans("TerminalInfo.InsertTerminalInfo", entity, NewMap);
                    object count = ExecuteQueryForObjectTrans("OldterPostbacktimes.SelectOldterPostbacktimesCount", entity.TerNo, NewMap);
                    if ((int)count == 0)
                    {
                        OldterPostbacktimes opt = new OldterPostbacktimes();
                        opt.Id = System.Guid.NewGuid().ToString();
                        opt.TerNo = entity.TerNo;
                        opt.Updatetime = DateTime.Now;
                        opt.Postbacktimes = 0;
                        ExecuteInsertTrans("OldterPostbacktimes.InsertOldterPostbacktimes", opt, NewMap);
                    }
                    NewMap.CommitTransaction();
                    return 0;
                }
                else
                {
                    NewMap.RollBackTransaction();
                    return -2;
                }

            }
            catch (Exception e)
            {
                NewMap.RollBackTransaction();
                return -1;
            }
        }

        public int Update(TerminalInfo entity)
        {
            //CheckTerNoRepeat
            string sql = @"select t.ter_no
                              from TERMINAL_INFO t
                             where t.ter_guid <> '" + entity.TerGuid + @"'
                               and t.ter_no = '" + entity.TerNo + @"'";

            DataSet ds = this.QueryForDataSet("ColligateQuery.ProteanQuery", sql);

            if (ds != null 
                && ds.Tables.Count > 0 
                && ds.Tables[0].Rows.Count > 0)
            {
                return -1;
            }

            return ExecuteUpdate("TerminalInfo.UpdateTerminalInfo", entity);
        }

        public int BindTerCar(TerminalInfo entity)
        {
            return ExecuteUpdate("TerminalInfo.BindTerCar", entity);
        }

        public string Delete(string condition)
        {
            ISqlMapper NewMap = SqlMapper.Instance();
            NewMap.BeginTransaction();
            try
            {
                string[] terNo = condition.Split(',');
                for (int i = 0; i < terNo.Length; i++) 
                {
                    ExecuteUpdateTrans("TerminalInfo.DeleteTerminalInfo", terNo[i], NewMap);
                }
                NewMap.CommitTransaction();
                return "true";

            }
            catch (Exception e)
            {
                NewMap.RollBackTransaction();
                return "false";
            }
        }

        public TerminalInfo GetTerminalInfo(object terguid)
        {
            return (TerminalInfo)ExecuteQueryForObject("TerminalInfo.SelectTerminalInfo", terguid);
        }

        public TerminalInfo GetTerminalInfoByTerNo(object terno)
        {
            return (TerminalInfo)ExecuteQueryForObject("TerminalInfo.SelectTerminalInfoByTerNo", terno);
        }

        public IList<TerminalInfo> GetTerminalInfoByCarId(object carid)
        {
            return ExecuteQueryForList<TerminalInfo>("TerminalInfo.SelectTerminalInfoByCarId", carid);
        }

        public IList<TerminalInfo> GetTerminalInfoByDeptId(object o)
        {
            return ExecuteQueryForList<TerminalInfo>("TerminalInfo.SelectTerminalInfoByDeptId", o);
        }

        public int GetTerminalInfoCount(object o)
        {
            object count = ExecuteQueryForObject("TerminalInfo.SelectTerminalInfoCount", o);
            return (int)count;
        }

        public string BindTerCar(string TerIds, string CarId)
        {
            ISqlMapper NewMap = SqlMapper.Instance();
            NewMap.BeginTransaction();
            try
            {
                if (TerIds != null && TerIds.Trim() != "")
                {
                    string[] ArrTer = TerIds.Split(new string[] { "&&--__" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < ArrTer.Length; k++)
                    {
                        TerminalInfo ti = new TerminalInfo();
                        ti.TerGuid = ArrTer[k];
                        ti.CarId = CarId;
                        ExecuteUpdateTrans("TerminalInfo.BindTerCar", ti, NewMap);
                    }
                }
                NewMap.CommitTransaction();
                return "true";
            }
            catch (Exception)
            {
                NewMap.RollBackTransaction();
                return "false";
            }
        }

        public IList<GetterpositionView> GetterpositionViewPage(GetterpositionView gpv)
        {
            return ExecuteQueryForList<GetterpositionView>("TerminalInfo.GetterpositionViewPage", gpv);
        }

        public int GetterpositionViewCount(GetterpositionView gpv) 
        {
            object count = ExecuteQueryForObject("TerminalInfo.GetterpositionViewCount", gpv);
            return (int)count;
        }

        public string AddCarAndTer(TerminalBind tb)
        {
            ISqlMapper NewMap = SqlMapper.Instance();
            NewMap.BeginTransaction();
            try
            {
                CarInfo ci = (CarInfo)ExecuteQueryForObjectTrans("CarInfo.SelectCarInfo", tb.CarNo, NewMap);
                if (ci == null)
                {
                    ci = new CarInfo();
                    ci.CarId = System.Guid.NewGuid().ToString();
                    ci.CarNo = tb.CarNo;
                    ci.Businessdivisionid = tb.DeptId;
                    ci.CarDeptcode = tb.Businessdivisioncode;
                    ci.TypeId = tb.TypeId;
                    ExecuteInsertTrans("CarInfo.InsertCarInfo", ci, NewMap);
                }

                TerminalInfo di = (TerminalInfo)ExecuteQueryForObjectTrans("TerminalInfo.SelectTerminalInfo", tb.TerGuid, NewMap);
                if (di == null)
                {
                    NewMap.RollBackTransaction();
                    return tb.TerNo + " NotExistTer";
                }
                else
                {
                    TerminalInfo ti = new TerminalInfo();
                    ti.TerGuid = tb.TerGuid;
                    ti.CarId = ci.CarId;
                    ti.TerCarno = ci.CarNo;
                    ti.DeptId = tb.DeptId;
                    ti.TerDeptcode = tb.Businessdivisioncode;
                    ExecuteUpdateTrans("TerminalInfo.BindTerCar", ti, NewMap);
                    NewMap.CommitTransaction();
                    return "true";
                }
            }
            catch (Exception e)
            {
                NewMap.RollBackTransaction();
                return "false";
            }
        }

        public IList<TerminalBind> GetTerminalBindPage(object o)
        {
            return ExecuteQueryForList<TerminalBind>("TerminalInfo.SelectTerminalBindPage", o);
        }

        public int GetTerminalBindCount(object o) 
        {
            object count = ExecuteQueryForObject("TerminalInfo.SelectTerminalBindCount", o);
            return (int)count;
        }

        public int GetTerminalByCarId(string CarId) {
            object count = ExecuteQueryForObject("TerminalInfo.SelectTerminalCountByCarId", CarId);
            return (int)count;
        }

        public string RemoveBind(string TerGuids) 
        {
            ISqlMapper NewMap = SqlMapper.Instance();
            NewMap.BeginTransaction();
            try
            {
                string[] TerGuidArr = TerGuids.Split(',');
                for (int i = 0; i < TerGuidArr.Length; i++)
                {
                    string carId = ExecuteQueryForObject("TerminalInfo.SelectCarIdByTerGuid", TerGuidArr[i]).ToString();
                    int cnt = GetTerminalByCarId(carId);
                    if (cnt > 1)
                    {
                        ExecuteUpdateTrans("TerminalInfo.TerminalRemoveBindByGuid", TerGuidArr[i], NewMap);
                    }
                    else
                    {
                        ExecuteUpdateTrans("CarInfo.DeleteCarInfo", carId, NewMap); //É¾³ý³µÁ¾
                        ExecuteUpdateTrans("TerminalInfo.TerminalRemoveBindByGuid", TerGuidArr[i], NewMap);
                    }
                }

                NewMap.CommitTransaction();
                return "true";
            }
            catch (Exception e)
            {
                NewMap.RollBackTransaction();
                return "false";               
            }
        }

        public string upLoadData(UpLoadTerBind ut,string DeptId)
        {
            Hashtable htTerType = new Hashtable();
            ProductsInfo pi = new ProductsInfo();
            ProductsInfoDao _iProductsInfoDao = new ProductsInfoDao();
            IList<ProductsInfo> lpt = _iProductsInfoDao.GetProductsInfoPage(pi);
            for (int n = 0; n < lpt.Count; n++)
            {
                if (!htTerType.ContainsKey(lpt[n].ProName))
                {
                    htTerType.Add(lpt[n].ProName, lpt[n].ProId);
                }
            }

            ISqlMapper NewMap = SqlMapper.Instance();
            NewMap.BeginTransaction();
            try
            {
                TerminalInfo ti = (TerminalInfo)ExecuteQueryForObjectTrans("TerminalInfo.SelectTerminalInfoByTerNo", ut.TerNo, NewMap);
                if (ti == null)
                {
                    ti = new TerminalInfo();
                    ti.TerGuid = System.Guid.NewGuid().ToString();
                    ti.TerNo = ut.TerNo;
                    ti.TerTypeid = htTerType[ut.TerType].ToString();
                    ti.TerSimcard = ut.SimCard;
                    ti.TerInnettime = ut.TerInnettime;
                    ti.DeptId = DeptId;
                    if (DeptId != null && DeptId != "")
                    {
                        GDAL.Basic.DeptInfoDao deptInfoBll = new Basic.DeptInfoDao();
                        DeptInfo di = deptInfoBll.GetDeptInfoById(DeptId);
                        ti.TerDeptcode = di.Businessdivisioncode;
                    }
                    ExecuteInsertTrans("TerminalInfo.InsertTerminalInfo", ti, NewMap);
                    object count = ExecuteQueryForObjectTrans("OldterPostbacktimes.SelectOldterPostbacktimesCount", ut.TerNo, NewMap);
                    if ((int)count == 0)
                    {
                        OldterPostbacktimes opt = new OldterPostbacktimes();
                        opt.Id = System.Guid.NewGuid().ToString();
                        opt.TerNo = ut.TerNo;
                        opt.Updatetime = DateTime.Now;
                        opt.Postbacktimes = 0;
                        ExecuteInsertTrans("OldterPostbacktimes.InsertOldterPostbacktimes", opt, NewMap);
                    }
                    NewMap.CommitTransaction();
                    return "true";
                }
                else
                {
                    NewMap.RollBackTransaction();
                    return "false";
                }
            }
            catch (Exception e)
            {
                NewMap.RollBackTransaction();
                return "false";               
            }
        }

        public int CarExChange(TerminalInfo entity)
        {
            return ExecuteUpdate("TerminalInfo.CarExChange", entity);
        }

        public int TerExChange(TerminalInfo entity) 
        {
            return ExecuteUpdate("TerminalInfo.TerExChange", entity);
        }

        public int SetReplyRealdata(TerminalInfo entity)
        {
            return ExecuteUpdate("TerminalInfo.SetReplyRealdata", entity);
        }

        public IList<TercensusData> GetTercensusData(Hashtable ht)
        {
            return ExecuteQueryForList<TercensusData>("TerminalInfo.GetTercensusData", ht);
        }

        public int GetTercensusDataCount(Hashtable ht)
        {
            object count = ExecuteQueryForObject("TerminalInfo.GetTercensusDataCount", ht);
            return (int)count;
        }
    }
}
