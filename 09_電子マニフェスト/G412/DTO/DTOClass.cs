using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.ElectronicManifest.TuusinnRirekiShoukai
{

    /// <summary>
    /// 通信履歴取得検索条件DTO
    /// </summary>
    public class MeisaiInfoDTOCls
    {
        /// <summary>
        /// 検索条件 :マニフェスト番号From
        /// </summary>
        public String MANIFEST_ID_FROM { get; set; }
        /// <summary>
        /// 検索条件 :マニフェスト番号To
        /// </summary>
        public String MANIFEST_ID_TO { get; set; }
        /// <summary>
        /// 検索条件 :状態
        /// </summary>
        public String STATUS_FLAG { get; set; }
        /// <summary>
        /// 日付区分
        /// </summary>
        public string HIDZUKE_KBN { get; set; }
        /// <summary>
        /// 日付From
        /// </summary>
        public DateTime HIDZUKE_FROM { get; set; }
        /// <summary>
        /// 日付To
        /// </summary>
        public DateTime HIDZUKE_TO { get; set; }

    }
}
