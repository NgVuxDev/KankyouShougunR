using r_framework.Const;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Const
{
    /// <summary>
    /// mapboxで使用する共通的な定義
    /// </summary>
    public static class MapboxConst
    {
        /// <summary>DirectionAPIのURI</summary>
        public static readonly string directions_uri = "https://api.mapbox.com/directions/v5/mapbox/driving/{0}?geometries=geojson&overview=full&access_token={1}";
        /// <summary>GeoCodingAPIのURI</summary>
        public static readonly string geocoding_uri = "https://api.mapbox.com/geocoding/v5/mapbox.places/{0}.json?limit=1&language=ja&access_token={1}";
        /// <summary>GeoCodingAPIのURI(住所取得用)</summary>
        public static readonly string geocoding_uri_address = "https://api.mapbox.com/geocoding/v5/mapbox.places/{0}.json?types=address&language=ja&access_token={1}";

        /// <summary>
        /// WINDOW_IDから地図表示に利用するテンプレートを返す
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ReturnTemplateString(this WINDOW_ID e)
        {
            string htmlPath = "Shougun.Core.ExternalConnection.ExternalCommon.Template.Mapbox.";
            switch (e)
            {
                case WINDOW_ID.M_TORIHIKISAKI:          // 取引先入力
                case WINDOW_ID.M_GYOUSHA:               // 業者入力
                case WINDOW_ID.M_GENBA:                 // 現場入力
                    return htmlPath + "M213_M215_M217.htm";
                case WINDOW_ID.M_TORIHIKISAKI_ICHIRAN:  // 取引先一覧
                case WINDOW_ID.M_GYOUSHA_ICHIRAN:       // 業者一覧
                case WINDOW_ID.M_GENBA_ICHIRAN:         // 現場一覧
                    return htmlPath + "M214_M216_M218.htm";
                case WINDOW_ID.M_COURSE:                // コース入力
                case WINDOW_ID.M_COURSE_ICHIRAN:        // コース一覧
                case WINDOW_ID.T_COURSE_HAISHA_IRAI:    // コース配車依頼入力
                case WINDOW_ID.T_TEIKI_HAISHA:          // 定期配車入力
                case WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN:  // 定期配車一覧
                    return htmlPath + "M232_M663_G030_G031_G032.htm";
                case WINDOW_ID.T_UKETSUKE_SHUSHU:       // 収集受付入力
                case WINDOW_ID.T_UKETSUKE_SHUKKA:       // 出荷受付入力
                    return htmlPath + "G015_G016.htm";
                case WINDOW_ID.T_UKETSUKE_ICHIRAN:      // 受付一覧
                    return htmlPath + "G021.htm";
                case WINDOW_ID.T_CONTENA_ICHIRAN:       // 設置コンテナ一覧
                    return htmlPath + "G041.htm";
                case WINDOW_ID.T_HAISHA_WARIATE_DAY:    // 配車割当
                    return htmlPath + "G026.htm";
                case WINDOW_ID.T_MOBILE_JOUKYOU_ICHIRAN:
                    return htmlPath + "G667.htm";
                default:
                    return "";
            }
        }

        /// <summary>
        /// WINDOW_IDから画面名を返す
        /// 現状はS_MAPBOX_ACCESS_TOKEN登録時の値に利用
        /// 地図のタイトルに使ってもいいかも
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ReturnMenuName(this WINDOW_ID e)
        {
            switch (e)
            {
                case WINDOW_ID.M_TORIHIKISAKI:
                    return "取引先入力";
                case WINDOW_ID.M_TORIHIKISAKI_ICHIRAN:
                    return "取引先一覧";
                case WINDOW_ID.M_GYOUSHA:
                    return "業者入力";
                case WINDOW_ID.M_GYOUSHA_ICHIRAN:
                    return "業者一覧";
                case WINDOW_ID.M_GENBA:
                    return "現場入力";
                case WINDOW_ID.M_GENBA_ICHIRAN:
                    return "現場一覧";
                case WINDOW_ID.M_COURSE:
                    return "コース入力";
                case WINDOW_ID.M_COURSE_ICHIRAN:
                    return "コース一覧";
                case WINDOW_ID.T_UKETSUKE_SHUSHU:
                    return "収集受付入力";
                case WINDOW_ID.T_UKETSUKE_SHUKKA:
                    return "出荷受付入力";
                case WINDOW_ID.T_UKETSUKE_ICHIRAN:
                    return "受付一覧";
                case WINDOW_ID.T_TEIKI_HAISHA:
                    return "定期配車入力";
                case WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN:
                    return "定期配車一覧";
                case WINDOW_ID.T_CONTENA_ICHIRAN:
                    return "設置コンテナ一覧";
                case WINDOW_ID.T_COURSE_HAISHA_IRAI:
                    return "コース配車依頼入力";
                case WINDOW_ID.T_HAISHA_WARIATE_DAY:
                    return "配車割当";
                case WINDOW_ID.T_MOBILE_JOUKYOU_ICHIRAN:
                    return "モバイル状況一覧";
                default:
                    return "";
            }
        }

    }
}
