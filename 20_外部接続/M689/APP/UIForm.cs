using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
//ロジこん連携用
using Shougun.Core.ExternalConnection.ExternalCommon.Const;

namespace Shougun.Core.ExternalConnection.DigitachoMasterRenkei
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private DigitachoMasterRenkei.LogicClass DMRLogic = null;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        private Boolean isLoaded = false;

        /// <summary>
        /// 親フォーム
        /// </summary>
        private BusinessBaseForm ParentBaseForm { get; set; }

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        private UIHeader HeaderForm { get; set; }

        /// <summary>
        /// 除外登録フラグ
        /// </summary>
        internal int exclusionFlg { get; set; }

        /// <summary>
        /// 出力日時
        /// </summary>
        internal string output_date { get; set; }

        /// <summary>
        /// デジタコ連携に使用するコード
        /// </summary>
        internal string renkei_cd { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_DEJITAKO_MASTER_RENKEI)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.DMRLogic = new LogicClass(this);

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

            if (!this.isLoaded)
            {
                //初期化、初期表示
                if (!this.DMRLogic.WindowInit()) { return; }

                //キー入力設定
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;

                //ヘッダーフォームを取得
                this.HeaderForm = (UIHeader)this.ParentBaseForm.headerForm;

                // 初期フォーカス位置を設定します
                this.txtNum_RenkeiMaster.Focus();

                // Anchorの設定はOnLoadで行う
                if (this.Ichiran1 != null)
                {
                    this.Ichiran1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                if (this.Ichiran2 != null)
                {
                    this.Ichiran2.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                if (this.Ichiran3 != null)
                {
                    this.Ichiran3.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                if (this.Ichiran4 != null)
                {
                    this.Ichiran4.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                if (this.Ichiran5 != null)
                {
                    this.Ichiran5.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                if (this.Ichiran6 != null)
                {
                    this.Ichiran6.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
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


            base.OnShown(e);
        }
        #endregion

        #region ファンクションボタンのイベント

        #region F1 除外登録
        /// <summary>
        /// 除外登録(F1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //除外登録チェック
            if (this.DMRLogic.kensaku_Flg == 2)
            {
                this.DMRLogic.errmessage.MessageBoxShow("E261", "既に除外登録済み", "除外登録");
                return;
            }

            //連携済みデータは除外登録不可
            if (this.DMRLogic.renkeikouho_flg != 1)
            {
                this.DMRLogic.errmessage.MessageBoxShow("E172", "連携解除");
                return;
            }

            //除外フラグを立てる
            this.exclusionFlg = 1;

            //出力日時はNULL
            this.output_date = "Null";

            //連携コードはブランクとする
            this.renkei_cd = string.Empty;

            //処理
            int count = this.DMRLogic.ExclusionRegist();
            if (count < 0)
            {
                return;
            }
            else if (count == 0)
            {
                this.DMRLogic.errmessage.MessageBoxShow("E034", "除外登録するデータ");
                return;
            }

            this.DMRLogic.errmessage.MessageBoxShow("I001", "除外登録");

            //処理完了したら再抽出
            this.bt_func8_Click(sender, e);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F2 除外解除
        /// <summary>
        /// 除外解除(F2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //除外登録チェック
            if (this.DMRLogic.kensaku_Flg == 1)
            {
                this.DMRLogic.errmessage.MessageBoxShow("E261", "除外登録されていない", "除外解除登録");
                return;
            }

            //連携済みデータは除外解除登録不可
            if (this.DMRLogic.renkeikouho_flg != 1)
            {
                this.DMRLogic.errmessage.MessageBoxShow("E172", "連携解除");
                return;
            }

            //除外フラグを解除
            this.exclusionFlg = 0;

            //出力日時はNULL
            this.output_date = "Null";

            //連携コードはブランクとする
            this.renkei_cd = string.Empty;

            //処理
            int count = this.DMRLogic.ExclusionRegist();
            if (count < 0)
            {
                return;
            }
            else if (count == 0)
            {
                this.DMRLogic.errmessage.MessageBoxShow("E034", "除外解除するデータ");
                return;
            }

            this.DMRLogic.errmessage.MessageBoxShow("I001", "除外解除登録");

            //処理完了したら再抽出
            this.bt_func8_Click(sender, e);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F4 削除連携
        /// <summary>
        /// 削除連携(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //除外登録チェック
            if (this.DMRLogic.kensaku_Flg == 2)
            {
                this.DMRLogic.errmessage.MessageBoxShow("E261", "除外登録済み", "削除連携");
                return;
            }

            //除外フラグは立てない
            this.exclusionFlg = 0;

            //出力日時をセット
            this.output_date = "Null";

            //必須項目チェック
            Boolean isCheckOK = this.DMRLogic.CheckBeforeUpdate();
            if (!isCheckOK)
            {
                return;
            }

            //削除リクエスト
            int count = this.DMRLogic.JsonConnection(HTTP_METHOD.DELETE);
            if (count < 0)
            {
                return;
            }
            else if (count == 0)
            {
                this.DMRLogic.errmessage.MessageBoxShow("E034", "削除するデータ");
                return;
            }

            this.DMRLogic.errmessage.MessageBoxShow("I001", "連携");

            //処理完了したら再抽出
            this.bt_func8_Click(sender, e);

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

            //ヘッダーのチェックボックスを解除
            this.DMRLogic.HeaderCheckBoxFalse();

            //検索
            int count = this.DMRLogic.Search();
            if (count < 0)
            {
                this.HeaderForm.ReadDataNumber.Text = "0";
            }
            else if (count == 0)
            {
                this.HeaderForm.ReadDataNumber.Text = "0";
                this.DMRLogic.errmessage.MessageBoxShow("C001");
            }
            else
            {
                this.HeaderForm.ReadDataNumber.Text = count.ToString();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F9 連携登録
        /// <summary>
        /// 連携(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //除外登録チェック
            if (this.DMRLogic.kensaku_Flg == 2)
            {
                this.DMRLogic.errmessage.MessageBoxShow("E261", "除外登録済み", "登録連携");
                return;
            }

            //除外フラグは立てない
            this.exclusionFlg = 0;

            //出力日時をセット
            this.output_date = " GETDATE() ";

            //種類未入力チェック(連携マスタ：品名でのみ有効)
            Boolean isCheckYesNo = this.DMRLogic.CheckShurui();
            if (!isCheckYesNo)
            {
                return;
            }

            //必須項目チェック
            Boolean isCheckOK = this.DMRLogic.CheckBeforeUpdate();
            if (!isCheckOK)
            {
                return;
            }

            //更新リクエスト
            int count = this.DMRLogic.JsonConnection(HTTP_METHOD.PUT);
            if (count < 0)
            {
                return;
            }
            else if (count == 0)
            {
                this.DMRLogic.errmessage.MessageBoxShow("E051", "連携するデータ");
                return;
            }

            this.DMRLogic.errmessage.MessageBoxShow("I001", "連携");

            //処理完了したら再抽出
            this.bt_func8_Click(sender, e);

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

            //処理
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #endregion

        #region 重複チェック
        /// <summary>
        /// 重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.meisaiForm_CellValidating(sender, e);
        }

        private void Ichiran2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.meisaiForm_CellValidating(sender, e);
        }

        private void Ichiran3_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.meisaiForm_CellValidating(sender, e);
        }

        private void Ichiran4_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.meisaiForm_CellValidating(sender, e);
        }

        private void Ichiran5_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.meisaiForm_CellValidating(sender, e);
        }

        private void Ichiran6_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.meisaiForm_CellValidating(sender, e);
        }

        /// <summary>
        /// 重複チェック本体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void meisaiForm_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (null != this.DMRLogic.meisaiForm.Rows[e.RowIndex].Cells[this.DMRLogic.chkColumn].Value)
                {
                    this.DMRLogic.meisaiForm.Rows[e.RowIndex].Cells[this.DMRLogic.chkColumn].Value = this.DMRLogic.meisaiForm.Rows[e.RowIndex].Cells[this.DMRLogic.chkColumn].Value.ToString().ToUpper();
                }

                // デジタコ連携IDの重複チェック
                if (!String.IsNullOrEmpty(Convert.ToString(DMRLogic.meisaiForm.Rows[e.RowIndex].Cells[this.DMRLogic.chkColumn].Value)) &&
                    DMRLogic.meisaiForm.Columns[e.ColumnIndex].DataPropertyName.Equals(this.DMRLogic.chkColumn))
                {
                    string ChkCDPK1 = (string)DMRLogic.meisaiForm.Rows[e.RowIndex].Cells[this.DMRLogic.chkColmnPK1].Value.ToString();
                    string ChkCDPK2 = string.Empty;
                    if (this.DMRLogic.chkColmnPK2 != string.Empty)
                    {
                        ChkCDPK2 = (string)DMRLogic.meisaiForm.Rows[e.RowIndex].Cells[this.DMRLogic.chkColmnPK2].Value.ToString();
                    }
                    string ChkCD = (string)DMRLogic.meisaiForm.Rows[e.RowIndex].Cells[this.DMRLogic.chkColumn].Value.ToString();
                    //ChkCD = ChkCD.PadLeft(10, '0');
                    bool catchErr = true;
                    bool isError = this.DMRLogic.DuplicationCheck(ChkCDPK1, ChkCDPK2, ChkCD, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isError)
                    {
                        this.DMRLogic.errmessage.MessageBoxShow("E022", "入力された" + this.DMRLogic.chkColumnName);
                        e.Cancel = true;
                        this.DMRLogic.meisaiForm.BeginEdit(false);

                        LogUtility.DebugMethodEnd();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("meisaiForm_CellValidating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 値保持
        #endregion

    }
}
