using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using r_framework.Const;
using r_framework.CustomControl;
using Seasar.Dao.Attrs;

namespace r_framework.Entity
{
    /// <summary>
    /// すべてのEntityの親となるクラス
    /// </summary>
    [TimestampProperty("TIME_STAMP")]
    [Serializable()]
    public class SuperEntity
    {
        /// <summary>
        /// 作成ユーザ
        /// </summary>
        public string CREATE_USER { get; set; }
        /// <summary>
        /// 作成日付
        /// </summary>
        public SqlDateTime CREATE_DATE { get; set; }
        /// <summary>
        /// 検索用作成日付
        /// </summary>
        public string SEARCH_CREATE_DATE { get; set; }
        /// <summary>
        /// 作成者ＰＣ名
        /// </summary>
        public string CREATE_PC { get; set; }
        /// <summary>
        /// 更新ユーザ
        /// </summary>
        public string UPDATE_USER { get; set; }
        /// <summary>
        /// 更新日付
        /// </summary>
        public SqlDateTime UPDATE_DATE { get; set; }
        /// <summary>
        /// 検索用更新日付
        /// </summary>
        public string SEARCH_UPDATE_DATE { get; set; }
        /// <summary>
        /// 更新者ＰＣ名
        /// </summary>
        public string UPDATE_PC { get; set; }
        /// <summary>
        /// TIME_STAMP
        /// </summary>
        public byte[] TIME_STAMP { get; set; }
        /// <summary>
        /// 削除フラッグいるかどうかの判断フラッグ
        /// </summary>
        public SqlBoolean ISNOT_NEED_DELETE_FLG { get; set; }

