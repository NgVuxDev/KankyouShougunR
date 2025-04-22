using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using r_framework.Configuration;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime;

//TODO・メモ
//GETは使わなさそうなのでPOSTのみとしている。
//各APIのURL、Content-Type、リクエストパラメータ作成用のDtoを渡せば勝手に処理する。

//引数のdtoに関しては、そのAPIのリクエストに関係ない項目が混ざった状態でRequestBody作成を行っても問題ないようなので、
//ここで使いそうな値を全て一まとめにしたNaviRequestDtoをそのまま使用している。
//ある程度汎用的に使えるはずだが、不要なパラメータが混ざっていたらリクエストを弾く、
//といったシステムと連携する場合は設計を見直す必要がある。
namespace Shougun.Core.ExternalConnection.ExternalCommon.Logic
{
    /// <summary>
    /// NAVI TIMEで使用する共通的な処理を定義
    /// </summary>
    public class NaviLogic
    {
        #region プロパティ
        /// <summary>ベースURL</summary>
        private string BASE_URL { get; set; }
        /// <summary>システム設定</summary>
        private M_SYS_INFO SYS_INFO { get; set; }
        #endregion

        #region 変数
        /// <summary>システム設定Dao</summary>
        private IM_SYS_INFODao sysInfoDao;
        /// <summary>NAVITIME接続管理Dao</summary>
        private IS_NAVI_CONNECTDao naviConnectDao;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NaviLogic()
        {
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.naviConnectDao = DaoInitUtility.GetComponent<IS_NAVI_CONNECTDao>();
        }
        #endregion

        //GET、PUT、DELETEは使わないので未作成
        #region POSTメソッド
        /// <summary>
        /// POSTメソッド
        /// </summary>
        /// <typeparam name="T">API通信の対象Dtoの型</typeparam>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">POST時に指定するパラメータ</param>
        /// <param name="result">API通信結果</param>
        /// <returns>true:処理成功, false:処理失敗</returns>
        public bool HttpPOST<T>(string api, string contentType, NaviRequestDto dto, out T result)
            where T : IApiDto
        {
            return HttpPOST<T>(this.BASE_URL, api, contentType, dto, out result);
        }

        /// <summary>
        /// POSTメソッド
        /// </summary>
        /// <typeparam name="T">API通信の対象Dtoの型</typeparam>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">POST時に指定するパラメータ</param>
        /// <param name="result">API通信結果</param>
        /// <returns>true:処理成功, false:処理失敗</returns>
        public bool HttpPOST<T>(string url, string api, string contentType, NaviRequestDto dto, out T result)
            where T : IApiDto
        {
            return HttpMethodMain<T>(url, api, contentType, dto, out result);
        }
        #endregion

