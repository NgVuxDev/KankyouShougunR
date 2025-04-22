using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Message;

namespace Shougun.Core.BusinessManagement.MitumoriIchiran
{
    [Implementation]
    public partial class MitumoriIchiranForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region プロパティ

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 手入力か判断するフラグ
        /// true = 手入力  false = ポップアップ入力
        /// </summary>
        internal bool ManualInputFlg = true;

        #endregion

        #region フィールド

        private MitumoriIchiran.LogicClass MitumoriIchiranLogic;

        private string selectQuery = string.Empty;

        private string orderQuery = string.Empty;

        private string strOldGyousyaCD = string.Empty;
        private string strOldHikiaiFlg = string.Empty;

        private string strOldGenbaCD = string.Empty;
        private string strOldGenbaHikiaiFlg = string.Empty;

        private HeaderForm header_new;

        private Boolean isLoaded;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region 見積一覧画面Form

        /// <summary>
        /// 見積一覧画面Form
        /// </summary>
        public MitumoriIchiranForm(DENSHU_KBN denshuKbn, string searchString, HeaderForm headerForm, string Syainid)
            : base(denshuKbn, false)
        {
            try
            {
                // 初期化
                this.InitializeComponent();
                this.header_new = headerForm;
                this.ShainCd = SystemProperty.Shain.CD; // 社員CDを取得
                this.DenshuKbn = DENSHU_KBN.MITSUMORI; // 伝種区分を取得

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.MitumoriIchiranLogic = new LogicClass(this);
                // 非表示列を登録
                this.SetHiddenColumns(
                    this.MitumoriIchiranLogic.HIDDEN_SYSTEM_ID,
                    this.MitumoriIchiranLogic.HIDDEN_MITUSMORI_NUMBER,
                    this.MitumoriIchiranLogic.HIDDEN_MITSUMORI_SHOSHIKI_KBN,
                    this.MitumoriIchiranLogic.HIDDEN_DETAIL_SYSTEM_ID);

                // ヘッダ設定
                this.MitumoriIchiranLogic.SetHeader(header_new);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);

                //初期化時全てチャックボックスが入れる
                this.radbtn_Subete.Checked = true;

                // Load値設定
                isLoaded = false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("MitumoriIchiranForm", ex);
                throw ex;
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

                // 画面情報の初期化
                if (isLoaded == false)
                {
                    this.MitumoriIchiranLogic.WindowInit();

                    this.customSearchHeader1.Visible = true;
                    this.customSearchHeader1.Location = new System.Drawing.Point(4, 91);
                    this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customSortHeader1.Location = new System.Drawing.Point(4, 113);
                    this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customDataGridView1.Location = new System.Drawing.Point(3, 140);
                    this.customDataGridView1.Size = new System.Drawing.Size(997, 290);

                    // Anchorの設定は必ずOnLoadで行うこと
                    if (this.customDataGridView1 != null)
                    {
                        int GRID_HEIGHT_MIN_VALUE = 250;
                        int GRID_WIDTH_MIN_VALUE = 900;
                        int h = this.Height - 192;
                        int w = this.Width;

                        if (h < GRID_HEIGHT_MIN_VALUE)
                        {
                            this.customDataGridView1.Height = GRID_HEIGHT_MIN_VALUE;
                        }
                        else
                        {
                            this.customDataGridView1.Height = h;
                        }
                        if (w < GRID_WIDTH_MIN_VALUE)
                        {
                            this.customDataGridView1.Width = GRID_WIDTH_MIN_VALUE;
                        }
                        else
                        {
                            this.customDataGridView1.Width = w;
                        }

                        if (this.customDataGridView1.Height <= GRID_HEIGHT_MIN_VALUE
                            || this.customDataGridView1.Width <= GRID_WIDTH_MIN_VALUE)
                        {
                            this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                        }
                        else
                        {
                            this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                        }

                        this.bt_ptn1.Top = this.customDataGridView1.Location.Y + this.customDataGridView1.Height + 8;
                        this.bt_ptn2.Top = this.customDataGridView1.Location.Y + this.customDataGridView1.Height + 8;
                        this.bt_ptn3.Top = this.customDataGridView1.Location.Y + this.customDataGridView1.Height + 8;
                        this.bt_ptn4.Top = this.customDataGridView1.Location.Y + this.customDataGridView1.Height + 8;
                        this.bt_ptn5.Top = this.customDataGridView1.Location.Y + this.customDataGridView1.Height + 8;
                    }
                }

                this.PatternReload(!this.isLoaded);

                // Load値設定
                isLoaded = true;

                // ソート条件の初期化
                this.customSortHeader1.ClearCustomSortSetting();

                // フィルタの初期化
                this.customSearchHeader1.ClearCustomSearchSetting();

                // DataGridView表示
                if (!this.DesignMode)
                {
                    this.logic.CreateDataGridView(this.Table);
                }

                //thongh 2015/10/16 #13526 start
                //読込データ件数の設定
                if (this.customDataGridView1 != null)
                {
                    this.header_new.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.header_new.ReadDataNumber.Text = "0";
                }
                //thongh 2015/10/16 #13526 end
                if (!isShown)
                {
                    this.Height -= 7;
                    isShown = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("OnLoad", ex);
                throw ex;
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

        // 状況ラジオボタン「全て」
        private void radbtn_Subete_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radbtn_Subete.Checked)
                {
                    this.txtNum_Jyoukyou.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("radbtn_Subete_CheckedChanged", ex);
                throw ex;
            }
        }

        // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 start
        //// 状況ラジオボタン「進行中」
        //private void radbtn_Sinkoutyuu_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (this.radbtn_Sinkoutyuu.Checked)
        //        {
        //            this.txtNum_Jyoukyou.Text = "1";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Fatal("radbtn_Sinkoutyuu_CheckedChanged", ex);
        //        throw ex;
        //    }
        //}
        // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 end

        // 状況ラジオボタン「受注」
        private void radbtn_Jyutyuu_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radbtn_Jyutyuu.Checked)
                {
                    // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 start
                    //this.txtNum_Jyoukyou.Text = "2";
                    this.txtNum_Jyoukyou.Text = "1";
                    // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 end
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("radbtn_Jyutyuu_CheckedChanged", ex);
                throw ex;
            }
        }

        // 状況ラジオボタン「失注」
        private void radbtn_Situtyuu_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radbtn_Situtyuu.Checked)
                {
                    // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 start
                    //this.txtNum_Jyoukyou.Text = "3";
                    this.txtNum_Jyoukyou.Text = "2";
                    // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 end
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("radbtn_Situtyuu_CheckedChanged", ex);
                throw ex;
            }
        }

        // 状況テクストボックス値変更
        private void txtNum_Jyoukyou_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // 状況値「0:全て」
                if ("0".Equals(this.txtNum_Jyoukyou.Text))
                {
                    this.radbtn_Subete.Checked = true;
                }

                // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 start
                //// 状況「1:進行中」
                //if ("1".Equals(this.txtNum_Jyoukyou.Text))
                //{
                //    this.radbtn_Sinkoutyuu.Checked = true;
                //}

                //// 状況「2:受注」
                //if ("2".Equals(this.txtNum_Jyoukyou.Text))
                //{
                //    this.radbtn_Jyutyuu.Checked = true;
                //}

                //// 状況「3:失注」
                //if ("3".Equals(this.txtNum_Jyoukyou.Text))
                //{
                //    this.radbtn_Situtyuu.Checked = true;
                //}

                //// 状況「null」
                //if (String.IsNullOrEmpty(this.txtNum_Jyoukyou.Text) == true)
                //{
                //    this.radbtn_Subete.Checked = true;
                //    this.txtNum_Jyoukyou.Text = "0";
                //}

                //// 状況「1」、「2」、「3」、「0」以外場合
                //if (!"1".Equals(this.txtNum_Jyoukyou.Text) && !"2".Equals(this.txtNum_Jyoukyou.Text) &&
                //    !"3".Equals(this.txtNum_Jyoukyou.Text) && !"0".Equals(this.txtNum_Jyoukyou.Text))
                //{
                //    MessageBoxUtility.MessageBoxShow("E042", "0～3");
                //}
                // 状況「1:受注」
                if ("1".Equals(this.txtNum_Jyoukyou.Text))
                {
                    this.radbtn_Jyutyuu.Checked = true;
                }

                // 状況「2:失注」
                if ("2".Equals(this.txtNum_Jyoukyou.Text))
                {
                    this.radbtn_Situtyuu.Checked = true;
                }

                //20151001 hoanghm #2315 状況CDがバックスペースやデリートでクリアできるようにする start
                //// 状況「null」
                //if (String.IsNullOrEmpty(this.txtNum_Jyoukyou.Text) == true)
                //{
                //    this.radbtn_Subete.Checked = true;
                //    this.txtNum_Jyoukyou.Text = "0";
                //}

                //// 状況「1」、「2」、「3」、「0」以外場合
                //if (!"1".Equals(this.txtNum_Jyoukyou.Text) && !"2".Equals(this.txtNum_Jyoukyou.Text) &&
                //    !"0".Equals(this.txtNum_Jyoukyou.Text))
                //{
                //    MessageBoxUtility.MessageBoxShow("E042", "0～2");
                //}
                //// 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 end

                if (String.IsNullOrEmpty(this.txtNum_Jyoukyou.Text) == true)
                {
                    this.radbtn_Subete.Checked = false;
                    this.radbtn_Jyutyuu.Checked = false;
                    this.radbtn_Situtyuu.Checked = false;
                }
                //20151001 hoanghm #2315 状況CDがバックスペースやデリートでクリアできるようにする end
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("txtNum_Jyoukyou_TextChanged", ex);
                throw ex;
            }
        }

        private void numTxtbox_TrhkskCD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.MitumoriIchiranLogic.TorihikisakiCheck(this.numTxtbox_TrhkskCD.Text);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("numTxtbox_TrhkskCD_Validating", ex);
                throw ex;
            }
        }

        public void GyousyaCD_PopupBefore()
        {
            strOldGyousyaCD = this.numTxtBox_GyousyaCD.Text;
            strOldHikiaiFlg = this.chkBox_Gyosya.Text;
            if (string.IsNullOrEmpty(strOldHikiaiFlg))
            {
                strOldHikiaiFlg = "0";
            }
        }

        public void GyoushaCD_PopupAfter()
        {
            if (strOldGyousyaCD != this.numTxtBox_GyousyaCD.Text || this.chkBox_Gyosya.Text != strOldHikiaiFlg)
            {
                this.ManualInputFlg = false;
                this.numTxtBox_GbCD.Text = string.Empty;
                this.chkBox_Gb.Text = "0";
                this.chkBox_Gb.Checked = false;
                this.txtBox_GbName.Text = string.Empty;
            }
        }

        private void numTxtBox_GyousyaCD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (ManualInputFlg)
                {
                    if (this.numTxtBox_GyousyaCD.Text != strOldGyousyaCD || this.chkBox_Gyosya.Text != strOldHikiaiFlg)
                    {
                        // 業者及び現場を空に設定
                        this.chkBox_Gyosya.Text = "0";
                        this.chkBox_Gyosya.Checked = false;
                        this.chkBox_Gb.Text = "0";
                        this.chkBox_Gb.Checked = false;
                    }
                }

                // 入力が変更された場合
                if (!this.ManualInputFlg &&
                    (strOldGyousyaCD != this.numTxtBox_GyousyaCD.Text || this.chkBox_Gyosya.Text != strOldHikiaiFlg))
                {
                    // 手入力フラグをTRUEに設定
                    this.ManualInputFlg = true;
                }

                if (this.numTxtBox_GyousyaCD.IsInputErrorOccured || strOldGyousyaCD != this.numTxtBox_GyousyaCD.Text || this.chkBox_Gyosya.Text != strOldHikiaiFlg)
                {
                    this.MitumoriIchiranLogic.GyousyaCheck(this.numTxtBox_GyousyaCD.Text, strOldGyousyaCD);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("numTxtBox_GyousyaCD_Validating", ex);
                throw ex;
            }
        }

        public void GenbaCD_PopupBefore()
        {
            this.strOldGenbaCD = this.numTxtBox_GbCD.Text;
            this.strOldGenbaHikiaiFlg = this.chkBox_Trhksk.Text;
            if (string.IsNullOrEmpty(strOldGenbaHikiaiFlg))
            {
                this.strOldGenbaHikiaiFlg = "0";
            }
        }

        public void GenbaCD_PopupAfter()
        {
            // 現場検索ポップアップから戻ってきた場合、業者CDには妥当なデータが設定されているはずなので前回値として保存する。
            this.numTxtBox_GyousyaCD_Enter(null, null);

            if ((strOldGyousyaCD != this.numTxtBox_GyousyaCD.Text || this.chkBox_Gyosya.Text != strOldHikiaiFlg)
                || (strOldGenbaCD != this.numTxtBox_GbCD.Text || this.chkBox_Gb.Text != strOldGenbaHikiaiFlg))
            {
                this.ManualInputFlg = false;
            }
        }

        private void numTxtBox_GbCD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!this.ManualInputFlg
                    && ((strOldGyousyaCD != this.numTxtBox_GyousyaCD.Text || this.chkBox_Gyosya.Text != strOldHikiaiFlg)
                        || (strOldGenbaCD != this.numTxtBox_GbCD.Text || this.chkBox_Gb.Text != strOldGenbaHikiaiFlg)
                    ))
                {
                    this.ManualInputFlg = true;
                }

                if (string.IsNullOrEmpty(this.numTxtBox_GyousyaCD.Text) && !string.IsNullOrEmpty(this.numTxtBox_GbCD.Text))
                {
                    MessageBoxUtility.MessageBoxShow("E051", "業者");
                    this.numTxtBox_GbCD.Text = string.Empty;
                    this.txtBox_GbName.Text = string.Empty;
                    this.numTxtBox_GbCD.Focus();

                    return;
                }
                else
                {
                    if (this.numTxtBox_GbCD.IsInputErrorOccured
                        || (strOldGyousyaCD != this.numTxtBox_GyousyaCD.Text || this.chkBox_Gyosya.Text != strOldHikiaiFlg)
                            || (strOldGenbaCD != this.numTxtBox_GbCD.Text || this.chkBox_Gb.Text != strOldGenbaHikiaiFlg))
                    {
                        this.MitumoriIchiranLogic.GenbaCheck(this.numTxtBox_GyousyaCD.Text, this.numTxtBox_GbCD.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("numTxtBox_GbCD_Validating", ex);
                throw ex;
            }
        }

        private void numTxtBox_GyousyaCD_Enter(object sender, EventArgs e)
        {
            try
            {
                strOldGyousyaCD = this.numTxtBox_GyousyaCD.Text;
                strOldHikiaiFlg = this.chkBox_Gyosya.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("numTxtBox_GyousyaCD_Enter", ex);
                throw ex;
            }
        }

        private void numTxtbox_TrhkskCD_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                this.chkBox_Trhksk.Checked = false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("numTxtbox_TrhkskCD_KeyUp", ex);
                throw ex;
            }
        }

        private void numTxtBox_GyousyaCD_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                this.chkBox_Gyosya.Checked = false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("numTxtBox_GyousyaCD_KeyUp", ex);
                throw ex;
            }
        }

        private void numTxtBox_GbCD_Enter(object sender, EventArgs e)
        {
            try
            {
                this.strOldGenbaCD = this.numTxtBox_GbCD.Text;
                this.strOldGenbaHikiaiFlg = this.chkBox_Gb.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("numTxtBox_GbCD_Enter", ex);
                throw ex;
            }
        }

        private void numTxtBox_GbCD_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                this.chkBox_Gb.Checked = false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("numTxtBox_GbCD_KeyUp", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 業者ポップアップ後に動くメソッド
        /// </summary>
        public void GyoushaPopUpAfter()
        {
            // ポップアップ入力なので手入力フラグをFalseに設定
            this.ManualInputFlg = false;
        }

        #endregion

        #region 営業担当者更新後イベント
        /// <summary>
        /// 営業担当者更新後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void txtBox_Eigyotantosya_OnValidated(object sender, EventArgs e)
        {
            this.MitumoriIchiranLogic.CheckEigyoTantousha();
        }
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
                LogUtility.Fatal("MoveToSort", ex);
                throw ex;
            }
        }

        #endregion

        #region 検索結果表示

        public virtual void ShowData()
        {
            try
            {
                if (!this.DesignMode)
                {
                    this.logic.AlertCount = this.MitumoriIchiranLogic.alertCount;
                    this.logic.CreateDataGridView(this.Table);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("ShowData", ex);
                throw ex;
            }
        }

        #endregion
    }
}