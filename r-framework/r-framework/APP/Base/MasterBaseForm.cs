using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.Const;
using Seasar.Quill;

namespace r_framework.APP.Base
{
    /// <summary>
    /// マスタ画面にて使用するベースクラス
    /// </summary>
    public partial class MasterBaseForm : BaseBaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form">埋め込むForm</param>
        /// <param name="windowType">画面タイプ</param>
        /// <param name="sideButtonFlag">処理Noボタンの表示可否フラグ</param>
        public MasterBaseForm(Form form, WINDOW_TYPE windowType, bool sideButtonFlag, RibbonMainMenu ribbon = null)
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

            switch (windowType)
            {
                case WINDOW_TYPE.ICHIRAN_WINDOW_FLAG:
                    headerForm = new ListHeaderForm();
                    break;
                default:
                    headerForm = new MasterHeaderForm();
                    break;
            }

            ProcessButtonPanel.Visible = sideButtonFlag;

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

        // 20150723 障害#11443一時対応 Start
        [Obsolete("画面起動最大数一時対応用、FormManagerを利用されてない画面が表示する時、自動的に本メソッドを呼び出す。今後、FormManagerを利用するよに修正は必要。")]
        public new void Show()
        {
            FormManager.FormManager.OpenNoneIdForm(this);
        }

        [Obsolete("画面起動最大数一時対応用、FormManagerを利用されてない画面が表示する時、自動的に本メソッドを呼び出す。今後、FormManagerを利用するよに修正は必要。")]
        public new DialogResult ShowDialog()
        {
            return FormManager.FormManager.OpenNoneIdFormModal(this);
        }
        // 20150723 障害#11443一時対応 End
    }
}