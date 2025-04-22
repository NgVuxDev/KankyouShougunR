// $Id: KeiryouChouseiHoshuForm.cs 37791 2014-12-19 08:22:08Z fangjk@oec-h.com $
using System;
using System.Data;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KeiryouChouseiHoshu.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using r_framework.Utility;

namespace KeiryouChouseiHoshu.APP
{
    /// <summary>
    /// 計量調整保守画面
    /// </summary>
    [Implementation]
    public partial class KeiryouChouseiHoshuForm : SuperForm
    {
        /// <summary>
        /// 計量調整保守画面ロジック
        /// </summary>
        private KeiryouChouseiHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 前回取引先CD
        /// </summary>
        private string ZenTorihikisaCD;

        /// <summary>
        /// 前回業者CD
        /// </summary>
        private string ZenGyousyaCD;

        /// <summary>
        /// 前回現場CD
        /// </summary>
        private string ZenGenbaCD;

        // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KeiryouChouseiHoshuForm()
            : base(WINDOW_ID.M_KEIRYOU_CHOUSEI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new KeiryouChouseiHoshuLogic(this);

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

            if (codeCheck(0))
            {
                //this.logic.Search();
                this.Search(null, e);
            }

			// Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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

            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            if (codeCheck(1))
            {
                int count = this.logic.Search();
                if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
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

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

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
            if (!base.RegistErrorFlag)
            {
                bool catchErr = false;
                /// 20141217 Houkakou 「計量調整入力」の日付チェックを追加する　start
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
                /// 20141217 Houkakou 「計量調整入力」の日付チェックを追加する　end

                // 検索部CODEチェック
                if (!codeCheck(1))
                {
                    return;
                }

                // 明細行データチェック
                DataTable dt = this.Ichiran.DataSource as DataTable;
                if (dt == null)
                {
                    return;
                }

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

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                bool catchErr = false;
                /// 20141217 Houkakou 「計量調整入力」の日付チェックを追加する　start
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
                /// 20141217 Houkakou 「計量調整入力」の日付チェックを追加する　end
                if (!codeCheck(1))
                {
                    return;
                }

                DataTable dt = this.Ichiran.DataSource as DataTable;
                if (dt == null)
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
            //this.Search(sender, e);

            if (this.Ichiran.DataSource != null)
            {
                this.Ichiran.DataSource = null;
            }

            this.TORIHIKISAKI_CD.Focus();
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
            /// 20141217 Houkakou 「計量調整入力」の日付チェックを追加する　start
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
            /// 20141217 Houkakou 「計量調整入力」の日付チェックを追加する　end

            DataTable dt = this.Ichiran.DataSource as DataTable;
            if (dt == null)
            {
                return;
            }
            this.logic.CSV();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            bool catchErr =this.logic.Cancel();
            if (catchErr)
            {
                return;
            }

            if (this.Ichiran.DataSource != null)
            {
                this.Ichiran.DataSource = null;
            }

            this.TORIHIKISAKI_CD.Focus();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
            Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
            Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
            Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;
            Properties.Settings.Default.GenbaValue_Text = this.GENBA_CD.Text;
            Properties.Settings.Default.GenbaName_Text = this.GENBA_NAME_RYAKU.Text;
            Properties.Settings.Default.GyoushaValue_Text = this.GYOUSHA_CD.Text;
            Properties.Settings.Default.GyoushaName_Text = this.GYOUSHA_NAME_RYAKU.Text;
            Properties.Settings.Default.TorihikisakiValue_Text = this.TORIHIKISAKI_CD.Text;
            Properties.Settings.Default.TorihikisakiName_Text = this.TORIHIKISAKI_NAME_RYAKU.Text;

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
                        gcMultiRow.CurrentCell.Value = DateTime.Today;
                    }
                }
            }
        }

        /// <summary>
        /// 計量調整の重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (this.Ichiran.CurrentRow.ReadOnly == true) return;
            if (e.CellName.Equals(Const.KeiryouChouseiHoshuConstans.HINMEI_CD) ||
                e.CellName.Equals(Const.KeiryouChouseiHoshuConstans.UNIT_CD))
            {
                bool isNoErr = this.logic.DuplicationCheck();
                if (!isNoErr)
                {
                    e.Cancel = true;

                    GcMultiRow gc = sender as GcMultiRow;
                    if (gc != null && gc.EditingControl != null)
                    {
                        ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                    }

                    return;
                }
            }

            if (e.CellName.Equals(Const.KeiryouChouseiHoshuConstans.HINMEI_CD))
            {
                String Hinmeicd = Ichiran[e.RowIndex, e.CellIndex].Value.ToString();
                if (!string.IsNullOrEmpty(Hinmeicd))
                {
                    this.logic.SearchHinmei(Hinmeicd, e);
                }
            }

            /// 20141217 Houkakou 「計量調整入力」の日付チェックを追加する　start
            if (e.CellName.Equals("TEKIYOU_BEGIN"))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                string strdate_to = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_END"].Value);

