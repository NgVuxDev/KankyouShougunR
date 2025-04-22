using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill.Attrs;

namespace Shougun.Core.Common.DenpyouRenkeiIchiran
{
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private DenpyouRenkeiIchiran.LogicClass IchiranLogic;

        private Boolean isLoaded;

        private UIHeader header;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        public string beforeCd;

        public bool isError;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        #region コンストラクタ

        public UIForm()
            // 20150824 katen #11961 伝票連携状況一覧 ウィンドウの名前が「環境将軍R」となっている start‏
            : base(WINDOW_ID.T_DENPYOU_RENKEI_JOUKYOU_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        // ----20150824 katen #11961 伝票連携状況一覧 ウィンドウの名前が「環境将軍R」となっている end‏
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.IchiranLogic = new LogicClass(this);
        }

        #endregion

        #region 画面コントロールイベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 画面情報の初期化
            if (!isLoaded)
            {
                // キー入力設定
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;
                this.IchiranLogic.parentForm = this.ParentBaseForm;

                this.IchiranLogic.WindowInit();

                // ヘッダーフォームを取得
                this.header = (UIHeader)this.ParentBaseForm.headerForm;

                //表示の初期化
                this.IchiranLogic.ClearScreen("Initial");
                Control[] formControls = controlUtil.GetAllControls(this);
                Control[] headerControls = controlUtil.GetAllControls(this.header);
                List<Control> allCon = new List<Control>();
                allCon.AddRange(headerControls);
                allCon.AddRange(formControls);
                this.allControl = allCon.ToArray();
            }

