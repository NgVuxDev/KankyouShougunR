// $Id: DenManiHaikiShuruiSaibunruiHoshuForm.cs 14165 2014-01-15 13:13:30Z sugioka $
using System;
using System.Windows.Forms;
using DenManiHaikiShuruiHoshu.Logic;
using GrapeCity.Win.MultiRow;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace DenManiHaikiShuruiHoshu.APP
{
    /// <summary>
    /// メニュー権限保守画面
    /// </summary>
    [Implementation]
    public partial class DenManiHaikiShuruiSaibunruiHoshuForm : SuperForm
    {
        /// <summary>
        /// 呼び出し元フォーム
        /// 社員情報等の取得に使用
        /// </summary>
        public SuperForm CallForm { get; set; }

        /// <summary>
        /// メニュー権限保守画面ロジック
        /// </summary>
        private DenManiHaikiShuruiSaibunruiHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenManiHaikiShuruiSaibunruiHoshuForm()
            : base(WINDOW_ID.M_DENSHI_HAIKI_SHURUI_SAIBUNRUI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new DenManiHaikiShuruiSaibunruiHoshuLogic(this);

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

            if (!string.IsNullOrWhiteSpace(this.EDI_MEMBER_ID.Text))
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
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
            this.Ichiran.AllowUserToAddRows = true;//thongh 2015/12/30 #1980
            int count = this.logic.Search();
            if (count == 0 && !string.IsNullOrWhiteSpace(this.EDI_MEMBER_ID.Text))
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
            }
            else if (count > 0)
            {
                this.logic.SetIchiran();
            }
            else
            {
                return;
            }

            this.logic.EditableToPrimaryKey();

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                bool catchErr = this.logic.CreateEntity(false);
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
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                bool catchErr = this.logic.CreateEntity(true);
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
            this.logic.Cancel();
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
            this.logic.CSV();
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

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            Properties.Settings.Default.SAIBUNRUI_EDI_MEMBER_ID_Text = this.EDI_MEMBER_ID.Text;

            Properties.Settings.Default.SAIBUNRUI_ConditionValue_Text = this.CONDITION_VALUE.Text;
            Properties.Settings.Default.SAIBUNRUI_ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
            Properties.Settings.Default.SAIBUNRUI_ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
            Properties.Settings.Default.SAIBUNRUI_ConditionItem_Text = this.CONDITION_ITEM.Text;

            Properties.Settings.Default.SAIBUNRUI_ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

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
                if (!this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
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
        private void Ichiran_CellEndEdit(object sender, GrapeCity.Win.MultiRow.CellEndEditEventArgs e)
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
        /// 社員CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD))
            {
                bool catchErr = this.logic.SetHaikiKbn(e.RowIndex);
                if (catchErr)
                {
                    return;
                }
                catchErr = this.logic.SetHaikiBunrui(e.RowIndex);
                if (catchErr)
                {
                    return;
                }
            }
            if (e.CellName.Equals(Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_SAIBUNRUI_CD) && e.FormattedValue != null)
            {
                string cd = e.FormattedValue.ToString().PadLeft(3, '0');
                if (cd.Equals("000") && this.Ichiran.Rows[e.RowIndex].Selectable)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E028");
                    this.Ichiran[e.RowIndex, e.CellIndex].Value = string.Empty;
                    e.Cancel = true;
                    return;
                }
            }
            if (e.CellName.Equals(Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD) || e.CellName.Equals(Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_SAIBUNRUI_CD))
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
        }

        private void Ichiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellName.Equals("HAIKI_KBN"))
            {
                this.logic.SetHaikiKbnName(e);
            }
            if (e.CellName.Equals("HAIKI_BUNRUI"))
            {
                this.logic.SetHaikiBunruiName(e);
            }
        }

        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 報告分類CDが空白の場合、明細入力ができないようにする
            var isLockedRow = DenManiHaikiShuruiSaibunruiHoshuLogic.CheckLockedRow(this.Ichiran.Rows[e.RowIndex]);
            if ((this.EDI_MEMBER_ID.TextLength <= 0) ||
                (this.logic.SearchResultAll == null) ||
                isLockedRow)
            {
                this.Ichiran.CurrentRow.Selectable = false;
            }
            else
            {
                this.Ichiran.CurrentRow.Selectable = true;
            }

            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow ||
                isLockedRow)
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = false;
            }
            else
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = true;
            }
        }

        /// <summary>
        /// 加入者番号入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EDI_MEMBER_ID_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool catchErr = false;
            if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID.Text) && !this.logic.SetJigyoushaName(this.EDI_MEMBER_ID.Text, out catchErr) && !catchErr)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "電子事業者");
                this.EDI_MEMBER_ID.Text = string.Empty;
                e.Cancel = true;
            }
            if (catchErr)
            {
                this.EDI_MEMBER_ID.Text = string.Empty;
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 加入者番号変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EDI_MEMBER_ID_TextChanged(object sender, EventArgs e)
        {
            this.JIGYOUSHA_NAME.Text = string.Empty;
            this.logic.SearchResult = null;
            this.logic.SearchResultAll = null;
            this.logic.SetIchiran();
            this.Ichiran.AllowUserToAddRows = false;//thongh 2015/12/30 #1980
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }

        /// <summary>
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void DenManiHaikiShuruiSaibunruiHoshuForm_UserRegistCheck(object sender, r_framework.Event.RegistCheckEventArgs e)
        {
            this.logic.RegistCheck(sender, e);
        }

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DenManiHaikiShuruiSaibunruiHoshuForm_Shown(object sender, EventArgs e)
        {
            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }
    }
}
