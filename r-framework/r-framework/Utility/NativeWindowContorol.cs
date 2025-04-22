using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.Event;

namespace r_framework.Utility
{
    public class NativeWindowContorol : NativeWindow
    {
        private const int WM_CHAR = 0x102;
        private const int WM_IME_COMPOSITION = 0x10F;
        private const int GCS_RESULTREADSTR = 0x0200;

        /// <summary>
        /// コンテキスト・ハンドルの取得
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("Imm32.dll")]
        private static extern int ImmGetContext(int hWnd);

        /// <summary>
        /// コンテキスト・ハンドルの解放
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hIMC"></param>
        /// <returns></returns>
        [DllImport("Imm32.dll")]
        private static extern int ImmReleaseContext(int hWnd, int hIMC);

        /// <summary>
        /// IMEより読みなどの文字列を取得する
        /// </summary>
        /// <param name="hIMC"></param>
        /// <param name="dwIndex"></param>
        /// <param name="lpBuf"></param>
        /// <param name="dwBufLen"></param>
        /// <returns></returns>
        [DllImport("Imm32.dll", CharSet = CharSet.Auto)]
        private static extern int ImmGetCompositionString(int hIMC, int dwIndex, byte[] lpBuf, int dwBufLen);

        /// <summary>
        /// IMEの状態取得
        /// </summary>
        /// <param name="hIMC"></param>
        /// <returns></returns>
        [DllImport("Imm32.dll")]
        private static extern int ImmGetOpenStatus(int hIMC);

        /// <summary>
        /// メッセージの監視状態
        /// </summary>
        public bool MsgEnabled { get; set; }

        /// <summary>
        /// IME変換イベントのデリゲート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void Converted(object sender, ConvertedEventArgs e);

        /// <summary>
        /// IME変換イベントの定義
        /// </summary>
        public event Converted OnConverted;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="c"></param>
        public NativeWindowContorol(Control c)
        {
            AssignHandle(c.Handle);
            c.HandleDestroyed += new EventHandler(OnHandleDestroyed);
            MsgEnabled = false;
        }

        /// <summary>
        /// ハンドル削除処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnHandleDestroyed(object sender, EventArgs e)
        {
            ReleaseHandle();
        }

        /// <summary>
        /// ウインドウメッセージ処理
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            int hIMC;
            int intLength;

            if (this.MsgEnabled)
            {
                switch (m.Msg)
                {
                    // IME変換イベント
                    case WM_IME_COMPOSITION:
                        string strYomi = string.Empty;
                        if (((uint)m.LParam & (uint)GCS_RESULTREADSTR) != 0)
                        {
                            hIMC = ImmGetContext(this.Handle.ToInt32());

                            // 読みの文字列を取得
                            intLength = ImmGetCompositionString(hIMC, GCS_RESULTREADSTR, null, 0);
                            if (intLength > 0)
                            {
                                // StringBuilderだとNULL終端されずゴミが混じる。そのためbyte[]で渡し、文字変換して対応
                                var tmp = new byte[intLength + 1];
                                var valueSize = ImmGetCompositionString(hIMC, GCS_RESULTREADSTR, tmp, intLength);
                                strYomi = System.Text.Encoding.Unicode.GetString(tmp, 0, valueSize);
                                if (strYomi.Length > intLength)
                                {
                                    strYomi = strYomi.Substring(0, intLength);
                                }

                                // IME変換時の入力イベントを発生
                                strYomi = Strings.StrConv(strYomi, VbStrConv.Wide);
                                ConvertedEventArgs ea = new ConvertedEventArgs(strYomi, false, intLength);
                                OnConverted(this, ea);
                            }
                            ImmReleaseContext(this.Handle.ToInt32(), hIMC);
                        }
                        break;

                    // 半角文字入力イベント
                    case WM_CHAR:
                        hIMC = ImmGetContext(this.Handle.ToInt32());

                        if (ImmGetOpenStatus(hIMC) == 0)
                        {
                            char chr = Convert.ToChar(m.WParam.ToInt32() & 0xff);
                            if (m.WParam.ToInt32() > 32)
                            {
                                string str = chr.ToString();

                                // 半角文字の入力イベントを発生
                                str = Strings.StrConv(str, VbStrConv.Wide);
                                ConvertedEventArgs ea = new ConvertedEventArgs(str, false, 1);
                                OnConverted(this, ea);
                            }
                        }
                        ImmReleaseContext(this.Handle.ToInt32(), hIMC);
                        break;

                }
            }

            base.WndProc(ref m);
        }
    }
}