        #region メイン処理
        /// <summary>
        /// HttpMethodのメイン処理。このメソッドを直接呼んで使用することはNG
        /// </summary>
        /// <typeparam name="T">>API通信の対象Dtoの型</typeparam>
        /// <param name="url">ベースURL</param>
        /// <param name="api">API毎のURL</param>
        /// <param name="contentType">コンテンツタイプ</param>
        /// <param name="dto">POST時に指定するパラメータ</param>
        /// <param name="result">API通信結果</param>
        /// <returns>true:処理成功, false:処理失敗</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool HttpMethodMain<T>(string url, string api, string contentType, NaviRequestDto dto, out T result)
            where T : IApiDto
        {
            try
            {
                Init();

                if (string.IsNullOrEmpty(url))
                {
                    url = this.BASE_URL;
                }
                // 送信先のURL
                string URL = url + api;

                // メソッドを指定
                var methodName = Enum.GetName(typeof(HTTP_METHOD), HTTP_METHOD.POST);

                // リクエスト発行
                HttpWebRequest req = CreateWebRequest(url + api, contentType, methodName, dto);

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
                MessageBox.Show("エラーしました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// <param name="dto">POST時に指定するパラメータ</param>
        /// <returns></returns>
        private HttpWebRequest CreateWebRequest(string url, string contentType, string method, NaviRequestDto dto)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // Content-Type指定
            req.ContentType = contentType;

            // Method指定
            req.Method = method;

            // bodyの作り方はContent-Typeによって変わるため分岐
            if (contentType == WebAPI_ContentType.APPLICATION_JSON)
            {
                // application/jsonの場合
                req = CreateRequestBody_json(req, dto);
            }
            else if (contentType == WebAPI_ContentType.APPLICATION_X_WWW_FORM_URLENCODED)
            {
                // application/x-www-form-urlencodedの場合
                req = CreateRequestBody_x_www_form_urlencoded(req, dto);
            }
            else if (contentType == WebAPI_ContentType.MULTIPART_FORM_DATA)
            {
                // multipart/form-dataの場合
                req = CreateRequestBody_form_data(req, dto);
            }
            return req;
        }
        #endregion

        #region リクエストbody作成(application/json)
        /// <summary>
        /// Content-Typeがapplication/jsonの場合のRequestBody作成
        /// </summary>
        /// <param name="req">リクエスト</param>
        /// <param name="dto">POST時に指定するパラメータ</param>
        /// <returns></returns>
        private HttpWebRequest CreateRequestBody_json(HttpWebRequest req, NaviRequestDto dto)
        {
            // Dtoをjson文字列にシリアライズするだけ
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                string jsonString = new JavaScriptSerializer().Serialize(dto);
                streamWriter.Write(jsonString);
            }
            return req;
        }
        #endregion

        #region リクエストbody作成(application/x-www-form-urlencoded)
        /// <summary>
        /// Content-Typeがapplication/x-www-form-urlencodedの場合のRequestBody作成
        /// </summary>
        /// <param name="req">リクエスト</param>
        /// <param name="dto">POST時に指定するパラメータ</param>
        /// <returns></returns>
        private HttpWebRequest CreateRequestBody_x_www_form_urlencoded(HttpWebRequest req, NaviRequestDto dto)
        {
            // Dtoをjson文字列にシリアライズする
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                string jsonString = new JavaScriptSerializer().Serialize(dto);

                if (req.Address.AbsolutePath.Contains(NaviConst.naviGET_EXPERIENCE))
                {
                    // 「6.案件の実績情報取得」でPOSTパラメーターが「matterCode」だと取得できない
                    // 例外的に「MatterCode」に変更して暫定対応とします
                    jsonString = jsonString.Replace("matterCode", "MatterCode");
                }

                // 文字列をapplication/x-www-form-urlencoded形式に無理矢理変換
                jsonString = jsonString.Replace("{", "");
                jsonString = jsonString.Replace("}", "");
                jsonString = jsonString.Replace("\"", "");
                jsonString = jsonString.Replace(",", "&");
                jsonString = jsonString.Replace(":", "=");
                //jsonString = jsonString.Replace("null", "");

                streamWriter.Write(jsonString);
            }
            return req;
        }
        #endregion