        /// <summary>
        /// フィールド名を指定してコントロールからEntityへ値を設定する
        /// </summary>
        public void SetValue(ICustomControl cont)
        {
            var resultItem = cont.GetResultText();

            if (string.IsNullOrEmpty(resultItem) && cont.ItemDefinedTypes != DB_TYPE.VARCHAR.ToTypeString())
            {
                return;
            }

            if (cont.ItemDefinedTypes == null)
            {
                return;
            }

            if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.DATETIME.ToTypeString())
            {
                DateTime dt;
                if (DateTime.TryParse(resultItem, out dt))
                {
                    SetDateTime(cont.DBFieldsName, SqlDateTime.Parse(resultItem));
                    SetString("SEARCH_" + cont.DBFieldsName, resultItem.Replace('/', '-'));
                }
                else
                {
                    SetString("SEARCH_" + cont.DBFieldsName, resultItem.Replace('/', '-'));
                }
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.VARCHAR.ToTypeString())
            {
                SetString(cont.DBFieldsName, resultItem);
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.DECIMAL.ToTypeString())
            {
                SetDecimal(cont.DBFieldsName, SqlDecimal.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.BIT.ToTypeString())
            {
                SetBit(cont.DBFieldsName, SqlBoolean.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.INT.ToTypeString())
            {
                SetInteger(cont.DBFieldsName, SqlInt32.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.BIGINT.ToTypeString())
            {
                SetInt64(cont.DBFieldsName, SqlInt64.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.SMALLINT.ToTypeString())
            {
                SetInt16(cont.DBFieldsName, SqlInt16.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.MONEY.ToTypeString())
            {
                SetDecimal(cont.DBFieldsName, SqlDecimal.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.FLOAT.ToTypeString())
            {
                SetDouble(cont.DBFieldsName, SqlDouble.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.TEXT.ToTypeString())
            {
                SetString(cont.DBFieldsName, resultItem);
            }
        }

        /// <summary>
        /// 指定されたコントロールのEntityへ別のコントロールの値を設定する
        /// </summary>
        /// <param name="setCont">設定を行いたいEntityの情報を保持したControl</param>
        /// <param name="getCont">設定する値を取得したいControl</param>
        public void SetValue(ICustomControl setCont, ICustomControl getCont)
        {
            var resultItem = getCont.GetResultText();

            if (string.IsNullOrEmpty(resultItem) && !setCont.ItemDefinedTypes.Contains(DB_TYPE.VARCHAR.ToTypeString()))
            {
                return;
            }

            if (setCont.ItemDefinedTypes == null)
            {
                return;
            }

            if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.DATETIME.ToTypeString())
            {
                DateTime dt;
                if (DateTime.TryParse(resultItem, out dt))
                {
                    SetDateTime(setCont.DBFieldsName, SqlDateTime.Parse(resultItem));
                    SetString("SEARCH_" + setCont.DBFieldsName, resultItem.Replace('/', '-'));
                }
                else
                {
                    SetString("SEARCH_" + setCont.DBFieldsName, resultItem.Replace('/', '-'));
                }
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.VARCHAR.ToTypeString())
            {
                SetString(setCont.DBFieldsName, resultItem);
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.DECIMAL.ToTypeString())
            {
                SetDecimal(setCont.DBFieldsName, SqlDecimal.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.BIT.ToTypeString())
            {
                SetBit(setCont.DBFieldsName, SqlBoolean.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.INT.ToTypeString())
            {
                SetInteger(setCont.DBFieldsName, SqlInt32.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.BIGINT.ToTypeString())
            {
                SetInt64(setCont.DBFieldsName, SqlInt64.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.SMALLINT.ToTypeString())
            {
                SetInt16(setCont.DBFieldsName, SqlInt16.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.MONEY.ToTypeString())
            {
                SetDecimal(setCont.DBFieldsName, SqlDecimal.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.FLOAT.ToTypeString())
            {
                SetDouble(setCont.DBFieldsName, SqlDouble.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.TEXT.ToTypeString())
            {
                SetString(setCont.DBFieldsName, resultItem);
            }
        }

        /// <summary>
        /// フィールド名を指定してEntityへ値を設定する
        /// </summary>
        public void SetValue(string fieldName, string value, string propertyName)
        {
            if (string.IsNullOrEmpty(value) && propertyName != DB_TYPE.VARCHAR.ToEntityDataTypeString())
            {
                return;
            }

            if (propertyName == null)
            {
                return;
            }

            if (propertyName == DB_TYPE.DATETIME.ToEntityDataTypeString())
            {
                DateTime dt;
                if (DateTime.TryParse(value, out dt))
                {
                    SetDateTime(fieldName, SqlDateTime.Parse(value));
                    SetString("SEARCH_" + fieldName, value.Replace('/', '-'));
                }
                else
                {
                    SetString("SEARCH_" + fieldName, value.Replace('/', '-'));
                }
            }
            else if (propertyName == DB_TYPE.VARCHAR.ToEntityDataTypeString())
            {
                SetString(fieldName, value);
            }
            else if (propertyName == DB_TYPE.DECIMAL.ToEntityDataTypeString())
            {
                SetDecimal(fieldName, SqlDecimal.Parse(value));
            }
            else if (propertyName == DB_TYPE.BIT.ToEntityDataTypeString())
            {
                SetBit(fieldName, SqlBoolean.Parse(value));
            }
            else if (propertyName == DB_TYPE.INT.ToEntityDataTypeString())
            {
                SetInteger(fieldName, SqlInt32.Parse(value));
            }
            else if (propertyName == DB_TYPE.BIGINT.ToEntityDataTypeString())
            {
                SetInt64(fieldName, SqlInt64.Parse(value));
            }
            else if (propertyName == DB_TYPE.SMALLINT.ToEntityDataTypeString())
            {
                SetInt16(fieldName, SqlInt16.Parse(value));
            }
            else if (propertyName == DB_TYPE.MONEY.ToEntityDataTypeString())
            {
                SetDecimal(fieldName, SqlDecimal.Parse(value));
            }
            else if (propertyName == DB_TYPE.FLOAT.ToEntityDataTypeString())
            {
                SetDouble(fieldName, SqlDouble.Parse(value));
            }
            else if (propertyName == DB_TYPE.TEXT.ToEntityDataTypeString())
            {
                SetString(fieldName, value);
            }
        }
        /// <summary>
        /// integerの設定を行う
        /// </summary>
        private void SetInteger(string fieldName, SqlInt32 number)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { number });
        }

        /// <summary>
        /// Bitの設定を行う
        /// </summary>
        private void SetBit(string fieldName, SqlBoolean bit)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { bit });
        }

        /// <summary>
        /// Stringの設定を行う
        /// </summary>
        private void SetString(string fieldName, string str)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { str });
        }

        /// <summary>
        /// DateTimeの設定を行う
        /// </summary>
        private void SetDateTime(string fieldName, SqlDateTime dateTime)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { dateTime });
        }

        /// <summary>
        /// Decimalの設定を行う
        /// </summary>
        private void SetDecimal(string fieldName, SqlDecimal sqlDecimal)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { sqlDecimal });
        }

        /// <summary>
        /// SqlInt64の設定を行う
        /// </summary>
        private void SetInt64(string fieldName, SqlInt64 sqlInt64)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { sqlInt64 });
        }

