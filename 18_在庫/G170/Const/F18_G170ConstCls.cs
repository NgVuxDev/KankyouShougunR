
namespace Shougun.Core.Stock.ZaikoShimeSyori.Const
{
    class F18_G170ConstCls
    {
        /// <summary>評価方法</summary>
        public static readonly string[] ZAIKO_HYOUKA_HOUHOU = {
                                                        "",
                                                        "総平均",
                                                        "移動平均",
                                                        "FIFO",
                                                        "最終仕入",
                                                        "在庫基準単価"
                                                      };
        /// <summary>評価方法：1.総平均</summary>
        public const int ZAIKO_HYOUKA_HOUHOU_1 = 1;
        /// <summary>評価方法：2.移動平均</summary>
        public const int ZAIKO_HYOUKA_HOUHOU_2 = 2;
        /// <summary>評価方法：3.FIFO</summary>
        public const int ZAIKO_HYOUKA_HOUHOU_3 = 3;
        /// <summary>評価方法：4.最終仕入</summary>
        public const int ZAIKO_HYOUKA_HOUHOU_4 = 4;
        /// <summary>評価方法：5.在庫基準単価</summary>
        public const int ZAIKO_HYOUKA_HOUHOU_5 = 5;

        /// <summary>ボタン定義ファイルパス</summary>
        public const string BUTTON_INFO_XML_PATH = "Shougun.Core.Stock.ZaikoShimeSyori.Setting.ButtonSetting.xml";

        /// <summary>#現場CD#検索関連情報</summary>
        public const string E_MSGID_SEARCH_GENBA_INFO_NOT_EXIST = "E020";// 現場存在しない,MSGID
        public const string E_MSGPARAM_SEARCH_GENBA_INFO_NOT_EXIST = "現場";// 現場存在しない,MSGパラメータ


        /// <summary>締情報検索結果の項目名</summary>
        public const string OUTPUT_COLUMN_NAME_GYOUSHA_CD = "RET_GYOUSHA_CD";                 // 業者CD    
        public const string OUTPUT_COLUMN_NAME_GENBA_CD = "RET_GENBA_CD";                     // 現場CD    
        public const string OUTPUT_COLUMN_NAME_GENBA_NAME_RYAKU = "RET_GENBA_NAME_RYAKU";     // 現場名    
        public const string OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_CD = "RET_ZAIKO_HINMEI_CD";       // 在庫CD    
        public const string OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_RYAKU = "RET_ZAIKO_HINMEI_RYAKU"; // 在庫品名  
        public const string OUTPUT_COLUMN_NAME_REMAIN_SUU = "RET_REMAIN_SUU";                 // 前月残数  
        public const string OUTPUT_COLUMN_NAME_ENTER_SUU = "RET_ENTER_SUU";                   // 当月受入数
        public const string OUTPUT_COLUMN_NAME_OUT_SUU = "RET_OUT_SUU";                       // 当月出荷量
        public const string OUTPUT_COLUMN_NAME_ADJUST_SUU = "RET_ADJUST_SUU";                 // 調整量    
        public const string OUTPUT_COLUMN_NAME_TOTAL_SUU = "RET_TOTAL_SUU";                   // 当月在庫残
        public const string OUTPUT_COLUMN_NAME_TANKA = "RET_TANKA";                           // 評価単価  
        public const string OUTPUT_COLUMN_NAME_MULT = "RET_MULT";                             // 在庫金額
        public const int TARGET_FLG_1 = 1;                                                    // 対象データ1(受入)
        public const int TARGET_FLG_2 = 2;                                                    // 対象データ2(出荷)
        public const int TARGET_FLG_3 = 3;                                                    // 対象データ3(在庫調整)

        /// <summary>評価方法検索結果の出力項目名</summary>
        public const string OUTPUT_COLUMN_NAME_ZAIKO_HYOUKA_HOUHOU = "RET_ZAIKO_HYOUKA_HOUHOU"; // 在庫評価方法

        /// <summary>CSV出力ダイアログ関連情報</summary>
        public const string COMMA = ",";//カンマ
        public const string CSV_DIALOG_INIT_FILE_NAME = "在庫管理表";                 // 初期ファイル名
        public const string CSV_DIALOG_TITLE = "CVSファイルの出力場所を選択してください"; // ダイアログのタイトル
        public const string CSV_FINISH_MSG_ID = "I000";                                   // CSV出力完了後メッセージのID
        public const string CSV_FINISH_MSG_PARAM = "CSV出力";                             // CSV出力完了後メッセージのパラメータ

        /// <summary>削除時確認メッセージ</summary>
        public const string C_MSGID_DELETE = "C026";
        /// <summary>実行時確認メッセージ</summary>
        public const string C_MSGID_REGIST = "C049";
        public static readonly string[] C_MSGPARAM_REGIST = { "在庫" };

        /// <summary>登録時エラーメッセージ</summary>
        public const string E_MSGID_INSERT_DATA_EXIST = "E022";// 締め済みエラー,MSGID
        public const string E_MSGID_INSERT_DATA_NOTEXIST = "E076";// 該当するデータがありません,MSGID
        public static readonly string[] E_MSGPARAM_INSERT_DATA_EXIST = { "該当在庫締め情報" };// 締め済みエラー,MSGパラメータ
        public const string E_MSG_INSERT = "DB更新する際、エラーが発生しました。画面に戻ったら、やり直してください。";
        public const string E_MSG_INSERT_DIALOG_TITLE = "インフォメーション";

        /// <summary>完了メッセージ</summary>
        public const string I_MSG_ID_PROCESS_FINISHED = "I001";
        public static readonly string[] I_MSG_PARAM_INSERT_FINISHED = { "実行" };
        public static readonly string[] I_MSG_PARAM_DELETE_FINISHED = { "削除" };

    }
}
