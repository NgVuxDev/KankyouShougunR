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
using Shougun.Core.ExternalConnection.GenbamemoNyuryoku.Const;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.FileUpload.FileUploadCommon;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using Shougun.Core.FileUpload.FileUploadCommon.Utility;
using Shougun.Core.ExternalConnection.FileUpload;

namespace Shougun.Core.ExternalConnection.GenbamemoNyuryoku
{
    /// <summary>
    /// 現場メモ入力
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal GenbamemoNyuryoku.LogicClass logic = null;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// システムID
        /// </summary>
        internal string SystemId { get; set; }

        /// <summary>
        /// SEQ
        /// </summary>
        internal string SEQ { get; set; }

        /// <summary>
        /// 契約状況
        /// </summary>
        internal long keiyakuJyoukyouValue = 0;


        /// <summary>
        /// コントロールFocus時値格納
        /// </summary>
        public Dictionary<string, string> dicControl = new Dictionary<string, string>();

        /// <summary>
        /// フォーカス設定コントロール格納
        /// </summary>
        private Control focusControl;

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        internal Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        /// <summary>
        /// 伝票発行ポップアップがキャンセルされたかどうか判断するためのフラグ
        /// </summary>
        internal bool bCancelDenpyoPopup = false;

        /// <summary>
        /// キーイベントを保持用
        /// </summary>
        internal KeyEventArgs Key;

        /// <summary>
        /// フレームワークのフォーカス処理をするかしないか判断するフラグ
        /// </summary>
        internal bool isNotMoveFocusFW = false;

        /// <summary>
        /// 現場メモ番号
        /// </summary>
        public string GenbamemoNumber { get; set; }

        /// <summary>
        /// ファイルアップロード処理クラス
        /// </summary>
        public FileUploadLogic uploadLogic;

        /// <summary>
        /// システムID(ファイルアップロード用)
        /// </summary>
        internal string FileuploadSystemId { get; set; }

        /// <summary>
        /// SEQ(ファイルアップロード用)
        /// </summary>
        internal string FileuploadSEQ { get; set; }

        /// <summary>
        /// 遷移元画面からのパラメータEntry
        /// </summary>
        public T_GENBAMEMO_ENTRY paramEntry { get; set; }

        /// <summary>
        /// 遷移元画面からのパラメータWindows_ID
        /// </summary>
        public string winId { get; set; }

