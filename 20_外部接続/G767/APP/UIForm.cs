using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.FormManager;
using r_framework.Entity;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.SmsNyuuryoku
{
    /// <summary>
    /// ショートメッセージ入力
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal SmsNyuuryoku.LogicClass logic = null;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// 遷移元画面からのパラメータWindows_ID
        /// </summary>
        public string winId { get; set; }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者リスト
        /// </summary>
        internal List<int> smsReceiverList;

        /// <summary>
        /// 遷移元画面のタイトル
        /// </summary>
        internal WINDOW_ID windowId;

        /// <summary>
        /// 遷移元画面のパラメータ(PKキー)リスト
        /// </summary>
        /// <remarks>
        /// 1.伝票種類
        /// 2.SEQ(伝票)
        /// 3.受付番号(PKキーではない)
        /// 4.業者CD
        /// 5.現場CD
        /// 6.作業日
        /// 7.行番号（定期用）
        /// </remarks>
        internal string[] paramList { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        internal string SystemId { get; set; }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ送信用項目
        /// </summary>
        internal List<string> sendPrame = new List<string>(); 

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : this(null, WINDOW_TYPE.NONE, WINDOW_ID.NONE, null, null)
        {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="smsReceiverList">ｼｮｰﾄﾒｯｾｰｼﾞ受信者リスト</param>
        /// <param name="windowType">画面区分</param>
        /// <param name="winId">遷移元画面からのパラメータWindows_ID</param>
        /// <param name="paramList">遷移元画面のパラメータ(PKキー)リスト</param>
        /// <param name="windowType">システムI(ｼｮｰﾄﾒｯｾｰｼﾞ)</param>
        public UIForm(List<int> smsReceiverList, WINDOW_TYPE windowType, WINDOW_ID windowId, 
                      string[] paramList, string systemId)
            : base(WINDOW_ID.T_SMS_NYUURYOKU, windowType)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.smsReceiverList = smsReceiverList;
            this.windowId = windowId;
            this.paramList = paramList;
            this.SystemId = systemId;

            //完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);

                //初期化、初期表示
                if (!this.logic.WindowInit()) { return; }

                // 初期フォーカス位置を設定します
                this.SUBJECT.Focus();

                // 受付情報を画面に反映
                this.logic.SetValue();
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw ex;
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

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }
        #endregion

        #region ファンクションボタンのイベント

        /// <summary>
        /// [F1]伝票参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DenpyouReference(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                string strFormId = string.Empty;

                if (this.DENPYOU_SHURUI.Text == "1")
                {
                    strFormId = "G015";
                }
                else if (this.DENPYOU_SHURUI.Text == "2")
                {
                    strFormId = "G016";
                }
                else if (this.DENPYOU_SHURUI.Text == "3")
                {
                    strFormId = "G018";
                }
                else if (this.DENPYOU_SHURUI.Text == "4")
                {
                    strFormId = "G030";
                }
                else
                {
                    return;
                }

                // 対象の画面を起動
                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.DENPYOU_NUMBER.Text);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("DenpyouReference", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F9]SMS送信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                SMSLogic smsLogic = new SMSLogic();

                string mobileNumber = string.Empty;
                string sendReceiverName = string.Empty;

                // ショートメッセージ合計文字数
                int msgCount = 0;

                // 最大文字数チェックの対象フラグ
                bool maxCntFlg = false;

                // 文字数チェックのメッセージ表示フラグ
                bool alertMsgFlg = false;

                int errCount = 0;
                int sucCount = 0;

                // 必須チェック
                if (!this.logic.RegistCheck())
                {
                    return;
                }

                //送信チェック
                Boolean isSmsSendCheckOK = this.logic.SmsSendCheck();
                if (!isSmsSendCheckOK)
                {
                    return;
                }

                // 送信項目設定
                this.logic.PrameSet();

                // ショートメッセージ送信内容設定
                smsLogic.SendMessageSetting(sendPrame);

                // 入力チェック
                for (int i = 0; i < this.customDataGridView1.RowCount; i++)
                {
                    // 送信先チェックボックスがtrueの場合に処理継続
                    if (this.customDataGridView1.Rows[i].Cells["sendFlg"].Value != null && (bool)this.customDataGridView1.Rows[i].Cells["sendFlg"].Value)
                    {
                        mobileNumber = this.customDataGridView1.Rows[i].Cells["mobilePhoneNumber"].Value.ToString();
                        if (this.RECEIVER_KBN.Text == "1")
                        {
                            // 受信者=「1．受信者」である場合のみセット
                            sendReceiverName = smsLogic.ReceiverSetting(this.customDataGridView1.Rows[i].Cells["receiverName"].Value.ToString());
                        }
                
                        // メッセージ文字数算出
                        msgCount = smsLogic.SMSWordCountCalc(sendPrame[0],
                                                            sendReceiverName,
                                                            sendPrame[1],
                                                            sendPrame[2],
                                                            sendPrame[3],
                                                            sendPrame[4],
                                                            sendPrame[5],
                                                            sendPrame[6]);
                        // 最大文字数チェック
                        if (!smsLogic.SMSMaxWordCountCheck(msgCount))
                        {
                            maxCntFlg = true;
                            break;
                        }
                        // アラート文字数チェック
                        if (!smsLogic.SMSAlertCountCheck(msgCount))
                        {
                            alertMsgFlg = true;
                        }
                    }
                }

                if (maxCntFlg)
                {
                    // 最大文字数チェックの対象となった場合、処理中断
                    return;
                }
                if (alertMsgFlg)
                {
                    // アラート文字数よりメッセージ文字数が大きい場合は、メッセージ表示
                    string msg = string.Format("メッセージの文字数が予定より超えています。\nメッセージの送信を行いますか？（メッセージ文字数={0}）", msgCount);
                    DialogResult result = this.logic.msgLogic.MessageBoxShowConfirm(msg);
                    if (result == DialogResult.No)
                    {
                        // メッセージで「いいえ」を選択した場合、処理中断
                        return;
                    }
                }

                // APIメッセージ送信
                for(int j = 0; j < this.customDataGridView1.RowCount; j++)
                {
                    // 送信先チェックボックスがtrueの場合に処理継続
                    if (this.customDataGridView1.Rows[j].Cells["sendFlg"].Value != null && (bool)this.customDataGridView1.Rows[j].Cells["sendFlg"].Value)
                    {
                        mobileNumber = this.customDataGridView1.Rows[j].Cells["mobilePhoneNumber"].Value.ToString();
                        if (this.RECEIVER_KBN.Text == "1")
                        {
                            // 受信者=「1．受信者」である場合のみセット
                            sendReceiverName = smsLogic.ReceiverSetting(this.customDataGridView1.Rows[j].Cells["receiverName"].Value.ToString());
                        }

                        // SMS送信APIのリクエスト作成
                        smsLogic.APIRequestSetting(sendPrame[0],
                                                    sendReceiverName,
                                                    sendPrame[1],
                                                    sendPrame[2],
                                                    sendPrame[3],
                                                    sendPrame[4],
                                                    sendPrame[5],
                                                    sendPrame[6]);
                        
                        if (!this.Regist(mobileNumber, sendReceiverName, smsLogic))
                        {
                            errCount++;
                        }
                        else
                        {
                            sucCount++;
                        }
                    }
                }
                // 1件でもエラーが発生した場合、エラーメッセージを表示
                if (errCount > 0)
                {
                    // リクエスト自体を送れた場合に、エラー表示
                    if (!this.logic.reqErrFlg)
                    {
                        this.logic.msgLogic.MessageBoxShowError("ショートメッセージの送信処理でエラーがありました。\nエラー内容の確認を一覧で行ってください。");
                    }
                }
                // エラーが1件も無く、成功のみの場合、完了メッセージを表示
                else if (sucCount > 0)
                {
                    this.logic.msgLogic.MessageBoxShow("I001", "SMS送信");
                    this.CloseWindow();
                }
            }
            catch(Exception ex)
            {
                LogUtility.Error("bt_func9_Click", ex);
                this.logic.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                // 送信用項目の初期化
                sendPrame.Clear();
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F11]取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Cancel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // 初期化
                if (!this.logic.Cancel())
                {
                    return;
                }

                this.SUBJECT.Focus();
            }
            catch(Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.CloseWindow();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [1]全文クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.Cancel();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 登録処理
        private bool Regist(string PhoneNumber, string rName, SMSLogic smsLogic)
        {
            try
            {
                LogUtility.DebugMethodStart(PhoneNumber, rName, smsLogic);

                using (Transaction tran = new Transaction())
                {
                    // 登録用Entity作成
                    this.logic.CreateEntity(PhoneNumber, rName);

                    // API送信
                    string[] response = smsLogic.LongSmsSplitSendAPI(this.logic.smsEntry);

                    // 更新（リクエストの可否で内容変更）
                    this.logic.LongSmsSplitSendAPI_After(response, smsLogic);
                
                    // モード別更新処理
                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            this.logic.RegistData();
                            break;
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.logic.UpdateData();
                            break;
                    }
                    // コミット
                    tran.Commit();

                    // 正常にショートメッセージを送信し、メッセージIDが帰ってきた場合のみtrue
                    if (!string.IsNullOrEmpty(response[0]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("Regist", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(PhoneNumber, rName, smsLogic);
            }
        }
        #endregion

        /// <summary>
        /// 一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.CellEnter(e.ColumnIndex);
        }

        /// <summary>
        /// 画面を閉じる。
        /// </summary>
        private void CloseWindow()
        {
            this.customDataGridView1.DataSource = "";
            var parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        #region テキストエリアクリアボタン処理
        /// <summary>
        /// 入力内容クリア
        /// </summary>
        private void btnClearSubject_Click(object sender, EventArgs e)
        {
            // 件名
            this.SUBJECT.Text = string.Empty;
        }

        private void btnClearGrettings_Click(object sender, EventArgs e)
        {
            // 挨拶文
            this.GREETINGS.Text = string.Empty;
        }

        private void btnClearText1_Click(object sender, EventArgs e)
        {
            // 本文1
            this.TEXT1.Text = string.Empty;
        }

        private void btnClearText2_Click(object sender, EventArgs e)
        {
            // 本文2
            this.TEXT2.Text = string.Empty;
        }

        private void btnClearText3_Click(object sender, EventArgs e)
        {
            // 本文3
            this.TEXT3.Text = string.Empty;
        }

        private void btnClearText4_Click(object sender, EventArgs e)
        {
            // 本文4
            this.TEXT4.Text = string.Empty;
        }

        private void btnClearSignature_Click(object sender, EventArgs e)
        {
            // 署名
            this.SIGNATURE.Text = string.Empty;
        }
        #endregion
    }
}
