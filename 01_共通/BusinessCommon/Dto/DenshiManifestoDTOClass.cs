using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class DenshiSearchParameterDtoCls
    {
        /// <summary>検索条件  :管理番号</summary>
        public string KANRI_ID { get; set; }
        /// <summary>検索条件  :枝番号</summary>
        public string SEQ { get; set; }
        /// <summary>検索条件  :キュー情報枝番</summary>
        public string QUE_SEQ { get; set; }
        /// <summary>検索条件  :加入者番号</summary>
        public string EDI_MEMBER_ID { get; set; }
        /// <summary>検索条件  :電子廃棄物名称CD</summary>
        public string HAIKI_NAME_CD { get; set; }
        /// <summary>検索条件  :廃棄種類CD</summary>
        public string HAIKI_SHURUI_CD { get; set; }
        /// <summary>検索条件  :電子業者CD</summary>
        public string GYOUSHA_CD { get; set; }
        /// <summary>検索条件  :電子事業場CD</summary>
        public string JIGYOUJOU_CD { get; set; }
        /// <summary>検索条件  :将軍現場CD</summary>
        public string GENBA_CD { get; set; }
        /// <summary>検索条件  :事業者区分[1.排出事業者　2.運搬業者　3.処分業者]</summary>
        public string JIGYOUSHA_KBN { get; set; }
        /// <summary>検索条件  :事業者区分[1.排出事業者　2.運搬業者　3.処分業者]</summary>
        public List<string> JIGYOUSHA_KBN_LIST { get; set; }
        /// <summary>検索条件  :事業場区分[1.排出事業場　2.運搬事業場　3.処分事業場]</summary>
        public string JIGYOUJOU_KBN { get; set; }
        /// <summary>検索条件  :事業場区分[1.排出事業場　2.運搬事業場　3.処分事業場]</summary>
        public List<string> JIGYOUJOU_KBN_LIST { get; set; }

        /// <summary>検索条件  :担当者区分[1.引渡　2.登録　3.運搬　4.報告　5.処分]</summary>
        public string TANTOUSHA_KBN { get; set; }
        /// <summary>検索条件  :担当者CD</summary>
        public string TANTOUSHA_CD { get; set; }
        /// <summary>検索条件  :有害物質CD</summary>
        public string YUUGAI_BUSSHITSU_CD { get; set; }
        /// <summary>検索条件  :排出事業者区分</summary>
        public string HST_KBN { get; set; }
        /// <summary>検索条件  :運搬業者区分</summary>
        public string UPN_KBN { get; set; }
        /// <summary>検索条件  :処分事業者区分</summary>
        public string SBN_KBN { get; set; }
        /// <summary>検索条件  :報告不要区分</summary>
        public string HOUKOKU_HUYOU_KBN { get; set; }
        /// <summary>検索条件  :廃棄物名称</summary>
        public string HAIKI_NAME { get; set; }
        /// <summary>検索条件  :担当者名</summary>
        public string TANTOUSHA_NAME { get; set; }
        /// <summary>検索条件  :事業者名</summary>
        public string JIGYOUJOU_NAME { get; set; }
        /// <summary>検索条件  :住所</summary>
        public string JIGYOUJOU_ADDRESS { get; set; }
        /// <summary>検索条件  :単位</summary>
        public string UNIT_CD { get; set; }
        /// <summary>検索条件  :荷姿CD</summary>
        public string NISUGATA_CD { get; set; }
        /// <summary>検索条件  :処分方法CD</summary>
        public string SHOBUN_HOUHOU_CD { get; set; }
         /// <summary>検索条件  :車輌CD</summary>
        public string SHARYOU_CD { get; set; }
        /// <summary>検索条件  :運搬方法CD</summary>
        public string UNPAN_HOUHOU_CD { get; set; }
        /// <summary>検索条件:廃棄物種類CD[7桁：廃棄物種類＋細分類CD]</summary>
        public string HAIKISHURUICD { get; set; }
        /// <summary>検索条件  :交付番号/マニフェスト番号</summary>
        public string MANIFEST_ID { get; set; }
        /// <summary>検索条件  :事業者区分OR条件 </summary>
        public string JIGYOUSHA_KBN_OR { get; set; }

        /// <summary>検索条件  :排出事業場電話番号</summary>
        public string JIGYOUJOU_TEL { get; set; }

        /// <summary>検索条件  :事業場番号検索FLG</summary>
        public string JIGYOUJOU_FLG { get; set; }

        /// <summary>
        /// 削除フラッグいるかどうかの判断フラッグ
        /// </summary>
        public SqlBoolean ISNOT_NEED_DELETE_FLG { get; set; }

        /// <summary>
        /// 電子事業者と電子事業場の連携業者が同じかどうかのフラッグ
        /// </summary>
        public SqlBoolean ISNEED_SAME_GYOUSHA_FLG { get; set; }
    }

}
