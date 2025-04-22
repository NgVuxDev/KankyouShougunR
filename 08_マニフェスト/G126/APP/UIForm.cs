using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Logic;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Logic;
using System.Data;

namespace Shougun.Core.PaperManifest.ManifestIchiran
{
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        //private r_framework.Logic.IBuisinessLogic logic;
        private ManifestIchiran.LogicClass MILogic = null;

        // 20140604 katen 不具合No.4131 start‏
        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }

        public int fromManiFlag = 1;
        // 20140604 katen 不具合No.4131 end‏

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        // 20140530 katen 不具合No.4129 start‏
        public string fromKbn = "";
        // 20140530 katen 不具合No.4129 end‏

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #region 出力パラメータ

        /// <summary>
        /// システムID
        /// </summary>
        public String ParamOut_SysID { get; set; }

        /// <summary>
        /// モード
        /// </summary>
        public Int32 ParamOut_WinType { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.MANIFEST_ICHIRAN, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MILogic = new LogicClass(this);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;

            // 20140604 katen 不具合No.4131 start‏
            //メッセージクラス
            this.messageShowLogic = new MessageBoxShowLogic();
            // 20140604 katen 不具合No.4131 end‏

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        public UIHeader HeaderForm { get; private set; }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.isLoaded)
            {
                // 初期化、初期表示
                if (!this.MILogic.WindowInit()) { return; }

                // キー入力設定
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;

                // ヘッダーフォームを取得
                this.HeaderForm = (UIHeader)this.ParentBaseForm.headerForm;

                // 一覧
                this.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(customDataGridView1_CellDoubleClick);

                // 非表示列登録
                this.SetHiddenColumns(this.MILogic.HIDDEN_SYSTEM_ID, this.MILogic.HIDDEN_SEQ, this.MILogic.HIDDEN_LATEST_SEQ,
                    this.MILogic.HIDDEN_KANRI_ID, this.MILogic.HIDDEN_HAIKI_KBN, this.MILogic.HIDDEN_DETAIL_SYSTEM_ID,
                    this.MILogic.HIDDEN_PRT_REC, this.MILogic.HIDDEN_KEIJYOU_REC, this.MILogic.HIDDEN_NISUGATA_REC,
                    this.MILogic.HIDDEN_SBN_REC1, this.MILogic.HIDDEN_SBN_REC2, this.MILogic.HIDDEN_HST_GYOUSHA_CD, this.MILogic.HIDDEN_TOC_STATUS_FLAG, this.MILogic.HIDDEN_QUE_STATUS_FLAG);

                //表示の初期化
                if (!this.MILogic.ClearScreen("Initial")) { return; }

                // フィルタ表示
                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(3, 240);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 25);

                this.customSortHeader1.Location = new System.Drawing.Point(3, 262);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 25);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 287);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 168);

                label17.BackColor = this.BackColor;

                // 汎用検索は一旦廃止
                this.searchString.Visible = false;

                // タイトルセット
                this.SetTitleString();
            }

            //2013-11-12 del ogawamut PT東北 No.1151 ⇒やっぱナシ。
            //this.MILogic.Search();

            this.isLoaded = true;

            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            //2013.12.15 naitou upd パターン更新 start
            //base.OnLoad時にthis.Tableに設定されたヘッダー情報をグリッドに表示する
            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                if (this.Table != null)
                {
                    this.logic.CreateDataGridView(this.Table);
                }
            }
            //2013.12.15 naitou upd パターン更新 end

            //thongh 2015/10/16 #13526 start
            //読込データ件数の設定
            if (this.customDataGridView1 != null)
            {
                this.HeaderForm.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.HeaderForm.ReadDataNumber.Text = "0";
            }
            //thongh 2015/10/16 #13526 end

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
                this.bt_ptn1.Top += 17;
                this.bt_ptn2.Top += 17;
                this.bt_ptn3.Top += 17;
                this.bt_ptn4.Top += 17;
                this.bt_ptn5.Top += 17;
            }

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

            base.OnShown(e);
        }

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        public virtual void ShowData()
        {
            if (this.Table != null)
            {
                int alertNum;
                int.TryParse(this.HeaderForm.AlertNumber.Text, System.Globalization.NumberStyles.AllowThousands, null, out alertNum);
                this.logic.AlertCount = alertNum;
                this.logic.CreateDataGridView(this.Table);
            }
        }

        /// <summary>
        /// ヘッダとタイトルラベルに現在の伝種のタイトル文字列を埋め込みます
        /// </summary>
        public void SetTitleString()
        {
            // 20140612 syunrei EV004715_タイトルラベルが入力画面と合わない start

            //// 20140604 katen 不具合No.4131 start‏
            //string strTitelName = this.DenshuKbn.ToTitleString();
            //strTitelName = (this.MILogic.maniFlag == 1 ? "一次" : "二次") + strTitelName;
            //this.HeaderForm.lb_title.Text = strTitelName;
            //this.Text = strTitelName;
            ////this.HeaderForm.lb_title.Text = this.DenshuKbn.ToTitleString();
            ////this.Text = this.DenshuKbn.ToTitleString();
            //// 20140604 katen 不具合No.4131 start‏

            
            string strTitelName = this.MILogic.maniFlag == 1 ? "一次" : "二次";
            switch (this.HAIKI_KBN_CD.Text)
            {
                case "1":
                    this.HeaderForm.lb_title.Text = "産廃マニフェスト(直行)一覧" + strTitelName;
                    break;
                case "2":
                    this.HeaderForm.lb_title.Text = "産廃マニフェスト(積替)一覧" + strTitelName;
                    break;
                case "3":
                    this.HeaderForm.lb_title.Text = "建廃マニフェスト一覧" + strTitelName;
                    break;
                case "4":
                    this.HeaderForm.lb_title.Text = "電子マニフェスト一覧" + strTitelName;
                    break;
                case "5":
                    this.HeaderForm.lb_title.Text = "マニフェスト一覧" + strTitelName;
                    break;
                default:
                    break;
            }
            // 20140612 syunrei EV004715_タイトルラベルが入力画面と合わない end


        }

        #region 画面コントロールイベント

        /// <summary>
        /// 交付年月日（開始）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KOUFU_DATE_FROM_MouseDown(object sender, MouseEventArgs e)
        {
            if (KOUFU_DATE_FROM.Value == null)
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.KOUFU_DATE_FROM.Value = DateTime.Now.Date;
                this.KOUFU_DATE_FROM.Value = this.MILogic.footer.sysDate.Date;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            }
        }

        private void KOUFU_DATE_FROM_ValueChanged(object sender, EventArgs e)
        {
            if (KOUFU_DATE_FROM.Value != null)
            {
                this.MILogic.KoufuDateFrom = KOUFU_DATE_FROM.Value.ToString();
            }
            else
            {
                this.MILogic.KoufuDateFrom = "";
            }

        }

        /// <summary>
        /// 交付年月日（終了）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KOUFU_DATE_TO_MouseDown(object sender, MouseEventArgs e)
        {
            if (KOUFU_DATE_TO.Value == null)
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.KOUFU_DATE_TO.Value = DateTime.Now.Date;
                this.KOUFU_DATE_TO.Value = this.MILogic.footer.sysDate.Date;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            }
        }

        private void KOUFU_DATE_TO_ValueChanged(object sender, EventArgs e)
        {
            if (KOUFU_DATE_TO.Value != null)
            {
                this.MILogic.KoufuDateTo = KOUFU_DATE_TO.Value.ToString();
            }
            else
            {
                this.MILogic.KoufuDateTo = "";
            }
        }

        /// <summary>
        /// 廃棄物区分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            switch (this.HAIKI_KBN_CD.Text)
            {
                case "1":
                    this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_CHOKKOU;
                    this.customRadioButton1.Checked = true;
                    this.customRadioButton2.Checked = false;
                    this.customRadioButton3.Checked = false;
                    this.customRadioButton4.Checked = false;
                    this.customRadioButton5.Checked = false;
                    this.ParentBaseForm.bt_func1.Enabled = false; // 受渡確認票
                    break;
                case "2":
                    this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_TSUMIKAE;
                    this.customRadioButton1.Checked = false;
                    this.customRadioButton2.Checked = true;
                    this.customRadioButton3.Checked = false;
                    this.customRadioButton4.Checked = false;
                    this.customRadioButton5.Checked = false;
                    this.ParentBaseForm.bt_func1.Enabled = false; // 受渡確認票
                    break;
                case "3":
                    this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_KENPAI;
                    this.customRadioButton1.Checked = false;
                    this.customRadioButton2.Checked = false;
                    this.customRadioButton3.Checked = true;
                    this.customRadioButton4.Checked = false;
                    this.customRadioButton5.Checked = false;
                    this.ParentBaseForm.bt_func1.Enabled = false; // 受渡確認票
                    break;
                case "4":
                    this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_DENSHI;
                    this.customRadioButton1.Checked = false;
                    this.customRadioButton2.Checked = false;
                    this.customRadioButton3.Checked = false;
                    this.customRadioButton4.Checked = true;
                    this.customRadioButton5.Checked = false;
                    this.ParentBaseForm.bt_func1.Enabled = true; // 受渡確認票
                    break;
                case "5":
                    this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_ALL;
                    this.customRadioButton1.Checked = false;
                    this.customRadioButton2.Checked = false;
                    this.customRadioButton3.Checked = false;
                    this.customRadioButton4.Checked = false;
                    this.customRadioButton5.Checked = true;
                    this.ParentBaseForm.bt_func1.Enabled = false; // 受渡確認票
                    break;
                default:
                    this.customRadioButton1.Checked = false;
                    this.customRadioButton2.Checked = false;
                    this.customRadioButton3.Checked = false;
                    this.customRadioButton4.Checked = false;
                    this.customRadioButton5.Checked = false;
                    this.ParentBaseForm.bt_func1.Enabled = false; // 受渡確認票
                    return;
            }
            // 20140603 katen 不具合No.4131 start‏
            //廃棄区分が電子の場合は、取引先をグレーアウトにします。
            this.cantxt_TorihikiCd.Enabled = this.HAIKI_KBN_CD.Text != "4";
            //廃棄区分が変わった場合は、廃棄物種類をクリアします。
            this.cantxt_HaikibutuShurui.Text = string.Empty;
            this.cantxt_ElecHaikiShurui.Text = string.Empty;
            this.ctxt_HaikibutuShurui.Text = string.Empty;

            if (this.HAIKI_KBN_CD.Text == "4")
            {
                //廃棄区分が電子の場合は、取引先をクリアします。
                this.cantxt_TorihikiCd.Text = string.Empty;
                this.ctxt_TorihikiName.Text = string.Empty;
                //廃棄物種類のコントロールが電子用のを替える
                this.cantxt_HaikibutuShurui.Visible = false;
                this.cantxt_HaikibutuShurui.Enabled = false;
                this.cbtn_HaikibutuShuruiSan.Visible = false;
                this.cbtn_HaikibutuShuruiSan.Enabled = false;
                this.cantxt_ElecHaikiShurui.Visible = true;
                this.cantxt_ElecHaikiShurui.Enabled = true;
                this.cbtn_ElecHaikibutuShuruiSan.Visible = true;
                this.cbtn_ElecHaikibutuShuruiSan.Enabled = true;

                //廃棄区分が変わった場合は、廃棄物名称をクリアします。
                this.cantxt_HaikibutuName.Text = string.Empty;
                this.cantxt_ElecHaikiName.Text = string.Empty;
                this.ctxt_HaikibutuName.Text = string.Empty;
                //廃棄物名称のコントロールが電子用のを替える
                this.cantxt_HaikibutuName.Visible = false;
                this.cantxt_HaikibutuName.Enabled = false;
                this.cbtn_HaikibutuNameSan.Visible = false;
                this.cbtn_HaikibutuNameSan.Enabled = false;
                this.cantxt_ElecHaikiName.Visible = true;
                this.cantxt_ElecHaikiName.Enabled = true;
                this.cbtn_ElecHaikibutuNameSan.Visible = true;
                this.cbtn_ElecHaikibutuNameSan.Enabled = true;
                if (this.cantxt_HaisyutuGyousyaCd == null || this.cantxt_HaisyutuGyousyaCd.Text == "")
                {
                    this.cantxt_ElecHaikiName.Enabled = false;
                    this.cbtn_ElecHaikibutuNameSan.Enabled = false;
                }
                else
                {
                    this.MILogic.GetPopUpDenshiHaikiNameData(this.cantxt_HaisyutuGyousyaCd.Text);
                }
            }
            else
            {
                this.ctxt_HaikibutuName.Text = string.Empty;
                //廃棄物種類のコントロールが紙用のを替える
                this.cantxt_HaikibutuShurui.Visible = true;
                //廃棄区分が全ての場合は、廃棄物種類をグレーアウトにします。
                this.cantxt_HaikibutuShurui.Enabled = this.HAIKI_KBN_CD.Text != "5";
                this.cbtn_HaikibutuShuruiSan.Visible = true;
                this.cbtn_HaikibutuShuruiSan.Enabled = this.HAIKI_KBN_CD.Text != "5";
                this.cantxt_ElecHaikiShurui.Visible = false;
                this.cantxt_ElecHaikiShurui.Enabled = false;
                this.cbtn_ElecHaikibutuShuruiSan.Visible = false;
                this.cbtn_ElecHaikibutuShuruiSan.Enabled = false;

                if (!this.cantxt_HaikibutuName.Enabled)
                {
                    //区分が電子の状態から変わったかもしらない場合
                    //廃棄物名称のコントロールが紙用のを替える
                    this.cantxt_HaikibutuName.Visible = true;
                    this.cantxt_HaikibutuName.Enabled = this.HAIKI_KBN_CD.Text != "5";
                    this.cbtn_HaikibutuNameSan.Visible = true;
                    this.cbtn_HaikibutuNameSan.Enabled = this.HAIKI_KBN_CD.Text != "5";
                    this.cantxt_ElecHaikiName.Visible = false;
                    this.cantxt_ElecHaikiName.Enabled = false;
                    this.cbtn_ElecHaikibutuNameSan.Visible = false;
                    this.cbtn_ElecHaikibutuNameSan.Enabled = false;
                }
                else
                {
                    //廃棄物名称が全ての場合は、廃棄物種類をグレーアウトにします。
                    this.cantxt_HaikibutuName.Enabled = this.HAIKI_KBN_CD.Text != "5";
                    this.cbtn_HaikibutuNameSan.Enabled = this.HAIKI_KBN_CD.Text != "5";
                }
                if (!this.cantxt_HaikibutuName.Enabled)
                {
                    //廃棄物名称を禁止した場合、内容をクリアする
                    //廃棄物名称をクリアします。
                    this.cantxt_HaikibutuName.Text = string.Empty;
                    this.cantxt_ElecHaikiName.Text = string.Empty;
                    this.ctxt_HaikibutuName.Text = string.Empty;
                }

                this.cantxt_HaikibutuShurui.popupWindowSetting.Clear();
                this.cbtn_HaikibutuShuruiSan.popupWindowSetting.Clear();
                JoinMethodDto dtowhere = new JoinMethodDto();
                dtowhere.IsCheckLeftTable = false;
                dtowhere.IsCheckRightTable = false;
                dtowhere.Join = JOIN_METHOD.WHERE;
                dtowhere.LeftTable = "M_HAIKI_SHURUI";

                SearchConditionsDto serdto = new SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.AND;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "HAIKI_KBN_CD";
                serdto.ValueColumnType = DB_TYPE.SMALLINT;

                switch (this.HAIKI_KBN_CD.Text)
                {
                    case "1":
                        serdto.Value = "1";
                        break;
                    case "2":
                        serdto.Value = "3";
                        break;
                    case "3":
                        serdto.Value = "2";
                        break;
                    case "5":
                        serdto.Value = "5";
                        break;
                }
                dtowhere.SearchCondition.Add(serdto);
                this.cantxt_HaikibutuShurui.popupWindowSetting.Add(dtowhere);
                this.cbtn_HaikibutuShuruiSan.popupWindowSetting.Add(dtowhere);
            }
            // 20140603 katen 不具合No.4131 end‏
            this.SetTitleString();
            this.MILogic.HaikiKbnCD = this.HAIKI_KBN_CD.Text;
            this.HAIKI_KBN_CD.SelectAll();
            this.PatternReload(true);

            // 20140624 syunrei EV004430_受付一覧で検索後、伝票種類を変更してからサブファンクションで伝票紐付をするとシステムエラー　start
            if (this.customDataGridView1 != null && this.customDataGridView1.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)this.customDataGridView1.DataSource;
                dt.Clear();
                this.customDataGridView1.DataSource = dt;

            }
            // 20140624 syunrei EV004430_受付一覧で検索後、伝票種類を変更してからサブファンクションで伝票紐付をするとシステムエラー　end
        }

        /// <summary>
        /// 廃棄物区分 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKI_KBN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.MILogic.Haiki_Kbn_CD_Check();
        }

        /// <summary>
        /// 一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MILogic.FormChanges(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
        }

        /// <summary>
        /// パターン1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn5_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 受渡確認(F1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func1_Click(object sender, EventArgs e)
        {
            if (this.HAIKI_KBN_CD.Text != "4")
            {
                return;
            }

            var current = this.customDataGridView1.CurrentRow;

            if (current == null)
            {
                MessageBoxUtility.MessageBoxShow("E029", "出力するマニフェスト", "マニフェスト一覧");
                return;
            }

            // カーソルのある行が[電子マニフェスト]でない場合
            if ("4".Equals(current.Cells[this.MILogic.HIDDEN_HAIKI_KBN].Value.ToString()))
            {
                UkewatashiKakuninHyouLogic logic = new UkewatashiKakuninHyouLogic();
                Cursor.Current = Cursors.WaitCursor;
                logic.UkewatashiKakuninHyouPrint(current.Cells[this.MILogic.HIDDEN_KANRI_ID].Value.ToString());
                Cursor.Current = Cursors.Default;
            }
            else
            {
                //エラーメッセージ表示
                MessageBoxUtility.MessageBoxShow("E051", "電子マニフェストの行");
            }
        }

        /// <summary>
        /// 新規(F2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func2_Click(object sender, EventArgs e)
        {
            MILogic.FormChanges(WINDOW_TYPE.NEW_WINDOW_FLAG);
        }

        /// <summary>
        /// 修正(F3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func3_Click(object sender, EventArgs e)
        {
            MILogic.FormChanges(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
        }

        /// <summary>
        /// 削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func4_Click(object sender, EventArgs e)
        {
            MILogic.FormChanges(WINDOW_TYPE.DELETE_WINDOW_FLAG);
        }

        /// <summary>
        /// CSV出力(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            CSVExport CSVExp = new CSVExport();
            CSVExp.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_MANIFEST_ICHIRAN), this);
        }

        /// <summary>
        /// 条件ｸﾘｱ(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {
            this.MILogic.ClearScreen("ClsSearchCondition");
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            // パターンチェック
            if (this.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            //必須チェック
            if (MILogic.SearchCheck())
            {
                return;
            }

            switch (this.MILogic.Search())
            {
                case -1:
                    return;
                case 0:
                    MessageBoxUtility.MessageBoxShow("C001");
                    break;
            }
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ShowCustomSortSettingDialog();
            // 20140602 kayo 不具合#4467 システムエラー修正 start
            // TODO　該当処理でエラーになっています。原因：廃棄物区分は５の場合、
            // TODO”電子CSV”をグレードビューに存在しないので、該当処理はもう要らないと思います
            //this.MILogic.ChackDenshiCSVColumn();
            // 20140602 kayo 不具合#4467 システムエラー修正 end
        }

        /// <summary>
        /// フィルタ(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func11_Click(object sender, EventArgs e)
        {
            this.customSearchHeader1.ShowCustomSearchSettingDialog();
            //読込データ件数           #13032
            if (this.customDataGridView1 != null)
            {
                this.HeaderForm.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.HeaderForm.ReadDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.customDataGridView1.DataSource = "";

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// パターン一覧(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            var sysID = this.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.ShowData();
            }
        }

        /// <summary>
        /// 1次マニと2次マニを切り替えます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, EventArgs e)
        {
            this.MILogic.SetManifestFrom("btn_process2");
        }

        // 20140604 katen 不具合No.4131 start‏
        ///// <summary>
        ///// 検索条件設定(2)
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void bt_process2_Click(object sender, EventArgs e)
        //{
        //    //仕様不明なため、未実装。確認用
        //    //MessageBox.Show("検索条件設定画面", "画面遷移");
        //}
        // 20140604 katen 不具合No.4131 end‏

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

        // 20140603 katen 不具合No.4131 start‏
        /// <summary>
        /// 排出事業者CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuGyousyaCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            if (this.HAIKI_KBN_CD.Text == "4")
            {
                this.cantxt_ElecHaikiName.Text = string.Empty;
                this.ctxt_HaikibutuName.Text = string.Empty;
                if (this.cantxt_HaisyutuGyousyaCd == null || this.cantxt_HaisyutuGyousyaCd.Text == "")
                {
                    this.cantxt_ElecHaikiName.Enabled = false;
                    this.cbtn_ElecHaikibutuNameSan.Enabled = false;
                }
                else
                {
                    this.cantxt_ElecHaikiName.Enabled = true;
                    this.cbtn_ElecHaikibutuNameSan.Enabled = true;
                    this.MILogic.GetPopUpDenshiHaikiNameData(this.cantxt_HaisyutuGyousyaCd.Text);
                }
            }

            //排出事業者チェック
            switch (this.MILogic.ChkGyosya(this.cantxt_HaisyutuGyousyaCd, "HAISHUTSU_NIZUMI_GYOUSHA_KBN"))
            {
                case 0://正常
                    // 排出業者CDが変更されているはずなので、関連する排出事業場をクリア
                    this.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
                    this.ctxt_HaisyutuJigyoubaName.Text = string.Empty;
                    break;

                case 1://空
                    //排出業者削除
                    this.ctxt_HaisyutuGyousyaName.Text = string.Empty;
                    //排出業場削除
                    this.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
                    this.ctxt_HaisyutuJigyoubaName.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_HaisyutuGyousyaCd, ctxt_HaisyutuGyousyaName,
                null, null, null,
                null, null, null,
                true, false, false, false, true);
        }

        /// <summary>
        /// 排出事業場CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuJigyoubaName_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            switch (this.MILogic.ChkJigyouba(this.cantxt_HaisyutuJigyoubaName, this.cantxt_HaisyutuGyousyaCd, "HAISHUTSU_NIZUMI_GENBA_KBN"))
            {
                case 0://正常

                    break;

                case 1://空
                    //排出業場削除
                    this.ctxt_HaisyutuJigyoubaName.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }

            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_HaisyutuGyousyaCd, ctxt_HaisyutuGyousyaName,
                null, null, null,
                null, null, null,
                true, false, false, false, true);

            //事業場　設定
            this.MILogic.SetAddressJigyouba("Ryakushou_Name", cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, ctxt_HaisyutuJigyoubaName, true, false, false, false, true);
        }

        /// <summary>
        /// 運搬受託者 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyutakuNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkGyosya(this.cantxt_UnpanJyutakuNameCd, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //運搬受託者削除
                    this.ctxt_UnpanJyutakuName.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }

            //排出業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_UnpanJyutakuNameCd, ctxt_UnpanJyutakuName,
                null, null, null,
                null, null, null,
                false, false, true, false, true);
        }

        /// <summary>
        /// 処分受託者(処分業者) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunJyutakuNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkGyosya(this.cantxt_SyobunJyutakuNameCd, "SHOBUN_NIOROSHI_GYOUSHA_KBN"))
            {
                case 0://正常
                    // 処分業者CDが変更されているはずなので、関連する処分先の事業場をクリア
                    this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                    this.ctxt_UnpanJyugyobaName.Text = string.Empty;
                    break;

                case 1://空
                    //処分受託者削除
                    this.ctxt_SyobunJyutakuName.Text = string.Empty;
                    //運搬先の事業場削除
                    this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                    this.ctxt_UnpanJyugyobaName.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }
            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_SyobunJyutakuNameCd, ctxt_SyobunJyutakuName,
                null, null, null,
                null, null, null,
                false, true, false, false, true);
        }

        /// <summary>
        /// 運搬先の事業場(処分業者の処理施設) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkJigyouba(this.cantxt_UnpanJyugyobaNameCd, this.cantxt_SyobunJyutakuNameCd, "SHOBUN_NIOROSHI_GENBA_KBN", "SAISHUU_SHOBUNJOU_KBN", this.ctxt_UnpanJyugyobaName))
            {
                case 0://正常
                    break;

                case 1://空
                    //運搬先の事業場削除
                    this.ctxt_UnpanJyugyobaName.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_SyobunJyutakuNameCd, ctxt_SyobunJyutakuName,
                null, null, null,
                null, null, null,
                false, true, false, false, true);

            /* 処分事業場は現場の「処分事業場/荷降現場区分」または「最終処分場」がTrueの現場データを設定可としています。　　　　　　　　　　　　 */
            /* 以下のメソッドでは2つのフラグを見れないかつ、前述のChkJigyoubaで存在チェックが出来ているのでChkJigyoubaで名称設定を行っています。 */
            //現場　設定
            //this.MILogic.SetAddressJigyouba("Ryakushou_Name", cantxt_SyobunJyutakuNameCd, cantxt_UnpanJyugyobaNameCd, ctxt_UnpanJyugyobaName,
            //    false, false, true, false, true);
        }

        /// <summary>
        /// 報告書分類 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HokokushoBunrui_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkHoukokushoBunrui(this.cantxt_HokokushoBunrui))
            {
                case 0://正常
                    break;

                case 1://空
                    //報告書分類削除
                    this.ctxt_HokokushoBunrui.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
        }

        /// <summary>
        /// 廃棄物種類 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaikibutuShurui_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkHaikibutuShurui(this.HAIKI_KBN_CD, this.cantxt_HaikibutuShurui))
            {
                case 0://正常
                    break;

                case 1://空
                    //報告書分類削除
                    this.ctxt_HaikibutuShurui.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
        }

        /// <summary>
        /// 廃棄物名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaikibutuName_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkHaikibutuName(this.cantxt_HaikibutuName))
            {
                case 0://正常
                    break;

                case 1://空
                    //報告書分類削除
                    this.ctxt_HaikibutuName.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
        }

        /// <summary>
        /// 電子廃棄物種類 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ElecHaikiShurui_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkDenshiHaikibutuShurui(this.cantxt_ElecHaikiShurui))
            {
                case 0://正常
                    break;

                case 1://空
                    //報告書分類削除
                    this.ctxt_HaikibutuShurui.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }
        }

        /// <summary>
        /// 電子廃棄物名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ElecHaikiName_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkDenshiHaikibutuName(this.cantxt_ElecHaikiName))
            {
                case 0://正常
                    break;

                case 1://空
                    //報告書分類削除
                    this.ctxt_HaikibutuName.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }
        }
        // 20140603 katen 不具合No.4131 end‏
        #endregion

        // 20140603 katen 不具合No.4131 start‏
        #region 値保持

        /// <summary>
        /// Enter時の値保持
        /// </summary>
        private Dictionary<Control, string> _EnterValue = new Dictionary<Control, string>();

        private object lastObject = null;

        internal void EnterEventInit()
        {
            foreach (var c in controlUtil.GetAllControls(this.Parent))
            {
                c.Enter += new EventHandler(this.SaveTextOnEnter);
            }
        }

        /// <summary>
        /// Enter時　入力値保存
        /// </summary>
        /// <param name="value"></param>
        private void SaveTextOnEnter(object sender, EventArgs e)
        {
            var value = sender as Control;

            if (value == null)
            {
                return;
            }

            //エラー等でフォーカス移動しなかった場合は、値クリアして強制チェックするようにする。 
            // ※1（正常）→0（エラー）→1と入れた場合 チェックする。
            // ※※この処理がない場合、0（エラー）→0（ノーチェック）となってしまう。
            if (lastObject == sender)
            {
                if (_EnterValue.ContainsKey(value))
                {
                    _EnterValue[value] = null;
                }
                else
                {
                    _EnterValue.Add(value, null);
                }

                return;

            }

            this.lastObject = sender;

            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = value.Text;
            }
            else
            {
                _EnterValue.Add(value, value.Text);
            }
        }
        /// <summary>
        /// 値比較時
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal string get_EnterValue(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return null;
            }
            return _EnterValue[value];
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, value.Text); //一致する場合変更なし
        }
        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender, string newText)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, newText); //一致する場合変更なし
        }

        #endregion
        // 20140603 katen 不具合No.4131 end‏

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
        private void KOUFU_DATE_FROM_Leave(object sender, EventArgs e)
        {
            this.KOUFU_DATE_TO.IsInputErrorOccured = false;
            this.KOUFU_DATE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void KOUFU_DATE_TO_Leave(object sender, EventArgs e)
        {
            this.KOUFU_DATE_FROM.IsInputErrorOccured = false;
            this.KOUFU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

        #region 検索ポップアップイベント
        private string popupBeforeHaisyutuGyoushaCd = string.Empty;
        private string popupBeforeSyobunJyutakuNameCd = string.Empty;
        private string popupBeforeTsumikaehokanGyoushaCd = string.Empty;

        /// <summary>
        /// 排出事業者検索ポップアップのPopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuGyousyaCd_PopupBeforeExecuteMethod()
        {
            popupBeforeHaisyutuGyoushaCd = this.cantxt_HaisyutuGyousyaCd.Text;
        }

        /// <summary>
        /// 排出事業者検索ポップアップのPopupAfterExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod()
        {
            if (!popupBeforeHaisyutuGyoushaCd.Equals(this.cantxt_HaisyutuGyousyaCd.Text))
            {
                this.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
                this.ctxt_HaisyutuJigyoubaName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分受託者検索ポップアップのPopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupBeforeExecuteMethod()
        {
            popupBeforeSyobunJyutakuNameCd = this.cantxt_SyobunJyutakuNameCd.Text;
        }

        /// <summary>
        /// 処分受託者検索ポップアップのPopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod()
        {
            if (!popupBeforeSyobunJyutakuNameCd.Equals(this.cantxt_SyobunJyutakuNameCd.Text))
            {
                this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                this.ctxt_UnpanJyugyobaName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 積替え保管業者検索ポップアップのPopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_TsumikaehokanGyoushaCd_PopupBeforeExecuteMethod()
        {
            popupBeforeTsumikaehokanGyoushaCd = this.cantxt_TsumikaehokanGyoushaCd.Text;
        }

        /// <summary>
        /// 積替え保管業者ポップアップのPopupAfterExecuteMethod
        /// </summary>
        public void cantxt_TsumikaehokanGyoushaCd_PopupAfterExecuteMethod()
        {
            if (!popupBeforeTsumikaehokanGyoushaCd.Equals(this.cantxt_TsumikaehokanGyoushaCd.Text))
            {
                this.cantxt_TsumikaehokanGyoubaCd.Text = string.Empty;
                this.cantxt_TsumikaehokanGyoubaName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 積替え保管業者CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TsumikaehokanGyoushaCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                if (string.IsNullOrEmpty(this.cantxt_TsumikaehokanGyoushaCd.Text))
                {
                    this.cantxt_TsumikaehokanGyoushaName.Text = string.Empty;
                }
                return;
            }

            switch (this.MILogic.ChkGyosya(this.cantxt_TsumikaehokanGyoushaCd, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
            {
                case 0://正常
                    //積替え保管場削除
                    this.cantxt_TsumikaehokanGyoubaCd.Text = string.Empty;
                    this.cantxt_TsumikaehokanGyoubaName.Text = string.Empty;
                    break;

                case 1://空
                    //積替え保管業者削除
                    this.cantxt_TsumikaehokanGyoushaName.Text = string.Empty;
                    //積替え保管場削除
                    this.cantxt_TsumikaehokanGyoubaCd.Text = string.Empty;
                    this.cantxt_TsumikaehokanGyoubaName.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }

            //積替え保管業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_TsumikaehokanGyoushaCd, cantxt_TsumikaehokanGyoushaName,
                null, null, null,
                null, null, null,
                false, false, true, false, true);
        }

        /// <summary>
        /// 積替え保管場CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_TsumikaehokanGyoubaCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                if (string.IsNullOrEmpty(this.cantxt_TsumikaehokanGyoubaCd.Text))
                {
                    this.cantxt_TsumikaehokanGyoubaName.Text = string.Empty;
                }
                return;
            }
            switch (this.MILogic.ChkJigyouba(this.cantxt_TsumikaehokanGyoubaCd, this.cantxt_TsumikaehokanGyoushaCd, "TSUMIKAEHOKAN_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //排出業場削除
                    this.cantxt_TsumikaehokanGyoubaName.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }

            //積替え保管業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_TsumikaehokanGyoushaCd, cantxt_TsumikaehokanGyoushaName,
                null, null, null,
                null, null, null,
                false, false, true, false, true);

            //積替え保管場　設定
            this.MILogic.SetAddressJigyouba("Ryakushou_Name", cantxt_TsumikaehokanGyoushaCd, cantxt_TsumikaehokanGyoubaCd, cantxt_TsumikaehokanGyoubaName, false, false, false, true, true);
        }
        #endregion
    }
}
