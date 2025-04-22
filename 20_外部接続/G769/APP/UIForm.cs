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
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.Entity;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.SmsResult.Logic;
using Shougun.Core.ExternalConnection.SmsResult.APP;

namespace Shougun.Core.ExternalConnection.SmsResult
{
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal LogicClass Logic;

        /// <summary>
        /// UIHeader
        /// </summary>
        private UIHeader header;

        /// <summary>
        /// 遷移元画面からのパラメータEntity
        /// </summary>
        public T_SMS paramEntity { get; set; }

        /// <summary>
        /// 遷移元画面からのパラメータWindows_ID
        /// </summary>
        public string winId { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.SMS_RESULT, false)
        {
            try
            {
                this.InitializeComponent();

                //this.paramEntity = entity;
                //this.winId = winId;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
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

                if (this.Logic == null)
                {
                    this.Logic = new LogicClass(this);
                    // 完全に固定。ここには変更を入れない
                    QuillInjector.GetInstance().Inject(this);

                    if (!this.Logic.WindowInit())
                    {
                        return;
                    }
                    this.header = this.Logic.header;

                    //画面初期表示
                    this.Logic.InitFrom();

                    this.customSearchHeader1.Location = new System.Drawing.Point(4, 160);
                    this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customSortHeader1.Location = new System.Drawing.Point(4, 190);
                    this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customDataGridView1.Location = new System.Drawing.Point(3, 220);
                    this.customDataGridView1.Size = new System.Drawing.Size(997, 210);

                    // Anchorの設定は必ずOnLoadで行うこと
                    if (this.customDataGridView1 != null)
                    {
                        this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    }
                }

                this.PatternReload();

                // フィルタの初期化
                this.customSearchHeader1.ClearCustomSearchSetting();
                this.customSortHeader1.ClearCustomSortSetting();
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(e);
            }
        }

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        public virtual void ShowData(DataTable searchResult)
        {
            try
            {
                LogUtility.DebugMethodStart(searchResult);
                this.Table = searchResult;
                if (!this.DesignMode)
                {
                    // 拠点を設定
                    this.Logic.SetUserProfile();

                    //DataGridViewに値の設定を行う
                    this.logic.CreateDataGridView(this.Table);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region ファンクションボタン

        /// <summary>
        /// F1 SMS状況照会
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // 電子契約照会条件を開く
                var callForm = new SmsResultShoukai();
                DialogResult dr = callForm.ShowDialog(this);

                // 照会処理をして返ってきた場合のみ検索を実行
                if (dr == DialogResult.OK)
                {
                    this.Logic.Search();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func1_Click", ex);
                this.Logic.msgLogic.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F2 個別照会
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (this.customDataGridView1.CurrentRow == null)
                {
                    this.Logic.msgLogic.MessageBoxShowError("選択されている明細がありません。\r\n履歴を紹介する明細を選択してください。");
                    return;
                }

                // 照会後、更新処理実行
                if (this.Update())
                {
                    this.Logic.msgLogic.MessageBoxShowInformation("履歴の照会が完了しました。");
                }
                else
                {
                    this.Logic.msgLogic.MessageBoxShowError(string.Format("履歴の照会に失敗しました。\r\n時間をおいて再度実行してください。（ステータスコード={0}）", this.Logic.smsEntry.ERROR_CODE));
                }

                //再検索
                this.Logic.Search();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func2_Click", ex);
                this.Logic.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F7 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.Clear();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // パターン未登録の場合検索処理を行わない
                if (this.PatternNo == 0)
                {
                    this.Logic.msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }
                //検索処理を行う
                this.Logic.Search();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                this.Logic.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
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

            this.Logic.RefreshHeaderForm();

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
        /// [1] パターン一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.PatternIchiranOpen();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 更新処理
        private bool Update()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 更新用Entity作成
                this.Logic.CreateEntity();
                
                // 照会を行うメッセージID取得
                string msgId = this.Logic.smsEntry.MESSAGE_ID;

                // API送信
                string[] response = this.Logic.smsLogic.SmsSendResultGetAPI(msgId);

                // 更新（リクエストの可否で内容変更）
                this.Logic.SmsSendResultGetAPI_After(response, this.Logic.smsLogic);
                
                this.Logic.UpdateData(this.Logic.smsEntry);

                // 正常に取得処理が実行できた場合のみtrue
                if (response[0].Contains("100"))
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
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

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
                var ret = this.Logic.GetGyousyaInfo(pGosyaCd, out catchErr);
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
                this.Logic.msgLogic.MessageBoxShow("E093", "");
                ren = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaChange", ex);
                this.Logic.msgLogic.MessageBoxShow("E245", "");
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
                var ret = this.Logic.GetGenbaInfo(pGenbaCd, pGyousyaCD, out catchErr);
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
                        var retGyo = this.Logic.GetGyousyaInfo(this.GYOUSHA_CD.Text, out catchErr);
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

        #endregion

        /// <summary>
        /// 日付取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.DATE_TO.Text = this.DATE_FROM.Text;
        }
    }
}
