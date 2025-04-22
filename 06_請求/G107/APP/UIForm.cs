using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using Seasar.Quill;
using Shougun.Core.Billing.SeikyuushoHakkou.APP;
using Shougun.Core.Billing.SeikyuushoHakkou.Const;

namespace Shougun.Core.Billing.SeikyuushoHakkou
{
    public partial class UIForm : SuperForm
    {
        #region プロパティ
        /// <summary>
        ///　帳票出力用支払データ
        /// </summary>
        public DataTable SeikyuDt { get; set; }
        #endregion プロパティ

        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// HeaderSeikyuushoHakkou.cs
        /// </summary>
        HeaderSeikyuushoHakkou headerForm;

        /// <summary>
        /// CheckedChangedイベントの処理を行うかどうか
        /// </summary>
        bool IsCheckedChangedEventRun = true;

        /// <summary>
        /// メッセージボックス
        /// </summary>
        private MessageBoxShowLogic errMsg = new MessageBoxShowLogic();

        #endregion

        public UIForm(HeaderSeikyuushoHakkou headerForm)
            : base(WINDOW_ID.T_SEIKYUSHO_HAKKO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        { 
            this.InitializeComponent();
            this.SeikyuuDenpyouItiran.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.headerForm = headerForm;
            this.logic.SetHeaderForm(headerForm);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            checkBoxAll.SendToBack();
            checkBoxAll_zumi.SendToBack();
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            checkBoxAll_densiCsv.SendToBack();
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            checkBoxAll_rakurakuCsv.SendToBack(); // 20211207 thucp v2.24_電子請求書 #157799
        }

        public UIForm(HeaderSeikyuushoHakkou headerForm, DTOClass dto)
            : base(WINDOW_ID.T_SEIKYUSHO_HAKKO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
            this.SeikyuuDenpyouItiran.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.headerForm = headerForm;
            this.logic.SetHeaderForm(headerForm);

            this.logic.InitDto = dto;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            checkBoxAll.SendToBack();
            checkBoxAll_zumi.SendToBack();
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            checkBoxAll_densiCsv.SendToBack();
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            checkBoxAll_rakurakuCsv.SendToBack(); // 20211207 thucp v2.24_電子請求書 #157799
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            this.SetPopUpTorihikisakiCd();
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.SeikyuuDenpyouItiran != null)
            {
                this.SeikyuuDenpyouItiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        /// <summary>
        /// Set Torihikisaki popup join method
        /// </summary>
        private void SetPopUpTorihikisakiCd()
        {
            if (r_framework.Configuration.AppConfig.AppOptions.IsInxsSeikyuusho())
            {
                JoinMethodDto joinData = new JoinMethodDto();
                joinData.Join = JOIN_METHOD.WHERE;
                joinData.LeftTable = "M_TORIHIKISAKI_SEIKYUU";

                Collection<SearchConditionsDto> searchConditions = new Collection<SearchConditionsDto>();
                SearchConditionsDto searchDto = new SearchConditionsDto();
                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.NOT_EQUALS;
                searchDto.LeftColumn = "INXS_SEIKYUU_KBN";
                searchDto.Value = "1"; //[取引先入力][INXS請求区分] = ２．しない
                searchDto.ValueColumnType = DB_TYPE.SMALLINT;
                searchConditions.Add(searchDto);
                joinData.SearchCondition = searchConditions;
                Collection<JoinMethodDto> popupWindowSetting = new Collection<JoinMethodDto>();
                if (this.TORIHIKISAKI_CD.popupWindowSetting != null)
                {
                    popupWindowSetting = (Collection<JoinMethodDto>)this.TORIHIKISAKI_CD.popupWindowSetting;
                }

                popupWindowSetting.Add(joinData);
                this.TORIHIKISAKI_CD.popupWindowSetting = popupWindowSetting;

                Collection<JoinMethodDto> popupWindowSettingButton = new Collection<JoinMethodDto>();
                if (this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting != null)
                {
                    popupWindowSettingButton = (Collection<JoinMethodDto>)this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting;
                }

                popupWindowSettingButton.Add(joinData);
                this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting = popupWindowSettingButton;
            }
        }
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

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

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();

            if (string.IsNullOrEmpty(this.PRINT_ORDER.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "印刷順");
                return;
            }

            if (string.IsNullOrEmpty(this.SEIKYU_PAPER.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "請求用紙");
                return;
            }

            if (string.IsNullOrEmpty(this.SEIKYU_STYLE.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "請求形態");
                return;
            }

            // No.4004->
            //if (string.IsNullOrEmpty(this.MEISAI.Text))
            //{
            //    MessageBox.Show(ConstCls.ErrStop6, ConstCls.DialogTitle);
            //    return;
            //}
            // No.4004<--

            if (string.IsNullOrEmpty(this.SEIKYUSHO_PRINTDAY.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "請求年月日");
                return;
            }

            if (string.IsNullOrEmpty(this.SEIKYU_HAKKOU.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "請求書発行日");
                return;
            }

            if (string.IsNullOrEmpty(this.HAKKOU_KBN.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "発行区分");
                return;
            }

            if (string.IsNullOrEmpty(this.OUTPUT_KBN.Text) && this.OUTPUT_KBN.Visible)
            {
                messageShowLogic.MessageBoxShow("E001", "出力区分");
                return;
            }

            if (string.IsNullOrEmpty(this.FILTERING_DATA.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "抽出データ");
                return;
            }

            /// 20141023 Houkakou 「請求書発行」の日付チェックを追加する　start
            this.headerForm.DenpyouHidukeFrom.IsInputErrorOccured = false;
            this.headerForm.DenpyouHidukeFrom.BackColor = Constans.NOMAL_COLOR;
            this.headerForm.DenpyouHidukeTo.IsInputErrorOccured = false;
            this.headerForm.DenpyouHidukeTo.BackColor = Constans.NOMAL_COLOR;
            /// 20141023 Houkakou 「請求書発行」の日付チェックを追加する　end

            if (!string.IsNullOrEmpty(this.headerForm.DenpyouHidukeFrom.GetResultText())
                && !string.IsNullOrEmpty(this.headerForm.DenpyouHidukeTo.GetResultText()))
            {
                DateTime dtpFrom = DateTime.Parse(this.headerForm.DenpyouHidukeFrom.GetResultText());
                DateTime dtpTo = DateTime.Parse(this.headerForm.DenpyouHidukeTo.GetResultText());
                DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                if (0 < diff)
                {
                    //対象期間内でないならエラーメッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.headerForm.DenpyouHidukeFrom.IsInputErrorOccured = true;
                    /// 20141023 Houkakou 「請求書発行」の日付チェックを追加する　start
                    this.headerForm.DenpyouHidukeFrom.BackColor = Constans.ERROR_COLOR;
                    this.headerForm.DenpyouHidukeTo.IsInputErrorOccured = true;
                    this.headerForm.DenpyouHidukeTo.BackColor = Constans.ERROR_COLOR;
                    //msgLogic.MessageBoxShow("E030", this.headerForm.DenpyouHidukeFrom.DisplayItemName, this.headerForm.DenpyouHidukeTo.DisplayItemName);
                    msgLogic.MessageBoxShow("E030", "請求日付From", "請求日付To");
                    /// 20141023 Houkakou 「請求書発行」の日付チェックを追加する　end
                    this.headerForm.DenpyouHidukeFrom.Select();
                    this.headerForm.DenpyouHidukeFrom.Focus();

                    return;
                }
            }

            if (this.logic.Search() == -1)
            {
                return;
            }
            if (!this.logic.SetIchiran())
            {
                return;
            }

            // 列ヘッダチェックボックスを初期化
            checkBoxAll.Checked = false;

            this.IsCheckedChangedEventRun = false;
            checkBoxAll_zumi.Checked = false;
            this.IsCheckedChangedEventRun = true;

            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            checkBoxAll_densiCsv.Checked = false;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end

            checkBoxAll_rakurakuCsv.Checked = false; // 20211207 thucp v2.24_電子請求書 #157799
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            this.logic.Regist();

        }


        /// <summary>
        /// プレビュー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PreView(object sender, EventArgs e)
        {
            try
            {
                this.Parent.Enabled = false;
                this.logic.PreView();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Parent.Enabled = true;
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            this.logic.SearchResult = new DataTable();
            parentForm.Close();
        }

        private void Kyoten_Cd_Leave(object sender, EventArgs e)
        {
            int i;
            if (!int.TryParse(this.KYOTEN_CD.Text, out i))
            {
                this.KYOTEN_CD.Text = "";
            }
        }

        #region DBGRIDVIEW

        // 列ヘッダーにチェックボックスを表示
        private void SeikyuuDenpyouItiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point pt1 = new Point((bmp.Width - checkBoxAll.Width) / 2, (bmp.Height - checkBoxAll.Height + 28) / 2);
                    if (pt1.X < 0) pt1.X = 0;
                    if (pt1.Y < 0) pt1.Y = 0;

                    // Bitmapに描画
                    checkBoxAll.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2; ;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp, pt2);
                    e.Handled = true;
                }
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            if (e.ColumnIndex == 1 && e.RowIndex == -1)
            {
                using (Bitmap bmp2 = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics g = Graphics.FromImage(bmp2))
                    {
                        g.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point pt1 = new Point((bmp2.Width - checkBoxAll_densiCsv.Width) / 2, (bmp2.Height - checkBoxAll_densiCsv.Height + 28) / 2);
                    if (pt1.X < 0) pt1.X = 0;
                    if (pt1.Y < 0) pt1.Y = 0;

                    // Bitmapに描画
                    checkBoxAll_densiCsv.DrawToBitmap(bmp2, new Rectangle(pt1.X, pt1.Y, bmp2.Width, bmp2.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp2.Width) / 2; ;
                    int y = (e.CellBounds.Height - bmp2.Height) / 2;

                    Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp2, pt2);
                    e.Handled = true;
                }
            }

