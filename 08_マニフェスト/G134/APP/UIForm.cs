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
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.CustomControl;
using r_framework.FormManager;
//using CommonChouhyouPopup.App;

namespace Shougun.Core.PaperManifest.JissekiHokoku
{
    /// <summary>
    ///G134_実績報告書（処分実績）
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;
        /// <summary>
        /// メッセージクラス
        /// </summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }    
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BasePopForm ParentBaseForm { get; private set; }

        public string fromKbn { get; set; }

        public int SYSTEM_ID { get; set; }

        internal bool isSearchErr { get; set; }

        private string previousHoukokuGyoushaCd { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// UIForm（「交付等状況報告書条件指定ポップアップ」）
        /// </summary>
        public UIForm(): base(WINDOW_ID.T_JISSEKIHOKOKU, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            messageShowLogic = new MessageBoxShowLogic();
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }
        #endregion 

        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                //初期化
                this.logic.WindowInit();
                this.ParentBaseForm = (BasePopForm)this.Parent; 
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
           
        }
        #endregion

        #region  実行処理(F9)

        /// <summary>
        /// 実行処理(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                if (this.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.logic.SetErrorFocus();

                    LogUtility.DebugMethodEnd(true);
                    return;
                }
                if (!base.RegistErrorFlag)
                {
                    this.logic.Jikou();
                }              
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #region UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        private Control[] GetAllControl()
        {
            try
            {
                LogUtility.DebugMethodStart();
                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.allControl); 
                return allControl.ToArray();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        #endregion

        #region  処分実績(F3)
        /// <summary>
        /// 処理施設(F3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                FormManager.OpenFormWithAuth("G603", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.NONE);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region  運搬実績(F4)
        /// <summary>
        /// 運搬実績(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                FormManager.OpenFormWithAuth("G606", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.NONE);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        
        #region クローズ処理(F12)
        /// <summary>
        /// Formクローズ処理(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.Close();
                this.ParentBaseForm.Close();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }      
        #endregion

        #region CSV出力処理(F6)
        /// <summary>
        /// FormCSV出力処理(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.messageShowLogic.MessageBoxShow("I022", "「対象期間」「廃棄物区分」「自社業種区分」「処分事業場抽出条件」「排出事業場条件」「住所抽出条件」");
                this.logic.ShowCsvPopup();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// 報告事業者Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void HOUKOKU_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.HOUKOKU_GYOUSHA_NAME.Text = string.Empty;
            this.HOUKOKU_GENBA_CD.Text = string.Empty;
            this.HOUKOKU_GENBA_NAME.Text = string.Empty;

            if (string.IsNullOrEmpty(this.HOUKOKU_GYOUSHA_CD.Text))
            {
                return;
            }
            else
            {
                M_GYOUSHA[] results = this.logic.GetGyousha(this.HOUKOKU_GYOUSHA_CD.Text);
                if (results != null && results.Length > 0)
                {
                    this.HOUKOKU_GYOUSHA_NAME.Text = results[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 報告事業場Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void HOUKOKU_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.HOUKOKU_GENBA_NAME.Text = string.Empty;

            if (string.IsNullOrEmpty(this.HOUKOKU_GENBA_CD.Text))
            {
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(this.HOUKOKU_GYOUSHA_CD.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E051", "報告事業者");
                    this.HOUKOKU_GENBA_CD.Text = string.Empty;
                    e.Cancel = true;
                    return;
                }
                bool catchErr = false;
                M_GENBA[] results = this.logic.GetGenba(this.HOUKOKU_GYOUSHA_CD.Text, this.HOUKOKU_GENBA_CD.Text, out catchErr);
                if (catchErr) { return; }
                if (results != null && results.Length > 0)
                {
                    this.HOUKOKU_GENBA_NAME.Text = results[0].GENBA_NAME_RYAKU;
                }
                else
                {
                    M_GENBA[] results1 = this.logic.GetGenba1(this.HOUKOKU_GYOUSHA_CD.Text, this.HOUKOKU_GENBA_CD.Text, out catchErr);
                    if (catchErr) { return; }
                    if (results1 != null && results1.Length > 0)
                    {
                        this.HOUKOKU_GENBA_NAME.Text = results1[0].GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        this.messageShowLogic.MessageBoxShow("E020", "現場");
                        e.Cancel = true;
                    }
                }
            }
        }

        public void Before_GyoushaPop()
        {
            this.previousHoukokuGyoushaCd = this.HOUKOKU_GYOUSHA_CD.Text;
        }

        public void After_GyoushaPop()
        {
            if (this.previousHoukokuGyoushaCd != this.HOUKOKU_GYOUSHA_CD.Text)
            {
                this.HOUKOKU_GENBA_CD.Text = string.Empty;
                this.HOUKOKU_GENBA_NAME.Text = string.Empty;
            }
        }
        #endregion

        #region 対象期間を1年前に
        public void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                DateTime hiduke_from = DateTime.Parse(this.DATE_BEGIN.Text);
                DateTime hiduke_to = DateTime.Parse(this.DATE_END.Text);
                if (hiduke_from.ToString("yyyy") == "1753" || hiduke_to.ToString("yyyy") == "1753")
                {
                    return;
                }
                hiduke_from = hiduke_from.AddYears(-1);
                hiduke_to = hiduke_to.AddYears(-1);

                this.DATE_BEGIN.Text = hiduke_from.ToString();
                this.DATE_END.Text = hiduke_to.ToString();

                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP", false);
                ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
                this.HOUKOKU_TITLE1.SetResultText(Convert.ToDateTime(this.DATE_BEGIN.Value).ToString("gg", ci) + "○○年度の産業廃棄物の処理の実績について、廃棄物の処理及び清掃に関する");

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 対象期間を1年後に
        public void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                DateTime hiduke_from = DateTime.Parse(this.DATE_BEGIN.Text);
                DateTime hiduke_to = DateTime.Parse(this.DATE_END.Text);
                if (hiduke_from.ToString("yyyy") == "9999" || hiduke_to.ToString("yyyy") == "9999")
                {
                    return;
                }
                hiduke_from = hiduke_from.AddYears(1);
                hiduke_to = hiduke_to.AddYears(1);

                this.DATE_BEGIN.Text = hiduke_from.ToString();
                this.DATE_END.Text = hiduke_to.ToString();

                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP", false);
                ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
                this.HOUKOKU_TITLE1.SetResultText(Convert.ToDateTime(this.DATE_BEGIN.Value).ToString("gg", ci) + "○○年度の産業廃棄物の処理の実績について、廃棄物の処理及び清掃に関する");

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 値保持

        /// <summary>
        /// Enter時の値保持
        /// </summary>
        private Dictionary<Control, string> _EnterValue = new Dictionary<Control, string>();

        private object lastObject = null;

        internal void EnterEventInit()
        {
            foreach (var c in controlUtil.GetAllControls(this.Parent))
            {
                c.Enter += new EventHandler(this.SaveTextOnEnter);
            }
        }
        /// <summary>
        /// Enter時　入力値保存
        /// </summary>
        /// <param name="value"></param>
        private void SaveTextOnEnter(object sender, EventArgs e)
        {
            var value = sender as Control;

            if (value == null)
            {
                return;
            }

            //エラー等でフォーカス移動しなかった場合は、値クリアして強制チェックするようにする。 
            // ※1（正常）→0（エラー）→1と入れた場合 チェックする。
            // ※※この処理がない場合、0（エラー）→0（ノーチェック）となってしまう。
            if (lastObject == sender)
            {
                if (_EnterValue.ContainsKey(value))
                {
                    _EnterValue[value] = null;
                }
                else
                {
                    _EnterValue.Add(value, null);
                }

                return;

            }

            this.lastObject = sender;

            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = value.Text;
            }
            else
            {
                _EnterValue.Add(value, value.Text);
            }
        }
        /// <summary>
        /// 値比較時
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal string get_EnterValue(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return null;
            }
            return _EnterValue[value];
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, value.Text); //一致する場合変更なし
        }
        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender, string newText)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, newText); //一致する場合変更なし
        }

        #endregion

        /// <summary>
        /// 対象期間Toのダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_END_DoubleClick(object sender, EventArgs e)
        {
            this.DATE_END.Value = this.DATE_BEGIN.Value;
        }

        /// <summary>
        /// 自社業種区分のテキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGYOUSHA_KBN_TextChanged(object sender, EventArgs e)
        {
            var kbn = ((TextBox)sender).Text;
            if (kbn.ToString() == "2")
            {
                // 自社業種区分が２．最終処分の場合
                this.rdoJIGYOUJOU.Enabled = false;
                this.txtJIGYOUJOU_KBN.Text = "2";
                //this.txtJIGYOUJOU_KBN.CharacterLimitList = new char[] {'2'};
                this.txtJIGYOUJOU_KBN.RangeSetting.Max = 2;
                this.txtJIGYOUJOU_KBN.RangeSetting.Min = 2;
            }
            else
            {
                // 自社業種区分が２．最終処分以外の場合
                this.rdoJIGYOUJOU.Enabled = true;
                //this.txtJIGYOUJOU_KBN.CharacterLimitList = new char[] { '1', '2' };
                this.txtJIGYOUJOU_KBN.RangeSetting.Max = 2;
                this.txtJIGYOUJOU_KBN.RangeSetting.Min = 1;
            }
        }

        private void DATE_BEGIN_Validated(object sender, EventArgs e)
        {
            DateTime hiduke_from = DateTime.Parse(this.DATE_BEGIN.Text);
            DateTime hiduke_to = DateTime.Parse(this.DATE_END.Text);

            if (hiduke_from.ToString("yyyy") == "9999" || hiduke_to.ToString("yyyy") == "9999")
            {
                return;
            }

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP", false);
            ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
            this.HOUKOKU_TITLE1.SetResultText(Convert.ToDateTime(this.DATE_BEGIN.Value).ToString("gg", ci) + "○○年度の産業廃棄物の処理の実績について、廃棄物の処理及び清掃に関する");

        }
    }
}
