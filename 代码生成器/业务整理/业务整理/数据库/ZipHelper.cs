using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;


namespace 业务管理.数据库
{
    public class ZipHelper
    {


        /// <summary>
        /// GZipStream压缩字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Stream GZipCompress(string str)
        {
            string temp = StringHelper.GB2312ToUTF8(str);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(temp);
            MemoryStream msReturn;

            using (MemoryStream msTemp = new MemoryStream())
            {
                using (GZipStream gz = new GZipStream(msTemp, CompressionMode.Compress, true))
                {
                    gz.Write(buffer, 0, buffer.Length);
                    gz.Close();

                    msReturn = new MemoryStream(msTemp.GetBuffer(), 0, (int)msTemp.Length);
                }
            }

            return msReturn;
        }

        /// <summary>
        /// GZipStream压缩Byte数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Stream GZipCompress(byte[] btArray)
        {
            MemoryStream msReturn;

            using (MemoryStream msTemp = new MemoryStream())
            {
                using (GZipStream gz = new GZipStream(msTemp, CompressionMode.Compress, true))
                {
                    gz.Write(btArray, 0, btArray.Length);
                    gz.Close();

                    msReturn = new MemoryStream(msTemp.GetBuffer(), 0, (int)msTemp.Length);
                }
            }

            return msReturn;
        }

        /// <summary>
        /// GZipStream解压字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string GZipDecompress(Stream stream)
        {
            byte[] buffer = new byte[100];
            int length = 0;

            using (GZipStream gz = new GZipStream(stream, CompressionMode.Decompress))
            {
                using (MemoryStream msTemp = new MemoryStream())
                {
                    while ((length = gz.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        msTemp.Write(buffer, 0, length);
                    }

                    return System.Text.Encoding.UTF8.GetString(msTemp.ToArray());
                }

            }
        }

        /// <summary>
        /// GZipStream解压字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public MemoryStream GZipDecompressStream(Stream stream)
        {
            byte[] buffer = new byte[100];
            int length = 0;

            using (GZipStream gz = new GZipStream(stream, CompressionMode.Decompress))
            {
                using (MemoryStream msTemp = new MemoryStream())
                {
                    while ((length = gz.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        msTemp.Write(buffer, 0, length);
                    }

                    return msTemp;
                }

            }
        }

    }
}