        /// <summary>
        /// 現場メモ一覧から複写で開かれたかを連携するフラグ
        /// </summary>
        public string hukushaFlg { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="systemId">システムID</param>
        /// <param name="SEQ">SEQ</param>
        /// <param name="entry">entry</param>
        /// <param name="winId">winId</param>
        /// <param name="hukushaFlg">現場メモ一覧から複写で開かれたかを連携するフラグ</param>
        public UIForm(WINDOW_TYPE windowType, string systemId, string SEQ, T_GENBAMEMO_ENTRY entry, string winId, string hukushaFlg)
            : base(WINDOW_ID.T_GENBA_MEMO_NYUURYOKU, windowType)
        {

            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.SystemId = systemId;
            this.SEQ = SEQ;
            this.paramEntry = entry;
            this.winId = winId;
            this.hukushaFlg = hukushaFlg;

            this.uploadLogic = new FileUploadLogic();

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
            base.OnLoad(e);

            //初期化、初期表示
            if (!this.logic.WindowInit()) { return; }

            // 初期フォーカス位置を設定します
            this.GENBAMEMO_NUMBER.Focus();

            // 現場メモ情報を画面に反映
            this.logic.SetValue();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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
        /// [F2]新規
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ChangeNewWindow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G741", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 新規モードに変更
                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    //base.OnLoad(e);

                    // 現場メモ番号クリア
                    this.GenbamemoNumber = string.Empty;

                    // システムIDクリア
                    this.SystemId = string.Empty;

                    // SEQクリア
                    this.SEQ = string.Empty;

                    // 変数をクリア
                    this.SystemId = string.Empty;
                    this.SEQ = string.Empty;
                    this.GenbamemoNumber = string.Empty;

                    // 表示初期化
                    if (!this.logic.WindowInit())
                    {
                        return;
                    }

                    // 画面項目の設定
                    this.logic.SetValue();

                    // 取引先、業者、現場はクリアする。
                    this.TORIHIKISAKI_CD.Text = string.Empty;
                    this.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                    this.GYOUSHA_CD.Text = string.Empty;
                    this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    this.GENBA_CD.Text = string.Empty;
                    this.GENBA_NAME_RYAKU.Text = string.Empty;

                    // 発生元無しで初期化する。
                    this.HASSEIMOTO_CD.Text = "1";
                    this.HASSEIMOTO_NAME.Text = "発生元無し";
                    this.HASSEIMOTO_NUMBER.Text = string.Empty;
                    this.HASSEIMOTO_MEISAI_NUMBER.Text = string.Empty;

                    // 発生元番号、発生元明細番号の制御
                    this.setEnableHasseimotoNumber(this.HASSEIMOTO_CD.Text);

                    // フォーカス設定
                    this.GENBAMEMO_NUMBER.Focus();
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F3]修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ChangeUpdateWindow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G742", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 修正モードに変更
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else if (r_framework.Authority.Manager.CheckAuthority("G742", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 参照モードに変更
                    this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                // 表示初期化
                if (!this.logic.WindowInit())
                {
                    return;
                }

                // 画面項目の設定
                this.logic.SetValue();

                // フォーカス設定
                this.GENBAMEMO_NUMBER.Focus();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F7]一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                bool retResult;
                // 現場メモ一覧を表示
                retResult = FormManager.OpenFormWithAuth("G742", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Regist();

            LogUtility.DebugMethodEnd();
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
        /// ファイルアップロードボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process1_Click(object sender, EventArgs e)
        {
            // ユーザ定義情報を取得
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            // ファイルアップロード参照先のフォルダを取得
            string serverPath = this.uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");

            // ファイルアップロード用DB接続を確立
            if (!this.uploadLogic.CanConnectDB())
            {
                this.logic.msgLogic.MessageBoxShowError("ファイルアップロード用DBに接続できませんでした。\n接続情報を確認してください。");
            }
            // システム個別設定入力の初期フォルダの設定有無をチェックする。
            else if (string.IsNullOrEmpty(serverPath) || !Directory.Exists(serverPath))
            {
                this.logic.msgLogic.MessageBoxShowError("システム個別設定入力 - ファイルアップロード - 初期フォルダへ\r\nフォルダ情報を入力してください。");
            }
            else
            {
                List<long> fileIdList = null;
                // ファイルアップロード画面に渡すシステムID
                string[] paramList = new string[2];

                // 新規モードの場合
                if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    if (this.logic.msgLogic.MessageBoxShowConfirm("ファイルアップロードの事前処理として登録処理を行います。\nよろしいですか？", MessageBoxDefaultButton.Button1)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        // 現場メモの登録処理を行う。
                        if (!this.Regist())
                        {
                            return;
                        }

                        if (this.SystemId == null)
                        {
                            return;
                        }

                        // ファイルアップロード画面を起動
                        paramList[0] = this.FileuploadSystemId;
                        paramList[1] = this.FileuploadSEQ;
                        FormManager.OpenFormModal("G730", fileIdList, WINDOW_ID.T_GENBA_MEMO_NYUURYOKU, paramList);
                    }
                }
                // 修正モードの場合
                else if (this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    // ファイルアップロード用の変数に設定
                    this.FileuploadSystemId = this.SystemId;
                    this.FileuploadSEQ = this.SEQ;

                    // 現場メモデータからファイルIDを取得する。
                    var fileLink = this.logic.fileLinkGenbamemoDao.GetDataBySystemId(this.FileuploadSystemId);
                    if (fileLink.Count != 0)
                    {
                        fileIdList = fileLink.Select(n => n.FILE_ID.Value).ToList();
                    }

                    // ファイルアップロード画面を起動
                    paramList[0] = this.FileuploadSystemId;
                    paramList[1] = this.FileuploadSEQ;
                    FormManager.OpenFormModal("G730", fileIdList, WINDOW_ID.T_GENBA_MEMO_NYUURYOKU, paramList);
                }
            }
        }

        #endregion

        #region 登録処理
        private bool Regist()
        {
            LogUtility.DebugMethodStart();

            int count = 0;

            // 必須チェック
            if (!this.logic.RegistCheck())
            {
                return false;
            }

            // モード別更新処理
            switch (this.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // 登録用Entity作成
                    this.logic.CreateEntity();
                    // 登録用DetailEntity作成
                    this.logic.CreateDetailEntity(this.logic.genbamemoEntry.SYSTEM_ID, int.Parse(this.logic.genbamemoEntry.SEQ.ToString()));

                    count = this.logic.RegistData();
                    break;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 削除用Entity作成
                    this.logic.CreateDelEntryEntity();

                    // 登録用Entity作成
                    this.logic.CreateEntity();

                    // 登録用DetailEntity作成
                    this.logic.CreateDetailEntity(this.logic.genbamemoEntry.SYSTEM_ID, int.Parse(this.logic.genbamemoEntry.SEQ.ToString()));

                    count = this.logic.UpdateData();
                    break;
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    // 削除用Entity作成
                    this.logic.CreateDelEntryEntity();

                    count = this.logic.LogicalDeleteData();
                    break;
            }

            
            // エラーしたら抜ける
            if (count == 0)
            {
                return false;
            }

            if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                this.logic.msgLogic.MessageBoxShow("I001", "削除");
            }
            else if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.logic.msgLogic.MessageBoxShow("I001", "登録");
            }
            else if (this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                this.logic.msgLogic.MessageBoxShow("I001", "更新");
            }

            // ファイルアップロード用の変数に設定
            this.FileuploadSystemId = this.logic.genbamemoEntry.SYSTEM_ID.ToString();
            this.FileuploadSEQ = this.logic.genbamemoEntry.SEQ.ToString();

            // 発生元と発生元番号を複写した状態で新規モードに変更する。
            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

            // 変数をクリア
            this.SystemId = string.Empty;
            this.SEQ = string.Empty;
            this.GenbamemoNumber = string.Empty;
            // 再表示
            this.logic.WindowInit();
            this.logic.SetValue();

            // 保持した発生元と発生元番号を設定
            this.HASSEIMOTO_CD.Text = "1";
            this.HASSEIMOTO_NAME.Text = "発生元無し";
            this.HASSEIMOTO_NUMBER.Text = string.Empty;
            this.HASSEIMOTO_MEISAI_NUMBER.Text = string.Empty;

            // 発生元番号、発生元明細番号の制御
            this.setEnableHasseimotoNumber(this.HASSEIMOTO_CD.Text);

            // 取引先、業者、現場はクリアする。
            this.TORIHIKISAKI_CD.Text = string.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.GYOUSHA_CD.Text = string.Empty;
            this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.GENBA_CD.Text = string.Empty;
            this.GENBA_NAME_RYAKU.Text = string.Empty;

            LogUtility.DebugMethodEnd();

            return true;
        }
        #endregion

