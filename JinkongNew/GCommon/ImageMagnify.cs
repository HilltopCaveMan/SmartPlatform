using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GCommon
{
    class ImageMagnify
    {
        /// <summary>
        /// 图片放大
        /// </summary>
        /// <param name="srcbitmap">源图片</param>
        /// <param name="multiple">放大倍数</param>
        /// <returns></returns>
        //public Bitmap Magnifier(Bitmap srcbitmap, double multiple)
        //{
        //    if (multiple <= 0)
        //    { multiple = 0; return srcbitmap; }
        //    Bitmap bitmap = new Bitmap(Convert.ToInt32(srcbitmap.Size.Width * multiple), Convert.ToInt32(srcbitmap.Size.Height * multiple));
        //    BitmapData srcbitmapdata = srcbitmap.LockBits
        //        (
        //        new Rectangle(new Point(0, 0), srcbitmap.Size),
        //        ImageLockMode.ReadOnly,
        //        PixelFormat.Format32bppArgb);
        //    BitmapData bitmapdata = bitmap.LockBits(new Rectangle(new Point(0, 0), bitmap.Size), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb
        //        );
        //    unsafe
        //    {
        //        byte* srcbyte = (byte*)(srcbitmapdata.Scan0.ToPointer()); byte* sourcebyte = (byte*)(bitmapdata.Scan0.ToPointer()); for (int y = 0; y < bitmapdata.Height; y++)
        //        {
        //            for (int x = 0; x < bitmapdata.Width; x++)
        //            {
        //                long index = (x / multiple) * 4 + (y / multiple) * srcbitmapdata.Stride;
        //                sourcebyte[0] = srcbyte[index]; sourcebyte[1] = srcbyte[index + 1]; sourcebyte[2] = srcbyte[index + 2]; sourcebyte[3] = srcbyte[index + 3]; sourcebyte += 4;
        //            }
        //        }
        //    }
        //    srcbitmap.UnlockBits(srcbitmapdata);
        //    bitmap.UnlockBits(bitmapdata); return bitmap;
        //}
        //public Bitmap Magnifier(Bitmap srcbitmap, int multiple)
        //{
        //    if (multiple <= 0)
        //    { multiple = 0; return srcbitmap; }
        //    Bitmap bitmap = new Bitmap(srcbitmap.Size.Width * multiple, srcbitmap.Size.Height * multiple);
        //    BitmapData srcbitmapdata = srcbitmap.LockBits(new Rectangle(new Point(0, 0), srcbitmap.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb); BitmapData bitmapdata = bitmap.LockBits(new Rectangle(new Point(0, 0), bitmap.Size), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        //    unsafe
        //    {
        //        byte* srcbyte = (byte*)(srcbitmapdata.Scan0.ToPointer()); 
        //        byte* sourcebyte = (byte*)(bitmapdata.Scan0.ToPointer()); 
        //        for (int y = 0; y < bitmapdata.Height; y++)
        //        {
        //            for (int x = 0; x < bitmapdata.Width; x++)
        //            {
        //                long index = (x / multiple) * 4 + (y / multiple) * srcbitmapdata.Stride;
        //                sourcebyte[0] = srcbyte[index]; sourcebyte[1] = srcbyte[index + 1]; sourcebyte[2] = srcbyte[index + 2]; sourcebyte[3] = srcbyte[index + 3]; sourcebyte += 4;
        //            }
        //        }
        //    }
        //    srcbitmap.UnlockBits(srcbitmapdata);
        //    bitmap.UnlockBits(bitmapdata); return bitmap;
        //}

        /// <summary>  
        /// 将图片Image转换成Byte[]  
        /// </summary>  
        /// <param name="Image">image对象</param>  
        /// <param name="imageFormat">后缀名</param>  
        /// <returns></returns>  
        public static byte[] ImageToBytes(Image Image, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if (Image == null) { return null; }
            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap Bitmap = new Bitmap(Image))
                {
                    Bitmap.Save(ms, imageFormat);
                    ms.Position = 0;
                    data = new byte[ms.Length];
                    ms.Read(data, 0, Convert.ToInt32(ms.Length));
                    ms.Flush();
                }
            }
            return data;
        }

        /// <summary>  
        /// byte[]转换成Image  
        /// </summary>  
        /// <param name="byteArrayIn">二进制图片流</param>  
        /// <returns>Image</returns>  
        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn))
            {
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                ms.Flush();
                return returnImage;
            }
        }


        //Image转换Bitmap  
        //Bitmap img = new Bitmap(imgSelect.Image);

        //Bitmap bmp = (Bitmap)pictureBox1.Image;

        //Bitmap转换成Image   

        //private static System.Windows.Controls.Image Bitmap2Image(System.Drawing.Bitmap Bi)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    Bi.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //    BitmapImage bImage = new BitmapImage();
        //    bImage.BeginInit();
        //    bImage.StreamSource = new MemoryStream(ms.ToArray());
        //    bImage.EndInit();
        //    ms.Dispose();
        //    Bi.Dispose();
        //    System.Windows.Controls.Image i = new System.Windows.Controls.Image();
        //    i.Source = bImage;
        //    return i;
        //}


        /// <summary>
        /// byte[] 转换 Bitmap  
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(Bytes);
                //return new Bitmap((Image)new Bitmap(stream));
                return new Bitmap(stream);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }

        //Bitmap转byte[]    
        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }
    }
}
