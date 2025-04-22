// $Id: ContenaLogicCls.cs 54491 2015-07-03 03:56:01Z quocthang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using System.Data.SqlTypes;
using System.Drawing;
using Shougun.Core.Common.BusinessCommon.Logic;

namespace Shougun.Core.Allocation.MobileJoukyouInfo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class ContenaLogicCls : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.MobileJoukyouInfo.Setting.ButtonSetting2.xml";

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private string kanriHouou;

        /// <summary>共通</summary>
        ManifestoLogic mlogic = null;

        /// <summary>
        /// UIForm form
        /// </summary>
        private ContenaForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// 画面上に表示するメッセージボックス
        /// </summary>
        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// ITeikihaishaDao
        /// </summary>
        private ITeikihaishaDao haishaDao;

        /// <summary>
        /// ribbon
        /// </summary>
        private RibbonMainMenu ribbon;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ContenaLogicCls(ContenaForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
                this.form = targetForm;
                this.haishaDao = DaoInitUtility.GetComponent<ITeikihaishaDao>();
                this.MsgBox = new MessageBoxShowLogic();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ContenaLogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit(WINDOW_TYPE windowType)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォーム
                this.parentForm = this.form.Parent as BusinessBaseForm;
                this.parentForm.ProcessButtonPanel.Visible = false;

                // HeaderFormのSet
                this.headerForm = (UIHeader)this.parentForm.headerForm;

                // RibbonMenuのSet
                this.ribbon = (RibbonMainMenu)this.parentForm.ribbonForm;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // システム情報を取得
                this.GetSysInfoInit();

                // リボンメニュー非表示
                this.ribbonHide();

                // システム情報を取得
                this.GetSysInfoInit();

                // モバイル将軍業務コンテナデータの取得
                this.GetMobileInfo();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
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

        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 閉じる
                parentForm.bt_func12.Click -= new System.EventHandler(this.bt_func12_Click);
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);
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

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
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
                LogUtility.Error("ButtonSetting", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region システム情報を取得
        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();

                this.kanriHouou = "1";
                if (sysInfo != null && !sysInfo[0].CONTENA_KANRI_HOUHOU.IsNull)
                {
                    this.kanriHouou = sysInfo[0].CONTENA_KANRI_HOUHOU.ToString();
                }
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
        #endregion

        #region リボンメニュー非表示
        /// <summary>
        /// リボンメニュー非表示
        /// </summary>
        private void ribbonHide()
        {
            // リボン非表示
            this.ribbon.Visible = false;

            // 非表示にした分、各Formを調整
            this.headerForm.Location = new Point(this.headerForm.Location.X, this.headerForm.Location.Y - this.ribbon.Height);
            this.form.Location = new Point(this.form.Location.X, this.form.Location.Y - this.ribbon.Height);
            this.parentForm.Size = new Size(this.parentForm.Size.Width, this.parentForm.Size.Height - this.ribbon.Height);
            this.form.Size = new Size(this.form.Size.Width, this.form.Size.Height + 100);
            this.parentForm.StartPosition = FormStartPosition.CenterParent;
        }
        #endregion

        /// <summary>
        /// モバイル将軍業務コンテナデータの取得
        /// </summary>
        private void GetMobileInfo()
        {

            // データ抽出用SQLサンプル
            SqlInt64 SEQ_NO = 0;
            if (!string.IsNullOrEmpty(this.form.seqNO))
            {
                SEQ_NO = SqlInt64.Parse(this.form.seqNO);
            }
            DataTable contenaData = this.haishaDao.GetContenaDetail(SEQ_NO);

            if (contenaData.Rows.Count > 0)
            {
                int rowIndex = 0;
                foreach (DataRow row in contenaData.Rows)
                {
                    this.form.ContenaDetail.Rows.Add();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["CONTENA_SHURUI_CD1"].Value = row["CONTENA_SHURUI_CD1"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["CONTENA_SHURUI_NAME_RYAKU1"].Value = row["CONTENA_SHURUI_NAME_RYAKU1"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["DAISUU_CNT1"].Value = row["DAISUU_CNT1"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["CONTENA_CD1"].Value = row["CONTENA_CD1"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["CONTENA_NAME_RYAKU1"].Value = row["CONTENA_NAME_RYAKU1"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["CONTENA_SET_KBN1"].Value = row["CONTENA_SET_KBN1"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["CONTENA_SHURUI_CD2"].Value = row["CONTENA_SHURUI_CD2"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["CONTENA_SHURUI_NAME_RYAKU2"].Value = row["CONTENA_SHURUI_NAME_RYAKU2"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["DAISUU_CNT2"].Value = row["DAISUU_CNT2"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["CONTENA_CD2"].Value = row["CONTENA_CD2"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["CONTENA_NAME_RYAKU2"].Value = row["CONTENA_NAME_RYAKU2"].ToString();
                    this.form.ContenaDetail.Rows[rowIndex].Cells["CONTENA_SET_KBN2"].Value = row["CONTENA_SET_KBN2"].ToString();
                    rowIndex++;
                }
            }

            //自動整列
            this.form.ContenaDetail.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            this.form.ContenaDetail.Columns["CONTENA_SHURUI_CD1"].Width = 165;
            this.form.ContenaDetail.Refresh();

            // 1.数量管理場合表示制御
            if (this.kanriHouou == "1")
            {
                this.form.ContenaDetail.Columns["CONTENA_CD1"].Visible = false;
                this.form.ContenaDetail.Columns["CONTENA_NAME_RYAKU1"].Visible = false;
                this.form.ContenaDetail.Columns["CONTENA_CD2"].Visible = false;
                this.form.ContenaDetail.Columns["CONTENA_NAME_RYAKU2"].Visible = false;
            }
            // 2.固体管理場合表示制御
            else
            {
                this.form.ContenaDetail.Columns["DAISUU_CNT1"].Visible = false;
                this.form.ContenaDetail.Columns["DAISUU_CNT2"].Visible = false;
            }
        }

        #region F12 閉じる処理
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
                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region DBNull値を指定値に変換
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="value">変化値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else
            {
                return obj;
            }
        }
        #endregion

        #region 自動生成（実装なし）
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

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            return 0;
        }
        #endregion
    }
}