using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Entity;
using r_framework.CustomControl;
using r_framework.Authority;
using r_framework.FormManager;
using System.Runtime.InteropServices;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.ElectronicManifest.CustomControls_Ex;
using r_framework.Dao;
using Shougun.Core.Common.BusinessCommon.Dao;
using Seasar.Framework.Exceptions;
using r_framework.Configuration;

namespace Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal LogicClass Logic;

        private DenshiMasterDataLogic MasterLogic;

        //処理モード
        public WINDOW_TYPE paramInMode = WINDOW_TYPE.NONE;
        //管理ID(一次マニ)
        public String paramInKanriId = string.Empty;
        //管理ID対応なSEQ(二次)
        public String paramInSeq = string.Empty;
        /// <summary>
        /// データ移動モード Flg
        /// True:データ移動モード
        /// </summary>
        internal bool moveData_flg = false;
        /// <summary>
        /// データ移動用 取引先
        /// </summary>
        internal string moveData_torihikisakiCd;
        /// <summary>
        /// データ移動用 業者
        /// </summary>
        internal string moveData_gyousyaCd;
        /// <summary>
        /// データ移動用 現場
        /// </summary>
        internal string moveData_genbaCd;

        /// <summary>
        /// 備考欄の最大行
        /// </summary>
        internal int bikouMaxRowCount = 5;

        // 20140606 katen 不具合No.4691 start‏
        /// <summary>
        /// 他の画面にもらったマニフェスト一次二次区分
        /// </summary>
        public int? fromManiFirstFlag = null;
        // 20140606 katen 不具合No.4691 start‏

        /// <summary>
        /// G142から開かれたかどうかの判定フラグ
        /// </summary>
        public bool isOpenG142 = false;

        /// <summary>
        /// 前回値保存用ディクショナリ
        /// </summary>
        private Dictionary<String, String> prevValueDictionary;

        /// <summary>
        /// 前回値保存用ディクショナリ(運搬情報用)
        /// </summary>
        private Dictionary<String, String> prevUnpanInfoValueDictionary;

        /// <summary>
        /// ポップアップによる入力かを判定するフラグ
        /// </summary>
        private bool popupFlg = false;

        /// <summary>最終処分情報入力済みフラグ</summary>
        internal bool existAllLastSbnInfo = true;

        internal string preKanyushaNoForGyousha { get; set; }
        private bool isEditHstGyousha = false;
        private bool isEditHstGenba = false;

        private bool isEnabled_UnpanReportInfo = true;
        private bool isEnabled_SBN_ReportInfo = true;

        /// <summary>
        /// 予約マニ向けの保存用処分受託者CD
        /// </summary>
        internal string yoyaku_shobun_gyousyaCd;
        /// <summary>
        /// 予約マニ向けの保存用処分事業場CD
        /// </summary>
        internal string yoyaku_shobun_genbaCd;

        /// <summary>
        /// Request inxs subapp tran id refs #158004
        /// </summary>
        internal string transactionId;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// 画面のコンストラクタ
        /// </summary>
        /// <param name="param_in_Mode"></param>    //遷移元の処理モード
        /// <param name="param_in_KanriId"></param> //遷移元の管理ID(一次マニ)
        /// <param name="param_in_Seq"></param>     //遷移元の管理ID対応なSEQ(二次)
        public UIForm(WINDOW_TYPE param_in_Mode, String param_in_KanriId, String param_in_Seq)
            : base(WINDOW_ID.T_DENSHI_MANIFEST, param_in_Mode)
        {
            this.InitializeComponent();
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);
            this.MasterLogic = new DenshiMasterDataLogic();
            this.Logic.Mode = param_in_Mode;
            this.Logic.KanriId = param_in_KanriId;
            this.Logic.strSeq = param_in_Seq;

            this.paramInMode = param_in_Mode;
            this.paramInKanriId = param_in_KanriId;
            this.paramInSeq = param_in_Seq;
        }
        /// <summary>
        /// 通知履歴明細から、電子マニフェスト伝票の比較処理を追加
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="approvalSeq"></param>
        /// <param name="latestSeq"></param>
        /// <param name="tuuchiCd"></param>
        /// <param name="tuuchiRirekiFlg"></param>
        public UIForm(WINDOW_TYPE param_in_Mode, String param_in_KanriId, String param_in_Seq,
            string approvalSeq, string latestSeq, string tuuchiCd, bool tuuchiRirekiFlg)
            : this(param_in_Mode, param_in_KanriId, param_in_Seq)
        {
            this.Logic.approvalSeq = approvalSeq;
            this.Logic.latestSeq = latestSeq;
            this.Logic.tuuchiCd = tuuchiCd;
            this.Logic.tuuchiRirekiFlg = tuuchiRirekiFlg;

            if (!string.IsNullOrEmpty(approvalSeq)
                && !string.IsNullOrEmpty(latestSeq)
                && !approvalSeq.Equals(latestSeq))
            {
                this.Logic.seqFlag = true;
            }
            else
            {
                this.Logic.seqFlag = false;
            }
        }
        /// <summary>
        /// データ移動用モード用のコンストラクタ
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="gyousyaCd"></param>
        /// <param name="genbaCd"></param>
        public UIForm(WINDOW_TYPE param_in_Mode, String param_in_KanriId, String param_in_Seq,
                                                                    string torihikisakiCd, string gyousyaCd, string genbaCd)
            : this(param_in_Mode, param_in_KanriId, param_in_Seq)
        {
            //データ移動用
            this.moveData_flg = true;
            this.moveData_torihikisakiCd = torihikisakiCd;
            this.moveData_gyousyaCd = gyousyaCd;
            this.moveData_genbaCd = genbaCd;
        }
        #endregion コンストラクタ

        /// <summary>
        /// 画面新規モードに変更
        /// </summary>
        public bool InitializeFormByMode(WINDOW_TYPE wintype)
        {
            try
            {
                //[F2]ボタン無効になる
                BusinessBaseForm parentform = (BusinessBaseForm)this.Parent;
                parentform.bt_func2.Enabled = false;
                //モード設定
                if (this.Logic.Mode != wintype)
                {
                    //画面処理モード
                    base.WindowType = wintype;
                    //base.OnLoad(new EventArgs());
                    this.Logic.Mode = wintype;

                }
                //新規モードで画面初期化
                if (wintype == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    var oldSize = this.Size;
                    this.Size = new Size(1000, 490);
                    this.Pnl_ALL.Location = new Point(2, 2);

                    this.Logic.KanriId = string.Empty;
                    this.Logic.Seq = 0;

                    this.Controls.Clear();
                    this.Pnl_ALL.Dispose();
                    this.InitializeComponent();

                    this.cdgv_Haikibutu.CellValidated += new DataGridViewCellEventHandler(this.cdgv_Haikibutu_CellValidated);

                    this.cdgv_Tyukanshori.CellValidating += new DataGridViewCellValidatingEventHandler(this.TyukanshoriCellValidating);

                    this.Location = new Point(parentform.headerForm.Location.X,
                                              parentform.headerForm.Location.Y + parentform.headerForm.Height);
                    var y = 0;
                    y += parentform.ribbonForm.Size.Height;
                    y += 6;
                    y += parentform.headerForm.Height;
                    y += 10;
                    this.Size = new Size(oldSize.Width, parentform.pn_foot.Location.Y - y);
                    this.TopLevel = false;
                    base.WindowType = wintype;
                    //base.OnLoad(new EventArgs());
                    //ヘーダフォームを初期化
                    this.Logic.headerform.ctxt_FirstRegistSha.Text = string.Empty;
                    this.Logic.headerform.ctxt_FirstRegistDate.Text = string.Empty;
                    this.Logic.headerform.ctxt_Lastctxt_LastModifySha.Text = string.Empty;
                    this.Logic.headerform.ctxt_LastModifyDate.Text = string.Empty;

                    // 備考欄を固定行に設定
                    this.AddDefaultRowForBikou();

                    //受入パラメータクリア
                    this.Logic.KanriId = null;
                    this.Logic.Seq = default(SqlInt16);

                    //新規モードで入力制限
                    if (!this.SetControlReadOnlyByInput_KBN(true))
                    {
                        return false;
                    }
                }
                //新規以外モードで
                else
                {
                    //画面処理モードより画面制御
                    this.SetFormControlsStatusByMode();
                }
                //画面処理モードより画面制御
                this.SetFormControlsStatusByMode();

                //DateTimePicker初期化
                this.DateTimePickerCtlClear();

                //廃棄物情報グリッドに自己を伝送
                this.cdgv_Haikibutu.Myform = this;
                //Formが運搬区間情報グリッドに伝送
                this.cdgv_UnpanInfo.Myform = this;

                //最初カーソル位置がマニ区分に設定
                this.cntxt_ManiKBN.Focus();

                parentform.bt_func2.Enabled = true;
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitializeFormByMode", ex);
                this.Logic.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

        }

        /// <summary>
        /// 画面処理モードより画面制御
        /// </summary>
        public void SetFormControlsStatusByMode()
        {
            this.cdgv_LastSBN_Genba_Jiseki.AllowUserToDeleteRows = true;

            //数量系コントロールのフォーマットの設定
            //運搬量
            this.cntxt_UnpanRyo.CustomFormatSetting = ConstCls.ELEC_MANIFEST_SUURYO_FORMAT;
            //有価物収拾量
            this.cntxt_YukabutuRyo.CustomFormatSetting = ConstCls.ELEC_MANIFEST_SUURYO_FORMAT;
            //受入量
            this.cntxt_Jyunyuryo.CustomFormatSetting = ConstCls.ELEC_MANIFEST_SUURYO_FORMAT;
            // 中間処理産業廃棄物の数量
            this.FM_HAIKI_SUU.CustomFormatSetting = this.Logic.mSysInfo.MANIFEST_SUURYO_FORMAT;

            //Formが廃棄物グリッドに伝送
            this.cdgv_Haikibutu.Myform = this;
            //Formが運搬区間情報グリッドに伝送
            this.cdgv_UnpanInfo.Myform = this;

            // 予約マニ向けの保存用処分受託者CD
            yoyaku_shobun_gyousyaCd = string.Empty;
            // 予約マニ向けの保存用処分事業場CD
            yoyaku_shobun_genbaCd = string.Empty;

            BusinessBaseForm parentform = (BusinessBaseForm)this.Parent;
            //新規モード
            if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                //マニフェスト区分をReadOnly属性の初期化
                this.cntxt_ManiKBN.ReadOnly = false;
                this.crdbtn_ManiKBN1.Enabled = true;
                this.crdbtn_ManiKBN2.Enabled = true;
                //入力区分をReadOnly属性の初期化
                this.cntxt_InputKBN.ReadOnly = false;
                this.crdbtn_InputKBN1.Enabled = true;
                this.crdbtn_InputKBN2.Enabled = true;

                //廃棄物種類情報の行が追加可
                this.cdgv_Haikibutu.AllowUserToAddRows = true;
                //廃棄物種類情報の行が削除可
                this.cdgv_Haikibutu.AllowUserToDeleteRows = true;
                //運搬情報の行が追加可
                this.cdgv_UnpanInfo.AllowUserToAddRows = true;
                //運搬報告情報が一番目行のTag初期化
                if (this.cdgv_UnpanInfo.RowCount > 0)
                {
                    UnpanHoukokuDataDTOCls UnpanRepInfo = new UnpanHoukokuDataDTOCls();
                    this.cdgv_UnpanInfo.Rows[0].Tag = UnpanRepInfo;
                }
                //運搬情報の行が削除可
                this.cdgv_UnpanInfo.AllowUserToDeleteRows = true;

                //footボタンの無効有効設定
                //追加ボタン有効になる
                parentform.bt_func2.Enabled = true;

                // 20140627 ria EV004945 修正モードより[F9]登録を押下した後、新規モードに切り替わるが、[F4]2次マニが非活性になっている。 start
                //[F4]2次マニ
                parentform.bt_func4.Enabled = true;
                // 20140627 ria EV004945 修正モードより[F9]登録を押下した後、新規モードに切り替わるが、[F4]2次マニが非活性になっている。 end

                //修正ボタン無効になる
                parentform.bt_func3.Enabled = false;
                //マニパタン登録有効になる
                parentform.bt_process1.Enabled = true;
                //マニパタン呼出有効になる
                parentform.bt_process2.Enabled = true;
                //一次紐付ボタン無効になる
                parentform.bt_process3.Enabled = false;
                parentform.bt_process3.Text = string.Empty;

                //受渡確認ボタン無効に設定
                parentform.bt_func8.Enabled = false;
                parentform.bt_func8.Text = string.Empty;

                //中間処理廃棄物情報編集不可を設定する
                this.SetTyukanshoriReadOnly(true);
                this.cdgv_Tyukanshori.AllowUserToAddRows = true;
                //this.cdgv_Tyukanshori.Rows.Clear();
                this.ccbx_Touranshitei.Enabled = false;
                this.ccbx_ItijiFuyou.Enabled = false;
                this.ccbx_ChouboKisai.Enabled = false;
                //

                // 委託契約書記載のとおり
                this.ccbx_YitakuKeyaku.Enabled = true;
                this.ccbx_YitakuKeyaku.Checked = false;
                // 当欄指定のとおり
                this.ccbx_Toulanshitei.Enabled = true;
                this.ccbx_Toulanshitei.Checked = false;

                this.SetHaikibutuYuugaiColumnReadOnly(true, true, true);

                this.Logic.headerform.windowTypeLabel.Text = "新規";
                this.Logic.headerform.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                this.Logic.headerform.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                // 各明細の削除チェックボックスを活性にする。
                this.cdgv_Haikibutu.Columns["Haikibutu_chb_delete"].ReadOnly = false;
                // 2次マニの場合
                if (this.Logic.maniFlag == 2)
                {
                    this.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = false;
                }
                this.cdgv_LastSBNbasyo_yotei.Columns["LastSBNbasyo_chb_delete"].ReadOnly = false;
                this.cdgv_UnpanInfo.Columns["UnpanInfo_chb_delete"].ReadOnly = false;
                // 入力区分 = 2.手動の場合
                if (cntxt_InputKBN.Text == "2")
                {
                    this.cdgv_LastSBN_Genba_Jiseki.Columns["LastSBN_Genba_chb_delete"].ReadOnly = false;
                }
            }
            //修正モード
            if (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                //データ無し場合
                if (this.Logic.ManiInfo == null)
                {
                    //画面のコントロールのReadOnlyに設定
                    this.SetEachControlReadOnlyOrEnabled(this.Controls);
                    //グリッドのカラムReadOnlyに設定
                    this.SetDgvColumnReadOnly();
                    return;
                }
                //マニ区分はマニフェストの場合、変更不可
                this.cntxt_ManiKBN.ReadOnly = !(this.cntxt_ManiKBN.Text == "1");
                this.crdbtn_ManiKBN1.Enabled = !this.cntxt_ManiKBN.ReadOnly;
                this.crdbtn_ManiKBN2.Enabled = !this.cntxt_ManiKBN.ReadOnly;

                //入力区分をReadOnlyに設定
                this.cntxt_InputKBN.ReadOnly = true;
                this.crdbtn_InputKBN1.Enabled = false;
                this.crdbtn_InputKBN2.Enabled = false;
                //予約修正権限の設定
                if (this.Logic.ManiInfo != null && this.Logic.ManiInfo.dt_r18 != null)
                {
                    if (this.Logic.ManiInfo.dt_r18.MANIFEST_KBN == 2)//マニフェスト場合
                    {
                        this.cntxt_ModifyRight.Text = "1";//非許可に固定
                        this.cntxt_ModifyRight.ReadOnly = true;
                        this.crdbtn_ModifyRight1.Enabled = false;
                        this.crdbtn_ModifyRight2.Enabled = false;
                        this.crdbtn_ModifyRight3.Enabled = false;
                        this.crdbtn_ModifyRight4.Enabled = false;
                    }
                }

                if (this.Logic.ManiInfo != null && this.Logic.ManiInfo.dt_mf_toc != null)
                {
                    if (this.Logic.ManiInfo.dt_mf_toc.KIND == 5)//手動の場合
                    {
                        //交付番号が修正モードで変更可
                        this.cantxt_ManifestNo.ReadOnly = false;
                    }
                    else
                    {
                        //交付番号が修正モードで変更不可
                        this.cantxt_ManifestNo.ReadOnly = true;
                    }
                }

                //排出事業者連携情報をReadOnlyになる
                this.cantxt_HaisyutuGyousyaCd.SetLikedControlsReadOnlyStatus(this.Logic.ManiInfo.bIsAutoMode);

                //排出事業場連携情報をReadOnlyになる
                //this.cantxt_HaisyutuGenbaCd.SetLikedControlsReadOnlyStatus(this.Logic.ManiInfo.bIsAutoMode);
                this.Logic.SetHstGenbaLikedControlsReadOnlyStatus(this.Logic.ManiInfo.bIsAutoMode);

                //処分受託者連携情報をReadOnlyになる
                this.cantxt_SBN_JyutakuShaCD.SetLikedControlsReadOnlyStatus(this.Logic.ManiInfo.bIsAutoMode);
                //処分事業場連携情報をReadOnlyになる
                this.cantxt_SBN_Genba_CD.SetLikedControlsReadOnlyStatus(this.Logic.ManiInfo.bIsAutoMode);

                //運搬報告エリアの編集不可
                this.isEnabled_UnpanReportInfo = !this.Logic.ManiInfo.bIsAutoMode;
                this.SetUnpanReportInfoReadOnly(this.Logic.ManiInfo.bIsAutoMode);
                //処分報告エリアが編集不可
                this.isEnabled_SBN_ReportInfo = !this.Logic.ManiInfo.bIsAutoMode;
                this.SetSbnReportInfoReadOnly(this.Logic.ManiInfo.bIsAutoMode);
                //グリッドの名称など編集不可
                this.SetDgvColumnReadOnlyByInputMode(this.Logic.ManiInfo.bIsAutoMode, false);

                //運搬情報グリッドクリアする
                this.cdgv_UnpanInfo.Rows.Clear();
                //修正モードで、予約場合はデータ無し場合は運搬区間情報が編集可
                if (this.Logic.ManiInfo.dt_r18.MANIFEST_KBN == 1)
                {
                    this.cdgv_UnpanInfo.ReadOnly = false;
                    //運搬情報追加行可
                    this.cdgv_UnpanInfo.AllowUserToAddRows = true;
                    if (this.Logic.ManiInfo.lstDT_R19.Count == 0)
                    {
                        //運搬情報削除行可
                        this.cdgv_UnpanInfo.AllowUserToDeleteRows = true;
                    }
                    else
                    {
                        //運搬情報削除行不可
                        this.cdgv_UnpanInfo.AllowUserToDeleteRows = false;
                    }
                }
                //上記以外場合は、入力モードより状態の設定
                else
                {
                    this.cdgv_UnpanInfo.ReadOnly = this.Logic.ManiInfo.bIsAutoMode;
                    //運搬情報追加行可
                    this.cdgv_UnpanInfo.AllowUserToAddRows = !this.Logic.ManiInfo.bIsAutoMode;
                    //運搬情報削除行可
                    this.cdgv_UnpanInfo.AllowUserToDeleteRows = !this.Logic.ManiInfo.bIsAutoMode;
                }

                //Footボタン有効無効設定  
                //2次マニボタン無効になる
                parentform.bt_func4.Enabled = false;

                //一次紐付ボタン無効になる
                parentform.bt_process3.Enabled = false;
                parentform.bt_process3.Text = string.Empty;
                //受渡確認ボタン有効になる
                parentform.bt_func8.Enabled = true;
                parentform.bt_func8.Text = "[F8]\r\n受渡確認";

                //発行件数の編集不可を設定する
                this.cantxt_HakkouCnt.Enabled = false;
                //廃棄物情報グリッドの設定
                this.cdgv_Haikibutu.Rows.Clear();
                if (0 == this.cdgv_Haikibutu.Rows.Count)
                {
                    this.cdgv_Haikibutu.Rows.Add();
                }
                this.cdgv_Haikibutu.AllowUserToAddRows = false;
                //最終処分事業場「予定」グリッドの設定
                this.cdgv_LastSBNbasyo_yotei.Rows.Clear();
                //this.cdgv_LastSBNbasyo_yotei.Rows.Add();
                //this.cdgv_LastSBNbasyo_yotei.AllowUserToAddRows = false;
                //最終処分事業場「実績」グリッドの設定
                this.cdgv_LastSBN_Genba_Jiseki.Rows.Clear();
                this.cdgv_LastSBN_Genba_Jiseki.AllowUserToAddRows = !this.Logic.ManiInfo.bIsAutoMode;
                //備考グリッドの設定
                this.cdgv_Bikou.Rows.Clear();
                // 行数固定
                this.AddDefaultRowForBikou();

                this.Logic.headerform.windowTypeLabel.Text = "修正";
                this.Logic.headerform.windowTypeLabel.BackColor = System.Drawing.Color.Yellow;
                this.Logic.headerform.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                // 入力区分が自動の場合、明細の削除チェックボックスは使用不可とする。
                if (cntxt_InputKBN.Text == "1")
                {
                    this.cdgv_Haikibutu.Columns["Haikibutu_chb_delete"].ReadOnly = true;
                    // 中間処理産業廃棄物は2次マニの場合に使用可能。
                    if (this.Logic.maniFlag == 2)
                    {
                        this.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = false;
                    }
                    this.cdgv_LastSBNbasyo_yotei.Columns["LastSBNbasyo_chb_delete"].ReadOnly = false;
                    this.cdgv_UnpanInfo.Columns["UnpanInfo_chb_delete"].ReadOnly = true;
                    this.cdgv_LastSBN_Genba_Jiseki.Columns["LastSBN_Genba_chb_delete"].ReadOnly = true;
                }
                // 入力区分が手動の場合、明細の削除チェックボックスは使用可能とする。
                else if (cntxt_InputKBN.Text == "2")
                {
                    this.cdgv_Haikibutu.Columns["Haikibutu_chb_delete"].ReadOnly = false;
                    this.cdgv_LastSBNbasyo_yotei.Columns["LastSBNbasyo_chb_delete"].ReadOnly = false;
                    this.cdgv_UnpanInfo.Columns["UnpanInfo_chb_delete"].ReadOnly = false;
                    this.cdgv_LastSBN_Genba_Jiseki.Columns["LastSBN_Genba_chb_delete"].ReadOnly = false;

                    // 中間処理産業廃棄物は2次マニの場合に使用可能。
                    if (this.Logic.maniFlag == 2)
                    {
                        this.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = false;
                    }
                }
            }
            //削除モード
            else if (this.Logic.Mode == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                //Foot部のボタン状態の設定
                parentform.bt_func2.Enabled = false;
                parentform.bt_func3.Enabled = false;
                parentform.bt_func4.Enabled = false;
                parentform.bt_func6.Enabled = false;
                parentform.bt_process1.Enabled = false;
                parentform.bt_process2.Enabled = false;
                parentform.bt_process3.Enabled = false;
                parentform.bt_process3.Text = string.Empty;
                //グリッドのカラムReadOnlyに設定
                this.SetDgvColumnReadOnly();
                //マニ区分状態
                this.cntxt_ManiKBN.ReadOnly = true;
                this.crdbtn_ManiKBN1.Enabled = false;
                this.crdbtn_ManiKBN2.Enabled = false;
                //中間処理産業廃棄物
                this.pnl_TyukanSyori.Enabled = false;
                this.cdgv_Tyukanshori.ReadOnly = true;
                if (!this.Logic.ManiInfo.bIsAutoMode)
                {
                    //手動の場合F9ボタンの文字が「削除」に変更
                    parentform.bt_func9.Text = "[F9]\r\n削除";
                    //[F10]がブランクで使用不可
                    parentform.bt_func10.Text = string.Empty;
                    parentform.bt_func10.Enabled = false;
                }
                else
                {
                    //自動の場合　F9ボタンの文字が「JWNET登録」にを変更
                    parentform.bt_func9.Text = "[F9]\r\nJWNET送信";
                    //[F10]がブランクで使用可
                    parentform.bt_func10.Text = "[F10]\r\n保留保存";
                    parentform.bt_func10.Enabled = true;
                }

                //画面のコントロールのReadOnlyに設定
                this.SetEachControlReadOnlyOrEnabled(this.Controls, true);

                this.Logic.headerform.windowTypeLabel.Text = "削除";
                this.Logic.headerform.windowTypeLabel.BackColor = System.Drawing.Color.Red;
                this.Logic.headerform.windowTypeLabel.ForeColor = System.Drawing.Color.White;

                // 各明細の削除チェックボックスを非活性にする。
                this.cdgv_Haikibutu.Columns["Haikibutu_chb_delete"].ReadOnly = true;
                this.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = true;
                this.cdgv_LastSBNbasyo_yotei.Columns["LastSBNbasyo_chb_delete"].ReadOnly = true;
                this.cdgv_UnpanInfo.Columns["UnpanInfo_chb_delete"].ReadOnly = true;
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LastSBN_Genba_chb_delete"].ReadOnly = true;
            }
            //参照モード
            else if (this.Logic.Mode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
            {
                //Foot部のボタン状態の設定
                parentform.bt_func2.Enabled = false;
                parentform.bt_func3.Enabled = false;
                parentform.bt_func4.Enabled = false;
                parentform.bt_func6.Enabled = false;
                parentform.bt_func8.Enabled = false;
                parentform.bt_func9.Enabled = false;
                parentform.bt_func10.Enabled = false;
                parentform.bt_process1.Enabled = false;
                parentform.bt_process2.Enabled = false;
                parentform.bt_process3.Enabled = false;
                parentform.bt_process3.Text = string.Empty;
                //マニ区分
                this.cntxt_ManiKBN.ReadOnly = true;
                this.crdbtn_ManiKBN1.Enabled = false;
                this.crdbtn_ManiKBN2.Enabled = false;
                //中間処理産業廃棄物
                this.pnl_TyukanSyori.Enabled = false;
                this.cdgv_Tyukanshori.ReadOnly = true;
                this.cdgv_Tyukanshori.AllowUserToAddRows = false;

                // 参照モードの時、最終処分の場所 （予定）に新規明細行は表示しない。
                this.cdgv_LastSBNbasyo_yotei.ReadOnly = true;
                this.cdgv_LastSBNbasyo_yotei.AllowUserToAddRows = false;

                // 参照モードの時、運搬情報に新規明細行は表示しない。
                this.cdgv_UnpanInfo.ReadOnly = true;
                this.cdgv_UnpanInfo.AllowUserToAddRows = false;

                // 参照モードの時、最終処分事 業 場（実績）に新規明細行は表示しない。
                this.cdgv_LastSBN_Genba_Jiseki.ReadOnly = true;
                this.cdgv_LastSBN_Genba_Jiseki.AllowUserToAddRows = false;

                //グリッドのカラムReadOnlyに設定
                this.SetDgvColumnReadOnly();

                //画面のコントロールのReadOnlyに設定
                this.SetEachControlReadOnlyOrEnabled(this.Controls, true);

                this.Logic.headerform.windowTypeLabel.Text = "参照";
                this.Logic.headerform.windowTypeLabel.BackColor = System.Drawing.Color.Orange;
                this.Logic.headerform.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                // 各明細の削除チェックボックスを非活性にする。
                this.cdgv_Haikibutu.Columns["Haikibutu_chb_delete"].ReadOnly = true;
                this.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = true;
                this.cdgv_LastSBNbasyo_yotei.Columns["LastSBNbasyo_chb_delete"].ReadOnly = true;
                this.cdgv_UnpanInfo.Columns["UnpanInfo_chb_delete"].ReadOnly = true;
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LastSBN_Genba_chb_delete"].ReadOnly = true;
            }

            //二次設定
            if ((this.Logic.Mode != WINDOW_TYPE.NEW_WINDOW_FLAG || this.isOpenG142) &&
                this.Logic.ManiInfo != null && this.Logic.ManiInfo.dt_r18 != null)
            {
                // 二次マニの判定方法は(DT_R18.FIRST_MANIFEST_FLAGに値が設定されている) かつ (排出事業者が自社区分ON)
                M_DENSHI_JIGYOUSHA jigyousha = DaoInitUtility.GetComponent<DENSHI_JIGYOUSHA_SearchDaoCls>().GetDataByCd(this.Logic.ManiInfo.dt_r18.HST_SHA_EDI_MEMBER_ID);
                M_GYOUSHA gyousha = jigyousha != null ? DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDataByCd(jigyousha.GYOUSHA_CD) : null;
                bool hstJishaKbn = gyousha != null ? (bool)gyousha.JISHA_KBN : false;

                //2次マニモードの場合
                if ((this.Logic.ManiInfo.dt_r18.FIRST_MANIFEST_FLAG != null && hstJishaKbn)
                    && ("1".Equals(this.Logic.ManiInfo.dt_r18.FIRST_MANIFEST_FLAG)
                    || "2".Equals(this.Logic.ManiInfo.dt_r18.FIRST_MANIFEST_FLAG)
                    || "3".Equals(this.Logic.ManiInfo.dt_r18.FIRST_MANIFEST_FLAG)))
                {
                    this.Logic.maniFlag = 2;
                }
                else
                {
                    this.Logic.maniFlag = 1;
                }
            }

            //二次初期化
            if (!this.Logic.SetManifestForm("Non")) { return; }

            //Foot部のボタン状態の設定
            //パタン登録呼出ボタン有効無効に設定「新規モードのみ有効」
            parentform.bt_process1.Enabled = this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG;
            parentform.bt_process2.Enabled = this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG;
            //修正ボタンの状態
            (this.Parent as BusinessBaseForm).bt_func3.Enabled = (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG);

            //共通設定
            //排出事業者加入者番号ReadOnly
            this.ctxt_Haisyutu_KanyushaNo.ReadOnly = true;
            //排出事業場番号
            this.ctxt_JIGYOUJYOU_CD.ReadOnly = true;
            //処分受託者の加入者番号をReadOnly
            this.ctxt_SBN_KanyuShaNo.ReadOnly = true;
            //処分受託者の許可番号をReadOnly
            this.ctxt_SBN_KyokaNo.ReadOnly = true;
            //処分事業場番号をReadOnly
            this.ctxt_SBN_JIGYOUJYOU_CD.ReadOnly = true;

            this.Logic.maniRelation = null; //紐付初期化

            if (this.isEnabled_UnpanReportInfo)
                this.cdtp_UnpanEndDate.ReadOnly = false;

            if (this.isEnabled_SBN_ReportInfo)
                this.cdtp_SBNEndDate.ReadOnly = false;
        }
        /// <summary>
        /// マニフェスト情報がDBから取得処理
        /// </summary>
        public void LoadManiInfoFromDB()
        {
            //DBからデータ読込
            this.Logic.ManiInfo = this.Logic.GetManiInfoFromDB(this.Logic.KanriId, this.Logic.Seq);
            if (this.Logic.seqFlag)
            {
                string seq = this.Logic.Seq.ToString();
                if (seq.Equals(this.Logic.latestSeq))
                {
                    seq = this.Logic.approvalSeq;
                } 
                else 
                {
                    seq = this.Logic.latestSeq;
                }
                this.Logic.SEQManiInfo = this.Logic.GetManiInfoFromDB(this.Logic.KanriId, SqlInt16.Parse(seq));
            }
        }

        /// <summary>
        /// 修正モードのみ、元データをReloadする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReloadModifiedManiInfo(object sender, EventArgs e)
        {
            // 権限チェック
            if (Manager.CheckAuthority("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                this.Logic.Mode = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            }
            else if (Manager.CheckAuthority("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                this.Logic.Mode = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            }
            else
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E158", "修正");
                return;
            }

            this.ExecDispModifiedManiInfo();
        }

        /// <summary>
        /// 修正モード表示用の
        /// </summary>
        internal void ExecDispModifiedManiInfo()
        {
            //マニフェスト情報がDBから取得処理
            this.LoadManiInfoFromDB();
            //画面クリア
            if (!this.InitializeFormByMode(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
            {
                return;
            }

            //自動手動より画面制御
            if (!this.SetControlReadOnlyByInput_KBN(this.Logic.ManiInfo.bIsAutoMode))
            {
                return;
            }
            //画面モードより制御する
            this.SetFormControlsStatusByMode();
            //対象を画面に設定
            this.SetFormFromEntity(this.Logic.ManiInfo);
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

			//Init INXS subapp transaction id refs #158004
            this.transactionId = Guid.NewGuid().ToString();

            if (!this.Logic.WindowInit())
            {
                return;
            }

            // 備考欄を固定行に設定
            this.AddDefaultRowForBikou();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Pnl_ALL != null)
            {
                this.Pnl_ALL.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            }

            //画面のサイズと位置の初期化
            this.Size = new Size(1000, 490);
            this.Pnl_ALL.Location = new Point(2, 2);
            //自己がグリッドに伝送
            this.cdgv_Haikibutu.Myform = this;
            //Formが運搬区間情報グリッドに伝送
            this.cdgv_UnpanInfo.Myform = this;

            //DateTimePicker初期化
            this.DateTimePickerCtlClear();

            //マニ区分の初期値をマニフェストに設定
            this.cntxt_ManiKBN.Text = "2";

            //最初カーソル位置の設定
            this.cntxt_ManiKBN.Focus();
            //モードより画面の設定
            //新規モード
            if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                //画面モードより画面制御
                this.SetFormControlsStatusByMode();
                //自動手動より画面制御
                this.SetControlReadOnlyByInput_KBN(true);
                return;
            }
            //新規モード以外場合、パラメータのチェック
            //パラメータが空白場合、エラーメッセージを出す
            else if (string.IsNullOrEmpty(this.Logic.KanriId))
            {
                this.Logic.msgLogic.MessageBoxShow("E045");
                //新規モードに変更
                if (!this.InitializeFormByMode(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                return;
            }

            //SEQ無しの場合は 最新SEQを探す
            if (string.IsNullOrEmpty(this.Logic.strSeq))
            {
                this.Logic.strSeq = this.Logic.GetSeq(this.Logic.KanriId);
                //見つからなかったら閉じる
                if (string.IsNullOrEmpty(this.Logic.strSeq))
                {
                    this.Logic.msgLogic.MessageBoxShow("E045");
                    this.FormClose(null, null);
                    return;
                }
            }


            this.Logic.Seq = SqlInt16.Parse(this.Logic.strSeq);
            //データなし場合、エラーメッセージを出す
            bool catchErr = false;
            var ret = this.Logic.IsManiInfoExist(this.Logic.KanriId, this.Logic.Seq, out catchErr);
            if (catchErr)
            {
                return;
            }

            if (!ret)
            {
                this.Logic.msgLogic.MessageBoxShow("E045");
                //新規モードに変更
                if (!this.InitializeFormByMode(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }
                return;
            }

            //マニフェスト情報がDBから取得処理
            this.LoadManiInfoFromDB();

            //QUE_INFOをチェックしてモード変更
            //(STATUS_FLAG=7保留、STATUS_FLAG=8,9JWNETエラー状態のデータだったら画面制御処理時のみ追加モードに変更)
            if (this.isOpenG142 && this.Logic.que_info_chk())
            {
                this.Logic.HoryuFlg = true;
                if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    this.Logic.HoryuINSFlg = false;
                    this.Logic.HoryuDelFlg = true;
                }
            }

            // 修正モードもしくは削除モード、且つ排出事業者加入番号が存在しない場合
            var partialUpdateMode = false;
            if ((this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG || this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG))
            {
                var retmenber = this.Logic.CheckEdiMemberExistence(out catchErr);
                if (catchErr)
                {
                    return;
                }
                else if (!retmenber)
                {
                    // 排出事業者加入番号がMS_JWNET_MEMBERに存在しなかった場合、部分更新(内部的には修正)モードに遷移
                    // ※この後行われる画面制御向けに一旦参照モードに変更
                    // ※部分更新モードセット時には修正モードに戻される
                    this.Logic.Mode = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    partialUpdateMode = true;
                }
            }

            //保留データの場合
            if (this.Logic.HoryuFlg && this.Logic.HoryuINSFlg)
            {
                this.Logic.Mode = WINDOW_TYPE.NEW_WINDOW_FLAG;
            }
            if (this.Logic.HoryuDelFlg)
            {
                this.Logic.Mode = WINDOW_TYPE.DELETE_WINDOW_FLAG;
            }

            //自動手動より画面制御
            if (!this.SetControlReadOnlyByInput_KBN(this.Logic.ManiInfo.bIsAutoMode)) { return; }

            //画面モードより画面制御
            this.SetFormControlsStatusByMode();


            //画面にデータを設定
            this.SetFormFromEntity(this.Logic.ManiInfo);
            //電子マニフェスト伝票の比較より、異なる箇所のラベル名称の背景色　＝　赤色、　文字色＝白に変更し
            this.ConfirmFormFromEntity(this.Logic.ManiInfo, this.Logic.SEQManiInfo);

            //保留データの場合
            if (this.Logic.HoryuFlg && this.Logic.HoryuINSFlg)
            {
                this.Logic.Mode = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            }
            if (this.Logic.HoryuDelFlg)
            {
                this.Logic.Mode = WINDOW_TYPE.DELETE_WINDOW_FLAG;

                //JWNET登録ボタンの表示名の設定
                BusinessBaseForm parentform = (BusinessBaseForm)this.Parent;
                parentform.bt_func9.Text = "[F9]\n削除";
                parentform.bt_func10.Enabled = false;
                parentform.bt_func10.Text = string.Empty;
            }
            else
            {
                if (partialUpdateMode == true)
                {
                    if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    {
                        // 参照モードセット
                        this.Logic.msgLogic.MessageBoxShow("E057", "排出事業者にEDI利用確認キーが登録", "削除");
                        this.Logic.setReferenceMode();
                    }
                    else if (this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    {
                        // 部分更新モードセット
                        if (!this.Logic.setPartialUpdateMode())
                        {
                            return;
                        }
                    }
                }
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

        /// <summary>
        /// 自動/手動モードより、全画面で入力できかとうか設定処理
        /// </summary>
        /// <param name="bIsAutoMode"></param>
        /// <param name="bIsClear"></param>
        public bool SetControlReadOnlyByInput_KBN(bool bIsAutoMode, bool bIsClear = false)
        {
            try
            {
                //各種Radio選択初期化

                //マニ区分をマニフェストに設定
                this.cntxt_ManiKBN.Text = "2";

                this.cntxt_ManiKBN.ReadOnly = !bIsAutoMode;
                this.crdbtn_ManiKBN1.Enabled = bIsAutoMode;
                this.crdbtn_ManiKBN2.Enabled = bIsAutoMode;

                //入力区分のRadioボタン
                this.crdbtn_InputKBN1.Enabled = (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG);
                this.crdbtn_InputKBN2.Enabled = (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG);
                //交付番号
                if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.cantxt_ManifestNo.ReadOnly = !(cntxt_InputKBN.Text != "1");
                }
                else
                {
                    this.cantxt_ManifestNo.ReadOnly = true;
                }
                //マニフェスト番号クリアする
                this.cantxt_ManifestNo.Text = string.Empty;
                if (bIsClear)
                {
                    //ボディの連絡番号と引渡日付、担当者クリアする
                    this.cdate_HikiwataDate.Value = null;
                    this.ctxt_HikiwataTantouSha.Text = string.Empty;
                    this.ctxt_TourokuTantouSha.Text = string.Empty;
                    this.ctxt_RenlakuNo1.Text = string.Empty;
                    this.ctxt_RenlakuNo2.Text = string.Empty;
                    this.ctxt_RenlakuNo3.Text = string.Empty;
                }
                //排出事業者連携情報
                if (bIsClear)
                {
                    if (!this.ClearEachControl(this.pnl_Hs.Controls))
                    {
                        return false;
                    }
                }
                //排出事業者連携情報有効無効状態の設定
                this.cantxt_HaisyutuGyousyaCd.SetLikedControlsReadOnlyStatus(bIsAutoMode);
                //郵便番号参照ボタンが自動場合は無効になる
                this.cbtn_HaisyutuGyousyaSan.Enabled = !cnt_HaisyutuGyousyaZip.ReadOnly;
                //修正モードで排出事業者名称入る場合、排出事業者CDが変更不可
                if (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    if (!string.IsNullOrEmpty(this.Logic.ManiInfo.dt_r18.HST_SHA_NAME))
                    {
                        //排出事業者CDが自動場合はReadOnlyになる
                        this.cantxt_HaisyutuGyousyaCd.ReadOnly = bIsAutoMode;
                        //ポップアップボタンが自動場合は無効になる
                        this.casbtn_HaisyutuGyousyaName.Enabled = !bIsAutoMode;
                        //削除ボタンが自動場合は無効になる
                        this.cbtn_HaisyutuGyousyaDel.Enabled = !bIsAutoMode;
                    }
                }

                //排出事業場連携情報
                if (bIsClear)
                {
                    if (!this.ClearEachControl(this.pnl_Hb.Controls))
                    {
                        return false;
                    }
                }
                //this.cantxt_HaisyutuGenbaCd.SetLikedControlsReadOnlyStatus(bIsAutoMode);
                this.Logic.SetHstGenbaLikedControlsReadOnlyStatus(bIsAutoMode);

                //郵便番号参照ボタンが自動場合は無効になる
                this.cbtn_HaisyutuGenbaSan.Enabled = !bIsAutoMode;

                //処分受託者連携情報
                if (bIsClear)
                {
                    if (!this.ClearEachControl(this.pnl_SBN_JYUTAKUSHA.Controls))
                    {
                        return false;
                    }
                }
                //処分受託者連携情報状態の設定
                this.cantxt_SBN_JyutakuShaCD.SetLikedControlsReadOnlyStatus(bIsAutoMode);
                if (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    if (!string.IsNullOrEmpty(this.Logic.ManiInfo.dt_r18.SBN_SHA_NAME))
                    {
                        //処分受託者CDが自動場合はReadOnlyになる
                        this.cantxt_SBN_JyutakuShaCD.ReadOnly = bIsAutoMode;
                        //ポップアップボタンが自動場合は無効になる
                        this.casbtn_SBNGyousyaName.Enabled = !bIsAutoMode;
                        //削除ボタンが自動場合は無効になる
                        this.cbtn_SBNGyousyaDel.Enabled = !bIsAutoMode;
                    }
                }
                //郵便番号参照ボタンが自動場合は無効になる
                this.cbtn_SBNGyousyaSan.Enabled = !bIsAutoMode;

                //処分事業場連携情報
                if (bIsClear)
                {
                    if (!this.ClearEachControl(this.pnl_SBN_Jigyouba.Controls))
                    {
                        return false;
                    }
                }
                //処分事業場連携情報状態の設定
                this.cantxt_SBN_Genba_CD.SetLikedControlsReadOnlyStatus(bIsAutoMode);
                if (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    string LastUpnGenbaName = string.Empty;
                    if (this.Logic.ManiInfo.lstDT_R19.Count > 0)
                    {
                        LastUpnGenbaName = this.Logic.ManiInfo.lstDT_R19[this.Logic.ManiInfo.lstDT_R19.Count - 1].UPNSAKI_JOU_NAME;
                    }
                    if (!string.IsNullOrEmpty(LastUpnGenbaName))
                    {
                        //処分事業場CDが自動場合はReadOnlyになる
                        this.cantxt_SBN_Genba_CD.ReadOnly = bIsAutoMode;
                        //ポップアップボタンが自動場合は無効になる
                        this.casbtn_SBNGenbaName.Enabled = !bIsAutoMode;
                        //削除ボタンが自動場合は無効になる
                        this.cbtn_SBNGenbaDel.Enabled = !bIsAutoMode;
                    }
                }
                //郵便番号参照ボタンが自動場合は無効になる
                this.cbtn_SBNGenbaSan.Enabled = !bIsAutoMode;

                //処分報告エリア
                if (bIsClear)
                {
                    if (!this.ClearEachControl(this.pnl_SBN_ReportInfo.Controls))
                    {
                        return false;
                    }
                }
                this.isEnabled_SBN_ReportInfo = !bIsAutoMode;
                this.SetSbnReportInfoReadOnly(bIsAutoMode);

                //報告区分
                if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.cntxt_HoukokuKBN.Text = "1";
                }
                //運搬報告エリア
                if (bIsClear)
                {
                    if (!this.ClearEachControl(this.pnl_UnpanReportInfo.Controls))
                    {
                        return false;
                    }
                }
                //運搬報告エリア無効有効の設定
                this.isEnabled_UnpanReportInfo = !bIsAutoMode;
                this.SetUnpanReportInfoReadOnly(bIsAutoMode);

                //グリッド関連項目の設定
                this.SetDgvColumnReadOnlyByInputMode(bIsAutoMode, bIsClear);

                //JWNET登録ボタンの表示名の設定
                BusinessBaseForm parentform = (BusinessBaseForm)this.Parent;
                parentform.bt_func9.Text = bIsAutoMode ? "[F9]\nJWNET送信" : "[F9]\n保存";
                parentform.bt_func10.Enabled = bIsAutoMode;
                parentform.bt_func10.Text = bIsAutoMode ? "[F10]\n保留保存" : string.Empty;


                //共通設定
                //排出事業者加入者番号ReadOnly
                this.ctxt_Haisyutu_KanyushaNo.ReadOnly = true;
                //排出事業場番号
                this.ctxt_JIGYOUJYOU_CD.ReadOnly = true;
                //処分受託者の加入者番号をReadOnly
                this.ctxt_SBN_KanyuShaNo.ReadOnly = true;
                //処分受託者の許可番号をReadOnly
                this.ctxt_SBN_KyokaNo.ReadOnly = true;
                //処分事業場番号をReadOnly
                this.ctxt_SBN_JIGYOUJYOU_CD.ReadOnly = true;
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetControlReadOnlyByInput_KBN", ex);
                this.Logic.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

        }
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }

        #region メソッド

        /// <summary>
        /// Entityから画面に反映処理
        /// </summary>
        /// <param name="ManiInfo"></param>
        public void SetFormFromEntity(DenshiManifestInfoCls ManiInfo)
        {
            if (ManiInfo == null) return;
            if (ManiInfo.dt_r18 == null) return;

            this.existAllLastSbnInfo = false;

            //入力区分[手動自動]
            //パタン呼出場合、入力区分変更しない
            if (!ManiInfo.bIsFromPattern)
            {
                //パタン呼出しない場合は、入力区分を設定する
                this.cntxt_InputKBN.Text = (ManiInfo.dt_mf_toc.KIND == 5) ? "2" : "1";
            }
            else
            {
                if (this.Logic.ManiPtnInfo.lstDT_PT_R18_EX.Count != 0)
                {
                    // 入力区分
                    this.cntxt_InputKBN.Text = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[0].INPUT_KBN.ToString();
                }
            }

            //入力モードを設定
            ManiInfo.bIsAutoMode = (this.cntxt_InputKBN.Text == "1");

            //パタンロード場合は各種グリッドの行をすべてクリアする
            if (ManiInfo.bIsFromPattern)
            {
                this.SetDgvColumnReadOnlyByInputMode(ManiInfo.bIsAutoMode, true);
            }

            //マニフェスト区分
            //手動場合はマニ登録固定
            this.cntxt_ManiKBN.Text = !ManiInfo.bIsAutoMode ? "2" : Convert.ToString(ManiInfo.dt_r18.MANIFEST_KBN);
            //修正モードでマニから予約に変更不可
            if (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                this.cntxt_ManiKBN.ReadOnly = (this.cntxt_ManiKBN.Text == "2");
                this.crdbtn_ManiKBN1.Enabled = !this.cntxt_ManiKBN.ReadOnly;
                this.crdbtn_ManiKBN2.Enabled = !this.cntxt_ManiKBN.ReadOnly;
            }
            //削除、参照モードで無効になる
            else if (this.Logic.Mode != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.cntxt_ManiKBN.ReadOnly = true;
                this.crdbtn_ManiKBN1.Enabled = !this.cntxt_ManiKBN.ReadOnly;
                this.crdbtn_ManiKBN2.Enabled = !this.cntxt_ManiKBN.ReadOnly;
            }

            //予約修正権限
            this.cntxt_ModifyRight.Text = (!ManiInfo.bIsAutoMode) ? "1" : Convert.ToString(ManiInfo.dt_r18.KENGEN_CODE);
            //引渡日
            if (ManiInfo.dt_r18.HIKIWATASHI_DATE != null && !string.IsNullOrWhiteSpace(ManiInfo.dt_r18.HIKIWATASHI_DATE))
            {
                this.cdate_HikiwataDate.Value = DateTime.ParseExact(ManiInfo.dt_r18.HIKIWATASHI_DATE,
                                                                    "yyyyMMdd",
                                                                    System.Globalization.CultureInfo.InvariantCulture);
            }
            //引渡担当者
            this.ctxt_HikiwataTantouSha.Text = ManiInfo.dt_r18.HIKIWATASHI_TAN_NAME;
            //登録担当者
            this.ctxt_TourokuTantouSha.Text = ManiInfo.dt_r18.REGI_TAN;
            //マニフェスト番号
            this.cantxt_ManifestNo.Text = ManiInfo.dt_r18.MANIFEST_ID;

            //連絡番号の設定
            foreach (var dt_r05 in ManiInfo.lstDT_R05)
            {
                if (dt_r05.RENRAKU_ID_NO.IsNull)
                {
                    continue;
                }

                if (dt_r05.RENRAKU_ID_NO == 1M)
                {
                    //連絡番号１の設定
                    this.ctxt_RenlakuNo1.Text = dt_r05.RENRAKU_ID;
                }
                else if (dt_r05.RENRAKU_ID_NO == 2M)
                {
                    //連絡番号２の設定
                    this.ctxt_RenlakuNo2.Text = dt_r05.RENRAKU_ID;
                }
                else if (dt_r05.RENRAKU_ID_NO == 3M)
                {
                    //連絡番号３の設定
                    this.ctxt_RenlakuNo3.Text = dt_r05.RENRAKU_ID;
                }
            }

            //排出事業者情報
            //排出事業者CD[基本拡張DT_R18_EX]
            this.cantxt_HaisyutuGyousyaCd.Text = (ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.HST_GYOUSHA_CD;
            //排出事業者名前
            this.ctxt_HaisyutuGyousyaName.Text = ManiInfo.dt_r18.HST_SHA_NAME;
            //排出事業者-郵便番号
            this.cnt_HaisyutuGyousyaZip.Text = ManiInfo.dt_r18.HST_SHA_POST;
            //排出事業者-電話番号
            this.cnt_HaisyutuGyousyaTel.Text = ManiInfo.dt_r18.HST_SHA_TEL;
            //排出事業者-住所
            this.ctxt_HaisyutuGyousyaAddr.Text = ManiInfo.dt_r18.HST_SHA_ADDRESS1 + ManiInfo.dt_r18.HST_SHA_ADDRESS2 + ManiInfo.dt_r18.HST_SHA_ADDRESS3 + ManiInfo.dt_r18.HST_SHA_ADDRESS4;
            //排出事業者-加入者番号
            this.ctxt_Haisyutu_KanyushaNo.Text = ManiInfo.dt_r18.HST_SHA_EDI_MEMBER_ID;
            //排出事業者CDの退避された加入者番号を設定する
            this.cantxt_HaisyutuGyousyaCd.CheckOK_KanyushaCD = ManiInfo.dt_r18.HST_SHA_EDI_MEMBER_ID;
            //排出事業場情報
            //排出事業場CD[基本拡張DT_R18_EX]
            this.cantxt_HaisyutuGenbaCd.Text = (ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.HST_GENBA_CD;
            //排出事業場名前
            this.ctxt_HaisyutuGenbaName.Text = ManiInfo.dt_r18.HST_JOU_NAME;
            //排出事業場-郵便番号
            this.ctxt_Haisyutu_GenbaZip.Text = ManiInfo.dt_r18.HST_JOU_POST_NO;
            //排出事業場-電話番号
            this.cnt_HaisyutuGenbaTel.Text = ManiInfo.dt_r18.HST_JOU_TEL;
            //排出事業場-住所
            this.ctxt_HaisyutuGenbaAddr.Text = ManiInfo.dt_r18.HST_JOU_ADDRESS1 + ManiInfo.dt_r18.HST_JOU_ADDRESS2 + ManiInfo.dt_r18.HST_JOU_ADDRESS3 + ManiInfo.dt_r18.HST_JOU_ADDRESS4;
            //排出事業場-事業場番号
            if (!string.IsNullOrEmpty(this.cantxt_HaisyutuGenbaCd.Text))
            {
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                dto.EDI_MEMBER_ID = ManiInfo.dt_r18.HST_SHA_EDI_MEMBER_ID;
                dto.GYOUSHA_CD = this.cantxt_HaisyutuGyousyaCd.Text;
                dto.GENBA_CD = this.cantxt_HaisyutuGenbaCd.Text;
                dto.HST_KBN = "1";
                dto.JIGYOUJOU_FLG = "1";
                // 事業場名称
                dto.JIGYOUJOU_NAME = this.ctxt_HaisyutuGenbaName.Text;
                // 事業場住所
                dto.JIGYOUJOU_ADDRESS = this.ctxt_HaisyutuGenbaAddr.Text;

                DataTable dt = this.MasterLogic.GetDenshiGenbaData(dto);
                if (dt.Rows.Count == 1)
                {
                    this.ctxt_JIGYOUJYOU_CD.Text = dt.Rows[0]["JIGYOUJOU_CD"].ToString();
                }
            }
            //排出事業場CDの退避されたチェックOK加入者番号を設定する
            this.cantxt_HaisyutuGenbaCd.CheckOK_KanyushaCD = ManiInfo.dt_r18.HST_SHA_EDI_MEMBER_ID;

            // 修正モードの呼び出しの場合はlstDT_PT_R18_EXを参照させない(エラーする)
            bool PtnChk = false;
            if (this.Logic.ManiPtnInfo != null)
            {
                if (this.Logic.ManiPtnInfo.lstDT_PT_R18_EX.Count != 0)
                {
                    PtnChk = true;
                }
            }

            // DT_PT_R18_EXのレコード件数が0件の場合：既存処理
            if (!PtnChk)
            {
                if (0 == this.cdgv_Haikibutu.Rows.Count)
                {
                    this.cdgv_Haikibutu.Rows.Add();
                }

                //廃棄物情報
                //廃棄物種類CD
                this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_SHURUI_CD"].Value = ManiInfo.dt_r18.HAIKI_DAI_CODE + ManiInfo.dt_r18.HAIKI_CHU_CODE + ManiInfo.dt_r18.HAIKI_SHO_CODE + ManiInfo.dt_r18.HAIKI_SAI_CODE;
                //廃棄物種類名称
                this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_SHURUI_NAME"].Value = ManiInfo.dt_r18.HAIKI_SHURUI;
                //廃棄物名称CD[基本拡張DT_R18_EX]
                this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_NAME_CD"].Value = (ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.HAIKI_NAME_CD;
                //廃棄物名称
                this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_NAME"].Value = ManiInfo.dt_r18.HAIKI_NAME;
                //数量
                if (!ManiInfo.dt_r18.HAIKI_SUU.IsNull)
                    this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_SUU"].Value = ManiInfo.dt_r18.HAIKI_SUU;
                //単位
                this.cdgv_Haikibutu.Rows[0].Cells["UNIT_CD"].Value = (ManiInfo.dt_r18.HAIKI_UNIT_CODE == null) ? null : this.Logic.PadLeftUnitCd(ManiInfo.dt_r18.HAIKI_UNIT_CODE.ToString());
                //単位名称
                this.cdgv_Haikibutu.Rows[0].Cells["UNIT_NAME"].Value = this.MasterLogic.GetCodeNameFromMasterInfo(WINDOW_ID.M_UNIT, ManiInfo.dt_r18.HAIKI_UNIT_CODE);
                //換算値[基本拡張DT_R18_EX]
                if (ManiInfo.dt_r18ExOld != null)
                {
                    if (!ManiInfo.dt_r18ExOld.KANSAN_SUU.IsNull)
                    {
                        this.cdgv_Haikibutu.Rows[0].Cells["KANSAN_SUU"].Value = (decimal)ManiInfo.dt_r18ExOld.KANSAN_SUU;
                    }
                    else
                    {
                        this.cdgv_Haikibutu.Rows[0].Cells["KANSAN_SUU"].Value = 0;
                    }
                    //減容量[基本拡張DT_R18_EX]
                    if (!ManiInfo.dt_r18ExOld.GENNYOU_SUU.IsNull)
                    {
                        this.cdgv_Haikibutu.Rows[0].Cells["GENNYOU_SUU"].Value = (decimal)ManiInfo.dt_r18ExOld.GENNYOU_SUU;
                    }
                    else
                    {
                        this.cdgv_Haikibutu.Rows[0].Cells["GENNYOU_SUU"].Value = 0;
                    }
                }
                //荷姿CD
                this.cdgv_Haikibutu.Rows[0].Cells["NISUGATA_CD"].Value = ManiInfo.dt_r18.NISUGATA_CODE;
                //荷姿名
                this.cdgv_Haikibutu.Rows[0].Cells["NISUGATA_NAME"].Value = ManiInfo.dt_r18.NISUGATA_NAME;
                //荷姿数量
                this.cdgv_Haikibutu.Rows[0].Cells["NISUGATA_SUU"].Value = ManiInfo.dt_r18.NISUGATA_SUU;
                //数量確定者CD
                this.cdgv_Haikibutu.Rows[0].Cells["SUU_KAKUTEI_CODE"].Value = ManiInfo.dt_r18.SUU_KAKUTEI_CODE;
                //数量確定者名称
                this.cdgv_Haikibutu.Rows[0].Cells["SUU_KAKUTEI_NAME"].Value = this.Logic.GetKakuteishaName(ManiInfo.dt_r18.SUU_KAKUTEI_CODE);
                //有害物質情報
                if (ManiInfo.lstDT_R02.Count > 0)
                {
                    //有害物質CD1
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE1"].Value = ManiInfo.lstDT_R02[0].YUUGAI_CODE;
                    //有害物質名称1
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_NAME1"].Value = ManiInfo.lstDT_R02[0].YUUGAI_NAME;

                    if (ManiInfo.lstDT_R02.Count > 1)
                    {
                        //有害物質CD2
                        this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE2"].Value = ManiInfo.lstDT_R02[1].YUUGAI_CODE;
                        //有害物質名称2
                        this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_NAME2"].Value = ManiInfo.lstDT_R02[1].YUUGAI_NAME;
                    }
                    if (ManiInfo.lstDT_R02.Count > 2)
                    {
                        //有害物質CD3
                        this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE3"].Value = ManiInfo.lstDT_R02[2].YUUGAI_CODE;
                        //有害物質名称3
                        this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_NAME3"].Value = ManiInfo.lstDT_R02[2].YUUGAI_NAME;
                    }
                    if (ManiInfo.lstDT_R02.Count > 3)
                    {
                        //有害物質CD4
                        this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE4"].Value = ManiInfo.lstDT_R02[3].YUUGAI_CODE;
                        //有害物質名称4
                        this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_NAME4"].Value = ManiInfo.lstDT_R02[3].YUUGAI_NAME;
                    }
                    if (ManiInfo.lstDT_R02.Count > 4)
                    {
                        //有害物質CD5
                        this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE5"].Value = ManiInfo.lstDT_R02[4].YUUGAI_CODE;
                        //有害物質名称5
                        this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_NAME5"].Value = ManiInfo.lstDT_R02[4].YUUGAI_NAME;
                    }
                    if (ManiInfo.lstDT_R02.Count > 5)
                    {
                        //有害物質CD6
                        this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE6"].Value = ManiInfo.lstDT_R02[5].YUUGAI_CODE;
                        //有害物質名称6
                        this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_NAME6"].Value = ManiInfo.lstDT_R02[5].YUUGAI_NAME;
                    }
                }

                // 有害物質項目の活性/非活性を制御
                if (this.CheckYuugaiColumnReadOnly(0))
                {
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE1"].ReadOnly = false;
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE2"].ReadOnly = false;
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE3"].ReadOnly = false;
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE4"].ReadOnly = false;
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE5"].ReadOnly = false;
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE6"].ReadOnly = false;
                }
                else
                {
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE1"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE2"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE3"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE4"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE5"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["YUUGAI_CODE6"].ReadOnly = true;
                }

                //廃棄物情報/参照モードの背景色設定
                if (this.Logic.Mode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_SHURUI_CD"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_NAME_CD"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_SUU"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["UNIT_CD"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["KANSAN_SUU"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["NISUGATA_CD"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["NISUGATA_SUU"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[0].Cells["SUU_KAKUTEI_CODE"].ReadOnly = true;
                }
            }
            // DT_PT_R18_EXのレコード件数が1件以上の場合：DT_PT_R18_EXから設定する。
            else
            {
                // 廃棄物情報に行を追加する。
                for (int i = 0; i < this.Logic.ManiPtnInfo.lstDT_PT_R18_EX.Count; i++)
                {
                    if (i != 0)
                    {
                        this.cdgv_Haikibutu.Rows.Add();
                    }
                }

                for (int i = 0; i < this.Logic.ManiPtnInfo.lstDT_PT_R18_EX.Count; i++)
                {
                    //廃棄物情報
                    //廃棄物種類CD
                    this.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SHURUI_CD"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].HAIKI_SHURUI_CODE;
                    //廃棄物種類名称
                    this.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SHURUI_NAME"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].HAIKI_SHURUI_NAME;
                    //廃棄物名称CD
                    this.cdgv_Haikibutu.Rows[i].Cells["HAIKI_NAME_CD"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].HAIKI_NAME_CODE;
                    //廃棄物名称
                    this.cdgv_Haikibutu.Rows[i].Cells["HAIKI_NAME"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].HAIKI_NAME;
                    //数量
                    if (!this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].HAIKI_SUU.IsNull)
                        this.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SUU"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].HAIKI_SUU;
                    //単位
                    this.cdgv_Haikibutu.Rows[i].Cells["UNIT_CD"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].UNIT_CODE;
                    //単位名称
                    this.cdgv_Haikibutu.Rows[i].Cells["UNIT_NAME"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].UNIT_NAME;
                    // 換算後数量
                    if (!this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].KANSAN_SUU.IsNull)
                        this.cdgv_Haikibutu.Rows[i].Cells["KANSAN_SUU"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].KANSAN_SUU;
                    // 減容後数量
                    if (!this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].GENNYOU_SUU.IsNull) 
                        this.cdgv_Haikibutu.Rows[i].Cells["GENNYOU_SUU"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].GENNYOU_SUU;
                    //荷姿CD
                    this.cdgv_Haikibutu.Rows[i].Cells["NISUGATA_CD"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].NISUGATA_CODE;
                    //荷姿名
                    this.cdgv_Haikibutu.Rows[i].Cells["NISUGATA_NAME"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].NISUGATA_NAME;
                    //荷姿数量
                    this.cdgv_Haikibutu.Rows[i].Cells["NISUGATA_SUU"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].NISUGATA_SUU;
                    //数量確定者CD
                    this.cdgv_Haikibutu.Rows[i].Cells["SUU_KAKUTEI_CODE"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].SUU_KAKUTEI_CODE;
                    //数量確定者名称
                    this.cdgv_Haikibutu.Rows[i].Cells["SUU_KAKUTEI_NAME"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].SUU_KAKUTEI_NAME;
                    //有害物質CD1
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE1"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_CODE1;
                    //有害物質名称1
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_NAME1"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_NAME1;
                    //有害物質CD2
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE2"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_CODE2;
                    //有害物質名称2
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_NAME2"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_NAME2;
                    //有害物質CD3
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE3"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_CODE3;
                    //有害物質名称3
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_NAME3"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_NAME3;
                    //有害物質CD4
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE4"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_CODE4;
                    //有害物質名称4
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_NAME4"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_NAME4;
                    //有害物質CD5
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE5"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_CODE5;
                    //有害物質名称5
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_NAME5"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_NAME5;
                    //有害物質CD6
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE6"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_CODE6;
                    //有害物質名称6
                    this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_NAME6"].Value = this.Logic.ManiPtnInfo.lstDT_PT_R18_EX[i].YUUGAI_NAME6;

                    // 有害物質項目の活性/非活性を制御
                    if (this.CheckYuugaiColumnReadOnly(i))
                    {
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE1"].ReadOnly = false;
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE2"].ReadOnly = false;
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE3"].ReadOnly = false;
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE4"].ReadOnly = false;
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE5"].ReadOnly = false;
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE6"].ReadOnly = false;
                    }
                    else
                    {
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE1"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE2"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE3"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE4"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE5"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE6"].ReadOnly = true;
                    }

                    //廃棄物情報/参照モードの背景色設定
                    if (this.Logic.Mode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                    {
                        this.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SHURUI_CD"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["HAIKI_NAME_CD"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SUU"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["UNIT_CD"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["KANSAN_SUU"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["NISUGATA_CD"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["NISUGATA_SUU"].ReadOnly = true;
                        this.cdgv_Haikibutu.Rows[i].Cells["SUU_KAKUTEI_CODE"].ReadOnly = true;
                    }
                }
            }

            //最終処分の場所 （予定）情報
            if ("1".Equals(ManiInfo.dt_r18.LAST_SBN_JOU_KISAI_FLAG))
            {
                //当欄指定のとおり
                this.ccbx_Toulanshitei.Checked = true;
            }
            else if ("0".Equals(ManiInfo.dt_r18.LAST_SBN_JOU_KISAI_FLAG))
            {
                //委託契約書記載のとおり
                this.ccbx_YitakuKeyaku.Checked = true;
            }

            for (int i = 0; i < ManiInfo.lstDT_R04.Count; i++)
            {
                //行の追加
                this.cdgv_LastSBNbasyo_yotei.Rows.Add();
                this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["No"].Value = i + 1;
                //最終処分業者情報
                if (ManiInfo.lstDT_R04_EX.Count > 0)//拡張データある場合及び元データが行数量同じ前提条件で
                {
                    if (ManiInfo.lstDT_R04_EX.Count == ManiInfo.lstDT_R04.Count)
                    {
                        //最終処分業者CD[最終処分の場所 （予定）拡張DT_R04_EX]
                        this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_GYOUSHA_CD"].Value = ManiInfo.lstDT_R04_EX[i].LAST_SBN_GYOUSHA_CD;
                        //最終処分業者名称と加入者番号を取得
                        DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                        if (!string.IsNullOrEmpty(ManiInfo.lstDT_R04_EX[i].LAST_SBN_GYOUSHA_CD))
                        {
                            dto.GYOUSHA_CD = ManiInfo.lstDT_R04_EX[i].LAST_SBN_GYOUSHA_CD;
                            dto.SBN_KBN = "1";
                            dto.ISNOT_NEED_DELETE_FLG = true;
                            DataTable dt = this.MasterLogic.GetDenshiGyoushaData(dto);
                            if (dt.Rows.Count == 1)
                            {
                                //最終処分業者名称
                                this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_GYOUSHA_NAME"].Value = dt.Rows[0]["JIGYOUSHA_NAME"].ToString();
                                //最終処分加入者番号が暗黙なカラムにを保存する
                                this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["Sbn_KanyushaCD"].Value = dt.Rows[0]["EDI_MEMBER_ID"].ToString();
                            }
                        }
                        //最終処分業場CD
                        this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_CD"].Value = ManiInfo.lstDT_R04_EX[i].LAST_SBN_GENBA_CD;
                    }
                }
                //最終処分業場名称
                this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_NAME"].Value = ManiInfo.lstDT_R04[i].LAST_SBN_JOU_NAME;
                //最終処分業場の郵便番号
                this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_POST"].Value = ManiInfo.lstDT_R04[i].LAST_SBN_JOU_POST;
                //最終処分業場の住所
                this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_ADDRESS"].Value = ManiInfo.lstDT_R04[i].LAST_SBN_JOU_ADDRESS1 +
                    ManiInfo.lstDT_R04[i].LAST_SBN_JOU_ADDRESS2 + ManiInfo.lstDT_R04[i].LAST_SBN_JOU_ADDRESS3 + ManiInfo.lstDT_R04[i].LAST_SBN_JOU_ADDRESS4;
                //最終処分業場の電話番号
                this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_TEL"].Value = ManiInfo.lstDT_R04[i].LAST_SBN_JOU_TEL;

            }
            //収集運搬情報
            for (int i = 0; i < ManiInfo.lstDT_R19.Count; i++)
            {
                //行の追加
                this.cdgv_UnpanInfo.Rows.Add();
                this.cdgv_UnpanInfo.Rows[i].Cells["UPN_ROUTE_NO"].Value = i + 1;//区間番号
                //運搬報告情報クラス利用
                UnpanHoukokuDataDTOCls UnpanRepInfo = new UnpanHoukokuDataDTOCls();
                this.cdgv_UnpanInfo.Rows[i].Tag = UnpanRepInfo;//タグに保存する
                //グリッドの内容を設定
                //収集運搬加入者番号
                if ((ManiInfo.lstDT_R19[i].UPN_SHA_EDI_MEMBER_ID == ConstCls.NO_REP_SBN_EDI_MEMBER_ID)
                && (false == string.IsNullOrEmpty(ManiInfo.lstDT_R19_EX[i].NO_REP_UPN_EDI_MEMBER_ID)))
                {
                    // 収集運搬業者加入者番号が報告不要業者(EDI_MEMBER_ID='0000000')、
                    // かつ、拡張情報に報告不要収集運搬業者加入者番号が登録されていれば、そちらを用いる
                    this.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_EDI_MEMBER_ID"].Value = ManiInfo.lstDT_R19_EX[i].NO_REP_UPN_EDI_MEMBER_ID;
                }
                else
                {
                    // 上記以外は収集運搬業者加入者番号を用いる
                    this.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_EDI_MEMBER_ID"].Value = ManiInfo.lstDT_R19[i].UPN_SHA_EDI_MEMBER_ID;
                }
                //運搬先業者加入者番号
                if (ConstCls.NO_REP_SBN_EDI_MEMBER_ID.Equals(ManiInfo.lstDT_R19[i].UPNSAKI_EDI_MEMBER_ID))
                {
                    if ((String.IsNullOrEmpty(ManiInfo.lstDT_R19_EX[i].NO_REP_UPNSAKI_EDI_MEMBER_ID)) || ConstCls.NO_REP_SBN_EDI_MEMBER_ID.Equals(ManiInfo.lstDT_R19_EX[i].NO_REP_UPNSAKI_EDI_MEMBER_ID))
                    {
                        if (ManiInfo.lstDT_R19_EX.Count > 0)
                        {
                            this.cdgv_UnpanInfo.Rows[i].Cells["Unpansaki_KanyushaCD"].Value = ConstCls.NO_REP_SBN_EDI_MEMBER_ID;
                        }
                    }
                    else
                    {
                        this.cdgv_UnpanInfo.Rows[i].Cells["Unpansaki_KanyushaCD"].Value = ManiInfo.lstDT_R19_EX[i].NO_REP_UPNSAKI_EDI_MEMBER_ID;
                    }
                }
                else
                {
                    this.cdgv_UnpanInfo.Rows[i].Cells["Unpansaki_KanyushaCD"].Value = ManiInfo.lstDT_R19[i].UPNSAKI_EDI_MEMBER_ID;
                }

                //[拡張DT_R19_EX]データある場合
                if (ManiInfo.lstDT_R19_EX.Count > 0)//拡張データある場合及び元データが行数量同じ前提条件で
                {
                    if (ManiInfo.lstDT_R19_EX.Count == ManiInfo.lstDT_R19.Count)
                    {
                        //収集運搬業者CD
                        this.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_CD"].Value = ManiInfo.lstDT_R19_EX[i].UPN_GYOUSHA_CD;
                        //運搬先業者CD
                        this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GYOUSHA_CD"].Value = ManiInfo.lstDT_R19_EX[i].UPNSAKI_GYOUSHA_CD;
                        //運搬先事業場CD  
                        this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GENBA_CD"].Value = ManiInfo.lstDT_R19_EX[i].UPNSAKI_GENBA_CD;
                        //運搬担当者CD 
                        this.cdgv_UnpanInfo.Rows[i].Cells["UNPANTAN_CD"].Value = ManiInfo.lstDT_R19_EX[i].UPN_TANTOUSHA_CD;
                        //運搬報告記載の運搬担当者CD
                        UnpanRepInfo.cantxt_UnpanTantoushaCd = ManiInfo.lstDT_R19_EX[i].UPNREP_UPN_TANTOUSHA_CD;
                        //運搬報告記載の報告担当者CD
                        UnpanRepInfo.cantxt_HoukokuTantoushaCD = ManiInfo.lstDT_R19_EX[i].HOUKOKU_TANTOUSHA_CD;
                        //車輌番号名称
                        this.cdgv_UnpanInfo.Rows[i].Cells["CAR_NAME"].Value = ManiInfo.lstDT_R19[i].CAR_NO;
                        //車輌番号CD 
                        this.cdgv_UnpanInfo.Rows[i].Cells["SHARYOU_CD"].Value = ManiInfo.lstDT_R19_EX[i].SHARYOU_CD;
                        //運搬報告記載の車両名称 入力区分：自動の場合はDT_R19.UPNREP_CAR_NOをそのままセットする
                        if (!ManiInfo.dt_mf_toc.KIND.IsNull && (ManiInfo.dt_mf_toc.KIND == 5))
                        {
                            UnpanRepInfo.ctxt_UnpanSyaryoName = this.MasterLogic.GetCodeNameFromMasterInfo(WINDOW_ID.M_SHARYOU, ManiInfo.lstDT_R19[i].UPNREP_CAR_NO, ManiInfo.lstDT_R19_EX[i].UPN_GYOUSHA_CD);
                        }
                        //運搬報告記載の車両CD
                        UnpanRepInfo.cantxt_SyaryoNo = ManiInfo.lstDT_R19_EX[i].UPNREP_SHARYOU_CD;

                        //予約マニ修正）明細自体は入力可能状態にするが
                        //              CDが入力されている収集運搬業者CD/運搬先業者CD/運搬先事業場CDは変更不可の為、ReadOnlyとする
                        if (ManiInfo.dt_r18.MANIFEST_KBN == 1 && this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        {
                            if (!string.IsNullOrEmpty(ManiInfo.lstDT_R19_EX[i].UPN_GYOUSHA_CD))
                            {
                                this.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_CD"].ReadOnly = true;
                            }
                            else
                            {
                                this.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_CD"].ReadOnly = false;
                            }
                            if (!string.IsNullOrEmpty(ManiInfo.lstDT_R19_EX[i].UPNSAKI_GYOUSHA_CD))
                            {
                                this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GYOUSHA_CD"].ReadOnly = true;
                            }
                            else
                            {
                                this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GYOUSHA_CD"].ReadOnly = false;
                            }
                            if (!string.IsNullOrEmpty(ManiInfo.lstDT_R19_EX[i].UPNSAKI_GENBA_CD))
                            {
                                this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GENBA_CD"].ReadOnly = true;
                            }
                            else
                            {
                                this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GENBA_CD"].ReadOnly = false;
                            }
                        }
                    }
                }
                //収集運搬業者名称
                this.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_NAME"].Value = ManiInfo.lstDT_R19[i].UPN_SHA_NAME;
                //運搬先業者名称
                this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GYOUSHA_NAME"].Value = ManiInfo.lstDT_R19[i].UPNSAKI_NAME;
                //運搬先事業場名称
                this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GENBA_NAME"].Value = ManiInfo.lstDT_R19[i].UPNSAKI_JOU_NAME;
                //運搬先事業場番号 
                if (this.cdgv_UnpanInfo.Rows[i].Cells["Unpansaki_KanyushaCD"].Value != null &&
                    this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GENBA_CD"].Value != null)
                {
                    DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                    object tmpobj = this.cdgv_UnpanInfo.Rows[i].Cells["Unpansaki_KanyushaCD"].Value;
                    dto.EDI_MEMBER_ID = tmpobj.ToString();
                    tmpobj = this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GYOUSHA_CD"].Value;
                    if (tmpobj != null
                        && !string.IsNullOrEmpty(tmpobj.ToString()))
                    {
                        dto.GYOUSHA_CD = tmpobj.ToString();
                    }
                    tmpobj = this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GENBA_CD"].Value;
                    dto.GENBA_CD = tmpobj.ToString();

                    // 事業場名称
                    dto.JIGYOUJOU_NAME = ManiInfo.lstDT_R19[i].UPNSAKI_JOU_NAME;
                    // 事業場住所
                    dto.JIGYOUJOU_ADDRESS = (ManiInfo.lstDT_R19[i].UPNSAKI_JOU_ADDRESS1 ?? "") +
                                            (ManiInfo.lstDT_R19[i].UPNSAKI_JOU_ADDRESS2 ?? "") +
                                            (ManiInfo.lstDT_R19[i].UPNSAKI_JOU_ADDRESS3 ?? "") +
                                            (ManiInfo.lstDT_R19[i].UPNSAKI_JOU_ADDRESS4 ?? "");
                    dto.JIGYOUJOU_FLG = "1";
                    dto.ISNOT_NEED_DELETE_FLG = true;
                    DataTable dt = this.MasterLogic.GetDenshiGenbaData(dto);
                    if (dt.Rows.Count == 1)
                    {
                        this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value = dt.Rows[0]["JIGYOUJOU_CD"].ToString();
                    }
                }

                // 運搬承認フラグを上書かないため前回値をセットしておく
                this.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHOUNIN_FLAG"].Value = ManiInfo.lstDT_R19[i].UPN_SHOUNIN_FLAG;

                //運搬方法
                this.cdgv_UnpanInfo.Rows[i].Cells["UPN_WAY_CODE"].Value = ManiInfo.lstDT_R19[i].UPN_WAY_CODE;
                //運搬方法名称
                this.cdgv_UnpanInfo.Rows[i].Cells["UPN_WAY_NAME"].Value = this.MasterLogic.GetCodeNameFromMasterInfo(WINDOW_ID.M_UNPAN_HOUHOU, ManiInfo.lstDT_R19[i].UPN_WAY_CODE, null, true);
                //運搬担当者名称
                this.cdgv_UnpanInfo.Rows[i].Cells["UPN_TAN_NAME"].Value = ManiInfo.lstDT_R19[i].UPN_TAN_NAME;

                //車輌番号名称 
                this.cdgv_UnpanInfo.Rows[i].Cells["CAR_NAME"].Value = ManiInfo.lstDT_R19[i].CAR_NO;
                //運搬終了日
                UnpanRepInfo.cdtp_UnpanEndDate = (ManiInfo.lstDT_R19[i].UPN_END_DATE == "0") ? null : ManiInfo.lstDT_R19[i].UPN_END_DATE;
                //運搬報告記載運搬担当者名称
                UnpanRepInfo.ctxt_UnpanTantoushaName = ManiInfo.lstDT_R19[i].UPNREP_UPN_TAN_NAME;
                //運搬報告記載報告担当者名称
                UnpanRepInfo.ctxt_HoukokuTantoushaName = ManiInfo.lstDT_R19[i].REP_TAN_NAME;
                //運搬量
                if (!ManiInfo.lstDT_R19[i].UPN_SUU.IsNull)
                {
                    //運搬量
                    UnpanRepInfo.cntxt_UnpanRyo = decimal.Parse(ManiInfo.lstDT_R19[i].UPN_SUU.ToString().Replace(",", ""));
                }
                //運搬量の単位CD
                UnpanRepInfo.cantxt_UnpanRyoUnitCd = (ManiInfo.lstDT_R19[i].UPN_UNIT_CODE == null) ? null : this.Logic.PadLeftUnitCd(ManiInfo.lstDT_R19[i].UPN_UNIT_CODE.ToString());
                //運搬量の単位名称
                UnpanRepInfo.ctxt_UnitName = this.MasterLogic.GetCodeNameFromMasterInfo(WINDOW_ID.M_UNIT, ManiInfo.lstDT_R19[i].UPN_UNIT_CODE);

                if (ManiInfo.dt_mf_toc.KIND.IsNull || ManiInfo.dt_mf_toc.KIND != 5)
                {
                    UnpanRepInfo.ctxt_UnpanSyaryoName = ManiInfo.lstDT_R19[i].UPNREP_CAR_NO;
                }
                else
                {
                    UnpanRepInfo.ctxt_UnpanSyaryoName = ManiInfo.lstDT_R19[i].UPNREP_CAR_NO;
                }

                //有価物収集量
                if (!ManiInfo.lstDT_R19[i].YUUKA_SUU.IsNull)
                {
                    //有価物収集量
                    UnpanRepInfo.cntxt_YukabutuRyo = decimal.Parse(ManiInfo.lstDT_R19[i].YUUKA_SUU.ToString().Replace(",", ""));
                }
                //有価物の単位CD
                UnpanRepInfo.cantxt_YukabutuRyoUnitCd = (ManiInfo.lstDT_R19[i].YUUKA_UNIT_CODE == null) ? null : this.Logic.PadLeftUnitCd(ManiInfo.lstDT_R19[i].YUUKA_UNIT_CODE.ToString());
                //有価物の単位名称
                UnpanRepInfo.cantxt_YukabutuRyoUnitName = this.MasterLogic.GetCodeNameFromMasterInfo(WINDOW_ID.M_UNIT, ManiInfo.lstDT_R19[i].YUUKA_UNIT_CODE);
                //備考
                UnpanRepInfo.ctxt_UnpanBikou = ManiInfo.lstDT_R19[i].BIKOU;
            }

            //20151026 hoanghm #13497 start
            if (!this.cdgv_UnpanInfo.ReadOnly)
            {
                // 運搬情報が読み取り専用の時には新規行入力不可なので、AllowUserToAddRowsの値は変更しない。
                this.cdgv_UnpanInfo.AllowUserToAddRows = !(cdgv_UnpanInfo.Rows.Count > 5);
            }
            //20151026 hoanghm #13497 end

            // 紐付済みの場合、運搬終了日が変更されると紐付情報が解除される。
            // その対策としてマニ紐付がされている場合は、運搬終了日を読み取り専用(ReadOnly=tru)にする。
            if (ManiInfo.dt_r18ExOld != null
                && !ManiInfo.dt_r18ExOld.SYSTEM_ID.IsNull
                && this.Logic.ChkRelation(ManiInfo.dt_r18ExOld.SYSTEM_ID, this.Logic.maniFlag)
                && this.Logic.maniFlag == 1 && (!ManiInfo.dt_mf_toc.KIND.IsNull && ManiInfo.dt_mf_toc.KIND == 5))
            {
                // マニ紐付がされており、入力区分：手動の一次マニの場合
                this.cdtp_UnpanEndDate.ReadOnly = true;
                this.cdtp_SBNEndDate.ReadOnly = true;
            }

            //運搬報告データが画面に設定する
            this.cdgv_UnpanInfo_SelectionChanged(cdgv_UnpanInfo, new EventArgs());
            //処分受託者情報
            //処分受託者CD[基本拡張DT_R18_EX]
            this.cantxt_SBN_JyutakuShaCD.Text = (ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.SBN_GYOUSHA_CD;
            //処分受託者名前
            this.ctxt_SBN_JyutakuShaName.Text = ManiInfo.dt_r18.SBN_SHA_NAME;
            //処分受託者-郵便番号
            this.ctxt_SBN_GyouShaPost.Text = ManiInfo.dt_r18.SBN_SHA_POST;
            //処分受託者-電話番号
            this.ctxt_SBN_GyouShaTel.Text = ManiInfo.dt_r18.SBN_SHA_TEL;
            //処分受託者-住所
            this.ctxt_SBN_GyouShaAddr.Text = ManiInfo.dt_r18.SBN_SHA_ADDRESS1 + ManiInfo.dt_r18.SBN_SHA_ADDRESS2 + ManiInfo.dt_r18.SBN_SHA_ADDRESS3 + ManiInfo.dt_r18.SBN_SHA_ADDRESS4;

            //処分受託者-加入者番号
            if ((ManiInfo.dt_r18.SBN_SHA_MEMBER_ID == ConstCls.NO_REP_SBN_EDI_MEMBER_ID)
            && (false == string.IsNullOrEmpty((ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.NO_REP_SBN_EDI_MEMBER_ID)))
            {
                // 処分業者加入者番号が報告不要業者(EDI_MEMBER_ID='0000000')、
                // かつ、拡張情報に報告不要処分事業者加入者番号が登録されていれば、そちらを用いる
                this.ctxt_SBN_KanyuShaNo.Text = ManiInfo.dt_r18ExOld.NO_REP_SBN_EDI_MEMBER_ID;
            }
            else
            {
                // 上記以外は処分業者加入者番号を用いる
                this.ctxt_SBN_KanyuShaNo.Text = ManiInfo.dt_r18.SBN_SHA_MEMBER_ID;
            }

            //処分業者統一許可番号
            this.ctxt_SBN_KyokaNo.Text = ManiInfo.dt_r18.SBN_SHA_KYOKA_ID;

            //処分事業者CDの退避された加入者番号を設定する
            this.cantxt_SBN_JyutakuShaCD.CheckOK_KanyushaCD = this.ctxt_SBN_KanyuShaNo.Text;

            //処分事業場情報
            //処分事業場CD[基本拡張DT_R18_EX]
            this.cantxt_SBN_Genba_CD.Text = (ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.SBN_GENBA_CD;
            //処分事業場CDの退避された加入者番号を設定する
            this.cantxt_SBN_Genba_CD.CheckOK_KanyushaCD = this.cantxt_SBN_JyutakuShaCD.CheckOK_KanyushaCD;
            //最終運搬区間の運搬先事業場情報より、処分事業場情報を設定する
            if (ManiInfo.lstDT_R19.Count > 0)
            {
                //処分事業場名前
                this.ctxt_SBN_Genba_Name.Text = ManiInfo.lstDT_R19[ManiInfo.lstDT_R19.Count - 1].UPNSAKI_JOU_NAME;
                //処分事業場-郵便番号
                this.ctxt_SBN_GenbaPost.Text = ManiInfo.lstDT_R19[ManiInfo.lstDT_R19.Count - 1].UPNSAKI_JOU_POST;
                //処分事業場-電話番号
                this.ctxt_SBN_GenbaTel.Text = ManiInfo.lstDT_R19[ManiInfo.lstDT_R19.Count - 1].UPNSAKI_JOU_TEL;
                //処分事業場-住所
                this.SBN_GenbaAddr.Text = ManiInfo.lstDT_R19[ManiInfo.lstDT_R19.Count - 1].UPNSAKI_JOU_ADDRESS1 +
                    ManiInfo.lstDT_R19[ManiInfo.lstDT_R19.Count - 1].UPNSAKI_JOU_ADDRESS2 +
                    ManiInfo.lstDT_R19[ManiInfo.lstDT_R19.Count - 1].UPNSAKI_JOU_ADDRESS3 +
                    ManiInfo.lstDT_R19[ManiInfo.lstDT_R19.Count - 1].UPNSAKI_JOU_ADDRESS4;
                //処分事業場-事業場番号
                //this.ctxt_SBN_JIGYOUJYOU_CD.Text = ManiInfo.lstDT_R19[ManiInfo.lstDT_R19.Count - 1].UPNSAKI_JOU_ID.IsNull?string.Empty:
                //    ManiInfo.lstDT_R19[ManiInfo.lstDT_R19.Count - 1].UPNSAKI_JOU_ID.ToString();
                if (!string.IsNullOrEmpty(this.cantxt_SBN_Genba_CD.Text))
                {
                    DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                    dto.EDI_MEMBER_ID = ctxt_SBN_KanyuShaNo.Text;
                    dto.GYOUSHA_CD = this.cantxt_SBN_JyutakuShaCD.Text;
                    dto.GENBA_CD = this.cantxt_SBN_Genba_CD.Text;
                    dto.SBN_KBN = "1";
                    // 事業場名称
                    dto.JIGYOUJOU_NAME = this.ctxt_SBN_Genba_Name.Text;
                    // 事業場住所
                    dto.JIGYOUJOU_ADDRESS = this.SBN_GenbaAddr.Text;

                    dto.JIGYOUJOU_FLG = "1";
                    dto.ISNOT_NEED_DELETE_FLG = true;
                    DataTable dt = this.MasterLogic.GetDenshiGenbaData(dto);
                    if (dt.Rows.Count == 1)
                    {
                        this.ctxt_SBN_JIGYOUJYOU_CD.Text = dt.Rows[0]["JIGYOUJOU_CD"].ToString();
                    }
                    else if (!ManiInfo.dt_r18.UPN_ROUTE_CNT.IsNull
                        && ManiInfo.dt_r18.UPN_ROUTE_CNT == ManiInfo.lstDT_R19.Count
                        && !ManiInfo.lstDT_R19[(int)ManiInfo.dt_r18.UPN_ROUTE_CNT - 1].UPNSAKI_JOU_ID.IsNull)
                    {
                        this.ctxt_SBN_JIGYOUJYOU_CD.Text = ManiInfo.lstDT_R19[(int)ManiInfo.dt_r18.UPN_ROUTE_CNT - 1].UPNSAKI_JOU_ID.ToString().PadLeft(10, '0');
                    }
                }
            }

            //予約マニ修正）運搬情報の明細自体は入力可能にするが、
            //              入力されている処分受託者情報は変更不可の為、登録時にチェックを掛ける為、情報を保持しておく
            if (ManiInfo.dt_r18.MANIFEST_KBN == 1 && this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                yoyaku_shobun_gyousyaCd = this.cantxt_SBN_JyutakuShaCD.Text;
                yoyaku_shobun_genbaCd = this.cantxt_SBN_Genba_CD.Text;
            }

            //処分方法CD
            if (!ManiInfo.dt_r18.SBN_WAY_CODE.IsNull)
            {
                this.cantxt_SBN_houhouCD.Text = ManiInfo.dt_r18.SBN_WAY_CODE.ToString();
            }
            //処分方法名称
            this.ctxt_SBN_houhouName.Text = ManiInfo.dt_r18.SBN_WAY_NAME;
            //将軍処分方法CD
            this.cantxt_Shogun_SBN_houhouCD.Text = (ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.SBN_HOUHOU_CD;
            //将軍処分方法名称
            if (ManiInfo.dt_r18ExOld != null)
            {
                var dao = DaoInitUtility.GetComponent<IM_SHOBUN_HOUHOUDao>();
                M_SHOBUN_HOUHOU shobunCond = new M_SHOBUN_HOUHOU();
                shobunCond.SHOBUN_HOUHOU_CD = ManiInfo.dt_r18ExOld.SBN_HOUHOU_CD;

                if (string.IsNullOrEmpty(shobunCond.SHOBUN_HOUHOU_CD))
                {
                    this.ctxt_Shogun_SBN_houhouName.Text = string.Empty;
                }
                else
                {
                    var validShobunHouhou = dao.GetAllValidData(shobunCond);
                    bool isEmptyFlg = (validShobunHouhou == null || validShobunHouhou.Count() < 1);
                    this.ctxt_Shogun_SBN_houhouName.Text = isEmptyFlg ? null : validShobunHouhou[0].SHOBUN_HOUHOU_NAME_RYAKU;
                }

            }

            // 処分報告情報承認待ちフラグ
            if (!ManiInfo.dt_r18.SBN_SHOUNIN_FLAG.IsNull)
            {
                this.ctxt_SBN_Shouni_Flag.Text = ManiInfo.dt_r18.SBN_SHOUNIN_FLAG.ToString();
            }

            //処分終了報告区分
            if (!ManiInfo.dt_r18.SBN_ENDREP_KBN.IsNull)
            {
                this.cntxt_HoukokuKBN.Text = ManiInfo.dt_r18.SBN_ENDREP_KBN.ToString();
            }
            //処分報告担当者
            this.cantxt_SBN_HoukokuTantouShaCD.Text = (ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.HOUKOKU_TANTOUSHA_CD;
            //処分報告担当者名称
            this.ctxt_SBN_HoukokuTantouShaName.Text = ManiInfo.dt_r18.REP_TAN_NAME;
            //処分担当者CD
            this.cantxt_SBN_SBNTantouShaCD.Text = (ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.SBN_TANTOUSHA_CD;
            //処分担当者名称
            this.ctxt_SBN_SBNTantouShaName.Text = ManiInfo.dt_r18.SBN_TAN_NAME;
            //処分報告の運搬担当者CD
            this.cantxt_SBN_UnpanTantouShaCD.Text = (ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.UPN_TANTOUSHA_CD;
            //処分報告の運搬担当者名称
            this.ctxt_SBN_UnpanTantouShaName.Text = ManiInfo.dt_r18.UPN_TAN_NAME;
            //処分報告の車輌番号CD
            this.cantxt_SBN_SyaryoNoCD.Text = (ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.SHARYOU_CD;
            //処分報告の車輌名称
            if (ManiInfo.dt_r18ExOld != null)
            {
                this.ctxt_cantxt_SBN_SyaryoNoName.Text = ManiInfo.dt_r18.CAR_NO;
            }
            //処分終了日
            if (!string.IsNullOrEmpty(ManiInfo.dt_r18.SBN_END_DATE))
            {
                this.cdtp_SBNEndDate.Value = DateTime.ParseExact(ManiInfo.dt_r18.SBN_END_DATE,
                                                                "yyyyMMdd",
                                                                System.Globalization.CultureInfo.InvariantCulture);
            }
            //廃棄物受領日
            if (!string.IsNullOrEmpty(ManiInfo.dt_r18.HAIKI_IN_DATE))
            {
                this.cdtp_HaikiAcceptDate.Value = DateTime.ParseExact(ManiInfo.dt_r18.HAIKI_IN_DATE,
                                                                        "yyyyMMdd",
                                                                        System.Globalization.CultureInfo.InvariantCulture);
            }
            //最終処分終了日
            if (!string.IsNullOrEmpty(ManiInfo.dt_r18.LAST_SBN_END_DATE))
            {
                this.cdpt_LastSBNEndDate.Value = DateTime.ParseExact(ManiInfo.dt_r18.LAST_SBN_END_DATE,
                                                                        "yyyyMMdd",
                                                                        System.Globalization.CultureInfo.InvariantCulture);
            }
            //受入量
            if (!ManiInfo.dt_r18.RECEPT_SUU.IsNull)
            {
                this.cntxt_Jyunyuryo.Text = ManiInfo.dt_r18.RECEPT_SUU.ToString();
            }
            //受入量の単位CD
            this.cantxt_JyunyuryoUnitCD.Text = (ManiInfo.dt_r18.RECEPT_UNIT_CODE == null) ? null : this.Logic.PadLeftUnitCd(ManiInfo.dt_r18.RECEPT_UNIT_CODE.ToString());
            //受入量の単位名称
            this.ctxt_JyunyuryoUnitName.Text = this.MasterLogic.GetCodeNameFromMasterInfo(WINDOW_ID.M_UNIT, ManiInfo.dt_r18.RECEPT_UNIT_CODE);
            //処分備考
            this.ctxt_SBN_Bikou.Text = ManiInfo.dt_r18.SBN_REP_BIKOU;

            // 紐付いている一次電マニが最終処分終了報告済みかフラグ
            bool isFixedFirstElecMani = false;
            if (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                var commonManiLogic = new ManifestoLogic();
                if (ManiInfo.dt_r18ExOld != null)
                {
                    isFixedFirstElecMani = commonManiLogic.IsFixedRelationFirstMani(ManiInfo.dt_r18ExOld.SYSTEM_ID, 4);
                }
            }

            // 入力制限判定用の変数
            bool existLastSbnEndDate = false;
            bool existLastSbnGenba = false;

            //最終処分事業場（実績）情報
            for (int i = 0; i < ManiInfo.lstDT_R13.Count; i++)
            {
                //行の追加
                this.cdgv_LastSBN_Genba_Jiseki.Rows.Add();
                this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["JISEIKI_No"].Value = i + 1;
                //最終処分終了日
                if (!string.IsNullOrEmpty(ManiInfo.lstDT_R13[i].LAST_SBN_END_DATE))
                {
                    this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_END_DATE"].Value =
                        DateTime.ParseExact(ManiInfo.lstDT_R13[i].LAST_SBN_END_DATE,
                        "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                    existLastSbnEndDate = true;
                }

                //最終処分の場所(実績)拡張情報
                if (ManiInfo.lstDT_R13_EX.Count > 0)//拡張データある場合及び元データが行数量同じ前提条件で
                {
                    if (ManiInfo.lstDT_R13_EX.Count == ManiInfo.lstDT_R13.Count)
                    {
                        //最終処分業者CD[最終処分の場所(実績)拡張DT_R13_EX]
                        this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_GYOUSHA_JISEKI_CD"].Value = ManiInfo.lstDT_R13_EX[i].LAST_SBN_GYOUSHA_CD;

                        //最終処分業者名称と加入者番号を取得
                        DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                        if (!string.IsNullOrEmpty(ManiInfo.lstDT_R13_EX[i].LAST_SBN_GYOUSHA_CD))
                        {
                            dto.GYOUSHA_CD = ManiInfo.lstDT_R13_EX[i].LAST_SBN_GYOUSHA_CD;
                            dto.SBN_KBN = "1";
                            dto.ISNOT_NEED_DELETE_FLG = true;
                            DataTable dt = this.MasterLogic.GetDenshiGyoushaData(dto);
                            if (dt.Rows.Count == 1)
                            {
                                //最終処分業者名称
                                this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_GYOUSHA_JISEKI_NAME"].Value = dt.Rows[0]["JIGYOUSHA_NAME"].ToString();
                                //最終処分加入者番号が暗黙なカラムにを保存する
                                this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_GYOUSHA_KanyushaCD"].Value = dt.Rows[0]["EDI_MEMBER_ID"].ToString();
                            }
                        }
                        //最終処分業場CD
                        this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_CD"].Value = ManiInfo.lstDT_R13_EX[i].LAST_SBN_GENBA_CD;
                    }
                }
                //最終処分業場名称
                this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_NAME"].Value = ManiInfo.lstDT_R13[i].LAST_SBN_JOU_NAME;

                if (!string.IsNullOrEmpty(ManiInfo.lstDT_R13[i].LAST_SBN_JOU_NAME))
                {
                    existLastSbnGenba = true;
                }

                //最終処分業場の郵便番号
                this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_POST"].Value = ManiInfo.lstDT_R13[i].LAST_SBN_JOU_POST;
                //最終処分業場の住所
                this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value = ManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS1 +
                    ManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS2 + ManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS3 + ManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS4;
                //最終処分業場の電話番号
                this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value = ManiInfo.lstDT_R13[i].LAST_SBN_JOU_TEL;

                // 二次マニの場合は紐付けた一次電マニが既に最終処分終了報告されていた場合
                // 最終処分情報を入力不可にする
                if (this.Logic.maniFlag == 2 && isFixedFirstElecMani
                    && (existLastSbnEndDate && existLastSbnGenba))
                {
                    this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_END_DATE"].ReadOnly = true;
                    this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_GYOUSHA_JISEKI_CD"].ReadOnly = true;
                    this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_GYOUSHA_JISEKI_NAME"].ReadOnly = true;
                    this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_CD"].ReadOnly = true;
                    this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_NAME"].ReadOnly = true;
                    this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_POST"].ReadOnly = true;
                    this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].ReadOnly = true;
                    this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_TEL"].ReadOnly = true;
                }
                else
                {
                    // 一件でも入力できる行がある場合はアラートを表示するためにフラグをfalseにする
                    this.existAllLastSbnInfo = false;
                }

            }

            // 二次マニの場合は紐付けた一次電マニが既に最終処分終了報告されていた場合
            // 最終処分情報を入力不可にする(既にAllowUserToAddRows = falseの場合は設定しない)
            if (this.Logic.maniFlag == 2 && isFixedFirstElecMani)
            {
                if (this.cdgv_LastSBN_Genba_Jiseki.AllowUserToAddRows)
                {
                    this.cdgv_LastSBN_Genba_Jiseki.AllowUserToAddRows = false;
                }
                if (this.cdgv_LastSBN_Genba_Jiseki.AllowUserToDeleteRows)
                {
                    this.cdgv_LastSBN_Genba_Jiseki.AllowUserToDeleteRows = false;
                }
            }

            //備考情報
            // 備考欄を固定行に設定
            this.AddDefaultRowForBikou();

            foreach (var targetData in ManiInfo.lstDT_R06)
            {
                // 行チェック
                if (targetData.BIKOU_NO.IsNull
                    || targetData.BIKOU_NO > bikouMaxRowCount)
                {
                    continue;
                }

                //備考
                this.cdgv_Bikou.Rows[(int)targetData.BIKOU_NO - 1].Cells["BIKOU"].Value = targetData.BIKOU;
            }

            if (ManiInfo.dt_r18.FIRST_MANIFEST_FLAG == null || string.IsNullOrEmpty(ManiInfo.dt_r18.FIRST_MANIFEST_FLAG))
            {
                this.Logic.maniFlag = 1;
            }
            else if ("1".Equals(ManiInfo.dt_r18.FIRST_MANIFEST_FLAG) ||
                "2".Equals(ManiInfo.dt_r18.FIRST_MANIFEST_FLAG) ||
                "3".Equals(ManiInfo.dt_r18.FIRST_MANIFEST_FLAG))
            {
                switch (ManiInfo.dt_r18.FIRST_MANIFEST_FLAG)
                {
                    case "1":
                        //当欄指定のとおり
                        this.ccbx_Touranshitei.Checked = true;
                        break;
                    case "2":
                        //1次不要
                        this.ccbx_ItijiFuyou.Checked = true;
                        break;
                    case "3":
                        //帳簿記載のとおり
                        this.ccbx_ChouboKisai.Checked = true;
                        break;

                }

                this.Logic.maniFlag = 2;
            }

            if ("2".Equals(this.cntxt_ManiKBN.Text) && (this.Logic.maniFlag == 2)) //マニ/二次
            {
                if (this.ccbx_ItijiFuyou.Checked) //一次不要
                {
                    this.ccbx_Touranshitei.Enabled = false;
                    this.ccbx_ItijiFuyou.Enabled = true;
                    this.ccbx_ChouboKisai.Enabled = false;
                }
                else
                {
                    this.ccbx_Touranshitei.Enabled = true;
                    this.ccbx_ItijiFuyou.Enabled = false;
                    this.ccbx_ChouboKisai.Enabled = true;
                }
            }

            //中間処理産業廃棄物
            if (ManiInfo.lstDT_R08 != null && ManiInfo.lstDT_R08_EX != null && ManiInfo.lstDT_R08.Count == ManiInfo.lstDT_R08_EX.Count)
            {
                var r18ExDao = DaoInitUtility.GetComponent<CommonDT_R18_EXDaoCls>();
                var maniCommonDao = DaoInitUtility.GetComponent<CommonEntryDaoCls>();

                for (int i = 0; i < ManiInfo.lstDT_R08.Count; i++)
                {

                    DataTable FirstManifestInfo = new DataTable();

                    SearchMasterDataDTOCls search = new SearchMasterDataDTOCls();
                    if (ManiInfo.lstDT_R08[i].MEDIA_TYPE == 2)  //紙マニの場合RENARAKU_IDに交付番号が入っている
                    {
                        search.MANIFEST_ID = ManiInfo.lstDT_R08[i].RENRAKU_ID;
                    }
                    else
                    {
                        search.MANIFEST_ID = ManiInfo.lstDT_R08[i].FIRST_MANIFEST_ID;
                    }

                    FirstManifestInfo = this.Logic.FirstManifestInfoDao.GetFirstManifestInfoall(search);

                    if (FirstManifestInfo != null && FirstManifestInfo.Rows.Count > 0)
                    {

                        this.cdgv_Tyukanshori.Rows.Add();

                        //1次交付番号／マニフェスト番号
                        if (ManiInfo.lstDT_R08[i].MEDIA_TYPE == 2)  //紙マニ
                        {
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_MANIFEST_ID"].Value = ManiInfo.lstDT_R08[i].RENRAKU_ID.ToString();
                        }
                        else
                        {
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_MANIFEST_ID"].Value = ManiInfo.lstDT_R08[i].FIRST_MANIFEST_ID.ToString();
                        }
                        //連絡番号１の設定
                        this.cdgv_Tyukanshori.Rows[i].Cells["FM_RENRAKU_ID1"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["RENRAKU_ID1"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["RENRAKU_ID1"]);
                        //連絡番号２の設定
                        this.cdgv_Tyukanshori.Rows[i].Cells["FM_RENRAKU_ID2"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["RENRAKU_ID2"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["RENRAKU_ID2"]);
                        //連絡番号３の設定
                        this.cdgv_Tyukanshori.Rows[i].Cells["FM_RENRAKU_ID3"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["RENRAKU_ID3"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["RENRAKU_ID3"]);
                        //排出事業者CD
                        this.cdgv_Tyukanshori.Rows[i].Cells["FM_HST_GYOUSHA_CD"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["HST_GYOUSHA_CD"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["HST_GYOUSHA_CD"]);
                        //排出事業者名称
                        this.cdgv_Tyukanshori.Rows[i].Cells["FM_HST_GYOUSHA_NAME"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["HST_GYOUSHA_NAME"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["HST_GYOUSHA_NAME"]);
                        //排出事業場CD
                        this.cdgv_Tyukanshori.Rows[i].Cells["FM_HST_GENBA_CD"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["HST_GENBA_CD"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["HST_GENBA_CD"]);
                        //排出事業場名称
                        this.cdgv_Tyukanshori.Rows[i].Cells["FM_HST_GENBA_NAME"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["HST_GENBA_NAME"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["HST_GENBA_NAME"]);
                        //引渡日／交付年月日
                        this.cdgv_Tyukanshori.Rows[i].Cells["FM_KOUFU_DATE"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["KOUFU_DATE"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["KOUFU_DATE"]);

                        // マニフェストの明細が１行の場合のみ、廃棄物種類CD、処分終了日、数量、単位CDを設定する。
                        if (FirstManifestInfo.Rows.Count == 1)
                        {
                            //処分終了日
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_SBN_END_DATE"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["SBN_END_DATE"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["SBN_END_DATE"]);
                            //廃棄物種類CD
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_SHURUI_CD"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["HAIKI_SHURUI_CD"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["HAIKI_SHURUI_CD"]);
                            //廃棄物種類名
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"]);
                            //数量
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_SUU"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["HAIKI_SUU"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["HAIKI_SUU"]);
                            //単位コード
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_UNIT_CD"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["HAIKI_UNIT_CD"])) ? null : this.PadLeftUnitCd(this.GetDbValue(FirstManifestInfo.Rows[0]["HAIKI_UNIT_CD"]));
                            //単位名
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_UNIT_NAME_RYAKU"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["UNIT_NAME_RYAKU"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["UNIT_NAME_RYAKU"]);
                        }
                        else
                        {
                            //処分終了日
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_SBN_END_DATE"].Value = null;
                            //廃棄物種類CD
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_SHURUI_CD"].Value = null;
                            //廃棄物種類名
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value = null;
                            //数量
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_SUU"].Value = null;
                            //単位コード
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_UNIT_CD"].Value = null;
                            //単位名
                            this.cdgv_Tyukanshori.Rows[i].Cells["FM_UNIT_NAME_RYAKU"].Value = null;
                        }

                        //電子/紙区分(非表示列)
                        this.cdgv_Tyukanshori.Rows[i].Cells["Hidden_FM_MEDIA_TYPE"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["HAIKI_KBN_CD"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["HAIKI_KBN_CD"]);
                        //システムid(非表示列)
                        this.cdgv_Tyukanshori.Rows[i].Cells["Hidden_FM_SYSTEM_ID"].Value = string.IsNullOrEmpty(this.GetDbValue(FirstManifestInfo.Rows[0]["SYSTEM_ID"])) ? null : this.GetDbValue(FirstManifestInfo.Rows[0]["SYSTEM_ID"]);
                    }
                }
            }

            //HeaderFormの情報を設定
            if (ManiInfo.dt_r18ExOld != null)
            {
                if (!this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    this.Logic.headerform.ctxt_FirstRegistSha.Text = ManiInfo.dt_r18ExOld.CREATE_USER;

                    this.Logic.headerform.ctxt_FirstRegistDate.Text = ManiInfo.dt_mf_toc.CREATE_DATE.ToString();

                    this.Logic.headerform.ctxt_Lastctxt_LastModifySha.Text = ManiInfo.dt_r18ExOld.UPDATE_USER;
                    if (!ManiInfo.dt_r18ExOld.UPDATE_DATE.IsNull)
                    {
                        this.Logic.headerform.ctxt_LastModifyDate.Text = ManiInfo.dt_r18ExOld.UPDATE_DATE.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 廃棄物種類CDの値をもとに、有害物質の項目の活性/非活性を判定する。
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns>true:非活性にする。false:活性にする。</returns>
        private bool CheckYuugaiColumnReadOnly(int rowIndex)
        {
            if (this.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SHURUI_CD"].Value == null
                            || string.IsNullOrEmpty(this.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SHURUI_CD"].Value.ToString()))
            {
                return false;
            }

            this.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SHURUI_CD"].Value =
                this.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SHURUI_CD"].Value.ToString().PadLeft(7, '0').ToUpper();

            string haikibutuHaikiShuruiCd = this.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SHURUI_CD"].Value.ToString();

            string haikiShuruiSaibunruiCd = haikibutuHaikiShuruiCd.Substring(0, 4);
            string haikiShuruiCd = haikibutuHaikiShuruiCd.Substring(4, 3);

            var currentInfoSaibunrui = this.Logic.ListDenshiHaikiShuruiSaibunrui.Where(
                s => s.EDI_MEMBER_ID == this.ctxt_Haisyutu_KanyushaNo.Text
                    && s.HAIKI_SHURUI_CD == haikiShuruiSaibunruiCd
                    && s.HAIKI_SHURUI_SAIBUNRUI_CD == haikiShuruiCd).FirstOrDefault();

            //データが存在しない場合
            if (currentInfoSaibunrui == null)
            {
                var currentInfo = this.Logic.ListDenshiHaikiShurui.Where(
                    s => s.HAIKI_SHURUI_CD == haikiShuruiSaibunruiCd).FirstOrDefault();

                if (currentInfo == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// データタイムピッカーコントロールクリアMethod
        /// </summary>
        private void DateTimePickerCtlClear()
        {
            this.cdate_HikiwataDate.Value = null;
            this.cdtp_UnpanEndDate.Value = null;
            this.cdtp_HaikiAcceptDate.Value = null;
            this.cdtp_SBNEndDate.Value = null;
            this.cdpt_LastSBNEndDate.Value = null;

        }
        /// <summary>
        /// 画面から全てEntityを作成
        /// </summary>
        /// <returns></returns>
        public DenshiManifestInfoCls MakeAllData(out bool catchErr)
        {
            catchErr = false;
            try
            {
                //マスタ情報検索ロジック
                DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
                //検索用DTO
                DenshiSearchParameterDtoCls dto;

                //マニ情報クラスの宣言
                DenshiManifestInfoCls ManiInfo = new DenshiManifestInfoCls();

                // フラグ初期化
                this.Logic.isMakeEmptyData_R19_EX = false;

                //マニ区分（1:予約；2:マニフェスト）
                ManiInfo.dt_r18.MANIFEST_KBN = SqlInt16.Parse(this.cntxt_ManiKBN.Text);

                //新規モードの場合、入力区分が設定する
                if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    //入力区分[1:自動;2:手動]
                    ManiInfo.bIsAutoMode = (this.cntxt_InputKBN.Text == "1");
                }
                else
                {
                    //新規以外場合入力区分が変更不可
                    ManiInfo.bIsAutoMode = this.Logic.ManiInfo.bIsAutoMode;
                    //保留データ呼び出し時はマニ区分変更可能
                    if (!(this.Logic.HoryuFlg && this.Logic.HoryuINSFlg))
                    {
                        //マニ区分2:マニフェストの場合はが変更不可
                        ManiInfo.dt_r18.MANIFEST_KBN = (this.Logic.ManiInfo.dt_r18.MANIFEST_KBN == 2) ? 2 : SqlInt16.Parse(this.cntxt_ManiKBN.Text);
                    }
                    //交付番号
                    ManiInfo.dt_r18.MANIFEST_ID = this.Logic.ManiInfo.dt_r18.MANIFEST_ID;
                }
                //手動モードで交付番号が画面から取得する
                if (!ManiInfo.bIsAutoMode)
                {//手動場合は設定する
                    ManiInfo.dt_r18.MANIFEST_ID = this.cantxt_ManifestNo.Text;
                }
                //先ずリスト情報が画面から取得する
                //有害物質リストDT_R02
                for (int i = 0; i < 6; i++)
                {
                    string ColName_CD = "YUUGAI_CODE" + (i + 1).ToString();
                    string ColName_Name = "YUUGAI_NAME" + (i + 1).ToString();
                    object tmpObj_CD = this.cdgv_Haikibutu.Rows[0].Cells[ColName_CD].Value;
                    object tmpObj_NAME = this.cdgv_Haikibutu.Rows[0].Cells[ColName_Name].Value;
                    if (tmpObj_CD != null)
                    {
                        if (!string.IsNullOrEmpty(tmpObj_CD.ToString()))
                        {
                            //有害物質Entityの宣言
                            DT_R02 dt_r02 = new DT_R02();
                            dt_r02.REC_SEQ = SqlInt16.Parse((ManiInfo.lstDT_R02.Count + 1).ToString());
                            dt_r02.YUUGAI_CODE = tmpObj_CD.ToString();
                            if (tmpObj_NAME != null)
                            {
                                dt_r02.YUUGAI_NAME = tmpObj_NAME.ToString();
                            }
                            ManiInfo.lstDT_R02.Add(dt_r02);
                        }
                    }

                }
                //最終処分事業場(予定)情報[DT_R04]
                DT_R04 dt_r04;
                for (int i = 0; i < cdgv_LastSBNbasyo_yotei.Rows.Count; i++)
                {
                    //行の有効フラグ
                    bool bIsValidRow = IsValidRowOfEveryInfo(this.cdgv_LastSBNbasyo_yotei.Rows[i]);
                    if (bIsValidRow)
                    {//有効データ設定される場合、Entityを作成
                        dt_r04 = new DT_R04();
                        //レコードのSeq
                        dt_r04.REC_SEQ = SqlInt16.Parse((ManiInfo.lstDT_R04.Count + 1).ToString());
                        object tmpObj = null;
                        //最終処分事業場名称
                        tmpObj = this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_NAME"].Value;
                        if (tmpObj != null)
                        {
                            dt_r04.LAST_SBN_JOU_NAME = tmpObj.ToString();
                        }
                        //最終処分事業場所在地郵便番号
                        tmpObj = this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_POST"].Value;
                        if (tmpObj != null)
                        {
                            dt_r04.LAST_SBN_JOU_POST = tmpObj.ToString();
                        }
                        if (ManiInfo.bIsAutoMode)//自動場合、DBから連携情報を取得
                        {
                            tmpObj = this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["Sbn_KanyushaCD"].Value;
                            if (tmpObj != null)
                            {
                                string EDI_MEMBER_ID = tmpObj.ToString();
                                tmpObj = this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_CD"].Value;
                                if (tmpObj != null)
                                {
                                    string genbaCd = tmpObj.ToString();
                                    //検索条件設定
                                    dto = new DenshiSearchParameterDtoCls();
                                    //加入者番号
                                    dto.EDI_MEMBER_ID = EDI_MEMBER_ID;
                                    //現場CD
                                    dto.GENBA_CD = genbaCd;
                                    //事業場区分[処分事業場]
                                    dto.JIGYOUJOU_KBN = "3";
                                    // 業者CD、事業場番号セット
                                    var lastSbnGyoushaCd = this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_GYOUSHA_CD"].Value;
                                    if (lastSbnGyoushaCd != null)
                                    {
                                        dto.GYOUSHA_CD = lastSbnGyoushaCd.ToString();
                                    }
                                    var lastSbnJigyoujouCd = this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["sbn_JigyoujouCD"].Value;
                                    if (lastSbnJigyoujouCd != null)
                                    {
                                        dto.JIGYOUJOU_CD = lastSbnJigyoujouCd.ToString();
                                    }

                                    DataTable dt = DsMasterLogic.GetDenshiGenbaData(dto);
                                    if (dt.Rows.Count == 1)
                                    {
                                        //最終処分事業場所在地１
                                        if (dt.Rows[0]["JIGYOUJOU_ADDRESS1"] != null)
                                            dt_r04.LAST_SBN_JOU_ADDRESS1 = dt.Rows[0]["JIGYOUJOU_ADDRESS1"].ToString();
                                        //最終処分事業場所在地２
                                        if (dt.Rows[0]["JIGYOUJOU_ADDRESS2"] != null)
                                            dt_r04.LAST_SBN_JOU_ADDRESS2 = dt.Rows[0]["JIGYOUJOU_ADDRESS2"].ToString();
                                        //最終処分事業場所在地３
                                        if (dt.Rows[0]["JIGYOUJOU_ADDRESS3"] != null)
                                            dt_r04.LAST_SBN_JOU_ADDRESS3 = dt.Rows[0]["JIGYOUJOU_ADDRESS3"].ToString();
                                        //最終処分事業場所在地４
                                        if (dt.Rows[0]["JIGYOUJOU_ADDRESS4"] != null)
                                            dt_r04.LAST_SBN_JOU_ADDRESS4 = dt.Rows[0]["JIGYOUJOU_ADDRESS4"].ToString();
                                    }
                                }
                            }

                        }
                        else
                        {//手動場合
                            string Address = string.Empty;
                            if (this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_ADDRESS"].Value != null)
                            {
                                Address = this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_ADDRESS"].Value.ToString();
                                if (!string.IsNullOrEmpty(Address))
                                {
                                    var maniLogic = new ManifestoLogic();
                                    string tempAddress1;
                                    string tempAddress2;
                                    string tempAddress3;
                                    string tempAddress4;
                                    maniLogic.SetAddress1ToAddress4(Address,
                                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                                    dt_r04.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                                    dt_r04.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                                    dt_r04.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                                    dt_r04.LAST_SBN_JOU_ADDRESS4 = tempAddress4;
                                }
                            }
                        }

                        //最終処分事業場電話番号
                        tmpObj = this.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_TEL"].Value;
                        if (tmpObj != null)
                        {
                            dt_r04.LAST_SBN_JOU_TEL = tmpObj.ToString();
                        }

                        //データがリストに追加
                        ManiInfo.lstDT_R04.Add(dt_r04);
                    }
                }
                //連絡番号情報[DT_R05]
                DT_R05 dt_r05;
                if (!string.IsNullOrEmpty(this.ctxt_RenlakuNo1.Text))
                {
                    dt_r05 = new DT_R05();
                    //連絡番号No
                    dt_r05.RENRAKU_ID_NO = 1M;
                    //連絡番号
                    dt_r05.RENRAKU_ID = this.ctxt_RenlakuNo1.Text;
                    //データがリストに追加
                    ManiInfo.lstDT_R05.Add(dt_r05);
                }
                if (!string.IsNullOrEmpty(this.ctxt_RenlakuNo2.Text))
                {
                    dt_r05 = new DT_R05();
                    //連絡番号No
                    dt_r05.RENRAKU_ID_NO = 2M;
                    //連絡番号
                    dt_r05.RENRAKU_ID = this.ctxt_RenlakuNo2.Text;
                    //データがリストに追加
                    ManiInfo.lstDT_R05.Add(dt_r05);
                }
                if (!string.IsNullOrEmpty(this.ctxt_RenlakuNo3.Text))
                {
                    dt_r05 = new DT_R05();
                    //連絡番号No
                    dt_r05.RENRAKU_ID_NO = 3M;
                    //連絡番号
                    dt_r05.RENRAKU_ID = this.ctxt_RenlakuNo3.Text;
                    //データがリストに追加
                    ManiInfo.lstDT_R05.Add(dt_r05);
                }
                //備考情報[DT_R06]
                DT_R06 dt_r06;
                for (int i = 0; i < cdgv_Bikou.Rows.Count; i++)
                {
                    if (this.cdgv_Bikou.Rows[i].Cells["BIKOU"].Value == null
                        || string.IsNullOrEmpty(this.cdgv_Bikou.Rows[i].Cells["BIKOU"].Value.ToString()))
                    {
                        continue;
                    }

                    dt_r06 = new DT_R06();
                    dt_r06.BIKOU_NO = i + 1;
                    object tmpObj = this.cdgv_Bikou.Rows[i].Cells["BIKOU"].Value;
                    dt_r06.BIKOU = tmpObj.ToString();

                    //データがリストに追加
                    ManiInfo.lstDT_R06.Add(dt_r06);
                }
                //最終処分終了日・事業場情報[DT_R13]
                DT_R13 dt_r13;//Entityの宣言
                for (int i = 0; i < cdgv_LastSBN_Genba_Jiseki.Rows.Count; i++)
                {
                    bool bIsValidRow = IsValidRowOfEveryInfo(this.cdgv_LastSBN_Genba_Jiseki.Rows[i]);
                    if (bIsValidRow)
                    {//有効データ設定される場合、Entityを作成
                        dt_r13 = new DT_R13();
                        //レコードのSeq
                        dt_r13.REC_SEQ = SqlInt16.Parse((ManiInfo.lstDT_R13.Count + 1).ToString());
                        //最終処分事業場名称
                        object tmpObj = this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_NAME"].Value;
                        if (tmpObj != null)
                        {
                            dt_r13.LAST_SBN_JOU_NAME = tmpObj.ToString();
                        }
                        //最終処分事業場所在地郵便番号
                        tmpObj = this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_POST"].Value;
                        if (tmpObj != null)
                        {
                            dt_r13.LAST_SBN_JOU_POST = tmpObj.ToString();
                        }
                        if (!ManiInfo.bIsAutoMode)//手動場合は編集可
                        {
                            string Address = string.Empty;
                            if (this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value != null)
                            {
                                Address = this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value.ToString();
                                if (!string.IsNullOrEmpty(Address))
                                {
                                    var maniLogic = new ManifestoLogic();
                                    string tempAddress1;
                                    string tempAddress2;
                                    string tempAddress3;
                                    string tempAddress4;
                                    maniLogic.SetAddress1ToAddress4(Address,
                                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                                    dt_r13.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                                    dt_r13.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                                    dt_r13.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                                    dt_r13.LAST_SBN_JOU_ADDRESS4 = tempAddress4;
                                }
                            }
                        }
                        //最終処分事業場電話番号
                        tmpObj = this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value;
                        if (tmpObj != null)
                        {
                            dt_r13.LAST_SBN_JOU_TEL = tmpObj.ToString();
                        }
                        //最終処分終了日
                        tmpObj = this.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_END_DATE"].Value;
                        if (tmpObj != null)
                        {
                            string EndDate = tmpObj.ToString();
                            if (!string.IsNullOrEmpty(EndDate))
                            {
                                dt_r13.LAST_SBN_END_DATE = EndDate.Substring(0, 10).Replace("/", "");
                            }
                        }
                        //データがリストに追加
                        ManiInfo.lstDT_R13.Add(dt_r13);
                    }
                }

                //中間処理廃棄物情報
                DT_R08 dt_r08;
                for (int i = 0; i < cdgv_Tyukanshori.Rows.Count; i++)
                {
                    bool bIsValidRow = IsValidRowOfEveryInfo(this.cdgv_Tyukanshori.Rows[i]);
                    if (bIsValidRow)
                    {
                        dt_r08 = new DT_R08();
                        //レコードのSeq
                        dt_r08.REC_SEQ = (SqlDecimal)(ManiInfo.lstDT_R08.Count + 1);
                        //マニフェスト番号
                        dt_r08.MANIFEST_ID = string.IsNullOrEmpty(cantxt_ManifestNo.Text) ? null : cantxt_ManifestNo.Text;
                        //一次マニフェスト番号/交付番号
                        object tmpObj = this.cdgv_Tyukanshori.Rows[i].Cells["FM_MANIFEST_ID"].Value;
                        if (tmpObj != null)
                        {
                            dt_r08.FIRST_MANIFEST_ID = tmpObj.ToString();
                        }
                        //電子/紙区分
                        tmpObj = this.cdgv_Tyukanshori.Rows[i].Cells["Hidden_FM_MEDIA_TYPE"].Value;
                        if (tmpObj != null)
                        {
                            dt_r08.MEDIA_TYPE = (tmpObj.ToString() == "4") ? 1 : 2;
                        }
                        //連絡番号
                        //紙の場合、連絡番号をセットする
                        if (dt_r08.MEDIA_TYPE == 2)
                        {
                            dt_r08.RENRAKU_ID = dt_r08.FIRST_MANIFEST_ID;
                        }
                        //交付年月日
                        tmpObj = this.cdgv_Tyukanshori.Rows[i].Cells["FM_KOUFU_DATE"].Value;
                        if (tmpObj != null && tmpObj.ToString() != "")
                        {
                            dt_r08.KOUHU_DATE = tmpObj.ToString().Replace("-", "").Replace("/", "").Substring(0, 8);
                        }
                        //処分終了日
                        tmpObj = this.cdgv_Tyukanshori.Rows[i].Cells["FM_SBN_END_DATE"].Value;
                        if (tmpObj != null && tmpObj.ToString() != "")
                        {

                            dt_r08.SBN_END_DATE = tmpObj.ToString().Replace("-", "").Replace("/", "").Substring(0, 8);

                        }
                        //排出事業者名称
                        tmpObj = this.cdgv_Tyukanshori.Rows[i].Cells["FM_HST_GYOUSHA_NAME"].Value;
                        if (tmpObj != null)
                        {
                            dt_r08.HST_SHA_NAME = tmpObj.ToString();
                        }
                        //排出事業場名称
                        tmpObj = this.cdgv_Tyukanshori.Rows[i].Cells["FM_HST_GENBA_NAME"].Value;
                        if (tmpObj != null)
                        {
                            dt_r08.HST_JOU_NAME = tmpObj.ToString();
                        }
                        //廃棄物種類名称
                        tmpObj = this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value;
                        if (tmpObj != null)
                        {
                            dt_r08.HAIKI_SHURUI = tmpObj.ToString();
                        }
                        //廃棄物数量
                        tmpObj = this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_SUU"].Value;
                        if (tmpObj != null && tmpObj.ToString() != "")
                        {
                            dt_r08.HAIKI_SUU = SqlDecimal.Parse(tmpObj.ToString().Replace(",", ""));
                        }
                        //廃棄物数量単位コード
                        tmpObj = this.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_UNIT_CD"].Value;
                        if (tmpObj != null && tmpObj.ToString() != "")
                        {
                            int len = tmpObj.ToString().Length;
                            dt_r08.HAIKI_SUU_UNIT = tmpObj.ToString().Substring(len - 1, 1);
                        }
                        //
                        //データがリストに追加
                        ManiInfo.lstDT_R08.Add(dt_r08);
                    }
                }
                //*********************DT_R18情報の処分受託者情報設定***開始****運搬の最後区間情報更新のため******************
                //処分受託者関連情報
                //処分業者加入者番号
                if (!string.IsNullOrEmpty(this.ctxt_SBN_KanyuShaNo.Text))
                {
                    //報告不要場合
                    if (this.Logic.GetNO_REP_FLG(this.ctxt_SBN_KanyuShaNo.Text))
                    {
                        ManiInfo.dt_r18.SBN_SHA_MEMBER_ID = ConstCls.NO_REP_SBN_EDI_MEMBER_ID;
                    }
                    else
                    {
                        ManiInfo.dt_r18.SBN_SHA_MEMBER_ID = this.ctxt_SBN_KanyuShaNo.Text;
                    }
                }

                //処分業者名
                ManiInfo.dt_r18.SBN_SHA_NAME = string.IsNullOrEmpty(this.ctxt_SBN_JyutakuShaName.Text) ? null : this.ctxt_SBN_JyutakuShaName.Text;
                //処分業者郵便番号
                ManiInfo.dt_r18.SBN_SHA_POST = string.IsNullOrEmpty(this.ctxt_SBN_GyouShaPost.Text) ? null : this.ctxt_SBN_GyouShaPost.Text;
                //処分業者住所の関連情報処理
                //手動モードの場合
                if (!ManiInfo.bIsAutoMode)
                {
                    string Address = this.ctxt_SBN_GyouShaAddr.Text;
                    if (!string.IsNullOrEmpty(Address))
                    {
                        var maniLogic = new ManifestoLogic();
                        string tempAddress1;
                        string tempAddress2;
                        string tempAddress3;
                        string tempAddress4;
                        maniLogic.SetAddress1ToAddress4(Address,
                        out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                        ManiInfo.dt_r18.SBN_SHA_ADDRESS1 = tempAddress1;
                        ManiInfo.dt_r18.SBN_SHA_ADDRESS2 = tempAddress2;
                        ManiInfo.dt_r18.SBN_SHA_ADDRESS3 = tempAddress3;
                        ManiInfo.dt_r18.SBN_SHA_ADDRESS4 = tempAddress4;
                    }
                }
                //自動場合
                else
                {
                    //DBから住所１～４の情報を取得
                    DenshiSearchParameterDtoCls dtoGyousha = new DenshiSearchParameterDtoCls();
                    dtoGyousha.EDI_MEMBER_ID = this.ctxt_SBN_KanyuShaNo.Text;
                    DataTable dtGyousha = this.MasterLogic.GetDenshiGyoushaData(dtoGyousha);
                    if (dtGyousha.Rows.Count == 1)
                    {
                        //処分業者所在地1
                        ManiInfo.dt_r18.SBN_SHA_ADDRESS1 = (dtGyousha.Rows[0]["JIGYOUSHA_ADDRESS1"] != null) ? dtGyousha.Rows[0]["JIGYOUSHA_ADDRESS1"].ToString() : string.Empty;
                        //処分業者所在地2
                        ManiInfo.dt_r18.SBN_SHA_ADDRESS2 = (dtGyousha.Rows[0]["JIGYOUSHA_ADDRESS2"] != null) ? dtGyousha.Rows[0]["JIGYOUSHA_ADDRESS2"].ToString() : string.Empty;
                        //処分業者所在地3
                        ManiInfo.dt_r18.SBN_SHA_ADDRESS3 = (dtGyousha.Rows[0]["JIGYOUSHA_ADDRESS3"] != null) ? dtGyousha.Rows[0]["JIGYOUSHA_ADDRESS3"].ToString() : string.Empty;
                        //処分業者所在地4
                        ManiInfo.dt_r18.SBN_SHA_ADDRESS4 = (dtGyousha.Rows[0]["JIGYOUSHA_ADDRESS4"] != null) ? dtGyousha.Rows[0]["JIGYOUSHA_ADDRESS4"].ToString() : string.Empty;
                    }
                }
                //処分業者電話番号
                ManiInfo.dt_r18.SBN_SHA_TEL = string.IsNullOrEmpty(this.ctxt_SBN_GyouShaTel.Text) ? null : this.ctxt_SBN_GyouShaTel.Text;
                //処分業者統一許可番号
                ManiInfo.dt_r18.SBN_SHA_KYOKA_ID = (string.IsNullOrEmpty(this.ctxt_SBN_KyokaNo.Text)) ? null : this.ctxt_SBN_KyokaNo.Text;
                //*********************DT_R18情報の処分受託者情報設定*******終了**********************

                //運搬情報の無効データを整理する
                if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG ||
                        (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG && !this.cdgv_UnpanInfo.ReadOnly))
                {
                    this.cdgv_UnpanInfo.AllowUserToAddRows = false;
                    for (int i = 0; i < this.cdgv_UnpanInfo.Rows.Count; i++)
                    {   //行の有効フラグ
                        bool bIsValidRow = this.IsValidRowOfEveryInfo(this.cdgv_UnpanInfo.Rows[i], true);
                        if (!bIsValidRow)
                        {
                            this.cdgv_UnpanInfo.Rows.RemoveAt(i);
                        }
                    }
                }
                DT_R19 dt_r19;
                for (int i = 0; i < this.cdgv_UnpanInfo.Rows.Count; i++)
                {
                    if (this.cdgv_UnpanInfo.Rows[i].IsNewRow) continue;

                    dt_r19 = new DT_R19();
                    //レコードのSeq
                    dt_r19.UPN_ROUTE_NO = (SqlDecimal)(ManiInfo.lstDT_R19.Count + 1);
                    object tmpObj = null;
                    //収集運搬業者加入者番号
                    tmpObj = this.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_EDI_MEMBER_ID"].Value;
                    if (tmpObj != null)
                    {
                        if (!string.IsNullOrEmpty(tmpObj.ToString()))
                        {
                            //報告不要場合
                            if (this.Logic.GetNO_REP_FLG(tmpObj.ToString()))
                            {
                                dt_r19.UPN_SHA_EDI_MEMBER_ID = ConstCls.NO_REP_SBN_EDI_MEMBER_ID;
                            }
                            else
                            {
                                dt_r19.UPN_SHA_EDI_MEMBER_ID = tmpObj.ToString();
                            }
                        }
                    }

                    //収集運搬業者名
                    tmpObj = null;
                    tmpObj = this.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_NAME"].Value;
                    if (tmpObj != null)
                    {
                        dt_r19.UPN_SHA_NAME = tmpObj.ToString();
                    }
                    //収集運搬業者加入者番号設定される場合,連携情報を設定する
                    if (!string.IsNullOrEmpty(dt_r19.UPN_SHA_EDI_MEMBER_ID))
                    {
                        dto = new DenshiSearchParameterDtoCls();
                        dto.EDI_MEMBER_ID = dt_r19.UPN_SHA_EDI_MEMBER_ID;
                        // 入力時にチェックをしているため登録時にはDELETE_FLGは絞り込まないようにする。
                        // DELETE_FLGを絞り込んでしまうと、修正モード時に既に設定しているデータをnullで設定してしまうため。
                        dto.ISNOT_NEED_DELETE_FLG = true;
                        DataTable dt = DsMasterLogic.GetDenshiGyoushaData(dto);
                        if (dt.Rows.Count == 1)
                        {
                            //収集運搬業者郵便番号
                            if (dt.Rows[0]["JIGYOUSHA_POST"] != null)
                                dt_r19.UPN_SHA_POST = dt.Rows[0]["JIGYOUSHA_POST"].ToString();
                            //収集運搬業者所在地1
                            if (dt.Rows[0]["JIGYOUSHA_ADDRESS1"] != null)
                                dt_r19.UPN_SHA_ADDRESS1 = dt.Rows[0]["JIGYOUSHA_ADDRESS1"].ToString();
                            //収集運搬業者所在地2
                            if (dt.Rows[0]["JIGYOUSHA_ADDRESS2"] != null)
                                dt_r19.UPN_SHA_ADDRESS2 = dt.Rows[0]["JIGYOUSHA_ADDRESS2"].ToString();
                            //収集運搬業者所在地3
                            if (dt.Rows[0]["JIGYOUSHA_ADDRESS3"] != null)
                                dt_r19.UPN_SHA_ADDRESS3 = dt.Rows[0]["JIGYOUSHA_ADDRESS3"].ToString();
                            //収集運搬業者所在地4
                            if (dt.Rows[0]["JIGYOUSHA_ADDRESS4"] != null)
                                dt_r19.UPN_SHA_ADDRESS4 = dt.Rows[0]["JIGYOUSHA_ADDRESS4"].ToString();
                            //収集運搬業者電話番号
                            if (dt.Rows[0]["JIGYOUSHA_TEL"] != null)
                                dt_r19.UPN_SHA_TEL = dt.Rows[0]["JIGYOUSHA_TEL"].ToString();
                            // 収集運搬業者FAX
                            if (dt.Rows[0]["JIGYOUSHA_FAX"] != null)
                                dt_r19.UPN_SHA_FAX = dt.Rows[0]["JIGYOUSHA_FAX"].ToString();
                        }
                    }

                    //運搬方法コード
                    tmpObj = this.cdgv_UnpanInfo.Rows[i].Cells["UPN_WAY_CODE"].Value;
                    if (tmpObj != null)
                    {
                        dt_r19.UPN_WAY_CODE = tmpObj.ToString();
                    }
                    //運搬担当者
                    tmpObj = this.cdgv_UnpanInfo.Rows[i].Cells["UPN_TAN_NAME"].Value;
                    if (tmpObj != null)
                    {
                        dt_r19.UPN_TAN_NAME = tmpObj.ToString();
                    }
                    //車両番号
                    tmpObj = this.cdgv_UnpanInfo.Rows[i].Cells["CAR_NAME"].Value;
                    if (tmpObj != null)
                    {
                        dt_r19.CAR_NO = tmpObj.ToString();
                    }
                    //運搬先業者CD設定される場合,連携情報加入者番号を設定する
                    tmpObj = this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GYOUSHA_CD"].Value;
                    // 報告不要業者にチェックがついている場合、加入者番号が0000000に置き換わるので入力値を保持する
                    string upnSakiEdiMemberId = string.Empty;
                    if (tmpObj != null)
                    {
                        //運搬先加入者番号
                        object tmp = this.cdgv_UnpanInfo.Rows[i].Cells["Unpansaki_KanyushaCD"].Value;
                        if (tmp != null)
                        {
                            upnSakiEdiMemberId = tmp.ToString();
                            if (!string.IsNullOrEmpty(tmp.ToString()))
                            {
                                //報告不要場合
                                if (this.Logic.GetNO_REP_FLG(tmp.ToString()))
                                {
                                    dt_r19.UPNSAKI_EDI_MEMBER_ID = ConstCls.NO_REP_SBN_EDI_MEMBER_ID;
                                }
                                else
                                {
                                    dt_r19.UPNSAKI_EDI_MEMBER_ID = tmp.ToString();
                                }
                            }

                        }
                    }
                    //運搬先事業者名
                    tmpObj = this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GYOUSHA_NAME"].Value;
                    if (tmpObj != null)
                    {
                        dt_r19.UPNSAKI_NAME = tmpObj.ToString();
                    }
                    //運搬先事業場CD設定される場合、連携情報を設定する
                    tmpObj = this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GENBA_CD"].Value;
                    if (tmpObj != null)
                    {
                        dto = new DenshiSearchParameterDtoCls();
                        // dt_r19.UPNSAKI_EDI_MEMBER_IDは0000000に置き換わってる可能性があるため、入力値を設定する。
                        dto.EDI_MEMBER_ID = !string.IsNullOrEmpty(upnSakiEdiMemberId) ? upnSakiEdiMemberId : dt_r19.UPNSAKI_EDI_MEMBER_ID;
                        dto.GENBA_CD = tmpObj.ToString();
                        // 業者CD、事業場番号もセット
                        var unpansakiGyoushaCd = this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GYOUSHA_CD"].Value;
                        if (unpansakiGyoushaCd != null)
                        {
                            dto.GYOUSHA_CD = unpansakiGyoushaCd.ToString();
                        }
                        var unpansakiJigyoujouNo = this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value;
                        if (unpansakiJigyoujouNo != null)
                        {
                            dto.JIGYOUJOU_CD = unpansakiJigyoujouNo.ToString();
                        }

                        DataTable dt = DsMasterLogic.GetDenshiGenbaData(dto);
                        if (dt.Rows.Count == 1)
                        {
                            //運搬先事業場区分
                            //if (dt.Rows[0]["JIGYOUJOU_KBN"] != null)
                            //   dt_r19.UPNSAKI_JOU_KBN = SqlInt16.Parse(dt.Rows[0]["JIGYOUJOU_KBN"].ToString());

                            //運搬先事業場名称
                            tmpObj = this.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GENBA_NAME"].Value;
                            if (tmpObj != null)
                                dt_r19.UPNSAKI_JOU_NAME = tmpObj.ToString();
                            //運搬先事業場番号
                            if (dt.Rows[0]["JIGYOUJOU_CD"] != null)
                            {
                                string cd = dt.Rows[0]["JIGYOUJOU_CD"].ToString();
                                if (cd.Length > 3)
                                {
                                    cd = cd.Substring(cd.Length - 3, 3);//最後の3桁に取得する
                                }
                                dt_r19.UPNSAKI_JOU_ID = SqlDecimal.Parse(cd);
                            }

                            //運搬先事業場郵便番号
                            if (dt.Rows[0]["JIGYOUJOU_POST"] != null)
                                dt_r19.UPNSAKI_JOU_POST = dt.Rows[0]["JIGYOUJOU_POST"].ToString();
                            //運搬先事業場所在地1
                            if (dt.Rows[0]["JIGYOUJOU_ADDRESS1"] != null)
                                dt_r19.UPNSAKI_JOU_ADDRESS1 = dt.Rows[0]["JIGYOUJOU_ADDRESS1"].ToString();
                            //運搬先事業場所在地2
                            if (dt.Rows[0]["JIGYOUJOU_ADDRESS2"] != null)
                                dt_r19.UPNSAKI_JOU_ADDRESS2 = dt.Rows[0]["JIGYOUJOU_ADDRESS2"].ToString();
                            //運搬先事業場所在地3
                            if (dt.Rows[0]["JIGYOUJOU_ADDRESS3"] != null)
                                dt_r19.UPNSAKI_JOU_ADDRESS3 = dt.Rows[0]["JIGYOUJOU_ADDRESS3"].ToString();
                            //運搬先事業場所在地4
                            if (dt.Rows[0]["JIGYOUJOU_ADDRESS4"] != null)
                                dt_r19.UPNSAKI_JOU_ADDRESS4 = dt.Rows[0]["JIGYOUJOU_ADDRESS4"].ToString();
                            //運搬先事業場電話番号
                            if (dt.Rows[0]["JIGYOUJOU_TEL"] != null)
                                dt_r19.UPNSAKI_JOU_TEL = dt.Rows[0]["JIGYOUJOU_TEL"].ToString();
                        }
                    }

                    // 運搬承認フラグ。引き継ぐデータがある場合は引き継ぐ、無い場合はデフォルト値をセット
                    tmpObj = this.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHOUNIN_FLAG"].Value;
                    if (tmpObj == null || string.IsNullOrEmpty(tmpObj.ToString()))
                    {
                        dt_r19.UPN_SHOUNIN_FLAG = 1M;
                    }
                    else
                    {
                        dt_r19.UPN_SHOUNIN_FLAG = SqlDecimal.Parse(tmpObj.ToString());
                    }

                    //運搬報告情報
                    if (!ManiInfo.bIsAutoMode)//手動場合設定する
                    {
                        if ((this.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls) != null)
                        {
                            //運搬終了日
                            dt_r19.UPN_END_DATE = (this.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cdtp_UnpanEndDate;
                            //運搬報告記載の運搬担当者
                            dt_r19.UPNREP_UPN_TAN_NAME = (this.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).ctxt_UnpanTantoushaName;
                            //報告担当者
                            dt_r19.REP_TAN_NAME = (this.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).ctxt_HoukokuTantoushaName;
                            //運搬報告記載の車両番号
                            dt_r19.UPNREP_CAR_NO = (this.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).ctxt_UnpanSyaryoName;
                            //運搬量
                            dt_r19.UPN_SUU = (this.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cntxt_UnpanRyo;
                            //運搬量の単位コード
                            string unitCd = (this.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cantxt_UnpanRyoUnitCd;
                            if (!string.IsNullOrEmpty(unitCd))
                            {
                                dt_r19.UPN_UNIT_CODE = unitCd.Substring(unitCd.Length - 1, 1);
                            }
                            //有価物拾集量
                            dt_r19.YUUKA_SUU = (this.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cntxt_YukabutuRyo;
                            //有価物拾集量の単位コード
                            unitCd = (this.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cantxt_YukabutuRyoUnitCd;
                            if (!string.IsNullOrEmpty(unitCd))
                            {
                                dt_r19.YUUKA_UNIT_CODE = unitCd.Substring(unitCd.Length - 1, 1);
                            }
                            //備考
                            dt_r19.BIKOU = (this.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).ctxt_UnpanBikou;
                        }
                    }
                    //データがリストに追加
                    ManiInfo.lstDT_R19.Add(dt_r19);
                }

                // 【下記の条件の場合は運搬情報の最後に処分事業場を追加】
                // 運搬区分で最後の区間の運搬先事業場が処分場以外の場合
                var lastJigyoujou = this.Logic.LastUnpansakiJigyoujou();
                var isInsertNewRow = false;
                if (lastJigyoujou != null)
                {
                    if (lastJigyoujou.JIGYOUSHA_KBN != ConstCls.JIGYOUJOU_KBN_SBN)
                    {
                        isInsertNewRow = true;
                    }
                }
                // 運搬情報が入力されていない場合
                if (this.cdgv_UnpanInfo.Rows.Count == 0)
                {
                    isInsertNewRow = true;
                }

                // 運搬情報の最後の運ぱ先事業場へ処分事業場を追加登録
                if (isInsertNewRow)
                {
                    // 処分事業場CDが空の時は処分事業場内のデータを運搬情報の最後に追加登録する
                    if (string.IsNullOrEmpty(this.cantxt_SBN_Genba_CD.Text))
                    {
                        dt_r19 = new DT_R19();

                        //レコードのSeq
                        dt_r19.UPN_ROUTE_NO = (SqlDecimal)(ManiInfo.lstDT_R19.Count + 1);

                        //運搬先事業場名称
                        if (!string.IsNullOrEmpty(this.ctxt_SBN_Genba_Name.Text))
                        {
                            dt_r19.UPNSAKI_JOU_NAME = this.ctxt_SBN_Genba_Name.Text;
                        }
                        //運搬先事業場郵便番号
                        if (!string.IsNullOrEmpty(this.ctxt_SBN_Genba_Name.Text))
                        {
                            dt_r19.UPNSAKI_JOU_POST = this.ctxt_SBN_GenbaPost.Text;
                        }
                        //運搬先事業場電話番号
                        if (!string.IsNullOrEmpty(this.ctxt_SBN_Genba_Name.Text))
                        {
                            dt_r19.UPNSAKI_JOU_TEL = this.ctxt_SBN_GenbaTel.Text;
                        }
                        //運搬先事業場の住所１～住所４の設定
                        if (!string.IsNullOrEmpty(SBN_GenbaAddr.Text))
                        {
                            var maniLogic = new ManifestoLogic();
                            string tempAddress1;
                            string tempAddress2;
                            string tempAddress3;
                            string tempAddress4;
                            maniLogic.SetAddress1ToAddress4(SBN_GenbaAddr.Text,
                            out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                            dt_r19.UPNSAKI_JOU_ADDRESS1 = tempAddress1;
                            dt_r19.UPNSAKI_JOU_ADDRESS2 = tempAddress2;
                            dt_r19.UPNSAKI_JOU_ADDRESS3 = tempAddress3;
                            dt_r19.UPNSAKI_JOU_ADDRESS4 = tempAddress4;
                        }

                        if (!dt_r19.UPN_ROUTE_NO.IsNull)
                        {
                            // R19_EXにからデータを作成するフラグを立てる
                            this.Logic.isMakeEmptyData_R19_EX = true;

                            //データをリストに追加
                            ManiInfo.lstDT_R19.Add(dt_r19);
                        }
                    }
                }

                ////********処分情報から最終運搬区間に設定開始***************
                ////最終区間情報の運搬先業者と事業場情報の確認「処分受託者と処分事業場情報入力した場合、優先的に設定する」
                int nDgvRowsCnt = this.cdgv_UnpanInfo.Rows.Count;
                int nValidIdx = ManiInfo.lstDT_R19.Count - 1;//最終区間情報のインデックス
                if (nDgvRowsCnt > 0 && nValidIdx >= 0)
                {
                    if (nValidIdx == nDgvRowsCnt - 1)
                    {
                        //仕様変更のため、以下ソースをコメントする
                        ////運搬先業者情報が処分受託者情報設定されている場合、設定する
                        //if (ManiInfo.dt_r18.SBN_SHA_MEMBER_ID != null)
                        //{
                        //    //運搬先事業者加入者番号が処分受託者加入者番号を設定する
                        //    ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_EDI_MEMBER_ID = ManiInfo.dt_r18.SBN_SHA_MEMBER_ID;
                        //    //画面に運搬先事業者CDが設定する
                        //    this.cdgv_UnpanInfo.Rows[nDgvRowsCnt - 1].Cells["UNPANSAKI_GYOUSHA_CD"].Value = cantxt_SBN_JyutakuShaCD.Text;
                        //}

                        ////運搬先事業者名称が処分受託者名称に設定
                        //if (ManiInfo.dt_r18.SBN_SHA_NAME != null)
                        //{
                        //    ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_NAME = ManiInfo.dt_r18.SBN_SHA_NAME;
                        //}
                        //運搬先事業場情報が処分事業場情報入力した場合処分事業場情報に設定する
                        ////処分事業場CDが入力された場合
                        //if (!string.IsNullOrEmpty(cantxt_SBN_Genba_CD.Text))
                        //{
                        //    //画面に運搬先事業場CDが処分事業場CDより設定する
                        //    this.cdgv_UnpanInfo.Rows[nDgvRowsCnt - 1].Cells["UNPANSAKI_GENBA_CD"].Value = cantxt_SBN_Genba_CD.Text;
                        //}
                        //仕様変更のため、ソースをコメント終了

                        //運搬先事業場名称
                        ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_NAME = (string.IsNullOrEmpty(ctxt_SBN_Genba_Name.Text)) ? ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_NAME : ctxt_SBN_Genba_Name.Text;
                        //運搬先事業場番号
                        if (!string.IsNullOrEmpty(ctxt_SBN_JIGYOUJYOU_CD.Text))
                        {
                            if (ctxt_SBN_JIGYOUJYOU_CD.Text.Length > 3)
                            {
                                ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_ID = SqlDecimal.Parse(ctxt_SBN_JIGYOUJYOU_CD.Text.Substring(ctxt_SBN_JIGYOUJYOU_CD.Text.Length - 3, 3));
                            }
                            else
                            {
                                ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_ID = SqlDecimal.Parse(ctxt_SBN_JIGYOUJYOU_CD.Text);
                            }
                        }


                        //運搬先事業場の郵便番号
                        ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_POST = (string.IsNullOrEmpty(ctxt_SBN_GenbaPost.Text)) ? ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_POST : ctxt_SBN_GenbaPost.Text;
                        //運搬先事業場の電話番号
                        ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_TEL = (string.IsNullOrEmpty(ctxt_SBN_GenbaTel.Text)) ? ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_TEL : ctxt_SBN_GenbaTel.Text;
                        //自動場合
                        if (ManiInfo.bIsAutoMode && !string.IsNullOrEmpty(this.cantxt_SBN_Genba_CD.Text))
                        {
                            //処分事業場CD入力された場合
                            if (!string.IsNullOrEmpty(cantxt_SBN_Genba_CD.Text))
                            {
                                //運搬先事業場の住所１～住所４の設定
                                dto = new DenshiSearchParameterDtoCls();
                                if (!string.IsNullOrEmpty(this.ctxt_SBN_KanyuShaNo.Text))
                                {
                                    dto.EDI_MEMBER_ID = this.ctxt_SBN_KanyuShaNo.Text;
                                }
                                else
                                {
                                    dto.EDI_MEMBER_ID = ManiInfo.dt_r18.SBN_SHA_MEMBER_ID;
                                }

                                if (!string.IsNullOrEmpty(ctxt_SBN_JIGYOUJYOU_CD.Text))
                                {
                                    dto.JIGYOUJOU_CD = ctxt_SBN_JIGYOUJYOU_CD.Text;
                                }
                                DataTable dt = DsMasterLogic.GetDenshiGenbaData(dto);
                                if (dt.Rows.Count == 1)
                                {
                                    ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_ADDRESS1 = dt.Rows[0]["JIGYOUJOU_ADDRESS1"].ToString();
                                    ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_ADDRESS2 = dt.Rows[0]["JIGYOUJOU_ADDRESS2"].ToString();
                                    ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_ADDRESS3 = dt.Rows[0]["JIGYOUJOU_ADDRESS3"].ToString();
                                    ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_ADDRESS4 = dt.Rows[0]["JIGYOUJOU_ADDRESS4"].ToString();
                                }
                            }
                        }
                        //手動場合
                        else
                        {
                            //運搬先事業場の住所１～住所４の設定
                            if (!string.IsNullOrEmpty(SBN_GenbaAddr.Text))
                            {
                                var maniLogic = new ManifestoLogic();
                                string tempAddress1;
                                string tempAddress2;
                                string tempAddress3;
                                string tempAddress4;
                                maniLogic.SetAddress1ToAddress4(SBN_GenbaAddr.Text,
                                out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                                ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_ADDRESS1 = tempAddress1;
                                ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_ADDRESS2 = tempAddress2;
                                ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_ADDRESS3 = tempAddress3;
                                ManiInfo.lstDT_R19[nValidIdx].UPNSAKI_JOU_ADDRESS4 = tempAddress4;
                            }
                        }
                    }
                }
                ////********処分情報から最終運搬区間に設定終了***************
                //運搬先事業場区分の設定
                for (int j = 0; j < ManiInfo.lstDT_R19.Count; j++)
                {
                    //中間区間場合１に設定：最終区間場合４を設定
                    ManiInfo.lstDT_R19[j].UPNSAKI_JOU_KBN = (j < ManiInfo.lstDT_R19.Count - 1) ? 1 : 4;
                }
                //運搬情報グリッドの追加削除状態を回復する
                this.cdgv_UnpanInfo.AllowUserToAddRows = this.GetUnpanInfoAddDeleteFlg();

                //*******運搬情報設定終了****************
                //DT_R18データを画面から作成
                //引渡し日
                if (this.cdate_HikiwataDate.Value != null)
                {
                    ManiInfo.dt_r18.HIKIWATASHI_DATE = this.cdate_HikiwataDate.Value.ToString().Substring(0, 10).Replace("/", "");
                }
                //引渡し担当者
                ManiInfo.dt_r18.HIKIWATASHI_TAN_NAME = this.ctxt_HikiwataTantouSha.Text;

                //登録担当者
                ManiInfo.dt_r18.REGI_TAN = this.ctxt_TourokuTantouSha.Text;
                //運搬終了報告済フラグ
                if (ManiInfo.lstDT_R19.Count > 0)
                {
                    //[最終区間の運搬終了日入れ場合、1に設定]
                    string sDate = ManiInfo.lstDT_R19[ManiInfo.lstDT_R19.Count - 1].UPN_END_DATE;
                    // 初期値
                    ManiInfo.dt_r18.UPN_ENDREP_FLAG = SqlInt16.Parse("0");
                    if (!string.IsNullOrEmpty(sDate))
                    {
                        ManiInfo.dt_r18.UPN_ENDREP_FLAG = SqlInt16.Parse("1");
                    }
                }

                //処分終了報告済フラグ
                // 初期値
                ManiInfo.dt_r18.SBN_ENDREP_FLAG = SqlInt16.Parse("0");
                if (this.cdtp_SBNEndDate.Value != null)
                {
                    ManiInfo.dt_r18.SBN_ENDREP_FLAG = SqlInt16.Parse("1");
                }

                //最終処分終了報告済フラグ
                // 初期値
                ManiInfo.dt_r18.LAST_SBN_ENDREP_FLAG = SqlInt16.Parse("0");
                if (this.cdpt_LastSBNEndDate.Value != null)
                {
                    ManiInfo.dt_r18.LAST_SBN_ENDREP_FLAG = SqlInt16.Parse("1");
                }

                //手動モードで報告区分が画面から取得する
                if (!ManiInfo.bIsAutoMode)
                {
                    ManiInfo.dt_r18.SBN_ENDREP_KBN = (string.IsNullOrEmpty(this.cntxt_HoukokuKBN.Text)) ? SqlInt16.Parse("1") : SqlInt16.Parse(this.cntxt_HoukokuKBN.Text);
                }
                //排出事業者情報を設定する
                //排出事業者の加入者番号
                ManiInfo.dt_r18.HST_SHA_EDI_MEMBER_ID = this.ctxt_Haisyutu_KanyushaNo.Text;
                //排出事業者名称
                ManiInfo.dt_r18.HST_SHA_NAME = this.ctxt_HaisyutuGyousyaName.Text;
                //排出事業者郵便番号
                ManiInfo.dt_r18.HST_SHA_POST = this.cnt_HaisyutuGyousyaZip.Text;
                if (ManiInfo.bIsAutoMode) //自動モード場合、DBから検索する
                {
                    dto = new DenshiSearchParameterDtoCls();
                    dto.EDI_MEMBER_ID = ManiInfo.dt_r18.HST_SHA_EDI_MEMBER_ID;
                    DataTable dt = DsMasterLogic.GetDenshiGyoushaData(dto);
                    if (dt.Rows.Count == 1)
                    {
                        //排出事業者所在地1
                        if (dt.Rows[0]["JIGYOUSHA_ADDRESS1"] != null)
                            ManiInfo.dt_r18.HST_SHA_ADDRESS1 = dt.Rows[0]["JIGYOUSHA_ADDRESS1"].ToString();
                        //排出事業者所在地2
                        if (dt.Rows[0]["JIGYOUSHA_ADDRESS2"] != null)
                            ManiInfo.dt_r18.HST_SHA_ADDRESS2 = dt.Rows[0]["JIGYOUSHA_ADDRESS2"].ToString();
                        //排出事業者所在地3
                        if (dt.Rows[0]["JIGYOUSHA_ADDRESS3"] != null)
                            ManiInfo.dt_r18.HST_SHA_ADDRESS3 = dt.Rows[0]["JIGYOUSHA_ADDRESS3"].ToString();
                        //排出事業者所在地4
                        if (dt.Rows[0]["JIGYOUSHA_ADDRESS4"] != null)
                            ManiInfo.dt_r18.HST_SHA_ADDRESS4 = dt.Rows[0]["JIGYOUSHA_ADDRESS4"].ToString();
                    }
                }
                //手動場合
                else
                {
                    string Address = this.ctxt_HaisyutuGyousyaAddr.Text;
                    var maniLogic = new ManifestoLogic();
                    string tempAddress1;
                    string tempAddress2;
                    string tempAddress3;
                    string tempAddress4;
                    maniLogic.SetAddress1ToAddress4(Address,
                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                    ManiInfo.dt_r18.HST_SHA_ADDRESS1 = tempAddress1;
                    ManiInfo.dt_r18.HST_SHA_ADDRESS2 = tempAddress2;
                    ManiInfo.dt_r18.HST_SHA_ADDRESS3 = tempAddress3;
                    ManiInfo.dt_r18.HST_SHA_ADDRESS4 = tempAddress4;
                }

                //排出事業者の代表電話番号
                ManiInfo.dt_r18.HST_SHA_TEL = this.cnt_HaisyutuGyousyaTel.Text;

                //排出事業場情報を設定する

                //排出事業場名称
                ManiInfo.dt_r18.HST_JOU_NAME = this.ctxt_HaisyutuGenbaName.Text;
                //排出事業場所在地の郵便番号
                ManiInfo.dt_r18.HST_JOU_POST_NO = this.ctxt_Haisyutu_GenbaZip.Text;
                if (ManiInfo.bIsAutoMode) //自動モード場合、住所がDBから検索する
                {
                    if (!string.IsNullOrEmpty(this.cantxt_HaisyutuGenbaCd.Text))
                    {
                        dto = new DenshiSearchParameterDtoCls();
                        dto.EDI_MEMBER_ID = ManiInfo.dt_r18.HST_SHA_EDI_MEMBER_ID;
                        dto.GENBA_CD = this.cantxt_HaisyutuGenbaCd.Text;
                        dto.GYOUSHA_CD = this.cantxt_HaisyutuGyousyaCd.Text;
                        if (!string.IsNullOrEmpty(this.ctxt_JIGYOUJYOU_CD.Text))
                        {
                            dto.JIGYOUJOU_CD = this.ctxt_JIGYOUJYOU_CD.Text;
                        }
                        DataTable dt = DsMasterLogic.GetDenshiGenbaData(dto);
                        if (dt.Rows.Count == 1)
                        {
                            if (dt.Rows[0]["JIGYOUJOU_ADDRESS1"] != null)
                                ManiInfo.dt_r18.HST_JOU_ADDRESS1 = dt.Rows[0]["JIGYOUJOU_ADDRESS1"].ToString();
                            if (dt.Rows[0]["JIGYOUJOU_ADDRESS1"] != null)
                                ManiInfo.dt_r18.HST_JOU_ADDRESS2 = dt.Rows[0]["JIGYOUJOU_ADDRESS2"].ToString();
                            if (dt.Rows[0]["JIGYOUJOU_ADDRESS1"] != null)
                                ManiInfo.dt_r18.HST_JOU_ADDRESS3 = dt.Rows[0]["JIGYOUJOU_ADDRESS3"].ToString();
                            if (dt.Rows[0]["JIGYOUJOU_ADDRESS1"] != null)
                                ManiInfo.dt_r18.HST_JOU_ADDRESS4 = dt.Rows[0]["JIGYOUJOU_ADDRESS4"].ToString();
                        }
                    }
                }
                else//手動モード場合、住所がSUBSTRING
                {
                    string Address = this.ctxt_HaisyutuGenbaAddr.Text;
                    if (!string.IsNullOrEmpty(Address))
                    {
                        var maniLogic = new ManifestoLogic();
                        string tempAddress1;
                        string tempAddress2;
                        string tempAddress3;
                        string tempAddress4;
                        maniLogic.SetAddress1ToAddress4(Address,
                        out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                        ManiInfo.dt_r18.HST_JOU_ADDRESS1 = tempAddress1;
                        ManiInfo.dt_r18.HST_JOU_ADDRESS2 = tempAddress2;
                        ManiInfo.dt_r18.HST_JOU_ADDRESS3 = tempAddress3;
                        ManiInfo.dt_r18.HST_JOU_ADDRESS4 = tempAddress4;
                    }
                }
                //排出事業場電話番号
                ManiInfo.dt_r18.HST_JOU_TEL = this.cnt_HaisyutuGenbaTel.Text;

                //処分受入量
                if (!string.IsNullOrEmpty(this.cntxt_Jyunyuryo.Text))
                {
                    ManiInfo.dt_r18.RECEPT_SUU = SqlDecimal.Parse(this.cntxt_Jyunyuryo.Text.Replace(",", ""));
                }
                //処分受入量の単位CD
                if (!string.IsNullOrEmpty(this.cantxt_JyunyuryoUnitCD.Text))
                {
                    int len = this.cantxt_JyunyuryoUnitCD.Text.Length;
                    ManiInfo.dt_r18.RECEPT_UNIT_CODE = this.cantxt_JyunyuryoUnitCD.Text.Substring(len - 1, 1);
                }
                //廃棄物種類から大中小分類を設定する
                object tmpObjHaiki = this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_SHURUI_CD"].Value;
                if (tmpObjHaiki != null)
                {
                    if (!string.IsNullOrEmpty(tmpObjHaiki.ToString()))
                    {
                        string haikisyuruyiCD = tmpObjHaiki.ToString();
                        //大分類コード
                        ManiInfo.dt_r18.HAIKI_DAI_CODE = haikisyuruyiCD.Substring(0, 2);
                        //中分類コード
                        ManiInfo.dt_r18.HAIKI_CHU_CODE = haikisyuruyiCD.Substring(2, 1);
                        //小分類コード
                        ManiInfo.dt_r18.HAIKI_SHO_CODE = haikisyuruyiCD.Substring(3, 1);
                        //細分類コード
                        ManiInfo.dt_r18.HAIKI_SAI_CODE = haikisyuruyiCD.Substring(4, 3);
                        //廃棄物種類名
                        tmpObjHaiki = this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_SHURUI_NAME"].Value;
                        if (tmpObjHaiki != null)
                        {
                            ManiInfo.dt_r18.HAIKI_SHURUI = tmpObjHaiki.ToString();
                        }

                        // 廃棄物の大分類名称
                        ManiInfo.dt_r18.HAIKI_BUNRUI = this.Logic.GetHaikiBunruiName(ManiInfo.dt_r18.HAIKI_DAI_CODE);
                    }
                }
                //廃棄物名称
                tmpObjHaiki = this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_NAME"].Value;
                if (tmpObjHaiki != null)
                {
                    //廃棄物名称
                    ManiInfo.dt_r18.HAIKI_NAME = tmpObjHaiki.ToString();
                }
                //廃棄物の数量
                tmpObjHaiki = this.cdgv_Haikibutu.Rows[0].Cells["HAIKI_SUU"].Value;
                if (tmpObjHaiki != null)
                {
                    //廃棄物の数量
                    ManiInfo.dt_r18.HAIKI_SUU = SqlDecimal.Parse(tmpObjHaiki.ToString().Replace(",", ""));
                }
                //廃棄物の数量単位コード
                tmpObjHaiki = this.cdgv_Haikibutu.Rows[0].Cells["UNIT_CD"].Value;
                if (tmpObjHaiki != null
                    && !string.IsNullOrEmpty(tmpObjHaiki.ToString()))
                {
                    //廃棄物の数量単位コード
                    int len = (tmpObjHaiki as string).Length;
                    ManiInfo.dt_r18.HAIKI_UNIT_CODE = (tmpObjHaiki as string).Substring(len - 1, 1);
                }
                //数量確定者コード
                tmpObjHaiki = this.cdgv_Haikibutu.Rows[0].Cells["SUU_KAKUTEI_CODE"].Value;
                if (tmpObjHaiki != null)
                {
                    //数量確定者コード
                    ManiInfo.dt_r18.SUU_KAKUTEI_CODE = tmpObjHaiki.ToString();
                }
                //廃棄物の確定数量と確定単位コードの設定
                if (!string.IsNullOrEmpty(ManiInfo.dt_r18.SUU_KAKUTEI_CODE))
                //&& "2".Equals(this.cntxt_InputKBN.Text)（手動時のみ登録する）//仕様変更のためコメントする
                {
                    if (ManiInfo.dt_r18.SUU_KAKUTEI_CODE == "01")//数量確定者が排出事業者場合
                    {
                        ManiInfo.dt_r18.HAIKI_KAKUTEI_SUU = ManiInfo.dt_r18.HAIKI_SUU;
                        //廃棄物の確定数量の単位コード
                        ManiInfo.dt_r18.HAIKI_KAKUTEI_UNIT_CODE = ManiInfo.dt_r18.HAIKI_UNIT_CODE;
                    }
                    else if (ManiInfo.dt_r18.SUU_KAKUTEI_CODE == "02")//数量確定者が処分事業者場合
                    {
                        ManiInfo.dt_r18.HAIKI_KAKUTEI_SUU = ManiInfo.dt_r18.RECEPT_SUU;//前頭設定された処分受入量
                        //廃棄物の確定数量の単位コード
                        ManiInfo.dt_r18.HAIKI_KAKUTEI_UNIT_CODE = ManiInfo.dt_r18.RECEPT_UNIT_CODE;
                    }
                    else
                    {
                        //廃棄物の確定者が運搬業者の場合
                        for (int i = 0; i < 5; i++)
                        {
                            if (i == int.Parse(ManiInfo.dt_r18.SUU_KAKUTEI_CODE) - 3)
                            {
                                if (cdgv_UnpanInfo.Rows.Count > i)
                                {
                                    if (cdgv_UnpanInfo.Rows[i].Tag != null)
                                    {
                                        ManiInfo.dt_r18.HAIKI_KAKUTEI_SUU = (cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cntxt_UnpanRyo;
                                        //廃棄物の確定数量の単位コード
                                        string unitCd = (cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cantxt_UnpanRyoUnitCd;
                                        if (!string.IsNullOrEmpty(unitCd))
                                        {
                                            ManiInfo.dt_r18.HAIKI_KAKUTEI_UNIT_CODE = unitCd.Substring(unitCd.Length - 1, 1);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                //荷姿コード
                object tmpObjNisu = this.cdgv_Haikibutu.Rows[0].Cells["NISUGATA_CD"].Value;
                if (tmpObjNisu != null)
                {
                    ManiInfo.dt_r18.NISUGATA_CODE = tmpObjNisu.ToString();
                }
                //荷姿名
                tmpObjNisu = this.cdgv_Haikibutu.Rows[0].Cells["NISUGATA_NAME"].Value;
                if (tmpObjNisu != null)
                {
                    ManiInfo.dt_r18.NISUGATA_NAME = tmpObjNisu.ToString();
                }
                //荷姿の数量
                tmpObjNisu = this.cdgv_Haikibutu.Rows[0].Cells["NISUGATA_SUU"].Value;
                if (tmpObjNisu != null)
                {
                    ManiInfo.dt_r18.NISUGATA_SUU = tmpObjNisu.ToString().Replace(",", "");
                }
                //処分方法コード
                if (!string.IsNullOrEmpty(this.cantxt_SBN_houhouCD.Text))
                {
                    ManiInfo.dt_r18.SBN_WAY_CODE = SqlInt16.Parse(this.cantxt_SBN_houhouCD.Text);
                }
                //処分方法名
                ManiInfo.dt_r18.SBN_WAY_NAME = this.ctxt_SBN_houhouName.Text;

                // 処分報告情報承認待ちフラグ
                // デフォルト値
                ManiInfo.dt_r18.SBN_SHOUNIN_FLAG = 1;
                if (!string.IsNullOrEmpty(this.ctxt_SBN_Shouni_Flag.Text))
                {
                    ManiInfo.dt_r18.SBN_SHOUNIN_FLAG = SqlDecimal.Parse(this.ctxt_SBN_Shouni_Flag.Text);
                }

                //処分終了日
                if (this.cdtp_SBNEndDate.Value != null)
                {
                    ManiInfo.dt_r18.SBN_END_DATE = this.cdtp_SBNEndDate.Text.Substring(0, 10).Replace("/", "");
                }
                //廃棄物の受領日
                if (this.cdtp_HaikiAcceptDate.Value != null)
                {
                    ManiInfo.dt_r18.HAIKI_IN_DATE = this.cdtp_HaikiAcceptDate.Text.Substring(0, 10).Replace("/", "");
                }
                //処分報告運搬担当者
                ManiInfo.dt_r18.UPN_TAN_NAME = string.IsNullOrEmpty(this.ctxt_SBN_UnpanTantouShaName.Text) ? null : this.ctxt_SBN_UnpanTantouShaName.Text;
                //処分報告車両名称
                ManiInfo.dt_r18.CAR_NO = string.IsNullOrEmpty(this.ctxt_cantxt_SBN_SyaryoNoName.Text) ? null : this.ctxt_cantxt_SBN_SyaryoNoName.Text;
                //報告担当者
                ManiInfo.dt_r18.REP_TAN_NAME = string.IsNullOrEmpty(this.ctxt_SBN_HoukokuTantouShaName.Text) ? null : this.ctxt_SBN_HoukokuTantouShaName.Text;
                //処分担当者
                ManiInfo.dt_r18.SBN_TAN_NAME = string.IsNullOrEmpty(this.ctxt_SBN_SBNTantouShaName.Text) ? null : this.ctxt_SBN_SBNTantouShaName.Text;
                //処分終了報告日
                if (this.cdtp_SBNEndDate.Value != null)
                {
                    ManiInfo.dt_r18.SBN_END_REP_DATE = this.cdtp_SBNEndDate.Value.ToString().Substring(0, 10).Replace("/", "");
                }
                //処分報告備考
                ManiInfo.dt_r18.SBN_REP_BIKOU = this.ctxt_SBN_Bikou.Text;
                //予約登録の修正権限コード
                ManiInfo.dt_r18.KENGEN_CODE = SqlInt16.Parse(this.cntxt_ModifyRight.Text);
                //最終処分事業場記載フラグ	
                if (ccbx_YitakuKeyaku.Checked)
                {//委託契約書記載のとおりチェックのみ
                    ManiInfo.dt_r18.LAST_SBN_JOU_KISAI_FLAG = "0";
                }
                if (ccbx_Toulanshitei.Checked)
                {//当欄指定のとおり
                    ManiInfo.dt_r18.LAST_SBN_JOU_KISAI_FLAG = "1";
                }
                //最終処分終了日
                if (!ManiInfo.bIsAutoMode)//手動場合設定する
                {
                    if (ManiInfo.lstDT_R13.Count > 0)//最終処分終了日、事業場情報（実績）データある場合
                    {
                        // 最終処分終了日[全ての行のうち最新の日付を設定]
                        DateTime latestLastSbnDate = DateTime.MinValue;
                        foreach (var dtR13 in ManiInfo.lstDT_R13)
                        {
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                            //DateTime tempDate = DateTime.Now;
                            DateTime tempDate = this.Logic.parentbaseform.sysDate;
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                            if (string.IsNullOrEmpty(dtR13.LAST_SBN_END_DATE))
                            {
                                latestLastSbnDate = DateTime.MinValue;
                                break;
                            }
                            else
                            {
                                tempDate = DateTime.ParseExact(dtR13.LAST_SBN_END_DATE, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                            }

                            if (DateTime.Compare(latestLastSbnDate, tempDate.Date) < 0)
                            {
                                latestLastSbnDate = tempDate.Date;
                            }
                        }

                        if (DateTime.Compare(DateTime.MinValue, latestLastSbnDate) < 0)
                        {
                            ManiInfo.dt_r18.LAST_SBN_END_DATE = latestLastSbnDate.ToString("yyyyMMdd");
                        }
                    }
                }

                //有害物質情報件数	
                ManiInfo.dt_r18.YUUGAI_CNT = SqlInt16.Parse(ManiInfo.lstDT_R02.Count.ToString());
                //収集運搬情報件数
                ManiInfo.dt_r18.UPN_ROUTE_CNT = SqlInt16.Parse(ManiInfo.lstDT_R19.Count.ToString());
                //最終処分事業場（予定）情報件数
                ManiInfo.dt_r18.LAST_SBN_PLAN_CNT = SqlInt16.Parse(ManiInfo.lstDT_R04.Count.ToString());
                //最終処分終了日･事業場情報件数	
                ManiInfo.dt_r18.LAST_SBN_CNT = SqlInt16.Parse(ManiInfo.lstDT_R13.Count.ToString());
                //備考情報件数
                ManiInfo.dt_r18.BIKOU_CNT = SqlInt16.Parse(ManiInfo.lstDT_R06.Count.ToString());
                //連絡番号件数
                ManiInfo.dt_r18.RENRAKU_CNT = SqlInt16.Parse(ManiInfo.lstDT_R05.Count.ToString());

                //二次マニ登録場合
                if (this.Logic.maniFlag == 2)
                {
                    //中間処理産業廃棄物情報管理方法フラグ
                    if (this.ccbx_Touranshitei.Checked)
                    {
                        ManiInfo.dt_r18.FIRST_MANIFEST_FLAG = "1";
                    }
                    else if (this.ccbx_ItijiFuyou.Checked)
                    {
                        ManiInfo.dt_r18.FIRST_MANIFEST_FLAG = "2";
                    }
                    else if (this.ccbx_ChouboKisai.Checked)
                    {
                        ManiInfo.dt_r18.FIRST_MANIFEST_FLAG = "3";
                    }
                }

                //中間処理産業廃棄物情報件数
                if (ManiInfo.lstDT_R08 != null && !string.IsNullOrWhiteSpace(ManiInfo.lstDT_R08.Count.ToString()) && this.cntxt_ManiKBN.Text != "1")
                {
                    ManiInfo.dt_r18.FIRST_MANIFEST_CNT = SqlDecimal.Parse(ManiInfo.lstDT_R08.Count.ToString());
                }


                //加入者番号
                //***加入者番号[DT_MF_MEMBER]***Start**************************************
                //排出事業者加入者番号
                ManiInfo.dt_mf_member.HST_MEMBER_ID = ManiInfo.dt_r18.HST_SHA_EDI_MEMBER_ID;
                //運搬情報あった場合
                if (ManiInfo.lstDT_R19.Count > 0)
                {
                    //収集運搬業者1加入者番号
                    ManiInfo.dt_mf_member.UPN1_MEMBER_ID = ManiInfo.lstDT_R19[0].UPN_SHA_EDI_MEMBER_ID;
                }
                if (ManiInfo.lstDT_R19.Count > 1)
                {
                    //収集運搬業者2加入者番号
                    ManiInfo.dt_mf_member.UPN2_MEMBER_ID = ManiInfo.lstDT_R19[1].UPN_SHA_EDI_MEMBER_ID;
                }
                if (ManiInfo.lstDT_R19.Count > 2)
                {
                    //収集運搬業者3加入者番号
                    ManiInfo.dt_mf_member.UPN3_MEMBER_ID = ManiInfo.lstDT_R19[2].UPN_SHA_EDI_MEMBER_ID;
                }
                if (ManiInfo.lstDT_R19.Count > 3)
                {
                    //収集運搬業者4加入者番号
                    ManiInfo.dt_mf_member.UPN4_MEMBER_ID = ManiInfo.lstDT_R19[3].UPN_SHA_EDI_MEMBER_ID;
                }
                if (ManiInfo.lstDT_R19.Count > 4)
                {
                    //収集運搬業者5加入者番号
                    ManiInfo.dt_mf_member.UPN5_MEMBER_ID = ManiInfo.lstDT_R19[4].UPN_SHA_EDI_MEMBER_ID;
                }
                //処分業者加入者番号
                ManiInfo.dt_mf_member.SBN_MEMBER_ID = ManiInfo.dt_r18.SBN_SHA_MEMBER_ID;
                //***加入者番号[DT_MF_MEMBER]***End**************************************

                return ManiInfo;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("MakeAllData", ex1);
                this.Logic.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("MakeAllData", ex);
                this.Logic.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return null;
            }

        }
        /// <summary>
        /// JWNET登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RegistManiToJWNET(object sender, EventArgs e)
        {
            this.RegistDenshiManifest(false);//JWNET登録
        }

        /// <summary>
        /// 保留登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RegistManiHouryou(object sender, EventArgs e)
        {
            this.RegistDenshiManifest(true);//保留登録
        }
        /// <summary>
        /// マニ登録処理メッソド
        /// </summary>
        /// <param name="bIsHouryou">保留保存フラグ</param>
        public void RegistDenshiManifest(bool bIsHouryou = false)
        {

            //新規モード以外場合「送信中」データが修正、削除不可
            if ((this.Logic.Mode != WINDOW_TYPE.NEW_WINDOW_FLAG) && !(this.Logic.HoryuFlg))
            {
                if (this.Logic.ManiInfo.dt_mf_toc.STATUS_DETAIL == 1)
                {
                    this.Logic.msgLogic.MessageBoxShow("E128");
                    return;
                }
            }

            //INXS start check delete INXS manifets refs #158004
            if (AppConfig.AppOptions.IsInxsManifest() && this.Logic.Mode == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                this.Logic.isUploadToInxs = this.Logic.inxsManifestLogic.IsUploadDenshiManifestToInxs(this.Logic.ManiInfo.dt_r18.KANRI_ID);
            }
            //INXS end

            //削除モードで、未登録データの削除
            if (this.Logic.Mode == WINDOW_TYPE.DELETE_WINDOW_FLAG && this.Logic.HoryuDelFlg)
            {
                this.Logic.MDelete(this.Logic.ManiInfo.dt_r18,this.Logic.ManiInfo.que_Info);
                return;
            }

            //削除モード
            if (this.Logic.Mode == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                if (this.Logic.msgLogic.MessageBoxShow("C046", "削除") == DialogResult.Yes)
                {

                    // 紐付けマニが存在しているかチェック
                    // 紐付けが解除されていない場合は削除不可
                    if (this.Logic.ManiInfo.dt_r18ExOld != null
                        && !this.Logic.ManiInfo.dt_r18ExOld.SYSTEM_ID.IsNull
                        && this.Logic.ChkRelation(this.Logic.ManiInfo.dt_r18ExOld.SYSTEM_ID, this.Logic.maniFlag))
                    {
                        if (this.Logic.maniFlag == 1)
                        {
                            this.Logic.msgLogic.MessageBoxShow("E177", "2次");

                        }
                        else
                        {
                            this.Logic.msgLogic.MessageBoxShow("E177", "1次");
                        }

                        return;
                    }

                    //INXS start confirm delete INXS manifets refs #158004
                    if (AppConfig.AppOptions.IsInxsManifest())
                    {
                        if (this.Logic.isUploadToInxs && this.Logic.msgLogic.MessageBoxShow("C117") == DialogResult.No)
                        {
                            return;
                        }
                    }
                    //INXS end

                    //削除を行う
                    this.Logic.Delete(this.Logic.ManiInfo.bIsAutoMode,
                                       bIsHouryou,
                                       this.Logic.ManiInfo.dt_r18,
                                       this.Logic.ManiInfo.que_Info,
                                       this.Logic.ManiInfo.dt_mf_toc,
                                       this.Logic.ManiInfo.dt_mf_member,
                                       this.Logic.ManiInfo.dt_r18ExOld,
                                       this.Logic.ManiInfo.lstDT_R19_EX,
                                       this.Logic.ManiInfo.lstDT_R04_EX,
                                       this.Logic.ManiInfo.lstDT_R13_EX,
                                       this.Logic.ManiInfo.lstDT_R08_EX);
                    return;
                }
            }

            Cursor.Current = Cursors.WaitCursor;

            // 各明細の削除チェックボックスにチェックが入っているか確認する。
            this.Logic.CheckDeleteRow();
            // 明細に削除チェックが１つでも入っている場合はエラーとする。
            if (this.Logic.delChk)
            {
                this.Logic.msgLogic.MessageBoxShowError("明細行に削除対象があります。明細削除を実行してください。");
                return;
            }

            //中間処理産業廃棄物のチェックボックスのチェック処理
            if (this.Logic.ChkFirstManifest())
            {
                return;
            }

            //【中間処理産業廃棄物-マニフェスト番号／交付】がセットされたレコードが1件もない場合
            //はエラーメッセージを表示し登録不可
            if (!this.Logic.ChkRegistFirstManifest())
            {
                this.Logic.msgLogic.MessageBoxShow("E001", "中間処理産業廃棄物のマニフェスト番号／交付");
                return;
            }

            //データが画面から取得する
            bool catchErr = false;
            DenshiManifestInfoCls ManiInfo = MakeAllData(out catchErr);
            if (catchErr)
            {
                return;
            }
            ManiInfo.bHouryuFlg = bIsHouryou;

            //入力必須チェック
            //登録前にチェック
            if (!this.Logic.CHk_MustbeInputItem(ManiInfo))
            {
                this.cdgv_UnpanInfo.AllowUserToAddRows = this.GetUnpanInfoAddDeleteFlg();
                return;
            }

            //引渡日に未来日を設定してもJWNET送信できてしまう  thongh 2015/08/21 #12431 start
            if (!bIsHouryou && this.cdate_HikiwataDate.Value != null
                && !string.IsNullOrEmpty(this.cntxt_ManiKBN.Text)
                && !string.IsNullOrEmpty(this.cntxt_InputKBN.Text))
            {
                DateTime Today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                if (this.cntxt_ManiKBN.Text == "2" && this.cntxt_InputKBN.Text == "1" && DateTime.Parse(Convert.ToString(this.cdate_HikiwataDate.Value)) > Today)
                {
                    this.Logic.msgLogic.MessageBoxShow("E243");
                    this.cdate_HikiwataDate.Focus();
                    return;
                }
            }
            //引渡日に未来日を設定してもJWNET送信できてしまう thongh 2015/08/21 #12431 end

            //マニフェスト番号チェック[新規モードで手動のマニ登録の場合]
            if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG && !ManiInfo.bIsAutoMode)
            {
                if (string.IsNullOrEmpty(ManiInfo.dt_r18.MANIFEST_ID))
                {
                    if (this.Logic.msgLogic.MessageBoxShow("C046", "マニフェスト番号が未入力です。登録") != DialogResult.Yes)
                    {
                        this.cantxt_ManifestNo.Focus();
                        this.cantxt_ManifestNo.SelectAll();
                        this.cdgv_UnpanInfo.AllowUserToAddRows = this.GetUnpanInfoAddDeleteFlg();
                        return;
                    }
                }
            }
            else if (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG && !ManiInfo.bIsAutoMode)
            {
                if (string.IsNullOrEmpty(ManiInfo.dt_r18.MANIFEST_ID))
                {
                    if (this.Logic.msgLogic.MessageBoxShow("C046", "マニフェスト番号が未入力です。更新") != DialogResult.Yes)
                    {
                        this.cantxt_ManifestNo.Focus();
                        this.cantxt_ManifestNo.SelectAll();
                        this.cdgv_UnpanInfo.AllowUserToAddRows = this.GetUnpanInfoAddDeleteFlg();
                        return;
                    }
                }
            }
            if (ManiInfo.dt_r18 != null)
            {
                //新規以外場合は最新LATEST_SEQと修正/取消SEQの取得
                if (this.Logic.Mode != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    //最新LATEST_SEQと修正/取消SEQの取得
                    DataTable dt = this.Logic.DT_MF_TOCDao.GetLATEST_APPROVAL_SEQ(" WHERE KANRI_ID = '" + this.Logic.KanriId + "'");
                    this.Logic.LastSEQ = Convert.ToInt16(dt.Rows[0]["LATEST_SEQ"]);
                    if (DBNull.Value != dt.Rows[0]["APPROVAL_SEQ"])
                    {
                        this.Logic.APPROVAL_SEQ = Convert.ToInt16(dt.Rows[0]["APPROVAL_SEQ"]);
                    }
                }

                //新規モード
                if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.Logic.Insert(ManiInfo.bIsAutoMode,
                                      ManiInfo.bHouryuFlg,
                                      ManiInfo.dt_r18,
                                      ManiInfo.que_Info,
                                      ManiInfo.dt_mf_toc,
                                      ManiInfo.dt_mf_member,
                                      ManiInfo.lstDT_R19,
                                      ManiInfo.lstDT_R02,
                                      ManiInfo.lstDT_R04,
                                      ManiInfo.lstDT_R05,
                                      ManiInfo.lstDT_R06,
                                      ManiInfo.lstDT_R13,
                                      ManiInfo.lstDT_R08,                    //一次マニフェスト情報
                                      ManiInfo.dt_r18ExOld,
                                      ManiInfo.lstDT_R19_EX,
                                      ManiInfo.lstDT_R04_EX,
                                      ManiInfo.lstDT_R13_EX,
                                      ManiInfo.lstDT_R08_EX,                 //一次マニフェスト情報拡張
                                      ManiInfo.lstT_MANIFEST_RELATION        //紐付け情報
                                      );
                    if (this.Logic.isRegistErr) { return; }
                    // 権限チェック
                    if (Manager.CheckAuthority("G141", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // 画面をクリアし新規入力状態とする。
                        this.Logic.ChangeNewWindowMode();
                    }
                    else
                    {
                        // 新規権限が無ければ閉じる
                        this.ParentForm.Close();
                    }
                }
                //修正モード
                else if (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    // 更新前に最新の状態を取得
                    //this.Logic.ManiInfo = this.Logic.GetManiInfoFromDB(this.Logic.KanriId, this.Logic.Seq);

                    // ２次マニの場合
                    if (this.Logic.maniFlag == 2)
                    {
                        // ２次マニ情報から紐付情報の有無と最終処分終了日の比較を行う。
                        // 電子１次に紐付されていて、かつ電子１次の最終処分終了日が設定済の場合、かつ２次マニの最終処分終了日が設定済の場合に確認メッセージを表示する。
                        if (this.Logic.CheckLastSbnDate())
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            if (msgLogic.MessageBoxShow("C046", "1次の最終処分終了日と2次の最終処分終了日に差異があります。\n登録") != DialogResult.Yes)
                            {
                                return;
                            }
                        }
                    }

                    // 最終処分終了済みかつ、最終処分情報に未入力が存在する場合、報告済みの内容と差異が発生する可能性があるのでアラートを表示
                    var commonManiLogic = new ManifestoLogic();
                    var isFixedFirstElecMani = commonManiLogic.IsFixedRelationFirstMani(this.Logic.ManiInfo.dt_r18ExOld.SYSTEM_ID, 4);
                    if (this.Logic.maniFlag == 2 && isFixedFirstElecMani && !this.existAllLastSbnInfo)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        if (msgLogic.MessageBoxShow("C080") == DialogResult.No)
                        {
                            return;
                        }
                    }

                    this.Logic.Update(ManiInfo.bIsAutoMode,
                                      ManiInfo.bHouryuFlg,
                                      ManiInfo.dt_r18,
                                      ManiInfo.que_Info,
                                      this.Logic.ManiInfo.dt_mf_toc,
                                      this.Logic.ManiInfo.dt_mf_member,
                                      ManiInfo.lstDT_R19,
                                      ManiInfo.lstDT_R02,
                                      ManiInfo.lstDT_R04,
                                      ManiInfo.lstDT_R05,
                                      ManiInfo.lstDT_R06,
                                      ManiInfo.lstDT_R13,
                                      ManiInfo.lstDT_R08,                            //一次マニフェスト情報
                                      this.Logic.ManiInfo.dt_r18ExOld,
                                      this.Logic.ManiInfo.lstDT_R19_EX,
                                      this.Logic.ManiInfo.lstDT_R04_EX,
                                      this.Logic.ManiInfo.lstDT_R13_EX,
                                      this.Logic.ManiInfo.lstDT_R08_EX,              //電子最終処分拡張
                                      this.Logic.ManiInfo.lstT_MANIFEST_RELATION     //紐付け情報      
                              );
                    if (this.Logic.isRegistErr) { return; }
                    this.Logic.Seq = (SqlInt16)ManiInfo.dt_r18.SEQ;

                    // 紐づいているマニフェストの更新
                    if (!bIsHouryou && !ManiInfo.bIsAutoMode)
                    {
                        this.Logic.UpdateFirstMani();
                        if (this.Logic.isRegistErr) { return; }
                    }

                    // 権限チェック
                    if (Manager.CheckAuthority("G141", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // 画面をクリアし新規入力状態とする。
                        this.Logic.ChangeNewWindowMode();
                    }
                    else
                    {
                        // 新規権限が無ければ閉じる
                        this.ParentForm.Close();
                    }
                }
                //参照モード
                else if (this.Logic.Mode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {


                }
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 各種グリッドの行が有効チェック処理
        /// </summary>
        /// <returns></returns>
        public bool IsValidRowOfEveryInfo(DataGridViewRow dgvRow, bool IsUnpanInfo = false)
        {
            bool bIsValidRow = false;//行の有効フラグ
            //行の有効チェック
            // 最終処分の場所（予定）、運搬情報、最終処分事業場（実績）は、１番目カラムが削除チェックボックス、２番目カラムが番号のため、チェックしない。
            // 産業廃棄物、中間処理産業廃棄物は、１番目カラムが削除チェックボックスのため、チェックしない。
            int startIndex = 2;
            if (dgvRow.DataGridView.Name == "cdgv_Tyukanshori") startIndex = 1;
            if (dgvRow.DataGridView.Name == "cdgv_Haikibutu") startIndex = 1;

            for (int j = startIndex; j < dgvRow.Cells.Count; j++)
            {
                //表示されるカラムのチェック
                object tmpObj = dgvRow.Cells[j].Value;
                if (tmpObj != null)
                {
                    if (!string.IsNullOrEmpty(tmpObj.ToString()))
                    {
                        bIsValidRow = true;
                        break;
                    }
                }
            }

            if (!bIsValidRow)
            {
                //運搬情報場合は報告情報有効チェック
                if (IsUnpanInfo)
                {
                    if ((dgvRow.Tag as UnpanHoukokuDataDTOCls) != null)
                    {
                        bIsValidRow = (dgvRow.Tag as UnpanHoukokuDataDTOCls).IsNotEmpty();
                    }
                }
            }

            return bIsValidRow;
        }

        #endregion メソッド


        #region イベント処理
        /// <summary>
        /// 運搬情報グリッドの行は選択変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_UnpanInfo_SelectionChanged(object sender, EventArgs e)
        {
            if (cdgv_UnpanInfo.CurrentRow != null)
            {
                //車輌番号検索とチェック条件の設定
                string gyoushaCD = cdgv_UnpanInfo.CurrentRow.Cells["UPN_SHA_CD"].Value != null ?
                    cdgv_UnpanInfo.CurrentRow.Cells["UPN_SHA_CD"].Value.ToString() : string.Empty;
                //チェック条件の設定
                this.Hidden_UnpanGyoushaCD.Text = gyoushaCD;
                //検索条件の設定
                //最新業者CDより運搬報告の車輌検索条件を設定する
                r_framework.Dto.JoinMethodDto methodDto = this.cantxt_SyaryoNo.popupWindowSetting[0];
                r_framework.Dto.SearchConditionsDto searchDto = methodDto.SearchCondition[0];
                if (methodDto != null && searchDto != null)
                {
                    searchDto.Value = gyoushaCD;
                }

                // 運搬情報フォーカスインすると運搬担当者名や報告担当者名がクリアされてしまう問題の対策
                string ediMemberId = cdgv_UnpanInfo.CurrentRow.Cells["UPN_SHA_EDI_MEMBER_ID"].Value != null ?
                    cdgv_UnpanInfo.CurrentRow.Cells["UPN_SHA_EDI_MEMBER_ID"].Value.ToString() : string.Empty;
                this.Hidden_KanyushaNo.Text = ediMemberId;

                this.GetUnpanHoukokuDataDtoToForm();
            }
        }

        /// <summary>
        /// 行追加後イベント[区間番号の設定、区間数の制限]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_UnpanInfo_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //行数が５個の制限
            cdgv_UnpanInfo.AllowUserToAddRows = !(cdgv_UnpanInfo.Rows.Count > 5);

            //行番号の設定
            for (int i = 0; i < cdgv_UnpanInfo.Rows.Count; i++)
            {
                cdgv_UnpanInfo.Rows[i].Cells[1].Value = i + 1;
            }
        }
        /// <summary>
        /// 行の削除後実行イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_UnpanInfo_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //行数が５個の制限
            cdgv_UnpanInfo.AllowUserToAddRows = !(cdgv_UnpanInfo.Rows.Count > 5);
            //行番号の設定
            for (int i = 0; i < cdgv_UnpanInfo.Rows.Count; i++)
            {
                cdgv_UnpanInfo.Rows[i].Cells[1].Value = i + 1;
            }
            //削除後の行チェック処理を行う
            this.Logic.ChkUnpansakiJigyoujou(false);
        }

        /// <summary>
        /// 排出事業者情報を削除ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_HaisyutuGyousyaDel_Click(object sender, EventArgs e)
        {
            //業者情報のクリアする
            if (!this.ClearEachControl(pnl_Hs.Controls))
            {
                return;
            }

            //連携事業場情報クリアする
            if (!this.ClearEachControl(pnl_Hb.Controls))
            {
                return;
            }
        }
        /// <summary>
        /// 排出事業場の削除ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_HaisyutuGenbaDel_Click(object sender, EventArgs e)
        {
            //連携事業場情報クリアする
            if (!this.ClearEachControl(pnl_Hb.Controls))
            {
                return;
            }
        }
        /// <summary>
        /// 処分受託者情報を削除ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SBNGyousyaDel_Click(object sender, EventArgs e)
        {
            //業者情報のクリアする
            if (!this.ClearEachControl(pnl_SBN_JYUTAKUSHA.Controls))
            {
                return;
            }
            //連携事業場情報クリアする
            if (!this.ClearEachControl(pnl_SBN_Jigyouba.Controls))
            {
                return;
            }
        }
        /// <summary>
        /// 処分事業場情報を削除ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SBNGenbaDel_Click(object sender, EventArgs e)
        {
            //連携事業場情報クリアする
            if (!this.ClearEachControl(pnl_SBN_Jigyouba.Controls))
            {
                return;
            }
        }
        /// <summary>
        /// 運搬報告情報エリアのEnterイベント（収集運搬事業者CDと運搬先加入者番号が暗黙コントロールに設定のため）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnl_UnpanReportInfo_Enter(object sender, EventArgs e)
        {
            if (this.cdgv_UnpanInfo.SelectedRows.Count > 0)
            {
                object tmp = this.cdgv_UnpanInfo.SelectedRows[0].Cells["UPN_SHA_CD"].Value;
                if (tmp != null)
                {
                    Hidden_UnpanGyoushaCD.Text = tmp.ToString();
                }
                else
                {
                    Hidden_UnpanGyoushaCD.Text = string.Empty;
                }

                tmp = this.cdgv_UnpanInfo.SelectedRows[0].Cells["UPN_SHA_EDI_MEMBER_ID"].Value;

                if (tmp != null)
                {
                    Hidden_KanyushaNo.Text = tmp.ToString();
                }
                else
                {
                    Hidden_KanyushaNo.Text = string.Empty;
                }
            }
        }
        /// <summary>
        /// 最終処分事業場『実績』グリッドの行追加イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_LastSBN_Genba_Jiseki_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // 一覧の有効行の中で最終行に削除チェックが入っている場合、行を追加しない。
            if (cdgv_LastSBN_Genba_Jiseki.Rows[cdgv_LastSBN_Genba_Jiseki.Rows.Count - 2].Cells[0].Value != null
                && cdgv_LastSBN_Genba_Jiseki.Rows[cdgv_LastSBN_Genba_Jiseki.Rows.Count - 2].Cells[0].Value.ToString().Equals("True")
                && cdgv_LastSBN_Genba_Jiseki.Rows[cdgv_LastSBN_Genba_Jiseki.Rows.Count - 1].IsNewRow)
            {
                cdgv_LastSBN_Genba_Jiseki.AllowUserToAddRows = false;
            }
            else
            {
                //行数が8件の制限
                cdgv_LastSBN_Genba_Jiseki.AllowUserToAddRows = (cdgv_LastSBN_Genba_Jiseki.Rows.Count >= 8) ? false : true;
            }

            //行番号の設定
            for (int i = 0; i < cdgv_LastSBN_Genba_Jiseki.Rows.Count; i++)
            {
                cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells[1].Value = i + 1;
            }
        }
        /// <summary>
        /// 最終処分事業場『実績』グリッドの行削除番号設定イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_LastSBN_Genba_Jiseki_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //行番号の設定
            for (int i = 0; i < cdgv_LastSBN_Genba_Jiseki.Rows.Count; i++)
            {
                cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells[1].Value = i + 1;
            }
            //行数が8件の制限
            cdgv_LastSBN_Genba_Jiseki.AllowUserToAddRows = (cdgv_LastSBN_Genba_Jiseki.Rows.Count > 8) ? false : true;

            // 最終処分終了日の再設定
            DateTime latestLastSbnDate = this.Logic.GetLatestLastSbnDate();
            if (DateTime.Compare(DateTime.MinValue, latestLastSbnDate) < 0)
            {
                this.cdpt_LastSBNEndDate.Value = latestLastSbnDate.Date;
            }
            else
            {
                this.cdpt_LastSBNEndDate.Value = null;
            }

        }
        /// <summary>
        /// 備考グリッドの行追加イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_Bikou_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //行番号の設定
            for (int i = 0; i < cdgv_Bikou.Rows.Count; i++)
            {
                cdgv_Bikou.Rows[i].Cells[0].Value = i + 1;
            }
            //行数が5件の制限
            cdgv_Bikou.AllowUserToAddRows = (cdgv_Bikou.Rows.Count > 5) ? false : true;
        }
        /// <summary>
        /// 備考グリッドの行削除イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_Bikou_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //行番号の設定
            for (int i = 0; i < cdgv_Bikou.Rows.Count; i++)
            {
                cdgv_Bikou.Rows[i].Cells[0].Value = i + 1;
            }
            //行数が5件の制限
            cdgv_Bikou.AllowUserToAddRows = (cdgv_Bikou.Rows.Count > 5) ? false : true;
        }

        /// <summary>
        /// 電子マニフェスト最終処分（予定）拡張行の追加イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_LastSBNbasyo_yotei_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // 一覧の有効行の中で最終行に削除チェックが入っている場合、行を追加しない。
            if (cdgv_LastSBNbasyo_yotei.Rows[cdgv_LastSBNbasyo_yotei.Rows.Count - 2].Cells[0].Value != null
                && cdgv_LastSBNbasyo_yotei.Rows[cdgv_LastSBNbasyo_yotei.Rows.Count - 2].Cells[0].Value.ToString().Equals("True")
                && cdgv_LastSBNbasyo_yotei.Rows[cdgv_LastSBNbasyo_yotei.Rows.Count - 1].IsNewRow)
            {
                cdgv_LastSBNbasyo_yotei.AllowUserToAddRows = false;
            }
            else
            {
                //10件の制限
                cdgv_LastSBNbasyo_yotei.AllowUserToAddRows = (cdgv_LastSBNbasyo_yotei.Rows.Count >= 10) ? false : true;
            }

            //行番号の設定
            for (int i = 0; i < cdgv_LastSBNbasyo_yotei.Rows.Count; i++)
            {
                cdgv_LastSBNbasyo_yotei.Rows[i].Cells[1].Value = i + 1;
            }
        }
        /// <summary>
        /// 電子マニフェスト最終処分（予定）拡張行の削除イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_LastSBNbasyo_yotei_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //行番号の設定
            for (int i = 0; i < cdgv_LastSBNbasyo_yotei.Rows.Count; i++)
            {
                cdgv_LastSBNbasyo_yotei.Rows[i].Cells[1].Value = i + 1;
            }
            //10件の制限
            cdgv_LastSBNbasyo_yotei.AllowUserToAddRows = (cdgv_LastSBNbasyo_yotei.Rows.Count > 10) ? false : true;
        }

        /// <summary>
        /// 交付番号チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ManifestNo_Validating(object sender, CancelEventArgs e)
        {
            //交付番号のチェック
            string ManiNo = this.cantxt_ManifestNo.Text;
            if (!string.IsNullOrEmpty(ManiNo))
            {
                bool catchErr = false;
                var retKohuno = this.Logic.ChkKohuNo(ManiNo, out catchErr);
                if (catchErr)
                {
                    return;
                }

                if (retKohuno)
                {
                    var retManifest = this.Logic.ExistManifestNo(ManiNo, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (retManifest)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            //BackColor設定
            cantxt_ManifestNo.IsInputErrorOccured = e.Cancel;
        }

        /// <summary>
        /// 入力区分変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_InputKBN_TextChanged(object sender, EventArgs e)
        {
            if (cntxt_InputKBN.Text == string.Empty) return;

            // 入力区分＝１．自働の場合 「F5部分更新」を活性化
            this.Logic.parentbaseform.bt_func5.Enabled = (this.cntxt_InputKBN.Text == "1");

            //新規モードで自動手動切替時の判断
            if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                if (cntxt_InputKBN.HaveBeChanged())
                {
                    if (cntxt_InputKBN.Text == "1")//自動に変更
                    {
                        //修正モードから新規モードかわる時確認メッセージ出さない
                        if (this.Logic.ManiInfo != null && this.Logic.ManiInfo.bIsAutoMode != (cntxt_InputKBN.Text == "1"))
                        {
                            //手動から自動に変更すると、画面制御実行
                            this.Logic.Mode = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            if (!this.InitializeFormByMode(this.Logic.Mode))
                            {
                                return;
                            }
                            this.Logic.ManiInfo = null;
                            this.Logic.KanriId = null;
                            this.Logic.Seq = default(SqlInt16);
                            return;
                        }
                        //パタン呼出場合は、手動から自動に変更時、確認メッセージ出さない

                        if (this.Logic.msgLogic.MessageBoxShow("C053") == DialogResult.Yes)
                        {
                            //手動から自動に変更すると、画面制御実行
                            //InitializeFormByMode(this.Logic.Mode);
                            if (!this.SetControlReadOnlyByInput_KBN(true, true))
                            {
                                return;
                            }
                        }
                        else
                        {
                            cntxt_InputKBN.Text = cntxt_InputKBN.GetlastValue();
                        }
                    }
                    else if (cntxt_InputKBN.Text == "2")//手動に変更
                    {
                        if (this.Logic.msgLogic.MessageBoxShow("C053") == DialogResult.Yes)
                        {
                            //自動から手動に変更すると、画面制御実行
                            if (!this.SetControlReadOnlyByInput_KBN(false, true))
                            {
                                return;
                            }
                        }
                        else
                        {
                            cntxt_InputKBN.Text = cntxt_InputKBN.GetlastValue();
                        }
                        //最終処分実績情報グリッドを編集可
                        if (!this.SetLastSbnGenbaJisekiReadOnly(false))
                        {
                            return;
                        }
                    }
                }
            }
            //修正モード
            else if (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                if (cntxt_InputKBN.Text == "2")
                {
                    this.SetLastSbnGenbaJisekiReadOnly(false);
                }
                else if (cntxt_InputKBN.Text == "1")
                {
                    this.SetLastSbnGenbaJisekiReadOnly(true);
                }
            }
            else
            {
                // 削除モード、参照モード
                return;
            }

            // 自動に変更した場合、最終処分事業場（実績）の削除チェックボックスのみ使用不可とする。
            if (cntxt_InputKBN.Text == "1")
            {
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LastSBN_Genba_chb_delete"].ReadOnly = true;
            }
            // 手動に変更した場合、明細の削除チェックボックスは使用可能とする。
            else if (cntxt_InputKBN.Text == "2")
            {
                this.cdgv_Haikibutu.Columns["Haikibutu_chb_delete"].ReadOnly = false;
                this.cdgv_LastSBNbasyo_yotei.Columns["LastSBNbasyo_chb_delete"].ReadOnly = false;
                this.cdgv_UnpanInfo.Columns["UnpanInfo_chb_delete"].ReadOnly = false;
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LastSBN_Genba_chb_delete"].ReadOnly = false;

                // 2次マニで当欄指定の場合
                if (this.Logic.maniFlag == 2 && this.ccbx_Touranshitei.Checked)
                {
                    this.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = false;
                }
            }
        }

        /// <summary>
        /// マニフェスト区分変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_ManiKBN_TextChanged(object sender, EventArgs e)
        {
            switch (this.Logic.Mode)
            {
                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                    //全て列編集不可を設定する
                    this.SetTyukanshoriReadOnly();

                    //部分列編集不可を設定する
                    this.SetTyukanshoriReadOnly(true);

                    break;

                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                    if (this.ccbx_Touranshitei.Checked)
                    {
                        //レコードの追加・編集を可能とする
                        this.cdgv_Tyukanshori.AllowUserToAddRows = true;
                        this.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = false;
                        this.cdgv_Tyukanshori.Columns["FM_MANIFEST_ID"].ReadOnly = false;

                    }
                    else if (this.ccbx_ItijiFuyou.Checked ||
                        this.ccbx_ChouboKisai.Checked)
                    {
                        //レコードが存在する場合はクリアする
                        //レコードの追加・編集を不可とする
                        this.cdgv_Tyukanshori.AllowUserToAddRows = true;
                        this.SetTyukanshoriReadOnly();
                    }
                    break;

            }

            //予約登録場合、予約修正権限利用可
            bool bIsMani = (cntxt_ManiKBN.Text == "2");
            bool enabledModifyRight = !bIsMani;
            switch (this.Logic.Mode)
            {
                // 削除・参照モード時は利用不可
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    enabledModifyRight = false;
                    break;
            }

            this.cntxt_ModifyRight.Enabled = enabledModifyRight;
            this.crdbtn_ModifyRight1.Enabled = enabledModifyRight;
            this.crdbtn_ModifyRight2.Enabled = enabledModifyRight;
            this.crdbtn_ModifyRight3.Enabled = enabledModifyRight;
            this.crdbtn_ModifyRight4.Enabled = enabledModifyRight;
            if (bIsMani)
            {
                this.cntxt_ModifyRight.Text = "1";
            }


            if ("1".Equals(this.cntxt_ManiKBN.Text) || this.Logic.maniFlag == 1)
            {
                this.ccbx_ItijiFuyou.Enabled = false;
            }
            else
            {
                this.ccbx_ItijiFuyou.Enabled = true;
            }
            this.ccbx_ItijiFuyou.Checked = false;


            if (this.Logic.maniFlag == 2)
            {
                if ("1".Equals(this.cntxt_ManiKBN.Text))//予約
                {
                    this.ccbx_Touranshitei.Enabled = true;
                    this.ccbx_ChouboKisai.Enabled = true;
                }

            }


            //発行件数が予定場合は入力可
            this.cantxt_HakkouCnt.ReadOnly = (this.cntxt_ManiKBN.Text == "2");
            if (this.cantxt_HakkouCnt.ReadOnly)
            {
                this.cantxt_HakkouCnt.Text = "1";
            }

            if (this.cntxt_ManiKBN.Text == "1")
            {
                this.cdgv_Tyukanshori.Rows.Clear();
                this.SetTyukanshoriReadOnly();
                this.SetTyukanshoriReadOnly(true);
            }

        }

        /// <summary>
        /// 最終処分「予定」情報の委託と当欄指定チェックボックス選択変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastSBN_Yotei_CheckedChanged(object sender, EventArgs e)
        {
            //新規及び修正モードで、イベント処理を行う
            if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG || this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                // 明細が空行の場合、追加行設定をtrueにする。
                if (this.cdgv_LastSBNbasyo_yotei.Rows.Count == 0)
                {
                    this.cdgv_LastSBNbasyo_yotei.AllowUserToAddRows = true;
                }

                this.cdgv_LastSBNbasyo_yotei.Rows[0].Selected = true;

                //委託変更場合
                if (sender.Equals(this.ccbx_YitakuKeyaku))
                {
                    if (this.ccbx_YitakuKeyaku.Checked && this.ccbx_Toulanshitei.Checked)
                    {
                        this.ccbx_Toulanshitei.Checked = false;
                    }
                    //最終処分「予定」情報をクリアする
                    if (this.ccbx_YitakuKeyaku.Checked)
                    {
                        //データクリア
                        this.cdgv_LastSBNbasyo_yotei.Rows.Clear();
                        //グリッドを無効になる
                        this.cdgv_LastSBNbasyo_yotei.ReadOnly = true;

                        // 削除チェックボックスを非活性にする。
                        this.cdgv_LastSBNbasyo_yotei.Columns["LastSBNbasyo_chb_delete"].ReadOnly = true;

                        // 明細が追加行設定falseの場合、trueに変更する。
                        if (!this.cdgv_LastSBNbasyo_yotei.AllowUserToAddRows)
                        {
                            this.cdgv_LastSBNbasyo_yotei.AllowUserToAddRows = true;
                        }
                    }

                    // 委託変更時の背景色設定
　                  this.Logic.ReadOnlyColorSet();
                }
                //当欄指定場合
                if (sender.Equals(this.ccbx_Toulanshitei))
                {
                    if (this.ccbx_Toulanshitei.Checked && this.ccbx_YitakuKeyaku.Checked)
                    {
                        // 削除チェックボックスを活性にする。
                        this.cdgv_LastSBNbasyo_yotei.Columns["LastSBNbasyo_chb_delete"].ReadOnly = false;
                        this.ccbx_YitakuKeyaku.Checked = false;
                    }
                    //グリッドを有効になる
                    if (!this.ccbx_Toulanshitei.Checked)
                    {
                        //データクリア
                        this.cdgv_LastSBNbasyo_yotei.Rows.Clear();
                        //グリッドを無効になる
                        this.cdgv_LastSBNbasyo_yotei.ReadOnly = true;

                        // 明細が追加行設定falseの場合、trueに変更する。
                        if (!this.cdgv_LastSBNbasyo_yotei.AllowUserToAddRows)
                        {
                            this.cdgv_LastSBNbasyo_yotei.AllowUserToAddRows = true;
                        }
                    }
                    else
                    {
                        // 当欄指定時の背景色設定（選択時含む）
　                      this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_GYOUSHA_CD"].Style.BackColor = Constans.NOMAL_COLOR;
                        this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_GYOUSHA_CD"].Style.SelectionBackColor = Constans.NOMAL_COLOR;
　                      this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_CD"].Style.BackColor = Constans.NOMAL_COLOR;
                        this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_CD"].Style.SelectionBackColor = Constans.NOMAL_COLOR;
                    }
                    //this.cdgv_LastSBNbasyo_yotei.Enabled = this.ccbx_Toulanshitei.Checked;
                    this.cdgv_LastSBNbasyo_yotei.ReadOnly = false;
                    //自動の場合
                    if ("1".Equals(this.cntxt_InputKBN.Text))
                    {
                        if (!this.SetLastSBNbasyoYoteiReadOnly(true))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (!this.SetLastSBNbasyoYoteiReadOnly(false))
                        {
                            return;
                        }
                        this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_NAME"].Style.BackColor = Constans.NOMAL_COLOR;
                        this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_NAME"].Style.SelectionBackColor = Constans.NOMAL_COLOR;
　                      this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_POST"].Style.BackColor = Constans.NOMAL_COLOR;
                        this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_POST"].Style.SelectionBackColor = Constans.NOMAL_COLOR;
                        this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_ADDRESS"].Style.BackColor = Constans.NOMAL_COLOR;
                        this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_ADDRESS"].Style.SelectionBackColor = Constans.NOMAL_COLOR;
　                      this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_TEL"].Style.BackColor = Constans.NOMAL_COLOR;
                        this.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_TEL"].Style.SelectionBackColor = Constans.NOMAL_COLOR;
                    }
                }

                if (!this.ccbx_YitakuKeyaku.Checked && !this.ccbx_Toulanshitei.Checked)
                {
                    // 当欄指定時ではない時の背景色設定（選択時含む）
                    this.Logic.ReadOnlyColorSet();

                    this.ccbx_Toulanshitei.Checked = false;
                    this.cdgv_LastSBNbasyo_yotei.ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 最終処分の場所（予定）のReadOnlyプロパティを設定する
        /// </summary>
        /// <param name="readOnlyFlg"></param>
        private bool SetLastSBNbasyoYoteiReadOnly(bool readOnlyFlg)
        {
            try
            {
                // 削除チェックボックスを活性にする。
                this.cdgv_LastSBNbasyo_yotei.Columns["LastSBNbasyo_chb_delete"].ReadOnly = false;

                this.cdgv_LastSBNbasyo_yotei.Columns["No"].ReadOnly = true;
                //非表示列
                this.cdgv_LastSBNbasyo_yotei.Columns["Sbn_KanyushaCD"].ReadOnly = true;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_GYOUSHA_CD"].ReadOnly = false;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_GYOUSHA_NAME"].ReadOnly = true;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_JOU_CD"].ReadOnly = false;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_JOU_NAME"].ReadOnly = readOnlyFlg;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_JOU_POST"].ReadOnly = readOnlyFlg;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_JOU_ADDRESS"].ReadOnly = readOnlyFlg;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_JOU_TEL"].ReadOnly = readOnlyFlg;
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetLastSBNbasyoYoteiReadOnly", ex);
                this.Logic.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 最終処分事業場（実績）のReadOnlyプロパティを設定する
        /// </summary>
        /// <param name="readOnlyFlg"></param>
        private bool SetLastSbnGenbaJisekiReadOnly(bool readOnlyFlg)
        {
            try
            {
                this.cdgv_LastSBN_Genba_Jiseki.Columns["JISEIKI_No"].ReadOnly = true;
                //非表示列
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LAST_SBN_GYOUSHA_KanyushaCD"].ReadOnly = true;

                this.cdgv_LastSBN_Genba_Jiseki.Columns["LAST_SBN_END_DATE"].ReadOnly = false;
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LAST_SBN_GYOUSHA_JISEKI_CD"].ReadOnly = false;
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LAST_SBN_GYOUSHA_JISEKI_NAME"].ReadOnly = true;
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LAST_SBN_JOU_JISEKI_CD"].ReadOnly = false;

                this.cdgv_LastSBN_Genba_Jiseki.Columns["LAST_SBN_JOU_JISEKI_NAME"].ReadOnly = readOnlyFlg;
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LAST_SBN_JOU_JISEKI_POST"].ReadOnly = readOnlyFlg;
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LAST_SBN_JOU_JISEKI_ADDRESS"].ReadOnly = readOnlyFlg;
                this.cdgv_LastSBN_Genba_Jiseki.Columns["LAST_SBN_JOU_JISEKI_TEL"].ReadOnly = readOnlyFlg;
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetLastSbnGenbaJisekiReadOnly", ex);
                this.Logic.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

        }

        /// <summary>
        /// 交付番号の禁則文字の制限イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ManifestNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // バックスペースキー以外の場合
            //if (e.KeyChar != (char)8)
            //{
            //    if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "^[0-9]+$") == false)
            //    {
            //        e.Handled = true;
            //    }
            //}
        }

        /// <summary>
        /// 廃棄物グリッドのCellチェック後イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cdgv_Haikibutu_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            string haikiShuruiCd = string.Empty;
            string haikiShuruiSaibunruiCd = string.Empty;
            string haikibutuHaikiShuruiCd = string.Empty;

            var cell = this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex] as ICustomDataGridControl;

            if (cell != null)
            {
                switch (cell.GetName())
                {
                    case "HAIKI_SHURUI_CD":
                        if (this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null
                            || string.IsNullOrEmpty(this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            this.SetHaikibutuYuugaiColumnReadOnly(true, false, true, e.RowIndex);
                            return;
                        }

                        this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                            this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().PadLeft(7, '0').ToUpper();

                        haikibutuHaikiShuruiCd = this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                        haikiShuruiSaibunruiCd = haikibutuHaikiShuruiCd.Substring(0, 4);
                        haikiShuruiCd = haikibutuHaikiShuruiCd.Substring(4, 3);

                        var currentInfoSaibunrui = this.Logic.ListDenshiHaikiShuruiSaibunrui.Where(
                            s => s.EDI_MEMBER_ID == this.ctxt_Haisyutu_KanyushaNo.Text
                                && s.HAIKI_SHURUI_CD == haikiShuruiSaibunruiCd
                                && s.HAIKI_SHURUI_SAIBUNRUI_CD == haikiShuruiCd).FirstOrDefault();

                        //データが存在しない場合
                        if (currentInfoSaibunrui == null)
                        {
                            var currentInfo = this.Logic.ListDenshiHaikiShurui.Where(
                                s => s.HAIKI_SHURUI_CD == haikiShuruiSaibunruiCd).FirstOrDefault();

                            if (currentInfo == null)
                            {
                                this.SetHaikibutuYuugaiColumnReadOnly(true, false, true, e.RowIndex);
                            }
                            else
                            {
                                this.SetHaikibutuYuugaiColumnReadOnly(false, false, false, e.RowIndex);
                            }
                        }
                        else
                        {
                            this.SetHaikibutuYuugaiColumnReadOnly(false, false, false, e.RowIndex);
                        }
                        break;
                    case "KANSAN_SUU":
                        // 換算後数量が変更された場合は減容後数量を再計算
                        var kansanSuu = SqlDecimal.Null;
                        if (this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        {
                            kansanSuu = SqlDecimal.Parse(this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        }
                        var genyouSuu = SqlDecimal.Null;
                        bool catchErr = false;
                        this.Logic.GetGenYougou_suu(kansanSuu, ref genyouSuu, e.RowIndex, out catchErr);
                        if (catchErr)
                        {
                            return;
                        }

                        if (!genyouSuu.IsNull)
                        {
                            this.cdgv_Haikibutu.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = genyouSuu.Value;
                        }
                        else
                        {
                            this.cdgv_Haikibutu.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = null;
                        }
                        break;
                    case "HAIKI_NAME_CD":
                        if (this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                            && !string.IsNullOrEmpty(this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                                this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().PadLeft(6, '0').ToUpper();
                        }
                        break;
                    case "NISUGATA_CD":
                    case "YUUGAI_CODE1":
                    case "YUUGAI_CODE2":
                    case "YUUGAI_CODE3":
                    case "YUUGAI_CODE4":
                    case "YUUGAI_CODE5":
                    case "YUUGAI_CODE6":
                        if (this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                            && !string.IsNullOrEmpty(this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                                this.cdgv_Haikibutu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().PadLeft(2, '0').ToUpper();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 収集運搬業者加入者番号変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hidden_KanyushaNo_TextChanged(object sender, EventArgs e)
        {
            //運搬報告情報の担当者情報をクリアする
            this.cantxt_UnpanTantoushaCd.Text = string.Empty;
            this.ctxt_UnpanTantoushaName.Text = string.Empty;
            this.cantxt_HoukokuTantoushaCD.Text = string.Empty;
            this.ctxt_HoukokuTantoushaName.Text = string.Empty;
        }

        /// <summary>
        /// 収集運搬業者CD変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hidden_UnpanGyoushaCD_TextChanged(object sender, EventArgs e)
        {
            //運搬報告情報の車輌CD名称をクリアする
            this.cantxt_SyaryoNo.Text = string.Empty;
            this.ctxt_UnpanSyaryoName.Text = string.Empty;

        }
        /// <summary>
        /// 処分受託者の加入者番号変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_SBN_KanyuShaNo_TextChanged(object sender, EventArgs e)
        {
            //処分事業場情報をクリアする
            this.cantxt_SBN_Genba_CD.ClearLikedControlText();
            this.cantxt_SBN_Genba_CD.Text = string.Empty;

            //処分報告情報の担当者情報クリアする
            this.cantxt_SBN_HoukokuTantouShaCD.Text = string.Empty;
            this.ctxt_SBN_HoukokuTantouShaName.Text = string.Empty;
            this.cantxt_SBN_SBNTantouShaCD.Text = string.Empty;
            this.ctxt_SBN_SBNTantouShaName.Text = string.Empty;
            this.cantxt_SBN_UnpanTantouShaCD.Text = string.Empty;
            this.ctxt_SBN_UnpanTantouShaName.Text = string.Empty;
            //車輌情報クリア
            this.cantxt_SBN_SyaryoNoCD.Text = string.Empty;
            this.ctxt_cantxt_SBN_SyaryoNoName.Text = string.Empty;
            //車輌情報の検索条件の再設定
            //最新業者CDより処分報告の車輌検索条件を設定する
            r_framework.Dto.JoinMethodDto methodDto = this.cantxt_SBN_SyaryoNoCD.popupWindowSetting[0];
            r_framework.Dto.SearchConditionsDto searchDto = methodDto.SearchCondition[0];
            if (methodDto != null && searchDto != null)
            {
                searchDto.Value = this.cantxt_SBN_JyutakuShaCD.Text;
            }
        }

        /// <summary>
        /// 排出事業者加入者番号変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ctxt_Haisyutu_KanyushaNo_TextChanged(object sender, EventArgs e)
        {
            //加入者番号変更すると、廃棄物種類CDと名称、廃棄物名称CDと名前クリアする
            foreach (DataGridViewRow dgvr in this.cdgv_Haikibutu.Rows)
            {
                if (!string.IsNullOrEmpty(this.ctxt_Haisyutu_KanyushaNo.Text))
                {
                    // 廃棄物種類
                    if (dgvr.Cells["HAIKI_SHURUI_CD"].Value != null && !dgvr.Cells["HAIKI_SHURUI_CD"].Value.Equals(""))
                    {
                        var haikiShuruiStr = dgvr.Cells["HAIKI_SHURUI_CD"].Value.ToString();

                        // 細分類じゃない廃棄種類CDだった場合は何もしない
                        if (haikiShuruiStr.Substring(4, 3) != "000")
                        {
                            var mDenshiHikiShurui = new M_DENSHI_HAIKI_SHURUI_SAIBUNRUI();
                            mDenshiHikiShurui.EDI_MEMBER_ID = this.ctxt_Haisyutu_KanyushaNo.Text;           // 加入者番号
                            mDenshiHikiShurui.HAIKI_SHURUI_CD = haikiShuruiStr.Substring(0, 4);             // 廃棄種類CD
                            mDenshiHikiShurui.HAIKI_SHURUI_SAIBUNRUI_CD = haikiShuruiStr.Substring(4, 3);   // 廃棄種類細分類CD

                            // マスタ存在チェック
                            var mSibunrui = this.Logic.im_denshi_haiki_shurui_saibunruidao.GetAllValidData(mDenshiHikiShurui).FirstOrDefault();
                            if (mSibunrui != null && mSibunrui.HAIKI_SHURUI_NAME != null)
                            {
                                // 該当するデータがあればセット
                                dgvr.Cells["HAIKI_SHURUI_NAME"].Value = mSibunrui.HAIKI_SHURUI_NAME;
                            }
                            else
                            {
                                // 無ければクリア
                                dgvr.Cells["HAIKI_SHURUI_CD"].Value = null;
                                dgvr.Cells["HAIKI_SHURUI_NAME"].Value = null;
                            }
                        }
                    }

                    // 廃棄物名称
                    if (dgvr.Cells["HAIKI_NAME_CD"].Value != null)
                    {
                        var mDenshiName = new M_DENSHI_HAIKI_NAME();
                        mDenshiName.EDI_MEMBER_ID = this.ctxt_Haisyutu_KanyushaNo.Text;
                        mDenshiName.HAIKI_NAME_CD = dgvr.Cells["HAIKI_NAME_CD"].Value.ToString();
                        // マスタ存在チェック
                        var mName = this.Logic.im_denshi_haiki_namedao.GetAllValidData(mDenshiName).FirstOrDefault(); ;
                        if (mName != null && mName.HAIKI_NAME != null)
                        {
                            dgvr.Cells["HAIKI_NAME"].Value = mName.HAIKI_NAME;
                        }
                        else
                        {
                            dgvr.Cells["HAIKI_NAME_CD"].Value = null;
                            dgvr.Cells["HAIKI_NAME"].Value = null;
                        }
                    }
                }
            }

            ////排出事業場情報をクリアする
            //this.cantxt_HaisyutuGenbaCd.ClearLikedControlText();
            //this.cantxt_HaisyutuGenbaCd.Text = string.Empty;

            //有害物質情報入力可否の判断処理
            string haikiShuruiCd = string.Empty;
            string haikiShuruiSaibunruiCd = string.Empty;
            string haikibutuHaikiShuruiCd = string.Empty;

            if (string.IsNullOrEmpty(this.ctxt_Haisyutu_KanyushaNo.Text))
            {
                this.SetHaikibutuYuugaiColumnReadOnly(true, true, true);
            }
            else
            {
                foreach (DataGridViewRow dgvr in this.cdgv_Haikibutu.Rows)
                {
                    haikibutuHaikiShuruiCd = (dgvr.Cells["HAIKI_SHURUI_CD"].Value == null) ? string.Empty : dgvr.Cells["HAIKI_SHURUI_CD"].Value.ToString();

                    if (string.IsNullOrEmpty(haikibutuHaikiShuruiCd))
                    {
                        this.SetHaikibutuYuugaiColumnReadOnly(true, false, true, dgvr.Index);
                        continue;
                    }

                    haikiShuruiSaibunruiCd = haikibutuHaikiShuruiCd.Substring(0, 4);
                    haikiShuruiCd = haikibutuHaikiShuruiCd.Substring(4, 3);

                    var currentInfoSaibunrui = this.Logic.ListDenshiHaikiShuruiSaibunrui.Where(
                        s => s.EDI_MEMBER_ID == this.ctxt_Haisyutu_KanyushaNo.Text
                            && s.HAIKI_SHURUI_CD == haikiShuruiSaibunruiCd
                            && s.HAIKI_SHURUI_SAIBUNRUI_CD == haikiShuruiCd).FirstOrDefault();

                    //データが存在しない場合
                    if (currentInfoSaibunrui == null)
                    {
                        var currentInfo = this.Logic.ListDenshiHaikiShurui.Where(
                            s => s.HAIKI_SHURUI_CD == haikiShuruiSaibunruiCd).FirstOrDefault();

                        if (currentInfo == null)
                        {
                            this.SetHaikibutuYuugaiColumnReadOnly(true, false, true, dgvr.Index);
                        }
                        else
                        {
                            this.SetHaikibutuYuugaiColumnReadOnly(false, false, false, dgvr.Index);
                        }
                    }
                    else
                    {
                        this.SetHaikibutuYuugaiColumnReadOnly(false, false, false, dgvr.Index);
                    }

                }
            }
        }


        #endregion イベント処理 完了

        /// <summary>
        /// 指定コントロール下に毎コントロールの内容をクリアする[モード切替、参照、削除モードのみ]
        /// </summary>
        /// <param name="ctls"></param>
        public bool ClearEachControl(Control.ControlCollection ctls)
        {
            try
            {
                foreach (Control con in ctls)
                {
                    if ((con as TextBox) != null)
                    {
                        if ((con as TextBox).Visible)
                        {
                            (con as TextBox).Text = string.Empty;
                        }
                    }
                    else if ((con as CustomDateTimePicker) != null)
                    {
                        (con as CustomDateTimePicker).Value = null;
                    }

                    //再帰メッソド呼ぶ
                    if (con.Controls.Count > 0) ClearEachControl(con.Controls);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearEachControl", ex);
                this.Logic.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 指定コントロール下に毎コントロールのReadOnly状態設定処理[参照、削除モードのみ]
        /// </summary>
        /// <param name="ctls"></param>
        /// <param name="IsReadOnly"></param>
        public void SetEachControlReadOnlyOrEnabled(Control.ControlCollection ctls,
                                                    bool IsReadOnly = true)
        {
            foreach (Control con in ctls)
            {
                if ((con as TextBox) != null)
                {
                    (con as TextBox).ReadOnly = IsReadOnly;
                }
                else if ((con as Button) != null)
                {
                    con.Enabled = !IsReadOnly;
                }
                else if ((con as RadioButton) != null)
                {
                    (con as RadioButton).Enabled = !IsReadOnly;
                    (con as RadioButton).ForeColor = Color.Black;
                }
                else if ((con as DateTimePicker) != null)
                {
                    (con as DateTimePicker).Enabled = !IsReadOnly;
                    (con as DateTimePicker).ForeColor = Color.Black;
                }
                else if ((con as CheckBox) != null)
                {
                    (con as CheckBox).Enabled = !IsReadOnly;
                }

                //再帰メッソド呼ぶ
                if (con.Controls.Count > 0) SetEachControlReadOnlyOrEnabled(con.Controls, IsReadOnly);
            }
        }
        /// <summary>
        /// 自動手動切替時各種グリッドのReadOnly状態設定
        /// </summary>
        /// <param name="bIsAutoMode">自動手動モードフラグ</param>
        /// <param name="bIsClear">データクリアフラグ</param>
        public void SetDgvColumnReadOnlyByInputMode(bool bIsAutoMode, bool bIsClear = true)
        {
            //入力区分変更時点で、全画面入力された内容をクリアする
            //廃棄物種類グリッド
            if (bIsClear) this.cdgv_Haikibutu.Rows.Clear();
            if (!bIsAutoMode)
            {
                //廃棄物情報グリッドの設定
                if (bIsClear && this.WindowType != WINDOW_TYPE.UPDATE_WINDOW_FLAG) this.cdgv_Haikibutu.Rows.Add();
                this.cdgv_Haikibutu.AllowUserToAddRows = false;
            }
            else
            {
                //廃棄物種類情報の行が追加可
                this.cdgv_Haikibutu.AllowUserToAddRows = true;
            }
            if (0 == this.cdgv_Haikibutu.Rows.Count)
            {
                this.cdgv_Haikibutu.Rows.Add();
            }

            //中間処理産業廃棄物
            //二次開発対応
            if (bIsClear) this.cdgv_Tyukanshori.Rows.Clear();
            //新規と修正モードでグリッドのカラムの有効無効設定
            if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG || this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                //最終処分情報『予定』
                //this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_GYOUSHA_NAME"].ReadOnly = bIsAutoMode;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_JOU_NAME"].ReadOnly = bIsAutoMode;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_JOU_POST"].ReadOnly = bIsAutoMode;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_JOU_ADDRESS"].ReadOnly = bIsAutoMode;
                this.cdgv_LastSBNbasyo_yotei.Columns["LAST_SBN_JOU_TEL"].ReadOnly = bIsAutoMode;
                //データクリア
                if (bIsClear) this.cdgv_LastSBNbasyo_yotei.Rows.Clear();

                //運搬情報
                this.cdgv_UnpanInfo.Columns["UPN_SHA_NAME"].ReadOnly = bIsAutoMode;
                this.cdgv_UnpanInfo.Columns["UNPANSAKI_GYOUSHA_NAME"].ReadOnly = bIsAutoMode;
                this.cdgv_UnpanInfo.Columns["UNPANSAKI_GENBA_NAME"].ReadOnly = bIsAutoMode;
                //データクリア
                if (bIsClear) this.cdgv_UnpanInfo.Rows.Clear();

                //最終処分情報『実績』
                this.cdgv_LastSBN_Genba_Jiseki.ReadOnly = bIsAutoMode;
                if (!bIsAutoMode)
                {
                    if (!this.SetLastSbnGenbaJisekiReadOnly(bIsAutoMode))
                    {
                        return;
                    }
                }

                //データクリア
                if (bIsClear) this.cdgv_LastSBN_Genba_Jiseki.Rows.Clear();

                //備考
                //データクリア
                if (bIsClear)
                {
                    this.cdgv_Bikou.Rows.Clear();
                    // 行数固定
                    this.AddDefaultRowForBikou();
                }
            }
            //削除、参照モードで無効設定
            else
            {
                this.SetDgvColumnReadOnly(true);
            }

        }

        /// <summary>
        /// 各種グリッドのカラムをReadOnlyに設定[削除、参照モードのみ]
        /// </summary>
        public void SetDgvColumnReadOnly(bool bIsReadOnly = true)
        {
            //廃棄物種類グリッド
            for (int j = 0; j < this.cdgv_Haikibutu.ColumnCount; j++)
            {
                if (this.cdgv_Haikibutu.Columns[j].Visible)
                {
                    this.cdgv_Haikibutu.Columns[j].ReadOnly = bIsReadOnly;
                }
            }

            //中間処理産業廃棄物
            for (int j = 0; j < this.cdgv_Tyukanshori.ColumnCount; j++)
            {
                if (this.cdgv_Tyukanshori.Columns[j].Visible)
                {
                    this.cdgv_Tyukanshori.Columns[j].ReadOnly = bIsReadOnly;
                }
            }

            //最終処分情報『予定』
            for (int j = 0; j < this.cdgv_LastSBNbasyo_yotei.ColumnCount; j++)
            {
                if (this.cdgv_LastSBNbasyo_yotei.Columns[j].Visible)
                {
                    this.cdgv_LastSBNbasyo_yotei.Columns[j].ReadOnly = bIsReadOnly;
                }
            }
            //運搬情報
            for (int j = 0; j < this.cdgv_UnpanInfo.ColumnCount; j++)
            {
                if (this.cdgv_UnpanInfo.Columns[j].Visible)
                {
                    this.cdgv_UnpanInfo.Columns[j].ReadOnly = bIsReadOnly;
                }
            }
            //最終処分情報『実績』
            for (int j = 0; j < this.cdgv_LastSBN_Genba_Jiseki.ColumnCount; j++)
            {
                if (this.cdgv_LastSBN_Genba_Jiseki.Columns[j].Visible)
                {
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[j].ReadOnly = bIsReadOnly;
                }
            }
            //備考
            for (int j = 0; j < this.cdgv_Bikou.ColumnCount; j++)
            {
                if (this.cdgv_Bikou.Columns[j].Visible)
                {
                    this.cdgv_Bikou.Columns[j].ReadOnly = bIsReadOnly;
                }
            }
        }

        /// <summary>
        /// 運搬情報項目をReadOnlyに設定
        /// </summary>
        /// <param name="bIsReadOnly"></param>
        public void SetUnpanReportInfoReadOnly(bool bIsReadOnly)
        {
            this.cantxt_UnpanRyoUnitCd.ReadOnly = bIsReadOnly;
            this.cantxt_SyaryoNo.ReadOnly = bIsReadOnly;
            this.cantxt_HoukokuTantoushaCD.ReadOnly = bIsReadOnly;
            this.cdtp_UnpanEndDate.ReadOnly = bIsReadOnly;
            this.cntxt_UnpanRyo.ReadOnly = bIsReadOnly;
            this.cantxt_UnpanTantoushaCd.ReadOnly = bIsReadOnly;
            this.cantxt_YukabutuRyoUnitCd.ReadOnly = bIsReadOnly;
            this.cntxt_YukabutuRyo.ReadOnly = bIsReadOnly;
            this.ctxt_UnpanBikou.ReadOnly = bIsReadOnly;
        }

        /// <summary>
        /// 処分情報項目をReadOnlyに設定
        /// </summary>
        /// <param name="bIsReadOnly"></param>
        public void SetSbnReportInfoReadOnly(bool bIsReadOnly)
        {
            this.ctxt_SBN_Bikou.ReadOnly = bIsReadOnly;
            this.cantxt_SBN_SBNTantouShaCD.ReadOnly = bIsReadOnly;
            this.cantxt_SBN_UnpanTantouShaCD.ReadOnly = bIsReadOnly;
            this.cantxt_SBN_HoukokuTantouShaCD.ReadOnly = bIsReadOnly;
            this.cntxt_HoukokuKBN.ReadOnly = bIsReadOnly;
            this.crdbtn_HoukokuKBN1.Enabled = !bIsReadOnly;
            this.crdbtn_HoukokuKBN2.Enabled = !bIsReadOnly;
            this.cdtp_HaikiAcceptDate.ReadOnly = bIsReadOnly;
            this.cantxt_SBN_SyaryoNoCD.ReadOnly = bIsReadOnly;
            this.cdtp_SBNEndDate.ReadOnly = bIsReadOnly;
            this.cntxt_Jyunyuryo.ReadOnly = bIsReadOnly;
            this.cantxt_JyunyuryoUnitCD.ReadOnly = bIsReadOnly;
        }

        /// <summary>
        /// マニフェスト一覧画面へ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ToIchiRan(object sender, EventArgs e)
        {
            this.Logic.ToIchiRan();//JWNET登録
        }

        /// <summary>
        /// マニフェスト紐付画面へ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ToHimodukeForm(object sender, EventArgs e)
        {
            this.Logic.ToHimodukeForm();
        }

        /// <summary>
        /// 受渡確認票印刷処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UkewatashiKakuninHyouPrint(object sender, EventArgs e)
        {
            this.Logic.UkewatashiKakuninHyouPrint();
        }

        /// <summary>
        /// 発行件数チェックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HakkouCntValidating(object sender, CancelEventArgs e)
        {
            if (!this.Logic.HakkouCntValidating())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 発行件数にフォーカスがあるときにキーが押されると発生します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HakkouCnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //「.」と「,」を除く
            if (e.KeyChar == (char)46 || e.KeyChar == (char)44) e.Handled = true;
        }

        /// <summary>
        /// プロセス４実行イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LastSbnEndrep(object sender, EventArgs e)
        {
            this.Logic.LastSbnEndrepPopup(true);
        }


        /// <summary>
        /// プロセス５実行イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LastSbnEndrepCancel(object sender, EventArgs e)
        {
            this.Logic.LastSbnEndrepPopup(false);
        }

        /// <summary>
        /// 1次/2次マニフェスト設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetManifestForm(object sender, EventArgs e)
        {
            this.Logic.SetManifestForm("F4");
        }

        /// <summary>
        /// 禁則文字入力不可イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_Tyukanshori_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.cdgv_Tyukanshori.RowCount > 0 && this.cdgv_Tyukanshori.CurrentCell != null)
            {
                if ("FM_SYSTEM_ID".Equals(this.cdgv_Tyukanshori.CurrentCell.OwningColumn.Name))
                {
                    //「.」と「,」を除く
                    if (e.KeyChar == (char)46 || e.KeyChar == (char)44) e.Handled = true;
                }
            }
        }

        /// <summary>
        /// 中間処理産業廃棄物のReadOnlyを設定する
        /// </summary>
        /// <param name="enableFlg"></param>
        public void SetTyukanshoriReadOnly(bool enableFlg)
        {
            this.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = enableFlg;
            this.cdgv_Tyukanshori.Columns["FM_MANIFEST_ID"].ReadOnly = enableFlg;
            this.cdgv_Tyukanshori.Columns["FM_HST_GYOUSHA_CD"].ReadOnly = enableFlg;
            this.cdgv_Tyukanshori.Columns["FM_HST_GENBA_CD"].ReadOnly = enableFlg;
            this.cdgv_Tyukanshori.Columns["FM_KOUFU_DATE"].ReadOnly = enableFlg;
            this.cdgv_Tyukanshori.Columns["FM_SBN_END_DATE"].ReadOnly = enableFlg;
            this.cdgv_Tyukanshori.Columns["FM_HAIKI_SHURUI_CD"].ReadOnly = enableFlg;
            this.cdgv_Tyukanshori.Columns["FM_HAIKI_SUU"].ReadOnly = enableFlg;
            this.cdgv_Tyukanshori.Columns["FM_HAIKI_UNIT_CD"].ReadOnly = enableFlg;
        }

        /// <summary>
        /// 中間処理産業廃棄物の活性化/非活性化を設定する
        /// </summary>
        /// <param name="enableFlg">活性化/非活性化</param>
        /// <param name="dgvr">データグリッド行</param>
        /// <param name="isClearFlg">クリアフラグ(true:クリアを行う)</param>
        public void SetTyukanshoriCellReadOnly(bool enableFlg, DataGridViewRow dgvr, bool isClearFlg)
        {
            //dgvr.Cells["FM_MANIFEST_ID"].ReadOnly = enableFlg;
            dgvr.Cells["FM_HST_GYOUSHA_CD"].ReadOnly = enableFlg;
            dgvr.Cells["FM_HST_GENBA_CD"].ReadOnly = enableFlg;
            dgvr.Cells["FM_KOUFU_DATE"].ReadOnly = enableFlg;
            dgvr.Cells["FM_SBN_END_DATE"].ReadOnly = enableFlg;
            dgvr.Cells["FM_HAIKI_SHURUI_CD"].ReadOnly = enableFlg;
            dgvr.Cells["FM_HAIKI_SUU"].ReadOnly = enableFlg;
            dgvr.Cells["FM_HAIKI_UNIT_CD"].ReadOnly = enableFlg;

            if (isClearFlg)
            {
                dgvr.Cells["FM_HST_GYOUSHA_CD"].Value = string.Empty;
                dgvr.Cells["FM_HST_GENBA_CD"].Value = string.Empty;
                dgvr.Cells["FM_KOUFU_DATE"].Value = string.Empty;
                dgvr.Cells["FM_SBN_END_DATE"].Value = string.Empty;
                dgvr.Cells["FM_HAIKI_SHURUI_CD"].Value = string.Empty;
                dgvr.Cells["FM_HAIKI_SUU"].Value = string.Empty;
                dgvr.Cells["FM_HAIKI_UNIT_CD"].Value = string.Empty;
                dgvr.Cells["Hidden_FM_MEDIA_TYPE"].Value = string.Empty;
                dgvr.Cells["FM_HST_GYOUSHA_NAME"].Value = string.Empty;
                dgvr.Cells["FM_HST_GENBA_NAME"].Value = string.Empty;
                dgvr.Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value = string.Empty;
                dgvr.Cells["FM_UNIT_NAME_RYAKU"].Value = string.Empty;
                dgvr.Cells["Hidden_FM_SYSTEM_ID"].Value = string.Empty;
                dgvr.Cells["Hidden_KANRI_ID"].Value = string.Empty;
                dgvr.Cells["Hidden_R18_MANIFEST_ID"].Value = string.Empty;
            }

        }

        /// <summary>
        /// 中間処理産業廃棄物
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TyukanshoriCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var cell = this.cdgv_Tyukanshori.CurrentCell as ICustomDataGridControl;

            if (cell != null)
            {
                if (("FM_MANIFEST_ID").Equals(cell.GetName()))
                {
                    //編集不可の場合、チェック処理を行わない
                    if (this.cdgv_Tyukanshori.CurrentCell.ReadOnly)
                    {
                        return;
                    }
                    bool catchErr = false;
                    var rettyukansho = this.Logic.TyukanshoriCellValidating(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }

                    if (rettyukansho)
                    {
                        e.Cancel = true;
                    }
                }
            }

        }

        /// <summary>
        /// 中間処理産業廃棄物チェックボックスを設定する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccbx_Tyukanshori_CheckedChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.ccbx_Touranshitei))
            {
                if (this.ccbx_Touranshitei.Enabled && this.ccbx_Touranshitei.Checked)
                {
                    //レコードの追加・編集を可能とする
                    this.cdgv_Tyukanshori.AllowUserToAddRows = true;
                    // 削除チェックボックスを活性にする。
                    this.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = false;
                    this.cdgv_Tyukanshori.Columns["FM_MANIFEST_ID"].ReadOnly = false;
                    this.cdgv_Tyukanshori.Rows[0].Cells["Tyukanshori_chb_delete"].Style.BackColor = Constans.NOMAL_COLOR;
                    this.cdgv_Tyukanshori.Rows[0].Cells["FM_MANIFEST_ID"].Style.BackColor = Constans.NOMAL_COLOR;

                    this.ccbx_ItijiFuyou.Checked = false;
                    this.ccbx_ChouboKisai.Checked = false;
                }
                else
                {
                    this.cdgv_Tyukanshori.Rows.Clear();
                    this.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = true;
                    this.cdgv_Tyukanshori.Columns["FM_MANIFEST_ID"].ReadOnly = true;
                    this.cdgv_Tyukanshori.Rows[0].Cells["FM_MANIFEST_ID"].Style.BackColor = Constans.READONLY_COLOR;
                    this.cdgv_Tyukanshori.AllowUserToAddRows = true;
                    this.SetTyukanshoriReadOnly();
                }
            }
            else if (sender.Equals(this.ccbx_ItijiFuyou))
            {
                if (this.ccbx_ItijiFuyou.Enabled && this.ccbx_ItijiFuyou.Checked)
                {
                    this.ccbx_Touranshitei.Checked = false;
                    this.ccbx_ChouboKisai.Checked = false;
                    //レコードが存在する場合はクリアする
                    //レコードの追加・編集を不可とする
                    this.cdgv_Tyukanshori.AllowUserToAddRows = true;
                    this.SetTyukanshoriReadOnly();
                }
            }
            else if (sender.Equals(this.ccbx_ChouboKisai))
            {
                if (this.ccbx_ChouboKisai.Enabled && this.ccbx_ChouboKisai.Checked)
                {
                    this.ccbx_Touranshitei.Checked = false;
                    this.ccbx_ItijiFuyou.Checked = false;
                    //レコードが存在する場合はクリアする
                    //レコードの追加・編集を不可とする
                    this.cdgv_Tyukanshori.AllowUserToAddRows = true;
                    this.SetTyukanshoriReadOnly();
                    this.cdgv_Tyukanshori.Rows[0].Cells["FM_MANIFEST_ID"].Style.BackColor = Constans.READONLY_COLOR;
                }
            }

            if (this.cntxt_ManiKBN.Text == "1")
            {
                this.cdgv_Tyukanshori.Rows.Clear();
                this.SetTyukanshoriReadOnly();
                this.SetTyukanshoriReadOnly(true);
            }
        }

        /// <summary>
        /// ESCテキストイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void txb_process_Enter(object sender, KeyEventArgs e)
        {
            int iret = 0;
            iret = this.Logic.DoProcess(e);
            if (iret == -1)
            {
                return;
            }

            if (iret == 2)
            {
                Properties.Settings.Default.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                Properties.Settings.Default.Save();
                base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                //base.OnLoad(e);

            }
        }

        /// <summary>
        /// 中間処理産業廃棄物グリッドの編集不可設定する
        /// </summary>
        public void SetTyukanshoriReadOnly()
        {
            foreach (DataGridViewColumn dgvc in this.cdgv_Tyukanshori.Columns)
            {
                dgvc.ReadOnly = true;
            }
        }

        /// <summary>
        /// 廃棄物有害物質
        /// </summary>
        /// <param name="readOnlyFlg"></param>
        /// <param name="isColumnFlg"></param>
        /// <param name="isClearFlg"></param>
        /// <param name="rowIndex"></param>
        private void SetHaikibutuYuugaiColumnReadOnly(bool readOnlyFlg, bool isColumnFlg, bool isClearFlg, int rowIndex = 0)
        {
            if (isColumnFlg)
            {
                // 保留保存されている場合、有害物質の項目が編集可能になるようにする。
                if (!(this.Logic.HoryuFlg && this.Logic.HoryuINSFlg))
                {
                    this.cdgv_Haikibutu.Columns["YUUGAI_CODE1"].ReadOnly = readOnlyFlg;
                    this.cdgv_Haikibutu.Columns["YUUGAI_CODE2"].ReadOnly = readOnlyFlg;
                    this.cdgv_Haikibutu.Columns["YUUGAI_CODE3"].ReadOnly = readOnlyFlg;
                    this.cdgv_Haikibutu.Columns["YUUGAI_CODE4"].ReadOnly = readOnlyFlg;
                    this.cdgv_Haikibutu.Columns["YUUGAI_CODE5"].ReadOnly = readOnlyFlg;
                    this.cdgv_Haikibutu.Columns["YUUGAI_CODE6"].ReadOnly = readOnlyFlg;
                }

                //クリア
                if (isClearFlg)
                {
                    foreach (DataGridViewRow dgvr in this.cdgv_Haikibutu.Rows)
                    {
                        dgvr.Cells["YUUGAI_CODE1"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_CODE2"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_CODE3"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_CODE4"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_CODE5"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_CODE6"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_NAME1"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_NAME2"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_NAME3"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_NAME4"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_NAME5"].Value = string.Empty;
                        dgvr.Cells["YUUGAI_NAME6"].Value = string.Empty;
                    }
                }
            }
            else
            {
                // 保留保存されている場合、有害物質の項目が編集可能になるようにする。
                if (!(this.Logic.HoryuFlg && this.Logic.HoryuINSFlg))
                {
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE1"].ReadOnly = readOnlyFlg;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE2"].ReadOnly = readOnlyFlg;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE3"].ReadOnly = readOnlyFlg;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE4"].ReadOnly = readOnlyFlg;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE5"].ReadOnly = readOnlyFlg;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE6"].ReadOnly = readOnlyFlg;
                }

                //クリア
                if (isClearFlg)
                {
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE1"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE2"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE3"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE4"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE5"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE6"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_NAME1"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_NAME2"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_NAME3"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_NAME4"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_NAME5"].Value = string.Empty;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_NAME6"].Value = string.Empty;
                }

                // 有害物質項目の活性/非活性を制御
                if (this.CheckYuugaiColumnReadOnly(rowIndex))
                {
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE1"].ReadOnly = false;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE2"].ReadOnly = false;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE3"].ReadOnly = false;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE4"].ReadOnly = false;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE5"].ReadOnly = false;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE6"].ReadOnly = false;
                }
                else
                {
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE1"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE2"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE3"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE4"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE5"].ReadOnly = true;
                    this.cdgv_Haikibutu.Rows[rowIndex].Cells["YUUGAI_CODE6"].ReadOnly = true;
                }
            }
        }


        /// <summary>
        /// 画面に換算値と減容後数量の計算処理
        /// </summary>
        public void KansanGenyouFromForm()
        {
            SqlDecimal kansan_suu = 0;
            if (this.cdgv_Haikibutu.CurrentRow != null)
            {
                if (this.Logic.GetKansan_suu(ref kansan_suu))
                {
                    this.cdgv_Haikibutu.CurrentRow.Cells["KANSAN_SUU"].Value = kansan_suu;
                    SqlDecimal genyou_suu = 0;
                    bool catchErr = false;
                    var retGenyougou = this.Logic.GetGenYougou_suu(kansan_suu, ref genyou_suu,
                        this.cdgv_Haikibutu.CurrentRow.Index, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }

                    if (retGenyougou)
                    {
                        this.cdgv_Haikibutu.CurrentRow.Cells["GENNYOU_SUU"].Value = genyou_suu;
                    }
                    else
                    {
                        this.cdgv_Haikibutu.CurrentRow.Cells["GENNYOU_SUU"].Value = null;
                    }
                }
                else
                {
                    this.cdgv_Haikibutu.CurrentRow.Cells["KANSAN_SUU"].Value = null;
                    this.cdgv_Haikibutu.CurrentRow.Cells["GENNYOU_SUU"].Value = null;
                }
            }
        }
        /// <summary>
        /// Foot部で追加ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClearFormByNewInfoMode(object sender, EventArgs e)
        {
            // 権限チェック
            if (!Manager.CheckAuthority("G141", WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }
            this.InitializeFormByMode(WINDOW_TYPE.NEW_WINDOW_FLAG);
            this.isOpenG142 = false;
        }
        /// <summary>
        /// 運搬情報の追加削除行許可フラグの取得処理
        /// </summary>
        /// <returns></returns>
        public bool GetUnpanInfoAddDeleteFlg()
        {
            bool bRet = false;
            if (this.Logic.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                if (string.IsNullOrWhiteSpace(this.cantxt_SBN_JyutakuShaCD.Text) && string.IsNullOrWhiteSpace(this.cantxt_SBN_Genba_CD.Text))
                {
                    bRet = true;
                }
            }
            if (this.Logic.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                if (this.Logic.ManiInfo.dt_r18.MANIFEST_KBN == 1 && this.Logic.ManiInfo.lstDT_R19.Count == 0)
                {
                    bRet = true;
                }
            }
            return bRet;
        }

        /// <summary>
        /// 運搬情報グリッドCellEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_UnpanInfo_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.cdgv_UnpanInfo.Columns[e.ColumnIndex].Name)
            {
                case "UPN_SHA_CD":              // 収集運搬業者CD
                case "UNPANSAKI_GYOUSHA_CD":    // 運搬先業者CD
                case "UNPANSAKI_GENBA_CD":		// 運搬先事業場CD
                case "UPN_WAY_CODE":    		// 運搬方法CD
                case "UNPANTAN_CD":    			// 運搬担当者CD
                case "SHARYOU_CD":              // 車輌番号CD
                    this.cdgv_UnpanInfo.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                default:
                    this.cdgv_UnpanInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
                    break;
            }

            if (e.RowIndex > -1 && e.ColumnIndex > -1 && !this.popupFlg)
            {
                this.SetUnpanInfoPrevValue(this.cdgv_UnpanInfo.Columns[e.ColumnIndex].Name,
                    Convert.ToString(this.cdgv_UnpanInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
            }

            this.popupFlg = false;
        }

        /// <summary>
        /// 引渡担当者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_HikiwataTantouSha_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }

                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 登録担当者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_TourokuTantouSha_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }

                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 排出事業者名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_HaisyutuGyousyaName_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 排出事業者住所
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_HaisyutuGyousyaAddr_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 排出事業場名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_HaisyutuGenbaName_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 排出事業場住所
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_HaisyutuGenbaAddr_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 運搬情報備考
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_UnpanBikou_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 処分受託者名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_SBN_JyutakuShaName_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 処分受託者住所
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_SBN_GyouShaAddr_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 処分事業場名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_SBN_Genba_Name_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }

                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 処分事業場住所
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SBN_GenbaAddr_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }

                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 処分受託者備考
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_SBN_Bikou_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox ctb = (CustomTextBox)sender;

            if (!ctb.ReadOnly)
            {
                //フォマート未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                {
                    bool catchErr = false;
                    var retcheck = this.Logic.KinsokuMoziCheck(ctb.Text, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }

                    if (!retcheck)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 産業廃棄物グリッドCellEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_Haikibutu_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.cdgv_Haikibutu.Columns[e.ColumnIndex].Name)
            {
                case "HAIKI_SHURUI_CD":			// 廃棄物種類CD
                case "HAIKI_NAME_CD":    		// 廃棄物名称CD
                case "HAIKI_SUU":    			// 数量
                case "UNIT_CD":    				// 単位CD
                case "NISUGATA_CD":    			// 荷姿CD
                case "NISUGATA_SUU":    		// 荷姿数量
                case "SUU_KAKUTEI_CODE":    	// 数量確定者CD
                    this.cdgv_Haikibutu.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                default:
                    this.cdgv_Haikibutu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
                    break;
            }
        }

        /// <summary>
        /// 中間処理産業廃棄物グリッドCellEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_Tyukanshori_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.cdgv_Tyukanshori.Columns[e.ColumnIndex].Name)
            {
                case "FM_MANIFEST_ID":			// マニフェスト番号/交付
                    this.cdgv_Tyukanshori.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                default:
                    this.cdgv_Tyukanshori.ImeMode = System.Windows.Forms.ImeMode.NoControl;
                    break;
            }
        }

        /// <summary>
        /// 最終処分の場所(予定)グリッドCellEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_LastSBNbasyo_yotei_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.cdgv_LastSBNbasyo_yotei.Columns[e.ColumnIndex].Name)
            {
                case "LAST_SBN_GYOUSHA_CD":		// 最終処分業者CD
                case "LAST_SBN_JOU_CD":    		// 最終処分事業場CD
                case "LAST_SBN_JOU_POST":       // 郵便番号
                case "LAST_SBN_JOU_TEL":        // 電話番号
                    this.cdgv_LastSBNbasyo_yotei.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "LAST_SBN_JOU_NAME":       //最終処分事業場名称
                    this.cdgv_LastSBNbasyo_yotei.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    this.cdgv_LastSBNbasyo_yotei.ImeMode = System.Windows.Forms.ImeMode.NoControl;
                    break;
            }

            // 一覧の最終行で削除チェック以外のカラムの場合、かつ行追加がOFFになっている場合、行追加をONにする。
            if (this.cdgv_LastSBNbasyo_yotei.AllowUserToAddRows.ToString().Equals("False")
                && !this.cdgv_LastSBNbasyo_yotei.Columns[e.ColumnIndex].Name.Equals("LastSBNbasyo_chb_delete")
                && this.cdgv_LastSBNbasyo_yotei.CurrentRow.Index == (this.cdgv_LastSBNbasyo_yotei.Rows.Count - 1))
            {
                //10件の制限
                cdgv_LastSBNbasyo_yotei.AllowUserToAddRows = (cdgv_LastSBNbasyo_yotei.Rows.Count >= 10) ? false : true;
            }
        }

        /// <summary>
        /// 最終処分事業場(実績)グリッドCellEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_LastSBN_Genba_Jiseki_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.cdgv_LastSBN_Genba_Jiseki.Columns[e.ColumnIndex].Name)
            {
                case "LAST_SBN_END_DATE":	        // 最終処分終了日
                case "LAST_SBN_GYOUSHA_JISEKI_CD":	// 最終処分業者CD
                case "LAST_SBN_JOU_JISEKI_CD":    	// 最終処分事業場CD
                case "LAST_SBN_JOU_JISEKI_POST":    // 郵便番号
                case "LAST_SBN_JOU_JISEKI_TEL":    	// 電話番号
                    this.cdgv_LastSBN_Genba_Jiseki.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "LAST_SBN_JOU_JISEKI_NAME":    //最終処分事業場名称
                    this.cdgv_LastSBN_Genba_Jiseki.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    this.cdgv_LastSBN_Genba_Jiseki.ImeMode = System.Windows.Forms.ImeMode.NoControl;
                    break;
            }

            // 一覧の最終行で削除チェック以外のカラムの場合、かつ行追加がOFFになっている場合、行追加をONにする。
            if (this.cdgv_LastSBN_Genba_Jiseki.AllowUserToAddRows.ToString().Equals("False")
                && !this.cdgv_LastSBN_Genba_Jiseki.Columns[e.ColumnIndex].Name.Equals("LastSBN_Genba_chb_delete")
                && this.cdgv_LastSBN_Genba_Jiseki.CurrentRow.Index == (this.cdgv_LastSBN_Genba_Jiseki.Rows.Count - 1))
            {
                //行数が8件の制限
                cdgv_LastSBN_Genba_Jiseki.AllowUserToAddRows = (cdgv_LastSBN_Genba_Jiseki.Rows.Count >= 8) ? false : true;
            }
        }

        /// <summary>
        /// 最終処分事業場(実績) CellValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_LastSBN_Genba_Jiseki_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.cdgv_LastSBN_Genba_Jiseki.Columns[e.ColumnIndex].Name)
            {
                // 最終処分終了日
                case "LAST_SBN_END_DATE":
                    DateTime latestLastSbnDate = this.Logic.GetLatestLastSbnDate();
                    if (DateTime.Compare(DateTime.MinValue, latestLastSbnDate) < 0)
                    {
                        this.cdpt_LastSBNEndDate.Value = latestLastSbnDate.Date;
                    }
                    else
                    {
                        this.cdpt_LastSBNEndDate.Value = null;
                    }

                    break;

                // 運搬先業者
                case "LAST_SBN_GYOUSHA_JISEKI_CD":
                    var gyoushaCd = this.cdgv_LastSBN_Genba_Jiseki.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (gyoushaCd != null && !string.IsNullOrEmpty(gyoushaCd.ToString()))
                    {
                        this.cdgv_LastSBN_Genba_Jiseki.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = gyoushaCd.ToString().PadLeft(6, '0').ToUpper();
                    }
                    break;

                // 運搬先現場
                case "LAST_SBN_JOU_JISEKI_CD":
                    var genbaCd = this.cdgv_LastSBN_Genba_Jiseki.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (genbaCd != null && !string.IsNullOrEmpty(genbaCd.ToString()))
                    {
                        this.cdgv_LastSBN_Genba_Jiseki.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = genbaCd.ToString().PadLeft(6, '0').ToUpper();
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// フォーム表示時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // データ移動初期表示処理
            this.Logic.SetMoveData();
        }

        /// <summary>
        /// 運搬情報入力グリッドCellValidated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_UnpanInfo_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.cdgv_UnpanInfo.Columns[e.ColumnIndex].Name)
            {
                case "UNPANSAKI_GENBA_CD":
                    if (!this.isChangeUnpanInfoValue(
                            this.cdgv_UnpanInfo.Columns[e.ColumnIndex].Name,
                            Convert.ToString(this.cdgv_UnpanInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                        )
                    {
                        break;
                    }

                    if (!this.Logic.ChkUnpansakiJigyoujou(false))
                    {
                        return;
                    }
                    break;
                case "UPN_WAY_CODE":
                    if (this.cdgv_UnpanInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                        && !string.IsNullOrEmpty(this.cdgv_UnpanInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                    {
                        this.cdgv_UnpanInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                            this.cdgv_UnpanInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
                    }
                    break;
                case "SHARYOU_CD":
                    if (this.cdgv_UnpanInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                        && !string.IsNullOrEmpty(this.cdgv_UnpanInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                    {
                        this.cdgv_UnpanInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                            this.cdgv_UnpanInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().PadLeft(6, '0').ToUpper();
                    }
                    break;
            }
        }

        #region 備考欄制御
        /// <summary>
        /// 備考欄にデフォルト行を追加
        /// </summary>
        internal void AddDefaultRowForBikou()
        {
            if (this.cdgv_Bikou.RowCount < bikouMaxRowCount)
            {
                var addCount = bikouMaxRowCount - this.cdgv_Bikou.RowCount;
                this.cdgv_Bikou.Rows.Add(addCount);
            }

            for (int i = 0; i < cdgv_Bikou.Rows.Count; i++)
            {
                cdgv_Bikou.Rows[i].Cells[0].Value = i + 1;

                //参照モードの背景色設定
                if (this.Logic.Mode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    cdgv_Bikou.Rows[i].Cells["BIKOU"].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 備考欄CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_Bikou_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.cdgv_Bikou.Columns[e.ColumnIndex].Name)
            {
                case "BIKOU":
                    this.cdgv_Bikou.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                    break;
                default:
                    this.cdgv_Bikou.ImeMode = System.Windows.Forms.ImeMode.NoControl;
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 処分方法CDテキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_SBN_houhouCD_Enter(object sender, EventArgs e)
        {
            this.SetPrevValue(this.cantxt_SBN_houhouCD);
        }

        /// <summary>
        /// 処分方法CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_SBN_houhouCD_Validated(object sender, EventArgs e)
        {
            if (this.isChangeValue(this.cantxt_SBN_houhouCD))
            {
                foreach (DataGridViewRow row in this.cdgv_Haikibutu.Rows)
                {
                    if (row.Cells["KANSAN_SUU"].Value != null)
                    {
                        var genyou_suu = SqlDecimal.Null;
                        bool catchErr = false;
                        var ret = this.Logic.GetGenYougou_suu(SqlDecimal.Parse(row.Cells["KANSAN_SUU"].Value.ToString()),
                            ref genyou_suu, row.Index, out catchErr);
                        if (catchErr)
                        {
                            return;
                        }

                        if (ret)
                        {
                            row.Cells["GENNYOU_SUU"].Value = genyou_suu.Value;
                        }
                        else
                        {
                            row.Cells["GENNYOU_SUU"].Value = null;
                        }
                    }
                    else
                    {
                        row.Cells["GENNYOU_SUU"].Value = null;
                    }
                }
            }
        }

        /// <summary>
        /// 運搬量テキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cntxt_UnpanRyo_Enter(object sender, EventArgs e)
        {
            this.SetPrevValue(this.cntxt_UnpanRyo);
        }

        /// <summary>
        /// 運搬量テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cntxt_UnpanRyo_Validated(object sender, EventArgs e)
        {
            if (this.isChangeValue(this.cntxt_UnpanRyo))
            {
                if (!this.SetUnpanHoukokuDataDtoFromForm())
                {
                    return;
                }

                // 選択している行の運搬区間が数量確定者の場合は、換算後数量を再計算する
                if (this.cdgv_UnpanInfo.CurrentRow != null)
                {
                    this.CalcKansangoSuryoFromUnpanJouhou(this.cdgv_UnpanInfo.CurrentRow.Index);
                }
            }
        }

        /// <summary>
        /// 運搬量単位CDテキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanRyoUnitCd_Enter(object sender, EventArgs e)
        {
            this.SetPrevValue(this.cantxt_UnpanRyoUnitCd);
        }

        /// <summary>
        /// 運搬量単位CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_UnpanRyoUnitCd_Validated(object sender, EventArgs e)
        {
            if (this.isChangeValue(this.cantxt_UnpanRyoUnitCd))
            {
                if (!this.SetUnpanHoukokuDataDtoFromForm()) { return; }

                // 選択している行の運搬区間が数量確定者の場合は、換算後数量を再計算する
                if (this.cdgv_UnpanInfo.CurrentRow != null)
                {
                    this.CalcKansangoSuryoFromUnpanJouhou(this.cdgv_UnpanInfo.CurrentRow.Index);
                }
            }
        }

        /// <summary>
        /// 運搬情報変更時の換算後数量計算をします
        /// </summary>
        /// <param name="unpanRowIndex">運搬情報データグリッドの行Index</param>
        private void CalcKansangoSuryoFromUnpanJouhou(int unpanRowIndex)
        {
            foreach (DataGridViewRow row in this.cdgv_Haikibutu.Rows)
            {
                var kansan_suu = SqlDecimal.Null;
                if (this.Logic.GetKansan_suu(ref kansan_suu))
                {
                    row.Cells["KANSAN_SUU"].Value = kansan_suu.Value;
                    var genyou_suu = SqlDecimal.Null;
                    bool catchErr = false;
                    var retGenyougou = this.Logic.GetGenYougou_suu(SqlDecimal.Parse(row.Cells["KANSAN_SUU"].Value.ToString()),
                        ref genyou_suu, row.Index, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }

                    if (retGenyougou)
                    {
                        row.Cells["GENNYOU_SUU"].Value = genyou_suu.Value;
                    }
                    else
                    {
                        row.Cells["GENNYOU_SUU"].Value = null;
                    }
                }
                else
                {
                    row.Cells["KANSAN_SUU"].Value = null;
                    row.Cells["GENNYOU_SUU"].Value = null;
                }
            }
        }

        /// <summary>
        /// 受入量テキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cntxt_Jyunyuryo_Enter(object sender, EventArgs e)
        {
            this.SetPrevValue(this.cntxt_Jyunyuryo);
        }

        /// <summary>
        /// 受入量テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_Jyunyuryo_Validated(object sender, EventArgs e)
        {
            if (this.isChangeValue(this.cntxt_Jyunyuryo))
            {
                this.CalcKansangoSuryoFromUkeireRyo();
            }
        }

        /// <summary>
        /// 受入量単位CDテキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_JyunyuryoUnitCD_Enter(object sender, EventArgs e)
        {
            this.SetPrevValue(this.cantxt_JyunyuryoUnitCD);
        }

        /// <summary>
        /// 受入量単位CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_JyunyuryoUnitCD_Validated(object sender, EventArgs e)
        {
            if (this.isChangeValue(this.cantxt_JyunyuryoUnitCD))
            {
                this.CalcKansangoSuryoFromUkeireRyo();
            }
        }

        /// <summary>
        /// 受入量変更時の換算後数量計算をします
        /// </summary>
        private void CalcKansangoSuryoFromUkeireRyo()
        {
            foreach (DataGridViewRow row in this.cdgv_Haikibutu.Rows)
            {
                var kansan_suu = SqlDecimal.Null;
                if (this.Logic.GetKansan_suu(ref kansan_suu))
                {
                    row.Cells["KANSAN_SUU"].Value = kansan_suu.Value;
                    var genyou_suu = SqlDecimal.Null;
                    bool catchErr = false;
                    var retGenyougou = this.Logic.GetGenYougou_suu(SqlDecimal.Parse(row.Cells["KANSAN_SUU"].Value.ToString()),
                        ref genyou_suu, row.Index, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }

                    if (retGenyougou)
                    {
                        row.Cells["GENNYOU_SUU"].Value = genyou_suu.Value;
                    }
                    else
                    {
                        row.Cells["GENNYOU_SUU"].Value = null;
                    }
                }
                else
                {
                    row.Cells["KANSAN_SUU"].Value = null;
                    row.Cells["GENNYOU_SUU"].Value = null;
                }
            }
        }

        /// <summary>
        /// （将軍）処分方法CDテキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_Shogun_SBN_houhouCD_Enter(object sender, EventArgs e)
        {
            this.SetPrevValue(this.cantxt_Shogun_SBN_houhouCD);
        }

        /// <summary>
        /// （将軍）処分方法CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_Shogun_SBN_houhouCD_Validated(object sender, EventArgs e)
        {
            if (this.isChangeValue(this.cantxt_Shogun_SBN_houhouCD))
            {
                foreach (DataGridViewRow row in this.cdgv_Haikibutu.Rows)
                {
                    if (row.Cells["KANSAN_SUU"].Value != null)
                    {
                        var genyou_suu = SqlDecimal.Null;
                        bool catchErr = false;
                        var ret = this.Logic.GetGenYougou_suu(SqlDecimal.Parse(row.Cells["KANSAN_SUU"].Value.ToString()),
                            ref genyou_suu, row.Index, out catchErr);
                        if (catchErr)
                        {
                            return;
                        }

                        if (ret)
                        {
                            row.Cells["GENNYOU_SUU"].Value = genyou_suu.Value;
                        }
                        else
                        {
                            row.Cells["GENNYOU_SUU"].Value = null;
                        }
                    }
                    else
                    {
                        row.Cells["GENNYOU_SUU"].Value = null;
                    }
                }
            }
        }

        /// <summary>
        /// 運搬情報入力グリッドRowValidated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_UnpanInfo_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.cdgv_UnpanInfo.Rows[e.RowIndex].IsNewRow)
            {
                this.SetUnpanHoukokuDataDtoFromForm(e.RowIndex);
            }
            else
            {
                this.cdgv_UnpanInfo.Rows[e.RowIndex].Tag = new UnpanHoukokuDataDTOCls();
            }
        }

        /// <summary>
        /// コントロールから運搬情報Dtoに値をセットします
        /// </summary>
        private bool SetUnpanHoukokuDataDtoFromForm()
        {
            try
            {
                var row = this.cdgv_UnpanInfo.CurrentRow;
                if (row != null)
                {
                    this.SetUnpanHoukokuDataDtoFromForm(row.Index);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetUnpanHoukokuDataDtoFromForm", ex);
                this.Logic.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// コントロールから運搬情報Dtoに値をセットします
        /// </summary>
        /// <param name="rowIndex">行index</param>
        private void SetUnpanHoukokuDataDtoFromForm(int rowIndex)
        {
            if (this.cdgv_UnpanInfo.Rows.Count - 1 >= rowIndex)
            {
                var row = this.cdgv_UnpanInfo.Rows[rowIndex];

                var dto = new UnpanHoukokuDataDTOCls();

                dto.cantxt_HoukokuTantoushaCD = this.cantxt_HoukokuTantoushaCD.Text;
                dto.cantxt_SyaryoNo = this.cantxt_SyaryoNo.Text;
                if (!String.IsNullOrEmpty(this.cantxt_UnpanRyoUnitCd.Text))
                {
                    dto.cantxt_UnpanRyoUnitCd = this.cantxt_UnpanRyoUnitCd.Text.Substring(this.cantxt_UnpanRyoUnitCd.Text.Length - 1, 1);
                }
                dto.cantxt_UnpanTantoushaCd = this.cantxt_UnpanTantoushaCd.Text;
                if (!String.IsNullOrEmpty(this.cantxt_YukabutuRyoUnitCd.Text))
                {
                    dto.cantxt_YukabutuRyoUnitCd = this.cantxt_YukabutuRyoUnitCd.Text.Substring(this.cantxt_YukabutuRyoUnitCd.Text.Length - 1, 1);
                }
                dto.cantxt_YukabutuRyoUnitName = this.cantxt_YukabutuRyoUnitName.Text;
                if (this.cdtp_UnpanEndDate.Value != null)
                {
                    dto.cdtp_UnpanEndDate = this.cdtp_UnpanEndDate.Value.ToString().Substring(0, 10).Replace("/", "");
                }
                if (!String.IsNullOrEmpty(this.cntxt_UnpanRyo.Text))
                {
                    dto.cntxt_UnpanRyo = decimal.Parse(this.cntxt_UnpanRyo.Text);
                }
                if (!String.IsNullOrEmpty(this.cntxt_YukabutuRyo.Text))
                {
                    dto.cntxt_YukabutuRyo = decimal.Parse(this.cntxt_YukabutuRyo.Text);
                }
                dto.ctxt_HoukokuTantoushaName = this.ctxt_HoukokuTantoushaName.Text;
                dto.ctxt_UnitName = this.ctxt_UnpanRyoUnitName.Text;
                dto.ctxt_UnpanBikou = this.ctxt_UnpanBikou.Text;
                dto.ctxt_UnpanSyaryoName = this.ctxt_UnpanSyaryoName.Text;
                dto.ctxt_UnpanTantoushaName = this.ctxt_UnpanTantoushaName.Text;

                row.Tag = dto;
            }
        }

        /// <summary>
        /// 運搬終了日テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cdtp_UnpanEndDate_Validated(object sender, EventArgs e)
        {
            this.SetUnpanHoukokuDataDtoFromForm();
        }

        /// <summary>
        /// 運搬担当者CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_UnpanTantoushaCd_Validated(object sender, EventArgs e)
        {
            this.SetUnpanHoukokuDataDtoFromForm();
        }

        /// <summary>
        /// 車輌番号テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_SyaryoNo_Validated(object sender, EventArgs e)
        {
            this.SetUnpanHoukokuDataDtoFromForm();
        }

        /// <summary>
        /// 報告担当者CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_HoukokuTantoushaCD_Validated(object sender, EventArgs e)
        {
            this.SetUnpanHoukokuDataDtoFromForm();
        }

        /// <summary>
        /// 有価物数量テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cntxt_YukabutuRyo_Validated(object sender, EventArgs e)
        {
            this.SetUnpanHoukokuDataDtoFromForm();
        }

        /// <summary>
        /// 有価物数量単位CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cantxt_YukabutuRyoUnitCd_Validated(object sender, EventArgs e)
        {
            this.SetUnpanHoukokuDataDtoFromForm();
        }

        /// <summary>
        /// 備考テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void ctxt_UnpanBikou_Validated(object sender, EventArgs e)
        {
            this.SetUnpanHoukokuDataDtoFromForm();
        }

        /// <summary>
        /// 運搬情報Dtoの値をコントロールにセットします
        /// </summary>
        private bool GetUnpanHoukokuDataDtoToForm()
        {
            try
            {
                var row = this.cdgv_UnpanInfo.CurrentRow;
                if (row != null)
                {
                    this.GetUnpanHoukokuDataDtoToForm(row.Index);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetUnpanHoukokuDataDtoToForm", ex);
                this.Logic.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 運搬情報Dtoの値をコントロールにセットします
        /// </summary>
        /// <param name="rowIndex">行index</param>
        private void GetUnpanHoukokuDataDtoToForm(int rowIndex)
        {
            if (this.cdgv_UnpanInfo.Rows.Count - 1 >= rowIndex)
            {
                var row = this.cdgv_UnpanInfo.Rows[rowIndex];
                if (row.Tag != null)
                {
                    var dto = (UnpanHoukokuDataDTOCls)row.Tag;

                    this.cantxt_HoukokuTantoushaCD.Text = dto.cantxt_HoukokuTantoushaCD;
                    this.cantxt_SyaryoNo.Text = dto.cantxt_SyaryoNo;
                    this.cantxt_UnpanRyoUnitCd.Text = dto.cantxt_UnpanRyoUnitCd;
                    this.cantxt_UnpanTantoushaCd.Text = dto.cantxt_UnpanTantoushaCd;
                    this.cantxt_YukabutuRyoUnitCd.Text = dto.cantxt_YukabutuRyoUnitCd;
                    this.cantxt_YukabutuRyoUnitName.Text = dto.cantxt_YukabutuRyoUnitName;
                    this.cdtp_UnpanEndDate.Value = dto.cdtp_UnpanEndDate;
                    if (!dto.cntxt_UnpanRyo.IsNull)
                    {
                        this.cntxt_UnpanRyo.Text = dto.cntxt_UnpanRyo.Value.ToString();
                    }
                    else
                    {
                        this.cntxt_UnpanRyo.Text = String.Empty;
                    }
                    if (!dto.cntxt_YukabutuRyo.IsNull)
                    {
                        this.cntxt_YukabutuRyo.Text = dto.cntxt_YukabutuRyo.Value.ToString();
                    }
                    else
                    {
                        this.cntxt_YukabutuRyo.Text = String.Empty;
                    }
                    this.ctxt_HoukokuTantoushaName.Text = dto.ctxt_HoukokuTantoushaName;
                    this.ctxt_UnpanRyoUnitName.Text = dto.ctxt_UnitName;
                    this.ctxt_UnpanBikou.Text = dto.ctxt_UnpanBikou;
                    this.ctxt_UnpanSyaryoName.Text = dto.ctxt_UnpanSyaryoName;
                    this.ctxt_UnpanTantoushaName.Text = dto.ctxt_UnpanTantoushaName;
                }
                else
                {
                    this.cantxt_HoukokuTantoushaCD.Text = String.Empty;
                    this.cantxt_SyaryoNo.Text = String.Empty;
                    this.cantxt_UnpanRyoUnitCd.Text = String.Empty;
                    this.cantxt_UnpanTantoushaCd.Text = String.Empty;
                    this.cantxt_YukabutuRyoUnitCd.Text = String.Empty;
                    this.cantxt_YukabutuRyoUnitName.Text = String.Empty;
                    this.cdtp_UnpanEndDate.Value = String.Empty;
                    this.cntxt_UnpanRyo.Text = String.Empty;
                    this.cntxt_YukabutuRyo.Text = String.Empty;
                    this.ctxt_HoukokuTantoushaName.Text = String.Empty;
                    this.ctxt_UnpanRyoUnitName.Text = String.Empty;
                    this.ctxt_UnpanBikou.Text = String.Empty;
                    this.ctxt_UnpanSyaryoName.Text = String.Empty;
                    this.ctxt_UnpanTantoushaName.Text = String.Empty;
                }
            }
        }

        /// <summary>
        /// テキストボックスに入力されている値を保存します
        /// </summary>
        /// <param name="control">対象のテキストボックス</param>
        private void SetPrevValue(TextBox control)
        {
            if (this.prevValueDictionary == null)
            {
                this.prevValueDictionary = new Dictionary<String, String>();
            }

            var name = control.Name;
            if (this.prevValueDictionary.ContainsKey(name))
            {
                this.prevValueDictionary[name] = control.Text;
            }
            else
            {
                this.prevValueDictionary.Add(name, control.Text);
            }
        }

        /// <summary>
        /// テキストボックスに入力されていた値を取得します
        /// </summary>
        /// <param name="control">対象のテキストボックス</param>
        /// <returns>保存されていた値</returns>
        private string GetPrevValue(TextBox control)
        {
            string ret = null;

            if (this.prevValueDictionary != null)
            {
                var name = control.Name;
                if (this.prevValueDictionary.ContainsKey(name))
                {
                    ret = this.prevValueDictionary[name];
                }
            }

            return ret;
        }

        /// <summary>
        /// テキストボックスの値が変更されたかどうかを判断します
        /// </summary>
        /// <param name="control">対象のテキストボックス</param>
        /// <returns>変更されている場合は、True</returns>
        private bool isChangeValue(TextBox control)
        {
            bool ret = true;

            if (this.prevValueDictionary != null)
            {
                var name = control.Name;
                if (this.prevValueDictionary.ContainsKey(name))
                {
                    if (control.Text == this.prevValueDictionary[name])
                    {
                        ret = false;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 入力されている値を保存します(運搬情報)
        /// </summary>
        /// <param name="controlNmae">cdgv_UnpanInfo.Cell.Name</param>
        /// <param name="value">cdgv_UnpanInfo.Cell.Value.ToString()</param>
        private void SetUnpanInfoPrevValue(string controlNmae, string value)
        {
            if (string.IsNullOrEmpty(controlNmae))
            {
                return;
            }

            if (this.prevUnpanInfoValueDictionary == null)
            {
                this.prevUnpanInfoValueDictionary = new Dictionary<String, String>();
            }

            if (this.prevUnpanInfoValueDictionary.ContainsKey(controlNmae))
            {
                this.prevUnpanInfoValueDictionary[controlNmae] = value;
            }
            else
            {
                this.prevUnpanInfoValueDictionary.Add(controlNmae, value);
            }
        }

        /// <summary>
        /// 値が変更されたかどうかを判断します(運搬情報)
        /// </summary>
        /// <param name="control">対象のテキストボックス</param>
        /// <returns>変更されている場合は、True</returns>
        private bool isChangeUnpanInfoValue(string controlNmae, string value)
        {
            bool ret = true;

            if (!string.IsNullOrEmpty(controlNmae)
                && this.prevUnpanInfoValueDictionary != null)
            {
                if (this.prevUnpanInfoValueDictionary.ContainsKey(controlNmae))
                {
                    if (value.Equals(this.prevUnpanInfoValueDictionary[controlNmae]))
                    {
                        ret = false;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// cdgv_LastSBNbasyo_yoteiのEditingControlShowingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_LastSBNbasyo_yotei_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            this.dgvKeyPressEventSwitch(sender, e);
        }

        /// <summary>
        /// cdgv_LastSBN_Genba_JisekiのEditingControlShowingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_LastSBN_Genba_Jiseki_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            this.dgvKeyPressEventSwitch(sender, e);
        }

        /// <summary>
        /// DataGridViewの郵便番号のカラムのKeyPressイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPostTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = this.postInputJudgment(e);
            base.OnKeyPress(e);
        }

        /// <summary>
        /// KeyPressイベントを切り替えます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvKeyPressEventSwitch(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridView dgv = (DataGridView)sender;

                // 編集のために表示されているコントロールを取得
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;

                // イベントハンドラを削除
                tb.KeyPress -= new KeyPressEventHandler(dgvPostTextBox_KeyPress);

                // 該当する例か調べる
                //  現在のカラムが郵便番号の場合
                if (dgv.CurrentCell.OwningColumn.Name == "LAST_SBN_JOU_POST" || dgv.CurrentCell.OwningColumn.Name == "LAST_SBN_JOU_JISEKI_POST")
                {
                    // イベントハンドラを追加
                    tb.KeyPress += new KeyPressEventHandler(dgvPostTextBox_KeyPress);
                }
            }
        }

        /// <summary>
        /// 郵便番号の入力審査を行います
        /// </summary>
        /// <param name="e">入力キー(KeyPressEventArgs)</param>
        /// <returns>
        /// true = 入力が数字・ハイフン・バックスペースの時
        /// false = 入力が数字・ハイフン・バックスペース以外の時
        /// </returns>
        private bool postInputJudgment(KeyPressEventArgs e)
        {
            var ren = false;

            // 数字・ハイフン・バックスペースしか入力できないようにする
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '-' && e.KeyChar != '\b')
            {
                ren = true;
            }

            return ren;
        }

        #region - 部分更新処理 -
        /// <summary>
        /// 部分更新イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void partialUpdate(object sender, EventArgs e)
        {
            // 部分更新処理
            this.Logic.partialUpdate();
        }

        #endregion - 部分更新処理 -

        #region - 明細削除処理 -
        /// <summary>
        /// 明細削除イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DeleteRow(object sender, EventArgs e)
        {
            // 明細削除処理
            this.Logic.DeleteRow();
        }

        #endregion - 明細削除処理 -

        #region 20150615 hoanghm #1226
        /// <summary>
        /// Current cell index befor delete
        /// </summary>
        private int currentHaikibutuCellIndex = 0;

        /// <summary>
        /// UserDeletingRowイベントを切り替えます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_Haikibutu_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (cdgv_Haikibutu.CurrentCell != null)
            {
                currentHaikibutuCellIndex = cdgv_Haikibutu.CurrentCell.ColumnIndex;
            }
        }

        /// <summary>
        /// UserDeletedRowイベントを切り替えます
        /// If property AllowUserToAddRows is false, add new row when user press delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_Haikibutu_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //If property AllowUserToAddRows is false and count of rows is 0, I will add new rows to grid.
            if (cdgv_Haikibutu.AllowUserToAddRows == false && cdgv_Haikibutu.Rows.Count == 0)
            {
                cdgv_Haikibutu.Rows.Add();
                cdgv_Haikibutu.CurrentCell = cdgv_Haikibutu.Rows[0].Cells[currentHaikibutuCellIndex];
            }
        }

        #endregion

        /// <summary>
        /// 排出事業者Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuGyousyaCd_Enter(object sender, EventArgs e)
        {
            this.preKanyushaNoForGyousha = this.ctxt_Haisyutu_KanyushaNo.Text;
        }

        /// <summary>
        /// 排出事業者TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuGyousyaCd_TextChanged(object sender, EventArgs e)
        {
            this.isEditHstGyousha = true;
        }

        /// <summary>
        /// 排出事業場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuGyousyaCd_Validating(object sender, EventArgs e)
        {
            if (this.isEditHstGyousha)
            {
                this.ctxt_Haisyutu_KanyushaNo.Text = string.Empty;
            }

            if (this.ctxt_Haisyutu_KanyushaNo.Text != this.preKanyushaNoForGyousha)
            {
                this.Logic.InitHstGenbaInfo();
            }
            this.isEditHstGyousha = false;
        }

        /// <summary>
        /// 排出事業者 PopupBeforeExecuteMethod
        /// </summary>
        public void HaisyutuGyousya_PopupBeforeExecuteMethod()
        {
            this.preKanyushaNoForGyousha = this.ctxt_Haisyutu_KanyushaNo.Text;
        }

        /// <summary>
        /// 排出事業者 PopupAfterExecuteMethod
        /// </summary>
        public void HaisyutuGyousya_PopupAfterExecuteMethod()
        {
            if (this.ctxt_Haisyutu_KanyushaNo.Text != this.preKanyushaNoForGyousha)
            {
                this.Logic.InitHstGenbaInfo();
            }
            this.isEditHstGyousha = false;
        }

        /// <summary>
        /// 排出事業場TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuGenbaCd_TextChanged(object sender, EventArgs e)
        {
            this.isEditHstGenba = true;
        }

        /// <summary>
        /// 排出事業場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuGenbaCd_Validating(object sender, EventArgs e)
        {
            if (this.isEditHstGenba)
            {
                this.ctxt_JIGYOUJYOU_CD.Text = string.Empty;
            }
            this.isEditHstGenba = false;
        }

        /// <summary>
        /// 排出事業場 PopupAfterExecuteMethod
        /// </summary>
        public void HaisyutuGenba_PopupAfterExecuteMethod()
        {
            this.isEditHstGyousha = false;
            this.isEditHstGenba = false;
        }

        /// <summary>
        /// 運搬情報の運搬先事業場CDのポップアップ選択後の処理
        /// </summary>
        /// <param name="control"></param>
        /// <param name="result"></param>
        public void PopupAfter_UNPANSAKI_GENBA_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            this.popupFlg = true;
        }
        /// <summary>
        /// DBデータを取得
        /// </summary>
        private string GetDbValue(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return string.Empty;
            }

            return obj.ToString();
        }
        /// <summary>
        /// 単位CDのゼロパディング処理
        /// </summary>
        /// <param name="unitCd"></param>
        /// <returns></returns>
        public string PadLeftUnitCd(string unitCd)
        {
            string unitCdRetVal = string.Empty;

            if (!string.IsNullOrEmpty(unitCd))
            {
                unitCdRetVal = unitCd.PadLeft(2, '0');
            }

            return unitCdRetVal;
        }

        /// <summary>
        /// Entityから画面に反映処理
        /// </summary>
        /// <param name="ManiInfo"></param>
        public void ConfirmFormFromEntity(DenshiManifestInfoCls ManiInfo, DenshiManifestInfoCls SEQManiInfo)
        {
            if (!this.Logic.seqFlag) return;
            if (ManiInfo == null) return;
            if (ManiInfo.dt_r18 == null) return;
            this.cdgv_Haikibutu.EnableHeadersVisualStyles = false;
            this.cdgv_UnpanInfo.EnableHeadersVisualStyles = false;
            this.cdgv_LastSBN_Genba_Jiseki.EnableHeadersVisualStyles = false;
            this.cdgv_Bikou.EnableHeadersVisualStyles = false;
            this.cdgv_Tyukanshori.EnableHeadersVisualStyles = false;
            if (SEQManiInfo == null) 
            {
                if (ConstCls.TUUCHICD_DT_R18.Contains(this.Logic.tuuchiCd))
                {
                    #region dr_r18
                    this.lbl_HikiwataDate.BackColor = System.Drawing.Color.Red;
                    this.lbl_HikiwataTantouSha.BackColor = System.Drawing.Color.Red;
                    this.lbl_TourokuTantouSha.BackColor = System.Drawing.Color.Red;
                    this.ctxt_HaisyutuGyousyaName.BackColor = System.Drawing.Color.Red;
                    this.ctxt_HaisyutuGyousyaName.ForeColor = System.Drawing.Color.White;
                    this.cnt_HaisyutuGyousyaZip.BackColor = System.Drawing.Color.Red;
                    this.cnt_HaisyutuGyousyaZip.ForeColor = System.Drawing.Color.White;
                    this.cnt_HaisyutuGyousyaTel.BackColor = System.Drawing.Color.Red;
                    this.cnt_HaisyutuGyousyaTel.ForeColor = System.Drawing.Color.White;
                    this.ctxt_HaisyutuGyousyaAddr.BackColor = System.Drawing.Color.Red;
                    this.ctxt_HaisyutuGyousyaAddr.ForeColor = System.Drawing.Color.White;
                    this.ctxt_HaisyutuGenbaName.BackColor = System.Drawing.Color.Red;
                    this.ctxt_HaisyutuGenbaName.ForeColor = System.Drawing.Color.White;
                    this.ctxt_Haisyutu_GenbaZip.BackColor = System.Drawing.Color.Red;
                    this.ctxt_Haisyutu_GenbaZip.ForeColor = System.Drawing.Color.White;
                    this.cnt_HaisyutuGenbaTel.BackColor = System.Drawing.Color.Red;
                    this.cnt_HaisyutuGenbaTel.ForeColor = System.Drawing.Color.White;
                    this.ctxt_HaisyutuGenbaAddr.BackColor = System.Drawing.Color.Red;
                    this.ctxt_HaisyutuGenbaAddr.ForeColor = System.Drawing.Color.White;
                    this.cdgv_Haikibutu.Columns[0].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[3].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[4].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[10].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[11].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[12].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[13].HeaderCell.Style.BackColor = Color.Red;
                    this.customPanel1.BackColor = Color.Red;
                    this.ccbx_YitakuKeyaku.BackColor = Color.Red;
                    this.ccbx_YitakuKeyaku.ForeColor = System.Drawing.Color.White;
                    this.ccbx_Toulanshitei.BackColor = Color.Red;
                    this.ccbx_Toulanshitei.ForeColor = System.Drawing.Color.White;
                    this.ctxt_SBN_UnpanTantouShaName.BackColor = Color.Red;
                    this.ctxt_SBN_UnpanTantouShaName.ForeColor = System.Drawing.Color.White;
                    this.ctxt_cantxt_SBN_SyaryoNoName.BackColor = Color.Red;
                    this.ctxt_cantxt_SBN_SyaryoNoName.ForeColor = System.Drawing.Color.White;
                    this.ctxt_SBN_HoukokuTantouShaName.BackColor = Color.Red;
                    this.ctxt_SBN_HoukokuTantouShaName.ForeColor = System.Drawing.Color.White;
                    this.cantxt_SBN_houhouCD.BackColor = Color.Red;
                    this.cantxt_SBN_houhouCD.ForeColor = System.Drawing.Color.White;
                    this.ctxt_SBN_houhouName.BackColor = Color.Red;
                    this.ctxt_SBN_houhouName.ForeColor = System.Drawing.Color.White;
                    this.ctxt_SBN_SBNTantouShaName.BackColor = Color.Red;
                    this.ctxt_SBN_SBNTantouShaName.ForeColor = System.Drawing.Color.White;
                    this.cdtp_SBNEndDate.BackColor = Color.Red;
                    this.cdtp_SBNEndDate.ForeColor = System.Drawing.Color.White;
                    this.cdtp_HaikiAcceptDate.BackColor = Color.Red;
                    this.cdtp_HaikiAcceptDate.ForeColor = System.Drawing.Color.White;
                    this.cntxt_Jyunyuryo.BackColor = Color.Red;
                    this.cntxt_Jyunyuryo.ForeColor = System.Drawing.Color.White;
                    this.cantxt_JyunyuryoUnitCD.BackColor = Color.Red;
                    this.cantxt_JyunyuryoUnitCD.ForeColor = System.Drawing.Color.White;
                    this.ctxt_JyunyuryoUnitName.BackColor = Color.Red;
                    this.ctxt_JyunyuryoUnitName.ForeColor = System.Drawing.Color.White;
                    this.ctxt_SBN_Bikou.BackColor = Color.Red;
                    this.ctxt_SBN_Bikou.ForeColor = System.Drawing.Color.White;
                    this.cntxt_HoukokuKBN.BackColor = Color.Red;
                    this.cntxt_HoukokuKBN.ForeColor = System.Drawing.Color.White;
                    #endregion
                }

                if (ConstCls.TUUCHICD_DT_R05.Contains(this.Logic.tuuchiCd))
                {
                    #region dr_r05
                    this.lbl_RenlakuNo1.BackColor = Color.Red;
                    this.lbl_RenlakuNo2.BackColor = Color.Red;
                    this.lbl_RenlakuNo3.BackColor = Color.Red;
                    #endregion
                }

                if (ConstCls.TUUCHICD_DT_R19.Contains(this.Logic.tuuchiCd))
                {
                    #region dt_r19
                    //this.cdgv_UnpanInfo.Columns[3].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_UnpanInfo.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_UnpanInfo.Columns[7].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_UnpanInfo.Columns[9].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_UnpanInfo.Columns[12].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_UnpanInfo.Columns[14].HeaderCell.Style.BackColor = Color.Red;
                    this.cdtp_UnpanEndDate.BackColor = Color.Red;
                    this.cdtp_UnpanEndDate.ForeColor = Color.White;
                    this.ctxt_UnpanTantoushaName.BackColor = Color.Red;
                    this.ctxt_UnpanTantoushaName.ForeColor = Color.White;
                    this.ctxt_UnpanSyaryoName.BackColor = Color.Red;
                    this.ctxt_UnpanSyaryoName.ForeColor = Color.White;
                    this.cntxt_UnpanRyo.BackColor = Color.Red;
                    this.cntxt_UnpanRyo.ForeColor = Color.White;
                    this.cantxt_UnpanRyoUnitCd.BackColor = Color.Red;
                    this.cantxt_UnpanRyoUnitCd.ForeColor = Color.White;
                    this.ctxt_UnpanRyoUnitName.BackColor = Color.Red;
                    this.ctxt_UnpanRyoUnitName.ForeColor = Color.White;
                    this.cntxt_YukabutuRyo.BackColor = Color.Red;
                    this.cntxt_YukabutuRyo.ForeColor = Color.White;
                    this.cantxt_YukabutuRyoUnitCd.BackColor = Color.Red;
                    this.cantxt_YukabutuRyoUnitCd.ForeColor = Color.White;
                    this.cantxt_YukabutuRyoUnitName.BackColor = Color.Red;
                    this.cantxt_YukabutuRyoUnitName.ForeColor = Color.White;
                    this.ctxt_HoukokuTantoushaName.BackColor = Color.Red;
                    this.ctxt_HoukokuTantoushaName.ForeColor = Color.White;
                    this.ctxt_UnpanBikou.BackColor = Color.Red;
                    this.ctxt_UnpanBikou.ForeColor = Color.White;
                    #endregion
                }

                if (ConstCls.TUUCHICD_DT_R13.Contains(this.Logic.tuuchiCd))
                {
                    #region dt_r13
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[6].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[7].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[8].HeaderCell.Style.BackColor = Color.Red;
                    #endregion
                }

                if (ConstCls.TUUCHICD_DT_R06.Contains(this.Logic.tuuchiCd))
                {
                    #region dt_r16
                    this.cdgv_Bikou.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                    #endregion
                }

                if (ConstCls.TUUCHICD_DT_R08.Contains(this.Logic.tuuchiCd))
                {
                    #region dt_r08
                    this.cdgv_Tyukanshori.Columns[0].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[2].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[3].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[4].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[6].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[7].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[8].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[9].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[10].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[11].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[12].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[13].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Tyukanshori.Columns[14].HeaderCell.Style.BackColor = Color.Red;
                    #endregion
                }
                return;
            }

            if (ConstCls.TUUCHICD_DT_R18.Contains(this.Logic.tuuchiCd))
            {
                if (SEQManiInfo.dt_r18 == null)
                {
                    #region dt_r18
                    this.lbl_HikiwataDate.BackColor = System.Drawing.Color.Red;
                    this.lbl_HikiwataTantouSha.BackColor = System.Drawing.Color.Red;
                    this.lbl_TourokuTantouSha.BackColor = System.Drawing.Color.Red;
                    this.ctxt_HaisyutuGyousyaName.BackColor = System.Drawing.Color.Red;
                    this.ctxt_HaisyutuGyousyaName.ForeColor = System.Drawing.Color.White;
                    this.cnt_HaisyutuGyousyaZip.BackColor = System.Drawing.Color.Red;
                    this.cnt_HaisyutuGyousyaZip.ForeColor = System.Drawing.Color.White;
                    this.cnt_HaisyutuGyousyaTel.BackColor = System.Drawing.Color.Red;
                    this.cnt_HaisyutuGyousyaTel.ForeColor = System.Drawing.Color.White;
                    this.ctxt_HaisyutuGyousyaAddr.BackColor = System.Drawing.Color.Red;
                    this.ctxt_HaisyutuGyousyaAddr.ForeColor = System.Drawing.Color.White;
                    this.ctxt_HaisyutuGenbaName.BackColor = System.Drawing.Color.Red;
                    this.ctxt_HaisyutuGenbaName.ForeColor = System.Drawing.Color.White;
                    this.ctxt_Haisyutu_GenbaZip.BackColor = System.Drawing.Color.Red;
                    this.ctxt_Haisyutu_GenbaZip.ForeColor = System.Drawing.Color.White;
                    this.cnt_HaisyutuGenbaTel.BackColor = System.Drawing.Color.Red;
                    this.cnt_HaisyutuGenbaTel.ForeColor = System.Drawing.Color.White;
                    this.ctxt_HaisyutuGenbaAddr.BackColor = System.Drawing.Color.Red;
                    this.ctxt_HaisyutuGenbaAddr.ForeColor = System.Drawing.Color.White;
                    this.cdgv_Haikibutu.Columns[0].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[3].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[4].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[10].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[11].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[12].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_Haikibutu.Columns[13].HeaderCell.Style.BackColor = Color.Red;
                    this.customPanel1.BackColor = Color.Red;
                    this.ccbx_YitakuKeyaku.BackColor = Color.Red;
                    this.ccbx_YitakuKeyaku.ForeColor = System.Drawing.Color.White;
                    this.ccbx_Toulanshitei.BackColor = Color.Red;
                    this.ccbx_Toulanshitei.ForeColor = System.Drawing.Color.White;
                    this.ctxt_SBN_UnpanTantouShaName.BackColor = Color.Red;
                    this.ctxt_SBN_UnpanTantouShaName.ForeColor = System.Drawing.Color.White;
                    this.ctxt_cantxt_SBN_SyaryoNoName.BackColor = Color.Red;
                    this.ctxt_cantxt_SBN_SyaryoNoName.ForeColor = System.Drawing.Color.White;
                    this.ctxt_SBN_HoukokuTantouShaName.BackColor = Color.Red;
                    this.ctxt_SBN_HoukokuTantouShaName.ForeColor = System.Drawing.Color.White;
                    this.cantxt_SBN_houhouCD.BackColor = Color.Red;
                    this.cantxt_SBN_houhouCD.ForeColor = System.Drawing.Color.White;
                    this.ctxt_SBN_houhouName.BackColor = Color.Red;
                    this.ctxt_SBN_houhouName.ForeColor = System.Drawing.Color.White;
                    this.ctxt_SBN_SBNTantouShaName.BackColor = Color.Red;
                    this.ctxt_SBN_SBNTantouShaName.ForeColor = System.Drawing.Color.White;
                    this.cdtp_SBNEndDate.BackColor = Color.Red;
                    this.cdtp_SBNEndDate.ForeColor = System.Drawing.Color.White;
                    this.cdtp_HaikiAcceptDate.BackColor = Color.Red;
                    this.cdtp_HaikiAcceptDate.ForeColor = System.Drawing.Color.White;
                    this.cntxt_Jyunyuryo.BackColor = Color.Red;
                    this.cntxt_Jyunyuryo.ForeColor = System.Drawing.Color.White;
                    this.cantxt_JyunyuryoUnitCD.BackColor = Color.Red;
                    this.cantxt_JyunyuryoUnitCD.ForeColor = System.Drawing.Color.White;
                    this.ctxt_JyunyuryoUnitName.BackColor = Color.Red;
                    this.ctxt_JyunyuryoUnitName.ForeColor = System.Drawing.Color.White;
                    this.ctxt_SBN_Bikou.BackColor = Color.Red;
                    this.ctxt_SBN_Bikou.ForeColor = System.Drawing.Color.White;
                    this.cntxt_HoukokuKBN.BackColor = Color.Red;
                    this.cntxt_HoukokuKBN.ForeColor = System.Drawing.Color.White;
                    #endregion
                }
                else
                {
                    #region dt_r18
                    //引渡日
                    if (!Compare(ManiInfo.dt_r18.HIKIWATASHI_DATE, SEQManiInfo.dt_r18.HIKIWATASHI_DATE))
                    {
                        this.lbl_HikiwataDate.BackColor = System.Drawing.Color.Red;
                    }
                    //引渡担当者
                    if (!Compare(ManiInfo.dt_r18.HIKIWATASHI_TAN_NAME, SEQManiInfo.dt_r18.HIKIWATASHI_TAN_NAME))
                    {
                        this.lbl_HikiwataTantouSha.BackColor = System.Drawing.Color.Red;
                    }
                    //登録担当者
                    if (!Compare(ManiInfo.dt_r18.REGI_TAN, SEQManiInfo.dt_r18.REGI_TAN))
                    {
                        this.lbl_TourokuTantouSha.BackColor = System.Drawing.Color.Red;
                    }
                    //排出事業者-名前
                    if (!Compare(ManiInfo.dt_r18.HST_SHA_NAME, SEQManiInfo.dt_r18.HST_SHA_NAME))
                    {
                        this.ctxt_HaisyutuGyousyaName.BackColor = System.Drawing.Color.Red;
                        this.ctxt_HaisyutuGyousyaName.ForeColor = System.Drawing.Color.White;
                    }
                    //排出事業者-郵便番号
                    if (!Compare(ManiInfo.dt_r18.HST_SHA_POST, SEQManiInfo.dt_r18.HST_SHA_POST))
                    {
                        this.cnt_HaisyutuGyousyaZip.BackColor = System.Drawing.Color.Red;
                        this.cnt_HaisyutuGyousyaZip.ForeColor = System.Drawing.Color.White;
                    }
                    //排出事業者-電話番号
                    if (!Compare(ManiInfo.dt_r18.HST_SHA_TEL, SEQManiInfo.dt_r18.HST_SHA_TEL))
                    {
                        this.cnt_HaisyutuGyousyaTel.BackColor = System.Drawing.Color.Red;
                        this.cnt_HaisyutuGyousyaTel.ForeColor = System.Drawing.Color.White;
                    }
                    //排出事業者-住所
                    if (!Compare(ManiInfo.dt_r18.HST_SHA_ADDRESS1, SEQManiInfo.dt_r18.HST_SHA_ADDRESS1)
                        || !Compare(ManiInfo.dt_r18.HST_SHA_ADDRESS2, SEQManiInfo.dt_r18.HST_SHA_ADDRESS2)
                        || !Compare(ManiInfo.dt_r18.HST_SHA_ADDRESS3, SEQManiInfo.dt_r18.HST_SHA_ADDRESS3)
                        || !Compare(ManiInfo.dt_r18.HST_SHA_ADDRESS4, SEQManiInfo.dt_r18.HST_SHA_ADDRESS4))
                    {
                        this.ctxt_HaisyutuGyousyaAddr.BackColor = System.Drawing.Color.Red;
                        this.ctxt_HaisyutuGyousyaAddr.ForeColor = System.Drawing.Color.White;
                    }
                    //排出事業場-名前
                    if (!Compare(ManiInfo.dt_r18.HST_JOU_NAME, SEQManiInfo.dt_r18.HST_JOU_NAME))
                    {
                        this.ctxt_HaisyutuGenbaName.BackColor = System.Drawing.Color.Red;
                        this.ctxt_HaisyutuGenbaName.ForeColor = System.Drawing.Color.White;
                    }
                    //排出事業場-郵便番号
                    if (!Compare(ManiInfo.dt_r18.HST_JOU_POST_NO, SEQManiInfo.dt_r18.HST_JOU_POST_NO))
                    {
                        this.ctxt_Haisyutu_GenbaZip.BackColor = System.Drawing.Color.Red;
                        this.ctxt_Haisyutu_GenbaZip.ForeColor = System.Drawing.Color.White;
                    }
                    //排出事業場-電話番号
                    if (!Compare(ManiInfo.dt_r18.HST_JOU_TEL, SEQManiInfo.dt_r18.HST_JOU_TEL))
                    {
                        this.cnt_HaisyutuGenbaTel.BackColor = System.Drawing.Color.Red;
                        this.cnt_HaisyutuGenbaTel.ForeColor = System.Drawing.Color.White;
                    }
                    //排出事業場-住所
                    if (!Compare(ManiInfo.dt_r18.HST_JOU_ADDRESS1, SEQManiInfo.dt_r18.HST_JOU_ADDRESS1)
                        || !Compare(ManiInfo.dt_r18.HST_JOU_ADDRESS2, SEQManiInfo.dt_r18.HST_JOU_ADDRESS2)
                        || !Compare(ManiInfo.dt_r18.HST_JOU_ADDRESS3, SEQManiInfo.dt_r18.HST_JOU_ADDRESS3)
                        || !Compare(ManiInfo.dt_r18.HST_JOU_ADDRESS4, SEQManiInfo.dt_r18.HST_JOU_ADDRESS4))
                    {
                        this.ctxt_HaisyutuGenbaAddr.BackColor = System.Drawing.Color.Red;
                        this.ctxt_HaisyutuGenbaAddr.ForeColor = System.Drawing.Color.White;
                    }

                    //廃棄物情報
                    //廃棄物種類CD
                    if (!Compare(ManiInfo.dt_r18.HAIKI_DAI_CODE, SEQManiInfo.dt_r18.HAIKI_DAI_CODE)
                        || !Compare(ManiInfo.dt_r18.HAIKI_CHU_CODE, SEQManiInfo.dt_r18.HAIKI_CHU_CODE)
                        || !Compare(ManiInfo.dt_r18.HAIKI_SHO_CODE, SEQManiInfo.dt_r18.HAIKI_SHO_CODE)
                        || !Compare(ManiInfo.dt_r18.HAIKI_SAI_CODE, SEQManiInfo.dt_r18.HAIKI_SAI_CODE))
                    {
                        this.cdgv_Haikibutu.Columns[0].HeaderCell.Style.BackColor = Color.Red;
                    }
                    //廃棄物種類名
                    if (!Compare(ManiInfo.dt_r18.HAIKI_SHURUI, SEQManiInfo.dt_r18.HAIKI_SHURUI))
                    {
                        this.cdgv_Haikibutu.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                    }
                    //廃棄物名称
                    if (!Compare(ManiInfo.dt_r18.HAIKI_NAME, SEQManiInfo.dt_r18.HAIKI_NAME))
                    {
                        this.cdgv_Haikibutu.Columns[3].HeaderCell.Style.BackColor = Color.Red;
                    }
                    //数量
                    if (!Compare(ManiInfo.dt_r18.HAIKI_SUU, SEQManiInfo.dt_r18.HAIKI_SUU))
                    {
                        this.cdgv_Haikibutu.Columns[4].HeaderCell.Style.BackColor = Color.Red;
                    }
                    //単位CD
                    if (!Compare(ManiInfo.dt_r18.HAIKI_UNIT_CODE, SEQManiInfo.dt_r18.HAIKI_UNIT_CODE))
                    {
                        this.cdgv_Haikibutu.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                        this.cdgv_Haikibutu.Columns[6].HeaderCell.Style.BackColor = Color.Red;
                    }
                    //荷姿CD
                    if (!Compare(ManiInfo.dt_r18.NISUGATA_CODE, SEQManiInfo.dt_r18.NISUGATA_CODE))
                    {
                        this.cdgv_Haikibutu.Columns[10].HeaderCell.Style.BackColor = Color.Red;
                    }
                    //荷姿名
                    if (!Compare(ManiInfo.dt_r18.NISUGATA_NAME, SEQManiInfo.dt_r18.NISUGATA_NAME))
                    {
                        this.cdgv_Haikibutu.Columns[11].HeaderCell.Style.BackColor = Color.Red;
                    }
                    //荷姿数量
                    if (!Compare(ManiInfo.dt_r18.NISUGATA_SUU, SEQManiInfo.dt_r18.NISUGATA_SUU))
                    {
                        this.cdgv_Haikibutu.Columns[12].HeaderCell.Style.BackColor = Color.Red;
                    }
                    //数量確定者CD
                    if (!Compare(ManiInfo.dt_r18.SUU_KAKUTEI_CODE, SEQManiInfo.dt_r18.SUU_KAKUTEI_CODE))
                    {
                        this.cdgv_Haikibutu.Columns[13].HeaderCell.Style.BackColor = Color.Red;
                        this.cdgv_Haikibutu.Columns[14].HeaderCell.Style.BackColor = Color.Red;
                    }
                    //最終処分の場所 （予定）情報
                    if (!Compare(ManiInfo.dt_r18.LAST_SBN_JOU_KISAI_FLAG, SEQManiInfo.dt_r18.LAST_SBN_JOU_KISAI_FLAG))
                    {
                        this.customPanel1.BackColor = Color.Red;
                        this.ccbx_YitakuKeyaku.BackColor = Color.Red;
                        this.ccbx_YitakuKeyaku.ForeColor = System.Drawing.Color.White;
                        this.ccbx_Toulanshitei.BackColor = Color.Red;
                        this.ccbx_Toulanshitei.ForeColor = System.Drawing.Color.White;
                    }
                    //処分報告の運搬担当者名称
                    if (!Compare(ManiInfo.dt_r18.UPN_TAN_NAME, SEQManiInfo.dt_r18.UPN_TAN_NAME))
                    {
                        this.ctxt_SBN_UnpanTantouShaName.BackColor = Color.Red;
                        this.ctxt_SBN_UnpanTantouShaName.ForeColor = System.Drawing.Color.White;
                    }
                    //処分報告の車輌名称
                    if (!Compare(ManiInfo.dt_r18.CAR_NO, SEQManiInfo.dt_r18.CAR_NO))
                    {
                        this.ctxt_cantxt_SBN_SyaryoNoName.BackColor = Color.Red;
                        this.ctxt_cantxt_SBN_SyaryoNoName.ForeColor = System.Drawing.Color.White;
                    }
                    //処分報告担当者名称
                    if (!Compare(ManiInfo.dt_r18.REP_TAN_NAME, SEQManiInfo.dt_r18.REP_TAN_NAME))
                    {
                        this.ctxt_SBN_HoukokuTantouShaName.BackColor = Color.Red;
                        this.ctxt_SBN_HoukokuTantouShaName.ForeColor = System.Drawing.Color.White;
                    }
                    //処分方法CD
                    if (!Compare(ManiInfo.dt_r18.SBN_WAY_CODE, SEQManiInfo.dt_r18.SBN_WAY_CODE))
                    {
                        this.cantxt_SBN_houhouCD.BackColor = Color.Red;
                        this.cantxt_SBN_houhouCD.ForeColor = System.Drawing.Color.White;
                    }
                    //処分方法名称
                    if (!Compare(ManiInfo.dt_r18.SBN_WAY_NAME, SEQManiInfo.dt_r18.SBN_WAY_NAME))
                    {
                        this.ctxt_SBN_houhouName.BackColor = Color.Red;
                        this.ctxt_SBN_houhouName.ForeColor = System.Drawing.Color.White;
                    }
                    //処分担当者名称
                    if (!Compare(ManiInfo.dt_r18.SBN_TAN_NAME, SEQManiInfo.dt_r18.SBN_TAN_NAME))
                    {
                        this.ctxt_SBN_SBNTantouShaName.BackColor = Color.Red;
                        this.ctxt_SBN_SBNTantouShaName.ForeColor = System.Drawing.Color.White;
                    }
                    //処分終了日
                    if (!Compare(ManiInfo.dt_r18.SBN_END_DATE, SEQManiInfo.dt_r18.SBN_END_DATE))
                    {
                        this.cdtp_SBNEndDate.BackColor = Color.Red;
                        this.cdtp_SBNEndDate.ForeColor = System.Drawing.Color.White;
                    }
                    //廃棄物受領日
                    if (!Compare(ManiInfo.dt_r18.HAIKI_IN_DATE, SEQManiInfo.dt_r18.HAIKI_IN_DATE))
                    {
                        this.cdtp_HaikiAcceptDate.BackColor = Color.Red;
                        this.cdtp_HaikiAcceptDate.ForeColor = System.Drawing.Color.White;
                    }
                    //受入量
                    if (!Compare(ManiInfo.dt_r18.RECEPT_SUU, SEQManiInfo.dt_r18.RECEPT_SUU))
                    {
                        this.cntxt_Jyunyuryo.BackColor = Color.Red;
                        this.cntxt_Jyunyuryo.ForeColor = System.Drawing.Color.White;
                    }
                    //受入量の単位CD
                    if (!Compare(ManiInfo.dt_r18.RECEPT_UNIT_CODE, SEQManiInfo.dt_r18.RECEPT_UNIT_CODE))
                    {
                        this.cantxt_JyunyuryoUnitCD.BackColor = Color.Red;
                        this.cantxt_JyunyuryoUnitCD.ForeColor = System.Drawing.Color.White;
                        this.ctxt_JyunyuryoUnitName.BackColor = Color.Red;
                        this.ctxt_JyunyuryoUnitName.ForeColor = System.Drawing.Color.White;
                    }
                    //処分備考
                    if (!Compare(ManiInfo.dt_r18.SBN_REP_BIKOU, SEQManiInfo.dt_r18.SBN_REP_BIKOU))
                    {
                        this.ctxt_SBN_Bikou.BackColor = Color.Red;
                        this.ctxt_SBN_Bikou.ForeColor = System.Drawing.Color.White;
                    }
                    //報告区分
                    if (!Compare(ManiInfo.dt_r18.SBN_ENDREP_KBN, SEQManiInfo.dt_r18.SBN_ENDREP_KBN))
                    {
                        this.cntxt_HoukokuKBN.BackColor = Color.Red;
                        this.cntxt_HoukokuKBN.ForeColor = System.Drawing.Color.White;
                    }
                    #endregion
                }
            }

            if (ConstCls.TUUCHICD_DT_R05.Contains(this.Logic.tuuchiCd))
            {
                #region lstDT_R05
                //連絡番号の設定
                if (ManiInfo.lstDT_R05.Count == SEQManiInfo.lstDT_R05.Count)
                {
                    for (int i = 0; i < ManiInfo.lstDT_R05.Count; i++)
                    {
                        if (!Compare(ManiInfo.lstDT_R05[i].RENRAKU_ID, SEQManiInfo.lstDT_R05[i].RENRAKU_ID))
                        {
                            if (ManiInfo.lstDT_R05[i].RENRAKU_ID_NO == 1M)
                            {
                                //連絡番号１
                                this.lbl_RenlakuNo1.BackColor = Color.Red;
                            }
                            else if (ManiInfo.lstDT_R05[i].RENRAKU_ID_NO == 2M)
                            {
                                //連絡番号２
                                this.lbl_RenlakuNo2.BackColor = Color.Red;
                            }
                            else if (ManiInfo.lstDT_R05[i].RENRAKU_ID_NO == 3M)
                            {
                                //連絡番号３
                                this.lbl_RenlakuNo3.BackColor = Color.Red;
                            }
                        }
                    }
                }
                else
                {
                    Dictionary<SqlDecimal, string> maniInfo = new Dictionary<SqlDecimal, string>();
                    Dictionary<SqlDecimal, string> SeqManiInfo = new Dictionary<SqlDecimal, string>();
                    if (ManiInfo.lstDT_R05.Count > 0)
                    {
                        for (int i = 0; i < ManiInfo.lstDT_R05.Count; i++)
                        {
                            maniInfo.Add(ManiInfo.lstDT_R05[i].RENRAKU_ID_NO, ManiInfo.lstDT_R05[i].RENRAKU_ID);
                        }
                    }

                    if (SEQManiInfo.lstDT_R05.Count > 0)
                    {
                        for (int i = 0; i < SEQManiInfo.lstDT_R05.Count; i++)
                        {
                            SeqManiInfo.Add(SEQManiInfo.lstDT_R05[i].RENRAKU_ID_NO, SEQManiInfo.lstDT_R05[i].RENRAKU_ID);
                        }
                    }

                    if (maniInfo == null || maniInfo.Count < 1)
                    {
                        if (SeqManiInfo != null)
                        {
                            foreach (var info in SeqManiInfo)
                            {
                                listDT_R05_BackChange(info.Key);
                            }
                        }
                    }
                    else if (SeqManiInfo == null || SeqManiInfo.Count < 1)
                    {
                        if (maniInfo != null)
                        {
                            foreach (var info in maniInfo)
                            {
                                listDT_R05_BackChange(info.Key);
                            }
                        }
                    }
                    else
                    {
                        if (maniInfo != null && SeqManiInfo != null)
                        {
                            foreach (var info in maniInfo)
                            {
                                if (SeqManiInfo.ContainsKey(info.Key))
                                {
                                    if (!SeqManiInfo[info.Key].Equals(info.Value))
                                    {
                                        listDT_R05_BackChange(info.Key);
                                    }
                                }
                                else
                                {
                                    listDT_R05_BackChange(info.Key);
                                }
                            }

                            foreach (var SeqInfo in SeqManiInfo)
                            {
                                if (!maniInfo.ContainsKey(SeqInfo.Key))
                                {
                                    listDT_R05_BackChange(SeqInfo.Key);
                                }
                            }
                        }
                    }
                }
                #endregion
            }

            if (ConstCls.TUUCHICD_DT_R19.Contains(this.Logic.tuuchiCd))
            {
                #region lstDT_R19
                if (ManiInfo.lstDT_R19.Count == SEQManiInfo.lstDT_R19.Count)
                {
                    for (int i = 0; i < ManiInfo.lstDT_R19.Count; i++)
                    {
                        //運搬先業者加入者番号
                        //if (ManiInfo.lstDT_R19[i].UPNSAKI_EDI_MEMBER_ID != SEQManiInfo.lstDT_R19[i].UPNSAKI_EDI_MEMBER_ID)
                        //{
                        //    this.cdgv_UnpanInfo.Columns[3].HeaderCell.Style.BackColor = Color.Red;
                        //}
                        //運搬先業者名称
                        if (!Compare(ManiInfo.lstDT_R19[i].UPNSAKI_NAME, SEQManiInfo.lstDT_R19[i].UPNSAKI_NAME))
                        {
                            this.cdgv_UnpanInfo.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                        }
                        //運搬先事業場名称
                        if (!Compare(ManiInfo.lstDT_R19[i].UPNSAKI_JOU_NAME, SEQManiInfo.lstDT_R19[i].UPNSAKI_JOU_NAME))
                        {
                            this.cdgv_UnpanInfo.Columns[7].HeaderCell.Style.BackColor = Color.Red;
                        }
                        //運搬方法
                        if (!Compare(ManiInfo.lstDT_R19[i].UPN_WAY_CODE, SEQManiInfo.lstDT_R19[i].UPN_WAY_CODE))
                        {
                            this.cdgv_UnpanInfo.Columns[9].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_UnpanInfo.Columns[10].HeaderCell.Style.BackColor = Color.Red;
                        }
                        //運搬担当者名称
                        if (!Compare(ManiInfo.lstDT_R19[i].UPN_TAN_NAME, SEQManiInfo.lstDT_R19[i].UPN_TAN_NAME))
                        {
                            this.cdgv_UnpanInfo.Columns[12].HeaderCell.Style.BackColor = Color.Red;
                        }
                        //車輌番号名称 
                        if (!Compare(ManiInfo.lstDT_R19[i].CAR_NO, SEQManiInfo.lstDT_R19[i].CAR_NO))
                        {
                            this.cdgv_UnpanInfo.Columns[14].HeaderCell.Style.BackColor = Color.Red;
                        }
                        //運搬終了日
                        if (!Compare(ManiInfo.lstDT_R19[i].UPN_END_DATE, SEQManiInfo.lstDT_R19[i].UPN_END_DATE))
                        {
                            this.cdtp_UnpanEndDate.BackColor = Color.Red;
                            this.cdtp_UnpanEndDate.ForeColor = Color.White;
                        }
                        //運搬報告記載運搬担当者名称
                        if (!Compare(ManiInfo.lstDT_R19[i].UPNREP_UPN_TAN_NAME, SEQManiInfo.lstDT_R19[i].UPNREP_UPN_TAN_NAME))
                        {
                            this.ctxt_UnpanTantoushaName.BackColor = Color.Red;
                            this.ctxt_UnpanTantoushaName.ForeColor = Color.White;
                        }
                        //運搬車輛名
                        if (!Compare(ManiInfo.lstDT_R19[i].UPNREP_CAR_NO, SEQManiInfo.lstDT_R19[i].UPNREP_CAR_NO))
                        {
                            this.ctxt_UnpanSyaryoName.BackColor = Color.Red;
                            this.ctxt_UnpanSyaryoName.ForeColor = Color.White;
                        }
                        //運搬量
                        if (!Compare(ManiInfo.lstDT_R19[i].UPN_SUU, SEQManiInfo.lstDT_R19[i].UPN_SUU))
                        {
                            this.cntxt_UnpanRyo.BackColor = Color.Red;
                            this.cntxt_UnpanRyo.ForeColor = Color.White;
                        }
                        //運搬量の単位CD
                        if (!Compare(ManiInfo.lstDT_R19[i].UPN_UNIT_CODE, SEQManiInfo.lstDT_R19[i].UPN_UNIT_CODE))
                        {
                            this.cantxt_UnpanRyoUnitCd.BackColor = Color.Red;
                            this.cantxt_UnpanRyoUnitCd.ForeColor = Color.White;
                            this.ctxt_UnpanRyoUnitName.BackColor = Color.Red;
                            this.ctxt_UnpanRyoUnitName.ForeColor = Color.White;
                        }
                        //有価物収集量
                        if (!Compare(ManiInfo.lstDT_R19[i].YUUKA_SUU, SEQManiInfo.lstDT_R19[i].YUUKA_SUU))
                        {
                            this.cntxt_YukabutuRyo.BackColor = Color.Red;
                            this.cntxt_YukabutuRyo.ForeColor = Color.White;
                        }
                        //有価物の単位CD
                        if (!Compare(ManiInfo.lstDT_R19[i].YUUKA_UNIT_CODE, SEQManiInfo.lstDT_R19[i].YUUKA_UNIT_CODE))
                        {
                            this.cantxt_YukabutuRyoUnitCd.BackColor = Color.Red;
                            this.cantxt_YukabutuRyoUnitCd.ForeColor = Color.White;
                            this.cantxt_YukabutuRyoUnitName.BackColor = Color.Red;
                            this.cantxt_YukabutuRyoUnitName.ForeColor = Color.White;
                        }
                        //運搬報告記載報告担当者名称
                        if (!Compare(ManiInfo.lstDT_R19[i].REP_TAN_NAME, SEQManiInfo.lstDT_R19[i].REP_TAN_NAME))
                        {
                            this.ctxt_HoukokuTantoushaName.BackColor = Color.Red;
                            this.ctxt_HoukokuTantoushaName.ForeColor = Color.White;
                        }
                        //備考
                        if (!Compare(ManiInfo.lstDT_R19[i].BIKOU, SEQManiInfo.lstDT_R19[i].BIKOU))
                        {
                            this.ctxt_UnpanBikou.BackColor = Color.Red;
                            this.ctxt_UnpanBikou.ForeColor = Color.White;
                        }
                    }
                }
                else
                {
                    //this.cdgv_UnpanInfo.Columns[3].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_UnpanInfo.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_UnpanInfo.Columns[7].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_UnpanInfo.Columns[9].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_UnpanInfo.Columns[12].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_UnpanInfo.Columns[14].HeaderCell.Style.BackColor = Color.Red;
                    this.cdtp_UnpanEndDate.BackColor = Color.Red;
                    this.cdtp_UnpanEndDate.ForeColor = Color.White;
                    this.ctxt_UnpanTantoushaName.BackColor = Color.Red;
                    this.ctxt_UnpanTantoushaName.ForeColor = Color.White;
                    this.ctxt_UnpanSyaryoName.BackColor = Color.Red;
                    this.ctxt_UnpanSyaryoName.ForeColor = Color.White;
                    this.cntxt_UnpanRyo.BackColor = Color.Red;
                    this.cntxt_UnpanRyo.ForeColor = Color.White;
                    this.cantxt_UnpanRyoUnitCd.BackColor = Color.Red;
                    this.cantxt_UnpanRyoUnitCd.ForeColor = Color.White;
                    this.ctxt_UnpanRyoUnitName.BackColor = Color.Red;
                    this.ctxt_UnpanRyoUnitName.ForeColor = Color.White;
                    this.cntxt_YukabutuRyo.BackColor = Color.Red;
                    this.cntxt_YukabutuRyo.ForeColor = Color.White;
                    this.cantxt_YukabutuRyoUnitCd.BackColor = Color.Red;
                    this.cantxt_YukabutuRyoUnitCd.ForeColor = Color.White;
                    this.cantxt_YukabutuRyoUnitName.BackColor = Color.Red;
                    this.cantxt_YukabutuRyoUnitName.ForeColor = Color.White;
                    this.ctxt_HoukokuTantoushaName.BackColor = Color.Red;
                    this.ctxt_HoukokuTantoushaName.ForeColor = Color.White;
                    this.ctxt_UnpanBikou.BackColor = Color.Red;
                    this.ctxt_UnpanBikou.ForeColor = Color.White;
                }

                //最終運搬区間の運搬先事業場情報より、処分事業場情報を設定する
                if (ManiInfo.lstDT_R19.Count > 0)
                {
                    if (ManiInfo.lstDT_R19.Count == SEQManiInfo.lstDT_R19.Count)
                    {
                        int length = ManiInfo.lstDT_R19.Count - 1;
                        //処分事業場名前
                        if (!Compare(ManiInfo.lstDT_R19[length].UPNSAKI_JOU_NAME, SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_NAME))
                        {
                            this.ctxt_SBN_Genba_Name.BackColor = Color.Red;
                            this.ctxt_SBN_Genba_Name.ForeColor = System.Drawing.Color.White;
                        }
                        //処分事業場-郵便番号
                        if (!Compare(ManiInfo.lstDT_R19[length].UPNSAKI_JOU_POST, SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_POST))
                        {
                            this.ctxt_SBN_GenbaPost.BackColor = Color.Red;
                            this.ctxt_SBN_GenbaPost.ForeColor = System.Drawing.Color.White;
                        }
                        //処分事業場-電話番号
                        if (!Compare(ManiInfo.lstDT_R19[length].UPNSAKI_JOU_TEL, SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_TEL))
                        {
                            this.ctxt_SBN_GenbaTel.BackColor = Color.Red;
                            this.ctxt_SBN_GenbaTel.ForeColor = System.Drawing.Color.White;
                        }
                        //処分事業場-住所
                        if (!Compare(ManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS1, SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS1)
                            || !Compare(ManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS2, SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS2)
                            || !Compare(ManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS3, SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS3)
                            || !Compare(ManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS4, SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS4))
                        {
                            this.SBN_GenbaAddr.BackColor = Color.Red;
                            this.SBN_GenbaAddr.ForeColor = System.Drawing.Color.White;
                        }
                        //事業場CD
                        if (!string.IsNullOrEmpty(this.cantxt_SBN_Genba_CD.Text))
                        {
                            // 画面
                            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                            dto.EDI_MEMBER_ID = ctxt_SBN_KanyuShaNo.Text;
                            dto.GYOUSHA_CD = this.cantxt_SBN_JyutakuShaCD.Text;
                            dto.GENBA_CD = this.cantxt_SBN_Genba_CD.Text;
                            dto.SBN_KBN = "1";
                            dto.JIGYOUJOU_NAME = this.ctxt_SBN_Genba_Name.Text;
                            dto.JIGYOUJOU_ADDRESS = this.SBN_GenbaAddr.Text;
                            dto.JIGYOUJOU_FLG = "1";
                            dto.ISNOT_NEED_DELETE_FLG = true;
                            DataTable dt = this.MasterLogic.GetDenshiGenbaData(dto);
                            // 比較
                            DenshiSearchParameterDtoCls dto2 = new DenshiSearchParameterDtoCls();
                            if ((ManiInfo.dt_r18.SBN_SHA_MEMBER_ID == ConstCls.NO_REP_SBN_EDI_MEMBER_ID)
                                && (false == string.IsNullOrEmpty((ManiInfo.dt_r18ExOld == null) ? string.Empty : ManiInfo.dt_r18ExOld.NO_REP_SBN_EDI_MEMBER_ID)))
                            {
                                dto2.EDI_MEMBER_ID = ctxt_SBN_KanyuShaNo.Text;
                            }
                            else
                            {
                                dto2.EDI_MEMBER_ID = SEQManiInfo.dt_r18.SBN_SHA_MEMBER_ID;
                            }
                            dto2.GYOUSHA_CD = this.cantxt_SBN_JyutakuShaCD.Text;
                            dto2.GENBA_CD = this.cantxt_SBN_Genba_CD.Text;
                            dto2.SBN_KBN = "1";
                            dto2.JIGYOUJOU_NAME = SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_NAME;
                            dto2.JIGYOUJOU_ADDRESS = SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS1 +
                                                        SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS2 +
                                                        SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS3 +
                                                        SEQManiInfo.lstDT_R19[length].UPNSAKI_JOU_ADDRESS4;
                            dto2.JIGYOUJOU_FLG = "1";
                            dto2.ISNOT_NEED_DELETE_FLG = true;
                            DataTable dt2 = this.MasterLogic.GetDenshiGenbaData(dto2);

                            if (dt.Rows.Count == 1)
                            {
                                if (dt.Rows.Count == dt2.Rows.Count)
                                {
                                    if (!Compare(dt.Rows[0]["JIGYOUJOU_CD"], dt2.Rows[0]["JIGYOUJOU_CD"]))
                                    {
                                        this.ctxt_SBN_JIGYOUJYOU_CD.BackColor = Color.Red;
                                        this.ctxt_SBN_JIGYOUJYOU_CD.ForeColor = System.Drawing.Color.White;
                                    }
                                }
                                else
                                {
                                    this.ctxt_SBN_JIGYOUJYOU_CD.BackColor = Color.Red;
                                    this.ctxt_SBN_JIGYOUJYOU_CD.ForeColor = System.Drawing.Color.White;
                                }
                            }
                            else if (!ManiInfo.dt_r18.UPN_ROUTE_CNT.IsNull
                                && ManiInfo.dt_r18.UPN_ROUTE_CNT == ManiInfo.lstDT_R19.Count
                                && !ManiInfo.lstDT_R19[(int)ManiInfo.dt_r18.UPN_ROUTE_CNT - 1].UPNSAKI_JOU_ID.IsNull)
                            {
                                if (ManiInfo.lstDT_R19[(int)ManiInfo.dt_r18.UPN_ROUTE_CNT - 1].UPNSAKI_JOU_ID.ToString().PadLeft(10, '0')
                                     != SEQManiInfo.lstDT_R19[(int)SEQManiInfo.dt_r18.UPN_ROUTE_CNT - 1].UPNSAKI_JOU_ID.ToString().PadLeft(10, '0'))
                                {
                                    this.ctxt_SBN_JIGYOUJYOU_CD.BackColor = Color.Red;
                                    this.ctxt_SBN_JIGYOUJYOU_CD.ForeColor = System.Drawing.Color.White;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ctxt_SBN_Genba_Name.BackColor = Color.Red;
                        this.ctxt_SBN_Genba_Name.ForeColor = System.Drawing.Color.White;
                        this.ctxt_SBN_GenbaPost.BackColor = Color.Red;
                        this.ctxt_SBN_GenbaPost.ForeColor = System.Drawing.Color.White;
                        this.ctxt_SBN_GenbaTel.BackColor = Color.Red;
                        this.ctxt_SBN_GenbaTel.ForeColor = System.Drawing.Color.White;
                        this.SBN_GenbaAddr.BackColor = Color.Red;
                        this.SBN_GenbaAddr.ForeColor = System.Drawing.Color.White;
                        this.ctxt_SBN_JIGYOUJYOU_CD.BackColor = Color.Red;
                        this.ctxt_SBN_JIGYOUJYOU_CD.ForeColor = System.Drawing.Color.White;
                    }
                }
                #endregion
            }

            if (ConstCls.TUUCHICD_DT_R13.Contains(this.Logic.tuuchiCd))
            {
                #region lstDT_R13
                if (ManiInfo.lstDT_R13.Count == SEQManiInfo.lstDT_R13.Count)
                {
                    for (int i = 0; i < ManiInfo.lstDT_R13.Count; i++)
                    {
                        //最終処分終了日
                        if (!Compare(ManiInfo.lstDT_R13[i].LAST_SBN_END_DATE, SEQManiInfo.lstDT_R13[i].LAST_SBN_END_DATE))
                        {
                            this.cdgv_LastSBN_Genba_Jiseki.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                        }
                        //最終処分業場名称
                        if (!Compare(ManiInfo.lstDT_R13[i].LAST_SBN_JOU_NAME, SEQManiInfo.lstDT_R13[i].LAST_SBN_JOU_NAME))
                        {
                            this.cdgv_LastSBN_Genba_Jiseki.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                        }
                        //最終処分業場の郵便番号
                        if (!Compare(ManiInfo.lstDT_R13[i].LAST_SBN_JOU_POST, SEQManiInfo.lstDT_R13[i].LAST_SBN_JOU_POST))
                        {
                            this.cdgv_LastSBN_Genba_Jiseki.Columns[6].HeaderCell.Style.BackColor = Color.Red;
                        }
                        //最終処分業場の住所
                        if (!Compare(ManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS1, SEQManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS1)
                            || !Compare(ManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS2, SEQManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS2)
                            || !Compare(ManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS3, SEQManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS3)
                            || !Compare(ManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS4, SEQManiInfo.lstDT_R13[i].LAST_SBN_JOU_ADDRESS4))
                        {
                            this.cdgv_LastSBN_Genba_Jiseki.Columns[7].HeaderCell.Style.BackColor = Color.Red;
                        }
                        //最終処分業場の電話番号
                        if (!Compare(ManiInfo.lstDT_R13[i].LAST_SBN_JOU_TEL, SEQManiInfo.lstDT_R13[i].LAST_SBN_JOU_TEL))
                        {
                            this.cdgv_LastSBN_Genba_Jiseki.Columns[8].HeaderCell.Style.BackColor = Color.Red;
                        }
                    }
                }
                else
                {
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[6].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[7].HeaderCell.Style.BackColor = Color.Red;
                    this.cdgv_LastSBN_Genba_Jiseki.Columns[8].HeaderCell.Style.BackColor = Color.Red;
                }
                #endregion
            }

            if (ConstCls.TUUCHICD_DT_R06.Contains(this.Logic.tuuchiCd))
            {
                #region lstDT_R06
                if (ManiInfo.lstDT_R06.Count == SEQManiInfo.lstDT_R06.Count)
                {
                    for (int i = 0; i < ManiInfo.lstDT_R06.Count; i++)
                    {
                        if (!Compare(ManiInfo.lstDT_R06[i].BIKOU, SEQManiInfo.lstDT_R06[i].BIKOU))
                        {
                            this.cdgv_Bikou.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Bikou.Rows[i].Cells[0].Style.BackColor = Color.Red;
                            this.cdgv_Bikou.Rows[i].Cells[0].Style.ForeColor = Color.White;
                            this.cdgv_Bikou.Rows[i].Cells[1].Style.BackColor = Color.Red;
                            this.cdgv_Bikou.Rows[i].Cells[1].Style.ForeColor = Color.White;
                        }
                    }
                }
                else
                {
                    this.cdgv_Bikou.Columns[1].HeaderCell.Style.BackColor = Color.Red;

                    Dictionary<SqlDecimal, string> maniInfo = new Dictionary<SqlDecimal, string>();
                    Dictionary<SqlDecimal, string> SeqManiInfo = new Dictionary<SqlDecimal, string>();
                    if (ManiInfo.lstDT_R06.Count > 0)
                    {
                        for (int i = 0; i < ManiInfo.lstDT_R06.Count; i++)
                        {
                            maniInfo.Add(ManiInfo.lstDT_R06[i].BIKOU_NO, ManiInfo.lstDT_R06[i].BIKOU);
                        }
                    }

                    if (SEQManiInfo.lstDT_R06.Count > 0)
                    {
                        for (int i = 0; i < SEQManiInfo.lstDT_R06.Count; i++)
                        {
                            SeqManiInfo.Add(SEQManiInfo.lstDT_R06[i].BIKOU_NO, SEQManiInfo.lstDT_R06[i].BIKOU);
                        }
                    }

                    if (maniInfo == null || maniInfo.Count < 1)
                    {
                        if (SeqManiInfo != null)
                        {
                            foreach (var info in SeqManiInfo)
                            {
                                listDT_R06_BackChange(info.Key);
                            }
                        }
                    }
                    else if (SeqManiInfo == null || SeqManiInfo.Count < 1)
                    {
                        if (maniInfo != null)
                        {
                            foreach (var info in maniInfo)
                            {
                                listDT_R06_BackChange(info.Key);
                            }
                        }
                    }
                    else
                    {
                        if (maniInfo != null && SeqManiInfo != null)
                        {
                            foreach (var info in maniInfo)
                            {
                                if (SeqManiInfo.ContainsKey(info.Key))
                                {
                                    if (!SeqManiInfo[info.Key].Equals(info.Value))
                                    {
                                        listDT_R06_BackChange(info.Key);
                                    }
                                }
                                else
                                {
                                    listDT_R06_BackChange(info.Key);
                                }
                            }

                            foreach (var SeqInfo in SeqManiInfo)
                            {
                                if (!maniInfo.ContainsKey(SeqInfo.Key))
                                {
                                    listDT_R06_BackChange(SeqInfo.Key);
                                }
                            }
                        }
                    }
                }
                #endregion
            }

            if (ConstCls.TUUCHICD_DT_R08.Contains(this.Logic.tuuchiCd))
            {
                #region lstDT_R08
                if (ManiInfo.lstDT_R08.Count == SEQManiInfo.lstDT_R08.Count)
                {
                    for (int i = 0; i < ManiInfo.lstDT_R08.Count; i++)
                    {
                        //1次交付番号／マニフェスト番号
                        if (!Compare(ManiInfo.lstDT_R08[i].FIRST_MANIFEST_ID, SEQManiInfo.lstDT_R08[i].FIRST_MANIFEST_ID))
                        {
                            this.cdgv_Tyukanshori.Columns[0].HeaderCell.Style.BackColor = Color.Red;
                        }

                        SearchMasterDataDTOCls search = new SearchMasterDataDTOCls();
                        search.MANIFEST_ID = ManiInfo.lstDT_R08[i].FIRST_MANIFEST_ID;
                        DataTable FirstManifestInfo = this.Logic.FirstManifestInfoDao.GetFirstManifestInfo(search);
                        search.MANIFEST_ID = SEQManiInfo.lstDT_R08[i].FIRST_MANIFEST_ID;
                        DataTable SEQFirstManifestInfo = this.Logic.FirstManifestInfoDao.GetFirstManifestInfo(search);

                        if (FirstManifestInfo.Rows.Count == SEQFirstManifestInfo.Rows.Count)
                        {
                            if (FirstManifestInfo.Rows.Count > 0 && SEQFirstManifestInfo.Rows.Count > 0)
                            {
                                //連絡番号１の設定
                                if (!Compare(FirstManifestInfo.Rows[0]["RENRAKU_ID1"], SEQFirstManifestInfo.Rows[0]["RENRAKU_ID1"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //連絡番号２の設定
                                if (!Compare(FirstManifestInfo.Rows[0]["RENRAKU_ID2"], SEQFirstManifestInfo.Rows[0]["RENRAKU_ID2"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[2].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //連絡番号３の設定
                                if (!Compare(FirstManifestInfo.Rows[0]["RENRAKU_ID3"], SEQFirstManifestInfo.Rows[0]["RENRAKU_ID3"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[3].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //排出事業者CD
                                if (!Compare(FirstManifestInfo.Rows[0]["HST_GYOUSHA_CD"], SEQFirstManifestInfo.Rows[0]["HST_GYOUSHA_CD"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[4].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //排出事業者名称
                                if (!Compare(FirstManifestInfo.Rows[0]["HST_GYOUSHA_NAME"], SEQFirstManifestInfo.Rows[0]["HST_GYOUSHA_NAME"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //排出事業場CD
                                if (!Compare(FirstManifestInfo.Rows[0]["HST_GENBA_CD"], SEQFirstManifestInfo.Rows[0]["HST_GENBA_CD"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[6].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //排出事業場名称
                                if (!Compare(FirstManifestInfo.Rows[0]["HST_GENBA_NAME"], SEQFirstManifestInfo.Rows[0]["HST_GENBA_NAME"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[7].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //引渡日／交付年月日
                                if (!Compare(FirstManifestInfo.Rows[0]["KOUFU_DATE"], SEQFirstManifestInfo.Rows[0]["KOUFU_DATE"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[8].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //処分終了日
                                if (!Compare(FirstManifestInfo.Rows[0]["SBN_END_DATE"], SEQFirstManifestInfo.Rows[0]["SBN_END_DATE"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[9].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //廃棄物種類CD
                                if (!Compare(FirstManifestInfo.Rows[0]["HAIKI_SHURUI_CD"], SEQFirstManifestInfo.Rows[0]["HAIKI_SHURUI_CD"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[10].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //廃棄物種類名
                                if (!Compare(FirstManifestInfo.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"], SEQFirstManifestInfo.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[11].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //数量
                                if (!Compare(FirstManifestInfo.Rows[0]["HAIKI_SUU"], SEQFirstManifestInfo.Rows[0]["HAIKI_SUU"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[12].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //単位コード
                                if (!Compare(FirstManifestInfo.Rows[0]["HAIKI_UNIT_CD"], SEQFirstManifestInfo.Rows[0]["HAIKI_UNIT_CD"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[13].HeaderCell.Style.BackColor = Color.Red;
                                }
                                //単位名
                                if (!Compare(FirstManifestInfo.Rows[0]["UNIT_NAME_RYAKU"], SEQFirstManifestInfo.Rows[0]["UNIT_NAME_RYAKU"]))
                                {
                                    this.cdgv_Tyukanshori.Columns[14].HeaderCell.Style.BackColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            this.cdgv_Tyukanshori.Columns[1].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[2].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[3].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[4].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[5].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[6].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[7].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[8].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[9].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[10].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[11].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[12].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[13].HeaderCell.Style.BackColor = Color.Red;
                            this.cdgv_Tyukanshori.Columns[14].HeaderCell.Style.BackColor = Color.Red;
                        }
                    }
                }
                #endregion
            }

            if (ConstCls.TUUCHICD_DT_R02.Contains(this.Logic.tuuchiCd))
            {
                #region lstDT_R02
                if (ManiInfo.lstDT_R02.Count == SEQManiInfo.lstDT_R02.Count)
                {
                    for (int i = 0; i < ManiInfo.lstDT_R02.Count; i++)
                    {
                        if (ManiInfo.lstDT_R02[i].REC_SEQ == SEQManiInfo.lstDT_R02[i].REC_SEQ)
                        {
                            if (!Compare(ManiInfo.lstDT_R02[i].YUUGAI_CODE, SEQManiInfo.lstDT_R02[i].YUUGAI_CODE))
                            {
                                if (ManiInfo.lstDT_R02[i].REC_SEQ == 1)
                                {
                                    this.cdgv_Haikibutu.Columns[13].HeaderCell.Style.BackColor = Color.Red;
                                    this.cdgv_Haikibutu.Columns[14].HeaderCell.Style.BackColor = Color.Red;
                                }
                                else if (ManiInfo.lstDT_R02[i].REC_SEQ == 2)
                                {
                                    this.cdgv_Haikibutu.Columns[15].HeaderCell.Style.BackColor = Color.Red;
                                    this.cdgv_Haikibutu.Columns[16].HeaderCell.Style.BackColor = Color.Red;
                                }
                                else if (ManiInfo.lstDT_R02[i].REC_SEQ == 3)
                                {
                                    this.cdgv_Haikibutu.Columns[17].HeaderCell.Style.BackColor = Color.Red;
                                    this.cdgv_Haikibutu.Columns[18].HeaderCell.Style.BackColor = Color.Red;
                                }
                                else if (ManiInfo.lstDT_R02[i].REC_SEQ == 4)
                                {
                                    this.cdgv_Haikibutu.Columns[19].HeaderCell.Style.BackColor = Color.Red;
                                    this.cdgv_Haikibutu.Columns[20].HeaderCell.Style.BackColor = Color.Red;
                                }
                                else if (ManiInfo.lstDT_R02[i].REC_SEQ == 5)
                                {
                                    this.cdgv_Haikibutu.Columns[21].HeaderCell.Style.BackColor = Color.Red;
                                    this.cdgv_Haikibutu.Columns[22].HeaderCell.Style.BackColor = Color.Red;
                                }
                                else if (ManiInfo.lstDT_R02[i].REC_SEQ == 6)
                                {
                                    this.cdgv_Haikibutu.Columns[23].HeaderCell.Style.BackColor = Color.Red;
                                    this.cdgv_Haikibutu.Columns[24].HeaderCell.Style.BackColor = Color.Red;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Dictionary<SqlDecimal, string> maniInfo = new Dictionary<SqlDecimal, string>();
                    Dictionary<SqlDecimal, string> SeqManiInfo = new Dictionary<SqlDecimal, string>();
                    if (ManiInfo.lstDT_R02.Count > 0)
                    {
                        for (int i = 0; i < ManiInfo.lstDT_R02.Count; i++)
                        {
                            maniInfo.Add(ManiInfo.lstDT_R02[i].REC_SEQ, ManiInfo.lstDT_R02[i].YUUGAI_CODE);
                        }
                    }

                    if (SEQManiInfo.lstDT_R02.Count > 0)
                    {
                        for (int i = 0; i < SEQManiInfo.lstDT_R02.Count; i++)
                        {
                            SeqManiInfo.Add(SEQManiInfo.lstDT_R02[i].REC_SEQ, SEQManiInfo.lstDT_R02[i].YUUGAI_CODE);
                        }
                    }

                    if (maniInfo == null || maniInfo.Count < 1)
                    {
                        if (SeqManiInfo != null)
                        {
                            foreach (var info in SeqManiInfo)
                            {
                                listDT_R02_BackChange(info.Key);
                            }
                        }
                    }
                    else if (SeqManiInfo == null || SeqManiInfo.Count < 1)
                    {
                        if (maniInfo != null)
                        {
                            foreach (var info in maniInfo)
                            {
                                listDT_R02_BackChange(info.Key);
                            }
                        }
                    }
                    else
                    {
                        if (maniInfo != null && SeqManiInfo != null)
                        {
                            foreach (var info in maniInfo)
                            {
                                if (SeqManiInfo.ContainsKey(info.Key))
                                {
                                    if (!SeqManiInfo[info.Key].Equals(info.Value))
                                    {
                                        listDT_R02_BackChange(info.Key);
                                    }
                                }
                                else
                                {
                                    listDT_R02_BackChange(info.Key);
                                }
                            }

                            foreach (var SeqInfo in SeqManiInfo)
                            {
                                if (!maniInfo.ContainsKey(SeqInfo.Key))
                                {
                                    listDT_R02_BackChange(SeqInfo.Key);
                                }
                            }
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 連絡番号の変更
        /// </summary>
        /// <param name="infoKey"></param>
        private void listDT_R05_BackChange(SqlDecimal infoKey)
        {
            if (infoKey == 1M)
            {
                //連絡番号１
                this.lbl_RenlakuNo1.BackColor = Color.Red;
            }
            else if (infoKey == 2M)
            {
                //連絡番号２
                this.lbl_RenlakuNo2.BackColor = Color.Red;
            }
            else if (infoKey == 3M)
            {
                //連絡番号３
                this.lbl_RenlakuNo3.BackColor = Color.Red;
            }
        }

        /// <summary>
        /// 備考の変更
        /// </summary>
        /// <param name="infoKey"></param>
        private void listDT_R06_BackChange(SqlDecimal infoKey)
        {
            this.cdgv_Bikou.Rows[(int)infoKey - 1].Cells[0].Style.BackColor = Color.Red;
            this.cdgv_Bikou.Rows[(int)infoKey - 1].Cells[0].Style.ForeColor = Color.White;
            this.cdgv_Bikou.Rows[(int)infoKey - 1].Cells[1].Style.BackColor = Color.Red;
            this.cdgv_Bikou.Rows[(int)infoKey - 1].Cells[1].Style.ForeColor = Color.White;
        }

        /// <summary>
        /// 有害物質の変更
        /// </summary>
        /// <param name="infoKey"></param>
        private void listDT_R02_BackChange(SqlDecimal infoKey)
        {
            if (infoKey == 1)
            {
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE1"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE1"].HeaderCell.Style.ForeColor = Color.White;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME1"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME1"].HeaderCell.Style.ForeColor = Color.White;
            }
            else if (infoKey == 2)
            {
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE2"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE2"].HeaderCell.Style.ForeColor = Color.White;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME2"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME2"].HeaderCell.Style.ForeColor = Color.White;
            }
            else if (infoKey == 3)
            {
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE3"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE3"].HeaderCell.Style.ForeColor = Color.White;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME3"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME3"].HeaderCell.Style.ForeColor = Color.White;
            }
            else if (infoKey == 4)
            {
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE4"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE4"].HeaderCell.Style.ForeColor = Color.White;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME4"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME4"].HeaderCell.Style.ForeColor = Color.White;
            }
            else if (infoKey == 5)
            {
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE5"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE5"].HeaderCell.Style.ForeColor = Color.White;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME5"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME5"].HeaderCell.Style.ForeColor = Color.White;
            }
            else if (infoKey == 6)
            {
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE6"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_CODE6"].HeaderCell.Style.ForeColor = Color.White;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME6"].HeaderCell.Style.BackColor = Color.Red;
                this.cdgv_Haikibutu.Columns["YUUGAI_NAME6"].HeaderCell.Style.ForeColor = Color.White;
            }
        }

        private bool Compare(object objA, object objB)
        {
            string cpA = objA == null ? string.Empty : objA.ToString();
            string cpB = objB == null ? string.Empty : objB.ToString();

            if (cpA == cpB)
                return true;
            else
                return false;
        }

        private bool Compare(string cpA, string cpB)
        {
            cpA = string.IsNullOrEmpty(cpA) ? string.Empty : cpA;
            cpB = string.IsNullOrEmpty(cpB) ? string.Empty : cpB;

            if (cpA == cpB)
                return true;
            else
                return false;
        }

        private bool Compare(SqlDecimal cpA, SqlDecimal cpB)
        {
            cpA = cpA.IsNull ? 0 : cpA;
            cpB = cpB.IsNull ? 0 : cpB;

            if (cpA == cpB)
                return true;
            else
                return false;
        }

        /// <summary>
        /// データ解析または検証操作が失敗した場合に発生するイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 例外を無視する
                //e.Cancel = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_DataError", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
}
