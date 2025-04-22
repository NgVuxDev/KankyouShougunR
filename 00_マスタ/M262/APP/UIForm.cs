// $Id: UIForm.cs 56609 2015-07-24 01:54:30Z minhhoang@e-mall.co.jp $
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
using Shougun.Core.Master.ShikuchousonHoshu.Logic;
using Shougun.Core.Master.ShikuchousonHoshu.Const;

namespace Shougun.Core.Master.ShikuchousonHoshu.APP
{
    /// <summary>
    /// 市区町村画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 市区町村画面ロジック
        /// </summary>
        private LogicCls logic;

        bool IsCdFlg = false;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        public UIForm()
            : base(WINDOW_ID.M_SHIKUCHOUSON, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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

            if (!this.InitialFlg)
            {
                this.Height -= 7;
                this.InitialFlg = true;
            }
            base.OnShown(e);
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
                this.Ichiran.CellValidating += Ichiran_CellValidating;

                // 主キーを非活性にする
                this.logic.EditableToPrimaryKey();
            }
            catch
            {
                throw;
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

                    Boolean isCheckOK = this.logic.CheckBeforeUpdate();
                    if (!isCheckOK)
                    {
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
                        return;
                    }
                    this.logic.Regist(base.RegistErrorFlag);
                    this.Search(sender, e);
                }
            }
            catch
            {
                throw;
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
                    if (this.logic.CheckDelete())
                    {
                        bool catchErr = true;
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

                        this.logic.CreateEntity(true, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        this.logic.LogicalDelete();
                        this.Search(sender, e);
                    }
                }
            }
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
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
                if (!this.logic.Cancel())
                {
                    return;
                }
                //this.logic.ClearCondition();
                this.CONDITION_ITEM.Focus();
            }
            catch
            {
                throw;
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
            catch
            {
                throw;
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
                    Ichiran.Rows[e.Row.Index].Cells["UPDATE_DATE"].Value = DBNull.Value;
                    Ichiran.Rows[e.Row.Index].Cells["CREATE_DATE"].Value = DBNull.Value;
                    Ichiran.Rows[e.Row.Index].Cells["DELETE_FLG"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["CREATE_PC"].Value = "";
                    Ichiran.Rows[e.Row.Index].Cells["UPDATE_PC"].Value = "";
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 市区町村CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // キー重複チェック処理
                if (!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["SHIKUCHOUSON_CD"].Value) &&
                    !"".Equals(Ichiran.Rows[e.RowIndex].Cells["SHIKUCHOUSON_CD"].Value) &&
                    Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(ConstCls.SHIKUCHOUSON_CD))
                {
                    string contena_Joukyou_Cd = (string)Ichiran.Rows[e.RowIndex].Cells["SHIKUCHOUSON_CD"].Value;
                    contena_Joukyou_Cd = contena_Joukyou_Cd.PadLeft(3, '0');
                    bool catchErr = true;
                    bool isError = this.logic.DuplicationCheck(contena_Joukyou_Cd, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isError)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E022", "入力された市区町村CD");
                        e.Cancel = true;
                        this.Ichiran.BeginEdit(false);
                        return;
                    }

                    Ichiran.Rows[e.RowIndex].Cells["SHIKUCHOUSON_CD"].Value = contena_Joukyou_Cd.ToUpper();
                }
            }
            catch
            {
                throw;
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
                // readonlyセルは飛ばす
                var cell = this.Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex];

                //if (cell != null && cell.ReadOnly)
                //{
                    //SendKeys.Send("{TAB}");
                //}

                string columName = Ichiran.Columns[e.ColumnIndex].Name;
                switch (columName)
                {
                    case "SHIKUCHOUSON_CD": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "SHIKUCHOUSON_NAME": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "SHIKUCHOUSON_NAME_RYAKU": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "SHIKUCHOUSON_FURIGANA": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Katakana; break;
                    case "SHIKUCHOUSON_BIKOU": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
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

                // 新規行の場合には削除チェックさせない
                this.Ichiran.Rows[e.RowIndex].Cells["chb_delete"].ReadOnly = this.Ichiran.Rows[e.RowIndex].IsNewRow;
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 市区町村CD入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns["SHIKUCHOUSON_CD"].Index)
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
                     && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch
            {
                throw;
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

            this.CONDITION_VALUE.ImeMode = this.CONDITION_VALUE.ImeMode;

            LogUtility.DebugMethodEnd();
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
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
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