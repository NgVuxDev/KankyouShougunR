using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Collections.Generic;
using Shougun.Core.Common.BusinessCommon.Logic;
using r_framework.CustomControl;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.PayByProxy.DainoMeisaihyoOutput
{
    /// <summary>
    /// 代納明細表 出力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>画面ロジック</summary>
        private LogicClass logic = null;

        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_DAINO_MEISAIHYOU_OUTPUT, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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
            this.logic.WindowInit();
        }

        /// <summary>
        /// 画面表示時の制御
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

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

            //base.OnLoad(e);
            if (!this.logic.WindowInit())
            {
                return;
            }

            //テキストボックスの削除
            ControlLoopDel(this.Controls);

            LogUtility.DebugMethodEnd();
        }

        #region ファンクションボタン
        /// <summary>
        /// (F5)表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func05_Click(object sender, EventArgs e)
        {
            this.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.DATE_TO.BackColor = Constans.NOMAL_COLOR;

            if (!string.IsNullOrEmpty(this.DATE_FROM.Text) && !string.IsNullOrEmpty(this.DATE_TO.Text))
            {
                DateTime date_from = DateTime.Parse(this.DATE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.DATE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.DATE_FROM.IsInputErrorOccured = true;
                    this.DATE_TO.IsInputErrorOccured = true;
                    this.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.DATE_TO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "日付From", "日付To" };
                    this.messageShowLogic.MessageBoxShow("E030", errorMsg);
                    this.DATE_FROM.Focus();
                    return;
                }
            }
            if (!this.SetTorihikisakiCdFromTo())
            {
                return;
            }

            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.logic.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();
            if (!this.RegistErrorFlag)
            {
                this.logic.CsvPrint();
            }
            else
            {
                this.UKEIRE_TORI_CD_FROM.CausesValidation = true;
                this.UKEIRE_TORI_CD_TO.CausesValidation = true;
                this.UKEIRE_GYOSHA_CD_FROM.CausesValidation = true;
                this.UKEIRE_GYOSHA_CD_TO.CausesValidation = true;
                this.UKEIRE_GENBA_CD_FROM.CausesValidation = true;
                this.UKEIRE_GENBA_CD_TO.CausesValidation = true;
                this.SHUKKA_TORI_CD_FROM.CausesValidation = true;
                this.SHUKKA_TORI_CD_TO.CausesValidation = true;
                this.SHUKKA_GYOSHA_CD_FROM.CausesValidation = true;
                this.SHUKKA_GYOSHA_CD_TO.CausesValidation = true;
                this.SHUKKA_GENBA_CD_FROM.CausesValidation = true;
                this.SHUKKA_GENBA_CD_TO.CausesValidation = true;
            }
        }

        /// <summary>
        /// (F7)表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func07_Click(object sender, EventArgs e)
        {
            this.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.DATE_TO.BackColor = Constans.NOMAL_COLOR;

            if (!string.IsNullOrEmpty(this.DATE_FROM.Text) && !string.IsNullOrEmpty(this.DATE_TO.Text))
            {
                DateTime date_from = DateTime.Parse(this.DATE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.DATE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.DATE_FROM.IsInputErrorOccured = true;
                    this.DATE_TO.IsInputErrorOccured = true;
                    this.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.DATE_TO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "日付From", "日付To" };
                    this.messageShowLogic.MessageBoxShow("E030", errorMsg);
                    this.DATE_FROM.Focus();
                    return;
                }
            }
            if (!this.SetTorihikisakiCdFromTo())
            {
                return;
            }

            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.logic.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();
            if (!this.RegistErrorFlag)
            {
                this.logic.Print();
            }
            else
            {
                this.UKEIRE_TORI_CD_FROM.CausesValidation = true;
                this.UKEIRE_TORI_CD_TO.CausesValidation = true;
                this.UKEIRE_GYOSHA_CD_FROM.CausesValidation = true;
                this.UKEIRE_GYOSHA_CD_TO.CausesValidation = true;
                this.UKEIRE_GENBA_CD_FROM.CausesValidation = true;
                this.UKEIRE_GENBA_CD_TO.CausesValidation = true;
                this.SHUKKA_TORI_CD_FROM.CausesValidation = true;
                this.SHUKKA_TORI_CD_TO.CausesValidation = true;
                this.SHUKKA_GYOSHA_CD_FROM.CausesValidation = true;
                this.SHUKKA_GYOSHA_CD_TO.CausesValidation = true;
                this.SHUKKA_GENBA_CD_FROM.CausesValidation = true;
                this.SHUKKA_GENBA_CD_TO.CausesValidation = true;
            }
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

        /// <summary>
        /// 取引先のFromとToに最小値と最大値をセットします
        /// </summary>
        private bool SetTorihikisakiCdFromTo()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                var mTorihikisakiArray = dao.GetAllValidData(new M_TORIHIKISAKI() { ISNOT_NEED_DELETE_FLG = true });

                IEnumerable<M_TORIHIKISAKI> mTorihikisakiList = new List<M_TORIHIKISAKI>();
                mTorihikisakiList = mTorihikisakiArray;
                if (mTorihikisakiList.Count() > 0)
                {
                    var minTorihikisaki = mTorihikisakiList.Where(g => g.TORIHIKISAKI_CD == mTorihikisakiList.Min(gy => gy.TORIHIKISAKI_CD)).FirstOrDefault();
                    var maxTorihikisaki = mTorihikisakiList.OrderByDescending(m => m.TORIHIKISAKI_CD).Where(g => g.TORIHIKISAKI_CD == mTorihikisakiList.Max(gy => gy.TORIHIKISAKI_CD)).FirstOrDefault();

                    if (String.IsNullOrEmpty(this.UKEIRE_TORI_CD_FROM.Text))
                    {
                        this.UKEIRE_TORI_CD_FROM.Text = minTorihikisaki.TORIHIKISAKI_CD;
                        this.UKEIRE_TORI_NAME_FROM.Text = minTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }

                    if (String.IsNullOrEmpty(this.UKEIRE_TORI_CD_TO.Text))
                    {
                        this.UKEIRE_TORI_CD_TO.Text = maxTorihikisaki.TORIHIKISAKI_CD;
                        this.UKEIRE_TORI_NAME_TO.Text = maxTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }

                    if (String.IsNullOrEmpty(this.SHUKKA_TORI_CD_FROM.Text))
                    {
                        this.SHUKKA_TORI_CD_FROM.Text = minTorihikisaki.TORIHIKISAKI_CD;
                        this.SHUKKA_TORI_NAME_FROM.Text = minTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }

                    if (String.IsNullOrEmpty(this.SHUKKA_TORI_CD_TO.Text))
                    {
                        this.SHUKKA_TORI_CD_TO.Text = maxTorihikisaki.TORIHIKISAKI_CD;
                        this.SHUKKA_TORI_NAME_TO.Text = maxTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetTorihikisakiCdFromTo", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikisakiCdFromTo", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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

        #region 受入

        /// <summary>
        /// 日付TO DoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DATE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.DATE_TO.Value = this.DATE_FROM.Value;
        }

        /// <summary>
        /// 取引先TO DoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_TORI_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            this.UKEIRE_TORI_CD_TO.Text = this.UKEIRE_TORI_CD_FROM.Text;
            this.UKEIRE_TORI_NAME_TO.Text = this.UKEIRE_TORI_NAME_FROM.Text;
        }

        /// <summary>
        /// 業者TO DoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_GYOSHA_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            this.UKEIRE_GYOSHA_CD_TO.Text = this.UKEIRE_GYOSHA_CD_FROM.Text;
            this.UKEIRE_GYOSHA_NAME_TO.Text = this.UKEIRE_GYOSHA_NAME_FROM.Text;
        }

        /// <summary>
        /// 現場TO DoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_GENBA_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            this.UKEIRE_GENBA_CD_TO.Text = this.UKEIRE_GENBA_CD_FROM.Text;
            this.UKEIRE_GENBA_NAME_TO.Text = this.UKEIRE_GENBA_NAME_FROM.Text;
        }

        /// <summary>
        /// 取引先TO DoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_TORI_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            this.SHUKKA_TORI_CD_TO.Text = this.SHUKKA_TORI_CD_FROM.Text;
            this.SHUKKA_TORI_NAME_TO.Text = this.SHUKKA_TORI_NAME_FROM.Text;
        }

        /// <summary>
        /// 業者TO DoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_GYOSHA_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            this.SHUKKA_GYOSHA_CD_TO.Text = this.SHUKKA_GYOSHA_CD_FROM.Text;
            this.SHUKKA_GYOSHA_NAME_TO.Text = this.SHUKKA_GYOSHA_NAME_FROM.Text;
        }

        /// <summary>
        /// 現場TO DoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_GENBA_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            this.SHUKKA_GENBA_CD_TO.Text = this.SHUKKA_GENBA_CD_FROM.Text;
            this.SHUKKA_GENBA_NAME_TO.Text = this.SHUKKA_GENBA_NAME_FROM.Text;
        }

        /// <summary>
        /// 取引先 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_TORI_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.UKEIRE_TORI_NAME_FROM.Text = string.Empty;
            if (string.IsNullOrEmpty(this.UKEIRE_TORI_CD_FROM.Text)) { return; }
            bool catchErr = true;
            M_TORIHIKISAKI result = this.logic.GetTorihikisaki(this.UKEIRE_TORI_CD_FROM.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (result != null)
            {
                this.UKEIRE_TORI_NAME_FROM.Text = result.TORIHIKISAKI_NAME_RYAKU;
            }
            else
            {
                this.messageShowLogic.MessageBoxShow("E020", "取引先");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 取引先 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_TORI_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.UKEIRE_TORI_NAME_TO.Text = string.Empty;
            if (string.IsNullOrEmpty(this.UKEIRE_TORI_CD_TO.Text)) { return; }
            bool catchErr = true;
            M_TORIHIKISAKI result = this.logic.GetTorihikisaki(this.UKEIRE_TORI_CD_TO.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (result != null)
            {
                this.UKEIRE_TORI_NAME_TO.Text = result.TORIHIKISAKI_NAME_RYAKU;
            }
            else
            {
                this.messageShowLogic.MessageBoxShow("E020", "取引先");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 受入業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_GYOSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!isChanged(sender))//変更がない場合は何もしない
                {
                    return;
                }
                this.UKEIRE_GYOSHA_NAME_FROM.Text = string.Empty;
                this.UKEIRE_GENBA_CD_FROM.Text = string.Empty;
                this.UKEIRE_GENBA_NAME_FROM.Text = string.Empty;
                if (string.IsNullOrEmpty(this.UKEIRE_GYOSHA_CD_FROM.Text)) { return; }
                bool catchErr = true;
                M_GYOUSHA result = this.logic.GetGyousha(this.UKEIRE_GYOSHA_CD_FROM.Text, 1, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (result != null)
                {
                    this.UKEIRE_GYOSHA_NAME_FROM.Text = result.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                }
            }
            finally
            {
                this.UKEIRE_GYOUSHA_Validated();
            }
        }

        /// <summary>
        /// 受入業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_GYOSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!isChanged(sender))//変更がない場合は何もしない
                {
                    return;
                }
                this.UKEIRE_GYOSHA_NAME_TO.Text = string.Empty;
                this.UKEIRE_GENBA_CD_TO.Text = string.Empty;
                this.UKEIRE_GENBA_NAME_TO.Text = string.Empty;
                if (string.IsNullOrEmpty(this.UKEIRE_GYOSHA_CD_TO.Text)) { return; }
                bool catchErr = true;
                M_GYOUSHA result = this.logic.GetGyousha(this.UKEIRE_GYOSHA_CD_TO.Text, 1, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (result != null)
                {
                    this.UKEIRE_GYOSHA_NAME_TO.Text = result.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                }
            }
            finally
            {
                this.UKEIRE_GYOUSHA_Validated();
            }
        }

        /// <summary>
        /// 受入業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_GYOUSHA_Validated()
        {
            if (!string.IsNullOrEmpty(this.UKEIRE_GYOSHA_CD_FROM.Text) && !string.IsNullOrEmpty(this.UKEIRE_GYOSHA_CD_TO.Text) && this.UKEIRE_GYOSHA_CD_FROM.Text == this.UKEIRE_GYOSHA_CD_TO.Text)
            {
                this.UKEIRE_GENBA_CD_FROM.Enabled = true;
                this.UKEIRE_GENBA_CD_TO.Enabled = true;
                this.btn_UKEIRE_GENBA_FROM.Enabled = true;
                this.btn_UKEIRE_GENBA_TO.Enabled = true;
            }
            else
            {
                this.UKEIRE_GENBA_CD_FROM.Text = "";
                this.UKEIRE_GENBA_NAME_FROM.Text = "";
                this.UKEIRE_GENBA_CD_TO.Text = "";
                this.UKEIRE_GENBA_NAME_TO.Text = "";
                this.UKEIRE_GENBA_CD_FROM.Enabled = false;
                this.UKEIRE_GENBA_CD_TO.Enabled = false;
                this.btn_UKEIRE_GENBA_FROM.Enabled = false;
                this.btn_UKEIRE_GENBA_TO.Enabled = false;
            }
        }

        /// <summary>
        /// 受入現場 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_GENBA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.UKEIRE_GENBA_NAME_FROM.Text = string.Empty;
            if (string.IsNullOrEmpty(this.UKEIRE_GENBA_CD_FROM.Text)) { return; }
            bool catchErr = true;
            DataTable result = this.logic.GetGenba(this.UKEIRE_GYOSHA_CD_FROM.Text, this.UKEIRE_GENBA_CD_FROM.Text, 1, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (result == null || result.Rows.Count == 0)
            {
                this.messageShowLogic.MessageBoxShow("E020", "現場");
                e.Cancel = true;
            }
            else if (result.Rows.Count == 1)
            {
                this.UKEIRE_GENBA_NAME_FROM.Text = Convert.ToString(result.Rows[0]["GENBA_NAME_RYAKU"]);
            }
            else
            {
                this.messageShowLogic.MessageBoxShow("E034", "受入業者");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 受入現場 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_GENBA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.UKEIRE_GENBA_NAME_TO.Text = string.Empty;
            if (string.IsNullOrEmpty(this.UKEIRE_GENBA_CD_TO.Text)) { return; }
            bool catchErr = true;
            DataTable result = this.logic.GetGenba(this.UKEIRE_GYOSHA_CD_TO.Text, this.UKEIRE_GENBA_CD_TO.Text, 1, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (result == null || result.Rows.Count == 0)
            {
                this.messageShowLogic.MessageBoxShow("E020", "現場");
                e.Cancel = true;
            }
            else if (result.Rows.Count == 1)
            {
                this.UKEIRE_GENBA_NAME_TO.Text = Convert.ToString(result.Rows[0]["GENBA_NAME_RYAKU"]);
            }
            else
            {
                this.messageShowLogic.MessageBoxShow("E034", "受入業者");
                e.Cancel = true;
            }
        }
        #endregion

        #region 出荷
        #endregion
        /// <summary>
        /// 取引先 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_TORI_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.SHUKKA_TORI_NAME_FROM.Text = string.Empty;
            if (string.IsNullOrEmpty(this.SHUKKA_TORI_CD_FROM.Text)) { return; }
            bool catchErr = true;
            M_TORIHIKISAKI result = this.logic.GetTorihikisaki(this.SHUKKA_TORI_CD_FROM.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (result != null)
            {
                this.SHUKKA_TORI_NAME_FROM.Text = result.TORIHIKISAKI_NAME_RYAKU;
            }
            else
            {
                this.messageShowLogic.MessageBoxShow("E020", "取引先");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 取引先 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_TORI_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.SHUKKA_TORI_NAME_TO.Text = string.Empty;
            if (string.IsNullOrEmpty(this.SHUKKA_TORI_CD_TO.Text)) { return; }
            bool catchErr = true;
            M_TORIHIKISAKI result = this.logic.GetTorihikisaki(this.SHUKKA_TORI_CD_TO.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (result != null)
            {
                this.SHUKKA_TORI_NAME_TO.Text = result.TORIHIKISAKI_NAME_RYAKU;
            }
            else
            {
                this.messageShowLogic.MessageBoxShow("E020", "取引先");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 出荷業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_GYOSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!isChanged(sender))//変更がない場合は何もしない
                {
                    return;
                }
                this.SHUKKA_GYOSHA_NAME_FROM.Text = string.Empty;
                this.SHUKKA_GENBA_CD_FROM.Text = string.Empty;
                this.SHUKKA_GENBA_NAME_FROM.Text = string.Empty;
                if (string.IsNullOrEmpty(this.SHUKKA_GYOSHA_CD_FROM.Text)) { return; }
                bool catchErr = true;
                M_GYOUSHA result = this.logic.GetGyousha(this.SHUKKA_GYOSHA_CD_FROM.Text, 2, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (result != null)
                {
                    this.SHUKKA_GYOSHA_NAME_FROM.Text = result.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                }
            }
            finally
            {
                this.SHUKKA_GYOUSHA_Validated();
            }
        }

        /// <summary>
        /// 出荷業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_GYOSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!isChanged(sender))//変更がない場合は何もしない
                {
                    return;
                }
                this.SHUKKA_GYOSHA_NAME_TO.Text = string.Empty;
                this.SHUKKA_GENBA_CD_TO.Text = string.Empty;
                this.SHUKKA_GENBA_NAME_TO.Text = string.Empty;
                if (string.IsNullOrEmpty(this.SHUKKA_GYOSHA_CD_TO.Text)) { return; }
                bool catchErr = true;
                M_GYOUSHA result = this.logic.GetGyousha(this.SHUKKA_GYOSHA_CD_TO.Text, 2, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (result != null)
                {
                    this.SHUKKA_GYOSHA_NAME_TO.Text = result.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                }
            }
            finally
            {
                this.SHUKKA_GYOUSHA_Validated();
            }
        }

        /// <summary>
        /// 出荷業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_GYOUSHA_Validated()
        {
            if (!string.IsNullOrEmpty(this.SHUKKA_GYOSHA_CD_FROM.Text) && !string.IsNullOrEmpty(this.SHUKKA_GYOSHA_CD_TO.Text) && this.SHUKKA_GYOSHA_CD_FROM.Text == this.SHUKKA_GYOSHA_CD_TO.Text)
            {
                this.SHUKKA_GENBA_CD_FROM.Enabled = true;
                this.SHUKKA_GENBA_CD_TO.Enabled = true;
                this.btn_SHUKKA_GENBA_FROM.Enabled = true;
                this.btn_SHUKKA_GENBA_TO.Enabled = true;
            }
            else
            {
                this.SHUKKA_GENBA_CD_FROM.Text = "";
                this.SHUKKA_GENBA_NAME_FROM.Text = "";
                this.SHUKKA_GENBA_CD_TO.Text = "";
                this.SHUKKA_GENBA_NAME_TO.Text = "";
                this.SHUKKA_GENBA_CD_FROM.Enabled = false;
                this.SHUKKA_GENBA_CD_TO.Enabled = false;
                this.btn_SHUKKA_GENBA_FROM.Enabled = false;
                this.btn_SHUKKA_GENBA_TO.Enabled = false;
            }
        }

        /// <summary>
        /// 出荷現場 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_GENBA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.SHUKKA_GENBA_NAME_FROM.Text = string.Empty;
            if (string.IsNullOrEmpty(this.SHUKKA_GENBA_CD_FROM.Text)) { return; }
            bool catchErr = true;
            DataTable result = this.logic.GetGenba(this.SHUKKA_GYOSHA_CD_FROM.Text, this.SHUKKA_GENBA_CD_FROM.Text, 2, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (result == null || result.Rows.Count == 0)
            {
                this.messageShowLogic.MessageBoxShow("E020", "現場");
                e.Cancel = true;
            }
            else if (result.Rows.Count == 1)
            {
                this.SHUKKA_GENBA_NAME_FROM.Text = Convert.ToString(result.Rows[0]["GENBA_NAME_RYAKU"]);
            }
            else
            {
                this.messageShowLogic.MessageBoxShow("E034", "出荷業者");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 出荷現場 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_GENBA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.SHUKKA_GENBA_NAME_TO.Text = string.Empty;
            if (string.IsNullOrEmpty(this.SHUKKA_GENBA_CD_TO.Text)) { return; }
            bool catchErr = true;
            DataTable result = this.logic.GetGenba(this.SHUKKA_GYOSHA_CD_TO.Text, this.SHUKKA_GENBA_CD_TO.Text, 2, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (result == null || result.Rows.Count == 0)
            {
                this.messageShowLogic.MessageBoxShow("E020", "現場");
                e.Cancel = true;
            }
            else if (result.Rows.Count == 1)
            {
                this.SHUKKA_GENBA_NAME_TO.Text = Convert.ToString(result.Rows[0]["GENBA_NAME_RYAKU"]);
            }
            else
            {
                this.messageShowLogic.MessageBoxShow("E034", "出荷業者");
                e.Cancel = true;
            }
        }
        #endregion

        /// <summary>
        /// 受入業者POPUP後処理
        /// </summary>
        public void UKEIRE_GYOSHA_CD_PopupAfterExecuteMethod()
        {
            this.UKEIRE_GYOUSHA_Validated();
        }

        /// <summary>
        /// 出荷業者POPUP後処理
        /// </summary>
        public void SHUKKA_GYOSHA_CD_PopupAfterExecuteMethod()
        {
            this.SHUKKA_GYOUSHA_Validated();
        }
    }
}
