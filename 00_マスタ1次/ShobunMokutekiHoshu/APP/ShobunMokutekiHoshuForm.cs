// $Id: ShobunMokutekiHoshuForm.cs 38032 2014-12-23 07:44:08Z fangjk@oec-h.com $
using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using ShobunMokutekiHoshu.Logic;

namespace ShobunMokutekiHoshu.APP
{
    /// <summary>
    /// 処分目的画面
    /// </summary>
    [Implementation]
    public partial class ShobunMokutekiHoshuForm : SuperForm
    {
        /// <summary>
        /// 処分目的画面ロジック
        /// </summary>
        private ShobunMokutekiHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShobunMokutekiHoshuForm()
            : base(WINDOW_ID.M_SHOBUN_MOKUTEKI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ShobunMokutekiHoshuLogic(this);

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

            int count = this.logic.Search();
            if (count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
            }
            else if(count > 0)
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
                /// 20141217 Houkakou 「処分目的入力」の日付チェックを追加する　start
                if (this.logic.DateCheck(out catchErr) && !catchErr)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    string[] errorMsg = { "適用開始日", "適用終了日" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    return;
                }
                /// 20141217 Houkakou 「処分目的入力」の日付チェックを追加する　end
                if (catchErr)
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
                /// 20141217 Houkakou 「処分目的入力」の日付チェックを追加する　start
                if (this.logic.DateCheck(out catchErr) && !catchErr)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    string[] errorMsg = { "適用開始日", "適用終了日" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    return;
                }
                /// 20141217 Houkakou 「処分目的入力」の日付チェックを追加する　end
                if (catchErr)
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
            Search(sender, e);
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public virtual void Preview(object sender, EventArgs e)
        //{
        //    this.logic.Preview();
        //}

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            bool catchErr = false;
            /// 20141217 Houkakou 「処分目的入力」の日付チェックを追加する　start
            if (this.logic.DateCheck(out catchErr) && !catchErr)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] errorMsg = { "適用開始日", "適用終了日" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                return;
            }
            /// 20141217 Houkakou 「処分目的入力」の日付チェックを追加する　end
            if (catchErr)
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
            bool catchErr = this.logic.CancelCondition();
            if (catchErr)
            {
                return;
            }
            Search(sender, e);
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
                        gcMultiRow.CurrentCell.Value = DateTime.Today;
                    }
                }
            }
        }

        /// <summary>
        /// 処分目的CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.ShobunMokutekiHoshuConstans.SHOBUN_MOKUTEKI_CD))
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

            /// 20141217 Houkakou 「処分目的入力」の日付チェックを追加する　start
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
            /// 20141217 Houkakou 「処分目的入力」の日付チェックを追加する　end
        }

        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 1行目が新行の場合、適用開始日に本日を設定
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran[e.RowIndex, "TEKIYOU_BEGIN"].Value = DateTime.Today;
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
        }
    }
}
