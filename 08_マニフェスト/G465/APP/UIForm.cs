using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using System.Linq;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Logic;
using System.Data;
using r_framework.CustomControl.DataGridCustomControl;
using System.ComponentModel;
using r_framework.Entity;
using System.Drawing;

namespace Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>

        private ManifestKansanSaikeisanIchiran.LogicClass MILogic = null;
        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        /// <summary>
        /// アラートフラグ
        /// </summary>
        internal Boolean isErr = false;

        // 前回値保存
        internal string preSbnHouhouCd = string.Empty;
        internal string preSbnHouhouName = string.Empty;
        internal string preHstGyoushaCd = string.Empty;
        internal string preSbnGyoushaCd = string.Empty;//157951

        private string popBeforeCd = string.Empty;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #region 出力パラメータ

        /// <summary>
        /// システムID
        /// </summary>
        //public String ParamOut_SysID { get; set; }

        /// <summary>
        /// モード
        /// </summary>
        //public Int32 ParamOut_WinType { get; set; }

        //初期時一覧の明細列
        private string[] strColumns = { 
                                         "対象"	
	                                    ,"廃棄物区分"
	                                    ,"交付年月日"
	                                    ,"マニフェスト番号"
	                                    ,"廃棄物種類"
	                                    ,"報告書分類"
	                                    ,"廃棄物数量"
	                                    ,"廃棄物数量単位"
	                                    ,"換算後数量(変更前)"
	                                    ,"減容後数量(変更前)"
                                        ,"換算後数量"
                                        ,"減容後数量"
	                                    ,"排出事業者"
	                                    ,"排出事業場"
	                                    ,"(区間1)運搬業者"
	                                    ,"処分事業者"
	                                    ,"廃棄物名称"
	                                    ,"荷姿"
	                                    ,"処分方法"
                                     };

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_MANIFEST_KANSANTI_SAIKEISAN)

        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MILogic = new LogicClass(this);

            //社員コードを取得すること
            //this.ShainCd = SystemProperty.Shain.CD;

            //メッセージクラス
            this.messageShowLogic = new MessageBoxShowLogic();

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
            try
            {
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

            if (!this.isLoaded)
            {
                // 初期化、初期表示
                if (!this.MILogic.WindowInit()) { return; }

                // キー入力設定
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;

                // ヘッダーフォームを取得
                this.HeaderForm = (UIHeader)this.ParentBaseForm.headerForm;

                // フィルタ表示
                //this.customSearchHeader1.Visible = false;
                //this.customSearchHeader1.Location = new System.Drawing.Point(x, y);
                //this.customSearchHeader1.Size = new System.Drawing.Size(xx, yy);

                //this.customSortHeader1.Location = new System.Drawing.Point(x, y + 23);
                //this.customSortHeader1.Size = new System.Drawing.Size(xx, yy);

                //this.customDataGridView1.Location = new System.Drawing.Point(3, 189);
                //this.customDataGridView1.Size = new System.Drawing.Size(997, 286);

                // 汎用検索は一旦廃止
                //this.searchString.Visible = false;
            }

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }

        #region 各項目のイベント

        /// <summary>
        /// 廃棄物区分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            //switch (this.cntxt_HaikiKbnCD.Text)
            //{
            //    case "1":
            //        this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_CHOKKOU;
            //        this.crbtnHaikiKbnCD_1.Checked = true;
            //        this.crbtnHaikiKbnCD_2.Checked = false;
            //        this.crbtnHaikiKbnCD_3.Checked = false;
            //        this.crbtnHaikiKbnCD_4.Checked = false;
            //        this.crbtnHaikiKbnCD_5.Checked = false;
            //        this.crbtnHaikiKbnCD_6.Checked = false;
            //        break;
            //    case "2":
            //        this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_TSUMIKAE;
            //        this.crbtnHaikiKbnCD_1.Checked = false;
            //        this.crbtnHaikiKbnCD_2.Checked = true;
            //        this.crbtnHaikiKbnCD_3.Checked = false;
            //        this.crbtnHaikiKbnCD_4.Checked = false;
            //        this.crbtnHaikiKbnCD_5.Checked = false;
            //        this.crbtnHaikiKbnCD_6.Checked = false;
            //        break;
            //    case "3":
            //        this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_KENPAI;
            //        this.crbtnHaikiKbnCD_1.Checked = false;
            //        this.crbtnHaikiKbnCD_2.Checked = false;
            //        this.crbtnHaikiKbnCD_3.Checked = true;
            //        this.crbtnHaikiKbnCD_4.Checked = false;
            //        this.crbtnHaikiKbnCD_5.Checked = false;
            //        this.crbtnHaikiKbnCD_6.Checked = false;
            //        break;
            //    case "4":
            //        this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_DENSHI;
            //        this.crbtnHaikiKbnCD_1.Checked = false;
            //        this.crbtnHaikiKbnCD_2.Checked = false;
            //        this.crbtnHaikiKbnCD_3.Checked = false;
            //        this.crbtnHaikiKbnCD_4.Checked = true;
            //        this.crbtnHaikiKbnCD_5.Checked = false;
            //        this.crbtnHaikiKbnCD_6.Checked = false;
            //        break;
            //    case "5":
            //        this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_DENSHI;
            //        this.crbtnHaikiKbnCD_1.Checked = false;
            //        this.crbtnHaikiKbnCD_2.Checked = false;
            //        this.crbtnHaikiKbnCD_3.Checked = false;
            //        this.crbtnHaikiKbnCD_4.Checked = false;
            //        this.crbtnHaikiKbnCD_5.Checked = true;
            //        this.crbtnHaikiKbnCD_6.Checked = false;
            //        break;
            //    case "6":
            //        this.DenshuKbn = DENSHU_KBN.MANI_ICHIRAN_ALL;
            //        this.crbtnHaikiKbnCD_1.Checked = false;
            //        this.crbtnHaikiKbnCD_2.Checked = false;
            //        this.crbtnHaikiKbnCD_3.Checked = false;
            //        this.crbtnHaikiKbnCD_4.Checked = false;
            //        this.crbtnHaikiKbnCD_5.Checked = false;
            //        this.crbtnHaikiKbnCD_6.Checked = true;
            //        break;
            //    default:
            //        this.crbtnHaikiKbnCD_1.Checked = false;
            //        this.crbtnHaikiKbnCD_2.Checked = false;
            //        this.crbtnHaikiKbnCD_3.Checked = false;
            //        this.crbtnHaikiKbnCD_4.Checked = false;
            //        this.crbtnHaikiKbnCD_5.Checked = false;
            //        this.crbtnHaikiKbnCD_6.Checked = false;
            //        return;
            //}

            //廃棄区分が変わった場合は、廃棄物種類をクリアします。
            this.cantxt_HaikibutuShuruiCd.Text = string.Empty;
            this.ctxt_HaikibutuShuruiName.Text = string.Empty;

            if (this.cntxt_HaikiKbnCD.Text == "4")
            {
                //廃棄物種類のコントロールが電子用のを替える
                this.cantxt_HaikibutuShuruiCd.Visible = false;
                this.cantxt_HaikibutuShuruiCd.Enabled = false;
                this.cbtn_HaikibutuShuruiSan.Visible = false;
                this.cbtn_HaikibutuShuruiSan.Enabled = false;
                this.cbtn_ElecHaikibutuShuruiSan.Visible = true;
                this.cbtn_ElecHaikibutuShuruiSan.Enabled = true;

                //廃棄区分が変わった場合は、廃棄物名称をクリアします。
                this.cantxt_HaikibutuNameCd.Text = string.Empty;
                this.ctxt_HaikibutuName.Text = string.Empty;
                //廃棄物名称のコントロールが電子用のを替える
                this.cantxt_HaikibutuNameCd.Visible = false;
                this.cantxt_HaikibutuNameCd.Enabled = false;
                this.cbtn_HaikibutuNameSan.Visible = false;
                this.cbtn_HaikibutuNameSan.Enabled = false;
                this.cbtn_ElecHaikibutuNameSan.Visible = true;
                this.cbtn_ElecHaikibutuNameSan.Enabled = true;
            }
            else
            {
                //廃棄物種類のコントロールが紙用のを替える
                this.cantxt_HaikibutuShuruiCd.Visible = true;
                //廃棄区分が全ての場合は、廃棄物種類をグレーアウトにします。
                this.cantxt_HaikibutuShuruiCd.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                this.cbtn_HaikibutuShuruiSan.Visible = true;
                this.cbtn_HaikibutuShuruiSan.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                this.cbtn_ElecHaikibutuShuruiSan.Visible = false;
                this.cbtn_ElecHaikibutuShuruiSan.Enabled = false;

                if (!this.cantxt_HaikibutuNameCd.Enabled)
                {
                    //区分が電子の状態から変わったかもしらない場合
                    //廃棄物名称のコントロールが紙用のを替える
                    this.cantxt_HaikibutuNameCd.Visible = true;
                    this.cantxt_HaikibutuNameCd.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                    this.cbtn_HaikibutuNameSan.Visible = true;
                    this.cbtn_HaikibutuNameSan.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                    this.cbtn_ElecHaikibutuNameSan.Visible = false;
                    this.cbtn_ElecHaikibutuNameSan.Enabled = false;
                }
                else
                {
                    //廃棄物名称が全ての場合は、廃棄物種類をグレーアウトにします。
                    this.cantxt_HaikibutuNameCd.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                    this.cbtn_HaikibutuNameSan.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                }
                if (!this.cantxt_HaikibutuNameCd.Enabled)
                {
                    //廃棄物名称を禁止した場合、内容をクリアする
                    //廃棄物名称をクリアします。
                    this.cantxt_HaikibutuNameCd.Text = string.Empty;
                    this.ctxt_HaikibutuName.Text = string.Empty;
                }

                this.cantxt_HaikibutuShuruiCd.popupWindowSetting.Clear();
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

                switch (this.cntxt_HaikiKbnCD.Text)
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
                    case "6":
                        serdto.Value = "6";
                        break;
                }
                dtowhere.SearchCondition.Add(serdto);
                this.cantxt_HaikibutuShuruiCd.popupWindowSetting.Add(dtowhere);
                this.cbtn_HaikibutuShuruiSan.popupWindowSetting.Add(dtowhere);
            }

        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KOUFU_DATE_FROM_Leave(object sender, EventArgs e)
        {
            this.DATE_TO.IsInputErrorOccured = false;
            this.DATE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void KOUFU_DATE_TO_Leave(object sender, EventArgs e)
        {
            this.DATE_FROM.IsInputErrorOccured = false;
            this.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
        }

        #endregion

        #region ファンクションボタンのイベント

        /// <summary>
        /// 一括選択(F1)
        /// 表示されているレコードについて、指定の条件に該当するレコードを選択するメソッドを呼び出す
        /// 指定の条件：換算後数量が登録データと入力データで相違がある
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            foreach (DataGridViewRow row in this.customDataGridView1.Rows)
            {
                row.Cells["Flg"].Value = true;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 再計算(F5)
        /// 表示されているレコードについて、換算後数量・減容後数量の再計算を行うメソッドを呼び出す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.MILogic.Saikeisann();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV出力(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            string title = "マニフェスト換算値再計算";
            CSVExport CSVExp = new CSVExport();
            CSVExp.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, title, this);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //必須チェック
            if (MILogic.SearchCheck())
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            if (this.MILogic.InputSearchCheck())
            {
                int count = this.MILogic.Search();
                if (count < 0)
                {
                    return;
                }
                else if (count == 0)
                {
                    this.messageShowLogic.MessageBoxShow("C001");
                    return;
                }
                else
                {
                    // 明細欄の[対象]カラムに初期値を設定
                    // （チェックがついていない[対象]の値がCSV出力時ブランクになる不具合の対策）
                    this.customDataGridView1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Flg"].Value == null).ToList().ForEach(c => c.Cells["Flg"].Value = false);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.MILogic.Regist(base.RegistErrorFlag);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public virtual void bt_func10_Click(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    //this.customSortHeader1.ShowCustomSortSettingDialog();

        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// フィルタ(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public virtual void bt_func11_Click(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);
        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.MILogic.SearchResult = new DataTable();
            this.customDataGridView1.Rows.Clear();

            this.Close();
            parentForm.Close();

            GC.Collect();
            GC.Collect();
            GC.Collect();

            LogUtility.DebugMethodEnd();
        }

#endregion

        /// <summary>
        /// 一覧初期列名
        /// </summary>
        public void SetInitCol()
        {
            //this.logic.AlertCount = this.MILogic.alertCount;

            //if (this.MILogic.SearchResult == null || this.MILogic.SearchResult.Rows.Count <= 0)
            //{
            //    this.MILogic.SearchResult = new DataTable();

            //    foreach (string s in this.strColumns)
            //    {
            //        this.MILogic.SearchResult.Columns.Add(s);
            //    }
            //    this.logic.CreateDataGridView(this.MILogic.SearchResult);
            //}
            //else
            //{
            //    this.logic.CreateDataGridView(this.MILogic.SearchResult);
            //    this.customDataGridView1.CurrentCell = this.customDataGridView1[4, 0];
            //    this.customDataGridView1.Refresh();

            //}

            ////列を隠す
            //string[] hideColumnName = { "SYSTEM_ID"
            //                            ,"DETAIL_SYSTEM_ID"
            //                            ,"HAIKI_KBN_CD"
            //                            ,"HAIKI_SHURUI_CD"
            //                            ,"HOUKOKUSHO_BUNRUI_CD"
            //                            ,"HAIKI_UNIT_CD"
            //                            ,"HST_GYOUSHA_CD"
            //                            ,"HST_GENBA_CD"
            //                            ,"UPN_GYOUSHA_CD"
            //                            ,"SBN_GYOUSHA_CD"
            //                            ,"HAIKI_NAME_CD"
            //                            ,"NISUGATA_CD"
            //                            ,"SBN_HOUHOU_CD"
            //                          };

            //foreach (string s in hideColumnName)
            //{
            //    //廃棄物区分によって抽出列に差があるため項目の存在チェックが必要
            //    if (this.customDataGridView1.Columns[s] != null)
            //    {
            //        this.customDataGridView1.Columns[s].Visible = false;
            //    }
            //}

            //foreach (DataColumn column in this.MILogic.SearchResult.Columns)
            //{
            //    if (column.ColumnName.Equals("対象 ") || column.ColumnName.Equals("換算後数量"))
            //    {
            //        column.ReadOnly = false;
            //        if (column.ColumnName.Equals("換算後数量"))
            //        {
            //            column.MaxLength = 8;
            //        }
            //    }
            //}

            ////foreach (DataGridViewRow row in this.customDataGridView1.Rows)
            ////{
            ////    row.Cells["対象 "].ReadOnly = false;
            ////    row.Cells["対象 "].Style.BackColor = Constans.NOMAL_COLOR;

            ////    row.Cells["換算後数量"].ReadOnly = false;
            ////    row.Cells["換算後数量"].Style.BackColor = Constans.NOMAL_COLOR;
            ////    //row.Cells["換算後数量"]
            ////    //r_framework.CustomControl.DgvCustomTextBoxColumn suuryou = (r_framework.CustomControl.DgvCustomTextBoxColumn)row.Cells["換算後数量"];
            ////}
            ////r_framework.CustomControl.DgvCustomTextBoxColumn NisugataName
            //foreach (DataGridViewColumn column in this.customDataGridView1.Columns)
            //{
            //    if (column.Name.Equals("換算後数量"))
            //    {
            //        DataGridViewTextBoxColumn test = this.customDataGridView1.Columns["換算後数量"] as DataGridViewTextBoxColumn;
            //        test.ReadOnly = false;
            //        //r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();

            //        //rangeSettingDto1.Max = new decimal(new int[] {
            //        //1410065407,
            //        //2,
            //        //0,
            //        //196608});
            //        //rangeSettingDto1.Min = new decimal(new int[] {
            //        //0,
            //        //0,
            //        //0,
            //        //0});
            //        //test.RangeLimitFlag = true;
            //        //test.RangeSetting = rangeSettingDto1;
            //        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            //        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            //        dataGridViewCellStyle1.Format = string.Format("N3");
            //        test.DefaultCellStyle = dataGridViewCellStyle1;
            //    }
            //    if (column.Name.Equals("対象 "))
            //    {
            //        DataGridViewTextBoxColumn test = this.customDataGridView1.Columns["対象 "] as DataGridViewTextBoxColumn;
            //        test.ReadOnly = false;
            //    }
            //}
        }

        /// <summary>
        /// 非表示項目削除
        /// </summary>
        public void RemoveHiddenCol()
        {
            string[] removeCol = {
                                    "SYSTEM_ID",
                                    "SEQ",
                                    "DETAIL_SYSTEM_ID",
                                    "HAIKI_KBN_CD",
                                    "ISKONGOU",
                                    //"Flg",
                                    //"HAIKI_KBN_NAME",
                                    //"KOUFU_DATE",
                                    //"MANIFEST_ID",
                                    "HAIKI_SHURUI_CD",
                                    //"HAIKI_SHURUI_NAME",
                                    "SUU_KAKUTEI_CODE",
                                    "HOUKOKUSHO_BUNRUI_CD",
                                    //"HOUKOKUSHO_BUNRUI_NAME",
                                    //"HAIKI_SUU",
                                    "HAIKI_UNIT_CD",
                                    "HAIKI_KAKUTEI_SUU",
                                    "HAIKI_KAKUTEI_UNIT_CODE",
                                    "SU1_UPN_SHA_EDI_PASSWORD",
                                    "SU1_UPN_SUU",
                                    "SU1_UPN_UNIT_CODE",
                                    "SU2_UPN_SHA_EDI_PASSWORD",
                                    "SU2_UPN_SUU",
                                    "SU2_UPN_UNIT_CODE",
                                    "SU3_UPN_SHA_EDI_PASSWORD",
                                    "SU3_UPN_SUU",
                                    "SU3_UPN_UNIT_CODE",
                                    "SU4_UPN_SHA_EDI_PASSWORD",
                                    "SU4_UPN_SUU",
                                    "SU4_UPN_UNIT_CODE",
                                    "SU5_UPN_SHA_EDI_PASSWORD",
                                    "SU5_UPN_SUU",
                                    "SU5_UPN_UNIT_CODE",
                                    "RECEPT_SUU",
                                    "RECEPT_UNIT_CODE",
                                    //"HAIKI_UNIT_NAME",
                                    //"DEN_OLD_KANSAN_SUU",
                                    //"DEN_OLD_KANSAN_UNIT_NAME",
                                    //"OLD_KANSAN_SUU",
                                    //"OLD_GENNYOU_SUU",
                                    //"NEW_KANSAN_SUU",
                                    //"NEW_GENNYOU_SUU",
                                    "HST_GYOUSHA_CD",
                                    //"HST_GYOUSHA_NAME",
                                    "HST_GENBA_CD",
                                    //"HST_GENBA_NAME",
                                    "UPN_GYOUSHA_CD",
                                    //"UPN_GYOUSHA_NAME",
                                    "SBN_GYOUSHA_CD",
                                    //"SBN_GYOUSHA_NAME",
                                    "HAIKI_NAME_CD",
                                    //"HAIKI_NAME",
                                    "NISUGATA_CD",
                                    //"NISUGATA_NAME",
                                    //"SBN_HOUHOU_CD",
                                    //"SHOBUN_HOUHOU_NAME",
                                    //"NEW_SBN_HOUHOU_CD",
                                    //"NEW_SBN_HOUHOU_NAME",
                                    "NEXT_MANIFEST_ID"
                                   };
            foreach (string column in removeCol)
            {
                // メモリ節約のため、非表示項目を削除する
                this.customDataGridView1.Columns.Remove(column);
            }
        }

        private void DATE_FROM_Leave(object sender, EventArgs e)
        {
            this.DATE_TO.IsInputErrorOccured = false;
            this.DATE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void DATE_TO_Leave(object sender, EventArgs e)
        {
            this.DATE_FROM.IsInputErrorOccured = false;
            this.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
        }

        /// <summary>
        /// 換算後数量入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //internal void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    try
        //    {
        //        //LogUtility.DebugMethodStart(sender,e);
        //        if (customDataGridView1.CurrentCell.ColumnIndex == customDataGridView1.Columns["換算後数量"].Index)
        //        {
        //            TextBox itemID = e.Control as TextBox;
        //            if (itemID != null)
        //            {
        //                itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("Ichiran_EditingControlShowing", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        //LogUtility.DebugMethodEnd();
        //    }
        //}

        /// <summary>
        /// 数字入力制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        //LogUtility.DebugMethodStart(sender,e);
        //        if (!char.IsControl(e.KeyChar)
        //            && !char.IsDigit(e.KeyChar))
        //        {
        //            e.Handled = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("itemID_KeyPress", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        //LogUtility.DebugMethodEnd();
        //    }
        //}
        /// <summary>
        /// セルフォーマット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //try
            //{
            //    if (this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("換算後数量"))
            //    {
            //        DgvCustomNumericTextBox2Column test = this.customDataGridView1.Columns[e.ColumnIndex] as DgvCustomNumericTextBox2Column;
            //        r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            //        rangeSettingDto1.Max = new decimal(new int[] {
            //        1410065407,
            //        2,
            //        0,
            //        196608});
            //        rangeSettingDto1.Min = new decimal(new int[] {
            //        0,
            //        0,
            //        0,
            //        0});
            //        test.RangeSetting = rangeSettingDto1;
            //        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            //        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            //        test.DefaultCellStyle = dataGridViewCellStyle1;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogUtility.Error("Ichiran_CellFormatting", ex);
            //    throw;
            //}
            //finally
            //{
            //    //LogUtility.DebugMethodEnd();
            //}
        }

        /// <summary>
        /// セルインテル処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("NEW_SBN_HOUHOU_CD") && !isErr)
            {
                this.preSbnHouhouCd = Convert.ToString(this.customDataGridView1.Rows[e.RowIndex].Cells["NEW_SBN_HOUHOU_CD"].Value);
                this.preSbnHouhouName = Convert.ToString(this.customDataGridView1.Rows[e.RowIndex].Cells["NEW_SBN_HOUHOU_NAME"].Value);
            }
        }

        private void customDataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("NEW_SBN_HOUHOU_CD") 
                || this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("SBN_HOUHOU_CD"))
            {
                string sbnHouhouCd = this.customDataGridView1.Columns[e.ColumnIndex].Name;
                string sbnHouhouName = "";
                if (this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("SBN_HOUHOU_CD"))
                {
                    sbnHouhouName = "SHOBUN_HOUHOU_NAME";
                }
                else if(this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("NEW_SBN_HOUHOU_CD"))
                {
                    sbnHouhouName = "NEW_SBN_HOUHOU_NAME";
                }

                if (this.MILogic.checkHouhou(e.RowIndex, sbnHouhouCd, sbnHouhouName))
                {
                    e.Cancel = true;
                    TextBox temp = this.customDataGridView1.EditingControl as TextBox;
                    temp.SelectAll();
                    this.customDataGridView1.BeginEdit(false);
                    return;
                }
            }
            if (this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("NEW_KANSAN_SUU"))
            {
                this.MILogic.SetGenyouti(e.RowIndex);
            }
        }

        public void sbnHouhouBeforePopup()
        {
            this.popBeforeCd = Convert.ToString(this.customDataGridView1.CurrentCell.Value);
        }

        public void sbnHouhouAfterPopup()
        {
            int index = this.customDataGridView1.CurrentCell.RowIndex;
            string cd = Convert.ToString(this.customDataGridView1.CurrentCell.Value);
            if (this.popBeforeCd != cd)
            {
                preSbnHouhouCd = cd;
                preSbnHouhouName = Convert.ToString(this.customDataGridView1.Rows[index].Cells["NEW_SBN_HOUHOU_NAME"].Value);
            }
        }

        public void denSbnHouhouBeforePopup()
        {
            this.popBeforeCd = Convert.ToString(this.customDataGridView1.CurrentCell.Value);
        }

        public void denSbnHouhouAfterPopup()
        {
            int index = this.customDataGridView1.CurrentCell.RowIndex;
            string cd = Convert.ToString(this.customDataGridView1.CurrentCell.Value);
            if (this.popBeforeCd != cd)
            {
                preSbnHouhouCd = cd;
                preSbnHouhouName = Convert.ToString(this.customDataGridView1.Rows[index].Cells["SHOBUN_HOUHOU_NAME"].Value);
            }
        }


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
        #region 画面コントロールイベント
        /// <summary>
        /// 排出事業者Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void cantxt_HaisyutuGyousyaCd_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.ctxt_HaisyutuGyousyaName.Text = string.Empty;
            this.cantxt_HaisyutuJigyoujouCd.Text = string.Empty;
            this.ctxt_HaisyutuJigyoujouName.Text = string.Empty;

            if (string.IsNullOrEmpty(this.cantxt_HaisyutuGyousyaCd.Text))
            {
                return;
            }
            else
            {
                bool catchErr = false;
                M_GYOUSHA[] results = this.MILogic.GetGyousha(this.cantxt_HaisyutuGyousyaCd.Text, 1, out catchErr);
                if (catchErr) { return; }
                if (results != null && results.Length > 0)
                {
                    this.ctxt_HaisyutuGyousyaName.Text = results[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "排出事業者");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 排出事業場Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void cantxt_HaisyutuJigyoujouCd_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.ctxt_HaisyutuJigyoujouName.Text = string.Empty;

            if (string.IsNullOrEmpty(this.cantxt_HaisyutuJigyoujouCd.Text))
            {
                return;
            }
            else
            {
                // 20150921 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                if (string.IsNullOrEmpty(this.cantxt_HaisyutuGyousyaCd.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E051", "排出事業者");
                    this.cantxt_HaisyutuJigyoujouCd.Text = string.Empty;
                    e.Cancel = true;
                    return;
                }
                // 20150921 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                bool catchErr = false;
                M_GENBA[] results = this.MILogic.GetGenba(this.cantxt_HaisyutuGyousyaCd.Text, this.cantxt_HaisyutuJigyoujouCd.Text,1, out catchErr);
                if (catchErr) { return; }
                if (results != null && results.Length > 0)
                {
                    this.ctxt_HaisyutuJigyoujouName.Text = results[0].GENBA_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "現場");
                    e.Cancel = true;
                }
            }
        }

        public void Before_GyoushaPop()
        {
            this.preHstGyoushaCd = this.cantxt_HaisyutuGyousyaCd.Text;
        }

        public void After_GyoushaPop()
        {
            if (this.preHstGyoushaCd != this.cantxt_HaisyutuGyousyaCd.Text)
            {
                this.cantxt_HaisyutuJigyoujouCd.Text = string.Empty;
                this.ctxt_HaisyutuJigyoujouName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 運搬受託者Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void cantxt_UnpanJyutakushaCd_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.ctxt_UnpanJyutakushaName.Text = string.Empty;

            if (string.IsNullOrEmpty(this.cantxt_UnpanJyutakushaCd.Text))
            {
                return;
            }
            else
            {
                bool catchErr = false;
                M_GYOUSHA[] results = this.MILogic.GetGyousha(this.cantxt_UnpanJyutakushaCd.Text, 2, out catchErr);
                if (catchErr) { return; }
                if (results != null && results.Length > 0)
                {
                    this.ctxt_UnpanJyutakushaName.Text = results[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 処分受託者Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void cantxt_SyobunJyutakushaCd_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.ctxt_SyobunJyutakushaName.Text = string.Empty;
            //157951 S
            this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
            this.ctxt_UnpanJyugyobaName.Text = string.Empty;
            //157951 E
            if (string.IsNullOrEmpty(this.cantxt_SyobunJyutakushaCd.Text))
            {
                return;
            }
            else
            {
                bool catchErr = false;
                M_GYOUSHA[] results = this.MILogic.GetGyousha(this.cantxt_SyobunJyutakushaCd.Text, 3, out catchErr);
                if (catchErr) { return; }
                if (results != null && results.Length > 0)
                {
                    this.ctxt_SyobunJyutakushaName.Text = results[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                }
            }
        }


        /// <summary>
        /// 廃棄物種類 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void cantxt_HaikibutuShuruiCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkHaikibutuShurui(this.cntxt_HaikiKbnCD, this.cantxt_HaikibutuShuruiCd))
            {
                case 0://正常
                    break;

                case 1://空
                    //報告書分類削除
                    this.ctxt_HaikibutuShuruiName.Text = string.Empty;

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
        internal void cantxt_HaikibutuNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkHaikibutuName(this.cantxt_HaikibutuNameCd))
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
        internal void cantxt_ElecHaikiShuruiCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkDenshiHaikibutuShurui(this.cantxt_ElecHaikiShuruiCd))
            {
                case 0://正常
                    break;

                case 1://空
                    //報告書分類削除
                    this.ctxt_HaikibutuShuruiName.Text = string.Empty;
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
        internal void cantxt_ElecHaikiNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkDenshiHaikibutuName(this.cantxt_ElecHaikiNameCd))
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
        #endregion

        /// <summary>
        /// 廃棄物区分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_HaikiKbnCD_TextChanged(object sender, EventArgs e)
        {

            //廃棄区分が変わった場合は、廃棄物種類をクリアします。
            this.cantxt_HaikibutuShuruiCd.Text = string.Empty;
            this.cantxt_ElecHaikiShuruiCd.Text = string.Empty;
            this.ctxt_HaikibutuShuruiName.Text = string.Empty;

            this.SHOBUN_HOUHOU_CD_REPLACE.Text = string.Empty;
            this.SHOBUN_HOUHOU_NAME_REPLACE.Text = string.Empty;

            if (this.cntxt_HaikiKbnCD.Text == "4" || this.cntxt_HaikiKbnCD.Text == "5")
            {
                //廃棄物種類のコントロールが電子用のを替える
                this.cantxt_HaikibutuShuruiCd.Visible = false;
                this.cantxt_HaikibutuShuruiCd.Enabled = false;
                this.cbtn_HaikibutuShuruiSan.Visible = false;
                this.cbtn_HaikibutuShuruiSan.Enabled = false;
                this.cantxt_ElecHaikiShuruiCd.Visible = true;
                this.cantxt_ElecHaikiShuruiCd.Enabled = true;
                this.cbtn_ElecHaikibutuShuruiSan.Visible = true;
                this.cbtn_ElecHaikibutuShuruiSan.Enabled = true;

                //廃棄区分が変わった場合は、廃棄物名称をクリアします。
                this.cantxt_HaikibutuNameCd.Text = string.Empty;
                this.cantxt_ElecHaikiNameCd.Text = string.Empty;
                this.ctxt_HaikibutuName.Text = string.Empty;
                //廃棄物名称のコントロールが電子用のを替える
                this.cantxt_HaikibutuNameCd.Visible = false;
                this.cantxt_HaikibutuNameCd.Enabled = false;
                this.cbtn_HaikibutuNameSan.Visible = false;
                this.cbtn_HaikibutuNameSan.Enabled = false;
                this.cantxt_ElecHaikiNameCd.Visible = true;
                this.cantxt_ElecHaikiNameCd.Enabled = true;
                this.cbtn_ElecHaikibutuNameSan.Visible = true;
                this.cbtn_ElecHaikibutuNameSan.Enabled = true;

                // 処分方法条件追加
                this.MILogic.shobunJyokenAdd();

            }
            else
            {
                //廃棄物種類のコントロールが紙用のを替える
                this.cantxt_HaikibutuShuruiCd.Visible = true;
                //廃棄区分が全ての場合は、廃棄物種類をグレーアウトにします。
                this.cantxt_HaikibutuShuruiCd.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                this.cbtn_HaikibutuShuruiSan.Visible = true;
                this.cbtn_HaikibutuShuruiSan.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                this.cantxt_ElecHaikiShuruiCd.Visible = false;
                this.cantxt_ElecHaikiShuruiCd.Enabled = false;
                this.cbtn_ElecHaikibutuShuruiSan.Visible = false;
                this.cbtn_ElecHaikibutuShuruiSan.Enabled = false;

                if (!this.cantxt_HaikibutuNameCd.Enabled)
                {
                    //区分が電子の状態から変わったかもしらない場合
                    //廃棄物名称のコントロールが紙用のを替える
                    this.cantxt_HaikibutuNameCd.Visible = true;
                    this.cantxt_HaikibutuNameCd.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                    this.cbtn_HaikibutuNameSan.Visible = true;
                    this.cbtn_HaikibutuNameSan.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                    this.cantxt_ElecHaikiNameCd.Visible = false;
                    this.cantxt_ElecHaikiNameCd.Enabled = false;
                    this.cbtn_ElecHaikibutuNameSan.Visible = false;
                    this.cbtn_ElecHaikibutuNameSan.Enabled = false;
                }
                else
                {
                    //廃棄物名称が全ての場合は、廃棄物種類をグレーアウトにします。
                    this.cantxt_HaikibutuNameCd.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                    this.cbtn_HaikibutuNameSan.Enabled = this.cntxt_HaikiKbnCD.Text != "6";
                }
                if (!this.cantxt_HaikibutuNameCd.Enabled)
                {
                    //廃棄物名称を禁止した場合、内容をクリアする
                    //廃棄物名称をクリアします。
                    this.cantxt_HaikibutuNameCd.Text = string.Empty;
                    this.cantxt_ElecHaikiNameCd.Text = string.Empty;
                    this.ctxt_HaikibutuName.Text = string.Empty;
                }

                this.cantxt_HaikibutuShuruiCd.popupWindowSetting.Clear();
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

                switch (this.cntxt_HaikiKbnCD.Text)
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
                    case "6":
                        serdto.Value = "5";
                        break;
                }
                dtowhere.SearchCondition.Add(serdto);
                this.cantxt_HaikibutuShuruiCd.popupWindowSetting.Add(dtowhere);
                this.cbtn_HaikibutuShuruiSan.popupWindowSetting.Add(dtowhere);

                // 処分方法条件追加
                this.MILogic.shobunJyokenAdd();
            }

            // 明細のレイアウト調整
            this.MILogic.ExecuteAlignmentForDetail();
        }

        /// <summary>
        /// 日付ToDoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DATE_TO_DoubleClick(object sender, EventArgs e)
        {
            // FROMをTOにCopy
            this.DATE_TO.Text = this.DATE_FROM.Text;
        }

        private void SHOBUN_HOUHOU_CD_SELECT_Validated(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(this.SHOBUN_HOUHOU_CD_SELECT.Text))
            {
                this.SHOBUN_CHECK.Checked = false;
                this.MILogic. checkHouhouSelect(this.SHOBUN_HOUHOU_CD_SELECT.Text);
            }
        }

        private void SHOBUN_CHECK_CheckedChanged(object sender, EventArgs e)
        {
            if(this.SHOBUN_CHECK.Checked)
            {
                this.SHOBUN_HOUHOU_CD_SELECT.Text = string.Empty;
                this.SHOBUN_HOUHOU_NAME_SELECT.Text = string.Empty;
            }
        }

        private void customDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("Flg"))
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(this.checkBoxAll.Width, this.checkBoxAll.Height))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // Bitmapに描画
                        this.checkBoxAll.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                        // 描画位置設定
                        int rightMargin = 4;
                        int x = (e.CellBounds.Width - this.checkBoxAll.Width) - rightMargin;
                        int y = ((e.CellBounds.Height - this.checkBoxAll.Height) / 2);

                        // DataGridViewの現在描画中のセルに描画
                        Point pt = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);
                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt);
                        e.Handled = true;
                    }
                }
            }
        }

        private void customDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.customDataGridView1.Columns[e.ColumnIndex].Name.Equals("Flg") && e.RowIndex == -1)
            {
                checkBoxAll.Checked = !checkBoxAll.Checked;
                this.customDataGridView1.Refresh();
            }
        }

        /// <summary>
        /// すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.customDataGridView1.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in this.customDataGridView1.Rows)
            {
                row.Cells["Flg"].Value = checkBoxAll.Checked;
            }
            //this.customDataGridView1.CurrentCell = this.customDataGridView1.Rows[0].Cells[6];
            // 再描画
            var parent = (BusinessBaseForm)this.Parent;
            parent.txb_process.Focus();
            //this.customDataGridView1.Focus();
        }

        private void shubunReplaceButton_Click(object sender, EventArgs e)
        {
            this.MILogic.ShubunHouhouReplace(sender, e);
        }

        private void SHOBUN_HOUHOU_CD_REPLACE_Validating(object sender, CancelEventArgs e)
        {
            if(!string.IsNullOrEmpty(this.SHOBUN_HOUHOU_CD_REPLACE.Text))
            {
                this.MILogic.checkHouhouReplace(this.SHOBUN_HOUHOU_CD_REPLACE.Text);
            }
        }
        //MOD NHU 20211102 #157951 S
        private void cantxt_UnpanJyugyobaNameCd_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.ctxt_UnpanJyugyobaName.Text = string.Empty;

            if (string.IsNullOrEmpty(this.cantxt_UnpanJyugyobaNameCd.Text))
            {
                return;
            }
            else
            {
                // 20150921 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                if (string.IsNullOrEmpty(this.cantxt_SyobunJyutakushaCd.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E051", "処分受託者");
                    this.cantxt_SyobunJyutakushaCd.Text = string.Empty;
                    e.Cancel = true;
                    return;
                }
                // 20150921 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                bool catchErr = false;
                M_GENBA[] results = this.MILogic.GetGenba(this.cantxt_SyobunJyutakushaCd.Text, this.cantxt_UnpanJyugyobaNameCd.Text,2, out catchErr);
                if (catchErr) { return; }
                if (results != null && results.Length > 0)
                {
                    if (results[0].SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || results[0].SAISHUU_SHOBUNJOU_KBN.IsTrue)
                    {
                        this.ctxt_UnpanJyugyobaName.Text = results[0].GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        this.messageShowLogic.MessageBoxShow("E020", "現場");
                        e.Cancel = true;
                    }
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E020", "現場");
                    e.Cancel = true;
                }
            }
        }
        public void Before_SbnGyoushaPop()
        {
            this.preSbnGyoushaCd = this.cantxt_SyobunJyutakushaCd.Text;
        }

        public void After_SbnGyoushaPop()
        {
            if (this.preSbnGyoushaCd != this.cantxt_SyobunJyutakushaCd.Text)
            {
                this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                this.ctxt_UnpanJyugyobaName.Text = string.Empty;
            }
        }
        //MOD NHU 20211102 #157951 E
    }
}
