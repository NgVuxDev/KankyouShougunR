using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;

namespace Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.APP
{
    /// <summary>
    /// G334：取引履歴一覧 フォーム
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region 内部変数

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        #endregion

        #region プロパティ

        /// <summary>
        /// 親フォーム
        /// </summary>
        internal BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal HeaderForm HeaderForm { get; private set; }

        // 20140717 syunrei EV005298_システム設定-個別メニューの拠点を参照していない。　start
        /// <summary>
        /// DBアクセッサー
        /// </summary>
        public Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Accessor.DBAccessor accessor;
        // 20140717 syunrei EV005298_システム設定-個別メニューの拠点を参照していない。　end

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_TORIHIKI_RIREKI_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント処理

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            try
            {
                base.OnLoad(e);

                if (!this.DesignMode)
                {
                    this.ParentBaseForm = (BusinessBaseForm)this.Parent;
                    this.HeaderForm = (HeaderForm)this.ParentBaseForm.headerForm;
                    // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                    if (this.logic != null)
                    {
                        this.logic.Dispose();
                        this.logic = null;
                    }
                    this.logic = new LogicCls(this);

                    // 画面初期化
                    if (!this.logic.WindowInit()) { return; }

                    // 20140717 syunrei EV005298_システム設定-個別メニューの拠点を参照していない。　start
                    // Accessor
                    this.accessor = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Accessor.DBAccessor();

                    // 拠点
                    this.HeaderForm.txtBox_KyotenCd.Text = string.Empty;
                    this.HeaderForm.txtBox_KyotenNameRyaku.Text = string.Empty;
                    const string KYOTEN_CD = "拠点CD";
                    CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                    this.HeaderForm.txtBox_KyotenCd.Text = this.logic.GetUserProfileValue(userProfile, KYOTEN_CD);
                    if (!string.IsNullOrEmpty(this.HeaderForm.txtBox_KyotenCd.Text.ToString()))
                    {
                        this.HeaderForm.txtBox_KyotenCd.Text = this.HeaderForm.txtBox_KyotenCd.Text.ToString().PadLeft(this.HeaderForm.txtBox_KyotenCd.MaxLength, '0');
                        this.logic.CheckKyotenCd();
                    }
                    // 20140717 syunrei EV005298_システム設定-個別メニューの拠点を参照していない。　end
                }
                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    int GRID_HEIGHT_MIN_VALUE = 313;
                    int GRID_WIDTH_MIN_VALUE = 889;
                    int h = this.Height - 110;
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
                        this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                    }
                    else
                    {
                        this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    }
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd(e);
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
        #endregion

