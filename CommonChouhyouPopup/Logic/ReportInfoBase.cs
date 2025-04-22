using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using C1.C1Report;
using Shougun.Core.Common.BusinessCommon.Xml;

namespace CommonChouhyouPopup.App
{
    #region - Class -

    /// <summary>レポート情報を表すベースクラス</summary>
    public class ReportInfoBase
    {
        #region - Fields -

        /// <summary>レコードを保持するフィールド</summary>
        private Record record = null;

        /// <summary>スタイルを保持するフィールド</summary>
        private Style style = null;

        #endregion - Fields -

        #region - Consructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoBase" /> class.</summary>
        public ReportInfoBase()
        {
            this.DataTableList = new Dictionary<string, DataTable>();
            this.DataTablePageList = new Dictionary<string, Dictionary<string, DataTable>>();
            this.ParameterList = new Dictionary<string, object>();
            
            this.style = new Style();
        }

        /// <summary>Initializes a new instance of the <see cref="ReportInfoBase" /> class.</summary>
        /// <param name="dataTable">データーテーブル</param>
        public ReportInfoBase(DataTable dataTable)
        {
            this.DataTableList = new Dictionary<string, DataTable>();
            this.DataTablePageList = new Dictionary<string, Dictionary<string, DataTable>>();
            this.ParameterList = new Dictionary<string, object>();

            this.style = new Style();

            this.SetRecord(dataTable);
        }

        #endregion - Consructors -

        #region - Enums -

        /// <summary>表示位置に関する列挙型</summary>
        public enum ALIGN_TYPE : int
        {
            /// <summary>中央に関する列挙型</summary>
            Middle = 1,
            
            /// <summary>右寄せに関する列挙型</summary>
            Right = 2,

            /// <summary>左寄せに関する列挙型</summary>
            Left = 3,
        }

        #endregion - Enums -

        #region - Poperties -

        /// <summary>XMLレポート定義情報読み取り有無の状態を保持するプロパティ</summary>
        /// <remarks>真の場合：読み取り済み, 偽の場合：読み取りされていない</remarks>
        public bool IsFormLoadComplete
        {
            get
            {
                return this.style.IsLoadComplete;
            }
        }

        /// <summary>外部からの受け渡しデーターテーブルリストを保持するプロパティ</summary>
        /// <remarks>例 : ヘッダ情報テーブルならばキー名はHeader内容はDataTableのリストとして設定する</remarks>
        public Dictionary<string, DataTable> DataTableList { get; set; }

        /// <summary>外部からの受け渡しデーターテーブルリストを保持するプロパティ</summary>
        /// <remarks>例 : ヘッダ情報テーブルならばキー名はHeader内容はDataTableのリストとして設定する</remarks>
        public Dictionary<string, Dictionary<string, DataTable>> DataTablePageList { get; set; }

        /// <summary>外部からの受け渡しパラメータリストを保持するプロパティ</summary>
        public Dictionary<string, object> ParameterList { get; set; }

        /// <summary>コンポーネントＯＮＥのレポートオブジェクトを保持するプロパティ</summary>
        internal C1Report ComponentOneReport
        {
            get
            {
                return this.style.ComponentOneReport;
            }
        }

        #region XPSファイルプロパティ用
        // タイトル
        public string Title { get; set; }
        // 発行済み
        public string Hakkouzumi { get; set; }
        //帳票ID("R999")
        public string ReportID { get; set; }
        
        #endregion

        #endregion - Poperties -

        #region - Methods -

        /// <summary>レコードの設定処理を実行する</summary>
        /// <param name="dataTable">dataTable</param>
        public void SetRecord(DataTable dataTable)
        {
            this.record = new Record(dataTable);
        }



        /// <summary>フィールドのテキスト名変更処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="text">フィールドテキスト</param>
        public void SetFieldName(string fieldName, string text)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            field.Text = text;
        }

