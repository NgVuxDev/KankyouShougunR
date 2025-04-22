using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.ExternalConnection.NaviTimeMasterRenkei
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic = null;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        private HeaderForm HeaderForm { get; set; }

        /// <summary>
        /// 除外登録フラグ
        /// </summary>
        internal int exclusionFlg { get; set; }

        /// <summary>
        /// 出力日時
        /// </summary>
        internal string output_date { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        internal int status { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_NAVI_TIME_MASTER_RENKEI)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
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

            //キー入力設定
            this.ParentBaseForm = (BusinessBaseForm)this.Parent;

            //ヘッダーフォームを取得
            this.HeaderForm = (HeaderForm)this.ParentBaseForm.headerForm;

            this.logic.WindowInit();

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
        }

        /// <summary>
        /// 画面表示
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

            // 除外登録チェック
            if (this.logic.kensakuJoken == ConstCls.SEARCH_RENKEI_JOGAI)
            {
                this.logic.msgLogic.MessageBoxShow("E261", "既に除外登録済み", "除外登録");
                return;
            }

            // 連携済みデータは除外登録不可
            if (this.logic.renkeiKouho != ConstCls.RENKEI_KOUHO_MIRENKEI)
            {
                this.logic.msgLogic.MessageBoxShow("E172", "連携解除");
                return;
            }

            // 件数チェック
            if (!this.logic.CheckCount())
            {
                return;
            }

            // 除外フラグを立てる
            this.exclusionFlg = 1;

            // 出力日時はNULL
            this.output_date = "Null";

            // 登録処理
            var ret_cnt = this.logic.TimeStampTableRegist();
            if (ret_cnt < 0)
            {
                return;
            }
            else if (ret_cnt == 0)
            {
                this.logic.msgLogic.MessageBoxShow("E034", "除外登録するデータ");
                return;
            }

            this.logic.msgLogic.MessageBoxShow("I001", "除外登録");

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

            // 除外登録チェック
            if (this.logic.kensakuJoken == ConstCls.SEARCH_RENKEI_TAISHO)
            {
                this.logic.msgLogic.MessageBoxShow("E261", "除外登録されていない", "除外解除登録");
                return;
            }

            // 連携済みデータは除外解除登録不可
            if (this.logic.renkeiKouho != ConstCls.RENKEI_KOUHO_MIRENKEI)
            {
                this.logic.msgLogic.MessageBoxShow("E172", "連携解除");
                return;
            }

            // 除外フラグを解除
            this.exclusionFlg = 0;

            // 出力日時はNULL
            this.output_date = "Null";

            // 登録処理
            var ret_cnt = this.logic.TimeStampTableRegist();
            if (ret_cnt < 0)
            {
                return;
            }
            else if (ret_cnt == 0)
            {
                this.logic.msgLogic.MessageBoxShow("E034", "除外解除登録するデータ");
                return;
            }

            this.logic.msgLogic.MessageBoxShow("I001", "除外解除登録");

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

            this.logic.ButtonEnabledFalse();

            // 除外登録アラート
            if (this.logic.kensakuJoken == ConstCls.SEARCH_RENKEI_JOGAI)
            {
                this.logic.msgLogic.MessageBoxShow("E261", "除外登録済み", "削除連携");
                this.logic.FunctionControl();
                return;
            }

            // 除外フラグを解除
            this.exclusionFlg = 0;

            // 出力日時はNULL
            this.output_date = "Null";

            // ステータスをセット
            this.status = ConstCls.STATUS_MIRENKEI;

            // リクエストの発行は社員、現場のみ
            if (this.logic.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN ||
                this.logic.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
            {
                // 社員マスタの場合、連携情報全件削除を弾く
                if (this.logic.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                {
                    if (!this.logic.DeleteCheck())
                    {
                        this.logic.FunctionControl();
                        return;
                    }
                }

                // CSV作成処理
                string filepath = this.logic.OutputCSV("DEL");
                if (string.IsNullOrEmpty(filepath))
                {
                    // 出力失敗
                    this.logic.FunctionControl();
                    return;
                }

                //更新リクエスト
                int count = this.logic.JsonConnection(filepath);
                if (count < 0)
                {
                    this.logic.FunctionControl();
                    return;
                }
                else if (count == 0)
                {
                    this.logic.msgLogic.MessageBoxShow("E051", "連携するデータ");
                    this.logic.FunctionControl();
                    return;
                }
            }

            //タイムスタンプテーブルに出力データを保存する
            var ret_cnt = this.logic.TimeStampTableRegist();
            if (ret_cnt < 0)
            {
                this.logic.FunctionControl();
                return;
            }

            this.logic.msgLogic.MessageBoxShow("I001", "連携");

            //処理完了したら再抽出
            this.bt_func8_Click(sender, e);

            this.logic.FunctionControl();

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

            if (!base.RegistErrorFlag)
            {
                // 一括チェックボックスを解除
                this.logic.HeaderCheckBoxFalse();

                // 検索
                int cnt = this.logic.Search();

                // 読み込み件数をヘッダーに表示
                this.CountDisplay(cnt);

                // 読み込みデータが0の場合はアラート
                if (cnt == 0)
                {
                    this.logic.msgLogic.MessageBoxShow("C001");
                }

            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F9 連携登録
        /// <summary>
        /// 連携登録(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ButtonEnabledFalse();

            // 除外フラグを解除
            this.exclusionFlg = 0;

            // 出力日時をセット
            this.output_date = " GETDATE() ";

            // ステータスをセット
            this.status = ConstCls.STATUS_RENKEIKANRYOU;

            // 件数チェック
            if (!this.logic.CheckCount())
            {
                this.logic.FunctionControl();
                return;
            }

            // 必須項目チェック
            Boolean isCheckOK = this.logic.CheckBeforeUpdate();
            if (!isCheckOK)
            {
                this.logic.FunctionControl();
                return;
            }

            // リクエストの発行は社員、現場のみ
            if (this.logic.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN ||
                this.logic.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
            {
                // CSV作成処理
                string filepath = this.logic.OutputCSV("INS");
                if (string.IsNullOrEmpty(filepath))
                {
                    // 出力失敗
                    this.logic.FunctionControl();
                    return;
                }

                //更新リクエスト
                int count = this.logic.JsonConnection(filepath);
                if (count < 0)
                {
                    this.logic.FunctionControl();
                    return;
                }
                else if (count == 0)
                {
                    this.logic.msgLogic.MessageBoxShow("E051", "連携するデータ");
                    this.logic.FunctionControl();
                    return;
                }
            }

            //タイムスタンプテーブルに出力データを保存する
            var ret_cnt = this.logic.TimeStampTableRegist();
            if (ret_cnt < 0)
            {
                this.logic.FunctionControl();
                return;
            }

            // 2秒停止
            System.Threading.Thread.Sleep(2000);

            int cnt = 0;

            // 現場の場合のみ、登録後に確認API連携を行う
            if (this.logic.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
            {
                // 出力日時をセット
                this.output_date = " null ";

                // API通信処理(確認)
                bool result = this.logic.CheckAPI();
                if (!result)
                {
                    this.logic.FunctionControl();
                    return;
                }

                // 一括チェックボックスを解除
                this.logic.HeaderCheckBoxFalse();

                // 検索
                cnt = this.logic.Search();

                // 読み込み件数をヘッダーに表示
                this.CountDisplay(cnt);

                this.logic.FunctionControl();
                return;
            }

            this.logic.msgLogic.MessageBoxShow("I001", "連携");

            // 一括チェックボックスを解除
            this.logic.HeaderCheckBoxFalse();

            // 検索
            cnt = this.logic.Search();

            // 読み込み件数をヘッダーに表示
            this.CountDisplay(cnt);

            this.logic.FunctionControl();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F10 連携確認
        /// <summary>
        /// 連計確認(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 件数チェック
            if (!this.logic.CheckCount())
            {
                return;
            }

            // 出力日時をセット
            this.output_date = " null ";

            // API通信処理(確認)
            bool result = this.logic.CheckAPI();
            if (result)
            {
                // 一括チェックボックスを解除
                this.logic.HeaderCheckBoxFalse();

                // 検索
                int cnt = this.logic.Search();

                // 読み込み件数をヘッダーに表示
                this.CountDisplay(cnt);
            }

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

            base.CloseTopForm();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #endregion

        #region 抽出件数表示
        /// <summary>
        /// 抽出した件数をヘッダーに表示する
        /// </summary>
        /// <param name="cnt"></param>
        private void CountDisplay(int cnt)
        {
            if (cnt < 0)
            {
                // 抽出エラー(-1)も0で表示
                this.HeaderForm.ReadDataNumber.Text = "0";
            }
            else
            {
                this.HeaderForm.ReadDataNumber.Text = cnt.ToString();
            }
        }
        #endregion

        #region ロールのポップアップ表示用

        /// <summary>
        /// KeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.Ichiran1.Columns[this.Ichiran1.CurrentCell.ColumnIndex].ReadOnly)
            {
                this.logic.CheckPopup1(e);
            }
        }

        /// <summary>
        /// ロールの項目の入力制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.Ichiran1.Columns[this.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("SHAIN_NAVI_ROLE"))
            {
                // 入力制御が難しいのでバックスペース以外不許可とした
                if (e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// セル編集中にイベントを発生させる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyDown -= this.Ichiran1_KeyDown;
            e.Control.KeyDown += this.Ichiran1_KeyDown;
            e.Control.KeyPress -= this.Ichiran1_KeyPress;
            e.Control.KeyPress += this.Ichiran1_KeyPress;
        }

        #endregion

        #region ロールの手入力処理
        /// <summary>
        /// セルのValidating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.Ichiran1.Columns[this.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("SHAIN_NAVI_ROLE"))
            {
                if (e.RowIndex >= 0)
                {
                    if (this.logic.GetCellValue(this.Ichiran1.Rows[e.RowIndex].Cells["SHAIN_NAVI_ROLE"].Value) == string.Empty)
                    {
                        // KeyPressイベントでバックスペース以外許可してないので、クリアのみ考慮する
                        this.Ichiran1.Rows[e.RowIndex].Cells["SHAIN_BUSINESS_CONTENT"].Value = string.Empty;
                    }
                }
            }
            // 重複チェック
            this.DispGridView_CellValidating(sender, e);
        }
        #endregion

        #region パスワードや重複のチェック
        private void Ichiran3_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // チェック
            this.DispGridView_CellValidating(sender, e);
        }


        /// <summary>
        /// チェック本体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // 重複チェック
                if (!String.IsNullOrEmpty(Convert.ToString(logic.DispGridView.Rows[e.RowIndex].Cells[this.logic.chkColumn1].Value)) &&
                    logic.DispGridView.Columns[e.ColumnIndex].Name.Equals(this.logic.chkColumn1))
                {
                    string ChkCDPK1 = (string)logic.DispGridView.Rows[e.RowIndex].Cells[this.logic.chkColmnPK1].Value.ToString();
                    string ChkCDPK2 = string.Empty;
                    if (this.logic.chkColmnPK2 != string.Empty)
                    {
                        ChkCDPK2 = (string)logic.DispGridView.Rows[e.RowIndex].Cells[this.logic.chkColmnPK2].Value.ToString();
                    }
                    string ChkCD = (string)logic.DispGridView.Rows[e.RowIndex].Cells[this.logic.chkColumn1].Value.ToString();
                    //ChkCD = ChkCD.PadLeft(10, '0');
                    bool catchErr = true;
                    bool isError = this.logic.DuplicationCheck(ChkCDPK1, ChkCDPK2, ChkCD, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isError)
                    {
                        this.logic.msgLogic.MessageBoxShow("E022", "入力された" + this.logic.chkColumnName1);
                        e.Cancel = true;
                        this.logic.DispGridView.BeginEdit(false);
                        return;
                    }
                }

                // 対象が社員・パスワードだった場合はパスワードチェックを行う
                if (this.logic.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN &&
                    this.logic.DispGridView.Columns[e.ColumnIndex].Name.Equals(this.logic.chkColumn3))
                {
                    string pass = Convert.ToString(logic.DispGridView.Rows[e.RowIndex].Cells[this.logic.chkColumn3].Value);
                    // まず文字数
                    if (pass.Length == 0)
                    {
                        return;
                    }
                    else if (pass.Length < 8)
                    {
                        var str = "パスワードを、8～32文字で入力してください。";
                        this.logic.msgLogic.MessageBoxShowInformation(str);
                        e.Cancel = true;
                        this.logic.DispGridView.BeginEdit(false);
                        return;
                    }

                    // 次に文字種
                    if (!this.logic.CheckPassword(pass, true))
                    {
                        var str = "パスワードは、半角英数字(大文字/小文字を区別)の二つ以上の組み合わせで8～32文字で入力してください。";
                        this.logic.msgLogic.MessageBoxShowInformation(str);
                        e.Cancel = true;
                        this.logic.DispGridView.BeginEdit(false);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DispGridView_CellValidating", ex);
                throw;
            }
        }
        #endregion

        #region ステータス(抽出条件)用の処理
        /// <summary>
        /// ステータス手入力時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void STATUS_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (this.STATUS_CD.Text)
            {
                case "0":
                    this.STATUS_NAME.Text = ConstCls.STATUS_MIRENKEI_STRING;
                    break;
                case "1":
                    this.STATUS_NAME.Text = ConstCls.STATUS_RENKEIKAKUNINNYOU_STRING;
                    break;
                case "2":
                    this.STATUS_NAME.Text = ConstCls.STATUS_RENKEIKANRYOU_STRING;
                    break;
                case "3":
                    this.STATUS_NAME.Text = ConstCls.STATUS_SAIRENKEIKOUHO_STRING;
                    break;
                case "9":
                    this.STATUS_NAME.Text = ConstCls.STATUS_SASHIMODOSHI_STRING;
                    break;
                default:
                    this.STATUS_NAME.Text = string.Empty;
                    break;
            }
        }
        /// <summary>
        /// ステータススペース押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void STATUS_CD_KeyDown(object sender, KeyEventArgs e)
        {
            this.logic.StatusPopup(e);
        }

        /// <summary>
        /// ステータスCD入力制限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void STATUS_CD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.logic.renkeiKouho == ConstCls.RENKEI_KOUHO_MIRENKEI)
            {
                if (e.KeyChar != '0' && e.KeyChar != '9' && e.KeyChar != '\b')
                {
                    // 未連携は0、9、バックスペース以外入力不許可
                    e.Handled = true;
                }
            }
            else if (this.logic.renkeiKouho == ConstCls.RENKEI_KOUHO_RENKEIZUMI)
            {
                if (e.KeyChar != '1' && e.KeyChar != '2' && e.KeyChar != '\b')
                {
                    // 連携済みは1、2、バックスペース以外入力不許可
                    e.Handled = true;
                }
            }
            // 再連携候補は入力不可のため考慮しない
        }
        #endregion
    }
}
