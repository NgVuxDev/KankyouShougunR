using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Shougun.Printing.Common;

namespace Shougun.Printing.Common.UI
{
    /// <summary>
    /// 環境将軍R印刷管理/帳票印刷設定ダイアログ(モーダル)
    /// </summary>
    public partial class ReportSettingsDialog : Form
    {
        /// <summary>
        /// 帳票設定ダイアログコンストラクタ
        /// </summary>
        public ReportSettingsDialog()
        {
            InitializeComponent();
            this.BackColor = UI.FormStyle.FormBackColor;
            this.label1.BackColor = UI.FormStyle.LabelBackColor;
            this.label1.ForeColor = UI.FormStyle.LabelForeColor;
            this.label2.BackColor = UI.FormStyle.LabelBackColor;
            this.label2.ForeColor = UI.FormStyle.LabelForeColor;
            this.label3.BackColor = UI.FormStyle.LabelBackColor;
            this.label3.ForeColor = UI.FormStyle.LabelForeColor;
            this.label4.BackColor = UI.FormStyle.LabelBackColor;
            this.label4.ForeColor = UI.FormStyle.LabelForeColor;
            this.label5.BackColor = UI.FormStyle.LabelBackColor;
            this.label5.ForeColor = UI.FormStyle.LabelForeColor;
            this.marginLabel.BackColor = UI.FormStyle.ReadOnlyBackColor;

        }

        /// <summary>
        /// OnLoad
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 「帳票名」リストボックスの項目設定
            foreach (var settings in ReportSettingsManager.Instance.Items)
            {
                settings.Load();
                this.reportNamesListBox.Items.Add(settings.Item.Caption);
            }

            // 通常使うプリンタ名を表示
            this.defualtPrinterNameLabel.Text = (new PrintDocument()).PrinterSettings.PrinterName;
            
            // インストールされているプリンタの一覧をドロップダウンリストボックスに表示
            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                this.printerNamesListBox.Items.Add(printerName);
            }

