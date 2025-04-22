using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranCommon.APP;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuRirekiIchiran
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
        private DenshiKeiyakuRirekiIchiran.LogicClass rirekiLogic = null;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// 前回業者コード
        /// </summary>
        internal string beforGyousaCD = string.Empty;

        /// <summary>
        /// 前回現場コード
        /// </summary>
        internal string beforeGenbaCD = string.Empty;

        /// <summary>
        /// 前回運搬業者コード
        /// </summary>
        internal string beforeUnpanGyousaCD = string.Empty;

        /// <summary>
        /// 前回処分受託者（処分）コード
        /// </summary>
        internal string beforeShobunJyutakushaShobunCD = string.Empty;

        /// <summary>
        /// 前回処分事業場コード
        /// </summary>
        internal string beforeShobunGenbaCD = string.Empty;

        /// <summary>
        /// 前回処分受託者（最終）コード
        /// </summary>
        internal string beforeShobunJyutakushaSaishuCD = string.Empty;

        /// <summary>
        /// 前回最終処分場コード
        /// </summary>
        internal string beforeSaishuShobunCD = string.Empty;

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
            : base(DENSHU_KBN.DENSHI_KEIYAKU_RIREKI_ICHIRAN, false)
        {
            InitializeComponent();
            this.ShainCd = SystemProperty.Shain.CD; // 社員CDを取得すること
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
            
            if (this.rirekiLogic == null)
            {
                this.rirekiLogic = new LogicClass(this);

                //初期化、初期表示
                if (!this.rirekiLogic.WindowInit()) { return; }

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 151);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(4, 173);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 200);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 240);

                // 汎用検索機能が未実装の為、汎用検索は非表示
                this.searchString.Visible = false;

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }

            this.PatternReload();

            //一覧内のチェックボックスの設定
            this.rirekiLogic.HeaderCheckBoxSupport();

            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            this.customSearchHeader1.Visible = false;

            this.DATE_SELECT.Text = "7";

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

        /// <summary>
        /// 修正(F3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.rirekiLogic.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.rirekiLogic.CheckClientId())
            {
                return;
            }

            DataGridViewRow row = this.customDataGridView1.CurrentRow;
            if (row == null)
            {
                this.rirekiLogic.msgLogic.MessageBoxShowError("データが選択されていません。");
                return;
            }

            // クライアントIDを取得する。
            this.rirekiLogic.GetClientID();

            // 選択行からドキュメントIDを取得する。
            if (row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value != null)
            {
                this.rirekiLogic.documentId = row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value.ToString();
            }

            // 契約状況を取得する。
            bool keiyakuJyoukyouRet = this.rirekiLogic.GetKeiyakuJyoukyou();
            if (keiyakuJyoukyouRet)
            {
                // 「2:締結済」、「3:取消、または却下」の場合、エラーとする。
                if (this.keiyakuJyoukyouValue == 2)
                {
                    this.rirekiLogic.msgLogic.MessageBoxShowError("締結済みのファイルが含まれているため、削除処理が行えません。\n削除が必要な場合は、覚書にて再締結を行ってください。");
                }
                else if (this.keiyakuJyoukyouValue == 3)
                {
                    this.rirekiLogic.msgLogic.MessageBoxShowError("既に取消、または却下されているため、削除処理が行えません。");
                }
                else
                {
                    this.rirekiLogic.OpenWindow(WINDOW_TYPE.DELETE_WINDOW_FLAG);
                }
            }

            LogUtility.DebugMethodEnd();
        }

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

        /// <summary>
        /// 条件クリア(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.rirekiLogic.ClearSearchJyouken();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.rirekiLogic.Search();

            LogUtility.DebugMethodEnd();
        }

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

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customDataGridView1.DataSource = "";

            if (this.rirekiLogic.parentForm != null)
            {
                this.rirekiLogic.parentForm.Close();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターン一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.rirekiLogic.OpenPatternIchiran();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 契約書ﾀﾞｳﾝﾛｰﾄﾞ
        /// </summary>
        internal void bt_process2_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.rirekiLogic.CheckClientId())
            {
                return;
            }

            // 一覧のチェックボックスを確認する。
            bool checkBoxFlg = this.rirekiLogic.CheckForCheckBox();

            // ダウンロード前チェックを行う。
            bool checkBeforeFlg = this.rirekiLogic.CheckBeforeFileDownload();

            // １つでもチェックされている、かつダウンロード前チェックがOKの場合、契約書のダウンロードを行う。
            if (checkBoxFlg && checkBeforeFlg)
            {
                bool ret = this.rirekiLogic.FileDownload();
                if (ret)
                {
                    this.rirekiLogic.msgLogic.MessageBoxShow("I001", "ダウンロード");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 契約状況取得
        /// </summary>
        internal void bt_process3_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.rirekiLogic.CheckClientId())
            {
                return;
            }

            // 一覧のチェックボックスを確認する。
            bool checkBoxFlg = this.rirekiLogic.CheckForCheckBox();

            // １つでもチェックされている場合、契約状況の取得を行う。
            if (checkBoxFlg)
            {
                bool ret = this.rirekiLogic.KeiyakuJyoukyouUpdate();
                if (ret)
                {
                    this.rirekiLogic.msgLogic.MessageBoxShow("I001", "取得");

                    // 再検索
                    this.rirekiLogic.Search();
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 合意締結証明書ﾀﾞｳﾝﾛｰﾄﾞ
        /// </summary>
        internal void bt_process4_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.rirekiLogic.CheckClientId())
            {
                return;
            }

            // 一覧のチェックボックスを確認する。
            bool checkBoxFlg = this.rirekiLogic.CheckForCheckBox();

            // ダウンロード前チェックを行う。
            bool checkBeforeFlg = this.rirekiLogic.CheckBeforeCertificateDownload();

            // １つでもチェックされている場合、合意締結証明書のダウンロードを行う。
            if (checkBoxFlg && checkBeforeFlg)
            {
                bool ret = this.rirekiLogic.CertificateDownload();
                if (ret)
                {
                    this.rirekiLogic.msgLogic.MessageBoxShow("I001", "ダウンロード");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 各項目のイベント処理

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
                        col.Name == ConstCls.KEY_ID2 ||
                        col.Name == ConstCls.HIDDEN_DOCUMENT_ID ||
                        col.Name == ConstCls.HIDDEN_DELETE_FLG ||
                        col.Name == ConstCls.HIDDEN_FILE_ID ||
                        col.Name == ConstCls.HIDDEN_FILE_PATH)
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

        /// <summary>
        /// 契約書種類のキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeiyakushoShuruiCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.rirekiLogic.CheckListPopup(1);
            }
        }

        /// <summary>
        /// 契約書種類の検索ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Keiyakusho_MouseClick(object sender, MouseEventArgs e)
        {
            this.rirekiLogic.CheckListPopup(1);
        }

        /// <summary>
        /// 許可証種類のキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kyokashou_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.rirekiLogic.CheckListPopup(2);
            }
        }

        /// <summary>
        /// 契約状況のキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Keiyaku_Jyoukyou_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.rirekiLogic.CheckListPopup(3);
            }
        }

        /// <summary>
        /// 契約書種類のフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIYAKUSHO_SHURUI_CD_Validated(object sender, EventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.KEIYAKUSHO_SHURUI_CD.Text))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = string.Empty;
                return;
            }

            string strKeiyakushoShuruiCd = this.KEIYAKUSHO_SHURUI_CD.Text;
            if (strKeiyakushoShuruiCd.Equals("11"))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = ConstCls.KEIYAKUSHO_SHURUI_NAME_11;
            }
            else if (strKeiyakushoShuruiCd.Equals("12"))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = ConstCls.KEIYAKUSHO_SHURUI_NAME_12;
            }
            else if (strKeiyakushoShuruiCd.Equals("13"))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = ConstCls.KEIYAKUSHO_SHURUI_NAME_13;
            }
            else if (strKeiyakushoShuruiCd.Equals("21"))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = ConstCls.KEIYAKUSHO_SHURUI_NAME_21;
            }
            else if (strKeiyakushoShuruiCd.Equals("22"))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = ConstCls.KEIYAKUSHO_SHURUI_NAME_22;
            }
            else if (strKeiyakushoShuruiCd.Equals("23"))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = ConstCls.KEIYAKUSHO_SHURUI_NAME_23;
            }
            else
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 許可証種類のフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KYOKASHOU_SHURUI_CD_Validated(object sender, EventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.KYOKASHOU_SHURUI_CD.Text))
            {
                this.KYOKASHOU_SHURUI_NAME.Text = string.Empty;
                return;
            }

            string strKyokashouShuruiCd = this.KYOKASHOU_SHURUI_CD.Text;
            if (strKyokashouShuruiCd.Equals("31"))
            {
                this.KYOKASHOU_SHURUI_NAME.Text = ConstCls.KYOKASHOU_SHURUI_NAME_31;
            }
            else if (strKyokashouShuruiCd.Equals("32"))
            {
                this.KYOKASHOU_SHURUI_NAME.Text = ConstCls.KYOKASHOU_SHURUI_NAME_32;
            }
            else if (strKyokashouShuruiCd.Equals("33"))
            {
                this.KYOKASHOU_SHURUI_NAME.Text = ConstCls.KYOKASHOU_SHURUI_NAME_33;
            }
            else
            {
                this.KYOKASHOU_SHURUI_NAME.Text = string.Empty;
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

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            this.GYOUSHA_CD.Text = this.GYOUSHA_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforGyousaCD == this.GYOUSHA_CD.Text)
            {
                return;
            }

            this.GENBA_CD.Text = string.Empty;
            this.GENBA_NAME_RYAKU.Text = string.Empty;

            this.rirekiLogic.CheckGyousha();
        }

        /// <summary>
        /// 排出事業場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            this.GENBA_CD.Text = this.GENBA_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeGenbaCD == this.GENBA_CD.Text)
            {
                return;
            }

            this.rirekiLogic.CheckGenba();
        }

        /// <summary>
        /// 排出事業者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.beforGyousaCD = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 排出事業場
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.beforeGenbaCD = this.GENBA_CD.Text;
        }

        /// <summary>
        /// 運搬業者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UNPANGYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.beforeUnpanGyousaCD = this.UNPANGYOUSHA_CD.Text;
        }

        /// <summary>
        /// 処分受託者（処分）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_JYUTAKUSHA_SHOBUN_CD_Enter(object sender, EventArgs e)
        {
            this.beforeShobunJyutakushaShobunCD = this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text;
        }

        /// <summary>
        /// 処分事業場
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.beforeShobunGenbaCD = this.SHOBUN_GENBA_CD.Text;
        }

        /// <summary>
        /// 処分受託者（最終）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_JYUTAKUSHA_SAISHU_CD_Enter(object sender, EventArgs e)
        {
            this.beforeShobunJyutakushaSaishuCD = this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text;
        }

        /// <summary>
        /// 処分事業場(最終)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SAISHUU_SHOBUNJOU_CD_Enter(object sender, EventArgs e)
        {
            this.beforeSaishuShobunCD = this.SAISHUU_SHOBUNJOU_CD.Text;
        }

        /// <summary>
        /// 排出事業者 PopupBeforeExecuteMethod
        /// </summary>
        internal void GYOUSHA_PopupBeforeExecuteMethod()
        {
            this.beforGyousaCD = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 排出事業者 PopupAfterExecuteMethod
        /// </summary>
        internal void GYOUSHA_PopupAfterExecuteMethod()
        {
            if (this.beforGyousaCD != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分受託者(処分) PopupBeforeExecuteMethod
        /// </summary>
        internal void SHOBUN_JYUTAKUSHA_SHOBUN_PopupBeforeExecuteMethod()
        {
            this.beforeShobunJyutakushaShobunCD = this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text;
        }

        /// <summary>
        /// 処分受託者(処分) PopupAfterExecuteMethod
        /// </summary>
        internal void SHOBUN_JYUTAKUSHA_SHOBUN_PopupAfterExecuteMethod()
        {
            if (this.beforeShobunJyutakushaShobunCD != this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text)
            {
                this.SHOBUN_GENBA_CD.Text = string.Empty;
                this.SHOBUN_GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分受託者(最終) PopupBeforeExecuteMethod
        /// </summary>
        internal void SHOBUN_JYUTAKUSHA_SAISHU_PopupBeforeExecuteMethod()
        {
            this.beforeShobunJyutakushaSaishuCD = this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text;
        }

        /// <summary>
        /// 処分受託者(最終) PopupAfterExecuteMethod
        /// </summary>
        internal void SHOBUN_JYUTAKUSHA_SAISHU_PopupAfterExecuteMethod()
        {
            if (this.beforeShobunJyutakushaSaishuCD != this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text)
            {
                this.SAISHUU_SHOBUNJOU_CD.Text = string.Empty;
                this.SAISHUU_SHOBUNJOU_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UNPANGYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.UNPANGYOUSHA_CD.Text))
            {
                this.UNPANGYOUSHA_NAME.Text = string.Empty;
                return;
            }

            //フォーマット6桁
            this.UNPANGYOUSHA_CD.Text = this.UNPANGYOUSHA_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeUnpanGyousaCD == this.UNPANGYOUSHA_CD.Text)
            {
                return;
            }

            this.rirekiLogic.CheckUnpanGyousha();
        }

        /// <summary>
        /// 処分受託者（処分）更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_JYUTAKUSHA_SHOBUN_CD_Validating(object sender, CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text))
            {
                this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text = string.Empty;
                this.SHOBUN_GENBA_CD.Text = string.Empty;
                this.SHOBUN_GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            //フォーマット6桁
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text = this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeShobunJyutakushaShobunCD == this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text)
            {
                return;
            }
            else
            {
                //業者が変わったので現場クリア
                this.SHOBUN_GENBA_CD.Text = string.Empty;
                this.SHOBUN_GENBA_NAME_RYAKU.Text = string.Empty;
            }

            this.rirekiLogic.CheckShobunJyutakushaShobun();
        }

        /// <summary>
        /// 処分事業場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            //フォーマット6桁
            if (string.IsNullOrEmpty(this.SHOBUN_GENBA_CD.Text))
            {
                this.SHOBUN_GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            this.SHOBUN_GENBA_CD.Text = this.SHOBUN_GENBA_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeShobunGenbaCD == this.SHOBUN_GENBA_CD.Text)
            {
                return;
            }

            this.rirekiLogic.CheckShobunGenba();
        }

        /// <summary>
        /// 処分受託者（最終）更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_JYUTAKUSHA_SAISHU_CD_Validating(object sender, CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text))
            {
                this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text = string.Empty;
                this.SAISHUU_SHOBUNJOU_CD.Text = string.Empty;
                this.SAISHUU_SHOBUNJOU_NAME.Text = string.Empty;
                return;
            }

            //フォーマット6桁
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text = this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeShobunJyutakushaSaishuCD == this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text)
            {
                return;
            }
            else
            {
                //業者が変わったので現場クリア
                this.SAISHUU_SHOBUNJOU_CD.Text = string.Empty;
                this.SAISHUU_SHOBUNJOU_NAME.Text = string.Empty;
            }

            this.rirekiLogic.CheckShobunJyutakushaSaishu();
        }

        /// <summary>
        /// 最終処分場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SAISHUU_SHOBUNJOU_CD_Validating(object sender, CancelEventArgs e)
        {
            //フォーマット6桁
            if (string.IsNullOrEmpty(this.SAISHUU_SHOBUNJOU_CD.Text))
            {
                this.SAISHUU_SHOBUNJOU_NAME.Text = string.Empty;
                return;
            }

            this.SAISHUU_SHOBUNJOU_CD.Text = this.SAISHUU_SHOBUNJOU_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeSaishuShobunCD == this.SAISHUU_SHOBUNJOU_CD.Text)
            {
                return;
            }

            this.rirekiLogic.CheckSaishuGenba();
        }

        /// <summary>
        /// 契約書種類更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void KEIYAKUSHO_SHURUI_CD_Validating(object sender, CancelEventArgs e)
        {
            this.rirekiLogic.CheckKeiyakuShuruiCD();
        }

        /// <summary>
        /// 許可証種類更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void KYOKASHOU_SHURUI_CD_Validating(object sender, CancelEventArgs e)
        {
            this.rirekiLogic.CheckKyokashouCD();
        }

        /// <summary>
        /// 日付選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_SELECT_TextChanged(object sender, EventArgs e)
        {
            if (this.DATE_SELECT.Text != "8")
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
                    this.labelInfo.Text = "送付日";
                    this.DATE_FROM.Tag = "[送付日]開始日を入力してください";
                    this.DATE_TO.Tag = "[送付日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[送付日]開始日";
                    this.DATE_TO.DisplayItemName = "[送付日]終了日";
                    break;
                case "3":
                    this.labelInfo.Text = "契約日";
                    this.DATE_FROM.Tag = "[契約日]開始日を入力してください";
                    this.DATE_TO.Tag = "[契約日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[契約日]開始日";
                    this.DATE_TO.DisplayItemName = "[契約日]終了日";
                    break;
                case "4":
                    this.labelInfo.Text = "有効期間開始";
                    this.DATE_FROM.Tag = "[有効期間開始]開始日を入力してください";
                    this.DATE_TO.Tag = "[有効期間開始]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[有効期間開始]開始日";
                    this.DATE_TO.DisplayItemName = "[有効期間開始]終了日";
                    break;
                case "5":
                    this.labelInfo.Text = "有効期間終了";
                    this.DATE_FROM.Tag = "[有効期間終了]開始日を入力してください";
                    this.DATE_TO.Tag = "[有効期間終了]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[有効期間終了]開始日";
                    this.DATE_TO.DisplayItemName = "[有効期間終了]終了日";
                    break;
                case "6":
                    this.labelInfo.Text = "自動更新終了日";
                    this.DATE_FROM.Tag = "[自動更新終了日]開始日を入力してください";
                    this.DATE_TO.Tag = "[自動更新終了日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[自動更新終了日]開始日";
                    this.DATE_TO.DisplayItemName = "[自動更新終了日]終了日";
                    break;
                case "7":
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
    }
}
