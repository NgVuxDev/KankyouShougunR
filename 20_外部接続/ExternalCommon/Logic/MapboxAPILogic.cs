using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Utility;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Logic
{
    /// <summary>
    /// MapboxAPIに関する処理を定義
    /// </summary>
    public class MapboxAPILogic
    {
        #region フィールド
        /// <summary>システム設定Dao</summary>
        private IM_SYS_INFODao sysInfoDao;
        private M_SYS_INFO sysInfoEntity;

        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MapboxAPILogic()
        {
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
        }
        #endregion

        #region GETメソッド
        /// <summary>
        /// GETメソッド(DirectionsAPIの場合)
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="api">API毎のURL</param>
        /// <param name="result">レスポンス結果</param>
        /// <returns>true:正常終了 false:異常終了</returns>
        public bool HttpGET<T>(List<mapboxArrayListGeometry> dto, out T result)
            where T : IApiDto
        {

            string URL = string.Empty;
            string coordinates = string.Empty;

            // 地点情報文字列化
            foreach (mapboxArrayListGeometry geometry in dto)
            {
                if (geometry.Longitude != "" && geometry.Latitude != "")
                {
                    if (coordinates != string.Empty)
                    {
                        coordinates += ";";
                    }
                    coordinates += geometry.Longitude + ",";
                    coordinates += geometry.Latitude;
                }
            }

            this.Init();

            // 送信先のURL
            URL = string.Format(MapboxConst.directions_uri, coordinates, this.sysInfoEntity.MAPBOX_ACCESS_TOKEN);

            return HttpMethodMain<T>(URL, out result);
        }

        /// <summary>
        /// GETメソッド(GeoCodingAPIの場合)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dto"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool HttpGET<T>(string address, out T result)
            where T : IApiDto
        {
            string adr = string.Empty;
            string URL = string.Empty;

            int i = address.IndexOf("-");

            // 住所に入っている「-」の数をカウント
            int cnt = address.Where(c => c == '-').Count();

            if (cnt == 1)
            {
                // 「-」が1つしかない場合⇒「番」に置き換える
                adr = address.Replace('-', '番');
            }
            else if (cnt == 2)
            {
                // 「-」が2つの場合
                // 1つ目の「-」⇒丁目
                adr = Strings.Replace(address, "-", "丁目", 1, 1, CompareMethod.Binary);
                // 2つ目の「-」⇒番
                adr = Strings.Replace(adr, "-", "番", 1, 1, CompareMethod.Binary);
            }
            else
            {
                // 「-」が0、もしくは3つ以上ある場合は何もしない
                // どこで「-」が使われているか不明なため置換しようがない
                adr = address;
            }

            string encAddress = RFC3986UriUtility.EscapeDataString(adr);

            this.Init();

            if ((this.sysInfoEntity.MAPBOX_ACCESS_TOKEN == null || this.sysInfoEntity.MAPBOX_ACCESS_TOKEN.Equals(""))
                || (this.sysInfoEntity.MAPBOX_MAP_STYLE == null || this.sysInfoEntity.MAPBOX_MAP_STYLE.Equals("")))            
            {
                result = default(T);    //nullを返す
                MessageBox.Show("ベンダーにMAPBOXのライセンス確認を行ってください", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 送信先のURL
            URL = string.Format(MapboxConst.geocoding_uri, encAddress, this.sysInfoEntity.MAPBOX_ACCESS_TOKEN);

            return HttpMethodMain<T>(URL, out result);
        }
        #endregion

        #region メイン処理
        /// <summary>
        /// HttpMethodのメイン処理。このメソッドを直接呼んで使用することはNG
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="api">API毎のURL</param>
        /// <param name="method">HTTPメソッド</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <param name="result">レスポンス結果</param>
        /// <returns>true:処理成功, false:処理失敗</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool HttpMethodMain<T>(string URL, out T result)
            where T : IApiDto
        {
            try
            {
                result = default(T);

                // メソッドを指定
                var methodName = Enum.GetName(typeof(HTTP_METHOD), HTTP_METHOD.GET);

                // 対象サーバーのTLSサポートが変更されたら要対応
                ServicePointManager.SecurityProtocol = ExternalConst.TLS12;

                // リクエスト発行
                HttpWebRequest req = CreateWebRequest(URL, methodName);

                // レスポンスを取得
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (res)
                {
                    using (var resStream = res.GetResponseStream())
                    {
                        var serializer = new DataContractJsonSerializer(typeof(T));
                        result = (T)serializer.ReadObject(resStream);
                    }
                }
                return true;
            }
            catch (WebException ex)
            {
                // WebExceptionだけ一括でエラー処理をする
                LogUtility.Error("HttpMethodMain", ex);

                #region エラー(後で)
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse errRes = (HttpWebResponse)ex.Response;
                    var errorDto = GetErrorDto<REST_Error>(errRes);
                    var title = string.Empty;

                    // リクエストによってエラーのレスポンスがあったりなかったりするので
                    // 冗長だがcase文に埋め込んでいく

                    var err = "下記エラーが発生しました。";
                    var message = new StringBuilder();

                    switch (errRes.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:         // 400
                            // リクエスト不正
                            title = "HTTP STATUS 400 Bad Request";
                            err = "クライアントエラーが発生しました。(400)";
                            message.AppendFormat("{0}", err);
                            break;
                        case HttpStatusCode.Unauthorized:       // 401
                            // アクセストークン無効
                            title = "HTTP STATUS 401 Unauthorized";
                            err = "トークンの有効期限が切れています。(401)";
                            message.AppendFormat("{0}", err);
                            break;
                        case HttpStatusCode.PaymentRequired:    // 402
                            // 
                            title = "HTTP STATUS 402 Payment Required";
                            err = "料金が未払いのためリクエストできません。(402)";
                            message.AppendFormat("{0}", err);
                            break;
                        case HttpStatusCode.Forbidden:          // 403
                            // アクセス拒否
                            title = "HTTP STATUS 403 Forbidden";
                            err = "指定したファイルの閲覧権限が付与されていません。(403)";
                            message.AppendFormat("{0}", err);
                            break;
                        case HttpStatusCode.NotFound:           // 404
                            // 指定されたページが存在しない。権限が無い。
                            title = "HTTP STATUS 404 Not Found";
                            err = "指定されたデータが存在しません。" + Environment.NewLine;
                            err += "確認してください。(404)";
                            message.AppendFormat("{0}", err);
                            break;
                        case HttpStatusCode.MethodNotAllowed:   // 405
                            // 未許可のメソッド
                            title = "HTTP STATUS 405 Method Not Allowed";
                            err = "送信するメソッドが許可されていません。(405)";
                            message.AppendFormat("{0}", err);
                            break;
                        case HttpStatusCode.NotAcceptable:      // 406
                            // 受理不可
                            title = "HTTP STATUS 406 Not Acceptable";
                            err = "受付不可能な値です。(406)";
                            message.AppendFormat("{0}", err);
                            break;
                        case HttpStatusCode.RequestEntityTooLarge:  // 413
                            // 情報膨大
                            title = "HTTP STATUS 413 Request Entity Too Large";
                            err = "アップロードするファイルのデータ量が大きすぎます。(413)";
                            message.AppendFormat("{0}", err);
                            break;
                        case HttpStatusCode.UnsupportedMediaType:   // 415
                            // 未サポートのメディアタイプ
                            title = "HTTP STATUS 415 Unsupported Media Type";
                            err = "リクエストがサーバー側で拒否されました。(415)";
                            message.AppendFormat("{0}", err);
                            break;
                        case (HttpStatusCode)422:
                            // 独自のステータスコード
                            // 25件以上の地点を一度にリクエストできない
                            title = "HTTP STATUS 422 Unprocessable Entity";
                            err = "入力した住所を変更し、地図ボタンを再度押下してください。(422)";
                            message.AppendFormat("{0}", err);
                            break;
                        case HttpStatusCode.InternalServerError:// 500
                            // サーバ内部エラー
                            title = "HTTP STATUS 500 Internal Server Error";
                            err = "サーバー内でエラーが発生しました。(500)";
                            message.AppendFormat("{0}", err);
                            break;
                        default:
                            title = "その他エラー";
                            err = "想定外のエラーが発生しました。(XXX)";
                            message.AppendFormat("{0}", err);
                            break;
                    }

                    // ログ出力用
                    var logMessage = new StringBuilder();
                    logMessage.AppendLine("【エラー情報】")
                              .AppendFormat("URL : {0}", errRes.ResponseUri.AbsoluteUri)
                              .Append(Environment.NewLine)
                              .AppendFormat("Httpメソッド : {0}", Enum.GetName(typeof(HTTP_METHOD), HTTP_METHOD.GET))
                              .Append(Environment.NewLine)
                              .AppendFormat("Error : {0}", errorDto.code)
                              .Append(Environment.NewLine)
                              .AppendFormat("Message : {0}", errorDto.message);

                    LogUtility.Error(logMessage.ToString());


                    MessageBox.Show(message.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion

                result = default(T);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HttpMethodMain", ex);
                throw;
            }
        }
        #endregion

        #region リクエスト作成
        /// <summary>
        /// リクエストの作成
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="method">HTTPメソッド名</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <returns>リクエスト</returns>
        private HttpWebRequest CreateWebRequest(string url, string method)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = method;
            return req;
        }
        #endregion

        #region エラー情報取得
        /// <summary>
        /// 例外処理時のエラー情報取得処理
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="errRes">レスポンス</param>
        /// <returns>エラーモデル</returns>
        private T GetErrorDto<T>(HttpWebResponse errRes)
            where T : IApiDto
        {
            T errResult;
            // ストリームを取得
            Stream st = errRes.GetResponseStream();
            using (st)
            {
                StreamReader sr = new StreamReader(st);
                string errStr = sr.ReadToEnd();

                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(errStr)))
                {
                    var serializer = new DataContractJsonSerializer(typeof(T));
                    // デシリアライズ
                    errResult = (T)serializer.ReadObject(ms);
                }

                return errResult;
            }
        }
        #endregion

        #region 初期化
        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Init()
        {
            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }
        #endregion
    }
}