        /// <summary>
        /// 取引先CDテキストボックスのバリデートが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void numTxtbox_TrhkskCD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var torihikisakiCd = this.numTxtbox_TrhkskCD.Text;
            if (!String.IsNullOrEmpty(torihikisakiCd))
            {
                bool catchErr = false;
                var torihikisaki = this.logic.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (catchErr) { return; }
                if (null != torihikisaki)
                {
                    this.numTxtbox_TrhkskCD.Text = torihikisaki.TORIHIKISAKI_CD;
                    this.txtBox_TrhkskName.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                }
                else
                {
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E011", "取引先マスタ");

                    this.numTxtbox_TrhkskCD.Text = String.Empty;
                    this.txtBox_TrhkskName.Text = String.Empty;
                    e.Cancel = true;
                }
            }
            else
            {
                this.numTxtbox_TrhkskCD.Text = String.Empty;
                this.txtBox_TrhkskName.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDテキストボックスのバリデートが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void numTxtBox_GyousyaCD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCd = this.numTxtBox_GyousyaCD.Text;
            if (this.BeforeGyoushaCd != gyoushaCd)
            {
                if (!String.IsNullOrEmpty(gyoushaCd))
                {
                    bool catchErr = false;
                    var gyousha = this.logic.GetGyousha(gyoushaCd, out catchErr);
                    if (catchErr) { return; }
                    if (null != gyousha)
                    {
                        this.numTxtBox_GyousyaCD.Text = gyousha.GYOUSHA_CD;
                        this.txtBox_GyousyaName.Text = gyousha.GYOUSHA_NAME_RYAKU;

                        //var torihikisakiCd = this.numTxtbox_TrhkskCD.Text;
                        //var torihikisaki = this.logic.GetTorihikisaki(gyousha.TORIHIKISAKI_CD);
                        //if (null != torihikisaki)
                        //{
                        //    this.numTxtbox_TrhkskCD.Text = torihikisaki.TORIHIKISAKI_CD;
                        //    this.txtBox_TrhkskName.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        //}

                        this.numTxtBox_GbCD.Text = String.Empty;
                        this.txtBox_GbName.Text = String.Empty;
                    }
                    else
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E011", "業者マスタ");

                        this.numTxtBox_GyousyaCD.Text = String.Empty;
                        this.txtBox_GyousyaName.Text = String.Empty;
                        this.numTxtBox_GbCD.Text = String.Empty;
                        this.txtBox_GbName.Text = String.Empty;
                        e.Cancel = true;
                    }
                }
                else
                {
                    this.numTxtBox_GyousyaCD.Text = String.Empty;
                    this.txtBox_GyousyaName.Text = String.Empty;
                    this.numTxtBox_GbCD.Text = String.Empty;
                    this.txtBox_GbName.Text = String.Empty;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void numTxtBox_GyousyaCD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCd = this.numTxtBox_GyousyaCD.Text;
            if (String.IsNullOrEmpty(gyoushaCd))
            {
                this.numTxtBox_GbCD.Text = String.Empty;
                this.txtBox_GbName.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void numTxtBox_GbCD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCd = this.numTxtBox_GyousyaCD.Text;
            var genbaCd = this.numTxtBox_GbCD.Text;

            if (String.IsNullOrEmpty(gyoushaCd) && !String.IsNullOrEmpty(genbaCd))
            {
                var messageLogic = new MessageBoxShowLogic();
                messageLogic.MessageBoxShow("E051", "業者");
                this.numTxtBox_GbCD.Text = string.Empty;
                e.Cancel = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(genbaCd))
                {
                    if (this.BeforeGenbaCd != genbaCd)
                    {
                        bool catchErr = false;
                        var genba = this.logic.GetGenba(gyoushaCd, genbaCd, out catchErr);
                        if (catchErr) { return; }
                        if (null != genba)
                        {
                            this.numTxtBox_GbCD.Text = genba.GENBA_CD;
                            this.txtBox_GbName.Text = genba.GENBA_NAME_RYAKU;
                        }
                        else
                        {
                            var messageLogic = new MessageBoxShowLogic();
                            messageLogic.MessageBoxShow("E011", "現場マスタ");

                            e.Cancel = true;

                            this.numTxtBox_GbCD.Text = String.Empty;
                            this.txtBox_GbName.Text = String.Empty;
                        }
                    }
                }
                else
                {
                    this.numTxtBox_GbCD.Text = String.Empty;
                    this.txtBox_GbName.Text = String.Empty;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDの前回入力値を取得・設定します
        /// </summary>
        private string BeforeGyoushaCd { get; set; }

        /// <summary>
        /// 現場CDの前回入力値を取得・設定します
        /// </summary>
        private string BeforeGenbaCd { get; set; }

        /// <summary>
        /// 業者CDテキストボックスがフォーカスを得たときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void numTxtBox_GyousyaCD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.BeforeGyoushaCd = this.numTxtBox_GyousyaCD.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者検索ポップアップボタンでポップアップを開く前に処理します
        /// </summary>
        public void BeforeGyoushaPopup()
        {
            LogUtility.DebugMethodStart();

            this.BeforeGyoushaCd = this.numTxtBox_GyousyaCD.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者検索ポップアップボタンで開いたポップアップを閉じた後に処理します
        /// </summary>
        public void AfterGyoushaPopup()
        {
            LogUtility.DebugMethodStart();

            var gyoushaCd = this.numTxtBox_GyousyaCD.Text;
            if (!String.IsNullOrEmpty(gyoushaCd) && this.BeforeGyoushaCd != gyoushaCd)
            {
                this.numTxtBox_GbCD.Text = String.Empty;
                this.txtBox_GbName.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDテキストボックスがフォーカスを得たときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void numTxtBox_GbCD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.BeforeGenbaCd = this.numTxtBox_GbCD.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場検索ポップアップボタンでポップアップを開く前に処理します
        /// </summary>
        public void BeforeGenbaPopup()
        {
            LogUtility.DebugMethodStart();

            this.BeforeGenbaCd = this.numTxtBox_GbCD.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// DataGridViewの赤枠線描画処理(G334オリジナル)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            CustomDataGridView dgv = (CustomDataGridView)sender;

            //カレント行でなければ抜ける
            if (dgv.CurrentRow == null || e.RowIndex != dgv.CurrentRow.Index)
                return;

            if (e.RowIndex >= 0 && dgv.CurrentCell.ColumnIndex >= 6 && dgv.CurrentCell.ColumnIndex <= 10)
            {
                // 「前回取引日」「前々回」「3回前」「4回前」「5回前」にフォーカスがある場合、セルだけ赤枠にする
                //線を引く位置を計算する
                var cellRect = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, false);
                var cellX = cellRect.X;
                var cellY = cellRect.Y;
                var endX = cellRect.Right;
                var endY = cellRect.Bottom;

                using (var pen = new Pen(Color.Red, 2))
                {
                    // 上
                    e.Graphics.DrawLine(pen, cellX, cellY + 1, endX, cellY + 1);
                    // 下
                    e.Graphics.DrawLine(pen, cellX, cellRect.Bottom - 2, endX, endY - 2);
                    // 左
                    e.Graphics.DrawLine(pen, cellX, cellY, cellX, endY - 2);
                    // 右
                    e.Graphics.DrawLine(pen, endX - 1, cellY, endX - 1, endY - 2);
                }
            }
            else
            {
                //カレント行に赤い枠線を引く(CustomDataGridViewのOnRowPostPaintと同じ)
                var rowRect = e.RowBounds;
                var rowX = rowRect.X;
                var rowY = rowRect.Y;
                var endX = dgv.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                var endY = rowRect.Bottom;

                if (dgv.RowHeadersVisible)
                {
                    endX += dgv.RowHeadersWidth; // 行ヘッダの幅
                }
                endX -= dgv.HorizontalScrollingOffset; // 全カラム幅からスクロールで見えない分を引く

                using (var pen = new Pen(Color.Red, 2))
                {
                    //上下の線
                    e.Graphics.DrawLine(pen, rowX - 1, rowY + 1, endX + 1, rowY + 1);
                    e.Graphics.DrawLine(pen, rowX - 1, endY - 2, endX + 1, endY - 2);

                    // 左端が見える状態なら左縦線を引く
                    if (dgv.HorizontalScrollingOffset == 0)
                    {
                        e.Graphics.DrawLine(pen, rowX + 1, rowY, rowX + 1, endY - 2);
                    }

                    // 右端が見える状態なら右縦線を引く
                    if (endX <= this.Width) // 見えてる分の幅がコントロール自体のサイズ以下なら右端が見えるはず
                    {
                        e.Graphics.DrawLine(pen, endX, rowY, endX, endY - 2);
                    }
                }
            }
        }
    }
}