using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Const;

namespace Shougun.Core.Common.ContenaShitei
{
    /// <summary>
    ///コンテナ指定
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal LogicClass logic;

        /// <summary>
        /// メッセージクラス</summary>
        /// </summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }

        /// <summary>
        /// 返却コンテナ稼動実績リスト
        /// </summary>
        public List<T_CONTENA_RESULT> RetCntRetList { get; set; }

        /// <summary>
        /// 返却コンテナ稼動予定リスト
        /// </summary>
        public List<T_CONTENA_RESERVE> RetCntResList { get; set; }

        /// <summary>
        /// 前回値チェック用変数(設置Detial用)
        /// </summary>
        internal Dictionary<string, string> beforeValuesForDetailSechi = new Dictionary<string, string>();

        /// <summary>
        /// 前回値チェック用変数(引揚Detial用)
        /// </summary>
        internal Dictionary<string, string> beforeValuesForDetailHikiage = new Dictionary<string, string>();

        /// <summary>
        /// 設置合計
        /// </summary>
        public int SeichiGoukei = 0;

        /// <summary>
        /// 引揚合計
        /// </summary>
        public int HikiageGoukei = 0;

        /// <summary>
        /// ボッタンフラグ
        /// </summary>
        public bool buttonFlag = false;

        /// <summary>
        /// グリッドカラム
        /// </summary>
        public enum enumCols
        {
            /// <summary>コンテナ種類CD</summary>
            ContenaShuruiCd = 0,
            /// <summary>コンテナ種類名</summary>
            ContenaShuruiNa,
            /// <summary>コンテナCD</summary>
            ContenaCd,
            /// <summary>コンテナ名</summary>
            ContenaNa,
            /// <summary>台数</summary>
            Count,
            /// <summary>削除フラグ</summary>
            Delete,
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BasePopForm ParentBaseForm { get; private set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// UIForm（（1:受入、売上支払、2:出荷）
        /// </summary>
        /// <param name="paramIn_ShoriMode">処理モード</param>
        /// <param name="paramIn_Yobidashi">呼び出し画面</param>
        /// <param name="paramIn_CntRetList">コンテナ稼動実績リスト</param>
        public UIForm(string paramIn_ShoriMode, string paramIn_Yobidashi, List<T_CONTENA_RESULT> paramIn_CntRetList, string denpyouDate, string gyoushaCd, string genbaCd)
            : base(WINDOW_ID.C_CONTENA_SHITEI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            if (paramIn_CntRetList != null && paramIn_CntRetList.Count > 0)
            {
                logic.denshuKB = paramIn_CntRetList[0].DENSHU_KBN_CD.ToString();        //伝種区分
                logic.systemID = paramIn_CntRetList[0].SYSTEM_ID.ToString();            //システムID
                logic.seq = paramIn_CntRetList[0].SEQ.ToString();                       //枝番
            }
            logic.ShoriMode = paramIn_ShoriMode;                                        //処理モード
            logic.Yobidashi = paramIn_Yobidashi;                                        //呼出画面
            logic.CntRetList = paramIn_CntRetList;                                      //コンテナ稼動実績リスト
            this.logic.denpyouDate = denpyouDate;                                       //伝票日付(または作業日)
            this.logic.GyoushaCd = gyoushaCd;                                           //起動元画面の業者CD
            this.logic.GenbaCd = genbaCd;                                               //起動元画面の現場CD

            messageShowLogic = new MessageBoxShowLogic();
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// UIForm（3:受付）
        /// </summary>
        /// <param name="paramIn_ShoriMode">処理モード</param>
        /// <param name="paramIn_Yobidashi">呼び出し画面</param>
        /// <param name="paramIn_CntResList">コンテナ稼動実績リスト</param>
        public UIForm(string paramIn_ShoriMode, string paramIn_Yobidashi, List<T_CONTENA_RESERVE> paramIn_CntResList, string denpyouDate, string gyoushaCd, string genbaCd)
            : base(WINDOW_ID.C_CONTENA_SHITEI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            if (paramIn_CntResList != null && paramIn_CntResList.Count > 0)
            {
                logic.systemID = paramIn_CntResList[0].SYSTEM_ID.ToString();            //システムID
                logic.seq = paramIn_CntResList[0].SEQ.ToString();                       //枝番
            }
            logic.ShoriMode = paramIn_ShoriMode;                                        //処理モード
            logic.Yobidashi = paramIn_Yobidashi;                                        //呼出画面
            logic.CntResList = paramIn_CntResList;                                      //コンテナ稼動予定リスト
            this.logic.denpyouDate = denpyouDate;                                       //伝票日付(または作業日)
            this.logic.GyoushaCd = gyoushaCd;                                           //起動元画面の業者CD
            this.logic.GenbaCd = genbaCd;                                               //起動元画面の現場CD
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
            base.OnLoad(e);
            //初期化
            this.logic.WindowInit();

            //処理モードを設定する。
            this.logic.SetMode();

            //画面の設置と引揚GridViewをクリアする。
            this.logic.ClearScreen();

            //設置と引揚GridViewのデータの読み込み
            if (this.logic.Yobidashi.Equals("1")
                || this.logic.Yobidashi.Equals("2"))
            {
                //1:受入、売上支払、2:出荷の場合
                this.logic.SearchContenaResult();
            }
            else if (this.logic.Yobidashi.Equals("3"))
            {
                //3:受付の場合
                this.logic.SearchContenaReserve();
            }

            //台数セル制御
            foreach (DataGridViewRow row in this.customDataGridViewSechi.Rows)
            {
                this.logic.DaisuuCntCellCtrl(row, true);
            }

            foreach (DataGridViewRow row in this.customDataGridViewHikiage.Rows)
            {
                this.logic.DaisuuCntCellCtrl(row, false);
            }

            //F9ボタンの使用可か不可か
            this.logic.F9SiyouKaFuka();

        }
        #endregion

        #region 画面フッター部コントロールイベント

        /// <summary>
        /// 実行処理(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            //（コンテナ種類CD+コンテナCD）重複チェック
            bool okFlg = false;
            //設置,引揚
            okFlg = this.logic.ContenaShuruiCDJuufukuChk();

            if (okFlg)
            {
                msgLogic.MessageBoxShow("E031", "【コンテナ種類CDとコンテナCD】");

                LogUtility.DebugMethodEnd();
                return;
            }

            //メッセージ出すのため
            bool flg = false;

            for (int i = 0; i < this.customDataGridViewSechi.RowCount - 1; i++)
            {
                if ((null != this.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value && !string.IsNullOrEmpty(this.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value.ToString()))
                    && (null != this.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU"].Value && !string.IsNullOrEmpty(this.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU"].Value.ToString()))
                    && (null != this.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value && !string.IsNullOrEmpty(this.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value.ToString()))
                    )
                {
                    flg = true;
                    break;
                }

            }

