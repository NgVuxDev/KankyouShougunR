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
    /// XPSファイル非同期転送スレッド
    /// XPSファイルの転送処理をバックグラウンドスレッドで非同期に実行する。
    /// クラウドの場合は一時作成XPSファイルをサーバー側ドライブからクライアント側のリダイレクトドライブに移動する。
    /// オンプレの場合は一時作成XPSファイルをリネームする。
    /// </summary>
    /// <
    internal static class AsyncXpsTransferThread
    {
        /// <summary>
        /// XPSファイル非同期転送スレッドの開始。アプリ起動時に一度だけ呼び出す。
        /// </summary>
        internal static void Start()
        {
            if (_wokerThread == null)
            {
                var m = System.Reflection.MethodBase.GetCurrentMethod();
                var mName = string.Format("{0}.{1}()", m.DeclaringType.Name, m.Name);
                Debug.WriteLine(mName);
                
                _requestQueue = new List<string>();
                _quitFlag = false;
                _waitEvent = new AutoResetEvent(true);
                _wokerThread = new Thread(new ThreadStart(_workerTreadProc));
                _wokerThread.Start();
            }
        }

        /// <summary>
        /// XPSファイル非同期転送の要求。XPSファイル生成ごとに呼び出す。
        /// </summary>
        /// <param name="src">転送元パス名</param>
        /// <param name="dest">転送先パス名</param>
        internal static void AddRequest(string src, string dest)
        {
            Start();

            if (_wokerThread != null)
            {
                lock (_requestQueue)
                {
                    _requestQueue.Add(src + ',' + dest);
                    _waitEvent.Set();
                }
            }
        }

        /// <summary>
        /// XPSファイル非同期転送の中断。連続印刷中断時に呼び出す。
        /// </summary>
        internal static void Abort()
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
        /// XPSファイル非同期転送スレッドの終了。アプリ終了時に一度だけ呼び出す。
        /// </summary>
        internal static void Terminate()
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
        /// リクエストキュー("転送元,転送先"のリスト)
        /// </summary>
        private static List<string> _requestQueue;
        
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
        /// XPSファイル非同期転送スレッド本体
        /// </summary>
        private static void _workerTreadProc()
        {
            var m = System.Reflection.MethodBase.GetCurrentMethod();
            var mName = string.Format("{0}.{1}()", m.DeclaringType.Name, m.Name);
            Debug.WriteLine("+" + mName);

            while (!_quitFlag)
            {
                Debug.WriteLine(" {0} {1}", mName, System.Environment.CurrentDirectory, 0);

                string request = null;
                lock (_requestQueue)
                {
                    if (_requestQueue.Count > 0)
                    {
                        request = _requestQueue.First();
                        if (request != null)
                        {
                            _requestQueue.Remove(request);
                        }
                    }
                }
                if (request != null)
                {
                    var split = request.Split(',');
                    try
                    {
                        if (_abortFlag)
                        {
                            Debug.WriteLine(" {0}  Delete {1}", mName, request, 0);
                            File.Delete(split[0]);
                        }
                        else
                        {
                            Debug.WriteLine(" {0} Move {1}", mName, request, 0);
                            File.Move(split[0], split[1]);
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
