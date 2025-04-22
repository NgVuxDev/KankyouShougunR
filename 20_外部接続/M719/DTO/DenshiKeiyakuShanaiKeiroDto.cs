// $Id: DenshiKeiyakuShanaiKeiroFindDto.cs 24958 2014-07-08 06:41:18Z nagata $
using System;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.DTO
{
    public class DenshiKeiyakuShanaiKeiroFindDto
    {
        /// <summary>
        /// 社内経路CD
        /// </summary>
        public Int16 DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD { get; set; }
        /// <summary>
        /// 社員CD
        /// </summary>
        public String SHAIN_CD { get; set; }
        /// <summary>
        /// 社員名
        /// </summary>
        public String SHAIN_NAME { get; set; }
        /// <summary>
        /// 最終更新者
        /// </summary>
        public String UPDATE_USER { get; set; }
        /// <summary>
        /// 最終更新日時
        /// </summary>
        public String UPDATE_DATE { get; set; }
        /// <summary>
        /// 作成者
        /// </summary>
        public String CREATE_USER { get; set; }
        /// <summary>
        /// 作成日時
        /// </summary>
        public String CREATE_DATE { get; set; }
    }
}