            for (int i = 0; i < this.customDataGridViewHikiage.RowCount - 1; i++)
            {
                if ((null != this.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value && !string.IsNullOrEmpty(this.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value.ToString()))
                    && (null != this.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE"].Value && !string.IsNullOrEmpty(this.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE"].Value.ToString()))
                    && (null != this.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value && !string.IsNullOrEmpty(this.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value.ToString()))
                    )
                {
                    flg = true;
                    break;
                }

            }
            //メッセージ
            if (flg)
            {
                msgLogic.MessageBoxShow("I014");
            }

            //実施
            this.logic.Jikou();
        }

        /// <summary>
        /// Formクローズ処理(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            bool flg = false;

            for (int i = 0; i < this.customDataGridViewSechi.RowCount - 1; i++)
            {
                if ((null != this.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value && !string.IsNullOrEmpty(this.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value.ToString()))
                    || (null != this.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value && !string.IsNullOrEmpty(this.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value.ToString()))
                    || (null != this.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value && !string.IsNullOrEmpty(this.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value.ToString()))
                    )
                {
                    flg = true;
                    break;
                }

            }

            for (int i = 0; i < this.customDataGridViewHikiage.RowCount - 1; i++)
            {
                if ((null != this.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value && !string.IsNullOrEmpty(this.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value.ToString()))
                    || (null != this.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value && !string.IsNullOrEmpty(this.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value.ToString()))
                    || (null != this.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value && !string.IsNullOrEmpty(this.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value.ToString()))
                    )
                {
                    flg = true;
                    break;
                }

            }

            if (flg)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (DialogResult.No == msgLogic.MessageBoxShow("C058"))
                {
                    // 終了しない
                    return;
                }
            }

            this.logic.FormClose();
        }

        /// <summary>
        /// 画面キーダウンイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    buttonFlag = true;
                    break;
                default:
                    buttonFlag = false;
                    break; ;
            }
            base.OnKeyDown(e);
        }


        ///// <summary>
        ///// Formクローズ処理(×)
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void parentForm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    this.logic.FormClose();
        //}

        #endregion

        #region 画面Detail部コントロールイベント

        #region 設置GridViewのCellValidatingイベント
        /// <summary>
        /// 設置GridViewのCellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridViewSechi_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (buttonFlag)
            {
                return;
            }
            DataGridViewCell currentcell = this.customDataGridViewSechi.CurrentCell;
            DataGridViewRow currentrow = this.customDataGridViewSechi.CurrentRow;
            var before = string.Empty;
            string cellname = this.customDataGridViewSechi.Columns[e.ColumnIndex].Name;

            if (cellname.Equals("CONTENA_SHURUI_CD") &&
                !DBNull.Value.Equals(this.customDataGridViewSechi[cellname, e.RowIndex].Value) &&
                this.customDataGridViewSechi[cellname, e.RowIndex].Value != null &&
                !"".Equals(this.customDataGridViewSechi[cellname, e.RowIndex].Value))
            {
                string contena_Shurui_Cd = (string)this.customDataGridViewSechi[cellname, e.RowIndex].Value;
                this.customDataGridViewSechi[cellname, e.RowIndex].Value = contena_Shurui_Cd.ToUpper();
            }

            string cellvalue = Convert.ToString(this.customDataGridViewSechi[cellname, e.RowIndex].Value);

            if (e.ColumnIndex == (int)enumCols.ContenaShuruiCd)
            {
                if (this.beforeValuesForDetailSechi.ContainsKey(cellname))
                {
                    before = this.beforeValuesForDetailSechi[cellname];
                }
                if (cellvalue == before && !this.logic.isInputError)
                {
                    return;
                }

                this.logic.CheckContenaShuruiCd(1, cellvalue, e);
            }
            else if (e.ColumnIndex == (int)enumCols.ContenaCd)
            {
                string contenaShuruiCd = Convert.ToString(currentrow.Cells[(int)enumCols.ContenaShuruiCd].Value);
                string contenaCd = Convert.ToString(currentrow.Cells[(int)enumCols.ContenaCd].Value);
                if (this.beforeValuesForDetailSechi.ContainsKey(cellname))
                {
                    before = this.beforeValuesForDetailSechi[cellname];
                }
                if (cellvalue == before && !this.logic.isInputError)
                {
                    return;
                }

                this.logic.CheckContenaCd(1, contenaShuruiCd, contenaCd, e);
            }
            if (e.Cancel)
            {
                return;
            }
            //削除フラグの設定処理
            this.logic.DeleteFlagSetei(this.customDataGridViewSechi.CurrentRow);

            //台数カラムのチェック処理
            if (!this.logic.DaishuCheck(currentcell))
            {
                this.messageShowLogic.MessageBoxShow("E042", "1～999");
                //currentcell.Value = "";
                e.Cancel = true;
            }

            //台数の累加チェック処理
            if (e.ColumnIndex == (int)enumCols.Count)
            {
                //台数の累加チェック処理
                if (!this.logic.DaishuRuikaCheckSechi())
                {
                    // this.messageShowLogic.MessageBoxShow("E042", "累加");
                    this.messageShowLogic.MessageBoxShow("E111");
                    //currentcell.Value = "";
                    e.Cancel = true;
                }
            }

            if (e.ColumnIndex == (int)enumCols.ContenaShuruiCd || e.ColumnIndex == (int)enumCols.ContenaCd)
            {
                //複数キーの一意制約チェック処理（コンテナ種類CD、コンテナCD）
                if (!this.logic.MulKeyTanitsuCheckSechi(currentrow, e.RowIndex))
                {
                    this.messageShowLogic.MessageBoxShow("E037", "コンテナ種類CDとコンテナCD");

                    e.Cancel = true;
                }
                else
                {
                    // 前回値と変更がある場合、台数をnullに設定する。
                    if (!beforeValuesForDetailSechi.ContainsKey(cellname)
                        || !beforeValuesForDetailSechi[cellname].Equals(cellvalue))
                    {
                        currentrow.Cells["DAISUU_CNT"].Value = null;
                    }
                }
            }

            //コンテナ種類チェック処理
            if (currentcell.ColumnIndex == (int)enumCols.ContenaShuruiCd)
            {
                if (this.logic.CheckContenaShuruiSechi(currentcell.Value, currentrow.Cells[2].Value, e))
                {
                    //this.messageShowLogic.MessageBoxShow("E062", "コンテナ種類");
                    //e.Cancel = true;
                }
            }

            //コンテナチェック処理
            if (currentcell.ColumnIndex == (int)enumCols.ContenaCd)
            {
                // コンテナCDが有でコンテナ種類CDが空の場合はエラー
                if (!string.IsNullOrEmpty(Convert.ToString(currentrow.Cells[(int)enumCols.ContenaCd].Value))
                    && string.IsNullOrEmpty(Convert.ToString(currentrow.Cells[(int)enumCols.ContenaShuruiCd].Value)))
                {
                    this.messageShowLogic.MessageBoxShow("E051", "コンテナ種類CD");
                    e.Cancel = true;
                    return;
                }

                if (!this.logic.CheckContenaSechi(currentrow.Cells[0].Value, currentcell.Value, e))
                {
                    this.messageShowLogic.MessageBoxShow("E062", "コンテナ種類");
                    e.Cancel = true;
                    return;
                }

            }
        }
        #endregion

        #region 設置GridViewのCellValidatedイベント
        /// <summary>
        /// 設置GridViewのCellValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridViewSechi_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentcell = this.customDataGridViewSechi.CurrentCell;
            DataGridViewRow currentrow = this.customDataGridViewSechi.CurrentRow;

            //台数カラムのチェック処理
            if (!this.logic.DaishuCheck(currentcell))
            {
                this.messageShowLogic.MessageBoxShow("E042", "1～999");
                //currentcell.Value = "";
                currentcell.Selected = true;
                this.customDataGridViewSechi.BeginEdit(false);
            }

            currentcell = this.customDataGridViewHikiage.CurrentCell;
            if (currentcell != null)
            {
                //台数カラムのチェック処理
                if (!this.logic.DaishuCheck(currentcell))
                {
                    this.messageShowLogic.MessageBoxShow("E042", "1～999");
                    //currentcell.Value = "";
                    currentcell.Selected = true;
                    this.customDataGridViewHikiage.BeginEdit(false);
                }
            }
            //台数の累加チェック処理
            if (!this.logic.DaishuRuikaCheckSechi())
            {
                // this.messageShowLogic.MessageBoxShow("E042", "累加");
                this.messageShowLogic.MessageBoxShow("E111");
                currentcell.Selected = true;
                this.customDataGridViewHikiage.BeginEdit(false);
            }

            //台数セル制御
            this.logic.DaisuuCntCellCtrl(currentrow, true);

            //F9使用可か不可か
            this.logic.F9SiyouKaFuka();

            ////複数キーの一意制約チェック処理（コンテナ種類CD、コンテナCD）
            //if (!this.logic.MulKeyTanitsuCheckSechi(currentrow, e.RowIndex))
            //{
            //    this.messageShowLogic.MessageBoxShow("E042", "一意");
            //    currentcell.Selected = true;
            //    this.customDataGridViewHikiage.BeginEdit(false);
            //}

        }
        #endregion

        #region 設置GridViewのMouseClickイベント
        /// <summary>
        /// 設置GridViewのMouseClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridViewSechi_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridViewCell currentcell = this.customDataGridViewSechi.CurrentCell;


            //台数カラムのチェック処理
            if (!this.logic.DaishuCheck(currentcell))
            {
                this.messageShowLogic.MessageBoxShow("E042", "1～999");
                //currentcell.Value = "";
                currentcell.Selected = true;
                this.customDataGridViewSechi.BeginEdit(false);
            }

            currentcell = this.customDataGridViewHikiage.CurrentCell;
            if (currentcell != null)
            {
                //台数カラムのチェック処理
                if (!this.logic.DaishuCheck(currentcell))
                {
                    this.messageShowLogic.MessageBoxShow("E042", "1～999");
                    //currentcell.Value = "";
                    currentcell.Selected = true;
                    this.customDataGridViewHikiage.BeginEdit(false);
                }
            }
        }
        #endregion

        #region 引揚GridViewのCellValidatingイベント
        /// <summary>
        /// 引揚GridViewのCellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridViewHikiage_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (buttonFlag)
            {
                return;
            }

            DataGridViewCell currentcell = this.customDataGridViewHikiage.CurrentCell;
            DataGridViewRow currentrow = this.customDataGridViewHikiage.CurrentRow;
            var before = string.Empty;
            string cellname = this.customDataGridViewHikiage.Columns[e.ColumnIndex].Name;

            if (cellname.Equals("CONTENA_SHURUI_CD_HIKIAGE") &&
                 !DBNull.Value.Equals(this.customDataGridViewHikiage[cellname, e.RowIndex].Value) &&
                 this.customDataGridViewHikiage[cellname, e.RowIndex].Value != null &&
                 !"".Equals(this.customDataGridViewHikiage[cellname, e.RowIndex].Value))
            {
                string contena_Shurui_Cd = (string)this.customDataGridViewHikiage[cellname, e.RowIndex].Value;
                this.customDataGridViewHikiage[cellname, e.RowIndex].Value = contena_Shurui_Cd.ToUpper();
            }

            string cellvalue = Convert.ToString(this.customDataGridViewHikiage[cellname, e.RowIndex].Value);

            if (e.ColumnIndex == (int)enumCols.ContenaShuruiCd)
            {
                if (this.beforeValuesForDetailHikiage.ContainsKey(cellname))
                {
                    before = this.beforeValuesForDetailHikiage[cellname];
                }
                if (cellvalue == before && !this.logic.isInputError)
                {
                    return;
                }

                this.logic.CheckContenaShuruiCd(2, cellvalue, e);
            }
            else if (e.ColumnIndex == (int)enumCols.ContenaCd)
            {
                string contenaShuruiCd = Convert.ToString(currentrow.Cells[(int)enumCols.ContenaShuruiCd].Value);
                string contenaCd = Convert.ToString(currentrow.Cells[(int)enumCols.ContenaCd].Value);
                if (this.beforeValuesForDetailHikiage.ContainsKey(cellname))
                {
                    before = this.beforeValuesForDetailHikiage[cellname];
                }
                if (cellvalue == before && !this.logic.isInputError)
                {
                    return;
                }
                this.logic.CheckContenaCd(2, contenaShuruiCd, contenaCd, e);
            }
            if (e.Cancel)
            {
                return;
            }

            //削除フラグの設定処理
            this.logic.DeleteFlagHikiage(this.customDataGridViewHikiage.CurrentRow);

            //台数カラムのチェック処理
            if (!this.logic.DaishuCheck(currentcell))
            {
                this.messageShowLogic.MessageBoxShow("E042", "1～999");
                //currentcell.Value = "";
                e.Cancel = true;
            }

            //台数の累加チェック
            if (e.ColumnIndex == (int)enumCols.Count)
            {
                //台数の累加チェック処理
                if (!this.logic.DaishuRuikaCheckHikiage())
                {
                    // this.messageShowLogic.MessageBoxShow("E042", "累加");
                    this.messageShowLogic.MessageBoxShow("E111");
                    //currentcell.Value = "";
                    e.Cancel = true;
                }
            }

            if (e.ColumnIndex == (int)enumCols.ContenaShuruiCd || e.ColumnIndex == (int)enumCols.ContenaCd)
            {
                //複数キーの一意制約チェック処理（コンテナ種類CD、コンテナCD）
                if (!this.logic.MulKeyTanitsuCheckHikiage(currentrow, e.RowIndex))
                {
                    this.messageShowLogic.MessageBoxShow("E037", "コンテナ種類CDとコンテナCD");
                    //currentcell.Value = "";
                    e.Cancel = true;
                }
                else
                {
                    // 前回値と変更がある場合、台数をnullに設定する。
                    if (!beforeValuesForDetailHikiage.ContainsKey(cellname)
                        || !beforeValuesForDetailHikiage[cellname].Equals(cellvalue))
                    {
                        currentrow.Cells["DAISUU_CNT_HIKIAGE"].Value = null;
                    }
                }
            }

            //コンテナ種類チェック処理
            if (currentcell.ColumnIndex == (int)enumCols.ContenaShuruiCd)
            {
                if (this.logic.CheckContenaShuruiHikiage(currentcell.Value, currentrow.Cells[2].Value, e))
                {
                    //this.messageShowLogic.MessageBoxShow("E062", "コンテナ種類");
                    //e.Cancel = true;
                }
            }

            //コンテナチェック処理
            if (currentcell.ColumnIndex == (int)enumCols.ContenaCd)
            {
                // コンテナCDが有でコンテナ種類CDが空の場合はエラー
                if (!string.IsNullOrEmpty(Convert.ToString(currentrow.Cells[(int)enumCols.ContenaCd].Value))
                    && string.IsNullOrEmpty(Convert.ToString(currentrow.Cells[(int)enumCols.ContenaShuruiCd].Value)))
                {
                    this.messageShowLogic.MessageBoxShow("E051", "コンテナ種類CD");
                    e.Cancel = true;
                    return;
                }

                if (!this.logic.CheckContenaHikiage(currentrow.Cells[0].Value, currentcell.Value, e))
                {
                    this.messageShowLogic.MessageBoxShow("E062", "コンテナ種類");
                    e.Cancel = true;
                    return;
                }
            }
        }
        #endregion

        #region 引揚GridViewのCellValidatedイベント
        /// <summary>
        /// 引揚GridViewのCellValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridViewHikiage_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentcell = this.customDataGridViewHikiage.CurrentCell;
            DataGridViewRow currentrow = this.customDataGridViewHikiage.CurrentRow;
            //台数カラムのチェック処理
            if (!this.logic.DaishuCheck(currentcell))
            {
                this.messageShowLogic.MessageBoxShow("E042", "1～999");
                //currentcell.Value = "";
                currentcell.Selected = true;
                this.customDataGridViewHikiage.BeginEdit(false);
            }

            currentcell = this.customDataGridViewSechi.CurrentCell;
            if (currentcell != null)
            {
                //台数カラムのチェック処理
                if (!this.logic.DaishuCheck(currentcell))
                {
                    this.messageShowLogic.MessageBoxShow("E042", "1～999");
                    //currentcell.Value = "";
                    currentcell.Selected = true;
                    this.customDataGridViewSechi.BeginEdit(false);
                }
            }

            //台数の累加チェック処理
            if (!this.logic.DaishuRuikaCheckHikiage())
            {
                // this.messageShowLogic.MessageBoxShow("E042", "累加");
                this.messageShowLogic.MessageBoxShow("E111");
                currentcell.Selected = true;
                this.customDataGridViewSechi.BeginEdit(false);
            }

            //台数セル制御
            this.logic.DaisuuCntCellCtrl(currentrow, false);

            //F9使用可か不可か
            this.logic.F9SiyouKaFuka();

            ////複数キーの一意制約チェック処理（コンテナ種類CD、コンテナCD）
            //if (!this.logic.MulKeyTanitsuCheckHikiage(currentrow, e.RowIndex))
            //{
            //    this.messageShowLogic.MessageBoxShow("E042", "一意");
            //    currentcell.Selected = true;
            //    this.customDataGridViewSechi.BeginEdit(false);
            //}
        }
        #endregion

        #region 引揚GridViewのMouseClickイベント
        /// <summary>
        /// 引揚GridViewのMouseClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridViewHikiage_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridViewCell currentcell = this.customDataGridViewHikiage.CurrentCell;

            //台数カラムのチェック処理
            if (!this.logic.DaishuCheck(currentcell))
            {
                this.messageShowLogic.MessageBoxShow("E042", "1～999");
                //currentcell.Value = "";
                currentcell.Selected = true;
                this.customDataGridViewHikiage.BeginEdit(false);
            }

            currentcell = this.customDataGridViewSechi.CurrentCell;
            if (currentcell != null)
            {
                //台数カラムのチェック処理
                if (!this.logic.DaishuCheck(currentcell))
                {
                    this.messageShowLogic.MessageBoxShow("E042", "1～999");
                    //currentcell.Value = "";
                    currentcell.Selected = true;
                    this.customDataGridViewSechi.BeginEdit(false);
                }
            }
        }
        #endregion

        #region 各CELLのフォーカス取得時処理(設置)
        /// <summary>
        /// 各CELLのフォーカス取得時処理(設置)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridViewSechi_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                DataGridViewRow row = this.customDataGridViewSechi.CurrentRow;

                if (row == null)
                {
                    return;
                }

                // 前回値チェック用データをセット
                String cellname = this.customDataGridViewSechi.Columns[e.ColumnIndex].Name;
                String cellvalue = Convert.ToString(this.customDataGridViewSechi[cellname, e.RowIndex].Value);
                if (beforeValuesForDetailSechi.ContainsKey(cellname))
                {
                    beforeValuesForDetailSechi[cellname] = cellvalue;
                }
                else
                {
                    beforeValuesForDetailSechi.Add(cellname, cellvalue);
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
                //LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 各CELLのフォーカス取得時処理(引揚)
        /// <summary>
        /// 各CELLのフォーカス取得時処理(引揚)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridViewHikiage_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                DataGridViewRow row = this.customDataGridViewHikiage.CurrentRow;

                if (row == null)
                {
                    return;
                }

                // 前回値チェック用データをセット
                String cellname = this.customDataGridViewHikiage.Columns[e.ColumnIndex].Name;
                String cellvalue = Convert.ToString(this.customDataGridViewHikiage[cellname, e.RowIndex].Value);
                if (beforeValuesForDetailHikiage.ContainsKey(cellname))
                {
                    beforeValuesForDetailHikiage[cellname] = cellvalue;
                }
                else
                {
                    beforeValuesForDetailHikiage.Add(cellname, cellvalue);
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
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        private void UIForm_Shown(object sender, EventArgs e)
        {
            if (this.customDataGridViewSechi.CurrentCell == this.customDataGridViewSechi[0, 0])
            {
                var parentForm = (BasePopForm)this.Parent;
                parentForm.lb_hint.Text = "コンテナ種類CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            }
        }

        #endregion
    }
}
