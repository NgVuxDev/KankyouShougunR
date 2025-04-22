using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Runtime.InteropServices;
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

namespace Shougun.Core.PaperManifest.SampaiManifestoThumiKae
{
    /// <summary>
    /// 産廃マニフェスト入力
    /// </summary>
    [Implementation]
    public partial class SampaiManifestoThumiKae : SuperForm
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
        public String HaikiKbnCD = "3";

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

        public Boolean isHeadVF = false;
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
        /// 変更前の処分受託者
        /// </summary>
        private string bak_SyobunJyutakuNameCd;

        // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end

        private string bak_TumiGyoCd;

        /// <summary>
        /// 変更前の最終処分業者(実績タブ)
        /// </summary>
        private string bak_SaisyuGyosyaCd = string.Empty;

        private string preGyoushaCd = string.Empty;

        internal bool isClearForm = false;

        internal bool isRegistErr = false;

        internal string preValue = string.Empty;

        internal bool isError = false;

        //モバイルオプション
        internal bool ismobile_mode = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string preTorihikisakiCd = string.Empty;

        // 2016.11.23 chinkeigen マニ入力と一覧 #101092 start
        private int selectedRowNo = 1;
        // 2016.11.23 chinkeigen マニ入力と一覧 #101092 end
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

        //20250402
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// パラメータ管理クラス
        /// </summary>
        internal Shougun.Core.PaperManifest.SampaiManifestoThumiKae.DTO.Parameters parameters = new Shougun.Core.PaperManifest.SampaiManifestoThumiKae.DTO.Parameters();

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="window_type">画面区分</param>
        /// <param name="RDentatuKbn">連携伝達区分</param>
        /// <param name="SystemId">システムID</param>
        /// <param name="RMeisaiId">連携明細システムID</param>
        /// <param name="iMode">処理モード</param>
        public SampaiManifestoThumiKae(WINDOW_TYPE window_type, string RDentatuKbn, string SystemId, string RMeisaiId, int iMode)
            : base(WINDOW_ID.T_TUMIKAE_MANIFEST, window_type)
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
                    Updflg = false;
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
            this.shapeContainer8.BringToFront();
            this.shapeContainer9.BringToFront();
            this.shapeContainer10.BringToFront();
            this.shapeContainer11.BringToFront();
            this.shapeContainer12.BringToFront();

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
        public SampaiManifestoThumiKae(WINDOW_TYPE windowType, string RDentatuKbn, string SystemId, string RMeisaiId, int iMode,
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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

			//Init INXS subapp transaction id refs #158004
            this.transactionId = Guid.NewGuid().ToString();

            bool catchErr = false;
            logic.WindowInit("OnLoad", out catchErr);
            if (catchErr) { return; }

            //グリッドの編集モード設定
            this.cdgrid_Jisseki.EditMode = DataGridViewEditMode.EditOnEnter;

            // Anchorの設定は必ずOnLoadで行うこと
            // VisualStugioのデザイナツールで表示するとtabControl1が縮小されるためプロパティから外したので実行時に設定する。
            if (!this.DesignMode)
            {
                // グリッドのAnchorにRightを追加
                if (this.tabControl1 != null)
                {
                    this.tabControl1.Anchor = (AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right);
                }
            }

            if (this.cdgrid_Jisseki != null)
            {
                this.cdgrid_Jisseki.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
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
            if (!Manager.CheckAuthority("G120", WINDOW_TYPE.NEW_WINDOW_FLAG))
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
            if (Manager.CheckAuthority("G120", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                this.parameters.Mode = (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            }
            else if (Manager.CheckAuthority("G120", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
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
                    if (catchErr) { return; }
                    break;

                case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード

                    if (!Manager.CheckAuthority("G120", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
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
                    if (catchErr) { return; }
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
        /// (F11)行削除イベント
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

            bool catchErr = false;
            // 20140620 kayo 不具合#4927　紐付いたマニフェストが削除されたら、データの不整合に生じる start
            if (this.logic.ChkRelationDetail(this.cdgrid_Jisseki.CurrentRow, out catchErr))
            {
                return;
            }
            if (catchErr) { return; }
            // 20140620 kayo 不具合#4927　紐付いたマニフェストが削除されたら、データの不整合に生じる end

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

            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL start
            //if (this.logic.maniFlag == 2 && this.cdgrid_Jisseki.Rows.Count == 0)
            //{
            //    this.cdgrid_Jisseki.AllowUserToAddRows = true;
            //}
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL end

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
            if (!Manager.CheckAuthority("G120", WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }

            bool catchErr = false;
            if (this.logic.CallPattern(out catchErr))
            {
                if (catchErr) { return; }

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
            bool catchErr = false;
            // 20140519 syunrei No.732 マニフェスト入力画面に対する機能追加（産廃マニフェスト（積替）入力） start
            //this.logic.ManiHimozuke();
            //・「マニフェストを登録し紐付け画面を表示します。」というMessageを表示する。

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

            if (DialogResult.Yes == this.messageShowLogic.MessageBoxShow("C065"))
            {
                //・このメッセージについて「OK」Buttonが押された後、二次マニフェスト入力画面の「F9:登録」Buttonが押された場合の処理を実行する。
                if (!this.logic.SetRegist_HimoDk())
                {
                    //このとき、二次マニフェスト入力画面が「新規Mode」である場合は,「修正Mode」にModeを変更する。
                    if (base.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                    {
                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        this.SystemId = this.parameters.SystemId;
                        this.parameters.Mode = (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        this.parameters.Save();

                        this.ClearScreen(e);
                        this.logic.WindowInit("SetUpdateFrom", out catchErr);
                        if (catchErr) { return; }
                        this.cdgrid_Jisseki.Rows[this.selectedRowNo].Cells[0].Selected = true;
                    }
                    // 2016.11.23 chinkeigen マニ入力と一覧 #101092 start
                    else if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    {
                        this.ClearScreen(e);
                        this.logic.WindowInit("SetUpdateFrom", out catchErr);
                        if (catchErr) { return; }
                        this.cdgrid_Jisseki.Rows[this.selectedRowNo].Cells[0].Selected = true;
                    }
                    // 2016.11.23 chinkeigen マニ入力と一覧 #101092 end
                        
                    if (!this.logic.ManiHimozuke()) { return; }

                    // 権限チェック
                    if (Manager.CheckAuthority("G120", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        //紐付け画面から正常に戻したら、新規モードになる
                        if (this.logic.maniRelation.result == DialogResult.OK || this.logic.maniRelation.result == DialogResult.Yes)
                        {
                            //登録後、修正モードで開きなおす(明細行キープ)
                            this.ClearScreen(e);
                            this.logic.WindowInit("SetUpdateFrom", out catchErr);
                            if (catchErr) { return; }
                        }
                        else
                        {
                            //紐付け画面を開く前に、登録することを行うので、データ設定
                            //this.logic.WindowInit()の戻り値説明、失敗：true 成功：false
                            if (this.logic.WindowInit("SetUpdateFrom", out catchErr))
                            {
                                if (catchErr) { return; }
                                // 権限チェック
                                if (Manager.CheckAuthority("G120", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                                {
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
                                    // 新規権限が無ければ閉じる
                                    this.ParentForm.Close();
                                }
                            }
                        }
                    }
                    else
                    {
                        // 修正権限が無い場合は、新規モードを表示
                        this.selectedRowNo = 0;
                        this.SetAddFrom(sender, e);
                    }
                }
                else
                {
                    return;
                }
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 start
                this.cdgrid_Jisseki.Rows[this.selectedRowNo].Cells[0].Selected = true;
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 end
            }
            else
            {
                return;
            }
            // 20140519 syunrei No.732 マニフェスト入力画面に対する機能追加（産廃マニフェスト（積替）入力） end
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
                if (Manager.CheckAuthority("G120", WINDOW_TYPE.NEW_WINDOW_FLAG))
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

        #region 削ボタン押下イベント

        /// <summary>
        /// 排出業者削ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_HaisyutuGyousyaDel_Click(object sender, EventArgs e)
        {
            this.logic.HaisyutuGyousyaDel();
            this.logic.HaisyutuJigyoubaDel();
        }

        /// <summary>
        /// 排出作業場削ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_HaisyutuJigyoubaDel_Click(object sender, EventArgs e)
        {
            this.logic.HaisyutuJigyoubaDel();
            //this.ctxt_KohuTantou.Text = string.Empty;#157148
        }

        /// <summary>
        /// 最終処分の場所 削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SaisyuGyousyaDel_Click(object sender, EventArgs e)
        {
            //2013/12/25 Tyou Upd Start
            //this.logic.SaisyuSyobunDel("cbtn_SaisyuGyousyaDel");
            this.logic.SaisyuSyobunDel("cbtn_SayishushobunDel");
            //2013/12/25 Tyou Upd End
        }

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
        /// 運搬受託者2削ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyutaku2Del_Click(object sender, EventArgs e)
        {
            this.logic.UnpanJyutaku2Del();
        }

        /// <summary>
        /// 運搬受託者3削ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyutaku3Del_Click(object sender, EventArgs e)
        {
            this.logic.UnpanJyutaku3Del();
        }

        /// <summary>
        /// 処分受託者（処分業者）削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SyobunJyutakuDel_Click(object sender, EventArgs e)
        {
            this.logic.SyobunJyutakuDel();
        }

        /// <summary>
        /// 運搬先の事業場１削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyugyobaDel_Click(object sender, EventArgs e)
        {
            if (this.crdo_JigyoubaSyobun.Checked)
            {
                this.logic.UnpanJyugyobaDel("cbtn_UnpanJyugyobaDel_Syobun");
            }
            else if (this.crdo_JigyoubaHokan.Checked)
            {
                this.logic.UnpanJyugyobaDel("cbtn_UnpanJyugyobaDel_Hokan");
            }
        }

        /// <summary>
        /// 運搬先の事業場２削ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyoba2Del_Click(object sender, EventArgs e)
        {
            if (this.crdo_Jigyouba2Syobun.Checked)
            {
                this.logic.UnpanJyugyoba2Del("cantxt_UnpanJyugyoba2Del_Syobun");
            }
            else if (this.crdo_Jigyouba2Hokan.Checked)
            {
                this.logic.UnpanJyugyoba2Del("cantxt_UnpanJyugyoba2Del_Hokan");
            }
        }

        /// <summary>
        /// 運搬先の事業場３削ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyoba3Del_Click(object sender, EventArgs e)
        {
            if (this.crdo_Jigyouba3Syobun.Checked)
            {
                this.logic.UnpanJyugyoba3Del("cantxt_UnpanJyugyoba3Del_Syobun");
            }
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
        /// 最終場所処分　削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SayishushobunDel_Click(object sender, EventArgs e)
        {
            this.logic.SaisyuBasyoSyozaiDel("cbtn_SayishushobunDel");
        }

        #endregion

        #region 斜線ボタンイベント

        /// <summary>
        /// 有害物質等ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Yugai_Click(object sender, EventArgs e)
        {
            //有害物質CD
            this.logic.SetEnableCtl(this.cantxt_Yugai);

            //有害物質名称
            this.logic.SetEnableCtl(this.txt_YugaiMei);

            //斜線
            this.logic.SetLineCtl(this.cantxt_Yugai, this.lineShape2);

            this.Refresh();
        }

        /// <summary>
        /// 中間処理産業廃棄物ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_TyukanHaikibutu_Click(object sender, EventArgs e)
        {
            //帳簿記載のとおりチェック
            this.logic.SetEnableCtl(this.ccbx_TyukanTyoubo);

            //当欄記載のとおりチェック
            this.logic.SetEnableCtl(this.ccbx_TyukanKisai);

            //中間処理産業廃棄物
            this.logic.SetEnableCtl(this.ctxt_TyukanHaikibutu);

            //斜線
            this.logic.SetLineCtl(this.ctxt_TyukanHaikibutu, this.lineShape4);

            this.Refresh();
        }

        /// <summary>
        /// 積替え又は保管ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_TumiHoka_Click(object sender, EventArgs e)
        {
            this.logic.SetEnableCtl(this.cbtn_TumiGyo);
            this.logic.SetEnableCtl(this.cbtn_TumiHokaName);
            this.logic.SetEnableCtl(this.cantxt_TumiGyoCd);
            this.logic.SetEnableCtl(this.ctxt_TumiGyoName);
            this.logic.SetEnableCtl(this.cantxt_TumiHokaNameCd);
            this.logic.SetEnableCtl(this.ctxt_TumiHokaName);
            this.logic.SetEnableCtl(this.cbtn_TumiHokaDel);
            this.logic.SetEnableCtl(this.cnt_TumiHokaZip);
            this.logic.SetEnableCtl(this.cnt_TumiHokaTel);
            this.logic.SetEnableCtl(this.ctxt_TumiHokaAdd);
            this.logic.SetLineCtl(this.cantxt_TumiGyoCd, this.lineShape1);

            this.SetTsumikaeAddressSearchEnabled(this.cantxt_TumiGyoCd.Enabled);

            this.Refresh();
        }

        /// <summary>
        /// 備考・通信欄ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJigyobaTokki_Click(object sender, EventArgs e)
        {
            this.logic.SetEnableCtl(this.ctxt_UnpanJigyobaTokki);
            this.logic.SetLineCtl(this.ctxt_UnpanJigyobaTokki, this.lineShape3);
            this.Refresh();
        }

        /// <summary>
        /// 運搬受託者（区間２）ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyutaku2_Click(object sender, EventArgs e)
        {
            if (this.cntxt_UnpanJigyoubaNm.Text != "1")
            {
                this.logic.SetEnableCtl(this.cantxt_UnpanJyutaku2NameCd);
                this.logic.SetEnableCtl(this.cantxt_UnpanJyutaku2Name);
                this.logic.SetEnableCtl(this.cbtn_UnpanJyutaku2San);
                this.logic.SetEnableCtl(this.cbtn_UnpanJyutaku2Del);
                this.logic.SetEnableCtl(this.cnt_UnpanJyutaku2Zip);
                this.logic.SetEnableCtl(this.cnt_UnpanJyutaku2Tel);
                this.logic.SetEnableCtl(this.ctxt_UnpanJyutakuAdd2);
                this.logic.SetEnableCtl(this.cantxt_Jyutaku2SyaNo);
                this.logic.SetEnableCtl(this.ctxt_Jyutaku2SyaNo);
                this.logic.SetEnableCtl(this.cantxt_Jyutaku2Syasyu);
                this.logic.SetEnableCtl(this.ctxt_Jyutaku2Syasyu);
                this.logic.SetEnableCtl(this.cantxt_UnpanJyutaku2HouhouCD);
                this.logic.SetEnableCtl(this.ctxt_UnpanJyutaku2HouhouMei);
                this.SetUnpanJutakusha2AddressSearchEnabled(this.cantxt_UnpanJyutaku2NameCd.Enabled);
            }
            this.logic.SetLineCtlEnable(this.lineShape5);

            this.Refresh();
        }

        /// <summary>
        /// 運搬受託者（区間３）ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyutaku3_Click(object sender, EventArgs e)
        {
            bool enable = false;
            if (this.cntxt_UnpanJigyoubaNm.Text == "1" || this.cntxt_UnpanJigyoubaNm2.Text == "1")
            {
                enable = true;
            }
            if (!enable)
            {
                this.logic.SetEnableCtl(this.cantxt_UnpanJyutaku3NameCd);
                this.logic.SetEnableCtl(this.cantxt_UnpanJyutaku3Name);
                this.logic.SetEnableCtl(this.cbtn_UnpanJyutaku3San);
                this.logic.SetEnableCtl(this.cbtn_UnpanJyutaku3Del);
                this.logic.SetEnableCtl(this.cnt_UnpanJyutaku3Tel);
                this.logic.SetEnableCtl(this.ctxt_UnpanJyutakuAdd3);
                this.logic.SetEnableCtl(this.cnt_UnpanJyutaku3Zip);
                this.logic.SetEnableCtl(this.cantxt_Jyutaku3SyaNo);
                this.logic.SetEnableCtl(this.ctxt_Jyutaku3SyaNo);
                this.logic.SetEnableCtl(this.cantxt_Jyutaku3Syasyu);
                this.logic.SetEnableCtl(this.ctxt_Jyutaku3Syasyu);
                this.logic.SetEnableCtl(this.cantxt_UnpanJyutaku3HouhouCD);
                this.logic.SetEnableCtl(this.ctxt_UnpanJyutaku3HouhouMei);
                this.SetUnpanJutakusha3AddressSearchEnabled(this.cantxt_UnpanJyutaku3NameCd.Enabled);
            }
            this.logic.SetLineCtlEnable(this.lineShape6);

            this.Refresh();
        }

        /// <summary>
        /// 運搬先の事業場（区間２）ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJigyouba2_Click(object sender, EventArgs e)
        {
            if (this.cntxt_UnpanJigyoubaNm.Text != "1")
            {
                this.logic.SetEnableCtl(this.cntxt_UnpanJigyoubaNm2);
                this.logic.SetEnableCtl(this.crdo_Jigyouba2Syobun);
                this.logic.SetEnableCtl(this.crdo_Jigyouba2Hokan);
                if (this.crdo_Jigyouba2Hokan.Checked)
                {
                    this.logic.SetEnableCtl(this.cantxt_UnpanJyugyobaGyoCD2);
                }
                if (this.cntxt_UnpanJigyoubaNm2.Text != string.Empty)
                {
                    this.logic.SetEnableCtl(this.cantxt_UnpanJyugyobaNameCd2);
                    this.logic.SetEnableCtl(this.cantxt_UnpanJyugyobaName2);
                    this.logic.SetEnableCtl(this.cbtn_UnpanJyugyobaSan2);
                    this.logic.SetEnableCtl(this.cnt_UnpanJyugyobaZip2);
                    this.logic.SetEnableCtl(this.cntxt_UnpanJyugyobaTel2);
                    this.logic.SetEnableCtl(this.cantxt_UnpanJyugyoba2Del);
                    this.logic.SetEnableCtl(this.ctxt_UnpanJyugyobaAdd2);
                }
                this.SetUnpansaki2AddressSearchEnabled(this.cbtn_UnpanJyugyobaSan2.Enabled);
            }
            this.logic.SetLineCtlEnable(this.lineShape7);

            this.Refresh();
        }

        /// <summary>
        /// 運搬先の事業場（区間３）ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJigyouba3_Click(object sender, EventArgs e)
        {
            bool enable = false;
            if (this.cntxt_UnpanJigyoubaNm.Text == "1" || this.cntxt_UnpanJigyoubaNm2.Text == "1")
            {
                enable = true;
            }
            if (!enable)
            {
                this.logic.SetEnableCtl(this.cntxt_UnpanJigyoubaNm3);
                this.logic.SetEnableCtl(this.crdo_Jigyouba3Syobun);
                if (this.cntxt_UnpanJigyoubaNm3.Text != string.Empty)
                {
                    this.logic.SetEnableCtl(this.cantxt_UnpanJyugyobaNameCd3);
                    this.logic.SetEnableCtl(this.cantxt_UnpanJyugyobaName3);
                    this.logic.SetEnableCtl(this.cbtn_UnpanJyugyobaSan3);
                    this.logic.SetEnableCtl(this.cnt_UnpanJyugyobaZip3);
                    this.logic.SetEnableCtl(this.cntxt_UnpanJyugyobaTel3);
                    this.logic.SetEnableCtl(this.cantxt_UnpanJyugyoba3Del);
                    this.logic.SetEnableCtl(this.ctxt_UnpanJyugyobaAdd3);
                }
                this.SetUnpansaki3AddressSearchEnabled(this.cbtn_UnpanJyugyobaSan3.Enabled);
            }
            this.logic.SetLineCtlEnable(this.lineShape8);

            this.Refresh();
        }

        /// <summary>
        /// 運搬の受託（区間２）ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyuCd2_Click(object sender, EventArgs e)
        {
            if (this.cntxt_UnpanJigyoubaNm.Text != "1")
            {
                //運搬の受託（区間２）
                this.logic.SetEnableCtl(this.cantxt_UnpanJyuCd2);
                this.logic.SetEnableCtl(this.ctxt_UnpanJyuName2);
                this.logic.SetEnableCtl(this.cbtn_UnpanJyu2Serch);
                this.logic.SetEnableCtl(this.cantxt_UnpanJyuUntenCd2);
                this.logic.SetEnableCtl(this.cantxt_UnpanJyuUntenName2);

                //運搬終了年月日（区間２）
                this.logic.SetEnableCtl(this.cdate_UnpanJyu2);

                //有価物拾得量（区間２）
                this.logic.SetEnableCtl(this.cntxt_YSuu2);
                this.logic.SetEnableCtl(this.cntxt_YTani2);
                this.logic.SetEnableCtl(this.cntxt_TaniMei2);
            }
            this.logic.SetLineCtlEnable(this.lineShape9);
            this.logic.SetLineCtlEnable(this.lineShape13);
            this.logic.SetLineCtlEnable(this.lineShape14);

            this.Refresh();
        }

        /// <summary>
        /// 運搬の受託（区間３）ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_UnpanJyuCd3_Click(object sender, EventArgs e)
        {
            bool enable = false;
            if (this.cntxt_UnpanJigyoubaNm.Text == "1" || this.cntxt_UnpanJigyoubaNm2.Text == "1")
            {
                enable = true;
            }
            if (!enable)
            {
                //運搬の受託（区間３）
                this.logic.SetEnableCtl(this.cantxt_UnpanJyuCd3);
                this.logic.SetEnableCtl(this.ctxt_UnpanJyuName3);
                this.logic.SetEnableCtl(this.cbtn_UnpanJyu3Serch);
                this.logic.SetEnableCtl(this.cantxt_UnpanJyuUntenCd3);
                this.logic.SetEnableCtl(this.cantxt_UnpanJyuUntenName3);

                //運搬終了年月日（区間３）
                this.logic.SetEnableCtl(this.cdate_UnpanJyu3);                

                //有価物拾得量（区間３）
                this.logic.SetEnableCtl(this.cntxt_YSuu3);
                this.logic.SetEnableCtl(this.cntxt_YTani3);
                this.logic.SetEnableCtl(this.cntxt_TaniMei3);                
            }
            this.logic.SetLineCtlEnable(this.lineShape10);
            this.logic.SetLineCtlEnable(this.lineShape15);
            this.logic.SetLineCtlEnable(this.lineShape16);

            this.Refresh();
        }

        /// <summary>
        /// B4ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SyougouKakuninB4_Click(object sender, EventArgs e)
        {
            this.logic.SetEnableCtl(this.cdate_SyougouKakuninB4);
            this.logic.SetLineCtl(this.cdate_SyougouKakuninB4, this.lineShape11);
            this.Refresh();
        }

        /// <summary>
        /// B6ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_SyougouKakuninB6_Click(object sender, EventArgs e)
        {
            this.logic.SetEnableCtl(this.cdate_SyougouKakuninB6);
            this.logic.SetLineCtl(this.cdate_SyougouKakuninB6, this.lineShape12);
            this.Refresh();
        }

        #endregion

        #region チェックボタン押下イベント

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

        #endregion

        #region ラジオボタン押下イベント

        /// <summary>
        /// 運搬先事業場ラジオボタンチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdo_JigyoubaSyobun_CheckedChanged(object sender, EventArgs e)
        {
            if (crdo_JigyoubaSyobun.Checked == true)
            {
                this.logic.SetTsumihoText(cntxt_UnpanJigyoubaNm, crdo_JigyoubaSyobun);
                this.logic.UnpanJyugyobaDel("crdo_JigyoubaSyobun");
                this.cantxt_UnpanJyugyobaGyoCD.Enabled = false;
                this.cantxt_UnpanJyugyobaGyoCD.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                cntxt_UnpanJigyoubaNm_Leave(null, null);

                this.cantxt_UnpanJyugyobaGyoCD.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                dto.Value = "TRUE";
                this.cantxt_UnpanJyugyobaGyoCD.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHAKBN_MANI";
                dto.Value = "TRUE";
                this.cantxt_UnpanJyugyobaGyoCD.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.Control = "cdate_KohuDate";
                dto.KeyName = "TEKIYOU_BEGIN";
                this.cantxt_UnpanJyugyobaGyoCD.PopupSearchSendParams.Add(dto);

                this.cantxt_UnpanJyugyobaNameCd.popupWindowSetting.Clear();
                this.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Clear();
                this.cbtn_UnpanJyugyobaSan.popupWindowSetting.Clear();
                this.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Clear();

                r_framework.Dto.PopupSearchSendParamDto dto1 = new r_framework.Dto.PopupSearchSendParamDto();
                dto1.And_Or = CONDITION_OPERATOR.AND;
                dto1.Control = "cantxt_SyobunJyutakuNameCd";
                dto1.KeyName = "GYOUSHA_CD";

                this.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Add(dto1);
                this.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Add(dto1);

                r_framework.Dto.JoinMethodDto dtowhere = new r_framework.Dto.JoinMethodDto();
                dtowhere.IsCheckLeftTable = true;
                dtowhere.IsCheckRightTable = true;
                dtowhere.Join = JOIN_METHOD.WHERE;
                dtowhere.LeftTable = "M_GENBA";

                r_framework.Dto.SearchConditionsDto serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.AND;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "SHOBUN_NIOROSHI_GENBA_KBN";
                serdto.Value = "True";
                serdto.ValueColumnType = DB_TYPE.BIT;
                dtowhere.SearchCondition.Add(serdto);

                serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.OR;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "SAISHUU_SHOBUNJOU_KBN";
                serdto.Value = "True";
                serdto.ValueColumnType = DB_TYPE.BIT;
                dtowhere.SearchCondition.Add(serdto);

                this.cantxt_UnpanJyugyobaNameCd.popupWindowSetting.Add(dtowhere);
                this.cbtn_UnpanJyugyobaSan.popupWindowSetting.Add(dtowhere);

                r_framework.Dto.JoinMethodDto dtowhere1 = new r_framework.Dto.JoinMethodDto();
                dtowhere1.IsCheckLeftTable = true;
                dtowhere1.IsCheckRightTable = true;
                dtowhere1.Join = JOIN_METHOD.WHERE;
                dtowhere1.LeftTable = "M_GYOUSHA";

                r_framework.Dto.SearchConditionsDto serdto1 = new r_framework.Dto.SearchConditionsDto();
                serdto1.And_Or = CONDITION_OPERATOR.AND;
                serdto1.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto1.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                serdto1.Value = "True";
                serdto1.ValueColumnType = DB_TYPE.BIT;
                dtowhere1.SearchCondition.Add(serdto1);

                serdto1 = new r_framework.Dto.SearchConditionsDto();
                serdto1.And_Or = CONDITION_OPERATOR.AND;
                serdto1.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto1.LeftColumn = "GYOUSHAKBN_MANI";
                serdto1.Value = "True";
                serdto1.ValueColumnType = DB_TYPE.BIT;
                dtowhere1.SearchCondition.Add(serdto1);

                this.cantxt_UnpanJyugyobaNameCd.popupWindowSetting.Add(dtowhere1);
                this.cbtn_UnpanJyugyobaSan.popupWindowSetting.Add(dtowhere1);

                this.cantxt_UnpanJyugyobaNameCd.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_POST,GENBA_TEL,GENBA_ADDRESS1,GYOUSHA_CD,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
                // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
                //this.cantxt_UnpanJyugyobaNameCd.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd,cantxt_UnpanJyugyobaName,cnt_UnpanJyugyobaZip,cnt_UnpanJyugyobaTel,ctxt_UnpanJyugyobaAdd,cantxt_UnpanJyugyobaGyoCD,cantxt_SyobunJyutakuNameCd,cantxt_SyobunJyutakuName";
                this.cantxt_UnpanJyugyobaNameCd.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd,x,x,x,x,cantxt_UnpanJyugyobaGyoCD,cantxt_SyobunJyutakuNameCd,x";
                // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
                this.cantxt_UnpanJyugyobaNameCd.PopupAfterExecuteMethod = "cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod";

                this.cantxt_UnpanJyugyobaNameCd.GetCodeMasterField = this.cantxt_UnpanJyugyobaNameCd.PopupGetMasterField;
                this.cantxt_UnpanJyugyobaNameCd.SetFormField = this.cantxt_UnpanJyugyobaNameCd.PopupSetFormField;

                this.cbtn_UnpanJyugyobaSan.PopupGetMasterField = this.cantxt_UnpanJyugyobaNameCd.PopupGetMasterField;
                this.cbtn_UnpanJyugyobaSan.PopupSetFormField = this.cantxt_UnpanJyugyobaNameCd.PopupSetFormField;
                this.cbtn_UnpanJyugyobaSan.PopupAfterExecuteMethod = this.cantxt_UnpanJyugyobaNameCd.PopupAfterExecuteMethod;

                this.cbtn_UnpanJyugyobaSan.SetFormField = this.cantxt_UnpanJyugyobaNameCd.PopupSetFormField;
            }

            this.SetUnpansaki1AddressSearchEnabled(this.cnt_UnpanJyugyobaZip.Enabled);
        }

        /// <summary>
        /// 運搬先事業場ラジオボタンチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdo_JigyoubaHokan_CheckedChanged(object sender, EventArgs e)
        {
            this.Crdo_JigyoubaHokan_Change();
        }

        /// <summary>
        /// 運搬先事業場ラジオボタンチェンジ
        /// </summary>
        internal void Crdo_JigyoubaHokan_Change()
        {
            if (crdo_JigyoubaHokan.Checked == true)
            {
                this.logic.SetTsumihoText(cntxt_UnpanJigyoubaNm, crdo_JigyoubaSyobun);
                this.logic.UnpanJyugyobaDel("crdo_JigyoubaHokan");
                this.cantxt_UnpanJyugyobaGyoCD.Enabled = true;
                cntxt_UnpanJigyoubaNm_Leave(null, null);

                this.cantxt_UnpanJyugyobaGyoCD.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                dto.Value = "TRUE";
                this.cantxt_UnpanJyugyobaGyoCD.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHAKBN_MANI";
                dto.Value = "TRUE";
                this.cantxt_UnpanJyugyobaGyoCD.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.Control = "cdate_KohuDate";
                dto.KeyName = "TEKIYOU_BEGIN";
                this.cantxt_UnpanJyugyobaGyoCD.PopupSearchSendParams.Add(dto);

                this.cantxt_UnpanJyugyobaGyoCD.popupWindowSetting.Clear();
                r_framework.Dto.JoinMethodDto dtoJoin = new r_framework.Dto.JoinMethodDto();
                dtoJoin.IsCheckLeftTable = true;
                dtoJoin.IsCheckRightTable = true;
                dtoJoin.Join = JOIN_METHOD.INNER_JOIN;
                dtoJoin.LeftKeyColumn = "GYOUSHA_CD";
                dtoJoin.LeftTable = "M_GENBA";
                dtoJoin.RightKeyColumn = "GYOUSHA_CD";
                dtoJoin.RightTable = "M_GYOUSHA";

                r_framework.Dto.JoinMethodDto dtowhere = new r_framework.Dto.JoinMethodDto();
                dtowhere.IsCheckLeftTable = true;
                dtowhere.IsCheckRightTable = true;
                dtowhere.Join = JOIN_METHOD.WHERE;
                dtowhere.LeftTable = "M_GENBA";
                r_framework.Dto.SearchConditionsDto serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.AND;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "TSUMIKAEHOKAN_KBN";
                serdto.Value = "True";
                serdto.ValueColumnType = DB_TYPE.BIT;
                dtowhere.SearchCondition.Add(serdto);
                this.cantxt_UnpanJyugyobaGyoCD.popupWindowSetting.Add(dtoJoin);
                this.cantxt_UnpanJyugyobaGyoCD.popupWindowSetting.Add(dtowhere);

                this.cantxt_UnpanJyugyobaNameCd.popupWindowSetting.Clear();
                this.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Clear();
                this.cbtn_UnpanJyugyobaSan.popupWindowSetting.Clear();
                this.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Clear();

                r_framework.Dto.PopupSearchSendParamDto dto1 = new r_framework.Dto.PopupSearchSendParamDto();
                dto1.And_Or = CONDITION_OPERATOR.AND;
                dto1.Control = "cantxt_UnpanJyugyobaGyoCD";
                dto1.KeyName = "GYOUSHA_CD";

                r_framework.Dto.PopupSearchSendParamDto dto2 = new r_framework.Dto.PopupSearchSendParamDto();
                dto2.And_Or = CONDITION_OPERATOR.AND;
                dto2.KeyName = "TSUMIKAEHOKAN_KBN";
                dto2.Value = "TRUE";

                r_framework.Dto.PopupSearchSendParamDto dto3 = new r_framework.Dto.PopupSearchSendParamDto();
                dto3.And_Or = CONDITION_OPERATOR.AND;
                dto3.Control = "cdate_KohuDate";
                dto3.KeyName = "TEKIYOU_BEGIN";

                this.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Add(dto1);
                this.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Add(dto2);
                this.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Add(dto3);
                this.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Add(dto1);
                this.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Add(dto2);
                this.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Add(dto3);

                r_framework.Dto.JoinMethodDto dtowhere1 = new r_framework.Dto.JoinMethodDto();
                dtowhere1.IsCheckLeftTable = true;
                dtowhere1.IsCheckRightTable = true;
                dtowhere1.Join = JOIN_METHOD.WHERE;
                dtowhere1.LeftTable = "M_GYOUSHA";

                r_framework.Dto.SearchConditionsDto serdto1 = new r_framework.Dto.SearchConditionsDto();
                serdto1.And_Or = CONDITION_OPERATOR.AND;
                serdto1.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto1.LeftColumn = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                serdto1.Value = "True";
                serdto1.ValueColumnType = DB_TYPE.BIT;
                dtowhere1.SearchCondition.Add(serdto1);

                serdto1 = new r_framework.Dto.SearchConditionsDto();
                serdto1.And_Or = CONDITION_OPERATOR.AND;
                serdto1.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto1.LeftColumn = "GYOUSHAKBN_MANI";
                serdto1.Value = "True";
                serdto1.ValueColumnType = DB_TYPE.BIT;
                dtowhere1.SearchCondition.Add(serdto1);

                this.cantxt_UnpanJyugyobaNameCd.popupWindowSetting.Add(dtowhere1);
                this.cbtn_UnpanJyugyobaSan.popupWindowSetting.Add(dtowhere1);

                this.cantxt_UnpanJyugyobaNameCd.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_POST,GENBA_TEL,GENBA_ADDRESS1,GYOUSHA_CD";
                this.cantxt_UnpanJyugyobaNameCd.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd,cantxt_UnpanJyugyobaName,cnt_UnpanJyugyobaZip,cnt_UnpanJyugyobaTel,ctxt_UnpanJyugyobaAdd,cantxt_UnpanJyugyobaGyoCD";
                this.cantxt_UnpanJyugyobaNameCd.PopupAfterExecuteMethod = "cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod";

                this.cantxt_UnpanJyugyobaNameCd.GetCodeMasterField = this.cantxt_UnpanJyugyobaNameCd.PopupGetMasterField;
                this.cantxt_UnpanJyugyobaNameCd.SetFormField = this.cantxt_UnpanJyugyobaNameCd.PopupSetFormField;

                this.cbtn_UnpanJyugyobaSan.PopupGetMasterField = this.cantxt_UnpanJyugyobaNameCd.PopupGetMasterField;
                this.cbtn_UnpanJyugyobaSan.PopupSetFormField = this.cantxt_UnpanJyugyobaNameCd.PopupSetFormField;
                this.cbtn_UnpanJyugyobaSan.PopupAfterExecuteMethod = this.cantxt_UnpanJyugyobaNameCd.PopupAfterExecuteMethod;

                this.cbtn_UnpanJyugyobaSan.SetFormField = this.cantxt_UnpanJyugyobaNameCd.PopupSetFormField;
            }

            this.SetUnpansaki1AddressSearchEnabled(this.cnt_UnpanJyugyobaZip.Enabled);
        }

        /// <summary>
        /// 運搬先事業場2ラジオボタンチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdo_Jigyouba2Syobun_CheckedChanged(object sender, EventArgs e)
        {
            if (crdo_Jigyouba2Syobun.Checked == true)
            {
                this.logic.UnpanJyugyoba2Del("crdo_Jigyouba2Syobun");
                this.cantxt_UnpanJyugyobaGyoCD2.Enabled = false;
                this.cantxt_UnpanJyugyobaGyoCD2.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                this.logic.SetTsumihoText(cntxt_UnpanJigyoubaNm2, crdo_Jigyouba2Syobun);
                cntxt_UnpanJigyoubaNm2_Leave(null, null);

                this.cantxt_UnpanJyugyobaGyoCD2.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                dto.Value = "TRUE";
                this.cantxt_UnpanJyugyobaGyoCD2.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHAKBN_MANI";
                dto.Value = "TRUE";
                this.cantxt_UnpanJyugyobaGyoCD2.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.Control = "cdate_KohuDate";
                dto.KeyName = "TEKIYOU_BEGIN";
                this.cantxt_UnpanJyugyobaGyoCD2.PopupSearchSendParams.Add(dto);

                this.cantxt_UnpanJyugyobaNameCd2.popupWindowSetting.Clear();
                this.cantxt_UnpanJyugyobaNameCd2.PopupSearchSendParams.Clear();
                this.cbtn_UnpanJyugyobaSan2.popupWindowSetting.Clear();
                this.cbtn_UnpanJyugyobaSan2.PopupSearchSendParams.Clear();

                r_framework.Dto.PopupSearchSendParamDto dto1 = new r_framework.Dto.PopupSearchSendParamDto();
                dto1.And_Or = CONDITION_OPERATOR.AND;
                dto1.Control = "cantxt_SyobunJyutakuNameCd";
                dto1.KeyName = "GYOUSHA_CD";

                r_framework.Dto.PopupSearchSendParamDto dto2 = new r_framework.Dto.PopupSearchSendParamDto();
                dto2.And_Or = CONDITION_OPERATOR.AND;
                dto2.Control = "cdate_KohuDate";
                dto2.KeyName = "TEKIYOU_BEGIN";

                this.cantxt_UnpanJyugyobaNameCd2.PopupSearchSendParams.Add(dto1);
                this.cantxt_UnpanJyugyobaNameCd2.PopupSearchSendParams.Add(dto2);
                this.cbtn_UnpanJyugyobaSan2.PopupSearchSendParams.Add(dto1);
                this.cbtn_UnpanJyugyobaSan2.PopupSearchSendParams.Add(dto2);

                r_framework.Dto.JoinMethodDto dtowhere = new r_framework.Dto.JoinMethodDto();
                dtowhere.IsCheckLeftTable = true;
                dtowhere.IsCheckRightTable = true;
                dtowhere.Join = JOIN_METHOD.WHERE;
                dtowhere.LeftTable = "M_GENBA";

                r_framework.Dto.SearchConditionsDto serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.AND;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "SHOBUN_NIOROSHI_GENBA_KBN";
                serdto.Value = "True";
                serdto.ValueColumnType = DB_TYPE.BIT;
                dtowhere.SearchCondition.Add(serdto);

                serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.OR;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "SAISHUU_SHOBUNJOU_KBN";
                serdto.Value = "True";
                serdto.ValueColumnType = DB_TYPE.BIT;
                dtowhere.SearchCondition.Add(serdto);

                this.cantxt_UnpanJyugyobaNameCd2.popupWindowSetting.Add(dtowhere);
                this.cbtn_UnpanJyugyobaSan2.popupWindowSetting.Add(dtowhere);

                r_framework.Dto.JoinMethodDto dtowhere1 = new r_framework.Dto.JoinMethodDto();
                dtowhere1.IsCheckLeftTable = true;
                dtowhere1.IsCheckRightTable = true;
                dtowhere1.Join = JOIN_METHOD.WHERE;
                dtowhere1.LeftTable = "M_GYOUSHA";

                r_framework.Dto.SearchConditionsDto serdto1 = new r_framework.Dto.SearchConditionsDto();
                serdto1.And_Or = CONDITION_OPERATOR.AND;
                serdto1.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto1.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                serdto1.Value = "True";
                serdto1.ValueColumnType = DB_TYPE.BIT;
                dtowhere1.SearchCondition.Add(serdto1);

                serdto1 = new r_framework.Dto.SearchConditionsDto();
                serdto1.And_Or = CONDITION_OPERATOR.AND;
                serdto1.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto1.LeftColumn = "GYOUSHAKBN_MANI";
                serdto1.Value = "True";
                serdto1.ValueColumnType = DB_TYPE.BIT;
                dtowhere1.SearchCondition.Add(serdto1);

                this.cantxt_UnpanJyugyobaNameCd2.popupWindowSetting.Add(dtowhere1);
                this.cbtn_UnpanJyugyobaSan2.popupWindowSetting.Add(dtowhere1);

                this.cantxt_UnpanJyugyobaNameCd2.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_POST,GENBA_TEL,GENBA_ADDRESS1,GYOUSHA_CD,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
                // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
                //this.cantxt_UnpanJyugyobaNameCd2.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd2,cantxt_UnpanJyugyobaName2,cnt_UnpanJyugyobaZip2,cntxt_UnpanJyugyobaTel2,ctxt_UnpanJyugyobaAdd2,cantxt_UnpanJyugyobaGyoCD2,cantxt_SyobunJyutakuNameCd,cantxt_SyobunJyutakuName";
                this.cantxt_UnpanJyugyobaNameCd2.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd2,x,x,x,x,cantxt_UnpanJyugyobaGyoCD2,cantxt_SyobunJyutakuNameCd,x";
                this.cantxt_UnpanJyugyobaNameCd2.PopupBeforeExecuteMethod = "cantxt_UnpanJyugyobaNameCd_PopupBeforeExecuteMethod";
                // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end
                this.cantxt_UnpanJyugyobaNameCd2.PopupAfterExecuteMethod = "cantxt_UnpanJyugyobaNameCd2_PopupAfterExecuteMethod";

                this.cantxt_UnpanJyugyobaNameCd2.GetCodeMasterField = this.cantxt_UnpanJyugyobaNameCd2.PopupGetMasterField;
                this.cantxt_UnpanJyugyobaNameCd2.SetFormField = this.cantxt_UnpanJyugyobaNameCd2.PopupSetFormField;

                this.cbtn_UnpanJyugyobaSan2.PopupGetMasterField = this.cantxt_UnpanJyugyobaNameCd2.PopupGetMasterField;
                this.cbtn_UnpanJyugyobaSan2.PopupSetFormField = this.cantxt_UnpanJyugyobaNameCd2.PopupSetFormField;
                this.cbtn_UnpanJyugyobaSan2.PopupAfterExecuteMethod = this.cantxt_UnpanJyugyobaNameCd2.PopupAfterExecuteMethod;

                this.cbtn_UnpanJyugyobaSan2.SetFormField = this.cantxt_UnpanJyugyobaNameCd2.PopupSetFormField;
            }
        }

        /// <summary>
        /// 運搬先事業場2ラジオボタンチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdo_Jigyouba2Hokan_CheckedChanged(object sender, EventArgs e)
        {
            if (crdo_Jigyouba2Hokan.Checked == true)
            {
                this.logic.SetTsumihoText(cntxt_UnpanJigyoubaNm2, crdo_Jigyouba2Syobun);
                this.logic.UnpanJyugyoba2Del("crdo_Jigyouba2Hokan");
                this.cantxt_UnpanJyugyobaGyoCD2.Enabled = true;
                cntxt_UnpanJigyoubaNm2_Leave(null, null);

                this.cantxt_UnpanJyugyobaGyoCD2.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                dto.Value = "TRUE";
                this.cantxt_UnpanJyugyobaGyoCD2.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHAKBN_MANI";
                dto.Value = "TRUE";
                this.cantxt_UnpanJyugyobaGyoCD2.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.Control = "cdate_KohuDate";
                dto.KeyName = "TEKIYOU_BEGIN";
                this.cantxt_UnpanJyugyobaGyoCD2.PopupSearchSendParams.Add(dto);

                this.cantxt_UnpanJyugyobaGyoCD2.popupWindowSetting.Clear();
                r_framework.Dto.JoinMethodDto dtoJoin = new r_framework.Dto.JoinMethodDto();
                dtoJoin.IsCheckLeftTable = true;
                dtoJoin.IsCheckRightTable = true;
                dtoJoin.Join = JOIN_METHOD.INNER_JOIN;
                dtoJoin.LeftKeyColumn = "GYOUSHA_CD";
                dtoJoin.LeftTable = "M_GENBA";
                dtoJoin.RightKeyColumn = "GYOUSHA_CD";
                dtoJoin.RightTable = "M_GYOUSHA";

                r_framework.Dto.JoinMethodDto dtowhere = new r_framework.Dto.JoinMethodDto();
                dtowhere.IsCheckLeftTable = true;
                dtowhere.IsCheckRightTable = true;
                dtowhere.Join = JOIN_METHOD.WHERE;
                dtowhere.LeftTable = "M_GENBA";
                r_framework.Dto.SearchConditionsDto serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.AND;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "TSUMIKAEHOKAN_KBN";
                serdto.Value = "True";
                serdto.ValueColumnType = DB_TYPE.BIT;
                dtowhere.SearchCondition.Add(serdto);
                this.cantxt_UnpanJyugyobaGyoCD2.popupWindowSetting.Add(dtoJoin);
                this.cantxt_UnpanJyugyobaGyoCD2.popupWindowSetting.Add(dtowhere);

                this.cantxt_UnpanJyugyobaNameCd2.popupWindowSetting.Clear();
                this.cantxt_UnpanJyugyobaNameCd2.PopupSearchSendParams.Clear();
                this.cbtn_UnpanJyugyobaSan2.popupWindowSetting.Clear();
                this.cbtn_UnpanJyugyobaSan2.PopupSearchSendParams.Clear();

                r_framework.Dto.PopupSearchSendParamDto dto1 = new r_framework.Dto.PopupSearchSendParamDto();
                dto1.And_Or = CONDITION_OPERATOR.AND;
                dto1.Control = "cantxt_UnpanJyugyobaGyoCD2";
                dto1.KeyName = "GYOUSHA_CD";

                r_framework.Dto.PopupSearchSendParamDto dto2 = new r_framework.Dto.PopupSearchSendParamDto();
                dto2.And_Or = CONDITION_OPERATOR.AND;
                dto2.KeyName = "TSUMIKAEHOKAN_KBN";
                dto2.Value = "TRUE";

                r_framework.Dto.PopupSearchSendParamDto dto3 = new r_framework.Dto.PopupSearchSendParamDto();
                dto3.And_Or = CONDITION_OPERATOR.AND;
                dto3.Control = "cdate_KohuDate";
                dto3.KeyName = "TEKIYOU_BEGIN";

                this.cantxt_UnpanJyugyobaNameCd2.PopupSearchSendParams.Add(dto1);
                this.cantxt_UnpanJyugyobaNameCd2.PopupSearchSendParams.Add(dto2);
                this.cantxt_UnpanJyugyobaNameCd2.PopupSearchSendParams.Add(dto3);
                this.cbtn_UnpanJyugyobaSan2.PopupSearchSendParams.Add(dto1);
                this.cbtn_UnpanJyugyobaSan2.PopupSearchSendParams.Add(dto2);
                this.cbtn_UnpanJyugyobaSan2.PopupSearchSendParams.Add(dto3);

                r_framework.Dto.JoinMethodDto dtowhere1 = new r_framework.Dto.JoinMethodDto();
                dtowhere1.IsCheckLeftTable = true;
                dtowhere1.IsCheckRightTable = true;
                dtowhere1.Join = JOIN_METHOD.WHERE;
                dtowhere1.LeftTable = "M_GYOUSHA";

                r_framework.Dto.SearchConditionsDto serdto1 = new r_framework.Dto.SearchConditionsDto();
                serdto1.And_Or = CONDITION_OPERATOR.AND;
                serdto1.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto1.LeftColumn = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                serdto1.Value = "True";
                serdto1.ValueColumnType = DB_TYPE.BIT;
                dtowhere1.SearchCondition.Add(serdto1);

                serdto1 = new r_framework.Dto.SearchConditionsDto();
                serdto1.And_Or = CONDITION_OPERATOR.AND;
                serdto1.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto1.LeftColumn = "GYOUSHAKBN_MANI";
                serdto1.Value = "True";
                serdto1.ValueColumnType = DB_TYPE.BIT;
                dtowhere1.SearchCondition.Add(serdto1);

                this.cantxt_UnpanJyugyobaNameCd2.popupWindowSetting.Add(dtowhere1);
                this.cbtn_UnpanJyugyobaSan2.popupWindowSetting.Add(dtowhere1);

                this.cantxt_UnpanJyugyobaNameCd2.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_POST,GENBA_TEL,GENBA_ADDRESS1,GYOUSHA_CD";
                this.cantxt_UnpanJyugyobaNameCd2.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd2,cantxt_UnpanJyugyobaName2,cnt_UnpanJyugyobaZip2,cntxt_UnpanJyugyobaTel2,ctxt_UnpanJyugyobaAdd2,cantxt_UnpanJyugyobaGyoCD2";
                this.cantxt_UnpanJyugyobaNameCd2.PopupAfterExecuteMethod = "cantxt_UnpanJyugyobaNameCd2_PopupAfterExecuteMethod";

                this.cantxt_UnpanJyugyobaNameCd2.GetCodeMasterField = this.cantxt_UnpanJyugyobaNameCd2.PopupGetMasterField;
                this.cantxt_UnpanJyugyobaNameCd2.SetFormField = this.cantxt_UnpanJyugyobaNameCd2.PopupSetFormField;

                this.cbtn_UnpanJyugyobaSan2.PopupGetMasterField = this.cantxt_UnpanJyugyobaNameCd2.PopupGetMasterField;
                this.cbtn_UnpanJyugyobaSan2.PopupSetFormField = this.cantxt_UnpanJyugyobaNameCd2.PopupSetFormField;
                this.cbtn_UnpanJyugyobaSan2.PopupAfterExecuteMethod = this.cantxt_UnpanJyugyobaNameCd2.PopupAfterExecuteMethod;

                this.cbtn_UnpanJyugyobaSan2.SetFormField = this.cantxt_UnpanJyugyobaNameCd2.PopupSetFormField;
            }
        }

        /// <summary>
        /// 運搬先事業場3ラジオボタンチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdo_Jigyouba3Syobun_CheckedChanged(object sender, EventArgs e)
        {
            if (crdo_Jigyouba3Syobun.Checked == true)
            {
                this.logic.UnpanJyugyoba3Del("crdo_Jigyouba3Syobun");
                this.cantxt_UnpanJyugyobaGyoCD3.Enabled = false;
                this.cantxt_UnpanJyugyobaGyoCD3.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                this.logic.SetTsumihoText(cntxt_UnpanJigyoubaNm3, crdo_Jigyouba3Syobun);
                cntxt_UnpanJigyoubaNm3_Leave(null, null);

                this.cantxt_UnpanJyugyobaGyoCD3.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                dto.Value = "TRUE";
                this.cantxt_UnpanJyugyobaGyoCD3.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHAKBN_MANI";
                dto.Value = "TRUE";
                this.cantxt_UnpanJyugyobaGyoCD3.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.Control = "cdate_KohuDate";
                dto.KeyName = "TEKIYOU_BEGIN";
                this.cantxt_UnpanJyugyobaGyoCD3.PopupSearchSendParams.Add(dto);

                this.cantxt_UnpanJyugyobaNameCd3.popupWindowSetting.Clear();
                this.cantxt_UnpanJyugyobaNameCd3.PopupSearchSendParams.Clear();
                this.cbtn_UnpanJyugyobaSan3.popupWindowSetting.Clear();
                this.cbtn_UnpanJyugyobaSan3.PopupSearchSendParams.Clear();

                r_framework.Dto.PopupSearchSendParamDto dto1 = new r_framework.Dto.PopupSearchSendParamDto();
                dto1.And_Or = CONDITION_OPERATOR.AND;
                dto1.Control = "cantxt_SyobunJyutakuNameCd";
                dto1.KeyName = "GYOUSHA_CD";

                r_framework.Dto.PopupSearchSendParamDto dto2 = new r_framework.Dto.PopupSearchSendParamDto();
                dto2.And_Or = CONDITION_OPERATOR.AND;
                dto2.Control = "cdate_KohuDate";
                dto2.KeyName = "TEKIYOU_BEGIN";

                this.cantxt_UnpanJyugyobaNameCd3.PopupSearchSendParams.Add(dto1);
                this.cantxt_UnpanJyugyobaNameCd3.PopupSearchSendParams.Add(dto2);
                this.cbtn_UnpanJyugyobaSan3.PopupSearchSendParams.Add(dto1);
                this.cbtn_UnpanJyugyobaSan3.PopupSearchSendParams.Add(dto2);

                r_framework.Dto.JoinMethodDto dtowhere = new r_framework.Dto.JoinMethodDto();
                dtowhere.IsCheckLeftTable = true;
                dtowhere.IsCheckRightTable = true;
                dtowhere.Join = JOIN_METHOD.WHERE;
                dtowhere.LeftTable = "M_GENBA";

                r_framework.Dto.SearchConditionsDto serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.AND;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "SHOBUN_NIOROSHI_GENBA_KBN";
                serdto.Value = "True";
                serdto.ValueColumnType = DB_TYPE.BIT;
                dtowhere.SearchCondition.Add(serdto);

                serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.OR;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "SAISHUU_SHOBUNJOU_KBN";
                serdto.Value = "True";
                serdto.ValueColumnType = DB_TYPE.BIT;
                dtowhere.SearchCondition.Add(serdto);

                this.cantxt_UnpanJyugyobaNameCd3.popupWindowSetting.Add(dtowhere);
                this.cbtn_UnpanJyugyobaSan3.popupWindowSetting.Add(dtowhere);

                r_framework.Dto.JoinMethodDto dtowhere1 = new r_framework.Dto.JoinMethodDto();
                dtowhere1.IsCheckLeftTable = true;
                dtowhere1.IsCheckRightTable = true;
                dtowhere1.Join = JOIN_METHOD.WHERE;
                dtowhere1.LeftTable = "M_GYOUSHA";

                r_framework.Dto.SearchConditionsDto serdto1 = new r_framework.Dto.SearchConditionsDto();
                serdto1.And_Or = CONDITION_OPERATOR.AND;
                serdto1.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto1.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                serdto1.Value = "True";
                serdto1.ValueColumnType = DB_TYPE.BIT;
                dtowhere1.SearchCondition.Add(serdto1);

                serdto1 = new r_framework.Dto.SearchConditionsDto();
                serdto1.And_Or = CONDITION_OPERATOR.AND;
                serdto1.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto1.LeftColumn = "GYOUSHAKBN_MANI";
                serdto1.Value = "True";
                serdto1.ValueColumnType = DB_TYPE.BIT;
                dtowhere1.SearchCondition.Add(serdto1);

                this.cantxt_UnpanJyugyobaNameCd3.popupWindowSetting.Add(dtowhere1);
                this.cbtn_UnpanJyugyobaSan3.popupWindowSetting.Add(dtowhere1);

                this.cantxt_UnpanJyugyobaNameCd3.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_POST,GENBA_TEL,GENBA_ADDRESS1,GYOUSHA_CD,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
                // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
                //this.cantxt_UnpanJyugyobaNameCd3.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd3,cantxt_UnpanJyugyobaName3,cnt_UnpanJyugyobaZip3,cntxt_UnpanJyugyobaTel3,ctxt_UnpanJyugyobaAdd3,cantxt_UnpanJyugyobaGyoCD3,cantxt_SyobunJyutakuNameCd,cantxt_SyobunJyutakuName";
                this.cantxt_UnpanJyugyobaNameCd3.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd3,x,x,x,x,cantxt_UnpanJyugyobaGyoCD3,cantxt_SyobunJyutakuNameCd,x";
                this.cantxt_UnpanJyugyobaNameCd3.PopupBeforeExecuteMethod = "cantxt_UnpanJyugyobaNameCd_PopupBeforeExecuteMethod";
                // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end
                this.cantxt_UnpanJyugyobaNameCd3.PopupAfterExecuteMethod = "cantxt_UnpanJyugyobaNameCd3_PopupAfterExecuteMethod";

                this.cantxt_UnpanJyugyobaNameCd3.GetCodeMasterField = this.cantxt_UnpanJyugyobaNameCd3.PopupGetMasterField;
                this.cantxt_UnpanJyugyobaNameCd3.SetFormField = this.cantxt_UnpanJyugyobaNameCd3.PopupSetFormField;

                this.cbtn_UnpanJyugyobaSan3.PopupGetMasterField = this.cantxt_UnpanJyugyobaNameCd3.PopupGetMasterField;
                this.cbtn_UnpanJyugyobaSan3.PopupSetFormField = this.cantxt_UnpanJyugyobaNameCd3.PopupSetFormField;
                this.cbtn_UnpanJyugyobaSan3.PopupAfterExecuteMethod = this.cantxt_UnpanJyugyobaNameCd3.PopupAfterExecuteMethod;

                this.cbtn_UnpanJyugyobaSan3.SetFormField = this.cantxt_UnpanJyugyobaNameCd3.PopupSetFormField;
            }
        }
        #endregion

        #endregion

        #region タブ

        //タブコントロールの色塗り
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
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
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (DgvFlg == false)
            {
                e.Cancel = true;
            }

            // 20140519 syunrei No.732 マニフェスト入力画面に対する機能追加（産廃マニフェスト（積替）入力） start
            switch (e.TabPageIndex)
            {
                case 0:
                    //実績を選んだ場合、下の「原本」タブのコントロールが入力不可になる
                    this.logic.SetSaisyuCtr(false);
                    break;

                case 1:
                    //原本を選んだ場合、下の「原本」タブのコントロールが入力可になる
                    this.logic.SetSaisyuCtr(true);
                    break;
            }
            this.tabControl2.Refresh();
            // 20140519 syunrei No.732 マニフェスト入力画面に対する機能追加（産廃マニフェスト（積替）入力） end
        }

        // 20140519 syunrei No.732 マニフェスト入力画面に対する機能追加（産廃マニフェスト（積替）入力） start
        //タブコントロールの色塗り
        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
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
            if (e.Index == tab.SelectedIndex && this.tabControl1.SelectedTab.Text.Equals("原本"))
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

        // 20140519 syunrei No.732 マニフェスト入力画面に対する機能追加（産廃マニフェスト（積替）入力） end

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
            if (this.cdgrid_Jisseki.Rows[iRow].Cells[(int)SampaiManifestoThumiKae.enumCols.SaisyuGyosyaCd].Value != null &&
                this.cdgrid_Jisseki.Rows[iRow].Cells[(int)SampaiManifestoThumiKae.enumCols.SaisyuGyosyaCd].Value.ToString() != string.Empty)
            {
                //条件変更
                this.SaisyuBasyoCd.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHA_CD";
                dto.Value = this.cdgrid_Jisseki.Rows[iRow].Cells[(int)SampaiManifestoThumiKae.enumCols.SaisyuGyosyaCd].Value.ToString();
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

        // 20140513 syunrei No.679 産廃マニフェスト（積替）入力連携 start

        #region 伝種区分

        /// <summary>
        /// 伝種区分 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_DenshuKbn_Validating(object sender, EventArgs e)
        {
            if (!this.chkRenkeiDenshuKbnFlg)
            {
                this.chkRenkeiDenshuKbnFlg = true;
                return;
            }

            //値変更フラグ
            bool valueChangeFlg = isChanged(sender);

            //伝種区分チェック
            if (!this.logic.ChkDenshu())
            {
                return;
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
                    if (int.Parse(dt.Rows[0].ItemArray[10].ToString()) == 2)
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

            //連携番号のラベル
            if (string.IsNullOrEmpty(cantxt_DenshuKbn.Text))
            {
                this.lbl_No.Text = "連携番号";
            }
            else
            {
                this.logic.SetRenkeiLabel(this.cantxt_DenshuKbn.Text);
            }

            //伝種区分、連携番号、明細行にいずれかフォーカスがあったら、該当として、true。以外の場合、該当外として、false。
            bool renkeiFlag = this.cantxt_DenshuKbn.Focused || this.cantxt_No.Focused || this.cantxt_Meisaigyou.Focused;

            //enter tabキー　focusedが無効に対応
            if (base.KeyEventKP != null)
            {
                if ((base.KeyEventKP.KeyCode == Keys.Tab || base.KeyEventKP.KeyCode == Keys.Enter) && base.KeyEventKP.Shift && this.cantxt_DenshuKbn.Focused)
                {
                    renkeiFlag = false;
                }
            }

            // 入力チェックを行う
            if (this.logic.ChkRenkei(renkeiFlag))
            {
                //連携データ検索
                this.logic.SetRenkeiData(valueChangeFlg, renkeiFlag);
            }
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

        #endregion

        #region 連携番号

        /// <summary>
        /// 連携番号 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_No_Validating(object sender, EventArgs e)
        {
            //値変更フラグ
            bool valueChangeFlg = isChanged(sender);
            //伝種区分、連携番号、明細行にいずれかフォーカスがあったら、該当として、true。以外の場合、該当外として、false。
            bool renkeiFlag = this.cantxt_DenshuKbn.Focused || this.cantxt_No.Focused || this.cantxt_Meisaigyou.Focused;
            if (base.KeyEventKP != null)
            {
                if ((base.KeyEventKP.KeyCode == Keys.Tab || base.KeyEventKP.KeyCode == Keys.Enter) && !base.KeyEventKP.Shift && this.cantxt_No.Focused)
                {
                    renkeiFlag = false;
                }
            }
            // 入力チェックを行う
            if (this.logic.ChkRenkei(renkeiFlag))
            {
                //連携データ検索
                this.logic.SetRenkeiData(valueChangeFlg, renkeiFlag);
            }
        }

        #endregion

        #region 連携明細行

        /// <summary>
        /// 連携明細行 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Meisaigyou_Validating(object sender, EventArgs e)
        {
            //値変更フラグ
            bool valueChangeFlg = isChanged(sender);
            //伝種区分、連携番号、明細行にいずれかフォーカスがあったら、該当として、true。以外の場合、該当外として、false。
            bool renkeiFlag = this.cantxt_DenshuKbn.Focused || this.cantxt_No.Focused || this.cantxt_Meisaigyou.Focused;

            //enter tabキー　focusedが無効に対応
            if (base.KeyEventKP != null)
            {
                if ((base.KeyEventKP.KeyCode == Keys.Tab || base.KeyEventKP.KeyCode == Keys.Enter) && !base.KeyEventKP.Shift && this.cantxt_Meisaigyou.Focused)
                {
                    renkeiFlag = false;
                }
            }
            // 入力チェックを行う
            if (this.logic.ChkRenkei(renkeiFlag))
            {
                //連携データ検索
                this.logic.SetRenkeiData(valueChangeFlg, renkeiFlag);
            }
        }

        #endregion

        // 20140513 syunrei No.679 産廃マニフェスト（積替）入力連携 end

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
        /// 交付番号テキストフォーカスValidatingイベント
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
            if (Manager.CheckAuthority("G120", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                //修正モード
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                Updflg = true;

                this.parameters.Mode = (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            }
            else if (Manager.CheckAuthority("G120", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
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

            this.ClearScreen(e);

            this.logic.WindowInit("cantxt_KohuNo", out catchErr);
        }

        #endregion

        #region 排出業者

        /// <summary>
        /// 排出業者CD Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuGyousyaCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 排出業者CD Validatedイベント
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
        /// 排出業者CD  PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod()
        {
            if (!string.Equals(this.cantxt_HaisyutuGyousyaCd.Text, preGyoushaCd))
            {
                //排出業者削除
                this.logic.HaisyutuJigyoubaDel();
            }

            // 20140612 katen 不具合No.4469 start
            //ManifestoLogic.SetAddrGyousha("All",cantxt_HaisyutuGyousyaCd, ctxt_HaisyutuGyousyaName1,
            //    cnt_HaisyutuGyousyaZip, cnt_HaisyutuGyousyaTel, ctxt_HaisyutuGyousyaAdd1,
            //    null,null,null
            //    , true, false, false, false);
            //業者　設定‏
            this.logic.SetAddrGyousha("All", cantxt_HaisyutuGyousyaCd, new CustomTextBox[] { ctxt_HaisyutuGyousyaName1, ctxt_HaisyutuGyousyaName2 },
                cnt_HaisyutuGyousyaZip, cnt_HaisyutuGyousyaTel, new CustomTextBox[] { ctxt_HaisyutuGyousyaAdd1, ctxt_HaisyutuGyousyaAdd2 },
                null, null, null
                , true, false, false, false, this.logic.isNotNeedDeleteFlg);
            // 20140612 katen 不具合No.4469 end
        }

        /// <summary>
        /// 排出業者CD  PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuGyousyaCd_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCd = this.cantxt_HaisyutuGyousyaCd.Text;
        }

        public void cbtn_HaisyutuGyousyaSan_PopupAfterExecuteMethod()
        {
            int ret = this.logic.SetAddrGyousha("All", cantxt_HaisyutuGyousyaCd, new CustomTextBox[] { ctxt_HaisyutuGyousyaName1, ctxt_HaisyutuGyousyaName2 },
                cnt_HaisyutuGyousyaZip, cnt_HaisyutuGyousyaTel, new CustomTextBox[] { ctxt_HaisyutuGyousyaAdd1, ctxt_HaisyutuGyousyaAdd2 },
                null, null, null
                , true, false, false, false);
            if (ret == -1) { return; }
            if (!string.Equals(this.cantxt_HaisyutuGyousyaCd.Text, preGyoushaCd))
            {
                //排出業者削除
                this.logic.HaisyutuJigyoubaDel();
            }
        }

        public void cbtn_HaisyutuGyousyaSan_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCd = this.cantxt_HaisyutuGyousyaCd.Text;
        }

        #endregion

        #region 排出事業場

        /// <summary>
        /// 排出事業場 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuJigyoubaName_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 排出事業場 Validatedイベント
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

            //排出事業場のチェック
            switch (this.logic.ChkJigyouba(cantxt_HaisyutuJigyoubaName, cantxt_HaisyutuGyousyaCd, "HAISHUTSU_NIZUMI_GENBA_KBN"))
            {
                case 0://正常

                    if (string.IsNullOrEmpty(ctxt_KohuTantou.Text))
                    {
                        //交付担当者を引用
                        ManifestoLogic.GetKoufuTantousha(cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, ctxt_KohuTantou, this.logic.isNotNeedDeleteFlg);
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
            ////2014.05.19 by 胡　start
            //// <summary>
            //// シ排出事業場を選択後、システム設定のA～E票使用区分が「使用しない」となっていた場合でも、
            //// 現場のA～E票使用区分が「使用しない」となっていたときは、グレーアウトする。
            //this.logic.SetHenkyakuhiNyuuryokuEnabled();
            ////2014.05.19 by 胡　end
        }

        /// <summary>
        /// 排出事業場 PopupAfterExecuteMethod
        /// </summary>
        // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start‏
        //public void cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod()
        public void cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod(ICustomControl sender = null)
        // 20140618 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end
        {
            // 20140612 katen 不具合No.4469 start
            //排出業者　設定
            //ManifestoLogic.SetAddrGyousha("All", cantxt_HaisyutuGyousyaCd, ctxt_HaisyutuGyousyaName1,
            //    cnt_HaisyutuGyousyaZip, cnt_HaisyutuGyousyaTel, ctxt_HaisyutuGyousyaAdd1,
            //    null, null, null
            //    , true, false, false, false);

            ////排出事業場　設定
            //this.mlogic.SetAddressJigyouba("All", cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, ctxt_HaisyutuJigyoubaName1,
            //    cnt_HaisyutuJigyoubaZip, cnt_HaisyutuJigyoubaTel, ctxt_HaisyutuJigyoubaAdd1, null
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
            // 20140612 katen 不具合No.4469 end

            if (string.IsNullOrEmpty(ctxt_KohuTantou.Text))
            {
                //交付担当者を引用
                ManifestoLogic.GetKoufuTantousha(cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, ctxt_KohuTantou, this.logic.isNotNeedDeleteFlg);
            }
            //2014.05.19 by 胡　start
            // <summary>
            // シ排出事業場を選択後、システム設定のA～E票使用区分が「使用しない」となっていた場合でも、
            // 現場のA～E票使用区分が「使用しない」となっていたときは、グレーアウトする。
            this.logic.SetHenkyakuhiNyuuryokuEnabled();
            //2014.05.19 by 胡　end
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
        private void cantxt_KongoCd_PopupAfterExecuteMethod()
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

            //フォーマットはFWで
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
            this.logic.SetGridGyosya(iRow);
        }

        /// <summary>
        /// 最終処分業者 PopupBeforeExecuteMethod
        /// </summary>
        public void cdgrid_Jisseki_SaisyuGyosyaCd_PopupBeforeExecuteMethod()
        {
            int iRow = this.cdgrid_Jisseki.CurrentCell.RowIndex;
            this.preGyoushaCd = (this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value == null) ? string.Empty :
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
            if (!this.logic.SetGridJigyouba(iRow)) { return; }

            if (this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value == null ||
                this.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value.ToString() == string.Empty)
            {
                return;
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

                case (int)enumCols.TaniName://単位名
                    this.DgvFlg = true;
                    return true;

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
                //if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.KansangoSuryo].Value)))
                //{
                //    cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.KansangoSuryo].Value = (0).ToString(this.logic.ManifestSuuryoFormat);
                //}

                ////減算後数量
                //if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.GenyoyugoTotalSuryo].Value)))
                //{
                //    cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.GenyoyugoTotalSuryo].Value = (0).ToString(this.logic.ManifestSuuryoFormat);
                //}
                if (!String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.HaikiCd].Value))
                     && !String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.Suryo].Value))
                     && !String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.TaniCd].Value)))
                {
                    //換算後数量
                    if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.KansangoSuryo].Value)))
                    {
                        cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.KansangoSuryo].Value = (0).ToString(this.logic.ManifestSuuryoFormat);
                    }

                    //減算後数量
                    if (String.IsNullOrEmpty(Convert.ToString(cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.GenyoyugoTotalSuryo].Value)))
                    {
                        cdgrid_Jisseki.Rows[row].Cells[(int)SampaiManifestoThumiKae.enumCols.GenyoyugoTotalSuryo].Value = (0).ToString(this.logic.ManifestSuuryoFormat);
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
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL start

            return;
        }

        #endregion

        #endregion

        #region 原本タブ

        #region 産業廃棄物

        /// <summary>
        /// 種類 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyuruiCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_SyuruiCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 種類 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyuruiCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            this.logic.ChkHaiki(cantxt_SyuruiCd, ctxt_SyuruiName);
        }

        /// <summary>
        /// 種類 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyuruiCd_PopupAfterExecuteMethod()
        {
            //チェックボックスの自動反映（ポップアップの場合は選択時に行う）
            this.logic.SetTokkanCheck(cantxt_SyuruiCd.Text);
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

        #endregion

        #region 数量(及び単位)

        /// <summary>
        /// 数量(及び単位) 数量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Suryo_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 数量(及び単位) 数量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Suryo_Validated(object sender, EventArgs e)
        {
            //this.logic.SetYukaJyuuryo(this.cantxt_Suryo);
        }

        /// <summary>
        /// 数量(及び単位) 単位 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_Tani_Validating(object sender, CancelEventArgs e)
        {
            this.cntxt_Tani_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 数量(及び単位) 単位 PopupAfterExecuteMethod
        /// </summary>
        public void cntxt_Tani_PopupAfterExecuteMethod()
        {
        }

        #endregion

        #region 有害物質等

        /// <summary>
        /// 有害物質等 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Yugai_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_Yugai_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 有害物質等 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Yugai_PopupAfterExecuteMethod()
        {
        }

        #endregion

        #endregion

        #region 最終処分の場所

        /// <summary>
        /// 最終処分の場所 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuGyousyaCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_SaisyuGyousyaCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 最終処分の場所 業者 Validatedイベント
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
        /// 最終処分の場所 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SaisyuGyousyaCd_PopupAfterExecuteMethod()
        {
            if (this.cantxt_SaisyuGyousyaCd.Text != this.preGyoushaCd)
            {
                this.ccbx_SaisyuTyoubo.Checked = false;
                this.ccbx_SaisyuKisai.Checked = true;

                // 最終処分の場所を初期化する
                this.logic.SaisyuSyobunDel("cantxt_SaisyuGyousyaNameCd");
            }
        }

        public void cantxt_SaisyuGyousyaCd_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCd = this.cantxt_SaisyuGyousyaCd.Text;
        }

        /// <summary>
        /// 最終処分の場所 事業場 Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuGyousyaNameCd_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_SaisyuGyousyaNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 最終処分の場所 事業場 Validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuGyousyaNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            //20150629 #5005 hoanghm start
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
            //20150629 #5005 hoanghm end

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
        /// 最終処分の場所 事業場 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SaisyuGyousyaNameCd_PopupAfterExecuteMethod()
        {
            this.ccbx_SaisyuTyoubo.Checked = false;
            this.ccbx_SaisyuKisai.Checked = true;

            //排出事業場　設定
            this.mlogic.SetAddressJigyouba("All", cantxt_SaisyuGyousyaCd, cantxt_SaisyuGyousyaNameCd, ctxt_SaisyuGyousyaName, cnt_SaisyuGyousyaZip, cnt_SaisyuGyousyaTel, cnt_SaisyuGyousyaAdd, null
                , false, true, false, false);
        }

        #endregion

        #region 運搬受託者(区間１)

        /// <summary>
        /// 運搬受託者(1) Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyutaku1NameCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(1) Validatedイベント
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
                    // 20140602 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す start
                    //運搬受託者と運搬の受託者 事業者削除
                    this.cantxt_UnpanJyutaku1Clear();
                    // 20140602 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す end
                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(1) PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod()
        {
            //業者　設定
            // 20140617 ria EV004895 運搬受託者を入力した時、運搬の受託項目の運搬受託者名が業者名１のみしかセットされない。 start
            //ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku1NameCd, cantxt_UnpanJyutaku1Name,
            //    cnt_UnpanJyutaku1Zip, cnt_UnpanJyutaku1Tel, ctxt_UnpanJyutakuAdd,
            //    "Part1", cantxt_UnpanJyuCd1, ctxt_UnpanJyuName1
            //    , false, false, true, false);
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku1NameCd, cantxt_UnpanJyutaku1Name,
                cnt_UnpanJyutaku1Zip, cnt_UnpanJyutaku1Tel, ctxt_UnpanJyutakuAdd,
                "All", cantxt_UnpanJyuCd1, ctxt_UnpanJyuName1
                , false, false, true, false, this.logic.isNotNeedDeleteFlg);
            // 20140617 ria EV004895 運搬受託者を入力した時、運搬の受託項目の運搬受託者名が業者名１のみしかセットされない。 end
        }

        /// <summary>
        /// 車種(1) Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku1Syasyu_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_Jyutaku1Syasyu_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 車種(1) PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Jyutaku1Syasyu_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 車輌CD(1) Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku1SyaNo_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_Jyutaku1SyaNo_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 車輌CD(1) Validatedイベント
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
        /// 車輌CD(1) PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Jyutaku1SyaNo_PopupAfterExecuteMethod()
        {
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku1NameCd, cantxt_UnpanJyutaku1Name,
                cnt_UnpanJyutaku1Zip, cnt_UnpanJyutaku1Tel, ctxt_UnpanJyutakuAdd,
                null, null, null
                , false, false, false, false); //車輌からの業者は 区分見ない？
        }

        #endregion

        #region 運搬受託者(区間２)

        /// <summary>
        /// 運搬受託者(2) Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyutaku2NameCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyutaku2NameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(2) Validatedイベント
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
                    // 20140602 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す start
                    //運搬受託者と運搬の受託者 事業者削除
                    this.cantxt_UnpanJyutaku2Clear();
                    // 20140602 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す end
                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_UnpanJyutaku2NameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(2) PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyutaku2NameCd_PopupAfterExecuteMethod()
        {
            //業者　設定
            // 20140617 ria EV004895 運搬受託者を入力した時、運搬の受託項目の運搬受託者名が業者名１のみしかセットされない。 start
            //ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku2NameCd, cantxt_UnpanJyutaku2Name,
            //    cnt_UnpanJyutaku2Zip, cnt_UnpanJyutaku2Tel, ctxt_UnpanJyutakuAdd2,
            //    "Part1", cantxt_UnpanJyuCd2, ctxt_UnpanJyuName2
            //    , false, false, true, false);
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku2NameCd, cantxt_UnpanJyutaku2Name,
                cnt_UnpanJyutaku2Zip, cnt_UnpanJyutaku2Tel, ctxt_UnpanJyutakuAdd2,
                "All", cantxt_UnpanJyuCd2, ctxt_UnpanJyuName2
                , false, false, true, false);
            // 20140617 ria EV004895 運搬受託者を入力した時、運搬の受託項目の運搬受託者名が業者名１のみしかセットされない。 end
        }

        /// <summary>
        /// 車種(2) Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku2Syasyu_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_Jyutaku2Syasyu_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 車種(2) PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Jyutaku2Syasyu_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 車輌CD(2) Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku2SyaNo_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_Jyutaku2SyaNo_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 車輌CD(2) Validatedイベント
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
        /// 車輌CD(2) PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Jyutaku2SyaNo_PopupAfterExecuteMethod()
        {
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku2NameCd, cantxt_UnpanJyutaku2Name,
                cnt_UnpanJyutaku2Zip, cnt_UnpanJyutaku2Tel, ctxt_UnpanJyutakuAdd2,
                null, null, null
                , false, false, false, false); //車輌からの業者は区分見ない？
        }

        #endregion

        #region 運搬受託者(区間３)

        /// <summary>
        /// 運搬受託者(3) Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyutaku3NameCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyutaku3NameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(3) Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyutaku3NameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            switch (this.logic.ChkGyosya(cantxt_UnpanJyutaku3NameCd, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
            {
                case 0://正常
                    this.logic.isInputError = false;
                    break;

                case 1://空
                    // 20140602 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す start
                    //運搬受託者と運搬の受託者 事業者削除
                    this.cantxt_UnpanJyutaku3Clear();
                    // 20140602 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す end
                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_UnpanJyutaku3NameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬受託者(3) PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyutaku3NameCd_PopupAfterExecuteMethod()
        {
            //業者　設定
            // 20140617 ria EV004895 運搬受託者を入力した時、運搬の受託項目の運搬受託者名が業者名１のみしかセットされない。 start
            //ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku3NameCd, cantxt_UnpanJyutaku3Name,
            //    cnt_UnpanJyutaku3Zip, cnt_UnpanJyutaku3Tel, ctxt_UnpanJyutakuAdd3,
            //    "Part1", cantxt_UnpanJyuCd3, ctxt_UnpanJyuName3
            //    , false, false, true, false);
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku3NameCd, cantxt_UnpanJyutaku3Name,
                cnt_UnpanJyutaku3Zip, cnt_UnpanJyutaku3Tel, ctxt_UnpanJyutakuAdd3,
                "All", cantxt_UnpanJyuCd3, ctxt_UnpanJyuName3
                , false, false, true, false);
            // 20140617 ria EV004895 運搬受託者を入力した時、運搬の受託項目の運搬受託者名が業者名１のみしかセットされない。 end
        }

        /// <summary>
        /// 車種(3) Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku3Syasyu_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_Jyutaku3Syasyu_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 車種(3) PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Jyutaku3Syasyu_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 車輌CD(3) Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku3SyaNo_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_Jyutaku3SyaNo_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 車輌CD(3) Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Jyutaku3SyaNo_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.logic.ChkCarData(cantxt_UnpanJyutaku3NameCd, cantxt_Jyutaku3Syasyu, ctxt_Jyutaku3Syasyu, cantxt_Jyutaku3SyaNo, ctxt_Jyutaku3SyaNo))
            {
                case 0://正常
                    break;

                case 1://空
                    this.ctxt_Jyutaku3SyaNo.Text = string.Empty;
                    return;

                case 2://エラー
                    this.ctxt_Jyutaku3SyaNo.Text = string.Empty;
                    return;
            }
            cantxt_Jyutaku3SyaNo_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 車輌CD(3) PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_Jyutaku3SyaNo_PopupAfterExecuteMethod()
        {
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyutaku3NameCd, cantxt_UnpanJyutaku3Name,
                cnt_UnpanJyutaku3Zip, cnt_UnpanJyutaku3Tel, ctxt_UnpanJyutakuAdd3,
                null, null, null
                , false, false, false, false); //車輌からの業者は区分見ない？
        }

        #endregion

        #region 処分受託者

        /// <summary>
        /// 処分受託者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void cantxt_SyobunJyutakuNameCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 処分受託者 Validatedイベント
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
                    //処分の受託   業者削除
                    this.cantxt_SyobunJyuCd.Text = string.Empty;
                    this.ctxt_SyobunJyuName.Text = string.Empty;

                    //処分の受託   運転者削除
                    this.cantxt_SyobunJyuUntenCd.Text = string.Empty;
                    this.cantxt_SyobunJyuUntenName.Text = string.Empty;
                    // 20140604 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す(処分も) end
                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 処分受託者 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupBefExecuteMethod()
        {
            this.bak_SyobunJyutakuNameCd = this.cantxt_SyobunJyutakuNameCd.Text;
        }

        /// <summary>
        /// 処分受託者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod()
        {
            // 20140612 ria No.679 伝種区分連携 start
            //業者　設定
            //ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
            //    cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
            //    "Part1", cantxt_SyobunJyuCd, ctxt_SyobunJyuName,
            //    false, true, false, false);
            ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
                cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
                "All", cantxt_SyobunJyuCd, ctxt_SyobunJyuName,
                false, true, false, false, this.logic.isNotNeedDeleteFlg);
            // 20140612 ria No.679 伝種区分連携 end

            //運搬先の事業場（1）
            if (crdo_JigyoubaSyobun.Checked)
            {
                this.cantxt_UnpanJyugyobaGyoCD.Text = this.cantxt_SyobunJyutakuNameCd.Text;

                if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text)
                {
                    this.logic.UnpanJyugyobaDel("cantxt_UnpanJyugyobaNameCd");
                }
            }

            //運搬先の事業場（2）
            if (crdo_Jigyouba2Syobun.Checked)
            {
                this.cantxt_UnpanJyugyobaGyoCD2.Text = this.cantxt_SyobunJyutakuNameCd.Text;

                if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text)
                {
                    this.logic.UnpanJyugyoba2Del("cantxt_UnpanJyugyobaNameCd2");
                }
            }

            //運搬先の事業場（3）
            if (crdo_Jigyouba3Syobun.Checked)
            {
                this.cantxt_UnpanJyugyobaGyoCD3.Text = this.cantxt_SyobunJyutakuNameCd.Text;

                if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text)
                {
                    this.logic.UnpanJyugyoba3Del("cantxt_UnpanJyugyobaNameCd3");
                }
            }
        }

        #endregion

        #region 運搬先の事業場(区間１)

        /// <summary>
        /// 運搬先の事業場(区間１) 処分施設･積替保管 TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_UnpanJigyoubaNm_TextChanged(object sender, EventArgs e)
        {
            this.logic.SetTsumihoRadio(cntxt_UnpanJigyoubaNm, crdo_JigyoubaSyobun, crdo_JigyoubaHokan, "1",false);
        }

        /// <summary>
        /// 運搬先の事業場(区間１) 処分施設･積替保管 フォーカスリーブイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_UnpanJigyoubaNm_Leave(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 運搬先の事業場(区間１) 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaGyoCD_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyugyobaGyoCD_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間１) 業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaGyoCD_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }
            else
            {
                //区間１：運搬先の事業場の削除
                this.logic.UnpanJyugyobaDel("cantxt_UnpanJyugyobaNameCd");
            }

            //処分施設
            if (this.crdo_JigyoubaSyobun.Checked)
            {
            }
            //積替保管
            else if (this.crdo_JigyoubaHokan.Checked)
            {
                switch (this.logic.ChkGyosya(cantxt_UnpanJyugyobaGyoCD, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        //区間１：運搬先の事業場の削除
                        this.logic.UnpanJyugyobaDel("cantxt_UnpanJyugyobaGyoCD");
                        return;

                    case 2://エラー
                        this.logic.isInputError = true;
                        return;
                }
            }
            cantxt_UnpanJyugyobaGyoCD_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間１) 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyugyobaGyoCD_PopupAfterExecuteMethod()
        {
            if (this.cantxt_UnpanJyugyobaGyoCD.Text != this.preGyoushaCd)
            {
                this.logic.UnpanJyugyobaDel("cantxt_UnpanJyugyobaNameCd");
            }
        }

        public void cantxt_UnpanJyugyobaGyoCD_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCd = this.cantxt_UnpanJyugyobaGyoCD.Text;
        }

        /// <summary>
        /// 運搬先の事業場(区間１) 事業場 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間１) 事業場 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            //処分施設
            if (this.crdo_JigyoubaSyobun.Checked)
            {
                //20150630 #5005 hoanghm start
                //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
                if (!string.IsNullOrEmpty(cantxt_UnpanJyugyobaNameCd.Text) && string.IsNullOrEmpty(cantxt_SyobunJyutakuNameCd.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E051", "処分受託者");
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
                        //区間１：運搬先の事業場の削除
                        this.logic.UnpanJyugyobaDel("cantxt_UnpanJyugyobaNameCd");
                        return;

                    case 2://エラー
                        this.logic.isInputError = true;
                        return;
                }
            }
            //積替保管
            else if (this.crdo_JigyoubaHokan.Checked)
            {
                //20150630 #5005 hoanghm start
                //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
                if (!string.IsNullOrEmpty(cantxt_UnpanJyugyobaNameCd.Text) && string.IsNullOrEmpty(cantxt_UnpanJyugyobaGyoCD.Text))
                {
                    // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                    this.messageShowLogic.MessageBoxShow("E051", "運搬先の事業者");
                    // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                    this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                    this.cantxt_UnpanJyugyobaNameCd.Focus();
                    this.logic.isInputError = true;
                    return;
                }
                //20150630 #5005 hoanghm end
                switch (this.logic.ChkJigyouba(cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaGyoCD, "TSUMIKAEHOKAN_KBN"))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        //区間１：運搬先の事業場の削除
                        this.logic.UnpanJyugyobaDel("cantxt_UnpanJyugyobaNameCd");
                        return;

                    case 2://エラー
                        this.logic.isInputError = true;
                        return;
                }
            }
            cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間１) 事業場 PopupAfterExecuteMethod
        /// </summary>
        // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
        //public void cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod()
        public void cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod(ICustomControl sender = null)
        // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end
        {
            //処分施設
            if (this.crdo_JigyoubaSyobun.Checked)
            {
                // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
                ////業者　設定
                //ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
                //    cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
                //    // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない start
                //    //null, null, null
                //    "All", cantxt_SyobunJyuCd, ctxt_SyobunJyuName
                //    // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない end
                //    , false, true, false, false);

                ////現場　設定
                //this.cantxt_UnpanJyugyobaGyoCD.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                //this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
                //    cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null
                //    , false, false, true, false);
                if (sender != null)
                {
                    if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text)
                    {
                        //業者　設定
                        ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
                            cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
                            "All", cantxt_SyobunJyuCd, ctxt_SyobunJyuName
                            , false, true, false, false, this.logic.isNotNeedDeleteFlg);
                    }
                    if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text || this.bak_UnpanJyugyobaNameCd != this.cantxt_UnpanJyugyobaNameCd.Text)
                    {
                        //現場　設定
                        this.cantxt_UnpanJyugyobaGyoCD.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                        // 運搬先の事業場　設定(処分受託者で検索）
                        this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
                            cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null
                            , false, false, true, false, this.logic.isNotNeedDeleteFlg);
                        // 運搬先の事業場　設定(最終処分場で検索）
                        this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
                            cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null
                            , false, true, false, false, this.logic.isNotNeedDeleteFlg);
                    }
                }
                else
                {
                    //現場　設定
                    this.cantxt_UnpanJyugyobaGyoCD.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                    // 運搬先の事業場　設定(処分受託者で検索）
                    this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
                        cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null
                        , false, false, true, false, this.logic.isNotNeedDeleteFlg);
                    // 運搬先の事業場　設定(最終処分場で検索）
                    this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
                        cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null
                        , false, true, false, false, this.logic.isNotNeedDeleteFlg);
                }
                // 20140620 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end
            }
            //積替保管
            else if (this.crdo_JigyoubaHokan.Checked)
            {
                //現場　設定
                this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD, cantxt_UnpanJyugyobaNameCd, cantxt_UnpanJyugyobaName,
                    cnt_UnpanJyugyobaZip, cnt_UnpanJyugyobaTel, ctxt_UnpanJyugyobaAdd, null,
                    false, false, false, true, this.logic.isNotNeedDeleteFlg);
            }
        }

        // 20140619 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
        /// <summary>
        /// 運搬先の事業場 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyugyobaNameCd_PopupBeforeExecuteMethod(ICustomControl sender = null)
        {
            this.bak_SyobunJyutakuNameCd = this.cantxt_SyobunJyutakuNameCd.Text;

            this.bak_UnpanJyugyobaNameCd = (sender as CustomAlphaNumTextBox).Text;
        }

        // 20140619 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end

        #endregion

        #region 運搬先の事業場(区間２)

        /// <summary>
        /// 運搬先の事業場(区間２) 処分施設･積替保管 TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_UnpanJigyoubaNm2_TextChanged(object sender, EventArgs e)
        {
            this.logic.SetTsumihoRadio(cntxt_UnpanJigyoubaNm2, crdo_Jigyouba2Syobun, crdo_Jigyouba2Hokan, "2",false);
        }

        /// <summary>
        /// 運搬先の事業場(区間２) 処分施設･積替保管 フォーカスリーブイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_UnpanJigyoubaNm2_Leave(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 運搬先の事業場(区間２) 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaGyoCD2_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyugyobaGyoCD2_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間２) 業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaGyoCD2_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }
            else
            {
                //区間２：運搬先の事業場の削除
                this.logic.UnpanJyugyoba2Del("cantxt_UnpanJyugyobaNameCd2");
            }
            //処分施設
            if (this.crdo_Jigyouba2Syobun.Checked)
            {
            }
            //積替保管
            else if (this.crdo_Jigyouba2Hokan.Checked)
            {
                switch (this.logic.ChkGyosya(cantxt_UnpanJyugyobaGyoCD2, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        //区間２：運搬先の事業場の削除
                        this.logic.UnpanJyugyoba2Del("cantxt_UnpanJyugyobaGyoCD2");
                        return;

                    case 2://エラー
                        this.logic.isInputError = true;
                        return;
                }
            }
            cantxt_UnpanJyugyobaGyoCD2_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間２) 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyugyobaGyoCD2_PopupAfterExecuteMethod()
        {
            if (this.cantxt_UnpanJyugyobaGyoCD2.Text != this.preGyoushaCd)
            {
                this.logic.UnpanJyugyoba2Del("cantxt_UnpanJyugyobaNameCd2");
            }
        }

        /// <summary>
        /// 運搬先の事業場(区間２) 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyugyobaGyoCD2_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCd = this.cantxt_UnpanJyugyobaGyoCD2.Text;
        }

        /// <summary>
        /// 運搬先の事業場(区間２) 事業場 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd2_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyugyobaNameCd2_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間２) 事業場 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd2_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            //処分施設
            if (this.crdo_Jigyouba2Syobun.Checked)
            {
                //20150630 #5005 hoanghm start
                //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
                if (!string.IsNullOrEmpty(cantxt_UnpanJyugyobaNameCd2.Text) && string.IsNullOrEmpty(cantxt_SyobunJyutakuNameCd.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E051", "処分受託者");
                    this.cantxt_UnpanJyugyobaNameCd2.Text = string.Empty;
                    this.cantxt_UnpanJyugyobaNameCd2.Focus();
                    this.logic.isInputError = true;
                    return;
                }
                //20150630 #5005 hoanghm end
                // 処分事業場もしくは最終処分場の現場かチェック
                string[] colNames = { "SHOBUN_NIOROSHI_GENBA_KBN", "SAISHUU_SHOBUNJOU_KBN" };
                switch (this.logic.ChkJigyouba(cantxt_UnpanJyugyobaNameCd2, cantxt_SyobunJyutakuNameCd, colNames))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        //区間２：運搬先の事業場の削除
                        this.logic.UnpanJyugyoba2Del("cantxt_UnpanJyugyobaNameCd2");
                        return;

                    case 2://エラー
                        this.logic.isInputError = true;
                        return;
                }
            }
            //積替保管
            else if (this.crdo_Jigyouba2Hokan.Checked)
            {
                //20150630 #5005 hoanghm start
                //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
                if (!string.IsNullOrEmpty(cantxt_UnpanJyugyobaNameCd2.Text) && string.IsNullOrEmpty(cantxt_UnpanJyugyobaGyoCD2.Text))
                {
                    // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                    this.messageShowLogic.MessageBoxShow("E051", "運搬先の事業者");
                    // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                    this.cantxt_UnpanJyugyobaNameCd2.Text = string.Empty;
                    this.cantxt_UnpanJyugyobaNameCd2.Focus();
                    this.logic.isInputError = true;
                    return;
                }
                //20150630 #5005 hoanghm end
                switch (this.logic.ChkJigyouba(cantxt_UnpanJyugyobaNameCd2, cantxt_UnpanJyugyobaGyoCD2, "TSUMIKAEHOKAN_KBN"))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        //区間２：運搬先の事業場の削除
                        this.logic.UnpanJyugyoba2Del("cantxt_UnpanJyugyobaNameCd2");
                        return;

                    case 2://エラー
                        this.logic.isInputError = true;
                        return;
                }
            }
            cantxt_UnpanJyugyobaNameCd2_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間２) 事業場 PopupAfterExecuteMethod
        /// </summary>
        // 20140621 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
        //public void cantxt_UnpanJyugyobaNameCd2_PopupAfterExecuteMethod()
        public void cantxt_UnpanJyugyobaNameCd2_PopupAfterExecuteMethod(ICustomControl sender = null)
        // 20140621 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end
        {
            //処分施設
            if (this.crdo_Jigyouba2Syobun.Checked)
            {
                // 20140621 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
                ////業者　設定
                //ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
                //    cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
                //    // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない start
                //    //null, null, null
                //    "All", cantxt_SyobunJyuCd, ctxt_SyobunJyuName
                //    // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない end
                //    , false, true, false, false);

                ////現場　設定
                //this.cantxt_UnpanJyugyobaGyoCD2.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                //this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD2, cantxt_UnpanJyugyobaNameCd2, cantxt_UnpanJyugyobaName2, cnt_UnpanJyugyobaZip2, cntxt_UnpanJyugyobaTel2, ctxt_UnpanJyugyobaAdd2, null
                //    , false, false, true, false);
                if (sender != null)
                {
                    if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text)
                    {
                        //業者　設定
                        ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
                            cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
                            "All", cantxt_SyobunJyuCd, ctxt_SyobunJyuName
                            , false, true, false, false);
                    }
                    if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text || this.bak_UnpanJyugyobaNameCd != this.cantxt_UnpanJyugyobaNameCd2.Text)
                    {
                        //現場　設定
                        this.cantxt_UnpanJyugyobaGyoCD2.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                        //運搬先の事業場　設定(処分受託者で検索）
                        this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD2, cantxt_UnpanJyugyobaNameCd2, cantxt_UnpanJyugyobaName2, cnt_UnpanJyugyobaZip2, cntxt_UnpanJyugyobaTel2, ctxt_UnpanJyugyobaAdd2, null
                            , false, false, true, false);
                        //運搬先の事業場　設定(最終処分場で検索）
                        this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD2, cantxt_UnpanJyugyobaNameCd2, cantxt_UnpanJyugyobaName2, cnt_UnpanJyugyobaZip2, cntxt_UnpanJyugyobaTel2, ctxt_UnpanJyugyobaAdd2, null
                            , false, true, false, false);
                    }
                }
                else
                {
                    //現場　設定
                    this.cantxt_UnpanJyugyobaGyoCD2.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                    //運搬先の事業場　設定(処分受託者で検索）
                    this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD2, cantxt_UnpanJyugyobaNameCd2, cantxt_UnpanJyugyobaName2, cnt_UnpanJyugyobaZip2, cntxt_UnpanJyugyobaTel2, ctxt_UnpanJyugyobaAdd2, null
                        , false, false, true, false);
                    //運搬先の事業場　設定(最終処分場で検索）
                    this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD2, cantxt_UnpanJyugyobaNameCd2, cantxt_UnpanJyugyobaName2, cnt_UnpanJyugyobaZip2, cntxt_UnpanJyugyobaTel2, ctxt_UnpanJyugyobaAdd2, null
                        , false, true, false, false);
                }
                // 20140621 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end
            }
            //積替保管
            else if (this.crdo_Jigyouba2Hokan.Checked)
            {
                //現場　設定
                this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD2, cantxt_UnpanJyugyobaNameCd2, cantxt_UnpanJyugyobaName2, cnt_UnpanJyugyobaZip2, cntxt_UnpanJyugyobaTel2, ctxt_UnpanJyugyobaAdd2, null
                    , false, false, false, true);
            }
        }

        #endregion

        #region 運搬先の事業場(区間３)

        /// <summary>
        /// 運搬先の事業場(区間３) 処分施設･積替保管 TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_UnpanJigyoubaNm3_TextChanged(object sender, EventArgs e)
        {
            this.logic.SetTsumihoRadio(cntxt_UnpanJigyoubaNm3, crdo_Jigyouba3Syobun, crdo_Jigyouba3Syobun, "3",false);
        }

        /// <summary>
        /// 運搬先の事業場(区間３) 処分施設･積替保管 フォーカスリーブイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_UnpanJigyoubaNm3_Leave(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 運搬先の事業場(区間３) 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaGyoCD3_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyugyobaGyoCD3_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間３) 業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaGyoCD3_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }
            else
            {
                //区間３：運搬先の事業場の削除
                this.logic.UnpanJyugyoba3Del("cantxt_UnpanJyugyobaNameCd3");
            }
            cantxt_UnpanJyugyobaGyoCD3_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間３) 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyugyobaGyoCD3_PopupAfterExecuteMethod()
        {
            if (this.cantxt_UnpanJyugyobaGyoCD3.Text != this.preGyoushaCd)
            {
                this.logic.UnpanJyugyoba3Del("cantxt_UnpanJyugyobaNameCd3");
            }
        }

        /// <summary>
        /// 運搬先の事業場(区間３) 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyugyobaGyoCD3_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCd = this.cantxt_UnpanJyugyobaGyoCD3.Text;
        }

        /// <summary>
        /// 運搬先の事業場(区間３) 事業場 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd3_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyugyobaNameCd3_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間３) 事業場 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd3_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            //処分施設
            if (this.crdo_Jigyouba3Syobun.Checked)
            {
                //20150630 #5005 hoanghm start
                //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
                if (!string.IsNullOrEmpty(cantxt_UnpanJyugyobaNameCd3.Text) && string.IsNullOrEmpty(cantxt_SyobunJyutakuNameCd.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E051", "処分受託者");
                    this.cantxt_UnpanJyugyobaNameCd3.Text = string.Empty;
                    this.cantxt_UnpanJyugyobaNameCd3.Focus();
                    this.logic.isInputError = true;
                    return;
                }
                //20150630 #5005 hoanghm end
                // 処分事業場もしくは最終処分場の現場かチェック
                string[] colNames = { "SHOBUN_NIOROSHI_GENBA_KBN", "SAISHUU_SHOBUNJOU_KBN" };
                switch (this.logic.ChkJigyouba(cantxt_UnpanJyugyobaNameCd3, cantxt_SyobunJyutakuNameCd, colNames))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        //区間３：運搬先の事業場の削除
                        this.logic.UnpanJyugyoba3Del("cantxt_UnpanJyugyobaNameCd3");
                        return;

                    case 2://エラー
                        this.logic.isInputError = true;
                        return;
                }
            }
            cantxt_UnpanJyugyobaNameCd3_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬先の事業場(区間３) 事業場 PopupAfterExecuteMethod
        /// </summary>
        // 20140621 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
        //public void cantxt_UnpanJyugyobaNameCd3_PopupAfterExecuteMethod()
        public void cantxt_UnpanJyugyobaNameCd3_PopupAfterExecuteMethod(ICustomControl sender = null)
        // 20140621 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう end
        {
            //処分施設
            if (this.crdo_Jigyouba3Syobun.Checked)
            {
                // 20140621 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
                ////業者　設定
                //ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
                //    cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
                //    // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない start
                //    //null, null, null
                //    "All", cantxt_SyobunJyuCd, ctxt_SyobunJyuName
                //    // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない end
                //     , false, true, false, false);

                ////現場　設定
                //this.cantxt_UnpanJyugyobaGyoCD3.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                //this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD3, cantxt_UnpanJyugyobaNameCd3, cantxt_UnpanJyugyobaName3, cnt_UnpanJyugyobaZip3, cntxt_UnpanJyugyobaTel3, ctxt_UnpanJyugyobaAdd3, null
                //    , false, false, true, false);
                if (sender != null)
                {
                    if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text)
                    {
                        //業者　設定
                        ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyutakuNameCd, cantxt_SyobunJyutakuName,
                            cnt_SyobunJyutakuZip, cnt_SyobunJyutakuTel, ctxt_SyobunJyutakuAdd,
                            "All", cantxt_SyobunJyuCd, ctxt_SyobunJyuName
                            , false, true, false, false);
                    }
                    if (this.bak_SyobunJyutakuNameCd != this.cantxt_SyobunJyutakuNameCd.Text || this.bak_UnpanJyugyobaNameCd != this.cantxt_UnpanJyugyobaNameCd3.Text)
                    {
                        //現場　設定
                        this.cantxt_UnpanJyugyobaGyoCD3.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                        //運搬先の事業場　設定(処分受託者で検索）
                        this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD3, cantxt_UnpanJyugyobaNameCd3, cantxt_UnpanJyugyobaName3, cnt_UnpanJyugyobaZip3, cntxt_UnpanJyugyobaTel3, ctxt_UnpanJyugyobaAdd3, null
                            , false, false, true, false);
                        //運搬先の事業場　設定(最終処分場で検索）
                        this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD3, cantxt_UnpanJyugyobaNameCd3, cantxt_UnpanJyugyobaName3, cnt_UnpanJyugyobaZip3, cntxt_UnpanJyugyobaTel3, ctxt_UnpanJyugyobaAdd3, null
                            , false, true, false, false);
                    }
                }
                else
                {
                    //現場　設定
                    this.cantxt_UnpanJyugyobaGyoCD3.Text = this.cantxt_SyobunJyutakuNameCd.Text;
                    //運搬先の事業場　設定(処分受託者で検索）
                    this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD3, cantxt_UnpanJyugyobaNameCd3, cantxt_UnpanJyugyobaName3, cnt_UnpanJyugyobaZip3, cntxt_UnpanJyugyobaTel3, ctxt_UnpanJyugyobaAdd3, null
                        , false, false, true, false);
                    //運搬先の事業場　設定(最終処分場で検索）
                    this.mlogic.SetAddressJigyouba("All", cantxt_UnpanJyugyobaGyoCD3, cantxt_UnpanJyugyobaNameCd3, cantxt_UnpanJyugyobaName3, cnt_UnpanJyugyobaZip3, cntxt_UnpanJyugyobaTel3, ctxt_UnpanJyugyobaAdd3, null
                        , false, true, false, false);
                }
                // 20140621 kayo 不具合No.4897 現場を変更すると、業者がリセットされてしまう start
            }
        }

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
                    //運搬先の事業場（処分業者の処理施設）削除
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

            ManifestoLogic.SetAddrGyousha("All", cantxt_TumiGyoCd, ctxt_TumiGyoName,
                null, null, null,
                null, null, null
                , false, false, false, true, this.logic.isNotNeedDeleteFlg);
        }

        /// <summary>
        /// 積替え又は保管 事業場 Validatedイベント
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
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
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
                    //運搬先の事業場（処分業者の処理施設）削除
                    this.logic.TumiHokaDel("cantxt_TumiHokaNameCd");

                    return;

                case 2://エラー
                    this.logic.isInputError = true;
                    return;
            }
            cantxt_TumiHokaNameCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 積替え又は保管 事業場 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_TumiHokaNameCd_PopupAfterExecuteMethod()
        {
            //業者　設定
            ManifestoLogic.SetAddrGyousha("All", cantxt_TumiGyoCd, ctxt_TumiGyoName,
                null, null, null,
                null, null, null
                , false, false, false, true, this.logic.isNotNeedDeleteFlg);

            //現場　設定
            this.mlogic.SetAddressJigyouba("All", cantxt_TumiGyoCd, cantxt_TumiHokaNameCd, ctxt_TumiHokaName,
                cnt_TumiHokaZip, cnt_TumiHokaTel, ctxt_TumiHokaAdd, null
                , false, false, false, true, this.logic.isNotNeedDeleteFlg);
        }

        #endregion

        #region 運搬の受託(区間１)

        /// <summary>
        /// 運搬の受託(区間１) 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuCd1_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyuCd1_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間１) 業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuCd1_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            // 20140617 ria EV004825 「運搬の受託」、「処分の受託」 start
            //switch (this.logic.ChkGyosya(cantxt_UnpanJyuCd1, "UNPAN_JUTAKUSHA_KBN"))
            //{
            //    case 0://正常
            //        break;

            //    case 1://空
            //        this.cantxt_UnpanJyuCd1.Text = string.Empty;
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
                        this.cantxt_UnpanJyuCd1.Text = string.Empty;
                        this.ctxt_UnpanJyuName1.Text = string.Empty;
                        return;

                    case 2://エラー
                        this.ctxt_UnpanJyuName1.Text = string.Empty;
                        this.logic.isInputError = true;
                        return;
                }
            }
            // 20140617 ria EV004825 「運搬の受託」、「処分の受託」 end
            cantxt_UnpanJyuCd1_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間１) 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyuCd1_PopupAfterExecuteMethod()
        {
            // 20140605 ria No.679 伝種区分連携 start
            //ManifestoLogic.SetAddrGyousha("Part1", cantxt_UnpanJyuCd1, ctxt_UnpanJyuName1,
            //    null, null, null,
            //    null, null, null
            //    , false, false, true, false);
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyuCd1, ctxt_UnpanJyuName1,
                null, null, null,
                null, null, null
                , false, false, true, false, this.logic.isNotNeedDeleteFlg);
            // 20140605 ria No.679 伝種区分連携 end
        }

        /// <summary>
        /// 運搬の受託(区間１) 運転者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuUntenCd1_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_UnpanJyuUntenCd1_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間１) 運転者 Validatedイベント
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
        /// 運搬の受託(区間１) 運転者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyuUntenCd1_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 運搬の受託(区間１) 有価物拾得量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_YSuu_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 運搬の受託(区間１) 有価物拾得量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_YSuu_Validated(object sender, EventArgs e)
        {
            //if (!isChanged(sender))//変更がない場合は何もしない
            //{
            //    return;
            //}
            //this.logic.SetYukaJyuuryo(this.cntxt_YSuu);
        }

        /// <summary>
        /// 運搬の受託(区間１) 有価物拾得量 単位 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_YTani_Validating(object sender, CancelEventArgs e)
        {
            this.cntxt_YTani_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間１) 有価物拾得量 単位 PopupAfterExecuteMethod
        /// </summary>
        public void cntxt_YTani_PopupAfterExecuteMethod()
        {
        }

        #endregion

        #region 運搬の受託(区間２)

        /// <summary>
        /// 運搬の受託(区間２) 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuCd2_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyuCd2_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間２) 業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuCd2_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            // 20140617 ria EV004825 「運搬の受託」、「処分の受託」 start
            //switch (this.logic.ChkGyosya(cantxt_UnpanJyuCd2, "UNPAN_JUTAKUSHA_KBN"))
            //{
            //    case 0://正常
            //        break;

            //    case 1://空
            //        this.cantxt_UnpanJyuCd2.Text = string.Empty;
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
                        this.cantxt_UnpanJyuCd2.Text = string.Empty;
                        this.ctxt_UnpanJyuName2.Text = string.Empty;
                        return;

                    case 2://エラー
                        this.ctxt_UnpanJyuName2.Text = string.Empty;
                        this.logic.isInputError = true;
                        return;
                }
            }
            // 20140617 ria EV004825 「運搬の受託」、「処分の受託」 end
            cantxt_UnpanJyuCd2_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間２) 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyuCd2_PopupAfterExecuteMethod()
        {
            // 20140613 ria EV004777 運搬の受託、処分の受託への業者名のセット start
            //ManifestoLogic.SetAddrGyousha("Part1", cantxt_UnpanJyuCd2, ctxt_UnpanJyuName2,
            //    null, null, null,
            //    null, null, null
            //    , false, false, true, false);
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyuCd2, ctxt_UnpanJyuName2,
                null, null, null,
                null, null, null
                , false, false, true, false);
            // 20140613 ria EV004777 運搬の受託、処分の受託への業者名のセット end
        }

        /// <summary>
        /// 運搬の受託(区間２) 運転者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuUntenCd2_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_UnpanJyuUntenCd2_Validating();
        }

        /// <summary>
        /// 運搬の受託(区間２) 運転者 Validatedイベント
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
        /// 運搬の受託(区間２) 運転者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyuUntenCd2_Validating()
        {
        }

        /// <summary>
        /// 運搬の受託(区間２) 有価物拾得量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_YSuu2_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 運搬の受託(区間２) 有価物拾得量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_YSuu2_Validated(object sender, EventArgs e)
        {
            //if (!isChanged(sender))//変更がない場合は何もしない
            //{
            //    return;
            //}
            //this.logic.SetYukaJyuuryo(this.cntxt_YSuu2);
        }

        /// <summary>
        /// 運搬の受託(区間２) 有価物拾得量 単位 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_YTani2_Validating(object sender, CancelEventArgs e)
        {
            this.cntxt_YTani2_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間２) 有価物拾得量 単位 PopupAfterExecuteMethod
        /// </summary>
        public void cntxt_YTani2_PopupAfterExecuteMethod()
        {
        }

        #endregion

        #region 運搬の受託(区間３)

        /// <summary>
        /// 運搬の受託(区間３) 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuCd3_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_UnpanJyuCd3_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間３) 業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuCd3_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            // 20140617 ria EV004825 「運搬の受託」、「処分の受託」 start
            //switch (this.logic.ChkGyosya(cantxt_UnpanJyuCd3, "UNPAN_JUTAKUSHA_KBN"))
            //{
            //    case 0://正常
            //        break;

            //    case 1://空
            //        this.cantxt_UnpanJyuCd3.Text = string.Empty;
            //        this.ctxt_UnpanJyuName3.Text = string.Empty;
            //        return;

            //    case 2://エラー
            //        this.ctxt_UnpanJyuName3.Text = string.Empty;
            //        return;
            //}
            if (!string.IsNullOrEmpty(this.cantxt_UnpanJyutaku3NameCd.Text))
            {
                if (this.cantxt_UnpanJyuCd3.Text != this.cantxt_UnpanJyutaku3NameCd.Text && !string.IsNullOrEmpty(this.cantxt_UnpanJyuCd3.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    this.ctxt_UnpanJyuName3.Text = string.Empty;
                    this.cantxt_UnpanJyuCd3.Focus();
                    this.logic.isInputError = true;
                    return;
                }
                else if (string.IsNullOrEmpty(this.cantxt_UnpanJyuCd3.Text))
                {
                    this.ctxt_UnpanJyuName3.Text = string.Empty;
                    return;
                }
            }
            else
            {
                switch (this.logic.ChkGyosya(cantxt_UnpanJyuCd3, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
                {
                    case 0://正常
                        this.logic.isInputError = false;
                        break;

                    case 1://空
                        this.cantxt_UnpanJyuCd3.Text = string.Empty;
                        this.ctxt_UnpanJyuName3.Text = string.Empty;
                        return;

                    case 2://エラー
                        this.ctxt_UnpanJyuName3.Text = string.Empty;
                        this.logic.isInputError = true;
                        return;
                }
            }
            // 20140617 ria EV004825 「運搬の受託」、「処分の受託」 end

            cantxt_UnpanJyuCd3_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間３) 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyuCd3_PopupAfterExecuteMethod()
        {
            // 20140613 ria EV004777 運搬の受託、処分の受託への業者名のセット start
            //ManifestoLogic.SetAddrGyousha("Part1", cantxt_UnpanJyuCd3, ctxt_UnpanJyuName3,
            //    null, null, null,
            //    null, null, null
            //    , false, false, true, false);
            ManifestoLogic.SetAddrGyousha("All", cantxt_UnpanJyuCd3, ctxt_UnpanJyuName3,
                null, null, null,
                null, null, null
                , false, false, true, false);
            // 20140613 ria EV004777 運搬の受託、処分の受託への業者名のセット end
        }

        /// <summary>
        /// 運搬の受託(区間３) 運転者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuUntenCd3_Validating(object sender, CancelEventArgs e)
        {
            this.cantxt_UnpanJyuUntenCd3_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間３) 運転者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyuUntenCd3_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            this.logic.ChkUntensha(cantxt_UnpanJyuUntenCd3);
        }

        /// <summary>
        /// 運搬の受託(区間３) 運転者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_UnpanJyuUntenCd3_PopupAfterExecuteMethod()
        {
        }

        /// <summary>
        /// 運搬の受託(区間３) 有価物拾得量 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_YSuu3_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 運搬の受託(区間３) 有価物拾得量 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_YSuu3_Validated(object sender, EventArgs e)
        {
            //if (!isChanged(sender))//変更がない場合は何もしない
            //{
            //    return;
            //}
            //this.logic.SetYukaJyuuryo(this.cntxt_YSuu3);
        }

        /// <summary>
        /// 運搬の受託(区間３) 有価物拾得量 単位 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_YTani3_Validating(object sender, CancelEventArgs e)
        {
            this.cntxt_YTani3_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 運搬の受託(区間３) 有価物拾得量 単位 PopupAfterExecuteMethod
        /// </summary>
        public void cntxt_YTani3_PopupAfterExecuteMethod()
        {
        }

        // 20140602 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す start
        /// <summary>
        /// 運搬受託者（区間１）を消し
        /// </summary>
        public void cantxt_UnpanJyutaku1Clear()
        {
            //運搬受託者（区間１）削除
            this.cantxt_UnpanJyutaku1NameCd.Text = string.Empty;
            this.cantxt_UnpanJyutaku1Name.Text = string.Empty;
            this.cnt_UnpanJyutaku1Zip.Text = string.Empty;
            this.cnt_UnpanJyutaku1Tel.Text = string.Empty;
            this.ctxt_UnpanJyutakuAdd.Text = string.Empty;
            this.cantxt_Jyutaku1Syasyu.Text = string.Empty;
            this.ctxt_Jyutaku1Syasyu.Text = string.Empty;
            this.cantxt_Jyutaku1SyaNo.Text = string.Empty;
            this.ctxt_Jyutaku1SyaNo.Text = string.Empty;
            this.cantxt_UnpanJyutakuHouhouCD.Text = string.Empty;
            this.ctxt_UnpanJyutakuHouhouMei.Text = string.Empty;

            //運搬の受託（区間1）事業者削除
            this.cantxt_UnpanJyuCd1.Text = string.Empty;
            this.ctxt_UnpanJyuName1.Text = string.Empty;

            //運搬の受託（区間1）運転者削除
            this.cantxt_UnpanJyuUntenCd1.Text = string.Empty;
            this.cantxt_UnpanJyuUntenName1.Text = string.Empty;
        }

        /// <summary>
        /// 運搬受託者（区間２）を消し
        /// </summary>
        public void cantxt_UnpanJyutaku2Clear()
        {
            //運搬受託者（区間２）削除
            this.cantxt_UnpanJyutaku2NameCd.Text = string.Empty;
            this.cantxt_UnpanJyutaku2Name.Text = string.Empty;
            this.cnt_UnpanJyutaku2Zip.Text = string.Empty;
            this.cnt_UnpanJyutaku2Tel.Text = string.Empty;
            this.ctxt_UnpanJyutakuAdd2.Text = string.Empty;
            this.cantxt_Jyutaku2Syasyu.Text = string.Empty;
            this.ctxt_Jyutaku2Syasyu.Text = string.Empty;
            this.cantxt_Jyutaku2SyaNo.Text = string.Empty;
            this.ctxt_Jyutaku2SyaNo.Text = string.Empty;
            this.cantxt_UnpanJyutaku2HouhouCD.Text = string.Empty;
            this.ctxt_UnpanJyutaku2HouhouMei.Text = string.Empty;

            //運搬の受託（区間2）事業者削除
            this.cantxt_UnpanJyuCd2.Text = string.Empty;
            this.ctxt_UnpanJyuName2.Text = string.Empty;

            //運搬の受託（区間2）運転者削除
            this.cantxt_UnpanJyuUntenCd2.Text = string.Empty;
            this.cantxt_UnpanJyuUntenName2.Text = string.Empty;
        }

        /// <summary>
        /// 運搬受託者（区間３）を消し
        /// </summary>
        public void cantxt_UnpanJyutaku3Clear()
        {
            //運搬受託者（区間３）削除
            this.cantxt_UnpanJyutaku3NameCd.Text = string.Empty;
            this.cantxt_UnpanJyutaku3Name.Text = string.Empty;
            this.cnt_UnpanJyutaku3Zip.Text = string.Empty;
            this.cnt_UnpanJyutaku3Tel.Text = string.Empty;
            this.ctxt_UnpanJyutakuAdd3.Text = string.Empty;
            this.cantxt_Jyutaku3Syasyu.Text = string.Empty;
            this.ctxt_Jyutaku3Syasyu.Text = string.Empty;
            this.cantxt_Jyutaku3SyaNo.Text = string.Empty;
            this.ctxt_Jyutaku3SyaNo.Text = string.Empty;
            this.cantxt_UnpanJyutaku3HouhouCD.Text = string.Empty;
            this.ctxt_UnpanJyutaku3HouhouMei.Text = string.Empty;

            //運搬の受託（区間3）事業者削除
            this.cantxt_UnpanJyuCd3.Text = string.Empty;
            this.ctxt_UnpanJyuName3.Text = string.Empty;

            //運搬の受託（区間3）運転者削除
            this.cantxt_UnpanJyuUntenCd3.Text = string.Empty;
            this.cantxt_UnpanJyuUntenName3.Text = string.Empty;
        }
        // 20140602 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す end

        #endregion

        #region 処分の受託

        /// <summary>
        /// 処分の受託 業者 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunJyuCd_Validating(object sender, CancelEventArgs e)
        {
            //this.cantxt_SyobunJyuCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 処分の受託 業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunJyuCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender) && !this.logic.isInputError)//変更がない場合は何もしない
            {
                return;
            }

            // 20140617 ria EV004825 「運搬の受託」、「処分の受託」 start
            //switch (this.logic.ChkGyosya(cantxt_SyobunJyuCd, "SHOBUN_JUTAKUSHA_KBN"))
            //{
            //    case 0://正常
            //        break;

            //    case 1://空
            //        this.cantxt_SyobunJyuCd.Text = string.Empty;
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
                        this.cantxt_SyobunJyuCd.Text = string.Empty;
                        this.ctxt_SyobunJyuName.Text = string.Empty;
                        return;

                    case 2://エラー
                        this.ctxt_SyobunJyuName.Text = string.Empty;
                        this.logic.isInputError = true;
                        return;
                }
            }
            // 20140617 ria EV004825 「運搬の受託」、「処分の受託」 end
            cantxt_SyobunJyuCd_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 処分の受託 業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyuCd_PopupAfterExecuteMethod()
        {
            ManifestoLogic.SetAddrGyousha("All", cantxt_SyobunJyuCd, ctxt_SyobunJyuName,
                null, null, null,
                null, null, null
                , false, true, false, false, this.logic.isNotNeedDeleteFlg);
        }

        #endregion

        #region 最終処分を行った場所

        /// <summary>
        /// 最終処分を行った場所 最終処分の業者CD Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuSyobunGyoCd_Validating(object sender, CancelEventArgs e)
        {
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

            //最終処分場CDのチェック
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

            return;
        }

        /// <summary>
        /// 最終処分を行った場所 最終処分の業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SaisyuSyobunGyoCd_PopupAfterExecuteMethod()
        {
            if (this.cantxt_SaisyuSyobunGyoCd.Text != this.preGyoushaCd)
            {
                this.logic.SaisyuBasyoSyozaiDel("cantxt_SaisyuSyobunbaCD");
            }
        }

        /// <summary>
        ///  最終処分を行った場所 最終処分の業者 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_SaisyuSyobunGyoCd_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCd = this.cantxt_SaisyuSyobunGyoCd.Text;
        }

        /// <summary>
        /// 最終処分を行った場所 名称 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SaisyuSyobunbaCD_Validating(object sender, CancelEventArgs e)
        {
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
        /// 最終処分を行った場所 名称  PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SaisyuSyobunbaCD_PopupAfterExecuteMethod()
        {
            this.mlogic.SetAddressJigyouba("All", cantxt_SaisyuSyobunGyoCd, cantxt_SaisyuSyobunbaCD, ctxt_SaisyuSyobunGyoName,
                cnt_SaisyuBasyoZip, cnt_SaisyuBasyoTel, ctxt_SaisyuBasyoSyozai, ctxt_SaisyuBasyoNo
                , false, true, false, false);
        }

        #endregion

        // 20140601 katen 不具合No.4133 start‏
        //指定コントロールのペイントを禁止
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

        private const int WM_SETREDRAW = 0xB;

        /// <summary>
        /// 「前」のボタン Clickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbtn_Previous_Click(object sender, EventArgs e)
        {
            bool catchErr = false;
            // 権限チェック
            bool containsUpdateAuthority = Manager.CheckAuthority("G120", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
            bool containsReferenceAuthority = Manager.CheckAuthority("G120", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);
            if (!containsUpdateAuthority && !containsReferenceAuthority)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E158", "修正");
                return;
            }

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

                //画面のペイントを禁止
                SendMessage(this.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
                if (this.logic.WindowInit("OnLoad", out catchErr))
                {
                    if (catchErr) { return; }
                    var checkAuthority = Manager.CheckAuthority("G120", WINDOW_TYPE.NEW_WINDOW_FLAG, false);
                    if (checkAuthority)
                    {
                        // 新規権限有かつ、エラー発生時には値をクリアする
                        base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.parameters.Save();
                    }

                    this.ClearScreen(e);
                    this.logic.WindowInit("SetAddFrom", out catchErr);
                    if (catchErr) { return; }

                    //画面のペイントを出来るようにする
                    SendMessage(this.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
                    //画面リフレッシュ
                    this.Refresh();

                    if (checkAuthority)
                    {
                        //フォーカス位置を設定
                        this.cdate_KohuDate.Focus();
                    }
                    else
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E158", "新規");

                        // 新規権限が無ければ閉じる
                        this.ParentForm.Close();
                    }
                    return;
                }
                else
                {
                    if (catchErr) { return; }
                }
                //画面のペイントを出来るようにする
                SendMessage(this.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
                //画面リフレッシュ
                this.Refresh();
                //フォーカス位置を設定
                this.cdate_KohuDate.Focus();
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
            bool catchErr = false;
            // 権限チェック
            bool containsUpdateAuthority = Manager.CheckAuthority("G120", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
            bool containsReferenceAuthority = Manager.CheckAuthority("G120", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);
            if (!containsUpdateAuthority && !containsReferenceAuthority)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E158", "修正");
                return;
            }

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

                this.SuspendLayout();
                //画面のペイントを禁止
                SendMessage(this.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
                if (this.logic.WindowInit("OnLoad", out catchErr))
                {
                    if (catchErr) { return; }
                    var checkAuthority = Manager.CheckAuthority("G120", WINDOW_TYPE.NEW_WINDOW_FLAG, false);
                    if (checkAuthority)
                    {
                        // 新規権限有かつ、エラー発生時には値をクリアする
                        base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.parameters.Save();
                    }

                    this.ClearScreen(e);
                    this.logic.WindowInit("SetAddFrom", out catchErr);
                    if (catchErr) { return; }

                    this.ResumeLayout(false);
                    this.PerformLayout();
                    //画面のペイントを出来るようにする
                    SendMessage(this.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
                    //画面リフレッシュ
                    this.Refresh();

                    if (checkAuthority)
                    {
                        //フォーカス位置を設定
                        this.cdate_KohuDate.Focus();
                    }
                    else
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E158", "新規");

                        // 新規権限が無ければ閉じる
                        this.ParentForm.Close();
                    }
                    return;
                }
                else
                {
                    if (catchErr) { return; }
                }
                this.ResumeLayout(false);
                this.PerformLayout();
                //画面のペイントを出来るようにする
                SendMessage(this.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
                //画面リフレッシュ
                this.Refresh();
                //フォーカス位置を設定
                this.cdate_KohuDate.Focus();
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

        /// <summary>
        /// 最終処分を行った場所名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_SaisyuSyobunGyoName_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 最終処分を行った場所住所
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_SaisyuBasyoSyozai_Validating(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// グリッドの数値列を返す
        /// </summary>
        /// <returns></returns>
        internal r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column[] GetNumericColumns()
        {
            return new[] { this.Suryo, this.KansangoSuryo, this.GenyoyugoTotalSuryo };
        }

        private void SampaiManifestoThumiKae_Shown(object sender, EventArgs e)
        {
            // データ移動初期表示処理
            this.logic.SetMoveData();
        }

        // 20140606 syunrei No.730 規定値機能の追加について start
        public void KyotenCd_Validated(string str, EventArgs e)
        {
            this.isHeadVF = true;
            bool catchErr = false;
            if (this.logic.SetKiteiValue(str))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (DialogResult.Yes == msgLogic.MessageBoxShow("C089", MessageBoxDefaultButton.Button2, "拠点"))
                {
                    // 権限チェック
                    if (Manager.CheckAuthority("G120", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        //base.OnLoad(e);

                        this.logic.WindowInit("bt_process2", out catchErr);
                        if (catchErr) { return; }
                    }
                }
            }
            this.isHeadVF = false;
        }

        // 20140606 syunrei No.730 規定値機能の追加について end

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
        /// 運搬受託者２の住所検索ボタンの使用可否をセットします
        /// </summary>
        /// <param name="enabled">使用可否</param>
        internal void SetUnpanJutakusha2AddressSearchEnabled(bool enabled)
        {
            this.UnpanJutakusha2AddressSearch.Enabled = enabled;
        }

        /// <summary>
        /// 運搬受託者３の住所検索ボタンの使用可否をセットします
        /// </summary>
        /// <param name="enabled">使用可否</param>
        internal void SetUnpanJutakusha3AddressSearchEnabled(bool enabled)
        {
            this.UnpanJutakusha3AddressSearch.Enabled = enabled;
        }

        /// <summary>
        /// 運搬先の事業場１の住所検索ボタンの使用可否をセットします
        /// </summary>
        /// <param name="enabled">使用可否</param>
        internal void SetUnpansaki1AddressSearchEnabled(bool enabled)
        {
            this.Unpansaki1AddressSearch.Enabled = enabled;
        }

        /// <summary>
        /// 運搬先の事業場２の住所検索ボタンの使用可否をセットします
        /// </summary>
        /// <param name="enabled">使用可否</param>
        internal void SetUnpansaki2AddressSearchEnabled(bool enabled)
        {
            this.Unpansaki2AddressSearch.Enabled = enabled;
        }

        /// <summary>
        /// 運搬先の事業場３の住所検索ボタンの使用可否をセットします
        /// </summary>
        /// <param name="enabled">使用可否</param>
        internal void SetUnpansaki3AddressSearchEnabled(bool enabled)
        {
            this.Unpansaki3AddressSearch.Enabled = enabled;
        }

        /// <summary>
        /// 積替え又は保管の住所検索ボタンの使用可否をセットします
        /// </summary>
        /// <param name="enabled">使用可否</param>
        internal void SetTsumikaeAddressSearchEnabled(bool enabled)
        {
            this.TsumikaeAddressSearch.Enabled = enabled;
        }

        /// <summary>
        /// 最終処分を行った場所の住所検索ボタンの使用可否をセットします
        /// </summary>
        /// <param name="enabled">使用可否</param>
        internal void SetLastShobunJissekiAddressSearchEnabled(bool enabled)
        {
            this.LastShobunJissekiAddressSearch.Enabled = enabled;
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
            if (!string.IsNullOrEmpty(Convert.ToString(this.cdate_UnpanJyu3.Value)))
            {
                dc.Compare(Convert.ToDateTime(this.cdate_UnpanJyu3.Value), "運搬区間3の運搬終了年月日");
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

        internal void cdate_UnpanJyu3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(this.cdate_UnpanJyu3.Value)) || string.IsNullOrEmpty(Convert.ToString(this.cdate_KohuDate.Value)))
            {
                return;
            }
            if (Convert.ToDateTime(this.cdate_UnpanJyu3.Value) < Convert.ToDateTime(this.cdate_KohuDate.Value))
            {
                this.messageShowLogic.MessageBoxShow("E281", "運搬終了年月日", "交付年月日");
                e.Cancel = true;
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

            bool valueChangeFlg = true;

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
                //fouced状態保存
                bool renkeiFlag = this.cantxt_DenshuKbn.Focused || this.cantxt_No.Focused || this.cantxt_Meisaigyou.Focused;

                // 入力チェックを行う
                if (this.logic.ChkRenkei(renkeiFlag))
                {
                    //連携データ検索
                    this.logic.SetRenkeiData(valueChangeFlg, renkeiFlag);
                }
            }
        }

        //20250402
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
        }

        private void cdate_UnpanDate_3_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.cdate_UnpanDate_3.Value == null || this.cdate_KohuDate.Value == null)
                {
                    return;
                }

                if ((DateTime)this.cdate_UnpanDate_3.Value < (DateTime)this.cdate_KohuDate.Value)
                {
                    this.errmessage.MessageBoxShowError("運搬終了日3が交付年月日より過去日になっています。運搬終了日3を見直してください");
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
        private void cdate_UnpanDate_3_Validated(object sender, EventArgs e)
        {
            if (this.cdate_UnpanDate_3.Value == null)
            {
                this.cdate_UnpanJyu3.Value = null;
            }
            else
            {
                this.cdate_UnpanJyu3.Value = this.cdate_UnpanDate_3.Value;
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