        /// <summary>フィールドの表示・非表示処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="isVisible">表示有無</param>
        public void SetFieldVisible(string fieldName, bool isVisible)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            field.Visible = isVisible;
        }

        /// <summary>フィールドのフォーマット指定処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="format">表示フォーマット</param>
        public void SetFieldFormat(string fieldName, string format)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            field.Format = format;
        }

        /// <summary>フィールドの表示位置指定処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="alignType">アライメントタイプ</param>
        public void SetFieldAlign(string fieldName, ALIGN_TYPE alignType)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            switch (alignType)
            {
                case ALIGN_TYPE.Middle: // 中央寄せ
                    field.Align = FieldAlignEnum.CenterMiddle;
                    field.MarginLeft = 25;
                    field.MarginRight = 25;
                    
                    break;
                case ALIGN_TYPE.Right:  // 右寄せ
                    field.Align = FieldAlignEnum.RightMiddle;
                    field.MarginRight = 50;
                    
                    break;
                case ALIGN_TYPE.Left:   // 左寄せ
                default:
                    field.Align = FieldAlignEnum.LeftMiddle;
                    field.MarginLeft = 50;

                    break;
            }
        }

        /// <summary>フィールドの前景色指定処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="color">前景色</param>
        public void SetFieldForeColor(string fieldName, Color color)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            field.ForeColor = color;
        }

        /// <summary>フィールドの背景色指定処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="color">背景色</param>
        public void SetFieldBackColor(string fieldName, Color color)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            field.BackColor = color;
        }

        /// <summary>フィールドの高さ指定処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="height">高さ</param>
        public void SetFieldHeight(string fieldName, int height)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            field.Height = height;
        }

        /// <summary>
        /// フィールドの幅を指定します
        /// </summary>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="width">幅</param>
        public void SetFieldWidth(string fieldName, int width)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            field.Width = width;
        }

        /// <summary>
        /// フィールドの位置（左）を指定します
        /// </summary>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="left">位置（左）</param>
        public void SetFieldLeft(string fieldName, int left)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            field.Left = left;
        }

        /// <summary>
        /// フィールドの位置（上）を指定します
        /// </summary>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="left">位置（上）</param>
        public void SetFieldTop(string fieldName, int Top)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            field.Top = Top;
        }
        /// <summary>
        /// フィールドの画像を指定します
        /// </summary>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="left">画像ファイルパス</param>
        public void SetFieldImage(string fieldName, string imagePath)
        {

            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            // 画像のファイルパス設定
            field.Picture = imagePath;
            // 画像のスケール設定 
            field.PictureScale = PictureScaleEnum.Scale;　//表示領域に合わせて画像を拡大縮小
            // 画像の前面または背面の設定
            field.ZOrder = -8; //最背面
        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            return result;
        }
        /// <summary>
        ///  カプセル化されたFontが太字かどうかを示す値を取得または設定します。
        /// </summary>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="isBold"></param>
        public void SetFieldBold(string fieldName, bool isBold)
        {
            if (!this.ComponentOneReport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field = this.ComponentOneReport.Fields[fieldName];
            if (field == null)
            {   // フィールド名が存在しない
                return;
            }

            field.Font.Bold = isBold;
        }
        /// <summary>グループの表示・非表示処理を実行する</summary>
        /// <param name="groupName">グループ名を表す文字列</param>
        /// <param name="isHeaderVisible">ヘッダの表示有無</param>
        /// <param name="isFooterVisible">フッタの表示有無</param>
        public void SetGroupVisible(string groupName, bool isHeaderVisible, bool isFooterVisible)
        {
            if (!this.ComponentOneReport.Groups.Contains(groupName))
            {   // キーが存在しない
                return;
            }

            Group group = this.ComponentOneReport.Groups[groupName];
            if (group == null)
            {   // フィールド名が存在しない
                return;
            }

            group.SectionHeader.Visible = isHeaderVisible;
            group.SectionFooter.Visible = isFooterVisible;
        }

        /// <summary>グループ書式の設定処理を実行する</summary>
        /// <param name="groupName">グループ名を表す文字列</param>
        /// <param name="groupBy">グループ書式</param>
        public void SetGroupGroupBy(string groupName, string groupBy)
        {
            if (!this.ComponentOneReport.Groups.Contains(groupName))
            {   // キーが存在しない
                return;
            }

            Group group = this.ComponentOneReport.Groups[groupName];
            if (group == null)
            {   // フィールド名が存在しない
                return;
            }

            group.GroupBy = groupBy;
        }

        /// <summary>サブレポートフィールドのテキスト名変更処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="text">フィールドテキスト</param>
        public void SetSubReportFieldName(string subReportName, string fieldName, string text)
        {
            if (!this.ComponentOneReport.Fields.Contains(subReportName))
            {   // キーが存在しない
                return;
            }

            Field field1 = this.ComponentOneReport.Fields[subReportName];
            if (field1 == null)
            {   // フィールド名が存在しない
                return;
            }

            if (!field1.Subreport.Fields.Contains(fieldName))
            {   // サブレポートキーが存在しない
                return;
            }

            Field field2 = field1.Subreport.Fields[fieldName];
            if (field2 == null)
            {   // フィールド名が存在しない
                return;
            }

            field2.Text = text;
        }

        /// <summary>サブレポートフィールドの表示・非表示処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="isVisible">表示有無</param>
        public void SetSubReportFieldVisible(string subReportName, string fieldName, bool isVisible)
        {
            if (!this.ComponentOneReport.Fields.Contains(subReportName))
            {   // キーが存在しない
                return;
            }

            Field field1 = this.ComponentOneReport.Fields[subReportName];
            if (field1 == null)
            {   // フィールド名が存在しない
                return;
            }

            if (!field1.Subreport.Fields.Contains(fieldName))
            {   // サブレポートキーが存在しない
                return;
            }

            Field field2 = field1.Subreport.Fields[fieldName];
            if (field2 == null)
            {   // フィールド名が存在しない
                return;
            }

            field2.Visible = isVisible;
        }

        /// <summary>サブレポートフィールドのフォーマット指定処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="format">表示フォーマット</param>
        public void SetSubReportFieldFormat(string subReportName, string fieldName, string format)
        {
            if (!this.ComponentOneReport.Fields.Contains(subReportName))
            {   // キーが存在しない
                return;
            }

            Field field1 = this.ComponentOneReport.Fields[subReportName];
            if (field1 == null)
            {   // フィールド名が存在しない
                return;
            }

            if (!field1.Subreport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field2 = field1.Subreport.Fields[fieldName];
            if (field2 == null)
            {   // フィールド名が存在しない
                return;
            }

            field2.Format = format;
        }

        /// <summary>サブレポートフィールドの表示位置指定処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="alignType">アライメントタイプ</param>
        public void SetSubReportFieldAlign(string subReportName, string fieldName, ALIGN_TYPE alignType)
        {
            if (!this.ComponentOneReport.Fields.Contains(subReportName))
            {   // キーが存在しない
                return;
            }

            Field field1 = this.ComponentOneReport.Fields[subReportName];
            if (field1 == null)
            {   // フィールド名が存在しない
                return;
            }

            if (!field1.Subreport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field2 = field1.Subreport.Fields[fieldName];
            if (field2 == null)
            {   // フィールド名が存在しない
                return;
            }

            switch (alignType)
            {
                case ALIGN_TYPE.Middle: // 中央寄せ
                    field2.Align = FieldAlignEnum.CenterMiddle;
                    field2.MarginLeft = 25;
                    field2.MarginRight = 25;

                    break;
                case ALIGN_TYPE.Right:  // 右寄せ
                    field2.Align = FieldAlignEnum.RightMiddle;
                    field2.MarginRight = 50;

                    break;
                case ALIGN_TYPE.Left:   // 左寄せ
                default:
                    field2.Align = FieldAlignEnum.LeftMiddle;
                    field2.MarginLeft = 50;

                    break;
            }
        }

        /// <summary>サブレポートフィールドの前景色指定処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="color">前景色</param>
        public void SetSubReportFieldForeColor(string subReportName, string fieldName, Color color)
        {
            if (!this.ComponentOneReport.Fields.Contains(subReportName))
            {   // キーが存在しない
                return;
            }

            Field field1 = this.ComponentOneReport.Fields[subReportName];
            if (field1 == null)
            {   // フィールド名が存在しない
                return;
            }

            if (!field1.Subreport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field2 = field1.Subreport.Fields[fieldName];
            if (field2 == null)
            {   // フィールド名が存在しない
                return;
            }

            field2.ForeColor = color;
        }

        /// <summary>サブレポートフィールドの背景色指定処理を実行する</summary>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="color">背景色</param>
        public void SetSubReportFieldBackColor(string subReportName, string fieldName, Color color)
        {
            if (!this.ComponentOneReport.Fields.Contains(subReportName))
            {   // キーが存在しない
                return;
            }

            Field field1 = this.ComponentOneReport.Fields[subReportName];
            if (field1 == null)
            {   // フィールド名が存在しない
                return;
            }

            if (!field1.Subreport.Fields.Contains(fieldName))
            {   // キーが存在しない
                return;
            }

            Field field2 = field1.Subreport.Fields[fieldName];
            if (field2 == null)
            {   // フィールド名が存在しない
                return;
            }

            field2.BackColor = color;
        }

        /// <summary>サブレポートグループの表示・非表示処理を実行する</summary>
        /// <param name="groupName">グループ名を表す文字列</param>
        /// <param name="isHeaderVisible">ヘッダの表示有無</param>
        /// <param name="isFooterVisible">フッタの表示有無</param>
        public void SetSubReportGroupVisible(string subReportName, string groupName, bool isHeaderVisible, bool isFooterVisible)
        {
            if (!this.ComponentOneReport.Fields.Contains(subReportName))
            {   // キーが存在しない
                return;
            }

            Field field1 = this.ComponentOneReport.Fields[subReportName];
            if (field1 == null)
            {   // フィールド名が存在しない
                return;
            }

            if (!field1.Subreport.Groups.Contains(groupName))
            {   // キーが存在しない
                return;
            }

            Group group = field1.Subreport.Groups[groupName];
            if (group == null)
            {   // フィールド名が存在しない
                return;
            }

            group.SectionHeader.Visible = isHeaderVisible;
            group.SectionFooter.Visible = isFooterVisible;
        }

        /// <summary>データテーブル情報から帳票情報作成処理を実行する</summary>
        /// <param name="stream">XMLレポート定義を含むストリーム</param>
        /// <param name="reportName">ファイルから取得するレポートの名前（大文字と小文字を区別しない）</param>
        /// <param name="dataTable">データーテーブル</param>
        public void Create(Stream stream, string reportName, DataTable dataTable)
        {
            if (this.style.ComponentOneReport.IsBusy)
            {   // レポートが現在作成中
                return;
            }

            // ストリームのXMLレポート定義からレポートをロード処理する
            this.style.Load(stream, reportName);

            // データテーブルから表示用レコード情報作成およびレコードセット登録処理を実行する
            this.Create();
        }

        /// <summary>データテーブル情報から帳票情報作成処理を実行する</summary>
        /// <param name="fullPathName">XML レポート定義ファイルの完全名</param>
        /// <param name="reportName">ファイルから取得するレポートの名前（大文字小文字を区別）</param>
        /// <param name="dataTable">データーテーブル</param>
        public void Create(string fullPathName, string reportName, DataTable dataTable)
        {
            if (this.style.ComponentOneReport.IsBusy)
            {   // レポートが現在作成中
                return;
            }

            // XML レポート定義ファイルからレポートをロード処理する
            this.style.Load(fullPathName, reportName);

            // データテーブルから表示用レコード情報作成およびレコードセット登録処理を実行する
            this.Create();
        }

        /// <summary>データテーブル情報から帳票情報作成処理を実行する</summary>
        /// <param name="doc">レポートが含まれるSystem.Xml.XmlDocumentへの参照</param>
        /// <param name="reportName">ファイルから取得するレポートの名前（大文字小文字を区別）</param>
        /// <param name="dataTable">データーテーブル</param>
        public void Create(XmlDocument doc, string reportName, DataTable dataTable)
        {
            if (this.style.ComponentOneReport.IsBusy)
            {   // レポートが現在作成中
                return;
            }

            // System.Xml.XmlDocumentからレポートをロード処理する
            this.style.Load(doc, reportName);

            // データテーブルから表示用レコード情報作成およびレコードセット登録処理を実行する
            this.Create();
        }

        #region サブレポートも含めたデータ作成（暫定版）メソッド
        /// <summary>
        /// データテーブル情報から帳票情報作成処理を実行する
        /// ※サブレポートも含めたデータ作成暫定版
        /// </summary>
        /// <param name="doc">レポートが含まれるSystem.Xml.XmlDocumentへの参照</param>
        /// <param name="reportName">ファイルから取得するレポートの名前（大文字小文字を区別）</param>
        /// <param name="dataDictionary">key:メインレポート名orサブレポート「コントロール名」　value:keyに指定したレポート用データテーブル</param>
        public void Create(XmlDocument doc, string reportName, Dictionary<string, DataTable> dataDictionary)
        {
            if (this.style.ComponentOneReport.IsBusy)
            {   // レポートが現在作成中
                return;
            }

            // System.Xml.XmlDocumentからレポートをロード処理する
            this.style.Load(doc, reportName);

            if (!this.style.IsLoadComplete)
            {   // フォームがロードされない
                return;
            }

            foreach (string key in dataDictionary.Keys)
            {
                // データテーブルから表示用レコード情報作成処理を実行する
                DataTable dataTable = dataDictionary[key];
                Record recordData = new Record(dataTable);
                recordData.CreateRecordInfo();

                if (key.Equals(reportName))
                {
                    // メインレポートレコードセット
                    this.style.ComponentOneReport.DataSource.Recordset = recordData;
                }
                else
                {
                    // サブレポートレコードセット
                    Field field = null;
                    if (this.style.ComponentOneReport.Fields.Contains(key))
                    {
                        field = this.style.ComponentOneReport.Fields[key];
                    }

                    if (field != null && field.Subreport != null)
                    {
                        field.Subreport.DataSource.Recordset = recordData;
                    }
                }
            }

            // フィールド状態の更新処理を実行する
            this.UpdateFieldsStatus();
        }

        /// <summary>
        /// データテーブル情報から帳票情報作成処理を実行する
        /// ※サブレポートも含めたデータ作成暫定版
        /// </summary>
        /// <param name="fullPathName">XML レポート定義ファイルの完全名</param>
        /// <param name="reportName">ファイルから取得するレポートの名前（大文字小文字を区別）</param>
        /// <param name="dataDictionary">key:メインレポート名orサブレポート「コントロール名」　value:keyに指定したレポート用データテーブル</param>
        public void Create(string fullPathName, string reportName, Dictionary<string, DataTable> dataDictionary)
        {
            if (this.style.ComponentOneReport.IsBusy)
            {   // レポートが現在作成中
                return;
            }

            // System.Xml.XmlDocumentからレポートをロード処理する
            this.style.Load(fullPathName, reportName);

            if (!this.style.IsLoadComplete)
            {   // フォームがロードされない
                return;
            }

            foreach (string key in dataDictionary.Keys)
            {
                // データテーブルから表示用レコード情報作成処理を実行する
                DataTable dataTable = dataDictionary[key];
                Record recordData = new Record(dataTable);
                recordData.CreateRecordInfo();

                if (key.Equals(reportName))
                {
                    // メインレポートレコードセット
                    this.style.ComponentOneReport.DataSource.Recordset = recordData;
                }
                else
                {
                    // サブレポートレコードセット
                    Field field = null;
                    if (this.style.ComponentOneReport.Fields.Contains(key))
                    {
                        field = this.style.ComponentOneReport.Fields[key];
                    }

                    if (field != null && field.Subreport != null)
                    {
                        field.Subreport.DataSource.Recordset = recordData;
                    }
                }
            }

            // フィールド状態の更新処理を実行する
            this.UpdateFieldsStatus();
        }
        #endregion サブレポートも含めたデータ作成（暫定版）メソッド

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected virtual void UpdateFieldsStatus()
        {
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected virtual void CreateDataTableInfo()
        {
        }

        /// <summary>データがDbNullかどうか取得する</summary>
        /// <param name="data">確認データオブジェクト</param>
        /// <returns>DBNullの場合は真</returns>
        protected bool IsDBNull(object data)
        {
            return DBNull.Value.Equals(data);
        }

        /// <summary>データテーブルから表示用レコード情報作成およびレコードセット登録処理を実行する</summary>
        private void Create()
        {
            if (!this.style.IsLoadComplete)
            {   // まだフォームがロードされていない
                return;
            }

            // 詳細情報作成処理
            this.CreateDataTableInfo();

            // データテーブルから表示用レコード情報作成処理を実行する
            this.record.CreateRecordInfo();

            this.style.ComponentOneReport.DataSource.Recordset = this.record;

            // TODO: 全コントロール取得で動作が遅いので、サブレポート対応のCreateメソッドに移行できるならここを削除
            // サブレポートがあるか検索してレコードセット
            foreach (Field field in this.style.ComponentOneReport.Fields)
            {
                if (field.Subreport != null)
                {
                    field.Subreport.DataSource.Recordset = this.record;
                }
            }

            // フィールド状態の更新処理を実行する
            this.UpdateFieldsStatus();
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
