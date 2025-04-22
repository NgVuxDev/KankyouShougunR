using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Common.KaisyuuHinmeShousai
{
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;
        /// <summary>
        /// 返却回収品名詳細リスト
        /// </summary>
        public List<DTOClass> RetKaishuHinmeiSyousaiList { get; set; }
        /// <summary>
        /// 受け取り回収品名詳細リスト
        /// </summary>
        public List<DTOClass> GetKaishuHinmeiSyousaiList { get; set; }
        /// <summary>
        /// 荷降リスト
        /// </summary>
        public List<string> NioroshiList { get; set; }

        public string winId { get; set; }

        /// <summary>
        /// 処理モード
        /// </summary>
        public string syoriMode;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// レコードID
        /// </summary>
        public long recID;

        /// <summary>
        /// 業者CD
        /// </summary>
        public string gyoushaCD;

        /// <summary>
        /// 現場CD
        /// </summary>
        public string genbaCD;

        /// <summary>
        /// 編集前の品名CD
        /// </summary>
        private string beforeHinmeiCd;

        /// <summary>
        /// 編集前の単位CD
        /// </summary>
        private string beforeUnitCd;

        /// <summary>
        /// Validatingイベント中か判定
        /// </summary>
        private bool isCellValidating = false;
        private bool isInputError;
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
                LogUtility.DebugMethodStart(e);
                base.OnLoad(e);
                this.Ichiran.RowsAdded -= this.Ichiran_RowsAdded;
                this.logic.WindowInit();
                this.Ichiran.RowsAdded += this.Ichiran_RowsAdded;
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region UIForm
        public UIForm(WINDOW_TYPE paramIn_windowType, long paramIn_RecID, long paramIn_RecSEQ, string paramIn_GyoushaCd, string paramIn_GenbaCd, List<DTOClass> paramIn_KaishuHinmeiSyousaiList, List<string> nioroshiList, string winId, DateTime now)
            : base(WINDOW_ID.C_KAISYUU_HINMEI_SHOUSAI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart(paramIn_windowType, paramIn_RecID, paramIn_RecSEQ, paramIn_GyoushaCd, paramIn_GenbaCd, paramIn_KaishuHinmeiSyousaiList, nioroshiList, winId, now);
                this.recID = paramIn_RecID;
                this.GetKaishuHinmeiSyousaiList = paramIn_KaishuHinmeiSyousaiList;
                if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(paramIn_windowType) || WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(paramIn_windowType))
                {
                    // 編集モード
                    this.syoriMode = "1";
                }
                else
                {
                    // 参照モード
                    this.syoriMode = "2";
                }
                this.gyoushaCD = paramIn_GyoushaCd;
                this.genbaCD = paramIn_GenbaCd;
                this.InitializeComponent();
                this.RetKaishuHinmeiSyousaiList = new List<DTOClass>();
                this.NioroshiList = nioroshiList;
                this.winId = winId;
                this.sysDate = now;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 明細一覧のcellを結合する。
        /// <summary>
        /// 明細一覧のcellを結合する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                this.logic.ChangeCell(sender, e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellPainting", ex);
                throw;
            }
            finally
            {
            }
        }

        private void Ichiran_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.Refresh();
        }

        #endregion

        #region 現場_定期品名の情報「基準値～曜日」の設定処理。
        /// <summary>
        /// 「基準値～曜日」設定処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool Ichiran_SetGenbaTeikiHinmei(object sender, DataGridViewCellValidatingEventArgs e, string hinmeiCd, int denpyouKbnCd, string unitCd)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e, hinmeiCd, denpyouKbnCd, unitCd);
                // 品名CD、単位CDの重複チェック
                bool isError = this.logic.DuplicationCheck(hinmeiCd, unitCd);

                if (isError)
                {
                    e.Cancel = true;
                    this.isInputError = true;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E031", "品名CD、単位CD");
                    this.Ichiran.BeginEdit(false);
                    return false;
                }
                else
                {
                    if (Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.INPUT_KBN_NAME].Value) == ConstCls.INPUT_KBN_2)
                    {
                        return true;
                    }
                    M_GENBA_TEIKI_HINMEI data = new M_GENBA_TEIKI_HINMEI();
                    M_GENBA_TEIKI_HINMEI mGenbaTeikiHinmei = null;
                    data.GYOUSHA_CD = this.gyoushaCD;
                    data.GENBA_CD = this.genbaCD;
                    data.HINMEI_CD = hinmeiCd;
                    data.DENPYOU_KBN_CD = SqlInt16.Parse(denpyouKbnCd.ToString());
                    if (!string.IsNullOrEmpty(unitCd))
                    {
                        data.UNIT_CD = SqlInt16.Parse(unitCd);
                        // 現場_定期品名データ
                        mGenbaTeikiHinmei = DaoInitUtility.GetComponent<IM_GENBA_TEIKI_HINMEI_POPUPDao>().GetAllValidData(data);
                    }
                    else
                    {
                        // 現場_定期品名データ
                        mGenbaTeikiHinmei = DaoInitUtility.GetComponent<IM_GENBA_TEIKI_HINMEI_POPUPDao>().GetHinmeiData(data.GYOUSHA_CD, data.GENBA_CD, data.HINMEI_CD, denpyouKbnCd);
                    }
                    if (mGenbaTeikiHinmei == null)
                    {
                        // 換算値
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSANCHI].ReadOnly = false;
                        // 換算後単位CD
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_CD].Value = string.Empty;
                        // 換算後単位
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Value = string.Empty;
                        // 契約区分と名称
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIYAKU_KBN].Value = string.Empty;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIYAKU_KBN_NM].Value = string.Empty;
                        // 集計区分と名称
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN].Value = string.Empty;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN_NM].Value = string.Empty;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN].ReadOnly = true;
                        // 要記入
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.YOU_KINYU].Value = 0;
                        // 実数
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.ANBUN_FLG].Value = 0;
                        return true;
                    }

                    // 単位CDと名称
                    this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_CD].Value = (!mGenbaTeikiHinmei.UNIT_CD.IsNull ? mGenbaTeikiHinmei.UNIT_CD.ToSqlString() : string.Empty);
                    if (!mGenbaTeikiHinmei.UNIT_CD.IsNull)
                    {
                        M_UNIT Unit = new M_UNIT();
                        Unit = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(int.Parse(mGenbaTeikiHinmei.UNIT_CD.ToString()));
                        // 単位名称
                        if (Unit != null)
                        {
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_NAME].Value = (Unit.UNIT_NAME != null ? Unit.UNIT_NAME : string.Empty);
                        }
                        else
                        {
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_NAME].Value = string.Empty;
                        }
                    }
                    // 換算後単位名称データ
                    if (!mGenbaTeikiHinmei.KANSAN_UNIT_CD.IsNull)
                    {
                        M_UNIT mUnit = new M_UNIT();
                        mUnit = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(int.Parse(mGenbaTeikiHinmei.KANSAN_UNIT_CD.ToString()));
                        // 換算後単位
                        if (mUnit != null)
                        {
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Value = (mUnit.UNIT_NAME_RYAKU != null ? mUnit.UNIT_NAME_RYAKU : string.Empty);
                        }
                        else
                        {
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Value = string.Empty;
                        }
                    }
                    // 換算値
                    if (!mGenbaTeikiHinmei.KANSANCHI.IsNull)
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSANCHI].Value = decimal.Parse(mGenbaTeikiHinmei.KANSANCHI.ToString());
                    }
                    else
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                    }
                    // 換算後単位CD
                    this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_CD].Value = (!mGenbaTeikiHinmei.KANSAN_UNIT_CD.IsNull ? mGenbaTeikiHinmei.KANSAN_UNIT_CD.ToSqlString() : string.Empty);
                    // 契約区分と名称
                    this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIYAKU_KBN].Value = (!mGenbaTeikiHinmei.KEIYAKU_KBN.IsNull ? mGenbaTeikiHinmei.KEIYAKU_KBN.ToSqlString() : string.Empty);
                    if (mGenbaTeikiHinmei.KEIYAKU_KBN == 0)
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN_NM].Value = string.Empty;
                    }
                    if (mGenbaTeikiHinmei.KEIYAKU_KBN == 1)
                    {
                        // 1:定期の場合
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIYAKU_KBN_NM].Value = "定期";
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN].ReadOnly = true;
                    }
                    else if (mGenbaTeikiHinmei.KEIYAKU_KBN == 2)
                    {
                        // 2:単価の場合
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIYAKU_KBN_NM].Value = "単価";
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN].ReadOnly = false;
                    }
                    else if (mGenbaTeikiHinmei.KEIYAKU_KBN == 3)
                    {
                        // なし
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIYAKU_KBN_NM].Value = "回収のみ";
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN].ReadOnly = true;
                    }
                    // 集計区分
                    this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN].Value = (!mGenbaTeikiHinmei.KEIJYOU_KBN.IsNull ? mGenbaTeikiHinmei.KEIJYOU_KBN.ToSqlString() : string.Empty);
                    if (mGenbaTeikiHinmei.KEIJYOU_KBN == 0)
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN_NM].Value = string.Empty;
                    }
                    else if (mGenbaTeikiHinmei.KEIJYOU_KBN == 1)
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN_NM].Value = "伝票";
                    }
                    else if (mGenbaTeikiHinmei.KEIJYOU_KBN == 2)
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN_NM].Value = "合算";
                    }
                    // 要記入
                    if (!mGenbaTeikiHinmei.KANSAN_UNIT_MOBILE_OUTPUT_FLG.IsNull)
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.YOU_KINYU].Value = (mGenbaTeikiHinmei.KANSAN_UNIT_MOBILE_OUTPUT_FLG.Value == true ? 1 : 0);
                    }
                    else
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.YOU_KINYU].Value = 0;
                    }
                    // 実数
                    this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.ANBUN_FLG].Value = (mGenbaTeikiHinmei.ANBUN_FLG.IsTrue ? 1 : 0);

                    if(mGenbaTeikiHinmei != null)
                    {
                        // 現場マスタの定期品名が存在する場合は、組込とする
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.INPUT_KBN].Value = "2";
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.INPUT_KBN_NAME].Value = ConstCls.INPUT_KBN_2;

                        isCellValidating = true;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.HINMEI_CD].ReadOnly = true;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_CD].ReadOnly = true;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSANCHI].ReadOnly = true;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_CD].ReadOnly = true;
                        // QN Tue Anh #158987 START
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.YOU_KINYU].ReadOnly = true;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.ANBUN_FLG].ReadOnly = true;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIYAKU_KBN].ReadOnly = true;
                        this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIJYOU_KBN].ReadOnly = true;
                        // QN Tue Anh #158987 END
                        isCellValidating = false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_SetGenbaTeikiHinmei", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 品名CD、単位CDのロストフォーカス処理。
        /// <summary>
        /// 品名CD、単位CDのロストフォーカス処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (isCellValidating)
                {
                    return;
                }
                
                if (this.Ichiran.ReadOnly == false)
                {
                    string hinmeiCd = "";
                    if (this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.HINMEI_CD].Value != null)
                    {
                        hinmeiCd = this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.HINMEI_CD].Value.ToString();
                    }

                    int denpyouKbnCd = 0;
                    if (this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.DENPYOU_KBN_CD].Value != null)
                    {
                        var denpyouCellValue = this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.DENPYOU_KBN_CD].Value.ToString();
                        int.TryParse(denpyouCellValue, out denpyouKbnCd);
                    }

                    // 品名CDロストフォーカス
                    if (this.Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(ConstCls.HINMEI_CD))
                    {
                        if (!string.IsNullOrEmpty(hinmeiCd))
                        {
                            if (this.beforeHinmeiCd == hinmeiCd && !this.isInputError)
                            {
                                return;
                            }
                            this.isInputError = false;
                            // 品名名称データ
                            M_HINMEI mHinmei = new M_HINMEI();
                            mHinmei = DaoInitUtility.GetComponent<IM_HINMEIDao>().GetDataByCd(hinmeiCd);
                            // 品名
                            if (mHinmei != null && mHinmei.DELETE_FLG != true)
                            {
                                if (this.beforeHinmeiCd != this.Ichiran.CurrentRow.Cells[e.ColumnIndex].Value.ToString()
                                          || null == this.Ichiran.CurrentRow.Cells["DENPYOU_KBN_CD"].Value
                                          || String.IsNullOrEmpty(this.Ichiran.CurrentRow.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                                {
                                    // 伝票区分をセット（品名が変更されたときだけ）
                                    bool isDenpyouKbn = this.logic.SetDenpyouKbn(mHinmei);
                                    if (false == isDenpyouKbn)
                                    {
                                        this.isInputError = true;
                                        e.Cancel = true;
                                        return;
                                    }
                                }

                                string unitCd = "";
                                if (this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_CD].Value != null)
                                {
                                    unitCd = this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_CD].Value.ToString();
                                }
                                if (this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.DENPYOU_KBN_CD].Value != null)
                                {
                                    // 伝票区分を再設定
                                    var denpyouCellValue = this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.DENPYOU_KBN_CD].Value.ToString();
                                    int.TryParse(denpyouCellValue, out denpyouKbnCd);
                                }
                                // [単位CD]が入力済の場合
                                if (!string.IsNullOrEmpty(unitCd))  // No.3160
                                {                                   // No.3160
                                    if (!this.Ichiran_SetGenbaTeikiHinmei(sender, e, hinmeiCd, denpyouKbnCd, unitCd))
                                    {
                                        return;
                                    }
                                }                                   // No.3160
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.HINMEI_NAME].Value = mHinmei.HINMEI_NAME_RYAKU;
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.HINMEI_NAME].Style.BackColor = Color.White;
                            }
                            else
                            {
                                // 品名
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.HINMEI_NAME].Value = string.Empty;
                                // 伝票区分CD
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.DENPYOU_KBN_CD].Value = String.Empty;
                                // 伝票区分
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.DENPYOU_KBN_CD_NM].Value = String.Empty;
                                // 換算値
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                                // 換算後単位CD
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_CD].Value = string.Empty;
                                // 換算後単位
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Value = string.Empty;
                                e.Cancel = true;
                                this.isInputError = true;
                                //レコードが存在しない
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E020", "品名");
                                this.Ichiran.BeginEdit(false);
                            }
                        }
                        else if (String.IsNullOrEmpty(hinmeiCd))
                        {
                            // 品名
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.HINMEI_NAME].Value = string.Empty;
                            // 伝票区分CD
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.DENPYOU_KBN_CD].Value = String.Empty;
                            // 伝票区分
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.DENPYOU_KBN_CD_NM].Value = String.Empty;
                            // 換算値
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                            // 換算後単位CD
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_CD].Value = string.Empty;
                            // 換算後単位
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Value = string.Empty;
                            this.isInputError = false;
                            return;
                        }

                    }

                    // 単位CDロストフォーカス
                    if (this.Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(ConstCls.UNIT_CD))
                    {
                        string unitCd = "";
                        if (this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_CD].Value != null)
                        {
                            unitCd = this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_CD].Value.ToString();
                        }
                        else
                        {
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_NAME].Value = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(unitCd))
                        {
                            if (this.beforeUnitCd == unitCd && !this.isInputError)
                            {
                                return;
                            }
                            this.isInputError = false;
                            //単位名称データ
                            M_UNIT mUnit = new M_UNIT();
                            int tmp;
                            if (!int.TryParse(unitCd, out tmp))
                            {
                                e.Cancel = true;
                                this.isInputError = true;
                                //レコードが存在しない
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E020", "単位");
                                this.Ichiran.BeginEdit(false);
                                return;
                            }
                            mUnit = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(int.Parse(unitCd));
                            // 単位名
                            if (mUnit != null && mUnit.DELETE_FLG != true)
                            {
                                // [品名CD]が入力済の場合
                                if (!string.IsNullOrEmpty(hinmeiCd))
                                {
                                    this.Ichiran.CellValidating -= this.Ichiran_CellValidating;
                                    if (!this.Ichiran_SetGenbaTeikiHinmei(sender, e, hinmeiCd, denpyouKbnCd, unitCd))
                                    {
                                        this.Ichiran.CellValidating += this.Ichiran_CellValidating;
                                        return;
                                    }
                                    this.Ichiran.CellValidating += this.Ichiran_CellValidating;
                                }
                                // Ichiran_SetGenbaTeikiHinmei()で単位CDがクリアされている場合は、元に戻す
                                if (string.IsNullOrEmpty(this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_CD].Value.ToString()))
                                {
                                    this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_CD].Value = unitCd;
                                }
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_NAME].Value = mUnit.UNIT_NAME_RYAKU;
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_NAME].Style.BackColor = Color.White;
                            }
                            else
                            {
                                // 単位名
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_NAME].Value = string.Empty;
                                // 換算値
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                                // 換算後単位CD
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_CD].Value = string.Empty;
                                // 換算後単位
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Value = string.Empty;
                                e.Cancel = true;
                                this.isInputError = true;
                                //レコードが存在しない
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E020", "単位");
                                this.Ichiran.BeginEdit(false);
                            }
                        }
                        else if (String.IsNullOrEmpty(unitCd))
                        {
                            // 単位名
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.UNIT_NAME].Value = string.Empty;
                            // 換算値
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                            // 換算後単位CD
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_CD].Value = string.Empty;
                            // 換算後単位
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Value = string.Empty;
                            this.isInputError = false;
                            return;
                        }
                    }

                    // 換算後単位CDロストフォーカス
                    if (this.Ichiran.Columns[e.ColumnIndex].Name.Equals(ConstCls.KANSAN_UNIT_CD))
                    {
                        string unitCd = "";
                        if (this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_CD].Value != null)
                        {
                            unitCd = this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_CD].Value.ToString();
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Value = string.Empty;
                        }
                        else
                        {
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Value = string.Empty;
                        }
                        if (!string.IsNullOrWhiteSpace(unitCd))
                        {
                            int tmp;
                            if (!int.TryParse(unitCd, out tmp))
                            {
                                e.Cancel = true;
                                //レコードが存在しない
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E020", "単位");
                                this.Ichiran.BeginEdit(false);
                                return;
                            }

                            //単位名称データ
                            M_UNIT mUnit = new M_UNIT();
                            mUnit = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(int.Parse(unitCd));
                            // 単位名
                            if (mUnit != null && mUnit.DELETE_FLG != true)
                            {
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Value = mUnit.UNIT_NAME_RYAKU;
                                this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KANSAN_UNIT_NAME].Style.BackColor = Color.White;
                            }
                            else
                            {
                                e.Cancel = true;
                                //レコードが存在しない
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E020", "単位");
                                this.Ichiran.BeginEdit(false);
                            }
                        }
                    }

                    // 荷降ロストフォーカス
                    if (this.Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(ConstCls.NIOROSHI_NUMBER))
                    {
                        string nioroshiNum = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.NIOROSHI_NUMBER].Value);
                        if (!this.NioroshiList.Contains(nioroshiNum) && !string.IsNullOrEmpty(nioroshiNum))
                        {
                            this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.NIOROSHI_NUMBER].Style.BackColor = Constans.ERROR_COLOR;
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E062", "荷降明細の荷降No");
                            e.Cancel = true;
                        }
                    }

                    // 契約区分ロストフォーカス
                    if (this.Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(ConstCls.KEIYAKU_KBN))
                    {
                        string inputKbn = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.INPUT_KBN].Value);
                        string keiyakuKbn = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.KEIYAKU_KBN].Value);
                        if (inputKbn == "1" && keiyakuKbn == "1")
                        {
                            this.Ichiran.Rows[e.RowIndex].Cells["KEIYAKU_KBN_NM"].Value = string.Empty;
                            this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN"].Value = string.Empty;
                            this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN_NM"].Value = string.Empty;
                            this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN"].ReadOnly = true;
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "契約区分");
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellValidating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 新規行の場合には削除チェックさせない処理。
        /// <summary>
        /// 品名CD、単位CDのロストフォーカス処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 新規行の場合には削除チェックさせない
                this.Ichiran.Rows[e.RowIndex].Cells["DELETE_FLG"].ReadOnly = this.Ichiran.Rows[e.RowIndex].IsNewRow;

                if (e.ColumnIndex == this.HINMEI_CD.Index)
                {
                    this.beforeHinmeiCd = Convert.ToString(this.Ichiran.CurrentRow.Cells[e.ColumnIndex].Value);
                }
                if (e.ColumnIndex == this.UNIT_CD.Index)
                {
                    this.beforeUnitCd = Convert.ToString(this.Ichiran.CurrentCell.Value);
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellEnter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion


        /// <summary>
        /// 「単位CD」と「換算後単位CD」をの表示切替
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            CustomDataGridView grid = sender as CustomDataGridView;
            if (grid != null)
            {
                // 入力区分を上書き
                if (e.ColumnIndex == grid.Columns[this.INPUT_KBN.Name].Index)
                {
                    if (grid[this.INPUT_KBN_NAME.Name, e.RowIndex].Value != null)
                    {
                        e.Value = grid[this.INPUT_KBN_NAME.Name, e.RowIndex].Value.ToString();
                    }
                }
                // 単位CDに単位名を上書き
                if (e.ColumnIndex == grid.Columns[this.UNIT_CD.Name].Index)
                {
                    if (grid[this.UNIT_NAME.Name, e.RowIndex].Value != null)
                    {
                        e.Value = grid[this.UNIT_NAME.Name, e.RowIndex].Value.ToString();
                    }
                }

                // 換算後単位CDに換算後単位名を上書き
                if (e.ColumnIndex == grid.Columns[this.KANSAN_UNIT_CD.Name].Index)
                {
                    if (grid[this.KANSAN_UNIT_CD.Name, e.RowIndex].Value != null
                        && !string.IsNullOrWhiteSpace(grid[this.KANSAN_UNIT_CD.Name, e.RowIndex].Value.ToString())
                        && grid[this.KANSAN_UNIT_NAME.Name, e.RowIndex].Value != null)
                    {
                        e.Value = grid[this.KANSAN_UNIT_NAME.Name, e.RowIndex].Value.ToString();
                    }
                }
            }
        }

        private void Ichiran_KeyDown(object sender, KeyEventArgs e)
        {
            this.logic.CheckPopup(e);
        }

        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            LogUtility.DebugMethodStart(sender, e);

            e.Control.KeyDown -= this.Ichiran_KeyDown;
            e.Control.KeyDown += this.Ichiran_KeyDown;
        }

        private void Ichiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);


                if (e.RowIndex >= 0)
                {
                    #region  契約区分名
                    // 契約区分名の表示を行う                
                    if (e.ColumnIndex == this.KEIYAKU_KBN.Index)
                    {
                        switch (this.logic.GetCellValue(this.Ichiran.Rows[e.RowIndex].Cells["KEIYAKU_KBN"]))
                        {
                            case "1":
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIYAKU_KBN_NM"].Value = "定期";
                                //集計区分
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN"].Value = string.Empty;
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN_NM"].Value = string.Empty;
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN"].ReadOnly = true;
                                this.Ichiran.Columns["KEIJYOU_KBN"].HeaderText = this.Ichiran.Columns["KEIJYOU_KBN"].HeaderText.Replace("※", "");

                                break;

                            case "2":
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIYAKU_KBN_NM"].Value = "単価";
                                string inputKbn = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["INPUT_KBN"].Value);
                                //集計区分
                                if (this.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                                {
                                    // QN Tue Anh #158987 START
                                    if (this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.INPUT_KBN].Value != null
                                        && this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.INPUT_KBN].Value.ToString() != "2")
                                    {
                                        this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN"].ReadOnly = false;
                                    }
                                    // QN Tue Anh #158987 EBD
                                }
                                this.Ichiran.Columns["KEIJYOU_KBN"].HeaderText = this.Ichiran.Columns["KEIJYOU_KBN"].HeaderText + "※";

                                break;
                            case "3":
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIYAKU_KBN_NM"].Value = "回収のみ";
                                //集計区分
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN"].Value = string.Empty;
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN_NM"].Value = string.Empty;
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN"].ReadOnly = true;
                                this.Ichiran.Columns["KEIJYOU_KBN"].HeaderText = this.Ichiran.Columns["KEIJYOU_KBN"].HeaderText.Replace("※", "");

                                break;
                            default:
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIYAKU_KBN_NM"].Value = string.Empty;
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN"].Value = string.Empty;
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN_NM"].Value = string.Empty;
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN"].ReadOnly = true;
                                break;
                        }
                    }
                    #endregion

                    #region 集計区分名の表示を行う
                    if (e.ColumnIndex == this.KEIJYOU_KBN.Index)
                    {
                        switch (this.logic.GetCellValue(this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN"]))
                        {
                            case "1":
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN_NM"].Value = "伝票";
                                break;

                            case "2":
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN_NM"].Value = "合算";
                                break;

                            default:
                                this.Ichiran.Rows[e.RowIndex].Cells["KEIJYOU_KBN_NM"].Value = string.Empty;
                                break;
                        }
                    }
                    #endregion

                    #region 品名CDの大文字
                    if (e.ColumnIndex == this.HINMEI_CD.Index)
                    {
                        string hinmeiCd = this.logic.GetCellValue(this.Ichiran.Rows[e.RowIndex].Cells["HINMEI_CD"]);
                        if (!string.IsNullOrEmpty(hinmeiCd))
                        {
                            this.Ichiran.Rows[e.RowIndex].Cells["HINMEI_CD"].Value = hinmeiCd.ToUpper();
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("syuusyuuDetailIchiran_CellValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        internal void Ichiran_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.logic.Ichiran_RowsAdded();
        }

    }
}
