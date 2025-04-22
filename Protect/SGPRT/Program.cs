using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGPRT
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string a = Environment.CommandLine;　//→"C:\～\SGPL.exe" /L
            string b = "";
            b = a.Substring(a.Length - 2, 2);
            if (b == "/L")
            {
                //プロテクト登録はプログラム処理
                ProtectLockEntry();
            }
            else if (b == "/K")
            {
                //解除キー登録は、フォームを開く
                Application.Run(new Form_Reg());
            }
            else
            {
                //なんもしない
            }
        }

        /// <summary>
        /// プロテクト登録
        /// </summary>
        static void ProtectLockEntry()
        {
            //管理者権限で起動しているかチェック(exeを直接起動された時の為のチェック
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();      //現在のユーザーを取得
            var principal = new System.Security.Principal.WindowsPrincipal(identity);   //identityを使って持つ権限などを確認できる
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator) == false)    //Administrator権限を持っているかを判断する 
            {
                MessageBox.Show("管理者権限で起動してください。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //GUID取得
            //Guid guidValue = Guid.NewGuid();
            //guidValue.ToString("N")   //N:32桁、D:ハイフン、B:{}とハイフン、P:()とハイフン
            //MessageBox.Show(guidValue.ToString("N").Substring(20, 12)); //0,8/8,4/12,4/16,4/20,12


            //将軍レジストリを開く
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Royknak");
            try
            {
                //■LOCKVALUE
                Guid guidValue = Guid.NewGuid();
                string strlockvalue = guidValue.ToString("N").Substring(0, 8) + guidValue.ToString("N").Substring(12, 4) + guidValue.ToString("N").Substring(20, 12);
                key.SetValue(ShougunProtect.Program.LockV, strlockvalue);   //GUIDの1,3,5ブロックの文字列を連結(24桁)

                //■初回登録日(システム日付取得→文字列入替→Ascii変換→16進数変換
                //データがある時は更新しない
                if (key.GetValue(ShougunProtect.Program.DayV, "") == "")
                {
                    string firsty = DateTime.Now.ToString("yyyy");
                    string firstm = DateTime.Now.ToString("MM");
                    string firstd = (DateTime.Now.ToString("dd")).PadLeft(2, '0');
                    //文字列入替(Y1Y2Y3Y4M1M2D1D2→Y1M1D1Y3Y2M1D1Y4)
                    string firstdate = firsty.Substring(0, 1) + firstm.Substring(0, 1) + firstd.Substring(0, 1) + firsty.Substring(2, 1)
                            + firsty.Substring(1, 1) + firstm.Substring(1, 1) + firstd.Substring(1, 1) + firsty.Substring(3, 1);

                    int count = 0;
                    string FirstDateS = "";

                    while (count < firstdate.Length)
                    {
                        //ASCII→10進数
                        FirstDateS = FirstDateS + ((int)firstdate[count]);
                        count = count + 1;
                    }
                    //10進数→16進数
                    FirstDateS = Convert.ToString((long.Parse(FirstDateS)), 16);
                    key.SetValue(ShougunProtect.Program.DayV, FirstDateS);
                }
                //■キー情報
                key.SetValue(ShougunProtect.Program.KeyV, "");
            }
            catch (Exception ex)
            {
                //起動確認ダイアログでいいえを選択するとエラーになる為。
                MessageBox.Show("ユーザーIDの登録に失敗しました。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            key.Close();

        }

    }
}