        #region リクエストbody作成(multipart/form-data)
        /// <summary>
        /// Content-Typeがmultipart/form-dataの場合のRequestBody作成
        /// ファイル送信が必要な場合は必ずここで作成
        /// </summary>
        /// <param name="req">リクエスト</param>
        /// <param name="dto">POST時に指定するパラメータ</param>
        /// <returns></returns>
        private HttpWebRequest CreateRequestBody_form_data(HttpWebRequest req, NaviRequestDto dto)
        {
            // 区切り文字列
            string boundary = Environment.TickCount.ToString();

            // ContentTypeを設定
            // CreateWebRequestでも設定しているが、この形式の場合特殊なため再設定
            req.ContentType = "multipart/form-data; boundary=" + boundary;

            // 文字コード
            Encoding enc = Encoding.GetEncoding("UTF-8");

            StringBuilder sb = new StringBuilder();

            if (dto.processingId != null)
            {
                // ProcessingIdを指定
                sb = parameterPostCommon(sb, boundary, dto.processingId, "processingId");
            }
            else if (dto.targetDate != null)
            {
                // 処理は作ったがどうしてもエラーが返ってくるため
                // application/x-www-form-urlencoded形式で飛ばすようにした
                // targetDateを指定
                sb = parameterPostCommon(sb, boundary, dto.targetDate, "targetDate");
                // userCodeを指定
                sb = parameterPostCommon(sb, boundary, dto.userCode, "userCode");
            }
            else
            {
                // 送信するファイルのパス
                string fileName = Path.GetFileName(dto.filePath);

                if (!File.Exists(dto.filePath))
                {
                    return null;
                }

                //ファイルのmd5計算
                string md5 = createMD5(dto.filePath);
                if (md5 == "")
                {
                    return null;
                }

                //ファイルパス送信版
                //送信するファイルを開く
                byte[] readData;
                using (FileStream fs = new FileStream(dto.filePath, FileMode.Open, FileAccess.Read))
                {
                    //送信するデータ（ファイル）を読み込む
                    readData = new byte[fs.Length];
                    fs.Read(readData, 0, readData.Length);
                }

                // 送信するファイルを指定
                sb = parameterFile(sb, boundary, dto.filePath, fileName, enc, readData);
                // ファイルの文字コードを指定
                sb = parameterPostCommon(sb, boundary, "UTF-8", "fileType");
                // MD5を指定指定
                sb = parameterMD5(sb, boundary, md5);
            }
            // 訪問先マスタ登録では「addValidation」もあるが、使わなければ設置しない
            // 郵便番号バリデーションチェック
            // 郵便番号の住所と住所の検索結果を比較して一致してなければエラー、精度C以下であればメッセージを出すチェック

            // paramをエンコードする
            byte[] headers = enc.GetBytes(sb.ToString());

            // 終端データ設定
            string param = "\r\n--" + boundary + "--\r\n";
            byte[] delimiter = enc.GetBytes(param);

            //POST送信するデータの長さを指定(ヘッダ＠パラメータの長さ＋終端)
            req.ContentLength = headers.Length + delimiter.Length;

            //データをPOST送信するためのStreamを取得
            using (Stream reqStream = req.GetRequestStream())
            {
                //送信するデータ（headers）を書き込む
                reqStream.Write(headers, 0, headers.Length);

                //送信するデータ（終端）を書き込む
                reqStream.Write(delimiter, 0, delimiter.Length);
            }
            return req;
        }

        #endregion

        #region md5
        /// <summary>
        /// 指定された文字列に対してMD5のハッシュ関数を適用した結果を生成
        /// </summary>
        /// <param name="filepath">対象ファイルのフルパス</param>
        /// <returns></returns>
        private string createMD5(string filepath)
        {
            string returnValue = "";
            try
            {
                byte[] bs;
                //ファイルパス送信版用
                //var tes = @"C:\edison\R-NAVITIME\テストコード\navitimetest\navitimetest\CSV\APIhaisou.csv";
                //FileStream fs = new FileStream(tes, FileMode.Open, FileAccess.Read, FileShare.Read);
                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    //MD5CryptoServiceProviderオブジェクトを作成
                    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                    {
                        //一旦List⇒stringに変換する必要がある

                        //ハッシュ値を計算する
                        bs = md5.ComputeHash(fs);  //ファイルパス送信版
                        //byte[] bs = md5.ComputeHash(Encoding.UTF8.GetBytes(filepath));  //string送信版
                    }
                }

                //byte型配列を16進数の文字列に変換
                StringBuilder result = new StringBuilder();
                foreach (byte b in bs)
                {
                    result.Append(b.ToString("x2"));
                }

                returnValue = result.ToString();
            }
            catch (Exception ex)
            {

            }

            return returnValue;
        }
        #endregion

