using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpan
{
    /// <summary>
    /// 実績報告書（運搬実績）
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>画面ロジック</summary>
        private LogicClass logic = null;

        /// <summary> 親フォーム</summary>
        public BasePopForm ParentBaseForm { get; private set; }

        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }

        /// <summary>処理モード</summary>
        public int IMode = 0;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="window_type">画面区分</param>
        /// <param name="RDentatuKbn">連携伝達区分</param>
        /// <param name="SystemId">システムID</param>
        /// <param name="RMeisaiId">連携明細システムID</param>
        /// <param name="iMode">処理モード</param>
        public UIForm(WINDOW_TYPE window_type)
            : base(WINDOW_ID.T_JISSEKIHOKOKU_UNPAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            //メッセージクラス
            messageShowLogic = new MessageBoxShowLogic();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }
        #endregion コンストラクタ

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.logic.WindowInit()) { return; }
            this.logic.InitRadioButton();
        }

        /// <summary>
        /// 画面表示時の制御
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        /// <summary>
        /// 画面の全てのTEXTBOXの内容をクリアする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlLoopDel(Control.ControlCollection controls)
        {
            foreach (System.Windows.Forms.Control control in controls)
            {
                if (control is r_framework.CustomControl.CustomAlphaNumTextBox)
                {
                    r_framework.CustomControl.CustomAlphaNumTextBox pb = (r_framework.CustomControl.CustomAlphaNumTextBox)control;
                    pb.Clear();
                }
                else if (control is System.Windows.Forms.Panel)
                {
                    ControlLoopDel(((System.Windows.Forms.Panel)control).Controls);
                }
            }
        }

        /// <summary>
        /// 画面クリア処理
        /// </summary>
        /// <param name="e"></param>
        internal void ClearScreen(EventArgs e)
        {
            LogUtility.DebugMethodStart();

            base.OnLoad(e);

            //テキストボックスの削除
            ControlLoopDel(this.Controls);

            LogUtility.DebugMethodEnd();
        }

        #region ファンクションボタン
        /// <summary>
        /// (F3)処分実績イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func3_Click(object sender, EventArgs e)
        {
            this.logic.ShobunJiseki();
        }

        /// <summary>
        /// (F4)処理施設イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func4_Click(object sender, EventArgs e)
        {
            this.logic.ShobunShisetu();
        }

        /// <summary>
        /// (F6)ＣＳＶ出力イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func6_Click(object sender, EventArgs e)
        {
            messageShowLogic.MessageBoxShow("I022", "「対象期間」「廃棄物区分」「住所抽出条件」");

            //必須チェック
            if (this.logic.InputCheck())
            {
                return;
            }
            bool catchErr = false;
            if (this.logic.SearchCheck(out catchErr))
            {
                if (!catchErr)
                {
                    messageShowLogic.MessageBoxShow("E044");
                }
                return;
            }
            this.logic.CSVOutput();
        }

        /// <summary>
        /// (F9)実行イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func9_Click(object sender, EventArgs e)
        {
            //必須チェック
            if (this.logic.InputCheck())
            {
                return;
            }
            bool catchErr = false;
            if (this.logic.SearchCheck(out catchErr))
            {
                if (!catchErr)
                {
                    messageShowLogic.MessageBoxShow("C001");
                }
                return;
            }
            //if (this.logic.DataCheck())
            //{
            //    return;
            //}
            if (!this.logic.CreateEntry()) { return; }
            if (this.logic.CreateUnpan() == -1) { return; }
            //if (this.logic.CreateDetail() == -1) { return; }
            this.logic.Regist(base.RegistErrorFlag);
        }

        /// <summary>
        /// (F12)閉じるイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
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

        #region 画面コントロールイベント
        /// <summary>
        /// 報告事業者Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txt_GyousyaCd_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.txt_GyousyaName.Text = string.Empty;

            if (string.IsNullOrEmpty(this.txt_GyousyaCd.Text))
            {
                return;
            }
            else
            {
                bool catchErr = false;
                M_GYOUSHA[] results = this.logic.GetGyousha(this.txt_GyousyaCd.Text, out catchErr);
                if (catchErr) { return; }
                if (results != null && results.Length > 0)
                {
                    this.txt_GyousyaName.Text = results[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                }
            }
        }
        
        /// <summary>
        /// 提出先Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txt_TeishutuSakiCD_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.txt_TeishutuSakiName.Text = string.Empty;

            if (string.IsNullOrEmpty(this.txt_TeishutuSakiCD.Text))
            {
                return;
            }
            else
            {
                bool catchErr = false;
                M_CHIIKI[] results = this.logic.GetChiiki(this.txt_TeishutuSakiCD.Text, out catchErr);
                if (catchErr) { return; }
                if (results != null && results.Length > 0)
                {
                    this.txt_TeishutuSakiName.Text = results[0].CHIIKI_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "地域");
                    e.Cancel = true;
                }
            }
        }

        #region 対象期間を1年前に
        public void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                DateTime hiduke_from = DateTime.Parse(this.HIDUKE_FROM.Text);
                DateTime hiduke_to = DateTime.Parse(this.HIDUKE_TO.Text);
                if (hiduke_from.ToString("yyyy") == "1753" || hiduke_to.ToString("yyyy") == "1753")
                {
                    return;
                }
                hiduke_from = hiduke_from.AddYears(-1);
                hiduke_to = hiduke_to.AddYears(-1);

                this.HIDUKE_FROM.Text = hiduke_from.ToString();
                this.HIDUKE_TO.Text = hiduke_to.ToString();

                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP", false);
                ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
                this.txt_HokokushoTitle1.SetResultText(Convert.ToDateTime(this.HIDUKE_FROM.Value).ToString("gg", ci) + "○○年度の産業廃棄物（特管）の処理実績について、廃棄物の処理及び清掃に関する");

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
                DateTime hiduke_from = DateTime.Parse(this.HIDUKE_FROM.Text);
                DateTime hiduke_to = DateTime.Parse(this.HIDUKE_TO.Text);
                if (hiduke_from.ToString("yyyy") == "9999" || hiduke_to.ToString("yyyy") == "9999")
                {
                    return;
                }
                hiduke_from = hiduke_from.AddYears(1);
                hiduke_to = hiduke_to.AddYears(1);

                this.HIDUKE_FROM.Text = hiduke_from.ToString();
                this.HIDUKE_TO.Text = hiduke_to.ToString();

                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP", false);
                ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
                this.txt_HokokushoTitle1.SetResultText(Convert.ToDateTime(this.HIDUKE_FROM.Value).ToString("gg", ci) + "○○年度の産業廃棄物（特管）の処理実績について、廃棄物の処理及び清掃に関する");

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

        /// <summary>
        /// 対象期間Toのダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.HIDUKE_TO.Value = this.HIDUKE_FROM.Value;
        }
        #endregion

        private void HIDUKE_FROM_Validated(object sender, EventArgs e)
        {

            DateTime hiduke_from = DateTime.Parse(this.HIDUKE_FROM.Text);
            DateTime hiduke_to = DateTime.Parse(this.HIDUKE_TO.Text);

            if (hiduke_from.ToString("yyyy") == "9999" || hiduke_to.ToString("yyyy") == "9999")
            {
                return;
            }
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP", false);
            ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
            this.txt_HokokushoTitle1.SetResultText(Convert.ToDateTime(this.HIDUKE_FROM.Value).ToString("gg", ci) + "○○年度の産業廃棄物（特管）の処理実績について、廃棄物の処理及び清掃に関する");

        }
    }
}
