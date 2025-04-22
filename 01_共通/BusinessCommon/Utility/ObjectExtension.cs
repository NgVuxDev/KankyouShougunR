using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    /*
     How to use:
     * 1. Add References Assembly BusinessCommon
     * 2. Using [Shougun.Core.Common.BusinessCommon.Utility]
     * 3. From object data, call extension method. 
     *      For example: 
     *          DataRow row = DataTable.Rows[0];
     *          row["SENTAKU_FLG"].ConvertToBoolean();
     *      
     *          If you want return default value if object is null: 
     *          Ex: I want false if object is null
     *          row["SENTAKU_FLG"].ConvertToBoolean(false)
     */
    /// <summary>
    /// Calss extension of object
    /// Used to convert objects to other data types 
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Exception object reference not set to an instance of an object
        /// </summary>
        private static ArgumentNullException NullException = new ArgumentNullException("オブジェクト参照がオブジェクトインスタンスに設定されていません 。 ");
        
        #region DefineGenericMethod
        
        /// <summary>
        /// Check object is null or empty
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object obj)
        {
            if (string.IsNullOrEmpty(Convert.ToString(obj)) || string.IsNullOrWhiteSpace(Convert.ToString(obj)))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Check object is null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            if (obj == null || DBNull.Value.Equals(obj))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Generic convert method   
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static T ConvertTo<T>(this object obj)
        {
            if (!obj.IsNullOrEmpty())
            {
                if (obj.GetType() == typeof(T))
                {
                    return (T)obj;
                }
                else
                {
                    return (T)Convert.ChangeType(obj, typeof(T));
                }
            }
            else
            {
                throw NullException;
            }
        }
        /// <summary>
        /// Generic convert method   
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultNullValue">Return value if obj is null or can not convert</param>
        /// <returns></returns>
        private static T ConvertTo<T>(this object obj, T defaultNullValue)
        {
            if (!obj.IsNullOrEmpty())
            {
                if (obj.GetType() == typeof(T))
                {
                    return (T)obj;
                }
                else
                {
                    try
                    {                        
                        return (T)Convert.ChangeType(obj, typeof(T));
                    }
                    catch (Exception ex)
                    {
                        return defaultNullValue;
                    }
                }
            }
            else
            {
                return defaultNullValue;
            }
        }
        #endregion
        #region String
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertToString(this object obj)
        {
            return obj.ConvertTo<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultNullValue">Return value if obj is null or can not convert</param>
        /// <returns></returns>
        public static string ConvertToString(this object obj, string defaultNullValue)
        {
            return obj.ConvertTo<string>(defaultNullValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="prmLenght"></param>
        /// <returns></returns>
        public static string SubStringByByte(this object obj, int prmLenght)
        {
            var ret = ConvertToString(obj, string.Empty);
            return ret.SubStringByByte(prmLenght);

        }       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="prmLenght"></param>
        /// <param name="prmLine"></param>
        /// <returns></returns>
        public static string SubStringByByte(this object obj, int prmLenght, int prmLine)
        {
            var ret = ConvertToString(obj, string.Empty);
            return ret.SubStringByByte(prmLenght, prmLine);

        }
        #endregion
        #region Boolean
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ConvertToBoolean(this object obj)
        {
            if (Convert.ToString(obj) == "0")
            {
                return false;
            }
            else if (Convert.ToString(obj) == "1")
            {
                return true;
            }
            else
            {
                return obj.ConvertTo<bool>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultNullValue">Return value if obj is null or can not convert</param>
        /// <returns></returns>
        public static bool ConvertToBoolean(this object obj, bool defaultNullValue)
        {
            return obj.ConvertTo<bool>(defaultNullValue);
        }
        #endregion
        #region DateTime
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this object obj)
        {
            return obj.ConvertTo<DateTime>(); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultNullValue">Return value if obj is null or can not convert</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this object obj, DateTime defaultNullValue)
        {
            return obj.ConvertTo<DateTime>(defaultNullValue); 
        }
        #endregion
        #region Int16
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int16 ConvertToInt16(this object obj)
        {
            return obj.ConvertTo<Int16>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultNullValue"></param>
        /// <returns></returns>
        public static decimal ConvertToInt16(this object obj, Int16 defaultNullValue)
        {
            return obj.ConvertTo<Int16>(defaultNullValue);
        }
        #endregion
        #region Int32
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int32 ConvertToInt32(this object obj)
        {
            return obj.ConvertTo<Int32>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultNullValue">Return value if obj is null or can not convert</param>
        /// <returns></returns>
        public static Int32 ConvertToInt32(this object obj, Int32 defaultNullValue)
        {
            return obj.ConvertTo<Int32>(defaultNullValue);
        }
        #endregion
        #region Int64
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int64 ConvertToInt64(this object obj)
        {
            return obj.ConvertTo<Int64>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultNullValue">Return value if obj is null or can not convert</param>
        /// <returns></returns>
        public static Int64 ConvertToInt64(this object obj, Int64 defaultNullValue)
        {
            return obj.ConvertTo<Int64>(defaultNullValue);
        }
        #endregion
        #region Float
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float ConvertToFloat(this object obj)
        {
            return obj.ConvertTo<float>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultNullValue">Return value if obj is null or can not convert</param>
        /// <returns></returns>
        public static float ConvertToFloat(this object obj, float defaultNullValue)
        {
            return obj.ConvertTo<float>(defaultNullValue);
        }
        #endregion
        #region Double
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ConvertToDouble(this object obj)
        {
            return obj.ConvertTo<double>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultNullValue">Return value if obj is null or can not convert</param>
        /// <returns></returns>
        public static double ConvertToDouble(this object obj, double defaultNullValue)
        {
            return obj.ConvertTo<double>(defaultNullValue);
        }
        #endregion
        #region Decimal
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(this object obj)
        {
            return obj.ConvertTo<decimal>(); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultNullValue">Return value if obj is null or can not convert</param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(this object obj, decimal defaultNullValue)
        {
            return obj.ConvertTo<decimal>(defaultNullValue);
        }
        #endregion
    }
}
