using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Reflection;
using System.Windows.Forms;
using Seasar.Framework.Exceptions;
using r_framework.Dto;
using r_framework.BrowseForFolder;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.IO;
using System.Data;
using Microsoft.VisualBasic;
using Shougun.Core.Common.BusinessCommon;

namespace ShukkinDataShutsuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;    
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;
        /// <summary>
        /// HeaderForm
        /// </summary>
        private UIHeader header;
        /// <summary>
        /// 親フォーム
        /// </summary>
        private BusinessBaseForm parentForm;
        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        public MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        /// <summary>
        /// 出金データ出力Dao
        /// </summary>
        private DAOClass dao;
        /// <summary>
        /// M_SYS_PREV_VALUEのDao
        /// </summary>
        private IM_SYS_PREV_VALUEDao sysPrevDao;
        //入力エラーフラグ 
        internal bool inputErrorFlg = false;
        //配列入力制御 
        internal Control[] arrInputControl;
        //辞書には、コントロールの以前のテキストが含まれています
        internal Dictionary<Control, string> dicPrevValue = new Dictionary<Control, string>();
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOClass(); 
            this.parentForm = (BusinessBaseForm)this.form.Parent;
            this.header = (UIHeader)this.parentForm.headerForm;
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.sysPrevDao = DaoInitUtility.GetComponent<IM_SYS_PREV_VALUEDao>();
            LogUtility.DebugMethodEnd();
        }
        #region 画面初期化処理
        /// <summary>
        /// 
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.arrInputControl = new Control[] { this.form.BANK_CD, this.form.BANK_SHITEN_CD, this.form.FURIKOMI_DATE, this.form.SHUTSURYOKU_JOUKYOU};
                //　ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();
                //振込日
                this.form.FURIKOMI_DATE.Value = this.parentForm.sysDate.Date;
                //出力状況
                this.form.SHUTSURYOKU_JOUKYOU.Text = ConstClass.SHUTSURYOKU_JOUKYOU_MI;

                this.form.pnlKINGAKU.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.form.Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                this.form.Ichiran.TabStop = false;
                this.form.Ichiran.AutoGenerateColumns = false;
                this.form.Ichiran.IsBrowsePurpose = true;
                this.ClearDataGridView();
                this.GetPreValue();
                this.SaveDicPrevValue();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        /// <summary>
        /// 辞書を初期化する 
        /// </summary>
        private void SaveDicPrevValue()
        {
            foreach (var ctr in this.arrInputControl)
            {
                this.SaveDicPrevValue(ctr);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctr"></param>
        private void SaveDicPrevValue(Control ctr)
        {
            if (!dicPrevValue.ContainsKey(ctr))
                dicPrevValue.Add(ctr, ctr.Text);
            else
                dicPrevValue[ctr] = ctr.Text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctr"></param>
        /// <returns></returns>
        private bool IsValueChanged(Control ctr)
        {
            if (dicPrevValue.ContainsKey(ctr)
                && this.dicPrevValue[ctr] != ctr.Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Control_Enter(object sender, EventArgs e)
        {
            var ctr = sender as Control;
            string text = ctr.Text;
            if (!this.inputErrorFlg)
            {
                text = null;
            }
            this.dicPrevValue[ctr] = ctr.Text;
            if (ctr == this.form.BANK_SHITEN_CD)
            {
                ctr.CausesValidation = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.inputErrorFlg = false;
            var textBox = sender as CustomTextBox;
            if (textBox != null)
            {
                this.inputErrorFlg = textBox.IsInputErrorOccured;
            }
            else
            {
                var datePicker = sender as CustomDateTimePicker;
                this.inputErrorFlg = datePicker.IsInputErrorOccured;
            }
        }
        #endregion
        #region ボタン初期化処理
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
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.BUTTON_SETTING_XML);
        }
        #endregion
        #region イベントの初期化処理
        /// <summary>
        /// ボタンのイベント初期化処理
        /// </summary>
        /// <returns></returns>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            this.parentForm.bt_func6.Click += this.bt_func6_Click;
            this.parentForm.bt_func8.Click += this.bt_func8_Click;
            this.parentForm.bt_func9.Click += this.bt_func9_Click;
            this.parentForm.bt_func12.Click += this.bt_func12_Click;

            this.form.BANK_CD.Validated += new EventHandler(BANK_CD_Validated);
            this.form.BANK_SHITEN_CD.Validated += new EventHandler(BANK_SHITEN_CD_Validated);
            this.form.FURIKOMI_DATE.Validated += new EventHandler(FURIKOMI_DATE_Validated);
            this.form.SHUTSURYOKU_SAKI.Validating += new System.ComponentModel.CancelEventHandler(SHUTSURYOKU_SAKI_Validating);
            this.form.SHUTSURYOKU_JOUKYOU.TextChanged += new EventHandler(SHUTSURYOKU_JOUKYOU_TextChanged);
            this.form.SEARCH_SHUTSURYOKU_SAKI.Click += new EventHandler(SEARCH_SHUTSURYOKU_SAKI_Click);
            
            foreach (var ctr in this.arrInputControl)
            {
                ctr.Enter += new EventHandler(Control_Enter);
                ctr.Validating += new System.ComponentModel.CancelEventHandler(Control_Validating);
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUTSURYOKU_SAKI_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.SHUTSURYOKU_SAKI.Text))
            {
                if (!System.IO.Directory.Exists(this.form.SHUTSURYOKU_SAKI.Text))
                {
                    this.form.SHUTSURYOKU_SAKI.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E327", this.form.SHUTSURYOKU_SAKI.DisplayItemName);
                    e.Cancel = true;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEARCH_SHUTSURYOKU_SAKI_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            string forderPath = string.Empty;
            if (!string.IsNullOrEmpty(this.form.SHUTSURYOKU_SAKI.Text))
            {
                forderPath = this.form.SHUTSURYOKU_SAKI.Text;
            }
            if (string.IsNullOrEmpty(forderPath))
            {
                forderPath = @"C:\";
            }
            var browserForFolder = new BrowseForFolder();
            var title = "参照するフォルダを選択してください。";// "フォルダの参照";
            var initialPath = forderPath;
            var windowHandle = this.form.Handle;
            var isFileSelect = false;
            var isTerminalMode = SystemProperty.IsTerminalMode;

            try
            {
                forderPath = browserForFolder.getFolderPath(title, initialPath, windowHandle, isFileSelect);
            }
            catch (Exception ex)
            {
                forderPath = "";
            }
            if (!string.IsNullOrEmpty(forderPath))
            {
                this.form.SHUTSURYOKU_SAKI.Text = forderPath;
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUTSURYOKU_JOUKYOU_TextChanged(object sender, EventArgs e)
        {
            var txt = sender as Control;
            if (this.IsValueChanged(txt))
            {
                this.ClearDataGridView();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void BANK_SHITEN_CD_Validated(object sender, EventArgs e)
        {
            var txt = sender as Control;
            if (this.IsValueChanged(txt))
            {
                this.ClearDataGridView();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void BANK_CD_Validated(object sender, EventArgs e)
        {
            var txt = sender as Control;
            if (string.IsNullOrEmpty(txt.Text) || this.IsValueChanged(txt))
            {
                this.form.BANK_SHITEN_CD.Text = string.Empty;
                this.form.BANK_SHITEN_NAME_RYAKU.Text = string.Empty;
                this.form.KOUZA_SHURUI.Text = string.Empty;
                this.form.KOUZA_NO.Text = string.Empty;
            }
            if (this.IsValueChanged(txt))
            {
                this.ClearDataGridView();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FURIKOMI_DATE_Validated(object sender, EventArgs e)
        {
            var txt = sender as Control;
            if (this.IsValueChanged(txt))
            {
                this.ClearDataGridView();
            }
        }
        #endregion        
        #region ボタンイベントハンドル
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.Ichiran.RowCount == 0)
                {
                    //アラートを表示し、CSV出力処理はしない
                    this.msgLogic.MessageBoxShow("E044");
                }
                else
                {
                    //CSV出力確認メッセージを表示する
                    if (this.msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        //共通部品を利用して、画面に表示されているデータをCSVに出力する
                        CSVExport CSVExport = new CSVExport();
                        CSVExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, true, this.form.WindowId.ToTitleString(), this.form);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.ResetInputErrorOccured();

                List<Control> checkCtrs = new List<Control>() { this.form.FURIKOMI_DATE, this.form.SHUTSURYOKU_JOUKYOU};
                AutoRegistCheckLogic checkLogic = new AutoRegistCheckLogic(checkCtrs.ToArray(), checkCtrs.ToArray());
                this.form.RegistErrorFlag = checkLogic.AutoRegistCheck();
                if (!this.form.RegistErrorFlag)
                {
                    //Search C001
                    int count = 0;
                    DTOClass dto = this.GetSearchDto();
                    var dtSearch = this.dao.GetShukkinData(dto);
                    count = dtSearch.Rows.Count;

                    this.form.Ichiran.TabStop = false;
                    if (count > 0)
                    {
                        this.form.Ichiran.IsBrowsePurpose = false;
                        this.form.Ichiran.DataSource = dtSearch;
                        this.form.Ichiran.TabStop = true;
                        this.form.KENSUU_GOUKEI.Text = this.form.Ichiran.Rows.Count.ToString("#,##0") ;
                        this.form.KINGAKU_GOUKEI.Text = this.form.Ichiran.Rows.Cast<DataGridViewRow>().Sum(r => (decimal)r.Cells["COL_FURIKOMI_KINGAKU"].Value).ToString("#,##0");
                        this.form.Ichiran.IsBrowsePurpose = true;
                    }
                    else
                    {
                        this.ClearDataGridView();
                        this.msgLogic.MessageBoxShow("C001");
                    }
                }
                else
                {
                    var ctr = checkCtrs.Where(c => c is ICustomAutoChangeBackColor && ((ICustomAutoChangeBackColor)c).IsInputErrorOccured == true)
                        .OrderBy(c => c.TabIndex).FirstOrDefault();
                    if (ctr != null)
                    {
                        ctr.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.ResetInputErrorOccured();
                List<Control> checkCtrs = new List<Control>() {this.form.BANK_CD, this.form.BANK_SHITEN_CD,this.form.SHUTSURYOKU_JOUKYOU,this.form.FURIKOMI_DATE, this.form.SHUTSURYOKU_SAKI};
                AutoRegistCheckLogic checkLogic = new AutoRegistCheckLogic(checkCtrs.ToArray(), checkCtrs.ToArray());
                this.form.RegistErrorFlag = checkLogic.AutoRegistCheck();
                
                if (!this.form.RegistErrorFlag)
                {
                    if (!string.IsNullOrEmpty(this.form.SHUTSURYOKU_SAKI.Text))
                    {
                        if (!System.IO.Directory.Exists(this.form.SHUTSURYOKU_SAKI.Text))
                        {
                            this.form.SHUTSURYOKU_SAKI.IsInputErrorOccured = true;
                            this.msgLogic.MessageBoxShow("E327", this.form.SHUTSURYOKU_SAKI.DisplayItemName);
                            this.form.RegistErrorFlag = true;

                        }
                    }
                }
                if (!this.form.RegistErrorFlag)
                {
                    this.SavePreValue();
                    if (this.form.Ichiran.Rows.Count > 0)
                    {
                        var validFlg = true;
                        validFlg = this.CheckContainFurikomiKingaku();
                        if (validFlg)
                        {
                            var dt = this.GetConvertDataTable();
                            validFlg = this.CheckCharacter(dt);
                            if (validFlg)
                            {
                                bool exportFlg = this.OutputTextFile(dt);
                                if (exportFlg)
                                {
                                    bool updateFlg = this.UpdateShukkin(dt);
                                    if (updateFlg)
                                    {
                                        this.msgLogic.MessageBoxShow("I001", "出力");
                                        this.bt_func8_Click(sender, e);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.msgLogic.MessageBoxShow("E328", "出力する");
                    }
                }
                if(this.form.RegistErrorFlag)
                {
                    var ctr = checkCtrs.Where(c => c is ICustomAutoChangeBackColor && ((ICustomAutoChangeBackColor)c).IsInputErrorOccured == true)
                        .OrderBy(c => c.TabIndex).FirstOrDefault();
                    if (ctr != null)
                    {
                        ctr.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func9_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e); 
                this.parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 加工方法
        /// <summary>
        /// 
        /// </summary>
        private void ResetInputErrorOccured()
        {
            foreach (Control ctr in this.form.allControl)
            {
                if (ctr is ICustomAutoChangeBackColor)
                {
                    ((ICustomAutoChangeBackColor)ctr).IsInputErrorOccured = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearDataGridView()
        {
            this.form.Ichiran.DataSource = null;
            this.form.KENSUU_GOUKEI.Text = "0";
            this.form.KINGAKU_GOUKEI.Text = "0";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckContainFurikomiKingaku()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            var dt = this.form.Ichiran.DataSource as DataTable;
            var furikomiRows = dt.AsEnumerable().Where(r => r.Field<bool>("FURIKOMI_FLG") == true);
            if (!furikomiRows.Any())
            {
                ret = false;
                this.msgLogic.MessageBoxShow("E329", "出金区分：振込の情報");
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 振込先銀行(名称)をチェック
        /// </summary>
        /// <param name="value">振込先銀行(名称)</param>
        /// <returns></returns>
        private bool OnlineBankingBankNameCheck(string value)
        {
            bool validFlg = true;
            if (!string.IsNullOrEmpty(value))
            {
                //英字26種(A～Z)、数字10種(0～9)、カナ45種(ア～ン、ただし"ヲ"を除く)
                //、濁音、半濁音、記号5種(丸括弧"()",スラッシュ"/",ピリオド".",ハイフン"-")、スペース
                validFlg = System.Text.RegularExpressions.Regex.IsMatch(
                   value,
                   @"^[ 0-9A-Z()/.\-\uFF67-\uFF9F]+$");
            }
            return validFlg;
        }
        /// <summary>
        /// 振込先支店(名称)をチェック
        /// </summary>
        /// <param name="value">振込先支店(名称)</param>
        /// <returns></returns>
        private bool OnlineBankingShitenNameCheck(string value)
        {
            bool validFlg = true;
            if (!string.IsNullOrEmpty(value))
            {
                //英字26種(A～Z)、数字10種(0～9)、カナ45種(ア～ン、ただし"ヲ"を除く)
                //、濁音、半濁音、ハイフン(-)、スペース
                validFlg = System.Text.RegularExpressions.Regex.IsMatch(
                   value,
                   @"^[ 0-9A-Z\-\uFF67-\uFF9F]+$");
            }
            return validFlg;
        }
        /// <summary>
        /// 振込先口座名をチェック
        /// </summary>
        /// <param name="value">振込先口座名</param>
        /// <returns></returns>
        private bool OnlineBankingKouzaNameCheck(string value)
        {
            bool validFlg = true;
            if (!string.IsNullOrEmpty(value))
            {
                //英字26種(A～Z)、数字10種(0～9)、カナ46種(ア～ン)、濁音、半濁音、記号8種(エンマーク"\"
                //,鍵括弧"「」",丸括弧"()",スラッシュ"/",ピリオド".",ハイフン"-")、スペース
                validFlg = System.Text.RegularExpressions.Regex.IsMatch(
                   value,
                   @"^[ 0-9A-Z\\｢｣()/.\-\uFF66-\uFF9F]+$");
            }
            return validFlg;
        }
        /// <summary>
        /// 全角カタカナ→半角カタカナ
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertToHalfKatakana(string value)
        {
            return Strings.StrConv(value, VbStrConv.Narrow, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private bool CheckCharacter(DataTable dt)
        {
            //Check valid Katakana
            bool ret = true;
            foreach (DataRow row in dt.Rows)
            {
                var bankFurigana = row["BANK_FURIGANA"].ConvertToString(string.Empty);
                var bankShitenFurigana = row["BANK_SHITEN_FURIGANA"].ConvertToString(string.Empty);
                var kouzaName = row["KOUZA_NAME"].ConvertToString(string.Empty);
                if (!this.OnlineBankingBankNameCheck(bankFurigana)
                    || !this.OnlineBankingShitenNameCheck(bankShitenFurigana)
                    || !this.OnlineBankingKouzaNameCheck(kouzaName))
                {
                    ret = false;
                    this.msgLogic.MessageBoxShow("E330");
                    break;
                }
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetConvertDataTable()
        {
            LogUtility.DebugMethodStart();

            var dt = this.form.Ichiran.DataSource as DataTable;
            var furikomiRows = dt.AsEnumerable().Where(r => r.Field<bool>("FURIKOMI_FLG") == true);
            DataTable ret = dt.Copy();
            if (furikomiRows.Any())
            {
                ret = furikomiRows.CopyToDataTable();
                foreach (DataRow row in ret.Rows)
                {
                    //振込依頼名
                    var kouzaName = row["KOUZA_NAME"].ConvertToString(string.Empty);
                    kouzaName = this.ConvertToHalfKatakana(kouzaName).ToUpper();
                    row["KOUZA_NAME"] = kouzaName;
                    //銀行名
                    var bankFurigana = row["BANK_FURIGANA"].ConvertToString(string.Empty);
                    bankFurigana = this.ConvertToHalfKatakana(bankFurigana).ToUpper();
                    row["BANK_FURIGANA"] = bankFurigana;
                    //銀行支店名
                    var bankShitenFurigana = row["BANK_SHITEN_FURIGANA"].ConvertToString(string.Empty);
                    bankShitenFurigana = this.ConvertToHalfKatakana(bankShitenFurigana).ToUpper();
                    row["BANK_SHITEN_FURIGANA"] = bankShitenFurigana;
                }
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private bool UpdateShukkin(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);
            bool ret = true;
            try
            {
                using (Transaction tran = new Transaction())
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var sysId = row.Field<Int64>("SYSTEM_ID");
                        var seq = row.Field<Int32>("SEQ");
                        var entity = this.dao.GetShukkinEntity(sysId, seq);
                        if (entity != null)
                        {
                            entity.BANK_EXPORTED_FLG = true;
                            entity.TIME_STAMP = row.Field<byte[]>("TIME_STAMP");
                            var binderLogic = new DataBinderLogic<T_SHUKKIN_ENTRY>(entity);
                            binderLogic.SetSystemProperty(entity, false);
                            this.dao.Update(entity);
                        }
                    }
                    tran.Commit();
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex);
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex);
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex);
                    this.msgLogic.MessageBoxShow("E245");
                }
                ret = false;
                return ret;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 入力値を前回値として保持する。
        /// </summary>
        private void SavePreValue()
        {
            try
            {
                LogUtility.DebugMethodStart();
                Properties.Settings.Default.SHUTSURYOKU_SAKI = this.form.SHUTSURYOKU_SAKI.Text;
                Properties.Settings.Default.Save();
                //To DB           
                List<Control> arrCtr = new List<Control>() { this.form.BANK_CD, this.form.BANK_SHITEN_CD, this.form.KOUZA_SHURUI, this.form.KOUZA_NO };
                using (Transaction tran = new Transaction())
                {
                    foreach (var ctr in arrCtr)
                    {
                        var entity = this.sysPrevDao.GetById(ConstClass.GAMEN_ID, ctr.Name);
                        if (entity != null)
                        {
                            entity.FIELD_VALUE = ctr.Text;
                            var binderLogic = new DataBinderLogic<M_SYS_PREV_VALUE>(entity);
                            binderLogic.SetSystemProperty(entity, false);
                            this.sysPrevDao.Update(entity);
                        }
                        else
                        {
                            entity = new M_SYS_PREV_VALUE() { GAMEN_ID = ConstClass.GAMEN_ID, FIELD_NAME = ctr.Name, FIELD_VALUE = ctr.Text };
                            var binderLogic = new DataBinderLogic<M_SYS_PREV_VALUE>(entity);
                            binderLogic.SetSystemProperty(entity, false);
                            this.sysPrevDao.Insert(entity);
                        }
                    }
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SavePreValue", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void GetPreValue()
        {
            LogUtility.DebugMethodStart();
            this.form.SHUTSURYOKU_SAKI.Text = Properties.Settings.Default.SHUTSURYOKU_SAKI;
            //From DB
            var prevBankInfo = this.dao.GetPrevBankData(ConstClass.GAMEN_ID);
            if (prevBankInfo.Rows.Count > 0)
            {
                var dr = prevBankInfo.AsEnumerable().Where(r => !string.IsNullOrEmpty(r["BANK_CD"].ConvertToString(string.Empty))).FirstOrDefault();  // prevBankInfo.Rows[0];
                if (dr != null)
                {
                    this.form.BANK_CD.Text = dr["BANK_CD"].ConvertToString(string.Empty);
                    this.form.BANK_NAME_RYAKU.Text = dr["BANK_NAME_RYAKU"].ConvertToString(string.Empty);

                    this.form.BANK_SHITEN_CD.Text = dr["BANK_SHITEN_CD"].ConvertToString(string.Empty);
                    this.form.BANK_SHITEN_NAME_RYAKU.Text = dr["BANK_SHITEN_NAME_RYAKU"].ConvertToString(string.Empty);
                    this.form.KOUZA_SHURUI.Text = dr["KOUZA_SHURUI"].ConvertToString(string.Empty);
                    this.form.KOUZA_NO.Text = dr["KOUZA_NO"].ConvertToString(string.Empty);
                }
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DTOClass GetSearchDto()
        {
            DTOClass ret = new DTOClass();
            ret.BANK_CD = this.form.BANK_CD.Text;
            ret.BANK_SHITEN_CD = this.form.BANK_SHITEN_CD.Text;
            ret.KOUZA_SHURUI = this.form.KOUZA_SHURUI.Text;
            ret.KOUZA_NO = this.form.KOUZA_NO.Text;
            if (!string.IsNullOrEmpty(this.form.FURIKOMI_DATE.Text))
            {
                ret.FURIKOMI_DATE = (DateTime)this.form.FURIKOMI_DATE.Value;
            } 
            if (!string.IsNullOrEmpty(this.form.SHUTSURYOKU_JOUKYOU.Text))
            {
                ret.SHUTSURYOKU_JOUKYOU = Int16.Parse(this.form.SHUTSURYOKU_JOUKYOU.Text);
            }
            ret.SHUTSURYOKU_SAKI = this.form.SHUTSURYOKU_SAKI.Text;
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        private string ZeroPading(string text, int lenght)
        {
            return text.PadLeft(lenght, '0');
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        private string SpaceAdd(string text, int lenght)
        {
            return text.PadRight(lenght);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsNull(object value)
        {
            if (value == null || DBNull.Value.Equals(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// ヘッダーレコード　
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="dr"></param>
        private void WriteHeaderRecord(StreamWriter sw, DataRow dr)
        {
            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            //データ区分
            sb.Append("1");
            //種別コード
            sb.Append("21");
            //コード区分
            sb.Append("0");
            //振込依頼人コード(取引企業コード)
            value = dr["FURIKOMI_IRAIJIN_CD"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.ZeroPading(value, 10).SubStringByByte(10));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 10));
            }
            //振込依頼人名
            value = dr["KOUZA_NAME"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.SpaceAdd(value, 40).SubStringByByte(40));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 40));
            }
            //取組日
            if (!this.IsNull(dr["DENPYOU_DATE"]))
            {
                sb.Append(((DateTime)dr["DENPYOU_DATE"]).ToString("MMdd"));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 4));
            }
            //仕向銀行番号
            value = dr["BANK_RENKEI_CD"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.ZeroPading(value, 4).SubStringByByte(4));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 4));
            }
            //仕向銀行名
            value = dr["BANK_FURIGANA"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.SpaceAdd(value, 15).SubStringByByte(15));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 15));
            }
            //仕向支店番号
            value = dr["BANK_SHITEN_RENKEI_CD"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.ZeroPading(value, 3).SubStringByByte(3));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 3));
            }
            //仕向支店名
            value = dr["BANK_SHITEN_FURIGANA"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.SpaceAdd(value, 15).SubStringByByte(15));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 15));
            }
            //預金種目(依頼人)
            value = dr["KOUZA_SHURUI"].ConvertToString(string.Empty);
            switch (value)
            {
                case "普通預金":
                    sb.Append("1");
                    break;
                case "当座預金":
                    sb.Append("2");
                    break;
                case "その他":
                    sb.Append("9");
                    break;
                default:
                    sb.Append(this.SpaceAdd(value, 1));
                    break;
            }
            //口座番号(依頼人)
            value = dr["KOUZA_NO"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.ZeroPading(value, 7).SubStringByByte(7));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 7));
            }
            //ダミー
            value = string.Empty;
            sb.Append(this.SpaceAdd(value, 17));

            sw.WriteLine(sb.ToString());
        }
        /// <summary>
        /// データレコード
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="dr"></param>
        private void WriteDetailRecord(StreamWriter sw, DataRow dr)
        {
            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            //データ区分
            sb.Append("2");
            //被仕向銀行番号
            value = dr["FURIKOMI_BANK_CD"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.ZeroPading(value, 4).SubStringByByte(4));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 4));
            }
            //被仕向銀行名
            value = dr["FURIKOMI_BANK_NAME"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.SpaceAdd(value, 15).SubStringByByte(15));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 15));
            }
            //被仕向支店番号
            value = dr["FURIKOMI_BANK_SHITEN_CD"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.ZeroPading(value, 3).SubStringByByte(3));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 3));
            }
            //被仕向支店名
            value = dr["FURIKOMI_BANK_SHITEN_NAME"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.SpaceAdd(value, 15).SubStringByByte(15));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 15));
            }
            //手形交換所番号
            value = string.Empty;
            sb.Append(this.SpaceAdd(value, 4));
            //預金種目
            value = dr["FURIKOMI_KOUZA_SHURUI_CD"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(value.SubStringByByte(1));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 1));
            }
            //口座番号
            value = dr["FURIKOMI_KOUZA_NO"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.ZeroPading(value, 7).SubStringByByte(7));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 7));
            }
            //受取人名
            value = dr["FURIKOMI_KOUZA_NAME"].ConvertToString(string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(this.SpaceAdd(value, 30).SubStringByByte(30));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 30));
            }
            //振込金額
            if (!this.IsNull(dr["FURIKOMI_KINGAKU"]))
            {
                value = ((decimal)dr["FURIKOMI_KINGAKU"]).ToString("0");
                sb.Append(this.ZeroPading(value, 10).SubStringByByte(10));
            }
            else
            {
                sb.Append(this.SpaceAdd(value, 10));
            }
            //新規コード
            sb.Append("0");
            //顧客コード1 or EDI情報
            value = string.Empty;
            sb.Append(this.SpaceAdd(value, 10));
            //顧客コード2 or EDI情報
            value = string.Empty;
            sb.Append(this.SpaceAdd(value, 10));
            //振込指定区分
            sb.Append("8");
            //識別表示
            value = string.Empty;
            sb.Append(this.SpaceAdd(value, 1));
            //ダミー
            value = string.Empty;
            sb.Append(this.SpaceAdd(value, 7));

            sw.WriteLine(sb.ToString());
        }
        /// <summary>
        /// トレーラレコード
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="dr"></param>
        private void WriteFooterRecord(StreamWriter sw, int totalKensuu, decimal totalKingaku)
        {
            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            //---トレーラレコード-------//
            //データ区分
            sb.Append("8");
            //合計件数
            sb.Append(this.ZeroPading(totalKensuu.ToString("0"), 6).SubStringByByte(6));
            //合計金額
            sb.Append(this.ZeroPading(totalKingaku.ToString("0"), 12).SubStringByByte(12));
            //ダミー
            value = string.Empty;
            sb.Append(this.SpaceAdd(value, 101));

            sw.WriteLine(sb.ToString());

            //---エンドレコード-------//
            sb = new StringBuilder();
            //データ区分
            sb.Append("9");
            //ダミー
            value = string.Empty;
            sb.Append(this.SpaceAdd(value, 119));

            sw.WriteLine(sb.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        private bool OutputTextFile(DataTable dt)
        {
            bool ret = false;
            StreamWriter sw = null;
            try
            {
                DateTime currentDate = this.dao.GetSystemDateTime(string.Empty);
                string fileName = Path.Combine(this.form.SHUTSURYOKU_SAKI.Text, string.Format("出金データ_{0}.txt", currentDate.ToString("yyyyMMdd_HHmmss")));
                var encoding = Encoding.GetEncoding("Shift_JIS");                
                
                using (sw = new StreamWriter(fileName, false, encoding))
                {
                    //ヘッダーレコード
                    var headerRow = dt.Rows[0];
                    this.WriteHeaderRecord(sw, headerRow);
                    //Detail
                    foreach (DataRow dr in dt.Rows)
                    {
                        this.WriteDetailRecord(sw, dr);
                    }
                    //Footer
                    var totalRecord = dt.Rows.Count;
                    var totalKingaku = dt.AsEnumerable().Sum(r => r.Field<decimal>("FURIKOMI_KINGAKU"));
                    this.WriteFooterRecord(sw, totalRecord, totalKingaku);
                    //Close StreamWriter
                    sw.Close();
                    sw.Dispose();
                    ret = true;
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("OutputTextFile", ex);
                throw ex;
            }
            finally
            {
                if (sw != null)
                {
                    try
                    {
                        sw.Close();
                        sw.Dispose();
                    }
                    catch
                    {
                        // 処理なし
                    }
                    finally
                    {
                        sw = null;
                    }
                }

            }
        }
        #endregion
        #region ImplementedNotUse
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
