
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
using Shougun.Core.Master.KaishiZaikoJouhouHoshu.Logic;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.KaishiZaikoJouhouHoshu.APP
{
    /// <summary>
    /// 開始在庫情報入力
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        private string prevZaikoHinmeiCd = string.Empty;
        private bool isErrorIchiran = false;
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        #region 画面初期処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_KAISHI_ZAIKO_INFO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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
            this.CONDITION_VALUE.Text = string.Empty;
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
                if (this.GYOUSHA_CD.Text == null || this.GYOUSHA_CD.Text == "")
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "業者");
                    this.GYOUSHA_CD.Focus();
                    return;
                }
                if (this.GENBA_CD.Text == null || this.GENBA_CD.Text == "")
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "現場");
                    this.GENBA_CD.Focus();
                    return;
                }
                if (this.logic.Search() == -1)
                {
                    return;
                }
                this.Ichiran.Focus();

                // 主キーを非活性にする
                this.logic.EditableToPrimaryKey();
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
                    //品名読込みモード時
                    if (this.logic.isNowLoadingZaikoHinmeiMaster)
                    {
                        this.logic.LogicalDeleteForZaikoHinmei();

                        this.logic.Regist(base.RegistErrorFlag);
                        if (this.logic.isRegist)
                        {
                            this.Search(sender, e);
                            this.logic.ResetZaikoHinmeiLoad();//リセット
                        }
                    }
                    else
                    {
                        this.logic.Regist(base.RegistErrorFlag);
                        if (this.logic.isRegist)
                        {
                            this.Search(sender, e);
                        }
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
                if (!this.logic.Cancel())
                {
                    return;
                }
                this.GYOUSHA_CD.Focus();
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

                this.logic.SaveDefaultCondition();

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
                if (this.logic.isNowLoadingZaikoHinmeiMaster)
                {
                    if (!this.logic.DeleteForZaikoHinmeiLoading())
                    {
                        return;
                    }
                    //this.Ichiran.DataSource = this.logic.SearchResult;
                }
                else
                {
                    if (!base.RegistErrorFlag)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        DataTable dtIchiran = this.Ichiran.DataSource as DataTable;

                        List<M_KAISHI_ZAIKO_INFO> listDelete = this.logic.CreateEntityData(dtIchiran, true);
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

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }


        #endregion

        #region コントロールイベント
        /// <summary>
        /// CONDITION_VALUE_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.CONDITION_VALUE.CharactersNumber = 40;
            this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            if (this.CONDITION_VALUE.DBFieldsName.Equals(Const.Constans.KAISHI_ZAIKO_KINGAKU) || this.CONDITION_VALUE.DBFieldsName.Equals(Const.Constans.KAISHI_ZAIKO_RYOU) || this.CONDITION_VALUE.DBFieldsName.Equals(Const.Constans.KAISHI_ZAIKO_TANKA))
            {
                this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Disable;

                this.CONDITION_VALUE.CharactersNumber = 13;
                if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Tab && e.KeyChar != (char)Keys.Back)
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
            //if ("TEKIYOU_BEGIN".Equals(this.CONDITION_VALUE.DBFieldsName) ||
            //    "TEKIYOU_END".Equals(this.CONDITION_VALUE.DBFieldsName) ||
            //    "DELETE_FLG".Equals(this.CONDITION_VALUE.DBFieldsName) ||
            //    "ZAIKO_HIRITSU".Equals(this.CONDITION_VALUE.DBFieldsName))
            //{
            //    this.CONDITION_VALUE.ImeMode = ImeMode.Disable;
            //}
            //else
            //{
            //    this.CONDITION_VALUE.ImeMode = this.CONDITION_VALUE.ImeMode;
            //}
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
                    if ((!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value)) &&
                                              Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value != null &&
                                              (!"".Equals(Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value)))
                    {
                        string zaikoHinmeiCD = Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                        Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value = zaikoHinmeiCD;
                        if (prevZaikoHinmeiCd.ToUpper() != zaikoHinmeiCD)
                        {
                            //存在チェック
                            if (!this.logic.ZaikoHinmeCDCheck(zaikoHinmeiCD))
                            {
                                e.Cancel = true;
                                this.isErrorIchiran = true;
                                this.Ichiran.BeginEdit(false);
                                prevZaikoHinmeiCd = string.Empty;
                                return;
                            }
                            else
                            {
                                Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_NAME_RYAKU"].Value
                                    = this.logic.SearchZaikoHinmei[0].ZAIKO_HINMEI_NAME_RYAKU;

                            }
                        }
                    }
                    else
                    {
                        Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_NAME_RYAKU"].Value = string.Empty;

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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.IchiranCellValidated(e);
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
                this.Ichiran["DELETE_FLG", e.RowIndex].ReadOnly = this.Ichiran.Rows[e.RowIndex].IsNewRow;

                if (!this.isErrorIchiran)
                {
                    if (e.ColumnIndex == this.Ichiran.Columns["ZAIKO_HINMEI_CD"].Index)
                    {

                        if (Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value != null && !DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value) && !String.IsNullOrEmpty(Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value.ToString()))
                        {
                            prevZaikoHinmeiCd = Ichiran.Rows[e.RowIndex].Cells["ZAIKO_HINMEI_CD"].Value.ToString().PadLeft(6, '0');
                        }
                        else
                        {
                            prevZaikoHinmeiCd = string.Empty;
                        }
                    }
                }
                else
                {
                    this.isErrorIchiran = false;
                }

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

        #region 業者CD - 現場CD
        /// <summary>
        /// 業者CDに関連する情報をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    LogUtility.DebugMethodStart(sender,e);

            //    this.GYOUSHA_NAME_RYAKU.Text = string.Empty;

            //    this.logic.CreateNoDataRecord();
            //}
            //catch (Exception ex)
            //{
            //    LogUtility.Error("GYOUSHA_CD_TextChanged", ex);
            //    throw;
            //}
            //finally
            //{
            //    LogUtility.DebugMethodEnd();
            //}
        }
        /// <summary>
        /// 現場に関連する情報をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    LogUtility.DebugMethodStart(sender,e);

            //    this.GENBA_NAME_RYAKU.Text = string.Empty;

            //    this.logic.CreateNoDataRecord();
            //}
            //catch (Exception ex)
            //{
            //    LogUtility.Error("GENBA_CD_TextChanged", ex);
            //    throw;
            //}
            //finally
            //{
            //    LogUtility.DebugMethodEnd();
            //}
        }

        /// <summary>
        /// 業者CDに関連する情報をセット
        /// </summary>
        public bool SetGyousha()
        {
            // 初期化
            bool ret = false;
            try
            {

                ret = this.logic.CheckGyousha();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// 現場に関連する情報をセット
        /// </summary>
        public bool SetGenba()
        {
            // 初期化
            bool ret = false;
            try
            {
                ret = this.logic.CheckGenba();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGenba", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGenba", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {


            this.SetGyousha();


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, CancelEventArgs e)
        {


            this.SetGenba();


        }
        #endregion

        #region  品名読み込み処理
        /// <summary>
        /// 品名読み込み処理
        /// </summary>
        public virtual void ZaikoHinmeiLoad(object sender, EventArgs e)
        {

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (this.GYOUSHA_CD.Text == null || this.GYOUSHA_CD.Text == "")
            {

                msgLogic.MessageBoxShow("E001", "業者");
                this.GYOUSHA_CD.Focus();
                return;
            }
            if (this.GENBA_CD.Text == null || this.GENBA_CD.Text == "")
            {
                msgLogic.MessageBoxShow("E001", "現場");
                this.GENBA_CD.Focus();
                return;
            }
            if (msgLogic.MessageBoxShow("C090") == System.Windows.Forms.DialogResult.Yes)
            {
                //存在データのみ検索
                this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;

                //検索条件を変えて、いきなり品名コピーを押した時のため再検索
                this.Search(sender, e);

                ////必須項目が無い場合処理中断
                //if (GYOUSHA_CD.Text == "")
                //{
                //    return;
                //}

                //表示されているデータを削除する。
                if (!this.logic.CreateDeleteEntity())
                {
                    return;
                }

                //一覧クリア
                this.logic.SearchResult.Clear();
                this.logic.dtSearchResult = this.logic.SearchResult.Copy();

                //this.Ichiran.CellValidated -= this.Ichiran_CellValidated;
                //this.logic.SetIchiranData();
                //this.Ichiran.CellValidated += this.Ichiran_CellValidated;
                this.logic.ColumnAllowDBNull(this.logic.dtSearchResult);

                //this.Ichiran.DataSource = null;//リロード
                this.Ichiran.DataSource = this.logic.dtSearchResult;
                Ichiran.AllowUserToAddRows = false;

                //品名ロード
                this.logic.LoadingZaikoHinmeiListToIchiran();
                this.logic.isNowLoadingZaikoHinmeiMaster = true;//読込み中は削除ボタン動作変更

                this.Ichiran.CellValidated -= this.Ichiran_CellValidated;
                this.logic.SetIchiranData();
                this.Ichiran.CellValidated += this.Ichiran_CellValidated;
            }
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

        /// <summary>
        /// 業者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            if (this.logic.prevGyousha == null)
            {
                this.logic.prevGyousha = new M_GYOUSHA();
            }

            this.logic.prevGyousha.GYOUSHA_CD = this.GYOUSHA_CD.Text;
            this.logic.prevGyousha.GYOUSHA_NAME_RYAKU = this.GYOUSHA_NAME_RYAKU.Text;
        }

        public void PopupBeforeGyousha()
        {
            if (this.logic.prevGyousha == null)
            {
                this.logic.prevGyousha = new M_GYOUSHA();
            }

            this.logic.prevGyousha.GYOUSHA_CD = this.GYOUSHA_CD.Text;
            this.logic.prevGyousha.GYOUSHA_NAME_RYAKU = this.GYOUSHA_NAME_RYAKU.Text;
        }

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }
    }
}