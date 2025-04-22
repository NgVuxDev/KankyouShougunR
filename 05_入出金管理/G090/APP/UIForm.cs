using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using System.Collections.Generic;

namespace Shougun.Core.ReceiptPayManagement.Syukinnyuryoku
{
    /// <summary>
    /// G090 出金入力 画面クラス
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// ロジッククラス
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 銀行CDの前回値保持用
        /// </summary>
        internal string beforeBankCd;

        /// <summary>
        /// 取引先CDの前回値
        /// </summary>
        internal string beforeTorihikisakiCd;

        /// <summary>
        /// 取引先CDの前回値
        /// </summary>
        internal string beforeTorihikisakiName;

        /// <summary>
        /// 伝票日付の前回値
        /// </summary>
        internal string beforeDenpyouDate;

        /// <summary>
        /// エラーフラグ
        /// </summary>
        internal bool isError;

        /// <summary>
        /// 検索伝票日付
        /// </summary>
        internal string searchDate;

        /// <summary>
        /// 入力エラーの状態を保持
        /// </summary>
        private bool IsInputErrorOccured;
        #endregion

        #region プロパティ
        /// <summary>
        /// データ移動モードで指定された出金先CDを取得・設定します
        /// </summary>
        internal string MoveDataShukkinsakiCd { get; set; }
        /// <summary>
        /// 支払から転送されたデータ 
        /// </summary>
        internal ShiharaiDTOClass ShiharaiDto { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面区分</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType) : this(headerForm, windowType, string.Empty, string.Empty) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面区分</param>
        /// <param name="nyuukinNumber">出金番号</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType, string nyuukinNumber) : this(headerForm, windowType, nyuukinNumber, string.Empty) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面区分</param>
        /// <param name="nyuukinNumber">出金番号</param>
        /// <param name="seq">出金一括入力SEQ</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType, string nyuukinNumber, string seq)
            : base(WINDOW_ID.T_SHUKKIN, windowType)
        {
            this.InitializeComponent();

            this.headerForm = headerForm;
            this.logic = new LogicClass(this.headerForm, this);

            if (!string.IsNullOrEmpty(nyuukinNumber) && !string.IsNullOrEmpty(seq))
            {
                this.logic.SetShukkinNumberAndSeq(this.logic.ConvertToSqlInt64(nyuukinNumber), this.logic.ConvertToSqlInt32(seq));
            }
            else if (!string.IsNullOrEmpty(nyuukinNumber))
            {
                this.logic.SetShukkinNumber(this.logic.ConvertToSqlInt64(nyuukinNumber));
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面区分</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="gyousyaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType, string torihikisakiCd, string gyousyaCd, string genbaCd)
            : this(headerForm, windowType)
        {
            this.MoveDataShukkinsakiCd = torihikisakiCd;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerForm"></param>
        /// <param name="windowType"></param>
        /// <param name="shiharaiPrm"></param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType, List<string> shiharaiPrm)
            : this(headerForm, windowType)
        {
            this.ShiharaiDto = new ShiharaiDTOClass();
            this.ShiharaiDto.TorihikisakiCd = shiharaiPrm[0];
            this.ShiharaiDto.SeisanDate = DateTime.Parse(shiharaiPrm[1]);
            this.ShiharaiDto.NyuushukinCd = Int16.Parse(shiharaiPrm[2]);
            for (int i = 3; i < shiharaiPrm.Count; i++)
            {
                this.ShiharaiDto.SeisanNumbers.Add(Int64.Parse(shiharaiPrm[i]));
            }
        }
        #endregion

        #region ボタン押下時メソッド
        /// <summary>
        /// [F2]新規ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());
            if (Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                this.logic.SetShukkinNumber(SqlInt64.Null);
                this.logic.WindowInit(true);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F3]修正ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (string.IsNullOrEmpty(this.SHUKKIN_NUMBER.Text))
            {
                return;
            }
            var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());
            // 修正権限が無い場合は、参照権限で表示
            if (r_framework.Authority.Manager.CheckAuthority(formID, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                this.logic.WindowInit(true);
            }
            else if (r_framework.Authority.Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                this.logic.WindowInit(true);
            }
            else
            {
                MessageBoxShowLogic msg = new MessageBoxShowLogic();
                msg.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.UPDATE_WINDOW_FLAG));

                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F5]消込修正ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 修正権限が無い場合は、参照権限で表示
            if (r_framework.Authority.Manager.CheckAuthority("G751", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                FormManager.OpenFormWithAuth("G751", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.DENPYOU_DATE.Text, this.TORIHIKISAKI_CD.Text, this.TORIHIKISAKI_NAME.Text, this.SHUKKIN_NUMBER.Text);
            }
            else if (r_framework.Authority.Manager.CheckAuthority("G751", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                FormManager.OpenFormWithAuth("G751", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.DENPYOU_DATE.Text, this.TORIHIKISAKI_CD.Text, this.TORIHIKISAKI_NAME.Text, this.SHUKKIN_NUMBER.Text);
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.REFERENCE_WINDOW_FLAG));
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F6]消込一覧ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenFormWithAuth("G752", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F7]一覧ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.DENPYOU_DATE.Value != null)
            {
                FormManager.OpenFormWithAuth("G078", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, "1");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F8]検索ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.logic.Search() == -1)
            {
                return;
            }

            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F9]登録ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.Regist();

            LogUtility.DebugMethodEnd();
        }

        ///// <summary>
        ///// [F10]並べ替えボタンをクリックしたときに処理します
        ///// </summary>
        ///// <param name="sender">イベントが発生したオブジェクト</param>
        ///// <param name="e">イベント引数</param>
        //public void bt_func10_Click(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    this.logic.DateSort();

        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// [F12]閉じるボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [1]出金額一括コピーボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.IkkatuKeshikomi())
            {
                return;
            }

            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [2]手形情報ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.TesuuryouKeshikomi())
            {
                return;
            }

            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [3]行削除ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.RowAdd())
            {
                return;
            }

            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [4]行挿入ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.RowRemove())
            {
                return;
            }

            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 前ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void PrevButton_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.Prev();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 次ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void NextButton_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.Next();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベント
        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            // オールコントロールにヘッダーのコントロールも含める
            this.allControl = this.allControl.Concat(this.headerForm.allControl).ToArray();

            this.logic.WindowInit(true);
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Anchorの設定は必ずOnShownで行うこと
            this.DETAIL_Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.KESHIKOMI_Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        }
        /// <summary>
        /// KeyDownイベントハンドラ
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.KESHIKOMI_Ichiran.RowCount == 0)
            {
                this.KESHIKOMI_Ichiran.TabStop = false;
            }
            else
            {
                this.KESHIKOMI_Ichiran.TabStop = true;
            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// 出金番号テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUKKIN_NUMBER_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.SHUKKIN_NUMBER_Validated(sender, e);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票日付が入力されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DENPYOU_DATE_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            DateTime denpyoudate;
            if (DateTime.TryParse(this.DENPYOU_DATE.Text, out denpyoudate))
            {
                this.beforeDenpyouDate = this.DENPYOU_DATE.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票日付のバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DENPYOU_DATE_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.DENPYOU_DATE_Validated(sender, e);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDテキストボックスが入力されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.IsInputErrorOccured && !this.isError)
            {
                // 取引先CDが入力エラーで無い場合、現在の入力値を変更前取引先CDとする
                this.beforeTorihikisakiCd = this.TORIHIKISAKI_CD.Text;
                this.beforeTorihikisakiName = this.TORIHIKISAKI_NAME.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.TORIHIKISAKI_CD_Validated(sender, e);
            // 今回消込額が残るので再計算
            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDテキストボックスのバリデート中に処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.IsInputErrorOccured = false;
            if (this.TORIHIKISAKI_CD.IsInputErrorOccured)
            {
                this.IsInputErrorOccured = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出金一括明細一覧の描画時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DETAIL_Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            this.logic.DETAIL_Ichiran_CellPainting(sender, e);
        }

        /// <summary>
        /// 出金一括明細一覧の編集用のセルが表示されているときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DETAIL_Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                var dgv = (CustomDataGridView)sender;
                var control = (DataGridViewTextBoxEditingControl)e.Control;

                switch (dgv.CurrentCell.OwningColumn.Name)
                {
                    case "MEISAI_BIKOU":
                        control.ImeMode = ImeMode.Hiragana;
                        break;
                    case "NYUUSHUKKIN_KBN_CD":
                    case "KINGAKU":
                    default:
                        control.ImeMode = ImeMode.Disable;
                        break;
                }
            }
        }

        /// <summary>
        /// 出金明細一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DETAIL_Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.DETAIL_Ichiran_CellEnter(sender, e);
        }

        /// <summary>
        /// 出金一括一覧で行のバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DETAIL_Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.logic.DETAIL_Ichiran_CellValidating(sender, e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KESHIKOMI_Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                var dgv = (CustomDataGridView)sender;
                var control = (DataGridViewTextBoxEditingControl)e.Control;

                switch (dgv.CurrentCell.OwningColumn.Name)
                {
                    case "KESHIKOMI_KINGAKU":
                        control.ImeMode = ImeMode.Disable;
                        break;
                    case "KESHIKOMI_BIKOU":
                        control.ImeMode = ImeMode.Hiragana;
                        break;
                    default:
                        control.ImeMode = ImeMode.Disable;
                        break;
                }
            }
        }

        /// <summary>
        /// 出金一覧のバリデート実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void KESHIKOMI_Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.KESHIKOMI_Ichiran_CellValidating(sender, e))
            {
                return;
            }

            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 
        /// </summary>
        public void BankShitenPopupAfter()
        {
            LogUtility.DebugMethodStart();

            this.ActiveControl = null;
            this.BANK_SHITEN_CD.Focus();

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 取引先CDポップアップを閉じた後に処理します
        /// </summary>
        public void TorihikisakiPopupBefore()
        {
            LogUtility.DebugMethodStart();
            this.beforeTorihikisakiCd = this.TORIHIKISAKI_CD.Text;
            this.beforeTorihikisakiName = this.TORIHIKISAKI_NAME.Text;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDポップアップを閉じた後に処理します
        /// </summary>
        public void TorihikisakiPopupAfter()
        {
            LogUtility.DebugMethodStart();

            this.logic.SetTorihikisaki();

            LogUtility.DebugMethodEnd();
        }

        #endregion  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KESHIKOMI_Ichiran_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.KESHIKOMI_Ichiran.Columns[e.ColumnIndex].Name == "DELETE_FLG")
                {
                    this.CalcKesiKomiAmountTotal();
                }                
            }
        }
        /// <summary>
        /// 今回消込額を計算します
        /// </summary>
        internal bool CalcKesiKomiAmountTotal()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 消込額にセットされた値の合計
                decimal kesikomiAmountTotal = 0;
                var rows = this.KESHIKOMI_Ichiran.Rows.Cast<DataGridViewRow>()
                    .Where(r => false == r.IsNewRow && this.logic.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse);
                    //.Sum(r => this.logic.ConvertToDecimal(r.Cells["KESHIKOMI_KINGAKU"].Value));
                if(rows.Any())
                {
                    kesikomiAmountTotal = rows.Sum(r => this.logic.ConvertToDecimal(r.Cells["KESHIKOMI_KINGAKU"].Value));
                }

                this.KESHIKOMIGAKU.Text = this.logic.FormatKingaku(kesikomiAmountTotal);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcKesiKomiAmountTotal", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
    }
}
