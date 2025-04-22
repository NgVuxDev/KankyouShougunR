// $Id$
using System;
using System.Reflection;
using System.Windows.Forms;
using KobetsuHinmeiTankaIchiran.Logic;
using KobetsuHinmeiTankaIchiran.Const;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.IchiranCommon.APP;
using System.ComponentModel;

namespace KobetsuHinmeiTankaIchiran.APP
{
    public partial class KobetsuHinmeiTankaIchiranForm : IchiranSuperForm
    {
        /// <summary>
        /// ビジネスロジック
        /// </summary>
        private LogicClass businessLogic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// イベントフラグ
        /// </summary>
        internal bool EventSetFlg = false;

        /// <summary>
        /// 前回業者コード
        /// </summary>
        public string beforGyoushaCD = string.Empty;

        /// <summary>
        /// 前回荷降業者コード
        /// </summary>
        public string beforNioroshiGyoushaCD = string.Empty;

        private string preNioroshiGyoushaCd { get; set; }
        private string curNioroshiGyoushaCd { get; set; }

        private string preGyoushaCd { get; set; }
        private string curGyoushaCd { get; set; }

        public string dennpyouKbn { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn"></param>
        public KobetsuHinmeiTankaIchiranForm(DENSHU_KBN denshuKbn)
            : base(denshuKbn, false)
        {
            InitializeComponent();
            this.logic.SettingAssembly = Assembly.GetExecutingAssembly();

            // 社員CDを取得すること
            this.ShainCd = SystemProperty.Shain.CD;
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // デフォルトパターンを設定する
            base.OnLoad(e);

            // 初回のみ
            if (this.businessLogic == null)
            {
                // ビジネスロジックの初期化
                this.businessLogic = new LogicClass(this);

                // 画面初期化
                bool catchErr = this.businessLogic.WindowInit();
                if (catchErr)
                {
                    return;
                }

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 155);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(4, 177);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 204);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 250);

                // 汎用検索機能が未実装の為、汎用検索は非表示
                this.searchString.Visible = false;

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }

            this.PatternReload();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            // パターンヘッダのみ表示
            this.ShowData();
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowData()
        {
            if (!this.DesignMode)
            {
                DialogResult dlgResult = System.Windows.Forms.DialogResult.Yes;

                // アラート件数を設定
                this.logic.AlertCount = this.businessLogic.GetAlertCount();

                if (dlgResult == DialogResult.Yes && this.Table != null && this.PatternNo != 0)
                {
                    // 明細に表示
                    this.logic.CreateDataGridView(this.Table);

                    // 検索件数を設定し、画面に表示
                    var parentForm = base.Parent;
                    var readDataNumber = (TextBox)controlUtil.FindControl(parentForm, "ReadDataNumber");
                    if (readDataNumber != null)
                    {
                        readDataNumber.Text = this.Table.Rows.Count.ToString();
                    }

                    this.HideKeyColumns();
                }
            }
        }

        /// <summary>
        /// 画面連携に使用するキー取得項目を隠す
        /// </summary>
        private void HideKeyColumns()
        {
            if (this.customDataGridView1.DataSource != null && this.Table != null)
            {
                foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
                {
                    if (col.Name == KobetsuHinmeiTankaIchiranConstans.KEY_ID1 ||
                        col.Name == KobetsuHinmeiTankaIchiranConstans.KEY_ID2 ||
                        col.Name == KobetsuHinmeiTankaIchiranConstans.KEY_ID3 ||
                        col.Name == KobetsuHinmeiTankaIchiranConstans.KEY_ID4)
                    {
                        col.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// 一覧表示条件チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ICHIRAN_HYOUJI_JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = (CheckBox)sender;
            if (!item.Checked)
            {
                if (!this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "表示条件");
                    item.Checked = true;
                }
            }
        }

        /// <summary>
        /// 画面クローズ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            try
            {
                // 表示条件保存
                this.businessLogic.SaveHyoujiJoukenDefault();
            }
            catch (Exception ex)
            {
                // 画面が閉じれなくなるのでログのみ
                LogUtility.Fatal("OnClosing", ex);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 業者が入力されてない場合
            if (String.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                // 関連項目クリア
                this.GYOUSHA_CD.Text = string.Empty;
                this.GYOUSHA_RNAME.Text = String.Empty;
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_RNAME.Text = String.Empty;
                this.beforGyoushaCD = string.Empty;
            }
            else if (this.beforGyoushaCD != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_RNAME.Text = String.Empty;
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }
        }

        /// <summary>
        /// 現場一覧のShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KobetsuHinmeiTankaIchiranForm_Shown(object sender, EventArgs e)
        {
            // 初期フォーカス位置を設定します
            this.DenpyouKubun.Focus();
        }

        #region 20150617 #2182 hoanghm テンポラリー変数に業者CDをセットする

        /// <summary>
        /// 現場CDValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //beforGyoushaCDは値がない場合に値をセットします。
            if (string.IsNullOrEmpty(this.beforGyoushaCD))
            {
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }

            if (string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_RNAME.Text = string.Empty;
                return;
            }

            var msgLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                msgLogic.MessageBoxShow("E051", "業者");
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_CD.Focus();
                return;
            }

            this.businessLogic.CheckGenba();
        }

        public void AfterPopupGenba()
        {
            if (string.IsNullOrEmpty(this.beforGyoushaCD))
            {
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }
        }

        #endregion

