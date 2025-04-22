using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using C1.C1Report;

namespace CommonChouhyouPopup.App
{
    #region - Enum -

    /// <summary>エラーコードに関する列挙型</summary>
    internal enum ErrorCodeInternal : int
    {
        /// <summary>正常終了</summary>
        OK,

        /// <summary>レポート生成中</summary>
        ReportCreating,

        /// <summary>接続文字列がない</summary>
        NoneConnectString,

        /// <summary>レコードソース文字列(ＳＱＬ文)がない</summary>
        NoneRecordSource,

        /// <summary>フォームがまだロードされていない</summary>
        NotFormLoad,

        /// <summary>オブジェクトがNULL</summary>
        NullObject,
    }

    #endregion - Enum -

    #region - Class -

    /// <summary>レポートコンポーネントのスタイルを表すベースクラス</summary>
    internal class Style
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="Style" /> class.</summary>
        public Style()
        {
            this.ComponentOneReport = new C1Report();

            this.IsLoadComplete = false;
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>レポートコンポーネントオブジェクトを保持するプロパティ</summary>
        public C1Report ComponentOneReport { get; private set; }

        /// <summary>XMLレポート定義情報読み取り有無の状態を保持するプロパティ</summary>
        /// <remarks>真の場合：読み取り済み, 偽の場合：読み取りされていない</remarks>
        public bool IsLoadComplete { get; private set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>ストリームのXMLレポート定義からレポートをロード処理する</summary>
        /// <param name="stream">XMLレポート定義を含むストリーム</param>
        /// <param name="reportName">ファイルから取得するレポートの名前（大文字と小文字を区別しない）</param>
        public void Load(Stream stream, string reportName)
        {
            this.ComponentOneReport.Load(stream, reportName);

            this.IsLoadComplete = true;
        }

        /// <summary>XML レポート定義ファイルからレポートをロード処理する</summary>
        /// <param name="fullPathName">XML レポート定義ファイルの完全名</param>
        /// <param name="reportName">ファイルから取得するレポートの名前（大文字小文字を区別）</param>
        public void Load(string fullPathName, string reportName)
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (!System.Environment.CurrentDirectory.Equals(directory))
            {
                System.Environment.CurrentDirectory = directory;
            }

            this.ComponentOneReport.Load(fullPathName, reportName);
            this.IsLoadComplete = true;
        }

        /// <summary>System.Xml.XmlDocumentからレポートをロード処理する</summary>
        /// <param name="doc">レポートが含まれるSystem.Xml.XmlDocumentへの参照</param>
        /// <param name="reportName">ファイルから取得するレポートの名前（大文字小文字を区別）</param>
        public void Load(XmlDocument doc, string reportName)
        {
            this.ComponentOneReport.Load(doc, reportName);

            this.IsLoadComplete = true;
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
