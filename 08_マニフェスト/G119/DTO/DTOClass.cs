using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.SampaiManifestoChokkou
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class SerchParameterDtoCls
    {
        /// <summary>検索条件  :システムID</summary>
        public string SYSTEM_ID { get; set; }

        /// <summary>検索条件  :枝番</summary>
        public string SEQ { get; set; }

        /// <summary>検索条件  :廃棄物区分CD</summary>
        public string HAIKI_KBN_CD { get; set; }

        /// <summary>検索条件  :連携伝種区分CD</summary>
        public string RENKEI_DENSHU_KBN_CD { get; set; }

        /// <summary>検索条件  :連携システムID</summary>
        public string RENKEI_SYSTEM_ID { get; set; }

        /// <summary>検索条件  :連携明細システムID</summary>
        public string RENKEI_MEISAI_SYSTEM_ID { get; set; }

        // 20140604 ria 不具合EV4133 start‏
        /// <summary>検索条件  :拠点CD</summary>
        public string KYOTEN { get; set; }
        // 20140604 ria 不具合EV4133 end‏

        // 20140606 ria No.730 規定値機能の追加について start
        /// <summary>検索条件  :1次マニ区分</summary>
        public string FIRST_MANIFEST_KBN { get; set; }
        // 20140606 ria No.730 規定値機能の追加について end
    }

    /// <summary>
    /// 混合種類マスタ検索
    /// </summary>
    public class GetKongouNameDtoCls
    {
        /// <summary>検索条件  :混合種類CD</summary>
        public string KONGOU_SHURUI_CD { get; set; }
    }

    /// <summary>
    /// マニ明細(T_MANIFEST_DETAIL)検索
    /// </summary>
    public class GetSysIdSeqDtoCls
    {
        /// <summary>検索条件  :システムID</summary>
        public string SYSTEM_ID { get; set; }
        /// <summary>検索条件  :シーケンス</summary>
        public string SEQ { get; set; }
    }

    ///// <summary>
    ///// 交付番号検索
    ///// </summary>
    //public class SerchKohuDtoCls
    //{
    //    /// <summary>検索条件  :交付番号</summary>
    //    public string MANIFEST_ID { get; set; }
    //}

    /// <summary>
    /// 単位名称取得
    /// </summary>
    public class GetUnitDtoCls
    {
        /// <summary>検索条件  :単位CD</summary>
        public string UNIT_CD { get; set; }
    }

    /// <summary>
    /// 廃棄物種類ポップアップ用検索
    /// </summary>
    public class GetHaikiShuruiDtoCls
    {
        /// <summary>検索条件  :廃棄物区分CD</summary>
        public string HAIKI_KBN_CD { get; set; }
        /// <summary>検索条件  :廃棄物種類CD</summary>
        public string HAIKI_SHURUI_CD { get; set; }
        /// <summary>検索条件  :FROM廃棄物種類CD</summary>
        public string FROM_HAIKI_SHURUI_CD { get; set; }
        /// <summary>検索条件  :TO廃棄物種類CD</summary>
        public string TO_HAIKI_SHURUI_CD { get; set; }
    }

    /// <summary>
    /// 運転者検索
    /// </summary>
    public class GetUntenshaDtoCls
    {
        /// <summary>検索条件  :社員CD</summary>
        public string SHAIN_CD { get; set; }
    }

    ///// <summary>
    ///// 処分担当者検索
    ///// </summary>
    //public class GetShobunTantoushaDtoCls
    //{
    //    /// <summary>検索条件  :社員CD</summary>
    //    public string SHAIN_CD { get; set; }
    //}

    /// <summary>
    /// 車輌検索
    /// </summary>
    public class GetCarDataDtoCls
    {
        /// <summary>検索条件  :車輌CD</summary>
        public string SHARYOU_CD { get; set; }
        /// <summary>検索条件  :業者CD</summary>
        public string GYOUSHA_CD { get; set; }
        /// <summary>検索条件  :車種CD</summary>
        public string SHASYU_CD { get; set; }
        /// <summary>検索条件  :交付年月日</summary>
        public SqlDateTime KOFU_DATE { get; set; }
    }

    // 20140513 ria No.679 伝種区分連携 start
    /// <summary>
    /// 伝種区分より検索
    /// </summary>
    public class SerachDenshuDtoCls
    {
        /// <summary>検索条件  :伝種区分</summary>
        public string RENKEI_KBN { get; set; }

        /// <summary>検索条件  :連携ID</summary>
        public string RENKEI_ID { get; set; }

        /// <summary>検索条件  :連携明細ID</summary>
        public string RENKEI_MEISAI_ID { get; set; }

        /// <summary>検索条件  :マニFlag</summary>
        public int RENKEI_MANI_FLAG { get; set; }

        /// <summary>検索条件  :チェックモード区分</summary>
        public int CHK_MODE_KBN { get; set; }
    }
    // 20140513 ria No.679 伝種区分連携 end

    // 20140523 ria No.679 伝種区分連携 start
    /// <summary>
    /// 連携システムIDより「連携番号」を取得する
    /// </summary>
    public class SerchRenkeiBangoDtoCls
    {
        /// <summary>検索条件  :システムID</summary>
        public string SYSTEM_ID { get; set; }

        /// <summary>検索条件  :枝番</summary>
        public string SEQ { get; set; }

        /// <summary>検索条件  :廃棄物区分CD</summary>
        public string HAIKI_KBN_CD { get; set; }

        /// <summary>検索条件  :連携伝種区分CD</summary>
        public string RENKEI_DENSHU_KBN_CD { get; set; }

        /// <summary>検索条件  :連携システムID</summary>
        public string RENKEI_SYSTEM_ID { get; set; }

        /// <summary>検索条件  :連携明細システムID</summary>
        public string RENKEI_MEISAI_SYSTEM_ID { get; set; }

        /// <summary>検索条件  :マニFlag</summary>
        public int RENKEI_MANI_FLAG { get; set; }
    }
    // 20140523 ria No.679 伝種区分連携 end

    public class DateCompare
    {
        public DateTime dt { get; set; }

        public string kbn { get; set; }

        /// <summary>日付比較</summary>
        public DateCompare(DateTime dt, string kbn)
        {
            this.dt = dt;
            this.kbn = kbn;
        }
        public void Compare(DateTime dt, string kbn)
        {
            if (dt < this.dt)
            {
                this.dt = dt;
                this.kbn = kbn;
            }
        }
    }
}
