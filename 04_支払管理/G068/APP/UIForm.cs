using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Seasar.Quill;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Dto;
using DataGridViewCheckBoxColumnHeader;

namespace Shougun.Core.SalesManagement.Shiharaikakuteinyuryoku
{
    public partial class UIForm : IchiranSuperForm
    {
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logicShiharaikakuteinyuryoku;

        /// <summary>
        /// 起動状態
        /// </summary>
        private Boolean isLoaded = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <param name="header">ヘッダオブジェクト</param>
        public UIForm(DENSHU_KBN denshuKbn, UIHeader header)
            : base(denshuKbn, false)
        {
            this.InitializeComponent();

            // 社員コード
            this.ShainCd = SystemProperty.Shain.CD;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logicShiharaikakuteinyuryoku = new LogicClass(this);

            // ロジックにヘッダをセット
            logicShiharaikakuteinyuryoku.SetHeader(header);

            // グリッドのタブインデックスをセット（デザインで設定できないため）
            customDataGridView1.TabIndex = 9;

            isLoaded = false;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #region 初期処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 画面情報の初期化
            if (isLoaded == false)
            {
                if (!this.logicShiharaikakuteinyuryoku.WindowInit())
                {
                    return;
                }
            }

            isLoaded = true;
            this.setLogicSelect();

            //if (isLoaded == true)
            //{
            //    this.logicShiharaikakuteinyuryoku.Search();
            //}
            
            //this.Table = this.logicShiharaikakuteinyuryoku.searchResult;

            if (!this.DesignMode)
            {
                // 確定区分が存在しない場合
                if (!this.customDataGridView1.Columns.Contains("確定区分") && (this.Table != null))
                {
                    //確定区分をグリッドに表示
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.Name = "確定区分";
                    DataGridviewCheckboxHeaderCell newheader = new DataGridviewCheckboxHeaderCell();
                    newheader.OnCheckBoxClicked +=
                            new DataGridViewCheckBoxColumnHeader.DataGridviewCheckboxHeaderCell.datagridviewcheckboxHeaderEventHander(
                                    this.logicShiharaikakuteinyuryoku.ch_OnCheckBoxClicked);
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
        #endregion 初期処理

        #region メソッド

        /// <summary>
        /// IchiranSuperFormで取得されたパターン一覧のSelectQeuryをセット
        /// </summary>
        public void setLogicSelect()
        {
            this.logicShiharaikakuteinyuryoku.selectQuery = this.logic.SelectQeury;
            this.logicShiharaikakuteinyuryoku.orderByQuery = this.logic.OrderByQuery;
        }

        /// <summary>
        /// 検索結果表示
        /// </summary>
        public virtual void ShowData()
        {
            this.Table = this.logicShiharaikakuteinyuryoku.searchResult;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
            }
        }

        #endregion

    }
}
