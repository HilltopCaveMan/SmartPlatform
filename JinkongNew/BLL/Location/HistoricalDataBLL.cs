// Generated by IBatisNetModel

using System;
using System.Collections;

using GModel.Location;
using GInterfaceDAL.Location;
using GDAL.Location;
using System.Collections.Generic;

namespace GBLL.Location
{
    public class HistoricalDataBLL
    {
        private IHistoricalDataDao _iHistoricalDataDao = null;
        public HistoricalDataBLL()
        {
            _iHistoricalDataDao = new HistoricalDataDao();
        }

		public HistoricalData GetHistoricalData(object o)
        {
            return _iHistoricalDataDao.GetHistoricalData( o);
        }

        public IList<HistoricalData> GetHistoricalDataPage(object o)
        {
            return _iHistoricalDataDao.GetHistoricalDataPage(o);
        }

		public int GetHistoricalDataCount(object o)
        {
            return _iHistoricalDataDao.GetHistoricalDataCount(o);
        }

        public IList<YXHistoricalData> GetYXHistoricalDataPage(Hashtable ht)
        {
            return _iHistoricalDataDao.GetYXHistoricalDataPage(ht);
        }

        public int GetYXHistoricalDataCount(Hashtable ht)
        {
            return _iHistoricalDataDao.GetYXHistoricalDataCount(ht);
        }

        //public IList<HistoricalData> GetHistoricalDataList(object o)
        //{
            //return _iHistoricalDataDao.GetHistoricalDataList(o);
        //}

		public string Insert(RealtimeData entity)
        {
            HistoricalData hid = new HistoricalData();
            hid.Id = System.Guid.NewGuid().ToString();
            hid.Rawdataid = entity.Rawdataid;
            hid.TerNo = entity.TerNo;
            hid.GenerationNum = entity.GenerationNum;
            hid.Factorynum = entity.Factorynum;
            hid.Rtime = entity.Rtime;
            hid.Protocolversion = entity.Protocolversion;
            hid.Programverson = entity.Programverson;
            hid.Gpsverson = entity.Gpsverson;
            hid.Positioningtime = entity.Positioningtime;
            hid.TerStatus = entity.TerStatus;
            hid.TerModel = entity.TerModel;
            hid.Worktime = entity.Worktime;
            hid.Sleeptime = entity.Sleeptime;
            hid.Ntervalltime = entity.Ntervalltime;
            hid.TerVbatt = entity.TerVbatt;
            hid.Totalworktime = entity.Totalworktime;
            hid.Blinddatanum = entity.Blinddatanum;
            hid.TerStatrtimes = entity.TerStatrtimes;
            hid.Ifblinddata = entity.Ifblinddata;
            hid.Ifposition = entity.Ifposition;
            hid.Northorsouth = entity.Northorsouth;
            hid.Eastorwest = entity.Eastorwest;
            hid.Latitude = entity.Latitude;
            hid.Longitude = entity.Longitude;
            hid.BaiduLatitude = entity.BaiduLatitude;
            hid.BaiduLongitude = entity.BaiduLongitude;
            hid.GoogleLatitude = entity.GoogleLatitude;
            hid.GoogleLongitude = entity.GoogleLongitude;
            hid.Position = entity.Position;
            hid.Speed = entity.Speed;
            hid.Direction = entity.Direction;
            hid.Height = entity.Height;
            hid.Gpsant = entity.Gpsant;
            hid.Usesatellite = entity.Usesatellite;
            hid.Visualsatellite = entity.Visualsatellite;
            hid.Gpsrssi = entity.Gpsrssi;
            hid.Gsmrssi = entity.Gsmrssi;
            hid.Lca = entity.Lca;
            hid.Cell = entity.Cell;
            hid.Gsmrssi1 = entity.Gsmrssi1;
            hid.Lca1 = entity.Lca1;
            hid.Cell1 = entity.Cell1;
            hid.Gsmrssi2 = entity.Gsmrssi2;
            hid.Lca2 = entity.Lca2;
            hid.Cell2 = entity.Cell2;
            hid.Gsmrssi3 = entity.Gsmrssi3;
            hid.Lca3 = entity.Lca3;
            hid.Cell3 = entity.Cell3;
            hid.Province = entity.Province;
            hid.City = entity.City;
            hid.County = entity.County;
            hid.Workstate = entity.Workstate;
            hid.Postbacktimes = entity.Postbacktimes;
            hid.Gsmrssi4 = entity.Gsmrssi4;
            hid.Lca4 = entity.Lca4;
            hid.Cell4 = entity.Cell4;
            hid.Gsmrssi5 = entity.Gsmrssi5;
            hid.Lca5 = entity.Lca5;
            hid.Cell5 = entity.Cell5;
            hid.Gsmrssi6 = entity.Gsmrssi6;
            hid.Lca6 = entity.Lca6;
            hid.Cell6 = entity.Cell6;
            hid.Accstate = entity.Accstate;
            hid.Lat = entity.Lat;
            hid.Lng = entity.Lng;
            hid.Carworkvmp = entity.Carworkvmp;
            hid.Carworktemp = entity.Carworktemp;
            hid.RemainlPct = entity.RemainlPct;
            hid.AddupDist = entity.AddupDist;
            return _iHistoricalDataDao.Insert(hid);
        }

        public int Update(HistoricalData entity)
        {
            return _iHistoricalDataDao.Update(entity);
        }
		
        public int Delete(string condition)
        {
            return _iHistoricalDataDao.Delete(condition);
        }


        public IList<HistoricalData> GetHistorical(object o) 
        {
          IList<HistoricalData> ihtd =  _iHistoricalDataDao.GetHistorical(o);
          return ihtd;
        }

        public IList<TerData> GetTerHistoryData(Hashtable ht)
        {
            return _iHistoricalDataDao.GetTerHistoryData(ht);

        }

        public int GetTerHistoryDataCount(Hashtable ht) 
        {
            return _iHistoricalDataDao.GetTerHistoryDataCount(ht);
        }
    }
}
	