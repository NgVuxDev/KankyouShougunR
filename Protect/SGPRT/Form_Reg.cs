using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace SGPRT
{
    public partial class Form_Reg : Form
    {
        public Form_Reg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初期表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Reg_Load(object sender, EventArgs e)
        {

            //ロック・キーバリューを初期表示
            this.Protect_Key.Text = "";
            this.Protect_Lock.Text = "";

            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Royknak");
            if (key != null)
            {
                this.Protect_Lock.Text = key.GetValue(ShougunProtect.Program.LockV, "").ToString();
                this.Protect_Key.Text = key.GetValue(ShougunProtect.Program.KeyV, "").ToString();
                key.Close();
            }
            
        }

        /// <summary>
        /// クリップボードにコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            string text = this.Protect_Lock.Text;

            if (text == "") { return; }

            Clipboard.SetText(text);
        }

        /// <summary>
        /// キー情報をレジストリに書き込む
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run_Click(object sender, EventArgs e)
        {

            //入力チェック
            if (this.Protect_Lock.Text == "")
            {
                MessageBox.Show("ユーザーIDが登録されていません。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.Protect_Key.Text == "")
            {
                MessageBox.Show("解除キーを入力してください。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(this.Protect_Key.Text, "^[a-fA-F0-9]+$"))
            {
                MessageBox.Show("解除キーが正しくありません。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //管理者権限で起動しているかチェック(exeを直接起動された時の為のチェック
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();      //現在のユーザーを取得
            var principal = new System.Security.Principal.WindowsPrincipal(identity);   //identityを使って持つ権限などを確認できる
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator) == false)    //Administrator権限を持っているかを判断する 
            {
                MessageBox.Show("管理者権限で起動してください。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //将軍レジストリを開く
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Royknak");
            try
            {
                //KEYVALUE登録
                key.SetValue(ShougunProtect.Program.KeyV, this.Protect_Key.Text);            //キー情報
                MessageBox.Show("解除キーを書き込みました。","確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                key.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                //起動確認ダイアログでいいえを選択するとエラーになる為。
                MessageBox.Show("解除キーの書込みに失敗しました。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                key.Close();
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 強制解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Reg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.Alt && e.KeyCode == Keys.PageUp)
            {
                //管理者権限で起動しているかチェック(exeを直接起動された時の為のチェック
                var identity = System.Security.Principal.WindowsIdentity.GetCurrent();      //現在のユーザーを取得
                var principal = new System.Security.Principal.WindowsPrincipal(identity);   //identityを使って持つ権限などを確認できる
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator) == false)    //Administrator権限を持っているかを判断する 
                {
                    MessageBox.Show("管理者権限で起動してください。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //将軍レジストリを開く
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Royknak");
                try
                {
                    string strlockvalue = "";
                    Guid guidValue = Guid.NewGuid();                    
                    if (key.GetValue(ShougunProtect.Program.LockV, "") == "")
                    {
                        //■LOCKVALUE
                        strlockvalue = guidValue.ToString("N").Substring(0, 8) + guidValue.ToString("N").Substring(12, 4) + guidValue.ToString("N").Substring(20, 12);
                        key.SetValue(ShougunProtect.Program.LockV, strlockvalue);   //GUIDの1,3,5ブロックの文字列を連結(24桁)
                    }
                    else
                    {
                        strlockvalue = key.GetValue(ShougunProtect.Program.LockV, "").ToString();
                    }

                    //■初回登録日(強制解除したら、2020/01/01で登録
                    key.SetValue(ShougunProtect.Program.DayV, "11EF9205F852D8");
          
                    //■キー情報
                    string strkeyvalue = guidValue.ToString("N").Substring(0, 20);
                    key.SetValue(ShougunProtect.Program.KeyV, strkeyvalue);

                    MessageBox.Show("強制解除しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    key.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    //起動確認ダイアログでいいえを選択するとエラーになる為。
                    MessageBox.Show("強制解除に失敗しました。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    key.Close();
                }
            }
        }
    }
}
