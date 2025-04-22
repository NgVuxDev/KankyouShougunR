using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using Shougun.Core.Common.IchiranCommon.Logic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using r_framework.CustomControl.DataGridCustomControl;
using System.ComponentModel;
using r_framework.Dto;

namespace Shougun.Core.Common.IchiranCommon.APP
{
    public partial class IchiranSuperForm : SuperForm
    {
        /// <summary>
        /// 社員コード
        /// </summary>
        public string ShainCd { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public DENSHU_KBN DenshuKbn { get; set; }

        /// <summary>
        /// シーケンス番号
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 実施パターン番号
        /// </summary>
        public int PatternNo { get; set; }

        /// <summary>
        /// パターンを選択しているかどうか
        /// </summary>
        public bool IsPatternSelected
        {
            get
            {
                return this.PatternNo <= 0;
            }
        }

        /// <summary>
        /// 選択しているパターンの出力区分
        /// </summary>
        public int CurrentPatternOutputKbn
        {
            get
            {
                if (this.logic.currentPatternDto != null &&
                    !this.logic.currentPatternDto.OutputPattern.OUTPUT_KBN.IsNull)
                {
                    return (int)this.logic.currentPatternDto.OutputPattern.OUTPUT_KBN;
                }
                else
                {
                    return (int)Const.OUTPUT_KBN.NONE;
                }
            }
        }

        /// <summary>
        /// 検索条件設定文字列（ＳＱＬ文を想定）
        /// </summary>
        public string SerachSetting { get; set; }

        /// <summary>
        /// 単純系の検索条件文字列
        /// </summary>
        public string SimpleSearchSettings { get; set; }

        /// <summary>
        /// 一覧のロジック処理
        /// </summary>
        public IchiranBaseLogic logic { get; protected set; }

        /// <summary>
        /// 表示用データ格納変数
        /// </summary>
        public DataTable Table { get; set; }

        /// <summary>
        /// パターンのロードをSuperFormで行うかどうか
        /// </summary>
        public bool IsDataLoad { get; private set; }

        /// <summary>
        /// SELECT句を保持するプロパティ
        /// </summary>
        public string SelectQuery
        {
            get { return this.logic.SelectQeury; }
        }

        /// <summary>
        /// ORDER BY句を保持するプロパティ
        /// </summary>
        public string OrderByQuery
        {
            get { return this.logic.OrderByQuery; }
        }

        /// <summary>
        /// JOIN句を保持するプロパティ
        /// </summary>
        public string JoinQuery
        {
            get { return this.logic.JoinQuery; }
        }

        /// <summary>
        /// 最初のLoadイベントかどうか
        /// </summary>
        private bool isFirstLoad = true;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public IchiranSuperForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// パターンはSuperForm側で読み込みません。
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        public IchiranSuperForm(DENSHU_KBN denshuKbn)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.DenshuKbn = denshuKbn;
                this.PatternNo = 0;
                this.logic = new IchiranBaseLogic(this);
                this.IsDataLoad = false;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <param name="isDataload">パターンの読み込みをSuperForm側で行うかどうかを表す値</param>
        public IchiranSuperForm(DENSHU_KBN denshuKbn, bool isDataload)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.DenshuKbn = denshuKbn;
                this.PatternNo = 0;
                this.logic = new IchiranBaseLogic(this);
                this.IsDataLoad = isDataload;
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(System.EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            this.customDataGridView1.IsBrowsePurpose = true;


            if (isFirstLoad)
            {
                base.OnLoad(e);
                isFirstLoad = false;

                var parentForm = base.Parent;
                var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");
                
                // タイトルはフォームヘッダーの画面タイトル文字列と同じであること
                titleControl.Text = this.DenshuKbn.ToTitleString();
                parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(this.DenshuKbn.ToTitleString());

                var readDataNumber = (TextBox)controlUtil.FindControl(parentForm, "ReadDataNumber");
                if (Table != null && readDataNumber != null)
                {
                    readDataNumber.Text = Table.Rows.Count.ToString();
                }
            }

            if (this.IsDataLoad)
            {
                // パターンロード
                this.PatternReload();
            }
        }

        /// <summary>
        /// パターンボタン押下時処理
        /// </summary>
        /// <param name="sender">イベント対象オブジェクト</param>
        /// <param name="e">イベントクラス</param>
        private void PatternButtonClick(object sender, System.EventArgs e)
        {
            var button = (Button)sender;

            if (!string.IsNullOrEmpty(button.Text))
            {
                this.PatternNo = int.Parse(button.Name.Replace("bt_ptn", ""));
            }
            else
            {
                this.PatternNo = 0;
            }
            this.ChangeBackGroundButton(button);    //Thang Nguyen Add 20150714 #11434
            this.PatternReload();
            this.OnLoad(e);
        }

        //Thang Nguyen Add 20150714 #11434 Start
        private void ChangeBackGroundButton(Button button)
        {
            this.ChangeColorButton(button);
            switch (button.Name)
            {
                case "bt_ptn1":
                    this.bt_ptn2.ForeColor = Color.Black;
                    this.bt_ptn2.BackgroundImage = null;
                    this.bt_ptn3.ForeColor = Color.Black;
                    this.bt_ptn3.BackgroundImage = null;
                    this.bt_ptn4.ForeColor = Color.Black;
                    this.bt_ptn4.BackgroundImage = null;
                    this.bt_ptn5.ForeColor = Color.Black;
                    this.bt_ptn5.BackgroundImage = null;
                    break;

                case "bt_ptn2":
                    this.bt_ptn1.ForeColor = Color.Black;
                    this.bt_ptn1.BackgroundImage = null;
                    this.bt_ptn3.ForeColor = Color.Black;
                    this.bt_ptn3.BackgroundImage = null;
                    this.bt_ptn4.ForeColor = Color.Black;
                    this.bt_ptn4.BackgroundImage = null;
                    this.bt_ptn5.ForeColor = Color.Black;
                    this.bt_ptn5.BackgroundImage = null;
                    break;

                case "bt_ptn3":
                    this.bt_ptn1.ForeColor = Color.Black;
                    this.bt_ptn1.BackgroundImage = null;
                    this.bt_ptn2.ForeColor = Color.Black;
                    this.bt_ptn2.BackgroundImage = null;
                    this.bt_ptn4.ForeColor = Color.Black;
                    this.bt_ptn4.BackgroundImage = null;
                    this.bt_ptn5.ForeColor = Color.Black;
                    this.bt_ptn5.BackgroundImage = null;
                    break;

                case "bt_ptn4":
                    this.bt_ptn1.ForeColor = Color.Black;
                    this.bt_ptn1.BackgroundImage = null;
                    this.bt_ptn2.ForeColor = Color.Black;
                    this.bt_ptn2.BackgroundImage = null;
                    this.bt_ptn3.ForeColor = Color.Black;
                    this.bt_ptn3.BackgroundImage = null;
                    this.bt_ptn5.ForeColor = Color.Black;
                    this.bt_ptn5.BackgroundImage = null;
                    break;

                case "bt_ptn5":
                    this.bt_ptn1.ForeColor = Color.Black;
                    this.bt_ptn1.BackgroundImage = null;
                    this.bt_ptn2.ForeColor = Color.Black;
                    this.bt_ptn2.BackgroundImage = null;
                    this.bt_ptn3.ForeColor = Color.Black;
                    this.bt_ptn3.BackgroundImage = null;
                    this.bt_ptn4.ForeColor = Color.Black;
                    this.bt_ptn4.BackgroundImage = null;
                    break;
            }
        
        }

        private void ChangeColorButton(Button button1)
        {
            button1.ForeColor = Color.White;

            Bitmap bmp = new Bitmap(button1.Width, button1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Rectangle gfRec = new Rectangle(0, 0, bmp.Width, bmp.Height);
                using (LinearGradientBrush br = new LinearGradientBrush(gfRec, Color.FromArgb(229, 224, 252), Color.FromArgb(109, 182, 221), LinearGradientMode.Vertical))
                {
                    g.FillRectangle(br, gfRec);
                }
            }

            button1.BackgroundImage = bmp;
            button1.ForeColor = Color.Black;
        }
        //Thang Nguyen Add 20150714 #11434 End

        /// <summary>
        /// 表出イベント（初回のみ）
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            if (this.customDataGridView1 != null)
            {
                // デザイナでの設定が初期化されるのでここで再設定
                this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            base.OnShown(e);

            var alertNo = (TextBox)controlUtil.FindControl(base.Parent, "alertNumber");
            if (alertNo != null)
            {
                this.logic.AlertCount = string.IsNullOrEmpty(alertNo.Text) ? 0 : int.Parse(alertNo.Text, NumberStyles.AllowThousands);
            }

            this.PatternReload(true);
        }

        /// <summary>
        /// パターンボタンイベント追加処理
        /// </summary>
        public void PatternButtonInit()
        {
            int positionActive = this.logic.PositionActive; //ThangNguyen [Add] 20150728 #11434

            var btnList = new List<CustomButton>() { this.bt_ptn1, this.bt_ptn2, this.bt_ptn3, this.bt_ptn4, this.bt_ptn5 };

            btnList.ForEach(btn =>
                {
                    btn.Click -= new EventHandler(PatternButtonClick);
                    btn.Click += new EventHandler(PatternButtonClick);
                    //ThangNguyen [Add] 20150728 #11434 Start
                    if (btn.Name.Substring(btn.Name.Length - 1).ToString() == positionActive.ToString())
                    {
                        this.ChangeBackGroundButton(btn);
                        this.Table = this.logic.GetColumnHeaderOnlyDataTable();
                        this.logic.CreateDataGridView(this.Table);
                    }
                    //ThangNguyen [Add] 20150728 #11434 End
                }
            );
        }

        /// <summary>
        /// パターン一覧を呼び出します。
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <returns>システムID（適用ボタンが押された場合）</returns>
        public string OpenPatternIchiran(int denshuKbn = (int)DENSHU_KBN.NONE)
        {
            return this.logic.OpenPatternIchiran(denshuKbn);
        }

        /// <summary>
        /// システムIDからパターン呼び出し、ヘッダのみのデータテーブルとクエリをセットします。
        /// </summary>
        /// <param name="systemId"></param>
        public void SetPatternBySysId(string systemId)
        {
            this.PatternNo = 6;
            this.logic.SetCurrentPattern(systemId);
            this.logic.SetSearchQuery();
            this.Table = this.logic.GetColumnHeaderOnlyDataTable();
        }

        /// <summary>
        /// パターンを読込直します。
        /// </summary>
        /// <param name="isDefault">デフォルトパターンを読み込む場合はTrueを指定</param>
        public void PatternReload(bool isDefault = false)
        {
            if (isDefault)
            {
                this.PatternNo = 0;
            }
            this.logic.PatternInit();
            this.logic.SetPatternButton();
            this.logic.SortHeaderInit();
            this.PatternButtonInit();
        }

        /// <summary>
        /// 非表示にする列を登録します。
        /// </summary>
        /// <param name="columns"></param>
        public void SetHiddenColumns(params string[] columns)
        {
            this.logic.hiddenColumns = columns;
        }

        //protected override void OnSizeChanged(EventArgs e)
        //{
        //    if (this.customDataGridView1 != null)
        //    {
        //        // デザイナでの設定が初期化されるのでここで再設定
        //        this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
        //    }
        //    base.OnSizeChanged(e);
        //}

        /// <summary>
        /// 
        /// </summary>
        public virtual void AdjustColumnSizeComplete()
        {
            //ヘッダチェックボックスの列の幅を調整
            var listHeaderCheckbox = this.customDataGridView1.Columns.Cast<DataGridViewColumn>()
                                            .Where(w => w.HeaderCell is DgvCustomCheckBoxHeaderCell && 
                                                        !String.IsNullOrEmpty(w.HeaderText));
            if (listHeaderCheckbox != null && listHeaderCheckbox.Count() > 0)
            {
                foreach (DataGridViewColumn col in listHeaderCheckbox)
                {
                    var checkboxCell = col.HeaderCell as DgvCustomCheckBoxHeaderCell;
                    if (checkboxCell != null)
                    {
                        //チェックボックスの位置が中央以外の場合、列の幅を増加する
                        if (checkboxCell.CheckboxPosition != r_framework.CustomControl.DataGridCustomControl.CheckAlign.MIDDLE)
                        {
                            Size textSize = TextRenderer.MeasureText(col.HeaderText, this.customDataGridView1.ColumnHeadersDefaultCellStyle.Font);
                            col.Width = textSize.Width + 30;
                        }
                    }
                }
            }
        }
    }
}
