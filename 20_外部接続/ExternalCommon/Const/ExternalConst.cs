using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Const
{
    /// <summary>
    /// HTTPメソッド
    /// </summary>
    public enum HTTP_METHOD : int
    {
        /// <summary>GET</summary>
        GET = 1,
        /// <summary>POST</summary>
        POST = 2,
        /// <summary>PUT</summary>
        PUT = 3,
        /// <summary>DELETE</summary>
        DELETE = 4,
    }

    /// <summary>
    /// 外部接続で使用する共通的な定数を定義
    /// </summary>
    public static class ExternalConst
    {
        public static readonly string CONTENT_TYPE_JOSON = "application/json";
        public static readonly int API_LOGICOMPASS = 1;
        public static readonly int API_NAVITIME = 2;

        /// <summary>TLS1.2</summary>
        public static readonly SecurityProtocolType TLS12 = (SecurityProtocolType)3072;
    }

    /// <summary>
    /// WebAPI連携で使用するContent-Typeを宣言
    /// たくさんあるのでとりあえず使ったものだけ宣言
    /// どの連携APIかによって使用可能なContent-Typeも違ってくるのでそこは検証
    /// </summary>
    public static class WebAPI_ContentType
    {
        /// <summary>JSON形式で連携する場合のContent-Type</summary>
        public static readonly string APPLICATION_JSON = "application/json";
        /// <summary>x-www-form-urlencoded形式で連携する場合のContent-Type</summary>
        public static readonly string APPLICATION_X_WWW_FORM_URLENCODED = "application/x-www-form-urlencoded";
        /// <summary>ファイル送信の連携が必要な場合のContent-Type</summary>
        public static readonly string MULTIPART_FORM_DATA = "multipart/form-data";
    }
}
