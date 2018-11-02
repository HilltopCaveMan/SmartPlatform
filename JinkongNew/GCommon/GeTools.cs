using System;
using System.Collections.Generic;
using System.Text;

namespace GCommon
{
    public struct MyPoint
    {
        public MyPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X, Y;
    }

    class GeTools
    {
        private double min(double x, double y)
        {
            if (x > y)
                return y;
            else
                return x;
        }
        private double max(double x, double y)
        {
            if (x > y)
                return x;
            else
                return y;
        }
        public int PtinPolygon(MyPoint pt, MyPoint[] ptPolygon)
        {
            int nCount = ptPolygon.Length;
            // 记录是否在多边形的边上
            bool isBeside = false;
            // 多边形外接矩形
            double maxx, maxy, minx, miny;
            if (nCount > 0)
            {
                maxx = ptPolygon[0].X;
                minx = ptPolygon[0].X;
                maxy = ptPolygon[0].Y;
                miny = ptPolygon[0].Y;
                for (int j = 1; j < nCount; j++)
                {
                    if (ptPolygon[j].X >= maxx)
                        maxx = ptPolygon[j].X;
                    else if (ptPolygon[j].X <= minx)
                        minx = ptPolygon[j].X;
                    if (ptPolygon[j].Y >= maxy)
                        maxy = ptPolygon[j].Y;
                    else if (ptPolygon[j].Y <= miny)
                        miny = ptPolygon[j].Y;
                }
                if ((pt.X > maxx) || (pt.X < minx) || (pt.Y > maxy) || (pt.Y < miny))
                    return -1;
            }
            // 射线法判断
            int nCross = 0;
            for (int i = 0; i < nCount; i++)
            {
                MyPoint p1 = ptPolygon[i];
                MyPoint p2 = ptPolygon[(i + 1) % nCount];
                if (p1.Y == p2.Y)
                {
                    if (pt.Y == p1.Y && pt.X >= min(p1.X, p2.X) && pt.X <= max(p1.X, p2.X))
                    {
                        isBeside = true;
                        continue;
                    }
                }
                // 交点在p1p2延长线上
                if (pt.Y < min(p1.Y, p2.Y) || pt.Y > max(p1.Y, p2.Y))
                    continue;
                // 求交点的X坐标
                double x = (double)(pt.Y - p1.Y) * (double)(p2.X - p1.X) / (double)(p2.Y - p1.Y) + p1.X;
                if (x > pt.X)
                    nCross++;  // 只统计单边交点
                else if (x == pt.X)
                    isBeside = true;
            }
            if (isBeside)
                return 0;  //多边形上
            else if (nCross % 2 == 1)
                return 1;  // 多边形内
            return -1;     // 多边形外
        }





       
    }
}
