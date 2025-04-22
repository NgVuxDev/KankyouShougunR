using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.ManifestCheckHyo
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class SerchCheckManiDtoCls
    {
        // 検索条件
        /// <summary> 検索条件  :チェック条件</summary>
        /// <remarks> 1.交付年月日</remarks>
        /// <remarks> 2.運搬終了日</remarks>
        /// <remarks> 3.処分終了日</remarks>
        /// <remarks> 4.最終処分終了日</remarks>
        /// <remarks> 5.交付年月日なし</remarks>
        /// <remarks> 6.紐付け不整合チェック</remarks>
        public string JOUKEN { get; set; }

        /// <summary> 検索条件  :範囲開始日</summary>
        public string DATE_START { get; set; }

        /// <summary> 検索条件  :範囲終了日</summary>
        public string DATE_END { get; set; }

        /// <summary> 検索条件  :チェック分類</summary>
        /// <remarks> 1.合算</remarks>
        /// <remarks> 2.紙のみ</remarks>
        /// <remarks> 3.処分終了日</remarks>
        public string BUNRUI { get; set; } 
        // 20140623 ria EV004852 一覧と抽出条件の変更 start
        /// <summary> 検索条件  :廃棄物区分CD</summary>
        public string S_HAIKI_KBN { get; set; }
        /// <summary> 検索条件  :交付番号</summary>
        public string S_MANIFEST_ID { get; set; }
        // 20140623 ria EV004852 一覧と抽出条件の変更 end

        public string S_SYSTEM_ID { get; set; }

        public string UPN_ROUTE_NO { get; set; }
        /// <summary> 検索条件  :拠点</summary>
        public string KYOTEN { get; set; }
        /// <summary> 検索条件  :マスタ削除FLG</summary>
        public bool DELETE_FLG { get; set; }
        /// <summary> 検索条件  :予約マニFLG</summary>
        public bool YOYAKU_FLG { get; set; }
        /// <summary> 検索条件  :運搬受託者CD</summary>
        public string UPN_CD { get; set; }
        /// <summary> 検索条件  :処分受託者CD</summary>
        public string SBNJ_CD { get; set; }
        /// <summary> 検索条件  :処分事業場CD</summary>
        public string SBNB_CD { get; set; }
        /// <summary> 検索条件  :地域CD</summary>
        public string CHIIKI_CD { get; set; }
    }

    internal class DTOClass
    {
    }
}
