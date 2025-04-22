using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku;
using Shougun.Core.ExternalConnection.ExternalCommon.Utility;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Logic
{
    /// <summary>
    /// 電子契約(クラウドサイン)で使用するWebAPI連携に関する共通処理
    /// </summary>
    /// <remarks>
    /// GET,POST,PUTを実装。DELETEは今回は使用予定がないため未実装
    /// クラウドサインのバージョン「0.8.0」のAPI仕様で実装
    /// ※ただし「0.10.1」で対応予定のID仕様変更(UUID v4形式⇒独自フォーマット)は想定して実装済み
    /// </remarks>
    public class DenshiLogic
    {
        #region プロパティ
        /// <summary>ベースURL</summary>
        private string BASE_URL { get; set; }
        /// <summary>システム設定</summary>
        private M_SYS_INFO SYS_INFO { get; set; }
        /// <summary>電子契約接続情報管理リスト</summary>
        private List<S_DENSHI_CONNECT> DENSHI_CONNECT_LIST { get; set; }
        #endregion

        #region 変数
        /// <summary>システム設定Dao</summary>
        private IM_SYS_INFODao sysInfoDao;
        /// <summary>電子契約接続情報管理Dao</summary>
        private IS_DENSHI_CONNECTDao denshiConnectDao;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenshiLogic()
        {
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.denshiConnectDao = DaoInitUtility.GetComponent<IS_DENSHI_CONNECTDao>();
        }
        #endregion

        #region GETメソッド
        /// <summary>
        /// GETメソッド
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <param name="result">レスポンス結果</param>
        /// <returns>true:正常終了 false:異常終了</returns>
        public bool HttpGET<T>(string api, string contentType, REQ_COMMON dto, out T result)
            where T : IApiDto
        {
            return HttpGET<T>(this.BASE_URL, api, contentType, dto, out result);
        }

        /// <summary>
        /// GETメソッド
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <param name="result">レスポンス結果</param>
        /// <returns>true:正常終了 false:異常終了</returns>
        public bool HttpGET<T>(string url, string api, string contentType, REQ_COMMON dto, out T result)
            where T : IApiDto
        {
            return HttpMethodMain<T>(url, api, contentType, HTTP_METHOD.GET, dto, null, out result);
        }

        /// <summary>
        /// GETメソッド（PDFファイルダウンロード専用）
        /// </summary>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <param name="createFilePath">PDFファイル作成先のフルパス(ファイル名込み)</param>
        /// <returns>true:正常終了 false:異常終了</returns>
        public bool HttpGET_DownLoadPDF(string api, string contentType, REQ_COMMON dto, string createFilePath)
        {
            if (string.IsNullOrEmpty(createFilePath))
            {
                return false;
            }

            DOCUMENT_MODEL result;
            return HttpMethodMain<DOCUMENT_MODEL>(this.BASE_URL, api, contentType, HTTP_METHOD.GET, dto, createFilePath, out result);
        }
        #endregion

        #region PUTメソッド
        /// <summary>
        /// PUTメソッド
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <param name="result">レスポンス結果</param>
        /// <returns>true:正常終了 false:異常終了</returns>
        public bool HttpPUT<T>(string api, string contentType, REQ_COMMON dto, out T result)
            where T : IApiDto
        {
            return HttpPUT(this.BASE_URL, api, contentType, dto, out result);
        }

        /// <summary>
        /// PUTメソッド
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <param name="result">レスポンス結果</param>
        /// <returns>true:正常終了 false:異常終了</returns>
        public bool HttpPUT<T>(string url, string api, string contentType, REQ_COMMON dto, out T result)
            where T : IApiDto
        {
            return HttpMethodMain<T>(url, api, contentType, HTTP_METHOD.PUT, dto, null, out result);
        }
        #endregion

        #region POSTメソッド
        /// <summary>
        /// POSTメソッド
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <param name="result">レスポンス結果</param>
        /// <returns>true:処理成功, false:処理失敗</returns>
        public bool HttpPOST<T>(string api, string contentType, REQ_COMMON dto, out T result)
            where T : IApiDto
        {
            return HttpPOST<T>(this.BASE_URL, api, contentType, dto, out result);
        }

        /// <summary>
        /// POSTメソッド
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <param name="result">レスポンス結果</param>
        /// <returns>true:処理成功, false:処理失敗</returns>
        public bool HttpPOST<T>(string url, string api, string contentType, REQ_COMMON dto, out T result)
            where T : IApiDto
        {
            return HttpMethodMain<T>(url, api, contentType, HTTP_METHOD.POST, dto, null, out result);
        }
        #endregion

        #region メイン処理
        /// <summary>
        /// HttpMethodのメイン処理。このメソッドを直接呼んで使用することはNG
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="method">HTTPメソッド</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <param name="createFilePath">PDFファイルダウンロード時の保存場所</param>
        /// <param name="result">レスポンス結果</param>
        /// <returns>true:処理成功, false:処理失敗</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool HttpMethodMain<T>(string url, string api, string contentType, HTTP_METHOD method, REQ_COMMON dto, string createFilePath, out T result)
            where T : IApiDto
        {
            try
            {
                result = default(T);
                Init();

                if (string.IsNullOrEmpty(url))
                {
                    url = this.BASE_URL;
                }
                // 送信先のURL
                string URL = url + api;

                // メソッドを指定
                var methodName = Enum.GetName(typeof(HTTP_METHOD), method);

                // クラウドサインはTLS1.2のみサポート(2019/01 時点)
                ServicePointManager.SecurityProtocol = ExternalConst.TLS12;

                // リクエスト発行
                HttpWebRequest req = CreateWebRequest(url + api, contentType, methodName, dto);

                // レスポンスを取得
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (res)
                {
                    using (var resStream = res.GetResponseStream())
                    {
                        // GETかつcreateFilePathに値があればPDFダウンロードと判定
                        if ("GET".Equals(methodName)
                            && !string.IsNullOrEmpty(createFilePath))
                        {
                            using (var fileStream = File.Create(createFilePath))
                            {
                                resStream.CopyTo(fileStream);
                            }
                        }
                        else
                        {
                            var serializer = new DataContractJsonSerializer(typeof(T));
                            result = (T)serializer.ReadObject(resStream);
                        }
                    }
                }
                return true;
            }
            catch (WebException ex)
            {
                // WebExceptionだけ一括でエラー処理をする
                LogUtility.Error("HttpMethodMain", ex);

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse errRes = (HttpWebResponse)ex.Response;
                    var errorDto = GetErrorDto<ERROR_MODERL>(errRes);
                    var title = string.Empty;

                    switch (errRes.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:         // 400
                            // リクエスト不正
                            title = "HTTP STATUS 400 Bad Request";
                            break;
                        case HttpStatusCode.Unauthorized:       // 401
                            // アクセストークン無効
                            title = "HTTP STATUS 401 Unauthorized";
                            break;
                        case HttpStatusCode.PaymentRequired:    // 402
                            // 
                            title = "HTTP STATUS 402 Payment Required";
                            break;
                        case HttpStatusCode.Forbidden:          // 403
                            // アクセス拒否
                            title = "HTTP STATUS 403 Forbidden";
                            break;
                        case HttpStatusCode.NotFound:           // 404
                            // 指定されたページが存在しない。権限が無い。
                            title = "HTTP STATUS 404 Not Found";
                            break;
                        case HttpStatusCode.MethodNotAllowed:   // 405
                            // 未許可のメソッド
                            title = "HTTP STATUS 405 Method Not Allowed";
                            break;
                        case HttpStatusCode.NotAcceptable:      // 406
                            // 受理不可
                            title = "HTTP STATUS 406 Not Acceptable";
                            break;
                        case HttpStatusCode.RequestEntityTooLarge:  // 413
                            // 情報膨大
                            title = "HTTP STATUS 413 Request Entity Too Large";
                            break;
                        case HttpStatusCode.UnsupportedMediaType:   // 415
                            // 未サポートのメディアタイプ
                            title = "HTTP STATUS 415 Unsupported Media Type";
                            break;
                        case HttpStatusCode.InternalServerError:// 500
                            // サーバ内部エラー
                            title = "HTTP STATUS 500 Internal Server Error";
                            break;
                        default:
                            title = "その他エラー";
                            break;
                    }

                    var logMessage = new StringBuilder();
                    logMessage.AppendLine("【エラー情報】")
                              .AppendFormat("URL : {0}", errRes.ResponseUri.AbsoluteUri)
                              .Append(Environment.NewLine)
                              .AppendFormat("Httpメソッド : {0}", Enum.GetName(typeof(HTTP_METHOD), method))
                              .Append(Environment.NewLine)
                              .AppendFormat("Error : {0}", errorDto.Error)
                              .Append(Environment.NewLine)
                              .AppendFormat("Message : {0}", errorDto.Message);
                    LogUtility.Error(logMessage.ToString());

                    var err = "下記エラーが発生しました。";
                    if (!string.IsNullOrEmpty(dto.errMessage))
                    {
                        // 任意のエラーメッセージがあれば置き換え
                        err = dto.errMessage;
                    }

                    var message = new StringBuilder();
                    message.AppendFormat("{0}", err)
                           .Append(Environment.NewLine)
                           .AppendFormat("Error : {0}", errorDto.Error)
                           .Append(Environment.NewLine)
                           .AppendFormat("Message : {0}", errorDto.Message);

                    if (typeof(T) == typeof(ATTRIBUTE_MODEL) && errRes.StatusCode == HttpStatusCode.NotFound)
                    {
                        // 書類情報GET時の404エラーはアラート表示させない。
                        // 2018/09/04以前に作成した電子契約データは書類情報が存在しないため、
                        // 必ず404が返ってくると思われる。
                        // ※2018/09/05に作成したデータは書類情報が返ってきたため。
                    }
                    else
                    {
                        MessageBox.Show(message.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        #region 初期化
        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Init()
        {
            // システム設定の取得
            this.SYS_INFO = this.SYS_INFO ?? this.sysInfoDao.GetAllDataForCode("0");
            this.DENSHI_CONNECT_LIST = this.DENSHI_CONNECT_LIST ?? this.denshiConnectDao.GetAllData().ToList();

            // ベースURLの取得
            if (string.IsNullOrEmpty(BASE_URL))
            {
                // テスト用、本番用URL切替箇所
                //var contentName = DenshiConst.CONTENT_NAME_TEST_BASE_URL;   // テスト用URL
                var contentName = DenshiConst.CONTENT_NAME_BASE_URL;        // 本番用URL

                this.BASE_URL = this.DENSHI_CONNECT_LIST.Where(n => n.CONTENT_NAME.Equals(contentName))
                                                        .Select(n => n.URL)
                                                        .First();
            }
        }
        #endregion

        #region トークン取得
        /// <summary>
        /// トークン管理の取得
        /// </summary>
        /// <param name="dto">リクエスト用DTO</param>
        private ACCESS_TOKEN_MODEL GetAccessTokenModel(REQ_COMMON dto)
        {
            var connect = this.DENSHI_CONNECT_LIST.Where(n => n.CONTENT_NAME.Equals(DenshiConst.CONTENT_NAME_TOKEN))
                                                  .First();

            var url = string.Format("{0}?{1}={2}", this.BASE_URL + connect.URL, DenshiConst.CLIENT_ID, dto.client_id);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ContentType = connect.CONTENT_TYPE;
            if (this.SYS_INFO != null && !string.IsNullOrEmpty(this.SYS_INFO.DENSHI_KEIYAKU_X_APP_ID))
            {
                // 動作に影響なし。主にクラウドサイン側でアプリケーションを識別するために使用
                req.Headers[DenshiConst.HEADERS_X_APPID] = this.SYS_INFO.DENSHI_KEIYAKU_X_APP_ID;
            }
            req.Method = Enum.GetName(typeof(HTTP_METHOD), HTTP_METHOD.POST);

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            ACCESS_TOKEN_MODEL result = null;
            if (0 < res.ContentLength)
            {
                using (res)
                {
                    using (var resStream = res.GetResponseStream())
                    {
                        var serializer = new DataContractJsonSerializer(typeof(ACCESS_TOKEN_MODEL));
                        result = (ACCESS_TOKEN_MODEL)serializer.ReadObject(resStream);
                    }
                }
            }

            return result;
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
        private HttpWebRequest CreateWebRequest(string url, string contentType, string method, REQ_COMMON dto)
        {
            // トークン取得
            // 有効時間が1時間と短い為、都度トークンを取得する
            var token = GetAccessTokenModel(dto);

            // トークン有効期間の上限
            int LimitSeconds = 10;
            if (token.Expires_In < LimitSeconds)
            {
                // 有効期間で指定した時間より残り時間が下回った場合、期限切れ対策のため待ち
                Thread.Sleep(1000 * LimitSeconds);
                // トークン再取得
                token = GetAccessTokenModel(dto);
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ContentType = contentType;
            var authorization = string.Format("{0} {1}", token.Token_Type, token.Access_Token);
            req.Headers["Authorization"] = authorization;
            if (this.SYS_INFO != null && !string.IsNullOrEmpty(this.SYS_INFO.DENSHI_KEIYAKU_X_APP_ID))
            {
                // 動作に影響なし。主にクラウドサイン側でアプリケーションを識別するために使用
                req.Headers[DenshiConst.HEADERS_X_APPID] = this.SYS_INFO.DENSHI_KEIYAKU_X_APP_ID;
            }
            req.Method = method;

            if (!"GET".Equals(method))
            {
                if (contentType == "multipart/form-data")
                {
                    // multipart/form-dataの場合
                    req = CreateRequestBody_form_data(req, (REQ_FILES_POST)dto);
                }
                else if (dto != null)
                {
                    // シリアライズ処理
                    using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                    {
                        //// DataMember属性を全てシリアライズしちゃうのでNG。
                        //// DataMember属性かつ、そのプロパティに値があるものだけシリアライズ対象に変更する。
                        //string jsonString = new JavaScriptSerializer().Serialize(dto);

                        //jsonString = jsonString.Replace("{", "");
                        //jsonString = jsonString.Replace("}", "");
                        //jsonString = jsonString.Replace("\"", "");
                        //jsonString = jsonString.Replace(",", "&");
                        //jsonString = jsonString.Replace(":", "=");

                        string jsonString = CreateJsonString<REQ_COMMON>(dto);
                        // シリアライズの内容をログに出力する
                        LogUtility.Debug(string.Format("{0} 内容【{1}】", method, jsonString));
                        streamWriter.Write(jsonString);
                    }
                }
            }

            return req;
        }
        #endregion

        /// <summary>
        /// Content-Typeがmultipart/form-dataの場合のRequest作成
        /// ファイル送信を伴うRequestの場合のみを想定
        /// </summary>
        /// <param name="req">リクエスト</param>
        /// <param name="dto">リクエスト用DTO</param>
        /// <returns></returns>
        private HttpWebRequest CreateRequestBody_form_data(HttpWebRequest req, REQ_FILES_POST dto)
        {
            if (!string.IsNullOrEmpty(dto.name))
            {
                // URLパラメータを付加するため、リクエスト(URL)再作成
                var uri = req.RequestUri;
                var ub = new UriBuilder(uri);
                var encName = RFC3986UriUtility.EscapeDataString(dto.name);
                var url = ub.Uri.ToString() + string.Format("?name={0}", encName);
                LogUtility.Debug(string.Format("【URL】:{0}", url));

                // 前回設定値を退避
                var contentType = req.ContentType;
                var headers = req.Headers["Authorization"];
                var method = req.Method;

                req = (HttpWebRequest)WebRequest.Create(url);
                req.ContentType = contentType;
                req.Headers["Authorization"] = headers;
                if (this.SYS_INFO != null && !string.IsNullOrEmpty(this.SYS_INFO.DENSHI_KEIYAKU_X_APP_ID))
                {
                    // 動作に影響なし。主にクラウドサイン側でアプリケーションを識別するために使用
                    req.Headers[DenshiConst.HEADERS_X_APPID] = this.SYS_INFO.DENSHI_KEIYAKU_X_APP_ID;
                }
                req.Method = method;
            }

            // 送信するファイルのパス
            string filePath = dto.uploadfile;
            string fileName = System.IO.Path.GetFileName(filePath);

            // 文字コード
            System.Text.Encoding enc =
                System.Text.Encoding.GetEncoding("shift_jis");
            // 区切り文字列
            string boundary = System.Environment.TickCount.ToString();

            // ContentTypeを設定
            // CreateWebRequestでも設定しているが、この形式の場合特殊なため再設定
            req.ContentType = "multipart/form-data; boundary=" + boundary;

            // POST送信するデータを作成
            string postData = "";
            var sb = new StringBuilder();

            // 文字化け対策不明のため、URLパラメータで暫定対応
            //sb.Append("--")
            //  .AppendLine(boundary)
            //  .AppendLine("Content-Disposition: form-data; name=\"name\"")
            //  .Append("\r\n")
            //  .AppendFormat("{0}\r\n", dto.name)
            //  //.AppendLine("Content-Transfer-Encoding: 7bit\r\n")
            //  ;

            sb.Append("--")
              .AppendLine(boundary)
              .Append("Content-Disposition: form-data; name=\"uploadfile\"; filename=\"")
              .AppendLine(fileName + "\"")
              .AppendLine("Content-Type: application/pdf")
              .AppendLine("Content-Transfer-Encoding: binary\r\n")
              ;

            postData = sb.ToString();

            // バイト型配列に変換
            byte[] startData = enc.GetBytes(postData);
            postData = "\r\n--" + boundary + "--\r\n";
            byte[] endData = enc.GetBytes(postData);

            // 送信するファイルを開く
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // POST送信するデータの長さを指定
                req.ContentLength = startData.Length + endData.Length + fs.Length;

                // データをPOST送信するためのStreamを取得
                using (var reqStream = req.GetRequestStream())
                {
                    // 送信するデータを書き込む
                    reqStream.Write(startData, 0, startData.Length);
                    // ファイルの内容を送信
                    byte[] readData = new byte[0x1000];
                    int readSize = 0;
                    while (true)
                    {
                        readSize = fs.Read(readData, 0, readData.Length);
                        if (readSize == 0)
                        {
                            break;
                        }
                        reqStream.Write(readData, 0, readSize);
                    }
                    reqStream.Write(endData, 0, endData.Length);
                }
            }

            return req;
        }

        /// <summary>
        /// 指定されたクラスで設定済みのプロパティをもとにシリアライズ化対象の文字列生成
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="dto">リクエスト用DTO</param>
        /// <returns></returns>
        private string CreateJsonString<T>(T dto)
            where T : IApiDto
        {
            if (dto == null)
            {
                return null;
            }

            var sb = new StringBuilder();

            foreach (var prop in dto.GetType().GetProperties())
            {
                // DataMember属性ではないプロパティは除外
                var list = prop.GetCustomAttributes(typeof(DataMemberAttribute), true);
                if (!list.Any())
                {
                    continue;
                }

                // 初期値のプロパティは未設定と判定し除外
                var value = prop.GetValue(dto, null);
                var propertyType = prop.PropertyType;
                var initValue = propertyType.IsValueType ? Activator.CreateInstance(propertyType) : null;
                if (Object.Equals(value, initValue))
                {
                    continue;
                }

                var att = (DataMemberAttribute)list.First();

                // クライアントIDは除外(トークン取得時のみ使用のため)
                if (DenshiConst.CLIENT_ID.Equals(att.Name))
                {
                    continue;
                }

                if (sb.Length != 0)
                {
                    sb.Append("&");
                }
                sb.AppendFormat("{0}={1}", att.Name, value);
            }

            return sb.ToString();
        }

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

                byte[] btjs = System.Text.Encoding.UTF8.GetBytes(errStr);
                var jsr = new DataContractJsonSerializer(typeof(T));
                //バイト列をストリームに
                var ms = new System.IO.MemoryStream(btjs);
                using (ms)
                {
                    //デシリアライズ
                    errResult = (T)jsr.ReadObject(ms);
                }

                return errResult;
            }
        }
        #endregion
    }
}
