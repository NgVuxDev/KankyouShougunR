// $Id: UIForm.cs 50565 2015-05-25 09:48:18Z wuq@oec-h.com $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.ZaikoHiritsuHoshu.Logic;
using Shougun.Core.Master.ZaikoHiritsuHoshu.DTO;

namespace Shougun.Core.Master.ZaikoHiritsuHoshu.APP
{
    /// <summary>
    /// メニュー権限保守画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;
        internal bool isError = false;
        internal string preValue = string.Empty;

        #region 画面初期処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_ZAIKO_HIRITSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
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
            try
            {
                LogUtility.DebugMethodStart(e);
                base.OnLoad(e);
                if (!this.logic.WindowInit())
                {
                    return;
                }
                this.logic.Search();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
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
        /// 検索値クリア
        /// </summary>
        public void ClearConditionValue()
        {
            // 品名CD、表示条件の初期化必要？
        }

        #endregion

        #region Founction処理

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
                this.logic.CSVOutput();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
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
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.ClearCondition();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                throw;
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
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.HINMEI_CD.Text == null || this.HINMEI_CD.Text == "")
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "品名");
                    this.HINMEI_CD.Focus();
                }
                else
                {
                    this.Ichiran.AllowUserToAddRows = true;
                    if (this.logic.Search() == -1)
                    {
                        return;
                    }
                    this.Ichiran.Focus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
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
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!base.RegistErrorFlag)
                {
                    try
                    {
                        this.logic.Regist(base.RegistErrorFlag);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.Cancel();
                this.HINMEI_CD.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.Ichiran.CellValidating -= Ichiran_CellValidating;

                var parentForm = (MasterBaseForm)this.Parent;

                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
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
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DataTable dtIchiran = this.Ichiran.DataSource as DataTable;
                    //DataTable delDt = dtIchiran.Clone();
                    bool catchErr = true;
                    List<DataDto> listDelete = this.logic.CreateEntityData(dtIchiran, out catchErr, true);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (listDelete.Count <= 0)
                    {
                        msgLogic.MessageBoxShow("E061");
                        return;
                    }
                    var result = msgLogic.MessageBoxShow("C021");
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    this.logic.LogicalDelete(listDelete);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// [1]出荷押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void process1Click(object sender, EventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            DialogResult ret = msgLogic.MessageBoxShow("C088");
            if (ret == DialogResult.Yes)
            {
                if (this.DENSHU_KBN_CD.Text == "1")
                {
                    this.logic.HeaderInit(2);
                }
                else
                {
                    this.logic.HeaderInit(1);
                }

                //[検索条件]は全てクリア
                this.HINMEI_CD.Text = string.Empty;
                this.HINMEI_NAME_RYAKU.Text = string.Empty;
                //明細部クリア
                this.logic.CreateNoDataRecord();
            }
        }

        #endregion

        #region コントロールイベント
        /// <summary>
        /// 明細一覧のcellを結合する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart();
                this.logic.ChangeCell(sender, e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellPainting", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);
                var msgLogic = new MessageBoxShowLogic();
                //在庫品名CD入力チェック
                if (e.ColumnIndex == this.Ichiran.Columns["ZAIKO_HINMEI_CD"].Index)
                {
                    var value = Convert.ToString(Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value);
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (this.preValue == value && !this.isError)
                        {
                            return;
                        }

                        this.isError = false;
                        string zaikoHinmeiCd = Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                        Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value = zaikoHinmeiCd;
                        //存在チェック
                        if (!this.logic.MandatoryCheck(zaikoHinmeiCd))
                        {
                            e.Cancel = true;
                            this.isError = true;
                            //this.Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value = string.Empty;
                            this.Ichiran.BeginEdit(false);
                            return;
                        }
                        else
                        {
                            Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_NAME"].Value
                                = this.logic.SearchZaikoHinmei.Rows[0]["ZAIKO_HINMEI_NAME"];
                        }
                    }
                    else
                    {
                        Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_NAME"].Value = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellValidating", ex);
                throw;
            }
            finally
            {
                // LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 在庫品名CD入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender,e);
                if (Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns["ZAIKO_HINMEI_CD"].Index)
                {
                    TextBox itemID = e.Control as TextBox;
                    if (itemID != null)
                    {
                        itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_EditingControlShowing", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// セルEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender,e);
                string name = this.Ichiran.Columns[e.ColumnIndex].DataPropertyName;
                // IME制御
                switch (name)
                {
                    case "ZAIKO_HINMEI_CD":
                        this.preValue = Convert.ToString(this.Ichiran.CurrentCell.Value);
                        this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                    case "BIKOU":
                        this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;

                    default:
                        this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                }
                // 新規行の場合には削除チェックさせない
                this.Ichiran[0, e.RowIndex].ReadOnly = this.Ichiran.Rows[e.RowIndex].IsNewRow;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellEnter", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void Ichiran_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender,e);
                if ("".Equals(e.Value)) //空文字を入力された場合
                {
                    e.Value = System.DBNull.Value;  //AllowDBNull=trueの場合は nullはNG DBNullはOK
                    e.ParsingApplied = true; //後続の解析不要
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellParsing", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// グリッドのWidthを変更した場合に画面をリフレッシュ
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e"></param>
        private void Ichiran_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                this.Ichiran.Refresh();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 数字入力制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender,e);
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("itemID_KeyPress", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        /// <summary>
        /// 品名入力制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HINMEI_CD_Validating(object sender, CancelEventArgs e)
        {
            this.logic.HinmeiCdValidating(e);
        }

        // 20150424 品名入力変更する時一覧がクリアされるように Start
        /// <summary>
        /// 品名入力変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HINMEI_CD_TextChanged(object sender, EventArgs e)
        {
            this.logic.HinmeiCdTextChanged(e);
        }

        // 20150424 品名入力変更する時一覧がクリアされるように End
    }
}