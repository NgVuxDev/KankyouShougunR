using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;


namespace Shougun.Printing.Verup
{
    internal class VerupClientProc
    {
        public int WaitClientProcessid = 0;
        public string ProgramDir = null;
        
        public void Run()
        {
            bool success = false;
            bool rollback = false;
            string printingDirectory = null;
            string backupDir = null;
            try
            {
                VerupForm.WriteLine("クライアント印刷プログラム更新プログラム");
                VerupForm.WriteLine("    {0}", Assembly.GetExecutingAssembly().Location);

                VerupForm.WriteLine("クライアント印刷プログラムの終了を待機");
                this.waitSyncPrintingClientProcess();

                VerupForm.WriteLine("バージョンアップ用ディレクトリ");
                string verupDir = VerupIO.GetLocalVerupDirectory();
                VerupForm.WriteLine("    {0}", verupDir);

                printingDirectory = Path.Combine(Path.GetDirectoryName(verupDir), "Printing");
                if (!VerupIO.ExistsVerupFlagFile(printingDirectory))
                {
                    throw new Exception("更新フラグファイルが見つかりませんでした\r\n" + printingDirectory);
                }

                VerupForm.WriteLine("バージョンアップ用リビジョン番号");
                int serverRev = VerupIO.ReadRevisionFile(verupDir);
                VerupForm.WriteLine("    {0}", serverRev);
                
                VerupForm.WriteLine("プログラムインストールディレクトリ");
                VerupForm.WriteLine("    {0}", ProgramDir);

                VerupForm.WriteLine("現在のリビジョン番号");
                int clientRev = VerupIO.ReadRevisionFile(ProgramDir);
                VerupForm.WriteLine("    {0}", clientRev);


                VerupForm.WriteLine("更新ファイル");
                var verupFiles = VerupIO.GetDirectoryFilesList(verupDir);
                foreach (var file in verupFiles)
                {
                    VerupForm.WriteLine("    {0}", file);
                }

                VerupForm.WriteLine("バックアップディレクトリのファイルを削除");
                backupDir = Path.Combine(verupDir, "Backup");
                VerupForm.WriteLine("    {0}", backupDir);
                VerupIO.DeleteFiles(backupDir);

                VerupForm.WriteLine("インストールフォルダの更新前ファイルをバックアップにコピー");
                VerupIO.CopyFiles(ProgramDir, backupDir, verupFiles, true); // コピー元が存在しなくてもエラーにしない

                rollback = true;

                VerupForm.WriteLine("インストールフォルダの更新前ファイルを削除");
                VerupIO.DeleteFiles(ProgramDir, verupFiles);

                VerupForm.WriteLine("インストールフォルダに更新ファイルをコピー");
                VerupIO.CopyFiles(verupDir, ProgramDir, verupFiles);

                rollback = false;

                VerupForm.WriteLine("更新に成功しました。");
                success = true;
            }
            catch ( Exception ex)
            {
                VerupForm.WriteLine("更新に失敗しました。");
                VerupForm.WriteLine("エラー:{0}", ex.Message);
                if (ex.InnerException != null)
                {
                    VerupForm.WriteLine("{0}", ex.InnerException.Message);
                }

            }

            if (rollback)
            {
                VerupForm.WriteLine("バックアップから復元中...");
                try
                {
                    VerupIO.CopyFiles(backupDir, ProgramDir);
                    VerupForm.WriteLine("復元成功");
                }
                catch (Exception ex)
                {
                    VerupForm.WriteLine("復元に失敗しました。");
                    VerupForm.WriteLine("エラー:{0}", ex.Message);
                }
            }

            if (backupDir != null)
            {
                try
                {
                    VerupForm.WriteLine("バックアップディレクトリのファイルを削除");
                    VerupForm.WriteLine("    {0}", backupDir);
                    VerupIO.DeleteFiles(backupDir);

                    VerupForm.WriteLine("バックアップディレクトリを削除");
                    Directory.Delete(backupDir);
                }
                catch (Exception ex)
                {
                    VerupForm.WriteLine("エラー:{0}", ex.Message);
                }
            }

            try
            {
                if (!success)
                {
                    VerupForm.WriteLine("リビジョンファイルの削除");
                    File.Delete(Path.Combine(VerupIO.GetLocalVerupDirectory(), VerupIO.RevisionFileName));
                }

                if (VerupIO.ExistsVerupFlagFile(printingDirectory))
                {
                    VerupForm.WriteLine("更新フラグファイルの削除");
                    VerupIO.DeleteVerupFlagFile(printingDirectory);
                }
            }
            catch (Exception ex)
            {
                VerupForm.WriteLine("エラー:{0}", ex.Message);
            }

            if (ProgramDir != null)
            {
                VerupForm.WriteLine("クライアント印刷プログラムの起動");
                VerupForm.WriteLine("    {0}", Path.Combine(ProgramDir, VerupIO.ClientProgramName));
                try
                {
                    Launcher.LaunchBackgroundPrintingProcess(ProgramDir, true, false);
                    VerupForm.WriteLine("起動しました。");
                }
                catch (Exception ex)
                {
                    VerupForm.WriteLine("エラー:{0}", ex.Message);
                }
            }

            VerupForm.Complete(success);
        }

        private void waitSyncPrintingClientProcess()
        {
            for (int i = 0; i < 30; i++)
            {
                try
                {
                    var process = Process.GetProcessById(this.WaitClientProcessid);
                    if (process.HasExited)
                    {
                        return;
                    }

                    System.Threading.Thread.Sleep(1000);
                }
                catch (ArgumentException)
                {
                    return;
                }
            }

            throw new Exception("クライアント印刷プログラムの終了を検出できませんでした。");
        }
    }
}
