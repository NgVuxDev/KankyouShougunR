using System.Reflection;

namespace r_framework.Utility
{
    /// <summary>
    /// プロパティ制御クラス
    /// </summary>
    public static class PropertyUtility
    {
        /// <summary>
        /// プロパティ情報取得
        /// </summary>
        /// <param name="obj">プロパティ値が設定されるオブジェクト</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="pi">プロパティ情報</param>
        /// <returns>結果</returns>
        public static bool TryGetInfo(object obj, string propertyName, out PropertyInfo pi)
        {
            pi = null;
            if (obj == null)
            {
                return false;
            }

            pi = obj.GetType().GetProperty(propertyName);
            if (pi == null)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// プロパティ情報取得
        /// </summary>
        /// <param name="obj">プロパティ値が設定されるオブジェクト</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="pi">プロパティ情報</param>
        /// <returns>結果</returns>
        public static bool TryGetInfo(System.Type t, string propertyName, out PropertyInfo pi)
        {
            pi = null;
            if (t == null)
            {
                return false;
            }

            pi = t.GetProperty(propertyName);
            if (pi == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// プロパティの値を設定
        /// </summary>
        /// <param name="obj">プロパティ値が設定されるオブジェクト</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="value">このプロパティの新しい値</param>
        /// <param name="index">インデックス付きプロパティのインデックス値（省略時 null）</param>
        /// <returns>結果</returns>
        public static bool SetValue(object obj, string propertyName, object value, object[] index)
        {
            PropertyInfo pi;
            if (!TryGetInfo(obj, propertyName, out pi))
            {
                return false;
            }

            pi.SetValue(obj, value, index);
            return true;
        }

        /// <summary>
        /// プロパティの値を設定
        /// </summary>
        /// <param name="obj">プロパティ値が設定されるオブジェクト</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="value">このプロパティの新しい値</param>
        /// <returns>結果</returns>
        public static bool SetValue(object obj, string propertyName, object value)
        {
            return SetValue(obj, propertyName, value, null);
        }

        /// <summary>
        /// Text もしくは Value を設定
        /// </summary>
        /// <param name="obj">新しい値</param>
        /// <returns>結果</returns>
        public static bool SetTextOrValue(object obj, object value)
        {
            // 新しい値が文字列型の場合のみTextに設定
            if (value is string)
            {
                if (SetValue(obj, "Text", value))
                {
                    return true;
                }
            }

            // Valueに設定
            if (!SetValue(obj, "Value", value))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 背景色設定
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetBackColor(object obj, System.Drawing.Color value)
        {
            if (SetValue(obj, "BackColor", value))
            {
                return true;
            }

            object temp;
            if (GetValue(obj, "Style", out temp))
            {
                if (SetValue(temp, "BackColor", value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// プロパティの値を取得
        /// </summary>
        /// <param name="obj">プロパティ値が返されるオブジェクト</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="value">このプロパティの値</param>
        /// <param name="index">インデックス付きプロパティのインデックス値</param>
        /// <returns>結果</returns>
        public static bool GetValue(object obj, string propertyName, out object value, out object[] index)
        {
            value = null;
            index = null;
            PropertyInfo pi;
            if (!TryGetInfo(obj, propertyName, out pi))
            {
                return false;
            }

            value = pi.GetValue(obj, index);

            return true;
        }

        /// <summary>
        /// プロパティの値を取得
        /// </summary>
        /// <param name="obj">プロパティ値が設定されるオブジェクト</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="value">このプロパティの値</param>
        /// <returns>結果</returns>
        public static bool GetValue(object obj, string propertyName, out object value)
        {
            object[] index;
            return GetValue(obj, propertyName, out value, out index);
        }

        /// <summary>
        /// Text もしくは Value を文字列で取得(全ての空白を削除)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetTextOrValue(object obj)
        {
            object temp;
            if (!GetValue(obj, "Text", out temp))
            {
                if (!GetValue(obj, "Value", out temp))
                {
                    return string.Empty;
                }
            }

            if (temp == null)
            {
                return string.Empty;
            }

            temp = temp.ToString().Replace(" ", "");
            temp = temp.ToString().Replace("　", "");

            if (string.IsNullOrEmpty(temp.ToString()))
            {
                return string.Empty;
            }

            return temp.ToString();
        }

        /// <summary>
        /// Text もしくは Value を文字列で取得（空白の削除は行わない）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetTextOrValueNotTrimSpace(object obj)
        {
            object temp;
            if (!GetValue(obj, "Text", out temp))
            {
                if (!GetValue(obj, "Value", out temp))
                {
                    return string.Empty;
                }
            }

            if (temp == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(temp.ToString()))
            {
                return string.Empty;
            }

            return temp.ToString();
        }

        /// <summary>
        /// プロパティの値を文字列で取得
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetString(object obj, string propertyName)
        {
            object temp;
            if (GetValue(obj, propertyName, out temp))
            {
                if (temp == null)
                {
                    return string.Empty;
                }
                else
                {
                    return temp.ToString();
                }
            }
            return string.Empty;
        }
    }
}
