using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DataGridViewCheckBoxColumnHeeader;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Logic;
using Seasar.Quill.Attrs;
using Shougun.Core.PapeMranifest.HenkyakuIchiran;

namespace Shougun.Core.PaperManifest.HenkyakuIchiran
{
    [Implementation]
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        private LogicClass kensakuLogic;

        private string selectQuery = string.Empty;

        private string orderByQuery = string.Empty;

        private Boolean isLoaded;
        private string tempHENSOU_GYOUSHA_CD = string.Empty;  //Thang Add 20150709 #11222
        private string tempHAISHUTSUJIGYOUSYA_CD = string.Empty;
        private string tempSBNGYOUSHA_CD = string.Empty;//157020
        /// <summary>
        /// 画面ロジック
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.HENKYAKUBI_ICHIRAN, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.kensakuLogic = new LogicClass(this);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;
            //Main画面で社員コード値を取得すること
            kensakuLogic.syainCode = SystemProperty.Shain.CD;
            //伝種区分を取得すること
            DENSHU_KBN time = (DENSHU_KBN)Enum.Parse(typeof(DENSHU_KBN), "HENKYAKUBI_ICHIRAN", true);
            kensakuLogic.denShu_Kbn = (int)time;
            isLoaded = false;
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 20151125 A～E票セルで入力システムエラーが発生するの不具合対応 Start
            this.customDataGridView1.IsBrowsePurpose = false;
            // 20151125 A～E票セルで入力システムエラーが発生するの不具合対応 End

            if (isLoaded == false)
            {
                this.customSortHeader1.Location = new System.Drawing.Point(3, 180);
                this.customDataGridView1.Location = new System.Drawing.Point(3, 210);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 240);
                this.bt_ptn1.Top -= 7;
                this.bt_ptn2.Top -= 7;
                this.bt_ptn3.Top -= 7;
                this.bt_ptn4.Top -= 7;
                this.bt_ptn5.Top -= 7;