        /// <summary>
        /// EditingControlShowingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyDown -= this.DetailIchiranEditingControl_KeyDown;
                e.Control.KeyDown += this.DetailIchiranEditingControl_KeyDown;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 一覧編集ボックスキーダウン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiranEditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            this.logic.CheckPopup(e);
        }

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

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <returns>object</returns>
        private string GetCellValue(DataGridViewCell obj)
        {
            if (obj.Value == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.Value.ToString();
            }
        }

        /// <summary>
        /// コントロールに入力されていた値を取得します
        /// </summary>
        /// <param name="key">コントロール名</param>
        /// <returns>前回値</returns>
        internal String GetBeforeText(string key)
        {
            LogUtility.DebugMethodStart(key);

            string ret = this.dicControl.Where(r => r.Key == key).Select(r => r.Value).DefaultIfEmpty(String.Empty).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 現場メモ分類
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBAMEMO_BUNRUI_CD_Validated(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetGenbamemoBunrui())
            {
                e.Cancel = true;
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 発生元
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HASSEIMOTO_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hasseimotoCd = this.HASSEIMOTO_CD.Text;

            // 前回値と同じ場合、処理しない。
            if (this.logic.beforeHsseimotoCd.Equals(hasseimotoCd))
            {
                return;
            }

            string hasseimotoName = "";
            switch(hasseimotoCd)
            {
                case "1":
                    hasseimotoName = "発生元無し";

                    break;
                case "2":
                    hasseimotoName = "収集受付";

                    break;
                case "3":
                    hasseimotoName = "出荷受付";

                    break;
                case "4":
                    hasseimotoName = "持込受付";

                    break;
                case "5":
                    hasseimotoName = "定期配車";

                    break;
                default:
                    break;
            }
            this.HASSEIMOTO_NAME.Text = hasseimotoName;

            this.setEnableHasseimotoNumber(hasseimotoCd);

            // 発生元番号、発生元明細番号をクリアする。
            this.HASSEIMOTO_NUMBER.Text = string.Empty;
            this.HASSEIMOTO_MEISAI_NUMBER.Text = string.Empty;

            // 前回値に保持する。
            this.logic.beforeHsseimotoCd = hasseimotoCd;
            this.logic.beforeHsseimotoNumber = string.Empty;
            this.logic.beforeHsseimotoMeisaiNumber = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 発生元による発生元番号、発生元明細番号の活性・非活性制御
        /// </summary>
        public void setEnableHasseimotoNumber(string hasseimotoCd)
        {
            switch (hasseimotoCd)
            {
                case "1":
                    // 発生元番号、発生元明細番号を非活性にする。
                    this.HASSEIMOTO_NUMBER.Enabled = false;
                    this.HASSEIMOTO_MEISAI_NUMBER.Enabled = false;

                    break;
                case "2":
                case "3":
                case "4":
                    // 発生元明細番号を非活性にする。
                    this.HASSEIMOTO_NUMBER.Enabled = true;
                    this.HASSEIMOTO_MEISAI_NUMBER.Enabled = false;

                    break;
                case "5":
                    // 発生元番号、発生元明細番号を活性にする。
                    this.HASSEIMOTO_NUMBER.Enabled = true;
                    this.HASSEIMOTO_MEISAI_NUMBER.Enabled = true;

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 発生元番号　変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HASSEIMOTO_NUMBER_TextChanged(object sender, EventArgs e)
        {
            // 発生元明細番号をクリアする。
            this.HASSEIMOTO_MEISAI_NUMBER.Text = string.Empty;
        }

        /// <summary>
        /// 発生元番号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HASSEIMOTO_NUMBER_Validated(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.checkHasseimotoNumber())
            {
                e.Cancel = true;
                this.HASSEIMOTO_NUMBER.Text = this.logic.beforeHsseimotoNumber;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 発生元明細番号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HASSEIMOTO_MEISAI_NUMBER_Validated(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.checkHasseimotoMeisaiNumber())
            {
                e.Cancel = true;
                this.HASSEIMOTO_MEISAI_NUMBER.Text = this.logic.beforeHsseimotoMeisaiNumber;
            }

            LogUtility.DebugMethodEnd();
        }

        #region 取引先更新後処理

        /// <summary>
        /// 取引先CDのバリデーションが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void TORIHIKISAKI_CD_Validated(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetTorihikisaki())
            {
                e.Cancel = true;
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先検索ポップアップから戻ってきたときに処理します
        /// </summary>
        public void TorihikisakiBtnPopupAfterExecute(object sender, DialogResult result)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, result);

                if (result != DialogResult.Yes && result != DialogResult.OK)
                    return;

                var before = this.GetBeforeText(this.TORIHIKISAKI_CD.Name);

                if (this.logic.isInputError || this.TORIHIKISAKI_CD.Text != before)
                {
                    this.SetTorihikisaki();
                }

                // フォーカスセット
                this.TORIHIKISAKI_CD.Focus();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, result);
            }
        }
        #endregion

        #region 業者更新後処理
        /// <summary>
        /// 業者CDのバリデーションが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_Validated(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetGyousha())
            {
                e.Cancel = true;
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者検索ポップアップから戻ってきたときに処理します
        /// </summary>
        public void GyoushaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // フォーカスセット
                this.GYOUSHA_CD.Focus();

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 現場更新後処理
        /// <summary>
        /// 現場CDのバリデーションが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_Validated(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetGenba())
            {
                e.Cancel = true;
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場検索ポップアップから戻ってきたときに処理します
        /// </summary>
        public void GenbaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var gyoushaCd = this.GYOUSHA_CD.Text;
                bool catchErr = true;

                var gyousha = this.logic.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (null != gyousha)
                {
                    this.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;

                    var genba = this.logic.GetGenba(this.GENBA_CD.Text, gyoushaCd, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                }

                this.GENBA_CD.Focus();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("GenbaBtnPopupMethod", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Control_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 入力で例外が発生した場合はその前回値を保持しない
                //if (!this.logic.isInputError)
                //{
                Type type = sender.GetType();
                if (type.Name == "CustomAlphaNumTextBox")
                {
                    CustomAlphaNumTextBox ctrl = (CustomAlphaNumTextBox)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }

                    // イベント削除
                    //ctrl.Enter -= this.Control_Enter;
                }
                else if (type.Name == "CustomNumericTextBox2")
                {
                    CustomNumericTextBox2 ctrl = (CustomNumericTextBox2)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }

                    // イベント削除
                    //ctrl.Enter -= this.Control_Enter;
                }
                else if (type.Name == "CustomPopupOpenButton")
                {
                    CustomPopupOpenButton ctrl = (CustomPopupOpenButton)sender;
                    // テキスト名を取得
                    String textName = this.logic.GetTextName(ctrl.Name);
                    Control control = controlUtil.FindControl(this, textName);

                    if (dicControl.ContainsKey(textName))
                    {
                        dicControl[textName] = control.Text;
                    }
                    else
                    {
                        dicControl.Add(textName, control.Text);
                    }

                    // イベント削除
                    //control.Enter -= this.Control_Enter;
                }
                else if (type.Name == "CustomDateTimePicker")
                {
                    CustomDateTimePicker ctrl = (CustomDateTimePicker)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public bool SetGenbamemoBunrui()
        {
            bool ret = true;
            try
            {
                if (!this.logic.CheckGenbamemoBunrui())
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGenbamemoBunrui", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGenbamemoBunrui", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 取引先CDに関連する項目をセットする
        /// </summary>
        public bool SetTorihikisaki()
        {
            bool ret = true;
            try
            {
                if (!this.logic.CheckTorihikisaki())
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetTorihikisaki", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikisaki", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 業者CDに関連する項目をセットする
        /// </summary>
        public bool SetGyousha()
        {
            bool ret = true;
            try
            {
                if (!this.logic.CheckGyousha())
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGyousha", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyousha", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 現場CDに関連する項目をセットする
        /// </summary>
        public bool SetGenba()
        {
            bool ret = true;
            try
            {
                if (!this.logic.CheckGenba())
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGenba", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGenba", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 現場ポップアップ表示後の処理
        /// </summary>
        public void GenbaPopupAfter()
        {
            
        }

        /// <summary>
        /// 現場ポップアップ表示後の処理
        /// </summary>
        public void GenbaPopupAfterExecute(object sender, DialogResult result)
        {
            if (result != DialogResult.Yes && result != DialogResult.OK)
                return;
        }

        #region 現場メモ番号「前」
        /// <summary>
        /// 現場メモ番号「前」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBAMEMO_PREVIOUS_BUTTON_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) ||
                    r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    String previousGenbamemoNumber;
                    String tableName = "T_GENBAMEMO_ENTRY";
                    String fieldName = "GENBAMEMO_NUMBER";
                    String GenbamemoNumber = this.GENBAMEMO_NUMBER.Text;
                    // 前の現場メモ番号を取得
                    bool catchErr = true;
                    previousGenbamemoNumber = this.logic.GetPreviousNumber(tableName, fieldName, GenbamemoNumber, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (previousGenbamemoNumber == "")
                    {
                        this.logic.msgLogic.MessageBoxShow("E045");
                        return;
                    }
                    // 現場メモ番号を設定
                    this.GENBAMEMO_NUMBER.Text = previousGenbamemoNumber;
                    // モードを初期化
                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    // 現場メモ番号更新後処理
                    this.GENBAMEMO_NUMBER_Validated(sender, e);
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region 現場メモ番号「次」
        /// <summary>
        /// 現場メモ番号「次」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBAMEMO_NEXT_BUTTON_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) ||
                    r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    String nextGenbamemoNumber;
                    String tableName = "T_GENBAMEMO_ENTRY";
                    String fieldName = "GENBAMEMO_NUMBER";
                    String GenbamemoNumber = this.GENBAMEMO_NUMBER.Text;
                    // 次の受付番号を取得
                    bool catchErr = true;
                    nextGenbamemoNumber = this.logic.GetNextNumber(tableName, fieldName, GenbamemoNumber, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (nextGenbamemoNumber == "")
                    {
                        this.logic.msgLogic.MessageBoxShow("E045");
                        return;
                    }
                    // 現場メモ番号を設定
                    this.GENBAMEMO_NUMBER.Text = nextGenbamemoNumber;
                    // モードを初期化
                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    // 現場メモ番号更新後処理
                    this.GENBAMEMO_NUMBER_Validated(sender, e);
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 現場メモ番号更新後処理
        /// <summary>
        /// 現場メモ番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBAMEMO_NUMBER_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 変更なしの場合
                if (this.dicControl.ContainsKey("GENBAMEMO_NUMBER") &&
                    this.dicControl["GENBAMEMO_NUMBER"].Equals(this.GENBAMEMO_NUMBER.Text))
                {
                    return;
                }

                // 新規モード and 未入力　の場合
                if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && string.IsNullOrEmpty(this.GENBAMEMO_NUMBER.Text))
                {
                    return;
                }
                else if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 受付番号をセット
                    this.GenbamemoNumber = this.GENBAMEMO_NUMBER.Text;

                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                        !r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        this.logic.msgLogic.MessageBoxShow("E158", "修正");

                        // 受付番号の初期化
                        this.GenbamemoNumber = string.Empty;
                        this.GENBAMEMO_NUMBER.Text = this.GenbamemoNumber;

                        // 画面初期化
                        if (!this.logic.WindowInit())
                        {
                            // イベント削除
                            this.GENBAMEMO_NUMBER.Enter -= this.Control_Enter;
                            // フォーカス設定
                            this.GENBAMEMO_NUMBER.Focus();

                            return;
                        }
                        
                        // 画面項目に設定
                        this.logic.SetValue();

                        // 処理終了
                        return;
                    }

                    // 新規モード and 受付番号データが存在しない場合
                    int count = this.logic.Search();
                    if (count == -1)
                    {
                        return;
                    }
                    else if (count == 0)
                    {
                        // メッセージ表示
                        this.logic.msgLogic.MessageBoxShow("E045");

                        // 受付番号の初期化
                        this.GENBAMEMO_NUMBER.Text = this.GenbamemoNumber;
                        // イベント削除
                        this.GENBAMEMO_NUMBER.Enter -= this.Control_Enter;

                        this.GENBAMEMO_NUMBER.Focus();

                        this.GENBAMEMO_NUMBER.Enter += this.Control_Enter;
                        // 処理終了
                        return;
                    }
                    else
                    {
                        if (r_framework.Authority.Manager.CheckAuthority("G741", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 編集モードに変更
                            this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        }
                        else
                        {
                            // ここに到達する前に修正＆参照なしをチェックしているので参照権限チェックは行っていない

                            // 参照モードに変更
                            this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        }

                        // 画面初期化
                        if (!this.logic.WindowInit())
                        {
                            return;
                        }
                        // 画面項目に設定
                        this.logic.SetValue();
                    }
                }

                // 編集 or 参照モード処理を行う
                this.GenbamemoNumber = this.GENBAMEMO_NUMBER.Text;
                if (!this.logic.WindowInit())
                {
                    // イベント削除
                    this.GENBAMEMO_NUMBER.Enter -= this.Control_Enter;
                    // フォーカス設定
                    this.GENBAMEMO_NUMBER.Focus();
                    return;
                }

                // 画面項目に設定
                this.logic.SetValue();

                // 前・次ﾎﾞﾀﾝクリック場合、値退避
                if (this.dicControl.ContainsKey("GENBAMEMO_NUMBER"))
                {
                    this.dicControl["GENBAMEMO_NUMBER"] = this.GenbamemoNumber;
                }
                else
                {
                    this.dicControl.Add("GENBAMEMO_NUMBER", this.GenbamemoNumber);
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        private void Hasseimoto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.logic.CheckListPopup();
            }
        }

        /// <summary>
        /// 表題のKeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hyoudai_KeyDown(object sender, KeyEventArgs e)
        {
            // 表題に入力途中かつ表題に既に値が入っている場合は、表題マスタ検索画面を表示しない。
            if (e.KeyCode == Keys.ProcessKey || e.KeyCode == Keys.Space)
            {
                if (string.IsNullOrWhiteSpace(this.HYOUDAI.Text))
                {
                    this.HYOUDAI.Text = string.Empty;
                    this.logic.SetHyoudaiEvent();
                }
                else
                {
                    this.logic.DeleteHyoudaiEvent();
                }
            }
            else
            {
                this.logic.DeleteHyoudaiEvent();
            }
        }

    }
}
