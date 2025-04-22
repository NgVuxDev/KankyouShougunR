using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace Shougun.Core.Common.DenpyouHimodukeIchiran
{
    [Implementation]
    public partial class UIForm : IchiranHimoSuperForm
    {
        #region プロパティ

        /// <summary>
        /// 親フォーム
        /// </summary>
        public UIBaseForm ParentBaseForm { get; set; }

        #endregion

        #region フィールド

        #region const

        /// <summary>
        /// 伝票日付ラジオボタン初期値
        /// </summary>
        private const string DENPYOU_DATE_INIT_NUM = "1";

        /// <summary>
        /// 伝票日付
        /// </summary>
        private const string DENPYOU_DATE = "伝票日付";

        /// <summary>
        /// 交付年月日
        /// </summary>
        private const string KOUHU_DATE = "交付年月日";

        /// <summary>
        /// 運搬終了日
        /// </summary>
        private const string UNPAN_DATE = "運搬終了日";

        /// <summary>
        /// 処分終了日
        /// </summary>
        private const string SHOBUN_DATE = "処分終了日";

        /// <summary>
        /// 最終処分終了日
        /// </summary>
        private const string SAISHUU_SHOBUN_DATE = "最終処分終了日";

        /// <summary>
        /// 伝票日付Fromテキスト文言
        /// </summary>
        private const string DENPYOU_DATE_FROM_HINT_TEXT = "伝票日付Fromを入力してください";

        /// <summary>
        /// 伝票日付Toテキスト文言
        /// </summary>
        private const string DENPYOU_DATE_TO_HINT_TEXT = "伝票日付Toを入力してください";

        /// <summary>
        /// 交付年月日Fromテキスト文言
        /// </summary>
        private const string KOUHU_DATE_FROM_HINT_TEXT = "交付年月日Fromを入力してください";

        /// <summary>
        /// 交付年月日Toテキスト文言
        /// </summary>
        private const string KOUHU_DATE_TO_HINT_TEXT = "交付年月日Toを入力してください";

        /// <summary>
        /// 運搬終了日Fromテキスト文言
        /// </summary>
        private const string UNPAN_DATE_FROM_HINT_TEXT = "運搬終了日Fromを入力してください";

        /// <summary>
        /// 交付年月日Toテキスト文言
        /// </summary>
        private const string UNPAN_DATE_TO_HINT_TEXT = "運搬終了日Toを入力してください";

        /// <summary>
        /// 処分終了日Fromテキスト文言
        /// </summary>
        private const string SHOBUN_DATE_FROM_HINT_TEXT = "処分終了日Fromを入力してください";

        /// <summary>
        /// 処分終了日Toテキスト文言
        /// </summary>
        private const string SHOBUN_DATE_TO_HINT_TEXT = "処分終了日Toを入力してください";

        /// <summary>
        /// 最終処分終了日Fromテキスト文言
        /// </summary>
        private const string SAISHUU_SHOBUN_DATE_FROM_HINT_TEXT = "最終処分終了日Fromを入力してください";

        /// <summary>
        /// 最終処分終了日Toテキスト文言
        /// </summary>
        private const string SAISHUU_SHOBUN_DATE_TO_HINT_TEXT = "最終処分終了日Toを入力してください";

        #endregion

        private DenpyouHimodukeIchiran.LogicClass DenpyouHimoLogic;

        private string selectQuery = string.Empty;

        private string orderQuery = string.Empty;

        private HeaderForm header_new;

        private Boolean isLoaded;

        #endregion

        #region 伝票紐付一覧画面Form

        /// <summary>
        /// 伝票紐付一覧画面Form
        /// </summary>
        public UIForm(DENSHU_KBN denshuKbn, string searchString, HeaderForm headerForm, string strSyainCD)
            : base(denshuKbn, false)
        {
            try
            {
                // 初期化
                this.InitializeComponent();
                this.header_new = headerForm;
                this.ShainCd = "12345";

                //// 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.DenpyouHimoLogic = new LogicClass(this);

                if (!string.IsNullOrEmpty(searchString))
                {
                    string getSearchString = searchString.Replace("\r", "").Replace("\n", "");
                    //検索対象文字列取得
                    this.DenpyouHimoLogic.searchString = getSearchString;
                }

                // ヘッダ設定
                DenpyouHimoLogic.SetHeader(header_new);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);

                //社員コードを取得すること
                this.ShainCd = strSyainCD;
                //Main画面で社員コード値を取得すること
                DenpyouHimoLogic.syainCode = strSyainCD;
                //伝種区分を取得すること
                DENSHU_KBN time = (DENSHU_KBN)Enum.Parse(typeof(DENSHU_KBN), "DENPYOU_HIMODUKE_ICHIRAN", true);
                DenpyouHimoLogic.denShu_Kbn = (int)time;

                // Load値設定
                isLoaded = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
        }

        #endregion

        #region 画面コントロールイベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);

                ParentBaseForm = (UIBaseForm)this.Parent;

                // 画面情報の初期化
                if (isLoaded == false)
                {
                    this.DenpyouHimoLogic.WindowInit();
                }

                this.DenpyouHimoLogic.selectQuery = this.logic.SelectQeury;
                this.DenpyouHimoLogic.orderByQuery = this.logic.OrderByQuery;

                if (isLoaded == true)
                {
                    this.DenpyouHimoLogic.Search();
                }
                else
                {
                    this.DenpyouHimoLogic.ColumnNameSet();
                }

                isLoaded = true;

                // DataGridView表示
                //if (!this.DesignMode)
                //{
                //    this.logic.CreateDataGridView(this.Table);
                //}

                ////初期化時全てチャックボックスが入れる
                //this.cstRdoBtn1.Checked = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                //throw;
            }
        }

        // 状況テクストボックス値変更
        private void numTxtBox_KsTsKn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // 1.受付
                if ("1".Equals(this.numTxtBox_KsTsKn.Text))
                {
                    this.cstRdoBtn1.Checked = true;
                }

                // 2.計量
                if ("2".Equals(this.numTxtBox_KsTsKn.Text))
                {
                    this.cstRdoBtn2.Checked = true;
                }

                // 3.受入
                if ("3".Equals(this.numTxtBox_KsTsKn.Text))
                {
                    this.cstRdoBtn3.Checked = true;
                }

                // 4.出荷
                if ("4".Equals(this.numTxtBox_KsTsKn.Text))
                {
                    this.cstRdoBtn4.Checked = true;
                }

                // 5.売上/支払
                if ("5".Equals(this.numTxtBox_KsTsKn.Text))
                {
                    this.cstRdoBtn5.Checked = true;
                }

                // 6.マニ１次
                if ("6".Equals(this.numTxtBox_KsTsKn.Text))
                {
                    this.cstRdoBtn6.Checked = true;
                }

                // 7.マニ２次
                if ("7".Equals(this.numTxtBox_KsTsKn.Text))
                {
                    this.cstRdoBtn7.Checked = true;
                }

                // 8.伝マニ
                if ("8".Equals(this.numTxtBox_KsTsKn.Text))
                {
                    this.cstRdoBtn8.Checked = true;
                }

                // 9.運賃
                if ("9".Equals(this.numTxtBox_KsTsKn.Text))
                {
                    this.cstRdoBtn9.Checked = true;
                }

                // 10.代納
                if ("10".Equals(this.numTxtBox_KsTsKn.Text))
                {
                    this.cstRdoBtn10.Checked = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("numTxtBox_KsTsKn_TextChanged", ex);
                throw;
            }
        }

        #region 検索対象機能ラジオボタンイベント

        private void cstRdoBtn1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cstRdoBtn1.Checked)
                {
                    this.numTxtBox_KsTsKn.Text = "1";
                    this.DenpyouHimoLogic.disp_Flg = 1;
                    DenpyouDatePanelDispChange(false);
                    this.DenoyouDate_Label.Text = DENPYOU_DATE;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstRdoBtn1_CheckedChanged", ex);
                throw;
            }
        }

        private void cstRdoBtn2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cstRdoBtn2.Checked)
                {
                    this.numTxtBox_KsTsKn.Text = "2";
                    this.DenpyouHimoLogic.disp_Flg = 2;
                    DenpyouDatePanelDispChange(false);
                    this.DenoyouDate_Label.Text = DENPYOU_DATE;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstRdoBtn2_CheckedChanged", ex);
                throw;
            }
        }

        private void cstRdoBtn3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cstRdoBtn3.Checked)
                {
                    this.numTxtBox_KsTsKn.Text = "3";
                    this.DenpyouHimoLogic.disp_Flg = 3;
                    DenpyouDatePanelDispChange(false);
                    this.DenoyouDate_Label.Text = DENPYOU_DATE;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstRdoBtn3_CheckedChanged", ex);
                throw;
            }
        }

        private void cstRdoBtn4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cstRdoBtn4.Checked)
                {
                    this.numTxtBox_KsTsKn.Text = "4";
                    this.DenpyouHimoLogic.disp_Flg = 4;
                    DenpyouDatePanelDispChange(false);
                    this.DenoyouDate_Label.Text = DENPYOU_DATE;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstRdoBtn4_CheckedChanged", ex);
                throw;
            }
        }

        private void cstRdoBtn5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cstRdoBtn5.Checked)
                {
                    this.numTxtBox_KsTsKn.Text = "5";
                    this.DenpyouHimoLogic.disp_Flg = 5;
                    DenpyouDatePanelDispChange(false);
                    this.DenoyouDate_Label.Text = DENPYOU_DATE;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstRdoBtn5_CheckedChanged", ex);
                throw;
            }
        }

        private void cstRdoBtn6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cstRdoBtn6.Checked)
                {
                    this.numTxtBox_KsTsKn.Text = "6";
                    this.DenpyouHimoLogic.disp_Flg = 6;
                    DenpyouDatePanelDispChange(true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstRdoBtn6_CheckedChanged", ex);
                throw;
            }
        }

        private void cstRdoBtn7_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cstRdoBtn7.Checked)
                {
                    this.numTxtBox_KsTsKn.Text = "7";
                    this.DenpyouHimoLogic.disp_Flg = 7;
                    DenpyouDatePanelDispChange(true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstRdoBtn7_CheckedChanged", ex);
                throw;
            }
        }

        private void cstRdoBtn8_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cstRdoBtn8.Checked)
                {
                    this.numTxtBox_KsTsKn.Text = "8";
                    this.DenpyouHimoLogic.disp_Flg = 8;
                    DenpyouDatePanelDispChange(true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstRdoBtn8_CheckedChanged", ex);
                throw;
            }
        }

        private void cstRdoBtn9_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cstRdoBtn9.Checked)
                {
                    this.numTxtBox_KsTsKn.Text = "9";
                    this.DenpyouHimoLogic.disp_Flg = 9;
                    DenpyouDatePanelDispChange(false);
                    this.DenoyouDate_Label.Text = DENPYOU_DATE;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstRdoBtn9_CheckedChanged", ex);
                throw;
            }
        }

        private void cstRdoBtn10_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cstRdoBtn10.Checked)
                {
                    this.numTxtBox_KsTsKn.Text = "10";
                    this.DenpyouHimoLogic.disp_Flg = 10;
                    DenpyouDatePanelDispChange(false);
                    this.DenoyouDate_Label.Text = DENPYOU_DATE;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstRdoBtn10_CheckedChanged", ex);
                throw;
            }
        }

        #endregion

        #region 伝票日付ラジオボタンイベント

        private void Denpyou_Date_RdoBtn1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Denpyou_Date_RdoBtn1.Checked)
                {
                    this.DenoyouDate_Label.Text = KOUHU_DATE;
                    this.dtpDateFrom.Tag = KOUHU_DATE_FROM_HINT_TEXT;
                    this.dtpDateTo.Tag = KOUHU_DATE_TO_HINT_TEXT;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Denpyou_Date_RdoBtn1_CheckedChanged", ex);
                throw;
            }
        }

        private void Denpyou_Date_RdoBtn2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Denpyou_Date_RdoBtn2.Checked)
                {
                    this.DenoyouDate_Label.Text = UNPAN_DATE;
                    this.dtpDateFrom.Tag = UNPAN_DATE_FROM_HINT_TEXT;
                    this.dtpDateTo.Tag = UNPAN_DATE_TO_HINT_TEXT;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Denpyou_Date_RdoBtn2_CheckedChanged", ex);
                throw;
            }
        }

        private void Denpyou_Date_RdoBtn3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Denpyou_Date_RdoBtn3.Checked)
                {
                    this.DenoyouDate_Label.Text = SHOBUN_DATE;
                    this.dtpDateFrom.Tag = SHOBUN_DATE_FROM_HINT_TEXT;
                    this.dtpDateTo.Tag = SHOBUN_DATE_TO_HINT_TEXT;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Denpyou_Date_RdoBtn2_CheckedChanged", ex);
                throw;
            }
        }

        private void Denpyou_Date_RdoBtn4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Denpyou_Date_RdoBtn4.Checked)
                {
                    this.DenoyouDate_Label.Text = SAISHUU_SHOBUN_DATE;
                    this.dtpDateFrom.Tag = SAISHUU_SHOBUN_DATE_FROM_HINT_TEXT;
                    this.dtpDateTo.Tag = SAISHUU_SHOBUN_DATE_TO_HINT_TEXT;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Denpyou_Date_RdoBtn4_CheckedChanged", ex);
                throw;
            }
        }

        #endregion

        #endregion

        #region 並替移動

        /// <summary>
        /// 並替移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MoveToSort(object sender, EventArgs e)
        {
            try
            {
                this.customSortHeader1.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("MoveToSort", ex);
                throw;
            }
        }

        #endregion

        #region 検索結果表示

        /// <summary>
        /// 検索結果表示
        /// </summary>
        public virtual void ShowData()
        {
            try
            {
                this.Table = this.DenpyouHimoLogic.searchResult;

                if (!this.DesignMode)
                {
                    this.logic.AlertCount = this.DenpyouHimoLogic.alertCount;
                    this.logic.CreateDataGridView(this.Table);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowData", ex);
                throw;
            }
        }

        #endregion

        #region イベント

        private void numTxtBox_KsTsKn_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int intSelLength = this.numTxtBox_KsTsKn.SelectionLength;
                int intSelStart = this.numTxtBox_KsTsKn.SelectionStart;
                string str = "";

                if (this.numTxtBox_KsTsKn.Text.Length == 2)
                {
                    if (intSelLength == 0)
                    {
                        str = this.numTxtBox_KsTsKn.Text;
                    }
                    else if (intSelLength == 1)
                    {
                        if (intSelStart == 0)
                        {
                            str = e.KeyChar.ToString() + this.numTxtBox_KsTsKn.Text.Substring(1, 1);
                        }
                        else if (intSelStart == 1)
                        {
                            str = this.numTxtBox_KsTsKn.Text.Substring(0, 1) + e.KeyChar.ToString();
                        }
                    }
                    else if (intSelLength == 2)
                    {
                        str = e.KeyChar.ToString();
                    }
                }

                if (this.numTxtBox_KsTsKn.Text.Length == 1)
                {
                    if (intSelLength == 0)
                    {
                        if (intSelStart == 0)
                        {
                            str = e.KeyChar.ToString() + this.numTxtBox_KsTsKn.Text;
                        }
                        else if (intSelStart == 1)
                        {
                            str = this.numTxtBox_KsTsKn.Text + e.KeyChar.ToString();
                        }
                    }
                    else if (intSelLength == 1)
                    {
                        if (intSelStart == 0)
                        {
                            str = e.KeyChar.ToString();
                        }
                    }
                }

                str = str.Replace("\b", "").Replace("\t", "").Replace("\r", "");

                if (str.Length > 0 && (int.Parse(str) > 10 || int.Parse(str) == 0))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("numTxtBox_KsTsKn_KeyPress", ex);
                throw;
            }
        }

        private void numTxtBox_KsTsKn_Leave(object sender, EventArgs e)
        {
            try
            {
                // null
                if (String.IsNullOrEmpty(this.numTxtBox_KsTsKn.Text) == true)
                {
                    this.cstRdoBtn1.Checked = true;
                    this.numTxtBox_KsTsKn.Text = "1";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("numTxtBox_KsTsKn_Leave", ex);
                throw;
            }
        }

        #endregion

        #region 伝票日付ラジオボタン画面表示切替

        /// <summary>
        /// 伝票日付ラジオボタンの表示切替
        /// </summary>
        /// <param name="isDisp">True:表示　False:非表示</param>
        private void DenpyouDatePanelDispChange(bool isDisp)
        {
            if (isDisp)
            {
                // 非表示から表示に変わった場合のみ初期化
                if (!this.numTxtBox_Denpyou_Date.Visible && !this.Denpyou_Date_Panel.Visible)
                {
                    this.numTxtBox_Denpyou_Date.Text = DENPYOU_DATE_INIT_NUM;
                }
                this.numTxtBox_Denpyou_Date.Visible = true;
                this.Denpyou_Date_Panel.Visible = true;
            }
            else
            {
                this.numTxtBox_Denpyou_Date.Visible = false;
                this.Denpyou_Date_Panel.Visible = false;
            }
        }

        #endregion
    }
}