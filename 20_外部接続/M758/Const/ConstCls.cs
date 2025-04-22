
using System.Drawing;
namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukaiWanSign
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>
        /// CHECKBOX
        /// </summary>
        public static readonly string CELL_CHECKBOX = "CHECKBOX";

        /// <summary>
        /// 紐付SystemID
        /// </summary>
        public static readonly string CELL_SYSTEM_ID = "紐付SystemID";

        /// <summary>
        /// 紐付不可理由
        /// </summary>
        public static readonly string CELL_HIMODZUKE_FUKA_RIYUU = "紐付不可理由";

        /// <summary>
        /// ドキュメントID
        /// </summary>
        public static readonly string HIDDEN_DOCUMENT_ID = "HIDDEN_DOCUMENT_ID";

        /// <summary>
        /// システムID
        /// </summary>
        public static readonly string HIDDEN_WANSIGN_SYSTEM_ID = "HIDDEN_WANSIGN_SYSTEM_ID";

        /// <summary>
        /// 紐付SystemID
        /// </summary>
        public static readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>
        /// 管理番号（WAN）
        /// </summary>
        public static readonly string HIDDEN_ORIGINAL_CONTROL_NUMBER = "HIDDEN_ORIGINAL_CONTROL_NUMBER";

        /// <summary>
        /// 関連コード
        /// </summary>
        public static readonly string HIDDEN_CONTROL_NUMBER = "HIDDEN_CONTROL_NUMBER";

        /// <summary>
        /// 契約状況（WAN）
        /// </summary>
        public static readonly string HIDDEN_SIGNING_DATETIME = "HIDDEN_SIGNING_DATETIME";

        /// <summary>
        /// メッセージBを表示
        /// </summary>
        public static readonly string MsgB = "関連コード取得に失敗しました。システム管理者に問合せください。（status＝{0}）";

        /// <summary>
        /// メッセージCを表示
        /// </summary>
        public static readonly string MsgC = "文書詳細情報取得に失敗しました。システム管理者に問合せください。（status＝{0}）";

        /// <summary>
        /// メッセージEを表示
        /// </summary>
        public static readonly string MsgE = "電子契約の照会が完了しました。再検索を行ってください。";

        /// <summary>
        /// メッセージGを表示
        /// </summary>
        public static readonly string MsgG = "紐付登録する電子契約を選択してください。";

        //#161239 20220308 CongBinh S
        /// <summary>
        /// メッセージHを表示
        /// </summary>
        //public static readonly string MsgH = "委託契約書（環境将軍R）に未登録の管理番号になります。管理番号の確認を行ってください。";
        public static readonly string MsgH = "委託契約書（環境将軍Rマスタ）に未登録のSystemIDになります。\n\r[F5]契約参照をクリックし、適切な委託契約書を選択してください。";
        //#161239 20220308 CongBinh E

        /// <summary>
        /// メッセージPを表示
        /// </summary>
        public static readonly string MsgP = "紐付状況が「済」の電子契約書は、紐付補助処理を行えません。選択をチェックオフに変更してください。";

        /// <summary>
        /// メッセージJを表示
        /// </summary>
        public static readonly string MsgJ = "紐付登録処理が完了しました。";

        /// <summary>
        /// メッセージKを表示
        /// </summary>
        public static readonly string MsgK = "ダウンロードを行う電子契約を選択してください。";

        /// <summary>
        /// メッセージMを表示
        /// </summary>
        public static readonly string MsgM = "既に表示している契約情報をキャンセルします。再検索となりますが、宜しいですか？";

        /// <summary>
        /// メッセージOを表示
        /// </summary>
        public static readonly string MsgO = "紐付処理を行う電子契約情報の選択をしてください。";

        /// <summary>
        /// メッセージIを表示
        /// </summary>
        public static readonly string MsgI = "同じ電子契約書と異なる紐付SystemIDがセットされました。入力した紐付IDに更新しますか（関連コード（WAN）＝{0}）";

        /// <summary>
        /// メッセージRを表示
        /// </summary>
        public static readonly string MsgR = "契約状況（WAN）が空白表示の電子契約は、契約書ダウンロードを行えません。\r\n「署名済」の電子契約を選択してください。";

        /// <summary>
        /// 委託契約WAN-Sign連携（M_ITAKU_LINK_WANSIGN_KEIYAKU）に委託契約書ーSystemID無し（電子契約と委託契約書が紐づいていない状態）
        /// かつ WANSIGN文書詳細情報（新規テーブル）ー管理番号＝入力有
        /// </summary>
        public static readonly string KANRINUMER_EXISTS = "該当管理番号未登録";

        //#161240 20220308 CongBinh S
        /// <summary>
        /// 
        /// </summary>
        public static readonly string KANRINUMER_EXISTS_1 = "[F9]登録処理待ち";
        /// <summary>
        /// 
        /// </summary>
        public static readonly string KANRINUMER_EXISTS_2 = "紐付契約番号が複数件有";
        //#161240 20220308 CongBinh E

        /// <summary>
        /// 委託契約WAN-Sign連携（M_ITAKU_LINK_WANSIGN_KEIYAKU）に委託契約書ーSystemID無し（電子契約と委託契約書が紐づいていない状態）
        /// かつ WANSIGN文書詳細情報（新規テーブル）ー管理番号＝無（BLANK）
        /// </summary>
        public static readonly string KANRINUMER_NOT_EXISTS = "管理番号未入力";

        /// <summary>
        /// 薄ピンク
        /// </summary>
        public static Color USUPINKU = Color.FromArgb(255, 153, 255);//Color.FromArgb(240, 230, 250);//#161239 20220308 CongBinh 

        /// <summary>
        /// 無効
        /// </summary>
        public static readonly string INVALID = "無効";
    }
}
