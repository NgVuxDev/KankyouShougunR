// $Id: KobetsuHinmeiTankaHoshuForm.cs 52370 2015-06-16 02:14:23Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KobetsuHinmeiTankaHoshu.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace KobetsuHinmeiTankaHoshu.APP
{
    /// <summary>
    /// 個別品名単価保守画面
    /// </summary>
    [Implementation]
    public partial class KobetsuHinmeiTankaHoshuForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 前回業者コード
        /// </summary>
        public string beforGyousaCD = string.Empty;

        /// <summary>
        /// 前回現場コード
        /// </summary>
        internal string beforGenbaCD = string.Empty;

        /// <summary>
        /// 個別品名単価保守画面ロジック
        /// </summary>
        private KobetsuHinmeiTankaHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 取引先CDのテキストチェンジイベント発生有無フラグ
        /// </summary>
        private bool IsTextChangedTorihikisaki = false;

        /// <summary>
        /// 業者CDのテキストチェンジイベント発生有無フラグ
        /// </summary>
        private bool IsTextChangedGyousha = false;

        /// <summary>
        /// 現場CDのテキストチェンジイベント発生有無フラグ
        /// </summary>
        private bool IsTextChangedGenba = false;

        private string nioroshiGyoushaBef { get; set; }
        private string nioroshiGyoushaAft { get; set; }

        private bool nowControlOut = false;

        /// <summary>
        /// 単位CD前回値
        /// </summary>
        private string preUnitCd = string.Empty;

        #endregion フィールド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KobetsuHinmeiTankaHoshuForm(WINDOW_TYPE windowType, string torihikisakiCd, string gyoushaCd, string genbaCd, string dennpyouKbn)
            : base(WINDOW_ID.M_KOBETSU_HINMEI_TANKA, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new KobetsuHinmeiTankaHoshuLogic(this);

            this.logic.WindowType = windowType;
            this.logic.TorihikisakiCd = torihikisakiCd;
            this.logic.GyoushaCd = gyoushaCd;
            this.logic.GenbaCd = genbaCd;
            this.logic.dennpyouKbn = dennpyouKbn;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }
            this.IsTextChangedTorihikisaki = false;
            this.IsTextChangedGyousha = false;
            this.IsTextChangedGenba = false;

            catchErr = Settitle();
            if (catchErr)
            {
                return;
            }

            if (codeCheck(0))
            {
                this.Search(null, e);
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        /// <summary>
        ///  title初期化
        /// </summary>
        private bool Settitle()
        {
            try
            {
                var parentForm = (MasterBaseForm)this.Parent;

                //title
                var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");

                //画面初期表示時、売上で画面作っていたためシステム設定値を読み込む
                this.logic.TitleInit();
                var titleCnt = titleControl.Text.Length;
                titleControl.Width = titleCnt * 30 + 60;

                // 修正権限が無い場合、基本品名単価読込を押下できないようにする
                if (r_framework.Authority.Manager.CheckAuthority("M212", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    parentForm.bt_process2.Enabled = true;
                }
                else
                {
                    parentForm.bt_process2.Enabled = false;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Settitle", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        ///　Title処理
        /// </summary>
        public virtual void Change(object sender, EventArgs e)
        {
            bool catchErr = this.Settitle();
            if (catchErr)
            {
                return;
            }
            if (codeCheck(0))
            {
                this.Search(sender, e);
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
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            var focus = (this.TopLevelControl as Form).ActiveControl;
            this.Ichiran.Focus();

            this.Ichiran.CausesValidation = false;

            if (codeCheck(1))
            {
                this.Ichiran.AllowUserToAddRows = true; // thongh 2015/12/28 #1978
                int count = this.logic.Search();
                if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    this.logic.SetIchiran(); // 空データをセットする。
                }
                else if (count > 0)
                {
                    if (this.logic.SetIchiran()) { return; }
                }
                else
                {
                    return;
                }
            }

            this.Ichiran.CausesValidation = true;

            this.logic.isNowLoadingHinmeiMaster = false;

            if (focus != null)
            {
                focus.Focus();
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            bool isNoErr = true;
            if (!base.RegistErrorFlag)
            {
                bool catchErr = false;
                /// 20141217 Houkakou 「個別品名単価入力」の日付チェックを追加する　start
                if (this.logic.DateCheck(out catchErr))
                {
                    if (catchErr)
                    {
                        return;
                    }
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    string[] errorMsg = { "適用開始日", "適用終了日" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    return;
                }
                /// 20141203 Houkakou 「個別品名単価入力」の日付チェックを追加する　end

                // 検索部CODEチェック
                if (!codeCheck(1))
                {
                    return;
                }

                //品名読込みモード時
                if (this.logic.isNowLoadingHinmeiMaster)
                {
                    catchErr = this.logic.LogicalDeleteForHinmeiYomikomi();
                    if (catchErr)
                    {
                        return;
                    }

                    isNoErr = this.logic.DuplicationCheck();
                    //重複データが登録不可
                    if (isNoErr)
                    {
                        catchErr = this.logic.CreateEntity(false);
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
                        this.ResetHinmeiLoad();//リセット
                    }
                }
                else
                {
                    //通常登録
                    isNoErr = this.logic.DuplicationCheck();
                    //重複データが登録不可
                    if (isNoErr)
                    {
                        catchErr = this.logic.CreateEntity(false);
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
            }
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            bool catchErr = false;
            if (this.logic.isNowLoadingHinmeiMaster)
            {
                catchErr = this.logic.DeleteForHinmeiLoading();
                if (catchErr)
                {
                    return;
                }
                //this.logic.SetIchiran();
                this.Ichiran.DataSource = null;//リロード
                this.Ichiran.DataSource = this.logic.SearchResult;
            }
            else
            {
                //通常削除
                if (!base.RegistErrorFlag)
                {
                    /// 20141217 Houkakou 「個別品名単価入力」の日付チェックを追加する　start
                    if (this.logic.DateCheck(out catchErr))
                    {
                        if (catchErr)
                        {
                            return;
                        }
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string[] errorMsg = { "適用開始日", "適用終了日" };
                        msgLogic.MessageBoxShow("E030", errorMsg);
                        return;
                    }
                    /// 20141203 Houkakou 「個別品名単価入力」の日付チェックを追加する　end

                    if (!codeCheck(1))
                    {
                        return;
                    }

                    catchErr = this.logic.CreateEntity(true);
                    if (catchErr)
                    {
                        return;
                    }
                    this.logic.LogicalDelete();
                    if (base.RegistErrorFlag)
                    {
                        return;
                    }
                    this.Search(sender, e);
                }
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            bool catchErr = this.logic.Cancel();
            if (catchErr)
            {
                return;
            }
            this.Search(sender, e);
            this.GYOUSHA_CD.Focus();
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Preview(object sender, EventArgs e)
        {
            this.logic.Preview();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            if (!codeCheck(1))
            {
                return;
            }

            bool catchErr = false;
            /// 20141217 Houkakou 「個別品名単価入力」の日付チェックを追加する　start
            if (this.logic.DateCheck(out catchErr))
            {
                if (catchErr)
                {
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] errorMsg = { "適用開始日", "適用終了日" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                return;
            }
            /// 20141203 Houkakou 「個別品名単価入力」の日付チェックを追加する　end

            this.logic.CSV();
        }


        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
            {
                // 取引先CD入力なしの場合
                Properties.Settings.Default.TorihikisakiValue_Text = string.Empty;
                Properties.Settings.Default.TorihikisakiName_Text = string.Empty;
            }
            else if (this.logic.CheckTorihikisaki(this.TORIHIKISAKI_CD.Text))
            {
                // 入力された取引先CDがマスタに存在する場合
                Properties.Settings.Default.TorihikisakiValue_Text = this.TORIHIKISAKI_CD.Text;
                Properties.Settings.Default.TorihikisakiName_Text = this.TORIHIKISAKI_NAME_RYAKU.Text;
            }

            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                // 業者CD、現場CDともに入力なしの場合
                Properties.Settings.Default.GyoushaValue_Text = string.Empty;
                Properties.Settings.Default.GyoushaName_Text = string.Empty;
                Properties.Settings.Default.GenbaValue_Text = string.Empty;
                Properties.Settings.Default.GenbaName_Text = string.Empty;
            }
            else if (this.logic.CheckGyousha(this.GYOUSHA_CD.Text))
            {
                // 入力された業者CDがマスタに存在する場合
                if (Properties.Settings.Default.GyoushaValue_Text != this.GYOUSHA_CD.Text)
                {
                    Properties.Settings.Default.GenbaValue_Text = string.Empty;
                    Properties.Settings.Default.GenbaName_Text = string.Empty;
                }
                Properties.Settings.Default.GyoushaValue_Text = this.GYOUSHA_CD.Text;
                Properties.Settings.Default.GyoushaName_Text = this.GYOUSHA_NAME_RYAKU.Text;
                if (this.logic.CheckGenba(this.GYOUSHA_CD.Text, this.GENBA_CD.Text))
                {
                    // 入力された現場CDがマスタに存在する場合
                    Properties.Settings.Default.GenbaValue_Text = this.GENBA_CD.Text;
                    Properties.Settings.Default.GenbaName_Text = this.GENBA_NAME_RYAKU.Text;
                }
            }

            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 一覧表示条件チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ICHIRAN_HYOUJI_JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = (CheckBox)sender;
            if (!item.Checked)
            {
                if (!this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "表示条件");
                    item.Checked = true;
                }
            }
        }

        /// <summary>
        /// 日付コントロールの初期設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CellEndEdit(object sender, GrapeCity.Win.MultiRow.CellEndEditEventArgs e)
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
                        gcMultiRow.CurrentCell.Value = this.logic.parentForm.sysDate.Date;
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                    }
                }
            }
        }

        /// <summary>
        /// 単位セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            this.logic.IchiranCellEnter(e);

            if (nowControlOut == false)
            {
                this.logic.IchiranCellSwitchCdName(e, Const.KobetsuHinmeiTankaHoshuConstans.FocusSwitch.IN);
            }
        }

        /// <summary>
        /// 取引先名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_TextChanged(object sender, EventArgs e)
        {
            this.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.IsTextChangedTorihikisaki = true;
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }

        /// <summary>
        /// 取引先CDチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.IsTextChangedTorihikisaki)
            {
                this.logic.SearchResult = null;
                this.logic.SearchResultAll = null;
                this.logic.SearchString = null;
                Ichiran.DataSource = null;
                Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1978
            }
            //  取引先名称の取得
            this.logic.SearchTorihikisakiName(e);

            this.IsTextChangedTorihikisaki = false;
        }

        /// <summary>
        /// 業者名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_TextChanged(object sender, EventArgs e)
        {
            //this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            //this.GENBA_CD.Text = String.Empty;
            //this.GENBA_NAME_RYAKU.Text = String.Empty;
            //this.IsTextChangedGyousha = true;
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }

        /// <summary>
        /// 業者CD検索ポップアップ後の処理を実施
        /// </summary>
        public void PopupAfterGyoushaCode()
        {
            if (beforGyousaCD != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }

            this.logic.SearchTorihikisakiByGyousha();
        }

        public void PopupBeforeGyoushaCode()
        {
            this.beforGyousaCD = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者CDチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            if (this.beforGyousaCD != this.GYOUSHA_CD.Text)
            {
                // 前回値と異なる場合
                this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;

                this.logic.SearchResult = null;
                this.logic.SearchResultAll = null;
                this.logic.SearchString = null;
                Ichiran.DataSource = null;
                Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1978
            }

            this.logic.SearchGyoushaName(e);
        }

        /// <summary>
        /// 現場名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_TextChanged(object sender, EventArgs e)
        {
            //this.GENBA_NAME_RYAKU.Text = string.Empty;
            //this.IsTextChangedGenba = true;
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }

        /// <summary>
        /// 現場CD検索ポップアップ後の処理を実施
        /// </summary>
        public void PopupAfterGenbaCode()
        {
            //this.logic.SearchTorihikisakiByGyousha();
            if (!beforGyousaCD.Equals(GYOUSHA_CD.Text) || !beforGenbaCD.Equals(GENBA_CD.Text))
            {
                this.logic.SearchGenbaName(null);
            }
        }

        public void PopupBeforeGenbaCode()
        {
            this.beforGyousaCD = this.GYOUSHA_CD.Text;
            this.beforGenbaCD = this.GENBA_CD.Text;
        }

        /// <summary>
        /// 現場CDチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            if (this.beforGyousaCD != this.GYOUSHA_CD.Text || this.beforGenbaCD != this.GENBA_CD.Text)
            {
                this.logic.SearchResult = null;
                this.logic.SearchResultAll = null;
                this.logic.SearchString = null;
                Ichiran.DataSource = null;
                Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1978
            }

            if (this.logic.ErrorCheckGenba())
            {
                this.logic.SearchGenbaName(e);
            }
        }

        /// <summary>
        /// 検索部CODEのチェック処理
        /// </summary>
        /// <param name="syoriKbn">処理区分</param>
        /// <returns>チェック結果</returns>
        private bool codeCheck(int syoriKbn)
        {
            try
            {
                var messageShowLogic = new MessageBoxShowLogic();

                // 取引先を必須入力項目から任意入力とし、業者を必須入力項目とする（2014/4/30）
                if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))//157931
                {
                    Ichiran.CurrentCell = null;
                    this.logic.SearchResult = null;
                    this.logic.SearchResultAll = null;
                    this.logic.SearchString = null;
                    Ichiran.DataSource = null;
                    Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1978

                    if (syoriKbn == 1)
                    {
                        this.GYOUSHA_CD.Focus();
                        messageShowLogic.MessageBoxShow("E001", "取引先また業者");//157931
                    }

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("codeCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// フォーカスアウト時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            bool catchErr = this.logic.IchiranCellValidating(e);
            if (catchErr)
            {
                return;
            }

            /// 20141217 Houkakou 「個別品名単価入力」の日付チェックを追加する　start
            if (e.CellName.Equals("TEKIYOU_BEGIN"))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                string strdate_to = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_END"].Value);

                if (!string.IsNullOrEmpty(strdate_to))
                {
                    //ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_END"], false);
                    this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_END"].Style.BackColor = Constans.NOMAL_COLOR;
                }
            }

            if (e.CellName.Equals("TEKIYOU_END"))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_END"].Style.BackColor = Constans.NOMAL_COLOR;
                string strdate_from = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_BEGIN"].Value);

                if (!string.IsNullOrEmpty(strdate_from))
                {
                    //ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_BEGIN"], false);
                    this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                }
            }
            /// 20141217 Houkakou 「個別品名単価入力」の日付チェックを追加する　end

            if (e.CellName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.UNIT_CD))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["UNIT_CD"].Style.BackColor = Constans.NOMAL_COLOR;
                if (e.FormattedValue == null || !e.FormattedValue.Equals(preUnitCd))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(e.FormattedValue)))
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells["UNIT_CD"].Value = DBNull.Value;
                        this.Ichiran.Rows[e.RowIndex].Cells["UNIT_NAME_RYAKU"].Value = string.Empty;
                    }
                    else
                    {
                        if (this.logic.unitCheck(e.RowIndex))
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "単位");
                            ((TextBox)this.Ichiran.EditingControl).SelectAll();
                            var cell = this.Ichiran.Rows[e.RowIndex].Cells["UNIT_CD"] as GcCustomTextBoxCell;
                            cell.IsInputErrorOccured = true;
                            cell.UpdateBackColor();
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidated(object sender, CellEventArgs e)
        {
            if (nowControlOut)
            {
                return;
            }

            nowControlOut = true;
            this.logic.IchiranCellSwitchCdName(e, Const.KobetsuHinmeiTankaHoshuConstans.FocusSwitch.OUT);
            nowControlOut = false;
        }

        /// <summary>
        /// セルフォーマット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            switch (e.CellName)
            {
                case "TANKA":
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        bool catchErr = false;
                        e.Value = this.logic.FormatSystemTanka(Decimal.Parse(e.Value.ToString()), out catchErr);
                    }
                    break;
            }
        }

        /// <summary>
        /// セル値変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Ichiran_CellValueChanged(object sender, CellEventArgs e)
        {
            if (e.CellName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_CD)
                && !this.Ichiran[e.RowIndex, e.CellIndex].Value.ToString().Equals(this.PreviousValue))
            {
                this.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_CD].Value = string.Empty;
                this.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_NAME_RYAKU].Value = string.Empty;
            }
        }

        /// <summary>
        /// 種別指定と全件の切り替えイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SYURUI_CheckedChanged(object sender, EventArgs e)
        {
            if (SYURUI_SHITEI.Checked)
            {
                this.SetSyuruiControlEnable(true);
            }
            else
            {
                this.SetSyuruiControlEnable(false);
            }
        }

        /// <summary>
        /// 種別指定と全件の切り替え用
        /// </summary>
        /// <param name="flg"></param>
        private void SetSyuruiControlEnable(bool flg)
        {
            this.SHURUI_CD.Text = "";
            this.SHURUI_NAME_RYAKU.Text = "";
            this.SHURUI_CD.Enabled = flg;
            this.SHURUI_NAME_RYAKU.Enabled = flg;
        }

        /// <summary>
        /// 種類CDチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHURUI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.SearchSyuruiName(e);
        }

        /// <summary>
        /// 種類CD変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHURUI_CD_TextChanged(object sender, EventArgs e)
        {
            this.SHURUI_NAME_RYAKU.Text = string.Empty;
        }

        /// <summary>
        /// 品名読み込み処理
        /// </summary>
        public virtual void HinmeiLoad(object sender, EventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (msgLogic.MessageBoxShowConfirm("登録単価をクリアして、品名を表示しますか？") == System.Windows.Forms.DialogResult.Yes)
            {
                //存在データのみ検索
                this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = true;
                this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
                this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = false;
                //検索条件を変えて、いきなり品名コピーを押した時のため再検索
                this.Search(sender, e);

                //必須項目が無い場合処理中断
                if (GYOUSHA_CD.Text == "" && this.TORIHIKISAKI_CD.Text == "")//157931
                {
                    return;
                }

                //表示されているデータを削除する。
                bool catchErr = this.logic.CreateDeleteEntity();
                if (catchErr)
                {
                    return;
                }

                //一覧クリア
                this.logic.SearchResult.Clear();
                this.Ichiran.DataSource = null;//リロード
                this.Ichiran.DataSource = this.logic.SearchResult;

                //品名ロード
                catchErr = this.logic.LoadingHinmeiListToIchiran();
                if (catchErr)
                {
                    return;
                }
                this.logic.isNowLoadingHinmeiMaster = true;//読込み中は削除ボタン動作変更

                this.Ichiran.DataSource = null;//リロード
                this.Ichiran.DataSource = this.logic.SearchResult;
            }
        }

        /// <summary>
        /// 品名ロード終了
        /// </summary>
        public void ResetHinmeiLoad()
        {
            this.logic.isNowLoadingHinmeiMaster = false;//リセット
        }

        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.beforGyousaCD = GYOUSHA_CD.Text;
        }

        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.beforGyousaCD = GYOUSHA_CD.Text;
            this.beforGenbaCD = GENBA_CD.Text;
        }

        // VUNGUYEN 20150525 #1294 START
        private void Ichiran_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            GcMultiRow gc = sender as GcMultiRow;
            this.logic.cell = gc.CurrentCell;

            if (this.logic.cell.Name.Equals("TEKIYOU_END"))
            {
                e.Control.DoubleClick += this.logic.IchiranDoubleClick;
            }
        }

        // VUNGUYEN 20150525 #1294 END

        /// <summary>
        /// 荷降業者POPUP_BEFイベント
        /// </summary>
        public void NIOROSHI_GYOUSHA_POPUP_BEF()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            nioroshiGyoushaBef = string.Empty;
            if (this.Ichiran.CurrentCell.Name == "NIOROSHI_GYOUSHA_CD")
            {
                nioroshiGyoushaBef = Convert.ToString(this.Ichiran.CurrentCell.EditedFormattedValue);
            }
        }

        /// <summary>
        /// 荷降業者POPUP_AFTイベント
        /// </summary>
        public void NIOROSHI_GYOUSHA_POPUP_AFT()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            if (this.Ichiran.CurrentCell.Name == "NIOROSHI_GYOUSHA_CD")
            {
                nioroshiGyoushaAft = Convert.ToString(this.Ichiran.CurrentCell.EditedFormattedValue);
                if (nioroshiGyoushaBef != nioroshiGyoushaAft)
                {
                    this.Ichiran.CurrentRow.Cells["NIOROSHI_GENBA_CD"].Value = string.Empty;
                    this.Ichiran.CurrentRow.Cells["NIOROSHI_GENBA_RYAKU"].Value = string.Empty;
                }
            }
        }
        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            this.logic.CancelCondition();
        }
    }
}