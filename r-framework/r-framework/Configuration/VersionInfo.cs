using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Utility;

namespace r_framework.Configuration
{
    /// <summary>
    /// バージョン情報クラス。AppConfigクラスで設定される
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// 製品名を取得する ex)"環境将軍R"
        /// </summary>
        public string ProductName { get; private set; }

        /// <summary>
        /// 製品情報を取得する ex)"製品Ver1.0"
        /// </summary>
        public string ProductInfo { get; private set; }

        /// <summary>
        /// ベンダ名を取得する ex)"株式会社エジソン"
        /// </summary>
        public string VenderName { get; private set; }

        /// <summary>
        /// ベンダー情報を取得する ex)連絡先など
        /// </summary>
        public string VenderInfo { get; private set; }

        /// <summary>
        /// 顧客名を取得する(カスタマイズバージョンの場合)
        /// </summary>
        public string CustomerName { get; private set; }

        /// <summary>
        /// バージョン説明を取得する
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// メジャーバージョン番号を取得する
        /// </summary>
        public int MajorVersion { get; private set; }

        /// <summary>
        /// マイナーバージョン番号を取得する
        /// </summary>
        public int MinorVersion { get; private set; }

        /// <summary>
        /// ビルド番号を取得する
        /// </summary>
        public int BuildNumber { get; private set; }

        /// <summary>
        /// リビジョン番号を取得する。SVNのリビジョン番号
        /// </summary>
        public int Revision { get; private set; }

        /// <summary>
        /// ビルド日時を取得する
        /// </summary>
        public DateTime BuildDate { get; private set; }

        /// <summary>
        /// コンストラクタ。プロパティはすべてゼロ/空の初期値が設定される。
        /// </summary>
        public VersionInfo()
        {
            ProductName = "";
            ProductInfo = "";
            VenderName = "";
            VenderInfo = "";
            CustomerName = "";
            Description = "";
            MajorVersion = 0;
            MinorVersion = 0;
            BuildNumber = 0;
            Revision = 0;
            BuildDate = new DateTime();
        }
        
        /// <summary>
        /// バージョン番号を設定する
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="build"></param>
        /// <param name="rev"></param>
        /// <param name="date"></param>
        public void SetVersionNumber(int major, int minor, int build, int rev, DateTime date)
        {
            this.MajorVersion = major;
            this.MinorVersion = minor;
            this.BuildNumber = build;
            this.Revision = rev;
            this.BuildDate = date;
        }

        /// <summary>
        /// 製品情報を設定する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="info"></param>
        public void SetProductInfo(string name, string info)
        {
            this.ProductName = name;
            this.ProductInfo = info;
        }

        /// <summary>
        /// ベンダ情報を設定する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="info"></param>
        public void SetVenderInfo(string name, string info)
        {
            this.VenderName = name;
            this.VenderInfo = info;
        }

        /// <summary>
        /// 顧客名を設定する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="info"></param>
        public void SetCustomerName(string name)
        {
            this.CustomerName = name;
        }

        /// <summary>
        /// 説明を設定する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="info"></param>
        public void SetDescription(string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// 簡易バージョン情報テキストを取得する
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var buf = new StringBuilder();
            buf.AppendFormat("Ver{0}.{1}.{2}.{3} ", MajorVersion, MinorVersion, BuildNumber, Revision);
            buf.AppendFormat("Date:{0} ", (BuildDate != null) ? BuildDate.ToString("yyyy/MM/dd HH:mm") : "unknown");

            if (!string.IsNullOrEmpty(ProductName)) buf.AppendFormat("{0} ", ProductName);
            if (!string.IsNullOrEmpty(ProductInfo)) buf.AppendFormat("{0} ", ProductInfo);
            if (!string.IsNullOrEmpty(VenderName)) buf.AppendFormat("{0} ", VenderName);
            if (!string.IsNullOrEmpty(VenderInfo)) buf.AppendFormat("{0} ", VenderInfo);
            if (!string.IsNullOrEmpty(CustomerName)) buf.AppendFormat("{0} ", CustomerName);
            if (!string.IsNullOrEmpty(Description)) buf.AppendFormat("{0} ", Description);

            return buf.ToString();
        }
    }
}
