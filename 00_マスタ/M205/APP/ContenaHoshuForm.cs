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
using Shougun.Core.Master.ContenaHoshu.Logic;
using Seasar.Quill;
using GrapeCity.Win.MultiRow;
using Seasar.Quill.Attrs;
using r_framework.Utility;

namespace Shougun.Core.Master.ContenaHoshu.APP
{
    [Implementation]
    public partial class ContenaHoshuForm : SuperForm
    {

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private ContenaHoshuLogic logic;

        /// <summary>
        /// レコード総数
        /// </summary>
        private int RecCnt = 0;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        #region コンストラクタ
        
        public ContenaHoshuForm()
            //public ContenaHoshuForm(ContenaHoshuHeader headerForm)
            : base(WINDOW_ID.M_CONTENA, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ContenaHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region 画面Load処理

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
                this.Search(null, e);
                this.txt_ConditionItem.Focus();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.mlt_gcCustomMultiRow != null)
                {
                    this.mlt_gcCustomMultiRow.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }

        }

        #endregion


        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            try
            {
		        // この画面を最大化したくない場合は下記のように
		        // OnShownでWindowStateをNomalに指定する
                //this.ParentForm.WindowState = FormWindowState.Normal;

                //if (this.RecCnt == 0)
                //{
                //    var messageShowLogic = new MessageBoxShowLogic();
                //    messageShowLogic.MessageBoxShow("C001");
                //    return;
                //}
                if (!this.InitialFlg)
                {
                    this.Height -= 7;
                    this.InitialFlg = true;
                }
                base.OnShown(e);
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

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }


