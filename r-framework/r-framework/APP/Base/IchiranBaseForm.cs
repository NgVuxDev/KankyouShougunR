using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base.IchiranHeader;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;
using Seasar.Quill;

namespace r_framework.APP.Base
{
    /// <summary>
    /// 一覧画面にて使用するベースクラス
    /// </summary>
    [Obsolete("代わりに 'r_framework.Base.APP.BussinessBaseForm' を使用し、ヘッダーフォームは各画面にて用意します。")]
    public partial class IchiranBaseForm : BaseBaseForm
    {

        /// <summary>
        /// コンストラクタ
        /// 引数で渡されたFormを埋め込み画面を表示する
        /// </summary>
        /// <param name="form">埋め込むForm</param>
        public IchiranBaseForm(Form form, DENSHU_KBN denshuKbn, RibbonMainMenu ribbon = null)
        {
            InitializeComponent();

            if (ribbon == null)
            {
                this.ribbonForm = new RibbonMainMenu(FormManager.FormManager.UserRibbonMenu.MenuConfigXML, (Dto.CommonInformation)FormManager.FormManager.UserRibbonMenu.GlobalCommonInformation.Clone());
            }
            else
            {
                this.ribbonForm = new RibbonMainMenu(ribbon.MenuConfigXML, ribbon.GlobalCommonInformation);
            }

            switch (denshuKbn)
            {
                case DENSHU_KBN.DENNSHI_MANIFEST_JIGYOUJO:
                case DENSHU_KBN.DENNSHI_MANIFEST_JIGYOUSHA:
                case DENSHU_KBN.ITAKU_KEIYAKUSHO:
                case DENSHU_KBN.DENSHI_KEIYAKU_RIREKI_ICHIRAN:
                    headerForm = new IchiranHeaderForm2();
                    break;
                case DENSHU_KBN.TORIHIKISAKI:
                case DENSHU_KBN.GENBA:
                case DENSHU_KBN.GAIBU_RENKEI_GENBA_ICHIRAN:
                case DENSHU_KBN.GYOUSHA:
                case DENSHU_KBN.KOBETSU_HINMEI_TANKA_ICHIRAN:
                    headerForm = new IchiranHeaderForm1();
                    break;
                default:
                    headerForm = new IchiranHeaderForm3();
                    break;
            }

            this.inForm = form;

            //コンストラクタで追加必須
            this.ribbonForm.TopLevel = false; //フォームを追加するには必須の設定
            this.headerForm.TopLevel = false;
            this.inForm.TopLevel = false;
            this.Controls.Add(this.ribbonForm);
            this.Controls.Add(this.headerForm);
            this.Controls.Add(this.inForm);

            QuillInjector.GetInstance().Inject(this);
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }


        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e); //リサイズ等ここで行うので、リボン非表示等はここより前で行うこと
        }

        /// <summary>
        /// プログレスバー描画開始処理
        /// </summary>
        public void ProgresStart()
        {
            //処理が行われているときは、何もしない
            if (backgroundWorker1.IsBusy)
                return;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            progresBar.Style = ProgressBarStyle.Marquee;
            progresBar.MarqueeAnimationSpeed = 30;

            backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// プログレスバー描画停止処理
        /// </summary>
        public void ProgresStop()
        {
            backgroundWorker1.CancelAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;

            bw.ReportProgress(30);

            while (true)
            {
                //キャンセルされたか調べる
                if (bw.CancellationPending)
                {
                    //キャンセルされたとき
                    e.Cancel = true;
                    return;
                }
            }

        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            progresBar.MarqueeAnimationSpeed = 0;

            progresBar.Style = ProgressBarStyle.Continuous;

            progresBar.Value = 0;
        }

    }
}