            isLoaded = true;

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }

        #endregion

        /// <summary>
        /// 条件ｸﾘｱ(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {
            this.IchiranLogic.ClearScreen("ClsSearchCondition");
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();

            if (this.RegistErrorFlag)
            {
                var focusControl = this.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
                return;
            }

            if (this.KINOU_KBN.Text == "1")
            {
                this.IchiranLogic.Search_Entry();
            }
            else
            {
                this.IchiranLogic.Search_Renkei();
            }
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            if (this.KINOU_KBN_1.Checked)
            {
                this.IchiranLogic.DataSort();
            }
            else
            {
                this.IchiranLogic.DataSort();
            }
        }

        /// <summary>
        /// フィルタ(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func11_Click(object sender, EventArgs e)
        {
            this.IchiranLogic.DataSearch();
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            Properties.Settings.Default.KYOTEN_CD = this.header.KYOTEN_CD.Text;
            Properties.Settings.Default.KINOU_KBN = this.KINOU_KBN.Text;
            Properties.Settings.Default.DENPYOU_KBN = this.DENPYOU_KBN.Text;
            Properties.Settings.Default.DATE_FROM = this.DATE_FROM.Text;
            Properties.Settings.Default.DATE_TO = this.DATE_TO.Text;
            Properties.Settings.Default.DENPYOU_NO = this.DENPYOU_NO.Text;
            Properties.Settings.Default.TORIHIKISAKI_CD = this.TORIHIKISAKI_CD.Text;
            Properties.Settings.Default.GYOUSHA_CD = this.GYOUSHA_CD.Text;
            Properties.Settings.Default.GENBA_CD = this.GENBA_CD.Text;
            Properties.Settings.Default.RENKEI_KBN = this.RENKEI_KBN.Text;
            Properties.Settings.Default.UKETSUKE_FLG = this.UKETSUKE_FLG.Checked;
            Properties.Settings.Default.UKEIRE_FLG = this.UKEIRE_FLG.Checked;
            Properties.Settings.Default.SHUKKA_FLG = this.SHUKKA_FLG.Checked;
            Properties.Settings.Default.UR_SH_FLG = this.UR_SH_FLG.Checked;
            Properties.Settings.Default.MANI_FLG = this.MANI_FLG.Checked;
            Properties.Settings.Default.UNCHIN_FLG = this.UNCHIN_FLG.Checked;
            Properties.Settings.Default.DAINOU_FLG = this.DAINOU_FLG.Checked;
            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 対象伝票参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            if (this.KINOU_KBN_1.Checked)
            {
                this.IchiranLogic.FormChanges(1);
            }
            else if (this.KINOU_KBN_2.Checked)
            {
                this.IchiranLogic.FormChanges(3);
            }
        }

        /// <summary>
        /// 派生伝票参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, EventArgs e)
        {
            if (this.KINOU_KBN_1.Checked)
            {
                this.IchiranLogic.FormChanges(2);
            }
            else if (this.KINOU_KBN_2.Checked)
            {
                this.IchiranLogic.FormChanges(4);
            }
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process3_Click(object sender, EventArgs e)
        {
            this.IchiranLogic.CSV();
        }

        #region 画面イベント

        #region 一覧イベント

        /// <summary>
        /// 対象明細ダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ENTRY_Ichiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                this.IchiranLogic.FormChanges(1);
        }

        private void ENTRY_Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.IchiranLogic.Search_Hasei();
        }

        /// <summary>
        /// 派生データダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HASEI_Ichiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                this.IchiranLogic.FormChanges(2);
        }

        /// <summary>
        /// 連携明細データダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RENKEI_Ichiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex <= 5)
            {
                this.IchiranLogic.FormChanges(3);
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex > 5)
            {
                this.IchiranLogic.FormChanges(4);
            }
        }

        /// <summary>
        /// 連携明細データダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RENKEI_Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                Rectangle rect = e.CellBounds;
                Graphics g = e.Graphics;
                //g.DrawLine(new Pen(Color.DarkGray, 2), rect.Right+2, rect.Top, rect.Right+2, rect.Bottom);
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                Rectangle rect = e.CellBounds;
                Graphics g = e.Graphics;
                g.DrawLine(new Pen(Color.Black, 2), rect.Left - 1, rect.Top, rect.Left - 1, rect.Bottom);
            }
        }

        #endregion

        // 20150824 katen #11971 伝票連携状況一覧 並び替えのテキスト部をクリックすると、並び替えのポップアップ出現 start‏
        //public void txboxSortSettingInfo_Enter(object sender, EventArgs e)
        //{
        //    this.IchiranLogic.DataSort();
        //}

        //public void txtSearchSettingInfo_Enter(object sender, EventArgs e)
        //{
        //    this.IchiranLogic.DataSearch();
        //}
        // 20150824 katen #11971 伝票連携状況一覧 並び替えのテキスト部をクリックすると、並び替えのポップアップ出現 end‏

        public void DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.DATE_TO.Text = this.DATE_FROM.Text;
        }

        private void KINOU_KBN_2_CheckedChanged(object sender, EventArgs e)
        {
            this.DENPYOU_KBN.Text = "1";
            this.IchiranLogic.SetEnableByKinou();
        }

        /// <summary>
        /// 伝票区分変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DENPYOU_KBN_TextChanged(object sender, EventArgs e)
        {
            this.IchiranLogic.SetGyoushaAndGenbaPopup();

            // 業者、現場情報初期化
            this.GYOUSHA_CD.Text = string.Empty;
            this.GYOUSHA_NAME.Text = string.Empty;
            this.GENBA_CD.Text = string.Empty;
            this.GENBA_NAME.Text = string.Empty;
        }

        private void DENPYOU_KBN_1_CheckedChanged(object sender, EventArgs e)
        {
            this.IchiranLogic.SetDateName();
        }

        private void DENPYOU_KBN_2_CheckedChanged(object sender, EventArgs e)
        {
            this.IchiranLogic.SetDateName();
        }

        private void DENPYOU_KBN_3_CheckedChanged(object sender, EventArgs e)
        {
            this.IchiranLogic.SetDateName();
        }

        private void DENPYOU_KBN_4_CheckedChanged(object sender, EventArgs e)
        {
            this.IchiranLogic.SetDateName();
        }

        private void DENPYOU_KBN_5_CheckedChanged(object sender, EventArgs e)
        {
            this.IchiranLogic.SetDateName();
        }

        private void DENPYOU_KBN_6_CheckedChanged(object sender, EventArgs e)
        {
            this.IchiranLogic.SetEnableByDenpyou();
            this.IchiranLogic.SetDateName();
        }

        private void DENPYOU_KBN_7_CheckedChanged(object sender, EventArgs e)
        {
            this.IchiranLogic.SetDateName();
        }

        internal void KYOTEN_CD_Validated(object sender, EventArgs e)
        {
            this.IchiranLogic.KYOTEN_CD_Validated();
        }

        internal void TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            this.IchiranLogic.TORIHIKISAKI_CD_Validated();
        }

        internal void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.IchiranLogic.GYOUSHA_CD_Validated();
        }

        internal void GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.IchiranLogic.GENBA_CD_Validated();
        }

        internal void KYOTEN_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                this.beforeCd = this.header.KYOTEN_CD.Text;
            }
        }

        internal void TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                this.beforeCd = TORIHIKISAKI_CD.Text;
            }
        }

        internal void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                this.beforeCd = GYOUSHA_CD.Text;
            }
        }

        internal void GENBA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                this.beforeCd = GENBA_CD.Text;
            }
        }

        public void GyoushaPopupBefore()
        {
            if (!this.isError)
            {
                this.beforeCd = GYOUSHA_CD.Text;
            }
        }

        public void GyoushaPopupAfter()
        {
            this.IchiranLogic.GYOUSHA_CD_Validated();
        }

        #endregion
    }
}