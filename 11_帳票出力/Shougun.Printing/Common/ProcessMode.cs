namespace Shougun.Printing.Common
{
    /// <summary>
    /// 環境将軍R印刷管理/処理モード列挙型
    /// </summary>
    public enum ProcessMode
    {
        /// <summary>
        /// 未設定
        /// Initializerで設定される。
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// オンプレミス/将軍モード。将軍本体アプリから実行。
        /// 印刷設定画面は将軍本体から呼び出される。
        /// </summary>
        OnPremisesFrontProcess,

        /// <summary>
        /// オンプレミス/印刷モード。バックグラウンドプロセスShougun.Printing.Clirntから実行。
        /// （バックグラウンドプロセスのメインウインドウは非表示、アイコンも非表示）
        /// 印刷画面はバックグラウンドプロセスで実行。
        /// </summary>
        OnPremisesBackProcess,

        /// <summary>
        /// クラウド/サーバ側モード。将軍本体アプリから実行。
        /// 将軍からサーバ用印刷設定画面（リダイレクトドライブの出力先先設定）を呼び出す。
        /// </summary>
        CloudServerSideProcess,

        /// <summary>
        /// クラウド/クライアント側モード。バックグラウンドプロセスShougun.Printing.Clirntから実行。
        /// （バックグラウンドプロセスのメインウインドウは非表示、タスクトレイにアイコンを表示）
        /// 印刷設定画面も印刷監視画面もバックグラウンドプロセス上で実行される。
        /// </summary>
        CloudClientSideProcess,
    }
}
