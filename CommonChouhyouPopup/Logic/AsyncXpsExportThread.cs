using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace CommonChouhyouPopup.Logic
{
    /// <summary>
    /// XPSファイル非同期出力スレッド
    /// XPSファイルの出力処理をバックグラウンドスレッドで非同期に実行する。
    /// </summary>
    /// <
    internal static class AsyncXpsExportThread
    {
        /// <summary>
        /// XPSファイル非同期出力スレッドの開始。アプリ起動時に一度だけ呼び出す。
        /// </summary>
        internal static void Start()
        {
            if (_wokerThread == null)
            {
                var m = System.Reflection.MethodBase.GetCurrentMethod();
                var mName = string.Format("{0}.{1}()", m.DeclaringType.Name, m.Name);
                Debug.WriteLine(mName);

                _requestQueue = new List<CommonChouhyouPopup.App.FormReport>();
                _quitFlag = false;
                _waitEvent = new AutoResetEvent(true);
                _wokerThread = new Thread(new ThreadStart(_workerTreadProc));
                _wokerThread.Start();
            }
        }

        /// <summary>
        /// XPSファイル非同期出力の要求。XPSファイル生成ごとに呼び出す。
        /// </summary>
        /// <param name="formReport">XPS出力したいFormReportオブジェクト</param>
        internal static void AddRequest(CommonChouhyouPopup.App.FormReport formReport)
        {
            Start();

            bool isUnProcessed = true;

            while (isUnProcessed)
            {
                // リクエストとXPS生成処理とで、リクエスト処理の方が速度が速いため
                // 捌ききれずにメモリにどんどん溜まっていってしまうため対策
                if (_wokerThread != null)
                {
                    if (_requestQueue.Count < 100)
                    {
                        lock (_requestQueue)
                        {
                            _requestQueue.Add(formReport);
                            _waitEvent.Set();
                            isUnProcessed = false;
                        }
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        /// <summary>
        /// XPSファイル非同期出力の中断。連続印刷中断時に呼び出す。
        /// </summary>
        public static void Abort()
        {
            var m = System.Reflection.MethodBase.GetCurrentMethod();
            var mName = string.Format("{0}.{1}()", m.DeclaringType.Name, m.Name);
            Debug.WriteLine(mName);

            if (_wokerThread != null)
            {
                _abortFlag = true;
                _waitEvent.Set();
                while (_abortFlag) Thread.Sleep(100);
            }
        }

        /// <summary>
        /// XPSファイル非同期出力スレッドの終了。アプリ終了時に一度だけ呼び出す。
        /// </summary>
        public static void Terminate()
        {
            var m = System.Reflection.MethodBase.GetCurrentMethod();
            var mName = string.Format("{0}.{1}()", m.DeclaringType.Name, m.Name);
            Debug.WriteLine(mName);

            if (_wokerThread != null)
            {
                _quitFlag = true;
                _waitEvent.Set();
                Debug.WriteLine(mName + " +join");
                _wokerThread.Join();
                Debug.WriteLine(mName + " -join");
                _waitEvent.Close();
                _wokerThread = null;
            }
        }

        /// <summary>
        /// リクエストキュー(FormReportのリスト)
        /// </summary>
        private static List<CommonChouhyouPopup.App.FormReport> _requestQueue;

        /// <summary>
        /// ワーカースレッド
        /// </summary>
        private static Thread _wokerThread = null;

        /// <summary>
        /// スレッド待機イベント（自動リセットイベント）
        /// </summary>
        private static AutoResetEvent _waitEvent;

        /// <summary>
        /// スレッド脱出フラグ
        /// </summary>
        private static bool _quitFlag;

        /// <summary>
        /// 中断フラグ
        /// </summary>
        private static bool _abortFlag;

        /// <summary>
        /// XPSファイル非同期出力スレッド本体
        /// </summary>
        private static void _workerTreadProc()
        {
            var m = System.Reflection.MethodBase.GetCurrentMethod();
            var mName = string.Format("{0}.{1}()", m.DeclaringType.Name, m.Name);
            Debug.WriteLine("+" + mName);

            while (!_quitFlag)
            {
                Debug.WriteLine(" {0} {1}", mName, System.Environment.CurrentDirectory, 0);

                CommonChouhyouPopup.App.FormReport formReport = null;
                lock (_requestQueue)
                {
                    if (_requestQueue.Count > 0)
                    {
                        formReport = _requestQueue.First();
                        if (formReport != null)
                        {
                            _requestQueue.Remove(formReport);
                        }
                    }
                }
                if (formReport != null)
                {
                    try
                    {
                        if (_abortFlag)
                        {
                            Debug.WriteLine(" {0} Ignore: {1}", mName, formReport.Caption, 0);
                        }
                        else
                        {
                            Debug.WriteLine(" {0} Export: {1}", mName, formReport.Caption, 0);
                            formReport.PrintXPS(false); // false:同期モード
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(" {0} Exception: {1}", mName, ex, 0);
                    }
                }
                else
                {
                    Debug.WriteLine(" {0} +WaitOne()", mName, 0);
                    _abortFlag = false;
                    _waitEvent.WaitOne();
                    Debug.WriteLine(" {0} -WaitOne()", mName, 0);
                }
            }
            Debug.WriteLine("-" + mName);
        }
    }
}
