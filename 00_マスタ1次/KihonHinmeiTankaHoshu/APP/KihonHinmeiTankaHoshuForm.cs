// $Id: KihonHinmeiTankaHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KihonHinmeiTankaHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace KihonHinmeiTankaHoshu.APP
{
    /// <summary>
    /// 基本品名単価保守画面
    /// </summary>
    [Implementation]
    public partial class KihonHinmeiTankaHoshuForm : SuperForm
    {
        /// <summary>
        /// 基本品名単価保守画面ロジック
        /// </summary>
        private KihonHinmeiTankaHoshuLogic logic;

        /// <summary>
        /// 単位CD前回値
        /// </summary>
        private string preUnitCd = string.Empty;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        private string nioroshiGyoushaBef { get; set; }
        private string nioroshiGyoushaAft { get; set; }

        private bool nowControlOut = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KihonHinmeiTankaHoshuForm()
            : base(WINDOW_ID.M_KIHON_HINMEI_TANKA, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new KihonHinmeiTankaHoshuLogic(this);

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
            this.Search(null, e);
            Settitle();

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
                bool catchErr = this.logic.TitleInit();
                if (catchErr)
                {
                    return true;
                }
                var titleCnt = titleControl.Text.Length;
                titleControl.Width = titleCnt * 30 + 60;
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
            bool catchErr = this.logic.TitleInit();
            if (catchErr)
            {
                return;
            }
            this.Search(sender, e);
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

            int count = this.logic.Search();
            if (count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
                this.logic.SetIchiran();//空データをセットする。
            }
            else if (count > 0)
            {
                if (this.logic.SetIchiran()) { return; }
            }
            else
            {
                return;
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
                /// 20141217 Houkakou 「基本品名単価入力」の日付チェックを追加する　start
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
                /// 20141217 Houkakou 「基本品名単価入力」の日付チェックを追加する　end

                catchErr = false;
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
                        this.ResetHinmeiLoad();//リセット
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
                this.logic.SetIchiran();
            }
            else
            {
                //通常削除
                if (!base.RegistErrorFlag)
                {
                    /// 20141217 Houkakou 「基本品名単価入力」の日付チェックを追加する　start
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
                    /// 20141217 Houkakou 「基本品名単価入力」の日付チェックを追加する　end

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
            Search(sender, e);
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
            bool catchErr = false;
            /// 20141217 Houkakou 「基本品名単価入力」の日付チェックを追加する　start
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
            /// 20141217 Houkakou 「基本品名単価入力」の日付チェックを追加する　end
            this.logic.CSV();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            bool catchErr = this.logic.CancelCondition();
            if (catchErr)
            {
                return;
            }
            this.ResetHinmeiLoad();//リセット
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

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
                this.logic.IchiranCellSwitchCdName(e, Const.KihonHinmeiTankaHoshuConstans.FocusSwitch.IN);
            }
        }

        /// <summary>
        /// フォーカスアウト時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.IchiranCellValidating(e);

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

            if (e.CellName.Equals(Const.KihonHinmeiTankaHoshuConstans.UNIT_CD))
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
            this.logic.IchiranCellSwitchCdName(e, Const.KihonHinmeiTankaHoshuConstans.FocusSwitch.OUT);
            nowControlOut = false;
        }

        /// <summary>
        /// セルフォーマット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            switch (e.CellName)
            {
                case "TANKA":
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        e.Value = this.logic.FormatSystemTanka(Decimal.Parse(e.Value.ToString()));
                    }
                    break;
            }
        }

        /// <summary>
        /// セル値変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CellValueChanged(object sender, CellEventArgs e)
        {
            if (e.CellName.Equals(Const.KihonHinmeiTankaHoshuConstans.DENSHU_KBN_CD)
                && !this.Ichiran[e.RowIndex, e.CellIndex].Value.ToString().Equals(this.PreviousValue))
            {
                this.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.HINMEI_CD].Value = string.Empty;
                this.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.HINMEI_NAME_RYAKU].Value = string.Empty;
            }
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
                this.logic.SetIchiran();
                this.logic.isNowLoadingHinmeiMaster = true;//読込み中は削除ボタン動作変更
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
        private bool SetSyuruiControlEnable(bool flg)
        {
            try
            {
                this.SHURUI_CD.Text = "";
                this.SHURUI_NAME_RYAKU.Text = "";
                this.SHURUI_CD.Enabled = flg;
                this.SHURUI_NAME_RYAKU.Enabled = flg;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSyuruiControlEnable", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
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
        /// 品名ロード終了
        /// </summary>
        public void ResetHinmeiLoad()
        {
            this.logic.isNowLoadingHinmeiMaster = false;//リセット
        }

        // VUNGUYEN 20150525 #1294 START
        private void Ichiran_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            GcMultiRow gc = sender as GcMultiRow;
            this.logic.cell = gc.CurrentCell;

            if (this.logic.cell.Name.Equals("TEKIYOU_END"))
            {
                e.Control.DoubleClick += this.logic.Ichiran_DoubleClick;
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
    }
}