            // 最初の表示状態を設定
            this.updateControls();

        }

        /// <summary>
        /// 「帳票名」リストボックス選択項目変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reportNameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.printerNamesListBox.Items.Clear();
            // インストールされているプリンタの一覧をドロップダウンリストボックスに表示
            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                this.printerNamesListBox.Items.Add(printerName);
            }
            this.updateControls();
        }

        /// <summary>
        /// 「プリンタ名」ドロップダウンリストボックス選択項目変更
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printerNamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selReportIndex = this.reportNamesListBox.SelectedIndex;
            if (selReportIndex >= 0)
            {
                int selPrinterIndex = this.printerNamesListBox.SelectedIndex;
                if (selPrinterIndex >= 0)
                {
                    // 選択されたプリンタ名で帳票設定を設定しなおす
                    var settings = ReportSettingsManager.Instance.Items[selReportIndex];
                    var selPrinterName = this.printerNamesListBox.Items[selPrinterIndex].ToString();

                    if (string.IsNullOrEmpty(settings.PrinterName) || !settings.PrinterName.Equals(selPrinterName))
                    {
                        settings.PrinterName = selPrinterName;
                        this.updateControls();
                    }
                }
            }
        }

        /// <summary>
        /// 表示更新
        /// </summary>
        private void updateControls()
        {
            int selReportIndex = this.reportNamesListBox.SelectedIndex;
            ReportSettings settings = null;
            int selPrinterIndex = -1;
            string selPrinterName = "";
            string margin = "";
            int DelPrinter = 0;

            // 有効な帳票名が選択されている場合
            if (selReportIndex >= 0)
            {
                settings = ReportSettingsManager.Instance.Items[selReportIndex];

                // 選択されているプリンタ名とリスト項目のインデックス
                selPrinterIndex = this.printerNamesListBox.SelectedIndex;
                if (selPrinterIndex >= 0)
                {
                    selPrinterName = (string)this.printerNamesListBox.Items[selPrinterIndex];
                }
            }

            // 有効な帳票設定が存在する場合
            if (settings != null)
            {
                // 設定内容に変更操作があれば変更フラグ設定

                // プリンタ名のリストの該当項目を特定
                if (string.IsNullOrEmpty(settings.PrinterName))
                {
                    // この帳票はまだプリンタ未選択
                    selPrinterIndex = -1;
                }
                else if (!selPrinterName.Equals(settings.PrinterName))
                {
                    DelPrinter = 1; // 削除されている可能性もあるので、帳票設定のプリンタ名がリスト項目にあるかチェック																																																																																																																																																																																																																																																															
                    // 帳票設定のプリンタ名と同じリスト項目を探す
                    for (int i = 0; i < this.printerNamesListBox.Items.Count; i++)
                    {
                        selPrinterName = (string)this.printerNamesListBox.Items[i];
                        if (settings.PrinterName.Equals(selPrinterName))
                        {
                            selPrinterIndex = i;
                            DelPrinter = 0;
                            break;
                        }
                    }
                }

                margin = string.Format("上{0} 下{1} 左{2} 右{3}", 
                                    settings.Margins.Top, settings.Margins.Bottom, 
                                    settings.Margins.Left, settings.Margins.Right);
            }

            // 表示用の概要説明を作成
            string description = null;
            if (selReportIndex < 0)
            {
                description = "帳票名を選択してください";
            }
            else if (DelPrinter == 1)
            {
                MessageBox.Show(this, string.Format("プリンタ ’{0} ’にアクセスする設定が有効ではありません。\n設定されているプリンタが削除されている可能性があるため、\nプリンタを選択しなおしてください。", settings.PrinterName), "アラート", MessageBoxButtons.OK, MessageBoxIcon.Error);
                selPrinterIndex = -1;
                description = "プリンタを選択してください";
            }
            else if (selPrinterIndex < 0)
            {
                description = "プリンタを選択してください";
            }
            else if (settings != null)
            {
                description = settings.Description;
            }

            // 表示を更新
            if (this.reportNamesListBox.SelectedIndex != selReportIndex)
            {
                this.reportNamesListBox.SelectedIndex = selReportIndex;
            }
            this.printerNamesListBox.Enabled = (selReportIndex >= 0);

            if (this.printerNamesListBox.SelectedIndex != selPrinterIndex)
            {
                this.printerNamesListBox.SelectedIndex = selPrinterIndex;
            }
            this.printerSettingButton.Enabled = (selPrinterIndex >= 0);
            this.marginEditButton.Enabled = (selReportIndex >= 0);

            this.settingsDescriptionLabel.Text = description;
            this.marginLabel.Text = margin;

            this.storeButton.Enabled = ReportSettingsManager.Instance.HasChanges;

            // 設定ファイルが存在する場合、設定クリアボタンを活性化する。
            if (settings != null)
            {
                string filePath = settings.getSettingsStoreFilePath();
                if (File.Exists(filePath))
                {
                    
                    this.settingClearButton.Enabled = true;
                }
                else
                {
                    this.settingClearButton.Enabled = false;
                }
            }
            else
            {
                this.settingClearButton.Enabled = false;
            }
        }
        
        /// <summary>
        /// プリンタ「設定」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printerSettingButton_Click(object sender, EventArgs e)
        {
            var selReportIndex = this.reportNamesListBox.SelectedIndex;
            if (selReportIndex >= 0)
            {
                int selPrinterIndex = this.printerNamesListBox.SelectedIndex;
                if (selPrinterIndex >= 0)
                {
                    // プリンタプロパティ「印刷設定」ダイアログ(ドライバ依存)を呼び出す
                    var settings = ReportSettingsManager.Instance.Items[selReportIndex];
                    try
                    {
                        Byte [] devMode;
                        if (DocumentPropertiesDialog.ShowDialog(this, settings.PrinterName, settings.DevMode, out devMode) == DialogResult.OK)
                        {
                            settings.DevMode = devMode;
                            this.updateControls();
                        }
                    }
                    catch (Exception ex)
                    {
                        LastError.Clear();
                        LastError.Exception = ex;
                        UI.ErrorMessageBox.ShowLastError();
                    }
                }
            }
        }

        /// <summary>
        /// 余白調整「変更」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void marginEditButton_Click(object sender, EventArgs e)
        {
            var selReportIndex = this.reportNamesListBox.SelectedIndex;
            if (selReportIndex >= 0)
            {
                var settings = ReportSettingsManager.Instance.Items[selReportIndex];
                var dialog = new MarginsSettingsDialog();
                dialog.Margins = settings.Margins;
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    settings.Margins = dialog.Margins;
                    this.updateControls();
                }
                dialog.Dispose();
            }
        }

        /// <summary>
        /// F1 設定クリアボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingClearButton_Click(object sender, EventArgs e)
        {
            if (this.settingClearButton.Enabled)
            {
                var result = MessageBox.Show("設定をクリアしますか？", "印刷設定",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // 選択されている帳票から設定情報を取得する。
                    int selReportIndex = this.reportNamesListBox.SelectedIndex;
                    ReportSettings settings = ReportSettingsManager.Instance.Items[selReportIndex];

                    // 設定ファイルのパスを取得する。
                    string filePath = settings.getSettingsStoreFilePath();
                    // 設定ファイルが存在する場合
                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        // 設定ファイル削除
                        File.Delete(filePath);

                        // プリンタ名と余白設定を初期化
                        settings.PrinterName = "";
                        settings.Margins = new Margins();
                        
                        // 変更判定変数を初期化
                        settings.HasChanged = false;
                    }

                    // 表示更新
                    this.updateControls();
                }
            }
        }

        /// <summary>
        /// F10保存ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storeButton_Click(object sender, EventArgs e)
        {
            if (this.storeButton.Enabled)
            {
                var result = MessageBox.Show("変更を保存しますか？", "印刷設定", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (!ReportSettingsManager.Instance.SaveAll())
                    {
                        MessageBox.Show(LastError.Message, "印刷設定", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.updateControls();
                }
            }
        }

        /// <summary>
        /// ユーザー終了確認
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (ReportSettingsManager.Instance.HasChanges)
            {
                var result = MessageBox.Show("変更を破棄して終了してもよろしいですか？", "印刷設定", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            foreach (var settings in ReportSettingsManager.Instance.Items)
            {
                if (settings.HasChanged)
                {
                    settings.Load();
                }
            }

            base.OnFormClosing(e);
        }

        /// <summary>
        /// キー押下イベントハンドラ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            switch (e.KeyData)
            {
                case Keys.F9:
                    storeButton_Click(storeButton, EventArgs.Empty);
                    break;
                case Keys.F12:
                    this.Close();
                    break;
                default:
                    return;
            }
            e.Handled = true;
        }
    }
}
