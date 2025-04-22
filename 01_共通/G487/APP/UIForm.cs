using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Shougun.Core.Common.InsatsuSettei;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using r_framework.APP.PopUp.Base;
using Shougun.Core.Common.InsatsuSettei.Const;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;


namespace Shougun.Core.Common.InsatsuSettei
{

    public partial class UIForm : SuperPopupForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private ReportListPrinterSettingLogic Logic;

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        #endregion

        #region 画面 処理
        public UIForm()
            : base(WINDOW_ID.C_REPORT_PRINTER_SETTING)
        {
            // TODO: Complete member initialization
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new ReportListPrinterSettingLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Logic.WindowInit();

            //TextBox1のLostFocusイベントハンドラを追加する
            this.OUTPUT_KBN_VALUE.LostFocus += new EventHandler(OUTPUT_KBN_VALUE_LostFocus);

            // イベントバンディング
            var allControl = controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }
            this.bt_func1.Focus();
        }

        /// <summary>
        /// フォーカスイン時に実行されるメソッドの追加を行う
        /// </summary>
        /// <param name="c">追加を行う対象のコントロール</param>
        /// <returns></returns>
        private void Control_Enter(Control c)
        {
            c.Enter -= c_GotFocus;
            c.Enter += c_GotFocus;
        }

        /// <summary>
        /// フォーカスが移ったときにヒントテキストを表示する
        /// </summary>
        protected void c_GotFocus(object sender, EventArgs e)
        {
            var activ = this.ActiveControl as SuperPopupForm;

            if (activ == null)
            {
                if (this.ActiveControl != null)
                {
                    this.lb_hint.Text = (string)this.ActiveControl.Tag;
                }
            }
        }

        /// <summary>
        /// 規定値呼出処理 F1 入力データチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormBankSetting(object sender, EventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();
            if (!this.Logic.CheckInputData())
            {
              return;
            }

            int count = 0;
            count = this.Logic.SelectDefaultMarginSettings();
            if (count <= 0)
            {
                //messageShowLogic.MessageBoxShow("E045", "該当データは存在しません。<br>他ユーザーによって削除されたか、未登録のデータです。");
                
                // 設定情報が無い場合は初期値を設定
                this.Logic.SetDefaultPrintermarInfo();
            }
        }

        /// <summary>
        /// F2 プレビュー
        /// </summary>
        public void Preview(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.Logic.TestPrint();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

         /// <summary>
        /// F10 保存データチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormSave(object sender, EventArgs e)
        {
            
            var messageShowLogic = new MessageBoxShowLogic();
            if (!this.Logic.CheckSaveData())
            {
                return;
            }
            
            //プリンターが選択された場合
            if (this.listBoxtOutputPrinter.SelectedIndex > -1)
            {
                this.Logic.RegistXmlFilet(this.listBoxtReprotDispName.SelectedItem.ToString(),
                    this.listBoxtOutputPrinter.SelectedItem.ToString());
            }
            this.Logic.GetLoadXmlPrinterInfo();
            messageShowLogic.MessageBoxShow("I001", "印刷設定の保存");

        }

        /// <summary>
        /// 印刷方向のロストフォーカス処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OUTPUT_KBN_VALUE_LostFocus(object sender, EventArgs e)
        {
            switch (this.OUTPUT_KBN_VALUE.Text)
            {
                case "1": //縦
                    this.OUTPUT_KBN_HORIZONTAL.Checked = true;
                    //this.COLOR_KBN_VALUE.Focus();
                    break;
                case "2"://横           
                    this.OUTPUT_KBN_VERTICAL.Checked = true;
                    //this.COLOR_KBN_VALUE.Focus();
                    break;
                default://空白など
                    this.OUTPUT_KBN_HORIZONTAL.Checked = false;
                    this.OUTPUT_KBN_VERTICAL.Checked = false;
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E001", "印刷方向");
                    //フォーカスを出力区分へ移動
                    this.OUTPUT_KBN_VALUE.Select();
                    break;
            }
        }

        /// <summary>
        /// カラー設定必須チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void COLOR_KBN_VALUE_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.COLOR_KBN_VALUE.Text))
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E001", "カラー設定");
                //フォーカスを出力区分へ移動
                this.COLOR_KBN_VALUE.Select();
            }
        }

        /// <summary>
        /// 印刷方向の値が変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OUTPUT_KBN_VALUE_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.OUTPUT_KBN_VALUE.Text))
            {
                
                this.OUTPUT_KBN_HORIZONTAL.Checked = false;
                this.OUTPUT_KBN_VERTICAL.Checked = false;
                return;
            }
        }


        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
           this.Close();
        }
        #endregion

        #region 出力プリンタ
        /// <summary>
        /// 出力プリンタの選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxtOutputPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PrintDocumentオブジェクトの作成
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            pd.PrinterSettings.PrinterName = this.listBoxtOutputPrinter.SelectedItem.ToString();

            this.Logic.SetPrintersizeInfo(pd);
            this.Logic.SetPrinterDeviceInfo(pd);
            
            //プリンターが選択された場合
            if (this.listBoxtOutputPrinter.SelectedIndex > -1)
            {
                this.Logic.SetDefaultPrinterInfoFromXML(this.listBoxtReprotDispName.SelectedItem.ToString(),
                    this.listBoxtOutputPrinter.SelectedItem.ToString());

                this.lb_hint.Text = this.GetPrinterModelName(this.listBoxtOutputPrinter.SelectedItem.ToString());
            }            
        }

        /// <summary>
        /// プリンタモデル名取得
        /// </summary>
        /// <param name="printer"></param>
        /// <returns></returns>
        private string GetPrinterModelName(string printer)
        {
            try
            {
                var ps = new System.Printing.PrintServer();
                return ps.GetPrintQueues().Where(s => s.Name == printer).Select(s => s.QueueDriver.Name).FirstOrDefault();
            }
            catch(Exception ex)
            {
                // エラー時はログだけ
                LogUtility.Error(ex);
                return String.Empty;
            }
        }
        /// <summary>
        /// 余白設定（ミリ）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxtOutputPaper_SelectedIndexChanged(object sender, EventArgs e)
        {

            //PrintDocumentオブジェクトの作成
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            pd.PrinterSettings.PrinterName = this.listBoxtOutputPrinter.SelectedItem.ToString();

            //this.Logic.SetDefaultPrintermarInfo();  
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxtReprotDispName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //プリンターが選択された場合
            if (this.listBoxtReprotDispName.SelectedIndex > -1)
            {
                // レポート名とプリンタ名をセット
                string reportName = Convert.ToString(this.listBoxtReprotDispName.SelectedItem);
                string printerName = string.Empty;
                List<CurrentUserCustomConfigProfile.SettingsCls.PrintSettings> printSettingsList = this.Logic.XmlResult;
                for (int i = 0; i < printSettingsList.Count; i++)
                {
                    if (printSettingsList[i].Name.Report != null)
                    {
                        if (printSettingsList[i].Name.Report.Equals(reportName)
                            && !string.IsNullOrEmpty(printSettingsList[i].Name.Printer))
                        {
                            printerName = printSettingsList[i].Name.Printer;
                            this.listBoxtOutputPrinter.SetResultText(printerName);
                            break;
                        }
                    }
                }

                this.Logic.SetDefaultPrinterInfoFromXML(reportName, printerName);
            }            
        }     
    }        
}