using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperGPS
{
    public class Transfers
    {
        private static System.Net.IPAddress GpsServerIP;
        private static System.Net.IPEndPoint GpsServerFamily;
        private static System.Threading.Thread Sockthread = null;
        private static System.Threading.Thread LinkWhThread = null;
        private static System.Threading.Thread ReLinkThread = null;
        private static System.Threading.Thread LinkCLThread = null;
        private static System.Threading.Thread SendDataThread = null;

        private static System.Net.Sockets.Socket LinkWGConnectsock;
        private static System.Net.Sockets.NetworkStream socknetstream;
        private static bool p_bReceiveON = false;
        //数据接收
        private static byte[] p_charDataServerBuf = new byte[16384];
        private static int p_charDataCount = 0;
        private static bool p_bDataReON = false;
        private static bool p_b5CON = false;
        private static int p_charCheckRe;
        //数据发送
        private static int[,] p_intSeCount = new int[3000, 3];//数据类型,长度，回应
        private static byte[,] p_charSeData = new byte[3000, 16384];
        private static byte[] p_charOneData = new byte[16384];
        private static int p_intSeSequence;
        private static string p_strLinkCL;

        public static void ReadConfig()
        {
            string strPath;
            strPath = AppDomain.CurrentDomain.BaseDirectory;
            strPath = strPath + "connect.ini";
            System.IO.StreamReader reader = new System.IO.StreamReader(strPath, System.Text.Encoding.Default);

            string strLineText;
            if (reader.Peek() >= 0)
            {
                strLineText = reader.ReadLine();
                string[] tmp = strLineText.Split('=');
                GlobalVariable.p_strWGCenterIP = tmp[1];
            }
            if (reader.Peek() >= 0)
            {
                strLineText = reader.ReadLine();
                string[] tmp = strLineText.Split('=');
                GlobalVariable.p_intWGCenterPort = Int32.Parse(tmp[1]);
            }
            if (reader.Peek() >= 0)
            {
                strLineText = reader.ReadLine();
                string[] tmp = strLineText.Split('=');
                GlobalVariable.p_strWGLoginName = tmp[1];
            }
            if (reader.Peek() >= 0)
            {
                strLineText = reader.ReadLine();
                string[] tmp = strLineText.Split('=');
                GlobalVariable.p_strWGLoginPassword = tmp[1];
            }
            reader.Close();
        }
        public static void LinkCenter(string strCenterIP, int intCenterPort)
        {
            try
            {
                GpsServerIP = System.Net.IPAddress.Parse(strCenterIP);
                GpsServerFamily = new System.Net.IPEndPoint(GpsServerIP, intCenterPort);
                LinkWGConnectsock = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                LinkWGConnectsock.Connect(GpsServerFamily);
                socknetstream = new System.Net.Sockets.NetworkStream(LinkWGConnectsock);
                if (!p_bReceiveON)
                {
                    Sockthread = new System.Threading.Thread(new System.Threading.ThreadStart(receive));
                    Sockthread.Start();
                    LinkWhThread = new System.Threading.Thread(new System.Threading.ThreadStart(LinkWh));
                    LinkWhThread.Start();
                    ReLinkThread = new System.Threading.Thread(new System.Threading.ThreadStart(relink));
                    ReLinkThread.Start();
                    LinkCLThread = new System.Threading.Thread(new System.Threading.ThreadStart(LinkCenterCL));
                    LinkCLThread.Start();
                    SendDataThread = new System.Threading.Thread(new System.Threading.ThreadStart(SendDatath));
                    SendDataThread.Start();
                    p_bReceiveON = true;
                }
                GlobalVariable.p_bLinkCenterON = true;
            }
            //catch (Exception ee)
            catch
            {
                return;
            }
        }
        private static void receive()
        {
            int i;
            int Buflen;
            byte[] Buf = new byte[16384];
            while (true)
            {
                System.Threading.Thread.Sleep(5);
                if (GlobalVariable.p_bLinkCenterON)
                {
                    try
                    {
                        Buflen = socknetstream.Read(Buf, 0, Buf.Length);
                        if (Buflen > 0)
                        {
                            for (i = 0; i < Buflen; i++)
                            {
                                if (p_bDataReON)
                                {
                                    if (Buf[i] == 0x5c)
                                    {
                                        p_b5CON = true;
                                    }
                                    else
                                    {
                                        if (p_b5CON)
                                        {
                                            p_charDataServerBuf[p_charDataCount] = (byte)(Buf[i] ^ 0x50);
                                            p_b5CON = false;
                                        }
                                        else
                                        {
                                            p_charDataServerBuf[p_charDataCount] = Buf[i];
                                        }
                                        p_charCheckRe = p_charCheckRe ^ p_charDataServerBuf[p_charDataCount];
                                        p_charDataCount++;
                                    }
                                    if (p_charDataCount > 16383)
                                    {
                                        p_bDataReON = false;
                                        return;
                                    }
                                    if (Buf[i] == 0x5d)
                                    {
                                        int charOwnCheck = 0x00;
                                        charOwnCheck = p_charDataServerBuf[p_charDataCount - 2] ^ p_charDataServerBuf[p_charDataCount - 2] ^ p_charDataServerBuf[p_charDataCount - 1];
                                        if (p_charCheckRe == charOwnCheck)
                                        {
                                            ReClientData(p_charDataCount - 3);
                                        }
                                        p_bDataReON = false;
                                    }
                                }
                                if (Buf[i] == 0x5b)
                                {
                                    p_charDataCount = 0;
                                    p_bDataReON = true;
                                    p_b5CON = false;
                                    p_charCheckRe = 0x00;
                                    p_charDataServerBuf[p_charDataCount] = Buf[i];
                                    p_charDataCount++;
                                }
                            }
                        }
                    }
                    catch
                    {
                        LinkWGConnectsock.Close();
                        GlobalVariable.p_bLinkCenterON = false;
                    }
                }
            }
        }
        private static void LinkWh()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(45000);
                SendData(1101, 0);
            }
        }
        private static void relink()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(10000);
                if (!GlobalVariable.p_bLinkCenterON)
                {
                    LinkCenter(GlobalVariable.p_strWGCenterIP, GlobalVariable.p_intWGCenterPort);
                }
            }
        }
        private static void LinkCenterCL()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(60000);
                if (GlobalVariable.p_bLinkCenterON)
                {
                    if (p_strLinkCL == "0" || p_strLinkCL == "1")
                    {
                        if (p_strLinkCL == "0")
                        {
                            p_strLinkCL = "1";
                        }
                        else
                        {
                            p_strLinkCL = "2";
                        }
                    }
                    else
                    {
                        p_strLinkCL = "0";
                        LinkWGConnectsock.Close();
                        GlobalVariable.p_bLinkCenterON = false;
                    }
                }
            }
        }
        private static void SendDatath()
        {
            int i, j;
            byte[] charSeOneData = new byte[16384];
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                if (GlobalVariable.p_bLinkCenterON)
                {
                    for (i = 0; i < 3000; i++)
                    {
                        if (p_intSeCount[i, 2] == 1)
                        {
                            for (j = 0; j < p_intSeCount[i, 1]; j++)
                            {
                                charSeOneData[j] = p_charSeData[i, j];
                            }
                            socknetstream.Write(charSeOneData, 0, p_intSeCount[i, 1]);
                            //if (p_intSeCount[i, 0] < 1000)
                            //{
                                p_intSeCount[i, 2] = 0;
                            //}
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
        }
        private static void ReClientData(int intDataLen)
        {
            int intDataType;
            int intSeSeSequence;

            intDataType = p_charDataServerBuf[1];
            intDataType = intDataType << 8 | p_charDataServerBuf[2];
            if (intDataType < 1000)
            {
                if (intDataType == 101 || intDataType == 111)
                {
                    intSeSeSequence = p_charDataServerBuf[5];
                    intSeSeSequence = intSeSeSequence << 8 | p_charDataServerBuf[6];
                    p_intSeCount[intSeSeSequence, 2] = 0;
                    if (intDataType == 101)
                    {
                        p_strLinkCL = "0";
                    }
                }
                else
                {
                    ReCommData(intDataType, intDataLen);
                }
            }
            else
            {
                p_charOneData[0] = p_charDataServerBuf[3];
                p_charOneData[1] = p_charDataServerBuf[4];
                SendData(111, 2);
                if (intDataType == 1100)
                {
                    p_strLinkCL = "0";
                    ClintSendCommData(1151, "1151", "", "", "", "", "", "", "", "", "", GlobalVariable.p_strWGLoginName, GlobalVariable.p_strWGLoginPassword, "", "", "", "", "", "");
                }
                else
                {
                    if (intDataType == 1114)
                    {
                    }
                    else
                    {
                        ReCommData(intDataType, intDataLen);
                    }
                }
            }
        }
        public static void ClintSendCommData(int intDataType, string strDataType, string stSetType, string strSetSN, string strSetSN1, string strAlmComType, string strHisType, string strPosType, string strFadeType, string strRecogType, string strRecogType1, string strParam1, string strParam2, string strParam3, string strParam4, string strParam5, string strParam6, string strParam7, string strParam8)
        {
            string strData = strDataType + "^" + stSetType + "^" + strSetSN + "^" + strSetSN1 + "^" + strAlmComType + "^" + strHisType + "^" + strPosType + "^" + strFadeType + "^" + strRecogType + "^" + strRecogType1 + "^" + strParam1 + "^" + strParam2 + "^" + strParam3 + "^" + strParam4 + "^" + strParam5 + "^" + strParam6 + "^" + strParam7 + "^" + strParam8 + "^";
            p_charOneData = System.Text.Encoding.Default.GetBytes(strData);
            SendData(intDataType, p_charOneData.Length);
        }
        private static void SendData(int intDataType, int intDataLen)
        {
            if (GlobalVariable.p_bLinkCenterON)
            {
                int i;
                int intTMDataLen = 0;
                int charCheckRe = 0x00;
                byte[] charSeOneData = new byte[16384];
                if (p_intSeSequence >= 3000)
                {
                    p_intSeSequence = 0;
                }
                p_intSeCount[p_intSeSequence, 0] = intDataType;
                if (intDataType == 111 || intDataType == 1101)
                {
                    p_intSeCount[p_intSeSequence, 2] = 0;
                }
                else
                {
                    p_intSeCount[p_intSeSequence, 2] = 1;
                }
                p_charSeData[p_intSeSequence, intTMDataLen] = 0x5b;
                intTMDataLen++;
                p_charSeData[p_intSeSequence, intTMDataLen] = (byte)(intDataType >> 8);
                charCheckRe = p_charSeData[p_intSeSequence, intTMDataLen];
                if (p_charSeData[p_intSeSequence, intTMDataLen] == 0x5b || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5c || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5d)
                {
                    p_charSeData[p_intSeSequence, intTMDataLen + 1] = (byte)(p_charSeData[p_intSeSequence, intTMDataLen] ^ 0x50);
                    p_charSeData[p_intSeSequence, intTMDataLen] = 0x5c;
                    intTMDataLen++;
                }
                intTMDataLen++;
                p_charSeData[p_intSeSequence, intTMDataLen] = (byte)(intDataType & 0x00ff);
                charCheckRe = charCheckRe ^ p_charSeData[p_intSeSequence, intTMDataLen];
                if (p_charSeData[p_intSeSequence, intTMDataLen] == 0x5b || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5c || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5d)
                {
                    p_charSeData[p_intSeSequence, intTMDataLen + 1] = (byte)(p_charSeData[p_intSeSequence, intTMDataLen] ^ 0x50);
                    p_charSeData[p_intSeSequence, intTMDataLen] = 0x5c;
                    intTMDataLen++;
                }
                intTMDataLen++;
                p_charSeData[p_intSeSequence, intTMDataLen] = (byte)(p_intSeSequence >> 8);
                charCheckRe = charCheckRe ^ p_charSeData[p_intSeSequence, intTMDataLen];
                if (p_charSeData[p_intSeSequence, intTMDataLen] == 0x5b || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5c || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5d)
                {
                    p_charSeData[p_intSeSequence, intTMDataLen + 1] = (byte)(p_charSeData[p_intSeSequence, intTMDataLen] ^ 0x50);
                    p_charSeData[p_intSeSequence, intTMDataLen] = 0x5c;
                    intTMDataLen++;
                }
                intTMDataLen++;
                p_charSeData[p_intSeSequence, intTMDataLen] = (byte)(p_intSeSequence & 0x00ff);
                charCheckRe = charCheckRe ^ p_charSeData[p_intSeSequence, intTMDataLen];
                if (p_charSeData[p_intSeSequence, intTMDataLen] == 0x5b || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5c || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5d)
                {
                    p_charSeData[p_intSeSequence, intTMDataLen + 1] = (byte)(p_charSeData[p_intSeSequence, intTMDataLen] ^ 0x50);
                    p_charSeData[p_intSeSequence, intTMDataLen] = 0x5c;
                    intTMDataLen++;
                }
                intTMDataLen++;
                for (i = 0; i < intDataLen; i++)
                {
                    p_charSeData[p_intSeSequence, intTMDataLen] = p_charOneData[i];
                    charCheckRe = charCheckRe ^ p_charSeData[p_intSeSequence, intTMDataLen];
                    if (p_charSeData[p_intSeSequence, intTMDataLen] == 0x5b || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5c || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5d)
                    {
                        p_charSeData[p_intSeSequence, intTMDataLen + 1] = (byte)(p_charSeData[p_intSeSequence, intTMDataLen] ^ 0x50);
                        p_charSeData[p_intSeSequence, intTMDataLen] = 0x5c;
                        intTMDataLen++;
                    }
                    intTMDataLen++;
                }
                p_charSeData[p_intSeSequence, intTMDataLen] = (byte)charCheckRe;
                if (p_charSeData[p_intSeSequence, intTMDataLen] == 0x5b || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5c || p_charSeData[p_intSeSequence, intTMDataLen] == 0x5d)
                {
                    p_charSeData[p_intSeSequence, intTMDataLen + 1] = (byte)(p_charSeData[p_intSeSequence, intTMDataLen] ^ 0x50);
                    p_charSeData[p_intSeSequence, intTMDataLen] = 0x5c;
                    intTMDataLen++;
                }
                intTMDataLen++;
                p_charSeData[p_intSeSequence, intTMDataLen] = 0x5d;
                intTMDataLen++;
                p_intSeCount[p_intSeSequence, 1] = intTMDataLen;

                if (intDataType == 111 || intDataType == 1101)
                {
                    for (i = 0; i < intTMDataLen; i++)
                    {
                        charSeOneData[i] = p_charSeData[p_intSeSequence, i];
                    }
                    socknetstream.Write(charSeOneData, 0, p_intSeCount[p_intSeSequence, 1]);
                }
                p_intSeSequence++;
            }
        }

        private static void ReCommData(int intDataType, int intDataLen)
        {
            string strReData = "";
            string[] strReStem;
            strReData = System.Text.Encoding.Default.GetString(p_charDataServerBuf, 5, intDataLen - 4);
            strReStem = strReData.Split(new char[] { '^' });
            ClintReceCommData(intDataType, strReStem[0], strReStem[1], strReStem[2], strReStem[3], strReStem[4], strReStem[05], strReStem[6], strReStem[7], strReStem[8], strReStem[9], strReStem[10], strReStem[11], strReStem[12], strReStem[13], strReStem[14], strReStem[15], strReStem[16], strReStem[17]);
        }
        public static void ClintReceCommData(int intDataType, string strDataType, string stSetType, string strSetSN, string strSetSN1, string strAlmComType, string strHisType, string strPosType, string strFadeType, string strRecogType, string strRecogType1, string strParam1, string strParam2, string strParam3, string strParam4, string strParam5, string strParam6, string strParam7, string strParam8)
        {

        }
    }
}