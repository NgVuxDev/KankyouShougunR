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
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.Const;
using Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.Logic;

namespace Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.APP
{
    /// <summary>
    /// 重要度入力
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 重要度入力画面ロジック
        /// </summary>
        private LogicCls logic;

        public DataTable dtDetailList = new DataTable();

        public MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_DENSHI_SHINSEI_JYUYOUDO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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

                // 検索条件入力チェック
                if (!this.logic.CheckSearchString())
                {
                    msgLogic.MessageBoxShow("E084", CONDITION_ITEM.Text);
                    CONDITION_VALUE.Focus();
                    return;
                }

                int count = this.logic.Search();
                if (count == -1)
                {
                    return;
                }
                else if (count == 0)
                {
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

                    //ThangNguyen [Delete] 20150828 #12324 Start
                    /*if (this.logic.UniqueCheck() == 0)
                    {
                        msgLogic.MessageBoxShow("E001", "申請初期値");
                        return;
                    }*/
                    //ThangNguyen [Delete] 20150828 #12324 End

                    bool catchErr = true;
                    this.logic.CreateEntity(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

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
                    bool catchErr = true;
                    bool isOK = this.logic.CreateEntity(true, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isOK)
                    {
                        this.logic.LogicalDelete();
                        if (this.logic.isRegist)
                        {
                            bool isSelect = this.logic.isSelectFlg();
                            if (isOK && isSelect)
                            {
                                this.Search(sender, e);
                            }
                        }
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E075");
                        return;
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
                if (!this.logic.GetSysInfoInit())
                {
                    return;
                }
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
        /// 明細欄のセルバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.ColumnIndex == this.Ichiran.Columns[ConstCls.JYUYOUDO_CD].Index)
                {
                    string targetColumnName = this.Ichiran.Columns[e.ColumnIndex].DataPropertyName;
                    switch (targetColumnName)
                    {
                        //重要度CD
                        case ("JYUYOUDO_CD"):
                            if (!string.IsNullOrEmpty(this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.JYUYOUDO_CD].Value.ToString()))
                            {
                                string jyuyoudo_Cd = this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.JYUYOUDO_CD].Value.ToString();
                                jyuyoudo_Cd = jyuyoudo_Cd.PadLeft(2, '0');

                                // 重要度CDの重複チェック
                                bool catchErr = true;
                                bool isError = this.logic.DuplicationCheck(jyuyoudo_Cd, dtDetailList, out catchErr);
                                if (!catchErr)
                                {
                                    return;
                                }

                                if (isError)
                                {
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    ControlUtility.SetInputErrorOccuredForDgvCell(this.Ichiran.Rows[e.RowIndex].Cells[ConstCls.JYUYOUDO_CD], true);
                                    msgLogic.MessageBoxShow("E022", "入力された重要度CD");
                                    e.Cancel = true;
                                    this.Ichiran.BeginEdit(false);
                                    return;
                                }
                                Ichiran.Rows[e.RowIndex].Cells[ConstCls.JYUYOUDO_CD].Value = jyuyoudo_Cd.ToUpper();
                            }
                            break;
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
        /// 明細欄のValueChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 申請初期値
            if (this.Ichiran.Columns[e.ColumnIndex].Name == ConstCls.JYUYOUDO_DEFAULT)
            {
                if (this.Ichiran.CurrentRow != null)
                {
                    if (this.logic.isCheck)
                    {
                        bool isError = false;
                        // 申請初期値の一意チェック
                        int count = this.logic.UniqueCheck();
                        if (count == -1)
                        {
                            return;
                        }
                        if (count >= 2)
                        {
                            isError = true;
                        }

                        if (isError)
                        {
                            this.logic.isCheck = false;

                            foreach (DataGridViewRow row in this.Ichiran.Rows)
                            {
                                if (row.Cells[ConstCls.JYUYOUDO_DEFAULT].Value!= null &&
                                    row.Cells[ConstCls.JYUYOUDO_DEFAULT].Value.Equals(true) &&
                                    e.RowIndex != row.Index)
                                {
                                    row.Cells[ConstCls.JYUYOUDO_DEFAULT].Value = false;
                                }
                            }

                            this.logic.isCheck = true;

                            this.Ichiran.BeginEdit(false);
                            this.Ichiran.RefreshEdit();

                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 明細欄CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // IME制御
                var name = this.Ichiran.Columns[e.ColumnIndex].Name;
                if ("JYUYOUDO_NAME".Equals(name))
                {
                    this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                }
                else
                {
                    this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
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
        /// 明細欄のCurrentCellDirtyStateChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //if (this.Ichiran.IsCurrentCellDirty)
            //{
            //    var colName = this.Ichiran.Columns[this.Ichiran.CurrentCell.ColumnIndex].Name;
            //    switch(colName)
            //    {
            //        case "chb_delete":
            //        case "JYUYOUDO_DEFAULT":
            //            // 明細欄のチェックボックスでCheckedChangedイベントがないので、
            //            // CellValueChangedを同様の振る舞いにさせるための対応
            //            this.Ichiran.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //            break;
            //    }
            //}
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
