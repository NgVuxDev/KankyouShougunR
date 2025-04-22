using System;
using System.ComponentModel;
using System.Reflection;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    public static class EnumUtil
    {
        public static string ToEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static bool IsDefined<T>(string strEnumText)
        {
            string s = strEnumText.Trim().ToUpper();
            string[] names = Enum.GetNames(typeof(T));
            foreach (string n in names)
            {
                if (n.ToUpper() == s)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsDefined<T>(int iEnumValue)
        {
            return Enum.IsDefined(typeof(T), iEnumValue);
        }

        public static T ToEnum<T>(this int iEnumValue)
        {
            return Parse<T>(iEnumValue);
        }

        public static T ToEnum<T>(this string strEnumText) where T : struct
        {
            return Parse<T>(strEnumText);
        }

        public static T ToEnum<T>(this string strEnumText, bool ignoreCase) where T : struct
        {
            return Parse<T>(strEnumText, ignoreCase);
        }

        public static T Parse<T>(int iEnumValue)
        {
            return (T)Enum.ToObject(typeof(T), iEnumValue);
        }

        public static T Parse<T>(string strEnumText) where T : struct
        {
            return (T)Enum.Parse(typeof(T), strEnumText, true);
        }

        public static T Parse<T>(string strEnumText, bool ignoreCase) where T : struct
        {
            return (T)Enum.Parse(typeof(T), strEnumText, ignoreCase);
        }
    }
}