        #region ボタンイベント

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender,e);


                if (!base.RegistErrorFlag)
                {
                    //登録対象チェック
                    bool catchErr = true;
                    bool bFlg = this.logic.CreateEntity(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!bFlg)
                    {
                        //登録対象が0件の場合
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E061");
                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    //登録チェック
                    if (this.logic.ChkRegist())
                    {
                        //登録
                        this.logic.Regist(base.RegistErrorFlag);
                        //再検索
                        if (this.logic.isRegist)
                        {
                            this.mlt_gcCustomMultiRow.CellValidating -= mlt_gcCustomMultiRow_CellValidating;
                            Search(sender, e);
                            this.mlt_gcCustomMultiRow.CellValidating += mlt_gcCustomMultiRow_CellValidating;
                        }
                    }
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender,e);

                if (!base.RegistErrorFlag)
                {
                    //削除対象データ取得
                    bool catchErr = true;
                    bool bFlg = this.logic.CreateEntity(true, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!bFlg)
                    {
                        //削除対象が0件の場合
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E075");
                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    //削除チェック
                    if (this.logic.LogicalChkDelete())
                    {
                        //削除
                        this.logic.LogicalDelete();
                        if (this.logic.isRegist)
                        {
                            //再検索
                            this.Search(sender, e);
                        }
                    }
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// CSVボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender,e);

                //CSV出力チェック
                if (this.logic.ChkCSVOutput())
                {
                    //CSV出力
                    this.logic.CSVOutput();
                }

                LogUtility.DebugMethodEnd();
            }
            catch
            {
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
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ClearCondition();
                this.logic.GetSysInfoInit();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender,e);

                //検索

                this.RecCnt = this.logic.Search();
                if (RecCnt == -1)
                {
                    return;
                }
                else if (RecCnt == 0)
                {
                    //if (sender != null)
                    //{
                    //    var messageShowLogic = new MessageBoxShowLogic();
                    //    messageShowLogic.MessageBoxShow("C001");
                    //}

                    this.logic.SearchResult.Rows.Clear();
                    var table1 = this.logic.SearchResult;

                    table1.BeginLoadData();

                    this.mlt_gcCustomMultiRow.DataSource = table1;

                    ////検索結果が0件の場合
                    //var messageShowLogic = new MessageBoxShowLogic();
                    //messageShowLogic.MessageBoxShow("C001");
                    LogUtility.DebugMethodEnd();
                    return;
                }

                var table = this.logic.SearchResult;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.mlt_gcCustomMultiRow.CellValidating -= mlt_gcCustomMultiRow_CellValidating;
                this.mlt_gcCustomMultiRow.DataSource = table;
                this.mlt_gcCustomMultiRow.CellValidating += mlt_gcCustomMultiRow_CellValidating;

                // 主キーを非活性にする
                this.logic.EditableToPrimaryKey();

                LogUtility.DebugMethodEnd();
            }
            catch(Exception ex)
            {
                throw ex;
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
                LogUtility.DebugMethodStart(sender,e);

                bool catchErr = true;
                this.logic.Cancel(out catchErr);
                if (!catchErr)
                {
                    return;
                }
                Search(sender, e);

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender,e);

                var parentForm = (MasterBaseForm)this.Parent;

                Properties.Settings.Default.ConditionValue_Text = this.txt_ConditionValue.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.txt_ConditionValue.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.txt_ConditionValue.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.txt_ConditionItem.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.chk_Sakujo.Checked;

                Properties.Settings.Default.Save();

                this.Close();
                parentForm.Close();

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region グリッドイベント

        #region キー項目重複チェック

        /// <summary>
        /// キー項目重複チェック(コンテナCD,コンテナ種類CD)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mlt_gcCustomMultiRow_CellValidating(object sender, CellValidatingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                //コンテナCD,コンテナ種類CDが対象
                if (e.CellName.Equals("txtContenaSyuruiCd") || e.CellName.Equals("txtContenaCd"))
                {
                    if (e.FormattedValue != null)
                    {
                        bool catchErr = true;
                        bool isNoErr = this.logic.DuplicationCheck(out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (!isNoErr)
                        {
                            e.Cancel = true;

                            GcMultiRow gc = sender as GcMultiRow;
                            if (gc != null && gc.EditingControl != null)
                            {
                                ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                            }
                            //LogUtility.DebugMethodEnd();
                            return;
                        }
                    }
                }

                //現場CDが対象
                if (e.CellName.Equals("txtGenbaCd"))
                {
                    if (this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaCd"].EditedFormattedValue == null)
                    {
                        //LogUtility.DebugMethodEnd();
                        return;
                    }

                    //現場CDが入力され、業者CDが未入力の場合、エラー
                    //業者CD関連チェック
                    if (this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGyoushaCd"].EditedFormattedValue == null)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        MessageBox.Show("先に業者CDを入力してください", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaCd"].Value = string.Empty;
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaMei"].Value = string.Empty;
                        //LogUtility.DebugMethodEnd();
                        e.Cancel = true;
                        return;
                    }

                    //現場存在チェック
                    // 現場情報を取得
                    GcMultiRow gcMultiRow = (GcMultiRow)sender;
                    string genbaname = string.Empty;
                    bool catchErr = true;
                    genbaname = this.logic.getGenbaInfo(gcMultiRow.Rows[e.RowIndex].Cells["txtGyoushaCd"].Value.ToString(),
                                                        gcMultiRow.Rows[e.RowIndex].Cells["txtGenbaCd"].EditedFormattedValue.ToString().PadLeft(6,'0'),
                                                        out catchErr);
                    if (!catchErr)
                    {
                        e.Cancel = true;
                        return;
                    }

                    // 取得できない場合
                    if (string.IsNullOrEmpty(genbaname))
                    {
                        //検索結果が0件の場合
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E062", "現場");
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaCd"].Value = string.Empty;
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaMei"].Value = string.Empty;
                        //LogUtility.DebugMethodEnd();
                        e.Cancel = true;
                        return;
                    }

                    //現場名
                    this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaMei"].Value = genbaname;
                }

                //車輌CDが対象
                if (e.CellName.Equals("txtSyaryouCd"))
                {
                    if (this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSyaryouCd"].EditedFormattedValue == null)
                    {
                        //LogUtility.DebugMethodEnd();
                        return;
                    }

                    //車輌CDが入力され、業者CDが未入力の場合、エラー
                    //業者CD関連チェック
                    if (this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGyoushaCd"].EditedFormattedValue == null)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        MessageBox.Show("先に業者CDを入力してください", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSyaryouCd"].Value = string.Empty;
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSharyouMei"].Value = string.Empty;
                        //LogUtility.DebugMethodEnd();
                        e.Cancel = true;
                        return;
                    }

                    //車輌存在チェック
                    //車輌情報を取得
                    GcMultiRow gcMultiRow = (GcMultiRow)sender;
                    string sharyou = string.Empty;
                    bool sharyouErr = true;
                    sharyou = this.logic.getSharyouInfo(gcMultiRow.Rows[e.RowIndex].Cells["txtSyaryouCd"].EditedFormattedValue.ToString().PadLeft(6,'0'),
                                                        gcMultiRow.Rows[e.RowIndex].Cells["txtGyoushaCd"].Value.ToString(), 
                                                        out sharyouErr);
                    if (!sharyouErr)
                    {
                        e.Cancel = true;
                        return;
                    }
                    // 取得できない場合
                    if (string.IsNullOrEmpty(sharyou))
                    {
                        //検索結果が0件の場合
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E062", "車輌");
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSyaryouCd"].Value = string.Empty;
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSharyouMei"].Value = string.Empty;
                        //LogUtility.DebugMethodEnd();
                        e.Cancel = true;
                        return;
                    }

                    //現場名
                    this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSharyouMei"].Value = sharyou;
                }

                //業者CDが対象
                if (e.CellName.Equals("txtGyoushaCd"))
                {
                    if (this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGyoushaCd"].EditedFormattedValue == null)
                    {
                        //LogUtility.DebugMethodEnd();
                        return;
                    }

                    //車輌CDが未入力の場合は処理を抜ける
                    if (this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSyaryouCd"].EditedFormattedValue == null)
                    {
                        //LogUtility.DebugMethodEnd();
                        return;
                    }

                    //車輌存在チェック
                    //車輌情報を取得
                    GcMultiRow gcMultiRow = (GcMultiRow)sender;
                    string sharyou = string.Empty;
                    bool sharyouErr = true;
                    sharyou = this.logic.getSharyouInfo(gcMultiRow.Rows[e.RowIndex].Cells["txtSyaryouCd"].Value.ToString(),
                                                        gcMultiRow.Rows[e.RowIndex].Cells["txtGyoushaCd"].EditedFormattedValue.ToString().PadLeft(6, '0'),
                                                        out sharyouErr);
                    if (!sharyouErr)
                    {
                        return;
                    }
                    // 取得できない場合
                    if (string.IsNullOrEmpty(sharyou))
                    {
                        //検索結果が0件の場合
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSyaryouCd"].Value = string.Empty;
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSharyouMei"].Value = string.Empty;

                        //現場チェックをする為、returnしない

                    }

                    //車輌名
                    this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSharyouMei"].Value = sharyou;


                    //現場存在チェック
                    // 現場情報を取得
                    string genbaname = string.Empty;
                    bool catchErr = true;
                    genbaname = this.logic.getGenbaInfo(gcMultiRow.Rows[e.RowIndex].Cells["txtGyoushaCd"].EditedFormattedValue.ToString().PadLeft(6, '0'),
                                                        gcMultiRow.Rows[e.RowIndex].Cells["txtGenbaCd"].Value.ToString(),
                                                        out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    // 取得できない場合
                    if (string.IsNullOrEmpty(genbaname))
                    {
                        //検索結果が0件の場合
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaCd"].Value = string.Empty;
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaMei"].Value = string.Empty;
                        //LogUtility.DebugMethodEnd();
                        return;
                    }

                    //現場名
                    this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaMei"].Value = genbaname;
                }

                //LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CellEnterイベント
        /// <summary>
        /// CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mlt_gcCustomMultiRow_CellEnter(object sender, CellEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                // 新規行の場合には削除チェックさせない
                if (this.mlt_gcCustomMultiRow.Rows[e.RowIndex].IsNewRow)
                {
                    //チェックしない
                    this.mlt_gcCustomMultiRow.Rows[e.RowIndex]["chkDelete"].Selectable = false;
                }
                else
                {
                    //チェックする
                    this.mlt_gcCustomMultiRow.Rows[e.RowIndex]["chkDelete"].Selectable = true;
                }

                //IME制御
                switch (e.CellIndex)
                {
                    //全角
                    case 3:
                    case 7:
                    case 10:
                    case 11:
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].Style.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Hiragana;
                        break;
                    //半角英数
                    case 1:
                    case 2:
                    case 6:
                    case 8:
                    case 9:
                    case 13:
                    case 14:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                        this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].Style.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.AlphanumericHalfWidth;
                        break;
                }

                //LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region CellEndEditイベント(業者CD入力チェック)
        /// <summary>
        /// CellEndEditイベント(業者CD入力チェック)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mlt_gcCustomMultiRow_CellEndEdit(object sender, CellEndEditEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                switch (e.CellName)
                {
                    case "txtGyoushaCd":
                        //業者CDチェック
                        if (string.IsNullOrEmpty(this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGyoushaCd"].Value.ToString()))
                        {
                            this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaCd"].Value = string.Empty;
                            this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaMei"].Value = string.Empty;
                            this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSyaryouCd"].Value = string.Empty;
                            this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSharyouMei"].Value = string.Empty;
                        }
                        break;

                    case "txtSyaryouCd":
                        //車輌CDチェック
                        if (string.IsNullOrEmpty(this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSyaryouCd"].Value.ToString()))
                        {
                            this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSyaryouCd"].Value = string.Empty;
                            this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtSharyouMei"].Value = string.Empty;
                        }
                        break;
                    case "txtGenbaCd":
                        //現場CDチェック
                        if (string.IsNullOrEmpty(this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaCd"].Value.ToString()))
                        {
                            this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaCd"].Value = string.Empty;
                            this.mlt_gcCustomMultiRow.Rows[e.RowIndex].Cells["txtGenbaMei"].Value = string.Empty;
                        }
                        break;

                }

                //LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region KeyPressイベント処理(暫定対応)
        /// <summary>
        /// KeyPressイベント処理(暫定対応)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_ConditionValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            this.txt_ConditionValue.CharactersNumber = 40;
            if (this.txt_ConditionValue.ItemDefinedTypes == "smallint")
            {
                this.txt_ConditionValue.CharactersNumber = 2;
                if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Tab && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// 検索条件IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_ConditionValue_Enter(object sender, EventArgs e)
        {
            if ("SECCHI_DATE".Equals(this.txt_ConditionValue.DBFieldsName) ||
                "KOUNYUU_DATE".Equals(this.txt_ConditionValue.DBFieldsName) ||
                "LAST_SHUUFUKU_DATE".Equals(this.txt_ConditionValue.DBFieldsName) ||
                "DELETE_FLG".Equals(this.txt_ConditionValue.DBFieldsName))
            {
                this.txt_ConditionValue.ImeMode = ImeMode.Disable;
            }

        }

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContenaHoshuForm_Shown(object sender, EventArgs e)
        {
            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

    }

        #endregion
    
}
