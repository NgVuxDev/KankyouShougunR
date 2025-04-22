using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    public class ConvertStrByte
    {
        /// <summary>
        /// ストリングをバイト配列に変換する
        /// </summary>
        /// <param name="inStr">ストリング</param>
        public static byte[] StringToByte(string inStr)
        {
            byte[] bytes = new byte[inStr.Length / 2];
            for (int i = 0; i < inStr.Length / 2; i++)
            {
                int btvalue = Convert.ToInt32(inStr.Substring(i * 2, 2), 16);
                bytes[i] = (byte)btvalue;
            }
            return bytes;
        }

        /// <summary>
        /// バイト配列をストリングに変換する
        /// </summary>
        /// <param name="inStr">ストリング</param>
        public static string ByteToString(byte[] inBytes)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (byte bt in inBytes)
            {
                strBuilder.AppendFormat("{0:X2}", bt);
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// In32をbyte[8]に変換する
        /// </summary>
        /// <param name="inStr">ストリング</param>
        public static byte[] In32ToByteArray(Int32 inData)
        {
            if (inData == null)
            {
                return null;
            }
            
            byte[] c = BitConverter.GetBytes(inData);
            byte[] d = new byte[8];
            d[0] = 0;
            d[1] = 0;
            d[2] = 0;
            d[3] = 0;
            d[4] = c[3];
            d[5] = c[2];
            d[6] = c[1];
            d[7] = c[0];
            
            return d;
        }
    }
}
