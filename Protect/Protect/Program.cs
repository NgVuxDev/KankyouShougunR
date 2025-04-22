using System;
using System.IO;
using System.Windows.Forms;
using r_framework.Configuration;
using r_framework.Logic;
using System.Text.RegularExpressions;

namespace ShougunProtect
{
    public class Program
    {

        public const string LockV = "LKW";
        public const string KeyV = "ULK";
        public const string DayV = "FRD";
        internal static double TrialD;
        public const string ProtectExe = "SGPRT.exe";

        /// <summary>
        /// プロテクトチェック
        /// </summary>
        /// <returns>true：起動OK　false:起動NG</returns>
        public static bool Open_Check()
        {

            MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

            //プロテクト実行チェック
            if (!AppConfig.IsProtectRun)
            {
                return true;
            }

            //オンプレチェック(クラウド環境は抜ける)
            if (AppConfig.IsTerminalMode)
            {
                return true;
            }

            //処理1:
            //①OPENでレジストリを開いて存在チェックを行う。
            //※該当サブキーが存在しない状態で、管理者権限が無くCreateで開くとエラーになる為。
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Royknak");

            //サブキー[Royknak]が存在するか又は、Lockキーが設定されてない場合はShogunProtect_Lock.exeを起動
            if ((key == null) || (key.GetValue(LockV, "") == ""))
            {
                try
                {
                    //R将軍起動フォルダ内にある、ShogunProtect_Lock.exeを管理者権限で起動
                    var proc = new System.Diagnostics.Process();
                    var lockDir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ProtectExe);
                    proc.StartInfo.Arguments = "/L";
                    proc.StartInfo.FileName = lockDir;
                    proc.StartInfo.Verb = "RunAs";          //Verbへ"RunAs"を指定する
                    proc.StartInfo.UseShellExecute = true;  //動詞(Verb)を利用する場合は、UseShellExecuteがTrue
                    proc.Start();
                    proc.WaitForExit();
                }
                catch (Exception e)
                {
                    //起動確認ダイアログでいいえを選択するとエラーになる為。
                    errmessage.MessageBoxShowWarn("プロテクトが設定されていません。\nログイン後、限定メニューが起動します。");
                    return false;
                }
            }
            //key.Close(); //keyがNULLの場合クローズするとエラーになる
            
            //処理2：
            //①解除キーの設定チェック
            //②解除キーが設定されていない場合、初回登録日から30日間は通常起動させる。
            //　以下の条件の場合限定メニューで起動：初回登録日無し。初回登録日日付変換エラー。初回登録日より30日経過
            int GMenue = 0;
            key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Royknak");

            //強制解除チェック
            if (key.GetValue(DayV, "") != "")
            {
                if (key.GetValue(DayV, "").ToString() == "11EF9205F852D8")
                {
                    key.Close();
                    return true;
                }
            }

