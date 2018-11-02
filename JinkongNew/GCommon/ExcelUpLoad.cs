using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GModel.Basic;
using GModel.Location;
using NPOI.HSSF.Util;
using GModel.Car;
using GBLL;
using System.Data;

namespace GCommon
{
    public class ExcelUpLoad
    {

        private ColligateQueryService query = new ColligateQueryService();

        //private ICellStyle Getcellstyle(IWorkbook wb, string str)
        //{
        //    ICellStyle cellStyle = wb.CreateCellStyle();

        //    //字体
        //    IFont font12 = wb.CreateFont();
        //    font12.FontHeightInPoints = 10;

        //    font12.FontName = "微软雅黑";

        //    IFont font = wb.CreateFont();
        //    font.FontName = "微软雅黑";
        //    //font.Underline = 1;

        //    IFont fontcolorblue = wb.CreateFont();
        //    fontcolorblue.Color = HSSFColor.OLIVE_GREEN.BLUE.index;
        //    fontcolorblue.IsItalic = true;
        //    fontcolorblue.FontName = "微软雅黑";


        //    //边框
        //    cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.DOTTED;
        //    cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.HAIR;
        //    cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.HAIR;
        //    cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.DOTTED;
        //    //边框颜色
        //    cellStyle.BottomBorderColor = HSSFColor.OLIVE_GREEN.BLUE.index;
        //    cellStyle.TopBorderColor = HSSFColor.OLIVE_GREEN.BLUE.index;

        //    //背景
        //    //cellStyle.FillBackgroundColor = HSSFColor.OLIVE_GREEN.BLUE.index;
        //    //cellStyle.FillForegroundColor = HSSFColor.OLIVE_GREEN.BLUE.index;
        //    cellStyle.FillForegroundColor = HSSFColor.WHITE.index;
        //    // cellStyle.FillPattern = FillPatternType.NO_FILL;
        //    cellStyle.FillBackgroundColor = HSSFColor.MAROON.index;
        //    //水平对齐
        //    cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;

        //    //垂直对齐
        //    cellStyle.VerticalAlignment = VerticalAlignment.CENTER;

        //    //自动换行
        //    cellStyle.WrapText = true;

        //    //缩进
        //    cellStyle.Indention = 0;
        //    switch (str)
        //    {
        //        case "头":
        //            // cellStyle.FillPattern = FillPatternType.LEAST_DOTS;
        //            cellStyle.SetFont(font12);
        //            break;
        //        case "时间":
        //            IDataFormat datastyle = wb.CreateDataFormat();

        //            cellStyle.DataFormat = datastyle.GetFormat("yyyy/mm/dd");
        //            cellStyle.SetFont(font);
        //            break;
        //        case "数字":
        //            cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
        //            cellStyle.SetFont(font);
        //            break;
        //        case "钱":
        //            IDataFormat format = wb.CreateDataFormat();
        //            cellStyle.DataFormat = format.GetFormat("￥#,##0");
        //            cellStyle.SetFont(font);
        //            break;
        //        case "url":
        //            fontcolorblue.Underline = 1;
        //            cellStyle.SetFont(fontcolorblue);
        //            break;
        //        case "百分比":
        //            cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
        //            cellStyle.SetFont(font);
        //            break;
        //        case "中文大写":
        //            IDataFormat format1 = wb.CreateDataFormat();
        //            cellStyle.DataFormat = format1.GetFormat("[DbNum2][$-804]0");
        //            cellStyle.SetFont(font);
        //            break;
        //        case "科学计数法":
        //            cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00E+00");
        //            cellStyle.SetFont(font);
        //            break;
        //        case "默认":
        //            cellStyle.SetFont(font);
        //            break;
        //    }
        //    return cellStyle;
        //}


