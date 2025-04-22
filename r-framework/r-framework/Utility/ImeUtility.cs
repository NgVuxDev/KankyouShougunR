using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace r_framework.Utility
{
    public class ImeUtility
    {
        [DllImport("imm32.dll", SetLastError = true)]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);
        [DllImport("imm32.dll", SetLastError = true)]
        private static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);
        [DllImport("imm32.dll", SetLastError = true)]
        private static extern bool ImmGetConversionStatus(IntPtr hIMC, ref int fdwConversion, ref int fdwSentence);
        [DllImport("imm32.dll", SetLastError = true)]
        private static extern bool ImmSetConversionStatus(IntPtr hIMC, int fdwConversion, int fdwSentence);
        [DllImport("Imm32.dll", SetLastError = true)]
        static extern bool ImmGetOpenStatus(IntPtr hIMC);
        [DllImport("Imm32.dll", SetLastError = true)]
        static extern bool ImmNotifyIME(IntPtr hIMC, uint dwAction, uint dwIndex, int dwValue);
        
        private const int IME_CMODE_ALPHANUMERIC = 0x0000;
        private const int IME_CMODE_NATIVE = 0x0001;
        private const int IME_CMODE_CHINESE = IME_CMODE_NATIVE;
        private const int IME_CMODE_HANGEUL = IME_CMODE_NATIVE;
        private const int IME_CMODE_HANGUL = IME_CMODE_NATIVE;
        private const int IME_CMODE_JAPANESE = IME_CMODE_NATIVE;
        private const int IME_CMODE_KATAKANA = 0x0002;
        private const int IME_CMODE_LANGUAGE = 0x0003;
        private const int IME_CMODE_FULLSHAPE = 0x0008;
        private const int IME_CMODE_ROMAN = 0x0010;
        private const int IME_CMODE_CHARCODE = 0x0020;
        private const int IME_CMODE_HANJACONVERT = 0x0040;
        private const int IME_CMODE_SOFTKBD = 0x0080;
        private const int IME_CMODE_NOCONVERSION = 0x0100;
        private const int IME_CMODE_EUDC = 0x0200;
        private const int IME_CMODE_SYMBOL = 0x0400;
        private const int IME_CMODE_FIXED = 0x0800;

        private const int IME_SMODE_NONE = 0x0000; // 無変換
        private const int IME_SMODE_PLAURALCLAUSE = 0x0001; // 人名/地名（複合語優先）
        private const int IME_SMODE_SINGLECONVERT = 0x0002; // （単変換）
        private const int IME_SMODE_AUTOMATIC = 0x0004; // （自動変換）
        private const int IME_SMODE_PHRASEPREDICT = 0x0008; // 一般（連文節変換）
        private const int IME_SMODE_CONVERSATION = 0x0010; // 話し言葉優先
        private const uint NI_SELECTCANDIDATESTR = 0x0015u;
        private const uint CPS_COMPLETE = 0x0001u;

        private static void setImmSentenceMode(IntPtr hWnd, int smode)
        {
            IntPtr hIMC = ImmGetContext(hWnd);
            try
            {
                int cmode = 0;
                int currentSmode = -1;
                bool result = ImmGetConversionStatus(hIMC, ref cmode, ref currentSmode);
                if (result && currentSmode != smode)
                {
                    Debug.WriteLine("ImmGetContext(0x{0:X}) =0x{1:X}, smode=0x{1:X}", hWnd, hIMC);
                    Debug.WriteLine("ImmGetConversionStatus() cmode=0x{0:X}, smode=0x{1:X}", cmode, currentSmode);
                    result = ImmSetConversionStatus(hIMC, cmode, smode);
                    Debug.WriteLine("ImmSetConversionStatus() cmode=0x{0:X}, smode=0x{1:X} result={2}", cmode, smode, result);
                }
            }
            finally
            {
                ImmReleaseContext(hWnd, hIMC);
            }
        }

        /// <summary>
        /// IMEの変換モードをコントロールにあわせたモードに調節する。
        /// WindowsでたまにIMEの変換モードが勝手に「無変換」になってしまう現象の対策。
        /// 指定したコントロール(TextBox派生のものならなんでも）のImeModeにあわせた変換モードを設定する。
        /// 例えばImeMode.Hiraganaなら変換モードを「一般」に設定する。
        /// 指定するコントロールにはFormやコンテナを指定できる。コンテナを指定した場合は
        /// ContainerControl.ActiveControlを再帰的に辿り、コンテナ派生ではないコントロールを対象とする。
        /// </summary>
        /// <param name="control">コントロールまたはントロールコンテナまたはFormなど</param>
        public static void AdjustControlImeSentenceMode(Control control)
        {
            // FormやUserControlなどのコンテナだった場合にコンテナではないアクティブコントロールまで辿る。
            // 入れ子になっている場合があるので再帰的に辿る。
            while ((control as ContainerControl) != null)
            {
                control =  (control as ContainerControl).ActiveControl;
            }
            
            // 入力テキストボックスでなければ何もしない
            var textBox = control as TextBox;
            if (textBox == null || textBox.ReadOnly)
            {
                return;
            }

            // 入力モードが「ひらがな」「カタカナ」「ON」なら変換モードを「一般」に設定
            if (textBox.ImeMode == ImeMode.Hiragana 
             || textBox.ImeMode == ImeMode.Katakana
             || textBox.ImeMode == ImeMode.On)
            {
#if DEBUG
                var caller = (new StackTrace()).GetFrames()[2].GetMethod();
                Debug.WriteLine("ImeUtility.SetTextBoxImeSentenceMode() {0}.{1} TextBox={2}　ImeMode={3}",
                    caller.DeclaringType.FullName, caller.Name, textBox.Name, textBox.ImeMode.ToString());
#endif
                textBox.ImeMode = textBox.ImeMode;
                ImeUtility.setImmSentenceMode(textBox.Handle, IME_SMODE_PHRASEPREDICT);
            }
        }

        public static void ReleaseImeMode(IntPtr handle)
        {
            var context = ImmGetContext(handle);
            if (context != IntPtr.Zero)
            {
                if (ImmGetOpenStatus(context))
                {
                    ImmNotifyIME(context, NI_SELECTCANDIDATESTR, CPS_COMPLETE, 0);
                    ImmReleaseContext(handle, context);
                }
            }
        }
    }
}
