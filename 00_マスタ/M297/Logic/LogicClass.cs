using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.EigyoTantoushaIkkatsu
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// 検索用DTO
        /// </summary>
        private DTO_EigyouTantou dtoEigyouTantou = new DTO_EigyouTantou();
        /// <summary>
        /// 取引先DTO
        /// </summary>
        private DTO_M_TORIHIKISAKI dtoM_TORIHIKISAKI = new DTO_M_TORIHIKISAKI();
        /// <summary>
        /// 業者DTO
        /// </summary>
        private DTO_M_GYOUSHA dtoM_GYOUSHA = new DTO_M_GYOUSHA();
        /// <summary>
        /// 現場DTO
        /// </summary>
        private DTO_M_GENBA dtoM_GENBA = new DTO_M_GENBA();

        /// <summary>
        /// 取引先DAO
        /// </summary>
        private DAO_M_TORIHIKISAKI daoM_TORIHIKISAKI = DaoInitUtility.GetComponent<DAO_M_TORIHIKISAKI>();
        /// <summary>
        /// 業者DAO
        /// </summary>
        private DAO_M_GYOUSHA daoM_GYOUSHA = DaoInitUtility.GetComponent<DAO_M_GYOUSHA>();
        /// <summary>
        /// 現場DAO
        /// </summary>
        private DAO_M_GENBA daoM_GENBA = DaoInitUtility.GetComponent<DAO_M_GENBA>();
        /// <summary>
        /// 社員DAO
        /// </summary>
        private DAO_M_SHAIN daoM_SHAIN = DaoInitUtility.GetComponent<DAO_M_SHAIN>();

        /// 部署DAO
        /// </summary>
        private IM_BUSHODao daoBusho = DaoInitUtility.GetComponent<IM_BUSHODao>();
        /// <summary>

        /// <summary>
        /// 検索結果DataTable
        /// </summary>
        private DataTable searchResult = new DataTable();

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm
        /// </summary>
        private HeaderForm headerForm;

        /// <summary>
        /// 親フォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// 処理区分
        /// </summary>
        private int targetOrd;

        /// <summary>
        /// 取引先、業者、現場DataGridView
        /// </summary>
        private DataGridView[] dgvs = new DataGridView[ConstClass.TARGET_COUNT];

        /// <summary>
        /// DataGridView
        /// </summary>
        internal DataGridView dgv;

        /// <summary>
        ///　エラー判定フラグ
        /// </summary>
        public bool isError { get; set; }


        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.parentForm = (BusinessBaseForm)this.form.Parent;
            this.headerForm = (HeaderForm)parentForm.headerForm;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.BUTTON_SETTING_XML);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンのイベント初期化処理
        /// </summary>
        /// <returns></returns>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            this.parentForm.KeyDown += new KeyEventHandler(this.UIFormKeyDown);

            this.parentForm.bt_func1.Click += this.bt_func1_Click;
            this.parentForm.bt_func8.Click += this.bt_func8_Click;
            this.parentForm.bt_func9.Click += this.bt_func9_Click;
            this.parentForm.bt_func11.Click += this.bt_func11_Click;
            this.parentForm.bt_func12.Click += this.bt_func12_Click;
            this.parentForm.bt_process1.Click += this.bt_process1_Click;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        public void WindowInit()
        {
            LogUtility.DebugMethodStart();

            //　ボタンのテキストを初期化
            this.ButtonInit();
            // イベントの初期化処理
            this.EventInit();

            this.dgv = this.dgvs[ConstClass.TORIHIKISAKI] = this.form.dgvTorihikisaki;
            this.dgvs[ConstClass.GYOUSHA] = this.form.dgvGyousha;
            this.dgvs[ConstClass.GENBA] = this.form.dgvGenba;

            this.dgvs[ConstClass.GYOUSHA].Location = this.dgvs[ConstClass.GENBA].Location = this.dgv.Location;
            this.dgvs[ConstClass.GYOUSHA].AutoGenerateColumns = this.dgvs[ConstClass.GENBA].AutoGenerateColumns = this.dgv.AutoGenerateColumns = false;

            this.headerForm.lb_title.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "(取引先)";

            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M297", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                this.DispReferenceMode();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.dgvGenba.ReadOnly = true;
            this.form.dgvGyousha.ReadOnly = true;
            this.form.dgvTorihikisaki.ReadOnly = true;
            this.form.EIGYOUSHA_CD_AFTER.ReadOnly = true;

            // FunctionButton
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_func1.Enabled = false;
            parentForm.bt_func9.Enabled = false;
        }

        #endregion

        #region 業務処理

        /// <summary>
        /// F1 一括設定
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.SetAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.SearchAndDisplay();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 登録
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.Regist(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F11 取消
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //157016 S
                //this.form.SHAINBUSHO_CD.Text = "";
                //this.form.SHAINBUSHO_NAME.Text = "";
                //157016 E
                this.form.EIGYOUSHA_CD.Text = "";
                this.form.EIGYOUSHA_NAME.Text = "";
                this.form.EIGYOUSHA_CD_AFTER.Text = "";
                this.form.EIGYOUSHA_NAME_AFTER.Text = "";
                //155767 S
                this.form.CONDITION_ITEM.Text = "";
                this.form.CONDITION_VALUE.Text = "";
                this.form.CHECK_ALL.Checked = false;
                //155767 E
                this.searchResult.Rows.Clear();

                //this.form.SHAINBUSHO_CD.Focus();//157016
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.dgv.CancelEdit();
                this.dgv.CurrentCell = null;
                // フォームを閉じる
                this.form.Close();
                this.parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [1]業者, [1]現場, [1]取引先
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_process1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.searchResult.Rows.Clear();
                this.dgv.Visible = false;

                this.targetOrd = (this.targetOrd + 1) % ConstClass.TARGET_COUNT;

                this.dgv = dgvs[this.targetOrd];
                this.dgv.Visible = true;

                this.parentForm.bt_process1.Text = ConstClass.TARGET_NAMES[targetOrd];
                //155767 S
                this.form.CONDITION_ITEM.Text = string.Empty;
                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CHECK_ALL.Checked = false;
                //155767 E
                switch (targetOrd)
                {
                    case ConstClass.TORIHIKISAKI:
                        this.headerForm.lb_title.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "(取引先)";
                        //155767 S
                        this.form.CONDITION_ITEM.PopupSendParams = new string[] {"dgvTorihikisaki"};                        
                        //155767 E
                        break;
                    case ConstClass.GYOUSHA:
                        this.headerForm.lb_title.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "(業者)";
                        this.form.CONDITION_ITEM.PopupSendParams = new string[] {"dgvGyousha"};//155767
                        break;
                    case ConstClass.GENBA:
                        this.headerForm.lb_title.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "(現場)";
                        this.form.CONDITION_ITEM.PopupSendParams = new string[] {"dgvGenba"};//155767
                        break;
                }
                
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void UIFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                this.bt_func12_Click(sender, e);
            }
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            // No2661-->
            //MOD NHU 20211102 #157016 delete S
            //部署の必須チェック
            //if (string.IsNullOrEmpty(this.form.SHAINBUSHO_CD.Text))
            //{
            //    //未入力チェック
            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //    msgLogic.MessageBoxShow("E001", "部署");
            //    this.form.SHAINBUSHO_CD.Focus();
            //    return 0;
            //}                    
            //if (!this.form.SHAINBUSHO_CD.Text.Equals("999"))
            //{
            //    this.dtoEigyouTantou.EigyouTantouBushoCd = this.form.SHAINBUSHO_CD.Text;
            //}
            //else
            //{
            //    this.dtoEigyouTantou.EigyouTantouBushoCd = string.Empty;
            //}
            // No2661<--
            this.dtoEigyouTantou.EigyouTantouBushoCd = string.Empty;
            //MOD NHU 20211102 #157016 E   
            this.SetSeachString();//155767
            this.dtoEigyouTantou.EigyouTantouCd = this.form.EIGYOUSHA_CD.Text;

            switch (targetOrd)
            {
                case ConstClass.TORIHIKISAKI:
                    this.searchResult = this.daoM_TORIHIKISAKI.GetDataForEntity(this.dtoEigyouTantou);                    
                    break;
                case ConstClass.GYOUSHA:
                    this.searchResult = this.daoM_GYOUSHA.GetDataForEntity(this.dtoEigyouTantou);
                    break;
                case ConstClass.GENBA:
                    this.searchResult = this.daoM_GENBA.GetDataForEntity(this.dtoEigyouTantou);
                    break;
            }
            this.form.CHECK_ALL.Checked = false;//157017
            this.searchResult.Columns["HENKOU_FLG"].ReadOnly = false;//155767
            int cnt = this.searchResult.Rows.Count;
            if (cnt == 0)
            {
                this.msgLogic.MessageBoxShow("C001");
            }

            LogUtility.DebugMethodEnd(cnt);

            return cnt;
        }

        /// <summary>
        /// 検索結果を表示
        /// </summary>
        public void SearchAndDisplay()
        {
            LogUtility.DebugMethodStart();

            this.Search();
            this.dgv.DataSource = this.searchResult;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一括設定
        /// </summary>
        public void SetAll()
        {
            LogUtility.DebugMethodStart();


            //必須条件
            //MOD NHU 20211102 #157017 S
            //if (this.form.EIGYOUSHA_CD.Text == "")
            //{
            //    this.msgLogic.MessageBoxShow("E012", this.form.labelEIGYOUSHA.Text);
            //    return;
            //}
            //MOD NHU 20211102 #157017 E

            //必須条件
            var eigyouCd = this.form.EIGYOUSHA_CD_AFTER.Text;
            if (eigyouCd == "")
            {
                this.msgLogic.MessageBoxShow("E012", this.form.labelEIGYOUSHA_AFTER.Text);
            }
            else
            {
                var drShain = GetShain(eigyouCd);
                if (drShain == null)
                {
                    this.form.EIGYOUSHA_CD_AFTER.Focus();
                    this.msgLogic.MessageBoxShow("E076");
                }
                else
                {
                    int count = 0;//157016
                    foreach (var dr in this.searchResult.AsEnumerable())
                    {
                        if (this.ConvertToBool(dr["HENKOU_FLG"]))//155767
                        {
                            this.UpdateEigyouTanto(dr, drShain);
                            count += 1;//157016
                        }
                    }
                    if (count == 0)//157016
                    {
                        this.msgLogic.MessageBoxShowError("一括設定するデータがありません。");//157016
                    }
                    else
                    {
                        this.msgLogic.MessageBoxShow("I010");
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            if (this.searchResult.Rows.Count == 0)
            {
                this.msgLogic.MessageBoxShow("E061");
            }
            else
            {
                foreach (DataRow row in searchResult.Rows)
                {
                    if (!DataTableUtility.IsDataRowChanged(row))
                    {
                        row.AcceptChanges();
                    }
                }

                DTO_EigyouTantou dto =
                    targetOrd == ConstClass.TORIHIKISAKI ? (DTO_EigyouTantou)this.dtoM_TORIHIKISAKI :
                    targetOrd == ConstClass.GYOUSHA ? (DTO_EigyouTantou)this.dtoM_GYOUSHA :
                    (DTO_EigyouTantou)this.dtoM_GENBA;
                //更新時間、更新者、更新PCを設定
                new DataBinderLogic<DTO_EigyouTantou>(dto).SetSystemProperty(dto, false);

                try
                {
                    int count = 0;//157016
                    using (var tran = new Transaction())
                    {
                        foreach (var dr in this.searchResult.AsEnumerable())
                        {
                            if (this.ConvertToBool(dr["HENKOU_FLG"]))//155767
                            {
                                dto.EigyouTantouBushoCd = dr.Field<string>("EIGYOU_TANTOU_BUSHO_CD_AFTER");
                                dto.EigyouTantouCd = dr.Field<string>("EIGYOU_TANTOU_CD_AFTER");

                                if (dr.RowState != DataRowState.Unchanged)
                                {
                                    switch (targetOrd)
                                    {
                                        case ConstClass.TORIHIKISAKI:
                                            this.dtoM_TORIHIKISAKI.TorihiksakiCd = dr.Field<string>("TORIHIKISAKI_CD");
                                            this.daoM_TORIHIKISAKI.UpdateEigyouTantou(this.dtoM_TORIHIKISAKI);
                                            break;
                                        case ConstClass.GYOUSHA:
                                            this.dtoM_GYOUSHA.GyoushaCd = dr.Field<string>("GYOUSHA_CD");
                                            this.daoM_GYOUSHA.UpdateEigyouTantou(this.dtoM_GYOUSHA);
                                            break;
                                        case ConstClass.GENBA:
                                            this.dtoM_GENBA.GyoushaCd = dr.Field<string>("GYOUSHA_CD");
                                            this.dtoM_GENBA.GenbaCd = dr.Field<string>("GENBA_CD");
                                            this.daoM_GENBA.UpdateEigyouTantou(this.dtoM_GENBA);
                                            break;
                                    }
                                }
                                count += 1;//157016
                            }
                        }
                        tran.Commit();
                    }
                    if (count > 0)
                    {
                        this.msgLogic.MessageBoxShow("I001", "登録");
                        this.SearchAndDisplay();
                    }
                    else
                    {
                        this.msgLogic.MessageBoxShowError("登録するデータがありません。");//157016
                    }
                }
                catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                catch (Exception ex)
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 営業担当者（明細部）変更後
        /// </summary>
        /// <param name="e">イベントデータ</param>
        public bool dgv_CellValidating(DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            try
            {
                var col = this.dgv.Columns[e.ColumnIndex];
                //MOD NHU 20211006 #155767 S
                if (col.Name.StartsWith("HENKOU_FLG"))
                {
                    bool check = this.ConvertToBool(e.FormattedValue);
                    this.dgv.CurrentCell.Value = check;
                    var dr = ((DataRowView)this.dgv.Rows[e.RowIndex].DataBoundItem).Row;
                    dr.SetField<bool>("HENKOU_FLG", check);
                }
                //MOD NHU 20211006 #155767 E
                if (col.Name.StartsWith("EIGYOU_TANTOU_CD_AFTER"))
                {
                    var wd = (int)((DgvCustomTextBoxColumn)col).CharactersNumber;
                    var eigyouCd = e.FormattedValue.ToString().PadLeft(wd, '0');
                    this.dgv.CurrentCell.Value = eigyouCd;
                    var dr = ((DataRowView)this.dgv.Rows[e.RowIndex].DataBoundItem).Row;

                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        dr.SetField<string>("EIGYOU_TANTOU_BUSHO_CD_AFTER", string.Empty);
                        dr.SetField<string>("BUSHO_NAME_RYAKU_AFTER", string.Empty);
                        dr.SetField<string>("EIGYOU_TANTOU_CD_AFTER", string.Empty);
                        dr.SetField<string>("SHAIN_NAME_RYAKU_AFTER", string.Empty);
                        dr.SetField<string>("EIGYOU_TANTOU_CD_AFTER_OLD", string.Empty);
                        return true;
                    }

                    if (dr.Field<string>("EIGYOU_TANTOU_CD_AFTER_OLD") != eigyouCd)
                    {
                        var drShain = GetShain(eigyouCd);
                        if (drShain == null)
                        {
                            dr.SetField<string>("SHAIN_NAME_RYAKU_AFTER", string.Empty);
                            dr.SetField<string>("EIGYOU_TANTOU_CD_AFTER_OLD", string.Empty);
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020","社員");
                            e.Cancel = true;
                        }
                        UpdateEigyouTanto(dr, drShain);
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("dgv_CellValidating", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgv_CellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 営業担当者、営業担当者部署（明細部）設定
        /// </summary>
        /// <param name="drEigyouTanto">営業者DataRow</param>
        /// <param name="dtShain">社員DataRow</param>
        private void UpdateEigyouTanto(DataRow drEigyouTanto, DataRow drShain)
        {
            LogUtility.DebugMethodStart(drEigyouTanto, drShain);

            if (drShain != null)
            {
                drEigyouTanto.SetField<string>("EIGYOU_TANTOU_BUSHO_CD_AFTER", drShain.Field<string>("BUSHO_CD"));
                drEigyouTanto.SetField<string>("BUSHO_NAME_RYAKU_AFTER", drShain.Field<string>("BUSHO_NAME_RYAKU"));
                drEigyouTanto.SetField<string>("EIGYOU_TANTOU_CD_AFTER", drShain.Field<string>("SHAIN_CD"));
                drEigyouTanto.SetField<string>("SHAIN_NAME_RYAKU_AFTER", drShain.Field<string>("SHAIN_NAME_RYAKU"));
                drEigyouTanto.SetField<string>("EIGYOU_TANTOU_CD_AFTER_OLD", drShain.Field<string>("SHAIN_CD"));
            }
            else
            {
                drEigyouTanto.SetField<string>("EIGYOU_TANTOU_BUSHO_CD_AFTER", string.Empty);
                drEigyouTanto.SetField<string>("BUSHO_NAME_RYAKU_AFTER", string.Empty);
                drEigyouTanto.SetField<string>("EIGYOU_TANTOU_CD_AFTER", string.Empty);
                drEigyouTanto.SetField<string>("SHAIN_NAME_RYAKU_AFTER", string.Empty);
                drEigyouTanto.SetField<string>("EIGYOU_TANTOU_CD_AFTER_OLD", string.Empty);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 社員DataRow取得
        /// </summary>
        /// <param name="eigyouCd">営業担当者CD</param>
        /// <returns>社員DataRow</returns>
        private DataRow GetShain(string eigyouCd)
        {
            LogUtility.DebugMethodStart(eigyouCd);

            this.dtoEigyouTantou.EigyouTantouCd = eigyouCd;
            var dt = this.daoM_SHAIN.GetDataForEntity(this.dtoEigyouTantou);
            var dr = dt.Rows.Count > 0 ? dt.Rows[0] : null;

            LogUtility.DebugMethodEnd(dr);

            return dr;
        }

        /// <summary>
        /// 社員DataRow取得
        /// 削除済データも検索する
        /// </summary>
        /// <param name="eigyouCd">営業担当者CD</param>
        /// <returns>社員DataRow</returns>
        private DataRow GetShainAll(string eigyouCd)
        {
            LogUtility.DebugMethodStart(eigyouCd);

            this.dtoEigyouTantou.EigyouTantouCd = eigyouCd;
            this.dtoEigyouTantou.ISNOT_NEED_DELETE_FLG = true;
            var dt = this.daoM_SHAIN.GetDataForEntity(this.dtoEigyouTantou);
            this.dtoEigyouTantou.ISNOT_NEED_DELETE_FLG = false;
            var dr = dt.Rows.Count > 0 ? dt.Rows[0] : null;

            LogUtility.DebugMethodEnd(dr);

            return dr;
        }

        /// <summary>
        /// 営業担当者、営業担当者（変更後）変更後
        /// </summary>
        /// <param name="tbCd">営業担当者CDテキストボックス</param>
        /// <param name="e">イベントデータ</param>
        public bool EIGYOUSHA_CD_Validating(CustomAlphaNumTextBox tbCd, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(tbCd, e);

            try
            {
                var tbName = this.form.Controls[tbCd.SetFormField.Split(',')[1]];
                var eigyouCd = tbCd.Text;
                if (eigyouCd == "")
                {
                    tbName.Text = "";
                }
                else
                {
                    DataRow drShain = null;
                    if (tbCd.Name.Equals(this.form.EIGYOUSHA_CD_AFTER.Name))
                    {
                        drShain = GetShain(eigyouCd);
                    }
                    else
                    {
                        drShain = GetShainAll(eigyouCd);
                    }
                    if (drShain == null)
                    {
                        this.msgLogic.MessageBoxShow("E076");
                        e.Cancel = true;
                        tbName.Text = "";
                    }
                    else
                    {
                        //部署情報取得
                        //MOD NHU 20211102 #157016 S
                        //this.form.SHAINBUSHO_CD.Text = drShain.Field<string>("BUSHO_CD");
                        //this.form.SHAINBUSHO_NAME.Text = string.Empty;
                        //if (!string.IsNullOrEmpty(this.form.SHAINBUSHO_CD.Text))
                        //{
                        //    M_BUSHO search = new M_BUSHO();
                        //    search.BUSHO_CD = this.form.SHAINBUSHO_CD.Text;
                        //    // 削除フラグに関係なく表示させる
                        //    search.ISNOT_NEED_DELETE_FLG = true;
                        //    M_BUSHO[] busho = this.daoBusho.GetAllValidData(search);
                        //    if (busho != null && busho.Length > 0)  
                        //    {
                        //        this.form.SHAINBUSHO_CD.Text = busho[0].BUSHO_CD.ToString();
                        //        this.form.SHAINBUSHO_NAME.Text = busho[0].BUSHO_NAME_RYAKU.ToString();
                        //        if (this.form.SHAINBUSHO_CD.Text.Equals("999"))
                        //        {
                        //            this.form.SHAINBUSHO_CD_POPUP.Text = string.Empty;
                        //        }
                        //        else
                        //        {
                        //            this.form.SHAINBUSHO_CD_POPUP.Text = this.form.SHAINBUSHO_CD.Text;
                        //        }
                        //    }
                        //}
                        //MOD NHU 20211102 #157016 E
                        //部署のテキストチェンジで初期化されるので、最後に行う
                        tbCd.Text = drShain.Field<string>("SHAIN_CD");
                        tbName.Text = drShain.Field<string>("SHAIN_NAME_RYAKU");
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("EIGYOUSHA_CD_Validating", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EIGYOUSHA_CD_Validating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 列の結合描写
        /// </summary>
        /// <param name="e"></param>
        public void dgv_CellPainting(DataGridViewCellPaintingEventArgs e)
        {
            //var col1 = this.dgv.Columns[e.ColumnIndex];
            //if (col1.HeaderText.StartsWith("変更"))
            //{

            //}
            //else if (col1.HeaderText == "営業者")
            //{
            //    var col2 = this.dgv.Columns[e.ColumnIndex + 1];

            //    // セルの矩形を取得
            //    var rect = e.CellBounds;

            //    rect.Width += col2.Width;
            //    rect.Y = e.CellBounds.Y + 1;

            //    // 背景、枠線、セルの値を描画
            //    using (var brush = new SolidBrush(this.dgv.ColumnHeadersDefaultCellStyle.BackColor))
            //    {
            //        // 背景の描画
            //        e.Graphics.FillRectangle(brush, rect);

            //        using (var pen = new Pen(dgv.GridColor))
            //        {
            //            // 枠線の描画
            //            e.Graphics.DrawRectangle(pen, rect);
            //        }
            //    }

            //    // セルに表示するテキストを描画
            //    TextRenderer.DrawText(e.Graphics,
            //                    string.Format("{0}（{1}）", col1.HeaderText, col2.HeaderText),
            //                    e.CellStyle.Font,
            //                    rect,
            //                    e.CellStyle.ForeColor,
            //                    TextFormatFlags.HorizontalCenter
            //                    | TextFormatFlags.VerticalCenter);
            //}
            //else
            //{
            //    e.Paint(e.ClipBounds, e.PaintParts);
            //}
            //// イベントハンドラ内で処理を行ったことを通知
            //e.Handled = true;
            //MOD NHU 20211006 #155767 S
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point pt1 = new Point((bmp.Width - this.form.CHECK_ALL.Width) / 2, (bmp.Height - this.form.CHECK_ALL.Height) / 2);
                    if (pt1.X < 0) pt1.X = 0;
                    if (pt1.Y < 0) pt1.Y = 0;

                    // Bitmapに描画
                    this.form.CHECK_ALL.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2; ;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp, pt2);
                    e.Handled = true;
                }
            }
            //MOD NHU 20211006 #155767 E
            else
            {
                var col1 = this.dgv.Columns[e.ColumnIndex];
                if (col1.HeaderText.StartsWith("変更"))
                {

                }
                else if (col1.HeaderText == "営業者")
                {
                    var col2 = this.dgv.Columns[e.ColumnIndex + 1];
                    int colIndex = e.ColumnIndex;

                    // セルの矩形を取得
                    var rect = e.CellBounds;

                    // 1列目の場合
                    if (e.ColumnIndex == colIndex)
                    {
                        // 2列目の幅を取得して、1列目の幅に足す
                        rect.Width += dgv.Columns[colIndex + 1].Width;
                        rect.Y = e.CellBounds.Y + 1;
                    }
                    else
                    {
                        // 1列目の幅を取得して、2列目の幅に足す
                        rect.Width += dgv.Columns[colIndex].Width;
                        rect.Y = e.CellBounds.Y + 1;

                        // Leftを1列目に合わせる
                        rect.X -= dgv.Columns[colIndex].Width;
                    }

                    // 背景、枠線、セルの値を描画
                    using (var brush = new SolidBrush(this.dgv.ColumnHeadersDefaultCellStyle.BackColor))
                    {
                        // 背景の描画
                        e.Graphics.FillRectangle(brush, rect);

                        using (var pen = new Pen(dgv.GridColor))
                        {
                            // 枠線の描画
                            e.Graphics.DrawRectangle(pen, rect);
                        }

                        using (Pen pen1 = new Pen(Color.DarkGray))
                        {
                            // 直線を描画(ヘッダ上部)
                            e.Graphics.DrawLine(pen1, rect.X, rect.Y - 1, rect.X + rect.Width, rect.Y - 1);

                            // 直線を描画(ヘッダ下部)
                            e.Graphics.DrawLine(pen1, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                        }
                    }

                    // セルに表示するテキストを描画
                    TextRenderer.DrawText(e.Graphics,
                                    string.Format("{0}（{1}）", col1.HeaderText, col2.HeaderText),
                                    e.CellStyle.Font,
                                    rect,
                                    e.CellStyle.ForeColor,
                                    TextFormatFlags.HorizontalCenter
                                    | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    e.Paint(e.ClipBounds, e.PaintParts);
                }
            }
            // イベントハンドラ内で処理を行ったことを通知
            e.Handled = true;
        }

        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        /* MOD NHU 20211102 #157016 DELETE
        /// <summary>
        /// 部署の値チェック
        /// </summary>
        public bool BushoCdValidated()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                this.form.SHAINBUSHO_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.SHAINBUSHO_CD.Text))
                {
                    M_BUSHO search = new M_BUSHO();
                    search.BUSHO_CD = this.form.SHAINBUSHO_CD.Text;
                    // 削除フラグに関係なく表示させる
                    search.ISNOT_NEED_DELETE_FLG = true;
                    M_BUSHO[] busho = this.daoBusho.GetAllValidData(search);
                    if (busho != null && busho.Length > 0)  // No2661
                    {
                        this.form.SHAINBUSHO_CD.Text = busho[0].BUSHO_CD.ToString();
                        this.form.SHAINBUSHO_NAME.Text = busho[0].BUSHO_NAME_RYAKU.ToString();
                        // No2661-->
                        if (this.form.SHAINBUSHO_CD.Text.Equals("999"))
                        {
                            this.form.SHAINBUSHO_CD_POPUP.Text = string.Empty;
                        }
                        else
                        {
                            this.form.SHAINBUSHO_CD_POPUP.Text = this.form.SHAINBUSHO_CD.Text;
                        }
                        // No2661<--
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "部署");
                        this.isError = true;
                        ret = false;
                    }
                }
                // No2661-->
                else
                {
                    this.form.SHAINBUSHO_CD_POPUP.Text = string.Empty;
                }
                // No2661<--

                if (this.isError)
                {
                    this.form.SHAINBUSHO_CD.SelectAll();
                    this.form.SHAINBUSHO_NAME.Text = string.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("BushoCdValidated", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BushoCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        */

        #region MOD NHU 20211006 #155767
        
        private void SetSeachString()
        {
            this.dtoEigyouTantou = new DTO_EigyouTantou();
            if (string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                return;
            }
            switch (this.form.CONDITION_ITEM.Text)
            {
                case "取引先CD":
                    this.dtoEigyouTantou.ToriCd = this.form.CONDITION_VALUE.Text;
                    break;
                case "取引先名":
                    this.dtoEigyouTantou.ToriName = this.form.CONDITION_VALUE.Text;
                    break;
                case "業者CD":
                    this.dtoEigyouTantou.GyoushaCd = this.form.CONDITION_VALUE.Text;
                    break;
                case "業者名":
                    this.dtoEigyouTantou.GyoushaName = this.form.CONDITION_VALUE.Text;
                    break;
                case "現場CD":
                    this.dtoEigyouTantou.GenbaCd = this.form.CONDITION_VALUE.Text;
                    break;
                case "現場名":
                    this.dtoEigyouTantou.GenbaName = this.form.CONDITION_VALUE.Text;
                    break;
                case "住所":
                    this.dtoEigyouTantou.Address = this.form.CONDITION_VALUE.Text;
                    break;
                case "電話番号":
                    this.dtoEigyouTantou.Tel = this.form.CONDITION_VALUE.Text;
                    break;
                case "部署CD":
                    this.dtoEigyouTantou.BushoCdBf = this.form.CONDITION_VALUE.Text;
                    break;
                case "部署名":
                    this.dtoEigyouTantou.BushoNameBf = this.form.CONDITION_VALUE.Text;
                    break;
                case "営業者":
                    this.dtoEigyouTantou.TantoushaCdBf = this.form.CONDITION_VALUE.Text;
                    break;
            }
        }

        internal bool ConvertToBool(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
            {
                return false;
            }
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
