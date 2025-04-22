using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Logic
{
    /// <summary>
    /// ロジコンパス(システック)で使用する共通的な処理を定義
    /// </summary>
    /// <remarks>
    /// HTTPメソッドを使用する場合に呼出しもとのCatchで、WebExceptionの場合は何も処理しない事を実装すること。
    /// LogiLogic側でWebExceptionの場合、詳細なエラーをアラート表示してからスローしている。
    /// そのため、呼出し側でもアラートを表示すると2重でアラートが表示されるため。
    /// </remarks>
    public class LogiLogic
    {
        #region プロパティ
        /// <summary>ベースURL</summary>
        private string BASE_URL { get; set; }
        /// <summary>システム設定</summary>
        private M_SYS_INFO SYS_INFO { get; set; }
        /// <summary>トークン管理</summary>
        private S_SYSTEC_TOKEN SYSTEC_TOKEN { get; set; }
        #endregion

        #region 変数
        /// <summary>サーバ日付取得Dao</summary>
        private GET_SYSDATEDao sysDateDao;
        /// <summary>システム設定Dao</summary>
        private IM_SYS_INFODao sysInfoDao;
        /// <summary>ロジこん接続管理Dao</summary>
        private IS_LOGI_CONNECTDao logiConnectDao;
        /// <summary>トークン管理Dao</summary>
        private IS_SYSTEC_TOKENDao systecTokenDao;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogiLogic()
        {
            this.sysDateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.logiConnectDao = DaoInitUtility.GetComponent<IS_LOGI_CONNECTDao>();
            this.systecTokenDao = DaoInitUtility.GetComponent<IS_SYSTEC_TOKENDao>();
        }
        #endregion

        #region GETメソッド
        /// <summary>
        /// GETメソッド
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <returns></returns>
        public T HttpGET<T>(string api, string contentType)
        {
            return HttpGET<T>(this.BASE_URL, api, contentType);
        }

        /// <summary>
        /// GETメソッド
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <returns></returns>
        public T HttpGET<T>(string url, string api, string contentType)
        {
            return HttpMethodMain<T>(url, api, contentType, HTTP_METHOD.GET, null);
        }
        #endregion

        #region PUTメソッド
        /// <summary>
        /// PUTメソッド
        /// </summary>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">リクエスト用のDTO</param>
        public void HttpPut(string api, string contentType, ISystecRegistDto dto)
        {
            HttpPut(this.BASE_URL, api, contentType, dto);
        }

        /// <summary>
        /// PUTメソッド
        /// </summary>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">リクエスト用のDTO</param>
        public void HttpPut(string url, string api, string contentType, ISystecRegistDto dto)
        {
            HttpMethodMain<Object>(url, api, contentType, HTTP_METHOD.PUT, dto);
        }
        #endregion

        #region DELETEメソッド
        /// <summary>
        /// DELETEメソッド
        /// </summary>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        public void HttpDelete(string api, string contentType)
        {
            HttpDelete(this.BASE_URL, api, contentType);
        }

        /// <summary>
        /// DELETEメソッド
        /// </summary>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        public void HttpDelete(string url, string api, string contentType)
        {
            HttpMethodMain<Object>(url, api, contentType, HTTP_METHOD.DELETE, null);
        }
        #endregion

        #region メイン処理
        /// <summary>
        /// HttpMethodのメイン処理。このメソッドを直接呼んで使用することはNG
        /// </summary>
        /// <typeparam name="T">GETメソッドの戻り値の型</typeparam>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="method">メソッド名</param>
        /// <param name="dto">リクエスト用のDTO</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private T HttpMethodMain<T>(string url, string api, string contentType, HTTP_METHOD method, ISystecRegistDto dto)
        {
            T result = default(T);

            // エラーメッセージ格納用
            string ErrTitle = "";
            string ErrMsg = "";

            try
            {
                Init();

                if (string.IsNullOrEmpty(this.SYS_INFO.DIGI_CORP_ID)
                    || string.IsNullOrEmpty(this.SYS_INFO.DIGI_UID)
                    || string.IsNullOrEmpty(this.SYS_INFO.DIGI_PWD))
                {
                    // ロジこん連携に必要なシステム設定が設定されて無い場合、強制終了
                    // 強制終了＆任意のアラートを表示するためにWebExceptionをスロー
                    ErrMsg = "システム設定で関連項目が設定されてないため、ロジこんぱす連携ができません。";
                    throw new WebException();
                }

                if (string.IsNullOrEmpty(url))
                {
                    url = this.BASE_URL;
                }

                var methodName = Enum.GetName(typeof(HTTP_METHOD), method);

                // エラートラップのため、トークン発行時のmethodはPOSTに差し替える
                var savemethod = method;
                method = HTTP_METHOD.POST;
                HttpWebRequest req = CreateWebRequest(url + api, contentType, methodName);
                method = savemethod;
                
                if (HTTP_METHOD.PUT.Equals(method))
                {
                    // PUT時のみシリアライズ処理
                    using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                    {
                        string jsonString = new JavaScriptSerializer().Serialize(dto);
                        // POST内容をログに出力する
                        LogUtility.Debug("POST内容【" + jsonString + "】");
                        streamWriter.Write(jsonString);
                    }
                }

                // レスポンスを取得
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                if (0 < res.ContentLength)
                {
                    // 以降の処理はGETのみを想定。PUT,DELETEはレスポンスデータは無しのため。

                    // ストリームを取得
                    Stream st = res.GetResponseStream();
                    using (st)
                    {
                        StreamReader sr = new StreamReader(st);

                        string jsonString = sr.ReadToEnd();
                        // JSON文字列に何故かエスケープ文字、対象文字列が「"」で囲まれているとデシリアライズ時に例外が発生する。その暫定対策
                        jsonString = System.Text.RegularExpressions.Regex.Unescape(jsonString);
                        jsonString = jsonString.TrimStart('"');
                        jsonString = jsonString.TrimEnd('"');

                        byte[] btjs = System.Text.Encoding.UTF8.GetBytes(jsonString);
                        var jsr = new DataContractJsonSerializer(typeof(T));
                        //バイト列をストリームに
                        var ms = new System.IO.MemoryStream(btjs);
                        using (ms)
                        {
                            //デシリアライズ
                            return (T)jsr.ReadObject(ms);
                        }
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse errRes = (HttpWebResponse)ex.Response;
                    LogUtility.Error("HttpMethodMain", ex);

                    switch (errRes.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:             // 400
                            ErrTitle = "HTTP STATUS 400 Bad Request";
                            
                            // PUT以外はdtoが不要なためmethodで判断
                            if (HTTP_METHOD.POST.Equals(method))
                            {
                                // POSTはトークン取得専用
                                // 「invalid_grant」しか出ないはず
                                // 分かりづらいのでここだけエラーレスポンスの取得はせず固定文言としておく
                                LogUtility.Error("Error：" + "トークンの取得に失敗しました");
                                ErrMsg = ErrMsg + "トークンの取得に失敗しました" + Environment.NewLine;
                            }
                            else if (HTTP_METHOD.GET.Equals(method))
                            {
                                // GETは配送系のみ
                                var errorGET = GetErrorDto<ERROR_DELIVERY_PERFORMANCES>(errRes);
                                LogUtility.Error("Message：" + errorGET.message);
                                ErrMsg = ErrMsg + errorGET.message + Environment.NewLine;
                            }
                            else if (HTTP_METHOD.DELETE.Equals(method))
                            {
                                // DELETEは他マスタ処理(配送計画のDELETEエラーもこれで拾えるので分けない)
                                var errorDELETE = GetErrorDto<ERROR_REQUEST>(errRes);
                                LogUtility.Error("Message：" + errorDELETE.Message);
                                // errorsがNullになるケースもあるので考慮
                                if (errorDELETE.errors != null && errorDELETE.errors.Any())
                                {
                                    foreach (var errors in errorDELETE.errors)
                                    {
                                        LogUtility.Error("Error_Contents：" + errors.Error_Contents);
                                        ErrMsg = ErrMsg + errors.Error_Contents + Environment.NewLine;
                                    }
                                }
                                else
                                {
                                    //errorsがnullの場合はMessageに詳細が記載されているのでこちらを採用
                                    ErrMsg = ErrMsg + errorDELETE.Message + Environment.NewLine;
                                }
                                break;
                            }
                            else
                            {
                                if (dto.GetType() == typeof(INFO_DELIVERY_PLANS))
                                {
                                    //配送計画用の400エラー
                                    var errorBadRequest = GetErrorDto<ERROR_DELIVERY_PLANS>(errRes);
                                    LogUtility.Error("Message：" + errorBadRequest.message);
                                    //errorsがNullになるケースもあるので考慮
                                    if (errorBadRequest.Errors != null && errorBadRequest.Errors.Any())
                                    {
                                        foreach (var errors in errorBadRequest.Errors)
                                        {
                                            LogUtility.Error("Sequence_No：" + errors.Sequence_No);
                                            LogUtility.Error("Car_Id：" + errors.Car_Id);
                                            LogUtility.Error("Delivery_Date：" + errors.Delivery_Date);
                                            LogUtility.Error("Delivery_No：" + errors.Delivery_No);
                                            LogUtility.Error("Delivery_Detail_No：" + errors.Delivery_Detail_No);
                                            LogUtility.Error("Goods_Detail_No：" + errors.Goods_Detail_No);
                                            LogUtility.Error("Error_Item_Name：" + errors.Error_Item_Name);
                                            LogUtility.Error("Error_Item_Value：" + errors.Error_Item_Value);
                                            LogUtility.Error("Error_Contents：" + errors.Error_Contents);
                                            ErrMsg = ErrMsg + errors.Error_Contents + Environment.NewLine;
                                            ErrMsg = ErrMsg + errors.Error_Item_Name + "：" + errors.Error_Item_Value + Environment.NewLine;
                                        }
                                    }
                                    else
                                    {
                                        //Errorsがnullの場合はmessageに詳細が記載されているのでこちらを採用
                                        ErrMsg = ErrMsg + errorBadRequest.message + Environment.NewLine;
                                    }
                                }
                                else if (dto.GetType() == typeof(DELIVERY_PERFORMANCES))
                                {
                                    //配送実績の400エラー
                                    var errorBadRequest = GetErrorDto<ERROR_DELIVERY_PERFORMANCES>(errRes);
                                    LogUtility.Error("Message：" + errorBadRequest.message);
                                    ErrMsg = ErrMsg + errorBadRequest.message + Environment.NewLine;
                                }
                                else
                                {
                                    //その他400エラー
                                    var errorBadRequest = GetErrorDto<ERROR_REQUEST>(errRes);
                                    LogUtility.Error("Message：" + errorBadRequest.Message);
                                    //errorsがNullになるケースもあるので考慮
                                    if (errorBadRequest.errors != null && errorBadRequest.errors.Any())
                                    {
                                        foreach (var errors in errorBadRequest.errors)
                                        {
                                            LogUtility.Error("Sequence_No：" + errors.Sequence_No);
                                            LogUtility.Error("Id1：" + errors.Id1);
                                            LogUtility.Error("Id2：" + errors.Id2);
                                            LogUtility.Error("Id3：" + errors.Id3);
                                            LogUtility.Error("Id4：" + errors.Id4);
                                            LogUtility.Error("Error_Item_Name：" + errors.Error_Item_Name);
                                            LogUtility.Error("Error_Item_Value：" + errors.Error_Item_Value);
                                            LogUtility.Error("Error_Contents：" + errors.Error_Contents);
                                            ErrMsg = ErrMsg + errors.Error_Contents + Environment.NewLine;
                                            ErrMsg = ErrMsg + errors.Error_Item_Name + "：" + errors.Error_Item_Value + Environment.NewLine;
                                        }
                                    }
                                    else
                                    {
                                        //errorsがnullの場合はMessageに詳細が記載されているのでこちらを採用
                                        ErrMsg = ErrMsg + errorBadRequest.Message + Environment.NewLine;
                                    }
                                }
                            }
                            break;
                        case HttpStatusCode.Unauthorized:           // 401 トークン有効期限切れ
                            ErrTitle = "HTTP STATUS 401 Unauthorized";
                            var errorUnauthorized = GetErrorDto<ERROR>(errRes);
                            LogUtility.Error("Message：" + errorUnauthorized.Message);
                            ErrMsg = ErrMsg + errorUnauthorized.Message + Environment.NewLine;
                            //this.EXPIRES_TIME = DateTime.MinValue;
                            break;
                        case HttpStatusCode.NotFound:               // 404
                            ErrTitle = "HTTP STATUS 404 Not Found";
                            LogUtility.Error("404 Not Found");
                            ErrMsg = ErrMsg + "サーバーが見つかりません" + Environment.NewLine;
                            break;
                        case HttpStatusCode.MethodNotAllowed:       // 405
                            ErrTitle = "HTTP STATUS 405 Method Not Allowed";
                            var errorMethodNotAllowed = GetErrorDto<ERROR>(errRes);
                            LogUtility.Error("Message：" + errorMethodNotAllowed.Message);
                            ErrMsg = ErrMsg + errorMethodNotAllowed.Message + Environment.NewLine;
                            break;
                        case HttpStatusCode.Conflict:               // 409
                            ErrTitle = "HTTP STATUS 409 Conflict";
                            var errorConflict = GetErrorDto<ERROR_REQUEST>(errRes);
                            LogUtility.Error("Message：" + errorConflict.Message);
                            ErrMsg = ErrMsg + errorConflict.Message + Environment.NewLine;
                            break;
                        case HttpStatusCode.UnsupportedMediaType:   // 415
                            //API仕様書の当エラーレスポンスには
                            //Message/ExceptionMessage/ExceptionType/StackTrace
                            //と記載されているが、現在このエラーを表示させるデータをリクエストすると
                            //Message
                            //のみしか返ってこないため暫定でERRORクラスに食わせておく
                            ErrTitle = "HTTP STATUS 415 Unsupported Media Type";
                            var errorUnsupportedMediaType = GetErrorDto<ERROR>(errRes);
                            LogUtility.Error("Message：" + errorUnsupportedMediaType.Message);
                            ErrMsg = ErrMsg + errorUnsupportedMediaType.Message + Environment.NewLine;
                            break;
                        case HttpStatusCode.InternalServerError:    // 500
                            ErrTitle = "HTTP STATUS 500 Internal Server Error";
                            var errorInternalServerError = GetErrorDto<ERROR_REQUEST>(errRes);
                            LogUtility.Error("Message：" + errorInternalServerError.Message);
                            ErrMsg = ErrMsg + errorInternalServerError.Message + Environment.NewLine;
                            break;
                        default:
                            LogUtility.Error("その他エラー");
                            break;
                    }
                }
                
                //例外をスローせずアラート表示
                MessageBox.Show(ErrMsg, ErrTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
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
            if (SYS_INFO == null)
            {
                this.SYS_INFO = this.sysInfoDao.GetAllDataForCode("0");
            }

            // ベースURLの取得
            if (string.IsNullOrEmpty(BASE_URL))
            {
                // テスト用、本番用URL切替箇所
                //var dto = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_TEST_BASE_URL); //テスト用URL
                var dto = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_BASE_URL); //本番用URL
                if (dto != null)
                {
                    this.BASE_URL = dto.URL;
                }
            }
        }
        #endregion

        #region システム日付取得
        /// <summary>
        /// システム日付取得
        /// </summary>
        /// <returns></returns>
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.sysDateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        #endregion

        #region 認証API作成
        /// <summary>
        /// 認証用APIの作成
        /// </summary>
        /// <returns></returns>
        private REQ_TOKEN CreateReqTokenDto()
        {
            if (SYS_INFO == null)
            {
                this.SYS_INFO = this.sysInfoDao.GetAllDataForCode("0");
            }

            var corpid = this.SYS_INFO.DIGI_CORP_ID;
            var userid = this.SYS_INFO.DIGI_UID;
            var password = this.SYS_INFO.DIGI_PWD;

            var dto = new REQ_TOKEN();
            dto.Grant_Type = LogiConst.GRANT_TYPE;
            dto.UserName = string.Format("{0}@{1}", userid, corpid);
            dto.PassWord = password;

            return dto;
        }
        #endregion

        #region 認証
        /// <summary>
        /// 環境将軍R連動WebAPIの認証を行う
        /// </summary>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contenType">コンテンツタイプ</param>
        /// <param name="dto">認証用API(送信)</param>
        /// <returns>認証用API(結果)</returns>
        private RES_TOKEN PostSystecJson(string url, string api, string contenType, REQ_TOKEN dto)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url + api);
            req.ContentType = contenType;
            req.Method = Enum.GetName(typeof(HTTP_METHOD), HTTP_METHOD.POST);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                string jsonString = new JavaScriptSerializer().Serialize(dto);
                // TODO:シリアライズが出来ない。。。
                //jsonString = "grant_type=password&username=user02@edisontest&password=user02&";

                //やはり、{とか"とか:とか,が混じる形式は許されないらしい
                //他APIはともかく、トークンの取得時の
                //リクエストボディ発行時はこのロジックを使う必要がありそう

                //もしかして指定する「ContentType」によって文字列の作成方法を変える必要がある…？
                //現状「application/json」で飛ばしてもトークン取得できてしまうので
                //テストサーバー側に問題アリ(本来は415エラー)

                //トークン取得でGETを飛ばすと400エラー(本来は405エラー)

                jsonString = jsonString.Replace("{", "");
                jsonString = jsonString.Replace("}", "");
                jsonString = jsonString.Replace("\"", "");
                jsonString = jsonString.Replace(",", "&");
                jsonString = jsonString.Replace(":", "=");

                streamWriter.Write(jsonString);
            }

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            RES_TOKEN result = null;
            if (0 < res.ContentLength)
            {
                using (res)
                {
                    using (var resStream = res.GetResponseStream())
                    {
                        var serializer = new DataContractJsonSerializer(typeof(RES_TOKEN));
                        result = (RES_TOKEN)serializer.ReadObject(resStream);
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
        /// <returns></returns>
        private HttpWebRequest CreateWebRequest(string url, string contentType, string method)
        {
            GetSystecToken();

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ContentType = contentType;
            req.Headers["Authorization"] = GetAuthorization(this.SYSTEC_TOKEN);
            req.Method = method;
            // TODO:タイムアウトの設定必要？
            //req.Timeout = 20;

            return req;
        }

        /// <summary>
        /// Authorization取得
        /// </summary>
        /// <param name="token">トークン管理情報</param>
        /// <returns></returns>
        private string GetAuthorization(S_SYSTEC_TOKEN token)
        {
            if (token == null)
            {
                return null;
            }
            else
            {
                // スペース区切りで一括設定しないと認証されない
                return string.Format("{0} {1}", token.TOKEN_TYPE, token.ACCESS_TOKEN);
            }
        }
        #endregion

        #region トークン取得
        /// <summary>
        /// トークン管理の取得
        /// </summary>
        private void GetSystecToken()
        {
            // システム日付取得
            var now = getDBDateTime();

            // キャッシュされた有効期間の判定
            if (this.SYSTEC_TOKEN == null || this.SYSTEC_TOKEN.EXPIRED_DATE <= now)
            {
                S_SYSTEC_TOKEN token = this.systecTokenDao.GetDataByUserId(this.SYS_INFO.DIGI_UID);
                // DBに格納された有効期間の判定
                if (token == null || token.EXPIRED_DATE <= now)
                {
                    // 認証用API作成
                    var dto = CreateReqTokenDto();

                    // トークンに関わるコネクト情報取得
                    var connectDto = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_TOKEN);
                    if (connectDto != null)
                    {
                        // 認証用API取得
                        RES_TOKEN result = PostSystecJson(this.BASE_URL, connectDto.URL, connectDto.CONTENT_TYPE, dto);
                        if (result != null)
                        {
                            var tokenEntity = new S_SYSTEC_TOKEN();
                            tokenEntity.USER_ID = this.SYS_INFO.DIGI_UID;
                            tokenEntity.ACCESS_TOKEN = result.Access_Token;
                            tokenEntity.TOKEN_TYPE = result.Token_Type;
                            tokenEntity.EXPIRES_IN = result.Expires_In;
                            // 有効期間設定。-1時間はバッファ。システム時間はDBから取得すること
                            tokenEntity.EXPIRED_DATE = getDBDateTime().AddSeconds(result.Expires_In).AddHours(LogiConst.BUFFER_TIME_HOUR);
                            tokenEntity.DELETE_FLG = false;

                            if (token == null)
                            {
                                // トークン管理
                                var dataBinder = new DataBinderLogic<S_SYSTEC_TOKEN>(tokenEntity);
                                dataBinder.SetSystemProperty(tokenEntity, false);

                                this.systecTokenDao.Insert(tokenEntity);
                            }
                            else
                            {
                                tokenEntity.UPDATE_PC = SystemInformation.ComputerName;
                                tokenEntity.UPDATE_USER = SystemProperty.UserName;
                                tokenEntity.UPDATE_DATE = getDBDateTime();
                                tokenEntity.TIME_STAMP = token.TIME_STAMP;

                                this.systecTokenDao.Update(tokenEntity);
                            }

                            token = tokenEntity;
                        }
                    }
                }

                this.SYSTEC_TOKEN = token;
            }
        }
        #endregion

        #region エラー情報取得
        /// <summary>
        /// 例外処理時のエラー情報取得処理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errRes"></param>
        /// <returns></returns>
        private T GetErrorDto<T>(HttpWebResponse errRes)
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
