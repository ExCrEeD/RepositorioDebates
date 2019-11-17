using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace debatesWebApi.Utilidades
{
    public  static class Utilidades
    {
        public static Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public static string GetRandomString()
        {
            Random rnd = new Random();
            int length = 15;
            string charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw xyz1234567890";
            StringBuilder rs = new StringBuilder();

            while (length > 0)
            {
                rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);
                length--;
            }
            return rs.ToString();
        }
    }
}