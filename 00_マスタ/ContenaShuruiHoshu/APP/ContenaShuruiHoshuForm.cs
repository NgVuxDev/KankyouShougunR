// $Id: ContenaShuruiHoshuForm.cs 4037 2013-10-18 01:56:36Z sys_dev_23 $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Intercepter;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Entity;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using ContenaShuruiHoshu.Logic;

namespace ContenaShuruiHoshu.APP
{
    /// <summary>
    /// コンテナ種類画面
    /// </summary>
    [Implementation]
    [Aspect(typeof(TraceLogIntercepter))]
    public partial class ContenaShuruiHoshuForm : SuperForm
    {
        /// <summary>
        /// コンテナ種類画面ロジック
        /// </summary>
        private ContenaShuruiHoshuLogic logic;

        bool IsCdFlg = false;

        public ContenaShuruiHoshuForm()
            : base(WINDOW_ID.M_CONTENA_SHURUI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ContenaShuruiHoshuLogic(this);

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
            this.logic.WindowInit();
            this.Search(null, e);
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            if (this.logic.Search() == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
                this.Ichiran.DataSource = this.logic.SearchResult;
                this.logic.ColumnAllowDBNull();
                return;
            }

            var table = this.logic.SearchResult;

            table.BeginLoadData();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }

            this.Ichiran.DataSource = table;
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
                if (this.logic.ActionBeforeCheck())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E061");
                    return;
                }
                Boolean isCheckOK = this.logic.CheckBeforeUpdate();

                if (!isCheckOK)
                {
                    return;
                }

                Boolean isOK = this.logic.CreateEntity(false);
                if (!isOK)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E061");
                    return;
                }
                this.logic.Regist(base.RegistErrorFlag);
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
                this.logic.CreateEntity(true);
                this.logic.LogicalDelete();
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
            Search(sender, e);
            this.CONDITION_ITEM.Focus();
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            if (this.logic.ActionBeforeCheck())
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E044");
                return;
            }
            this.logic.CSVOutput();
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClearCondition(object sender, EventArgs e)
        {
            this.logic.ClearCondition();
            this.CONDITION_ITEM.Focus();
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
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 新追加行のセル既定値処理
        /// </summary>
        private void Ichiran_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                // セルの既定値処理
                Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_BEGIN"].Value = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, 1);
                Ichiran.Rows[e.RowIndex].Cells["TEKIYOU_END"].Value = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.DaysInMonth(DateTime.Now.Date.Year, DateTime.Now.Date.Month));
                Ichiran.Rows[e.RowIndex].Cells["UPDATE_DATE"].Value = DateTime.Now;
                Ichiran.Rows[e.RowIndex].Cells["CREATE_DATE"].Value = DateTime.Now;
                Ichiran.Rows[e.RowIndex].Cells["DELETE_FLG"].Value = false;
                Ichiran.Rows[e.RowIndex].Cells["CREATE_PC"].Value = "";
                Ichiran.Rows[e.RowIndex].Cells["UPDATE_PC"].Value = "";
            }
        }

        /// <summary>
        /// コンテナ種類CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["CONTENA_SHURUI_CD"].Value) &&
                !"".Equals(Ichiran.Rows[e.RowIndex].Cells["CONTENA_SHURUI_CD"].Value) &&
                Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(Const.ContenaShuruiHoshuConstans.CONTENA_SHURUI_CD))
            {
                string contena_Shurui_Cd = (string)Ichiran.Rows[e.RowIndex].Cells["CONTENA_SHURUI_CD"].Value;

                bool isError = this.logic.DuplicationCheck(contena_Shurui_Cd);

                if (isError)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E022", "入力されたコンテナ種類CD");
                    e.Cancel = true;
                    this.Ichiran.BeginEdit(false);
                    return;
                }
            }
        }

        /// <summary>
        /// IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                case 6:
                case 7:
                    Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case 2:
                case 3:
                case 5:
                    Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                    break;
                case 4:
                    Ichiran.ImeMode = System.Windows.Forms.ImeMode.Katakana;
                    break;
            }
        }

        /// <summary>
        /// コンテナCD入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns["CONTENA_SHURUI_CD"].Index)
            {
                IsCdFlg = true;
                TextBox itemID = e.Control as TextBox;
                if (itemID != null)
                {
                    itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                }
            }
            else
            {
                IsCdFlg = false;
            }

        }

        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsCdFlg && !char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
