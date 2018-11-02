// Generated by IBatisNetModel

using System;
using GInterfaceDAL.Location;
using System.Collections.Generic;
using GModel.Location;
using System.Collections;

namespace GDAL.Location
{
    public class HistoricalDataDao : BaseSqlMapDao, IHistoricalDataDao
    {
        public string Insert(HistoricalData entity)
        {
            try
            {
                ExecuteInsert("HistoricalData.InsertHistoricalData", entity);
                return "true";
            }
            catch (Exception e)
            {
                return e.Message;               
            }
             
             
        }

        public int Update(HistoricalData entity)
        {
            return ExecuteUpdate("HistoricalData.UpdateHistoricalData", entity);
        }

        public int Delete(object condition)
        {
            //删除跟插入调同一个方法。
            return ExecuteUpdate("HistoricalData.DeleteHistoricalData", condition);
        }

        public HistoricalData GetHistoricalData(object userinfoId)
        {
            return (HistoricalData)ExecuteQueryForObject("HistoricalData.SelectHistoricalData", userinfoId);
        }

        //public IList<HistoricalData> GetHistoricalDataList(object o)
        //{
        //return ExecuteQueryForList<HistoricalData>("HistoricalData.SelectHistoricalData", o);  
        //}

        public IList<HistoricalData> GetHistoricalDataPage(object o)
        {
            return ExecuteQueryForList<HistoricalData>("HistoricalData.SelectHistoricalDataPage", o);
        }

        public int GetHistoricalDataCount(object o)
        {
            object count = ExecuteQueryForObject("HistoricalData.SelectHistoricalDataCount", o);
            return (int)count;
        }

        public IList<HistoricalData> GetHistorical(object ht)
        {
            return ExecuteQueryForList<HistoricalData>("HistoricalData.SelectHistorical", ht);
        }

        public IList<HistoricalData> GetRealData(object ht)
        {
            return ExecuteQueryForList<HistoricalData>("HistoricalData.SelectRealData", ht);
        }


        public IList<TerData> GetTerHistoryData(Hashtable ht) 
        {
            return ExecuteQueryForList<TerData>("RealtimeData.GetTerHistoryData", ht);
        }

        public int GetTerHistoryDataCount(Hashtable ht) 
        {
            object count = ExecuteQueryForObject("RealtimeData.GetTerHistoryDataCount", ht);
            return (int)count;
        }

        public IList<YXHistoricalData> GetYXHistoricalDataPage(Hashtable ht)
        {
            return ExecuteQueryForList<YXHistoricalData>("HistoricalData.SelectYXHistoricalDataPage", ht);
        }

        public int GetYXHistoricalDataCount(Hashtable ht)
        {
            object count = ExecuteQueryForObject("HistoricalData.GetYXHistoricalDataCount", ht);
            return (int)count;
        }
    }
}