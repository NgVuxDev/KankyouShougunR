using System;
using System.Xml.Serialization;

namespace r_framework.Dto
{
    /// <summary>
    /// フォーマット形式
    /// </summary>
    public enum FORMAT_TYPE
    {
        /// <summary>正規表現</summary>
        [XmlEnum(Name = "Expression")]
        Expression = 0,
        /// <summary>数値フォーマット</summary>
        [XmlEnum(Name = "Numeric")]
        Numeric = 1,
        /// <summary>システム設定(M_SYS_INFO参照)</summary>
        [XmlEnum(Name = "SysInfo")]
        SysInfo = 2,
        /// <summary>カスタム書式(CustomFormatSettingプロパティで指定された形式でフォーマット)</summary>
        /// <remarks>「#」「,」「0」「.」の組合せ形式のフォーマットのみ対応</remarks>
        [XmlEnum(Name = "Custom")]
        Custom = 3,
    }

    /// <summary>
    /// フォーマット設定DTO
    /// </summary>
    [Serializable]
    public class FormatSettingDto
    {
        /// <summary>フォーマット形式</summary>
        public FORMAT_TYPE FormatType { get; set; }
        /// <summary>フォーマット表示名</summary>
        public string FormatName { get; set; }
        /// <summary>正規表現（Expressionで使用）</summary>
        public string Expression { get; set; }
        /// <summary>数値用フォーマット（数値フォーマットで使用）</summary>
        public string Format { get; set; }
        /// <summary>DB参照用項目名（システム設定参照で使用）</summary>
        public string ColumnName { get; set; }
    }
}
