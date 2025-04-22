using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Accessor;

namespace Shougun.Core.Billing.SeikyuShimeShori
{
    public partial class G101_SeikyuShimeShoriForm : SuperForm
    {
        #region フィールド

        private SeikyuShimeShori.G101_SeikyuShimeShoriLogic shimeshoriLogic;

        internal UIHeader header;

        /// <summary>
        /// チェックボックスのスペースキー対応用
        /// </summary>
        internal bool SpaceChk = false;
        internal bool SpaceON = false;

        #endregion

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private r_framework.Logic.IBuisinessLogic logic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        public G101_SeikyuShimeShoriForm(UIHeader headerForm)
            : base(WINDOW_ID.T_SEIKYU_SHIME, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart(WINDOW_ID.T_SEIKYU_SHIME, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.shimeshoriLogic = new G101_SeikyuShimeShoriLogic(this);

            //================================CurrentUserCustomConfigProfile.xmlを読み込み=======================
            XMLAccessor fileAccess = new XMLAccessor();
            CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
            //ヘッダに表示予定の値
            this.header = headerForm;
            int kyotenCd;
            if (int.TryParse(configProfile.ItemSetVal1, out kyotenCd))
            {
                this.header.KYOTEN_CD.Text = String.Format("{0:D2}", kyotenCd);
            }

            // ヘッダタイトル名称の設定
            SetHeaderTittle();

            //logicにも設定
            this.shimeshoriLogic.headerKyotenCd = configProfile.ItemSetVal1;

            //拠点名称取得
            this.shimeshoriLogic.GetHeaderInfo(this.header);
            //ヘッダ情報設定
            this.shimeshoriLogic.SetHeader(this.header);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            LogUtility.DebugMethodEnd(WINDOW_ID.T_SEIKYU_SHIME, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        #region ヘッダタイトルの設定

        /// <summary>
        /// ヘッダタイトルの設定を実行する
        /// </summary>
        internal void SetHeaderTittle()
        {
            LogUtility.DebugMethodStart();

            if (!this.shimeshoriLogic.shimeChangeFlag)
            {
                this.header.headerTittle = "期間請求締処理";
            }
            else
            {
                this.header.headerTittle = "伝票請求締処理";
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面コントロールイベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);
            this.header.SetHeaderTittle();            
            //伝票種類イベント
            //tb_denpyoushurui.TextChanged += new EventHandler(tb_denpyoushurui_TextChanged);
            //DGVチェックボックス制御
            this.customDataGridView1.CellContentClick += new DataGridViewCellEventHandler(customDataGridView1_CellContentClick);
            //160013 S
            if (!this.shimeshoriLogic.WindowInit())        // 画面情報の初期化
            {
                return;
            }
            // Anchorの設定は必ずOnLoadで行うこと
            if (this.pnlGoukei != null)
            {
                this.pnlGoukei.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            }
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            }
            //160013 E
            ////VAN 20210502 #148577 S
            //if (this.lb_goukeikingaku != null)
            //{
            //    this.lb_goukeikingaku.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            //}
            //if (this.tb_goukeikingaku != null)
            //{
            //    this.tb_goukeikingaku.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            //}

            //if (this.lb_goukeikingaku_nyuukin != null)
            //{
            //    this.lb_goukeikingaku_nyuukin.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            //}
            //if (this.tb_goukeikingaku_nyuukin != null)
            //{
            //    this.tb_goukeikingaku_nyuukin.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            //}
            ////VAN 20210502 #148577 E
            LogUtility.DebugMethodEnd(e);
        }

        ///// <summary>
        ///// 初回表示イベント 160013
        ///// </summary>
        ///// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        #endregion

        #region 伝票種類選択(RadioButtonと連動)イベント

        /// <summary>
        /// 伝票種類選択(RadioButtonと連動)イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        //internal void tb_denpyoushurui_TextChanged(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);
        //    //設定値を保持
        //    this.shimeshoriLogic.beforeDenpyouShurui = this.tb_denpyoushurui.Text;

        //    //switch (this.tb_denpyoushurui.Text)
        //    //{
        //    //    case "1":
        //    //        this.rb_Button1.Checked = true;
        //    //        this.rb_Button2.Checked = false;
        //    //        this.rb_Button3.Checked = false;
        //    //        this.rb_Button4.Checked = false;
        //    //        this.rb_Button1.Focus();
        //    //        //設定値を保持
        //    //        this.shimeshoriLogic.beforeDenpyouShurui = "1";
        //    //        break;

        //    //    case "2":
        //    //        this.rb_Button1.Checked = false;
        //    //        this.rb_Button2.Checked = true;
        //    //        this.rb_Button3.Checked = false;
        //    //        this.rb_Button4.Checked = false;
        //    //        this.rb_Button2.Focus();
        //    //        //設定値を保持
        //    //        this.shimeshoriLogic.beforeDenpyouShurui = "2";
        //    //        break;

        //    //    case "3":
        //    //        this.rb_Button1.Checked = false;
        //    //        this.rb_Button2.Checked = false;
        //    //        this.rb_Button3.Checked = true;
        //    //        this.rb_Button1.Checked = false;
        //    //        this.rb_Button3.Focus();
        //    //        //設定値を保持
        //    //        this.shimeshoriLogic.beforeDenpyouShurui = "3";
        //    //        break;

        //    //    case "4":
        //    //        this.rb_Button1.Checked = false;
        //    //        this.rb_Button2.Checked = false;
        //    //        this.rb_Button3.Checked = false;
        //    //        this.rb_Button4.Checked = true;
        //    //        this.rb_Button4.Focus();
        //    //        //設定値を保持
        //    //        this.shimeshoriLogic.beforeDenpyouShurui = "4";
        //    //        break;

        //    //    default:
        //    //        this.rb_Button1.Checked = true;
        //    //        this.rb_Button2.Checked = false;
        //    //        this.rb_Button3.Checked = false;
        //    //        this.rb_Button4.Checked = false;
        //    //        this.rb_Button1.Focus();
        //    //        //設定値を保持
        //    //        this.shimeshoriLogic.beforeDenpyouShurui = "1";
        //    //        break;

        //    //}
        //    LogUtility.DebugMethodEnd(sender, e);
        //}

        #endregion

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        #region 画面上でEscapeキー押下イベント

        /// <summary>
        /// 画面上でEscapeキー押下イベント(Focus移動)
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void G101_SeikyuShimeShoriForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.KeyCode == Keys.Escape)
            {
                this.shimeshoriLogic.form_PreviewKeyDown(sender, e);
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        private void customDataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }

        //チェックボックス制御=======================================

        #region DataGridViewのチェックボックス制御処理

        /// <summary>
        /// DataGridViewのチェックボックス制御処理の実行
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void customDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.shimeshoriLogic.customDataGridView1_CellContentClick(sender, e);
        }

