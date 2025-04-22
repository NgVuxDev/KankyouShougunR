using System;
using System.Text;
using System.IO;
using System.Configuration;
using System.Reflection;

namespace Shougun.Printing.Verup
{
    internal class VerupServerProc
    {
        public void Run()
        {
            try
            {
                VerupForm.WriteLine("クライアント印刷プログラム更新プログラム");
                VerupForm.WriteLine("    {0}", Assembly.GetExecutingAssembly().Location);

                VerupForm.WriteLine("プログラムインストールディレクトリ(サーバー側)");
                string serverProgramDir = VerupIO.GetLocalApplicationDirectory();
                VerupForm.WriteLine("    {0}", serverProgramDir);

                VerupForm.WriteLine("サーバー側リビジョン番号");
                int serverRev = VerupIO.ReadRevisionFile(serverProgramDir);
                VerupForm.WriteLine("    {0}", serverRev);


                VerupForm.WriteLine("設定ファイルパス(サーバー側)");
                string serverSettingsPath = VerupIO.GetServerPrintSettingsFilePath();
                VerupForm.WriteLine("    {0}", serverSettingsPath);

                VerupForm.WriteLine("バージョンアップ用ディレクトリ(クライアント側)");
                string clientVerupDir = VerupIO.GetClientVerupDirectory();
                VerupForm.WriteLine("    {0}", clientVerupDir);

                VerupForm.WriteLine("クライアント側リビジョン番号");
                int clientRev = VerupIO.ReadRevisionFile(clientVerupDir);
                VerupForm.WriteLine("    {0}", clientRev);

                VerupForm.WriteLine("バージョンアップ用ディレクトリのファイルを削除");
                VerupIO.DeleteFiles(clientVerupDir);

                VerupForm.WriteLine("更新ファイル");
                var fileList = VerupIO.GetDirectoryFilesList(serverProgramDir, "Shougun.Printing.*.*");
                foreach (var file in fileList)
                {
                    VerupForm.WriteLine("    {0}", file);
                }

                VerupForm.WriteLine("更新ファイルのコピー");
                VerupIO.CopyFiles(serverProgramDir, clientVerupDir, fileList);

                VerupForm.WriteLine("更新フラグファイルの作成");
                var printingDirectory = VerupIO.GetClientPrintingDirectory();
                VerupIO.CreateVerupFlagFile(printingDirectory);

                VerupForm.WriteLine("更新に成功しました。");
                VerupForm.Complete(true);
            }
            catch (Exception ex)
            {
                VerupForm.WriteLine("更新に失敗しました。");
                VerupForm.WriteLine("{0}", ex.Message);
                if (ex.InnerException != null)
                {
                    VerupForm.WriteLine("{0}", ex.InnerException.Message);
                }
                VerupForm.Complete(false);
            }
        }
    }
}
