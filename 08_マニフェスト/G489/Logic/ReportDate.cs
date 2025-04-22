using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PaperManifest.InsatsuBusuSettei
{
    /// <summary>
    /// 帳票に印字する日付関係
    /// </summary>
    internal class ReportDate
    {
        /// <summary>和暦変換用カルチャ</summary>
        private System.Globalization.CultureInfo cultureInfoJa = new System.Globalization.CultureInfo("ja-JP");

        /// <summary>テンプレートセット用メソッド</summary>
        private Action<string, string> setFieldName;

        /// <summary>年/月/日 0=年/1=月/2=日</summary>
        private string[] ymd = new string[] { "", "", "" };

        /// <summary>基になる日付</summary>
        private DateTime originalDate = DateTime.MinValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setfieldName"></param>
        /// <param name="orgDate"></param>
        /// <param name="isJapanese">和暦かどうか。デフォルトはtrue(和暦)、falseでシステム既定</param>
        /// <param name="yearformat">年のフォーマット(yyyy or yy)、デフォルトはyy。isJapanese=true時は無視される</param>
        public ReportDate(Action<string, string> setfieldName, string orgDate, bool isJapanese = true, string yearformat = "yy")
        {
            // 和暦設定
            this.cultureInfoJa.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();

            this.setFieldName = setfieldName;

            if (!string.IsNullOrWhiteSpace(orgDate) && orgDate.Trim() != "Null")
            {
                if (DateTime.TryParse(orgDate, out this.originalDate))
                {
                    this.ymd[0] = isJapanese ? this.originalDate.ToString("yy", this.cultureInfoJa) : this.originalDate.ToString(yearformat);
                    this.ymd[1] = this.originalDate.ToString("MM");
                    this.ymd[2] = this.originalDate.ToString("dd");
                }
                else
                {
                    throw new InvalidCastException(String.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E148"), orgDate));
                }
            }
        }

        /// <summary>
        /// テンプレートへ日付項目の値設定
        /// </summary>
        /// <param name="fields">年/月/日</param>
        public void SetReportField(string[] fields)
        {
            if (this.setFieldName == null)
            {
                throw new NullReferenceException("Action setFieldName is null");
            }

            for (var i = 0; i < this.ymd.Length; i++)
            {
                this.setFieldName(fields[i], this.ymd[i]);
            }
        }

        public string ToString(string format)
        {
            if (this.ymd.Any(s => String.IsNullOrWhiteSpace(s)))
            {
                return String.Empty;
            }
            else
            {
                return this.originalDate.ToString(format);
            }
        }
    }
}