                if (!string.IsNullOrEmpty(strdate_to))
                {
                    this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_END"].Style.BackColor = Constans.NOMAL_COLOR;
                }
            }

            if (e.CellName.Equals("TEKIYOU_END"))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_END"].Style.BackColor = Constans.NOMAL_COLOR;
                string strdate_from = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_BEGIN"].Value);

                if (!string.IsNullOrEmpty(strdate_from))
                {
                    this.Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                }
            }
            /// 20141217 Houkakou 「計量調整入力」の日付チェックを追加する　end
        }

        /// <summary>
        /// 単位セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            if ((this.TORIHIKISAKI_CD.TextLength <= 0 && this.GYOUSHA_CD.TextLength <= 0) ||
            (this.logic.SearchResultAll == null))
            {
                this.Ichiran.CurrentRow.Selectable = false;
            }
            else
            {
                this.Ichiran.CurrentRow.Selectable = true;
            }

            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = false;
            }
            else
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = true;
            }

            // 1行目が新行の場合、適用開始日に本日を設定
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran[e.RowIndex, "TEKIYOU_BEGIN"].Value = DateTime.Today;
            }
        }

        /// <summary>
        /// 一覧セルフォーマット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellName.Equals(Const.KeiryouChouseiHoshuConstans.UNIT_CD))
            {
                if (this.Ichiran[e.RowIndex, Const.KeiryouChouseiHoshuConstans.UNIT_NAME_RYAKU].Value != null)
                {
                    e.Value = this.Ichiran[e.RowIndex, Const.KeiryouChouseiHoshuConstans.UNIT_NAME_RYAKU].Value.ToString();
                }
            }
        }

        /// <summary>
        /// セルデータエラーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_DataError(object sender, DataErrorEventArgs e)
        {
            // 例外を無視する
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (e.CellName.Equals(Const.KeiryouChouseiHoshuConstans.UNIT_CD) && (e.Context & DataErrorContexts.CurrentCellChange) != DataErrorContexts.CurrentCellChange)
            {
                msgLogic.MessageBoxShow("E020", "単位");
                e.Cancel = true;
                ((TextBox)this.Ichiran.EditingControl).SelectAll();
            }
        }

        /// <summary>
        /// 取引先名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_TextChanged(object sender, EventArgs e)
        {
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            //this.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            //Ichiran.DataSource = null;
            //Ichiran.RowCount = 1;
            //this.logic.SearchResult = null;
            //this.logic.SearchResultAll = null;
            //this.logic.SearchString = null;
            //FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        }

        /// <summary>
        /// 取引先CDチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            if (!this.ZenTorihikisaCD.Equals(this.TORIHIKISAKI_CD.Text))
            {
                this.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                Ichiran.DataSource = null;
                Ichiran.RowCount = 1;
                this.logic.SearchResult = null;
                this.logic.SearchResultAll = null;
                this.logic.SearchString = null;
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);

                this.logic.SearchTorihikisakiName(e);
            }
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        }

        /// <summary>
        /// 業者名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_TextChanged(object sender, EventArgs e)
        {
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            //this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            //this.GENBA_CD.Text = String.Empty;
            //this.GENBA_NAME_RYAKU.Text = String.Empty;
            //Ichiran.DataSource = null;
            //Ichiran.RowCount = 1;
            //this.logic.SearchResult = null;
            //this.logic.SearchResultAll = null;
            //this.logic.SearchString = null;
            //FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        }

        /// <summary>
        /// 業者CDチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            if (!this.ZenGyousyaCD.Equals(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_NAME_RYAKU.Text = String.Empty;
                Ichiran.DataSource = null;
                Ichiran.RowCount = 1;
                this.logic.SearchResult = null;
                this.logic.SearchResultAll = null;
                this.logic.SearchString = null;
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);

                this.logic.SearchGyoushaName(e);
            }
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        }

        /// <summary>
        /// 現場名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_TextChanged(object sender, EventArgs e)
        {
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            //this.GENBA_NAME_RYAKU.Text = string.Empty;
            //Ichiran.DataSource = null;
            //Ichiran.RowCount = 1;
            //this.logic.SearchResult = null;
            //this.logic.SearchResultAll = null;
            //this.logic.SearchString = null;
            //FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        }

        /// <summary>
        /// 現場CDチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            if (!this.ZenGenbaCD.Equals(this.GENBA_CD.Text))
            {
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                Ichiran.DataSource = null;
                Ichiran.RowCount = 1;
                this.logic.SearchResult = null;
                this.logic.SearchResultAll = null;
                this.logic.SearchString = null;
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);

                this.logic.SearchGenbaName(e);
            }
            // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
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

                if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text) && string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    if (syoriKbn == 1)
                    {
                        Ichiran.DataSource = null;
                        Ichiran.RowCount = 1;
                        this.logic.SearchResult = null;
                        this.logic.SearchResultAll = null;
                        this.logic.SearchString = null;
                        FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
                        messageShowLogic.MessageBoxShow("E001", "取引先又は業者");

                        if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
                        {
                            this.TORIHIKISAKI_CD.Focus();
                        }
                        else if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                        {
                            this.GYOUSHA_CD.Focus();
                        }
                    }

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("codeCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }

        }

        // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        private void TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            ZenTorihikisaCD = this.TORIHIKISAKI_CD.Text;
        }

        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            ZenGyousyaCD = this.GYOUSHA_CD.Text;
        }

        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            ZenGenbaCD = this.GENBA_CD.Text;
        }

        // 2015/10/12 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
    }
}