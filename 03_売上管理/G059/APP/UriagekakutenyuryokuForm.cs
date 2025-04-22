using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Dto;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using DataGridViewCheckBoxColumnHeeader;

namespace Shougun.Core.SalesPayment.Uriagekakutenyuryoku
{
    [Implementation]
    public partial class UriagekakutenyuryokuForm : IchiranSuperForm
    {
        #region フィールド

        private Uriagekakutenyuryoku.LogicClass UriagekakutenyuryokuLogic;

        private string selectQuery = string.Empty;

        private string orderQuery = string.Empty;

        HeaderForm header_new;

        private Boolean isLoaded;

        #endregion

        /// <summary>
        /// 画面ロジック
        /// </summary>
        //private r_framework.Logic.IBuisinessLogic logic;

        public UriagekakutenyuryokuForm(DENSHU_KBN denshuKbn, HeaderForm headerFor)
            : base(denshuKbn,false)
        {
            this.InitializeComponent();

            this.header_new = headerFor;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.UriagekakutenyuryokuLogic = new LogicClass(this);

            // ヘッダセット
            UriagekakutenyuryokuLogic.SetHeader(header_new);

            // グリッドのタブインデックスをセット（デザインで設定できないため）
            customDataGridView1.TabIndex = 9;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;

            //初期化時全てチャックボックスが入れる
            //this.txtNum_HidukeSentaku.Text = "1";
            this.radbtn_Subete.Checked = true;
            isLoaded = false;
        }

        #region 画面コントロールイベント
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 画面情報の初期化
            if (isLoaded == false)
            {
                if (!this.UriagekakutenyuryokuLogic.WindowInit())
                {
                    return;
                }
            }

            isLoaded = true;

            this.UriagekakutenyuryokuLogic.selectQuery = this.logic.SelectQeury;
            this.UriagekakutenyuryokuLogic.orderByQuery = this.logic.OrderByQuery;

            //if (isLoaded == true)
            //{
            //    this.UriagekakutenyuryokuLogic.Search();
            //}
            
            ////検索処理
            //int count = NyuuSyutuKinLogic.Search();

            //this.Table = this.UriagekakutenyuryokuLogic.searchResult;

            if (!this.DesignMode)
            {
                // 確定区分が存在しない場合
                if (!this.customDataGridView1.Columns.Contains("確定区分") && (this.Table != null))
                {
                    //確定区分をグリッドに表示
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.Name = "確定区分";
                    DataGridviewCheckboxHeaderCell newheader = new DataGridviewCheckboxHeaderCell();
                    newheader.OnCheckBoxClicked += new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.datagridviewcheckboxHeaderEventHander(this.UriagekakutenyuryokuLogic.ch_OnCheckBoxClicked);
                    newColumn.HeaderCell = newheader;
                    this.customDataGridView1.Columns.Insert(0, newColumn);
                }

                if (this.Table != null)
                {
                    //分類、システムID、枝番のカラムをテーブルに追加
                    this.Table.Columns.Add("分類", Type.GetType("System.String"));
                    this.Table.Columns.Add("システムID", Type.GetType("System.Int64"));
                    this.Table.Columns.Add("枝番", Type.GetType("System.Int32"));
                    this.Table.Columns.Add("明細・システムID", Type.GetType("System.Int64"));
                }

                this.logic.CreateDataGridView(this.Table);
            }
            
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
            this.customSortHeader1.Focus();
        }

        #endregion

        #region 検索結果表示

        public virtual void ShowData()
        {
            this.Table = this.UriagekakutenyuryokuLogic.searchResult;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
            }
        }

        #endregion

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

    }
}
