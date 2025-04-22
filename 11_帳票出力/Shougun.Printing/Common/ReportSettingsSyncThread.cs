using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace Shougun.Printing.Common
{
    /// <summary>
    /// クラウド環境のサーバ側プロセスのための帳票設定ファイル同期スレッド
    /// クライアント側の帳票設定ファイルの更新を監視しサーバ側の設定ファイルに同期させる。
    /// サーバ側でXPSファイル作成時に帳票設定の余白設定値が必要になるが、
    /// クライアント側のリダイレクトドライブから読み込むと非常に遅くなるので、
    /// このスレッドでバックグラウンドでサーバ側に同期し、余白設定の読み込みは
    /// サーバ側で行うようにする。
    /// </summary>
    internal class ReportSettingsSyncThread
    {
        // クラウド上でDUBUG版のときのみ有効なデバッグメッセージ出力ウインドウ
        static UI.DebugMessageWindow debugw = null;

        // シングルトンパターン
        static private ReportSettingsSyncThread instance = null;

        /// <summary>
        /// 帳票設定ファイルの同期監視開始
        /// </summary>
        static public void Start()
        {
            if (ReportSettingsSyncThread.instance == null)
            {
                // デバッグメッセージ出力ウインドウ作成
                // falseでDEBUGシンボル有効時だけウインドウ生成する、trueで必ず生成する。
                ReportSettingsSyncThread.debugw = new UI.DebugMessageWindow("ReportSettingsSyncThread", false); 
                
                // このクラスのインスタンス作成
                ReportSettingsSyncThread.instance = new ReportSettingsSyncThread();
            }
            ReportSettingsSyncThread.instance.start();
        }

        /// <summary>
        /// 帳票設定ファイルの同期監視停止
        /// </summary>
        static public void Stop()
        {
            if (ReportSettingsSyncThread.instance != null)
            {
                ReportSettingsSyncThread.instance.stop();
            }
        }

        /// <summary>
        /// 割り込み中断制御、trueで中断、falseで再開。
        /// 連続印刷の開始時にtrueで、連続印刷の終了時にfalseで呼ばれる。
        /// AbortPrinting.BeginSequence() -> Interrupt(true)
        /// AbortPrinting.TerminateSequence() -> Interrupt(false)
        /// </summary>
        /// <param name="doInterrupt"></param>
        static public void Interrupt(bool doInterrupt)
        {
            if (ReportSettingsSyncThread.instance != null)
            {
                // フラグがtrueならスレッドは設定ファイルの更新チェックをしない
                // (しかかっているものは可能な限り早めに切りいいところで中断する)
                ReportSettingsSyncThread.instance.interruptFlag = doInterrupt;
            }
        }

        private Thread wokerThread; // ワーカースレッド
        private ManualResetEvent quitEvent; // スレッド終了イベント
        private bool quitFlag; // スレッド関数脱出フラグ
        private bool interruptFlag; // 割り込み中断フラグ（フラグが落ちたら再開する）
        private string srcDir; // 同期元ディレクトリ（クライアント側）
        private string dstDir; // 同期先ディレクトリ（）

        /// <summary>
        /// 帳票設定ファイル同期スレッドクラスのコンストラクタ
        /// </summary>
        private ReportSettingsSyncThread()
        {
            this.wokerThread = null;
            this.quitEvent = null;
            this.quitFlag = false;
            this.interruptFlag = false;
            this.srcDir = null;
            this.dstDir = null;
        }

        /// <summary>
        /// 同期監視の開始
        /// </summary>
        private void start()
        {
            stop();
            
            this.quitFlag = false;
            this.quitEvent = new ManualResetEvent(false);
            
            this.srcDir = ServerSettings.GetClientSettingsDirectory();
            this.dstDir = ServerSettings.GetServerSettingsDirectory();
            try
            {
                Directory.CreateDirectory(this.dstDir);
            }
            catch (Exception e)
            {
                debugw.WriteLine("Exception:{0}", e.ToString(), 0);
            }

            if (Directory.Exists(this.srcDir) && Directory.Exists(this.dstDir))
            {
                this.wokerThread = new Thread(new ThreadStart(this.workerTreadProc));
                this.wokerThread.Start();
                while (!this.wokerThread.IsAlive) ;
            }
        }

        /// <summary>
        /// 同期監視の停止
        /// </summary>
        private void stop()
        {
            if (this.wokerThread == null)
            {
                return ;
            }
            debugw.WriteLine("監視スレッドの停止待ち");
            this.quitFlag = true;
            this.quitEvent.Set();
#if DEBUG
            while (this.quitFlag) Application.DoEvents();
#endif
            this.wokerThread.Join();
            this.wokerThread = null;
            this.quitEvent.Close();
            this.quitEvent = null;
            debugw.WriteLine("監視スレッドの停止完了");
        }

        /// <summary>
        /// ワーカースレッド関数
        /// </summary>
        private void workerTreadProc()
        {
            debugw.WriteLine("監視を開始します");
            debugw.WriteLine("同期元:" + this.srcDir);
            debugw.WriteLine("同期先:" + this.dstDir);

            int count = 0;

            while (!this.quitFlag)
            {
                if (!this.interruptFlag)
                {
                    debugw.WriteLine("監視{0}回目", ++count);
                    try
                    {
                        this.syncSettingsFilesProc();
                    }
                    catch (Exception e)
                    {
                        debugw.WriteLine("Exception:{0}", e.ToString(), 0);
                    }
                }

                // タイムアウトまで待機することで一定周期の監視を繰り返す。
                // ただし終了時はシグナル状態になりすぐに待機から抜ける。
                this.quitEvent.WaitOne(5000); 
            }

            debugw.WriteLine("監視を終了します");
            this.quitFlag = false;
        }

        /// <summary>
        /// ２つのディレクトリ間で帳票設定ファイルを同期する
        /// </summary>
        private void syncSettingsFilesProc()
        {
            // 同期元と同期先の各ディレクトリ内のファイルをFileInfoリストで取得
            var srcFilesList = this.getFileInfoList(this.srcDir);
            var dstFilesList = this.getFileInfoList(this.dstDir);

            // 同期元と同じ名前と更新時刻のものが同期先になければ同期元から同期先にコピーする
            foreach (var sf in srcFilesList)
            {
                if (this.quitFlag || this.interruptFlag) break;
                
                if (dstFilesList.Count(df => df.Name == sf.Name 
                        && df.LastWriteTime == sf.LastWriteTime) == 0)
                {
                    debugw.WriteLine("copy:{0}", sf.Name, 0);
                    sf.CopyTo(Path.Combine(this.dstDir, sf.Name), true);
                }
            }

            // 同期先と同じ名前のものが同期元になければ同期先から削除する
            foreach (var df in dstFilesList)
            {
                if (this.quitFlag || this.interruptFlag) break;

                if (srcFilesList.Count(sf => sf.Name == df.Name) == 0)
                {
                    debugw.WriteLine("delete:{0}", df.Name, 0);
                    df.Delete();
                }
            }
        }
        /// <summary>
        /// 指定したディレクトリ内の帳票設定ファイルのFileInfoリストを取得する
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private List<FileInfo> getFileInfoList(string dir)
        {
            var paths = Directory.EnumerateFiles(dir, "ReportSettings_*");
            var list = new List<FileInfo>();
            foreach (var path in paths)
            {
                if (this.quitFlag || this.interruptFlag) break;

                list.Add(new FileInfo(path));
            }
            return list;
        }
    }
}
