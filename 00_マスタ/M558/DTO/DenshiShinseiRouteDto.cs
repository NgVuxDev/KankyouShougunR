// $Id: DenshiShinseiRouteFindDto.cs 24958 2014-07-08 06:41:18Z nagata $
using System;

namespace Shougun.Core.Master.DenshiShinseiRoute.DTO
{
    public class DenshiShinseiRouteFindDto
    {
        /// <summary>
        /// 電子申請経路CD
        /// </summary>
        public Int16 DENSHI_SHINSEI_ROUTE_CD { get; set; }
        /// <summary>
        /// 部署CD
        /// </summary>
        public String BUSHO_CD { get; set; }
        /// <summary>
        /// 部署略称名
        /// </summary>
        public String BUSHO_NAME_RYAKU { get; set; }
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
