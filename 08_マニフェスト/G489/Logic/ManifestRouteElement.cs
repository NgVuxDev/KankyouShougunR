using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PaperManifest.InsatsuBusuSettei
{
    /// <summary>マニフェストルート共通要素(名称/郵便番号/電話番号/住所)</summary>
    internal class ManifestRouteElement
    {
        /// <summary>名称</summary>
        public string Name;
        /// <summary>郵便番号</summary>
        public string Post;
        /// <summary>電話番号</summary>
        public string Tel;
        /// <summary>住所</summary>
        public string Address;

        /// <summary>テンプレートセット用メソッド(CommonChouhyouPopup.App.ReportInfoBase.SetFieldName)</summary>
        private Action<string, string> setFieldName;

        public ManifestRouteElement()
        {
            this.Name = String.Empty;
            this.Post = String.Empty;
            this.Tel = String.Empty;
            this.Address = String.Empty;
        }

        public ManifestRouteElement(Action<string, string> setfieldName)
        {
            this.setFieldName = setfieldName;
            this.Name = String.Empty;
            this.Post = String.Empty;
            this.Tel = String.Empty;
            this.Address = String.Empty;
        }

        /// <summary>
        /// テンプレートへ項目の値設定
        /// </summary>
        /// <param name="setFieldName"></param>
        /// <param name="fields">名称/郵便番号/電話番号/住所</param>
        public void SetReportField(string[] fields)
        {
            if (this.setFieldName == null)
            {
                throw new NullReferenceException("Action setFieldName is null");
            }
            this.setFieldName(fields[0], this.Name);
            this.setFieldName(fields[1], this.Post);
            this.setFieldName(fields[2], this.Tel);
            this.setFieldName(fields[3], this.Address);
        }

        public override string ToString()
        {
            return String.Format("Name = {0}, Post = {1}, Adress = {2}, Tel = {3}", this.Name, this.Post, this.Address, this.Tel);
        }
    }
}
