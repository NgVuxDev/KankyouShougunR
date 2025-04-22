using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Logic;

namespace Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku
{
    /// <summary>
    /// 建廃マニフェスト入力
    /// </summary>
    [Implementation]
    public partial class KenpaiManifestoNyuryoku : SuperForm
    {
        #region フィールド

        /// <summary>画面ロジック</summary>
        private LogicClass logic = null;

        /// <summary>共通</summary>
        private Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }

        /// <summary>修正ボタンフラグ</summary>
        public bool Updflg { get; set; }

        /// <summary>廃棄物区分CD</summary>
        public String HaikiKbnCD = "2";

        /// <summary>画面区分</summary>
        public WINDOW_TYPE Window_Type = WINDOW_TYPE.NONE;

        /// <summary>処理モード</summary>
        public int IMode = 0;

        /// <summary>システムID</summary>
        public String SystemId = "";

        /// <summary>枝番</summary>
        public String Seq = "";

        /// システム明細ID
        public string SystemDetailId = "";// 2016.11.23 chinkeigen マニ入力と一覧 #101092 


        /// <summary>連携伝達区分</summary>
        public String RDentatuKbn = "";

        /// <summary>連携明細システムID</summary>
        public String RMeisaiId = "";

        /// <summary>「実績」正常判定フラグ</summary>
        private Boolean DgvFlg = true;

        // 20140606 katen 不具合No.4691 start‏
        /// <summary>他の画面にもらったマニフェスト一次二次区分</summary>
        public int? fromManiFirstFlag = null;

        // 20140606 katen 不具合No.4691 start‏

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
        /// KEY EVENTを保存する
        /// </summary>
        internal int keyEventFlg = 0;

        // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start‏
        /// <summary>
        /// 変更前の排出業者
        /// </summary>
        private string bak_HaisyutuGyousyaCd;

        /// <summary>
        /// 変更前の排出現場
        /// </summary>
        private string bak_HaisyutuJigyoubaName;

        /// <summary>
        /// 変更前の運搬事業場
        /// </summary>
        private string bak_UnpanJyugyobaNameCd;

        /// <summary>
        /// 変更前の運搬事業場名
        /// </summary>
        private string bak_UnpanJyugyobaName;

        /// <summary>
        /// 変更前の処分受託者
        /// </summary>
        private string bak_SyobunJyutakuNameCd;

        /// <summary>
        /// 変更前の処分受託者名
        /// </summary>
        private string bak_SyobunJyutakuName;

        // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end

        /// <summary>
        /// 変更前の最終処分業者(実績タブ)
        /// </summary>
        private string bak_SaisyuGyosyaCd = string.Empty;

        private string bak_TumiGyoCd;

        // 2016.11.23 chinkeigen マニ入力と一覧 #101092 start
        private int selectedRowNo = 1;
        // 2016.11.23 chinkeigen マニ入力と一覧 #101092 end

        /// <summary>
        /// 拠点をフォーカスアウトしたか判断します
        /// </summary>
        internal bool isKyotenFocusOut = false;

        private string preGyoushaCD = string.Empty;

        internal bool isClearForm = false;
        internal bool isRegistErr = false;

        internal string preValue = string.Empty;

        internal bool isError = false;

        internal bool ismobile_mode = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string preTorihikisakiCd = string.Empty;
        /// <summary>
        /// 変更前の混合種類
        /// </summary>
        private string bak_KongouSyuruiCd = "";
        /// <summary>
        /// 変更前の混合種類名
        /// </summary>
        private string bak_KongouSyuruiName = "";

        /// <summary>
        /// 修正前の交付番号、交付番号区分
        /// </summary>
        internal string bak_ManifestId = string.Empty;
        internal string bak_ManifestKbn = string.Empty;

        /// <summary>
        /// Request inxs subapp tran id refs #158004
        /// </summary>
        internal string transactionId;

        public bool chkRenkeiDenshuKbnFlg = false;

        #endregion

        /// <summary>
        /// グリッドカラム
        /// </summary>
        public enum enumCols
        {
            /// <summary>廃棄物種類CD</summary>
            HaikiCd = 0,

            /// <summary>廃棄物種類名</summary>
            HaikiSyuruiName,

            /// <summary>廃棄物の名称CD</summary>
            HaikiNameCd,

            /// <summary>廃棄物の名前</summary>
            HaikiName,

            /// <summary>荷姿CD</summary>
            NisugataCd,

            /// <summary>荷姿名</summary>
            NisugataName,

            /// <summary>割合(%)</summary>
            Wariai,

            /// <summary>数量</summary>
            Suryo,

            /// <summary>単位CD</summary>
            TaniCd,

            /// <summary>単位名</summary>
            TaniName,

            /// <summary>換算後数量</summary>
            KansangoSuryo,

            /// <summary>減算後数量</summary>
            GenyoyugoTotalSuryo,

            /// <summary>処分方法CD</summary>
            SyobunCd,

            /// <summary>処分方法名</summary>
            SyobunName,

            /// <summary>処分終了日</summary>
            SyobunEndDate,

            /// <summary>最終処分終了日</summary>
            SaisyuSyobunEndDate,

            /// <summary>最終処分業者CD</summary>
            SaisyuGyosyaCd,

            /// <summary>最終処分業者名</summary>
            SaisyuGyosyaName,

            /// <summary>最終処分場所CD</summary>
            SaisyuBasyoCd,

            /// <summary>最終処分場所名</summary>
            SaisyuSyobunBasyo,

            /// <summary>二次マニ交付番号</summary>
            NijiManiNo,

            /// <summary>明細システムID</summary>
            DetailSystemID,

            /// <summary>タイムスタンプ</summary>
            TimeStamp,
        }

        //20250401
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// パラメータ管理クラス
        /// </summary>
        internal Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.DTO.Parameters parameters = new Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.DTO.Parameters();

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="window_type">画面区分</param>
        /// <param name="RDentatuKbn">連携伝達区分</param>
        /// <param name="SystemId">システムID</param>
        /// <param name="RMeisaiId">連携明細システムID</param>
        /// <param name="iMode">処理モード</param>
        public KenpaiManifestoNyuryoku(WINDOW_TYPE window_type, string RDentatuKbn, string SystemId, string RMeisaiId, int iMode)
            : base(WINDOW_ID.T_KENPAI_MANIFEST, window_type)
        {
            this.InitializeComponent();

            //メッセージクラス
            messageShowLogic = new MessageBoxShowLogic();

            //パラメータ
            this.parameters.Mode = iMode;
            this.parameters.SystemId = SystemId;
            this.parameters.Seq = string.Empty;
            this.parameters.SeqRD = string.Empty;
            this.parameters.ManifestID = string.Empty;
            this.parameters.KongoCd = string.Empty;
            this.parameters.RenkeiDenshuKbnCd = RDentatuKbn;
            this.parameters.RenkeiSystemId = SystemId;
            this.parameters.RenkeiMeisaiSystemId = RMeisaiId;
            this.parameters.PtSystemId = string.Empty;
            this.parameters.PtSeq = string.Empty;
            this.parameters.Save();

            this.Window_Type = window_type;
            this.RDentatuKbn = RDentatuKbn;
            this.SystemId = SystemId;
            this.RMeisaiId = RMeisaiId;
            this.IMode = iMode;

            //モード変更
            switch (iMode)
            {
                case (int)WINDOW_TYPE.NEW_WINDOW_FLAG:
                    base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    Updflg = false;
                    break;

                case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    Updflg = true;
                    break;

                case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    base.WindowType = WINDOW_TYPE.DELETE_WINDOW_FLAG;
                    Updflg = true;
                    break;

                case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    Updflg = false;
                    break;

                default:
                    base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    Updflg = false;
                    break;
            }

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();

            this.shapeContainer1.BringToFront();
            this.shapeContainer2.BringToFront();
            this.shapeContainer3.BringToFront();
            this.shapeContainer4.BringToFront();
            this.shapeContainer5.BringToFront();
            this.shapeContainer6.BringToFront();
            this.shapeContainer7.BringToFront();

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// データ移動用モード用のコンストラクタ
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="gyousyaCd"></param>
        /// <param name="genbaCd"></param>
        public KenpaiManifestoNyuryoku(WINDOW_TYPE windowType, string RDentatuKbn, string SystemId, string RMeisaiId, int iMode,
                                                                    string torihikisakiCd, string gyousyaCd, string genbaCd)
            : this(windowType, RDentatuKbn, SystemId, RMeisaiId, iMode)
        {
            //データ移動用
            this.moveData_flg = true;
            this.moveData_torihikisakiCd = torihikisakiCd;
            this.moveData_gyousyaCd = gyousyaCd;
            this.moveData_genbaCd = genbaCd;
        }

        #endregion コンストラクタ

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

			//Init INXS subapp transaction id refs #158004
            this.transactionId = Guid.NewGuid().ToString();

            bool catchErr = false;
            this.logic.WindowInit("OnLoad", out catchErr);
            if (catchErr) { return; }

            //グリッドの編集モード設定
            this.cdgrid_Jisseki.EditMode = DataGridViewEditMode.EditOnEnter;

            // Anchorの設定は必ずOnLoadで行うこと
            // VisualStugioのデザイナツールで表示するとtabControl1が縮小されるためプロパティから外したので実行時に設定する。
            if (!this.DesignMode)
            {
                // グリッドのAnchorにRightを追加
                if (this.tbc_Sanpai != null)
                {
                    this.tbc_Sanpai.Anchor = (AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right);
                }
            }

            if (this.cdgrid_Jisseki != null)
            {
                this.cdgrid_Jisseki.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }

            // 明細連携区分の活性/非活性を制御する。
            this.MeisaiRenkeiKbnSetting(this.cantxt_DenshuKbn.Text);
        }