        #region パラメータの作成(multipart/form-dataのリクエスト作成で使用)
        /// <summary>
        /// ファイル
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="boundary"></param>
        /// <param name="text"></param>
        /// <param name="fileName"></param>
        /// <param name="enc"></param>
        /// <param name="readData"></param>
        /// <returns></returns>
        private StringBuilder parameterFile(StringBuilder sb, string boundary, string text, string fileName, Encoding enc, byte[] readData)
        {
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"file\"; filename=\"");
            sb.Append(fileName + "\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: text/csv");
            sb.Append("\r\n");
            sb.Append("Content-Transfer-Encoding: UTF-8");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append(enc.GetString(readData));   //ファイルパス送信版
            sb.Append("\r\n");
            return sb;
        }
        /// <summary>
        /// md5
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="boundary"></param>
        /// <param name="md5"></param>
        /// <returns></returns>
        private StringBuilder parameterMD5(StringBuilder sb, string boundary, string md5)
        {
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"md5\"");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append(md5);
            //ここだけ改行するとエラーしてしまうため、汎用から分けた
            //sb.Append("\r\n");
            return sb;
        }
        /// <summary>
        /// 汎用POST用パラメータ引渡し
        /// ProcessingId,TargetDate,UserCodeで使用
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="boundary"></param>
        /// <param name="param"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private StringBuilder parameterPostCommon(StringBuilder sb, string boundary, string param, string name)
        {
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\"", name);
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append(param);
            sb.Append("\r\n");
            return sb;
        }
        #endregion

        #region 初期化
        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Init()
        {
            // ロジこん方式(テーブルにURL用意)でテスト用本番用のURL切替を行うなら使うのでとりあえず残しておく
            // システム設定の取得
            this.SYS_INFO = this.SYS_INFO ?? this.sysInfoDao.GetAllDataForCode("0");

            // ベースURLの取得
            if (string.IsNullOrEmpty(BASE_URL))
            {
                // テスト用、本番用URL切替箇所
                //var dto = this.naviConnectDao.GetDataByContentName(NaviConst.CONTENT_NAME_TEST_BASE_URL);   // テスト用URL
                var dto = this.naviConnectDao.GetDataByContentName(NaviConst.CONTENT_NAME_BASE_URL);   // 本番用URL
                if (dto != null)
                {
                    this.BASE_URL = dto.URL;
                }

                // 会社ID追加
                if (this.SYS_INFO != null && !string.IsNullOrEmpty(this.SYS_INFO.NAVI_CORP_ID))
                {
                    this.BASE_URL = this.BASE_URL + "/" + this.SYS_INFO.NAVI_CORP_ID;
                }
            }
        }
        #endregion

        #region CSV出力処理
        /// <summary>
        /// CSV出力処理
        /// </summary>
        /// <remarks>
        /// NAVITIMEのAPI連携時に必要なCSV出力処理
        /// 環境将軍Rのログと同じ場所にCSVファイルを作成
        /// </remarks>
        /// <param name="fileName">ファイル名</param>
        /// <param name="dataList">出力対象のデータリスト</param>
        /// <returns>CSVファイルのフルパス</returns>
        public string OutputCSV(string fileName, List<string> dataList)
        {
            if (string.IsNullOrEmpty(fileName) || dataList == null || dataList.Count == 0)
            {
                // 引数が不正な場合はブランクを返す
                return string.Empty;
            }

            var name = fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

            // パスはログと同じ場所を指定
            string filepath = AppData.LogOutputDirectoryPath;

            // ファイルのフルパス
            string fullName = Path.Combine(filepath, name);

            // ファイルのエンコード(UTF-8)指定
            Encoding encoding = new UTF8Encoding(true);

            try
            {
                // ファイル展開
                using (var sw = new StreamWriter(fullName, false, encoding))
                {
                    // 書き込み
                    dataList.ForEach(data => sw.WriteLine(data));
                }
                return fullName;
            }
            catch (Exception ex)
            {
                LogUtility.Error("OutputCSV", ex);
                throw;
            }
        }
        #endregion
    }
}
