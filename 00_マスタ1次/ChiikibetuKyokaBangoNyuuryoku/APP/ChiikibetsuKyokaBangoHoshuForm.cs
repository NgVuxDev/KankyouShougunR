// $Id: ChiikibetsuKyokaBangoHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using ChiikibetsuKyokaBangoHoshu.Const;
using ChiikibetsuKyokaBangoHoshu.Logic;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace ChiikibetsuKyokaBangoHoshu.APP
{
    /// <summary>
    /// 地域別許可番号入力画面
    /// </summary>
    [Implementation]
    public partial class ChiikibetsuKyokaBangoHoshuForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 地域別許可番号入力画面ロジック
        /// </summary>
        private ChiikibetsuKyokaBangoHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();
        //CongBinh 20210714 #152813 S
        internal short KyokaKbn = 0;
        internal string GyoushaCd = string.Empty;
        internal string GenbaCd = string.Empty;
        //CongBinh 20210714 #152813 E

        /// <summary>
        /// Request inxs subapp transaction id 
        /// </summary>
        internal string transactionId;
        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChiikibetsuKyokaBangoHoshuForm()
            : base(WINDOW_ID.M_CHIIKIBETSU_KYOKA_UPN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ChiikibetsuKyokaBangoHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        //CongBinh 20210714 #152813 S
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kyokaKbn"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        public ChiikibetsuKyokaBangoHoshuForm(short kyokaKbn, string gyoushaCd, string genbaCd)
            : this()
        {
            this.KyokaKbn = kyokaKbn;
            this.GyoushaCd = gyoushaCd;
            this.GenbaCd = genbaCd;
        }
        //CongBinh 20210714 #152813 E
        #endregion

        #region 画面ロードイベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

			//Init inxs subapp transaction id
            this.transactionId = Guid.NewGuid().ToString();

            // 画面の初期化
            this.logic.WindowInit();

            this.SearchDataFromIchiran();//CongBinh 20210714 #152813

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
        }

        #endregion

        #region ファンクションボタンイベント

        /// <summary>
        /// 論理削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                /// 20141217 Houkakou 「地域別許可番号入力」の日付チェックを追加する　start
                if (this.logic.AllDateCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「地域別許可番号入力」の日付チェックを追加する　end
                // 検索実行前に削除を押下された場合の対応
                if (this.Ichiran.DataSource == null)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E064", "削除処理");
                    return;
                }

                bool catchErr = this.logic.CreateEntity(true);
                if (catchErr)
                {
                    return;
                }
                this.logic.LogicalDelete();
                if (catchErr)
                {
                    return;
                }
                this.Search(sender, e);
            }
        }

        /// <summary>
        /// CSV(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            /// 20141217 Houkakou 「地域別許可番号入力」の日付チェックを追加する　start
            if (this.logic.AllDateCheck())
            {
                return;
            }
            /// 20141217 Houkakou 「地域別許可番号入力」の日付チェックを追加する　end
            this.logic.CSV();
        }

        /// <summary>
        /// 条件取消(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            this.logic.CancelCondition();
        }

        /// <summary>
        /// 検索処理(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            this.Ichiran.CellValidating -= new EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.Ichiran_CellValidating);

            var messageShowLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "業者CD");
                this.GYOUSHA_CD.Focus();
            }
            else if (string.IsNullOrEmpty(this.GENBA_CD.Text)
                && (this.logic.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SHOBUN
                    || this.logic.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SAISHUSHOBUN))
            {
                messageShowLogic.MessageBoxShow("E001", "現場CD");
                this.GENBA_CD.Focus();
            }
            else
            {
                this.Ichiran.AllowUserToAddRows = true;//thongh 2015/12/28 #1981
                int count = this.logic.Search();
                if (count == 0)
                {
                    messageShowLogic.MessageBoxShow("E020", "地域別許可番号");
                }
                else if (count > 0)
                {
                    this.Ichiran.ReadOnly = false;
                    bool catchErr = this.logic.SetIchiran();
                    if (catchErr)
                    {
                        return;
                    }

                    //ポップボタンの使用状態をセットする
                    for (int i = 0; i < this.Ichiran.Rows.Count; i++)
                    {
                        catchErr = SetBtnHinmeiEnabled(i, true);
                        if (catchErr)
                        {
                            return;
                        }
                    }

                    // 検索直後は明細のRowsAddedイベントが発生しないのでここでRowsAddedイベントと同内容の設定をする
                    catchErr = SetNewRowValue(this.Ichiran.Rows.Count - 1);
                    if (catchErr)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            this.Ichiran.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.Ichiran_CellValidating);

            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        /// <summary>
        /// 登録処理(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                /// 20141217 Houkakou 「地域別許可番号入力」の日付チェックを追加する　start
                if (this.logic.AllDateCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「地域別許可番号入力」の日付チェックを追加する　end

                // 報告書分類CD重複チェック
                if (false == this.logic.DuplicationCheck(WINDOW_ID.M_HOUKOKUSHO_BUNRUI))
                {
                    // 重複があった場合は処理なし
                    return;
                }

                // 検索実行前に登録を押下された場合の対応
                if (this.Ichiran.DataSource == null || this.Ichiran.RowCount == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E064", "登録処理");
                    return;
                }

                bool catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }
                this.logic.Regist(base.RegistErrorFlag);
                if (base.RegistErrorFlag)
                {
                    return;
                }
                this.Search(sender, e);
            }
        }

        /// <summary>
        /// 取消(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.Cancel();
        }

        /// <summary>
        /// Formクローズ処理(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            // 許可区分
            Properties.Settings.Default.KyokaKbnValue = (short)this.logic.KyokaKbn;

            // 業者コード
            Properties.Settings.Default.GyoshaCDValue_Text = this.GYOUSHA_CD.Text;

            // 現場コード
            Properties.Settings.Default.GenbaCDValue_Text = this.GENBA_CD.Text;

            Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
            Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
            Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
            Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;

            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        #endregion

        #region 処理Noボタンイベント

        /// <summary>
        /// 運搬処理(処理No1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ModeChangeUnpan(object sender, EventArgs e)
        {
            this.logic.ModeChange(ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.UNPAN);
        }

        /// <summary>
        /// 処分処理(処理No2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ModeChangeShobun(object sender, EventArgs e)
        {
            this.logic.ModeChange(ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SHOBUN);
        }

        /// <summary>
        /// 最終処分処理(処理No3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ModeChangeSaishuShobun(object sender, EventArgs e)
        {
            this.logic.ModeChange(ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SAISHUSHOBUN);
        }

        #endregion

        #region 業者CDイベント

        /// <summary>
        /// 業者CD Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.PrevGyoushaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者CD Leaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.logic.CheckGyoushaCD())
            {
                e.Cancel = true;
            }
            else if (this.logic.PrevGyoushaCd != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                this.logic.IchiranClear();
            }
        }

        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            if (this.GYOUSHA_CD.Text != this.logic.PrevGyoushaCd)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                this.logic.IchiranClear();
            }
        }

        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            this.logic.PrevGyoushaCd = this.GYOUSHA_CD.Text;
        }

        #endregion

        #region 現場CDイベント

        /// <summary>
        /// 現場CD Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.PrevGenbaCd = this.GENBA_CD.Text;
        }

        /// <summary>
        /// 現場CD Leaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.logic.CheckGenbaCD())
            {
                e.Cancel = true;
            }
            else if (this.logic.PrevGenbaCd != this.GENBA_CD.Text)
            {
                this.logic.IchiranClear();
            }
        }

        #endregion

        #region 許可区分イベント

        /// <summary>
        /// 許可区分変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KYOKA_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeKyokaKbn();
        }

        #endregion

        #region 一覧イベント

        /// <summary>
        /// 日付コントロールの初期設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEndEdit(object sender, GrapeCity.Win.MultiRow.CellEndEditEventArgs e)
        {
            GcMultiRow gcMultiRow = (GcMultiRow)sender;
            if (e.EditCanceled == false)
            {
                if (gcMultiRow.CurrentCell is GcCustomDataTimePicker)
                {
                    if (gcMultiRow.CurrentCell.Value == null
                        || string.IsNullOrEmpty(gcMultiRow.CurrentCell.Value.ToString()))
                    {
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                        //gcMultiRow.CurrentCell.Value = DateTime.Today;
                        gcMultiRow.CurrentCell.Value = this.logic.parentForm.sysDate;
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                    }
                }
            }
        }

        /// <summary>
        /// 地域CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            // 登録チェックメソッド設定変更処理
            bool catchErr = this.logic.Ichiran_ChangeRegistCheckMethod(e.RowIndex);
            if (catchErr)
            {
                return;
            }

            // 取消押下時の重複チェックを抑制する為、検索結果がNULLでないかをチェックする。
            if (e.CellName.Equals(Const.ChiikibetsuKyokaBangouNyuuryokuConstans.CHIIKI_CD) &&
                this.logic.SearchResult != null)
            {
                bool isNoErr = this.logic.DuplicationCheck(WINDOW_ID.M_CHIIKI);
                if (!isNoErr)
                {
                    e.Cancel = true;

                    GcMultiRow gc = sender as GcMultiRow;
                    if (gc != null && gc.EditingControl != null)
                    {
                        ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                    }

                    return;
                }
            }

            /// 20141217 Houkakou 「地域別許可番号入力」の日付チェックを追加する　start
            if (e.CellName.Equals("FUTSUU_KYOKA_BEGIN"))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["FUTSUU_KYOKA_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                string strdate_to = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["FUTSUU_KYOKA_END"].Value);

                if (!string.IsNullOrEmpty(strdate_to))
                {
                    this.Ichiran.Rows[e.RowIndex].Cells["FUTSUU_KYOKA_END"].Style.BackColor = Constans.NOMAL_COLOR;
                }
            }

            if (e.CellName.Equals("FUTSUU_KYOKA_END"))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["FUTSUU_KYOKA_END"].Style.BackColor = Constans.NOMAL_COLOR;
                string strdate_from = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["FUTSUU_KYOKA_BEGIN"].Value);

                if (!string.IsNullOrEmpty(strdate_from))
                {
                    this.Ichiran.Rows[e.RowIndex].Cells["FUTSUU_KYOKA_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                }
            }

            if (e.CellName.Equals("TOKUBETSU_KYOKA_BEGIN"))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["TOKUBETSU_KYOKA_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                string strdate_to = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["TOKUBETSU_KYOKA_END"].Value);

                if (!string.IsNullOrEmpty(strdate_to))
                {
                    this.Ichiran.Rows[e.RowIndex].Cells["TOKUBETSU_KYOKA_END"].Style.BackColor = Constans.NOMAL_COLOR;
                }
            }

            if (e.CellName.Equals("TOKUBETSU_KYOKA_END"))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["TOKUBETSU_KYOKA_END"].Style.BackColor = Constans.NOMAL_COLOR;
                string strdate_from = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["TOKUBETSU_KYOKA_BEGIN"].Value);

                if (!string.IsNullOrEmpty(strdate_from))
                {
                    this.Ichiran.Rows[e.RowIndex].Cells["TOKUBETSU_KYOKA_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                }
            }
            /// 20141217 Houkakou 「地域別許可番号入力」の日付チェックを追加する　end
        }

        /// <summary>
        /// セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 業者CDが空白の場合、明細入力ができないようにする
            if ((this.GYOUSHA_CD.TextLength <= 0) ||
                //(this.GENBA_CD.TextLength <= 0) || // 現場CDは必須ではない為、外す
                (this.logic.SearchResultAll == null && this.Ichiran.CurrentRow == null))
            {
                this.Ichiran.CurrentRow.Selectable = false;
            }
            else
            {
                this.Ichiran.CurrentRow.Selectable = true;
            }

            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran.Rows[e.RowIndex]["DELETE_FLG"].Selectable = false;
            }
            else
            {
                this.Ichiran.Rows[e.RowIndex]["DELETE_FLG"].Selectable = true;
            }
        }

        /// <summary>
        /// セルボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellContentButtonClick(object sender, CellEventArgs e)
        {
            if (this.logic.SearchResultAll == null)
            {
                return;
            }

            if (e.CellName == "btnSanshoFutsu")
            {
                // 許可証参照ボタン押下処理
                this.logic.FileRefClick(
                    false,
                    e.RowIndex);
            }
            else if (e.CellName == "btnSanshoTokkan")
            {
                // 許可証参照ボタン押下処理
                this.logic.FileRefClick(
                    true,
                    e.RowIndex);
            }
            else if (e.CellName == "btnEtsuranFutsu")
            {
                // 許可証閲覧ボタン押下処理
                this.logic.BrowseClick(
                    false,
                    e.RowIndex);
            }
            else if (e.CellName == "btnEtsuranTokkan")
            {
                // 許可証閲覧ボタン押下処理
                this.logic.BrowseClick(
                    true,
                    e.RowIndex);
            }
            else if (e.CellName == "btnClearFutsu")
            {
                // 許可証クリアボタン押下処理
                this.logic.FileClearClick(
                    false,
                    e.RowIndex);
            }
            else if (e.CellName == "btnClearTokkan")
            {
                // 許可証クリアボタン押下処理
                this.logic.FileClearClick(
                    true,
                    e.RowIndex);
            }
        }

        #endregion

        #region 品目ボタン制御用処理

        // 品目ボタン押下時の検索SQLに地域CD、業者CDが必須のため
        // ①品目ボタン押下の条件として地域CDを必須
        // ②新規行が追加されたら業者CDを設定
        // としている

        // 注意：検索実行直後はRowsAddedイベントが発生しないため、検索実行後の
        // 　　　新規行は別途設定を行っている。

        /// <summary>
        /// 明細 - RowsAddedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            SetNewRowValue(e.RowIndex);
        }

        /// <summary>
        /// 明細の新規行が追加された際の初期設定を行います
        /// </summary>
        /// <param name="index"></param>
        private bool SetNewRowValue(int index)
        {
            try
            {
                if (index < 0)
                {
                    return false;
                }

                if (this.Ichiran.Rows[index].IsNewRow)
                {
                    // 新規行が追加された時点では地域が決まっていないので品目ボタンは押せない
                    bool catchErr = this.SetBtnHinmeiEnabled(index, false);
                    if (catchErr)
                    {
                        return true;
                    }

                    // 許可モード - 運搬時
                    if (this.logic.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.UNPAN)
                    {
                        // 検索用業者CDを設定(現場CDはなし)
                        this.Ichiran.Rows[index]["GYOUSHA_CD"].Value = this.GYOUSHA_CD.Text;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetNewRowValue", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 品目ボタンの使用可否を設定します
        /// </summary>
        /// <param name="rowIndex">明細のIndex</param>
        /// <param name="enabled">Enableプロパティの値</param>
        private bool SetBtnHinmeiEnabled(int rowIndex, bool enabled)
        {
            try
            {
                if (this.Ichiran.DataSource != null)
                {
                    if (this.Ichiran.Rows[rowIndex]["CHIIKI_CD"].Value != null
                        && !string.IsNullOrEmpty(this.Ichiran.Rows[rowIndex]["CHIIKI_CD"].Value.ToString()))
                    {
                        this.Ichiran.Rows[rowIndex]["BtnHinmeiFutsu"].Enabled = enabled;
                        this.Ichiran.Rows[rowIndex]["BtnHinmeiTokkan"].Enabled = enabled;
                        this.Ichiran.Rows[rowIndex]["BtnHinmeiFutsu"].ReadOnly = !enabled;
                        this.Ichiran.Rows[rowIndex]["BtnHinmeiTokkan"].ReadOnly = !enabled;
                    }
                    else
                    {
                        this.Ichiran.Rows[rowIndex]["BtnHinmeiFutsu"].Enabled = false;
                        this.Ichiran.Rows[rowIndex]["BtnHinmeiTokkan"].Enabled = false;
                        this.Ichiran.Rows[rowIndex]["BtnHinmeiFutsu"].ReadOnly = true;
                        this.Ichiran.Rows[rowIndex]["BtnHinmeiTokkan"].ReadOnly = true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetBtnHinmeiEnabled", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 明細 - CellValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidated(object sender, CellEventArgs e)
        {
            if (e.CellName == "CHIIKI_CD")
            {
                // 地域CDが品目ボタンで必須のため地域CDの有無によって品目ボタンの使用可否を設定
                if (this.Ichiran.Rows[e.RowIndex]["CHIIKI_CD"].Value != null &&
                    !string.IsNullOrEmpty(this.Ichiran.Rows[e.RowIndex]["CHIIKI_CD"].Value.ToString()))
                {
                    bool catchErr = this.SetBtnHinmeiEnabled(e.RowIndex, true);
                    if (catchErr)
                    {
                        return;
                    }

                    // 許可モード - 運搬時
                    if (this.logic.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.UNPAN)
                    {
                        this.Ichiran.Rows[e.RowIndex]["GYOUSHA_CD"].Value = this.GYOUSHA_CD.Text;
                    }
                }
                else
                {
                    bool catchErr = this.SetBtnHinmeiEnabled(e.RowIndex, false);
                    if (catchErr)
                    {
                        return;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChiikibetsuKyokaBangoHoshuForm_Shown(object sender, EventArgs e)
        {
            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        // thongh 2015/12/28 #1983 start
        public void GENBA_CD_PopupAfterExecuteMethod()
        {
            if (this.logic.PrevGenbaCd != this.GENBA_CD.Text)
            {
                this.logic.IchiranClear();
            }
        }
        // thongh 2015/12/28 #1983 end

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }

        //CongBinh 20210714 #152813 S
        /// <summary>
        /// 
        /// </summary>
        internal void SearchDataFromIchiran()
        {
            if (this.KyokaKbn > 0)
            {
                this.logic.CancelCondition();
                if (this.KyokaKbn == 1)
                {
                    this.logic.ModeChange(ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.UNPAN);
                }
                else if (this.KyokaKbn == 2)
                {
                    this.logic.ModeChange(ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SHOBUN);
                }
                else
                {
                    this.logic.ModeChange(ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SAISHUSHOBUN);
                }
                this.GYOUSHA_CD.Text = this.GyoushaCd;
                if (this.logic.CheckGyoushaCD())
                {
                    this.ActiveControl = this.GYOUSHA_CD;
                    this.GYOUSHA_CD.Focus();
                    return;
                }
                if (this.KyokaKbn > 1)
                {
                    this.GENBA_CD.Text = this.GenbaCd;
                    if (this.logic.CheckGenbaCD())
                    {
                        this.ActiveControl = this.GENBA_CD;
                        this.GENBA_CD.Focus();
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    this.Search(null, null);
                }
            }
        }
        //CongBinh 20210714 #152813 E
    }
}