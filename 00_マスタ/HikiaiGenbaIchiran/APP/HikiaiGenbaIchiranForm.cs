// $Id: HikiaiGenbaIchiranForm.cs 3960 2013-10-17 10:22:23Z sys_dev_27 $
using System;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using HikiaiGenbaIchiran.Logic;

namespace HikiaiGenbaIchiran.APP
{
    public partial class HikiaiGenbaIchiranForm : IchiranSuperForm
    {
        /// <summary>
        /// 結合設定ファイルの場所を指定
        /// </summary>
        private readonly string JOIN_XML_PATH = "HikiaiGenbaIchiran.Setting.JoinSetting.xml";

        /// <summary>
        /// 検索設定ファイルの場所を指定
        /// </summary>
        private readonly string PATTERN_XML_PATH = "HikiaiGenbaIchiran.Setting.PatternSetting.xml";

        /// <summary>
        /// ビジネスロジック
        /// </summary>
        private LogicClass businessLogic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn"></param>
        public HikiaiGenbaIchiranForm(DENSHU_KBN denshuKbn)
            : base(denshuKbn)
        {
            InitializeComponent();
            this.customDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.logic.JoinXmlPath = JOIN_XML_PATH;
            this.logic.PatternXmlPath = PATTERN_XML_PATH;
            this.logic.SettingAssembly = Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                // ビジネスロジックの初期化
                this.businessLogic = new LogicClass(this);
                this.businessLogic.WindowInit();

                // 一覧の初期化
                this.logic.CreateDataGridView(this.Table);
                ///this.customSortHeader1.CreateSortColumns();
            }
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
                this.logic.CreateDataGridView(this.Table);
            }
        }
    }
}

