using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PaperManifest.InsatsuBusuSettei
{
    /// <summary>運搬の受託</summary>
    internal class UpnJyutakuElement
    {
        /// <summary>受託者の氏名又は名称</summary>
        public string Name;
        /// <summary>運搬担当者の氏名</summary>
        public string Person;
        /// <summary>運搬終了年月日</summary>
        public ReportDate EndDate;
        /// <summary>有価物収拾量</summary>
        public string Number;
        /// <summary>有価物収拾量　単位</summary>
        public string Unit;

        /// <summary>テンプレートセット用メソッド(CommonChouhyouPopup.App.ReportInfoBase.SetFieldName)</summary>
        private Action<string, string> setFieldName;

        public UpnJyutakuElement()
        {
            this.Name = String.Empty;
            this.Person = String.Empty;
            this.Number = String.Empty;
            this.Unit = String.Empty;
        }

        public UpnJyutakuElement(Action<string, string> setfieldName)
        {
            this.setFieldName = setfieldName;
            this.Name = String.Empty;
            this.Person = String.Empty;
            this.Number = String.Empty;
            this.Unit = String.Empty;
        }

        /// <summary>
        /// テンプレートへ項目の値設定
        /// </summary>
        /// <param name="setFieldName"></param>
        /// <param name="fields">名称/運搬担当/運搬終了年/運搬終了月/運搬終了日/有価物収拾量/単位</param>
        public void SetReportField(string[] fields)
        {
            if (this.setFieldName == null)
            {
                throw new NullReferenceException("Action setFieldName is null");
            }
            setFieldName(fields[0], this.Name);
            setFieldName(fields[1], this.Person);
            this.EndDate.SetReportField(new[] { fields[2], fields[3], fields[4] });
            if (!String.IsNullOrWhiteSpace(fields[5]))
            {
                setFieldName(fields[5], this.Number);
            }
            if (!String.IsNullOrWhiteSpace(fields[6]))
            {
                setFieldName(fields[6], this.Unit);
            }
        }

        public override string ToString()
        {
            return String.Format("Name = {0}, Person = {1}", this.Name, this.Person);
        }
    }
}
