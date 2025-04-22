using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.ReceiptPayManagement.NyukinKeshikomiNyuryoku;
using System.Data;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2
{
    /// <summary>
    /// G459 入金入力 画面クラス
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
        private SelectCheckDto bankCdRegistCheckMethod;

        /// <summary>
        /// 銀行支店CD必須チェックオブジェクト
        /// </summary>
        private SelectCheckDto bankShitenCdRegistCheckMethod;

        /// <summary>
        /// 銀行CDの前回値保持用
        /// </summary>
        private string BeforeBankCd;

        /// <summary>
        /// 入金先CDの前回値
        /// </summary>
        private string beforeNyuukinsakiCd;

        /// <summary>
        /// 入金先名の前回値
        /// </summary>
        private string beforeNyuukinsakiName;

        /// <summary>
        /// 取引先CDの前回値
        /// </summary>
        private string beforeTorihikisakiCd;

        /// <summary>
        /// 取引先名の前回値
        /// </summary>
        private string beforeTorihikisakiName;

        /// <summary>
        /// 伝票日付の前回値
        /// </summary>
        private string beforeDenpyouDate;

        /// <summary>
        /// 入力エラーの状態を保持
        /// </summary>
        private bool IsInputErrorOccured;

        ///// <summary>
        ///// 表示されたフラグ
        ///// </summary>
        //private bool isShown = false;

        /// <summary>
        /// NYUUKIN_Ichiranの前回値(key：Cell.Name)
        /// </summary>
        private Dictionary<string, string> beforeNyuukinIchiranDictionary = new Dictionary<string, string>();

        /// <summary>
        /// NYUUKIN_SUM_DETAIL_Ichiranの前回値(key：Cell.Name)
        /// </summary>
        private Dictionary<string, string> beforeNyuukinSumDetailIchiranDictionary = new Dictionary<string, string>();

        /// <summary>
        /// NYUUKIN_Ichiranの入力エラー時に前回値保存処理を回避するためのフラグ
        /// </summary>
        internal bool isNyuukinIchiranInputError = false;

        /// <summary>
        /// NYUUKIN_SUM_DETAIL_Ichiranの入力エラー時に前回値保存処理を回避するためのフラグ
        /// </summary>
        internal bool isNyuukinSumDetailIchiranInputError = false;
        #endregion

        #region プロパティ
        /// <summary>
        /// データ移動モードで指定された入金先CDを取得・設定します
        /// </summary>
        internal string MoveDataNyuukinsakiCd { get; private set; }

        /// <summary>
        /// データ移動モードかどうかを取得・設定します
        /// </summary>
        internal bool IsMoveData { get; private set; }

        /// <summary>
        /// 前回の入金番号(入金消込ボタン用)
        /// </summary>
        internal string BeforeNuukinNumber { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面区分</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType) : this(headerForm, windowType, String.Empty, String.Empty) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面区分</param>
        /// <param name="nyuukinNumber">入金番号</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType, string nyuukinNumber) : this(headerForm, windowType, nyuukinNumber, String.Empty) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面区分</param>
        /// <param name="nyuukinNumber">入金番号</param>
        /// <param name="seq">入金一括入力SEQ</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType, string nyuukinNumber, string seq)
            : base(WINDOW_ID.T_NYUKIN, windowType)
        {
            this.InitializeComponent();

            this.headerForm = headerForm;
            this.logic = new LogicClass(this.headerForm, this);

            if (!String.IsNullOrEmpty(nyuukinNumber) && !String.IsNullOrEmpty(seq))
            {
                this.logic.SetNyuukinNumberAndSeq(this.logic.ConvertToSqlInt64(nyuukinNumber), this.logic.ConvertToSqlInt32(seq));
            }
            else if (!String.IsNullOrEmpty(nyuukinNumber))
            {
                this.logic.SetNyuukinNumber(this.logic.ConvertToSqlInt64(nyuukinNumber));
            }

            this.CreateNyuukinIchiran();
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
            this.IsMoveData = true;
            this.MoveDataNyuukinsakiCd = torihikisakiCd;
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

            var formID = r_framework.FormManager.FormManager.GetFormID(System.Reflection.Assembly.GetExecutingAssembly());
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

            var formID = r_framework.FormManager.FormManager.GetFormID(System.Reflection.Assembly.GetExecutingAssembly());
            if (!Manager.CheckAuthority(formID, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false)
                && !Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                MessageBoxShowLogic msg = new MessageBoxShowLogic();
                msg.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.UPDATE_WINDOW_FLAG));

                return;
            }

            if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.WindowType)
            {
                this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                this.logic.WindowInit(true);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F6]消込履歴ボタンをクリックしたときに処理します
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
        /// [F9]登録ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.RegistEntity();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F10]行挿入ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Count < this.logic.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
            {
                if (this.NYUUKIN_SUM_DETAIL_Ichiran.CurrentRow == null)
                {
                    this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Add();
                }
                else
                {
                    this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Insert(this.NYUUKIN_SUM_DETAIL_Ichiran.CurrentRow.Index, 1);
                }
            }

            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F11]行削除ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.NYUUKIN_SUM_DETAIL_Ichiran.CurrentRow != null)
            {
                if (false == this.NYUUKIN_SUM_DETAIL_Ichiran.CurrentRow.IsNewRow)
                {
                    this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.RemoveAt(this.NYUUKIN_SUM_DETAIL_Ichiran.CurrentRow.Index);
                }
            }

            if (this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Count < this.logic.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
            {
                this.NYUUKIN_SUM_DETAIL_Ichiran.AllowUserToAddRows = true;
            }

            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }

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

            if (this.logic.dto.NyuukinKeshikomiList.Count() > 0)
            {
                var messageLogic = new MessageBoxShowLogic();
                messageLogic.MessageBoxShow("E187");
            }
            else
            {
                var dialogResult = DialogResult.Yes;
                if (this.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow).Count() > 0)
                {
                    var messageLogic = new MessageBoxShowLogic();
                    dialogResult = messageLogic.MessageBoxShow("C055", "入力済の明細を削除して一括コピーを実行");
                }

                if (DialogResult.Yes == dialogResult)
                {
                    // 入力済みの行に削除チェックを付ける
                    foreach (DataGridViewRow row in this.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow))
                    {
                        row.Cells["DELETE_FLG"].Value = true;
                        row.Visible = false;
                    }

                    // 取引先数分の行を追加
                    var denpyouDate = (DateTime)this.DENPYOU_DATE.Value;
                    bool catchErr = true;
                    var torihikisakiSeikyuuList = this.logic.GetTorihikisakiSeikyuuListByNyuukinsakiCdAndDenpyouDate(this.NYUUKINSAKI_CD.Text, denpyouDate, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    foreach (var entity in torihikisakiSeikyuuList)
                    {
                        this.NYUUKIN_Ichiran.Rows.Insert(this.NYUUKIN_Ichiran.Rows.Count - 1, 1);
                        var row = this.NYUUKIN_Ichiran.Rows[this.NYUUKIN_Ichiran.Rows.Count - 2];
                        //20150918 hoanghm 入金額一括コピーを行ってもシステムIDが再割り振りされずにSEQが進むようにする。start
                        foreach (DataGridViewRow gr in this.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow))
                        {
                            if (gr.Cells["TORIHIKISAKI_CD"].Value != null && gr.Cells["TORIHIKISAKI_CD"].Value.ToString() == entity.TORIHIKISAKI_CD)
                            {
                                row.Cells["SYSTEM_ID"].Value = gr.Cells["SYSTEM_ID"].Value;
                            }
                        }
                        //20150918 hoanghm 入金額一括コピーを行ってもシステムIDが再割り振りされずにSEQが進むようにする。end
                        row.Cells["TORIHIKISAKI_CD"].Value = entity.TORIHIKISAKI_CD;
                        var torihikisaki = this.logic.GetTorihikisaki(entity.TORIHIKISAKI_CD, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (null != torihikisaki)
                        {
                            row.Cells["TORIHIKISAKI_NAME_RYAKU"].Value = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                       
                        // 作成した行に、請求日付と請求金額を設定
                        if (!this.SetSeikyuuDateAndSeikyuuKingaku(row))
                        {
                            return;
                        }
                    }

                    // 先頭の取引先に入金額を設定
                    var firstRow = this.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow && this.logic.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse)
                                                                                    .OrderBy(r => this.logic.ConvertToString(r.Cells["TORIHIKISAKI_CD"].Value))
                                                                                    .FirstOrDefault();
                    if (null != firstRow)
                    {
                        var index = 1;
                        foreach (var row in this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow && false == this.logic.IsNullOrEmpty(r.Cells["NYUUSHUKKIN_KBN_CD"].Value)))
                        {
                            firstRow.Cells["NYUUSHUKKIN_KBN_CD_" + index.ToString()].Value = row.Cells["NYUUSHUKKIN_KBN_CD"].Value;
                            firstRow.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU_" + index.ToString()].Value = row.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value;
                            firstRow.Cells["NYUUKIN_KINGAKU_" + index.ToString()].Value = row.Cells["KINGAKU"].Value;

                            index++;
                        }
                    }

                    this.logic.CalcAll();
                }
            }
            

            LogUtility.DebugMethodEnd();
        }

        /// <summary>入金消込画面起動時に使用するメッセージ</summary>
        private const string CONFIRM_REGIST_DATA_MSG = "入金消込画面を表示する場合、一度データを登録してください。\nこのままデータを登録しますか？";

        /// <summary>
        /// [3]入金消込情報ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var messageLogic = new MessageBoxShowLogic();
            int rowIndex = 0;
            var dto = new WindowInitDataDTO();

            try
            {
                // 新規なら一度登録
                if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.WindowType) || WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.WindowType))
                {
                    // 必須チェック
                    var autoCheckLogic = new AutoRegistCheckLogic(this.allControl, this.allControl);
                    this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                    // 入金一覧行に入力が無い場合はエラー
                    if (!this.RegistErrorFlag && this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Count == 1)
                    {
                        DataGridViewRow row = this.NYUUKIN_SUM_DETAIL_Ichiran.Rows[this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Count - 1];
                        if (row.IsNewRow
                            && (string.IsNullOrEmpty(Convert.ToString(row.Cells["NYUUSHUKKIN_KBN_CD"].Value)) || string.IsNullOrEmpty(Convert.ToString(row.Cells["KINGAKU"].Value))))
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E001", "入金区分および入金額");
                            this.RegistErrorFlag = true;
                        }
                    }

                    if (!this.RegistErrorFlag)
                    {
                        // 新規行を選択済みの場合はエラー
                        if (0 < this.NYUUKIN_Ichiran.SelectedCells.Count)
                        {
                            var cell = this.NYUUKIN_Ichiran.SelectedCells[0];
                            
                            DataGridViewRow row = this.NYUUKIN_Ichiran.Rows[cell.RowIndex];
                            if (row.IsNewRow)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E001", "入金区分および入金額");
                                this.RegistErrorFlag = true;
                            }
                        }
                    }

                    // 入金消込情報ボタン押下時の削除フラグチェック
                    if (this.NYUUKIN_Ichiran.CurrentRow != null
                        && this.logic.ConvertToSqlBooleanDefaultFalse(this.NYUUKIN_Ichiran.CurrentRow.Cells["DELETE_FLG"].Value).IsTrue
                        && !this.RegistErrorFlag)
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E271");
                        this.RegistErrorFlag = true;
                    }

                    if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.WindowType && !this.RegistErrorFlag)
                    {
                        // 入金明細一覧をバリデート
                        this.RegistErrorFlag = this.logic.CheckNyuukinIchiran(true);
                    }

                    if (false == this.RegistErrorFlag)
                    {
                        var targetRow = this.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow && this.logic.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse);

                        foreach (DataGridViewRow row in targetRow)
                        {
                            if (false == this.logic.IsNullOrEmpty(row.Cells["TORIHIKISAKI_CD"].Value))
                            {
                                if (this.JudgeTorihikisakiKeshikomi(row.Cells["TORIHIKISAKI_CD"].Value.ToString()))
                                {
                                    var columnCount = this.logic.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value;
                                    var cnt = 0;
                                    for (int i = 1; i <= columnCount; i++)
                                    {
                                        if (true == this.logic.IsNullOrEmpty(row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()].Value) && true == this.logic.IsNullOrEmpty(row.Cells["NYUUKIN_KINGAKU_" + i.ToString()].Value))
                                        {
                                            cnt++;
                                        }
                                    }

                                    if (cnt == columnCount)
                                    {
                                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                        messageLogic.MessageBoxShow("E233", "入金区分と入金金額");
                                        this.RegistErrorFlag = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (this.RegistErrorFlag)
                    {
                        return;
                    }

                    if (DialogResult.Yes == messageLogic.MessageBoxShowConfirm(CONFIRM_REGIST_DATA_MSG))
                    {
                        if (this.NYUUKIN_Ichiran.CurrentRow != null)
                        {
                            rowIndex = this.NYUUKIN_Ichiran.CurrentRow.Index;
                        }
                        if (this.NYUUKIN_Ichiran.CurrentRow != null)
                        {
                            dto.TorihikisakiCd = Convert.ToString(this.NYUUKIN_Ichiran.Rows[rowIndex].Cells["TORIHIKISAKI_CD"].Value);
                            dto.TorihikisakiName = Convert.ToString(this.NYUUKIN_Ichiran.Rows[rowIndex].Cells["TORIHIKISAKI_NAME_RYAKU"].Value);
                        }
                        if (!this.logic.RegistEntity())
                        {
                            return;
                        }
                        if (!this.RegistErrorFlag)
                        {
                            if (!string.IsNullOrEmpty(this.BeforeNuukinNumber))
                            {
                                this.OpenUpdateWindow(this.BeforeNuukinNumber);
                                this.BeforeNuukinNumber = string.Empty;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        // 入金消込画面内でDBの値を参照する処理があるため、DBに登録されていない場合は開いちゃダメ
                        return;
                    }
                }
                // 他モード時の考慮は特にしない。そこまでの厳密性は求められてないと判断するため。
                // もし要望があれば対応
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
            dto.DenpyoDate = this.DENPYOU_DATE.Text;
            //dto.TorihikisakiCd = this.NYUUKINSAKI_CD.Text;
            //dto.TorihikisakiName = this.NYUUKINSAKI_NAME_RYAKU.Text;
            
            dto.KonkaiNyuukingaku = this.NYUUKIN_AMOUNT_TOTAL.Text;
            dto.KonkaiWarifurigaku = this.WARIFURIGAKU.Text;
            dto.NyuukinNumber = Convert.ToString(this.logic.dto.NyuukinSumEntry.NYUUKIN_NUMBER.Value);
            dto.SumSystemId = Convert.ToString(this.logic.dto.NyuukinSumEntry.SYSTEM_ID.Value);

            FormManager.OpenFormModal("G611", dto);

            // 入金消込を再読込
            this.logic.ReloadNyuukinKeshikomiList();

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

            var nyuukinNumber = SqlInt64.Null;
            if (!String.IsNullOrEmpty(this.NYUUKIN_NUMBER.Text))
            {
                nyuukinNumber = this.logic.ConvertToSqlInt64(this.NYUUKIN_NUMBER.Text);
            }

            // 読み込む対象の入金番号を取得
            bool catchErr = true;
            nyuukinNumber = this.logic.GetPrevNyuukinNumber(nyuukinNumber, out catchErr);
            if (!catchErr)
            {
                return;
            }

            if (nyuukinNumber.IsNull)
            {
                //try to get maximun nyuukin number
                nyuukinNumber = this.logic.GetPrevNyuukinNumber(nyuukinNumber, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                //after try to get maximun nyuukin number, if nyuukin number is null then show message
                if (nyuukinNumber.IsNull)
                {
                    // 読み込む対象の入金番号を取得
                    //ThangNguyen [Update] 20150814 #11409 Start
                    //nyuukinNumber = this.logic.GetPrevNyuukinNumber(nyuukinNumber);
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E045");
                    return;
                    //ThangNguyen [Update] 20150814 #11409 End
                }
            }

            var formID = r_framework.FormManager.FormManager.GetFormID(System.Reflection.Assembly.GetExecutingAssembly());

            this.logic.SetNyuukinNumber(nyuukinNumber);

            if (nyuukinNumber.IsNull)
            {
                if (!Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                // 入金データがない
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            }
            else
            {
                if (!Manager.CheckAuthority(formID, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false)
                    && !Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.UPDATE_WINDOW_FLAG));
                    return;
                }

                this.NYUUKIN_NUMBER.Text = nyuukinNumber.Value.ToString();
                this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            }

            this.logic.WindowInit(true);

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

            var nyuukinNumber = SqlInt64.Null;
            if (!String.IsNullOrEmpty(this.NYUUKIN_NUMBER.Text))
            {
                nyuukinNumber = this.logic.ConvertToSqlInt64(this.NYUUKIN_NUMBER.Text);
            }

            // 読み込む対象の入金番号を取得
            bool catchErr = true;
            nyuukinNumber = this.logic.GetNextNyuukinNumber(nyuukinNumber, out catchErr);
            if (!catchErr)
            {
                return;
            }

            if (nyuukinNumber.IsNull)
            {
                //try to get minimum number if nyuukin number is maximum
                nyuukinNumber = this.logic.GetNextNyuukinNumber(0, out catchErr);
                if (!catchErr)
                {
                    return;
                }

                //after try to get minimum number, if nyuukin number is null then show message
                if (nyuukinNumber.IsNull)
                {
                    // 読み込む対象の入金番号を取得
                    //ThangNguyen [Update] 20150814 #11409 Start
                    //nyuukinNumber = this.logic.GetNextNyuukinNumber(nyuukinNumber);
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E045");
                    return;
                    //ThangNguyen [Update] 20150814 #11409 End
                }
            }

            var formID = r_framework.FormManager.FormManager.GetFormID(System.Reflection.Assembly.GetExecutingAssembly());

            this.logic.SetNyuukinNumber(nyuukinNumber);

            if (nyuukinNumber.IsNull)
            {
                if (!Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                // 入金データがない
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            }
            else
            {
                if (!Manager.CheckAuthority(formID, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false)
                    && !Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.UPDATE_WINDOW_FLAG));
                    return;
                }

                this.NYUUKIN_NUMBER.Text = nyuukinNumber.Value.ToString();
                this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            }

            this.logic.WindowInit(true);

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
            //if (this.NYUUKIN_Ichiran != null)
            //{
            //    int GRID_HEIGHT_MIN_VALUE = 169;
            //    int GRID_WIDTH_MIN_VALUE = 989;
            //    int h = this.Height - 271;
            //    int w = this.Width;

            //    if (h < GRID_HEIGHT_MIN_VALUE)
            //    {
            //        this.NYUUKIN_Ichiran.Height = GRID_HEIGHT_MIN_VALUE;
            //    }
            //    else
            //    {
            //        this.NYUUKIN_Ichiran.Height = h;
            //    }
            //    if (w < GRID_WIDTH_MIN_VALUE)
            //    {
            //        this.NYUUKIN_Ichiran.Width = GRID_WIDTH_MIN_VALUE;
            //    }
            //    else
            //    {
            //        this.NYUUKIN_Ichiran.Width = w;
            //    }

            //    if (this.NYUUKIN_Ichiran.Height <= GRID_HEIGHT_MIN_VALUE
            //        || this.NYUUKIN_Ichiran.Width <= GRID_WIDTH_MIN_VALUE)
            //    {
            //        this.NYUUKIN_Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            //    }
            //    else
            //    {
            //        this.NYUUKIN_Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            //    }
            //}
            //if (this.NYUUKIN_SUM_DETAIL_Ichiran != null)
            //{
            //    this.NYUUKIN_SUM_DETAIL_Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            //} 
            
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金番号テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKIN_NUMBER_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 再読み込みするかを示します
            bool isReload = false;

            // 入金蛮行空じゃない場合
            if (!string.IsNullOrEmpty(this.NYUUKIN_NUMBER.Text))
            {
                // 前回値がNULLじゃない場合
                if (!this.logic.GetNyuukinNumber().IsNull)
                {
                    // 入金番号と前回値が同じじゃない場合
                    if (this.logic.ConvertToSqlInt64(this.NYUUKIN_NUMBER.Text).Value != this.logic.GetNyuukinNumber().Value)
                    {
                        isReload = true;
                    }
                }
                else
                {
                    isReload = true;
                }
            }
            else
            {
                // 入金番号の前回値をクリア
                this.logic.SetNyuukinNumber(SqlInt64.Null);
            }

            // 上記条件に合致する場合は読み込みを行う
            if (isReload)
            {
                var formID = r_framework.FormManager.FormManager.GetFormID(System.Reflection.Assembly.GetExecutingAssembly());

                if (!Manager.CheckAuthority(formID, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false)
                    && !Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.UPDATE_WINDOW_FLAG));

                    this.NYUUKIN_NUMBER.Focus();
                    return;
                }

                var dao = DaoInitUtility.GetComponent<IT_NYUUKIN_ENTRYDao>();
                var entry = new T_NYUUKIN_ENTRY();
                entry.NYUUKIN_NUMBER = this.logic.ConvertToSqlInt64(this.NYUUKIN_NUMBER.Text);
                entry.DELETE_FLG = false;
                entry = dao.GetNyuukinEntryList(entry).FirstOrDefault();
                if (entry != null && entry.TOK_INPUT_KBN.IsFalse)
                {
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShowError("該当のデータを修正するには、入金入力（取引先）画面で行ってください。この画面では修正できません。");
                    this.NYUUKIN_NUMBER.Focus();
                    return;
                }
                else if (null == entry)
                {
                    // 該当データなし
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E045");
                    this.NYUUKIN_NUMBER.Focus();
                    return;
                }

                this.OpenUpdateWindow(this.NYUUKIN_NUMBER.Text);
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票日付が入力されたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DENPYOU_DATE_Enter(object sender, EventArgs e)
        {
            DateTime denpyoudate;
            if (DateTime.TryParse(Convert.ToString(this.DENPYOU_DATE.Text), out denpyoudate))
            {
                this.beforeDenpyouDate = Convert.ToString(this.DENPYOU_DATE.Value);
            }
        }

        /// <summary>
        /// 伝票日付テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DENPYOU_DATE_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 入金消込データがある場合は、伝票日付変更時にアラート表示
            if (beforeDenpyouDate != Convert.ToString(this.DENPYOU_DATE.Value))
            {
                if (this.logic.dto.NyuukinKeshikomiList.Count > 0)
                {
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E233", "伝票日付");
                    this.DENPYOU_DATE.Value = beforeDenpyouDate;
                    this.DENPYOU_DATE.Focus();
                    return;
                }
            }

            this.SetSeikyuuDateAndSeikyuuKingaku();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金先CDテキストボックスが入力されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKINSAKI_CD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.IsInputErrorOccured)
            {
                this.beforeNyuukinsakiCd = this.NYUUKINSAKI_CD.Text;
                this.beforeNyuukinsakiName = this.NYUUKINSAKI_NAME_RYAKU.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金先CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKINSAKI_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var messageLogic = new MessageBoxShowLogic();
            if (this.beforeNyuukinsakiCd != this.NYUUKINSAKI_CD.Text)
            {
                // 入金消込データがある場合は、入金先変更時にアラート表示
                    if (this.logic.dto.NyuukinKeshikomiList.Count > 0)
                    {
                        messageLogic.MessageBoxShow("E233", "入金先");
                        this.NYUUKINSAKI_CD.Text = this.beforeNyuukinsakiCd;
                        this.NYUUKINSAKI_NAME_RYAKU.Text = this.beforeNyuukinsakiName;
                        this.NYUUKINSAKI_CD.Focus();
                        return;
                    }
                
                bool catchErr = true;
                if (this.logic.dto.BeforeKariukeControl == null || this.logic.dto.BeforeKariukeControl.NYUUKINSAKI_CD != this.NYUUKINSAKI_CD.Text)
                {
                    if (!String.IsNullOrEmpty(this.NYUUKINSAKI_CD.Text))
                    {
                        var kariukeControl = this.logic.GetKariukeControl(this.NYUUKINSAKI_CD.Text, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (null != kariukeControl)
                        {
                            this.logic.dto.KariukeControl = kariukeControl;
                            if (this.logic.dto.KariukeControl.KARIUKE_TOTAL_KINGAKU.IsNull)
                            {
                                this.KARIUKEKIN.Text = this.logic.FormatKingaku(0, out catchErr);
                                if (!catchErr)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                this.KARIUKEKIN.Text = this.logic.FormatKingaku(this.logic.dto.KariukeControl.KARIUKE_TOTAL_KINGAKU.Value, out catchErr);
                                if (!catchErr)
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            this.KARIUKEKIN.Text = this.logic.FormatKingaku(0, out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        this.KARIUKEKIN.Text = this.logic.FormatKingaku(0, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                    }

                    // 入金先が変更されたら入金入力一覧はクリアする
                    if (this.NYUUKINSAKI_CD.Text != this.logic.NyuukinsakiCd)
                    {
                        this.NYUUKIN_Ichiran.Rows.Clear();
                    }                    
                }
                else
                {
                    var kariukekin = 0m;
                    if (this.logic.dto.BeforeKariukeControl != null && !this.logic.dto.BeforeKariukeControl.KARIUKE_TOTAL_KINGAKU.IsNull)
                    {
                        kariukekin = this.logic.dto.BeforeKariukeControl.KARIUKE_TOTAL_KINGAKU.Value;
                    }
                    if (this.logic.dto.KariukeChousei != null)
                    {
                        kariukekin -= this.logic.dto.KariukeChousei.KINGAKU.Value;
                    }                    
                    this.KARIUKEKIN.Text = this.logic.FormatKingaku(kariukekin, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    this.logic.dto.KariukeControl = this.logic.dto.BeforeKariukeControl;
                }

                this.logic.NyuukinsakiCd = this.NYUUKINSAKI_CD.Text.ToUpper();
            }

            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }
        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NYUUKINSAKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.IsInputErrorOccured = false;
            if (this.NYUUKINSAKI_CD.IsInputErrorOccured)
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
            this.BeforeBankCd = this.BANK_CD.Text;
        }

        /// <summary>
        /// 銀行CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BANK_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (String.IsNullOrEmpty(this.BANK_CD.Text) || !this.BeforeBankCd.Equals(this.BANK_CD.Text))
            {
                this.BANK_SHITEN_CD.Text = String.Empty;
                this.BANK_SHIETN_NAME_RYAKU.Text = String.Empty;
                this.KOUZA_SHURUI.Text = String.Empty;
                this.KOUZA_NO.Text = String.Empty;
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
        private void NYUUKIN_SUM_DETAIL_Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dgv = (DataGridView)sender;

            if (e.RowIndex == -1)
            {
                var columnName = dgv.Columns[e.ColumnIndex].Name;
                if ("NYUUSHUKKIN_KBN_CD" == columnName || "NYUUSHUKKIN_KBN_NAME_RYAKU" == columnName)
                {
                    var rect = e.CellBounds;

                    if ("NYUUSHUKKIN_KBN_CD" == columnName)
                    {
                        rect.Width += dgv.Columns["NYUUSHUKKIN_KBN_NAME_RYAKU"].Width;
                        rect.Y = e.CellBounds.Y + 1;
                    }
                    else
                    {
                        rect.Width += dgv.Columns["NYUUSHUKKIN_KBN_CD"].Width;
                        rect.Y = e.CellBounds.Y + 1;

                        rect.X -= dgv.Columns["NYUUSHUKKIN_KBN_CD"].Width;
                    }

                    using (var brush = new SolidBrush(dgv.ColumnHeadersDefaultCellStyle.BackColor))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                    using (var pen = new Pen(dgv.GridColor))
                    {
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                    using (var pen = new Pen(Color.DarkGray))
                    {
                        e.Graphics.DrawLine(pen, rect.X, rect.Y - 1, rect.X + rect.Width, rect.Y - 1);
                        e.Graphics.DrawLine(pen, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                    }

                    TextRenderer.DrawText(e.Graphics, "入金区分", e.CellStyle.Font, rect, e.CellStyle.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
                }
                else
                {
                    e.Paint(e.ClipBounds, e.PaintParts);
                }

                e.Handled = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一括明細一覧の編集用のセルが表示されているときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKIN_SUM_DETAIL_Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
        private void NYUUKIN_SUM_DETAIL_Ichiran_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow).Count() >= this.logic.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
            {
                this.NYUUKIN_SUM_DETAIL_Ichiran.AllowUserToAddRows = false;
            }
        }

        /// <summary>
        /// 入金一括明細一覧で行のバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKIN_SUM_DETAIL_Ichiran_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetBankCheck();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一括明細一覧のCellValidatingが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKIN_SUM_DETAIL_Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var columnName = this.NYUUKIN_SUM_DETAIL_Ichiran.Columns[e.ColumnIndex].Name;
            switch (columnName)
            {
                case "NYUUSHUKKIN_KBN_CD":
                    string val = Convert.ToString(this.NYUUKIN_SUM_DETAIL_Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    if (this.beforeNyuukinSumDetailIchiranDictionary.ContainsKey(columnName) &&
                       !this.beforeNyuukinSumDetailIchiranDictionary[columnName].Equals(val))
                    {
                        bool result = this.logic.CheckAndSetNyuukinKbn(true, e.RowIndex, columnName, "NYUUSHUKKIN_KBN_NAME_RYAKU");
                        if (!result) e.Cancel = true;
                    }
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一括明細一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NYUUKIN_SUM_DETAIL_Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.isNyuukinSumDetailIchiranInputError)
            {
                string cellName = this.NYUUKIN_SUM_DETAIL_Ichiran.Columns[e.ColumnIndex].Name;
                if (this.beforeNyuukinSumDetailIchiranDictionary.ContainsKey(cellName))
                {
                    beforeNyuukinSumDetailIchiranDictionary[cellName] = Convert.ToString(this.NYUUKIN_SUM_DETAIL_Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
                else
                {
                    beforeNyuukinSumDetailIchiranDictionary.Add(cellName, Convert.ToString(this.NYUUKIN_SUM_DETAIL_Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一括明細一覧のバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKIN_SUM_DETAIL_Ichiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var columnName = this.NYUUKIN_SUM_DETAIL_Ichiran.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "NYUUSHUKKIN_KBN_CD":
                    if (this.logic.IsNullOrEmpty(this.NYUUKIN_SUM_DETAIL_Ichiran.CurrentRow.Cells[columnName].Value))
                    {
                        this.NYUUKIN_SUM_DETAIL_Ichiran.CurrentRow.Cells["KINGAKU"].Value = String.Empty;
                    }
                    break;

                default:
                    break;
            }

            // イベントが発生したセルにかかわらず処理
            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一覧の編集用のセルが表示されているときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKIN_Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                var dgv = (CustomDataGridView)sender;
                var control = (DataGridViewTextBoxEditingControl)e.Control;

                if (dgv.CurrentCell.OwningColumn.Name.StartsWith("BIKOU_"))
                {
                    control.ImeMode = ImeMode.Hiragana;
                }
                else
                {
                    control.ImeMode = ImeMode.Disable;
                }

                var columnName = dgv.CurrentCell.OwningColumn.Name;
                switch (columnName)
                {
                    case "TORIHIKISAKI_CD":
                        {
                            if (!this.IsInputErrorOccured)
                            {
                                this.beforeTorihikisakiCd = control.Text;
                                if (dgv.CurrentRow.Cells["TORIHIKISAKI_NAME_RYAKU"].Value != null)
                                {
                                    this.beforeTorihikisakiName = dgv.CurrentRow.Cells["TORIHIKISAKI_NAME_RYAKU"].Value.ToString();
                                }
                                else
                                {
                                    this.beforeTorihikisakiName = string.Empty;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一覧のCellValueChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NYUUKIN_Ichiran_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (NYUUKIN_Ichiran.IsCurrentCellDirty)
            {
                NYUUKIN_Ichiran.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            var cellName = this.NYUUKIN_Ichiran.Columns[e.ColumnIndex].Name;
            var cell = this.NYUUKIN_Ichiran.CurrentCell;

            switch (cellName)
            {
                case "DELETE_FLG":
                    {
                        if (this.beforeNyuukinIchiranDictionary.ContainsKey(cellName))
                        {
                            var before = this.beforeNyuukinIchiranDictionary[cellName];
                            if (before != Convert.ToString(this.NYUUKIN_Ichiran[e.ColumnIndex, e.RowIndex].Value))
                            {
                                if (cell == null)
                                {
                                    // currentCellがnullの場合、画面操作以外でCellの値が変更された可能性があるので、処理をスキップする。
                                    break;
                                }

                                // 取引先CDの存在チェック
                                var torihikisakiCd = this.logic.ConvertToString(this.NYUUKIN_Ichiran.Rows[cell.RowIndex].Cells["TORIHIKISAKI_CD"].Value);
                                if (torihikisakiCd == null) return;

                                //削除チェックの状態取得
                                var deleteFlg = this.logic.ConvertToSqlBooleanDefaultFalse(cell.Value);
                                if (deleteFlg.IsFalse)
                                {
                                    // すでに同じ取引先CDが入力されていればエラーとする
                                    if (this.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Any(r => !r.IsNewRow &&
                                                                                              r.Index != cell.RowIndex &&
                                                                                              r.Cells["TORIHIKISAKI_CD"].Value != null &&
                                                                                              this.logic.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse &&
                                                                                              this.logic.ConvertToString(torihikisakiCd) == r.Cells["TORIHIKISAKI_CD"].Value.ToString()))
                                    {
                                        var messageLogic = new MessageBoxShowLogic();
                                        messageLogic.MessageBoxShow("E031", "取引先");
                                        cell.Value = true;
                                    }
                                }
                                else
                                {
                                    // 消込されている取引先の場合はエラーとする
                                    if (this.logic.dto.NyuukinKeshikomiList.Any(n => torihikisakiCd.ToString() == n.TORIHIKISAKI_CD))
                                    {
                                        var messageLogic = new MessageBoxShowLogic();
                                        messageLogic.MessageBoxShow("E186");
                                        cell.Value = false;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 入金一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NYUUKIN_Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.isNyuukinIchiranInputError)
            {
                string cellName = this.NYUUKIN_Ichiran.Columns[e.ColumnIndex].Name;
                if (this.beforeNyuukinIchiranDictionary.ContainsKey(cellName))
                {
                    beforeNyuukinIchiranDictionary[cellName] = Convert.ToString(this.NYUUKIN_Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
                else
                {
                    beforeNyuukinIchiranDictionary.Add(cellName, Convert.ToString(this.NYUUKIN_Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一覧のバリデート実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKIN_Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var columnName = this.NYUUKIN_Ichiran.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "TORIHIKISAKI_CD":
                    {
                        // 20150910 BUNN #12115 コード値としてアルファベット小文字が入力されたときの対処 STR
                        if (this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_CD"].Value != null
                            && !string.IsNullOrEmpty(this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_CD"].Value.ToString()))
                        {
                            this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_CD"].Value =
                                this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                        }
                        // 20150910 BUNN #12115 コード値としてアルファベット小文字が入力されたときの対処 END

                        // 前回値比較
                        string val = Convert.ToString(this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_CD"].Value);
                        if (!this.beforeNyuukinIchiranDictionary.ContainsKey(columnName) ||
                            this.beforeNyuukinIchiranDictionary[columnName].Equals(val))
                        {
                            break;
                        }

                        this.isNyuukinIchiranInputError = false;
                        var messageId = String.Empty;
                        var message = String.Empty;
                        var nyuukinsakiCd = this.NYUUKINSAKI_CD.Text;
                        var torihikisakiCd = this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_CD"].Value;
                        if (false == this.logic.IsNullOrEmpty(torihikisakiCd) && nyuukinsakiCd == this.logic.NyuukinsakiCd)
                        {
                            if (String.IsNullOrEmpty(nyuukinsakiCd))
                            {
                                e.Cancel = true;
                                messageId = "E012";
                                message = "入金先";
                            }
                            else
                            {
                                // すでに同じ取引先CDが入力されていればエラーとする
                                if (0 < this.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow &&
                                                                                                     r.Index != e.RowIndex &&
                                                                                                     this.logic.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse &&
                                                                                                     false == this.logic.IsNullOrEmpty(r.Cells["TORIHIKISAKI_CD"].Value) &&
                                                                                                     this.logic.ConvertToString(torihikisakiCd) == this.logic.ConvertToString(r.Cells["TORIHIKISAKI_CD"].Value))
                                                                                         .Count())
                                {
                                    e.Cancel = true;
                                    messageId = "E031";
                                    message = "取引先";
                                }

                                bool catchErr = true;
                                var torihikisakiSeikyuu = this.logic.GetTorihikisakiSeikyuuByTorihikisakiCdAndNyuukinsakiCd(torihikisakiCd.ToString(), nyuukinsakiCd, out catchErr);
                                if (!catchErr)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                                if (null != torihikisakiSeikyuu)
                                {
                                    var torihikisaki = this.logic.GetTorihikisaki(torihikisakiCd.ToString(), out catchErr);
                                    if (!catchErr)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    if (null != torihikisaki)
                                    {
                                        SqlDateTime tekiyouDate = SqlDateTime.Null;
                                        DateTime date;
                                        if (!string.IsNullOrWhiteSpace(this.DENPYOU_DATE.Text) && DateTime.TryParse(this.DENPYOU_DATE.Text, out date))
                                        {
                                            tekiyouDate = date;
                                        }
                                        if (tekiyouDate.IsNull)
                                        {
                                            this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_NAME_RYAKU"].Value = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                                        }
                                        else
                                        {
                                            if (torihikisaki.TEKIYOU_BEGIN.IsNull && torihikisaki.TEKIYOU_END.IsNull)
                                            {
                                                this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_NAME_RYAKU"].Value = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                                            }
                                            else if (torihikisaki.TEKIYOU_BEGIN.IsNull && !torihikisaki.TEKIYOU_END.IsNull
                                                && tekiyouDate.CompareTo(torihikisaki.TEKIYOU_END) <= 0)
                                            {
                                                this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_NAME_RYAKU"].Value = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                                            }
                                            else if (!torihikisaki.TEKIYOU_BEGIN.IsNull && torihikisaki.TEKIYOU_END.IsNull
                                                 && tekiyouDate.CompareTo(torihikisaki.TEKIYOU_BEGIN) >= 0)
                                            {
                                                this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_NAME_RYAKU"].Value = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                                            }
                                            else if (!torihikisaki.TEKIYOU_BEGIN.IsNull && !torihikisaki.TEKIYOU_END.IsNull
                                                 && tekiyouDate.CompareTo(torihikisaki.TEKIYOU_BEGIN) >= 0
                                                 && tekiyouDate.CompareTo(torihikisaki.TEKIYOU_END) <= 0)
                                            {
                                                this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_NAME_RYAKU"].Value = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                                messageId = "E004";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        e.Cancel = true;
                                        messageId = "E004";
                                    }
                                }
                                else
                                {
                                    e.Cancel = true;
                                    messageId = "E004";
                                }
                            }
                        }
                        else
                        {
                            this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_NAME_RYAKU"].Value = String.Empty;
                        }

                        this.IsInputErrorOccured = false;
                        if (e.Cancel)
                        {
                            this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_NAME_RYAKU"].Value = String.Empty;
                            this.isNyuukinIchiranInputError = true;

                            var messageLogic = new MessageBoxShowLogic();
                            messageLogic.MessageBoxShow(messageId, message);

                            this.IsInputErrorOccured = true;
                        }
                        break;
                    }
                default:
                    break;
            }

            // 入金区分
            if (this.NYUUKIN_Ichiran.Columns[e.ColumnIndex].Name.Contains("NYUUSHUKKIN_KBN_CD"))
            {
                string cellNameKbn = this.NYUUKIN_Ichiran.Columns[e.ColumnIndex].Name;
                string val = Convert.ToString(this.NYUUKIN_Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (this.beforeNyuukinIchiranDictionary.ContainsKey(cellNameKbn) &&
                    !this.beforeNyuukinIchiranDictionary[cellNameKbn].Equals(val))
                {
                    string cellNameKbnNm = "NYUUSHUKKIN_KBN_NAME_RYAKU" + cellNameKbn.Replace("NYUUSHUKKIN_KBN_CD", "");
                    bool result = this.logic.CheckAndSetNyuukinKbn(false, e.RowIndex, cellNameKbn, cellNameKbnNm);
                    if (!result) e.Cancel = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一覧のバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKIN_Ichiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var columnName = this.NYUUKIN_Ichiran.Columns[e.ColumnIndex].Name;

            if (columnName.StartsWith("NYUUSHUKKIN_KBN_CD_"))
            {
                var count = columnName.Replace("NYUUSHUKKIN_KBN_CD_", "");
                if (true == this.logic.IsNullOrEmpty(this.NYUUKIN_Ichiran.CurrentRow.Cells[columnName].Value))
                {
                    this.NYUUKIN_Ichiran.CurrentRow.Cells["NYUUKIN_KINGAKU_" + count.ToString()].Value = String.Empty;
                }
            }
            else if (columnName == "TORIHIKISAKI_CD")
            {
                bool TorihikisakiKeshiKomi = false;
                if (this.NYUUKIN_Ichiran.CurrentCell.Value != null)
                {
                    // 取引先CD入力有り、かつ、取引先CDが変更された場合
                    if (this.beforeTorihikisakiCd != this.NYUUKIN_Ichiran.CurrentCell.Value.ToString())
                    {
                        TorihikisakiKeshiKomi = JudgeTorihikisakiKeshikomi(this.beforeTorihikisakiCd);
                    }
                }
                else
                {
                    // 取引先CD入力無し
                    TorihikisakiKeshiKomi = JudgeTorihikisakiKeshikomi(this.beforeTorihikisakiCd);
                }
                if (TorihikisakiKeshiKomi)
                {
                    // 入金消込データがある場合は、取引先変更時にアラート表示
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E233", "取引先");
                    this.NYUUKIN_Ichiran.CurrentCell.Value = this.beforeTorihikisakiCd;
                    this.NYUUKIN_Ichiran.CurrentRow.Cells["TORIHIKISAKI_NAME_RYAKU"].Value = this.beforeTorihikisakiName;
                    return;
                }

                // 請求日付と請求金額の設定
                this.SetSeikyuuDateAndSeikyuuKingaku(this.NYUUKIN_Ichiran.CurrentRow);
            }

            // イベントが発生したセルにかかわらず処理
            this.logic.CalcAll();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 該当の取引先について入金消込があるか否か判定
        /// </summary>
        /// <param name="TorihikisakiCD">取引先CD</param>
        /// <returns>true：消込あり、false：消込なし</returns>
        public bool JudgeTorihikisakiKeshikomi(string TorihikisakiCD)
        {
            bool retVal = false;
            if (this.logic.dto.NyuukinKeshikomiList.Count > 0)
            {
                foreach (T_NYUUKIN_KESHIKOMI row in this.logic.dto.NyuukinKeshikomiList)
                {
                    if (row.TORIHIKISAKI_CD == TorihikisakiCD)
                    {
                        retVal = true;
                        break;
                    }
                }
            }            
            return retVal;
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 入金一覧のデータグリッドを作成します
        /// </summary>
        internal void CreateNyuukinIchiran()
        {
            LogUtility.DebugMethodStart();

            var count = this.logic.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value;

            for (var i = 2; i <= count; i++)
            {
                var kbnCdColumn = (DgvCustomNumericTextBox2Column)this.NYUUKIN_Ichiran.Columns["NYUUSHUKKIN_KBN_CD_1"].Clone();
                var kbnNameRyakuColumn = (DgvCustomTextBoxColumn)this.NYUUKIN_Ichiran.Columns["NYUUSHUKKIN_KBN_NAME_RYAKU_1"].Clone();
                var kingakuColumn = (DgvCustomNumericTextBox2Column)this.NYUUKIN_Ichiran.Columns["NYUUKIN_KINGAKU_1"].Clone();
                var bikouColumn = (DgvCustomTextBoxColumn)this.NYUUKIN_Ichiran.Columns["BIKOU_1"].Clone();

                kbnCdColumn.Name = kbnCdColumn.Name.Replace("_1", "_" + i.ToString());
                kbnCdColumn.SetFormField = kbnCdColumn.SetFormField.Replace("_1", "_" + i.ToString());
                kbnCdColumn.HeaderText = kbnCdColumn.HeaderText.Replace("1", i.ToString());

                kbnNameRyakuColumn.Name = kbnNameRyakuColumn.Name.Replace("_1", "_" + i.ToString());
                kbnNameRyakuColumn.HeaderText = kbnNameRyakuColumn.HeaderText.Replace("1", i.ToString());

                kingakuColumn.Name = kingakuColumn.Name.Replace("_1", "_" + i.ToString());
                kingakuColumn.HeaderText = kingakuColumn.HeaderText.Replace("1", i.ToString());

                bikouColumn.Name = bikouColumn.Name.Replace("_1", "_" + i.ToString());
                bikouColumn.HeaderText = bikouColumn.HeaderText.Replace("1", i.ToString());

                this.NYUUKIN_Ichiran.Columns.Add(kbnCdColumn);
                this.NYUUKIN_Ichiran.Columns.Add(kbnNameRyakuColumn);
                this.NYUUKIN_Ichiran.Columns.Add(kingakuColumn);
                this.NYUUKIN_Ichiran.Columns.Add(bikouColumn);
            }

            // 背景色を更新
            foreach (DataGridViewRow row in this.NYUUKIN_Ichiran.Rows)
            {
                for (int i = 0; i < this.NYUUKIN_Ichiran.Columns.Count; i++)
                {
                    var cell = row.Cells[i] as DgvCustomTextBoxCell;
                    if (null != cell)
                    {
                        cell.UpdateBackColor();
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面のコントロールを初期化します
        /// </summary>
        /// <param name="isClearDenpyouDate">伝票日付をクリアするかどうかのフラグ</param>
        internal void ClearFormData(bool isClearDenpyouDate)
        {
            LogUtility.DebugMethodStart();

            // 画面上のデータをクリア
            this.NYUUKIN_SUM_DETAIL_Ichiran.AllowUserToAddRows = false;
            this.NYUUKIN_Ichiran.AllowUserToAddRows = false;

            // 修正モード登録時などに削除済みデータがあるとチェックイベントが走ってしまうのでイベントを一旦削除してからクリアする
            this.NYUUKIN_SUM_DETAIL_Ichiran.CellValidating -= new DataGridViewCellValidatingEventHandler(this.NYUUKIN_SUM_DETAIL_Ichiran_CellValidating);
            this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Clear();
            this.NYUUKIN_SUM_DETAIL_Ichiran.CellValidating += new DataGridViewCellValidatingEventHandler(this.NYUUKIN_SUM_DETAIL_Ichiran_CellValidating);
            this.NYUUKIN_Ichiran.CellValidating -= new DataGridViewCellValidatingEventHandler(this.NYUUKIN_Ichiran_CellValidating);
            this.NYUUKIN_Ichiran.Rows.Clear();
            this.NYUUKIN_Ichiran.CellValidating += new DataGridViewCellValidatingEventHandler(this.NYUUKIN_Ichiran_CellValidating);

            this.logic.SetKyoten();

            if (false == this.logic.GetNyuukinNumber().IsNull)
            {
                this.NYUUKIN_NUMBER.Text = this.logic.GetNyuukinNumber().Value.ToString();
            }
            else
            {
                this.NYUUKIN_NUMBER.Text = String.Empty;
            }
            if (isClearDenpyouDate)
            {
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //this.DENPYOU_DATE.Value = DateTime.Today;
                this.DENPYOU_DATE.Value = this.logic.parentForm.sysDate;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
            }
            this.NYUUKINSAKI_CD.Text = String.Empty;
            this.NYUUKINSAKI_NAME_RYAKU.Text = String.Empty;

            this.logic.SetCorpBank();

            this.KOUZA_SHURUI.Text = String.Empty;
            this.KOUZA_NO.Text = String.Empty;
            this.KOUZA_NAME.Text = String.Empty;
            this.NYUUKIN_AMOUNT_TOTAL.Text = "0";
            this.CHOUSEI_AMOUNT_TOTAL.Text = "0";
            this.KARIUKEKIN_WARIATE_TOTAL.Text = "0";
            this.TOTAL.Text = "0";
            this.WARIFURIGAKU.Text = "0";
            this.KARIUKEKIN_JUUTOU.Text = "0";
            this.KARIUKEKIN.Text = "0";
            this.KARIUKEKIN_TOTAL.Text = "0";

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面上のコントロールの状態を変更します
        /// </summary>
        internal void ChangeControlState()
        {
            LogUtility.DebugMethodStart();

            var readOnly = false;
            switch (this.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    readOnly = false;
                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                default:
                    readOnly = true;
                    break;
            }

            this.headerForm.txtKyotenCd.ReadOnly = readOnly;
            this.DENPYOU_DATE.ReadOnly = readOnly;
            this.NYUUKINSAKI_CD.ReadOnly = readOnly;
            this.NYUUKINSAKI_POPUP.Enabled = !readOnly;
            this.BANK_CD.ReadOnly = readOnly;
            this.BANK_SHITEN_CD.ReadOnly = readOnly;
            foreach (DataGridViewRow row in this.NYUUKIN_SUM_DETAIL_Ichiran.Rows)
            {
                row.ReadOnly = readOnly;
                row.Cells.OfType<DgvCustomTextBoxCell>().ToList().ForEach(c => c.UpdateBackColor());
            }
            foreach (DataGridViewRow row in this.NYUUKIN_Ichiran.Rows)
            {
                row.ReadOnly = readOnly;
                row.Cells.OfType<DgvCustomTextBoxCell>().ToList().ForEach(c => c.UpdateBackColor());
            }

            if (false == readOnly)
            {
                this.NYUUKIN_SUM_DETAIL_Ichiran.AllowUserToAddRows = true;
                if (this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow).Count() >= this.logic.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
                {
                    this.NYUUKIN_SUM_DETAIL_Ichiran.AllowUserToAddRows = false;
                }
                this.NYUUKIN_Ichiran.AllowUserToAddRows = true;
            }

            // 先頭の列までスクロールする
            this.NYUUKIN_Ichiran.FirstDisplayedScrollingColumnIndex = this.NYUUKIN_Ichiran.Columns["DELETE_FLG"].Index;
            this.NYUUKIN_SUM_DETAIL_Ichiran.FirstDisplayedScrollingColumnIndex = this.NYUUKIN_SUM_DETAIL_Ichiran.Columns["NYUUSHUKKIN_KBN_CD"].Index;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンの活性状態を変更します
        /// </summary>
        internal void ChangeButtonState()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.Parent;

            parentForm.bt_func2.Enabled = true;
            parentForm.bt_func3.Enabled = true;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func7.Enabled = true;
            parentForm.bt_func9.Enabled = true;
            parentForm.bt_func10.Enabled = true;
            parentForm.bt_func11.Enabled = true;
            parentForm.bt_func12.Enabled = true;
            parentForm.bt_process1.Enabled = true;
            parentForm.bt_process3.Enabled = true;

            switch (this.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    break;
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    parentForm.bt_func10.Enabled = false;
                    parentForm.bt_func11.Enabled = false;
                    parentForm.bt_process1.Enabled = false;
                    parentForm.bt_process3.Enabled = false;
                    break;
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    parentForm.bt_func3.Enabled = false;
                    parentForm.bt_func9.Enabled = false;
                    parentForm.bt_func10.Enabled = false;
                    parentForm.bt_func11.Enabled = false;
                    parentForm.bt_process1.Enabled = false;
                    parentForm.bt_process3.Enabled = false;
                    break;
                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行・銀行支店のバリデートを設定します
        /// </summary>
        internal bool SetBankCheck()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 「2:振込」の行が存在する場合は、「銀行CD」「銀行支店CD」の入力を必須とする
                var count = this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Cast<DataGridViewRow>()
                                                                .Where(r => false == r.IsNewRow && ConstInfo.NYUUKIN_KBN_FURIKOMI == this.logic.ConvertToString(r.Cells["NYUUSHUKKIN_KBN_CD"].Value))
                                                                .Count();

                var bank = this.BANK_CD.RegistCheckMethod.Where(r => r.CheckMethodName == "必須チェック").FirstOrDefault();
                var bankShiten = this.BANK_SHITEN_CD.RegistCheckMethod.Where(r => r.CheckMethodName == "必須チェック").FirstOrDefault();


                if (0 < count)
                {
                    if (null == bank)
                    {
                        this.BANK_CD.RegistCheckMethod.Add(this.bankCdRegistCheckMethod);
                    }
                    if (null == bankShiten)
                    {
                        this.BANK_SHITEN_CD.RegistCheckMethod.Add(this.bankShitenCdRegistCheckMethod);
                    }
                }
                else
                {
                    if (null != bank)
                    {
                        this.bankCdRegistCheckMethod = bank;
                        this.BANK_CD.RegistCheckMethod.Clear();
                    }
                    if (null != bankShiten)
                    {
                        this.bankShitenCdRegistCheckMethod = bankShiten;
                        this.BANK_SHITEN_CD.RegistCheckMethod.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetBankCheck", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 取得した入金伝票を画面にセットします
        /// </summary>
        internal void SetNyuukinData()
        {
            LogUtility.DebugMethodStart();

            this.ClearFormData(true);
            this.logic.GetNyuukinData(this.logic.GetNyuukinNumber(), this.logic.GetSeq());
            if (null == this.logic.dto.NyuukinSumEntry)
            {
                // 該当データなし
                var messageLogic = new MessageBoxShowLogic();
                messageLogic.MessageBoxShow("E045");

                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                this.logic.SetNyuukinNumber(SqlInt64.Null);
                this.logic.WindowInit(true);
            }
            else
            {
                // 画面にデータセット
                this.headerForm.txtKyotenCd.Text = this.logic.dto.NyuukinSumEntry.KYOTEN_CD.Value.ToString("00");
                var kyoten = this.logic.GetKyoten(this.logic.dto.NyuukinSumEntry.KYOTEN_CD.Value.ToString());
                if (null != kyoten)
                {
                    this.headerForm.txtKyotenName.Text = kyoten.KYOTEN_NAME_RYAKU;
                }
                this.NYUUKIN_NUMBER.Text = this.logic.dto.NyuukinSumEntry.NYUUKIN_NUMBER.Value.ToString();
                this.DENPYOU_DATE.Value = this.logic.dto.NyuukinSumEntry.DENPYOU_DATE;
                this.NYUUKINSAKI_CD.Text = this.logic.dto.NyuukinSumEntry.NYUUKINSAKI_CD;
                var nyuukinsaki = this.logic.GetNyuukinsaki(this.logic.dto.NyuukinSumEntry.NYUUKINSAKI_CD);
                if (null != nyuukinsaki)
                {
                    this.NYUUKINSAKI_NAME_RYAKU.Text = nyuukinsaki.NYUUKINSAKI_NAME_RYAKU;
                }
                this.BANK_CD.Text = this.logic.dto.NyuukinSumEntry.BANK_CD;
                var bank = this.logic.GetBank(this.logic.dto.NyuukinSumEntry.BANK_CD);
                if (null != bank)
                {
                    this.BANK_NAME_RYAKU.Text = bank.BANK_NAME_RYAKU;
                }
                else
                {
                    this.BANK_NAME_RYAKU.Text = String.Empty;
                }
                this.BANK_SHITEN_CD.Text = this.logic.dto.NyuukinSumEntry.BANK_SHITEN_CD;
                var bankShiten = this.logic.GetBankShiten(this.logic.dto.NyuukinSumEntry.BANK_CD, this.logic.dto.NyuukinSumEntry.BANK_SHITEN_CD, this.logic.dto.NyuukinSumEntry.KOUZA_NO);
                if (null != bankShiten)
                {
                    this.BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                }
                else
                {
                    this.BANK_SHIETN_NAME_RYAKU.Text = String.Empty;
                }
                this.KOUZA_SHURUI.Text = this.logic.dto.NyuukinSumEntry.KOUZA_SHURUI;
                this.KOUZA_NO.Text = this.logic.dto.NyuukinSumEntry.KOUZA_NO;
                this.KOUZA_NAME.Text = this.logic.dto.NyuukinSumEntry.KOUZA_NAME;
                bool catchErr = true;
                this.NYUUKIN_AMOUNT_TOTAL.Text = this.logic.FormatKingaku(this.logic.dto.NyuukinSumEntry.NYUUKIN_AMOUNT_TOTAL.Value, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                this.CHOUSEI_AMOUNT_TOTAL.Text = this.logic.FormatKingaku(this.logic.dto.NyuukinSumEntry.CHOUSEI_AMOUNT_TOTAL.Value, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (!this.logic.dto.NyuukinSumEntry.KARIUKEKIN_WARIATE_TOTAL.IsNull)
                {
                    this.KARIUKEKIN_WARIATE_TOTAL.Text = this.logic.FormatKingaku(this.logic.dto.NyuukinSumEntry.KARIUKEKIN_WARIATE_TOTAL.Value, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                }

                if (this.logic.dto.NyuukinSumDetailList.Count > 0)
                {
                    this.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Add(this.logic.dto.NyuukinSumDetailList.Count);
                    var index = 0;
                    foreach (var entity in this.logic.dto.NyuukinSumDetailList)
                    {
                        var row = this.NYUUKIN_SUM_DETAIL_Ichiran.Rows[index];
                        row.Cells["NYUUSHUKKIN_KBN_CD"].Value = entity.NYUUSHUKKIN_KBN_CD.Value;
                        var nyuushukkinKbn = this.logic.GetNyuushukkinKbn(entity.NYUUSHUKKIN_KBN_CD.Value);
                        if (null != nyuushukkinKbn)
                        {
                            row.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = nyuushukkinKbn.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                        if (false == entity.KINGAKU.IsNull)
                        {
                            row.Cells["KINGAKU"].Value = entity.KINGAKU.Value;
                        }
                        row.Cells["MEISAI_BIKOU"].Value = entity.MEISAI_BIKOU;

                        index++;
                    }
                }

                if (this.logic.dto.NyuukinEntryList.Where(e => null != e.TORIHIKISAKI_CD && !String.IsNullOrEmpty(e.TORIHIKISAKI_CD)).Count() > 0)
                {
                    this.NYUUKIN_Ichiran.Rows.Add(this.logic.dto.NyuukinEntryList.Count);
                    var index = 0;
                    foreach (var entity in this.logic.dto.NyuukinEntryList)
                    {
                        var row = this.NYUUKIN_Ichiran.Rows[index];
                        row.Cells["SYSTEM_ID"].Value = entity.SYSTEM_ID.Value;
                        row.Cells["DELETE_FLG"].Value = false;
                        row.Cells["TORIHIKISAKI_CD"].Value = entity.TORIHIKISAKI_CD;
                        var torihikisaki = this.logic.GetTorihikisakiAll(entity.TORIHIKISAKI_CD, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (null != torihikisaki)
                        {
                            row.Cells["TORIHIKISAKI_NAME_RYAKU"].Value = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }

                        var count = 1;
                        foreach (var detailEntity in this.logic.dto.NyuukinDetailList.Where(e => e.SYSTEM_ID.Value == entity.SYSTEM_ID.Value && false == e.NYUUSHUKKIN_KBN_CD.IsNull))
                        {
                            if (this.NYUUKIN_Ichiran.Columns.Contains("NYUUSHUKKIN_KBN_CD_" + count.ToString()))
                            {

                                row.Cells["NYUUSHUKKIN_KBN_CD_" + count.ToString()].Value = detailEntity.NYUUSHUKKIN_KBN_CD.Value;
                                var nyuushukkinKbn = this.logic.GetNyuushukkinKbn(detailEntity.NYUUSHUKKIN_KBN_CD.Value);
                                if (null != nyuushukkinKbn)
                                {
                                    row.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU_" + count.ToString()].Value = nyuushukkinKbn.NYUUSHUKKIN_KBN_NAME_RYAKU;
                                }
                                row.Cells["NYUUKIN_KINGAKU_" + count.ToString()].Value = detailEntity.KINGAKU.Value;
                                row.Cells["BIKOU_" + count.ToString()].Value = detailEntity.MEISAI_BIKOU;

                                count++;
                            }
                        }

                        index++;
                    }
                }

                var kariukekin = 0m;
                if (null != this.logic.dto.KariukeControl && !this.logic.dto.KariukeControl.KARIUKE_TOTAL_KINGAKU.IsNull)
                {
                    kariukekin = this.logic.dto.KariukeControl.KARIUKE_TOTAL_KINGAKU.Value;
                }
                if (null != this.logic.dto.KariukeChousei)
                {
                    kariukekin = kariukekin - this.logic.dto.KariukeChousei.KINGAKU.Value;
                }
                this.KARIUKEKIN.Text = this.logic.FormatKingaku(kariukekin, out catchErr);
                if (!catchErr)
                {
                    return;
                }

                this.logic.CalcAll();
            }

            LogUtility.DebugMethodEnd();
        }

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
        /// 修正モードで画面を表示する
        /// </summary>
        /// <param name="nyuukinNumber">入金番号</param>
        private bool OpenUpdateWindow(string nyuukinNumber)
        {
            this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            this.logic.SetNyuukinNumber(this.logic.ConvertToSqlInt64(nyuukinNumber));
            if (!this.logic.WindowInit(true))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 入金一覧の全ての行に請求日付と請求金額を設定します
        /// </summary>
        internal void SetSeikyuuDateAndSeikyuuKingaku()
        {
            LogUtility.DebugMethodStart();

            foreach (DataGridViewRow row in this.NYUUKIN_Ichiran.Rows)
            {
                this.SetSeikyuuDateAndSeikyuuKingaku(row);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金一覧の指定された行に請求日付と請求金額を設定します
        /// </summary>
        /// <param name="row">対象の行</param>
        private bool SetSeikyuuDateAndSeikyuuKingaku(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(row);

                var seikyuuDate = String.Empty;
                var seikyuuKingaku = 0m;

                if (null != row.Cells["TORIHIKISAKI_CD"].Value)
                {
                    var torihikisakiCd = row.Cells["TORIHIKISAKI_CD"].Value.ToString();
                    var denpyouDate = (DateTime)this.DENPYOU_DATE.Value;

                    var seikyuuDenpyou = this.logic.GetLastSeikyuuDenpyouByTorihikisakiCdAndNyuukinYoteiBi(torihikisakiCd, denpyouDate);
                    if (null != seikyuuDenpyou && seikyuuDenpyou.Rows.Count > 0)
                    {
                        // 請求データがあれば、そのデータを利用
                        seikyuuDate = seikyuuDenpyou.Rows[0]["SEIKYUU_DATE"].ToString();

                        foreach (DataRow dr in seikyuuDenpyou.Rows)
                        {
                            if (!string.IsNullOrEmpty(dr["KONKAI_URIAGE_GAKU"].ToString()))
                            {
                                seikyuuKingaku += Convert.ToDecimal(dr["KONKAI_URIAGE_GAKU"].ToString());
                            }

                            if (!string.IsNullOrEmpty(dr["KONKAI_SEI_UTIZEI_GAKU"].ToString()))
                            {
                                seikyuuKingaku += Convert.ToDecimal(dr["KONKAI_SEI_UTIZEI_GAKU"].ToString());
                            }

                            if (!string.IsNullOrEmpty(dr["KONKAI_SEI_SOTOZEI_GAKU"].ToString()))
                            {
                                seikyuuKingaku += Convert.ToDecimal(dr["KONKAI_SEI_SOTOZEI_GAKU"].ToString());
                            }

                            if (!string.IsNullOrEmpty(dr["KONKAI_DEN_UTIZEI_GAKU"].ToString()))
                            {
                                seikyuuKingaku += Convert.ToDecimal(dr["KONKAI_DEN_UTIZEI_GAKU"].ToString());
                            }

                            if (!string.IsNullOrEmpty(dr["KONKAI_DEN_SOTOZEI_GAKU"].ToString()))
                            {
                                seikyuuKingaku += Convert.ToDecimal(dr["KONKAI_DEN_SOTOZEI_GAKU"].ToString());
                            }

                            if (!string.IsNullOrEmpty(dr["KONKAI_MEI_UTIZEI_GAKU"].ToString()))
                            {
                                seikyuuKingaku += Convert.ToDecimal(dr["KONKAI_MEI_UTIZEI_GAKU"].ToString());
                            }

                            if (!string.IsNullOrEmpty(dr["KONKAI_MEI_SOTOZEI_GAKU"].ToString()))
                            {
                                seikyuuKingaku += Convert.ToDecimal(dr["KONKAI_MEI_SOTOZEI_GAKU"].ToString());
                            }
                        }
                    }
                    else
                    {
                        // 請求データがなければ、マスタの開始売掛残高を利用
                        var torihikisakiSeikyuu = this.logic.GetTorihikisakiSeikyuuByTorihikisakicd(torihikisakiCd);
                        if (null != torihikisakiSeikyuu)
                        {
                            seikyuuDate = "開始残高";
                            seikyuuKingaku = torihikisakiSeikyuu.KAISHI_URIKAKE_ZANDAKA.Value;
                        }
                    }
                }

                row.Cells["SEIKYUU_DATE"].Value = seikyuuDate;
                row.Cells["SEIKYUU_KINGAKU"].Value = seikyuuKingaku;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetSeikyuuDateAndSeikyuuKingaku", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSeikyuuDateAndSeikyuuKingaku", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 画面モードによって入金番号のReadOnlyを切り替えます
        /// </summary>
        internal void NyuukinNumberReadOnlySwitch()
        {
            switch (this.WindowType)
            {
                // 新規モードの時は入金番号を入力可の状態にする
                case (WINDOW_TYPE.NEW_WINDOW_FLAG):
                    this.NYUUKIN_NUMBER.ReadOnly = false;
                    this.NYUUKIN_NUMBER.TabStop = true;
                    break;

                // 修正・削除・参照モードの時は読み取り専用とする
                case (WINDOW_TYPE.UPDATE_WINDOW_FLAG):
                case (WINDOW_TYPE.DELETE_WINDOW_FLAG):
                case (WINDOW_TYPE.REFERENCE_WINDOW_FLAG):
                    this.NYUUKIN_NUMBER.ReadOnly = true;
                    this.NYUUKIN_NUMBER.TabStop = false;
                    break;
            }

        }

        /// <summary>
        /// 入金先ポップアップを閉じた後に処理します
        /// </summary>
        public void NyuukinsakiPopupBefore()
        {
            LogUtility.DebugMethodStart();

            this.beforeNyuukinsakiCd = this.NYUUKINSAKI_CD.Text;
            this.beforeNyuukinsakiName = this.NYUUKINSAKI_NAME_RYAKU.Text;

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 入金先ポップアップを閉じた後に処理します
        /// </summary>
        public void NyuukinsakiPopupAfter()
        {
            LogUtility.DebugMethodStart();

            NYUUKINSAKI_CD_Validated(0, new EventArgs());

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
            this.NYUUKIN_Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            this.NYUUKIN_SUM_DETAIL_Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;// | AnchorStyles.Bottom;
        }

        //thongh 2015/08/07 #12106 start
        private void NYUUKIN_Ichiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.CalcWarifuri();
        }

        private void NYUUKIN_Ichiran_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.CalcWarifuri();
        }

        /// <summary>
        /// 今回割振額を計算します
        /// </summary>
        internal bool CalcWarifuri()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var warifurigaku = 0m;

                var targetRow = this.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow && this.logic.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].EditedFormattedValue).IsFalse);
                foreach (DataGridViewRow row in targetRow)
                {
                    for (int count = 1; count <= this.logic.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value; count++)
                    {
                        warifurigaku += this.logic.ConvertToDecimal(row.Cells["NYUUKIN_KINGAKU_" + count.ToString()].Value);
                    }
                }

                bool catchErr = true;
                this.WARIFURIGAKU.Text = this.logic.FormatKingaku(warifurigaku, out catchErr);
                if (!catchErr)
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcWarifuri", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        //thongh 2015/08/07 #12106 end
    }
}
