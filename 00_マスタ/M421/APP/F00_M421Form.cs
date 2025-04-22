using System;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Utility;
using System.Windows.Forms;

namespace Shougun.Core.Master.OboeGakiIkkatuIchiran
{
    public partial class M421Form : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>

        private M421Logic logic;
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ,メニューから起動の場合
        /// </summary>
        public M421Form()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new M421Logic(this);
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面Load
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            try
            {
                base.OnLoad(e);
                //初期化
                this.logic.WindowInit();
                //検索
                this.Search(null, e);
                //ヘッダ部セット
                this.logic.SetHearderItem();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
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
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {

                int result = this.logic.Search();

                this.Ichiran.IsBrowsePurpose = false;

                if (result == -1)
                {
                    return;
                }
                else if (result == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    this.Ichiran.DataSource = this.logic.SearchResult;

                    this.Ichiran.IsBrowsePurpose = true;
                    return;
                }
                var table = this.logic.SearchResult;
                table.BeginLoadData();
                this.Ichiran.DataSource = table;

                this.Ichiran.IsBrowsePurpose = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F2 新規処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SinkiClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.Sinki();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F3 修正処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShuuseiClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.Shuusei();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F4 削除処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SakujyoClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.Sakujyo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F6 CSV出力処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.CSVOutput();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F12 Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.FormClose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細ダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Ichiran_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridViewCellMouseEventArgs datagridviewcell = (DataGridViewCellMouseEventArgs)e;
            if (datagridviewcell.RowIndex >= 0)
            {
                this.logic.Shuusei();
            }
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

    }
}
