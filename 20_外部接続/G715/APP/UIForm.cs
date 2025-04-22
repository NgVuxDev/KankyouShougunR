using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku.Const;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku
{
    /// <summary>
    /// 電子契約入力
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal DenshiKeiyakuNyuryoku.LogicClass logic = null;

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
        /// <param name="windowType">画面区分</param>
        /// <param name="systemId">委託契約システムID</param>
        /// <param name="denshiSystemId">電子契約システムID</param>
        public UIForm(WINDOW_TYPE windowType, string systemId, string denshiSystemId)
            : base(WINDOW_ID.T_DENSHI_KEIYAKU_NYUURYOKU, windowType)
        {

            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.logic.systemId = systemId;
            this.logic.denshiSystemId = denshiSystemId;

            //完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            // 新規モードの時、電子契約タブは非表示
            if (windowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                this.DenshiKeiyakuData.TabPages.Remove(this.tabDenshiKeiyaku);
            }
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
            //this.txtNum_HaishaKBN.Focus();

            // 委託契約情報を画面に反映
            this.logic.SetValue();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            if (this.customDataGridView2 != null)
            {
                this.customDataGridView2.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            if (this.denshiKeiyakuIchiran != null)
            {
                this.denshiKeiyakuIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            if (this.kyoyusakiIchiran != null)
            {
                this.kyoyusakiIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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
        /// 契約書取消(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.logic.CheckClientId())
            {
                return;
            }

            // 作成者とログイン者について、社員CDとクライアントIDのマッチングを行う。
            if (!this.logic.MatchingClientId())
            {
                return;
            }

            // データの再取得
            this.logic.GetDenshiKeiyakuJyouhou();

            // 契約書状況取得のAPIで状況を取得する。
            bool keiyakuJyoukyouRet = this.logic.GetKeiyakuJyoukyou(2);
            if (!keiyakuJyoukyouRet)
            {
                return;
            }
            // 「1:先方確認中」以外の場合、処理なし
            if (this.keiyakuJyoukyouValue != 1)
            {
                this.logic.msgLogic.MessageBoxShowError("契約状況が「先方確認中」ではないため取消できません。");
                return;
            }
            // 契約書の取消処理
            bool ret = this.logic.ShinseiTorikeshi();
            if (ret)
            {
                this.logic.msgLogic.MessageBoxShow("I001", "取消");

                // 画面を閉じる。
                this.CloseWindow();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 部分更新(F5)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // データの再取得
            this.logic.GetDenshiKeiyakuJyouhou();

            // 社内備考と送付情報一覧の備考を更新する。
            bool ret = this.logic.BubunUpdate();
            if (ret)
            {
                this.logic.msgLogic.MessageBoxShow("I001", "更新");

                // 画面を閉じる。
                this.CloseWindow();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 契約書送付(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.logic.CheckClientId())
            {
                return;
            }

            // 共有先情報の入力チェック
            if (!this.logic.CheckKyoyusaki())
            {
                return;
            }

            // 委託契約書で電子契約タブの送付先情報が設定されているかチェックする。
            if (this.logic.denshiSouhusaki != null
                && (this.logic.denshiSouhusaki.Length == 1
                    && string.IsNullOrEmpty(this.logic.denshiSouhusaki[0].MAIL_ADDRESS)))
            {
                this.logic.msgLogic.MessageBoxShowError("契約先の氏名、メールアドレスが未入力です。委託契約書入力画面で確認をしてください。");
                return;
            }

            // 契約書送付の必須チェック
            bool checkResult = this.logic.SendBeforeCheck();

            if (checkResult)
            {
                // 同一ファイル名チェック
                bool sameFile = this.logic.SameFileNameCheck();
                if (sameFile)
                {
                    DialogResult ret = this.logic.msgLogic.MessageBoxShowConfirm("同一ファイル名が選択されています。このまま契約書送付を行いますか？");
                    if (ret != DialogResult.Yes)
                    {
                        return;
                    }
                }

                bool apiResult = false;

                // データの再取得
                this.logic.GetDenshiKeiyakuJyouhou();

                // 画面のデータを登録・更新する。
                bool updateResult = this.logic.UpdateDisplayData();

                if (updateResult)
                {
                    // 書類を作成する。
                    apiResult = this.logic.ShoruiCreate();
                    if (apiResult)
                    {
                        // ファイルを追加する。
                        apiResult = this.logic.FileAdd();
                    }
                    if (apiResult)
                    {
                        // 宛先を追加する。
                        apiResult = this.logic.AtesakiAdd();
                    }
                    // 下記IF文の条件に、共有先追加が「1.追加する」が選択された時だけも追加
                    if (apiResult && this.KYOYUSAKI_TSUIKA.Text == "1")
                    {
                        // 共有先を追加する。
                        apiResult = this.logic.KyoyusakiAdd();
                    }
                    if (apiResult)
                    {
                        // 書類を送信する。
                        apiResult = this.logic.ShoruiSend();
                    }

                    // API連携がすべて完了した場合、完了メッセージを出力する。
                    if (apiResult)
                    {
                        this.logic.msgLogic.MessageBoxShow("I001", "送付");

                        // 画面を閉じる。
                        this.CloseWindow();
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 行削除(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 選択中の行を一覧から削除する。
            if (this.customDataGridView1.CurrentCell != null && !this.customDataGridView1.CurrentRow.IsNewRow)
            {
                this.customDataGridView1.Rows.RemoveAt(this.customDataGridView1.CurrentCell.RowIndex);
            }


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
        /// 契約書ダウンロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.logic.CheckClientId())
            {
                return;
            }

            // 送付情報一覧のチェックボックス確認
            bool checkResult = this.logic.CheckBeforeDownload();

            if (checkResult)
            {
                bool ret = this.logic.FileDownload();

                if (ret)
                {
                    this.logic.msgLogic.MessageBoxShow("I001", "ダウンロード");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 契約状況取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.logic.CheckClientId())
            {
                return;
            }

            // データの再取得
            this.logic.GetDenshiKeiyakuJyouhou();

            // 契約状況を取得して設定する。
            bool ret = this.logic.GetKeiyakuJyoukyou(1);
            if (ret)
            {
                this.logic.msgLogic.MessageBoxShow("I001", "取得");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 合意締結証明書ダウンロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // クライアントIDが設定されているか確認する。
            if (!this.logic.CheckClientId())
            {
                return;
            }

            // ダウンロード
            bool ret = this.logic.CertificateDownload();

            if (ret)
            {
                this.logic.msgLogic.MessageBoxShow("I001", "ダウンロード");
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 各項目のイベント処理

        /// <summary>
        /// 各CELLのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 参照ボタンをクリックする時
                if (this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("colFilePathSansyo")
                    && !this.customDataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly)
                {
                    // フォルダを開く。
                    this.logic.FileRefClick();

                }
                else if (this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("colFilePathEtsuran")
                    && !this.customDataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly)
                {
                    // PDFを閲覧する。
                    this.logic.BrowseClick();
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
        }

        #endregion

        #region 明細一覧のcellを結合する
        /// <summary>
        /// チェックボックスのヘッダーを起動モードによって、送付、ﾀﾞｳﾝﾛｰﾄﾞ、削除に切り替える。
        /// 列ヘッダーの罫線を消して結合しているように表示する。
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void DetailIchiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    // ヘッダー以外は処理なし
                    return;
                }

                // WINDOW_TYPEにより、チェックボックス列のヘッダーテキストを変更する。
                // 新規の場合「送付」にする。
                if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.customDataGridView1.Columns["colCheckBox"].HeaderText = "送付";
                }
                // 修正の場合「ﾀﾞｳﾝﾛｰﾄﾞ」にする。
                else if (this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    this.customDataGridView1.Columns["colCheckBox"].HeaderText = "ﾀﾞｳﾝﾛｰﾄﾞ";
                }
                // 削除の場合「削除」にする。
                else if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    this.customDataGridView1.Columns["colCheckBox"].HeaderText = "削除";
                }

                // ファイル種類列から結合する
                int colIndex = this.customDataGridView1.Columns["colFileShuruiCD"].Index;

                // ファイル種類
                this.DetailIchiranCellPainting(sender, e, colIndex, "ファイル種類※");

                // ファイルパス
                this.DetailIchiranCellPainting(sender, e, colIndex + 8, "");

                // 結合セル以外は既定の描画を行う
                if (!(e.ColumnIndex == colIndex || e.ColumnIndex == colIndex + 1 || e.ColumnIndex == colIndex + 8 || e.ColumnIndex == colIndex + 9))
                {
                    e.Paint(e.ClipBounds, e.PaintParts);
                }

                // イベントハンドラ内で処理を行ったことを通知
                e.Handled = true;


                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.ColumnIndex == 0 && e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(this.chkSelect.Width, this.chkSelect.Height))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // Bitmapに描画
                        this.chkSelect.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                        // 描画位置設定
                        int rightMargin = 4;
                        int x = (e.CellBounds.Width - this.chkSelect.Width) - rightMargin;
                        int y = ((e.CellBounds.Height - this.chkSelect.Height) / 2);

                        // DataGridViewの現在描画中のセルに描画
                        Point pt = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);
                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// DataGridViewCellClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 && this.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                if (this.customDataGridView1.RowCount > 0)
                {
                    if (this.customDataGridView1.Columns[e.ColumnIndex].Name == "colCheckBox")
                    {
                        // 全選択チェックボックスが押下された場合、チェックボックス状態を反転する
                        this.chkSelect.Checked = !this.chkSelect.Checked;

                        // チェックボックスの全ての状態を書き換え
                        foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                        {
                            if (!row.IsNewRow && !row.ReadOnly)
                            {
                                // 新規行以外のチェックボックスを更新
                                row.Cells["colCheckBox"].Value = this.chkSelect.Checked;
                            }
                        }

                        // 再描画
                        var parent = (BusinessBaseForm)this.Parent;
                        parent.txb_process.Focus();
                        this.customDataGridView1.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// セルを結合する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="arCcolIndex"></param>
        /// <param name="colName"></param>
        private void DetailIchiranCellPainting(object sender, DataGridViewCellPaintingEventArgs e, int arCcolIndex, string colName)
        {
            // ファイル種類、ファイルパス
            int colIndex = arCcolIndex;

            if (e.ColumnIndex == colIndex || e.ColumnIndex == colIndex + 1)
            {
                // セルの矩形を取得
                Rectangle rect = e.CellBounds;

                DataGridView dgv = (DataGridView)sender;

                // 1列目の場合
                if (e.ColumnIndex == colIndex)
                {
                    // 2列目の幅を取得して、1列目の幅に足す
                    rect.Width += dgv.Columns[colIndex + 1].Width;
                    rect.Y = e.CellBounds.Y + 1;
                }
                else
                {
                    // 1列目の幅を取得して、2列目の幅に足す
                    rect.Width += dgv.Columns[colIndex].Width;
                    rect.Y = e.CellBounds.Y + 1;

                    // Leftを1列目に合わせる
                    rect.X -= dgv.Columns[colIndex].Width;
                }
                // 背景、枠線、セルの値を描画
                using (SolidBrush brush = new SolidBrush(this.customDataGridView1.ColumnHeadersDefaultCellStyle.BackColor))
                {
                    // 背景の描画
                    e.Graphics.FillRectangle(brush, rect);

                    using (Pen pen = new Pen(dgv.GridColor))
                    {
                        // 枠線の描画
                        e.Graphics.DrawRectangle(pen, rect);
                    }

                    using (Pen pen1 = new Pen(Color.DarkGray))
                    {
                        // 直線を描画(ヘッダ上部)
                        e.Graphics.DrawLine(pen1, rect.X, rect.Y - 1, rect.X + rect.Width, rect.Y - 1);

                        // 直線を描画(ヘッダ下部)
                        e.Graphics.DrawLine(pen1, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                    }
                }

                // セルに表示するテキストを描画
                TextRenderer.DrawText(e.Graphics,
                                colName,
                                e.CellStyle.Font,
                                rect,
                                e.CellStyle.ForeColor,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
            }
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
        /// ファイル種類のフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var row = this.customDataGridView1.Rows[e.RowIndex];
                    if (this.customDataGridView1.Columns["colFileShuruiCD"].Index == e.ColumnIndex)
                    {
                        switch (this.GetCellValue(row.Cells["colFileShuruiCD"]))
                        {
                            case "11":
                                row.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_11;
                                break;
                            case "12":
                                row.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_12;
                                break;
                            case "13":
                                row.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_13;
                                break;
                            case "21":
                                row.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_21;
                                break;
                            case "22":
                                row.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_22;
                                break;
                            case "23":
                                row.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_23;
                                break;
                            case "31":
                                row.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_31;
                                break;
                            case "32":
                                row.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_32;
                                break;
                            case "33":
                                row.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_33;
                                break;
                            case "99":
                                row.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_99;
                                break;
                            default:
                                if (!string.IsNullOrEmpty(this.GetCellValue(row.Cells["colFileShuruiCD"])))
                                {
                                    this.logic.msgLogic.MessageBoxShow("E020", "ファイル種類");
                                }
                                row.Cells["colFileShuruiName"].Value = string.Empty;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailIchiran_CellValidated", ex);
                throw ex;
            }
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
            var parentForm = (BusinessBaseForm)this.Parent;
            this.customDataGridView1.DataSource = "";
            this.Close();
            parentForm.Close();
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
        /// 送付情報一覧のチェックボックス初期値の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (this.customDataGridView1.Rows[e.Row.Index].IsNewRow)
            {
                this.customDataGridView1.Rows[e.Row.Index].Cells["colCheckBox"].Value = false;
            }
        }

        /// <summary>
        /// CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KyoyusakiIchiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var name = this.kyoyusakiIchiran.Columns[e.ColumnIndex].Name;
                if ("KYOYUSAKI_CORP_NAME".Equals(name))
                {
                    kyoyusakiIchiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                }
                else if ("KYOYUSAKI_NAME".Equals(name))
                {
                    kyoyusakiIchiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                }
                else if ("KYOYUSAKI_MAIL_ADDRESS".Equals(name))
                {
                    kyoyusakiIchiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("kyoyusakiIchiran_CellEnter", ex);
                throw;
            }
        }

        /// <summary>
        /// 共有先追加テキストボックス値変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KYOYUSAKI_TSUIKA_TextChanged(object sender, EventArgs e)
        {
            this.logic.KyoyusakiTsuika_TextChanged(this.KYOYUSAKI_TSUIKA.Text);
        }

        /// <summary>
        /// 共有先明細行追加イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kyoyusakiIchiran_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex <= 0)
            {
                return;
            }

            if (this.kyoyusakiIchiran.Rows[e.RowIndex].IsNewRow)
            {
                this.kyoyusakiIchiran.Rows[e.RowIndex].Cells["KYOYUSAKI_YUUSEN_NO"].Value = e.RowIndex + 1;
            }
        }

        /// <summary>
        /// CellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kyoyusakiIchiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // メールアドレスの重複チェック
                if (e.ColumnIndex == this.kyoyusakiIchiran.Columns["KYOYUSAKI_MAIL_ADDRESS"].Index)
                {
                    if (kyoyusakiIchiran.Rows[e.RowIndex].Cells["KYOYUSAKI_MAIL_ADDRESS"].Value != null)
                    {
                        string address = (string)kyoyusakiIchiran.Rows[e.RowIndex].Cells["KYOYUSAKI_MAIL_ADDRESS"].Value.ToString();
                        // 共有先タブのメールアドレスに対して重複チェック
                        bool catchErr = true;
                        bool isError = this.logic.DuplicationCheck(address, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (isError)
                        {
                            this.logic.msgLogic.MessageBoxShow("E003", "メールアドレス", address);
                            e.Cancel = true;
                            this.kyoyusakiIchiran.BeginEdit(false);
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.kyoyusakiIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex], true);
                            return;
                        }
                        // 委託契約書入力-電子契約タブのメールアドレスに対して重複チェック
                        bool catchErrAR = true;
                        bool isErrorAR = this.logic.DuplicationCheckApprovalRoute(address, out catchErrAR);
                        if (!catchErrAR)
                        {
                            return;
                        }
                        if (isErrorAR)
                        {
                            string errormessage = "承認経路で登録しているメールアドレスは共有先に登録できません。\n\r重複しているアドレスを削除してください";
                            errormessage += "（" + address + "）";
                            this.logic.msgLogic.MessageBoxShowError(errormessage);
                            e.Cancel = true;
                            this.kyoyusakiIchiran.BeginEdit(false);
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.kyoyusakiIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex], true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("kyoyusakiIchiran_CellValidating", ex);
                throw;
            }
        }
    }
}
