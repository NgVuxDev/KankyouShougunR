using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Const
{
    /// <summary>
    /// ロジコンパス(システック)で使用する共通的な定数を定義
    /// </summary>
    public static class LogiConst
    {
        public static readonly string GRANT_TYPE = "password";

        // UNDONE:システック　バッフア時間は固定値で持つで問題無いか要確認！！
        /// <summary>トークンの有効期間設定時のバッファ時間</summary>
        /// <remarks>バッファ時間のためマイナスで設定すること。最大でも0</remarks>
        public static readonly int BUFFER_TIME_HOUR = -1;

        #region S_LOGI_CONNECTテーブルのCONTENT_NAME
        public static readonly string CONTENT_NAME_TEST_BASE_URL = "TEST_BASE_URL";
        public static readonly string CONTENT_NAME_BASE_URL = "BASE_URL";
        public static readonly string CONTENT_NAME_TOKEN = "TOKEN";
        public static readonly string CONTENT_NAME_POINTS = "Points";
        public static readonly string CONTENT_NAME_CAR_GROUPS = "CarGroups";
        public static readonly string CONTENT_NAME_CAR_RELATIONS = "CarRelations";
        public static readonly string CONTENT_NAME_DRIVERS = "Drivers";
        public static readonly string CONTENT_NAME_GOODS = "Goods";
        public static readonly string CONTENT_NAME_GOODS_UNITS = "GoodsUnits";
        public static readonly string CONTENT_NAME_GOODS_KINDS = "GoodsKinds";
        public static readonly string CONTENT_NAME_DELIVERY_PLANS = "DeliveryPlans";
        public static readonly string CONTENT_NAME_DELIVERY_PERFORMANCES = "DeliveryPerformances";
        #endregion

        // UNDONE:システック　S_LOGI_CONNECTテーブルにカラム追加したほうが良いか？
        #region API部分のパラメーター部分のURLを定義
        public static readonly string API_PARAMETER_URL_COMPANY = "?companyId={0}";
        public static readonly string API_PARAMETER_URL_CAR_GROUPS = "?companyId={0}&carGroupId={1}";
        public static readonly string API_PARAMETER_URL_CAR_RELATIONS = "?companyId={0}&carGroupId={1}&carId={2}";
        public static readonly string API_PARAMETER_URL_DRIVERS = "?companyId={0}&driverId={1}";
        public static readonly string API_PARAMETER_URL_GOODS = "?companyId={0}&goodsId={1}";
        public static readonly string API_PARAMETER_URL_GOODS_UNITS = "?companyId={0}&goodsUnitId={1}";
        public static readonly string API_PARAMETER_URL_GOODS_KINDS = "?companyId={0}&goodsKindId={1}";
        public static readonly string API_PARAMETER_URL_POINTS = "?companyId={0}&pointId={1}";
        public static readonly string API_PARAMETER_URL_DELIVERY_PLANS = "?companyId={0}&carId={1}&deliveryDate={2}&deliveryNo={3}";
        public static readonly string API_PARAMETER_URL_DELIVERY_PERFORMANCES = "?companyId={0}&carId={1}&deliveryDate={2}&deliveryNo={3}";
        #endregion
    }
}
