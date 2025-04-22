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

namespace Shougun.Core.ReceiptPayManagement.NyukinNyuryoku3
{
    /// <summary>
    /// G619 入金入力 画面クラス
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
        /// 銀行CD必須チェックオブジェクト
        /// </summary>
        internal SelectCheckDto bankCdRegistCheckMethod;

        /// <summary>
        /// 銀行支店CD必須チェックオブジェクト
        /// </summary>
        internal SelectCheckDto bankShitenCdRegistCheckMethod;

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

        ///// <summary>
        ///// 表示されたフラグ
        ///// </summary>
        //private bool isShown = false;
        /// <summary>
        /// 請求から転送されたデータ 
        /// </summary>
        internal SeikyuuDTOClass SeikyuuDto { get; set; }//160013

        #endregion

        #region プロパティ
        /// <summary>
        /// データ移動モードで指定された入金先CDを取得・設定します
        /// </summary>
        internal string MoveDataNyuukinsakiCd { get; set; }
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
        /// <param name="nyuukinNumber">入金番号</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType, string nyuukinNumber) : this(headerForm, windowType, nyuukinNumber, string.Empty) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面区分</param>
        /// <param name="nyuukinNumber">入金番号</param>
        /// <param name="seq">入金一括入力SEQ</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType, string nyuukinNumber, string seq)
            : base(WINDOW_ID.T_NYUKIN_TORIHIKISAKI, windowType)
        {
            this.InitializeComponent();

            this.headerForm = headerForm;
            this.logic = new LogicClass(this.headerForm, this);

            if (!string.IsNullOrEmpty(nyuukinNumber) && !string.IsNullOrEmpty(seq))
            {
                this.logic.SetNyuukinNumberAndSeq(this.logic.ConvertToSqlInt64(nyuukinNumber), this.logic.ConvertToSqlInt32(seq));
            }
            else if (!string.IsNullOrEmpty(nyuukinNumber))
            {
                this.logic.SetNyuukinNumber(this.logic.ConvertToSqlInt64(nyuukinNumber));
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
            this.MoveDataNyuukinsakiCd = torihikisakiCd;
        }
        /// <summary>
        /// 160013
        /// </summary>
        /// <param name="headerForm"></param>
        /// <param name="windowType"></param>
        /// <param name="seikyuuPrm"></param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType, List<string> seikyuuPrm)

            : this(headerForm, windowType)
        {
            this.SeikyuuDto = new SeikyuuDTOClass();
            this.SeikyuuDto.TorihikisakiCd = seikyuuPrm[0];
            this.SeikyuuDto.SeikyuuDate = DateTime.Parse(seikyuuPrm[1]);
            this.SeikyuuDto.NyuushukinCd = Int16.Parse(seikyuuPrm[2]);
            for (int i = 3; i < seikyuuPrm.Count; i++)
            {
                this.SeikyuuDto.SeikyuuNumbers.Add(Int64.Parse(seikyuuPrm[i]));
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
                this.logic.SetNyuukinNumber(SqlInt64.Null);
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

            if (string.IsNullOrEmpty(this.NYUUKIN_NUMBER.Text))
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
            if (r_framework.Authority.Manager.CheckAuthority("G620", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                FormManager.OpenFormWithAuth("G620", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.DENPYOU_DATE.Text, this.TORIHIKISAKI_CD.Text, this.TORIHIKISAKI_NAME.Text, this.NYUUKIN_NUMBER.Text);
            }
            else if (r_framework.Authority.Manager.CheckAuthority("G620", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                FormManager.OpenFormWithAuth("G620", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.DENPYOU_DATE.Text, this.TORIHIKISAKI_CD.Text, this.TORIHIKISAKI_NAME.Text, this.NYUUKIN_NUMBER.Text);
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

            FormManager.OpenFormWithAuth("G084", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

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
        /// [1]入金額一括コピーボタンをクリックしたときに処理します
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

            //// Anchorの設定は必ずOnLoadで行うこと
            //if (this.DETAIL_Ichiran != null)
            //{
            //    this.DETAIL_Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            //}

            //if (this.KESHIKOMI_Ichiran != null)
            //{
            //    int GRID_HEIGHT_MIN_VALUE = 169;
            //    int GRID_WIDTH_MIN_VALUE = 989;
            //    int h = this.Height - 271;
            //    int w = this.Width;

            //    if (h < GRID_HEIGHT_MIN_VALUE)
            //    {
            //        this.KESHIKOMI_Ichiran.Height = GRID_HEIGHT_MIN_VALUE;
            //    }
            //    else
            //    {
            //        this.KESHIKOMI_Ichiran.Height = h;
            //    }
            //    if (w < GRID_WIDTH_MIN_VALUE)
            //    {
            //        this.KESHIKOMI_Ichiran.Width = GRID_WIDTH_MIN_VALUE;
            //    }
            //    else
            //    {
            //        this.KESHIKOMI_Ichiran.Width = w;
            //    }

            //    if (this.KESHIKOMI_Ichiran.Height <= GRID_HEIGHT_MIN_VALUE
            //        || this.KESHIKOMI_Ichiran.Width <= GRID_WIDTH_MIN_VALUE)
            //    {
            //        this.KESHIKOMI_Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            //    }
            //    else
            //    {
            //        this.KESHIKOMI_Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            //    }
            //}
            LogUtility.DebugMethodEnd();
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
        /// 入金番号テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKIN_NUMBER_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.NYUUKIN_NUMBER_Validated(sender, e);

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
        /// 銀行CDエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_CD_Enter(object sender, EventArgs e)
        {
            this.beforeBankCd = this.BANK_CD.Text;
        }

        /// <summary>
        /// 銀行CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BANK_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (string.IsNullOrEmpty(this.BANK_CD.Text) || !this.beforeBankCd.Equals(this.BANK_CD.Text))
            {
                this.BANK_SHITEN_CD.Text = string.Empty;
                this.BANK_SHIETN_NAME_RYAKU.Text = string.Empty;
                this.KOUZA_SHURUI.Text = string.Empty;
                this.KOUZA_NO.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行支店CDテキストボックスにフォーカスが移動したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BANK_SHITEN_CD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.BANK_SHITEN_CD.CausesValidation = true;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一括明細一覧の描画時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DETAIL_Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.DETAIL_Ichiran_CellPainting(sender, e);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一括明細一覧の編集用のセルが表示されているときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DETAIL_Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

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

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一括明細一覧にユーザが行を追加したときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DETAIL_Ichiran_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (this.DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).Count() == this.logic.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
            {
                this.DETAIL_Ichiran.AllowUserToAddRows = false;
            }
        }

        /// <summary>
        /// 入金明細一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DETAIL_Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.DETAIL_Ichiran_CellEnter(sender, e);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一括一覧で行のバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DETAIL_Ichiran_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.SetBankCheck();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一括一覧で行のバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DETAIL_Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.DETAIL_Ichiran_CellValidating(sender, e);

            LogUtility.DebugMethodEnd();
        }

        private void KESHIKOMI_Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

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

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一覧のバリデート実行時に処理します
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
        /// 銀行支店ポップアップを閉じた後に処理します
        /// </summary>
        public void BankShitenPopupAfter()
        {
            LogUtility.DebugMethodStart();

            // 何でこれを設定しているかわからないので残しておく
            // コレがあると 存在しないCD入力 ⇒ アラート ⇒ ポップアップ表示 ⇒ 未選択で閉じる でフォーカスアウトが出来てしまう
            //this.BANK_SHITEN_CD.CausesValidation = false;
            // ↓
            // 上記の設定は銀行入力⇒支店をポップアップで入力⇒フォーカスアウト でもう一度ポップアップが上がらないようにするためのものだったと思われます。
            // フォーカスアウト時のポップアップの制御はframework側で行っているため前回値もそっちで持っている。
            // framework側の前回値を更新するため(OnEnterを走らせるため)フォーカスを当てなおしています。
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
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            //if (!isShown)
            //{
            //    this.Height -= 7;
            //    isShown = true;
            //}
            base.OnShown(e);
            // Anchorの設定は必ずOnShownで行うこと
            this.DETAIL_Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.KESHIKOMI_Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        }

        //thongh 2015/08/07 #12106 start
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
                if (rows.Any())
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
        //thongh 2015/08/07 #12106 end
    }
}
