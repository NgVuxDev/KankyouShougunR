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
using r_framework.Dto;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.SmsIchiran.Logic;

namespace Shougun.Core.ExternalConnection.SmsIchiran.APP
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal LogicClass logic;

        /// <summary>
        /// 処理で使用する情報（携帯番号、受信者名）
        /// </summary>
        private string mobileNumber = string.Empty;
        private string receiverName = string.Empty;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ送信用項目
        /// </summary>
        internal List<string> sendPrame = new List<string>(); 

        /// <summary>
        /// 受信者検索結果テーブル（ｼｮｰﾄﾒｯｾｰｼﾞ入力から登録されていないデータを送信時使用）
        /// </summary>
        internal DataTable searchResult = null;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_SMS_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
        }

        #region ファンクションボタン

        /// <summary>
        /// F1 ｼｮｰﾄﾒｯｾｰｼﾞ送信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // エラー件数
                int errCount = 0;
                // 成功件数
                int sucCount = 0;

                // 送信チェック
                Boolean isSmsSendCheck = this.logic.SmsSendCheck();
                if (!isSmsSendCheck)
                {
                    return;
                }

                // プログレスバーのイベント登録
                SubLogicClass subLogicClass = new SubLogicClass();
                subLogicClass.setProgressBar += new SubLogicClass.SetProgressBarEventHandler(this.SetProgressBar);
                subLogicClass.incProgressBar += new SubLogicClass.IncProgressBarEventHandler(this.IncProgressBar);

                // プログレスバーの初期化
                // 送信チェックがONの件数を設定する。
                int sendCnt = 0;
                for (int i = 0; i < this.Ichiran.Rows.Count; i++)
                {
                    if (this.Ichiran.Rows[i].Cells["sendFlg"].Value != null && (bool)this.Ichiran.Rows[i].Cells["sendFlg"].Value)
                    {
                        sendCnt++;
                    }
                }
                subLogicClass.onSetProgressBar(0, sendCnt, 0);

                // 入力チェック
                for (int i = 0; i < this.Ichiran.Rows.Count; i++)
                {
                    // 設定項目の初期化
                    receiverName = string.Empty;
                    sendPrame.Clear();

                    if (this.Ichiran.Rows[i].Cells["sendFlg"].Value != null && (bool)this.Ichiran.Rows[i].Cells["sendFlg"].Value)
                    {
                        // ｼｮｰﾄﾒｯｾｰｼﾞ入力から登録されていないデータは、紐づく受信者全てにｼｮｰﾄﾒｯｾｰｼﾞを送信
                        if (string.IsNullOrEmpty(this.Ichiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].FormattedValue.ToString()) &&
                           string.IsNullOrEmpty(this.Ichiran.Rows[i].Cells["RECEIVER_NAME"].FormattedValue.ToString()))
                        {
                            // 選択されている行の業者、現場CDを使って受信者検索
                            searchResult = this.logic.SmsReceiverSearch(this.Ichiran.Rows[i].Cells["SMS_GYOUSHA_CD"].FormattedValue.ToString(), 
                                                         this.Ichiran.Rows[i].Cells["SMS_GENBA_CD"].FormattedValue.ToString());
                            if (searchResult != null)
                            {
                                for (int k = 0; k < searchResult.Rows.Count; k++)
                                {
                                    if (searchResult.Rows[k]["RECEIVER_NAME"] != null)
                                    {
                                        receiverName = this.logic.smsLogic.ReceiverSetting(searchResult.Rows[k]["RECEIVER_NAME"].ToString());
                                    }
                                }
                            }
                            else
                            {
                                this.logic.msgLogic.MessageBoxShowError(string.Format("現場CD:{0}に受信者情報が登録されていません。\r\n受信者情報を登録してください。)",
                                                                                      this.Ichiran.CurrentRow.Cells["SMS_GENBA_CD"].FormattedValue.ToString()));
                                break;
                            }
                        }
                    }

                    // 設定項目の初期化
                    receiverName = string.Empty;
                    sendPrame.Clear();

                    // 送信先チェックボックスがtrueの場合に処理継続
                    if (this.Ichiran.Rows[i].Cells["sendFlg"].Value != null && (bool)this.Ichiran.Rows[i].Cells["sendFlg"].Value)
                    {
                        // ｼｮｰﾄﾒｯｾｰｼﾞ入力から登録されていないデータは、紐づく受信者全てにｼｮｰﾄﾒｯｾｰｼﾞを送信
                        if (string.IsNullOrEmpty(this.Ichiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].FormattedValue.ToString()) &&
                            string.IsNullOrEmpty(this.Ichiran.Rows[i].Cells["RECEIVER_NAME"].FormattedValue.ToString()))
                        {
                            if (searchResult != null)
                            {
                                for (int l = 0; l < searchResult.Rows.Count; l++)
                                {
                                    mobileNumber = searchResult.Rows[l]["MOBILE_PHONE_NUMBER"].ToString();
                                    if (searchResult.Rows[l]["RECEIVER_NAME"] != null)
                                    {
                                        receiverName = this.logic.smsLogic.ReceiverSetting(searchResult.Rows[l]["RECEIVER_NAME"].ToString());
                                    }
                                    // 送信項目設定
                                    this.logic.PrameSet(i);

                                    // ショートメッセージ送信内容設定
                                    this.logic.smsLogic.SendMessageSetting(sendPrame);

                                    // SMS送信APIのリクエスト作成
                                    this.logic.smsLogic.APIRequestSetting(sendPrame[0],
                                                                receiverName,
                                                                sendPrame[1],
                                                                sendPrame[2],
                                                                sendPrame[3],
                                                                sendPrame[4],
                                                                sendPrame[5],
                                                                sendPrame[6]);
                                    if (!this.Insert(i, mobileNumber, receiverName))
                                    {
                                        errCount++;
                                    }
                                    else
                                    {
                                        sucCount++;
                                    }

                                    // すぐに同じ携帯番号にショートメッセージを送信できないので、5秒停止
                                    System.Threading.Thread.Sleep(5000);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (this.Ichiran.Rows[i].Cells["RECEIVER_NAME"].Value != null)
                            {
                                // 受信者が登録されている場合のみセット
                                receiverName = this.Ichiran.Rows[i].Cells["RECEIVER_NAME"].Value.ToString();
                            }
                        
                            // 送信項目設定
                            this.logic.PrameSet(i);

                            // ショートメッセージ送信内容設定
                            this.logic.smsLogic.SendMessageSetting(sendPrame);

                            // SMS送信APIのリクエスト作成
                            this.logic.smsLogic.APIRequestSetting(sendPrame[0],
                                                        receiverName,
                                                        sendPrame[1],
                                                        sendPrame[2],
                                                        sendPrame[3],
                                                        sendPrame[4],
                                                        sendPrame[5],
                                                        sendPrame[6]);

                            if (!this.Insert(i, "", ""))
                            {
                                errCount++;
                            }
                            else
                            {
                                sucCount++;
                            }
                       }

                       //プログレスバーのカウントアップ
                       subLogicClass.onIncProgressBar();
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
                    // 再検索
                    this.logic.Search();
                }

                // プログレスバーのリセット
                this.ResetProgressBar();
            }
            catch(Exception ex)
            {
                LogUtility.Error("bt_func9_Click", ex);
                this.logic.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                // 受信者検索結果の初期化
                searchResult = null;
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F2 新規
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ChangeNewWindow();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F7 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.Clear();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 検索処理
            this.logic.Search();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customSortHeader1.ShowCustomSortSettingDialog();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customSearchHeader1.ShowCustomSearchSettingDialog();

            this.logic.RefreshHeaderForm();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            base.CloseTopForm();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [1] 着信一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.IchiranOpen();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="index">行番号</param>
        /// <param name="mPhoneNumber">携帯番号</param>
        /// <param name="rName">受信者名</param>
        /// <returns></returns>
        private bool Insert(int index, string mPhoneNumber, string rName)
        {
            try
            {
                LogUtility.DebugMethodStart(index, mPhoneNumber, rName);

                // 新規用Entity作成
                var smsEntity = this.logic.CreateEntity(index, mPhoneNumber, rName);

                // API送信
                string[] response = this.logic.smsLogic.LongSmsSplitSendAPI(smsEntity);

                // 更新（リクエストの可否で内容変更）
                this.logic.LongSmsSplitSendAPI_After(smsEntity, response, this.logic.smsLogic);
                
                this.logic.RegistData(smsEntity);

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
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(index, mPhoneNumber, rName);
            }
        }
        #endregion

        /// <summary>
        /// 伝票種類　値チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SMS_DENPYOU_SHURUI_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.Ichiran.Rows.Count > 0)
            {
                // 一覧をクリア
                this.Ichiran.DataSource = null;
                // 読込データ件数
                this.logic.headerForm.readDataNumber.Text = "0";
            }

            string denpyouShurui = string.Empty;
            if (!string.IsNullOrEmpty(this.SMS_DENPYOU_SHURUI.Text))
            {
                denpyouShurui = this.SMS_DENPYOU_SHURUI.Text;
                if (denpyouShurui == "1" || denpyouShurui == "2" || denpyouShurui == "4")
                {
                    // 定期専用行非表示
                    this.logic.IchiranColumnSetting_Teiki(false);
                    // 送信状況=1．未送信時のみ変更
                    if (this.logic.headerForm.SEND_JOKYO.Text == "1")
                    {
                        // 1．収集、2．出荷、4．収集+出荷を選択した場合、
                        // 配車状況を活性化、システム設定より初期値をセット
                        this.haishaJokyoPanel.Enabled = true;
                        if (!this.logic.SysInfo.SMS_HAISHA_JOKYO.IsNull)
                        {
                            this.SMS_HAISHA_JOKYO.Text = this.logic.SysInfo.SMS_HAISHA_JOKYO.ToString();
                        }
                        
                        if(string.IsNullOrEmpty(this.SMS_HAISHA_JOKYO.Text))
                        {
                            this.SMS_HAISHA_JOKYO.Text = "1";
                        }
                    }
                }
            
                else if (denpyouShurui == "3" || denpyouShurui == "5" || denpyouShurui == "6")
                {
                    // 6．定期を選択した場合のみ、明細行追加（コースCD・名、順番、回数）
                    if (denpyouShurui == "6")
                    {
                        this.logic.IchiranColumnSetting_Teiki(true);
                    }
                    else
                    {
                        this.logic.IchiranColumnSetting_Teiki(false);
                    }
                    // 3．持込、5.収集+持込、6．定期を選択した場合、
                    // 配車状況を非活性化、値をクリア
                    this.haishaJokyoPanel.Enabled = false;
                    this.SMS_HAISHA_JOKYO.Text = string.Empty;
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShowError("伝票種類に正しくない値がセットされています。");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        private void Ichiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            if (this.Ichiran.Columns[e.ColumnIndex].Name.Equals("sendFlg"))
            {
                if (e.RowIndex == -1)
                {
                    parentForm.lb_hint.Text = "すべてのｼｮｰﾄﾒｯｾｰｼﾞを一括送信したい場合、チェックを付けてください";
                }
                else
                {
                    parentForm.lb_hint.Text = "一括送信の対象データの場合、チェックを付けてください";
                }
            }
        }

        private void Ichiran_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 例外を無視する
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_DataError", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region LostFoucs関連チェック
        /// <summary>
        /// 業者CD有効性チェック 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 業者CD
                string pGosyaCd = this.GYOUSHA_CD.Text.ToString().Trim();
                // 業者CD変更チェック
                if (!this.CheckGyoushaChange())
                {
                    return;
                }
                //業者情報取得
                bool catchErr = true;
                var ret = this.logic.GetGyousyaInfo(pGosyaCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool bRet = true;
                if (ret.Length == 0)
                {
                    //業者マスタにデータ存在しない
                    this.GYOUSHA_CD.IsInputErrorOccured = true;
                    this.GYOUSHA_NAME.Clear();
                    msgLogic.MessageBoxShow("E020", "業者");
                    bRet = false;
                }
                else
                {
                    this.GYOUSHA_NAME.Text = ret[0].GYOUSHA_NAME_RYAKU;
                }
                //存在しない
                if (!bRet)
                {
                    this.GYOUSHA_CD.SelectAll();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 業者CDが変わったかチェック
        /// </summary>
        /// <returns>
        /// true = 変更あり
        /// false = 変更なし
        /// </returns>
        internal bool CheckGyoushaChange()
        {
            bool ren = true;
            try
            {
                //業者CD
                string gyoushaCd = this.GYOUSHA_CD.Text.ToString().Trim();
                if (gyoushaCd != "")
                {
                    gyoushaCd = gyoushaCd.PadLeft(6, '0');
                }
                if (gyoushaCd == "")
                {
                    this.GYOUSHA_NAME.Clear();
                    this.GENBA_CD.Text = string.Empty;
                    this.GENBA_NAME.Text = string.Empty;
                    ren = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGyoushaChange", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                ren = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaChange", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ren = false;
            }
            return ren;
        }

        /// <summary>
        /// 現場CD有効性チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //現場CD
                string pGenbaCd = this.GENBA_CD.Text.ToString().Trim();
                if (pGenbaCd != "")
                {
                    pGenbaCd = pGenbaCd.PadLeft(6, '0');
                }
                else
                {
                    this.GENBA_NAME.Clear();
                    return;
                }
                // 業者が入力されてない場合
                if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.GENBA_CD.Text = string.Empty;
                    e.Cancel = true;
                    return;
                }
                //業者CD
                string pGyousyaCD = this.GYOUSHA_CD.Text.ToString().Trim();
                if (pGyousyaCD != "")
                {
                    pGyousyaCD = pGyousyaCD.PadLeft(6, '0');
                }
                //業者と関連チェック
                bool catchErr = true;
                var ret = this.logic.GetGenbaInfo(pGenbaCd, pGyousyaCD, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                switch (ret.Length)
                {
                    case 0:
                        //<レコードが存在しない
                        this.GENBA_CD.IsInputErrorOccured = true;
                        this.GENBA_NAME.Clear();
                        msgLogic.MessageBoxShow("E020", "現場");
                        this.GENBA_CD.SelectAll();
                        e.Cancel = true;
                        break;
                    case 1:
                        this.GENBA_NAME.Text = ret[0].GENBA_NAME_RYAKU;
                        this.GYOUSHA_CD.Text = ret[0].GYOUSHA_CD;
                        var retGyo = this.logic.GetGyousyaInfo(this.GYOUSHA_CD.Text, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        this.GYOUSHA_NAME.Text = retGyo[0].GYOUSHA_NAME_RYAKU;
                        break;
                    default:
                        SendKeys.Send(" ");
                        e.Cancel = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 業者CDのポップアップから戻ってきたときの処理
        /// </summary>
        public void GyoushaCdPopUpAfter()
        {
            CheckGyoushaChange();
        }

        /// <summary>
        /// 運搬業者CD有効性チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //運搬業者CD
                string pUnpanGosyaCd = this.UNPAN_GYOUSHA_CD.Text.ToString().Trim();
                if (pUnpanGosyaCd != "")
                {
                    pUnpanGosyaCd = pUnpanGosyaCd.PadLeft(6, '0');
                }
                else
                {
                    this.UNPAN_GYOUSHA_NAME.Clear();
                    return;
                }
                //運搬業者情報取得
                bool catchErr = true;
                var ret = this.logic.GetGyousyaInfo(pUnpanGosyaCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool bRet = true;
                if (ret.Length > 0)
                {
                    //運搬業者関連チェック
                    // 20151026 BUNN #12040 STR
                    if (ret[0].UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    // 20151026 BUNN #12040 END
                    {
                        this.UNPAN_GYOUSHA_NAME.Text = ret[0].GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        this.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                        this.UNPAN_GYOUSHA_NAME.Clear();
                        msgLogic.MessageBoxShow("E062", "運搬業者");
                        bRet = false;
                    }
                }
                else
                {
                    //業者マスタにデータ存在しない
                    this.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.UNPAN_GYOUSHA_NAME.Clear();
                    msgLogic.MessageBoxShow("E020", "業者");
                    bRet = false;
                }
                //存在しない
                if (!bRet)
                {
                    this.UNPAN_GYOUSHA_CD.SelectAll();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        /// <summary>
        /// 作業日付取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_DATE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.SAGYOU_DATE_TO.Text = this.SAGYOU_DATE_FROM.Text;
        }

        #region プログレスバー更新イベント

        /// <summary>
        /// プログレスバーの範囲を設定
        /// </summary>
        /// <param name="min">プログレスバーに反映する最小の値</param>
        /// <param name="max">プログレスバーに反映する最大の値</param>
        /// <param name="value">プログレスバーに反映するその時の値</param>
        public void SetProgressBar(int min, int max, int value)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.progresBar.Maximum = max;
            parentForm.progresBar.Minimum = min;
            parentForm.progresBar.Value = value;
        }

        /// <summary>
        /// プログレスバーを加算
        /// </summary>
        public void IncProgressBar()
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            if (parentForm.progresBar.Maximum > parentForm.progresBar.Value)
            {
                parentForm.progresBar.Value += 1;
            }
        }

        /// <summary>
        /// プログレスバーをリセット
        /// </summary>
        public void ResetProgressBar()
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.progresBar.Value = 0;
        }

        #endregion
    }
}