            if (key.GetValue(KeyV, "") == "")
            {
                if (key.GetValue(DayV, "") == "")
                {
                    //初回登録日なし→限定起動
                    GMenue = 1;
                }
                else
                {
                    //データチェック
                    string CCheckD = (key.GetValue(DayV, "").ToString());
                    if (!(Check_First_Date(CCheckD)))
                    {
                        GMenue = 1;
                    }
                }

                if (GMenue == 1)
                {
                    //errmessage.MessageBoxShowWarn("体験版期間が終了しました。\nログイン後、限定メニューが起動します。\nプロテクトの解除を行ってください。");
                    key.Close();
                    return true;
                }
                else
                {
                    key.Close();
                    return true;
                }
            }
            else
            {
                //処理3:
                //LockValueとkeyValueでXOR判定を行い、アプリケーション名と突合する
                int count = 0;

                //①アプリ名取得(Ascii変換)→10進数→2進数に変換
                //アプリ名は、大文字小文字でAsciiコードが異なる為、.ToLower()で小文字変換する
                string menuApp = Path.GetFileName(Application.ExecutablePath).ToLower();
                string NameAppStwo = "";
                while (count < menuApp.Length)
                {
                    NameAppStwo = NameAppStwo + Convert.ToString(((int)menuApp[count]), 2);
                    count = count + 1;
                }

                //②lockvalueを取得(16進数)→【16進数かチェック】→10進数(ToInt32/16)→2進数(ToString/2)に変換
                count = 0;
                string GLockV = (key.GetValue(LockV)).ToString();
                ////16進数チェック
                if (!Regex.IsMatch(GLockV, "^[a-fA-F0-9]+$"))
                {
                    errmessage.MessageBoxShowWarn("ユーザーIDが不正です。\nログイン後、限定メニューが起動します。\n開発元にご連絡ください。");
                    key.Close();
                    return false;
                }                 
                ////2進数変換
                string LockWordStwo = "";
                while (count < GLockV.Length)
                {
                    LockWordStwo = LockWordStwo + Convert.ToString(Convert.ToInt32(GLockV.Substring(count, 1), 16), 2);
                    count = count + 1;
                }
                
                //③keyvalueを取得(16進数)→10進数→2進数に変換
                count = 0;
                string GKeyV = (key.GetValue(KeyV)).ToString();
                ////16進数チェック
                if (!Regex.IsMatch(GKeyV, "^[a-fA-F0-9]+$"))
                {
                    errmessage.MessageBoxShowWarn("解除キーが不正です。\nログイン後、限定メニューが起動します。\nプロテクトの解除を行ってください。");
                    key.Close();
                    return false;
                }
                ////key入れ替え(2341→1234)
                if (GKeyV.Length > 0)
                {
                    GKeyV = GKeyV.Substring(GKeyV.Length-1, 1) + GKeyV.Substring(0, GKeyV.Length - 1);
                }
                ////2進数変換
                string keyOneStwo = "";
                while (count < GKeyV.Length)
                {
                    keyOneStwo = keyOneStwo + Convert.ToString(Convert.ToInt32(GKeyV.Substring(count, 1), 16), 2).PadLeft(4,'0');
                    count = count + 1;
                }

                //④突合チェック
                int CheckKeyErr = 0;
                count = 0;
                int count2 = 0;

                while (count < NameAppStwo.Length)
                {
                    //lockvalue情報が足りなくなったら、頭から読み直す
                    if (count2 == LockWordStwo.Length)
                    {
                        count2 = 0;
                    }
                    //アプリ名(2進)と、lockvalueとkeyvalueのXORが一致しなかったらエラー
                    if (Convert.ToInt32(NameAppStwo[count].ToString()) != ((int)LockWordStwo[count2] ^ (int)keyOneStwo[count]))
                    {
                        CheckKeyErr = 1;
                        break;
                    }
                    count = count + 1;
                    count2 = count2 + 1;
                }
                key.Close();

                if (CheckKeyErr == 1)
                {
                    errmessage.MessageBoxShowWarn("解除キーが正しくありません。\nログイン後、限定メニューが起動します。");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 試用期間のPOP表示>>ログイン後のメニュー画面で、試用期間残数をPOP表示する
        /// 表示対象>>プロテクト実行有且つ、オンプレ且つ、レジストリ書き込みあり、解除キー(強制解除)未登録且つ、体験版期間(初回登録日から30日間)
        /// 表示対象外>>プロテクト実行無し、クラウド、レジストリ書き込み無し、強制解除後、解除キー登録後、初回登録日エラーのいずれか
        /// </summary>
        public static void TrialDisplay()
        {

            MessageBoxShowLogic Trialmessage = new MessageBoxShowLogic();

            //プロテクト実行チェック
            if (!AppConfig.IsProtectRun)
            {
                return;
            }

            //オンプレチェック(クラウド環境は抜ける)
            if (AppConfig.IsTerminalMode)
            {
                return;
            }

            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Royknak");

            //プロテクト設定無し
            if (key == null)
            {
                return;
            }

            //強制解除チェック
            if (key.GetValue(DayV, "") != "")
            {
                if (key.GetValue(DayV, "").ToString() == "11EF9205F852D8")
                {
                    key.Close();
                    return;
                }
            }

            //解除キー登録無し、日付有(正常値)の場合、試用期間の残数を表示する
            if (key.GetValue(KeyV, "") == "")
            {
                if (key.GetValue(DayV, "") != "")
                {
                    //データチェック
                    string CCheckD = (key.GetValue(DayV, "").ToString());
                    if (Check_First_Date(CCheckD))
                    {
                        //体験版期間内なら、残数を表示する
                        if (TrialD > 26)
                        {
                            Trialmessage.MessageBoxShowWarn(string.Format("試用期間 残り {0}日です。", (30 - TrialD)));
                        }
                        else
                        {
                            Trialmessage.MessageBoxShowInformation(string.Format("試用期間 残り {0}日です。", (30 - TrialD)));
                        }
                    }
                }
            }
            key.Close();
            return;

        }

        /// <summary>
        /// 初回登録日のデータチェック
        /// </summary>
        /// <param name="FirstDay"></param>
        /// <returns></returns>
        internal static bool Check_First_Date(string FirstDay)
        {
            MessageBoxShowLogic okmessage = new MessageBoxShowLogic();

            bool CheckFirstDate = true;
            TrialD = 0;

            if (!Regex.IsMatch(FirstDay, "^[a-fA-F0-9]+$"))
            {
                CheckFirstDate = false;
            }
            else
            {
                //16進数→10進数(例：11EF945A0484F8→5048495048525040)
                string strShokaiDay = Convert.ToInt64(FirstDay, 16).ToString();
                if (strShokaiDay.Length == 16) 
                {
                    //文字の入れ替え(Y1M1D1Y3Y2M2D2Y4→Y1Y2Y3Y4M1M2D1D2)
                    //10進→ASCII
                    string firstdateY = ((char)Convert.ToInt32(strShokaiDay.Substring(0, 2))).ToString() + ((char)Convert.ToInt32(strShokaiDay.Substring(8, 2))).ToString() + ((char)Convert.ToInt32(strShokaiDay.Substring(6, 2))).ToString() + ((char)Convert.ToInt32(strShokaiDay.Substring(14, 2))).ToString();
                    string firstdateM = ((char)Convert.ToInt32(strShokaiDay.Substring(2, 2))).ToString() + ((char)Convert.ToInt32(strShokaiDay.Substring(10, 2))).ToString();
                    string firstdateD = ((char)Convert.ToInt32(strShokaiDay.Substring(4, 2))).ToString() + ((char)Convert.ToInt32(strShokaiDay.Substring(12, 2))).ToString();
                    string firstdate = firstdateY + "/" + firstdateM + "/" + firstdateD;
                    //日付判定
                    DateTime dt;
                    DateTime dtNow = Convert.ToDateTime(System.DateTime.Now.Year + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Day);
                    if (DateTime.TryParse(firstdate, out dt))
                    {
                        double span = (dtNow - dt).TotalDays;
                        if (span < 0 || span >= 30)
                        {
                            //体験版期限切れ→限定起動
                            CheckFirstDate = false;
                        }
                        else
                        {
                            TrialD = span;
                        }
                    }
                    else
                    {
                        //初回登録日日付変換エラー→限定起動
                        CheckFirstDate = false;
                    }

                }
                else
                {
                    //16進数→10進数にした際に、16桁以外の場合は、日付変換失敗→限定起動
                    CheckFirstDate = false;
                }
            }               

            return CheckFirstDate;
        }

    }
}
