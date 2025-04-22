// $Id$
using System;
using System.Reflection;
using System.Windows.Forms;
using NyuukinsakiIchiran.Const;
using NyuukinsakiIchiran.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using Shougun.Core.Message;
using r_framework.Dto;
using r_framework.Utility;
using r_framework.Logic;

namespace NyuukinsakiIchiran.APP
{
    public partial class NyuukinsakiIchiranForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        /// <summary>
        /// ビジネスロジック
        /// </summary>
        private LogicClass businessLogic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// イベントフラグ
        /// </summary>
        internal bool IsLoaded = false;

        /// <summary>
        /// ヘッダーオブジェクト
        /// </summary>
        internal HeaderForm headerForm;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn"></param>
        public NyuukinsakiIchiranForm(DENSHU_KBN denshuKbn)
            : base(denshuKbn)
        {
            InitializeComponent();
            this.logic.SettingAssembly = Assembly.GetExecutingAssembly();

            // 社員CDを取得すること
            this.ShainCd = SystemProperty.Shain.CD;

            // 伝種区分
            this.DenshuKbn = denshuKbn;
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.IsLoaded)
            {
                // ヘッダフォームを取得
                this.headerForm = (HeaderForm)((BusinessBaseForm)this.ParentForm).headerForm;

                // ビジネスロジックの初期化
                this.businessLogic = new LogicClass(this);
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

                // 非表示項目の登録
                this.SetHiddenColumns(this.businessLogic.KEY_ID1, this.businessLogic.KEY_ID2);

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }

            this.PatternReload(!this.IsLoaded);

            this.IsLoaded = true;

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
            }

            //thongh 2015/10/16 #13526 start
            //読込データ件数の設定
            if (this.customDataGridView1 != null)
            {
                this.headerForm.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headerForm.ReadDataNumber.Text = "0";
            }
            //thongh 2015/10/16 #13526 end
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
                var rowCount = 0;

                // 表示件数を取得
                if (this.Table != null)
                {
                    rowCount = this.Table.Rows.Count;
                }

                // アラート件数を設定し、検索実行
                this.logic.AlertCount = this.businessLogic.GetAlertCount();

                if (dlgResult == DialogResult.Yes && this.Table != null && this.PatternNo != 0)
                {
                    // 明細に表示
                    this.logic.CreateDataGridView(this.Table);

                    // 検索件数を設定し、画面に表示
                    this.headerForm.ReadDataNumber.Text = rowCount.ToString();

                    // ソート用ヘッダーの表示・非表示
                    if (this.customSortHeader1 != null)
                    {
                        if (this.Table != null)
                        {
                            this.customSortHeader1.Visible = true;
                        }
                        else
                        {
                            this.customSortHeader1.Visible = false;
                        }
                    }
                }
            }
        }

    }
}

