using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Windows.Forms;
//using MasterKyoutsuPopup2.APP;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.SMS;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.SmsResult.DAO;

namespace Shougun.Core.ExternalConnection.SmsResult
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClassShoukai
    {
        #region フィールド

        /// <summary>
        /// Form
        /// </summary>
        private SmsResultShoukai form;

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ着信状況照会DAO
        /// </summary>
        private DAOClass smsResultDao;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞロジッククラス
        /// </summary>
        internal SMSLogic smsLogic;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClassShoukai(SmsResultShoukai targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            try
            {
                this.form = targetForm;
                this.smsResultDao = DaoInitUtility.GetComponent<DAOClass>();

                this.smsLogic = new SMSLogic();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is NotSingleRowUpdatedRuntimeException)) { throw; }
            }
            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 初期化

        #region WindowInit

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンの初期化
                this.ButtonInit();

                // Headerの初期化
                this.SetHeaderText();

                // コントロール初期化
                this.ControlInit();

                // イベントの初期化
                this.EventInit();

                // ロジック初期化
                this.msgLogic = new MessageBoxShowLogic();

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region コントロールの初期化

        /// <summary>
        /// コントロール初期化処理
        /// </summary>
        private void ControlInit()
        {
            try
            {
                // 作業日の初期値
                this.form.SAGYOU_DATE.Text = sysDate();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is NotSingleRowUpdatedRuntimeException)) { throw; }
            }
        }

        #endregion

        #region ヘッダの初期化

        /// <summary>
        /// Headerの初期化を行う
        /// </summary>
        private void SetHeaderText()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //ヘッダを設定する
                this.form.lb_title.Text = ConstClsShoukai.FORM_HEADER_TITLE;

                // Formタイトルの設定
                this.form.Text = ConstClsShoukai.FORM_HEADER_TITLE;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is NotSingleRowUpdatedRuntimeException)) { throw; }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.form.bt_func8.Text = ConstClsShoukai.BUTTON_REFERENCE_NAME;
                this.form.bt_func12.Text = ConstClsShoukai.BUTTON_CANCEL_NAME;

                this.form.bt_func8.Enabled = true;
                this.form.bt_func12.Enabled = true;

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is NotSingleRowUpdatedRuntimeException)) { throw; }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベントの初期化

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            try
            {
                // 最新照会(F8)イベント生成
                this.form.bt_func8.Click += new EventHandler(this.form.Reference);

                // キャンセル(F12)イベント生成
                this.form.bt_func12.Click += new EventHandler(this.form.FormClose);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is NotSingleRowUpdatedRuntimeException)) { throw; }
            }
        }

        #endregion

        #endregion

        #region ファンクションイベント


        #region F8 状況照会

        /// <summary>
        /// F8 状況照会
        /// </summary>
        /// <returns></returns>
        internal bool SmsJokyoShoukai()
        {
            LogUtility.DebugMethodStart();

            bool result = false;

            try
            {
                // 作業日が取得できない場合、処理中断。
                if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                {
                    this.msgLogic.MessageBoxShowError("作業日が入力されていません。\r\n状況照会を中断します。");
                    return false;
                }

                string tCode = ((DateTime)this.form.SAGYOU_DATE.Value).ToString("yyyyMMdd");

                RES_SMS_SEND_TRACKING_RESULT_GET_API ListDto = this.smsLogic.SmsSendTrackingResultGetAPI(tCode);

                if (ListDto != null)
                {
                    if (ListDto.List.Count > 0)
                    {
                        // 書類情報詳細の取得、登録処理
                        if (this.update(ListDto))
                        {
                            result = true;
                        }
                        else
                        {
                            this.msgLogic.MessageBoxShowError(string.Format("履歴の照会に失敗しました。\r\n時間をおいて再度実行してください。(ステータスコード={0})", ListDto.Status));
                        }
                    }
                    else
                    {
                        this.msgLogic.MessageBoxShowInformation("履歴の照会を行うデータが存在しません。");
                    }
                    
                }
                else
                {
                    this.msgLogic.MessageBoxShowInformation("照会の結果取得に失敗しました。\r\n繰り返し発生する場合はシステム管理者へ問い合わせてください。");
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogUtility.Error("SmsJokyoShoukai", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            return result;
        }

        #endregion

        #region ｼｮｰﾄﾒｯｾｰｼﾞの情報取得、更新処理

        /// <summary>
        /// 情報取得、更新処理の中身
        /// </summary>
        /// <param name="ListDto"></param>
        private bool update(RES_SMS_SEND_TRACKING_RESULT_GET_API ListDto)
        {
            // メッセージID
            string messageId = string.Empty;

            // Documentsの中身
            foreach (TRACKING_LIST list in ListDto.List)
            {
                // メッセージID保存
                messageId = list.MessageId;

                try
                {
                    T_SMS entry = this.smsResultDao.GetDataByMessageId(messageId);

                    if (entry != null)
                    {
                        // UPDATE
                        if (!string.IsNullOrEmpty(list.Messagestatus))
                        {
                            entry.SMS_STATUS = SqlInt16.Parse(list.Messagestatus);
                        }
                        if (!string.IsNullOrEmpty(list.Resultstatus))
                        {
                            entry.RECEIVER_STATUS = SqlInt16.Parse(list.Resultstatus);
                        }
                        if (!string.IsNullOrEmpty(list.Carrier))
                        {
                            entry.CARRIER = SqlInt16.Parse(list.Carrier);
                        }
                        if (!string.IsNullOrEmpty(list.Senddate))
                        {
                            entry.SEND_DATE_KARADEN = SqlDateTime.Parse(list.Senddate);
                        }

                        using (Transaction tran = new Transaction())
                        {
                            // T_SMS登録
                            this.smsResultDao.Update(entry);
                            tran.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.Error("update", ex);
                    if (ex is NotSingleRowUpdatedRuntimeException)
                    {
                        this.msgLogic.MessageBoxShow("E080");
                    }
                    else if (ex is SQLRuntimeException)
                    {
                        this.msgLogic.MessageBoxShow("E093");
                    }
                    else
                    {
                        this.msgLogic.MessageBoxShow("E245");
                    }
                    return false;
                }
            }
            return true;
        }

        #endregion

        #endregion

        #region その他イベント

        #region システム日付の取得

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string sysDate()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return ((DateTime)now).ToString("yyyy/MM/dd(ddd)");
        }

        #endregion

        #endregion
    }
}