        //读取EXCEL
        public List<UpLoadTerBind> ReadExcel(string excelPath, ref string msg)
        {
            StringBuilder sbr = new StringBuilder();
            try
            {
                List<UpLoadTerBind> lut = new List<UpLoadTerBind>();
                using (FileStream fs = File.OpenRead(excelPath))   //打开myxls.xls文件
                {
                    string fileExt = Path.GetExtension(excelPath);
                    IWorkbook wk = null;
                    if (fileExt == ".xls")
                    {
                        wk = new HSSFWorkbook(fs);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        wk = new XSSFWorkbook(fs);
                    }
                    if (wk != null)
                    {
                        for (int i = 0; i < wk.NumberOfSheets; i++)  //NumberOfSheets是myxls.xls中总共的表数
                        {
                            ISheet sheet = wk.GetSheetAt(i);   //读取当前表数据
                            string a = sheet.GetRow(0).GetCell(0).ToString().Trim();
                            string b = sheet.GetRow(0).GetCell(1).ToString().Trim();
                            string c = sheet.GetRow(0).GetCell(2).ToString().Trim();
                            int km = sheet.GetRow(0).LastCellNum;
                            if (sheet.LastRowNum >= 1 && sheet.GetRow(0).GetCell(0).ToString().Trim() == "NRS终端编号" && sheet.GetRow(0).GetCell(1).ToString().Trim() == "SIM卡号" && sheet.GetRow(0).GetCell(2).ToString().Trim() == "终端类型")
                            {
                                for (int j = 1; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                                {
                                    IRow row = sheet.GetRow(j);  //读取当前行数据
                                    if (row != null)
                                    {
                                        UpLoadTerBind ut = new UpLoadTerBind();
                                        for (int k = 0; k <= row.LastCellNum; k++)  //LastCellNum 是当前行的总列数
                                        {
                                            ICell cell = row.GetCell(k);  //当前表格
                                            if (cell != null && cell.ToString().Trim()!="")
                                            {
                                                switch (k)
                                                {
                                                    case 0:
                                                        ut.TerNo = cell.ToString().Trim();
                                                        break;
                                                    case 1:
                                                        ut.SimCard = cell.ToString().Trim();
                                                        break;
                                                    case 2:
                                                        ut.TerType = cell.ToString().Trim();
                                                        break;
                                                }
                                                ut.TerInnettime = DateTime.Now;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(ut.TerNo))
                                        {
                                            lut.Add(ut);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                msg = "数据格式不正确，请用模板填写数据。";
                                return null;
                            }
                        }
                    }
                }
                return lut;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 车辆监控导出EXCEL
        /// </summary>
        /// <param name="irt"></param>
        /// <returns></returns>
        public MemoryStream CreateExcel(IList<Selectcarmonitor> irt)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("车辆位置信息");
            sheet.SetColumnWidth(0, 20 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 12 * 256);
            sheet.SetColumnWidth(3, 10 * 256);
            sheet.SetColumnWidth(4, 10 * 256);
            sheet.SetColumnWidth(5, 15 * 256);
            sheet.SetColumnWidth(6, 13 * 256);
            sheet.SetColumnWidth(7, 30 * 256);
            sheet.SetColumnWidth(8, 10 * 256);
            sheet.SetColumnWidth(9, 20 * 256);
            sheet.SetColumnWidth(10, 20 * 256);
            sheet.SetColumnWidth(11, 20 * 256);
            sheet.SetColumnWidth(12, 20 * 256);
            sheet.SetColumnWidth(13, 20 * 256);

            IRow row1 = sheet.CreateRow(0);
            row1.Height = 300;
            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("回传时间");

            cell = row1.CreateCell(1);
            cell.SetCellValue("车牌号");

            cell = row1.CreateCell(2);
            cell.SetCellValue("终端编号");

            cell = row1.CreateCell(3);
            cell.SetCellValue("终端状态");

            cell = row1.CreateCell(4);
            cell.SetCellValue("蓝牙绑定");

            cell = row1.CreateCell(5);
            cell.SetCellValue("蓝牙状态");

            cell = row1.CreateCell(6);
            cell.SetCellValue("定位方式");

            cell = row1.CreateCell(7);
            cell.SetCellValue("当前位置");

            cell = row1.CreateCell(8);
            cell.SetCellValue("SIM卡号");

            cell = row1.CreateCell(9);
            cell.SetCellValue("所属组织");

            cell = row1.CreateCell(10);
            cell.SetCellValue("回传次数");

            cell = row1.CreateCell(11);
            cell.SetCellValue("车主电话");

            cell = row1.CreateCell(12);
            cell.SetCellValue("安装位置");

            cell = row1.CreateCell(13);
            cell.SetCellValue("安装时间");

            // 创建新增行
            if (irt != null && irt.Count > 0)
            {
                for (int i = 0; i < irt.Count; i++)
                {
                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    cell.SetCellValue(irt[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss"));

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(irt[i].CarNo);

                    cell = row1.CreateCell(2);
                    cell.SetCellValue(irt[i].TerNo);

                    cell = row1.CreateCell(3);
                    cell.SetCellValue(irt[i].TerStatus);

                    cell = row1.CreateCell(4);
                    cell.SetCellValue(irt[i].Northorsouth=="1"?"已绑蓝牙":"");

                    cell = row1.CreateCell(5);
                    string lyzt = "";
                    if (irt[i].Eastorwest == "1")
                    {
                        lyzt = "正常";
                    }
                    else if (irt[i].Eastorwest == "0")
                    {
                        if (irt[i].ProName == "一代无线GPS" || irt[i].ProName == "二代无线GPS" || irt[i].ProName == "五代无线GPS")
                        {
                            lyzt = "有线蓝牙断开";
                        }
                        else
                        {
                            lyzt = "蓝牙断开";
                        }
                    }
                    cell.SetCellValue(lyzt);

                    cell = row1.CreateCell(6);
                    string posstr = "";
                    if (irt[i].ProName == "一代无线GPS" || irt[i].ProName == "二代无线GPS" || irt[i].ProName == "五代无线GPS")
                    {
                        posstr=irt[i].Ifposition;
                    }
                    else
                    {
                        if (irt[i].Ifposition == "GPS")
                        {
                            posstr = "GPS";
                        }
                        else
                        {
                            posstr = "否";
                        }
                    }
                    cell.SetCellValue(posstr);

                    cell = row1.CreateCell(7);
                    cell.SetCellValue(irt[i].Position);

                    cell = row1.CreateCell(8);
                    cell.SetCellValue(irt[i].TerSimcard);

                    cell = row1.CreateCell(9);
                    cell.SetCellValue(irt[i].Businessdivisionname);

                    cell = row1.CreateCell(10);
                    cell.SetCellValue(irt[i].Postbacktimes);

                    cell = row1.CreateCell(11);
                    cell.SetCellValue(irt[i].OwerPhone);

                    cell = row1.CreateCell(12);
                    cell.SetCellValue(irt[i].InstallAddress);

                    cell = row1.CreateCell(13);
                    cell.SetCellValue(irt[i].InstallTime);
                }
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 历史轨迹导出EXCEL(0 无线 1有线)
        /// </summary>
        /// <returns></returns>
        public MemoryStream CreateExcel(IList<HistoricalData> ihd, int type)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("历史轨迹信息");
            sheet.SetColumnWidth(0, 20 * 256);
            sheet.SetColumnWidth(1, 80 * 256);
            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 20 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 15 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            IRow row1 = sheet.CreateRow(0);
            row1.Height = 300;
            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("更新时间");

            cell = row1.CreateCell(1);
            cell.SetCellValue("当前位置");

            cell = row1.CreateCell(2);
            cell.SetCellValue("终端编号");

            cell = row1.CreateCell(3);
            if (type == 0)
            {
                cell.SetCellValue("终端状态");
            }
            else
            {
                cell.SetCellValue("报警状态");
            }

            cell = row1.CreateCell(4);
            cell.SetCellValue("实际定位");

            cell = row1.CreateCell(5);
            cell.SetCellValue("谷歌纬度");

            cell = row1.CreateCell(6);
            cell.SetCellValue("谷歌经度");

            cell = row1.CreateCell(7);
            cell.SetCellValue("百度纬度");

            cell = row1.CreateCell(8);
            cell.SetCellValue("百度经度");

            // 创建新增行
            if (ihd != null && ihd.Count > 0)
            {
                for (int i = 0; i < ihd.Count; i++)
                {
                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    cell.SetCellValue(ihd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss"));

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(ihd[i].Position);

                    cell = row1.CreateCell(2);
                    cell.SetCellValue(ihd[i].TerNo);

                    cell = row1.CreateCell(3);
                    string TerState = "";
                    if (type == 0)
                    {
                        switch (ihd[i].TerStatus)
                        {
                            case "1":
                                TerState = "测试";
                                break;
                            case "2":
                                TerState = "待激活";
                                break;
                            case "3":
                                TerState = "已激活";
                                break;
                            case "4":
                                TerState = "已拆除";
                                break;
                            case "5":
                                TerState = "休眠";
                                break;
                            case "6":
                                TerState = "到期";
                                break;
                            case "7":
                                TerState = "已使用";
                                break;
                            default:
                                TerState = "其他";
                                break;
                        }
                    }
                    else
                    {
                        switch (ihd[i].ReplydataName)
                        {
                            case "":
                                TerState = "正常";
                                break;
                            default:
                                TerState = ihd[i].ReplydataName;
                                break;
                        }
                    }
                    cell.SetCellValue(TerState);

                    cell = row1.CreateCell(4);
                    string posstr = "";
                    if(ihd[i].Ifposition=="定位")
                    {
                        posstr = "GPS";
                    }
                    else
                    {
                        posstr = "否";
                    }

                    cell.SetCellValue(posstr);


                    cell = row1.CreateCell(5);
                    cell.SetCellValue(ihd[i].GoogleLatitude);

                    cell = row1.CreateCell(6);
                    cell.SetCellValue(ihd[i].GoogleLongitude);

                    cell = row1.CreateCell(7);
                    cell.SetCellValue(ihd[i].BaiduLatitude);

                    cell = row1.CreateCell(8);
                    cell.SetCellValue(ihd[i].BaiduLongitude);
                }

            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 终端上传处历史轨迹导出(无线)
        /// </summary>
        /// <param name="itd"></param>
        /// <returns></returns>
        public MemoryStream CreateExcel(IList<TerData> itd)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("历史轨迹信息");
            sheet.SetColumnWidth(0, 20 * 256);
            sheet.SetColumnWidth(1, 20 * 256);
            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 20 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 15 * 256);
            sheet.SetColumnWidth(6, 60 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            sheet.SetColumnWidth(9, 15 * 256);
            sheet.SetColumnWidth(10, 15 * 256);
            sheet.SetColumnWidth(11, 15 * 256);
            sheet.SetColumnWidth(12, 15 * 256);
            sheet.SetColumnWidth(13, 15 * 256);
            sheet.SetColumnWidth(14, 15 * 256);
            sheet.SetColumnWidth(15, 15 * 256);
            sheet.SetColumnWidth(16, 15 * 256);
            sheet.SetColumnWidth(17, 15 * 256);
            sheet.SetColumnWidth(18, 15 * 256);
            sheet.SetColumnWidth(19, 15 * 256);
            sheet.SetColumnWidth(20, 15 * 256);
            sheet.SetColumnWidth(21, 15 * 256);
            sheet.SetColumnWidth(22, 15 * 256);
            sheet.SetColumnWidth(23, 15 * 256);
            sheet.SetColumnWidth(24, 15 * 256);
            sheet.SetColumnWidth(25, 15 * 256);
            sheet.SetColumnWidth(26, 15 * 256);
            sheet.SetColumnWidth(27, 15 * 256);

            IRow row1 = sheet.CreateRow(0);
            row1.Height = 300;
            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("终端编号");

            cell = row1.CreateCell(1);
            cell.SetCellValue("SIM卡号");

            cell = row1.CreateCell(2);
            cell.SetCellValue("工作时间");

            cell = row1.CreateCell(3);
            cell.SetCellValue("设备状态");

            cell = row1.CreateCell(4);
            cell.SetCellValue("回传时间");

            cell = row1.CreateCell(5);
            cell.SetCellValue("入网时间");

            cell = row1.CreateCell(6);
            cell.SetCellValue("地理位置");

            cell = row1.CreateCell(7);
            cell.SetCellValue("定位时间");

            cell = row1.CreateCell(8);
            cell.SetCellValue("纬度");

            cell = row1.CreateCell(9);
            cell.SetCellValue("经度");

            cell = row1.CreateCell(10);
            cell.SetCellValue("设备软件版本");

            cell = row1.CreateCell(11);
            cell.SetCellValue("设备电池电压");

            cell = row1.CreateCell(12);
            cell.SetCellValue("设备间隔时间");

            cell = row1.CreateCell(13);
            cell.SetCellValue("GSM信号强度");

            cell = row1.CreateCell(14);
            cell.SetCellValue("设备启动次数");

            cell = row1.CreateCell(15);
            cell.SetCellValue("蓝牙绑定");

            cell = row1.CreateCell(16);
            cell.SetCellValue("蓝牙状态");

            cell = row1.CreateCell(17);
            cell.SetCellValue("设备休眠时间");

            cell = row1.CreateCell(18);
            cell.SetCellValue("GPS速度");

            cell = row1.CreateCell(19);
            cell.SetCellValue("可视卫星颗数");

            cell = row1.CreateCell(20);
            cell.SetCellValue("使用卫星颗数");

            cell = row1.CreateCell(21);
            cell.SetCellValue("实际定位");

            cell = row1.CreateCell(22);
            cell.SetCellValue("设备工作模式");

            cell = row1.CreateCell(23);
            cell.SetCellValue("GPS高度（海拔）");

            cell = row1.CreateCell(24);
            cell.SetCellValue("盲区数据标志");

            cell = row1.CreateCell(25);
            cell.SetCellValue("GPS方向");

            cell = row1.CreateCell(26);
            cell.SetCellValue("盲区数据数量");

            cell = row1.CreateCell(27);
            cell.SetCellValue("设备硬件版本");

            // 创建新增行
            if (itd != null && itd.Count > 0)
            {
                for (int i = 0; i < itd.Count; i++)
                {
                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    cell.SetCellValue(itd[i].TerNo);

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(itd[i].TerSimcard);

                    cell = row1.CreateCell(2);
                    cell.SetCellValue(itd[i].Totalworktime);

                    cell = row1.CreateCell(3);
                    string state = "";
                    if (itd[i].TerStatus == "1")
                    {
                        state = "测试";
                    }
                    else if (itd[i].TerStatus == "2")
                    {
                        state = "待激活";
                    }
                    else if (itd[i].TerStatus == "3")
                    {
                        state = "已激活";
                    }
                    else if (itd[i].TerStatus == "4")
                    {
                        state = "已拆除";
                    }
                    else if (itd[i].TerStatus == "5")
                    {
                        state = "休眠";
                    }
                    else if (itd[i].TerStatus == "6")
                    {
                        state = "到期";
                    }
                    else if (itd[i].TerStatus == "7")
                    {
                        state = "已使用";
                    }
                    else
                    {
                        state = "其他";
                    }
                    cell.SetCellValue(state);


                    cell = row1.CreateCell(4);
                    string Rtimedate = "";
                    if (itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                    {
                        Rtimedate = itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    cell.SetCellValue(Rtimedate);

                    cell = row1.CreateCell(5);
                    string innettimedate = "";
                    if (itd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                    {
                        innettimedate = itd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    cell.SetCellValue(innettimedate);

                    cell = row1.CreateCell(6);
                    cell.SetCellValue(itd[i].Position);

                    cell = row1.CreateCell(7);
                    cell.SetCellValue(itd[i].Positioningtime.ToString("yyyy-MM-dd HH:mm:ss"));

                    cell = row1.CreateCell(8);
                    cell.SetCellValue(itd[i].Latitude);

                    cell = row1.CreateCell(9);
                    cell.SetCellValue(itd[i].Longitude);

                    cell = row1.CreateCell(10);
                    cell.SetCellValue(itd[i].Programverson);

                    cell = row1.CreateCell(11);
                    cell.SetCellValue(itd[i].TerVbatt);

                    cell = row1.CreateCell(12);
                    cell.SetCellValue(itd[i].Ntervalltime);

                    cell = row1.CreateCell(13);
                    cell.SetCellValue(itd[i].Gsmrssi);

                    cell = row1.CreateCell(14);
                    cell.SetCellValue(itd[i].TerStatrtimes);

                    cell = row1.CreateCell(15);
                    cell.SetCellValue(itd[i].Northorsouth == "1" ? "已绑蓝牙" : "");

                    cell = row1.CreateCell(16);
                    string lyzt = "";
                    if (itd[i].Eastorwest == "1")
                    {
                        lyzt = "正常";
                    }
                    else if (itd[i].Eastorwest == "0")
                    {
                        if (itd[i].ProName == "一代无线GPS" || itd[i].ProName == "二代无线GPS" || itd[i].ProName == "五代无线GPS")
                        {
                            lyzt = "有线蓝牙断开";
                        }
                        else
                        {
                            lyzt = "蓝牙断开";
                        }
                    }
                    cell.SetCellValue(lyzt);

                    cell = row1.CreateCell(17);
                    cell.SetCellValue(itd[i].Sleeptime);

                    cell = row1.CreateCell(18);
                    cell.SetCellValue(itd[i].Speed);

                    cell = row1.CreateCell(19);
                    cell.SetCellValue(itd[i].Visualsatellite);

                    cell = row1.CreateCell(20);
                    cell.SetCellValue(itd[i].Usesatellite);

                    cell = row1.CreateCell(21);
                    string dingweistr = "";
                    if (itd[i].Ifposition == "定位")
                    {
                        dingweistr = "GPS";
                    }
                    else if (itd[i].Ifposition == "不定位")
                    {
                        dingweistr = "基站";
                    }
                    else
                    {
                        dingweistr = "其他";
                    }
                    cell.SetCellValue(dingweistr);

                    cell = row1.CreateCell(22);
                    string termodel = "";
                    if (itd[i].TerModel == "1")
                    {
                        termodel = "标准模式";
                    }
                    else if (itd[i].TerModel == "2")
                    {
                        termodel = "精准模式";
                    }
                    else if (itd[i].TerModel == "3")
                    {
                        termodel = "追车模式";
                    }
                    else
                    {
                        termodel = "其他";
                    }
                    cell.SetCellValue(termodel);

                    cell = row1.CreateCell(23);
                    cell.SetCellValue(itd[i].Height);

                    cell = row1.CreateCell(24);
                    string bliddata = "";
                    if (itd[i].Ifblinddata == "1")
                    {
                        bliddata = "盲区数据";
                    }
                    else
                    {
                        bliddata = "正常数据";
                    }
                    cell.SetCellValue(bliddata);

                    cell = row1.CreateCell(25);
                    cell.SetCellValue(itd[i].Direction);

                    cell = row1.CreateCell(26);
                    cell.SetCellValue(itd[i].Blinddatanum);

                    cell = row1.CreateCell(27);
                    cell.SetCellValue(itd[i].Gpsverson);
                }
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 终端上传处数据(管理员和生产用)
        /// </summary>
        /// <param name="itd"></param>
        /// <returns></returns>
        public MemoryStream CreateExcelTerData(IList<TerData> itd)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            int num = itd.Count / 30000;
            if (num == 0)
            {
                ISheet sheet = book.CreateSheet("终端上传数据信息");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 15 * 256);
                sheet.SetColumnWidth(6, 15 * 256);
                sheet.SetColumnWidth(7, 15 * 256);
                sheet.SetColumnWidth(8, 15 * 256);
                sheet.SetColumnWidth(9, 15 * 256);
                sheet.SetColumnWidth(10, 15 * 256);
                sheet.SetColumnWidth(11, 15 * 256);
                sheet.SetColumnWidth(12, 15 * 256);
                sheet.SetColumnWidth(13, 15 * 256);
                sheet.SetColumnWidth(14, 15 * 256);
                sheet.SetColumnWidth(15, 15 * 256);
                sheet.SetColumnWidth(16, 15 * 256);
                sheet.SetColumnWidth(17, 15 * 256);
                sheet.SetColumnWidth(18, 15 * 256);
                sheet.SetColumnWidth(19, 15 * 256);
                sheet.SetColumnWidth(20, 15 * 256);
                sheet.SetColumnWidth(21, 15 * 256);
                sheet.SetColumnWidth(22, 15 * 256);
                sheet.SetColumnWidth(23, 15 * 256);
                sheet.SetColumnWidth(24, 15 * 256);
                sheet.SetColumnWidth(25, 15 * 256);
                sheet.SetColumnWidth(26, 15 * 256);
                sheet.SetColumnWidth(27, 15 * 256);
                sheet.SetColumnWidth(28, 15 * 256);
                sheet.SetColumnWidth(29, 15 * 256);
                sheet.SetColumnWidth(30, 15 * 256);
                sheet.SetColumnWidth(31, 15 * 256);
                sheet.SetColumnWidth(32, 15 * 256);
                sheet.SetColumnWidth(33, 15 * 256);
                sheet.SetColumnWidth(34, 15 * 256);
                sheet.SetColumnWidth(35, 15 * 256);
                sheet.SetColumnWidth(36, 15 * 256);

                IRow row1 = sheet.CreateRow(0);
                row1.Height = 300;
                ICell cell = row1.CreateCell(0);
                cell.SetCellValue("终端编号");

                cell = row1.CreateCell(1);
                cell.SetCellValue("车牌号");

                cell = row1.CreateCell(2);
                cell.SetCellValue("车主姓名");

                cell = row1.CreateCell(3);
                cell.SetCellValue("终端状态");

                cell = row1.CreateCell(4);
                cell.SetCellValue("所属企业");

                cell = row1.CreateCell(5);
                cell.SetCellValue("工作时间");

                cell = row1.CreateCell(6);
                cell.SetCellValue("设备状态");

                cell = row1.CreateCell(7);
                cell.SetCellValue("回传时间");

                cell = row1.CreateCell(8);
                cell.SetCellValue("停车时长");

                cell = row1.CreateCell(9);
                cell.SetCellValue("联动设备");

                cell = row1.CreateCell(10);
                cell.SetCellValue("最新激活时间");

                cell = row1.CreateCell(11);
                cell.SetCellValue("激活次数");

                cell = row1.CreateCell(12);
                cell.SetCellValue("入网时间");

                cell = row1.CreateCell(13);
                cell.SetCellValue("地理位置");

                cell = row1.CreateCell(14);
                cell.SetCellValue("回传次数");

                cell = row1.CreateCell(15);
                cell.SetCellValue("SIM卡号");

                cell = row1.CreateCell(16);
                cell.SetCellValue("定位时间");

                cell = row1.CreateCell(17);
                cell.SetCellValue("纬度");

                cell = row1.CreateCell(18);
                cell.SetCellValue("经度");

                cell = row1.CreateCell(19);
                cell.SetCellValue("实际定位");

                cell = row1.CreateCell(20);
                cell.SetCellValue("设备软件版本");

                cell = row1.CreateCell(21);
                cell.SetCellValue("设备电池电压");

                cell = row1.CreateCell(22);
                cell.SetCellValue("设备间隔时间");

                cell = row1.CreateCell(23);
                cell.SetCellValue("GSM信号强度");

                cell = row1.CreateCell(24);
                cell.SetCellValue("设备启动次数");

                cell = row1.CreateCell(25);
                cell.SetCellValue("蓝牙绑定");

                cell = row1.CreateCell(26);
                cell.SetCellValue("蓝牙状态");

                cell = row1.CreateCell(27);
                cell.SetCellValue("设备休眠时间");

                cell = row1.CreateCell(28);
                cell.SetCellValue("GPS速度");

                cell = row1.CreateCell(29);
                cell.SetCellValue("可视卫星颗数");

                cell = row1.CreateCell(30);
                cell.SetCellValue("使用卫星颗数");

                cell = row1.CreateCell(31);
                cell.SetCellValue("设备工作模式");

                cell = row1.CreateCell(32);
                cell.SetCellValue("GPS高度（海拔）");

                cell = row1.CreateCell(33);
                cell.SetCellValue("盲区数据标志");

                cell = row1.CreateCell(34);
                cell.SetCellValue("GPS方向");

                cell = row1.CreateCell(35);
                cell.SetCellValue("盲区数据数量");

                cell = row1.CreateCell(36);
                cell.SetCellValue("设备硬件版本");



                // 创建新增行
                if (itd != null && itd.Count > 0)
                {
                    for (int i = 0; i < itd.Count; i++)
                    {
                        row1 = sheet.CreateRow(i + 1);

                        cell = row1.CreateCell(0);
                        cell.SetCellValue(itd[i].TerNo);

                        cell = row1.CreateCell(1);
                        cell.SetCellValue(itd[i].CarNo);

                        cell = row1.CreateCell(2);
                        cell.SetCellValue(itd[i].CarAdminName);

                        cell = row1.CreateCell(3);
                        cell.SetCellValue(itd[i].StateName);

                        cell = row1.CreateCell(4);
                        cell.SetCellValue(itd[i].Businessdivisionname);

                        cell = row1.CreateCell(5);
                        cell.SetCellValue(itd[i].Totalworktime);

                        cell = row1.CreateCell(6);
                        string state = "";
                        if (itd[i].TerStatus == "1")
                        {
                            state = "测试";
                        }
                        else if (itd[i].TerStatus == "2")
                        {
                            state = "待激活";
                        }
                        else if (itd[i].TerStatus == "3")
                        {
                            state = "已激活";
                        }
                        else if (itd[i].TerStatus == "4")
                        {
                            state = "已拆除";
                        }
                        else if (itd[i].TerStatus == "5")
                        {
                            state = "到期";
                        }
                        else if (itd[i].TerStatus == "6")
                        {
                            state = "休眠";
                        }
                        else if (itd[i].TerStatus == "7")
                        {
                            state = "已使用";
                        }
                        else
                        {
                            state = "其他";
                        }
                        cell.SetCellValue(state);

                        cell = row1.CreateCell(7);
                        string Rtimedate = "";
                        if (itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                        {
                            Rtimedate = itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        cell.SetCellValue(Rtimedate);

                        cell = row1.CreateCell(8);
                        cell.SetCellValue(itd[i].StopLong);

                        cell = row1.CreateCell(9);
                        cell.SetCellValue(itd[i].Rawdataid);

                        cell = row1.CreateCell(10);
                        string Acttime = "";
                        if (itd[i].ActTime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                        {
                            Acttime = itd[i].ActTime.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        cell.SetCellValue(Acttime);

                        cell = row1.CreateCell(11);
                        cell.SetCellValue(itd[i].ActCount);

                        cell = row1.CreateCell(12);
                        string innettimedate = "";
                        if (itd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                        {
                            innettimedate = itd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        cell.SetCellValue(innettimedate);

                        cell = row1.CreateCell(13);
                        cell.SetCellValue(itd[i].Position);

                        cell = row1.CreateCell(14);
                        cell.SetCellValue(itd[i].Postbacktimes);

                        cell = row1.CreateCell(15);
                        cell.SetCellValue(itd[i].TerSimcard);

                        cell = row1.CreateCell(16);
                        cell.SetCellValue(itd[i].Positioningtime.ToString("yyyy-MM-dd HH:mm:ss"));

                        cell = row1.CreateCell(17);
                        cell.SetCellValue(itd[i].Latitude);

                        cell = row1.CreateCell(18);
                        cell.SetCellValue(itd[i].Longitude);

                        cell = row1.CreateCell(19);
                        string dingweistr = "";
                        if (itd[i].Ifposition == "定位")
                        {
                            dingweistr = "GPS";
                        }
                        else if (itd[i].Ifposition == "不定位")
                        {
                            if (itd[i].ProName == "一代无线GPS" || itd[i].ProName == "二代无线GPS" || itd[i].ProName == "五代无线GPS")
                            {
                                dingweistr = "基站";
                            }
                            else
                            {
                                dingweistr = "否";
                            }
                        }
                        else
                        {
                            if (itd[i].ProName == "一代无线GPS" || itd[i].ProName == "二代无线GPS" || itd[i].ProName == "五代无线GPS")
                            {
                                dingweistr = "其他";
                            }
                            else
                            {
                                dingweistr = "否";
                            }
                        }
                        cell.SetCellValue(dingweistr);

                        cell = row1.CreateCell(20);
                        cell.SetCellValue(itd[i].Programverson);

                        cell = row1.CreateCell(21);
                        cell.SetCellValue(itd[i].TerVbatt);

                        cell = row1.CreateCell(22);
                        cell.SetCellValue(itd[i].Ntervalltime);

                        cell = row1.CreateCell(23);
                        cell.SetCellValue(itd[i].Gsmrssi);

                        cell = row1.CreateCell(24);
                        cell.SetCellValue(itd[i].TerStatrtimes);

                        cell = row1.CreateCell(25);
                        cell.SetCellValue(itd[i].Northorsouth=="1"?"已绑蓝牙":"");

                        cell = row1.CreateCell(26);
                        string lyzt = "";
                        if (itd[i].Eastorwest == "1")
                        {
                            lyzt = "正常";
                        }
                        else if (itd[i].Eastorwest == "0")
                        {
                            if (itd[i].ProName == "一代无线GPS" || itd[i].ProName == "二代无线GPS" || itd[i].ProName == "五代无线GPS")
                            {
                                lyzt = "有线蓝牙断开";
                            }
                            else
                            {
                                lyzt = "蓝牙断开";
                            }
                        }
                        cell.SetCellValue(lyzt);

                        cell = row1.CreateCell(27);
                        cell.SetCellValue(itd[i].Sleeptime);

                        cell = row1.CreateCell(28);
                        cell.SetCellValue(itd[i].Speed);

                        cell = row1.CreateCell(29);
                        cell.SetCellValue(itd[i].Visualsatellite);

                        cell = row1.CreateCell(30);
                        cell.SetCellValue(itd[i].Usesatellite);

                        cell = row1.CreateCell(31);
                        string termodel = "";
                        if (itd[i].TerModel == "1")
                        {
                            termodel = "标准模式";
                        }
                        else if (itd[i].TerModel == "2")
                        {
                            termodel = "精准模式";
                        }
                        else if (itd[i].TerModel == "3")
                        {
                            termodel = "追车模式";
                        }
                        else
                        {
                            termodel = "其他";
                        }
                        cell.SetCellValue(termodel);

                        cell = row1.CreateCell(32);
                        cell.SetCellValue(itd[i].Height);

                        cell = row1.CreateCell(33);
                        string bliddata = "";
                        if (itd[i].Ifblinddata == "1")
                        {
                            bliddata = "盲区数据";
                        }
                        else
                        {
                            bliddata = "正常数据";
                        }
                        cell.SetCellValue(bliddata);

                        cell = row1.CreateCell(34);
                        cell.SetCellValue(itd[i].Direction);

                        cell = row1.CreateCell(35);
                        cell.SetCellValue(itd[i].Blinddatanum);

                        cell = row1.CreateCell(36);
                        cell.SetCellValue(itd[i].Gpsverson);
                    }
                }
            }
            else
            {
                int endnum = itd.Count % 30000 == 0 ? num : num + 1;
                for (int j = 0; j < endnum; j++)
                {
                    ISheet sheet = book.CreateSheet("终端上传数据信息"+(j+1));
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 15 * 256);
                    sheet.SetColumnWidth(6, 15 * 256);
                    sheet.SetColumnWidth(7, 15 * 256);
                    sheet.SetColumnWidth(8, 15 * 256);
                    sheet.SetColumnWidth(9, 15 * 256);
                    sheet.SetColumnWidth(10, 15 * 256);
                    sheet.SetColumnWidth(11, 15 * 256);
                    sheet.SetColumnWidth(12, 15 * 256);
                    sheet.SetColumnWidth(13, 15 * 256);
                    sheet.SetColumnWidth(14, 15 * 256);
                    sheet.SetColumnWidth(15, 15 * 256);
                    sheet.SetColumnWidth(16, 15 * 256);
                    sheet.SetColumnWidth(17, 15 * 256);
                    sheet.SetColumnWidth(18, 15 * 256);
                    sheet.SetColumnWidth(19, 15 * 256);
                    sheet.SetColumnWidth(20, 15 * 256);
                    sheet.SetColumnWidth(21, 15 * 256);
                    sheet.SetColumnWidth(22, 15 * 256);
                    sheet.SetColumnWidth(23, 15 * 256);
                    sheet.SetColumnWidth(24, 15 * 256);
                    sheet.SetColumnWidth(25, 15 * 256);
                    sheet.SetColumnWidth(26, 15 * 256);
                    sheet.SetColumnWidth(27, 15 * 256);
                    sheet.SetColumnWidth(28, 15 * 256);
                    sheet.SetColumnWidth(29, 15 * 256);
                    sheet.SetColumnWidth(30, 15 * 256);
                    sheet.SetColumnWidth(31, 15 * 256);
                    sheet.SetColumnWidth(32, 15 * 256);
                    sheet.SetColumnWidth(33, 15 * 256);
                    sheet.SetColumnWidth(34, 15 * 256);
                    sheet.SetColumnWidth(35, 15 * 256);
                    sheet.SetColumnWidth(36, 15 * 256);

                    IRow row1 = sheet.CreateRow(0);
                    row1.Height = 300;
                    ICell cell = row1.CreateCell(0);
                    cell.SetCellValue("终端编号");

                    cell = row1.CreateCell(1);
                    cell.SetCellValue("车牌号");

                    cell = row1.CreateCell(2);
                    cell.SetCellValue("车主姓名");

                    cell = row1.CreateCell(3);
                    cell.SetCellValue("终端状态");

                    cell = row1.CreateCell(4);
                    cell.SetCellValue("所属企业");

                    cell = row1.CreateCell(5);
                    cell.SetCellValue("工作时间");

                    cell = row1.CreateCell(6);
                    cell.SetCellValue("设备状态");

                    cell = row1.CreateCell(7);
                    cell.SetCellValue("回传时间");

                    cell = row1.CreateCell(8);
                    cell.SetCellValue("停车时长");

                    cell = row1.CreateCell(9);
                    cell.SetCellValue("联动设备");

                    cell = row1.CreateCell(10);
                    cell.SetCellValue("最新激活时间");

                    cell = row1.CreateCell(11);
                    cell.SetCellValue("激活次数");

                    cell = row1.CreateCell(12);
                    cell.SetCellValue("入网时间");

                    cell = row1.CreateCell(13);
                    cell.SetCellValue("地理位置");

                    cell = row1.CreateCell(14);
                    cell.SetCellValue("回传次数");

                    cell = row1.CreateCell(15);
                    cell.SetCellValue("SIM卡号");

                    cell = row1.CreateCell(16);
                    cell.SetCellValue("定位时间");

                    cell = row1.CreateCell(17);
                    cell.SetCellValue("纬度");

                    cell = row1.CreateCell(18);
                    cell.SetCellValue("经度");

                    cell = row1.CreateCell(19);
                    cell.SetCellValue("实际定位");

                    cell = row1.CreateCell(20);
                    cell.SetCellValue("设备软件版本");

                    cell = row1.CreateCell(21);
                    cell.SetCellValue("设备电池电压");

                    cell = row1.CreateCell(22);
                    cell.SetCellValue("设备间隔时间");

                    cell = row1.CreateCell(23);
                    cell.SetCellValue("GSM信号强度");

                    cell = row1.CreateCell(24);
                    cell.SetCellValue("设备启动次数");

                    cell = row1.CreateCell(25);
                    cell.SetCellValue("蓝牙绑定");

                    cell = row1.CreateCell(26);
                    cell.SetCellValue("蓝牙状态");

                    cell = row1.CreateCell(27);
                    cell.SetCellValue("设备休眠时间");

                    cell = row1.CreateCell(28);
                    cell.SetCellValue("GPS速度");

                    cell = row1.CreateCell(29);
                    cell.SetCellValue("可视卫星颗数");

                    cell = row1.CreateCell(30);
                    cell.SetCellValue("使用卫星颗数");

                    cell = row1.CreateCell(31);
                    cell.SetCellValue("设备工作模式");

                    cell = row1.CreateCell(32);
                    cell.SetCellValue("GPS高度（海拔）");

                    cell = row1.CreateCell(33);
                    cell.SetCellValue("盲区数据标志");

                    cell = row1.CreateCell(34);
                    cell.SetCellValue("GPS方向");

                    cell = row1.CreateCell(35);
                    cell.SetCellValue("盲区数据数量");

                    cell = row1.CreateCell(36);
                    cell.SetCellValue("设备硬件版本");

                    // 创建新增行
                    if (itd != null && itd.Count > 0)
                    {
                        int EndIndex = 0;
                        if (j == num)
                        {
                            EndIndex = itd.Count;
                        }
                        else
                        {
                            EndIndex = (j + 1) * 30000;
                        }
                        for (int i = j * 30000; i < EndIndex; i++)
                        {
                            int rownum = i % 30000;
                            row1 = sheet.CreateRow(rownum + 1);

                            cell = row1.CreateCell(0);
                            cell.SetCellValue(itd[i].TerNo);

                            cell = row1.CreateCell(1);
                            cell.SetCellValue(itd[i].CarNo);

                            cell = row1.CreateCell(2);
                            cell.SetCellValue(itd[i].CarAdminName);

                            cell = row1.CreateCell(3);
                            cell.SetCellValue(itd[i].StateName);

                            cell = row1.CreateCell(4);
                            cell.SetCellValue(itd[i].Businessdivisionname);

                            cell = row1.CreateCell(5);
                            cell.SetCellValue(itd[i].Totalworktime);

                            cell = row1.CreateCell(6);
                            string state = "";
                            if (itd[i].TerStatus == "1")
                            {
                                state = "测试";
                            }
                            else if (itd[i].TerStatus == "2")
                            {
                                state = "待激活";
                            }
                            else if (itd[i].TerStatus == "3")
                            {
                                state = "已激活";
                            }
                            else if (itd[i].TerStatus == "4")
                            {
                                state = "已拆除";
                            }
                            else if (itd[i].TerStatus == "5")
                            {
                                state = "到期";
                            }
                            else if (itd[i].TerStatus == "6")
                            {
                                state = "休眠";
                            }
                            else if (itd[i].TerStatus == "7")
                            {
                                state = "已使用";
                            }
                            else
                            {
                                state = "其他";
                            }
                            cell.SetCellValue(state);


                            cell = row1.CreateCell(7);
                            string Rtimedate = "";
                            if (itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                            {
                                Rtimedate = itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            cell.SetCellValue(Rtimedate);

                            cell = row1.CreateCell(8);
                            cell.SetCellValue(itd[i].StopLong);

                            cell = row1.CreateCell(9);
                            cell.SetCellValue(itd[i].Rawdataid);

                            cell = row1.CreateCell(10);
                            string Acttime = "";
                            if (itd[i].ActTime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                            {
                                Acttime = itd[i].ActTime.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            cell.SetCellValue(Acttime);

                            cell = row1.CreateCell(11);
                            cell.SetCellValue(itd[i].ActCount);

                            cell = row1.CreateCell(12);
                            string innettimedate = "";
                            if (itd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                            {
                                innettimedate = itd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            cell.SetCellValue(innettimedate);

                            cell = row1.CreateCell(13);
                            cell.SetCellValue(itd[i].Position);

                            cell = row1.CreateCell(14);
                            cell.SetCellValue(itd[i].Postbacktimes);

                            cell = row1.CreateCell(15);
                            cell.SetCellValue(itd[i].TerSimcard);

                            cell = row1.CreateCell(16);
                            cell.SetCellValue(itd[i].Positioningtime.ToString("yyyy-MM-dd HH:mm:ss"));

                            cell = row1.CreateCell(17);
                            cell.SetCellValue(itd[i].Latitude);

                            cell = row1.CreateCell(18);
                            cell.SetCellValue(itd[i].Longitude);

                            cell = row1.CreateCell(19);
                            string dingweistr = "";
                            if (itd[i].Ifposition == "定位")
                            {
                                dingweistr = "GPS";
                            }
                            else if (itd[i].Ifposition == "不定位")
                            {
                                if (itd[i].ProName == "一代无线GPS" || itd[i].ProName == "二代无线GPS" || itd[i].ProName == "五代无线GPS")
                                {
                                    dingweistr = "基站";
                                }
                                else
                                {
                                    dingweistr = "暂无";
                                }
                            }
                            else
                            {
                                dingweistr = "其他";
                            }
                            cell.SetCellValue(dingweistr);

                            cell = row1.CreateCell(20);
                            cell.SetCellValue(itd[i].Programverson);

                            cell = row1.CreateCell(21);
                            cell.SetCellValue(itd[i].TerVbatt);

                            cell = row1.CreateCell(22);
                            cell.SetCellValue(itd[i].Ntervalltime);

                            cell = row1.CreateCell(23);
                            cell.SetCellValue(itd[i].Gsmrssi);

                            cell = row1.CreateCell(24);
                            cell.SetCellValue(itd[i].TerStatrtimes);

                            cell = row1.CreateCell(25);
                            cell.SetCellValue(itd[i].Northorsouth == "1" ? "已绑蓝牙" : "");

                            cell = row1.CreateCell(26);
                            string lyzt = "";
                            if (itd[i].Eastorwest == "1")
                            {
                                lyzt = "正常";
                            }
                            else if (itd[i].Eastorwest == "0")
                            {
                                if (itd[i].ProName == "一代无线GPS" || itd[i].ProName == "二代无线GPS" || itd[i].ProName == "五代无线GPS")
                                {
                                    lyzt = "有线蓝牙断开";
                                }
                                else
                                {
                                    lyzt = "蓝牙断开";
                                }
                            }
                            cell.SetCellValue(lyzt);

                            cell = row1.CreateCell(27);
                            cell.SetCellValue(itd[i].Sleeptime);

                            cell = row1.CreateCell(28);
                            cell.SetCellValue(itd[i].Speed);

                            cell = row1.CreateCell(29);
                            cell.SetCellValue(itd[i].Visualsatellite);

                            cell = row1.CreateCell(30);
                            cell.SetCellValue(itd[i].Usesatellite);

                            cell = row1.CreateCell(31);
                            string termodel = "";
                            if (itd[i].TerModel == "1")
                            {
                                termodel = "标准模式";
                            }
                            else if (itd[i].TerModel == "2")
                            {
                                termodel = "精准模式";
                            }
                            else if (itd[i].TerModel == "3")
                            {
                                termodel = "追车模式";
                            }
                            else
                            {
                                termodel = "其他";
                            }
                            cell.SetCellValue(termodel);

                            cell = row1.CreateCell(32);
                            cell.SetCellValue(itd[i].Height);

                            cell = row1.CreateCell(33);
                            string bliddata = "";
                            if (itd[i].Ifblinddata == "1")
                            {
                                bliddata = "盲区数据";
                            }
                            else
                            {
                                bliddata = "正常数据";
                            }
                            cell.SetCellValue(bliddata);

                            cell = row1.CreateCell(34);
                            cell.SetCellValue(itd[i].Direction);

                            cell = row1.CreateCell(35);
                            cell.SetCellValue(itd[i].Blinddatanum);

                            cell = row1.CreateCell(36);
                            cell.SetCellValue(itd[i].Gpsverson);
                        }
                    }
                }
            }
            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 终端上传处数据(普通用户)
        /// </summary>
        /// <param name="itd"></param>
        /// <returns></returns>
        public MemoryStream CreateExcelTerDataByUser(IList<TerData> itd)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            int num = itd.Count / 30000;
            if (num == 0)
            {
                ISheet sheet = book.CreateSheet("终端上传数据信息");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 15 * 256);
                sheet.SetColumnWidth(5, 15 * 256);
                sheet.SetColumnWidth(6, 15 * 256);
                sheet.SetColumnWidth(7, 15 * 256);
                sheet.SetColumnWidth(8, 15 * 256);
                sheet.SetColumnWidth(9, 15 * 256);

                IRow row1 = sheet.CreateRow(0);
                row1.Height = 300;
                ICell cell = row1.CreateCell(0);
                cell.SetCellValue("终端编号");

                cell = row1.CreateCell(1);
                cell.SetCellValue("车牌号");

                cell = row1.CreateCell(2);
                cell.SetCellValue("车主姓名");

                cell = row1.CreateCell(3);
                cell.SetCellValue("终端状态");

                cell = row1.CreateCell(4);
                cell.SetCellValue("所属企业");

                cell = row1.CreateCell(5);
                cell.SetCellValue("设备状态");

                cell = row1.CreateCell(6);
                cell.SetCellValue("回传时间");

                cell = row1.CreateCell(7);
                cell.SetCellValue("地理位置");

                cell = row1.CreateCell(8);
                cell.SetCellValue("回传次数");

                cell = row1.CreateCell(9);
                cell.SetCellValue("实际定位");



                // 创建新增行
                if (itd != null && itd.Count > 0)
                {
                    for (int i = 0; i < itd.Count; i++)
                    {
                        row1 = sheet.CreateRow(i + 1);

                        cell = row1.CreateCell(0);
                        cell.SetCellValue(itd[i].TerNo);

                        cell = row1.CreateCell(1);
                        cell.SetCellValue(itd[i].CarNo);

                        cell = row1.CreateCell(2);
                        cell.SetCellValue(itd[i].CarAdminName);

                        cell = row1.CreateCell(3);
                        cell.SetCellValue(itd[i].StateName);

                        cell = row1.CreateCell(4);
                        cell.SetCellValue(itd[i].Businessdivisionname);

                        cell = row1.CreateCell(5);
                        string state = "";
                        if (itd[i].TerStatus == "1")
                        {
                            state = "测试";
                        }
                        else if (itd[i].TerStatus == "2")
                        {
                            state = "待激活";
                        }
                        else if (itd[i].TerStatus == "3")
                        {
                            state = "已激活";
                        }
                        else if (itd[i].TerStatus == "4")
                        {
                            state = "已拆除";
                        }
                        else if (itd[i].TerStatus == "5")
                        {
                            state = "到期";
                        }
                        else if (itd[i].TerStatus == "6")
                        {
                            state = "休眠";
                        }
                        else if (itd[i].TerStatus == "7")
                        {
                            state = "已使用";
                        }
                        else
                        {
                            state = "其他";
                        }
                        cell.SetCellValue(state);

                        cell = row1.CreateCell(6);
                        string Rtimedate = "";
                        if (itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                        {
                            Rtimedate = itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        cell.SetCellValue(Rtimedate);

                        cell = row1.CreateCell(7);
                        cell.SetCellValue(itd[i].Position);

                        cell = row1.CreateCell(8);
                        cell.SetCellValue(itd[i].Postbacktimes);

                        cell = row1.CreateCell(9);
                        string dingweistr = "";
                        if (itd[i].Ifposition == "定位")
                        {
                            dingweistr = "GPS";
                        }
                        else if (itd[i].Ifposition == "不定位")
                        {
                            if (itd[i].ProName == "一代无线GPS" || itd[i].ProName == "二代无线GPS" || itd[i].ProName == "五代无线GPS")
                            {
                                dingweistr = "基站";
                            }
                            else
                            {
                                dingweistr = "否";
                            }
                        }
                        else
                        {
                            if (itd[i].ProName == "一代无线GPS" || itd[i].ProName == "二代无线GPS" || itd[i].ProName == "五代无线GPS")
                            {
                                dingweistr = "其他";
                            }
                            else
                            {
                                dingweistr = "否";
                            }
                        }
                        cell.SetCellValue(dingweistr);
                    }
                }
            }
            else
            {
                int endnum = itd.Count % 30000 == 0 ? num : num + 1;
                for (int j = 0; j < endnum; j++)
                {
                    ISheet sheet = book.CreateSheet("终端上传数据信息" + (j + 1));
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 15 * 256);
                    sheet.SetColumnWidth(5, 15 * 256);
                    sheet.SetColumnWidth(6, 15 * 256);
                    sheet.SetColumnWidth(7, 15 * 256);
                    sheet.SetColumnWidth(8, 15 * 256);
                    sheet.SetColumnWidth(9, 15 * 256);

                    IRow row1 = sheet.CreateRow(0);
                    row1.Height = 300;
                    ICell cell = row1.CreateCell(0);
                    cell.SetCellValue("终端编号");

                    cell = row1.CreateCell(1);
                    cell.SetCellValue("车牌号");

                    cell = row1.CreateCell(2);
                    cell.SetCellValue("车主姓名");

                    cell = row1.CreateCell(3);
                    cell.SetCellValue("终端状态");

                    cell = row1.CreateCell(4);
                    cell.SetCellValue("所属企业");

                    cell = row1.CreateCell(5);
                    cell.SetCellValue("设备状态");

                    cell = row1.CreateCell(6);
                    cell.SetCellValue("回传时间");

                    cell = row1.CreateCell(7);
                    cell.SetCellValue("地理位置");

                    cell = row1.CreateCell(8);
                    cell.SetCellValue("回传次数");

                    cell = row1.CreateCell(9);
                    cell.SetCellValue("实际定位");


                    // 创建新增行
                    if (itd != null && itd.Count > 0)
                    {
                        int EndIndex = 0;
                        if (j == num)
                        {
                            EndIndex = itd.Count;
                        }
                        else
                        {
                            EndIndex = (j + 1) * 30000;
                        }
                        for (int i = j * 30000; i < EndIndex; i++)
                        {
                            int rownum = i % 30000;
                            row1 = sheet.CreateRow(rownum + 1);

                            cell = row1.CreateCell(0);
                            cell.SetCellValue(itd[i].TerNo);

                            cell = row1.CreateCell(1);
                            cell.SetCellValue(itd[i].CarNo);

                            cell = row1.CreateCell(2);
                            cell.SetCellValue(itd[i].CarAdminName);

                            cell = row1.CreateCell(3);
                            cell.SetCellValue(itd[i].StateName);

                            cell = row1.CreateCell(4);
                            cell.SetCellValue(itd[i].Businessdivisionname);

                            cell = row1.CreateCell(5);
                            string state = "";
                            if (itd[i].TerStatus == "1")
                            {
                                state = "测试";
                            }
                            else if (itd[i].TerStatus == "2")
                            {
                                state = "待激活";
                            }
                            else if (itd[i].TerStatus == "3")
                            {
                                state = "已激活";
                            }
                            else if (itd[i].TerStatus == "4")
                            {
                                state = "已拆除";
                            }
                            else if (itd[i].TerStatus == "5")
                            {
                                state = "到期";
                            }
                            else if (itd[i].TerStatus == "6")
                            {
                                state = "休眠";
                            }
                            else if (itd[i].TerStatus == "7")
                            {
                                state = "已使用";
                            }
                            else
                            {
                                state = "其他";
                            }
                            cell.SetCellValue(state);


                            cell = row1.CreateCell(6);
                            string Rtimedate = "";
                            if (itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                            {
                                Rtimedate = itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            cell.SetCellValue(Rtimedate);

                            cell = row1.CreateCell(7);
                            cell.SetCellValue(itd[i].Position);

                            cell = row1.CreateCell(8);
                            cell.SetCellValue(itd[i].Postbacktimes); 

                            cell = row1.CreateCell(9);
                            string dingweistr = "";
                            if (itd[i].Ifposition == "定位")
                            {
                                dingweistr = "GPS";
                            }
                            else if (itd[i].Ifposition == "不定位")
                            {
                                if (itd[i].ProName == "一代无线GPS" || itd[i].ProName == "二代无线GPS" || itd[i].ProName == "五代无线GPS")
                                {
                                    dingweistr = "基站";
                                }
                                else
                                {
                                    dingweistr = "暂无";
                                }
                            }
                            else
                            {
                                dingweistr = "其他";
                            }
                            cell.SetCellValue(dingweistr);                          
                        }
                    }
                }
            }
            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 终端报表处数据
        /// </summary>
        /// <param name="itd"></param>
        /// <returns></returns>
        public MemoryStream CreateExcelTerReportData(IList<CarReport_TerData> itd)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            int num = itd.Count / 50000;
            if (num == 0)
            {
                ISheet sheet = book.CreateSheet("终端报表数据信息");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 60 * 256);
                sheet.SetColumnWidth(5, 15 * 256);
                sheet.SetColumnWidth(6, 15 * 256);
                sheet.SetColumnWidth(7, 15 * 256);
                sheet.SetColumnWidth(8, 15 * 256);

                IRow row1 = sheet.CreateRow(0);
                row1.Height = 300;
                ICell cell = row1.CreateCell(0);
                cell.SetCellValue("终端编号");

                cell = row1.CreateCell(1);
                cell.SetCellValue("车牌号");

                cell = row1.CreateCell(2);
                cell.SetCellValue("回传时间");

                cell = row1.CreateCell(3);
                cell.SetCellValue("回传条数");

                cell = row1.CreateCell(4);
                cell.SetCellValue("回传位置");

                cell = row1.CreateCell(5);
                cell.SetCellValue("入网时间");

                cell = row1.CreateCell(6);
                cell.SetCellValue("SIM卡号");

                cell = row1.CreateCell(7);
                cell.SetCellValue("终端状态");

                cell = row1.CreateCell(8);
                cell.SetCellValue("终端类型");

                // 创建新增行
                if (itd != null && itd.Count > 0)
                {
                    for (int i = 0; i < itd.Count; i++)
                    {
                        row1 = sheet.CreateRow(i + 1);

                        cell = row1.CreateCell(0);
                        cell.SetCellValue(itd[i].TerNo);

                        cell = row1.CreateCell(1);
                        cell.SetCellValue(itd[i].CarNo);

                        cell = row1.CreateCell(2);
                        string Rtimedate = "";
                        if (itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                        {
                            Rtimedate = itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        cell.SetCellValue(Rtimedate);

                        cell = row1.CreateCell(3);
                        cell.SetCellValue(itd[i].Postbacktimes);

                        cell = row1.CreateCell(4);
                        cell.SetCellValue(itd[i].Position);

                        cell = row1.CreateCell(5);
                        string innettimedate = "";
                        if (itd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                        {
                            innettimedate = itd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        cell.SetCellValue(innettimedate);

                        cell = row1.CreateCell(6);
                        cell.SetCellValue(itd[i].TerSimcard);

                        cell = row1.CreateCell(7);
                        cell.SetCellValue(itd[i].StateName);

                        cell = row1.CreateCell(8);
                        cell.SetCellValue(itd[i].ProName);                     
                    }
                }
            }
            else
            {
                for (int j = 0; j < num + 1; j++)
                {
                    ISheet sheet = book.CreateSheet("终端报表数据信息" + (j + 1));
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 60 * 256);
                    sheet.SetColumnWidth(5, 15 * 256);
                    sheet.SetColumnWidth(6, 15 * 256);
                    sheet.SetColumnWidth(7, 15 * 256);
                    sheet.SetColumnWidth(8, 15 * 256);

                    IRow row1 = sheet.CreateRow(0);
                    row1.Height = 300;
                    ICell cell = row1.CreateCell(0);
                    cell.SetCellValue("终端编号");

                    cell = row1.CreateCell(1);
                    cell.SetCellValue("车牌号");

                    cell = row1.CreateCell(2);
                    cell.SetCellValue("回传时间");

                    cell = row1.CreateCell(3);
                    cell.SetCellValue("回传条数");

                    cell = row1.CreateCell(4);
                    cell.SetCellValue("回传位置");

                    cell = row1.CreateCell(5);
                    cell.SetCellValue("入网时间");

                    cell = row1.CreateCell(6);
                    cell.SetCellValue("SIM卡号");

                    cell = row1.CreateCell(7);
                    cell.SetCellValue("终端状态");

                    cell = row1.CreateCell(8);
                    cell.SetCellValue("终端类型");

                    // 创建新增行
                    if (itd != null && itd.Count > 0)
                    {
                        int EndIndex = 0;
                        if (j == num)
                        {
                            EndIndex = itd.Count;
                        }
                        else
                        {
                            EndIndex = (j + 1) * 50000;
                        }
                        for (int i = j * 50000; i < EndIndex; i++)
                        {
                            int rownum = i % 50000;
                            row1 = sheet.CreateRow(rownum + 1);

                            cell = row1.CreateCell(0);
                            cell.SetCellValue(itd[i].TerNo);

                            cell = row1.CreateCell(1);
                            cell.SetCellValue(itd[i].CarNo);

                            cell = row1.CreateCell(2);
                            string Rtimedate = "";
                            if (itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                            {
                                Rtimedate = itd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            cell.SetCellValue(Rtimedate);

                            cell = row1.CreateCell(3);
                            cell.SetCellValue(itd[i].Postbacktimes);

                            cell = row1.CreateCell(4);
                            cell.SetCellValue(itd[i].Position);

                            cell = row1.CreateCell(5);
                            string innettimedate = "";
                            if (itd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                            {
                                innettimedate = itd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            cell.SetCellValue(innettimedate);

                            cell = row1.CreateCell(6);
                            cell.SetCellValue(itd[i].TerSimcard);

                            cell = row1.CreateCell(7);
                            cell.SetCellValue(itd[i].StateName);

                            cell = row1.CreateCell(8);
                            cell.SetCellValue(itd[i].ProName);                   
                        }
                    }
                }
            }
            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 终端上传处历史轨迹导出(有线)
        /// </summary>
        /// <param name="itd"></param>
        /// <returns></returns>
        public MemoryStream CreateExcel(IList<YXHistoricalData> iyxtd)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("历史轨迹信息");
            sheet.SetColumnWidth(0, 20 * 256);
            sheet.SetColumnWidth(1, 20 * 256);
            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 20 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 60 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            sheet.SetColumnWidth(9, 15 * 256);
            sheet.SetColumnWidth(10, 15 * 256);
            sheet.SetColumnWidth(11, 15 * 256);
            sheet.SetColumnWidth(12, 15 * 256);
            sheet.SetColumnWidth(13, 15 * 256);
            sheet.SetColumnWidth(14, 15 * 256);
            sheet.SetColumnWidth(15, 15 * 256);
            sheet.SetColumnWidth(16, 15 * 256);

            IRow row1 = sheet.CreateRow(0);
            row1.Height = 300;
            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("终端编号");

            cell = row1.CreateCell(1);
            cell.SetCellValue("SIM卡号");

            cell = row1.CreateCell(2);
            cell.SetCellValue("报警状态");

            cell = row1.CreateCell(3);
            cell.SetCellValue("回传时间");

            cell = row1.CreateCell(4);
            cell.SetCellValue("入网时间");

            cell = row1.CreateCell(5);
            cell.SetCellValue("地理位置");

            cell = row1.CreateCell(6);
            cell.SetCellValue("纬度");

            cell = row1.CreateCell(7);
            cell.SetCellValue("经度");

            cell = row1.CreateCell(8);
            cell.SetCellValue("设备软件版本");

            cell = row1.CreateCell(9);
            cell.SetCellValue("设备电池电压");

            cell = row1.CreateCell(10);
            cell.SetCellValue("GSM信号强度");

            cell = row1.CreateCell(11);
            cell.SetCellValue("蓝牙绑定");

            cell = row1.CreateCell(12);
            cell.SetCellValue("蓝牙状态");

            cell = row1.CreateCell(13);
            cell.SetCellValue("GPS速度");

            cell = row1.CreateCell(14);
            cell.SetCellValue("实际定位");

            cell = row1.CreateCell(15);
            cell.SetCellValue("GPS方向");

            cell = row1.CreateCell(16);
            cell.SetCellValue("设备硬件版本");

            // 创建新增行
            if (iyxtd != null && iyxtd.Count > 0)
            {
                for (int i = 0; i < iyxtd.Count; i++)
                {
                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    cell.SetCellValue(iyxtd[i].TerNo);

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(iyxtd[i].TerSimcard);

                    cell = row1.CreateCell(2);
                    string WarnState = "";
                    if (iyxtd[i].ReplydataName == "")
                    {
                        WarnState = "正常";
                    }
                    else
                    {
                        WarnState = iyxtd[i].ReplydataName;
                    }
                    cell.SetCellValue(WarnState);

                    cell = row1.CreateCell(3);
                    string Rtimedate = "";
                    if (iyxtd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                    {
                        Rtimedate = iyxtd[i].Rtime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    cell.SetCellValue(Rtimedate);

                    cell = row1.CreateCell(4);
                    string innettimedate = "";
                    if (iyxtd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                    {
                        innettimedate = iyxtd[i].Ter_Innettime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    cell.SetCellValue(innettimedate);

                    cell = row1.CreateCell(5);
                    cell.SetCellValue(iyxtd[i].Position);

                    cell = row1.CreateCell(6);
                    cell.SetCellValue(iyxtd[i].Latitude);

                    cell = row1.CreateCell(7);
                    cell.SetCellValue(iyxtd[i].Longitude);

                    cell = row1.CreateCell(8);
                    cell.SetCellValue(iyxtd[i].Programverson);

                    cell = row1.CreateCell(9);
                    cell.SetCellValue(iyxtd[i].TerVbatt);

                    cell = row1.CreateCell(10);
                    cell.SetCellValue(iyxtd[i].Gsmrssi);

                    cell = row1.CreateCell(11);
                    cell.SetCellValue(iyxtd[i].Northorsouth == "1" ? "已绑蓝牙" : "");

                    cell = row1.CreateCell(12);
                    string lyzt = "";
                    if (iyxtd[i].Eastorwest == "1")
                    {
                        lyzt = "正常";
                    }
                    else if (iyxtd[i].Eastorwest == "0")
                    {
                        lyzt = "蓝牙断开";
                    }
                    cell.SetCellValue(lyzt);

                    cell = row1.CreateCell(13);
                    cell.SetCellValue(iyxtd[i].Speed);


                    cell = row1.CreateCell(14);
                    string dingweistr = "";
                    if (iyxtd[i].Ifposition == "定位")
                    {
                        dingweistr = "GPS";
                    }
                    else if (iyxtd[i].Ifposition == "不定位")
                    {
                        dingweistr = "否";
                    }
                    else
                    {
                        dingweistr = "否";
                    }
                    cell.SetCellValue(dingweistr);


                    cell = row1.CreateCell(15);
                    cell.SetCellValue(iyxtd[i].Direction);

                    cell = row1.CreateCell(16);
                    cell.SetCellValue(iyxtd[i].Gpsverson);
                }
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        public MemoryStream CreateTerExcel(IList<CarReport_LCTJView> TerInfo)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("超里程车辆统计信息");

            sheet.SetColumnWidth(0, 15 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 80 * 256);
            sheet.SetColumnWidth(4, 13 * 256);

            IRow row1 = sheet.CreateRow(0);
            row1.Height = 300;

            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("回传时间");

            cell = row1.CreateCell(1);
            cell.SetCellValue("车牌号");

            cell = row1.CreateCell(2);
            cell.SetCellValue("终端编号");

            cell = row1.CreateCell(3);
            cell.SetCellValue("里程数(公里)");

            cell = row1.CreateCell(4);
            cell.SetCellValue("当前位置");

            // 创建新增行
            if (TerInfo != null && TerInfo.Count > 0)
            {
                for (int i = 0; i < TerInfo.Count; i++)
                {
                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    cell.SetCellValue(TerInfo[i].Rtime);

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(TerInfo[i].CarNo);

                    cell = row1.CreateCell(2);
                    cell.SetCellValue(TerInfo[i].TerNo);

                    cell = row1.CreateCell(3);
                    cell.SetCellValue(TerInfo[i].LCTJ.ToString());

                    cell = row1.CreateCell(4);
                    cell.SetCellValue(TerInfo[i].Position);
                }
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 终端管理导出EXCEL
        /// </summary>
        /// <param name="TerInfo"></param>
        /// <returns></returns>
        public MemoryStream CreateTerExcel(IList<TerminalInfoView> TerInfo)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            int num = TerInfo.Count / 50000;
            if (num == 0)
            {
                ISheet sheet = book.CreateSheet("终端信息");
                sheet.SetColumnWidth(0, 15 * 256);
                sheet.SetColumnWidth(1, 15 * 256);
                sheet.SetColumnWidth(2, 80 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 15 * 256);
                sheet.SetColumnWidth(5, 20 * 256);
                sheet.SetColumnWidth(6, 15 * 256);
                sheet.SetColumnWidth(7, 15 * 256);

                IRow row1 = sheet.CreateRow(0);
                row1.Height = 300;

                ICell cell = row1.CreateCell(0);
                cell.SetCellValue("终端编号");

                //cell = row1.CreateCell(1);
                //cell.SetCellValue("终端型号");

                cell = row1.CreateCell(1);
                cell.SetCellValue("终端类型");

                cell = row1.CreateCell(2);
                cell.SetCellValue("所属企业");

                cell = row1.CreateCell(3);
                cell.SetCellValue("SIM卡");

                cell = row1.CreateCell(4);
                cell.SetCellValue("入网时间");

                cell = row1.CreateCell(5);
                cell.SetCellValue("车牌号");

                cell = row1.CreateCell(6);
                cell.SetCellValue("车主姓名");

                cell = row1.CreateCell(7);
                cell.SetCellValue("里程标准(公里)");

                // 创建新增行
                if (TerInfo != null && TerInfo.Count > 0)
                {
                    for (int i = 0; i < TerInfo.Count; i++)
                    {
                        row1 = sheet.CreateRow(i + 1);

                        cell = row1.CreateCell(0);
                        cell.SetCellValue(TerInfo[i].TerNo);

                        //cell = row1.CreateCell(1);
                        //cell.SetCellValue(TerInfo[i].ProModel);

                        cell = row1.CreateCell(1);
                        cell.SetCellValue(TerInfo[i].ProName);

                        cell = row1.CreateCell(2);
                        cell.SetCellValue(TerInfo[i].Businessdivisionname);

                        cell = row1.CreateCell(3);
                        cell.SetCellValue(TerInfo[i].TerSimcard);

                        cell = row1.CreateCell(4);
                        cell.SetCellValue(TerInfo[i].TerInnettime.ToString("yyyy-MM-dd HH:mm:ss"));

                        cell = row1.CreateCell(5);
                        cell.SetCellValue(TerInfo[i].CarNo);

                        cell = row1.CreateCell(6);
                        cell.SetCellValue(TerInfo[i].CarAdminName);

                        cell = row1.CreateCell(7);
                        cell.SetCellValue(TerInfo[i].TerPctline);
                    }
                }
            }
            else
            {
                for (int j = 0; j < num + 1; j++)
                {
                    ISheet sheet = book.CreateSheet("终端信息" + (j + 1));
                    sheet.SetColumnWidth(0, 15 * 256);
                    sheet.SetColumnWidth(1, 15 * 256);
                    sheet.SetColumnWidth(2, 80 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 15 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 15 * 256);
                    sheet.SetColumnWidth(7, 15 * 256);

                    IRow row1 = sheet.CreateRow(0);
                    row1.Height = 300;

                    ICell cell = row1.CreateCell(0);
                    cell.SetCellValue("终端编号");

                    //cell = row1.CreateCell(1);
                    //cell.SetCellValue("终端型号");

                    cell = row1.CreateCell(1);
                    cell.SetCellValue("终端类型");

                    cell = row1.CreateCell(2);
                    cell.SetCellValue("所属企业");

                    cell = row1.CreateCell(3);
                    cell.SetCellValue("SIM卡");

                    cell = row1.CreateCell(4);
                    cell.SetCellValue("入网时间");

                    cell = row1.CreateCell(5);
                    cell.SetCellValue("车牌号");

                    cell = row1.CreateCell(6);
                    cell.SetCellValue("车主姓名");

                    cell = row1.CreateCell(7);
                    cell.SetCellValue("里程标准(公里)");

                    // 创建新增行
                    if (TerInfo != null && TerInfo.Count > 0)
                    {
                        int EndIndex = 0;
                        if (j == num)
                        {
                            EndIndex = TerInfo.Count;
                        }
                        else
                        {
                            EndIndex = (j + 1) * 50000;
                        }
                        for (int i = j * 50000; i < EndIndex; i++)
                        {
                            int rownum = i % 50000;
                            row1 = sheet.CreateRow(rownum + 1);
                            cell = row1.CreateCell(0);
                            cell.SetCellValue(TerInfo[i].TerNo);

                            //cell = row1.CreateCell(1);
                            //cell.SetCellValue(TerInfo[i].ProModel);

                            cell = row1.CreateCell(1);
                            cell.SetCellValue(TerInfo[i].ProName);

                            cell = row1.CreateCell(2);
                            cell.SetCellValue(TerInfo[i].Businessdivisionname);

                            cell = row1.CreateCell(3);
                            cell.SetCellValue(TerInfo[i].TerSimcard);

                            cell = row1.CreateCell(4);
                            cell.SetCellValue(TerInfo[i].TerInnettime.ToString("yyyy-MM-dd HH:mm:ss"));

                            cell = row1.CreateCell(5);
                            cell.SetCellValue(TerInfo[i].CarNo);

                            cell = row1.CreateCell(6);
                            cell.SetCellValue(TerInfo[i].CarAdminName);

                            cell = row1.CreateCell(7);
                            cell.SetCellValue(TerInfo[i].TerPctline);

                        }
                    }
                }
            }
            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 里程统计导出EXCEL
        /// </summary>
        /// <param name="TerInfo"></param>
        /// <returns></returns>
        public MemoryStream CreateRptLctjToExcel(IList<CarReport_LCTJView> RptDatas)
        {
            XSSFWorkbook book = new XSSFWorkbook();

            ISheet sheet = book.CreateSheet("里程统计信息");

            sheet.SetColumnWidth(0, 15 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 40 * 256);
            sheet.SetColumnWidth(4, 15 * 256);
            IRow row1 = sheet.CreateRow(0);

            row1.Height = 300;

            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("终端编号");

            cell = row1.CreateCell(1);
            cell.SetCellValue("车牌号");

            cell = row1.CreateCell(2);
            cell.SetCellValue("回传时间");

            cell = row1.CreateCell(3);
            cell.SetCellValue("回传位置");

            cell = row1.CreateCell(4);
            cell.SetCellValue("里程数(km)");

            // 创建新增行
            if (RptDatas != null && RptDatas.Count > 0)
            {
                for (int i = 0; i < RptDatas.Count; i++)
                {
                    CarReport_LCTJView row = (CarReport_LCTJView)RptDatas[i];

                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    cell.SetCellValue(row.TerNo);

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(row.CarNo);

                    cell = row1.CreateCell(2);
                    cell.SetCellValue(row.Rtime);

                    cell = row1.CreateCell(3);
                    cell.SetCellValue(row.Position);

                    cell = row1.CreateCell(4);
                    cell.SetCellValue(row.LCTJ.ToString());

                }
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 报警统计导出EXCEL
        /// </summary>
        /// <param name="RptDatas"></param>
        /// <returns></returns>
        public MemoryStream CreateRptBjtjToExcel(IList<CarReport_BJView> RptDatas)
        {
            XSSFWorkbook book = new XSSFWorkbook();

            ISheet sheet = book.CreateSheet("里程统计信息");

            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 10 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 30 * 256);

            IRow row1 = sheet.CreateRow(0);

            row1.Height = 300;

            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("终端编号");

            cell = row1.CreateCell(1);
            cell.SetCellValue("车牌号");

            cell = row1.CreateCell(2);
            cell.SetCellValue("设备状态");

            cell = row1.CreateCell(3);
            cell.SetCellValue("报警状态");

            cell = row1.CreateCell(4);
            cell.SetCellValue("回传时间");

            cell = row1.CreateCell(5);
            cell.SetCellValue("回传位置");

            // 创建新增行
            if (RptDatas != null && RptDatas.Count > 0)
            {
                for (int i = 0; i < RptDatas.Count; i++)
                {
                    CarReport_BJView row = (CarReport_BJView)RptDatas[i];

                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    cell.SetCellValue(row.TerNo);

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(row.CarNo);

                    cell = row1.CreateCell(2);
                    cell.SetCellValue(row.TerStatus);

                    cell = row1.CreateCell(3);
                    cell.SetCellValue(row.ReplyDataName);

                    cell = row1.CreateCell(4);
                    cell.SetCellValue(row.HCSJ.ToString());

                    cell = row1.CreateCell(5);
                    cell.SetCellValue(row.Position.ToString());

                }
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 终端统计导出EXCEL
        /// </summary>
        /// <param name="RptDatas"></param>
        /// <param name="TerType">终端类型：有线、无线</param>
        /// <returns></returns>
        public MemoryStream CreateRptZdtjToExcel(IList<CarReport_ZDTJView> RptDatas, string TerType)
        {
            XSSFWorkbook book = new XSSFWorkbook();

            switch (TerType)
            {
                case "yxzd":
                    book = CreateRptZdtjToExcel_YX(RptDatas);
                    break;
                case "wxzd":
                    book = CreateRptZdtjToExcel_WX(RptDatas);
                    break;
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }
        private XSSFWorkbook CreateRptZdtjToExcel_YX(IList<CarReport_ZDTJView> RptDatas)
        {
            XSSFWorkbook book = new XSSFWorkbook();

            ISheet sheet = book.CreateSheet("终端统计信息");

            sheet.SetColumnWidth(0, 15 * 256);
            sheet.SetColumnWidth(1, 15 * 256);

            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 20 * 256);
            sheet.SetColumnWidth(4, 20 * 256);

            sheet.SetColumnWidth(5, 20 * 256);
            sheet.SetColumnWidth(6, 20 * 256);
            sheet.SetColumnWidth(7, 20 * 256);

            sheet.SetColumnWidth(8, 20 * 256);

            IRow row1 = sheet.CreateRow(0);

            row1.Height = 300;

            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("终端编号");
            cell = row1.CreateCell(1);
            cell.SetCellValue("车牌号");

            cell = row1.CreateCell(2);
            cell.SetCellValue("回传时间");
            cell = row1.CreateCell(3);
            cell.SetCellValue("总里程");
            cell = row1.CreateCell(4);
            cell.SetCellValue("离线天数");
            cell = row1.CreateCell(5);
            cell.SetCellValue("报警状态");

            cell = row1.CreateCell(6);
            cell.SetCellValue("回传次数");
            cell = row1.CreateCell(7);
            cell.SetCellValue("累计工时");
            cell = row1.CreateCell(8);
            cell.SetCellValue("统计时间");

            // 创建新增行
            if (RptDatas != null && RptDatas.Count > 0)
            {
                for (int i = 0; i < RptDatas.Count; i++)
                {
                    CarReport_ZDTJView row = (CarReport_ZDTJView)RptDatas[i];

                    row1 = sheet.CreateRow(i + 1);

                    //终端编号
                    cell = row1.CreateCell(0);
                    cell.SetCellValue(row.TerNo);

                    //车牌号
                    cell = row1.CreateCell(1);
                    cell.SetCellValue(row.CarNo);

                    //回传时间
                    cell = row1.CreateCell(2);
                    cell.SetCellValue(row.HCSJ.ToString());
                    //总里程
                    cell = row1.CreateCell(3);
                    cell.SetCellValue(row.LCTJ.ToString());
                    //离线天数
                    cell = row1.CreateCell(4);
                    cell.SetCellValue(row.LJLXTS.ToString());
                    //报警状态
                    cell = row1.CreateCell(5);
                    cell.SetCellValue(row.DQBJZT.ToString());

                    //回传次数
                    cell = row1.CreateCell(6);
                    cell.SetCellValue(row.LJHCCS.ToString());
                    //累计工时
                    cell = row1.CreateCell(7);
                    cell.SetCellValue(row.LJGS.ToString());
                    //统计时间
                    cell = row1.CreateCell(8);
                    cell.SetCellValue(row.TJSJ.ToString());

                }
            }

            return book;
        }
        private XSSFWorkbook CreateRptZdtjToExcel_WX(IList<CarReport_ZDTJView> RptDatas)
        {
            XSSFWorkbook book = new XSSFWorkbook();

            ISheet sheet = book.CreateSheet("终端统计信息");

            sheet.SetColumnWidth(0, 15 * 256);
            sheet.SetColumnWidth(1, 15 * 256);

            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 20 * 256);
            sheet.SetColumnWidth(4, 20 * 256);

            sheet.SetColumnWidth(5, 20 * 256);
            sheet.SetColumnWidth(6, 20 * 256);
            sheet.SetColumnWidth(7, 20 * 256);

            sheet.SetColumnWidth(8, 20 * 256);
            sheet.SetColumnWidth(9, 20 * 256);
            sheet.SetColumnWidth(10, 20 * 256);

            IRow row1 = sheet.CreateRow(0);

            row1.Height = 300;

            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("终端编号");
            cell = row1.CreateCell(1);
            cell.SetCellValue("车牌号");

            cell = row1.CreateCell(2);
            cell.SetCellValue("回传时间");
            cell = row1.CreateCell(3);
            cell.SetCellValue("离线天数");
            cell = row1.CreateCell(4);
            cell.SetCellValue("报警状态");

            cell = row1.CreateCell(5);
            cell.SetCellValue("标准比例");
            cell = row1.CreateCell(6);
            cell.SetCellValue("精准比例");
            cell = row1.CreateCell(7);
            cell.SetCellValue("追车比例");

            cell = row1.CreateCell(8);
            cell.SetCellValue("回传次数");
            cell = row1.CreateCell(9);
            cell.SetCellValue("累计工时");
            cell = row1.CreateCell(10);
            cell.SetCellValue("统计时间");

            // 创建新增行
            if (RptDatas != null && RptDatas.Count > 0)
            {
                for (int i = 0; i < RptDatas.Count; i++)
                {
                    CarReport_ZDTJView row = (CarReport_ZDTJView)RptDatas[i];

                    row1 = sheet.CreateRow(i + 1);

                    //终端编号
                    cell = row1.CreateCell(0);
                    cell.SetCellValue(row.TerNo);

                    //车牌号
                    cell = row1.CreateCell(1);
                    cell.SetCellValue(row.CarNo);

                    //回传时间
                    cell = row1.CreateCell(2);
                    cell.SetCellValue(row.HCSJ.ToString());
                    //离线天数
                    cell = row1.CreateCell(3);
                    cell.SetCellValue(row.LJLXTS.ToString());
                    //报警状态
                    cell = row1.CreateCell(4);
                    cell.SetCellValue(row.DQBJZT.ToString());

                    //标准比例
                    cell = row1.CreateCell(5);
                    cell.SetCellValue(row.MS_BZBL.ToString());
                    //精准比例
                    cell = row1.CreateCell(6);
                    cell.SetCellValue(row.MS_JZBL.ToString());
                    //追车比例
                    cell = row1.CreateCell(7);
                    cell.SetCellValue(row.MS_ZCBL.ToString());

                    //回传次数
                    cell = row1.CreateCell(8);
                    cell.SetCellValue(row.LJHCCS.ToString());
                    //累计工时
                    cell = row1.CreateCell(9);
                    cell.SetCellValue(row.LJGS.ToString());
                    //统计时间
                    cell = row1.CreateCell(10);
                    cell.SetCellValue(row.TJSJ.ToString());

                }
            }

            return book;
        }

        /// <summary>
        /// 在线统计导出EXCEL
        /// </summary>
        /// <param name="TerInfo"></param>
        /// <returns></returns>
        public MemoryStream CreateRptZxtjToExcel(IList<CarReport_ZXTJView> RptDatas)
        {
            XSSFWorkbook book = new XSSFWorkbook();

            ISheet sheet = book.CreateSheet("在线统计信息");

            sheet.SetColumnWidth(0, 15 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            //sheet.SetColumnWidth(4, 15 * 256);
            IRow row1 = sheet.CreateRow(0);

            row1.Height = 300;

            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("终端编号");

            cell = row1.CreateCell(1);
            cell.SetCellValue("车牌号");

            cell = row1.CreateCell(2);
            cell.SetCellValue("回传时间");

            cell = row1.CreateCell(3);
            cell.SetCellValue("回传位置");

            //cell = row1.CreateCell(4);
            //cell.SetCellValue("分析结果");

            // 创建新增行
            if (RptDatas != null && RptDatas.Count > 0)
            {
                for (int i = 0; i < RptDatas.Count; i++)
                {
                    CarReport_ZXTJView row = (CarReport_ZXTJView)RptDatas[i];

                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    cell.SetCellValue(row.TerNo);

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(row.CarNo);

                    cell = row1.CreateCell(2);
                    cell.SetCellValue(row.Rtime.ToString("yyyy-MM-dd HH:mm:ss"));

                    cell = row1.CreateCell(3);
                    cell.SetCellValue(row.Position);

                    //cell = row1.CreateCell(4);
                    //cell.SetCellValue("");
                }
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 离线统计导出EXCEL
        /// </summary>
        /// <param name="TerInfo"></param>
        /// <returns></returns>
        public MemoryStream CreateRptLxtjToExcel(IList<CarReport_LXTJView> RptDatas)
        {
            XSSFWorkbook book = new XSSFWorkbook();

            ISheet sheet = book.CreateSheet("离线统计信息");

            sheet.SetColumnWidth(0, 15 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            //sheet.SetColumnWidth(5, 15 * 256);
            IRow row1 = sheet.CreateRow(0);

            row1.Height = 300;

            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("终端编号");

            cell = row1.CreateCell(1);
            cell.SetCellValue("车牌号");

            cell = row1.CreateCell(2);
            cell.SetCellValue("回传时间");

            cell = row1.CreateCell(3);
            cell.SetCellValue("离线设备天数");

            cell = row1.CreateCell(4);
            cell.SetCellValue("回传位置");

            //cell = row1.CreateCell(5);
            //cell.SetCellValue("分析结果");

            // 创建新增行
            if (RptDatas != null && RptDatas.Count > 0)
            {
                for (int i = 0; i < RptDatas.Count; i++)
                {
                    CarReport_LXTJView row = (CarReport_LXTJView)RptDatas[i];

                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    cell.SetCellValue(row.TerNo);

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(row.CarNo);

                    cell = row1.CreateCell(2);
                    cell.SetCellValue(row.Rtime.ToString("yyyy-MM-dd HH:mm:ss"));

                    cell = row1.CreateCell(3);
                    cell.SetCellValue(row.NotrtimeDay);

                    cell = row1.CreateCell(4);
                    cell.SetCellValue(row.Position);

                    //cell = row1.CreateCell(5);
                    //cell.SetCellValue("");
                }
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 里程统计导出Excel
        /// </summary>
        /// <returns></returns>
        public MemoryStream CreatePctdataExcel(IList<CarReport_LCBBView> lcbblist)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("里程统计信息");
            sheet.SetColumnWidth(0, 15 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 15 * 256);
            sheet.SetColumnWidth(5, 20 * 256);
            IRow row1 = sheet.CreateRow(0);
            row1.Height = 300;

            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("回传时间");

            cell = row1.CreateCell(1);
            cell.SetCellValue("行驶时间(分钟)");

            cell = row1.CreateCell(2);
            cell.SetCellValue("停车时间(分钟)");

            cell = row1.CreateCell(3);
            cell.SetCellValue("停车次数");

            cell = row1.CreateCell(4);
            cell.SetCellValue("里程(X公里/天)");

            cell = row1.CreateCell(5);
            cell.SetCellValue("总里程(公里)");

             // 创建新增行
            if (lcbblist != null && lcbblist.Count > 0)
            {
                for (int i = 0; i < lcbblist.Count; i++)
                {
                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    if (lcbblist[i].StatDate != "合计")
                    {
                        cell.SetCellValue(Convert.ToDateTime(lcbblist[i].StatDate).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        cell.SetCellValue("合计");
                    }

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(lcbblist[i].RunTime);

                    cell = row1.CreateCell(2);
                    cell.SetCellValue(lcbblist[i].StopTime);

                    cell = row1.CreateCell(3);
                    cell.SetCellValue(lcbblist[i].StopCount);

                    cell = row1.CreateCell(4);
                    cell.SetCellValue(lcbblist[i].DayMile);

                    cell = row1.CreateCell(5);
                    cell.SetCellValue(lcbblist[i].DaySumMile);
                }
            }
            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 终端统计导出Excel
        /// </summary>
        /// <param name="TerCensusInfo"></param>
        /// <returns></returns>
        public MemoryStream CreateTercensusExcel(IList<TercensusData> TerCensusInfo)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("统计分析信息");
            sheet.SetColumnWidth(0, 15 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 15 * 256);
            sheet.SetColumnWidth(5, 20 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            sheet.SetColumnWidth(9, 15 * 256);
            sheet.SetColumnWidth(10, 15 * 256);
            sheet.SetColumnWidth(11, 15 * 256);
            sheet.SetColumnWidth(12, 15 * 256);
            sheet.SetColumnWidth(13, 15 * 256);
            sheet.SetColumnWidth(14, 15 * 256);
            sheet.SetColumnWidth(15, 15 * 256);
            sheet.SetColumnWidth(16, 20 * 256);
            sheet.SetColumnWidth(17, 15 * 256);
            IRow row1 = sheet.CreateRow(0);
            row1.Height = 300;

            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("终端编号");

            cell = row1.CreateCell(1);
            cell.SetCellValue("所属企业");

            cell = row1.CreateCell(2);
            cell.SetCellValue("设备类型");

            cell = row1.CreateCell(3);
            cell.SetCellValue("设备软件版本");

            cell = row1.CreateCell(4);
            cell.SetCellValue("设备状态");

            cell = row1.CreateCell(5);
            cell.SetCellValue("最新回传时间");

            cell = row1.CreateCell(6);
            cell.SetCellValue("未回传天数");

            cell = row1.CreateCell(7);
            cell.SetCellValue("设备电池电压");

            cell = row1.CreateCell(8);
            cell.SetCellValue("设备启动次数");

            cell = row1.CreateCell(9);
            cell.SetCellValue("回传次数");

            cell = row1.CreateCell(10);
            cell.SetCellValue("设备间隔时间");

            cell = row1.CreateCell(11);
            cell.SetCellValue("累计工时");

            cell = row1.CreateCell(12);
            cell.SetCellValue("当前报警状态");

            cell = row1.CreateCell(13);
            cell.SetCellValue("标准比例(%)");

            cell = row1.CreateCell(14);
            cell.SetCellValue("精准比例(%)");

            cell = row1.CreateCell(15);
            cell.SetCellValue("追车比例(%)");

            cell = row1.CreateCell(16);
            cell.SetCellValue("统计时间");

            cell = row1.CreateCell(17);
            cell.SetCellValue("分析结果");

            // 创建新增行
            if (TerCensusInfo != null && TerCensusInfo.Count > 0)
            {
                for (int i = 0; i < TerCensusInfo.Count; i++)
                {
                    row1 = sheet.CreateRow(i + 1);

                    cell = row1.CreateCell(0);
                    cell.SetCellValue(TerCensusInfo[i].TerNo);

                    cell = row1.CreateCell(1);
                    cell.SetCellValue(TerCensusInfo[i].DeptName);

                    cell = row1.CreateCell(2);
                    cell.SetCellValue(TerCensusInfo[i].TerTypeid);

                    cell = row1.CreateCell(3);
                    cell.SetCellValue(TerCensusInfo[i].Programverson);

                    cell = row1.CreateCell(4);
                    string TerState = "";
                    switch (TerCensusInfo[i].TerStatus)
                    {
                        case "1":
                            TerState = "测试";
                            break;
                        case "2":
                            TerState = "待激活";
                            break;
                        case "3":
                            TerState = "已激活";
                            break;
                        case "4":
                            TerState = "已拆除";
                            break;
                        case "5":
                            TerState = "到期";
                            break;
                        case "6":
                            TerState = "休眠";
                            break;
                        case "7":
                            TerState = "已使用";
                            break;
                        default:
                            TerState = "其他";
                            break;
                    }
                    cell.SetCellValue(TerState);

                    cell = row1.CreateCell(5);
                    cell.SetCellValue(TerCensusInfo[i].NewRtime.ToString("yyyy-MM-dd HH:mm:ss"));

                    cell = row1.CreateCell(6);
                    cell.SetCellValue(TerCensusInfo[i].NotrtimeDay);

                    cell = row1.CreateCell(7);
                    cell.SetCellValue(TerCensusInfo[i].TerVbatt);

                    cell = row1.CreateCell(8);
                    cell.SetCellValue(TerCensusInfo[i].TerStatrtimes);

                    cell = row1.CreateCell(9);
                    cell.SetCellValue(TerCensusInfo[i].PostbackTimes);

                    cell = row1.CreateCell(10);
                    cell.SetCellValue(TerCensusInfo[i].Ntervalltime);

                    cell = row1.CreateCell(11);
                    cell.SetCellValue(TerCensusInfo[i].TotalworkTime);

                    cell = row1.CreateCell(12);
                    cell.SetCellValue(TerCensusInfo[i].CuralarmState);

                    cell = row1.CreateCell(13);
                    cell.SetCellValue(TerCensusInfo[i].StandardScale);

                    cell = row1.CreateCell(14);
                    cell.SetCellValue(TerCensusInfo[i].PreciseScale);

                    cell = row1.CreateCell(15);
                    cell.SetCellValue(TerCensusInfo[i].ChasecarScale);

                    cell = row1.CreateCell(16);
                    cell.SetCellValue(TerCensusInfo[i].CensusTime.ToString("yyyy-MM-dd HH:mm:ss"));

                    cell = row1.CreateCell(17);
                    cell.SetCellValue(TerCensusInfo[i].AnalysisResult);
                }
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        /// <summary>
        /// 车辆信息导出EXCEL
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public MemoryStream CreateCarInfoExcel(DataTable dt, string[] filedlist)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("车辆信息");
            sheet.SetColumnWidth(0, 15 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 80 * 256);
            sheet.SetColumnWidth(4, 13 * 256);
            sheet.SetColumnWidth(5, 20 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            IRow row1 = sheet.CreateRow(0);
            row1.Height = 300;
            ICell cell = null;
            if (filedlist.Length > 0)
            {
                for (int i = 0; i < filedlist.Length; i++)
                {
                    cell = row1.CreateCell(i);
                    cell.SetCellValue(filedlist[i].ToString());
                }

                if (dt.Rows.Count > 0 && dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        row1 = sheet.CreateRow(i + 1);
                        for (int j = 0; j < filedlist.Length; j++) //dt.Columns.Count
                        {
                            cell = row1.CreateCell(j);
                            var filed = filedlist[j].ToString();
                            cell.SetCellValue(dt.Rows[i][filed].ToString());
                        }
                    }
                }
            }
            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            return ms;
        }

        public List<UpLoadTerBind> ReadCarInfo(string excelPath, ref string msg)
        {
            try
            {
                List<UpLoadTerBind> lut = new List<UpLoadTerBind>();
                using (FileStream fs = File.OpenRead(excelPath))   //打开myxls.xls文件
                {
                    string fileExt = Path.GetExtension(excelPath);
                    IWorkbook wk = null;
                    if (fileExt == ".xls")
                    {
                        wk = new HSSFWorkbook(fs);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        wk = new XSSFWorkbook(fs);
                    }
                    if (wk != null)
                    {
                        for (int i = 0; i < wk.NumberOfSheets; i++)  //NumberOfSheets是myxls.xls中总共的表数
                        {
                            ISheet sheet = wk.GetSheetAt(i);

                            if (sheet.LastRowNum > 1
                                && sheet.GetRow(0).LastCellNum >= 3
                                && sheet.GetRow(0).GetCell(0).ToString().Trim() == "车牌号"
                                && sheet.GetRow(0).GetCell(1).ToString().Trim() == "车辆类型"
                                && sheet.GetRow(0).GetCell(2).ToString().Trim() == "终端编号")
                            {
                                //读取当前表数据
                                for (int j = 1; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                                {
                                    IRow row = sheet.GetRow(j);  //读取当前行数据
                                    if (row != null)
                                    {
                                        UpLoadTerBind ut = new UpLoadTerBind();
                                        for (int k = 0; k <= row.LastCellNum; k++)  //LastCellNum 是当前行的总列数
                                        {
                                            ICell cell = row.GetCell(k);  //当前表格
                                            if (cell != null)
                                            {
                                                switch (k)
                                                {
                                                    case 0:
                                                        ut.CarNo = cell.ToString().Trim();
                                                        break;
                                                    case 1:
                                                        ut.CarType = cell.ToString().Trim();
                                                        break;
                                                    case 2:
                                                        ut.TerNo = cell.ToString().Trim();
                                                        break;
                                                }
                                            }
                                        }
                                        if (ut.CarNo != "" && ut.CarType != "" && ut.TerNo != "")
                                        {
                                            lut.Add(ut);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                msg = "数据格式不正确，请用模板填写数据。";
                                return null;
                            }
                        }
                    }
                }
                return lut;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 智能星数据导入模板
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public List<UpLoadTerBind> ReadCarInfo_ZNX(string excelPath, ref string msg)
        {
            try
            {
                #region 加载部门信息

                string dept_tree_view = @" select d.businessdivisionid   dept_id,
                                                    d.businessdivisionname dept_name
                                                from (select * from dept_info t where t.isdel = 0) d 　　
                                                start with d.fatherid is null　　
                                            connect by prior d.businessdivisionid = d.fatherid ";

                string sql = @" select t.dept_id,t.dept_name from ( " + dept_tree_view + " ) t ";

                System.Data.DataSet ds = query.GetColligateQuery("ColligateQuery.ProteanQuery", sql);

                #endregion

                List<UpLoadTerBind> lut = new List<UpLoadTerBind>();

                using (FileStream fs = File.OpenRead(excelPath))   //打开myxls.xls文件
                {
                    string fileExt = Path.GetExtension(excelPath);
                    IWorkbook wk = null;
                    if (fileExt == ".xls")
                    {
                        wk = new HSSFWorkbook(fs);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        wk = new XSSFWorkbook(fs);
                    }
                    if (wk != null)
                    {
                        for (int i = 0; i < wk.NumberOfSheets; i++)  //NumberOfSheets是myxls.xls中总共的表数
                        {
                            ISheet sheet = wk.GetSheetAt(i);

                            if (sheet.LastRowNum >= 1 && sheet.GetRow(0).LastCellNum >= 1)
                            {

                                //标题行
                                IRow TitleRow = sheet.GetRow(0);

                                if (TitleRow.Cells.Count(c => c.StringCellValue.Trim() == "车牌号") == 0 || TitleRow.Cells.Count(c => c.StringCellValue.Trim() == "终端编号") == 0)
                                {
                                    msg = "数据表中必须有车牌号和终端编号这两列";
                                    break;
                                }


                                //读取当前表数据
                                for (int j = 1; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                                {
                                    IRow row = sheet.GetRow(j);  //读取当前行数据
                                    if (row != null)
                                    {
                                        UpLoadTerBind ut = this.CreateUpLoadTerBind(TitleRow, row, ds);
                                        if (ut.TerNo != "" && ut.TerNo!=null)
                                        {
                                            lut.Add(ut);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                msg = "数据格式不正确，请用模板填写数据。";
                                break;
                            }
                        }
                    }
                }
                return lut;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #region 私有函数

        /// <summary>
        /// 全局变量，存放需导入数据各企业自定义字段集合对象
        /// </summary>
        private ArrayList G_Arr_FieldIdName = new ArrayList();

        /// <summary>
        /// 上传数据绑定
        /// </summary>
        /// <param name="TitleRow">标题列</param>
        /// <param name="dataRow">数据列</param>
        /// <param name="ds">公司部门信息</param>
        /// <returns></returns>
        private UpLoadTerBind CreateUpLoadTerBind(IRow TitleRow, IRow dataRow, DataSet ds)
        {
            UpLoadTerBind ut = new UpLoadTerBind();

            for (int k = 0; k <= dataRow.LastCellNum; k++)  //LastCellNum 是当前行的总列数
            {
                ICell titlecell = TitleRow.GetCell(k); //当前标题
                ICell cell = dataRow.GetCell(k);  //当前表格
                if (cell != null && titlecell!=null)
                {
                    switch (titlecell.ToString().Trim())
                    {
                        //车牌号	车辆类型	终端编号
                        case "车牌号":
                            ut.CarNo = cell.ToString().Trim();
                            break;

                        case "车辆类型":
                            ut.CarType = cell.ToString().Trim();
                            break;

                        case "车辆颜色":
                            ut.CarColor = cell.ToString().Trim();
                            break;

                        case "终端编号":
                            ut.TerNo = cell.ToString().Trim();
                            break;

                        //属性企业  车主姓名	身份证号
                        case "所属企业":
                            ut.BusinessDivisionId = cell.ToString().Trim();

                            #region 转换公司名称--> 公司ID

                            if (ds != null
                                && ds.Tables.Count > 0
                                && ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                                {
                                    if (ut.BusinessDivisionId.Equals(dr["dept_name"].ToString()))
                                    {
                                        ut.BusinessDivisionId = dr["dept_id"].ToString();
                                        break;
                                    }
                                }
                            }

                            #endregion

                            break;
                        case "车主姓名":
                            ut.CarAdminName = cell.ToString().Trim();
                            break;
                        case "身份证号":
                            ut.CarAdminCardId = cell.ToString().Trim();
                            break;
                        case "车主地址":
                            ut.OwerAddress = cell.ToString().Trim();
                            break;
                        case "车主电话":
                            ut.OwerPhone = cell.ToString().Trim();
                            break;

                        case "安装人":
                            ut.InstallName = cell.ToString().Trim();
                            break;

                        case "安装位置":
                            ut.InstallAddress = cell.ToString().Trim();
                            break;

                        case "安装人联系电话":
                            ut.InstallPhone = cell.ToString().Trim();
                            break;

                        case "安装地点":
                            ut.InstallPlace = cell.ToString().Trim();
                            break;

                        case "安装时间":
                            ut.InstallTime = cell.ToString().Trim();
                            break;

                        case "录入人":
                            ut.EntryName = cell.ToString().Trim();
                            break;

                        case "录入人电话":
                            ut.EntryPhone = cell.ToString().Trim();
                            break;

                        case "合同号":
                            ut.ContractNum = cell.ToString().Trim();
                            break;

                        case "保险单号":
                            ut.SafeOrder = cell.ToString().Trim();
                            break;

                        case "融资金额":
                            ut.LoanMoney = cell.ToString().Trim();
                            break;

                        case "贷款年限":
                            ut.LoanYear = cell.ToString().Trim();
                            break;

                        case "车辆型号":
                            ut.CarModel = cell.ToString().Trim();
                            break;

                        case "发动机号":
                            ut.EngineNumber = cell.ToString().Trim();
                            break;

                        case "车架号":
                            ut.CarFrame = cell.ToString().Trim();
                            break;

                        case "备注":
                            ut.Description = cell.ToString().Trim();
                            break;
                    }
                }
            }

            #region   自定义字段（废弃）
            ////加载组织机构用户自定义字段对象列表
            //List<UserFields> arr_FieldIdName = null;
            //if (G_Arr_FieldIdName.Count > 0)
            //{
            //    foreach (DictionaryEntry de in G_Arr_FieldIdName)
            //    {
            //        if (de.Key != null && ut.BusinessDivisionId != null)
            //        {
            //            if (de.Key.Equals(ut.BusinessDivisionId))
            //            {
            //                arr_FieldIdName = (List<UserFields>)de.Value;
            //                break;
            //            }
            //        }
            //    }
            //}
            //if (arr_FieldIdName == null)
            //{
            //    if (ut.BusinessDivisionId != null)
            //    {
            //        arr_FieldIdName = (List<UserFields>)(new GBLL.Basic.UserFieldsBLL()).GetUserFieldsList(ut.BusinessDivisionId);

            //        DictionaryEntry tmp = new DictionaryEntry();

            //        tmp.Key = ut.BusinessDivisionId;
            //        tmp.Value = arr_FieldIdName;

            //        G_Arr_FieldIdName.Add(tmp);
            //    }
            //}

            ////安装人	安装位置	录入人	安装时间	机主电话	
            ////贷款年限	融资金额
            //if (dataRow.LastCellNum > 6)
            //{
            //    for (int k = 6; k < dataRow.LastCellNum; k++)  //LastCellNum 是当前行的总列数
            //    {
            //        ICell title = TitleRow.GetCell(k);  //当前表格
            //        ICell cell = dataRow.GetCell(k);  //当前表格

            //        if (arr_FieldIdName.Count > 0)
            //        {
            //            foreach (UserFields uf in arr_FieldIdName)
            //            {
            //                //存在，则保留
            //                if (uf.UfName.Equals(title.StringCellValue))
            //                {
            //                    FieldValues fv = new FieldValues();

            //                    fv.UfId = uf.UfId;

            //                    if (uf.FieldType.ToString().ToUpper() == "DATE")
            //                    {
            //                        //DateTime tmp = DateTime.FromOADate(Convert.ToInt32(cell.NumericCellValue));

            //                        fv.FieldValue = cell.StringCellValue; //tmp.Year.ToString() + "-" + tmp.Month.ToString() + "-" + tmp.Day.ToString();
            //                    }
            //                    else
            //                    {
            //                        fv.FieldValue = cell.ToString().Trim();
            //                    }

            //                    fv.CarId = ut.CarNo;
            //                    fv.FieldId = System.Guid.NewGuid().ToString();

            //                    ut.FieldValuesList.Add(fv);

            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion 
            return ut;
        }

        #endregion
    }
}