                if (!kensakuLogic.WindowInit())
                {
                    return;
                }
            }

            // パターン読み込み（初回のみデフォルト選択）
            this.PatternReload(!isLoaded);

            this.kensakuLogic.selectQuery = this.SelectQuery;
            this.kensakuLogic.orderByQuery = this.OrderByQuery;
            this.kensakuLogic.joinQuery = this.JoinQuery;
            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                if (this.Table != null)
                {
                    this.logic.CreateDataGridView(this.Table);

                    if (isLoaded == false)
                    {
                        ////選択チェックボックス作成
                        DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                        column.Name = "CHECKBOX";
                        column.DataPropertyName = "CHECKBOX";
                        column.ReadOnly = false;
                        column.Width = 50;
                        column.DefaultCellStyle.Tag = "処理対象とする場合はチェックしてください";
                        DataGridviewCheckboxHeaderCell newheader = new DataGridviewCheckboxHeaderCell();
                        newheader.ToolTipText = "処理対象とする場合はチェックしてください";
                        newheader.OnCheckBoxClicked += new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.
                                datagridviewcheckboxHeaderEventHander(this.kensakuLogic.ch_OnCheckBoxClicked);
                        column.HeaderCell = newheader;
                        column.HeaderText = string.Empty;
                        this.customDataGridView1.Columns.Insert(0, column);
                    }
                }
                this.kensakuLogic.GetSysInfoInit();  //CongBinh 20200330 #134989
                this.kensakuLogic.SetKyoten();//155769
            }

            //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 start
            //並び順ソートヘッダー
            this.customSortHeader1.ClearCustomSortSetting();
            //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 end

            isLoaded = true;

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
            base.OnShown(e);
            if (this.kensakuLogic.headForm != null)
            {
                this.kensakuLogic.headForm.txtNum_BarcodeKubun.Focus();
            }
            this.customDataGridView1.TabIndex = 13;
        }

        /// <summary>
        /// 一括入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void F_Put(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Kensaku(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Touroku(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        #region 検索結果表示

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowData()
        {
            //バーコードリーダーの処理を追加
            if (this.kensakuLogic.barcodeFlg == true)
            {
                //バーコードの場合
                this.Table = this.kensakuLogic.SearchBarcodeResult;
            }
            else
            {
                this.Table = this.kensakuLogic.SearchResult;
            }

            this.customDataGridView1.Columns["CHECKBOX"].Visible = false;
            this.customSortHeader1.SortDataTable(Table); // まず抽出データをソートしてから
            this.customDataGridView1.Columns["CHECKBOX"].Visible = true;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
                foreach (var row in this.customDataGridView1.Rows.Cast<DataGridViewRow>())
                {
                    foreach (var cell in row.Cells.Cast<DataGridViewCell>())
                    {
                        cell.UpdateBackColor(false);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_Hidsuke_TextChanged(object sender, EventArgs e)
        {
            if ("1".Equals(this.txtNum_Hidsuke.Text))
            {
                this.radbtn_Unpanshuryobi.Checked = true;
            }
            if ("2".Equals(this.txtNum_Hidsuke.Text))
            {
                this.radbtn_shobunshuryobi.Checked = true;
            }
            if ("3".Equals(this.txtNum_Hidsuke.Text))
            {
                this.radbtn_SaishuShobun.Checked = true;
            }
            if (!string.IsNullOrEmpty(this.txtNum_Hidsuke.Text) &&
                !"1".Equals(this.txtNum_Hidsuke.Text) &&
                !"2".Equals(this.txtNum_Hidsuke.Text) &&
                !"3".Equals(this.txtNum_Hidsuke.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E042", "1～3");
            }
        }

        private void radbtn_Unpanshuryobi_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtn_Unpanshuryobi.Checked)
            {
                this.txtNum_Hidsuke.Text = "1";
            }
        }

        private void radbtn_shobunshuryobi_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtn_shobunshuryobi.Checked)
            {
                this.txtNum_Hidsuke.Text = "2";
            }
        }

        private void radbtn_SaishuShobun_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtn_SaishuShobun.Checked)
            {
                this.txtNum_Hidsuke.Text = "3";
            }
        }

        private void txtNum_Hencyakujyoutai_TextChanged(object sender, EventArgs e)
        {
            if ("1".Equals(this.txtNum_Hencyakujyoutai.Text))
            {
                this.radbtn_Hencyakuzumi.Checked = true;
            }
            if ("2".Equals(this.txtNum_Hencyakujyoutai.Text))
            {
                this.radbtn_Mihencyaku.Checked = true;
            }
            if (!string.IsNullOrEmpty(this.txtNum_Hencyakujyoutai.Text) &&
                !"1".Equals(this.txtNum_Hencyakujyoutai.Text) &&
                !"2".Equals(this.txtNum_Hencyakujyoutai.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E042", "1～2");
            }
        }

        private void radbtn_Hencyakuzumi_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtn_Hencyakuzumi.Checked)
            {
                this.txtNum_Hencyakujyoutai.Text = "1";
            }
        }

        private void radbtn_Mihencyaku_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtn_Mihencyaku.Checked)
            {
                this.txtNum_Hencyakujyoutai.Text = "2";
            }
        }
               

        //返却状態入力制約
        private void txtNum_Hencyakujyoutai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '1' || '2' < e.KeyChar)
            {
                //押されたキーが 1～2でない場合は、イベントをキャンセルする
                e.Handled = true;
            }
        }

        //日付力制約
        private void txtNum_Hidukei_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '1' || '3' < e.KeyChar)
            {
                //押されたキーが 1～3でない場合は、イベントをキャンセルする
                e.Handled = true;
            }
        }

        //日付一括入力制約
        private void txtNum_Hidukeikatsunyuryoku_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        //日付deleteキー制約
        private void txtNum_Hidukei_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                txtNum_Hidsuke.Text = "";
                radbtn_Unpanshuryobi.Checked = false;
                radbtn_shobunshuryobi.Checked = false;
                radbtn_SaishuShobun.Checked = false;
            }
        }

        //返却状態deleteキー制約
        private void txtNum_Hencyakujyoutai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                txtNum_Hencyakujyoutai.Text = "";
                radbtn_Hencyakuzumi.Checked = false;
                radbtn_Mihencyaku.Checked = false;
            }
        }

        //日付一括入力deleteキー制約
        private void txtNum_Hidukeikatsunyuryoku_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void HIDSUKE_Enter(object sender, EventArgs e)
        {
        }

        private void HIDSUKE_HIDSUKE_TO_Enter(object sender, EventArgs e)
        {
        }

        private void IKATSU_NYURYOKU_Enter(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// パターンボタン更新処理
        /// </summary>
        /// <param name="sender">イベント対象オブジェクト</param>
        /// <param name="e">イベントクラス</param>
        /// <param name="ptnNo">パターンNo(0はデフォルトパターンを表示)</param>
        public void PatternButtonUpdate(object sender, System.EventArgs e, int ptnNo = -1)
        {
            if (ptnNo != -1) this.PatternNo = ptnNo;
            this.OnLoad(e);
        }

        private void HAISHUTSUJIGYOUSYA_CD_Validated(object sender, EventArgs e)
        {
            //排出事業者チェック
            switch (this.kensakuLogic.ChkGyosya(HAISHUTSUJIGYOUSYA_CD, "HAISHUTSU_NIZUMI_GYOUSHA_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //排出業者削除
                    this.kensakuLogic.HaisyutuGyousyaDel();
                    // 排出事業場初期化
                    this.HAISHUTSUJIGYOUBA_CD.Text = string.Empty;
                    this.HAISHUTSUJIGYOUBA_NAME_RYAKU.Text = string.Empty;

                    return;

                case 2://エラー
                    //排出業者削除
                    this.kensakuLogic.HaisyutuGyousyaDel();
                    // 排出事業場初期化
                    this.HAISHUTSUJIGYOUBA_CD.Text = string.Empty;
                    this.HAISHUTSUJIGYOUBA_NAME_RYAKU.Text = string.Empty;
                    return;
            }
            //排出業者　設定
            this.kensakuLogic.SetAddressGyousha(HAISHUTSUJIGYOUSYA_CD, HAISHUTSUJIGYOUSYA_NAME_RYAKU);

            if (tempHAISHUTSUJIGYOUSYA_CD == this.HAISHUTSUJIGYOUSYA_CD.Text)
                return;

            // 排出事業場初期化
            this.HAISHUTSUJIGYOUBA_CD.Text = string.Empty;
            this.HAISHUTSUJIGYOUBA_NAME_RYAKU.Text = string.Empty;
        }

        /// <summary>
        /// 排出業者CD PopupAfterExecuteMethod
        /// </summary>
        public void HAISHUTSUJIGYOUSYA_CD_PopupAfterExecuteMethod()
        {
            if (tempHAISHUTSUJIGYOUSYA_CD == this.HAISHUTSUJIGYOUSYA_CD.Text)
                return;

            // 排出事業場初期化
            this.HAISHUTSUJIGYOUBA_CD.Text = string.Empty;
            this.HAISHUTSUJIGYOUBA_NAME_RYAKU.Text = string.Empty;

            //排出事業者チェック
            switch (this.kensakuLogic.ChkGyosya(HAISHUTSUJIGYOUSYA_CD, "HAISHUTSU_NIZUMI_GYOUSHA_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //排出業者削除
                    this.kensakuLogic.HaisyutuGyousyaDel();

                    return;

                case 2://エラー
                    //排出業者削除
                    this.kensakuLogic.HaisyutuGyousyaDel();
                    return;
            }
            //排出業者　設定
            this.kensakuLogic.SetAddressGyousha(HAISHUTSUJIGYOUSYA_CD, HAISHUTSUJIGYOUSYA_NAME_RYAKU);
            return;
        }

        public void Btn_haishutsuPopupBeforeExecuteMethod()
        {
            tempHAISHUTSUJIGYOUSYA_CD = this.HAISHUTSUJIGYOUSYA_CD.Text;
        }

        public void Btn_haishutsuPopupAfterExecuteMethod()
        {
            HAISHUTSUJIGYOUSYA_CD_PopupAfterExecuteMethod();
        }

        public void Btn_HENSOUPopupBeforeExecuteMethod()
        {
            tempHENSOU_GYOUSHA_CD = this.HENSOU_GYOUSHA_CD.Text;
        }

        public void Btn_HENSOUPopupAfterExecuteMethod()
        {
            HENSOUSAKIGYOUSHA_CD_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 排出業場CD PopupAfterExecuteMethod
        /// </summary>
        public void HAISHUTSUJIGYOUJOU_CD_PopupAfterExecuteMethod()
        {
            //排出事業場チェック
            switch (this.kensakuLogic.ChkGenba(HAISHUTSUJIGYOUSYA_CD, HAISHUTSUJIGYOUBA_CD, this.HAISHUTSUJIGYOUBA_NAME_RYAKU))
            {
                case 0://正常
                    break;

                case 1://空
                    //排出業場削除
                    this.kensakuLogic.HaisyutuGyouBaDel();

                    return;

                case 2://エラー
                    //排出業場削除
                    this.kensakuLogic.HaisyutuGyouBaDel();
                    return;
            }

            return;
        }

        /// <summary>
        /// 返送先(取引先)CD PopupAfterExecuteMethod
        /// </summary>
        public void HENSOUSAKITORIHIKISAKI_CD_PopupAfterExecuteMethod()
        {
            //排出事業場チェック
            switch (this.kensakuLogic.ChkHensouTorihikisaki(HENSOU_TORIHIKISAKI_CD))
            {
                case 0://正常
                    break;

                case 1://空
                    //返送先(取引先)削除
                    this.kensakuLogic.HensouTorihikisakiDel();

                    return;

                case 2://エラー
                    return;
            }

            return;
        }

        /// <summary>
        /// 返送先(業者)CD PopupAfterExecuteMethod
        /// </summary>
        public void HENSOUSAKIGYOUSHA_CD_PopupAfterExecuteMethod()
        {
            if (tempHENSOU_GYOUSHA_CD == this.HENSOU_GYOUSHA_CD.Text)
                return;

            // 返送先(現場)初期化
            this.HENSOU_GENBA_CD.Text = string.Empty;
            this.HENSOU_GENBA_NAME_RYAKU.Text = string.Empty;

            // 返送先(業者)チェック
            switch (this.kensakuLogic.ChkHensouGyosya(HENSOU_GYOUSHA_CD))
            {
                case 0://正常
                    break;

                case 1://空
                    //返送先(業者)削除
                    this.kensakuLogic.HensouGyoushaDel();

                    return;

                case 2://エラー
                    //返送先(業者)削除
                    this.kensakuLogic.HensouGyoushaDel();
                    return;
            }
            // 返送先(業者)　設定
            this.kensakuLogic.SetAddressGyousha(HENSOU_GYOUSHA_CD, HENSOU_GYOUSHA_NAME_RYAKU);

            return;
        }

        /// <summary>
        /// 返送先(現場)CD PopupAfterExecuteMethod
        /// </summary>
        public void HENSOUSAKIGENBA_CD_PopupAfterExecuteMethod()
        {
            //排出事業場チェック
            switch (this.kensakuLogic.ChkHensouGenba(HENSOU_GENBA_CD, this.HENSOU_GENBA_NAME_RYAKU, HENSOU_GYOUSHA_CD.Text))
            {
                case 0://正常
                    break;

                case 1://空
                    //返送先(現場)削除
                    this.kensakuLogic.HensouGenbaDel();

                    return;

                case 2://エラー
                    //返送先(現場)削除
                    this.kensakuLogic.HensouGenbaDel();
                    return;
            }
            return;
        }

        /// <summary>
        /// バーコード区分(オン)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void radbtn_Barcode_On_CheckedChanged(object sender, EventArgs e) //CongBinh 20200330 #134989 
        {
            //コントロール制御
            //非表示
            panel_Barcode_Off.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            KOUFUBANNGO.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label3.Visible = false;
            // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する start
            customPanel6.Visible = false;
            label14.Visible = false;
            // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する end

            //表示
            panel_Barcode_On.Visible = true;

            //F1F8を使用不可にする
            this.kensakuLogic.SetEnableFnc1("2");

            //バーコード用検索結果を初期化
            this.kensakuLogic.SearchBarcodeResult = new DataTable();
            //2014.03.06 バーコードoff時は・Func4を使用不可,キ－ダウン無効にする。by　胡　begin
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.bt_func4.Enabled = true;
            //2014.03.06 バーコードoff時は・Func4を使用不可,キ－ダウン無効にする。by　胡　end

            //2014.03.24 バーコードを「オン」「オフ」を切り替えたとき。by　胡　begin
            //バーコードを「オン」「オフ」を切り替えたとき、明細がクリアされるように修正してください。また、一括入力と同様、バーコード「オン」のときは、検索ボタンをグレーアウトしてください。
            if (this.customDataGridView1.DataSource != null)
            {
                DataTable temp = (DataTable)this.customDataGridView1.DataSource;
                temp.Rows.Clear();
                this.customDataGridView1.DataSource = temp;
            }
            //2014.03.24 バーコードを「オン」「オフ」を切り替えたとき。by　胡　begin

            // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない start
            this.barCode = "";
            // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない end
            //CongBinh 20200330 #134989 S
            this.label15.Visible = false;
            this.IKATSU_NYURYOKU.Visible = false;
            this.label1.Visible = false;
            this.panel2.Visible = false;
            //CongBinh 20200330 #134989 E
        }

        /// <summary>
        /// バーコード区分(オフ)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void radbtn_Barcode_Off_CheckedChanged(object sender, EventArgs e) //CongBinh 20200330 #134989
        {
            //非表示
            panel_Barcode_On.Visible = false;

            //表示
            panel_Barcode_Off.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            KOUFUBANNGO.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label3.Visible = true;
            // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する start
            customPanel6.Visible = true;
            label14.Visible = true;
            // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する end

            //F1F8を使用不可にする
            this.kensakuLogic.SetEnableFnc1("1");

            //2014.03.06 バーコードoff時は・Func4を使用不可,キ－ダウン無効にする。by　胡　begin
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.bt_func4.Enabled = false;
            //2014.03.06 バーコードoff時は・Func4を使用不可,キ－ダウン無効にする。by　胡　end

            //2014.03.24 バーコードを「オン」「オフ」を切り替えたとき。by　胡　begin
            //バーコードを「オン」「オフ」を切り替えたとき、明細がクリアされるように修正してください。
            if (this.customDataGridView1.DataSource != null)
            {
                DataTable temp = (DataTable)this.customDataGridView1.DataSource;
                temp.Rows.Clear();
                this.customDataGridView1.DataSource = temp;
            }
            //2014.03.24 バーコードを「オン」「オフ」を切り替えたとき。by　胡　begin

            // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない start
            this.barCode = "";
            // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない end
            //CongBinh 20200330 #134989 S
            this.label15.Visible = true;
            this.IKATSU_NYURYOKU.Visible = true;
            this.label1.Visible = true;
            this.panel2.Visible = true;
            if ("1".Equals(this.txtNum_Renzoku.Text))
            {
                this.kensakuLogic.parentForm.bt_func8.Enabled = false;
                this.kensakuLogic.SearchBarcodeResult = new DataTable();
            }
            else
            {
                this.kensakuLogic.parentForm.bt_func8.Enabled = true;
            }
            //CongBinh 20200330 #134989 E
        }
         

        /// <summary>
        /// 排出事業場CDイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHUTSUJIGYOUBA_CD_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.HAISHUTSUJIGYOUSYA_CD.Text) && !string.IsNullOrEmpty(this.HAISHUTSUJIGYOUBA_CD.Text))
            {
                //業者未入力の場合はメッセージを表示して処理を終了
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                msgLogic.MessageBoxShow("E051", "排出事業者");
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                this.HAISHUTSUJIGYOUBA_CD.Text = string.Empty;
                this.HAISHUTSUJIGYOUBA_NAME_RYAKU.Text = string.Empty;
                this.HAISHUTSUJIGYOUBA_CD.Focus();
                return;
            }
            //入力チェック
            HAISHUTSUJIGYOUJOU_CD_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 返送(取引先)イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HENSOU_TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            //入力チェック
            HENSOUSAKITORIHIKISAKI_CD_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 返送(業者)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HENSOU_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            // 返送先(業者)チェック
            switch (this.kensakuLogic.ChkHensouGyosya(HENSOU_GYOUSHA_CD))
            {
                case 0://正常
                    break;

                case 1://空
                    //返送先(業者)削除
                    this.kensakuLogic.HensouGyoushaDel();
                    // 返送先(現場)初期化
                    this.HENSOU_GENBA_CD.Text = string.Empty;
                    this.HENSOU_GENBA_NAME_RYAKU.Text = string.Empty;

                    return;

                case 2://エラー
                    //返送先(業者)削除
                    this.kensakuLogic.HensouGyoushaDel();
                    // 返送先(現場)初期化
                    this.HENSOU_GENBA_CD.Text = string.Empty;
                    this.HENSOU_GENBA_NAME_RYAKU.Text = string.Empty;
                    return;
            }
            // 返送先(業者)　設定
            this.kensakuLogic.SetAddressGyousha(HENSOU_GYOUSHA_CD, HENSOU_GYOUSHA_NAME_RYAKU);

            if (tempHENSOU_GYOUSHA_CD == this.HENSOU_GYOUSHA_CD.Text)
                return;

            // 返送先(現場)初期化
            this.HENSOU_GENBA_CD.Text = string.Empty;
            this.HENSOU_GENBA_NAME_RYAKU.Text = string.Empty;
        }

        /// <summary>
        /// 返送(現場)Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HENSOU_GENBA_CD_Validated(object sender, EventArgs e)
        {
            //返送(業者)の入力チェック
            if (string.IsNullOrEmpty(this.HENSOU_GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.HENSOU_GENBA_CD.Text))
            {
                //未入力の場合はメッセージを表示して処理を終了
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                msgLogic.MessageBoxShow("E051", "返送先（業者）");
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                this.HENSOU_GENBA_CD.Text = string.Empty;
                this.HENSOU_GENBA_NAME_RYAKU.Text = string.Empty;
                this.HENSOU_GENBA_CD.Focus();
            }
            else
            {
                //入力チェック
                HENSOUSAKIGENBA_CD_PopupAfterExecuteMethod();
            }
        }

        #region バーコード処理

        // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない start
        public string barCode = "";

        // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない end
        /// <summary>
        /// 交付番号バーコードリーダーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtKoufuNo_Hyou_Validating(object sender, CancelEventArgs e)
        {
            this.logic.IsFocusDGV = true;

            // パターンが設定されていない場合はエラーとする。
            if (this.PatternNo == 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            //交付番号が空の場合は処理を抜ける
            if (string.IsNullOrEmpty(txtKoufuNo_Hyou.Text))
            {
                return;
            }
            int result = this.kensakuLogic.chkBarcode(txtKoufuNo_Hyou.Text);
            bool catchErr = false;
            switch (result)
            {
                case 0:
                    this.kensakuLogic.barcodeFlg = true;
                    //交付番号で対象データを検索
                    if (this.kensakuLogic.Search() == -1) { return; }
                    this.kensakuLogic.barcodeFlg = false;
                    //追加された行を選択行にする。
                    if (this.customDataGridView1.Rows.Count != 0)
                    {
                        // 入力された交付番号のRowを選択
                        bool isExistManiId = false;
                        foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                        {
                            if (row.Cells["hiddenMANIFEST_ID"].Value == null) continue;

                            if (this.txtKoufuNo_Hyou.Text.Equals(row.Cells["hiddenMANIFEST_ID"].Value.ToString()))
                            {
                                this.customDataGridView1.CurrentCell = row.Cells[0];
                                isExistManiId = true;
                                break;
                            }
                        }

                        if (!isExistManiId)
                        {
                            // 入力した交付番号が無ければ最後のRowを選択
                            this.customDataGridView1.CurrentCell = customDataGridView1.Rows[this.customDataGridView1.Rows.Count - 1].Cells[0];
                        }
                        this.txtKoufuNo_Hyou.Focus();
                        if (!string.IsNullOrEmpty(this.barCode))
                        {
                            if (this.kensakuLogic.chkBarcodeHyou(this.barCode, out catchErr))
                            {
                                this.barCode = null;
                            }
                            else
                            {
                                if (catchErr) { return; }
                                this.barCode = "";
                            }
                        }
                    }
                    //「交付番号/票」にフォーカスをセットする。
                    this.logic.IsFocusDGV = false;
                    this.txtKoufuNo_Hyou.Focus();
                    break;

                case 1:
                    //2014.05.16 「交付番号/票」にフォーカスをセットする。
                    this.txtKoufuNo_Hyou.Focus();
                    if (this.customDataGridView1.Rows.Count != 0)
                    {
                        if (this.kensakuLogic.chkBarcodeHyou(txtKoufuNo_Hyou.Text, out catchErr))
                        {
                            this.barCode = null;
                        }
                        else
                        {
                            if (catchErr) { return; }
                            this.barCode = "";
                        }
                    }
                    else if (this.barCode != null)
                    {
                        switch (txtKoufuNo_Hyou.Text.Substring(7, 4))
                        {
                            case "0101":
                            //A票
                            case "0201":
                            //B1票
                            case "0202":
                            //B2票
                            case "0301":
                            //C1票
                            case "0302":
                            //C2票
                            case "0401":
                            //D票
                            case "0501":
                                //E票
                                this.barCode = txtKoufuNo_Hyou.Text;
                                break;

                            default:
                                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("C001");
                                this.barCode = "";
                                break;
                        }
                    }
                    break;

                case 2:
                    //「交付番号/票」にフォーカスをセットする。
                    this.txtKoufuNo_Hyou.Focus();

                    // 一度読み込んだ交付番号を再入力した場合、読み込み済みのRowを選択する
                    // 並び替え機能があるのでDataGridViewを利用すること。
                    foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                    {
                        if (row.Cells["hiddenMANIFEST_ID"].Value == null) continue;

                        if (this.txtKoufuNo_Hyou.Text.Equals(row.Cells["hiddenMANIFEST_ID"].Value.ToString()))
                        {
                            this.customDataGridView1.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                    break;
            }

            txtKoufuNo_Hyou.Text = string.Empty;
        }

        # endregion

        // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する start
        /// <summary>
        /// 交付区分Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_KofuKbn_Validated(object sender, EventArgs e)
        {
            CustomNumericTextBox2 text = sender as CustomNumericTextBox2;
            if (string.IsNullOrEmpty(text.Text))
            {
                //抽出対象区分が空の場合、メッセージ「抽出対象区分は必須項目です。入力してください。」を表示する
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E001", text.DisplayItemName);
                //フォーカスを抽出対象区分へ移動
                text.Select();
            }
        }

        /// <summary>
        /// 交付番号Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KOUFUBANNGO_Validating(object sender, CancelEventArgs e)
        {
            //CongBinh 20200330 #134989 S
            if ("1".Equals(this.txtNum_Renzoku.Text))
            {             
                this.logic.IsFocusDGV = true;

                // パターンが設定されていない場合はエラーとする。
                if (this.PatternNo == 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                //交付番号が空の場合は処理を抜ける
                if (string.IsNullOrEmpty(this.KOUFUBANNGO.Text))
                {
                    return;
                }
                int result = this.kensakuLogic.chkBarcode(this.KOUFUBANNGO.Text, true);
                switch (result)
                {
                    case 0:
                        this.kensakuLogic.barcodeFlg = true;
                        //交付番号で対象データを検索
                        if (this.kensakuLogic.Search() == -1)
                        {
                            return;
                        }
                        this.kensakuLogic.barcodeFlg = false;
                        //追加された行を選択行にする。
                        if (this.customDataGridView1.Rows.Count != 0)
                        {
                            // 入力された交付番号のRowを選択
                            bool isExistManiId = false;
                            foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                            {
                                if (row.Cells["hiddenMANIFEST_ID"].Value == null) continue;

                                if (this.KOUFUBANNGO.Text.Equals(row.Cells["hiddenMANIFEST_ID"].Value.ToString()))
                                {
                                    this.customDataGridView1.CurrentCell = row.Cells[0];
                                    isExistManiId = true;
                                    break;
                                }
                            }
                            if (!isExistManiId)
                            {
                                // 入力した交付番号が無ければ最後のRowを選択
                                this.customDataGridView1.CurrentCell = customDataGridView1.Rows[this.customDataGridView1.Rows.Count - 1].Cells[0];
                            }
                        }
                        this.logic.IsFocusDGV = false;
                        this.KOUFUBANNGO.Focus();
                        break; 
                    case 2:
                        this.KOUFUBANNGO.Focus();
                        // 一度読み込んだ交付番号を再入力した場合、読み込み済みのRowを選択する
                        // 並び替え機能があるのでDataGridViewを利用すること。
                        foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                        {
                            if (row.Cells["hiddenMANIFEST_ID"].Value == null) continue;

                            if (this.KOUFUBANNGO.Text.Equals(row.Cells["hiddenMANIFEST_ID"].Value.ToString()))
                            {
                                this.customDataGridView1.CurrentCell = row.Cells[0];
                                break;
                            }
                        }
                        break;
                }

                this.KOUFUBANNGO.Text = string.Empty;
            }
            //CongBinh 20200330 #134989 E
        }
        // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する end

        /// 20141022 Houkakou 「返却日入力」の日付チェックを追加する　start
        private void HIDSUKE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HIDSUKE_TO.Text))
            {
                this.HIDSUKE_TO.IsInputErrorOccured = false;
                this.HIDSUKE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void HIDSUKE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HIDSUKE_FROM.Text))
            {
                this.HIDSUKE_FROM.IsInputErrorOccured = false;
                this.HIDSUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void HAISHUTSUJIGYOUSYA_CD_Enter(object sender, EventArgs e)
        {
            tempHAISHUTSUJIGYOUSYA_CD = this.HAISHUTSUJIGYOUSYA_CD.Text;
        }

        private void HENSOU_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            tempHENSOU_GYOUSHA_CD = this.HENSOU_GYOUSHA_CD.Text;
        }
      
        /// 20141022 Houkakou 「返却日入力」の日付チェックを追加する　end
        #region CongBinh 20200330 #134989
        private void txtNum_Renzoku_TextChanged(object sender, EventArgs e)
        {
            if ("1".Equals(this.txtNum_Renzoku.Text))
            {
                this.kensakuLogic.parentForm.bt_func8.Enabled = false;
                this.kensakuLogic.SearchBarcodeResult = new DataTable();
                this.KOUFUBANNGO.Enabled = true; //CongBinh 20200513 #136891
            }
            else
            {
                this.KOUFUBANNGO.Enabled = false; //CongBinh 20200513 #136891
                this.kensakuLogic.parentForm.bt_func8.Enabled = true;                
            }
            if (this.customDataGridView1.DataSource != null)
            {
                DataTable temp = (DataTable)this.customDataGridView1.DataSource;
                temp.Rows.Clear();
                this.customDataGridView1.DataSource = temp;
            }
        }       
        #endregion

        #region MOD NHU 20211103 #157020
        /// <summary>
        /// 処分受託者検索ポップアップのPopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupBeforeExecuteMethod()
        {
            tempSBNGYOUSHA_CD = this.cantxt_SyobunJyutakuNameCd.Text;
        }

        /// <summary>
        /// 処分受託者検索ポップアップのPopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod()
        {
            SBNGYOUSHA_CD_PopupAfterExecuteMethod();
        }
        private void cantxt_SyobunJyutakuNameCd_Enter(object sender, EventArgs e)
        {
            tempSBNGYOUSHA_CD = this.cantxt_SyobunJyutakuNameCd.Text;
        }
        public void SBNGYOUSHA_CD_PopupAfterExecuteMethod()
        {
            if (tempSBNGYOUSHA_CD == this.cantxt_SyobunJyutakuNameCd.Text)
                return;

            // 処分事業場初期化
            this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
            this.ctxt_UnpanJyugyobaName.Text = string.Empty;

            //処分受託者チェック
            switch (this.kensakuLogic.ChkGyosya(cantxt_SyobunJyutakuNameCd, "SHOBUN_NIOROSHI_GYOUSHA_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //処分受託者削除
                    this.ctxt_SyobunJyutakuName.Text = string.Empty;

                    return;

                case 2://エラー
                    //処分受託者削除
                    this.ctxt_SyobunJyutakuName.Text = string.Empty;
                    return;
            }
            //処分受託者　設定
            this.kensakuLogic.SetAddressGyousha(cantxt_SyobunJyutakuNameCd, ctxt_SyobunJyutakuName);
            return;
        }
        /// <summary>
        /// 処分受託者(処分業者) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunJyutakuNameCd_Validated(object sender, EventArgs e)
        {

            switch (this.kensakuLogic.ChkGyosya(this.cantxt_SyobunJyutakuNameCd, "SHOBUN_NIOROSHI_GYOUSHA_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //処分受託者削除
                    this.ctxt_SyobunJyutakuName.Text = string.Empty;
                    // 処分事業場初期化
                    this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                    this.ctxt_UnpanJyugyobaName.Text = string.Empty;

                    return;

                case 2://エラー
                    //処分受託者削除
                    this.ctxt_SyobunJyutakuName.Text = string.Empty;
                    // 処分事業場初期化
                    this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                    this.ctxt_UnpanJyugyobaName.Text = string.Empty;
                    return;
            }
            //処分受託者　設定
            this.kensakuLogic.SetAddressGyousha(cantxt_SyobunJyutakuNameCd, ctxt_SyobunJyutakuName);

            if (tempSBNGYOUSHA_CD == this.cantxt_SyobunJyutakuNameCd.Text)
                return;

            // 処分事業場初期化
            this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
            this.ctxt_UnpanJyugyobaName.Text = string.Empty;
        }
        /// <summary>
        /// 処分事業場(処分事業場) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cantxt_SyobunJyutakuNameCd.Text) && !string.IsNullOrEmpty(this.cantxt_UnpanJyugyobaNameCd.Text))
            {
                //業者未入力の場合はメッセージを表示して処理を終了
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                msgLogic.MessageBoxShow("E051", "処分受託者");
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                this.ctxt_UnpanJyugyobaName.Text = string.Empty;
                this.cantxt_UnpanJyugyobaNameCd.Focus();
                return;
            }
            //入力チェック
            SBN_GENBA_CD_PopupAfterExecuteMethod();
        }
        /// <summary>
        /// 処分事業場CD PopupAfterExecuteMethod
        /// </summary>
        public void SBN_GENBA_CD_PopupAfterExecuteMethod()
        {
            //処分事業場チェック
            switch (this.kensakuLogic.ChkGenbaSbn(cantxt_SyobunJyutakuNameCd, cantxt_UnpanJyugyobaNameCd, this.ctxt_UnpanJyugyobaName))
            {
                case 0://正常
                    break;

                case 1://空
                    //処分事業場削除
                    this.ctxt_UnpanJyugyobaName.Text = string.Empty;

                    return;

                case 2://エラー
                    //処分事業場削除
                    this.ctxt_UnpanJyugyobaName.Text = string.Empty;
                    return;
            }

            return;
        }
        #endregion

       

        
    }
}