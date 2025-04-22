// $Id$
using System;
using System.Reflection;
using System.Windows.Forms;
using GyoushaIchiran.Const;
using GyoushaIchiran.Logic;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Dto;
using r_framework.Utility;
using Shougun.Core.Common.IchiranCommon.APP;

namespace GyoushaIchiran.APP
{
    public partial class GyoushaIchiranForm : IchiranSuperForm
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
        /// 前回取引先コード
        /// </summary>
        public string beforTorihikisakiCD = string.Empty;

        /// <summary>
        /// 前回業コード
        /// </summary>
        public string beforGyoushaCD = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        public GyoushaIchiranForm(DENSHU_KBN denshuKbn)
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
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 135);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(4, 157);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 184);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 270);

                // 汎用検索機能が未実装の為、汎用検索は非表示
                this.searchString.Visible = false;

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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
                    this.notReadOnlyColumns();
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
                    if (col.Name == GyoushaIchiranConstans.KEY_ID1 ||
                        // 以下、mapbox連携で利用する項目
                        col.Name == GyoushaIchiranConstans.HIDDEN_GYOUSHA_LATITUDE ||
                        col.Name == GyoushaIchiranConstans.HIDDEN_GYOUSHA_LONGITUDE ||
                        col.Name == GyoushaIchiranConstans.HIDDEN_GYOUSHA_NAME_RYAKU ||
                        col.Name == GyoushaIchiranConstans.HIDDEN_GYOUSHA_ADDRESS1 ||
                        col.Name == GyoushaIchiranConstans.HIDDEN_GYOUSHA_ADDRESS2 ||
                        col.Name == GyoushaIchiranConstans.HIDDEN_GYOUSHA_POST ||
                        col.Name == GyoushaIchiranConstans.HIDDEN_GYOUSHA_TEL ||
                        col.Name == GyoushaIchiranConstans.HIDDEN_BIKOU1 ||
                        col.Name == GyoushaIchiranConstans.HIDDEN_BIKOU2 ||
                        col.Name == GyoushaIchiranConstans.HIDDEN_TODOUFUKEN_NAME)
                    {
                        col.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// 地図表示のチェックボックスを使用可能にする
        /// </summary>
        private void notReadOnlyColumns()
        {
            if (this.customDataGridView1.DataSource != null && this.Table != null)
            {
                foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
                {
                    // 現状「地図表示」のチェックのみ
                    if (col.Name == GyoushaIchiranConstans.DATA_TAISHO)
                    {
                        col.ReadOnly = false;
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

        /// <summary>
        /// 業者一覧のShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GyoushaIchiranForm_Shown(object sender, EventArgs e)
        {
            // 初期フォーカス位置を設定します
            this.GYOUSHA_CD.Focus();
        }

        ///<summary>
        ///取引先CDの削除済みチェック
        ///</summary>
        public void TORIHIKISAKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.TORIHIKISAKI_CD.Text))
            {
                this.TORIHIKISAKI_CD.Text = string.Empty;
                this.TORIHIKISAKI_RNAME.Text = string.Empty;
                this.beforTorihikisakiCD = string.Empty;
            }
            if (this.beforTorihikisakiCD != this.TORIHIKISAKI_CD.Text)
            {
                this.beforTorihikisakiCD = this.TORIHIKISAKI_CD.Text;
                var cd = this.TORIHIKISAKI_CD.Text;
                var checkResult = this.businessLogic.checkDelTorihikisaki(cd);
                if (checkResult == DialogResult.Yes)
                {
                    e.Cancel = false;
                    return;
                }
                else if (checkResult == DialogResult.No)
                {
                    e.Cancel = true;
                    this.TORIHIKISAKI_CD.Text = string.Empty;
                    this.TORIHIKISAKI_RNAME.Text = string.Empty;
                    this.beforTorihikisakiCD = string.Empty;
                }
            }
        }

        ///<summary>
        ///業者CDの削除済みチェック
        ///</summary>
        public void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_CD.Text = string.Empty;
                this.GYOUSHA_RNAME.Text = string.Empty;
                this.beforGyoushaCD = string.Empty;
            }
            if (this.beforGyoushaCD != this.GYOUSHA_CD.Text)
            {
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
                var cd = this.GYOUSHA_CD.Text;
                var checkResult = this.businessLogic.checkDelGyousha(cd);
                if (checkResult == DialogResult.Yes)
                {
                    e.Cancel = false;
                    return;
                }
                else if (checkResult == DialogResult.No)
                {
                    e.Cancel = true;
                    this.GYOUSHA_CD.Text = string.Empty;
                    this.GYOUSHA_RNAME.Text = string.Empty;
                    this.beforGyoushaCD = string.Empty;
                }
            }
        }
    }
}