            // 20211207 thucp v2.24_電子請求書 #157799 begin
            if (e.ColumnIndex == 2 && e.RowIndex == -1)
            {
                using (Bitmap bmp2 = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics g = Graphics.FromImage(bmp2))
                    {
                        g.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point pt1 = new Point((bmp2.Width - checkBoxAll_rakurakuCsv.Width) / 2, (bmp2.Height - checkBoxAll_rakurakuCsv.Height + 28) / 2);
                    if (pt1.X < 0) pt1.X = 0;
                    if (pt1.Y < 0) pt1.Y = 0;
                    // Bitmapに描画
                    checkBoxAll_rakurakuCsv.DrawToBitmap(bmp2, new Rectangle(pt1.X, pt1.Y, bmp2.Width, bmp2.Height));
                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp2.Width) / 2; ;
                    int y = (e.CellBounds.Height - bmp2.Height) / 2;
                    Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);
                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp2, pt2);
                    e.Handled = true;
                }
            }
            // 20211207 thucp v2.24_電子請求書 #157799 end

            if (e.ColumnIndex == 3 && e.RowIndex == -1)
            {
                using (Bitmap bmp2 = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics g = Graphics.FromImage(bmp2))
                    {
                        g.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point pt1 = new Point((bmp2.Width - checkBoxAll_zumi.Width) / 2, (bmp2.Height - checkBoxAll_zumi.Height + 28) / 2);
                    if (pt1.X < 0) pt1.X = 0;
                    if (pt1.Y < 0) pt1.Y = 0;

                    // Bitmapに描画
                    checkBoxAll_zumi.DrawToBitmap(bmp2, new Rectangle(pt1.X, pt1.Y, bmp2.Width, bmp2.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp2.Width) / 2; ;
                    int y = (e.CellBounds.Height - bmp2.Height) / 2;

                    Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp2, pt2);
                    e.Handled = true;
                }
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
        }

        // 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        private void SeikyuuDenpyouItiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                checkBoxAll.Checked = !checkBoxAll.Checked;
                SeikyuuDenpyouItiran.Refresh();
                checkBoxAll.Focus();
            }

            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            if (e.ColumnIndex == 1 && e.RowIndex == -1)
            {
                checkBoxAll_densiCsv.Checked = !checkBoxAll_densiCsv.Checked;
                SeikyuuDenpyouItiran.Refresh();
                checkBoxAll_densiCsv.Focus();
            }

            // 20211207 thucp v2.24_電子請求書 #157799 begin
            if (e.ColumnIndex == 2 && e.RowIndex == -1)
            {
                checkBoxAll_rakurakuCsv.Checked = !checkBoxAll_rakurakuCsv.Checked;
                SeikyuuDenpyouItiran.Refresh();
                checkBoxAll_rakurakuCsv.Focus();
            }
            // 20211207 thucp v2.24_電子請求書 #157799 end

            if (e.ColumnIndex == 3 && e.RowIndex == -1)
            {
                checkBoxAll_zumi.Checked = !checkBoxAll_zumi.Checked;
                SeikyuuDenpyouItiran.Refresh();
                checkBoxAll_zumi.Focus();
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void SeikyuuDenpyouItiran_Enter(object sender, EventArgs e)
        //{
        //    if (this.SeikyuuDenpyouItiran.CurrentRow != null)
        //    {
        //        this.SeikyuuDenpyouItiran.CurrentCell = this.SeikyuuDenpyouItiran.CurrentRow.Cells["SEIKYUU_NUMBER"];
        //    }
        //}
        /// <summary>
        /// 発行列すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SeikyuuDenpyouItiran.Rows.Count == 0)
            {
                return;
            }
            var currentCell = this.SeikyuuDenpyouItiran.CurrentCell;
            this.SeikyuuDenpyouItiran.CurrentCell = null;
            foreach (DataGridViewRow row in this.SeikyuuDenpyouItiran.Rows)
            {
                row.Cells["HAKKOU"].Value = checkBoxAll.Checked;
            }
            this.SeikyuuDenpyouItiran.CurrentCell = currentCell;
            this.SeikyuuDenpyouItiran.Refresh();
        }

        /// <summary>
        /// 発行済みチェック列すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAll_zumi_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SeikyuuDenpyouItiran.Rows.Count == 0 || !this.IsCheckedChangedEventRun)
            {
                return;
            }
            var currentCell = this.SeikyuuDenpyouItiran.CurrentCell;
            this.SeikyuuDenpyouItiran.CurrentCell = null;
            foreach (DataGridViewRow row in this.SeikyuuDenpyouItiran.Rows)
            {
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                row.Cells["HAKKOUZUMI"].Value = checkBoxAll_zumi.Checked;
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
            }
            this.SeikyuuDenpyouItiran.CurrentCell = currentCell;
            this.SeikyuuDenpyouItiran.Refresh();
        }

        #endregion

        private void SEIKYUSHO_PRINTDAY_TextChanged(object sender, EventArgs e)
        {
            this.logic.CdtSiteiPrintHidukeEnable(SEIKYUSHO_PRINTDAY.Text);
        }

        /// <summary>
        /// フォームクローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 電子CSVチェック列すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAll_densiCsv_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SeikyuuDenpyouItiran.Rows.Count == 0)
            {
                return;
            }
            var currentCell = this.SeikyuuDenpyouItiran.CurrentCell;
            this.SeikyuuDenpyouItiran.CurrentCell = null;
            foreach (DataGridViewRow row in this.SeikyuuDenpyouItiran.Rows)
            {
                row.Cells["DETAIL_OUTPUT_KBN"].Value = checkBoxAll_densiCsv.Checked;
            }
            this.SeikyuuDenpyouItiran.CurrentCell = currentCell;
            this.SeikyuuDenpyouItiran.Refresh();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            this.logic.CSV();
        }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        // 20211207 thucp v2.24_電子請求書 #157799 begin
        private void checkBoxAll_rakurakuCsv_CheckedChanged(object sender, EventArgs e)
        {
            if (SeikyuuDenpyouItiran.Rows.Count == 0)
            {
                return;
            }
            var currentCell = SeikyuuDenpyouItiran.CurrentCell;
            SeikyuuDenpyouItiran.CurrentCell = null;
            foreach (DataGridViewRow row in SeikyuuDenpyouItiran.Rows)
            {
                row.Cells["RAKURAKU_CSV_OUTPUT"].Value = checkBoxAll_rakurakuCsv.Checked;
            }
            SeikyuuDenpyouItiran.CurrentCell = currentCell;
            SeikyuuDenpyouItiran.Refresh();
        }
        // 20211207 thucp v2.24_電子請求書 #157799 begin
    }
}