        #endregion

        #region DBGRIDVIEW 列ヘッダーにチェックボックスを表示

        #region 列ヘッダーにチェックボックスを表示

        /// <summary>
        /// 列ヘッダーにチェックボックスを表示
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void customDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.RowIndex == -1)
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
                        int x = (e.CellBounds.Width - bmp.Width) / 2;
                        int y = (e.CellBounds.Height - bmp.Height) / 2;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt2);
                        e.Handled = true;
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(100, 100))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // 描画領域の中央に配置
                        Point pt1 = new Point((bmp.Width - checkBoxAllSaiShime.Width) / 2, (bmp.Height - checkBoxAllSaiShime.Height + 28) / 2);
                        if (pt1.X < 0) pt1.X = 0;
                        if (pt1.Y < 0) pt1.Y = 0;

                        // Bitmapに描画
                        checkBoxAllSaiShime.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                        // DataGridViewの現在描画中のセルの中央に描画
                        int x = (e.CellBounds.Width - bmp.Width) / 2;
                        int y = (e.CellBounds.Height - bmp.Height) / 2;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt2);
                        e.Handled = true;
                    }
                }
                else
                {
                    if (this.customDataGridView1[e.ColumnIndex,e.RowIndex].ReadOnly)
                    {
                        //e.PaintBackground(e.CellBounds, false);
                        Rectangle r = e.CellBounds;
                        r.Width = 15;
                        r.Height = 15;
                        r.X += e.CellBounds.Width / 2 - 8;
                        r.Y += e.CellBounds.Height / 2 - 7;
                        ControlPaint.DrawCheckBox(e.Graphics, r, ButtonState.Flat | ButtonState.Inactive);
                        e.Handled = true;
                    }
                }
            }
        }

        #endregion

        #region 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え

        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void customDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if ((!"0".Equals(this.cb_shimebi.Text) && IsPeriodPattern())
            //    || !IsPeriodPattern())
            //{
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                checkBoxAll.Checked = !checkBoxAll.Checked;
                this.customDataGridView1.Refresh();
            }
            else if (e.ColumnIndex == 1 && e.RowIndex == -1 && !this.customDataGridView1.Columns[1].ReadOnly)
            {
                checkBoxAllSaiShime.Checked = !checkBoxAllSaiShime.Checked;
                this.customDataGridView1.Refresh();
            }

            if (!IsPeriodPattern())
            {
                #region 適格請求書

                string chkOnOff = "OFF";
                int F9FLG = 0;
                string POPDenshuKBN = string.Empty;
                string POPDenNO = string.Empty;
                string POPDate = string.Empty;

                int i = 0;
                DialogResult result = DialogResult.None;

                //[締]をクリック
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    if ((bool)this.SpaceChk)
                    {
                        if ((bool)this.SpaceON)
                        {
                            //スペースでチェックON
                            chkOnOff = "ON";
                        }
                    }
                    else
                    {
                        if ((this.customDataGridView1[0, e.RowIndex].Value == null)
                            || (!(bool)this.customDataGridView1[0, e.RowIndex].Value))
                        {
                            //マウスでチェックでON
                            chkOnOff = "ON";
                        }
                    }

                    //明細チェック(選択している明細行以外で、チェックONの明細の税計算区分の情報をカウント
                    foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                    {
                        if ((i != e.RowIndex) && ((bool)row.Cells[0].Value))
                        {
                            if ("伝票毎".Equals(row.Cells["ZEI_KEISAN_KBN_NAME"].Value))
                            {
                                if (F9FLG == 0 &&
                                    ((row.Cells["HINMEI_SOTO_ZEI_COUNT"].Value.ToString() != "0") || 
                                    (row.Cells["HINMEI_NASI_ZEI_COUNT"].Value.ToString() != "0")))
                                {
                                    F9FLG = 1;  //伝票毎外税・品名外税ありの場合、F9非活性
                                }
                                //DenCount += 1;
                            }
                            else if ("請求毎".Equals(row.Cells["ZEI_KEISAN_KBN_NAME"].Value))
                            {
                                if (F9FLG == 0 &&
                                    (row.Cells["HINMEI_SOTO_ZEI_COUNT"].Value.ToString() != "0"))
                                {
                                    F9FLG = 1;  //品名外税ありの場合、F9非活性
                                }
                                //SeiCount += 1;
                            }
                            else if ("明細毎".Equals(row.Cells["ZEI_KEISAN_KBN_NAME"].Value))
                            {
                                if (F9FLG == 0 &&
                                    ((row.Cells["HINMEI_SOTO_ZEI_COUNT"].Value.ToString() != "0") ||
                                    ((row.Cells["HINMEI_NASI_ZEI_COUNT"].Value.ToString() != "0") && row.Cells["URIAGE_ZEI_KBN_CD"].Value.ToString() == "1")))
                                {
                                    F9FLG = 1;  //明細毎外税・品名外税ありの場合、F9非活性
                                }
                                //MeiCount += 1;
                            }
                        }
                        else if (i == e.RowIndex)
                        {
                            POPDenshuKBN = row.Cells["DENPYOUSHURUI"].Value.ToString();
                            POPDenNO = row.Cells["DENPYOU_NUMBER"].Value.ToString();
                            POPDate = row.Cells["DENPYOU_DATE"].Value.ToString();
                        }
                        i++;
                    }

                    if (chkOnOff.Equals("ON"))
                    {
                        //レイアウト区分 1.適格請求書
                        if (this.header.INVOICE_KBN.Text == "1")
                        {
                            //品名外税ありor明細毎外税
                            if ((this.customDataGridView1.Rows[e.RowIndex].Cells["HINMEI_SOTO_ZEI_COUNT"].Value.ToString() != "0") ||
                                (("明細毎".Equals(this.customDataGridView1.Rows[e.RowIndex].Cells["ZEI_KEISAN_KBN_NAME"].Value)) &&
                                (this.customDataGridView1.Rows[e.RowIndex].Cells["HINMEI_NASI_ZEI_COUNT"].Value.ToString() != "0") &&
                                (this.customDataGridView1.Rows[e.RowIndex].Cells["URIAGE_ZEI_KBN_CD"].Value.ToString() == "1")))
                            {
                                result = MessageBox.Show(string.Format("適格請求書では、明細毎の消費税計算が行えません。\r締処理から除外となりますがよろしいですか？\r(伝票種類：{0}、伝票番号：{1}、伝票日付：{2})"
                                                , POPDenshuKBN
                                                , POPDenNO
                                                , POPDate), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            }
                            else if ("伝票毎".Equals(this.customDataGridView1.Rows[e.RowIndex].Cells["ZEI_KEISAN_KBN_NAME"].Value) &&
                                                    (this.customDataGridView1.Rows[e.RowIndex].Cells["HINMEI_NASI_ZEI_COUNT"].Value.ToString() != "0"))
                            {
                                //伝票毎品名税なし
                                result = MessageBox.Show("伝票毎の消費税計算では、\r適格請求書が作成できませんがよろしいですか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            }

                            //POPの分岐処理
                            if (result == DialogResult.Yes)
                            {
                                var parentForm = (BusinessBaseForm)this.Parent;
                                parentForm.bt_func9.Enabled = false;
                                parentForm.bt_process4.Enabled = false;
                                parentForm.bt_process5.Enabled = false;
                            }
                            else if (result == DialogResult.No)
                            {
                                this.customDataGridView1[0, e.RowIndex].Value = !(bool)this.SpaceChk;
                                this.SpaceChk = false;
                            }
                        }

                        //チェックボックスの更新処理
                        if (this.SpaceChk)
                        {
                            if (this.customDataGridView1[0, e.RowIndex].Value == null)
                            {
                                this.customDataGridView1[0, e.RowIndex].Value = true;
                            }
                            else
                            {
                                this.customDataGridView1[0, e.RowIndex].Value = !(bool)this.customDataGridView1[0, e.RowIndex].Value;
                            }
                        }
                    }
                    else if (chkOnOff.Equals("OFF"))
                    {
                        //レイアウト区分 1.適格請求書
                        if ((this.header.INVOICE_KBN.Text == "1") && (F9FLG != 1))
                        {
                            //チェックを外した行以外で、条件に合致しない場合、→F9[実行]を使用かにする
                            var parentForm = (BusinessBaseForm)this.Parent;
                            parentForm.bt_func9.Enabled = true;
                            parentForm.bt_process4.Enabled = true;
                            parentForm.bt_process5.Enabled = true;
                        }
                    }
                    this.SpaceChk = false;
                }
                #endregion 適格請求書

                // 合計金額の再計算
                this.shimeshoriLogic.CalcGoukeiKingaku();
            }
            //}
        }

        #endregion

        #region すべての行のチェック状態を切り替える

        /// <summary>
        /// すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.customDataGridView1.Rows.Count == 0) 160013
            //{
            //    return;
            //}

            #region 適格請求書
            var parentForm = (BusinessBaseForm)this.Parent;
            if (IsPeriodPattern() || !checkBoxAll.Checked)
            {
                //期間締 or チェックOFF
                foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                {
                    row.Cells[0].Value = checkBoxAll.Checked;
                }
                parentForm.bt_func9.Enabled = true;
                if (!IsPeriodPattern())
                {
                    parentForm.bt_process4.Enabled = true;
                    parentForm.bt_process5.Enabled = true;
                }
            }
            else
            {
                //伝票締 and チェックON
                foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                {
                    if (this.header.INVOICE_KBN.Text == "1")
                    {
                        //レイアウト区分 1.適格請求書
                        //請求毎・入金伝票の場合はチェックをON
                        //伝票毎・明細毎の場合はチェックをOFF
                        if (row.Cells["DENPYOU_NUMBER"].Value != null)
                        {
                            if ("伝票毎".Equals(row.Cells["ZEI_KEISAN_KBN_NAME"].Value))
                            {
                                //伝票毎(品名税なしor品名外税)の場合、チェックOFF
                                if ((row.Cells["HINMEI_SOTO_ZEI_COUNT"].Value.ToString() != "0") ||
                                    (row.Cells["HINMEI_NASI_ZEI_COUNT"].Value.ToString() != "0"))
                                {
                                    row.Cells[0].Value = false;
                                }
                                else
                                {
                                    row.Cells[0].Value = checkBoxAll.Checked;
                                }
                            }
                            else if ("請求毎".Equals(row.Cells["ZEI_KEISAN_KBN_NAME"].Value))
                            {
                                //請求毎(品名外税)の場合、チェックOFF
                                if (row.Cells["HINMEI_SOTO_ZEI_COUNT"].Value.ToString() != "0")
                                {
                                    row.Cells[0].Value = false;
                                }
                                else
                                {
                                    row.Cells[0].Value = checkBoxAll.Checked;
                                }
                            }
                            else if ("明細毎".Equals(row.Cells["ZEI_KEISAN_KBN_NAME"].Value))
                            {
                                //明細毎(品名外税 or 品名税なしand外税)の場合、締チェックOFF
                                if ((row.Cells["HINMEI_SOTO_ZEI_COUNT"].Value.ToString() != "0") ||
                                    ((row.Cells["HINMEI_NASI_ZEI_COUNT"].Value.ToString() != "0") && (row.Cells["URIAGE_ZEI_KBN_CD"].Value.ToString() == "1")))
                                {
                                    row.Cells[0].Value = false;
                                }
                                else
                                {
                                    row.Cells[0].Value = checkBoxAll.Checked;
                                }
                            }
                            else
                            {
                                row.Cells[0].Value = checkBoxAll.Checked;
                            }
                        }
                    }
                    else
                    {
                        //レイアウト区分 2.旧請求書
                        //全明細チェックをON
                        row.Cells[0].Value = checkBoxAll.Checked;
                    }
                }

                parentForm.bt_func9.Enabled = true;
                parentForm.bt_process4.Enabled = true;
                parentForm.bt_process5.Enabled = true;

            }
            #endregion 適格請求書

            if (this.customDataGridView1.Rows.Count > 0)//160013
            {
                this.customDataGridView1.CurrentCell = null;
                this.customDataGridView1.CurrentCell = this.customDataGridView1.Rows[0].Cells[0];
            }
        }

        /// <summary>
        /// すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void checkBoxAllSaiShime_CheckedChanged(object sender, EventArgs e)
        {
            if (this.customDataGridView1.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in this.customDataGridView1.Rows)
            {
                row.Cells[1].Value = checkBoxAllSaiShime.Checked;
            }

            this.customDataGridView1.CurrentCell = null;
            this.customDataGridView1.CurrentCell = this.customDataGridView1.Rows[0].Cells[1];
        }
        #endregion

        #region 締め処理区分が期間か判定

        /// <summary>
        /// 締め処理区分が期間か判定
        /// </summary>
        /// <returns></returns>
        private bool IsPeriodPattern()
        {
            if (!this.shimeshoriLogic.shimeChangeFlag)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// 取引先CDのバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            var ret = this.shimeshoriLogic.CheckTorihikisakiKyoten();
            if (!ret)
            {
                e.Cancel = true;
            }
            //// 取引先と拠点の関係をチェック
            //if (!string.IsNullOrEmpty(this.tb_torihikisaki_cd.Text) &&
            //!string.IsNullOrEmpty(this.tb_kyoten_cd.Text))
            //{

            //    if (!this.shimeshoriLogic.CheckTorihikisakiKyoten())
            //    {
            //        this.tb_torihikisaki_cd.Focus();
            //        this.tb_torihikisaki_name.Text = string.Empty;
            //        return;
            //    }
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        public void TorihikisakiPopupAfterMethod()
        {
            this.shimeshoriLogic.Control_Validated(this.tb_torihikisaki_cd, new EventArgs());
        }
        /// <summary>
        /// 
        /// </summary>
        public void TorihikisakiPopupBeforeMethod()
        {
            this.shimeshoriLogic.Control_Enter(this.tb_torihikisaki_cd, new EventArgs());
        }
        /// <summary>
        /// 拠点CDのエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_kyoten_cd_Enter(object sender, EventArgs e)
        {
            this.KyotenPopupBeforeMethod();
        }

        /// <summary>
        /// 拠点CDのバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_kyoten_cd_Validating(object sender, CancelEventArgs e)
        {
            //// 拠点のチェック
            //this.CheckKyoten();
        }
        public void KyotenPopupAfterMethod()
        {
            this.shimeshoriLogic.Control_Validated(this.tb_kyoten_cd, new EventArgs());
        }
        ///// <summary>
        ///// 拠点のチェックを行う
        ///// </summary>
        //public void CheckKyoten()
        //{

        //    // 拠点マスタ検索処理
        //    this.shimeshoriLogic.GetKyotenCdAndName();
        //}

        /// <summary>
        /// 拠点CDの前回値を保持します
        /// </summary>
        public void KyotenPopupBeforeMethod()
        {
            //// 拠点CDの前回値を保持
            //this.shimeshoriLogic.oldKyotenCd = this.tb_kyoten_cd.Text;
            this.shimeshoriLogic.Control_Enter(this.tb_kyoten_cd, new EventArgs());
        }

        private void customDataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            #region 適格請求書
            if (!IsPeriodPattern())
            {

                if (e.KeyCode == Keys.Space)
                {
                    DataGridViewCell curCell = this.customDataGridView1.CurrentCell;
                    //[締]
                    if (curCell.ColumnIndex == 0 && curCell.RowIndex >= 0)
                    {
                        this.SpaceChk = true;
                        this.SpaceON = false;

                        //[締]OFFにする場合は、何もしない。
                        //[締]ONにする場合は、一度チェックボックスを反転させておく(チェック処理中に画面上ONになってしまうので)
                        if (this.customDataGridView1[0, curCell.RowIndex].Value == null)
                        {
                            this.SpaceON = true;
                            this.customDataGridView1[0, curCell.RowIndex].Value = true;
                        }
                        else
                        {
                            if (!(bool)this.customDataGridView1[0, curCell.RowIndex].Value)
                            {
                                this.SpaceON = true;
                                this.customDataGridView1[0, curCell.RowIndex].Value = !(bool)this.customDataGridView1[0, curCell.RowIndex].Value;
                            }
                        }
                    }
                }
            }
            #endregion 適格請求書
        }
    }
}