        /// <summary>
        /// SqlInt16の設定を行う
        /// </summary>
        private void SetInt16(string fieldName, SqlInt16 sqlInt16)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { sqlInt16 });
        }

        /// <summary>
        /// SqlDoubleの設定を行う
        /// </summary>
        private void SetDouble(string fieldName, SqlDouble sqlDouble)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { sqlDouble });
        }

        /// <summary>
        /// 全てのプロパティをDictionary(名前、値)に変換
        /// </summary>
        /// <returns>プロパティ名、値のDictionary</returns>
        public Dictionary<string, object> ConvertToDictionary()
        {
            return this.GetType().GetProperties().ToDictionary(s => s.Name, s => s.GetValue(this, null));
        }
    }

    /// <summary>
    /// ph1の互換性を保持用SuperEntity
    /// </summary>
    /// <remarks>
    /// MS_JWNET_MEMBERのみ利用
    /// </remarks>
    [Serializable()]
    [Obsolete("MS_JWNET_MEMBER用、他Entityの継承元はSuperEntityを利用してください。")]
    public class SuperEntityBackward
    {
        /// <summary>
        /// 検索用作成日付
        /// </summary>
        public string SEARCH_CREATE_DATE { get; set; }
        /// <summary>
        /// 検索用更新日付
        /// </summary>
        public string SEARCH_UPDATE_DATE { get; set; }

        /// <summary>
        /// フィールド名を指定してコントロールからEntityへ値を設定する
        /// </summary>
        public void SetValue(ICustomControl cont)
        {
            var resultItem = cont.GetResultText();

            if (string.IsNullOrEmpty(resultItem) && cont.ItemDefinedTypes != DB_TYPE.VARCHAR.ToTypeString())
            {
                return;
            }

            if (cont.ItemDefinedTypes == null)
            {
                return;
            }

            if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.DATETIME.ToTypeString())
            {
                DateTime dt;
                if (DateTime.TryParse(resultItem, out dt))
                {
                    SetDateTime(cont.DBFieldsName, SqlDateTime.Parse(resultItem));
                    SetString("SEARCH_" + cont.DBFieldsName, resultItem.Replace('/', '-'));
                }
                else
                {
                    SetString("SEARCH_" + cont.DBFieldsName, resultItem.Replace('/', '-'));
                }
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.VARCHAR.ToTypeString())
            {
                SetString(cont.DBFieldsName, resultItem);
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.DECIMAL.ToTypeString())
            {
                SetDecimal(cont.DBFieldsName, SqlDecimal.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.BIT.ToTypeString())
            {
                SetBit(cont.DBFieldsName, SqlBoolean.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.INT.ToTypeString())
            {
                SetInteger(cont.DBFieldsName, SqlInt32.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.BIGINT.ToTypeString())
            {
                SetInt64(cont.DBFieldsName, SqlInt64.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.SMALLINT.ToTypeString())
            {
                SetInt16(cont.DBFieldsName, SqlInt16.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.MONEY.ToTypeString())
            {
                SetDecimal(cont.DBFieldsName, SqlDecimal.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.FLOAT.ToTypeString())
            {
                SetDouble(cont.DBFieldsName, SqlDouble.Parse(resultItem));
            }
            else if (cont.ItemDefinedTypes.ToLower() == DB_TYPE.TEXT.ToTypeString())
            {
                SetString(cont.DBFieldsName, resultItem);
            }
        }

        /// <summary>
        /// 指定されたコントロールのEntityへ別のコントロールの値を設定する
        /// </summary>
        /// <param name="setCont">設定を行いたいEntityの情報を保持したControl</param>
        /// <param name="getCont">設定する値を取得したいControl</param>
        public void SetValue(ICustomControl setCont, ICustomControl getCont)
        {
            var resultItem = getCont.GetResultText();

            if (string.IsNullOrEmpty(resultItem) && !setCont.ItemDefinedTypes.Contains(DB_TYPE.VARCHAR.ToTypeString()))
            {
                return;
            }

            if (setCont.ItemDefinedTypes == null)
            {
                return;
            }

            if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.DATETIME.ToTypeString())
            {
                DateTime dt;
                if (DateTime.TryParse(resultItem, out dt))
                {
                    SetDateTime(setCont.DBFieldsName, SqlDateTime.Parse(resultItem));
                    SetString("SEARCH_" + setCont.DBFieldsName, resultItem.Replace('/', '-'));
                }
                else
                {
                    SetString("SEARCH_" + setCont.DBFieldsName, resultItem.Replace('/', '-'));
                }
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.VARCHAR.ToTypeString())
            {
                SetString(setCont.DBFieldsName, resultItem);
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.DECIMAL.ToTypeString())
            {
                SetDecimal(setCont.DBFieldsName, SqlDecimal.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.BIT.ToTypeString())
            {
                SetBit(setCont.DBFieldsName, SqlBoolean.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.INT.ToTypeString())
            {
                SetInteger(setCont.DBFieldsName, SqlInt32.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.BIGINT.ToTypeString())
            {
                SetInt64(setCont.DBFieldsName, SqlInt64.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.SMALLINT.ToTypeString())
            {
                SetInt16(setCont.DBFieldsName, SqlInt16.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.MONEY.ToTypeString())
            {
                SetDecimal(setCont.DBFieldsName, SqlDecimal.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.FLOAT.ToTypeString())
            {
                SetDouble(setCont.DBFieldsName, SqlDouble.Parse(resultItem));
            }
            else if (setCont.ItemDefinedTypes.ToLower() == DB_TYPE.TEXT.ToTypeString())
            {
                SetString(setCont.DBFieldsName, resultItem);
            }
        }

        /// <summary>
        /// フィールド名を指定してEntityへ値を設定する
        /// </summary>
        public void SetValue(string fieldName, string value, string propertyName)
        {
            if (string.IsNullOrEmpty(value) && propertyName != DB_TYPE.VARCHAR.ToEntityDataTypeString())
            {
                return;
            }

            if (propertyName == null)
            {
                return;
            }

            if (propertyName == DB_TYPE.DATETIME.ToEntityDataTypeString())
            {
                DateTime dt;
                if (DateTime.TryParse(value, out dt))
                {
                    SetDateTime(fieldName, SqlDateTime.Parse(value));
                    SetString("SEARCH_" + fieldName, value.Replace('/', '-'));
                }
                else
                {
                    SetString("SEARCH_" + fieldName, value.Replace('/', '-'));
                }
            }
            else if (propertyName == DB_TYPE.VARCHAR.ToEntityDataTypeString())
            {
                SetString(fieldName, value);
            }
            else if (propertyName == DB_TYPE.DECIMAL.ToEntityDataTypeString())
            {
                SetDecimal(fieldName, SqlDecimal.Parse(value));
            }
            else if (propertyName == DB_TYPE.BIT.ToEntityDataTypeString())
            {
                SetBit(fieldName, SqlBoolean.Parse(value));
            }
            else if (propertyName == DB_TYPE.INT.ToEntityDataTypeString())
            {
                SetInteger(fieldName, SqlInt32.Parse(value));
            }
            else if (propertyName == DB_TYPE.BIGINT.ToEntityDataTypeString())
            {
                SetInt64(fieldName, SqlInt64.Parse(value));
            }
            else if (propertyName == DB_TYPE.SMALLINT.ToEntityDataTypeString())
            {
                SetInt16(fieldName, SqlInt16.Parse(value));
            }
            else if (propertyName == DB_TYPE.MONEY.ToEntityDataTypeString())
            {
                SetDecimal(fieldName, SqlDecimal.Parse(value));
            }
            else if (propertyName == DB_TYPE.FLOAT.ToEntityDataTypeString())
            {
                SetDouble(fieldName, SqlDouble.Parse(value));
            }
            else if (propertyName == DB_TYPE.TEXT.ToEntityDataTypeString())
            {
                SetString(fieldName, value);
            }
        }
        /// <summary>
        /// integerの設定を行う
        /// </summary>
        private void SetInteger(string fieldName, SqlInt32 number)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { number });
        }

        /// <summary>
        /// Bitの設定を行う
        /// </summary>
        private void SetBit(string fieldName, SqlBoolean bit)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { bit });
        }

        /// <summary>
        /// Stringの設定を行う
        /// </summary>
        private void SetString(string fieldName, string str)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { str });
        }

        /// <summary>
        /// DateTimeの設定を行う
        /// </summary>
        private void SetDateTime(string fieldName, SqlDateTime dateTime)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { dateTime });
        }

        /// <summary>
        /// Decimalの設定を行う
        /// </summary>
        private void SetDecimal(string fieldName, SqlDecimal sqlDecimal)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { sqlDecimal });
        }

        /// <summary>
        /// SqlInt64の設定を行う
        /// </summary>
        private void SetInt64(string fieldName, SqlInt64 sqlInt64)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { sqlInt64 });
        }

        /// <summary>
        /// SqlInt16の設定を行う
        /// </summary>
        private void SetInt16(string fieldName, SqlInt16 sqlInt16)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { sqlInt16 });
        }

        /// <summary>
        /// SqlDoubleの設定を行う
        /// </summary>
        private void SetDouble(string fieldName, SqlDouble sqlDouble)
        {
            this.GetType().InvokeMember(fieldName, BindingFlags.SetProperty, null, this, new object[] { sqlDouble });
        }

        /// <summary>
        /// 全てのプロパティをDictionary(名前、値)に変換
        /// </summary>
        /// <returns>プロパティ名、値のDictionary</returns>
        public Dictionary<string, object> ConvertToDictionary()
        {
            return this.GetType().GetProperties().ToDictionary(s => s.Name, s => s.GetValue(this, null));
        }
    }
}