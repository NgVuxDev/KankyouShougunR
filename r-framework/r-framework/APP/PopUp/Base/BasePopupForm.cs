using r_framework.Const;
namespace r_framework.APP.PopUp.Base
{
    /// <summary>
    /// ポップアップ画面のベースとなるクラス
    /// </summary>
    public partial class BasePopupForm : SuperPopupForm
    {
        private string[] str ={"ア,イ,ウ,エ,オ",
                                "カ,キ,ク,ケ,コ",
                                "サ,シ,ス,セ,ソ",
                                "タ,チ,ツ,テ,ト",
                                "ナ,ニ,ヌ,ネ,ノ",
                                "ハ,ヒ,フ,ヘ,ホ",
                                "マ,ミ,ム,メ,モ",
                                "ヤ,ユ,ヨ,,",
                                "ラ,リ,ル,レ,ロ",
                                "ワ,,,,",
                                ",,,,"};

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BasePopupForm()
            : base(WINDOW_ID.UKETSUKE_SHUSHU)
        {
            InitializeComponent();
            this.Width = 640;
            this.Height = 510;
        }

        /// <summary>
        /// ボタン名設定クラス
        /// </summary>
        /// <param name="no">設定する対象の番号</param>
        private void SetButtonText(int no)
        {
            string[] strhead = str[no].Split(',');

            this.button17.Text = string.IsNullOrEmpty(strhead[0]) ? "" : strhead[0];
            this.button18.Text = string.IsNullOrEmpty(strhead[1]) ? "" : strhead[1];
            this.button19.Text = string.IsNullOrEmpty(strhead[2]) ? "" : strhead[2];
            this.button20.Text = string.IsNullOrEmpty(strhead[3]) ? "" : strhead[3];
            this.button21.Text = string.IsNullOrEmpty(strhead[4]) ? "" : strhead[4];
        }

        /// <summary>
        /// 「あ」行ボタン押下時処理
        /// </summary>
        private void button5_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(0);
        }

        /// <summary>
        /// 「か」行ボタン押下時処理
        /// </summary>
        private void button6_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(1);
        }

        /// <summary>
        /// 「さ」行ボタン押下時処理
        /// </summary>
        private void button7_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(2);
        }

        /// <summary>
        /// 「た」行ボタン押下時処理
        /// </summary>
        private void button8_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(3);
        }

        /// <summary>
        /// 「な」行ボタン押下時処理
        /// </summary>
        private void button9_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(4);
        }

        /// <summary>
        /// 「は」行ボタン押下時処理
        /// </summary>
        private void button10_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(5);
        }

        /// <summary>
        /// 「ま」行ボタン押下時処理
        /// </summary>
        private void button11_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(6);
        }

        /// <summary>
        /// 「や」行ボタン押下時処理
        /// </summary>
        private void button12_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(7);
        }

        /// <summary>
        /// 「ら」行ボタン押下時処理
        /// </summary>
        private void button13_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(8);
        }

        /// <summary>
        /// 「わ」行ボタン押下時処理
        /// </summary>
        private void button14_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(9);
        }

        /// <summary>
        /// 閉じるボタン押下時処理
        /// </summary>
        private void button16_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 英字ボタン押下時処理
        /// </summary>
        private void button23_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(10);
        }

        /// <summary>
        /// その他ボタン押下時処理
        /// </summary>
        private void button15_Click(object sender, System.EventArgs e)
        {
            this.SetButtonText(10);
        }
    }
}