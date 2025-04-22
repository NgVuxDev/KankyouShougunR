using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox
{
    /// <summary>
    /// mapbox gl jsに渡すjsonデータ用のクラス
    /// </summary>
    public class mapboxArrayList
    {
        public string type;
        public mapboxArrayListProperties properties;
        public mapboxArrayListGeometry geometry;
    }

    /// <summary>
    /// Properties
    /// </summary>
    public class mapboxArrayListProperties
    {
        /// <summary>タブ名</summary>
        public string tabName;
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
        /// <summary>住所</summary>
        public string address;
        /// <summary>HTML格納用</summary>
        public string description;
        /// <summary></summary>
        public int iata_code;
        /// <summary>順番</summary>
        public string rowNo;
        /// <summary>回数</summary>
        public string roundNo;
        /// <summary>品名</summary>
        public string hinmei;
        /// <summary>出発地点(true:出発地点である、false:出発地点ではない)</summary>
        public bool shuppatsuFlag;
        /// <summary>元画面判別用</summary>
        public int windowId;
        // 収集出荷一覧用
        /// <summary>番号</summary>
        public string denpyouNo;
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
        /// <summary>色の保存用</summary>
        public string layerColor;

        /// <summary>ヘッダ</summary>
        public string header;
        /// <summary>ヘッダ2行目</summary>
        public string header2;
        /// <summary>データの種類(0：割当済み、1：未割当、2：設置コンテナ)</summary>
        public string dataShurui;
        /// <summary>種別区分(0：空、1：収集受付、2：出荷受付、3：定期配車)</summary>
        public string dataKBN;
        /// <summary>荷積荷降業者名</summary>
        public string NNGyoushaName;
        /// <summary>荷積荷降現場名</summary>
        public string NNGenbaName;
        /// <summary>荷積荷降住所</summary>
        public string NNAddress;
        /// <summary>設置重複</summary>
        public string secchiChouhuku;

        /// <summary>業者名(配車割当一日の定期用)</summary>
        public string gyoushaName_2;
        /// <summary>現場名(配車割当一日の定期用)</summary>
        public string genbaName_2;
        /// <summary>住所(配車割当一日の定期用)</summary>
        public string address_2;
        /// <summary>荷積荷降業者名</summary>
        public string NNGyoushaName_2;
        /// <summary>荷積荷降現場名</summary>
        public string NNGenbaName_2;
        /// <summary>荷積荷降住所</summary>
        public string NNAddress_2;
        /// <summary>HTML格納用(配車割当一日の定期用)</summary>
        public string description_2;
        /// <summary>品名</summary>
        public string hinmei_2;
    }

    /// <summary>
    /// Geometry
    /// </summary>
    public class mapboxArrayListGeometry
    {
        /// <summary>type</summary>
        public string type;
        /// <summary>拡大率</summary>
        public string zoom;
        /// <summary>緯度経度</summary>
        public List<double> coordinates;
        /// <summary>緯度</summary>
        public string Latitude;
        /// <summary>経度</summary>
        public string Longitude;
        /// <summary>緯度経度(配車割当一日の定期用)</summary>
        public List<double> coordinates_2;
        /// <summary>緯度(配車割当一日の定期用)</summary>
        public string Latitude_2;
        /// <summary>経度(配車割当一日の定期用)</summary>
        public string Longitude_2;
    }
}
