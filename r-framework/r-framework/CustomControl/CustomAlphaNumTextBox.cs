using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using r_framework.Const;

namespace r_framework.CustomControl
{
    /// <summary>
    /// 英数字入力テキストボックス
    /// </summary>
    public partial class CustomAlphaNumTextBox : CustomTextBox
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomAlphaNumTextBox()
        {
            InitializeComponent();

            //IMEを無効にする
            base.ImeMode = ImeMode.Disable;
            this.AlphabetLimitFlag = true;
            this.NumberLimitFlag = true;
            this.CharacterLimitList = null;
        }

        #region Property

        [Category("EDISONプロパティ_画面設定")]
        [Description("英語入力の可能有無を指定してください。")]
        public bool AlphabetLimitFlag { get; set; }

        private bool ShouldSerializeAlphabetLimitFlag()
        {
            return this.AlphabetLimitFlag != true;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("数字入力の可能有無を指定してください。")]
        public bool NumberLimitFlag { get; set; }

        private bool ShouldSerializeNumberLimitFlag()
        {
            return this.NumberLimitFlag != true;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("制限を行う場合は入力可能となる文字を設定してください。")]
        public string CharacterLimitList { get; set; }

        #endregion

        #region ウィンドウプロセスメソッド

        /// <summary>
        /// ペーストメッセージ
        /// </summary>
        private const int WM_PASTE = 0x302;

        /// <summary>
        /// ウィンドウプロセスメソッド
        /// </summary>
        /// <param name="m">ウィンドウメッセージ</param>
        [System.Security.Permissions.SecurityPermission(
            System.Security.Permissions.SecurityAction.LinkDemand,
            Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTE)
            {
                IDataObject iData = Clipboard.GetDataObject();
                //文字列がクリップボードにあるか
                if (iData != null && iData.GetDataPresent(DataFormats.Text))
                {
                    string clipStr = (string)iData.GetData(DataFormats.Text);
                    //クリップボードの文字列が数字のみか調べる

                    //Char 型の1次元配列に変換する
                    char[] clip = clipStr.ToCharArray();
                    bool check = false;
                    if (clip != null)
                    {
                        for (int i = 0; i < clip.Length; i++)
                        {
                            if (CheckUseInputData(clip[i]))
                            {
                                check = true;
                                break;
                            }
                        }
                        if (check)
                        {
                            return;
                        }
                    }
                }
            }

            base.WndProc(ref m);
        }

        #endregion

        #region キー入力イベント処理

        /// <summary>
        /// キー入力イベント処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            bool handled = true;

            if (Control.ModifierKeys == Keys.Control)
            {
                handled = false;
            }
            else
            {
                handled = CheckUseInputData(e.KeyChar);
            }

            e.Handled = handled;
        }

        [Description("コントロールのIMEモードを取得")]
        public new ImeMode ImeMode
        {
            get { return base.ImeMode; }
            set { }
        }

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        ///// <summary>
        ///// <para>PopupDataSourceを指定した場合、ここで指定した列名がDataGridViewのタイトル行に使用される。</para>
        ///// <para>とりあえず、CustomAlphaNumTextBoxだけ。</para>
        ///// </summary>
        //[Browsable(false)]
        //public string[] PopupDataHeaderTitle { get; set; }

        /// <summary>
        /// 入力データのチェックを行う
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public bool CheckUseInputData(char chr)
        {
            bool handled = true;

            if (Constans.ALLOW_KEY_CHARS_ALLINPUT.Contains(chr))
            {
                return false;
            }

            if (('a' <= chr && chr <= 'z') ||
               ('A' <= chr && chr <= 'Z'))
            {
                if (this.AlphabetLimitFlag)
                {
                    handled = false;
                }
            }
            else if ('0' <= chr && chr <= '9')
            {
                if (this.NumberLimitFlag)
                {
                    handled = false;
                }
            }
            else
            {
                string targetStr = this.CharacterLimitList;
                if (targetStr != null)
                {
                    //Char 型の1次元配列に変換する
                    char[] symbol = targetStr.ToCharArray();
                    if (symbol != null)
                    {
                        for (int i = 0; i < symbol.Length; i++)
                        {
                            if (symbol[i].Equals(chr))
                            {
                                handled = false;
                                break;
                            }
                        }
                    }
                }
            }

            return handled;
        }

        #endregion
    }
}