// $Id: UIForm.cs 48317 2015-04-25 06:19:48Z minhhoang@e-mall.co.jp $
using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.ZaikoHinmeiHoshu.Logic;

namespace Shougun.Core.Master.ZaikoHinmeiHoshu.APP
{
    /// <summary>
    /// 在庫品名画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 在庫品名画面ロジック
        /// </summary>
        private LogicCls logic;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_ZAIKO_HINMEI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region イベント処理

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                if (!this.logic.WindowInit())
                {
                    return;
                }
                if (this.logic.Search() == -1)
                {
                    return;
                }
                this.logic.SetIchiran();

                // Anchorの設定は必ずOnLoadで行うこと
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
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
            try
            {
                if (this.logic.Search() == -1)
                {
                    return;
                }
                if (!this.logic.SetIchiran())
                {
                    return;
                }

                // 主キーを非活性にする
                this.logic.EditableToPrimaryKey();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
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
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!base.RegistErrorFlag)
                {
                    // 20141217 Houkakou 「在庫品名入力」の日付チェックを追加する　end
                    int count = this.logic.CreateEntity(false);
                    if (count == -1)
                    {
                        return;
                    }
                    else if (count == 0 && this.Ichiran.Rows.Count <= 1)
                    {
                        msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    this.logic.Regist(base.RegistErrorFlag);
                    if (this.logic.isRegist)
                    {
                        msgLogic.MessageBoxShow("I001", "登録");
                        if (this.logic.Search() == -1)
                        {
                            return;
                        }
                        if (!this.logic.SetIchiran())
                        {
                            return;
                        }
                    }
                }

                // 主キーを非活性にする
                this.logic.EditableToPrimaryKey();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
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
            try
            {
                if (!base.RegistErrorFlag)
                {
                    if (this.logic.CheckDelete())
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        int count = this.logic.CreateEntity(true);
                        if (count == -1)
                        {
                            return;
                        }
                        else if (count == 0)
                        {
                            msgLogic.MessageBoxShow("E061");
                            return;
                        }
                        var result = msgLogic.MessageBoxShow("C021");
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                        this.logic.LogicalDelete();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
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
                //条件初期化
                this.logic.ClearCondition();
                // システム情報を取得し、初期値をセットする
                if (!this.logic.GetSysInfoInit())
                {
                    return;
                }
                //データ取得
                if (this.logic.Search() == -1)
                {
                    return;
                }
                //データ設定
                this.logic.SetIchiran();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
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
                if (this.Ichiran.Rows.Count <= 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");
                    return;
                }
                this.logic.CSVOutput();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
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
                this.logic.ClearCondition();

                // システム情報を取得し、初期値をセットする
                if (!this.logic.GetSysInfoInit())
                {
                    return;
                }

                this.CONDITION_ITEM.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_DBFIELD.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_DBFIELD.ItemDefinedTypes;

                Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                this.Ichiran.CellValidating -= this.Ichiran_CellValidating;
                var parentForm = (MasterBaseForm)this.Parent;
                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        #endregion

        #region GridView処理

        /// <summary>
        /// 在庫品名CDの重複チェックとCombox処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var row = this.Ichiran.Rows[e.RowIndex];
            var colName = this.Ichiran.Columns[e.ColumnIndex].Name;
            var cellValue = this.GetCellValue(row, colName);

            if (!string.IsNullOrEmpty(cellValue))
            {
                if (colName.Equals(ZAIKO_HINMEI_CD.Name))
                {
                    string zaikoHinmeiCd = cellValue.PadLeft(6, '0').ToUpper();
                    bool catchErr = true;
                    bool isOk = this.logic.ZaikoHinmeiCdCheck(zaikoHinmeiCd, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isOk)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        // 20150421 在庫品名CD重複チェックメッセージ修正(有価在庫不具合一覧68) Start
                        //msgLogic.MessageBoxShow("E022", "入力された在庫品名CD");
                        msgLogic.MessageBoxShow("E003", "在庫品名CD", zaikoHinmeiCd);
                        // 20150421 在庫品名CD重複チェックメッセージ修正(有価在庫不具合一覧68) End
                        this.Ichiran.CurrentCell = row.Cells[colName];
                        e.Cancel = true;
                        this.Ichiran.BeginEdit(false);
                        return;
                    }

                    this.SetCellValue(row, colName, zaikoHinmeiCd.ToUpper());
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
            DataGridView dgv = (DataGridView)sender;
            string name = this.Ichiran.Columns[e.ColumnIndex].DataPropertyName;
            switch (name)
            {
                case "ZAIKO_HINMEI_NAME":
                case "ZAIKO_HINMEI_NAME_RYAKU":
                case "BIKOU":
                    dgv.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                    break;

                case "ZAIKO_HINMEI_FURIGANA":
                    dgv.ImeMode = System.Windows.Forms.ImeMode.Katakana;
                    break;

                default:
                    dgv.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
            }

            this.logic.IchiranCellEnter(e);
        }

        /// <summary>
        /// セルのデータ取得
        /// </summary>
        /// <param name="grdRow"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetCellValue(DataGridViewRow grdRow, string columnName)
        {
            if (grdRow != null && grdRow.Cells[columnName] != null && grdRow.Cells[columnName].Value != null)
            {
                return grdRow.Cells[columnName].Value.ToString().Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// セルのデータ設定
        /// </summary>
        /// <param name="grdRow"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public void SetCellValue(DataGridViewRow grdRow, string columnName, object value)
        {
            if (grdRow != null && grdRow.Cells[columnName] != null)
            {
                grdRow.Cells[columnName].Value = value;
            }
        }

        #region 明細一覧のcellを結合する

        /// <summary>
        /// カラムサイズ変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.Ichiran.Refresh();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void Ichiran_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if ("".Equals(e.Value)) //空文字を入力された場合
            {
                e.Value = System.DBNull.Value; // AllowDBNull=trueの場合は nullはNG DBNullはOK
                e.ParsingApplied = true; //後続の解析不要
            }
        }

        #endregion

        #endregion

        #region 検索条件処理

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_Validated(object sender, EventArgs e)
        {
            TextBox obj = (TextBox)sender;
            // 基準単価
            if (!string.IsNullOrEmpty(obj.Text) &&
                (this.CONDITION_DBFIELD.DBFieldsName.Equals("ZAIKO_TANKA")
                ))
            {
                try
                {
                    Decimal.Parse(obj.Text);
                }
                catch
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E048", "数字");
                    obj.Text = string.Empty;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            TextBox obj = (TextBox)sender;
            // 基準単価
            if (this.CONDITION_DBFIELD.DBFieldsName.Equals("ZAIKO_TANKA") ||
                this.CONDITION_DBFIELD.DBFieldsName.Equals("ZAIKO_HINMEI_CD") ||
                this.CONDITION_DBFIELD.DBFieldsName.Equals("UPDATE_DATE") ||
                this.CONDITION_DBFIELD.DBFieldsName.Equals("CREATE_DATE") ||
                this.CONDITION_DBFIELD.DBFieldsName.Equals("DELETE_FLG")
                )
            {
                obj.ImeMode = System.Windows.Forms.ImeMode.Disable;
            }
            else if (this.CONDITION_DBFIELD.DBFieldsName.Equals("ZAIKO_HINMEI_FURIGANA"))
            {
                obj.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            }
            else
            {
                obj.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            }
        }

        /// <summary>
        /// CONDITION_VALUE_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.CONDITION_DBFIELD.DBFieldsName.Equals("ZAIKO_TANKA") ||
                this.CONDITION_DBFIELD.DBFieldsName.Equals("DELETE_FLG"))
            {
                if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void PopupAfter()
        {
            this.CONDITION_VALUE.Text = string.Empty;
        }

        #endregion

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