using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Logic
{
    /// <summary>
    /// リクエスト
    /// </summary>
    public class HttpRequestApiWanSign
    {
        /// <summary>
        /// ルート
        /// </summary>
        private string route = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="route"></param>
        public HttpRequestApiWanSign(string route)
        {
            this.route = route;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public T ExecutePost<D, T>(D requestDto)
            where T : IApiDto
            where D : IApiDto
        {
            try
            {
                //サービスポイントマネージャー
                ServicePointManager.SecurityProtocol = ExternalConst.TLS12;

                // リクエスト
                var request = (HttpWebRequest)WebRequest.Create(DenshiWanSignConst.URL + this.route);

                string jsonRequest = CreateJsonString(requestDto);
                var data = Encoding.UTF8.GetBytes(jsonRequest);

                // メソッド
                request.Method = "POST";
                ////コンテンツタイプ
                request.ContentType = "application/json";
                //コンテンツサイズ
                request.ContentLength = data.Length;
                //サービスポイント
                request.ServicePoint.Expect100Continue = false;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                //レスポンス
                T responseDto = (T)Activator.CreateInstance(typeof(T));
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream reader = response.GetResponseStream())
                        {
                            var serializer = new DataContractJsonSerializer(typeof(T));
                            responseDto = (T)serializer.ReadObject(reader);
                        }
                    }
                }

                return responseDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Jsonを変換する
        /// </summary>
        /// <typeparam name="T">クラス名</typeparam>
        /// <param name="dto">パラメータ</param>
        /// <returns></returns>
        private string CreateJsonString<T>(T dto) where T : IApiDto
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

                if (sb.Length != 0)
                {
                    sb.Append(",");
                }
                sb.AppendFormat("\"{0}\":\"{1}\"", att.Name, value);
            }

            return "{" + sb.ToString() + "}";
        }
    }
}
