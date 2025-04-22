// $Id: UIForm.cs 42733 2015-02-20 04:30:49Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;
using System.Data;

namespace Shougun.Core.BusinessManagement.JuchuuYojitsuKanrihyou
{

    public partial class JuchuuYojitsuKanrihyouForm : SuperForm
    {
        internal HeaderForm header;
        /// <summary>
        /// コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private JuchuuYojitsuKanrihyouLogic logic;

        #region juchuuYojitsukanrihyouForm
        public JuchuuYojitsuKanrihyouForm(HeaderForm headerForm)
            : base(WINDOW_ID.T_JYUCYUU_YOJITSU_KANNRIHYOU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new JuchuuYojitsuKanrihyouLogic(this);

                //ヘッダ
                this.header = headerForm;
                this.logic.SetHeaderInfo(this.header);
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            try
            {
                base.OnLoad(e);

                if (!this.logic.WindowInit()) { return; }        // 画面情報の初期化
                this.header.rdbGetuji.Checked = true;

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.grdIchiran != null)
                {
                    int GRID_HEIGHT_MIN_VALUE = 376;
                    int GRID_WIDTH_MIN_VALUE = 330;
                    int h = this.Height - 56;
                    int w = this.Width;

                    if (h < GRID_HEIGHT_MIN_VALUE)
                    {
                        this.grdIchiran.Height = GRID_HEIGHT_MIN_VALUE;
                    }
                    else
                    {
                        this.grdIchiran.Height = h;
                    }
                    if (w < GRID_WIDTH_MIN_VALUE)
                    {
                        this.grdIchiran.Width = GRID_WIDTH_MIN_VALUE;
                    }
                    else
                    {
                        this.grdIchiran.Width = w;
                    }

                    if (this.grdIchiran.Height <= GRID_HEIGHT_MIN_VALUE
                        || this.grdIchiran.Width <= GRID_WIDTH_MIN_VALUE)
                    {
                        this.grdIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                    }
                    else
                    {
                        this.grdIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(e);
            }
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

        #region １～４カラム目を編集不可にする
        /// <summary>
        /// １～４カラム目を編集不可にする
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void customDataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            LogUtility.DebugMethodEnd(sender, e);
            try
            {
                this.logic.customDataGridView1_CellBeginEdit(sender, e);
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        private void tb_busho_cd_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                bool catchErr = false;
                // データソース
                this.tb_busho_cd.PopupDataSource = this.logic.getPopupBusyoInfo(out catchErr);
                if (catchErr)
                {
                    return;
                }

                // 列名
                this.tb_busho_cd.PopupDataHeaderTitle = new string[] { "部署CD", "部署名" };
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
}
