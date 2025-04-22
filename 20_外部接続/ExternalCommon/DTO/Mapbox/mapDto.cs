using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox
{
    /// <summary>
    /// レイヤーが複数ある場合はレイヤーIDが増える
    /// マーカーや連番、ルートの色が増えるとレイヤーを追加する
    /// 現状の対象はコース一覧
    /// </summary>
    [DataContract]
    public class mapDtoList
    {
        public int layerId { get; set; }
        public List<mapDto> dtos { get; set; }
    }

    /// <summary>
    /// 画面からMapboxGLJSLogicに渡すときにこのDtoに値をセットする
    /// 画面側では出力対象データをこのDtoにセットして渡すだけ
    /// 処理は全てLogic側で処理するように
    /// </summary>
    public class mapDto
    {
        /// <summary>全データの連番</summary>いらない疑惑
        public int id;
        /// <summary>レイヤNo</summary>いらない疑惑
        public int layerNo;
        /// <summary>割当(0：割当済み、1：未割当、2：設置コンテナ)/マスタ(0：取引先、1：業者、2：現場)</summary>
        public string dataShurui;
        /// <summary>コース名</summary>
        public string courseName;
        /// <summary>曜日名</summary>
        public string dayName;
        /// <summary>定期車番号</summary>
        public string teikiHaishaNo;
        /// <summary>取引先CD</summary>
        public string torihikisakiCd;
        /// <summary>取引先名</summary>
        public string torihikisakiName;
        /// <summary>業者CD</summary>
        public string gyoushaCd;
        /// <summary>業者名</summary>
        public string gyoushaName;
        /// <summary>現場CD</summary>
        public string genbaCd;
        /// <summary>現場名</summary>
        public string genbaName;
        /// <summary>郵便番号</summary>
        public string post;
        /// <summary>住所</summary>
        public string address;
        /// <summary>電話番号</summary>
        public string tel;
        /// <summary>備考1</summary>
        public string bikou1;
        /// <summary>備考2</summary>
        public string bikou2;
        /// <summary>No</summary>
        public int number;
        /// <summary>順番</summary>
        public int rowNo;
        /// <summary>回数</summary>
        public int roundNo;
        /// <summary>品名</summary>
        public string hinmei;
        /// <summary>緯度</summary>
        public string latitude;
        /// <summary>経度</summary>
        public string longitude;
        /// <summary>出発地点(true:出発地点である、false:出発地点ではない)</summary>
        public bool shuppatsuFlag;
        // 受付一覧用
        /// <summary>作業日</summary>
        public string sagyouDate;
        /// <summary>現場着(現場着名+現場着時間)</summary>
        public string genbaChaku;
        /// <summary>車種</summary>
        public string shasyu;
        /// <summary>車輛</summary>
        public string sharyou;
        /// <summary>運転者</summary>
        public string driver;
        /// <summary>運転者指示事項1</summary>
        public string sijijikou1;
        /// <summary>運転者指示事項2</summary>
        public string sijijikou2;
        /// <summary>運転者指示事項3</summary>
        public string sijijikou3;
        // 設置コンテナ一覧用
        /// <summary>コンテナ種類名</summary>
        public string contenaShuruiName;
        /// <summary>コンテナ名</summary>
        public string contenaName;
        /// <summary>設置日/最終更新日</summary>
        public string secchiDate;
        /// <summary>設置台数</summary>
        public string daisuu;
        /// <summary>経過日数/無回転日数</summary>
        public string daysCount;
        /// <summary>数字表示(組込の場合のみTrueをセット)</summary>
        public bool NoCount;
        // 配車割当用
        /// <summary>ヘッダ</summary>
        public string header;
        /// <summary>ヘッダ2行目</summary>
        public string header2;
        /// <summary>データの区分(0：空、1：収集受付、2：出荷受付、3：定期配車)</summary>
        public string dataKBN;
        /// <summary>荷積荷降業者名</summary>
        public string NNGyoushaName;
        /// <summary>荷積荷降現場名</summary>
        public string NNGenbaName;
        /// <summary>荷積荷降住所</summary>
        public string NNAddress;
        /// <summary>荷積荷降業者名</summary>
        public string NNGyoushaName_2;
        /// <summary>荷積荷降現場名</summary>
        public string NNGenbaName_2;
        /// <summary>荷積荷降住所</summary>
        public string NNAddress_2;
        /// <summary>設置重複</summary>
        public string secchiChouhuku;
        /// <summary>業者名</summary>
        public string gyoushaName_2;
        /// <summary>現場名</summary>
        public string genbaName_2;
        /// <summary>郵便番号</summary>
        public string post_2;
        /// <summary>住所</summary>
        public string address_2;
        /// <summary>電話番号</summary>
        public string tel_2;
        /// <summary>備考1</summary>
        public string bikou1_2;
        /// <summary>備考2</summary>
        public string bikou2_2;
        /// <summary>緯度</summary>
        public string latitude_2;
        /// <summary>経度</summary>
        public string longitude_2;
        /// <summary>品名</summary>
        public string hinmei_2;
    }
    
    public class ArrayListKadoujyoukyou
    {
        public string type;
        public KadoujyoukyouProperties properties;
        public Geometry geometry;
    }
    /// <summary>
    /// 
    /// </summary>
    public class KadoujyoukyouProperties
    {
        /// <summary></summary>
        public string untenshaCd;
        /// <summary></summary>
        public string untenshaName;
        /// <summary></summary>
        public string updateTime;
        /// <summary></summary>
        public string address;
        /// <summary></summary>
        public string jyoukyouMi;
        /// <summary></summary>
        public string jyoukyouZumi;
        /// <summary></summary>
        public string jyoukyouJyo;
        /// <summary></summary>
        public string jyoukyouTotal;
        /// <summary></summary>
        public string sharyouName;
        /// <summary></summary>
        public string shashuName;
        /// <summary></summary>
        public string description;
        /// <summary></summary>
        public int iata_code;
    }

    /// <summary>
    /// 
    /// </summary>
    public class Geometry
    {
        /// <summary>レイヤーID</summary>
        public string type;
        /// <summary>マーカー名</summary>
        public string zoom;
        /// <summary></summary>
        public List<double> coordinates;
        /// <summary>緯度</summary>
        public string Latitude;
        /// <summary>経度</summary>
        public string Longitude;
    }
}