        /// <summary>
        /// 画面表示時の制御
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            if (this.parameters.Mode == (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
            {
                // 参照モードの場合、フォーカスが先頭に移動しないので強制的に交付番号にフォーカスさせる。
                this.AutoScrollPosition = new Point(0, 0);
                this.cantxt_KohuNo.Focus();
            }
            else
            {
                this.cantxt_DenshuKbn.Focus();
                this.cdate_KohuDate.Focus();
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
            foreach (System.Windows.Forms.Control control in controls)//遍历groupBox2上的所有控件
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

            this.cckb_KanriFree03.Checked = false;
            this.cantxt_KanriFreeCd03.Text = "";
            this.ctxt_KanriFreeName03.Text = "";
            this.cntxt_KanriFreeSuryo03.Text = "";
        }

        /// <summary>
        /// 画面クリア処理
        /// </summary>
        /// <param name="e"></param>
        internal void ClearScreen(EventArgs e)
        {
            LogUtility.DebugMethodStart();

            //base.OnLoad(e);

            //テキストボックスの削除
            ControlLoopDel(this.Controls);

            //isClearForm = true => do not check valid on cdgrid_Jisseki when clear form
            this.isClearForm = true;
            //グリッドの削除
            this.cdgrid_Jisseki.Rows.Clear();
            //reset variable
            this.isClearForm = false;

            //紐付情報クリア
            this.logic.maniRelation = null;

            LogUtility.DebugMethodEnd();
        }

        #region ファンクションボタン

        /// <summary>
        /// (F1)切替イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetKirikaeFrom(object sender, EventArgs e)
        {
            this.logic.SetKirikaeFrom();
        }

        /// <summary>
        /// (F2)新規イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetAddFrom(object sender, EventArgs e)
        {
            // 権限チェック
            if (!Manager.CheckAuthority("G121", WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }

            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
            this.parameters.Save();

            this.ClearScreen(e);
            bool catchErr = false;
            this.logic.WindowInit("SetAddFrom", out catchErr);
        }

        /// <summary>
        /// (F3)修正イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetUpdateFrom(object sender, EventArgs e)
        {
            // 権限チェック
            if (Manager.CheckAuthority("G121", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                this.parameters.Mode = (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            }
            else if (Manager.CheckAuthority("G121", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                this.parameters.Mode = (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            }
            else
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E158", "修正");

                return;
            }

            this.parameters.Save();

            this.ClearScreen(e);
            bool catchErr = false;
            this.logic.WindowInit("SetUpdateFrom", out catchErr);
        }

        /// <summary>
        /// (F4)1次/2次マニフェスト設定イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetManifestFrom(object sender, EventArgs e)
        {
            this.logic.SetManifestFrom("F4");

            // 20140609 katen No.730 規定値機能の追加について start‏
            if (this.parameters.Mode == (int)WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.SetKiteiValue(this.logic.headerform.ctxt_KyotenCd.Text);
            }
            // 20140609 katen No.730 規定値機能の追加について end‏

            this.ClearScreen(e);
            bool catchErr = false;
            this.logic.WindowInit("SetAddFrom", out catchErr);
            if (catchErr) { return; }
        }

        /// <summary>
        /// (F5)連票イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetRenhyouFrom(object sender, EventArgs e)
        {
            switch (this.logic.SetRenhyouFrom())
            {
                case false://マニフェスト登録なし
                    break;

                case true://マニフェスト登録あり
                    //単票･連票 モード変更(初期化)
                    this.ClearFormValue(e);
                    break;
            }
        }

        /// <summary>
        /// (F6)単票イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetTahyouFrom(object sender, EventArgs e)
        {
            switch (this.logic.SetTahyouFrom())
            {
                case false://マニフェスト登録なし
                    break;

                case true://マニフェスト登録あり
                    //単票･連票 モード変更(初期化)
                    this.ClearFormValue(e);
                    break;
            }
        }

        /// <summary>
        /// 単票･連票 モード変更(初期化)
        /// </summary>
        /// <param name="e"></param>
        public void ClearFormValue(EventArgs e)
        {
            this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
            this.parameters.Save();
            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            this.ClearScreen(e);
            bool catchErr = false;
            switch (this.parameters.Mode)
            {
                case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                    this.logic.WindowInit("SetRegistFrom_Delete", out catchErr);
                    break;

                case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                    this.logic.WindowInit("SetRegistFrom_New", out catchErr);
                    break;

                case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                    this.logic.WindowInit("SetRegistFrom_Update", out catchErr);
                    break;
            }
        }

        /// <summary>
        /// (F7)一覧イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetItiranFrom(object sender, EventArgs e)
        {
            // 権限チェック
            if (!Manager.CheckAuthority("G126", WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
            {
                return;
            }

            this.logic.SetItiranFrom();
            //this.FormClose(sender, e);
        }

        /// <summary>
        /// (F8)状況イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetJokyoFrom(object sender, EventArgs e)
        {
            // 権限チェック
            if (!Manager.CheckAuthority("G589", WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
            {
                return;
            }

            this.logic.SetJokyoFrom();
        }

        /// <summary>
        /// (F9)登録イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetRegistFrom(object sender, EventArgs e)
        {
            if (this.logic.SetRegist())
            {
                return;
            }
            if (this.isRegistErr) { return; }

            bool catchErr = false;
            switch (this.parameters.Mode)
            {
                case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                    this.ParentForm.Close();//削除の場合は更新後に閉じる
                    return;

                case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                    this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.parameters.Save();
                    base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.ClearScreen(e);
                    this.logic.WindowInit("SetRegistFrom_New", out catchErr);
                    break;

                case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード

                    if (!Manager.CheckAuthority("G121", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // 新規権限が無ければ閉じる
                        this.ParentForm.Close();
                        return;
                    }

                    this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.parameters.Save();
                    base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.ClearScreen(e);
                    this.logic.WindowInit("SetRegistFrom_Update", out catchErr);
                    break;
            }
        }

        /// <summary>
        /// (F10)契約イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetKeiyakuFrom(object sender, EventArgs e)
        {
            this.logic.SetKeiyakuFrom();
        }

        /// <summary>
        /// 行削除イベント(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func11_Click(object sender, EventArgs e)
        {
            //フォーカスがグリッドにあるかを調べる。
            if (this.cdgrid_Jisseki.CurrentRow == null)
            {
                return;
            }

            //新規行は削除しない。
            if (this.cdgrid_Jisseki.CurrentRow.IsNewRow)
            {
                return;
            }

            // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる start
            if (this.logic.ChkRelationDetail(this.cdgrid_Jisseki.CurrentRow))
            {
                return;
            }
            // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる end

            bool catchErr = false;
            // 二次マニの場合は紐付けた一次電マニが既に最終処分終了報告されていた場合
            // 最終処分情報が変更されるような操作は不可とする
            if (this.logic.maniFlag == 2
                && WINDOW_TYPE.UPDATE_WINDOW_FLAG.GetHashCode() == this.parameters.Mode)
            {
                if (this.logic.IsFixedRelationFirstMani(SqlInt64.Parse(this.parameters.SystemId), out catchErr) || catchErr)
                {
                    if (!catchErr) 
                    {
                        this.messageShowLogic.MessageBoxShow("E220", "既に最終処分終了報告が完了しているため");
                    }
                    return;
                }
                else if (this.logic.IsExecutingLastSbnEndRep(SqlInt64.Parse(this.parameters.SystemId), out catchErr) || catchErr)
                {
                    if (!catchErr)
                    {
                        // 最終処分終了報告中
                        this.messageShowLogic.MessageBoxShow("E220", "最終処分終了報告中のため");
                    }
                    return;
                }
            }

            //該当行を削除。
            this.cdgrid_Jisseki.Rows.RemoveAt(this.cdgrid_Jisseki.CurrentCell.RowIndex);

            if (this.logic.maniFlag == 2 && this.cdgrid_Jisseki.Rows.Count == 0)
            {
                this.cdgrid_Jisseki.AllowUserToAddRows = true;
            }

            this.logic.SetTotal();
        }

        /// <summary>
        /// (F12)Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// プロセス１実行イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            bool catchErr = false;
            this.logic.UpdatePattern(out catchErr);
        }

        /// <summary>
        /// プロセス２実行イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, EventArgs e)
        {
            // 権限チェック
            if (!Manager.CheckAuthority("G121", WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }

            bool catchErr = false;
            if (this.logic.CallPattern(out catchErr))
            {
                if (catchErr) { return; }
                //this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                //this.parameters.Save();
                base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                //base.OnLoad(e);

                this.logic.WindowInit("bt_process2", out catchErr);
            }
        }

        /// <summary>
        /// プロセス３実行イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cantxt_KohuNo.Text))
            {
                this.messageShowLogic.MessageBoxShow("E001", "交付番号");
                this.cantxt_KohuNo.Focus();
                return;
            }

            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 start
            //明細がないため紐付けできません。
            if (this.cdgrid_Jisseki.RowCount == 0)
            {
                this.messageShowLogic.MessageBoxShow("E057", new string[] { "明細が登録", "紐付け" });
                return;
            }

            if (this.cdgrid_Jisseki.RowCount == 1)
            {
                if (this.cdgrid_Jisseki.Rows[0].IsNewRow)
                {
                    this.messageShowLogic.MessageBoxShow("E057", new string[] { "明細が登録", "紐付け" });
                    return;
                }
            }

            if (this.cdgrid_Jisseki.CurrentRow == null || this.cdgrid_Jisseki.CurrentRow.IsNewRow)
            {
                return;
            }

            this.selectedRowNo = this.cdgrid_Jisseki.CurrentRow.Index;
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 end

            if (this.parameters.Mode == (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                // ２次マニ情報から紐付情報の有無と最終処分終了日の比較を行う。
                // 電子１次に紐付されていて、かつ電子１次マニの最終処分終了日が設定済の場合、かつ２次マニの最終処分終了日が設定済の場合に確認メッセージを表示する。
                if (this.logic.CheckLastSbnDate())
                {
                    if (this.messageShowLogic.MessageBoxShow("C046", "1次の最終処分終了日と2次の最終処分終了日に差異があります。\n登録") != DialogResult.Yes)
                    {
                        return;
                    }
                }
            }

            // 20140519 katen No.730 マニフェスト入力画面に対する機能追加（建廃マニフェスト入力） start‏
            if (Message.MessageBoxUtility.MessageBoxShow("C065") != DialogResult.Yes)
            {
                return;
            }

            if (this.logic.SetRegistForHimoduke())
            {
                //登録失敗の場合に、画面に戻る
                return;
            }
            bool catchErr = false;
            //登録完了の場合に、次の処理を続く
            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                //画面が新規状態の場合、更新状態に変更する
                this.SystemId = this.parameters.SystemId;
                this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                this.parameters.Mode = (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                this.parameters.Save();

                this.ClearScreen(e);
                this.logic.WindowInit("SetUpdateFrom", out catchErr);
                this.cdgrid_Jisseki.Rows[this.selectedRowNo].Cells[0].Selected = true;
            }
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 start
            else if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                this.ClearScreen(e);
                this.logic.WindowInit("SetUpdateFrom", out catchErr);
                this.cdgrid_Jisseki.Rows[this.selectedRowNo].Cells[0].Selected = true;
            }
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 end

            // 20140519 katen No.730 マニフェスト入力画面に対する機能追加（建廃マニフェスト入力） end‏

            if (!this.logic.ManiHimozuke()) { return; }

            // 権限チェック
            if (Manager.CheckAuthority("G121", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // 20140519 katen No.730 マニフェスト入力画面に対する機能追加（建廃マニフェスト入力） start‏
                if (this.logic.maniRelation != null && this.logic.maniRelation.result == DialogResult.OK || this.logic.maniRelation.result == DialogResult.Yes)
                {
                    //登録後、修正モードで開きなおす(明細行キープ)
                    this.ClearScreen(e);
                    this.logic.WindowInit("SetUpdateFrom", out catchErr);
                    if (catchErr) { return; }
                }
                else
                {
                    //マニフェスト紐付画面に登録しなければ、DBから先に更新したデータを取得し、画面に表示する
                    if (this.logic.WindowInit("SetUpdateFrom", out catchErr))
                    {
                        if (catchErr) { return; }
                        // 権限チェック
                        if (Manager.CheckAuthority("G121", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            //先に更新したデータを取得できない場合、画面を初期化する
                            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.parameters.Save();

                            this.ClearScreen(e);
                            this.logic.WindowInit("SetAddFrom", out catchErr);
                            if (catchErr) { return; }
                            this.selectedRowNo = 0;
                        }
                        else
                        {
                            if (catchErr) { return; }
                            // 新規権限が無ければ閉じる
                            this.ParentForm.Close();
                        }
                    }
                }
                // 20140519 katen No.730 マニフェスト入力画面に対する機能追加（建廃マニフェスト入力） end‏
            }
            else
            {
                // 修正権限が無い場合は、新規モードを表示
                this.selectedRowNo = 0;
                this.SetAddFrom(sender, e);
            }
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 start
            this.cdgrid_Jisseki.Rows[this.selectedRowNo].Cells[0].Selected = true;
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 end
        }

        /// <summary>
        /// プロセス４実行イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process4_Click(object sender, EventArgs e)
        {
            this.logic.CallSousinnHoryuuPopuUp(true);
        }

        /// <summary>
        /// プロセス５実行イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process5_Click(object sender, EventArgs e)
        {
            this.logic.CallSousinnHoryuuPopuUp(false);
        }

        /// <summary>
        /// ESCテキストイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void txb_process_Enter(object sender, KeyEventArgs e)
        {
            int iret = 0;
            bool catchErr = false;
            iret = this.logic.DoProcess(e);
            if (iret == -1)
            {
                return;
            }
            else if (iret == 2)
            {
                // 権限チェック
                if (Manager.CheckAuthority("G121", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.parameters.Save();
                    base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    //base.OnLoad(e);

                    this.logic.WindowInit("bt_process2", out catchErr);
                }
            }
        }

        #endregion

        #region ボタン

        #region 検索ボタン押下イベント

        /// <summary>
        /// 排出事業場検索ボタンValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_HaisyutuJigyoubaSan_Validated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 最終処分場検索ボタンValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void casbtn_SaisyuGyousya_Validated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 運搬先の事業場検索ボタンValidatedイベント
        /// </summary>
        private void cbtn_UnpanJyugyobaSan_Validated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 処分受託者検索ボタンValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SyobunJyutakuSan_Validated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 積換保管事業場検索ボタンValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_TumiHokaName_Validated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 最終処分場所検索ボタンValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void casbtn_SaisyuBasyo_Validated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 運搬受託者検索ボタンValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyutaku1San_Validated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 積換保管業者検索ボタンValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_TumiGyo_Validated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 積替保管検索ボタンValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_HaisyutuGyousyaSan_Validated(object sender, EventArgs e)
        {
        }

        #endregion

        #region 削ボタン押下イベント

        /// <summary>
        /// 排出業者 削ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_HaisyutuGyousyaDel_Click(object sender, EventArgs e)
        {
            this.logic.HaisyutuGyousyaDel();
            this.logic.HaisyutuJigyoubaDel();
        }

        /// <summary>
        /// 排出作業場 削ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_HaisyutuJigyoubaDel_Click(object sender, EventArgs e)
        {
            this.logic.HaisyutuJigyoubaDel();
            //this.ctxt_KohuTantou.Text = string.Empty;#157148
        }

        #endregion

        #region 斜線ボタン押下イベント

        /// <summary>
        /// 照合確認日 B1票ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SyougouKakuninB1_Click(object sender, EventArgs e)
        {
            //B1票日付
            this.logic.SetEnableCtl(this.cdate_SyougouKakuninB1);

            //斜線
            this.logic.SetLineCtl(this.cdate_SyougouKakuninB1, this.lineShape1);

            this.Refresh();
        }

        /// <summary>
        /// 照合確認日 B2票ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SyougouKakuninB2_Click(object sender, EventArgs e)
        {
            //B2票日付
            this.logic.SetEnableCtl(this.cdate_SyougouKakuninB2);

            //斜線
            this.logic.SetLineCtl(this.cdate_SyougouKakuninB2, this.lineShape2);

            this.Refresh();
        }

        /// <summary>
        /// 照合確認日 D票ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SyougouKakuninD_Click(object sender, EventArgs e)
        {
            //D票日付
            this.logic.SetEnableCtl(this.cdate_SyougouKakuninD);

            //斜線
            this.logic.SetLineCtl(this.cdate_SyougouKakuninD, this.lineShape3);

            this.Refresh();
        }

        /// <summary>
        /// 照合確認日 E票ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SyougouKakuninE_Click(object sender, EventArgs e)
        {
            //E票日付
            this.logic.SetEnableCtl(this.cdate_SyougouKakuninE);

            //斜線
            this.logic.SetLineCtl(this.cdate_SyougouKakuninE, this.lineShape4);

            this.Refresh();
        }

        #endregion

        /// <summary>
        /// 事前協議ボタン押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_JizenKyougi_Click(object sender, EventArgs e)
        {
            //事前協議番号
            this.logic.SetEnableCtl(this.cntxt_jizenkyougi);

            //事前協議日付
            this.logic.SetEnableCtl(this.cdate_JizenKyougi);

            //斜線
            this.logic.SetLineCtl(this.cntxt_jizenkyougi, this.lineShape6);

            this.Refresh();
        }

        /// <summary>
        /// 中間処理産業廃棄物ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_TyukanHaikibutu_Click(object sender, EventArgs e)
        {
            //帳簿記載とおりチェック
            this.logic.SetEnableCtl(this.ccbx_TyukanTyoubo);

            //当欄記載とおりチェック
            this.logic.SetEnableCtl(this.ccbx_TyukanKisai);

            //中間処理産業廃棄物
            this.logic.SetEnableCtl(this.ctxt_TyukanHaikibutu);

            //斜線
            this.logic.SetLineCtl(this.ctxt_TyukanHaikibutu, this.lineShape5);

            this.Refresh();
        }

        /// <summary>
        /// 積替え又は保管ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_TumiHoka_Click(object sender, EventArgs e)
        {
            this.logic.SetEnableCtl(this.ctxt_TumiGyoName);
            this.logic.SetEnableCtl(this.ctxt_TumiHokaName);
            this.logic.SetEnableCtl(this.cantxt_TumiGyoCd);
            this.logic.SetEnableCtl(this.cbtn_TumiGyo);
            this.logic.SetEnableCtl(this.cantxt_TumiHokaNameCd);
            this.logic.SetEnableCtl(this.cbtn_TumiHokaName);
            this.logic.SetEnableCtl(this.cnt_TumiHokaZip);
            this.logic.SetEnableCtl(this.cbtn_TumiHokaSan);
            this.logic.SetEnableCtl(this.cbtn_TumiHokaDel);
            this.logic.SetEnableCtl(this.cnt_TumiHokaTel);
            this.logic.SetEnableCtl(this.ctxt_TumiHokaAdd);
            this.logic.SetEnableCtl(this.cckb_TumiHokaAri);
            this.logic.SetEnableCtl(this.cckb_TumiHokaNasi);
            this.logic.SetEnableCtl(this.cntxt_TumiHokaJisseki);
            this.logic.SetEnableCtl(this.cckb_TumiHokaT);
            this.logic.SetEnableCtl(this.cckb_TumiHokaM3);
            this.logic.SetLineCtl(this.cantxt_TumiGyoCd, this.lineShape7);
            this.Refresh();
        }

        /// <summary>
        /// 追加特記事項ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJigyobaTokki_Click(object sender, EventArgs e)
        {
            this.logic.SetEnableCtl(this.ctxt_UnpanJigyobaTokki);
            this.logic.SetLineCtl(this.ctxt_UnpanJigyobaTokki, this.lineShape9);
            this.Refresh();
        }

        /// <summary>
        /// 帳簿チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccbx_TyukanTyoubo_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        /// <summary>
        /// 帳簿チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccbx_TyukanKisai_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        /// <summary>
        /// 委託契約書記載チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccbx_SaisyuTyoubo_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        /// <summary>
        /// 当欄記載チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccbx_SaisyuKisai_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        /// <summary>
        /// 最終処分（埋立処分、再生等）の場所削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SaisyuGyousyaDel_Click(object sender, EventArgs e)
        {
            this.logic.SaisyuSyobunDel("cbtn_SaisyuGyousyaDel");
        }

        /// <summary>
        /// 積換保管実績数量tチェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cckb_TumiHokaT_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        /// <summary>
        /// 積換保管実績数量m3チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cckb_TumiHokaM3_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
        /// <summary>
        /// 積換保管有チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cckb_Jyutaku1HokanAri_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        /// <summary>
        /// 積換保管無チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cckb_Jyutaku1HokanNasi_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        /// <summary>
        /// 積換保管有チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cckb_Jyutaku2HokanAri_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        /// <summary>
        /// 積換保管無チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cckb_Jyutaku2HokanNasi_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        /// <summary>
        /// 有価物拾集有チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cckb_TumiHokaAri_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }

        /// <summary>
        /// 有価物拾集無チェックボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cckb_TumiHokaNasi_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.SetCheckCtl(sender);
        }
        // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end

        /// <summary>
        /// 運搬受託者削ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyutaku1Del_Click(object sender, EventArgs e)
        {
            this.logic.UnpanJyutaku1Del();
        }

        /// <summary>
        /// 運搬受託者（収集運搬業者）（２）削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyutaku2Del_Click(object sender, EventArgs e)
        {
            this.logic.UnpanJyutaku2Del();
        }

        /// <summary>
        /// 処分受託者（処分業者）削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SyobunJyutakuDel_Click(object sender, EventArgs e)
        {
            this.logic.SyobunJyutakuDel();
            this.logic.UnpanJyugyobaDel();
        }

        /// <summary>
        /// 運搬先の事業場（処分業者の処理施設）削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyugyobaDel_Click(object sender, EventArgs e)
        {
            this.logic.UnpanJyugyobaDel();
        }

        /// <summary>
        /// 積替え又は保管削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_TumiHokaDel_Click(object sender, EventArgs e)
        {
            this.logic.TumiHokaDel("cbtn_TumiHokaDel");
        }

        /// <summary>
        /// 最終処分削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SaisyuBasyoDel_Click(object sender, EventArgs e)
        {
            this.logic.SaisyuBasyoSyozaiDel("cbtn_SaisyuBasyoDel");
        }

        /// <summary>
        /// 運搬業者(２)ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_UnpanJyutaku2_Click(object sender, EventArgs e)
        {
            this.logic.SetEnableCtl(this.cnt_UnpanJyutaku2Zip);
            this.logic.SetEnableCtl(this.cbtn_UnpanJyutaku2San);
            this.logic.SetEnableCtl(this.cbtn_UnpanJyutaku2Del);
            this.logic.SetEnableCtl(this.cnt_UnpanJyutaku2Tel);
            this.logic.SetEnableCtl(this.ctxt_UnpanJyutaku2Add);
            this.logic.SetEnableCtl(this.cantxt_UnpanJyutaku2NameCd);
            this.logic.SetEnableCtl(this.cantxt_UnpanJyutaku2Name);
            this.logic.SetEnableCtl(this.casbtn_UnpanJyutaku2);
            this.logic.SetEnableCtl(this.ctxt_Jyutaku2SyaNo);
            this.logic.SetEnableCtl(this.ctxt_Jyutaku2Syasyu);
            this.logic.SetEnableCtl(this.cantxt_UnpanHouhou2CD);
            this.logic.SetEnableCtl(this.ctxt_UnpanHouhouMei2);

            this.logic.SetEnableCtl(this.cckb_Jyutaku2HokanAri);
            this.logic.SetEnableCtl(this.cckb_Jyutaku2HokanNasi);
            this.logic.SetEnableCtl(this.cantxt_Jyutaku2SyaNo);
            this.logic.SetEnableCtl(this.cantxt_Jyutaku2Syasyu);

            //斜線
            this.logic.SetLineCtl(this.cnt_UnpanJyutaku2Zip, this.lineShape8);
            this.Refresh();
        }

        /// <summary>
        /// 運搬の受託（2）ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyu2_Click(object sender, EventArgs e)
        {
            this.logic.SetEnableCtl(this.cantxt_UnpanJyuCd2);
            this.logic.SetEnableCtl(this.ctxt_UnpanJyuName2);
            this.logic.SetEnableCtl(this.cantxt_UnpanJyuUntenCd2);
            this.logic.SetEnableCtl(this.cantxt_UnpanJyuUntenName2);
            this.logic.SetEnableCtl(this.customButton2);
            this.logic.SetEnableCtl(this.cdate_UnpanJyu2);
            this.logic.SetLineCtl(this.cantxt_UnpanJyuCd2, this.lineShape10);
            this.Refresh();
        }

        #endregion

        #region タブ

        //タブコントロールの色塗り
        private void tbc_Sanpai_DrawItem(object sender, DrawItemEventArgs e)
        {
            //背景色
            Color BackColor = Color.FromArgb(232, 247, 240);

            //入力可能テキストコントロール
            Color PreColor = Color.FromArgb(255, 255, 255);

            //入力中テキストコントロール
            Color InputColor = Color.FromArgb(0, 255, 255);

            TabControl tab = (TabControl)sender;
            Graphics g = e.Graphics;
            Brush b;
            TabPage page = tab.TabPages[e.Index];
            //タブページのテキストを取得
            string txt = page.Text;

            //StringFormatを作成
            StringFormat sf = new StringFormat();
            //縦書きにする
            sf.FormatFlags = StringFormatFlags.DirectionVertical;
            //ついでに、水平垂直方向の中央に、行が完全に表示されるようにする
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            sf.FormatFlags |= StringFormatFlags.LineLimit;

            //背景の描画
            if (e.Index == tab.SelectedIndex)
            {
                // イベントが選択ページに対しての場合
                b = new SolidBrush(InputColor);
            }
            else
            {
                // イベントがその他のページの場合
                b = new SolidBrush(PreColor);
            }
            g.FillRectangle(b, tab.GetTabRect(e.Index));

            //Textの描画
            Brush foreBrush = new SolidBrush(page.ForeColor);
            e.Graphics.DrawString(txt, page.Font, foreBrush, e.Bounds, sf);
            foreBrush.Dispose();

            //余白を塗る
            if (e.Index == tab.TabCount - 1)
            {
                // 最後のタブの場合、タブ領域の隣からタブコントロール全体の右端まで色を塗る
                b = new SolidBrush(BackColor);
                Rectangle r = tab.GetTabRect(e.Index);

                // タブコントロールのウィンドウハンドルからGraphicsオブジェクトを生成
                g = Graphics.FromHwnd(tab.Handle);
                g.FillRectangle(b, tab.Left, r.Top + r.Height, r.Width, tab.Height - r.Top - r.Height);
                g.Dispose();
            }
        }

        //タブコントロールの無効
        private void tbc_Sanpai_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (DgvFlg == false)
            {
                e.Cancel = true;
            }
        }

        // 20140519 katen No.730 マニフェスト入力画面に対する機能追加（建廃マニフェスト入力） start‏
        //タブコントロールの色塗り
        private void tbc_SaishuShobunBaai_DrawItem(object sender, DrawItemEventArgs e)
        {
            //背景色
            Color BackColor = Color.FromArgb(232, 247, 240);

            //入力可能テキストコントロール
            Color PreColor = Color.FromArgb(255, 255, 255);

            //入力中テキストコントロール
            Color InputColor = Color.FromArgb(0, 255, 255);

            TabControl tab = (TabControl)sender;
            Graphics g = e.Graphics;
            Brush b;
            TabPage page = tab.TabPages[e.Index];
            //タブページのテキストを取得
            string txt = page.Text;

            //StringFormatを作成
            StringFormat sf = new StringFormat();
            //縦書きにする
            sf.FormatFlags = StringFormatFlags.DirectionVertical;
            //ついでに、水平垂直方向の中央に、行が完全に表示されるようにする
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            sf.FormatFlags |= StringFormatFlags.LineLimit;

            //背景の描画
            if (e.Index == tab.SelectedIndex && this.tbc_Sanpai.SelectedTab.Text.Equals("原本"))
            {
                // イベントが選択ページに対しての場合
                b = new SolidBrush(InputColor);
            }
            else
            {
                // イベントがその他のページの場合
                b = new SolidBrush(PreColor);
            }
            g.FillRectangle(b, tab.GetTabRect(e.Index));

            //Textの描画
            Brush foreBrush = new SolidBrush(page.ForeColor);
            e.Graphics.DrawString(txt, page.Font, foreBrush, e.Bounds, sf);
            foreBrush.Dispose();
            //余白を塗る
            if (e.Index == tab.TabCount - 1)
            {
                // 最後のタブの場合、タブ領域の隣からタブコントロール全体の右端まで色を塗る
                b = new SolidBrush(BackColor);
                Rectangle r = tab.GetTabRect(e.Index);

                // タブコントロールのウィンドウハンドルからGraphicsオブジェクトを生成
                g = Graphics.FromHwnd(tab.Handle);
                g.FillRectangle(b, tab.Left, r.Top + r.Height, r.Width, tab.Height - r.Top - r.Height);
                g.Dispose();
            }
        }

        /// <summary>
        /// 廃棄物タブSelectedIndexChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbc_Sanpai_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tbc_Sanpai.SelectedIndex)
            {
                case 0:
                    //実績を選んだ場合、下の「原本」タブのコントロールが入力不可になる
                    this.logic.ControlEnabledSet(false);
                    break;

                case 1:
                    //原本を選んだ場合、下の「原本」タブのコントロールが入力可になる
                    this.logic.ControlEnabledSet(true);
                    break;
            }
            this.tbc_SaishuShobunBaai.Refresh();
        }
        // 20140519 katen No.730 マニフェスト入力画面に対する機能追加（建廃マニフェスト入力） end‏

        #endregion

        #region グリッド

        /// <summary>
        /// 行追加イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgrid_Jisseki_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //セルの既定値を指定する
            e.Row.Cells[(int)enumCols.SaisyuSyobunEndDate].Value = null;
            e.Row.Cells[(int)enumCols.SyobunEndDate].Value = null;
            e.Row.Cells[(int)enumCols.TaniCd].Value = String.Format("{0:00}", this.canTxt_JissekiTaniCd.Text);
            e.Row.Cells[(int)enumCols.TaniName].Value = this.logic.GetTaniName(this.canTxt_JissekiTaniCd.Text);
        }

        /// <summary>
        /// 行削除イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgrid_Jisseki_KeyUp(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// 処分検索ポップアップの条件設定
        /// </summary>
        public void SetGridFilteringShobunba(int iRow)
        {
            if (this.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value != null &&
                this.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value.ToString() != string.Empty)
            {
                //条件変更
                this.SaisyuBasyoCd.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHA_CD";
                dto.Value = this.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value.ToString();
                this.SaisyuBasyoCd.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "SHOBUN_NIOROSHI_GENBA_KBN";
                dto.Value = "True";
                this.SaisyuBasyoCd.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.Control = "cdate_KohuDate";
                dto.KeyName = "TEKIYOU_BEGIN";
                this.SaisyuBasyoCd.PopupSearchSendParams.Add(dto);
            }
            else
            {
                //条件変更
                this.SaisyuBasyoCd.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "SHOBUN_NIOROSHI_GENBA_KBN";
                dto.Value = "True";
                this.SaisyuBasyoCd.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.Control = "cdate_KohuDate";
                dto.KeyName = "TEKIYOU_BEGIN";
                this.SaisyuBasyoCd.PopupSearchSendParams.Add(dto);
            }
        }

        #endregion

        #region フォーム

        #region 取引先
        private void cantxt_TorihikiCd_Enter(object sender, EventArgs e)
        {
            if (!this.cantxt_TorihikiCd.IsInputErrorOccured)
            {
                this.preTorihikisakiCd = this.cantxt_TorihikiCd.Text;
            }
        }
        /// <summary>
        /// 取引先 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TorihikiCd_Validated(object sender, EventArgs e)
        {
            if (this.cantxt_TorihikiCd.IsInputErrorOccured || this.cantxt_TorihikiCd.Text != this.preTorihikisakiCd)
            {
                this.logic.SetTorihikisaki();
            }
        }

        /// <summary>
        /// 取引先 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_TorihikiCd_PopupAfterExecuteMethod()
        {
            if (this.cantxt_TorihikiCd.IsInputErrorOccured || this.cantxt_TorihikiCd.Text != this.preTorihikisakiCd)
            {
                this.logic.SetTorihikisaki();
            }
        }

        #endregion

        #region 交付番号

        /// <summary>
        /// 交付番号 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KohuNo_Validating(object sender, CancelEventArgs e)
        {
            if (this.logic.ChkKohuNo())
            {
                e.Cancel = true;
                return;
            }

            //交付番号の同一チェック
            if (this.parameters.ManifestID == this.cantxt_KohuNo.Text)
            {
                return;
            }

            //交付番号存在チェック
            string SystemId = string.Empty;
            string Seq = string.Empty;
            string SeqRD = string.Empty;
            bool catchErr = false;

            if (this.mlogic.ExistKohuNo(this.logic.FormHaikiKbn, this.cantxt_KohuNo.Text, ref SystemId, ref Seq, ref SeqRD))
            {
                return;
            }

            // キャンセルされた場合のために現在のモードを保持しておく
            int tmpMoode = this.parameters.Mode;

            // 権限チェック
            if (Manager.CheckAuthority("G121", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                //修正モード
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                Updflg = true;

                this.parameters.Mode = (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            }
            else if (Manager.CheckAuthority("G121", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                //参照モード
                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                Updflg = false;

                this.parameters.Mode = (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            }
            else
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E158", "修正");

                e.Cancel = true;
                return;
            }

            //修正モードになる時、確認メッセージを表示する。
            string errorMessage = string.Empty;
            var messageUtil = new MessageUtility();
            errorMessage = messageUtil.GetMessage("C017").MESSAGE;
            DialogResult result = MessageBox.Show(errorMessage, Constans.CONFIRM_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                //VAN 20201125 #143822, #143823, #143824 S
                //cantxt_KohuNo.Clear();
                if (tmpMoode == (int)WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    cantxt_KohuNo.Clear();
                }
                else
                {
                    cantxt_KohuNo.Text = bak_ManifestId;

                    // 交付番号区分
                    if (String.Equals(bak_ManifestKbn, "1"))
                    {
                        this.crdo_KohuTujyo.Checked = true;
                        this.crdo_KohuReigai.Checked = false;
                    }
                    else
                    {
                        this.crdo_KohuTujyo.Checked = false;
                        this.crdo_KohuReigai.Checked = true;
                    }
                }
                //VAN 20201125 #143822, #143823, #143824 E
                this.parameters.Mode = tmpMoode;
                return;
            }

            //システムID(全般･マニ返却日)
            this.parameters.SystemId = SystemId;
            this.SystemId = SystemId;

            //枝番(全般)
            this.parameters.Seq = Seq;

            //枝番(マニ返却日)
            this.parameters.SeqRD = SeqRD;

            //交付番号
            this.parameters.ManifestID = this.cantxt_KohuNo.Text;

            this.parameters.Save();

            //画面の初期化
            this.ClearScreen(e);
            this.logic.WindowInit("cantxt_KohuNo", out catchErr);
        }

        #endregion

        #region 排出業者

        /// <summary>
        /// 排出事業者CD Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuGyousyaCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 排出事業者CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuGyousyaCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            //排出事業者チェック
            switch (this.logic.ChkGyosya(cantxt_HaisyutuGyousyaCd, "HAISHUTSU_NIZUMI_GYOUSHA_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //排出業者削除
                    this.logic.HaisyutuGyousyaDel();

                    //排出業場削除
                    this.logic.HaisyutuJigyoubaDel();

                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 排出事業者CD PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod()
        {
            if (this.cantxt_HaisyutuGyousyaCd.Text != this.preGyoushaCD)
            {
                //排出業場削除
                this.logic.HaisyutuJigyoubaDel();
            }

            // 20140611 katen 不具合No.4469 start
            //ManifestoLogic.SetAddrGyousha("All", cantxt_HaisyutuGyousyaCd, ctxt_HaisyutuGyousyaName1,
            //    cnt_HaisyutuGyousyaZip, cnt_HaisyutuGyousyaTel, ctxt_HaisyutuGyousyaAdd1,
            //    null, null, null
            //    , true, false, false, false);
            //業者　設定‏
            this.logic.SetAddrGyousha("All", cantxt_HaisyutuGyousyaCd, new CustomTextBox[] { ctxt_HaisyutuGyousyaName1, ctxt_HaisyutuGyousyaName2 },
                cnt_HaisyutuGyousyaZip, cnt_HaisyutuGyousyaTel, new CustomTextBox[] { ctxt_HaisyutuGyousyaAdd1, ctxt_HaisyutuGyousyaAdd2 },
                null, null, null
                , true, false, false, false, this.logic.isNotNeedDeleteFlg);
            // 20140611 katen 不具合No.4469 end
        }

        /// <summary>
        /// 排出事業者CD PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuGyousyaCd_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCD = this.cantxt_HaisyutuGyousyaCd.Text;
        }

        public void cbtn_HaisyutuGyousyaSan_PopupAfterExecuteMethod()
        {
            int ret = this.logic.SetAddrGyousha("All", cantxt_HaisyutuGyousyaCd, new CustomTextBox[] { ctxt_HaisyutuGyousyaName1, ctxt_HaisyutuGyousyaName2 },
                cnt_HaisyutuGyousyaZip, cnt_HaisyutuGyousyaTel, new CustomTextBox[] { ctxt_HaisyutuGyousyaAdd1, ctxt_HaisyutuGyousyaAdd2 },
                null, null, null
                , true, false, false, false);
            if (ret == -1) { return; }
            if (!string.Equals(this.cantxt_HaisyutuGyousyaCd.Text, this.preGyoushaCD))
            {
                this.logic.HaisyutuJigyoubaDel();
            }
        }

        public void cbtn_HaisyutuGyousyaSan_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCD = this.cantxt_HaisyutuGyousyaCd.Text;
        }

        #endregion

        #region 排出事業場

        /// <summary>
        /// 排出事業場CD Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuJigyoubaName_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 排出事業場CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuJigyoubaName_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }
            //20150618 #5005 hoanghm start
            //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
            if (!string.IsNullOrEmpty(cantxt_HaisyutuJigyoubaName.Text) && string.IsNullOrEmpty(cantxt_HaisyutuGyousyaCd.Text))
            {
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                this.messageShowLogic.MessageBoxShow("E051", "排出事業者");
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                this.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
                this.cantxt_HaisyutuJigyoubaName.Focus();
                this.logic.isInputError = true;
                return;
            }
            //20150618 #5005 hoanghm end
            switch (this.logic.ChkJigyouba(cantxt_HaisyutuJigyoubaName, cantxt_HaisyutuGyousyaCd, "HAISHUTSU_NIZUMI_GENBA_KBN"))
            {
                case 0://正常

                    if (string.IsNullOrEmpty(ctxt_KohuTantou.Text))
                    {
                        //交付担当者を引用
                        ManifestoLogic.GetKoufuTantousha(cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, ctxt_KohuTantou);
                    }
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //排出事業場　削除
                    this.logic.HaisyutuGyoubaDel();

                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod();
            ////2014.05.19 by 胡 start
            //// <summary>
            //// シ排出事業場を選択後、システム設定のA～E票使用区分が「使用しない」となっていた場合でも、
            //// 現場のA～E票使用区分が「使用しない」となっていたときは、グレーアウトする。
            //this.logic.SetHenkyakuhiNyuuryokuEnabled();
            ////2014.05.19 by 胡 end
        }

        /// <summary>
        /// 排出事業場CD PopupAfterExecuteMethod
        /// </summary>
        // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start‏
        //public void cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod()
        public void cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod(ICustomControl sender = null)
        // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end
        {
            // 20140611 katen 不具合No.4469 start
            ////業者　設定‏
            //ManifestoLogic.SetAddrGyousha("All", cantxt_HaisyutuGyousyaCd, ctxt_HaisyutuGyousyaName1,
            //    cnt_HaisyutuGyousyaZip, cnt_HaisyutuGyousyaTel, ctxt_HaisyutuGyousyaAdd1,
            //    null, null, null
            //    , true, false, false, false);

            ////事業場　設定
            //this.mlogic.SetAddressJigyouba("All", cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, ctxt_HaisyutuJigyoubaName1, cnt_HaisyutuJigyoubaZip, cnt_HaisyutuJigyoubaTel, ctxt_HaisyutuJigyoubaAdd1, null
            //    , true, false, false, false);

            // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start‏
            ////業者　設定‏
            //this.logic.SetAddrGyousha("All", cantxt_HaisyutuGyousyaCd, new CustomTextBox[] { ctxt_HaisyutuGyousyaName1, ctxt_HaisyutuGyousyaName2 },
            //    cnt_HaisyutuGyousyaZip, cnt_HaisyutuGyousyaTel, new CustomTextBox[] { ctxt_HaisyutuGyousyaAdd1, ctxt_HaisyutuGyousyaAdd2 },
            //    null, null, null
            //    , true, false, false, false);
            ////事業場　設定
            //this.logic.SetAddressJigyouba("All", cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, new CustomTextBox[] { ctxt_HaisyutuJigyoubaName1, ctxt_HaisyutuJigyoubaName2 },
            //    cnt_HaisyutuJigyoubaZip, cnt_HaisyutuJigyoubaTel, new CustomTextBox[] { ctxt_HaisyutuJigyoubaAdd1, ctxt_HaisyutuJigyoubaAdd2 }, null
            //    , true, false, false, false);

            int ret = 0;
            if (sender != null)
            {
                if (this.bak_HaisyutuGyousyaCd != this.cantxt_HaisyutuGyousyaCd.Text)
                {
                    //業者　設定‏
                    ret = this.logic.SetAddrGyousha("All", cantxt_HaisyutuGyousyaCd, new CustomTextBox[] { ctxt_HaisyutuGyousyaName1, ctxt_HaisyutuGyousyaName2 },
                        cnt_HaisyutuGyousyaZip, cnt_HaisyutuGyousyaTel, new CustomTextBox[] { ctxt_HaisyutuGyousyaAdd1, ctxt_HaisyutuGyousyaAdd2 },
                        null, null, null
                        , true, false, false, false, this.logic.isNotNeedDeleteFlg);
                    if (ret == -1) { return; }
                }

                if (this.bak_HaisyutuGyousyaCd != this.cantxt_HaisyutuGyousyaCd.Text || this.bak_HaisyutuJigyoubaName != this.cantxt_HaisyutuJigyoubaName.Text)
                {
                    //事業場　設定
                    ret = this.logic.SetAddressJigyouba("All", cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, new CustomTextBox[] { ctxt_HaisyutuJigyoubaName1, ctxt_HaisyutuJigyoubaName2 },
                        cnt_HaisyutuJigyoubaZip, cnt_HaisyutuJigyoubaTel, new CustomTextBox[] { ctxt_HaisyutuJigyoubaAdd1, ctxt_HaisyutuJigyoubaAdd2 }, null
                        , true, false, false, false, this.logic.isNotNeedDeleteFlg);
                    if (ret == -1) { return; }
                }
            }
            else
            {
                //事業場　設定
                ret = this.logic.SetAddressJigyouba("All", cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, new CustomTextBox[] { ctxt_HaisyutuJigyoubaName1, ctxt_HaisyutuJigyoubaName2 },
                    cnt_HaisyutuJigyoubaZip, cnt_HaisyutuJigyoubaTel, new CustomTextBox[] { ctxt_HaisyutuJigyoubaAdd1, ctxt_HaisyutuJigyoubaAdd2 }, null
                    , true, false, false, false, this.logic.isNotNeedDeleteFlg);
                if (ret == -1) { return; }
            }
            // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end‏
            // 20140611 katen 不具合No.4469 end‏
            if (string.IsNullOrEmpty(ctxt_KohuTantou.Text))
            {
                //交付担当者を引用
                ManifestoLogic.GetKoufuTantousha(cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, ctxt_KohuTantou, this.logic.isNotNeedDeleteFlg);
            }
            //2014.05.19 by 胡 start
            // <summary>
            // シ排出事業場を選択後、システム設定のA～E票使用区分が「使用しない」となっていた場合でも、
            // 現場のA～E票使用区分が「使用しない」となっていたときは、グレーアウトする。
            this.logic.SetHenkyakuhiNyuuryokuEnabled();
            //2014.05.19 by 胡 end
        }

        // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start‏
        /// <summary>
        /// 排出事業場CD PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuJigyoubaName_PopupBeforeExecuteMethod()
        {
            this.bak_HaisyutuGyousyaCd = this.cantxt_HaisyutuGyousyaCd.Text;

            this.bak_HaisyutuJigyoubaName = this.cantxt_HaisyutuJigyoubaName.Text;
        }

        // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end

        // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 start‏
        /// <summary>
        /// 伝種区分 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_DenshuKbn_Validated(object sender, EventArgs e)
        {
            bool isChangedFlag = isChanged(sender);
            if (isChangedFlag)
            {
                switch (this.logic.ChkDenshuKbn(this.cantxt_DenshuKbn))
                {
                    case 0://正常
                        break;

                    case 1://空
                        this.lbl_No.Text = "連携番号";
                        break;

                    case 2://エラー
                        return;
                }
            }

            // 明細連携区分の制御
            this.MeisaiRenkeiKbnSetting(this.cantxt_DenshuKbn.Text);

            // 明細連携区分にシステム設定の値を設定する。
            if (this.cantxt_DenshuKbn.Text.Equals("001") || this.cantxt_DenshuKbn.Text.Equals("140"))
            {
                DataTable dt = null;
                // 初期処理
                dt = this.logic.SerchSysInfo();
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0].ItemArray[9].ToString()) == 2)
                    {
                        this.cantxt_Renkei_Mode.Text = "2";
                    }
                    else
                    {
                        this.cantxt_Renkei_Mode.Text = "1";
                    }
                }
            }
            else
            {
                this.cantxt_Renkei_Mode.Text = "1";
            }

            if (!this.chkRenkeiDenshuKbnFlg)
            {
                this.chkRenkeiDenshuKbnFlg = true;
                return;
            }

            // フォーカスを伝種区分、連携番号、明細行にいるかまいか

            Boolean isRelevantPart = this.cantxt_No.Focused || this.cantxt_DenshuKbn.Focused || this.cantxt_Meisaigyou.Focused;

            if (base.KeyEventKP != null)
            {
                if ((base.KeyEventKP.KeyCode == Keys.Tab || base.KeyEventKP.KeyCode == Keys.Enter) && base.KeyEventKP.Shift && this.cantxt_DenshuKbn.Focused)
                {
                    isRelevantPart = false;
                }
            }

            this.logic.setDetail(this.cantxt_DenshuKbn, this.cantxt_No, this.cantxt_Meisaigyou, isRelevantPart, isChanged(sender));
        }

        /// <summary>
        /// 伝種区分の値により、明細連携区分の活性/非活性を制御する。
        /// </summary>
        /// <param name="denshuKbn"></param>
        public void MeisaiRenkeiKbnSetting(string denshuKbn)
        {
            if (string.IsNullOrEmpty(denshuKbn)
                || (!denshuKbn.Equals("001") && !denshuKbn.Equals("140"))
                || WINDOW_TYPE.REFERENCE_WINDOW_FLAG.GetHashCode() == this.parameters.Mode)
            {
                this.Renkei_Mode_1.Enabled = false;
                this.Renkei_Mode_2.Enabled = false;
            }
            // #149374 紐付いているマニフェストが存在する場合は、連携項目を非活性に変更
            else if (WINDOW_TYPE.NEW_WINDOW_FLAG.GetHashCode() != this.parameters.Mode &&
                    mlogic.ChkRelation(SqlInt64.Parse(this.parameters.SystemId), this.logic.maniFlag))
            {
                this.cantxt_DenshuKbn.Enabled = false;
                this.cantxt_No.Enabled = false;
                this.cantxt_Meisaigyou.Enabled = false;
                this.Renkei_Mode_1.Enabled = false;
                this.Renkei_Mode_2.Enabled = false;
            }
            else
            {
                this.Renkei_Mode_1.Enabled = true;
                this.Renkei_Mode_2.Enabled = true;
            }
        }

        /// <summary>
        /// 連携番号 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_No_Validated(object sender, EventArgs e)
        {
            // フォーカスを伝種区分、連携番号、明細行にいるかまいか
            Boolean isRelevantPart = this.cantxt_No.Focused || this.cantxt_DenshuKbn.Focused || this.cantxt_Meisaigyou.Focused;
            if (base.KeyEventKP != null)
            {
                if ((base.KeyEventKP.KeyCode == Keys.Tab || base.KeyEventKP.KeyCode == Keys.Enter) && !base.KeyEventKP.Shift && this.cantxt_No.Focused)
                {
                    isRelevantPart = false;
                }
            }
            this.logic.setDetail(this.cantxt_DenshuKbn, this.cantxt_No, this.cantxt_Meisaigyou, isRelevantPart, isChanged(sender));
        }

        /// <summary>
        /// 連携明細行連携 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Meisaigyou_Validated(object sender, EventArgs e)
        {
            // フォーカスを伝種区分、連携番号、明細行にいるかまいか
            Boolean isRelevantPart = this.cantxt_No.Focused || this.cantxt_DenshuKbn.Focused || this.cantxt_Meisaigyou.Focused;
            if (base.KeyEventKP != null)
            {
                if ((base.KeyEventKP.KeyCode == Keys.Tab || base.KeyEventKP.KeyCode == Keys.Enter) && !base.KeyEventKP.Shift && this.cantxt_Meisaigyou.Focused)
                {
                    isRelevantPart = false;
                }
            }

            this.logic.setDetail(this.cantxt_DenshuKbn, this.cantxt_No, this.cantxt_Meisaigyou, isRelevantPart, isChanged(sender));
        }

        // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 end‏

        #endregion

        #region 実績タブ

        /// <summary>
        /// 混合種類 Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KongoCd_Enter(object sender, EventArgs e)
        {
            bak_KongouSyuruiCd = "";
            //修正モード時混合種類が既に入力されている場合、混合種類が変更された際のチェックに使用する変数に変更前のCDをセット
            if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                if (this.cantxt_KongoCd.Text != null && this.cantxt_KongoCd.Text != "")
                {
                    bak_KongouSyuruiCd = this.cantxt_KongoCd.Text;
                    bak_KongouSyuruiName = this.ctxt_KongoName.Text;
                }
            }
        }

        /// <summary>
        /// 混合種類 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KongoCd_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_KongoCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 混合種類 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KongoCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                if (mlogic.ChkRelation(SqlInt64.Parse(this.parameters.SystemId), this.logic.maniFlag))
                {
                    //メッセージ 「紐付済みの明細があるため、混合種類を上書きできません。マニ紐付を解除してから混合種類を入力してください。」
                    this.messageShowLogic.MessageBoxShow("E293");
                    if (bak_KongouSyuruiCd != "")
                    {
                        this.cantxt_KongoCd.Text = bak_KongouSyuruiCd;
                        this.ctxt_KongoName.Text = bak_KongouSyuruiName;
                    }
                    else
                    {
                        this.cantxt_KongoCd.Text = string.Empty;
                        this.ctxt_KongoName.Text = string.Empty;
                    }
                    return;
                }
            }

            if (!this.logic.SetKongouName()) { return; }
            if (!this.logic.SetSuu()) { return; }
            this.logic.SetJissekiTani();
            return;
        }

        /// <summary>
        /// 混合種類 PopupAfterExecuteMethod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cantxt_KongoCd_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 混合数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_JissekiSuryo_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 混合数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_JissekiSuryo_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            if (string.IsNullOrEmpty(this.cntxt_JissekiSuryo.Text))
            {
                return;
            }

            //入力時 端数はFWに任せる
            //double JissekiSuryo = Convert.ToDouble(this.cntxt_JissekiSuryo.Text.ToString().Replace(",", ""));
            //this.cntxt_JissekiSuryo.Text = this.mlogic.GetSuuryoRound(JissekiSuryo, this.logic.ManifestSuuryoFormatCD).ToString(this.logic.ManifestSuuryoFormat);

            this.logic.SetSuu();

            return;
        }

        /// <summary>
        /// 混合単位 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canTxt_JissekiTaniCd_Validating(object sender, CancelEventArgs e)
        {
            this.canTxt_JissekiTaniCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 混合単位 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canTxt_JissekiTaniCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }
            //単位CDチェック
            switch (this.logic.ChkJissekiTani())
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //
                    this.logic.DelJissekiTani();

                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            this.logic.SetJissekiTani();
        }

        /// <summary>
        /// 混合単位 PopupAfterExecuteMethod
        /// </summary>
        public void canTxt_JissekiTaniCd_PopupAfterExecuteMethod()
        {
        }

        #region グリッド

        /// <summary>
        /// 単位 PopupAfterExecuteMethod
        /// </summary>
        public void cdgrid_Jisseki_JissekiTaniCd_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 最終処分業者 PopupAfterExecuteMethod
        /// </summary>
        public void cdgrid_Jisseki_SaisyuGyosyaCd_PopupAfterExecuteMethod()
        {
            int iRow = this.cdgrid_Jisseki.CurrentCell.RowIndex;
            string gyoushaCd = (this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value == null) ? string.Empty :
                this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value.ToString();
            if (gyoushaCd != this.preValue)
            {
                cdgrid_Jisseki.Rows[iRow].Cells["SaisyuBasyoCd"].Value = null;
                cdgrid_Jisseki.Rows[iRow].Cells["SaisyuSyobunBasyo"].Value = null;
            }

            if (this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value == null ||
                this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value.ToString() == string.Empty)
            {
                return;
            }
            else
            {
                this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value =
                    this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value.ToString().PadLeft(6, '0').ToUpper();
            }
            this.logic.SetGridGyosya(iRow);
        }

        /// <summary>
        /// 最終処分業者 PopupBeforeExecuteMethod
        /// </summary>
        public void cdgrid_Jisseki_SaisyuGyosyaCd_PopupBeforeExecuteMethod()
        {
            int iRow = this.cdgrid_Jisseki.CurrentCell.RowIndex;
            this.preGyoushaCD = (this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value == null) ? string.Empty :
                this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value.ToString();
        }

        /// <summary>
        /// 最終処分場所 PopupAfterExecuteMethod
        /// </summary>
        public void cdgrid_Jisseki_SaisyuBasyoCd_PopupAfterExecuteMethod()
        {
            int iRow = this.cdgrid_Jisseki.CurrentCell.RowIndex;

            if (String.IsNullOrEmpty(Convert.ToString(this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuBasyoCd"].Value)))
            {
                return;
            }
            this.logic.SetGridJigyouba(iRow);

            if (this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value == null ||
                this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value.ToString() == string.Empty)
            {
                return;
            }
            else
            {
                this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value =
                    this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value.ToString().PadLeft(6, '0').ToUpper();
            }
            this.logic.SetGridGyosya(iRow);
        }

        /// <summary>
        /// ロストフォーカスチェック
        /// </summary>
        public bool cdgrid_Jisseki_LostFocusCheck(DataGridViewCellValidatingEventArgs e)
        {
            //if clear form then do not check valid on gird
            if (isClearForm)
            {
                return true;
            }

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            switch (col)
            {
                case (int)enumCols.HaikiCd://廃棄物種類CD
                    switch (this.logic.ChkGridHaiki(row))
                    {
                        case 0://正常
                            this.DgvFlg = true;
                            //return true; //リターンNG 再計算させる
                            break;

                        case 1://空
                            cdgrid_Jisseki.Rows[row].Cells["HaikiCd"].Value = null;
                            cdgrid_Jisseki.Rows[row].Cells["HaikiSyuruiName"].Value = null;
                            this.DgvFlg = true;
                            return true;

                        case 2://エラー
                            cdgrid_Jisseki.Rows[row].Cells["HaikiSyuruiName"].Value = null;
                            this.DgvFlg = false;
                            return false;
                    }
                    break;

                case (int)enumCols.HaikiSyuruiName://廃棄物種類名
                    this.DgvFlg = true;
                    return true;

                case (int)enumCols.HaikiNameCd://廃棄物の名称CD
                    if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells["HaikiNameCd"].Value)))
                    {
                        cdgrid_Jisseki.Rows[row].Cells["HaikiNameCd"].Value = null;
                        cdgrid_Jisseki.Rows[row].Cells["HaikiName"].Value = null;
                    }
                    else
                    {
                        cdgrid_Jisseki.Rows[row].Cells["HaikiNameCd"].Value = cdgrid_Jisseki.Rows[row].Cells["HaikiNameCd"].Value.ToString().PadLeft(6, '0').ToUpper();
                    }
                    break;

                case (int)enumCols.HaikiName://廃棄物の名前
                    this.DgvFlg = true;
                    return true;

                case (int)enumCols.NisugataCd://荷姿CD
                    if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells["NisugataCd"].Value)))
                    {
                        cdgrid_Jisseki.Rows[row].Cells["NisugataCd"].Value = null;
                        cdgrid_Jisseki.Rows[row].Cells["NisugataName"].Value = null;
                    }
                    else
                    {
                        cdgrid_Jisseki.Rows[row].Cells["NisugataCd"].Value = cdgrid_Jisseki.Rows[row].Cells["NisugataCd"].Value.ToString().PadLeft(2, '0').ToUpper();
                    }
                    break;

                case (int)enumCols.NisugataName://荷姿名
                    this.DgvFlg = true;
                    return true;

                case (int)enumCols.Wariai://割合(％)
                    if (this.cntxt_JissekiSuryo.Text == null || cntxt_JissekiSuryo.Text == String.Empty)
                    {
                    }
                    else
                    {
                        switch (this.logic.ChkGridWariai(row))
                        {
                            case 0://正常
                                break;

                            case 1://空
                                this.DgvFlg = true;
                                return true;

                            case 2://エラー
                                this.DgvFlg = false;
                                return false;
                        }
                    }
                    // 20140707 ria EV005128 一度入力した割合をdelete又はbackspaceにてクリアするとフォーカスアウトした際割合に0がセットされてしまう。 start
                    if (cdgrid_Jisseki.Rows[row].Cells["Wariai"].Value != null)
                    {
                        cdgrid_Jisseki.Rows[row].Cells["Wariai"].Value
                            = Convert.ToDecimal(cdgrid_Jisseki.Rows[row].Cells["Wariai"].Value).ToString();
                    }
                    // 20140707 ria EV005128 一度入力した割合をdelete又はbackspaceにてクリアするとフォーカスアウトした際割合に0がセットされてしまう。 end
                    break;

                case (int)enumCols.Suryo://数量
                    switch (this.logic.ChkGridSuryo(row))
                    {
                        case 0://正常
                            break;

                        case 1://空
                            this.DgvFlg = true;
                            return true;

                        case 2://エラー
                            this.DgvFlg = false;
                            return false;
                    }
                    cdgrid_Jisseki.Rows[row].Cells["Suryo"].Value
                        = Convert.ToDecimal(cdgrid_Jisseki.Rows[row].Cells["Suryo"].Value).ToString(this.logic.ManifestSuuryoFormat);
                    break;

                case (int)enumCols.TaniCd://単位CD
                    switch (this.logic.ChkGridTaniCd(row))
                    {
                        case 0://正常
                            break;

                        case 1://空
                            cdgrid_Jisseki.Rows[row].Cells["TaniCd"].Value = null;
                            cdgrid_Jisseki.Rows[row].Cells["TaniName"].Value = null;
                            this.DgvFlg = true;
                            return true;

                        case 2://エラー
                            cdgrid_Jisseki.Rows[row].Cells["TaniName"].Value = null;
                            this.DgvFlg = false;
                            return false;
                    }
                    break;

                case (int)enumCols.TaniName://単位名
                    this.DgvFlg = true;
                    return true;

                case (int)enumCols.KansangoSuryo://換算後数量
                    switch (this.logic.ChkGridKansangoSuryo(row))
                    {
                        case 0://正常
                            break;

                        case 1://空
                            break;

                        case 2://エラー
                            this.DgvFlg = false;
                            return false;
                    }
                    if (cdgrid_Jisseki.Rows[row].Cells["KansangoSuryo"].Value != null
                        && !string.IsNullOrEmpty(cdgrid_Jisseki.Rows[row].Cells["KansangoSuryo"].Value.ToString()))
                    {
                        cdgrid_Jisseki.Rows[row].Cells["KansangoSuryo"].Value
                            = Convert.ToDouble(cdgrid_Jisseki.Rows[row].Cells["KansangoSuryo"].Value).ToString(this.logic.ManifestSuuryoFormat);
                    }
                    else
                    {
                        cdgrid_Jisseki.Rows[row].Cells["KansangoSuryo"].Value = string.Empty;
                        cdgrid_Jisseki.Rows[row].Cells["GenyoyugoTotalSuryo"].Value = string.Empty;
                    }
                    break;

                case (int)enumCols.GenyoyugoTotalSuryo://減容後数量
                    switch (this.logic.ChkGridGenyoyugoTotalSuryo(row))
                    {
                        case 0://正常
                            break;

                        case 1://空
                            this.DgvFlg = true;
                            return true;

                        case 2://エラー
                            this.DgvFlg = false;
                            return false;
                    }
                    cdgrid_Jisseki.Rows[row].Cells["GenyoyugoTotalSuryo"].Value
                        = Convert.ToDecimal(cdgrid_Jisseki.Rows[row].Cells["GenyoyugoTotalSuryo"].Value).ToString(this.logic.ManifestSuuryoFormat);
                    break;

                case (int)enumCols.SyobunCd://処分方法CD
                    if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells["SyobunCd"].Value)))
                    {
                        cdgrid_Jisseki.Rows[row].Cells["SyobunCd"].Value = null;
                        cdgrid_Jisseki.Rows[row].Cells["SyobunName"].Value = null;
                    }
                    else
                    {
                        cdgrid_Jisseki.Rows[row].Cells["SyobunCd"].Value = cdgrid_Jisseki.Rows[row].Cells["SyobunCd"].Value.ToString().PadLeft(3, '0').ToUpper();
                    }
                    break;

                case (int)enumCols.SyobunName://処分方法名
                    this.DgvFlg = true;
                    return true;

                case (int)enumCols.SyobunEndDate://処分終了日
                    if (string.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[col].Value)) || string.IsNullOrEmpty(Convert.ToString(this.cdate_KohuDate.Value)))
                    {
                        break;
                    }
                    if (Convert.ToDateTime(cdgrid_Jisseki.Rows[row].Cells[col].Value) < Convert.ToDateTime(this.cdate_KohuDate.Value))
                    {
                        this.messageShowLogic.MessageBoxShow("E281", "処分終了年月日", "交付年月日");
                        this.DgvFlg = false;
                        return false;
                    }
                    break;

                case (int)enumCols.SaisyuSyobunEndDate://最終処分終了日
                    if (string.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[col].Value)) || string.IsNullOrEmpty(Convert.ToString(this.cdate_KohuDate.Value)))
                    {
                        break;
                    }
                    if (Convert.ToDateTime(cdgrid_Jisseki.Rows[row].Cells[col].Value) < Convert.ToDateTime(this.cdate_KohuDate.Value))
                    {
                        this.messageShowLogic.MessageBoxShow("E281", "最終処分終了年月日", "交付年月日");
                        this.DgvFlg = false;
                        return false;
                    }
                    break;

                case (int)enumCols.SaisyuGyosyaCd://最終処分業者CD
                    switch (this.logic.ChkGridGyosya(row))
                    {
                        case 0://正常
                            break;

                        case 1://空
                            cdgrid_Jisseki.Rows[row].Cells["SaisyuGyosyaName"].Value = null;
                            cdgrid_Jisseki.Rows[row].Cells["SaisyuBasyoCd"].Value = null;
                            cdgrid_Jisseki.Rows[row].Cells["SaisyuSyobunBasyo"].Value = null;
                            this.DgvFlg = true;
                            return true;

                        case 2://エラー
                            cdgrid_Jisseki.Rows[row].Cells["SaisyuGyosyaName"].Value = null;
                            this.DgvFlg = false;
                            return false;
                    }
                    cdgrid_Jisseki_SaisyuGyosyaCd_PopupAfterExecuteMethod();
                    break;

                case (int)enumCols.SaisyuGyosyaName://最終処分業者名
                    this.DgvFlg = true;
                    return true;

                case (int)enumCols.SaisyuBasyoCd://最終処分場所CD
                    switch (this.logic.ChkGridJigyouba(row))
                    {
                        case 0://正常
                            this.logic.SetAddressGyoushaForDgv(row);
                            break;

                        case 1://空
                            cdgrid_Jisseki.Rows[row].Cells["SaisyuSyobunBasyo"].Value = null;
                            //this.messageShowLogic.MessageBoxShow("E034", "処分事業者");
                            this.DgvFlg = true;
                            return true;

                        case 2://エラー
                            cdgrid_Jisseki.Rows[row].Cells["SaisyuSyobunBasyo"].Value = null;
                            this.DgvFlg = false;
                            return false;
                    }
                    cdgrid_Jisseki_SaisyuBasyoCd_PopupAfterExecuteMethod();
                    break;

                case (int)enumCols.SaisyuSyobunBasyo://最終処分場所名
                    this.DgvFlg = true;
                    return true;

                case (int)enumCols.NijiManiNo://二次マニ交付番号
                    this.DgvFlg = true;
                    return true;
            }

            if (e.RowIndex == 0 && col != (int)enumCols.TaniCd
                && (this.cdgrid_Jisseki.Rows[e.RowIndex].Cells[(int)enumCols.TaniCd].Value == null
                || string.IsNullOrEmpty(this.cdgrid_Jisseki.Rows[e.RowIndex].Cells[(int)enumCols.TaniCd].Value.ToString())))
            {
                // 1行目限定で単位以外の項目の場合は混合単位を設定する
                // ※新規モード時にcdgrid_Jisseki_DefaultValuesNeededが2行目以降しか動かない場合の対策
                this.cdgrid_Jisseki.Rows[e.RowIndex].Cells[(int)enumCols.TaniCd].Value = String.Format("{0:00}", this.canTxt_JissekiTaniCd.Text);
                this.cdgrid_Jisseki.Rows[e.RowIndex].Cells[(int)enumCols.TaniName].Value = this.logic.GetTaniName(this.canTxt_JissekiTaniCd.Text);
            }

            if (col < (int)enumCols.KansangoSuryo) //換算後終了含め、左側の変更時のみ自動計算 ※DBアクセスあるので高頻度呼び出しに注意
            {
                //換算値
                if (!this.logic.SetKansanti(row)) { return false; }
            }

            if (col < (int)enumCols.GenyoyugoTotalSuryo || col == (int)enumCols.SyobunCd) //換算後終了含め、左側の変更時のみ自動計算
            {
                //減容
                if (!this.logic.SetGenyouti(row)) { return false; }
            }

            //合計
            if (!this.logic.SetTotal()) { return false; }
            this.logic.SetMaxSyobunEndDate();
            this.logic.SetMaxSaisyuSyobunEndDate();

            return true;
        }

        /// <summary>
        /// グリッド編集モード開始イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgrid_Jisseki_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            if (rowIndex >= 0)
            {
                switch (columnIndex)
                {
                    case (int)enumCols.HaikiCd://廃棄物種類CD
                        this.preValue = this.cdgrid_Jisseki.Rows[rowIndex].Cells[columnIndex].Value == null ? string.Empty : this.cdgrid_Jisseki.Rows[rowIndex].Cells[columnIndex].Value.ToString();
                        break;
                    case (int)enumCols.SaisyuGyosyaCd://最終処分業者CD
                        this.preValue = this.cdgrid_Jisseki.Rows[rowIndex].Cells[columnIndex].Value == null ? string.Empty : this.cdgrid_Jisseki.Rows[rowIndex].Cells[columnIndex].Value.ToString();
                        break;
                    case (int)enumCols.SaisyuBasyoCd://最終処分場所CD
                        this.preValue = this.cdgrid_Jisseki.Rows[rowIndex].Cells[columnIndex].Value == null ? string.Empty : this.cdgrid_Jisseki.Rows[rowIndex].Cells[columnIndex].Value.ToString();
                        break;
                }
            }

            //20151006 hoanghm #12748 start
            switch (e.ColumnIndex)
            {
                case (int)enumCols.HaikiCd:
                case (int)enumCols.HaikiNameCd:
                case (int)enumCols.NisugataCd:
                case (int)enumCols.Wariai:
                case (int)enumCols.Suryo:
                case (int)enumCols.TaniCd:
                case (int)enumCols.KansangoSuryo:
                case (int)enumCols.SyobunCd:
                case (int)enumCols.SaisyuGyosyaCd:
                case (int)enumCols.SaisyuBasyoCd:
                    this.cdgrid_Jisseki.ImeMode = ImeMode.Disable;
                    break;

                default:
                    this.cdgrid_Jisseki.ImeMode = ImeMode.Off;
                    break;
            }
            //20151006 hoanghm #12748 end
        }

        /// <summary>
        /// グリッド編集モード終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgrid_Jisseki_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (row >= 0)
            {
                // 20140617 ria EV004757 実績タブに値を入力しなくてもフォーカス移動だけで換算後数量が計算されてしまう start
                ////換算後数量
                //if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value)))
                //{
                //    cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value = (0).ToString(this.logic.ManifestSuuryoFormat);
                //}

                ////減算後数量
                //if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value)))
                //{
                //    cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value = (0).ToString(this.logic.ManifestSuuryoFormat);
                //}
                if (!String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value))
                     && !String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value))
                     && !String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value)))
                {
                    //換算後数量
                    if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value)))
                    {
                        cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value = (0).ToString(this.logic.ManifestSuuryoFormat);
                    }

                    //減算後数量
                    if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value)))
                    {
                        cdgrid_Jisseki.Rows[row].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value = (0).ToString(this.logic.ManifestSuuryoFormat);
                    }
                }
                // 20140617 ria EV004757 実績タブに値を入力しなくてもフォーカス移動だけで換算後数量が計算されてしまう end

                switch (col)
                {
                    case (int)enumCols.SaisyuGyosyaCd://最終処分業者CD
                        string saisyuGyosyaCd = this.cdgrid_Jisseki.Rows[row].Cells[col].Value == null ? string.Empty : this.cdgrid_Jisseki.Rows[row].Cells[col].Value.ToString();
                        if (!saisyuGyosyaCd.Equals(preValue))
                        {
                            // 最終処分業者CD変更時は最終処分場所をクリアする
                            this.cdgrid_Jisseki.Rows[row].Cells[(int)enumCols.SaisyuBasyoCd].Value = null;
                            this.cdgrid_Jisseki.Rows[row].Cells[(int)enumCols.SaisyuSyobunBasyo].Value = null;
                        }
                        break;
                }
            }

            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL start
            //if (this.logic.maniFlag == 2 && !cdgrid_Jisseki.Rows[row].IsNewRow)
            //{
            //    this.cdgrid_Jisseki.AllowUserToAddRows = false;
            //}
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL end

            return;
        }

        #endregion

        #endregion

        #region 原本タブ

        /// <summary>
        /// 産業廃棄物の種類 単位 テキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_SanpaiTani_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 産業廃棄物の種類 単位 「t」チェックボタン チェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.crdo_SanpaiSyuruiT.Checked)
            {
                this.crdo_SanpaiSyuruiKg.Checked = false;
                this.crdo_SanpaiSyuruiM3.Checked = false;
                this.crdo_SanpaiSyuruiRittoru.Checked = false;
                this.cntxt_SanpaiTani.Text = "1";
                this.ctxt_TaniName.Text = logic.GetTaniName("1");
            }
            else if (
                this.crdo_SanpaiSyuruiKg.Checked == false &&
                this.crdo_SanpaiSyuruiM3.Checked == false &&
                this.crdo_SanpaiSyuruiRittoru.Checked == false
                )
            {
                this.cntxt_SanpaiTani.Text = String.Empty;
                this.ctxt_TaniName.Text = String.Empty;
            }
        }

        /// <summary>
        /// 産業廃棄物の種類 単位 「Kg」チェックボタン チェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.crdo_SanpaiSyuruiKg.Checked)
            {
                this.crdo_SanpaiSyuruiT.Checked = false;
                this.crdo_SanpaiSyuruiM3.Checked = false;
                this.crdo_SanpaiSyuruiRittoru.Checked = false;
                // 20140618 katen EV004605 入力画面と一覧で「ｋｇ」と「」が逆に表示されてしまう start
                //this.cntxt_SanpaiTani.Text = "2";
                this.cntxt_SanpaiTani.Text = "3";
                // 20140618 katen EV004605 入力画面と一覧で「ｋｇ」と「」が逆に表示されてしまう end
                this.ctxt_TaniName.Text = logic.GetTaniName("3");
            }
            else if (
                this.crdo_SanpaiSyuruiT.Checked == false &&
                this.crdo_SanpaiSyuruiM3.Checked == false &&
                this.crdo_SanpaiSyuruiRittoru.Checked == false
                )
            {
                this.cntxt_SanpaiTani.Text = String.Empty;
                this.ctxt_TaniName.Text = String.Empty;
            }
        }

        /// <summary>
        /// 産業廃棄物の種類 単位 「㎥」チェックボタン チェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.crdo_SanpaiSyuruiM3.Checked)
            {
                this.crdo_SanpaiSyuruiT.Checked = false;
                this.crdo_SanpaiSyuruiKg.Checked = false;
                this.crdo_SanpaiSyuruiRittoru.Checked = false;
                // 20140618 katen EV004605 入力画面と一覧で「ｋｇ」と「」が逆に表示されてしまう start
                //this.cntxt_SanpaiTani.Text = "3";
                this.cntxt_SanpaiTani.Text = "2";
                // 20140618 katen EV004605 入力画面と一覧で「ｋｇ」と「」が逆に表示されてしまう end
                this.ctxt_TaniName.Text = logic.GetTaniName("2");
            }
            else if (
                this.crdo_SanpaiSyuruiT.Checked == false &&
                this.crdo_SanpaiSyuruiKg.Checked == false &&
                this.crdo_SanpaiSyuruiRittoru.Checked == false
                )
            {
                this.cntxt_SanpaiTani.Text = String.Empty;
                this.ctxt_TaniName.Text = String.Empty;
            }
        }

        /// <summary>
        /// 産業廃棄物の種類 単位 「ℓ」チェックボタン チェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.crdo_SanpaiSyuruiRittoru.Checked)
            {
                this.crdo_SanpaiSyuruiT.Checked = false;
                this.crdo_SanpaiSyuruiKg.Checked = false;
                this.crdo_SanpaiSyuruiM3.Checked = false;
                this.cntxt_SanpaiTani.Text = "4";
                this.ctxt_TaniName.Text = logic.GetTaniName("4");
            }
            else if (
                this.crdo_SanpaiSyuruiT.Checked == false &&
                this.crdo_SanpaiSyuruiKg.Checked == false &&
                this.crdo_SanpaiSyuruiM3.Checked == false
                )
            {
                this.cntxt_SanpaiTani.Text = String.Empty;
                this.ctxt_TaniName.Text = String.Empty;
            }
        }

        /// <summary>
        /// 産業廃棄物の名称 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SanpaiSyuruiCd_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_SanpaiSyuruiCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 産業廃棄物の名称 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SanpaiSyuruiCd_PopupAfterExecuteMethod()
        {
        }

        #region 安定型品目

        /// <summary>
        /// 安定型品目 01 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo01_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 01 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo01_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Antei01, this.cntxt_AnteiSuryo01);
        }

        /// <summary>
        /// 安定型品目 02 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo02_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 02 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo02_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Antei02, this.cntxt_AnteiSuryo02);
        }

        /// <summary>
        /// 安定型品目 03 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo03_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 03 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo03_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Antei03, this.cntxt_AnteiSuryo03);
        }

        /// <summary>
        /// 安定型品目 04 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo04_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 04 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo04_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Antei04, this.cntxt_AnteiSuryo04);
        }

        /// <summary>
        /// 安定型品目 05 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo05_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 05 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo05_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Antei05, this.cntxt_AnteiSuryo05);
        }

        /// <summary>
        /// 安定型品目 06 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo06_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 06 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo06_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Antei06, this.cntxt_AnteiSuryo06);
        }

        /// <summary>
        /// 安定型品目 07 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo07_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 07 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo07_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Antei07, this.cntxt_AnteiSuryo07);
        }

        /// <summary>
        /// 安定型品目 08 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo08_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 08 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiSuryo08_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Antei08, this.cntxt_AnteiSuryo08);
        }

        /// <summary>
        /// 安定型品目 Free01 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_AnteiFreeCd01_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_AnteiFreeCd01_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 安定型品目 Free01 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_AnteiFreeCd01_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_AnteiFreeCd01, ctxt_AnteiFreeName01);
        }

        /// <summary>
        /// 安定型品目 Free01 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_AnteiFreeCd01_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 安定型品目 Free01 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiFreeSuryo01_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 Free01 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiFreeSuryo01_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_AnteiFree01, this.cntxt_AnteiFreeSuryo01);
        }

        /// <summary>
        /// 安定型品目 Free02 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_AnteiFreeCd02_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_AnteiFreeCd02_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 安定型品目 Free02 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_AnteiFreeCd02_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_AnteiFreeCd02, ctxt_AnteiFreeName02);
        }

        /// <summary>
        /// 安定型品目 Free02 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_AnteiFreeCd02_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 安定型品目 Free02 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiFreeSuryo02_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 Free02 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiFreeSuryo02_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_AnteiFree02, this.cntxt_AnteiFreeSuryo02);
        }

        /// <summary>
        /// 安定型品目 Free03 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_AnteiFreeCd03_Validating(object sender, CancelEventArgs e)
        {
            cantxt_AnteiFreeCd03_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 安定型品目 Free03 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_AnteiFreeCd03_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_AnteiFreeCd03, ctxt_AnteiFreeName03);
        }

        /// <summary>
        /// 安定型品目 Free03 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_AnteiFreeCd03_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 安定型品目 Free03 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiFreeSuryo03_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 Free03 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiFreeSuryo03_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_AnteiFree03, this.cntxt_AnteiFreeSuryo03);
        }

        /// <summary>
        /// 安定型品目 Free04 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_AnteiFreeCd04_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_AnteiFreeCd04_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 安定型品目 Free04 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_AnteiFreeCd04_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_AnteiFreeCd04, ctxt_AnteiFreeNam04);
        }

        /// <summary>
        /// 安定型品目 Free04 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_AnteiFreeCd04_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 安定型品目 Free04 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiFreeSuryo04_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 安定型品目 Free04 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_AnteiFreeSuryo04_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_AnteiFree04, this.cntxt_AnteiFreeSuryo04);
        }

        #endregion

        #region 管理型品目

        /// <summary>
        /// 管理型品目 11 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo11_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 管理型品目 11 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo11_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Kanri01, this.cntxt_KanriSuryo11);
        }

        /// <summary>
        /// 管理型品目 12 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo12_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 管理型品目 12 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo12_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Kanri02, this.cntxt_KanriSuryo12);
        }

        /// <summary>
        /// 管理型品目 13 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo13_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 管理型品目 13 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo13_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Kanri03, this.cntxt_KanriSuryo13);
        }

        /// <summary>
        /// 管理型品目 14 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo14_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 管理型品目 14 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo14_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Kanri04, this.cntxt_KanriSuryo14);
        }

        /// <summary>
        /// 管理型品目 15 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo15_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 管理型品目 15 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo15_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Kanri05, this.cntxt_KanriSuryo15);
        }

        /// <summary>
        /// 管理型品目 16 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo16_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 管理型品目 16 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo16_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Kanri06, this.cntxt_KanriSuryo16);
        }

        /// <summary>
        /// 管理型品目 17 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo17_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 管理型品目 17 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriSuryo17_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Kanri17, this.cntxt_KanriSuryo17);
        }

        /// <summary>
        /// 管理型品目 Free01 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KanriFreeCd01_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_KanriFreeCd01_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 管理型品目 Free01 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KanriFreeCd01_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_KanriFreeCd01, ctxt_KanriFreeName01);
        }

        /// <summary>
        /// 管理型品目 Free01 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_KanriFreeCd01_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 管理型品目 Free01 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriFreeSuryo01_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 管理型品目 Free01 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriFreeSuryo01_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_KanriFree01, this.cntxt_KanriFreeSuryo01);
        }

        /// <summary>
        /// 管理型品目 Free02 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KanriFreeCd02_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_KanriFreeCd02_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 管理型品目 Free02 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KanriFreeCd02_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_KanriFreeCd02, ctxt_KanriFreeName02);
        }

        /// <summary>
        /// 管理型品目 Free02 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_KanriFreeCd02_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 管理型品目 Free02 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriFreeSuryo02_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 管理型品目 Free02 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriFreeSuryo02_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_KanriFree02, this.cntxt_KanriFreeSuryo02);
        }

        /// <summary>
        /// 管理型品目 Free03 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KanriFreeCd03_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_KanriFreeCd03_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 管理型品目 Free03 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KanriFreeCd03_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_KanriFreeCd03, ctxt_KanriFreeName03);
        }

        /// <summary>
        /// 管理型品目 Free03 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_KanriFreeCd03_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 管理型品目 Free03 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriFreeSuryo03_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 管理型品目 Free03 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KanriFreeSuryo03_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_KanriFree03, this.cntxt_KanriFreeSuryo03);
        }

        #endregion

        #region 特別管理産廃

        /// <summary>
        /// 特別管理産廃 21 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuSuryo21_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 特別管理産廃 21 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuSuryo21_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_Tokubetu21, this.cntxt_TokubetuSuryo21);
        }

        /// <summary>
        /// 特別管理産廃 Free01 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TokubetuFreeCd01_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_TokubetuFreeCd01_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 特別管理産廃 Free01 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TokubetuFreeCd01_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_TokubetuFreeCd01, cantxt_TokubetuFreeName01);
        }

        /// <summary>
        /// 特別管理産廃 Free01 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_TokubetuFreeCd01_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 特別管理産廃 Free01 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuFreeSuryo01_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 特別管理産廃 Free01 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuFreeSuryo01_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_TokubetuFree01, this.cntxt_TokubetuFreeSuryo01);
        }

        /// <summary>
        /// 特別管理産廃 Free02 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TokubetuFreeCd02_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_TokubetuFreeCd02_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 特別管理産廃 Free02 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TokubetuFreeCd02_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_TokubetuFreeCd02, cantxt_TokubetuFreeName02);
        }

        /// <summary>
        /// 特別管理産廃 Free02 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_TokubetuFreeCd02_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 特別管理産廃 Free02 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuFreeSuryo02_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 特別管理産廃 Free02 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuFreeSuryo02_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_TokubetuFree02, this.cntxt_TokubetuFreeSuryo02);
        }

        /// <summary>
        /// 特別管理産廃 Free03 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TokubetuFreeCd03_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_TokubetuFreeCd03_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 特別管理産廃 Free03 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TokubetuFreeCd03_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_TokubetuFreeCd03, cantxt_TokubetuFreeName03);
        }

        /// <summary>
        /// 特別管理産廃 Free03 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_TokubetuFreeCd03_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 特別管理産廃 Free03 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuFreeSuryo03_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 特別管理産廃 Free03 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuFreeSuryo03_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_TokubetuFree03, this.cntxt_TokubetuFreeSuryo03);
        }

        /// <summary>
        /// 特別管理産廃 Free04 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TokubetuFreeCd04_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_TokubetuFreeCd04_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 特別管理産廃 Free04 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TokubetuFreeCd04_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_TokubetuFreeCd04, cantxt_TokubetuFreeName04);
        }

        /// <summary>
        /// 特別管理産廃 Free04 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_TokubetuFreeCd04_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 特別管理産廃 Free04 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuFreeSuryo04_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 特別管理産廃 Free04 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuFreeSuryo04_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_TokubetuFree04, this.cntxt_TokubetuFreeSuryo04);
        }

        /// <summary>
        /// 特別管理産廃 Free05 品目 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TokubetuFreeCd05_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_TokubetuFreeCd05_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 特別管理産廃 Free05 品目 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TokubetuFreeCd05_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkHaiki(cantxt_TokubetuFreeCd05, cantxt_TokubetuFreeName05);
        }

        /// <summary>
        /// 特別管理産廃 Free05 品目 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_TokubetuFreeCd05_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 特別管理産廃 Free05 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuFreeSuryo05_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 特別管理産廃 Free05 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TokubetuFreeSuryo05_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.SetTotalJyuuryo(this.cckb_TokubetuFree05, this.cntxt_TokubetuFreeSuryo05);
        }

        #endregion

        #region 形状

        /// <summary>
        /// 形状 Free01 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KeijyoFreeCd01_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_KeijyoFreeCd01_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 形状 Free01 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_KeijyoFreeCd01_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 形状 Free02 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KeijyoFreeCd02_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_KeijyoFreeCd02_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 形状 Free02 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_KeijyoFreeCd02_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 形状 Free03 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KeijyoFreeCd03_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_KeijyoFreeCd03_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 形状 Free03 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_KeijyoFreeCd03_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 形状 Free04 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KeijyoFreeCd04_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_KeijyoFreeCd04_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 形状 Free04 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_KeijyoFreeCd04_PopupAfterExecuteMethod()
        {
        }

        #endregion

        #region 荷姿

        /// <summary>
        /// 荷姿 Free01 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_NisugataFreeCd01_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_NisugataFreeCd01_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 荷姿 Free01 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_NisugataFreeCd01_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 荷姿 Free02 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_NisugataFreeCd02_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_NisugataFreeCd02_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 荷姿 Free02 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_NisugataFreeCd02_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 荷姿 Free03 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_NisugataFreeCd03_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_NisugataFreeCd03_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 荷姿 Free03 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_NisugataFreeCd03_PopupAfterExecuteMethod()
        {
        }

        #endregion

        #endregion



        #region 最終処分(埋立処分、再生等)の場所(予定) 所在地/名称

        /// <summary>
        /// 最終処分(埋立処分、再生等)の場所(予定) 所在地/名称 最終処分の業者CD Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuGyousyaCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_SaisyuGyousyaCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 最終処分(埋立処分、再生等)の場所(予定) 所在地/名称 最終処分の業者CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuGyousyaCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            //最終処分の場所業者CDチェック
            switch (this.logic.ChkGyosya(cantxt_SaisyuGyousyaCd, "SHOBUN_NIOROSHI_GYOUSHA_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //最終処分の場所削除
                    this.logic.SaisyuSyobunDel("cantxt_SaisyuGyousyaCd");

                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_SaisyuGyousyaCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 最終処分(埋立処分、再生等)の場所(予定) 所在地/名称 最終処分の業者CD PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SaisyuGyousyaCd_PopupAfterExecuteMethod()
        {
            if (this.cantxt_SaisyuGyousyaCd.Text != this.preGyoushaCD)
            {
                this.ccbx_SaisyuTyoubo.Checked = false;
                this.ccbx_SaisyuKisai.Checked = true;

                // 最終処分場所を初期化する
                this.logic.SaisyuSyobunDel("cantxt_SaisyuGyousyaNameCd");
            }
        }

        /// <summary>
        /// 最終処分(埋立処分、再生等)の場所(予定) 所在地/名称 最終処分の業者CD PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_SaisyuGyousyaCd_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCD = this.cantxt_SaisyuGyousyaCd.Text;
        }

        /// <summary>
        /// 最終処分(埋立処分、再生等)の場所(予定) 所在地/名称 名称 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuGyousyaNameCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_SaisyuGyousyaNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 最終処分(埋立処分、再生等)の場所(予定) 所在地/名称 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuGyousyaNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            //20150630 #5005 hoanghm start
            //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
            if (!string.IsNullOrEmpty(cantxt_SaisyuGyousyaNameCd.Text) && string.IsNullOrEmpty(cantxt_SaisyuGyousyaCd.Text))
            {
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                this.messageShowLogic.MessageBoxShow("E051", "最終処分業者");
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                this.cantxt_SaisyuGyousyaNameCd.Text = string.Empty;
                this.cantxt_SaisyuGyousyaNameCd.Focus();
                this.logic.isInputError = true;
                return;
            }
            //20150630 #5005 hoanghm end

            //最終処分の場所　名称チェック
            switch (this.logic.ChkJigyouba(cantxt_SaisyuGyousyaNameCd, cantxt_SaisyuGyousyaCd, "SAISHUU_SHOBUNJOU_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //最終処分の場所削除
                    this.logic.SaisyuSyobunDel("cantxt_SaisyuGyousyaNameCd");
                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_SaisyuGyousyaNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 最終処分(埋立処分、再生等)の場所(予定) 所在地/名称 名称 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SaisyuGyousyaNameCd_PopupAfterExecuteMethod()
        {
            this.ccbx_SaisyuTyoubo.Checked = false;
            this.ccbx_SaisyuKisai.Checked = true;

            //排出事業場　設定
            this.mlogic.SetAddressJigyouba("All", cantxt_SaisyuGyousyaCd, cantxt_SaisyuGyousyaNameCd, cantxt_SaisyuGyousyaName,
                null, null, ctxt_SaisyuGyousyaAdd, null
                , false, true, false, false);
        }

        #endregion

        #region 運搬受託者(収集運搬業者) (1)

        /// <summary>
        /// 運搬受託者(収集運搬業者) (1) 名称 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyutaku1NameCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (1) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyutaku1NameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            switch (this.logic.ChkGyosya(cantxt_UnpanJyutaku1NameCd, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    // 20140604 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す(処分も) start
                    //運搬受託者(収集運搬業者) (1)  削除
                    this.logic.UnpanJyutaku1Del();

                    //運搬の受託(１)  削除
                    this.logic.UnpanJyuCd1Del();
                    // 20140604 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す(処分も) end
                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (1) 名称 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod()
        {
            //排出業者　設定
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku1NameCd, cantxt_UnpanJyutaku1Name,
                cnt_UnpanJyutaku1Zip, cnt_UnpanJyutaku1Tel, ctxt_UnpanJyutaku1Add,
                "Part1", cantxt_UnpanJyuCd1, ctxt_UnpanJyuName1
                , false, false, true, false, this.logic.isNotNeedDeleteFlg);
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (1) 収集･運搬車輌番号 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku1SyaNo_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_Jyutaku1SyaNo_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (1) 収集･運搬車輌番号 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku1SyaNo_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            switch (this.logic.ChkCarData(cantxt_UnpanJyutaku1NameCd, cantxt_Jyutaku1Syasyu, ctxt_Jyutaku1Syasyu, cantxt_Jyutaku1SyaNo, ctxt_Jyutaku1SyaNo))
            {
                case 0://正常
                    break;

                case 1://空
                    this.ctxt_Jyutaku1SyaNo.Text = string.Empty;
                    return;

                case 2://エラー
                    this.ctxt_Jyutaku1SyaNo.Text = string.Empty;
                    return;
            }
            cantxt_Jyutaku1SyaNo_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (1) 収集･運搬車輌番号 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Jyutaku1SyaNo_PopupAfterExecuteMethod()
        {
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku1NameCd, cantxt_UnpanJyutaku1Name,
                cnt_UnpanJyutaku1Zip, cnt_UnpanJyutaku1Tel, ctxt_UnpanJyutaku1Add,
                null, null, null
                , false, false, true, false);
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (1) 車種 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku1Syasyu_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_Jyutaku1Syasyu_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (1) 車種 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Jyutaku1Syasyu_PopupAfterExecuteMethod()
        {
        }

        #endregion

        #region 運搬受託者(収集運搬業者) (2)

        /// <summary>
        /// 運搬受託者(収集運搬業者) (2) 名称 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyutaku2NameCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyutaku2NameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (2) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyutaku2NameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            switch (this.logic.ChkGyosya(cantxt_UnpanJyutaku2NameCd, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    // 20140604 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す(処分も) start
                    //運搬受託者(収集運搬業者) (1)  削除
                    this.logic.UnpanJyutaku2Del();

                    //運搬の受託(１)  削除
                    this.logic.UnpanJyuCd2Del();
                    // 20140604 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す(処分も) end
                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_UnpanJyutaku2NameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (2) 名称 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyutaku2NameCd_PopupAfterExecuteMethod()
        {
            //排出業者　設定
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku2NameCd, cantxt_UnpanJyutaku2Name,
                cnt_UnpanJyutaku2Zip, cnt_UnpanJyutaku2Tel, ctxt_UnpanJyutaku2Add,
                "Part1", cantxt_UnpanJyuCd2, ctxt_UnpanJyuName2
                , false, false, true, false);
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (2) 収集･運搬車輌番号 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku2SyaNo_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_Jyutaku2SyaNo_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (2) 収集･運搬車輌番号 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku2SyaNo_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            switch (this.logic.ChkCarData(cantxt_UnpanJyutaku2NameCd, cantxt_Jyutaku2Syasyu, ctxt_Jyutaku2Syasyu, cantxt_Jyutaku2SyaNo, ctxt_Jyutaku2SyaNo))
            {
                case 0://正常
                    break;

                case 1://空
                    this.ctxt_Jyutaku2SyaNo.Text = string.Empty;
                    return;

                case 2://エラー
                    this.ctxt_Jyutaku2SyaNo.Text = string.Empty;
                    return;
            }
            cantxt_Jyutaku2SyaNo_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (2) 収集･運搬車輌番号 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Jyutaku2SyaNo_PopupAfterExecuteMethod()
        {
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku2NameCd, cantxt_UnpanJyutaku2Name,
                cnt_UnpanJyutaku2Zip, cnt_UnpanJyutaku2Tel, ctxt_UnpanJyutaku2Add,
                null, null, null
                , false, false, true, false);
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (2) 車種 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku2Syasyu_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_Jyutaku2Syasyu_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(収集運搬業者) (2) 車種 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Jyutaku2Syasyu_PopupAfterExecuteMethod()
        {
        }

        #endregion

        #region 処分受託者(処分業者)

        /// <summary>
        /// 処分受託者(処分業者) 名称 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunJyutakuNameCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 処分受託者(処分業者) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunJyutakuNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            switch (this.logic.ChkGyosya(cantxt_SyobunJyutakuNameCd, "SHOBUN_NIOROSHI_GYOUSHA_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //処分受託者削除
                    this.logic.SyobunJyutakuDel();
                    // 20140604 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す(処分も) start
                    //処分の受託  削除
                    this.logic.SyobunJyuCDDel();
                    // 20140604 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す(処分も) end
                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 処分受託者(処分業者) 名称 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupBefExecuteMethod()
        {
            this.bak_SyobunJyutakuNameCd = this.cantxt_SyobunJyutakuNameCd.Text;
        }

        /// <summary>
        /// 処分受託者(処分業者) 名称 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod()
        {
            if (this.cantxt_SyobunJyutakuNameCd.Text != this.bak_SyobunJyutakuNameCd)
            {
                this.logic.UnpanJyugyobaDel();
            }

            //業者　設定
            ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
                cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
                "Part1", cantxt_SyobunJyuCd, ctxt_SyobunJyuName
                , false, true, false, false, this.logic.isNotNeedDeleteFlg);
            // 20140530 katen No.679 start‏
            this.cantxt_SyobunSyoCd.Text = this.cantxt_SyobunJyuCd.Text;
            this.ctxt_SyobunSyoName.Text = this.ctxt_SyobunJyuName.Text;
            // 20140530 katen No.679 end‏
        }

        #endregion

        #region 運搬先の事業場(処分業者の処理施設)

        /// <summary>
        /// 運搬先の事業場(処分業者の処理施設) 名称 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(処分業者の処理施設) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            //20150630 #5005 hoanghm start
            //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
            if (!string.IsNullOrEmpty(cantxt_UnpanJyugyobaNameCd.Text) && string.IsNullOrEmpty(cantxt_SyobunJyutakuNameCd.Text))
            {
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                this.messageShowLogic.MessageBoxShow("E051", "処分受託者");
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                this.cantxt_UnpanJyugyobaNameCd.Focus();
                this.logic.isInputError = true;
                return;
            }
            //20150630 #5005 hoanghm end

            // 処分事業場もしくは最終処分場の現場かチェック
            string[] colNames = { "SHOBUN_NIOROSHI_GENBA_KBN", "SAISHUU_SHOBUNJOU_KBN" };
            switch (this.logic.ChkJigyouba(cantxt_UnpanJyugyobaNameCd, cantxt_SyobunJyutakuNameCd, colNames))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //運搬先の事業場削除
                    this.logic.UnpanJyugyobaDel();

                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(処分業者の処理施設) 名称 PopupAfterExecuteMethod
        /// </summary>
        // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
        //public void cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod()
        //{
        //    //業者　設定
        //    ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
        //        cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
        //        "Part1", cantxt_SyobunJyuCd, ctxt_SyobunJyuName
        //        , false, true, false, false);

        //    //現場　設定
        //    this.mlogic.SetAddressJigyouba("All", cantxt_SyobunJyutakuNameCd, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
        //        cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null
        //        , false, false, true, false);

        //    // 20140609 katen No.4435 start‏
        //    this.cantxt_SyobunSyoCd.Text = this.cantxt_SyobunJyuCd.Text;
        //    this.ctxt_SyobunSyoName.Text = this.ctxt_SyobunJyuName.Text;
        //    // 20140609 katen No.4435 end‏
        //}
        public void cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod(ICustomControl sender = null)
        {
            if (sender != null)
            {
                if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text)
                {
                    //業者　設定
                    ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
                        cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
                        "Part1", cantxt_SyobunJyuCd, ctxt_SyobunJyuName
                        , false, true, false, false, this.logic.isNotNeedDeleteFlg);
                    this.cantxt_SyobunSyoCd.Text = this.cantxt_SyobunJyuCd.Text;
                    this.ctxt_SyobunSyoName.Text = this.ctxt_SyobunJyuName.Text;
                }
                else
                {
                    this.cantxt_SyobunJyutakuName.Text = this.bak_SyobunJyutakuName;
                }

                if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text || this.bak_UnpanJyugyobaNameCd != this.cantxt_UnpanJyugyobaNameCd.Text)
                {
                    // 運搬先の事業場　設定(処分受託者で検索）
                    this.mlogic.SetAddressJigyouba("All", cantxt_SyobunJyutakuNameCd, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
                        cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null
                        , false, false, true, false, this.logic.isNotNeedDeleteFlg);
                    // 運搬先の事業場　設定(最終処分場で検索）
                    this.mlogic.SetAddressJigyouba("All", cantxt_SyobunJyutakuNameCd, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
                        cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null
                        , false, true, false, false, this.logic.isNotNeedDeleteFlg);
                }
                else
                {
                    this.cantxt_UnpanJyugyobaName.Text = this.bak_UnpanJyugyobaName;
                }

            }
            else
            {
                // 運搬先の事業場　設定(処分受託者で検索）
                this.mlogic.SetAddressJigyouba("All", cantxt_SyobunJyutakuNameCd, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
                    cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null
                    , false, false, true, false, this.logic.isNotNeedDeleteFlg);
                // 運搬先の事業場　設定(最終処分場で検索）
                this.mlogic.SetAddressJigyouba("All", cantxt_SyobunJyutakuNameCd, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
                    cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null
                    , false, true, false, false, this.logic.isNotNeedDeleteFlg);
            }
        }

        /// <summary>
        /// 運搬先の事業場 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyugyobaNameCd_PopupBeforeExecuteMethod()
        {
            this.bak_SyobunJyutakuNameCd = this.cantxt_SyobunJyutakuNameCd.Text;

            this.bak_UnpanJyugyobaNameCd = this.cantxt_UnpanJyugyobaNameCd.Text;

            this.bak_SyobunJyutakuName = this.cantxt_SyobunJyutakuName.Text;

            this.bak_UnpanJyugyobaName = this.cantxt_UnpanJyugyobaName.Text;

        }
        // 20140619 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end

        #endregion

        #region 積替え又は保管

        /// <summary>
        /// 積替え又は保管 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TumiGyoCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_TumiGyoCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 積替え又は保管 業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TumiGyoCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            switch (this.logic.ChkGyosya(cantxt_TumiGyoCd, "TSUMIKAEHOKAN_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //運搬先の事業場（処分業者の処理施設）削除削除
                    this.logic.TumiHokaDel("cantxt_TumiGyoCd");
                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_TumiGyoCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 積替え又は保管 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_TumiGyoCd_PopupBefExecuteMethod()
        {
            this.bak_TumiGyoCd = this.cantxt_TumiGyoCd.Text;
        }

        /// <summary>
        /// 積替え又は保管 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_TumiGyoCd_PopupAfterExecuteMethod()
        {
            if (this.cantxt_TumiGyoCd.Text != this.bak_TumiGyoCd)
            {
                this.logic.TumiHokaDel("cantxt_TumiHokaNameCd");
            }

            //業者　設定
            ManifestoLogic.SetAddrGyousha("All", cantxt_TumiGyoCd, ctxt_TumiGyoName,
                null, null, null,
                null, null, null
                , false, false, false, true, this.logic.isNotNeedDeleteFlg);
        }

        /// <summary>
        /// 積替え又は保管 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TumiHokaNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            //20150630 #5005 hoanghm start
            //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
            if (!string.IsNullOrEmpty(cantxt_TumiHokaNameCd.Text) && string.IsNullOrEmpty(cantxt_TumiGyoCd.Text))
            {
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                this.messageShowLogic.MessageBoxShow("E051", "業者");
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                this.cantxt_TumiHokaNameCd.Text = string.Empty;
                this.cantxt_TumiHokaNameCd.Focus();
                this.logic.isInputError = true;
                return;
            }
            //20150630 #5005 hoanghm end

            switch (this.logic.ChkJigyouba(cantxt_TumiHokaNameCd, cantxt_TumiGyoCd, "TSUMIKAEHOKAN_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //運搬先の事業場（処分業者の処理施設）削除削除
                    this.logic.TumiHokaDel("cantxt_TumiHokaNameCd");

                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_TumiHokaNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 積替え又は保管 名称 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_TumiHokaNameCd_PopupAfterExecuteMethod()
        {
            //業者　設定
            ManifestoLogic.SetAddrGyousha("All", cantxt_TumiGyoCd, ctxt_TumiGyoName,
                null, null, null,
                null, null, null
                , false, false, false, true, this.logic.isNotNeedDeleteFlg);

            //現場　設定
            this.mlogic.SetAddressJigyouba("All", cantxt_TumiGyoCd, cantxt_TumiHokaNameCd, ctxt_TumiHokaName, cnt_TumiHokaZip, cnt_TumiHokaTel, ctxt_TumiHokaAdd, null
                , false, false, false, true, this.logic.isNotNeedDeleteFlg);
        }

        /// <summary>
        /// 積替え又は保管 有価物収拾 実績数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TumiHokaJisseki_Validated(object sender, EventArgs e)
        {
            //if (!isChanged(sender))//変更がない場合は何もしない
            //{
            //    return;
            //}
            //this.logic.SetYukaJyuuryo(this.cntxt_TumiHokaJisseki);
        }

        #endregion

        #region 運搬の受託(１)

        /// <summary>
        /// 運搬の受託(１) 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuCd1_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyuCd1_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(１) 業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuCd1_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            // 20140617 ria EV004824 「運搬の受託」、「処分の受託」 start
            //switch (this.logic.ChkGyosya(cantxt_UnpanJyuCd1, "UNPAN_JUTAKUSHA_KBN"))
            //{
            //    case 0://正常
            //        break;

            //    case 1://空
            //        this.ctxt_UnpanJyuName1.Text = string.Empty;
            //        return;

            //    case 2://エラー
            //        this.ctxt_UnpanJyuName1.Text = string.Empty;
            //        return;
            //}
            if (!string.IsNullOrEmpty(this.cantxt_UnpanJyutaku1NameCd.Text))
            {
                if (this.cantxt_UnpanJyuCd1.Text != this.cantxt_UnpanJyutaku1NameCd.Text && !string.IsNullOrEmpty(this.cantxt_UnpanJyuCd1.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    this.ctxt_UnpanJyuName1.Text = string.Empty;
                    this.cantxt_UnpanJyuCd1.Focus();
                    this.logic.isInputError = true;
                    return;
                }
                else if (string.IsNullOrEmpty(this.cantxt_UnpanJyuCd1.Text))
                {
                    this.ctxt_UnpanJyuName1.Text = string.Empty;
                    return;
                }
            }
            else
            {
                switch (this.logic.ChkGyosya(cantxt_UnpanJyuCd1, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        this.ctxt_UnpanJyuName1.Text = string.Empty;
                        return;

                    case 2://エラー
                        this.ctxt_UnpanJyuName1.Text = string.Empty;
                        this.logic.isInputError = true;
                        return;
                }
            }
            // 20140617 ria EV004824 「運搬の受託」、「処分の受託」 end
            cantxt_UnpanJyuCd1_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(１) 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyuCd1_PopupAfterExecuteMethod()
        {
            ManifestoLogic.SetAddrGyousha("Part1", cantxt_UnpanJyuCd1, ctxt_UnpanJyuName1,
                null, null, null,
                null, null, null
                , false, false, true, false, this.logic.isNotNeedDeleteFlg);
        }

        /// <summary>
        /// 運搬の受託(１) 運転者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuUntenCd1_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_UnpanJyuUntenCd1_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(１) 運転者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuUntenCd1_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkUntensha(cantxt_UnpanJyuUntenCd1);
        }

        /// <summary>
        /// 運搬の受託(１) 運転者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyuUntenCd1_PopupAfterExecuteMethod()
        {
        }

        #endregion

        #region 運搬の受託(２)

        /// <summary>
        /// 運搬の受託(２) 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuCd2_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyuCd2_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(２) 業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuCd2_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            // 20140617 ria EV004824 「運搬の受託」、「処分の受託」 start
            //switch (this.logic.ChkGyosya(cantxt_UnpanJyuCd2, "UNPAN_JUTAKUSHA_KBN"))
            //{
            //    case 0://正常
            //        break;

            //    case 1://空
            //        this.ctxt_UnpanJyuName2.Text = string.Empty;
            //        return;

            //    case 2://エラー
            //        this.ctxt_UnpanJyuName2.Text = string.Empty;
            //        return;
            //}
            if (!string.IsNullOrEmpty(this.cantxt_UnpanJyutaku2NameCd.Text))
            {
                if (this.cantxt_UnpanJyuCd2.Text != this.cantxt_UnpanJyutaku2NameCd.Text && !string.IsNullOrEmpty(this.cantxt_UnpanJyuCd2.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    this.ctxt_UnpanJyuName2.Text = string.Empty;
                    this.cantxt_UnpanJyuCd2.Focus();
                    this.logic.isInputError = true;
                    return;
                }
                else if (string.IsNullOrEmpty(this.cantxt_UnpanJyuCd2.Text))
                {
                    this.ctxt_UnpanJyuName2.Text = string.Empty;
                    return;
                }
            }
            else
            {
                switch (this.logic.ChkGyosya(cantxt_UnpanJyuCd2, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        this.ctxt_UnpanJyuName2.Text = string.Empty;
                        return;

                    case 2://エラー
                        this.ctxt_UnpanJyuName2.Text = string.Empty;
                        this.logic.isInputError = true;
                        return;
                }
            }
            // 20140617 ria EV004824 「運搬の受託」、「処分の受託」 end
            cantxt_UnpanJyuCd2_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(２) 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyuCd2_PopupAfterExecuteMethod()
        {
            ManifestoLogic.SetAddrGyousha("Part1", cantxt_UnpanJyuCd2, ctxt_UnpanJyuName2,
                null, null, null,
                null, null, null
                , false, false, true, false);
        }

        /// <summary>
        /// 運搬の受託(２) 運転者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuUntenCd2_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_UnpanJyuUntenCd2_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(２) 運転者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuUntenCd2_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            this.logic.ChkUntensha(cantxt_UnpanJyuUntenCd2);
        }

        /// <summary>
        /// 運搬の受託(２) 運転者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyuUntenCd2_PopupAfterExecuteMethod()
        {
        }

        #endregion

        #region 処分の受託(受領)

        /// <summary>
        /// 処分の受託(受領) Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunJyuCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            // 20140617 ria EV004824 「運搬の受託」、「処分の受託」 start
            //switch (this.logic.ChkGyosya(cantxt_SyobunJyuCd, "SHOBUN_JUTAKUSHA_KBN"))
            //{
            //    case 0://正常
            //        break;

            //    case 1://空
            //        this.ctxt_SyobunJyuName.Text = string.Empty;
            //        return;

            //    case 2://エラー
            //        this.ctxt_SyobunJyuName.Text = string.Empty;
            //        return;
            //}
            if (!string.IsNullOrEmpty(this.cantxt_SyobunJyutakuNameCd.Text))
            {
                if (this.cantxt_SyobunJyuCd.Text != this.cantxt_SyobunJyutakuNameCd.Text && !string.IsNullOrEmpty(this.cantxt_SyobunJyuCd.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    this.ctxt_SyobunJyuName.Text = string.Empty;
                    this.cantxt_SyobunJyuCd.Focus();
                    this.logic.isInputError = true;
                    return;
                }
                else if (string.IsNullOrEmpty(this.cantxt_SyobunJyuCd.Text))
                {
                    this.ctxt_SyobunJyuName.Text = string.Empty;
                    return;
                }
            }
            else
            {
                switch (this.logic.ChkGyosya(cantxt_SyobunJyuCd, "SHOBUN_NIOROSHI_GYOUSHA_KBN"))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        this.ctxt_SyobunJyuName.Text = string.Empty;
                        return;

                    case 2://エラー
                        this.ctxt_SyobunJyuName.Text = string.Empty;
                        this.logic.isInputError = true;
                        return;
                }
            }
            // 20140617 ria EV004824 「運搬の受託」、「処分の受託」 end
            cantxt_SyobunJyuCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 処分の受託(受領) PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyuCd_PopupAfterExecuteMethod()
        {
            ManifestoLogic.SetAddrGyousha("Part1", cantxt_SyobunJyuCd, ctxt_SyobunJyuName,
                null, null, null,
                null, null, null
                , false, true, false, false, this.logic.isNotNeedDeleteFlg);
        }

        #endregion

        #region 処分の受託(処分)

        /// <summary>
        /// 処分の受託(処分) Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunSyoCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_SyobunSyoCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 処分の受託(処分) Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunSyoCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            // 20140617 ria EV004824 「運搬の受託」、「処分の受託」 start
            //switch (this.logic.ChkGyosya(cantxt_SyobunSyoCd, "SHOBUN_JUTAKUSHA_KBN"))
            //{
            //    case 0://正常
            //        break;

            //    case 1://空
            //        this.ctxt_SyobunSyoName.Text = string.Empty;
            //        return;

            //    case 2://エラー
            //        this.ctxt_SyobunSyoName.Text = string.Empty;
            //        return;
            //}
            if (!string.IsNullOrEmpty(this.cantxt_SyobunJyutakuNameCd.Text))
            {
                if (this.cantxt_SyobunSyoCd.Text != this.cantxt_SyobunJyutakuNameCd.Text && !string.IsNullOrEmpty(this.cantxt_SyobunSyoCd.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    this.ctxt_SyobunSyoName.Text = string.Empty;
                    this.cantxt_SyobunSyoCd.Focus();
                    this.logic.isInputError = true;
                    return;
                }
                else if (string.IsNullOrEmpty(this.cantxt_SyobunSyoCd.Text))
                {
                    this.ctxt_SyobunSyoName.Text = string.Empty;
                    return;
                }
            }
            else
            {
                switch (this.logic.ChkGyosya(cantxt_SyobunSyoCd, "SHOBUN_NIOROSHI_GYOUSHA_KBN"))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        this.ctxt_SyobunSyoName.Text = string.Empty;
                        return;

                    case 2://エラー
                        this.ctxt_SyobunSyoName.Text = string.Empty;
                        this.logic.isInputError = true;
                        return;
                }
            }
            // 20140617 ria EV004824 「運搬の受託」、「処分の受託」 end

            cantxt_SyobunSyoCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 処分の受託CD PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyobunSyoCd_PopupAfterExecuteMethod()
        {
            ManifestoLogic.SetAddrGyousha("Part1", cantxt_SyobunSyoCd, ctxt_SyobunSyoName,
                null, null, null,
                null, null, null
                , false, true, false, false, this.logic.isNotNeedDeleteFlg);
        }

        /// <summary>
        /// 処分の受託(処分) 担当者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyobunSyoUntenCd_PopupAfterExecuteMethod()
        {
            this.mlogic.SetShobunTantousha(cantxt_SyobunSyoUntenCd, cantxt_SyobunSyoUntenName);
        }

        // 20140601 katen 不具合No.4133 start‏
        /// <summary>
        /// 「前」のボタン Clickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_Previous_Click(object sender, EventArgs e)
        {
            // 権限チェック
            bool containsUpdateAuthority = Manager.CheckAuthority("G121", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
            bool containsReferenceAuthority = Manager.CheckAuthority("G121", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);
            if (!containsUpdateAuthority && !containsReferenceAuthority)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E158", "修正");
                return;
            }

            bool catchErr = false;
            if (!this.logic.SetAllDataForPreviousOrNext("SearchPreviousData", out catchErr))
            {
                if (catchErr) { return; }
                // データが取得できた場合
                if (containsUpdateAuthority)
                {
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.parameters.Mode = (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else
                {
                    this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    this.parameters.Mode = (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                //base.OnLoad(e);

                if (this.logic.WindowInit("OnLoad", out catchErr))
                {
                    if (catchErr) { return; }
                    var checkAuthority = Manager.CheckAuthority("G121", WINDOW_TYPE.NEW_WINDOW_FLAG, false);
                    if (checkAuthority)
                    {
                        // エラー発生時には値をクリアする
                        base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.parameters.Save();
                    }

                    this.ClearScreen(e);
                    this.logic.WindowInit("SetAddFrom", out catchErr);
                    if (catchErr) { return; }

                    if (!checkAuthority)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E158", "新規");

                        // 新規権限が無ければ閉じる
                        this.ParentForm.Close();
                    }
                    return;
                }
            }
            else
            {
                if (catchErr) { return; }
                // データが取得できなかった場合
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E045");
                return;
            }
        }

        /// <summary>
        /// 「次」のボタン Clickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_Next_Click(object sender, EventArgs e)
        {
            // 権限チェック
            bool containsUpdateAuthority = Manager.CheckAuthority("G121", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
            bool containsReferenceAuthority = Manager.CheckAuthority("G121", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);
            if (!containsUpdateAuthority && !containsReferenceAuthority)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E158", "修正");
                return;
            }

            bool catchErr = false;
            if (!this.logic.SetAllDataForPreviousOrNext("SearchNextData", out catchErr))
            {
                if (catchErr) { return; }
                // 入力されている受入番号の後の受入番号が取得できた場合
                if (containsUpdateAuthority)
                {
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.parameters.Mode = (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else
                {
                    this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    this.parameters.Mode = (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                //base.OnLoad(e);

                if (this.logic.WindowInit("OnLoad", out catchErr))
                {
                    if (catchErr) { return; }
                    var checkAuthority = Manager.CheckAuthority("G121", WINDOW_TYPE.NEW_WINDOW_FLAG, false);
                    if (checkAuthority)
                    {
                        // エラー発生時には値をクリアする
                        base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.parameters.Save();
                    }

                    this.ClearScreen(e);
                    this.logic.WindowInit("SetAddFrom", out catchErr);
                    if (catchErr) { return; }

                    if (!checkAuthority)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E158", "新規");

                        // 新規権限が無ければ閉じる
                        this.ParentForm.Close();
                    }

                    return;
                }
            }
            else
            {
                if (catchErr) { return; }
                // 入力されている受入番号の後の受入番号が取得できなかった場合
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E045");
                return;
            }
        }

        // 20140601 katen 不具合No.4133 end‏

        #endregion

        #region 最終処分を行った場所

        /// <summary>
        /// 最終処分を行った場所 最終処分の業者CD Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuSyobunGyoCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_SaisyuSyobunGyoCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 最終処分を行った場所 最終処分の業者CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuSyobunGyoCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            switch (this.logic.ChkGyosya(cantxt_SaisyuSyobunGyoCd, "SHOBUN_NIOROSHI_GYOUSHA_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //最終処分場削除
                    this.logic.SaisyuBasyoSyozaiDel("cantxt_SaisyuSyobunGyoCd");

                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_SaisyuSyobunGyoCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 最終処分を行った場所 最終処分の業者CD PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SaisyuSyobunGyoCd_PopupAfterExecuteMethod()
        {
            if (this.cantxt_SaisyuSyobunGyoCd.Text != this.preGyoushaCD)
            {
                this.logic.SaisyuBasyoSyozaiDel("cantxt_SaisyuSyobunbaCD");
            }
        }

        /// <summary>
        /// 最終処分を行った場所 最終処分の業者CD PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_SaisyuSyobunGyoCd_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCD = this.cantxt_SaisyuSyobunGyoCd.Text;
        }

        /// <summary>
        /// 最終処分を行った場所 名称 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuSyobunbaCD_Validating(object sender, CancelEventArgs e)
        {
            //if (!isChanged(sender))//変更がない場合は何もしない
            //{
            //    return;
            //}

            ////最終処分場所CDチェック
            //switch (this.logic.ChkJigyouba(cantxt_SaisyuSyobunbaCD, cantxt_SaisyuSyobunGyoCd, "SAISHUU_SHOBUNJOU_KBN"))
            //{
            //    case 0://正常
            //        break;

            //    case 1://空
            //        this.logic.SaisyuBasyoSyozaiDel("cantxt_SaisyuSyobunbaCD");
            //        return;

            //    case 2://エラー
            //        e.Cancel = true;
            //        return;
            //}

            //this.cantxt_SaisyuSyobunbaCD_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 最終処分を行った場所 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuSyobunbaCD_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            //20150630 #5005 hoanghm start
            //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
            if (!string.IsNullOrEmpty(cantxt_SaisyuSyobunbaCD.Text) && string.IsNullOrEmpty(cantxt_SaisyuSyobunGyoCd.Text))
            {
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                this.messageShowLogic.MessageBoxShow("E051", "最終処分業者");
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                this.cantxt_SaisyuSyobunbaCD.Text = string.Empty;
                this.cantxt_SaisyuSyobunbaCD.Focus();
                this.logic.isInputError = true;
                return;
            }
            //20150630 #5005 hoanghm end

            //最終処分場所CDチェック
            switch (this.logic.ChkJigyouba(cantxt_SaisyuSyobunbaCD, cantxt_SaisyuSyobunGyoCd, "SAISHUU_SHOBUNJOU_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    //
                    this.logic.SaisyuBasyoSyozaiDel("cantxt_SaisyuSyobunbaCD");

                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_SaisyuSyobunbaCD_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 最終処分を行った場所 名称 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SaisyuSyobunbaCD_PopupAfterExecuteMethod()
        {
            //最終処分場所　設定
            this.mlogic.SetAddressJigyouba("All", cantxt_SaisyuSyobunGyoCd, cantxt_SaisyuSyobunbaCD, ctxt_SaisyuSyobunGyoName,
                ctxt_SaisyuSyobunGyoZip, null, ctxt_SaisyuBasyoSyozai, ctxt_SaisyuBasyoNo
                , false, true, false, false);
        }

        #endregion

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
                if (c.GetType().ToString() == "System.Windows.Forms.Panel" || c.GetType().ToString() == "r_framework.CustomControl.CustomPanel")
                {
                    continue;
                }
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

            // エラー等でフォーカス移動しなかった場合は、値クリアして強制チェックするようにする。
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

        #region 禁則文字チェック

        /// <summary>
        /// 最終処分(埋立処分、再生等)を行った場所　所在地/名称(委託契約書記載の最終処分場所については、処分先Noでも可)
        /// 所在地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_SaisyuBasyoSyozai_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 最終処分(埋立処分、再生等)を行った場所　所在地/名称(委託契約書記載の最終処分場所については、処分先Noでも可)
        /// 名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_SaisyuSyobunGyoName_Validating(object sender, CancelEventArgs e)
        {
        }

        #endregion

        /// <summary>
        /// 総重量又は総容量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_TotalJyuryo_Validated(object sender, EventArgs e)
        {
            //if (!isChanged(sender))//変更がない場合は何もしない
            //{
            //    return;
            //}
            //this.logic.SetYukaJyuuryo(this.cntxt_TotalJyuryo);
        }

        /// <summary>
        /// グリッドの数値列を返す
        /// </summary>
        /// <returns></returns>
        internal r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column[] GetNumericColumns()
        {
            return new[] { this.Suryo, this.KansangoSuryo, this.GenyoyugoTotalSuryo };
        }

        private void KenpaiManifestoNyuryoku_Shown(object sender, EventArgs e)
        {
            // データ移動初期表示処理
            this.logic.SetMoveData();
        }

        // 20140609 katen No.730 規定値機能の追加について start
        /// <summary>
        /// 拠点でパターンデータを設定する
        /// </summary>
        /// <param name="kyotenCd">拠点CD</param>
        public bool SetKiteiValue(string kyotenCd)
        {
            bool result = this.logic.SetKiteiValue("bt_process2", kyotenCd);
            if (!result)
            {
                string temp_KyotenCd = this.logic.headerform.ctxt_KyotenCd.Text;
                string temp_KyotenName = this.logic.headerform.ctxt_KyotenMei.Text;
                if (!this.logic.SetManifestFrom("Non"))
                {
                    result = false;
                    return result;
                }
                this.logic.headerform.ctxt_KyotenCd.Text = temp_KyotenCd;
                this.logic.headerform.ctxt_KyotenMei.Text = temp_KyotenName;

                if (!string.IsNullOrEmpty(this.cantxt_DenshuKbn.Text))
                {
                    this.logic.SetRenkeiLabel();
                }
                else
                {
                    this.lbl_No.Text = "連携番号";
                    this.lbl_Misaigyou.Text = "明細行";
                }
            }
            return result;
        }
        // 20140609 katen No.730 規定値機能の追加について end‏

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // 改行可能項目にてCtrl+Enterで次の項目にフォーカスが移動しないよう判断

            var act = ControlUtility.GetActiveControl(this);
            // EnterとControlキー押下判断
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                var textBox = act as TextBox;
                // 改行できるTextBoxか判断
                if (textBox.Multiline)
                {
                    e.Handled = true;
                    return;
                }
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// 車輌名称のバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_JyutakuSyaNo_Validating(object sender, CancelEventArgs e)
        {
            var textBox = ((TextBox)sender);
            // 入力文字列のバイト数がMaxLengthプロパティの値を超えたらエラー
            var byteLength = this.mlogic.GetByteLength(textBox.Text);
            if (byteLength > (textBox.MaxLength))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E012", "全角10文字/半角20文字以内で車輌名");
                e.Cancel = true;
            }
        }

        private void cdgrid_Jisseki_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (row >= 0)
            {
                switch (col)
                {
                    case (int)enumCols.NisugataCd://荷姿
                        this.logic.ChkNisugata(e);
                        break;
                    case (int)enumCols.SyobunCd://処分方法
                        this.logic.ChkSyobunCd(e);
                        break;
                    case (int)enumCols.TaniCd://単位
                        this.logic.ChkTaniCd(e);
                        break;
                }
            }
        }

        /// <summary>
        /// 交付年月日 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void cdate_KohuDate_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(this.cdate_KohuDate.Value)))
            {
                return;
            }
            DateCompare dc = new DateCompare(Convert.ToDateTime(this.cdate_KohuDate.Value), "交付年月日");
            for (int i = 0; i < this.cdgrid_Jisseki.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[i].Cells[(int)enumCols.SyobunEndDate].Value)))
                {
                    dc.Compare(Convert.ToDateTime(cdgrid_Jisseki.Rows[i].Cells[(int)enumCols.SyobunEndDate].Value), "処分終了年月日");
                }
                if (!string.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[i].Cells[(int)enumCols.SaisyuSyobunEndDate].Value)))
                {
                    dc.Compare(Convert.ToDateTime(cdgrid_Jisseki.Rows[i].Cells[(int)enumCols.SaisyuSyobunEndDate].Value), "最終処分終了年月日");
                }
            }

            if (!string.IsNullOrEmpty(Convert.ToString(this.cdate_UnpanJyu1.Value)))
            {
                dc.Compare(Convert.ToDateTime(this.cdate_UnpanJyu1.Value), "運搬区間1の運搬終了年月日");
            }
            if (!string.IsNullOrEmpty(Convert.ToString(this.cdate_UnpanJyu2.Value)))
            {
                dc.Compare(Convert.ToDateTime(this.cdate_UnpanJyu2.Value), "運搬区間2の運搬終了年月日");
            }
            if (dc.kbn == "交付年月日")
            {
                return;
            }
            else
            {
                this.messageShowLogic.MessageBoxShow("E280", "交付年月日", dc.kbn);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 運搬終了日 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void cdate_UnpanJyu1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(this.cdate_UnpanJyu1.Value)) || string.IsNullOrEmpty(Convert.ToString(this.cdate_KohuDate.Value)))
            {
                return;
            }
            if (Convert.ToDateTime(this.cdate_UnpanJyu1.Value) < Convert.ToDateTime(this.cdate_KohuDate.Value))
            {
                this.messageShowLogic.MessageBoxShow("E281", "運搬終了年月日", "交付年月日");
                e.Cancel = true;
            }
        }
        internal void cdate_UnpanJyu2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(this.cdate_UnpanJyu2.Value)) || string.IsNullOrEmpty(Convert.ToString(this.cdate_KohuDate.Value)))
            {
                return;
            }
            if (Convert.ToDateTime(this.cdate_UnpanJyu2.Value) < Convert.ToDateTime(this.cdate_KohuDate.Value))
            {
                this.messageShowLogic.MessageBoxShow("E281", "運搬終了年月日", "交付年月日");
                e.Cancel = true;
            }
        }

        internal void cantxt_UnpanJigyobaFreeCd1_Validating(object sender, CancelEventArgs e)
        {
            if (this.cantxt_UnpanJigyobaFreeCd1.IsInputErrorOccured)
            {
                return;
            }

            if (this.cantxt_UnpanJigyobaFreeCd1.Text != null
                && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd1.Text))
            {
                if (this.cantxt_UnpanJigyobaFreeCd2.Text != null
                    && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd2.Text))
                {
                    this.messageShowLogic.MessageBoxShow("W009", "4", "5", "6");
                }
                else if (this.cantxt_UnpanJigyobaFreeCd3.Text != null
                         && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd3.Text))
                {
                    this.messageShowLogic.MessageBoxShow("W009", "4", "5", "6");
                }
            }
        }

        internal void cantxt_UnpanJigyobaFreeCd2_Validating(object sender, CancelEventArgs e)
        {
            if (this.cantxt_UnpanJigyobaFreeCd2.IsInputErrorOccured)
            {
                return;
            }

            if (this.cantxt_UnpanJigyobaFreeCd1.Text != null
                && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd1.Text))
            {
                if (this.cantxt_UnpanJigyobaFreeCd2.Text != null
                    && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd2.Text))
                {
                    this.messageShowLogic.MessageBoxShow("W008", "4", "5");
                }

            }
        }

        internal void cantxt_UnpanJigyobaFreeCd3_Validating(object sender, CancelEventArgs e)
        {
            if (this.cantxt_UnpanJigyobaFreeCd3.IsInputErrorOccured)
            {
                return;
            }

            if (this.cantxt_UnpanJigyobaFreeCd1.Text != null
               && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd1.Text))
            {
                if (this.cantxt_UnpanJigyobaFreeCd3.Text != null
                       && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd3.Text))
                {
                    this.messageShowLogic.MessageBoxShow("W008", "4", "6");
                }
            }
        }

        internal void cantxt_UnpanJigyobaFreeCd4_Validating(object sender, CancelEventArgs e)
        {
            if (this.cantxt_UnpanJigyobaFreeCd4.IsInputErrorOccured)
            {
                return;
            }

            if (this.cantxt_UnpanJigyobaFreeCd4.Text != null
                && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd4.Text))
            {
                if (this.cantxt_UnpanJigyobaFreeCd5.Text != null
                    && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd5.Text))
                {
                    this.messageShowLogic.MessageBoxShow("W008", "7", "8");
                }
            }
        }

        internal void cantxt_UnpanJigyobaFreeCd5_Validating(object sender, CancelEventArgs e)
        {
            if (this.cantxt_UnpanJigyobaFreeCd5.IsInputErrorOccured)
            {
                return;
            }

            if (this.cantxt_UnpanJigyobaFreeCd4.Text != null
                   && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd4.Text))
            {
                if (this.cantxt_UnpanJigyobaFreeCd5.Text != null
                    && !string.IsNullOrEmpty(this.cantxt_UnpanJigyobaFreeCd5.Text))
                {
                    this.messageShowLogic.MessageBoxShow("W008", "7", "8");
                }
            }

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            var ret = base.ProcessDialogKey(keyData);

            if (keyData == (Keys.Shift | Keys.Tab)
                || keyData == (Keys.Shift | Keys.Enter))
            {
                if (this.cdgrid_Jisseki.Rows[this.cdgrid_Jisseki.Rows.Count - 1].Cells["SaisyuSyobunBasyo"].Selected)
                {
                    this.cdgrid_Jisseki.Rows[this.cdgrid_Jisseki.Rows.Count - 1].Cells["SaisyuBasyoCd"].Selected = true;
                }
            }

            //終了
            return ret;
        }

        ///// <summary>
        ///// 明細連携モード(受入・計量明細連携）切替
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void cantxt_Renkei_Mode_TextChanged(object sender, EventArgs e)
        {

            if (this.logic.maniFlag != 1)
            {
                return;
            }

            if (this.cantxt_Renkei_Mode.Text.Equals("1"))
            {
                this.Renkei_Mode_1.Checked = true;
            }
            else
            {
                this.Renkei_Mode_2.Checked = true;
            }

            if (string.IsNullOrEmpty(this.cantxt_DenshuKbn.Text))
            {
                return;
            }

            int iKbn;
            iKbn = Convert.ToInt32(this.cantxt_DenshuKbn.Text);

            //受入と計量のみ処理を通す
            if (iKbn.Equals((int)DENSHU_KBN.UKEIRE) || iKbn.Equals((int)DENSHU_KBN.KEIRYOU))
            {
                // フォーカスを伝種区分、連携番号、明細行にいるかまいか
                Boolean isRelevantPart = this.cantxt_No.Focused || this.cantxt_DenshuKbn.Focused || this.cantxt_Meisaigyou.Focused;
                if (base.KeyEventKP != null)
                {
                    if ((base.KeyEventKP.KeyCode == Keys.Tab || base.KeyEventKP.KeyCode == Keys.Enter) && !base.KeyEventKP.Shift && this.cantxt_Meisaigyou.Focused)
                    {
                        isRelevantPart = false;
                    }
                }
                this.logic.setDetail(this.cantxt_DenshuKbn, this.cantxt_No, this.cantxt_Meisaigyou, isRelevantPart, true);

            }
        }

        //20250401
        private void cdate_UnpanDate_1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.cdate_UnpanDate_1.Value == null || this.cdate_KohuDate.Value == null)
                {
                    return;
                }

                if ((DateTime)this.cdate_UnpanDate_1.Value < (DateTime)this.cdate_KohuDate.Value)
                {
                    this.errmessage.MessageBoxShowError("運搬終了日1が交付年月日より過去日になっています。運搬終了日1を見直してください");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.errmessage.MessageBoxShowError(ex.Message);
                e.Cancel = true;
            }
        }

        //20250403
        private void cdate_UnpanDate_1_Validated(object sender, EventArgs e)
        {
            if (this.cdate_UnpanDate_1.Value == null)
            {
                this.cdate_UnpanJyu1.Value = null;
            }
            else
            {
                this.cdate_UnpanJyu1.Value = this.cdate_UnpanDate_1.Value;
            }

            if (this.cdate_UnpanDate_2.Value == null)
            {
                this.cdate_ShobunJuryoDate.Value = this.cdate_UnpanDate_1.Value;
            }
            else
            {
                //20250402
                if (this.cdate_UnpanDate_1.Value != null && (DateTime)this.cdate_UnpanDate_1.Value > (DateTime)this.cdate_UnpanDate_2.Value)
                {
                    this.cdate_ShobunJuryoDate.Value = this.cdate_UnpanDate_1.Value;
                }
            }
        }

        private void cdate_UnpanDate_2_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.cdate_UnpanDate_2.Value == null || this.cdate_KohuDate.Value == null)
                {
                    return;
                }

                if ((DateTime)this.cdate_UnpanDate_2.Value < (DateTime)this.cdate_KohuDate.Value)
                {
                    this.errmessage.MessageBoxShowError("運搬終了日2が交付年月日より過去日になっています。運搬終了日2を見直してください");
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.errmessage.MessageBoxShowError(ex.Message);
                e.Cancel = true;
            }
        }

        //20250403
        private void cdate_UnpanDate_2_Validated(object sender, EventArgs e)
        {
            if (this.cdate_UnpanDate_2.Value == null)
            {
                this.cdate_UnpanJyu2.Value = null;
            }
            else
            {
                this.cdate_UnpanJyu2.Value = this.cdate_UnpanDate_2.Value;
            }

            if (this.cdate_UnpanDate_1.Value == null)
            {
                this.cdate_ShobunJuryoDate.Value = this.cdate_UnpanDate_2.Value;
            }
            else
            {
                //20250402
                if (this.cdate_UnpanDate_2.Value != null && (DateTime)this.cdate_UnpanDate_1.Value <= (DateTime)this.cdate_UnpanDate_2.Value)
                {
                    this.cdate_ShobunJuryoDate.Value = this.cdate_UnpanDate_2.Value;
                }
            }
        }

        private void cdate_ShobunJuryoDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.cdate_ShobunJuryoDate.Value == null || this.cdate_KohuDate.Value == null)
                {
                    return;
                }

                if ((DateTime)this.cdate_ShobunJuryoDate.Value < (DateTime)this.cdate_KohuDate.Value)
                {
                    this.errmessage.MessageBoxShowError("処分受領日が交付年月日より過去日になっています。処分受領日を見直してください");
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.errmessage.MessageBoxShowError(ex.Message);
                e.Cancel = true;
            }
        }

        //20250403
        private void cdate_ShobunJuryoDate_Validated(object sender, EventArgs e)
        {
            if (this.cdate_ShobunJuryoDate.Value == null)
            {
                this.cdate_SyobunJyu.Value = null;
            }
            else
            {
                this.cdate_SyobunJyu.Value = this.cdate_ShobunJuryoDate.Value;
            }
        }

        private void cdate_ShobunShuryoDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.cdate_ShobunShuryoDate.Value == null || this.cdate_KohuDate.Value == null)
                {
                    return;
                }

                if ((DateTime)this.cdate_ShobunShuryoDate.Value < (DateTime)this.cdate_KohuDate.Value)
                {
                    this.errmessage.MessageBoxShowError("処分終了日が交付年月日より過去日になっています。処分終了日を見直してください");
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.errmessage.MessageBoxShowError(ex.Message);
                e.Cancel = true;
            }
        }

        //20250403
        private void cdate_ShobunShuryoDate_Validated(object sender, EventArgs e)
        {
            if (this.cdate_ShobunShuryoDate.Value == null)
            {
                this.cdgrid_Jisseki.Columns["SyobunEndDate"].ReadOnly = false;
                this.cdgrid_Jisseki.Refresh();
            }
            else
            {
                this.cdgrid_Jisseki.Columns["SyobunEndDate"].ReadOnly = true;
                this.cdgrid_Jisseki.Refresh();
            }
        }

        private void cdgrid_Jisseki_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.cdgrid_Jisseki.Columns[e.ColumnIndex].Name == "SyobunEndDate")
                {
                    if (this.cdgrid_Jisseki.Columns["SyobunEndDate"].ReadOnly)
                    {
                        e.CellStyle.BackColor = Color.Gainsboro;
                        e.CellStyle.ForeColor = Color.DimGray;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.errmessage.MessageBoxShowError(ex.Message);
            }
        }


    }
}