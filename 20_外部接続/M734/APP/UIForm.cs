using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranCommon.APP;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
{
    /// <summary>
    /// 電子契約履歴一覧
    /// </summary>
    public partial class UIForm : IchiranSuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass shoukaiLogic = null;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// 契約状況
        /// </summary>
        internal long keiyakuJyoukyouValue = 0;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.DENSHI_KEIYAKU_SAISHIN_SHOUKAI, false)
        {
            InitializeComponent();
            //this.ShainCd = SystemProperty.Shain.CD; // 社員CDを取得すること
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

            if (this.shoukaiLogic == null)
            {
                this.shoukaiLogic = new LogicClass(this);

                //初期化、初期表示
                if (!this.shoukaiLogic.WindowInit()) { return; }

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 151);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(4, 173);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 200);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 240);

                // 汎用検索機能が未実装の為、汎用検索は非表示
                this.searchString.Visible = false;

                this.DATE_SELECT.Text = "6";

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }

            this.PatternReload();

            //一覧内のチェックボックスの設定
            this.shoukaiLogic.HeaderCheckBoxSupport();

            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            this.customSearchHeader1.Visible = true;

            this.HideKeyColumns();
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

        #region F1 電子契約照会

        /// <summary>
        /// 電子契約照会(F1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // クライアントIDが設定されているか確認する。
                if (!this.shoukaiLogic.CheckClientId()) { return; }

                // 電子契約照会条件を開く
                var callForm = new ShoukaiJouken();
                DialogResult dr = callForm.ShowDialog(this);

                // 照会処理をして返ってきた場合のみ検索を実行
                if (dr == DialogResult.OK)
                {
                    this.bt_func8_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func1_Click", ex);
                this.shoukaiLogic.msgLogic.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F6 CSV

        /// <summary>
        /// CSV(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            CSVExport csvLogic = new CSVExport();
            DENSHU_KBN id = this.DenshuKbn;
            csvLogic.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, id.ToTitleString(), this);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F7 条件クリア

        /// <summary>
        /// 条件クリア(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.shoukaiLogic.ClearSearchJyouken();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F8 検索
        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 日付のチェック
            if (!this.shoukaiLogic.CheckDate()) { return; }

            // 検索条件のチェック
            if (!this.shoukaiLogic.CheckCondition()) { return; }

            if (this.shoukaiLogic.Search() == 0)
            {
                this.shoukaiLogic.msgLogic.MessageBoxShowInformation("検索結果が存在しませんでした。");
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F9 電子契約登録

        /// <summary>
        /// 登録処理(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.shoukaiLogic.CheckClientId()) { return; }

            // 一覧のチェックボックスを確認する。
            bool checkBoxFlg = this.shoukaiLogic.CheckForCheckBox();

            // 登録前チェックを行う。
            bool checkBeforeFlg = this.shoukaiLogic.CheckBeforeRegistData();

            // １つでもチェックされている、かつ登録前チェックがOKの場合、登録処理を行う。
            if (checkBoxFlg && checkBeforeFlg)
            {
                // 登録処理
                if (this.shoukaiLogic.RegistData())
                {
                    this.shoukaiLogic.msgLogic.MessageBoxShowInformation("登録処理が完了しました。");
                    this.bt_func8_Click(sender, e);
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F10 並び替え

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customSortHeader1.ShowCustomSortSettingDialog();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F11 フィルタ

        /// <summary>
        /// フィルタ(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customSearchHeader1.ShowCustomSearchSettingDialog();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F12 閉じる

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customDataGridView1.DataSource = "";

            if (this.shoukaiLogic.parentForm != null)
            {
                this.shoukaiLogic.parentForm.Close();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region SubF1 パターン一覧

        /// <summary>
        /// パターン一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.shoukaiLogic.OpenPatternIchiran();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region SubF2 ファイルダウンロード

        /// <summary>
        /// ファイルダウンロード
        /// </summary>
        internal void bt_process2_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.shoukaiLogic.CheckClientId()) { return; }

            // 一覧のチェックボックスを確認する。
            bool checkBoxFlg = this.shoukaiLogic.CheckForCheckBox();

            // ダウンロード前チェックを行う。
            bool checkBeforeFlg = this.shoukaiLogic.CheckBeforeFileDownload();

            // １つでもチェックされている、かつダウンロード前チェックがOKの場合、契約書のダウンロードを行う。
            if (checkBoxFlg && checkBeforeFlg)
            {
                bool ret = this.shoukaiLogic.FileDownload();
                if (ret)
                {
                    this.shoukaiLogic.msgLogic.MessageBoxShow("I001", "ダウンロード");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region SubF3 契約状況取得

        /// <summary>
        /// 契約状況取得
        /// </summary>
        internal void bt_process3_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.shoukaiLogic.CheckClientId())
            {
                return;
            }

            // 一覧のチェックボックスを確認する。
            bool checkBoxFlg = this.shoukaiLogic.CheckForCheckBox();

            // １つでもチェックされている場合、契約状況の取得を行う。
            if (checkBoxFlg)
            {
                bool ret = this.shoukaiLogic.KeiyakuJyoukyouUpdate();
                if (ret)
                {
                    this.shoukaiLogic.msgLogic.MessageBoxShow("I001", "取得");

                    // 再検索
                    this.shoukaiLogic.Search();
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// 合意締結証明書ﾀﾞｳﾝﾛｰﾄﾞ
        /// </summary>
        internal void bt_process4_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.shoukaiLogic.CheckClientId())
            {
                return;
            }

            // 一覧のチェックボックスを確認する。
            bool checkBoxFlg = this.shoukaiLogic.CheckForCheckBox();

            // ダウンロード前チェックを行う。
            bool checkBeforeFlg = this.shoukaiLogic.CheckBeforeCertificateDownload();

            // １つでもチェックされている場合、合意締結証明書のダウンロードを行う。
            if (checkBoxFlg && checkBeforeFlg)
            {
                bool ret = this.shoukaiLogic.CertificateDownload();
                if (ret)
                {
                    this.shoukaiLogic.msgLogic.MessageBoxShow("I001", "ダウンロード");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 各項目のイベント処理

        #region 契約状況

        /// <summary>
        /// 契約状況のキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Keiyaku_Jyoukyou_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.shoukaiLogic.CreateKeiyakuJyoukyouPopup();
            }
        }

        /// <summary>
        /// 契約状況のフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIYAKU_JYOUKYOU_CD_Validated(object sender, EventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.KEIYAKU_JYOUKYOU_CD.Text))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = string.Empty;
                return;
            }

            string strKeiyakuJyoukyouCd = this.KEIYAKU_JYOUKYOU_CD.Text;
            if (strKeiyakuJyoukyouCd.Equals("0"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_0;
            }
            else if (strKeiyakuJyoukyouCd.Equals("1"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_1;
            }
            else if (strKeiyakuJyoukyouCd.Equals("2"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_2;
            }
            else if (strKeiyakuJyoukyouCd.Equals("3"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_3;
            }
            else if (strKeiyakuJyoukyouCd.Equals("4"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_4;
            }
            else
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = string.Empty;
            }
        }

        #endregion

        #region 日付

        /// <summary>
        /// 日付選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_SELECT_TextChanged(object sender, EventArgs e)
        {
            if (this.DATE_SELECT.Text != "7")
            {
                this.DATE_FROM.Enabled = true;
                this.DATE_TO.Enabled = true;
            }
            switch (this.DATE_SELECT.Text)
            {
                case "1":
                    this.labelInfo.Text = "作成日";
                    this.DATE_FROM.Tag = "[作成日]開始日を入力してください";
                    this.DATE_TO.Tag = "[作成日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[作成日]開始日";
                    this.DATE_TO.DisplayItemName = "[作成日]終了日";
                    break;
                case "2":
                    this.labelInfo.Text = "更新日";
                    this.DATE_FROM.Tag = "[更新日]開始日を入力してください";
                    this.DATE_TO.Tag = "[更新日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[更新日]開始日";
                    this.DATE_TO.DisplayItemName = "[更新日]終了日";
                    break;
                case "3":
                    this.labelInfo.Text = "契約締結日";
                    this.DATE_FROM.Tag = "[契約締結日]開始日を入力してください";
                    this.DATE_TO.Tag = "[契約締結日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[契約締結日]開始日";
                    this.DATE_TO.DisplayItemName = "[契約締結日]終了日";
                    break;
                case "4":
                    this.labelInfo.Text = "契約開始日";
                    this.DATE_FROM.Tag = "[契約開始日]開始日を入力してください";
                    this.DATE_TO.Tag = "[契約開始日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[契約開始日]開始日";
                    this.DATE_TO.DisplayItemName = "[契約開始日]終了日";
                    break;
                case "5":
                    this.labelInfo.Text = "契約終了日";
                    this.DATE_FROM.Tag = "[契約終了日]開始日を入力してください";
                    this.DATE_TO.Tag = "[契約終了日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[契約終了日]開始日";
                    this.DATE_TO.DisplayItemName = "[契約終了日]終了日";
                    break;
                case "6":
                    this.labelInfo.Text = "日付なし";
                    this.DATE_FROM.Tag = string.Empty;
                    this.DATE_TO.Tag = string.Empty;
                    this.DATE_FROM.DisplayItemName = string.Empty;
                    this.DATE_TO.DisplayItemName = string.Empty;
                    this.DATE_FROM.Value = null;
                    this.DATE_TO.Value = null;
                    this.DATE_FROM.Enabled = false;
                    this.DATE_TO.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// 検索条件の日付範囲(To)のダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.DATE_TO.Text = this.DATE_FROM.Text;
        }

        #endregion

        #region 検索条件1～4

        private void CONDITION_VALUE1_Enter(object sender, EventArgs e)
        {
            CONDITION_VALUE1.ImeMode = ImeMode.Hiragana;
        }

        private void CONDITION_VALUE2_Enter(object sender, EventArgs e)
        {
            CONDITION_VALUE2.ImeMode = ImeMode.Hiragana;
        }

        private void CONDITION_VALUE3_Enter(object sender, EventArgs e)
        {
            CONDITION_VALUE3.ImeMode = ImeMode.Hiragana;
        }

        private void CONDITION_VALUE4_Enter(object sender, EventArgs e)
        {
            CONDITION_VALUE4.ImeMode = ImeMode.Hiragana;
        }

        #endregion

        #endregion

        #region その他処理

        /// <summary>
        /// 指定した明細列を非表示に
        /// </summary>
        private void HideKeyColumns()
        {
            if (this.customDataGridView1.DataSource != null && this.Table != null)
            {
                foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
                {
                    if (col.Name == ConstCls.KEY_ID1 ||
                        col.Name == ConstCls.HIDDEN_SEND_MESSAGE ||
                        col.Name == ConstCls.HIDDEN_SEND_TITLE ||
                        col.Name == ConstCls.HIDDEN_RENKEI ||
                        col.Name == ConstCls.HIDDEN_AUTO_UPDATE ||
                        col.Name == ConstCls.HIDDEN_KEIYAKU_JYOUKYOU ||
                        col.Name == ConstCls.HIDDEN_SHANAI_BIKO ||
                        col.Name == ConstCls.HIDDEN_KEIYAKUSHO_KEIYAKU_DATE ||
                        col.Name == ConstCls.HIDDEN_KEIYAKUSHO_CREATE_DATE ||
                        col.Name == ConstCls.HIDDEN_YUUKOU_BEGIN ||
                        col.Name == ConstCls.HIDDEN_YUUKOU_END ||
                        col.Name == ConstCls.HIDDEN_FILE_ID ||
                        col.Name == ConstCls.HIDDEN_FILE_NAME ||
                        col.Name == ConstCls.HIDDEN_DELETE_FLG)
                    {
                        col.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ShowData()
        {
            if (!this.DesignMode)
            {
                if (this.Table != null && this.PatternNo != 0)
                {
                    // 明細に表示
                    this.logic.CreateDataGridView(this.Table);

                    // 指定した明細行を非表示にする。
                    this.HideKeyColumns();

                    // 明細のチェックボックスを編集可能にする。
                    this.customDataGridView1.Columns[ConstCls.CELL_CHECKBOX].ReadOnly = false;
                }
            }
        }

        #endregion


    }
}
