// $Id: UIForm.cs 36558 2014-12-05 01:19:56Z fangjk@oec-h.com $
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
using r_framework.Utility;
using r_framework.Entity;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.ContenaJoukyouHoshu.Logic;
using Shougun.Core.Master.ContenaJoukyouHoshu.Const;

namespace Shougun.Core.Master.ContenaJoukyouHoshu.APP
{
    /// <summary>
    /// コンテナ状況画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// コンテナ状況画面ロジック
        /// </summary>
        private LogicCls logic;

        bool IsCdFlg = false;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public UIForm()
            : base(WINDOW_ID.M_CONTENA_JOUKYOU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            LogUtility.DebugMethodEnd();
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

                //フォーカス
                //this.CONDITION_VALUE.Focus();

				// Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
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
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                int result = this.logic.Search();

                if (result == -1)
                {
                    return;
                }
                else if (result == 0)
                {
                    //既存の検索結果を初期化
                    this.logic.SearchResult.Clear();

                    //var messageShowLogic = new MessageBoxShowLogic();
                    //messageShowLogic.MessageBoxShow("C001");
                    //バインド前にチェックをしない
                    this.Ichiran.CellValidating -= Ichiran_CellValidating;

                    this.Ichiran.DataSource = this.logic.SearchResult;

                    //バインド後にチェックを追加
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

                //バインド前にチェックをしない
                this.Ichiran.CellValidating -= Ichiran_CellValidating;

                this.Ichiran.DataSource = table;

                // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
                this.logic.SetFixedIchiran();

                //バインド後にチェックを追加
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
                    bool catchErr = true;
                    Boolean isOK = this.logic.CreateEntity(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
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

                    bool catchErr = true;
                    this.logic.CreateEntity(true, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (this.logic.CheckDelete())
                    {
                        this.logic.LogicalDelete();
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
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.logic.Cancel())
                {
                    return;
                }
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

                    LogUtility.DebugMethodEnd();
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
                //this.logic.ClearCondition();
                if (!this.logic.Cancel())
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
                var parentForm = (MasterBaseForm)this.Parent;

                Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                //チェックを外す
                this.Ichiran.CellValidating -= Ichiran_CellValidating;

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
        /// コンテナ状況CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // キー重複チェック処理
                if (!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["CONTENA_JOUKYOU_CD"].Value) &&
                    !"".Equals(Ichiran.Rows[e.RowIndex].Cells["CONTENA_JOUKYOU_CD"].Value) &&
                    Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(ConstCls.CONTENA_JOUKYOU_CD))
                {
                    string contena_Joukyou_Cd = Ichiran.Rows[e.RowIndex].Cells["CONTENA_JOUKYOU_CD"].Value.ToString();

                    bool catchErr = true;
                    bool isError = this.logic.DuplicationCheck(contena_Joukyou_Cd, out catchErr);

                    if (!catchErr)
                    {
                        return;
                    }
                    if (isError)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E022", "入力されたコンテナ状況CD");
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
                string columName = Ichiran.Columns[e.ColumnIndex].Name;
                switch (columName)
                {
                    case "CONTENA_JOUKYOU_CD": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "CONTENA_JOUKYOU_NAME": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CONTENA_JOUKYOU_NAME_RYAKU": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CONTENA_JOUKYOU_BIKOU": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CREATE_USER": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "CREATE_DATE": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "CREATE_PC": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "UPDATE_USER": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "UPDATE_DATE": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "UPDATE_PC": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "DELETE_FLG": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "TIME_STAMP": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    //default: Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
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
        /// コンテナ状況CD入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns["CONTENA_JOUKYOU_CD"].Index)
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
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (IsCdFlg && !char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
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
        /// ポップアップ画面から戻って、フォーカスの設定
        /// </summary>
        public void SetFocus()
        {
            //LogUtility.DebugMethodStart();

            //this.CONDITION_VALUE.Focus();

            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CONDITION_VALUE_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                
                if ("CONTENA_JOUKYOU_CD".Equals(this.CONDITION_VALUE.DBFieldsName))
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
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
        /// CONDITION_VALUE_Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                TextBox obj = (TextBox)sender;

                string name = this.CONDITION_VALUE.DBFieldsName;

                switch (name)
                {
                    case "CONTENA_JOUKYOU_CD":
                    case "UPDATE_DATE":
                    case "CREATE_DATE":
                        obj.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                    case "CONTENA_JOUKYOU_NAME":
                    case "UPDATE_USER":
                    case "CREATE_USER":
                    case "CONTENA_JOUKYOU_NAME_RYAKU":
                        obj.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;

                    case "CONTENA_JOUKYOU_FURIGANA":
                        obj.ImeMode = System.Windows.Forms.ImeMode.Katakana;
                        break;

                    case "CONTENA_JOUKYOU_BIKOU":
                        obj.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;

                    default:
                        obj.ImeMode = System.Windows.Forms.ImeMode.NoControl;

                        break;
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
        /// CELLの編集モード終了イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">CELL情報</param>
        private void Ichiran_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //// CELL毎回移動で呼出すため、ログはいらない
            //if (e.RowIndex == (Ichiran.Rows.Count - 1 - 1))
            //{
            //    // 追加された新行の前行(新行CELLにデータ入力すると、新行が追加される)
            //    bool notEdited = string.IsNullOrEmpty(Ichiran.Rows[e.RowIndex].Cells["UPDATE_DATE"].Value.ToString());
            //    if (notEdited)
            //    {
            //        // 行は未初期化
            //        bool noInput_CONTENA_JOUKYOU_CD = string.IsNullOrEmpty(Ichiran.Rows[e.RowIndex].Cells["CONTENA_JOUKYOU_CD"].Value.ToString());
            //        bool noInput_CONTENA_JOUKYOU_NAME = string.IsNullOrEmpty(Ichiran.Rows[e.RowIndex].Cells["CONTENA_JOUKYOU_NAME"].Value.ToString());
            //        bool noInput_CONTENA_JOUKYOU_NAME_RYAKU = string.IsNullOrEmpty(Ichiran.Rows[e.RowIndex].Cells["CONTENA_JOUKYOU_NAME_RYAKU"].Value.ToString());
            //        bool noInput_CONTENA_JOUKYOU_BIKOU = string.IsNullOrEmpty(Ichiran.Rows[e.RowIndex].Cells["CONTENA_JOUKYOU_BIKOU"].Value.ToString());
            //        if (!noInput_CONTENA_JOUKYOU_CD || !noInput_CONTENA_JOUKYOU_NAME || !noInput_CONTENA_JOUKYOU_NAME_RYAKU
            //            || !noInput_CONTENA_JOUKYOU_BIKOU)
            //        {
            //            // 行に入力あり
            //            Ichiran.Rows[e.RowIndex].Cells["UPDATE_DATE"].Value = DateTime.Now;
            //            Ichiran.Rows[e.RowIndex].Cells["CREATE_DATE"].Value = DateTime.Now;
            //            Ichiran.Rows[e.RowIndex].Cells["DELETE_FLG"].Value = false;
            //            Ichiran.Rows[e.RowIndex].Cells["CREATE_PC"].Value = "";
            //            Ichiran.Rows[e.RowIndex].Cells["UPDATE_PC"].Value = "";
            //        }

            //    }
            //}

        }

        /// <summary>
        /// 新追加行のセル既定値処理
        /// </summary>
        private void Ichiran_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (Ichiran.Rows[e.Row.Index].IsNewRow)
                {
                    // セルの既定値処理
                    Ichiran.Rows[e.Row.Index].Cells["UPDATE_DATE"].Value = DateTime.Now;
                    Ichiran.Rows[e.Row.Index].Cells["CREATE_DATE"].Value = DateTime.Now;
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
            LogUtility.DebugMethodEnd();
        }

        ///// <summary>
        ///// 検索値クリア
        ///// </summary>
        //public void clearConditionValue()
        //{
        //    LogUtility.DebugMethodStart();

        //    this.CONDITION_VALUE.Text = string.Empty;

        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// 初期表示時処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
            this.Ichiran.CellValidating -= this.Ichiran_CellValidating;

            // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
            this.logic.SetFixedIchiran();

            this.Ichiran.CellValidating += this.Ichiran_CellValidating;
        }
    }
}