        /// <summary>
        /// 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            preGyoushaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者 PopupAfterExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            curGyoushaCd = this.GYOUSHA_CD.Text;
            if (preGyoushaCd != curGyoushaCd)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_RNAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 業者 NioroshiGyoushaBtnPopupBeforeMethod
        /// </summary>
        public void NioroshiGyoushaBtnPopupBeforeMethod()
        {
            preNioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者 NioroshiGyoushaBtnPopupMethod
        /// </summary>
        public void NioroshiGyoushaBtnPopupMethod()
        {
            curNioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
            if (preNioroshiGyoushaCd != curNioroshiGyoushaCd)
            {
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 品名名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HINMEI_CD_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.HINMEI_CD.Text))
            {
                this.HINMEI_NAME.Text = string.Empty;
            }

            // 品名名称の取得
            this.businessLogic.SearchHinmeiName(e);
        }

        /// <summary>
        ///　運搬名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                this.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            }

            //  運搬名称の取得
            this.businessLogic.SearchUnpanGyoushaName(e);
        }

        /// <summary>
        /// 荷降先業者名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text))
            {
                this.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
            }
            else if (this.beforNioroshiGyoushaCD != this.NIOROSHI_GYOUSHA_CD.Text)
            {
                this.NIOROSHI_GENBA_CD.Text = String.Empty;
                this.NIOROSHI_GENBA_NAME.Text = String.Empty;
                this.beforNioroshiGyoushaCD = this.NIOROSHI_GYOUSHA_CD.Text;
            }

            // 荷降先名称の取得
            this.businessLogic.SearchNioroshiGyoushaName(e);
        }

        /// <summary>
        /// 荷降先現場名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.NIOROSHI_GENBA_CD.Text))
            {
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
            }

            // 荷降先現場名称の取得
            this.businessLogic.SearchNioroshiGenbaName(e);
        }

        private void TorihikisakiKbn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.TorihikisakiKbn.Checked)
            {
                this.TORIHIKISAKI_CD.Enabled = false;
                this.TORIHIKISAKI_CD.Text = string.Empty;
                this.TORIHIKISAKI_RNAME.Text = string.Empty;
                this.TORIHIKISAKI_SEARCH_BUTTON.Enabled = false;
            }
            else
            {
                this.TORIHIKISAKI_CD.Enabled = true;
                this.TORIHIKISAKI_SEARCH_BUTTON.Enabled = true;
            }
        }

        private void GyoushaKbn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.GyoushaKbn.Checked)
            {
                this.GYOUSHA_CD.Enabled = false;
                this.GENBA_CD.Enabled = false;
                this.GYOUSHA_CD.Text = string.Empty;
                this.GYOUSHA_RNAME.Text = string.Empty;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_RNAME.Text = string.Empty;
                this.GenbaKbn.Enabled = false;
                this.GenbaKbn.Checked = true;
                this.GYOUSHA_SEARCH_BUTTON.Enabled = false;
                this.GENBA_SEARCH_BUTTON.Enabled = false;
            }
            else
            {
                this.GenbaKbn.Enabled = true;
                this.GenbaKbn.Checked = false;
                this.GENBA_CD.Enabled = true;
                this.GYOUSHA_CD.Enabled = true;
                this.GYOUSHA_SEARCH_BUTTON.Enabled = true;
                this.GENBA_SEARCH_BUTTON.Enabled = true;
            }
        }

        private void GenbaKbn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.GenbaKbn.Checked)
            {
                this.GENBA_CD.Enabled = false;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_RNAME.Text = string.Empty;
                this.GENBA_SEARCH_BUTTON.Enabled = false;
            }
            else
            {
                this.GENBA_CD.Enabled = true;
                this.GENBA_SEARCH_BUTTON.Enabled = true;
            }
        }

        private void UnpanGyoushaKbn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.UnpanGyoushaKbn.Checked)
            {
                this.UNPAN_GYOUSHA_CD.Enabled = false;
                this.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                this.UNPAN_GYOUSHA_SEARCH_BUTTON.Enabled = false;
            }
            else
            {
                this.UNPAN_GYOUSHA_CD.Enabled = true;
                this.UNPAN_GYOUSHA_SEARCH_BUTTON.Enabled = true;
            }
        }

        private void NioroshiGyoushaKbn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NioroshiGyoushaKbn.Checked)
            {
                this.NIOROSHI_GYOUSHA_CD.Enabled = false;
                this.NIOROSHI_GENBA_CD.Enabled = false;
                this.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                this.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.NioroshiGenbaKbn.Enabled = false;
                this.NioroshiGenbaKbn.Checked = true;
                this.NIOROSHI_GYOUSHA_SEARCH_BUTTON.Enabled = false;
                this.NIOROSHI_GENBA_SEARCH_BUTTON.Enabled = false;
            }
            else
            {
                this.NIOROSHI_GYOUSHA_CD.Enabled = true;
                this.NioroshiGenbaKbn.Enabled = true;
                this.NioroshiGenbaKbn.Checked = false;
                this.NIOROSHI_GENBA_CD.Enabled = true;
                this.NIOROSHI_GYOUSHA_SEARCH_BUTTON.Enabled = true;
                this.NIOROSHI_GENBA_SEARCH_BUTTON.Enabled = true;
            }
        }

        private void NioroshiGenbaKbn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NioroshiGenbaKbn.Checked)
            {
                this.NIOROSHI_GENBA_CD.Enabled = false;
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.NIOROSHI_GENBA_SEARCH_BUTTON.Enabled = false;
            }
            else
            {
                this.NIOROSHI_GENBA_CD.Enabled = true;
                this.NIOROSHI_GENBA_SEARCH_BUTTON.Enabled = true;
            }
        }
    }
}

