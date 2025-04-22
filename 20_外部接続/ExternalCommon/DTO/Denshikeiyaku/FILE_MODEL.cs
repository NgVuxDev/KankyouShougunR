using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    /// <summary>
    /// ファイルのレスポンス
    /// </summary>
    [DataContract]
    public class FILE_MODEL : IApiDto
    {
        /// <summary>ファイルのID</summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>ファイルのタイトル</summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>書類内のファイルの順序</summary>
        [DataMember(Name = "order")]
        public int Order { get; set; }

        /// <summary>入力項目の一覧</summary>
        [DataMember(Name = "widgets")]
        public List<WIDGET_MODEL> Widgets { get; set; }
    }
}
