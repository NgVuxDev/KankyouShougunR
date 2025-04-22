using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using C1.C1Preview.Export;
using C1.Win.C1Preview;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Utility;
using r_framework.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;

namespace CommonChouhyouPopup.App
{
    #region - Class -
    /// <summary>印刷ポップアップを表すクラス・コントロール</summary>
    public class FormReportPrintPopup : FormReport
    {
        public FormReportPrintPopup(ReportInfoBase reportInfoBase, WINDOW_ID windowID = WINDOW_ID.NONE) 
            : base(reportInfoBase, windowID)
        {
        }

        public FormReportPrintPopup(ReportInfoBase[] reportInfoBase, WINDOW_ID windowID = WINDOW_ID.NONE)
            : base(reportInfoBase, windowID)
        {
        }

        public FormReportPrintPopup(ReportInfoBase reportInfoBase, string projectId, WINDOW_ID windowID = WINDOW_ID.NONE)
            : base(reportInfoBase, projectId, windowID)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="FormReport" /> class.</summary>
        /// <param name="reportInfoBase">レポート情報の基本クラス</param>
        /// <param name="projectId">帳票プロジェクトID</param>
        public FormReportPrintPopup(ReportInfoBase[] reportInfoBase, string projectId, WINDOW_ID windowID = WINDOW_ID.NONE)
            : base(reportInfoBase, projectId, windowID)
        {
        }


        #region - Properties -

        /// <summary>プリンター設定情報を保持するプロパティ</summary>
        /// 削除予定のG487でしか参照していない。不要なプロパティ
        public PrinterSettings PrinterSettingInfo { get; set; }

        /// <summary>帳票用キャプションを保持するプロパティ</summary>
        public string ReportCaption
        {
            get
            {
                return this.Caption;
            }


            set
            {
                this.Caption = value; 
            }
        }


        #endregion - Properties -

        #region - Methods -

        /// <summary>印刷処理を実行する</summary>
        public void Print()
        {
            // ShowDialogを独自実装し印刷ボタンクリックによる呼び出しがないので、ここに来る場合は直接印刷
            this.PrintInitAction = 1;
            this.PrintXPS();

        }

        /// <summary>
        /// XPSファイル形式で印刷処理を実行する
        /// 汎用帳票などの呼び出し元の変更が間に合わないため無理やりここで出力させる。
        /// </summary>
        public DialogResult ShowDialog()
        {
            // ここが呼ばれる場合はポップアップ表示
            this.PrintInitAction = 3;
            this.PrintXPS();
            return DialogResult.OK;
        }

        /// <summary>
        /// XPSファイル形式で印刷処理を実行する
        /// 汎用帳票などの呼び出し元の変更が間に合わないため無理やりここで出力させる。
        /// </summary>
        public void Show()
        {
            // ここが呼ばれる場合はポップアップ表示
            this.PrintInitAction = 3;
            this.PrintXPS();
        }


        /// <summary>エクセル出力処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void ButtonExcel_Click(object sender, EventArgs e)
        {
            string chouhyouName = string.Empty;
            string fullPathNameBase = string.Empty;
            string fullPathName = string.Empty;
            string defaultFullPathName = string.Empty;

            if (this.windowID != WINDOW_ID.NONE)
            {
                chouhyouName = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.windowID);
            }
            else
            {
                chouhyouName = "Default";
            }

            string fileName = chouhyouName + (this.IsOutputPDF ? ".pdf" : ".xls");

            // ファイルを保存するダイアログを表示
            DialogResult dialogResult = this.ShowSaveFileDialog(fileName, ref fullPathName);

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            string path = string.Empty;
            string exp;

            for (int i = 0; i < this.reportInfoCount; i++)
            {
                if (!this.reportInfoBase[i].IsFormLoadComplete)
                {   // フォームがロードされていない
                    return;
                }

                if (this.reportInfoBase[i].ComponentOneReport.IsBusy)
                {   // レポート処理中
                    return;
                }

                if (this.reportInfoCount != 1)
                {
                    path = Path.GetDirectoryName(fullPathName);
                    fileName = Path.GetFileNameWithoutExtension(fullPathName);
                    exp = Path.GetExtension(fullPathName);

                    fullPathName = path + string.Format(@"\{0}-{1}{2}", fileName, i + 1, exp);
                }

                if (File.Exists(fullPathName))
                {   // 既にファイルが存在している
                    File.Delete(fullPathName);
                }

                // ファイル書き込み
                this.reportInfoBase[i].ComponentOneReport.C1Document.Export(fullPathName);
            }
        }

        /// <summary>ファイルを保存するダイアログを表示する処理を実行する</summary>
        /// <param name="defaultFileName">デフォルトファイル名を表す文字列</param>
        /// <param name="fullPathName">指定された保存ファイルフルパス名を表す文字列</param>
        /// <returns>ダイアログ結果</returns>
        private DialogResult ShowSaveFileDialog(string defaultFileName, ref string fullPathName)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            string executablePath = System.Windows.Forms.Application.ExecutablePath;

            string path = Path.GetDirectoryName(executablePath);
            string fileName = defaultFileName;

            // はじめのファイル名を指定する
            sfd.FileName = fileName;

            // はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = path;

            // [ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = this.IsOutputPDF ? "PDFファイル(*.pdf)|*.pdf|すべてのファイル(*.*)|*.*" : "EXCELファイル(*.xls)|*.xls|すべてのファイル(*.*)|*.*";

            // [ファイルの種類]で始めにエクセル又はＰＤＦのファイル」が選択されているようにする
            sfd.FilterIndex = 1;

            // タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";

            // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;

            // 既に存在するファイル名を指定したとき警告する(デフォルトでTrueなので指定する必要はない)
            sfd.OverwritePrompt = true;

            // 存在しないパスが指定されたとき警告を表示する(デフォルトでTrueなので指定する必要はない)
            sfd.CheckPathExists = true;

            // ダイアログを表示する
            DialogResult dialogResult = sfd.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                fullPathName = sfd.FileName;
            }

            return dialogResult;
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
