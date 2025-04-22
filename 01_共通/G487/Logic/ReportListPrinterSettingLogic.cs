using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using Seasar.Quill.Attrs;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using Shougun.Core.Common.InsatsuSettei.DAO;
using System.Drawing;
using System.Data.SqlTypes;
using Shougun.Core.Common.InsatsuSettei.Const;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using r_framework.APP.PopUp.Base;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections;
using System.Xml.Serialization;
using System.Drawing.Printing;
using r_framework.CustomControl;
using System.ComponentModel;
using r_framework.Dto;
using CommonChouhyouPopup.App;
using Shougun.Core.Scale.Keiryou;
using Shougun.Core.Scale.Keiryou.Dto;
using Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku;

namespace Shougun.Core.Common.InsatsuSettei
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class ReportListPrinterSettingLogic
    {
        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Common.InsatsuSettei.Setting.ButtonSetting.xml";

        /// <summary>
        /// Enum
        /// </summary>
        public enum CONDITION_TYPE
        {
            SELECT_DMS = 0,
            INSERT_DMS = 1,
            UPDATE_DMS = 2
        }
        private CONDITION_TYPE _type;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm MyForm;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        /// <summary>
        /// 更新条件
        /// </summary>
        public List<S_DEFAULTMARGINSETTINGS> DmsList { get; set; }

        /// <summary>
        /// 帳票名 - 計量票
        /// </summary>
        private const string REPORT_NAME_KEIRYOUHYOU = "計量票";
        /// <summary>
        /// 帳票名 - 請求書
        /// </summary>
        private const string REPORT_NAME_SEIKYUSHO = "請求書";
        /// <summary>
        /// 帳票名 - 支払明細書
        /// </summary>
        private const string REPORT_NAME_SHIHARAIMEISAISHO = "支払明細書";
        /// <summary>
        /// 帳票名 - 見積書
        /// </summary>
        private const string REPORT_NAME_MITSUMORISHO = "見積書";
        /// <summary>
        /// 帳票名 - 配車指示書
        /// </summary>
        private const string REPORT_NAME_HAISHASHIJISHO = "指示書";
        /// <summary>
        /// 帳票名 - 領収書
        /// </summary>
        private const string REPORT_NAME_RYOUSHUSHO = "領収書";
        /// <summary>
        /// 帳票名 - 仕切書
        /// </summary>
        private const string REPORT_NAME_SHIKIRISHO = "仕切書";

        #region プロパティ

        /// <summary>
        /// 印刷設定帳票リスト
        /// </summary>
        public SystemProperty.PrintSettings.ReportInfo[] ReportSettings { get; set; } 

        /// <summary>
        /// 検索条件
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>
        /// XMLロード結果
        /// </summary>
        public List<CurrentUserCustomConfigProfile.SettingsCls.PrintSettings> XmlResult { get; set; }

        /// <summary>
        /// CurrentUserCustomConfigProfile結果
        /// </summary>
        public CurrentUserCustomConfigProfile CurrentUserCustomConfigProfileResult { get; set; }

        /// <summary>
        /// 処理区分
        /// </summary>
        public PROCESS_KBN ProcessKbn { get; set; }

        /// <summary>
        /// 登録時エラーフラグ
        /// </summary>
        public bool RegistErrorFlag = true;

        /// <summary>
        /// フォーカスアウトエラーフラグ
        /// </summary>
        public bool FocusOutErrorFlag = false;

        /// <summary>
        /// 画面に表示しているすべてのコントロールを格納するフィールド
        /// </summary>
        public Control[] allControl;

        /// <summary>
        /// 現在フォーカスがあたっているコントロール
        /// </summary>
        [Category("EDISONプロパティ")]
        public Control FoucusControl { get; set; }

        public IS_REPORTLISTPRINTERSETTINGSDao daoReportListPrinterSetting;
        public Shougun.Core.Common.InsatsuSettei.DAO.IS_DEFAULTMARGINSETTINGSDao daoSetDefaultMarginSetting;
        public Shougun.Core.Common.InsatsuSettei.DAO.IS_DEFAULTMARGINSETTINGSDao daoGetDefaultMarginSetting;
        public Shougun.Core.Common.InsatsuSettei.DAO.IS_DEFAULTMARGINSETTINGSDao daoUpdDefaultMarginSetting;
        private DBAccessor CommonDBAccessor;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        #endregion

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ReportListPrinterSettingLogic(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.MyForm = targetForm;
            this.MyForm.KeyPreview = true;

            this.daoSetDefaultMarginSetting = DaoInitUtility.GetComponent<Shougun.Core.Common.InsatsuSettei.DAO.IS_DEFAULTMARGINSETTINGSDao>();
            this.daoGetDefaultMarginSetting = DaoInitUtility.GetComponent<Shougun.Core.Common.InsatsuSettei.DAO.IS_DEFAULTMARGINSETTINGSDao>();
            this.daoUpdDefaultMarginSetting = DaoInitUtility.GetComponent<Shougun.Core.Common.InsatsuSettei.DAO.IS_DEFAULTMARGINSETTINGSDao>();

            this.CommonDBAccessor = new DBAccessor();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンのテキストを初期化
            this.ButtonInit();

            //通常使うプリンタ
            this.GenaralPrinter();

            //プリンタ情報の初期値の設定
            this.SetDefaultPrinterInfo();

            //XMLプリンタ
            this.GetLoadXmlPrinterInfo();

            // 使用可能なプリンタをリスト表示
            this.SetPrinterInfo();

            // ﾏﾆﾌｪｽﾄ印字ﾌﾟﾘﾝﾀ表示
            this.MyForm.ptlRecognizeReport.Text = this.CurrentUserCustomConfigProfileResult.Settings.ManifestPrinter;

            // システム情報から帳票リストを取得
            this.ReportSettings = SystemProperty.PrintSettings.ReportList.ToArray();
            if (this.ReportSettings.Length > 0)
            {
                this.SetReportListPrinter();
                this.MyForm.listBoxtReprotDispName.SelectedIndex = 0;

            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region ボタン処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (SuperPopupForm)this.MyForm;
            var controlUtil = new ControlUtility();

            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
            }

            // イベントの初期化処理
            this.EventInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            //生成したアセンブリの情報を送って
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BasePopForm)this.MyForm.Parent;

            //余白ボタン(F1)イベント生成
            this.MyForm.bt_func1.DialogResult = DialogResult.None;
            this.MyForm.bt_func1.Click += new EventHandler(this.MyForm.FormBankSetting);

            //ﾌﾟﾚﾋﾞｭｰボタン(F2)イベント生成
            this.MyForm.bt_func2.Click += new EventHandler(this.MyForm.Preview);

            //保存ボタン(F10)イベント生成
            this.MyForm.bt_func10.Click += new EventHandler(this.MyForm.FormSave);


            //閉じるボタン(F12)イベント生成
            this.MyForm.bt_func12.DialogResult = DialogResult.Cancel;
            this.MyForm.bt_func12.Click += new EventHandler(this.MyForm.FormClose);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region　帳票名出力
        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        /// <returns></returns>
        public void SetReportListPrinter()
        {
            LogUtility.DebugMethodStart();

            //前の結果をクリア
            int k = this.MyForm.listBoxtReprotDispName.Items.Count;
            if (k > 0)
            {
                this.MyForm.listBoxtReprotDispName.Items.Clear();
            }

            //検索結果設定
            for (int i = 0; i < this.ReportSettings.Length; i++)
            {
                //画面表示項目
                this.MyForm.listBoxtReprotDispName.Items.Add(this.ReportSettings[i].DispName);
            } 

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region XMLからデフォルトログイン情報取得
        /// <summary>
        /// XMLからデフォルトログイン情報取得
        /// </summary>
        /// <returns></returns>
        public void GetLoadXmlPrinterInfo()
        {
            LogUtility.DebugMethodStart();

            this.XmlResult = null;
            CurrentUserCustomConfigProfile currentUserCustomConfigProfile;
            try
            {
                currentUserCustomConfigProfile = CurrentUserCustomConfigProfile.Load();

                this.CurrentUserCustomConfigProfileResult = currentUserCustomConfigProfile;
                
                if (currentUserCustomConfigProfile.Settings.PrintReport.Count == 0)
                {
                     CurrentUserCustomConfigProfile.SettingsCls.PrintSettings printer = new CurrentUserCustomConfigProfile.SettingsCls.PrintSettings();
                     printer.Name = new CurrentUserCustomConfigProfile.SettingsCls.PrintSettings.NameCls();
                     printer.Margin = new CurrentUserCustomConfigProfile.SettingsCls.PrintSettings.MarginCls();
                     printer.Paper = new CurrentUserCustomConfigProfile.SettingsCls.PrintSettings.PaperCls();
                     currentUserCustomConfigProfile.Settings.PrintReport.Add(printer);
                }

                this.XmlResult = currentUserCustomConfigProfile.Settings.PrintReport;
            }
            catch
            {
                LogUtility.Error(String.Format("{0}の形式が正しくありません。", "CurrentUserCustomConfigProfile.xml"));
            }
            finally
            {
                currentUserCustomConfigProfile = null;
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region プリンタ情報の初期値の設定
        /// <summary>
        /// プリンタ情報の初期値の設定
        /// </summary>
        public void SetDefaultPrinterInfo()
        {
            this.MyForm.OUTPUT_KBN_VALUE.Text = "1";
            this.MyForm.OUTPUT_KBN_HORIZONTAL.Checked = true;
            this.MyForm.COLOR_KBN_VALUE.Text = "1";
            this.MyForm.COLOR_KBN_MONOCHRO.Checked = true;
            
        }
        public void SetDefaultPrintermarInfo()
        {
            string reportName = string.Empty;
            if (this.MyForm.listBoxtReprotDispName.SelectedIndex > -1)
            {
                reportName = Convert.ToString(this.MyForm.listBoxtReprotDispName.SelectedItem);
            }

            SetDefaultMargin(reportName);
        }
        /// <summary>
        /// 指定した帳票によって余白の初期値を設定します
        /// </summary>
        /// <param name="reportName">帳票名</param>
        public void SetDefaultMargin(string reportName)
        {
            decimal top = 0;
            decimal bottom = 0;
            decimal left = 0;
            decimal right = 0;

            switch (reportName)
            {
                case REPORT_NAME_KEIRYOUHYOU:
                    // 計量票
                    // システム設定によってレイアウトを変更
                    CommonShogunData.Create(SystemProperty.Shain.CD);
                    M_SYS_INFO sysInfo = CommonShogunData.SYS_INFO;
                    if (sysInfo.KEIRYOU_LAYOUT_KBN.Value == 1)
                    {
                        // LAYOUT1
                        top = 0;
                        bottom = 0;
                        left = 0;
                        right = 0;
                    }
                    else
                    {
                        // LAYOUT2,3
                        top = 5;
                        bottom = 5;
                        left = 0;
                        right = 0;
                    }
                    break;
                case REPORT_NAME_SEIKYUSHO:
                    // 請求書
                    top = 5;
                    bottom = 5;
                    left = 5;
                    right = 5;
                    break;
                case REPORT_NAME_SHIHARAIMEISAISHO:
                    // 支払明細書
                    top = 5;
                    bottom = 5;
                    left = 5;
                    right = 5;
                    break;
                case REPORT_NAME_MITSUMORISHO:
                    // 見積書
                    top = 0;
                    bottom = 0;
                    left = 5;
                    right = 5;
                    break;
                case REPORT_NAME_HAISHASHIJISHO:
                    // 指示書
                    top = 5;
                    bottom = 5;
                    left = 5;
                    right = 5;
                    break;
                case REPORT_NAME_RYOUSHUSHO:
                    // 領収書
                    top = 0;
                    bottom = 0;
                    left = 0;
                    right = 0;
                    break;
                case REPORT_NAME_SHIKIRISHO:
                    // 仕切書
                    top = 0;
                    bottom = 0;
                    left = 0;
                    right = 0;
                    break;
                default:
                    // その他(マニは余白は保存によって変わるため0で設定)
                    break;
            }

            // 画面へセット
            this.MyForm.txtPrtSetBlankTop.Text = top.ToString();
            this.MyForm.txtPrtSetBlankBottom.Text = bottom.ToString();
            this.MyForm.txtPrtSetBlankLeft.Text = left.ToString();
            this.MyForm.txtPrtSetBlankRight.Text = right.ToString();
        }
        #endregion

        #region XMLデータより初期値のプリンタ情報の設定
        /// <summary>
        /// XMLデータより初期値のプリンタ情報の設定
        /// </summary>
        /// <param name="report">帳票名</param>
        /// <param name="printer">プリンタ</param>
        public void SetDefaultPrinterInfoFromXML(string report, string printer)
        {
            //検索結果を設定する
            List<CurrentUserCustomConfigProfile.SettingsCls.PrintSettings> printSettingsList = this.XmlResult;
            bool check = false;
            try
            {
                for (int i = 0; i < printSettingsList.Count; i++)
                {
                    if (printSettingsList[i].Name.Report != null)
                    {
                        if (printSettingsList[i].Name.Report.Equals(report)
                            && printSettingsList[i].Name.Printer.Equals(printer))
                        {
                            this.MyForm.listBoxtOutputPaper.SelectedItem = printSettingsList[i].Paper.Size;
                            this.MyForm.listBoxOutputDevice.SelectedItem = printSettingsList[i].Paper.Source;


                            if (printSettingsList[i].Paper.Landscape)
                            {
                                this.MyForm.OUTPUT_KBN_VALUE.Text = "1";
                                this.MyForm.OUTPUT_KBN_HORIZONTAL.Checked = true;
                            }
                            else
                            {
                                this.MyForm.OUTPUT_KBN_VALUE.Text = "2";
                                this.MyForm.OUTPUT_KBN_VERTICAL.Checked = true;
                            }

                            if (printSettingsList[i].Paper.Color)
                            {
                                this.MyForm.COLOR_KBN_VALUE.Text = "2";
                                this.MyForm.COLOR_KBN_COLOR.Checked = true;
                            }
                            else
                            {
                                this.MyForm.COLOR_KBN_VALUE.Text = "1";
                                this.MyForm.COLOR_KBN_MONOCHRO.Checked = true;
                            }

                            this.MyForm.txtPrtSetBlankTop.Text = printSettingsList[i].Margin.Top.ToString();
                            this.MyForm.txtPrtSetBlankBottom.Text = printSettingsList[i].Margin.Bottom.ToString();
                            this.MyForm.txtPrtSetBlankLeft.Text = printSettingsList[i].Margin.Left.ToString();
                            this.MyForm.txtPrtSetBlankRight.Text = printSettingsList[i].Margin.Right.ToString();

                            check = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                printSettingsList = null;
            }

            if (!check)
            {
                this.MyForm.listBoxtOutputPaper.SelectedIndex = -1;
                this.SetDefaultPrintermarInfo();
            }

        }

        #endregion

        //#region プリンタ情報選択
        //internal void SelectPrinterInfo()
        //{
        //}
        //#endregion

        #region データチェック

        /// <summary>
        /// F1 入力データチェック
        /// </summary>
        public bool CheckInputData()
        {
            var messageShowLogic = new MessageBoxShowLogic();

            if (this.MyForm.listBoxtOutputPrinter.SelectedIndex < 0)
            {
                messageShowLogic.MessageBoxShow("E051", "出力プリンタ");
                this.MyForm.listBoxtOutputPrinter.Focus();
                return false;
            }

            if (this.MyForm.listBoxtReprotDispName == null)
            {
                messageShowLogic.MessageBoxShow("E051", "出力プリンタ");
                this.MyForm.listBoxtReprotDispName.Focus();
                return false;
            }


            this.MyForm.listBoxtOutputPrinter.Focus();
            return true;
        }

        /// <summary>
        /// F9 保存データチェック
        /// </summary>
        public bool CheckSaveData()
        {
            var messageShowLogic = new MessageBoxShowLogic();

            if (this.MyForm.listBoxtOutputPrinter.SelectedIndex < 0)
            {
                messageShowLogic.MessageBoxShow("E051", "出力プリンタ");
                this.MyForm.listBoxtOutputPrinter.Focus();
                return false;
            }
            if (this.MyForm.listBoxtOutputPaper.SelectedIndex < 0)
            {
                messageShowLogic.MessageBoxShow("E051", " 出力用紙");
                this.MyForm.listBoxtOutputPaper.Focus();
                return false;
            }
            if (this.MyForm.listBoxOutputDevice.SelectedIndex < 0)
            {
                messageShowLogic.MessageBoxShow("E051", "出力デバイス");
                this.MyForm.listBoxOutputDevice.Focus();
                return false;
            }
            if (this.MyForm.listBoxtReprotDispName.SelectedIndex < 0)
            {
                messageShowLogic.MessageBoxShow("E046", "帳票名");
                this.MyForm.listBoxtReprotDispName.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(this.MyForm.OUTPUT_KBN_VALUE.PrevText))
            {
                messageShowLogic.MessageBoxShow("E001", "印刷方向");
                this.MyForm.OUTPUT_KBN_VALUE.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(this.MyForm.COLOR_KBN_VALUE.PrevText))
            {
                messageShowLogic.MessageBoxShow("E001", "カラー設定");
                this.MyForm.COLOR_KBN_VALUE.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(this.MyForm.txtPrtSetBlankTop.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "上余白");
                this.MyForm.txtPrtSetBlankTop.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(this.MyForm.txtPrtSetBlankLeft.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "左余白");
                this.MyForm.txtPrtSetBlankLeft.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(this.MyForm.txtPrtSetBlankRight.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "右余白");
                this.MyForm.txtPrtSetBlankRight.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(this.MyForm.txtPrtSetBlankBottom.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "下余白");
                this.MyForm.txtPrtSetBlankBottom.Focus();
                return false;
            }
            this.MyForm.listBoxtOutputPrinter.Focus();
            return true;
        }
        #endregion

        #region XML更新：出力項目
        /// <summary>
        /// XML更新：出力項目
        /// </summary>
        public void RegistXmlFilet(string report, string printer)
        {
            CurrentUserCustomConfigProfile currentUserCustomConfigProfile = this.CurrentUserCustomConfigProfileResult;
            List<CurrentUserCustomConfigProfile.SettingsCls.PrintSettings> printSettingsList = this.XmlResult;
            List<CurrentUserCustomConfigProfile.SettingsCls.PrintSettings> printSettingsAddList = new List<CurrentUserCustomConfigProfile.SettingsCls.PrintSettings>(); // = this.XmlResult;
            bool check = true;
            try
            {
                for (int i = 0; i < printSettingsList.Count; i++)
                {
                    if (printSettingsList[i].Name.Report != null)
                    {
                        if (printSettingsList[i].Name.Report.Equals(report))
                        {
                            currentUserCustomConfigProfile.Settings.PrintReport[i].Name.Printer = this.MyForm.listBoxtOutputPrinter.SelectedItem.ToString();
                            currentUserCustomConfigProfile.Settings.PrintReport[i].Paper.Size = this.MyForm.listBoxtOutputPaper.SelectedItem.ToString();
                            currentUserCustomConfigProfile.Settings.PrintReport[i].Paper.Source = this.MyForm.listBoxOutputDevice.SelectedItem.ToString();
                            if (this.MyForm.OUTPUT_KBN_VALUE.Text.ToString().Equals("1"))
                            {
                                currentUserCustomConfigProfile.Settings.PrintReport[i].Paper.Landscape = true;
                            }
                            else
                            {
                                currentUserCustomConfigProfile.Settings.PrintReport[i].Paper.Landscape = false;
                            }
                            if (this.MyForm.COLOR_KBN_VALUE.Text.Equals("1"))
                            {
                                currentUserCustomConfigProfile.Settings.PrintReport[i].Paper.Color = false;
                            }
                            else
                            {
                                currentUserCustomConfigProfile.Settings.PrintReport[i].Paper.Color = true;
                            }
                            currentUserCustomConfigProfile.Settings.PrintReport[i].Margin.Top = Convert.ToDecimal(this.MyForm.txtPrtSetBlankTop.Text);
                            currentUserCustomConfigProfile.Settings.PrintReport[i].Margin.Bottom = Convert.ToDecimal(this.MyForm.txtPrtSetBlankBottom.Text);
                            currentUserCustomConfigProfile.Settings.PrintReport[i].Margin.Left = Convert.ToDecimal(this.MyForm.txtPrtSetBlankLeft.Text);
                            currentUserCustomConfigProfile.Settings.PrintReport[i].Margin.Right = Convert.ToDecimal(this.MyForm.txtPrtSetBlankRight.Text);

                            printSettingsAddList = null;
                            currentUserCustomConfigProfile.Save();
                            check = false;
                        }  //break;
                    }
                }

                if (check)
                {
                    CurrentUserCustomConfigProfile.SettingsCls.PrintSettings printerReport = new CurrentUserCustomConfigProfile.SettingsCls.PrintSettings();
                    printerReport.Name = new CurrentUserCustomConfigProfile.SettingsCls.PrintSettings.NameCls();
                    printerReport.Margin = new CurrentUserCustomConfigProfile.SettingsCls.PrintSettings.MarginCls();
                    printerReport.Paper = new CurrentUserCustomConfigProfile.SettingsCls.PrintSettings.PaperCls();
                    //currentUserCustomConfigProfile.Settings.PrintReport.Add(printer);


                    //// ルートの要素を取得
                    printerReport.Name.Report = this.MyForm.listBoxtReprotDispName.SelectedItem.ToString();
                    printerReport.Name.Printer = this.MyForm.listBoxtOutputPrinter.SelectedItem.ToString();
                    printerReport.Paper.Size = this.MyForm.listBoxtOutputPaper.SelectedItem.ToString();
                    printerReport.Paper.Source = this.MyForm.listBoxOutputDevice.SelectedItem.ToString();
                    if (this.MyForm.OUTPUT_KBN_VALUE.Text.ToString().Equals("1"))
                    {
                        printerReport.Paper.Landscape = true;
                    }
                    else
                    {
                        printerReport.Paper.Landscape = false;
                    }
                    if (this.MyForm.COLOR_KBN_VALUE.Text.ToString().Equals("1"))
                    {
                        printerReport.Paper.Color = false;
                    }
                    else
                    {
                        printerReport.Paper.Color = true;
                    }
                    printerReport.Margin.Top = Convert.ToDecimal(this.MyForm.txtPrtSetBlankTop.Text);
                    printerReport.Margin.Bottom = Convert.ToDecimal(this.MyForm.txtPrtSetBlankBottom.Text);
                    printerReport.Margin.Left = Convert.ToDecimal(this.MyForm.txtPrtSetBlankLeft.Text);
                    printerReport.Margin.Right = Convert.ToDecimal(this.MyForm.txtPrtSetBlankRight.Text);

                    printSettingsList.Add(printerReport);
                    printSettingsAddList = null;

                }
                currentUserCustomConfigProfile.Save();

                var name = this.MyForm.listBoxtReprotDispName.SelectedItem.ToString();
                if (name.Contains("マニフェスト"))
                {
                    this.SaveManifestReportConfig(name);
                }
            }
            catch  (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                currentUserCustomConfigProfile = null;
                printSettingsList = null;
            }
        }
        #endregion

        #region 登録：出力項目
        /// <summary>
        /// 登録：出力項目
        /// </summary>
        public void Regist_OutPut()
        {
            LogUtility.DebugMethodStart();

            //データが存在する場合、更新処理を行う。
            this.UpdateOutPut();

            //登録処理を行う。
            this.GetDefaultMarginSettingsData(CONDITION_TYPE.INSERT_DMS);
            if (DmsList != null && DmsList.Count() > 0)
            {
                foreach (S_DEFAULTMARGINSETTINGS dms in DmsList)
                {
                    int CntMopIns = daoSetDefaultMarginSetting.Insert(dms);
                }
            }
            
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void UpdateOutPut()
        {
            LogUtility.DebugMethodStart();

            this.GetDefaultMarginSettingsData(CONDITION_TYPE.UPDATE_DMS);
            if (DmsList != null && DmsList.Count() > 0)
            {
                foreach (S_DEFAULTMARGINSETTINGS dms in DmsList)
                {
                    int CntMopIns = this.daoUpdDefaultMarginSetting.Update(dms);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public int SelectDefaultMarginSettings()
        {
            LogUtility.DebugMethodStart();
            int count = 0;
            count = this.GetDefaultMarginSettingsData(CONDITION_TYPE.SELECT_DMS);

            LogUtility.DebugMethodEnd();

            return count;
        }

        /// <summary>
        /// 一覧明細情報を取得する
        /// </summary>
        public int GetDefaultMarginSettingsData(CONDITION_TYPE type)
        {
            LogUtility.DebugMethodStart();

            int count = 0;

            List<S_DEFAULTMARGINSETTINGS> DmsListTemp = new List<S_DEFAULTMARGINSETTINGS>();
            S_DEFAULTMARGINSETTINGS dms;

            _type = (CONDITION_TYPE)type;
            switch (_type)
            {
                case CONDITION_TYPE.INSERT_DMS:
                    //出力項目の取得
                    var allDefaultMarginSetting = this.daoGetDefaultMarginSetting.GetAllDefaultMarginSetting();
                    count = allDefaultMarginSetting.Count();

                    //一覧出力項目
                    dms = new S_DEFAULTMARGINSETTINGS();
                    //発番
                    if (count > 0)
                    {
                        dms.ID = count + 1;
                    }
                    else
                    {
                        dms.ID = 1;
                    }
                    dms.PRINTERMAKERNAME = this.MyForm.listBoxtOutputPrinter.SelectedItem.ToString();
                    dms.PRINTERNAME = this.MyForm.listBoxtOutputPrinter.SelectedItem.ToString();
                    dms.REPORTDISPNAME = this.MyForm.listBoxtReprotDispName.SelectedItem.ToString();
                    dms.MARGIN_TOP = Convert.ToDecimal(this.MyForm.txtPrtSetBlankTop.Text);
                    dms.MARGIN_BOTTOM = Convert.ToDecimal(this.MyForm.txtPrtSetBlankBottom.Text);
                    dms.MARGIN_LEFT = Convert.ToDecimal(this.MyForm.txtPrtSetBlankLeft.Text);
                    dms.MARGIN_RIGHT = Convert.ToDecimal(this.MyForm.txtPrtSetBlankRight.Text);
                    dms.DELETE_FLG = false;

                    DataBinderLogic<S_DEFAULTMARGINSETTINGS> WHO_InsDMS = new DataBinderLogic<S_DEFAULTMARGINSETTINGS>(dms);
                    WHO_InsDMS.SetSystemProperty(dms, false);

                    //TODO:排他制御の修正
                    dms.TIME_STAMP = ConvertStrByte.In32ToByteArray(Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")));

                    DmsListTemp.Add(dms);
                    this.DmsList = DmsListTemp;
                    break;

                case CONDITION_TYPE.UPDATE_DMS:
                    var defaultMarginSetting = this.daoGetDefaultMarginSetting.GetDefaultMarginSetting(
                      this.MyForm.listBoxtReprotDispName.SelectedItem.ToString(),
                      this.MyForm.listBoxtOutputPrinter.SelectedItem.ToString(),
                      false
                      );
                    count = defaultMarginSetting.Count();

                    //発番
                    if (count > 0)
                    {
                        //更新
                        //一覧出力項目
                        dms = new S_DEFAULTMARGINSETTINGS();

                        //検索結果設定
                        List<S_DEFAULTMARGINSETTINGS> defaultMarginSettingList = defaultMarginSetting.ToList();

                        dms.ID = defaultMarginSettingList[0].ID;
                        dms.PRINTERNAME = defaultMarginSettingList[0].PRINTERNAME;
                        dms.REPORTDISPNAME = defaultMarginSettingList[0].REPORTDISPNAME;
                        dms.DELETE_FLG = true;

                        DataBinderLogic<S_DEFAULTMARGINSETTINGS> WHO_UpdDMS = new DataBinderLogic<S_DEFAULTMARGINSETTINGS>(dms);
                        WHO_UpdDMS.SetSystemProperty(dms, false);

                        //TODO:排他制御の修正
                        dms.TIME_STAMP = ConvertStrByte.In32ToByteArray(Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")));

                        DmsListTemp.Add(dms);
                        this.DmsList = DmsListTemp;
                    }
                    break;

                case CONDITION_TYPE.SELECT_DMS:
                    var defaultMarginSettingData = this.daoGetDefaultMarginSetting.GetDefaultMarginSetting(
                      this.MyForm.listBoxtReprotDispName.SelectedItem.ToString(),
                      this.MyForm.listBoxtOutputPrinter.SelectedItem.ToString(),
                      false
                      );
                    count = defaultMarginSettingData.Count();
                    //発番
                    if (count > 0)
                    {
                        //一覧出力項目
                        //検索結果設定
                        List<S_DEFAULTMARGINSETTINGS> defaultMarginSettingListData = defaultMarginSettingData.ToList();

                        this.MyForm.txtPrtSetBlankTop.Text = defaultMarginSettingListData[0].MARGIN_TOP.ToString();
                        this.MyForm.txtPrtSetBlankBottom.Text = defaultMarginSettingListData[0].MARGIN_BOTTOM.ToString();
                        this.MyForm.txtPrtSetBlankLeft.Text = defaultMarginSettingListData[0].MARGIN_LEFT.ToString();
                        this.MyForm.txtPrtSetBlankRight.Text = defaultMarginSettingListData[0].MARGIN_RIGHT.ToString();
                    }

                    break;
            }

            LogUtility.DebugMethodEnd();
            return count;
        }

        #endregion

        #region 通常使うプリンタ設定
        /// <summary>
        /// 通常使うプリンタ設定
        /// </summary>
        public void GenaralPrinter()
        {
            System.Drawing.Printing.PrinterSettings ps = new System.Drawing.Printing.PrinterSettings();

            string defaultPrinterName = ps.PrinterName;

            this.MyForm.ptGenaralPrinter.Text = (ps.PrinterName);
        }
        #endregion

        #region 使用可能なプリンタをリスト表示
        /// <summary>
        /// 使用可能なプリンタをリスト表示
        /// </summary>
        public void SetPrinterInfo()
        {

            LogUtility.DebugMethodStart();

            //前の結果をクリアS
            int k = this.MyForm.listBoxtReprotDispName.Items.Count;
            if (k > 0)
            {
                this.MyForm.listBoxtReprotDispName.Items.Clear();
            }

            //使用可能なプリンタをリスト表示
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                this.MyForm.listBoxtOutputPrinter.Items.Add(printer);
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 出力用紙
        /// <summary>
        /// 出力用紙
        /// </summary>
        public void SetPrintersizeInfo(PrintDocument printDocument)
        {
            LogUtility.DebugMethodStart();

            //前の結果をクリア
            int k = this.MyForm.listBoxtOutputPaper.Items.Count;
            if (k > 0)
            {
                this.MyForm.listBoxtOutputPaper.Items.Clear();
            }

            //PrinterSettings.PaperSizeCollection psizeCollection = this.MyForm.PrtPaperSizes.PrinterSettings.PaperSizes;
            PrinterSettings.PaperSizeCollection psizeCollection = printDocument.PrinterSettings.PaperSizes;

            for (int i = 0; i < psizeCollection.Count; i++)
                this.MyForm.listBoxtOutputPaper.Items.Add(psizeCollection[i].PaperName);

            this.MyForm.listBoxtOutputPaper.SelectedIndex = -1;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 出力デバイス
        /// <summary>
        /// Device情報
        /// </summary>
        /// <param name="printDocument"></param>
        public void SetPrinterDeviceInfo(PrintDocument printDocument)
        {
            LogUtility.DebugMethodStart();

            //前の結果をクリア
            int k = this.MyForm.listBoxOutputDevice.Items.Count;
            if (k > 0)
            {
                this.MyForm.listBoxOutputDevice.Items.Clear();
            }

            PrinterSettings.PaperSourceCollection sorceCollection = printDocument.PrinterSettings.PaperSources;
           
            for (int i = 0; i < sorceCollection.Count; i++)
                this.MyForm.listBoxOutputDevice.Items.Add(sorceCollection[i].SourceName);

            this.MyForm.listBoxOutputDevice.SelectedIndex = -1;

            LogUtility.DebugMethodEnd();
            
        }

        #endregion

        #region 余白既定値の呼出
        /// <summary>
        /// 余白既定値の呼出
        /// </summary>
        /// <param name="printDocument"></param>
        public void SetPrinterSpace(PrintDocument printDocument)
        {
            //PrintControllerプロパティをStandardPrintControllerに
            printDocument.PrintController = new System.Drawing.Printing.StandardPrintController();
            if (printDocument.DefaultPageSettings.Landscape)
            {
                this.MyForm.OUTPUT_KBN_VALUE.PrevText = "2";
            }
            else
            {
                this.MyForm.OUTPUT_KBN_VALUE.PrevText = "1";
            }

            if (printDocument.DefaultPageSettings.Color)
            {
                this.MyForm.COLOR_KBN_VALUE.PrevText = "2";
            }
            else
            {
                this.MyForm.COLOR_KBN_VALUE.PrevText = "1";
            }

            this.MyForm.txtPrtSetBlankTop.Text = "0";
            this.MyForm.txtPrtSetBlankLeft.Text = "0";
            this.MyForm.txtPrtSetBlankRight.Text = "0";
            this.MyForm.txtPrtSetBlankBottom.Text = "0";
        }

        #endregion

        #region テスト印刷
        public void TestPrint()
        {
            LogUtility.DebugMethodStart();
            if (this.MyForm.listBoxtReprotDispName.SelectedIndex < 0)
            {
                return;
            }

            // 入力チェック(保存チェックを使用)
            if (!this.CheckSaveData())
            {
                return;
            }
            // プリンタ名
            string printerName = this.MyForm.listBoxtOutputPrinter.SelectedItem.ToString();
            // 出力用紙
            string size = this.MyForm.listBoxtOutputPaper.SelectedItem.ToString();
            // 出力デバイス
            string sourceName = this.MyForm.listBoxOutputDevice.SelectedItem.ToString();


            // レポート名を使用して帳票フルパスとレイアウト名を取得
            string reportName = Convert.ToString(this.MyForm.listBoxtReprotDispName.SelectedItem);
            SystemProperty.PrintSettings.ReportInfo reportInfo = SearchReportInfo(reportName);
            
            // テーブルに設定が無い場合はアラート
            if (reportInfo == null)
            {
                LogUtility.Warn("PreviewFileDataNotFound");
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E024", "テスト印刷");
                return;
            }
            
            // 帳票フルパス
            string fullPath = reportInfo.ReportButsuriName;
            // レイアウト名
            string layout = reportInfo.ReportLayoutName;

            // 印刷設定作成
            PrinterSettings printerSettings = this.createPrintSetting(printerName, size, sourceName);
            if (printerSettings == null)
            {
                // 設定が作成出来ない場合中断
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E057", "プリンタのドライバ情報が正しく設定", "印刷");
                return;
            }
            
            // 帳票サンプルデータ生成
            DataTable sampleData = new DataTable();
            ReportInfoBase reportInfoBase = new ReportInfoBase(sampleData);

            // 空DataTableで何も表示されなくなるものだけデータを作成
            switch (reportName)
            {
                case REPORT_NAME_HAISHASHIJISHO:
                    // 配車指示書
                    reportInfoBase = this.CreateSampleData_Haishashijisho(fullPath, layout);
                    break;
                case REPORT_NAME_SHIKIRISHO:
                    // 仕切書
                    //reportInfoBase = this.CreateSampleData_Shikirisho(fullPath, layout);
                    break;
                case REPORT_NAME_RYOUSHUSHO:
                    // 領収書
                    reportInfoBase = this.CreateSampleData_Ryoushusho(fullPath, layout);
                    break;
                default:
                    // その他
                    reportInfoBase.Create(fullPath, layout, sampleData);
                    break;
            }

            // 印刷設定反映
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfoBase);
            reportPopup.PrinterSettingInfo = printerSettings;
            reportPopup.IsManifestReport = reportName.Contains("マニフェスト");

            // 印刷実行
            reportPopup.Print();
            reportPopup.Dispose();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ReportInfoクラスを帳票表示名で検索
        /// </summary>
        /// <param name="reportName">帳票表示名</param>
        /// <returns>見つからない場合はnull</returns>
        private SystemProperty.PrintSettings.ReportInfo SearchReportInfo(string reportName)
        {
            LogUtility.DebugMethodStart(reportName);
            SystemProperty.PrintSettings.ReportInfo val = null;
            for (int i = 0; i < this.ReportSettings.Length; i++)
            {
                SystemProperty.PrintSettings.ReportInfo reportInfo = this.ReportSettings[i];
                if (reportInfo.DispName.Equals(reportName) && !string.IsNullOrEmpty(reportInfo.ReportButsuriName) && !string.IsNullOrEmpty(reportInfo.ReportLayoutName))
                {
                    val = reportInfo;
                    break;
                }
            }

            if (val == null)
            {
                LogUtility.Warn(String.Format("帳票名：{0} の設定データがありません", reportName));
            }

            LogUtility.DebugMethodEnd(val);
            return val;
        }

        #region 印刷設定データ作成
        /// <summary>
        /// 画面情報から印刷設定を作成
        /// </summary>
        /// <param name="printerName">プリンター名</param>
        /// <param name="size">用紙サイズ</param>
        /// <param name="sourceName">出力デバイス</param>
        /// <returns>指定プリンタのドライバから指定用紙サイズが見つからない場合Null</returns>
        private PrinterSettings createPrintSetting(string printerName, string size, string sourceName)
        {
            LogUtility.DebugMethodStart(printerName, size, sourceName);
            // 設定情報
            PrinterSettings printerSettingInfo = new PrinterSettings();

            /* 印刷設定で指定されているプリンタドライバのpaperSizeを取得する */
            // 印刷設定
            var printDocument = new PrintDocument();
            // プリンタ名
            printDocument.PrinterSettings.PrinterName = printerName;
            
            var paperSize = printDocument.PrinterSettings.PaperSizes.Cast<PaperSize>().FirstOrDefault(n => n.PaperName.StartsWith(size));
            if (paperSize != null)
            {
                printerSettingInfo = new PrinterSettings();
                // プリンタ名
                printerSettingInfo.PrinterName = printerName;
                // 出力用紙サイズ
                printerSettingInfo.DefaultPageSettings.PaperSize = paperSize;
                // 出力デバイス
                var paperSource = new PaperSource();
                paperSource.SourceName = sourceName;
                printerSettingInfo.DefaultPageSettings.PaperSource = paperSource;
                // 出力用紙向き
                if (this.MyForm.OUTPUT_KBN_VALUE.Text.ToString().Equals("1"))
                {
                    printerSettingInfo.DefaultPageSettings.Landscape = true;
                }
                else
                {
                    printerSettingInfo.DefaultPageSettings.Landscape = false;
                }
                // 出力色設定
                if (this.MyForm.COLOR_KBN_VALUE.Text.Equals("1"))
                {
                    printerSettingInfo.DefaultPageSettings.Color = false;
                }
                else
                {
                    printerSettingInfo.DefaultPageSettings.Color = true;
                }
                // マージン
                var margins = new Margins();
                margins.Top = int.Parse(this.MyForm.txtPrtSetBlankTop.Text, System.Globalization.NumberStyles.AllowThousands);
                margins.Bottom = int.Parse(this.MyForm.txtPrtSetBlankBottom.Text, System.Globalization.NumberStyles.AllowThousands);
                margins.Left = int.Parse(this.MyForm.txtPrtSetBlankLeft.Text, System.Globalization.NumberStyles.AllowThousands);
                margins.Right = int.Parse(this.MyForm.txtPrtSetBlankRight.Text, System.Globalization.NumberStyles.AllowThousands);
                printerSettingInfo.DefaultPageSettings.Margins = margins;
            }
            else
            {
                LogUtility.Warn(string.Format("用紙サイズ\"{0}\"がプリンタドライバから見つかりませんでした。",size));
                printerSettingInfo = null;
            }

            LogUtility.DebugMethodEnd(printerSettingInfo);
            return printerSettingInfo;
        }
        #endregion 印刷設定データ作成

        #region 帳票サンプルデータ

        #region 配車指示書
        /// <summary>
        /// 配車指示書サンプルデータ作成
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="layoutName"></param>
        /// <returns></returns>
        private ReportInfoBase CreateSampleData_Haishashijisho(string fullPath, string layoutName)
        {
            LogUtility.DebugMethodStart(fullPath, layoutName);
            ReportInfoR345_R350 reportInfo = new ReportInfoR345_R350(WINDOW_ID.R_SAGYOU_SIJISYO, null);
            reportInfo.CreateSampleData();
            reportInfo.Create(fullPath, layoutName, new DataTable());
            LogUtility.DebugMethodEnd(reportInfo);
            return reportInfo;
        }
        #endregion 配車指示書

        //#region 仕切書
        ///// <summary>
        ///// 仕切書サンプルデータ作成
        ///// </summary>
        ///// <param name="fullPath"></param>
        ///// <param name="layoutName"></param>
        ///// <returns></returns>
        //private ReportInfoBase CreateSampleData_Shikirisho(string fullPath, string layoutName)
        //{
        //    LogUtility.DebugMethodStart(fullPath, layoutName);
        //    ReportInfoR338 reportInfo = new ReportInfoR338(WINDOW_ID.R_SHIKIRISYO);
        //    reportInfo.CreateSampleData();
        //    reportInfo.Create(fullPath, layoutName, new DataTable());
        //    LogUtility.DebugMethodEnd(reportInfo);
        //    return reportInfo;
        //}
        //#endregion 仕切書

        #region 領収書
        /// <summary>
        /// 領収書サンプルデータ作成
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="layoutName"></param>
        /// <returns></returns>
        private ReportInfoBase CreateSampleData_Ryoushusho(string fullPath, string layoutName)
        {
            LogUtility.DebugMethodStart(fullPath, layoutName);
            DataTable reportTable = new DataTable();
            // Colum定義
            reportTable.Columns.Add("GYOUSHA_CD");
            reportTable.Columns["GYOUSHA_CD"].ReadOnly = false;
            reportTable.Columns.Add("GYOUSHA_NAME1");
            reportTable.Columns["GYOUSHA_NAME1"].ReadOnly = false;
            reportTable.Columns.Add("KEISHOU1");
            reportTable.Columns["KEISHOU1"].ReadOnly = false;
            reportTable.Columns.Add("GYOUSHA_NAME2");
            reportTable.Columns["GYOUSHA_NAME2"].ReadOnly = false;
            reportTable.Columns.Add("KEISHOU2");
            reportTable.Columns["KEISHOU2"].ReadOnly = false;
            reportTable.Columns.Add("DENPYOU_DATE");
            reportTable.Columns["DENPYOU_DATE"].ReadOnly = false;
            reportTable.Columns.Add("RECEIPT_NUMBER");
            reportTable.Columns["RECEIPT_NUMBER"].ReadOnly = false;
            reportTable.Columns.Add("KINGAKU_TOTAL");
            reportTable.Columns["KINGAKU_TOTAL"].ReadOnly = false;
            reportTable.Columns.Add("TADASHIGAKI");
            reportTable.Columns["TADASHIGAKI"].ReadOnly = false;
            reportTable.Columns.Add("CORP_RYAKU_NAME");
            reportTable.Columns["CORP_RYAKU_NAME"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_NAME");
            reportTable.Columns["KYOTEN_NAME"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_POST");
            reportTable.Columns["KYOTEN_POST"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_ADDRESS1");
            reportTable.Columns["KYOTEN_ADDRESS1"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_TEL");
            reportTable.Columns["KYOTEN_TEL"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_FAX");
            reportTable.Columns["KYOTEN_FAX"].ReadOnly = false;

            // データセット
            DataRow row = reportTable.NewRow();
            row["GYOUSHA_CD"] = "123456";
            row["GYOUSHA_NAME1"] = "あいうえおあいうえおあいうえおあいうえお";
            row["GYOUSHA_NAME2"] = "あいうえおあいうえおあいうえおあいうえお";
            row["KEISHOU1"] = "あい";
            row["KEISHOU2"] = "あい";
            row["DENPYOU_DATE"] = "9999/12/31";
            row["RECEIPT_NUMBER"] = "123456";
            row["KINGAKU_TOTAL"] = "12345678901234";
            row["TADASHIGAKI"] = "あいうえおあいうえおあいうえお";
            row["CORP_RYAKU_NAME"] = "あいうえおあいうえおあいうえおあいうえお";
            row["KYOTEN_NAME"] = "あいうえおあいうえお";
            row["KYOTEN_POST"] = "012-3456";
            row["KYOTEN_ADDRESS1"] = "あいうえおあいうえおあいうえおあいうえお";
            row["KYOTEN_TEL"] = "0123-456-7890";
            row["KYOTEN_FAX"] = "0123-456-7890";
            reportTable.Rows.Add(row);

            ReportInfoBase reportInfo = new ReportInfoBase(reportTable);
            reportInfo.Create(fullPath, layoutName, reportTable);
            LogUtility.DebugMethodEnd(reportInfo);
            return reportInfo;
        }
        #endregion 領収書

        #endregion 帳票サンプルデータ

        #endregion テスト印刷

        #region マニ印字の暫定対応

        /// <summary>
        /// マニ帳票のテンプレートファイルの余白を直接書き換えます
        /// </summary>
        /// <param name="reportName"></param>
        private void SaveManifestReportConfig(string reportName)
        {
            LogUtility.DebugMethodStart(reportName);

            var templateInfo = this.SearchReportInfo(reportName);
            if ( templateInfo== null)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            if (File.Exists(templateInfo.ReportButsuriName))
            {
                var element = XElement.Load(templateInfo.ReportButsuriName);
                var layout = element.Descendants("Layout").Where(n => n.NodeType == XmlNodeType.Element);
                decimal margin = 0;
                decimal.TryParse(this.MyForm.txtPrtSetBlankLeft.Text, out margin);
                layout.Descendants("MarginLeft").First().Value = (288 + this.MillimeterToTwips(margin)).ToString();
                margin = 0;
                decimal.TryParse(this.MyForm.txtPrtSetBlankTop.Text, out margin);
                layout.Descendants("MarginTop").First().Value = (288 + this.MillimeterToTwips(margin)).ToString();
                margin = 0;
                decimal.TryParse(this.MyForm.txtPrtSetBlankRight.Text, out margin);
                layout.Descendants("MarginRight").First().Value = (288 + this.MillimeterToTwips(margin)).ToString();
                margin = 0;
                decimal.TryParse(this.MyForm.txtPrtSetBlankBottom.Text, out margin);
                layout.Descendants("MarginBottom").First().Value = (288 + this.MillimeterToTwips(margin)).ToString();
                element.Save(templateInfo.ReportButsuriName);
            }
            else
            {
                throw new FileNotFoundException(String.Format("テンプレートファイル：{0}　が見つかりません", templateInfo.ReportButsuriName));
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// mm→twips単位変換
        /// </summary>
        /// <param name="millimeter">mmの値</param>
        /// <returns>twipsの値</returns>
        private decimal MillimeterToTwips(decimal millimeter)
        {
            return Math.Round((decimal)(millimeter * 56.6929m), MidpointRounding.AwayFromZero);
        }

        #endregion
    }
}