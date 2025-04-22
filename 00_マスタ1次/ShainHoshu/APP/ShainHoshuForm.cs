// $Id: ShainHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using ShainHoshu.Const;
using ShainHoshu.Logic;
using System.Windows.Forms;


namespace ShainHoshu.APP
{
    /// <summary>
    /// 社員保守画面
    /// </summary>
    [Implementation]
    public partial class ShainHoshuForm : SuperForm
    {
        /// <summary>
        /// 社員保守画面ロジック
        /// </summary>
        private ShainHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShainHoshuForm()
            : base(WINDOW_ID.M_SHAIN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ShainHoshuLogic(this);

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

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
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

            int count = this.logic.Search();
            if (count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
            }
            else if (count > 0)
            {
                bool catchErr = this.logic.SetIchiran();
                if (catchErr)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
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
                if (this.logic.HasErrorRegistCheck(false))
                {
                    return;
                }
                
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
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                if (this.logic.HasErrorRegistCheck(true))
                {
                    return;
                }

                if (this.logic.CheckDelete())
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

            Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
            Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
            Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
            Properties.Settings.Default.S = this.CONDITION_ITEM.Text;

            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
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
            if (e.CellName.Equals(Const.ShainHoshuConstans.SHAIN_CD))
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

            //部署CDのチェック
            if (e.CellName.Equals(Const.ShainHoshuConstans.BUSHO_CD))
            {
                if (!this.logic.BushoCdValidated())
                {
                    e.Cancel = true;
                }
            }

            //ログインIDチェック
            if (e.CellName.Equals(Const.ShainHoshuConstans.LOGIN_ID))
            {
                bool isNoErr = this.logic.DuplicationCheckLoginId();
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

            //if (e.CellName.Equals(ShainHoshuConstans.WARIATE_JUN))
            //{

            //}
        }

        /// <summary>
        /// セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran.Rows[e.RowIndex][ShainHoshuConstans.DELETE_FLG].Selectable = false;
            }
            else
            {
                bool catchErr = false;
                // テーブル固定値のデータかどうかを調べる
                if (!this.logic.CheckFixedRow(this.Ichiran.Rows[e.RowIndex], out catchErr) && !catchErr)
                {
                    this.Ichiran.Rows[e.RowIndex][ShainHoshuConstans.DELETE_FLG].Selectable = true;
                }
            }
        }

        /// <summary>
        /// 一覧行追加処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellName.Equals(Const.ShainHoshuConstans.PASSWORD))
            {
                this.logic.SetPasswordChars(e);
            }
        }

        /// <summary>
        /// 表示時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Form_Shown(object sender, EventArgs e)
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
            if (this.logic.SetFixedIchiran()) { return; }

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
        }

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShainHoshuForm_Shown(object sender, EventArgs e)
        {
            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        public void BeforeRegist()
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
            this.logic.SetFixedIchiran();
            this.logic.EditableToPrimaryKey();
            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
        }

        //20250311
        private void Ichiran_CellValueChanged(object sender, CellEventArgs e)
        {
            if (e.CellName.Equals(ShainHoshuConstans.UNTEN_KBN))
            {
                this.Ichiran.CommitEdit(DataErrorContexts.Commit);
                this.Ichiran.EndEdit();

                var row = this.Ichiran.Rows[e.RowIndex];
                object unten_kbn = row.Cells[ShainHoshuConstans.UNTEN_KBN].Value;

                if (unten_kbn != null && bool.TryParse(unten_kbn.ToString(), out bool result) && !result)
                {
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].Value = DBNull.Value;
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].ReadOnly = true;
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].UpdateBackColor(false);

                }
                else
                {
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].ReadOnly = false;
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].UpdateBackColor(true);
                }
                this.Ichiran.Invalidate();
                this.Ichiran.Refresh();
            }
        }

        private void Ichiran_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.RegistCheck())
            {
                e.Cancel = true;
            }
        }

        private void Ichiran_CellEditedFormattedValueChanged(object sender, CellEditedFormattedValueChangedEventArgs e)
        {
            if (e.CellName.Equals(ShainHoshuConstans.UNTEN_KBN))
            {
                this.Ichiran.CommitEdit(DataErrorContexts.Commit);
                this.Ichiran.EndEdit();

                var row = this.Ichiran.Rows[e.RowIndex];
                object unten_kbn = row.Cells[ShainHoshuConstans.UNTEN_KBN].Value;

                if (unten_kbn != null && bool.TryParse(unten_kbn.ToString(), out bool result) && !result)
                {
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].Value = string.Empty;
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].ReadOnly = true;
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].UpdateBackColor(false);

                }
                else
                {
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].ReadOnly = false;
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].UpdateBackColor(true);
                }
                this.Ichiran.Invalidate();
                this.Ichiran.Refresh();
            }

            if (e.CellName.Equals(ShainHoshuConstans.NYUURYOKU_TANTOU_KBN))
            {
                this.Ichiran.CommitEdit(DataErrorContexts.Commit);
                this.Ichiran.EndEdit();

                var row = this.Ichiran.Rows[e.RowIndex];
                object nin_i = row.Cells[ShainHoshuConstans.NYUURYOKU_TANTOU_KBN].Value;

                if (nin_i != null && bool.TryParse(nin_i.ToString(), out bool result) && !result)
                {
                    row.Cells[ShainHoshuConstans.NIN_I_TORIHIKISAKI_FUKA].Enabled = false;
                    row.Cells[ShainHoshuConstans.NIN_I_TORIHIKISAKI_FUKA].Value = false;
                }
                else
                {
                    row.Cells[ShainHoshuConstans.NIN_I_TORIHIKISAKI_FUKA].Enabled = true;
                }
                this.Ichiran.Invalidate();
                this.Ichiran.Refresh();
            }
        }
    }
}