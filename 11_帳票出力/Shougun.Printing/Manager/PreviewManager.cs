using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shougun.Printing.Viewer;

namespace Shougun.Printing.Manager
{
    /// <summary>
    /// プレビューマネージャ。XPSファイルのプレビュー画面を管理する。
    /// </summary>
    internal class PreviewManager
    {
        public static PreviewManager Instance
        {
            get
            {
                return PreviewManager.singleton;
            }
        }
        private static PreviewManager singleton = new PreviewManager();

        /// <summary>
        /// ビューアリスト。最大5つまで。
        /// </summary>
        public List<XpsViewer> Items {get; private set;}
        
        /// <summary>
        /// プレビューマネージャのコンストラクタ
        /// </summary>
        private PreviewManager()
        {
            this.Items = new List<XpsViewer>();
        }

        /// <summary>
        /// ビューアからアプリケーションメインウインドウへのコマンド中継イベント
        /// </summary>
        public event CommandEventHandler CommandEventRelayHandler = null;

        /// <summary>
        /// ビューアからのイベントコマンドハンドラ。
        /// アプリケーションメインウインドウへのハンドラが登録されていれば中継する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="command"></param>
        private void onViewerCommand(object sender, string command)
        {
            var xpsViewer = sender as XpsViewer;
            if (xpsViewer != null && this.Items.Contains(xpsViewer))
            {
                if (command.Equals("Closed"))
                {
                    this.Items.Remove(xpsViewer);
                }

                if (this.CommandEventRelayHandler != null)
                {
                    this.CommandEventRelayHandler(xpsViewer, command);
                }
            }
        }
        /// <summary>
        /// 指定されたXPSファイルのプレビューを表示する。
        /// </summary>
        /// <param name="xpsFile"></param>
        public void Open(string path, string title)
        {
            // 既にプレビュー中なら最前面表示して終わり
            foreach (var xpsViewer in this.Items)
            {
                if (xpsViewer.XpsPath.CompareTo(path) == 0)
                {
                    xpsViewer.Show();
                    return;
                }
            }

            // プレビュー中のものが既にたくさんある場合はもっとも古いものを閉じる
            if (this.Items.Count >= 5)
            {
                this.Close(this.Items[0]);
            }

            // 新しいプレビューを作成して表示
            if (System.IO.File.Exists(path))
            {
                var xpsViewer = new XpsViewer();
                xpsViewer.ViewXpsFile(path, title);
                xpsViewer.CommandEventHandler += this.onViewerCommand;
                xpsViewer.Show();
                this.Items.Add(xpsViewer);
            }
        }

        /// <summary>
        /// 指定されたビューアのプレビューを閉じる。
        /// </summary>
        /// <param name="xpsFile"></param>
        public void Close(XpsViewer xpsViewer)
        {
            if (xpsViewer != null)
            {
                xpsViewer.Close();
                if (this.Items.Contains(xpsViewer))
                {
                    this.Items.Remove(xpsViewer);
                }
            }
        }

        /// <summary>
        /// 指定されたXPSファイルのプレビューを閉じる。
        /// </summary>
        /// <param name="xpsFile"></param>
        public void Close(string path)
        {
            var xpsViewer = this.Items.Find(v => v.XpsPath.CompareTo(path) == 0);
            if (xpsViewer != null)
            {
                xpsViewer.CommandEventHandler -= this.onViewerCommand;
                xpsViewer.Close();
                this.Items.Remove(xpsViewer);
            }
        }

        /// <summary>
        /// オープン中の全てのプレビューを閉じる（アプリ終了時に呼び出す）
        /// </summary>
        public void CloseAll()
        {
            // イベントハンドラをマスクして全てのビューアを閉じる
            if (Items != null)
            {
                foreach (var xpsViewer in this.Items)
                {
                    xpsViewer.CommandEventHandler -= this.onViewerCommand;
                    xpsViewer.Close();
                }
                this.Items.RemoveRange(0, this.Items.Count);
            }
        }

        /// <summary>
        /// 既にオープン済みのプレビューの内容を差し替える。
        /// </summary>
        /// <param name="xpsViewer"></param>
        /// <param name="xpsFile"></param>
        public void Replace(XpsViewer xpsViewer, string path, string title)
        {
            this.Close(path);
            xpsViewer.ViewXpsFile(path, title);
            xpsViewer.Show();
        }

        /// <summary>
        /// 指定されたXPSファイルが今プレビュー中かどうか調べる。
        /// </summary>
        /// <param name="xpsFile"></param>
        /// <returns></returns>
        public bool IsPreviewing(string path)
        {
            return this.Items.Exists(v => v.XpsPath.CompareTo(path) == 0);
        }

        public XpsViewer FindItemByXpsFilePath(string path)
        {
            return this.Items.Find(v => v.XpsPath.CompareTo(path) == 0);
        }

        public bool HasActiveSomeone()
        {
            foreach(var xpsViewer in this.Items)
            {
                if (xpsViewer.IsAcive)
                {
                    return true;
                }
            }
            return false;
        }

        public void BlockPreviewing(bool block)
        {
            foreach(var xpsViewer in this.Items)
            {
                xpsViewer.Block(block);
            }
        }
    }
}
