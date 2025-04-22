// $Id: LogicClass.cs 86156 2016-09-08 07:01:09Z huangxk@oec-h.com $
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Common.NioroshiNoSettei
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Common.NioroshiNoSettei.Setting.ButtonSetting.xml";

        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 現場マスタのDao
        /// </summary>
        private IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// 不正な入力をされたかを示します
        /// </summary>
        internal bool isInputError = false;

        internal MessageBoxShowLogic MsgBox;
        internal List<NioroshiDto> nioroshiList;
        internal List<DetailDto> detailList;
        internal List<string> nioroshiNumList;
        internal List<string> hinmeiList;
        public bool isRegist { get; set; }

        #endregion

        #region プロパティ

        #endregion

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.hinmeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_HINMEIDao>();                 // 品名マスタのDao
            
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="windowType"></param>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                // イベントの初期化処理
                this.EventInit(parentForm);

                SetDataForWindow();

                // 品名
                this.form.HINMEI_CD_HEAD.Text = string.Empty;
                this.form.HINMEI_NAME_HEAD.Text = string.Empty;
                this.form.HINMEI_CD_HEAD.Focus();
                this.form.RetDetailList = new Dictionary<string,List<DetailDto>>();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //<summary>
        //イベントの初期化処理
        //</summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 一括設定イベント生成
                this.form.C_Regist(parentForm.bt_func1);
                parentForm.bt_func1.Click += new EventHandler(this.form.IkkatsuSet);
                parentForm.bt_func1.ProcessKbn = PROCESS_KBN.NEW;

                // 登録(F9)イベント生成
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // 品名
                this.form.HINMEI_CD_HEAD.Validated -= new EventHandler(this.form.HINMEI_CDValidated);
                this.form.HINMEI_CD_HEAD.Validated += new EventHandler(this.form.HINMEI_CDValidated);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 画面設定
        /// <summary>
        /// 画面設定
        /// </summary>
        public void SetDataForWindow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.nioroshiNumList = new List<string>();
                int rowIndex = 0;
                DataGridViewRow row;
                if (this.nioroshiList.Count > 0)
                {
                    this.form.NioroshiIchiran.Rows.Add(this.nioroshiList.Count);
                    foreach (NioroshiDto dto in this.nioroshiList)
                    {
                        row = this.form.NioroshiIchiran.Rows[rowIndex];
                        row.Cells[NioroshiColName.NIOROSHI_NUMBER].Value = dto.NIOROSHI_NUMBER;
                        row.Cells[NioroshiColName.NIOROSHI_GYOUSHA_CD].Value = dto.NIOROSHI_GYOUSHA_CD;
                        row.Cells[NioroshiColName.NIOROSHI_GYOUSHA_NAME].Value = dto.NIOROSHI_GYOUSHA_NAME;
                        row.Cells[NioroshiColName.NIOROSHI_GENBA_CD].Value = dto.NIOROSHI_GENBA_CD;
                        row.Cells[NioroshiColName.NIOROSHI_GENBA_NAME].Value = dto.NIOROSHI_GENBA_NAME;
                        nioroshiNumList.Add(dto.NIOROSHI_NUMBER);
                        rowIndex++;
                    }
                }

                rowIndex = 0;
                this.hinmeiList = new List<string>();
                if (this.detailList.Count > 0)
                {
                    this.form.DetailIchiran.Rows.Add(this.detailList.Count);
                    foreach (DetailDto dto in this.detailList)
                    {
                        row = this.form.DetailIchiran.Rows[rowIndex];
                        row.Cells[DetailColName.ROW_NO].Value = dto.ROW_NO;
                        row.Cells[DetailColName.TABLE_ROW_NO].Value = dto.TABLE_ROW_NO;
                        row.Cells[DetailColName.ROUND_NO].Value = dto.ROUND_NO;
                        row.Cells[DetailColName.GYOUSHA_CD].Value = dto.GYOUSHA_CD;
                        row.Cells[DetailColName.GYOUSHA_NAME].Value = dto.GYOUSHA_NAME;
                        row.Cells[DetailColName.GENBA_CD].Value = dto.GENBA_CD;
                        row.Cells[DetailColName.GENBA_NAME].Value = dto.GENBA_NAME;
                        row.Cells[DetailColName.HINMEI_CD].Value = dto.HINMEI_CD;
                        row.Cells[DetailColName.HINMEI_NAME].Value = dto.HINMEI_NAME;
                        row.Cells[DetailColName.UNIT_CD].Value = dto.UNIT_CD;
                        row.Cells[DetailColName.UNIT_NAME].Value = dto.UNIT_NAME;
                        row.Cells[DetailColName.NIOROSHI_NUMBER_DETAIL].Value = dto.NIOROSHI_NUMBER_DETAIL;
                        if (dto.isRenkei)
                        {
                            row.Cells[DetailColName.NIOROSHI_NUMBER_DETAIL].ReadOnly = true;
                        }
                        else
                        {
                            row.Cells[DetailColName.NIOROSHI_NUMBER_DETAIL].ReadOnly = false;
                        }
                        row.Cells[DetailColName.TABLE_NAME].Value = dto.TABLE_NAME;
                        if (!hinmeiList.Contains(dto.HINMEI_CD))
                        {
                            hinmeiList.Add(dto.HINMEI_CD);
                        }
                        rowIndex++;
                    }
                    this.form.DetailIchiran.CurrentCell = this.form.DetailIchiran.Rows[0].Cells[DetailColName.NIOROSHI_NUMBER_DETAIL];
                    this.form.DetailIchiran.TabStop = true;
                }
                else
                {
                    this.form.DetailIchiran.TabStop = false;
                }
                this.form.HINMEI_CD_HEAD.PopupSearchSendParams[0].Value = string.Join(",", hinmeiList.ToArray());
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDataForWindow", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 一括設定
        /// <summary>
        /// 一括設定処理を行う
        /// </summary>
        /// <returns>true: エラーなし, false: エラーあり</returns>
        internal bool IkkatsuSet()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.form.HINMEI_CD_HEAD.Text))
                {
                    this.form.HINMEI_CD_HEAD.BackColor = Constans.ERROR_COLOR;
                    this.MsgBox.MessageBoxShow("E001", "品名CD");
                    this.form.HINMEI_CD_HEAD.Focus();
                    return returnVal;
                }
                else if (!hinmeiList.Contains(this.form.HINMEI_CD_HEAD.Text))
                {
                    this.form.HINMEI_CD_HEAD.BackColor = Constans.ERROR_COLOR;
                    this.MsgBox.MessageBoxShow("E062", "明細の品名CD");
                    this.form.HINMEI_CD_HEAD.Focus();
                    return returnVal;
                }
                
                if (!string.IsNullOrEmpty(this.form.NIOROSHI_NUMBER_HEAD.Text) && !nioroshiNumList.Contains(this.form.NIOROSHI_NUMBER_HEAD.Text))
                {
                    this.form.NIOROSHI_NUMBER_HEAD.BackColor = Constans.ERROR_COLOR;
                    this.MsgBox.MessageBoxShow("E062", "荷降明細の荷降No");
                    this.form.NIOROSHI_NUMBER_HEAD.Focus();
                    return returnVal;
                }

                string hinmeiCd = this.form.HINMEI_CD_HEAD.Text;
                string hinmeiCdTmp = string.Empty;
                foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
                {
                    hinmeiCdTmp = Convert.ToString(row.Cells[DetailColName.HINMEI_CD].Value);
                    if (hinmeiCdTmp == hinmeiCd)
                    {
                        if (!row.Cells[DetailColName.NIOROSHI_NUMBER_DETAIL].ReadOnly)
                        {
                            row.Cells[DetailColName.NIOROSHI_NUMBER_DETAIL].Value = this.form.NIOROSHI_NUMBER_HEAD.Text;
                        }
                    }
                }
                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IkkatsuSet", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        /// <summary>
        /// 確定処理
        /// </summary>
        /// <param name="registFlg"></param>
        public void Regist(bool registFlg)
        {
            DetailDto dto = new DetailDto();
            List<DetailDto> list = new List<DetailDto>();
            this.form.RetDetailList = new Dictionary<string, List<DetailDto>>();
            foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
            {
                dto = new DetailDto();
                dto.ROW_NO = Convert.ToInt32(row.Cells[DetailColName.ROW_NO].Value);
                dto.TABLE_ROW_NO = Convert.ToInt32(row.Cells[DetailColName.TABLE_ROW_NO].Value);
                dto.ROUND_NO = Convert.ToString(row.Cells[DetailColName.ROUND_NO].Value);
                dto.GYOUSHA_CD = Convert.ToString(row.Cells[DetailColName.GYOUSHA_CD].Value);
                dto.GYOUSHA_NAME = Convert.ToString(row.Cells[DetailColName.GYOUSHA_NAME].Value);
                dto.GENBA_CD = Convert.ToString(row.Cells[DetailColName.GENBA_CD].Value);
                dto.GENBA_NAME = Convert.ToString(row.Cells[DetailColName.GENBA_NAME].Value);
                dto.HINMEI_CD = Convert.ToString(row.Cells[DetailColName.HINMEI_CD].Value);
                dto.HINMEI_NAME = Convert.ToString(row.Cells[DetailColName.HINMEI_NAME].Value);
                dto.UNIT_CD = Convert.ToString(row.Cells[DetailColName.UNIT_CD].Value);
                dto.UNIT_NAME = Convert.ToString(row.Cells[DetailColName.UNIT_NAME].Value);
                dto.NIOROSHI_NUMBER_DETAIL = Convert.ToString(row.Cells[DetailColName.NIOROSHI_NUMBER_DETAIL].Value);
                dto.TABLE_NAME = Convert.ToString(row.Cells[DetailColName.TABLE_NAME].Value);

                if (this.form.RetDetailList.ContainsKey(dto.TABLE_NAME))
                {
                    this.form.RetDetailList[dto.TABLE_NAME].Add(dto);
                }
                else
                {
                    list = new List<DetailDto>();
                    list.Add(dto);
                    this.form.RetDetailList.Add(dto.TABLE_NAME,list);
                }
            }
        }
        #region 品名CDバリデート
        /// <summary>
        /// 品名CDバリデート
        /// </summary>
        public bool HINMEI_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.HINMEI_NAME_HEAD.Text = "";

                var hinmeiin = this.hinmeiDao.GetAllValidData(new M_HINMEI()).FirstOrDefault(s => s.HINMEI_CD == this.form.HINMEI_CD_HEAD.Text);
                if (hinmeiin == null)
                {
                    // エラーメッセージ
                    this.form.HINMEI_CD_HEAD.IsInputErrorOccured = true;
                    this.form.HINMEI_CD_HEAD.BackColor = Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "品名");
                    this.isInputError = true;
                    this.form.HINMEI_CD_HEAD.Focus();
                    return false;
                }
                else if (!hinmeiList.Contains(this.form.HINMEI_CD_HEAD.Text))
                {
                    // エラーメッセージ
                    this.form.HINMEI_CD_HEAD.IsInputErrorOccured = true;
                    this.form.HINMEI_CD_HEAD.BackColor = Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    this.MsgBox.MessageBoxShow("E062", "明細の品名CD");
                    this.isInputError = true;
                    this.form.HINMEI_CD_HEAD.Focus();
                    return false;
                }

                this.form.HINMEI_NAME_HEAD.Text = hinmeiin.HINMEI_NAME_RYAKU;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HINMEI_CDValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return true;
        }
        #endregion

        #region 荷降Noバリデート
        /// <summary>
        /// 荷降Noバリデート
        /// </summary>
        public bool NIOROSHI_NUMBER_HEAD_Validating()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string nioroshiNum = this.form.NIOROSHI_NUMBER_HEAD.Text;
                if (!string.IsNullOrEmpty(nioroshiNum) && !nioroshiNumList.Contains(nioroshiNum))
                {
                    this.form.NIOROSHI_NUMBER_HEAD.BackColor = Constans.ERROR_COLOR;
                    this.MsgBox.MessageBoxShow("E062", "荷降明細の荷降No");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NIOROSHI_NUMBER_HEAD_Validating", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return true;
        }
        #endregion

        #region 明細一覧CellValidating
        /// <summary>
        /// 明細一覧CellValidating
        /// </summary>
        public bool DetailIchiran_CellValidating(DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var col = this.form.DetailIchiran.Columns[e.ColumnIndex];
                var cell = this.form.DetailIchiran[e.ColumnIndex, e.RowIndex];
                if (col.Name == DetailColName.NIOROSHI_NUMBER_DETAIL)
                {
                    string nioroshiNo = Convert.ToString(cell.Value);
                    if (string.IsNullOrEmpty(nioroshiNo))
                    {
                        return true;
                    }

                    if (!this.nioroshiNumList.Contains(nioroshiNo))
                    {
                        ControlUtility.SetInputErrorOccuredForDgvCell(cell, true);
                        this.MsgBox.MessageBoxShow("E062", "荷降明細の荷降No");
                        e.Cancel = true;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailIchiran_CellValidating", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return true;
        }
        #endregion

        #region 入力チェック
        /// <summary>
        /// 登録時の入力チェックを行う
        /// </summary>
        /// <returns>true: エラーなし, false: エラーあり</returns>
        internal bool RegistCheck()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                Dictionary<string, string> list = new Dictionary<string, string>();
                string hinmeiCd = string.Empty;
                string nioroshiNum = string.Empty;
                foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
                {
                    hinmeiCd = Convert.ToString(row.Cells[DetailColName.HINMEI_CD].Value);
                    nioroshiNum = Convert.ToString(row.Cells[DetailColName.NIOROSHI_NUMBER_DETAIL].Value);
                    if (!string.IsNullOrEmpty(nioroshiNum) && !nioroshiNumList.Contains(nioroshiNum))
                    {
                        row.Cells[DetailColName.NIOROSHI_NUMBER_DETAIL].Style.BackColor = Constans.ERROR_COLOR;
                        this.MsgBox.MessageBoxShow("E062", "荷降明細の荷降No");
                        return returnVal;
                    }
                }

                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion
        
        #region 必須
        public int Search()
        {
            throw new NotImplementedException();
        }
        public void Update(bool registFlg)
        {
            throw new NotImplementedException();
        }
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
