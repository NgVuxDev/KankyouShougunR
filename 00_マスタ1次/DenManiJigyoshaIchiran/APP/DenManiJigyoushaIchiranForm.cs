// $Id$
using System;
using System.Reflection;
using System.Windows.Forms;
using DenManiJigyoushaIchiran.Logic;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;

namespace DenManiJigyoushaIchiran.APP
{
    public partial class DenManiJigyoushaIchiranForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
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
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn"></param>
        public DenManiJigyoushaIchiranForm(DENSHU_KBN denshuKbn)
            : base(denshuKbn)
        {
            InitializeComponent();
            this.logic.SettingAssembly = Assembly.GetExecutingAssembly();
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

                // 非表示にする列名を登録
                this.SetHiddenColumns(this.businessLogic.KEY_ID1);

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
            LogUtility.DebugMethodStart();

            try
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
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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
    }
}

