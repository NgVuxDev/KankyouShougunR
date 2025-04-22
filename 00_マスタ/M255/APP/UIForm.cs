// $Id: UIForm.cs 38032 2014-12-23 07:44:08Z fangjk@oec-h.com $
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
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.ManiFestTeHaiHoshu.Logic;
using r_framework.Utility;
using r_framework.CustomControl.DataGridCustomControl;

namespace Shougun.Core.Master.ManiFestTeHaiHoshu.APP
{
    /// <summary>
    /// マニフェスト手配入力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// マニフェスト手配入力画面ロジック
        /// </summary>
        private LogicCls logic;

        bool IsCdFlg = false;

        public MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        public UIForm()
            : base(WINDOW_ID.M_MANIFEST_TEHAI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);
            
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            try
            {
                base.OnLoad(e);
                if (!this.logic.WindowInit())
                {
                    return;
                }
                this.Search(null, e);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                int count = this.logic.Search();
                if (count == -1)
                {
                    return;
                }
                else if (count == 0)
                {
                    //var messageShowLogic = new MessageBoxShowLogic();
                    //messageShowLogic.MessageBoxShow("C001");
                    this.logic.SearchResult.Rows.Clear();
                    this.Ichiran.CellValidating -= Ichiran_CellValidating;
                    this.Ichiran.DataSource = this.logic.SearchResult;
                    this.Ichiran.CellValidating += Ichiran_CellValidating;

                    this.logic.ColumnAllowDBNull();
                    return;
                }

                var table = this.logic.SearchResult;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.Ichiran.CellValidating -= Ichiran_CellValidating;

                this.Ichiran.DataSource = table;

                // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
                this.logic.SetFixedIchiran();

                this.Ichiran.CellValidating += Ichiran_CellValidating;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!base.RegistErrorFlag)
                {
                    bool catchErr = true;
                   
                    if (this.logic.ActionBeforeCheck())
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    Boolean isOK = this.logic.CreateEntity(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    //if (!isOK)
                    //{
                    //    msgLogic.MessageBoxShow("E061");
                    //    return;
                    //}
                    bool ret = this.logic.RegistData(base.RegistErrorFlag);

                    if (ret)
                    {
                        msgLogic.MessageBoxShow("I001", "登録");
                        this.Search(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!base.RegistErrorFlag)
                {
                    bool catchErr = true;
                   
                    Boolean isOK = this.logic.CreateEntity(true, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isOK)
                    {
                        if (this.logic.CheckDelete())
                        {
                            this.logic.LogicalDelete();
                            this.Search(sender, e);
                        }
                    }
                    else 
                    {
                        msgLogic.MessageBoxShow("E075");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                this.logic.Cancel();
                this.Search(null, e);
                this.CONDITION_ITEM.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (this.logic.ActionBeforeCheck())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");
                    return;
                }
                this.logic.CSVOutput();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClearCondition(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                this.logic.ClearCondition();
                if (!this.logic.GetSysInfoInit())
                {
                    return;
                }
                this.CONDITION_ITEM.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                this.Ichiran.CellValidating -= Ichiran_CellValidating;

                var parentForm = (MasterBaseForm)this.Parent;
                Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// マニフェストCDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["MANIFEST_TEHAI_CD"].Value) &&
                Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(Const.ConstCls.MANIFEST_TEHAI_CD))
                {
                    string manifest_Tehai_Cd = Ichiran.Rows[e.RowIndex].Cells["MANIFEST_TEHAI_CD"].Value.ToString();

                    bool catchErr = true;
                    bool isError = this.logic.DuplicationCheck(manifest_Tehai_Cd, out catchErr);

                    if (!catchErr)
                    {
                        return;
                    }
                    if (isError)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.Ichiran.Rows[e.RowIndex].Cells["MANIFEST_TEHAI_CD"], true);
                        msgLogic.MessageBoxShow("E022", "入力されたマニフェスト手配CD");
                        e.Cancel = true;
                        this.Ichiran.BeginEdit(false);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // IME制御
                switch (e.ColumnIndex)
                {
                    case 1:
                    case 5:
                    case 6:
                        Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                    case 2:
                    case 3:
                    case 4:
                        Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;
                }

                if (this.Ichiran.Rows[e.RowIndex].IsNewRow || this.logic.CheckFixedRow(this.Ichiran.Rows[e.RowIndex]))
                {
                    // 新規行の場合には削除チェックさせない
                    this.Ichiran.Rows[e.RowIndex].Cells["chb_delete"].ReadOnly = true;
                }
                else
                {
                    this.Ichiran.Rows[e.RowIndex].Cells["chb_delete"].ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コンテナCD入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns["MANIFEST_TEHAI_CD"].Index)
                {
                    TextBox itemID = e.Control as TextBox;
                    if (itemID != null)
                    {
                        IsCdFlg = true;
                        itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                    }
                }
                else 
                {
                    IsCdFlg = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// itemID_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsCdFlg && !char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// CONDITION_VALUE_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("MANIFEST_TEHAI_CD".Equals(this.CONDITION_VALUE.DBFieldsName))
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// 検索条件IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            if ("DELETE_FLG".Equals(this.CONDITION_VALUE.DBFieldsName))
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.Disable;
            }
        }

        /// <summary>
        /// 検索値クリア
        /// </summary>
        public void clearConditionValue()
        {
            this.CONDITION_VALUE.Text = string.Empty;
        }

        /// <summary>
        /// //グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void Ichiran_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if ("".Equals(e.Value)) //空文字を入力された場合
            {
                e.Value = System.DBNull.Value;  //AllowDBNull=trueの場合は nullはNG DBNullはOK
                e.ParsingApplied = true; //後続の解析不要
            }
        }

        /// <summary>
        /// 新追加行のセル既定値処理
        /// </summary>
        private void Ichiran_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (Ichiran.Rows[e.Row.Index].IsNewRow)
                {
                    // セルの既定値処理
                    Ichiran.Rows[e.Row.Index].Cells["DELETE_FLG"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["CREATE_PC"].Value = "";
                    Ichiran.Rows[e.Row.Index].Cells["UPDATE_PC"].Value = "";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 初期表示時処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            base.OnShown(e);
            this.Ichiran.CellValidating -= this.Ichiran_CellValidating;

            // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
            this.logic.SetFixedIchiran();

            this.Ichiran.CellValidating += this.Ichiran_CellValidating;
        }
    }
}
