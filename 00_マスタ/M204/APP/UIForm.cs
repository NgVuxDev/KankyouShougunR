// $Id: UIForm.cs 54201 2015-07-01 05:06:18Z quocthang@e-mall.co.jp $
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
using Shougun.Core.Master.ContenaShuruiHoshu.Logic;
using Shougun.Core.Master.ContenaShuruiHoshu.Const;

namespace Shougun.Core.Master.ContenaShuruiHoshu.APP
{
    /// <summary>
    /// コンテナ種類画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// コンテナ種類画面ロジック
        /// </summary>
        private LogicCls logic;

        public DataTable dtDetailList = new DataTable();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        public MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        public UIForm()
            : base(WINDOW_ID.M_CONTENA_SHURUI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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
            base.OnLoad(e);
            this.logic.WindowInit();
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
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                int count = this.logic.Search();
                if (count < 0)
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

                    dtDetailList = this.logic.SearchResult.Copy();
                    this.logic.ColumnAllowDBNull();
                    return;
                }

                var table = this.logic.SearchResult;

                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                dtDetailList = this.logic.SearchResult.Copy();

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
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!base.RegistErrorFlag)
                {
                    if (this.logic.ActionBeforeCheck())
                    {
                        msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    Boolean isOK = this.logic.CreateEntity(false);
                    //if (!isOK)
                    //{
                    //    var messageShowLogic = new MessageBoxShowLogic();
                    //    messageShowLogic.MessageBoxShow("E061");
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
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!base.RegistErrorFlag)
                {
                    if (this.logic.CheckDelete())
                    {
                        bool isOK = this.logic.CreateEntity(true);
                        if (isOK)
                        {
                            this.logic.LogicalDelete();
                            bool isSelect = this.logic.isSelectFlg();
                            if (isOK && isSelect)
                            {
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
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.Cancel();
                this.Search(null, e);
                this.CONDITION_ITEM.Focus();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                
                if (this.logic.ActionBeforeCheck())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");
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
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClearCondition(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.ClearCondition();
                this.logic.GetSysInfoInit();
                this.CONDITION_ITEM.Focus();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
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

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// コンテナ種類CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!DBNull.Value.Equals(this.Ichiran.Rows[e.RowIndex].Cells["CONTENA_SHURUI_CD"].Value) &&
                !"".Equals(this.Ichiran.Rows[e.RowIndex].Cells["CONTENA_SHURUI_CD"].Value) &&
                this.Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(ConstCls.CONTENA_SHURUI_CD))
                {
                    string contena_Shurui_Cd = (string)this.Ichiran.Rows[e.RowIndex].Cells["CONTENA_SHURUI_CD"].Value;

                    bool catchErr = true;
                    bool isError = this.logic.DuplicationCheck(contena_Shurui_Cd, dtDetailList, out catchErr);

                    if (!catchErr)
                    {
                        return;
                    }
                    else if (isError)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.Ichiran.Rows[e.RowIndex].Cells["CONTENA_SHURUI_CD"], true);
                        msgLogic.MessageBoxShow("E022", "入力されたコンテナ種類CD");
                        e.Cancel = true;
                        this.Ichiran.BeginEdit(false);
                        return;
                    }

                    this.Ichiran.Rows[e.RowIndex].Cells["CONTENA_SHURUI_CD"].Value = contena_Shurui_Cd.ToUpper();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // IME制御
                switch (e.ColumnIndex)
                {
                    case 1:
                    case 6:
                    case 7:
                        this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                    case 2:
                    case 3:
                    case 5:
                        this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;
                    case 4:
                        this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Katakana;
                        break;
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
                LogUtility.DebugMethodEnd(sender, e);
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
                if (this.Ichiran.Rows[e.Row.Index].IsNewRow)
                {
                    // セルの既定値処理
                    this.Ichiran.Rows[e.Row.Index].Cells["DELETE_FLG"].Value = false;
                    this.Ichiran.Rows[e.Row.Index].Cells["CREATE_PC"].Value = "";
                    this.Ichiran.Rows[e.Row.Index].Cells["UPDATE_PC"].Value = "";
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
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
