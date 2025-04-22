using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration; // 参照にSystem.Configration.Installが必要
using Shougun.Printing.Verup;

[System.ComponentModel.RunInstaller(true)]
public class Installer : System.Configuration.Install.Installer
{
    protected override void OnBeforeInstall(System.Collections.IDictionary stateSaver)
    {
        base.OnBeforeInstall(stateSaver);
        
        var processes = Process.GetProcessesByName("Shougun.Printing.Client");
        if (processes.Length > 0)
        {
            string message = "クライアント印刷プログラムが実行中です。インストール前に印刷プログラムを終了します";
            MessageBox.Show(message, "クライアント印刷プログラム - インストーラ", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            Launcher.TerminateBackgroundPrintingProcess();
        }
    }

    protected override void OnCommitted(System.Collections.IDictionary savedState)
    {
        base.OnCommitted(savedState);

        string message = "クライアント印刷プログラムを起動します。よろしいですか？\r\n「いいえ」を選択した場合は後でスタートメニューから起動してください。";
        var result = MessageBox.Show(message, "クライアント印刷プログラム - インストーラ", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);  
        if (result == DialogResult.Yes)
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            Launcher.LaunchBackgroundPrintingProcess(directory, true, false);
        }
    }
}