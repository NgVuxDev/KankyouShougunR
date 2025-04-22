// $Id: F00_M224Form.cs 37912 2014-12-22 07:16:00Z fangjk@oec-h.com $
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
using r_framework.CustomControl;
using Seasar.Quill;
using Seasar.Quill.Attrs;


namespace Shougun.Core.Master.ContenaSousaHoshu
{
    /// <summary>
    /// コンテナ操作画面
    /// </summary>
    [Implementation]
    public partial class M224Form : SuperForm
    {
        /// <summary>
        /// コンテナ操作画面ロジック
        /// </summary>
        private M224Logic logic;

        bool IsCdFlg = false;
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public M224Form()
            : base(WINDOW_ID.M_CONTENA_SOUSA, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new M224Logic(this);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
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
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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

                //バインド
                var table = this.logic.SearchResult;

                table.BeginLoadData();

                //行の編集可
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

                //削除の場合、行の編集不可
                //bool delChk = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                //if (delChk)
                //{
                //    for (int k = 0; k < table.Rows .Count; k++)
                //    {
                //        for (int i = 1; i < this.Ichiran.ColumnCount; i++)
                //        {
                //            this.Ichiran.Rows [k].Cells[i].ReadOnly = true;
                //        }
                //    }
                //}     

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
                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    Boolean isOK = this.logic.CreateEntity(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!isOK)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E061");
                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    this.logic.Regist(base.RegistErrorFlag);
                    if (this.logic.isRegist)
                    {
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
                    this.logic.CreateEntity(true, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    this.logic.LogicalDelete();
                    if (this.logic.isRegist)
                    {
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
                Search(sender, e);
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
                this.logic.ClearCondition();
                //システム情報
                this.logic.GetSysInfo();
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
                Properties.Settings.Default.CONDITION_DBFIELD = this.CONDITION_DBFIELD.Text;


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
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// コンテナ操作CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["CONTENA_SOUSA_CD"].Value) &&
                    Ichiran.Rows[e.RowIndex].Cells["CONTENA_SOUSA_CD"].Value != null &&
                    !"".Equals(Ichiran.Rows[e.RowIndex].Cells["CONTENA_SOUSA_CD"].Value) &&
                    Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(ContenaSousaHoshuConstans.CONTENA_SOUSA_CD))
                {
                    string contena_Sousa_Cd = Ichiran.Rows[e.RowIndex].Cells["CONTENA_SOUSA_CD"].Value.ToString();

                    bool catchErr = true;
                    bool isError = this.logic.DuplicationCheck(contena_Sousa_Cd, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isError)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E022", "入力されたコンテナ操作CD");
                        e.Cancel = true;
                        this.Ichiran.BeginEdit(false);
                        LogUtility.DebugMethodEnd();
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
                    case "CONTENA_SOUSA_CD": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "CONTENA_SOUSA_NAME": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CONTENA_SOUSA_NAME_RYAKU": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CONTENA_SOUSA_BIKOU": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CREATE_USER": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CREATE_DATE": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "CREATE_PC": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "UPDATE_USER": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "UPDATE_DATE": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "UPDATE_PC": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "DELETE_FLG": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "TIME_STAMP": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;

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
        /// 追加行日付設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //this.logic.SetLastRowContext(false);
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
                if (Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns["CONTENA_SOUSA_CD"].Index)
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
        /// 検索条件はコンテナCDの場合入力制限
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
                    case "CONTENA_SOUSA_CD": obj.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "CONTENA_SOUSA_NAME": obj.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CONTENA_SOUSA_NAME_RYAKU": obj.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CONTENA_SOUSA_BIKOU": obj.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CREATE_USER": obj.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CREATE_DATE": obj.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "UPDATE_USER": obj.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "UPDATE_DATE": obj.ImeMode = System.Windows.Forms.ImeMode.Disable; break;

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

        // <summary>
        /// 検索条件のKeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (this.CONDITION_VALUE.DBFieldsName.Equals("CONTENA_SOUSA_CD"))
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
        /// //グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void Ichiran_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if ("".Equals(e.Value)) //空文字を入力された場合
                {
                    e.Value = System.DBNull.Value;  //AllowDBNull=trueの場合は nullはNG DBNullはOK
                    e.ParsingApplied = true; //後続の解析不要
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
        /// 一覧のチェックボックスがチェックされた場合、行の編集不可にする
        /// </summary>
        private void Ichiran_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);
            ////列
            //int colIndex = e.ColumnIndex;
            ////行
            //int rowIndex=e.RowIndex;
            ////行数
            //int colCnt = this.Ichiran.ColumnCount;

            //bool delChk = false;
            //if (rowIndex>=0)
            //{
            //    if (colIndex == 0)
            //    {
            //        delChk = (bool)this.Ichiran.Rows[rowIndex].Cells["chb_delete"].Value;

            //        if (delChk)
            //        {
            //            //行の編集が不可にする
            //            for (int i = 1; i < colCnt; i++)
            //            {
            //                this.Ichiran.Rows[rowIndex].Cells[i].ReadOnly = true;
            //            }
            //        }
            //        else
            //        {
            //            //行の編集が可にする
            //            for (int i = 1; i < colCnt; i++)
            //            {
            //                this.Ichiran.Rows[rowIndex].Cells[i].ReadOnly = false;
            //            }
            //        }
            //    }
            //}
            //LogUtility.DebugMethodEnd();
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
            this.Ichiran.CellValidating -= this.Ichiran_CellValidating;

            // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
            this.logic.SetFixedIchiran();

            this.Ichiran.CellValidating += this.Ichiran_CellValidating;
        }
    }
}
