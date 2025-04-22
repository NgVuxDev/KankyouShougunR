using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku
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

        // 20140604 katen 不具合No.4133 start‏
        /// <summary>検索条件  :拠点</summary>
        public string KYOTEN { get; set; }
        // 20140604 katen 不具合No.4133 end‏

        // 20140609 katen No.730 規定値機能の追加について start
        /// <summary>検索条件  :マニフェスト一次二次区分</summary>
        public string FIRST_MANIFEST_KBN { get; set; }
        // 20140609 katen No.730 規定値機能の追加について end
    }

    /// <summary>
    /// 混合種類マスタ検索
    /// </summary>
    public class GetKongouNameDtoCls
    {
        /// <summary>検索条件  :混合種類CD</summary>
        public string KONGOU_SHURUI_CD { get; set; }
    }

    ///// <summary>
    ///// マニフェスト換算値検索
    ///// </summary>
    //public class GetKanSanDtoCls
    //{
    //    /// <summary>検索条件  :廃棄物種類CD</summary>
    //    public string HAIKI_SHURUI_CD { get; set; }

    //    /// <summary>検索条件  :廃棄物名称CD</summary>
    //    public string HAIKI_NAME_CD { get; set; }

    //    /// <summary>検索条件  :荷姿CD</summary>
    //    public string NISUGATA_CD { get; set; }

    //    /// <summary>検索条件  :単位CD</summary>
    //    public string UNIT_CD { get; set; }
    //}
    ///// <summary>
    ///// マニフェスト減容換算値検索
    ///// </summary>
    //public class GetGenYouDtoCls
    //{
    //    /// <summary>検索条件  :廃棄物種類CD</summary>
    //    public string HAIKI_SHURUI_CD { get; set; }

    //    /// <summary>検索条件  :廃棄物名称CD</summary>
    //    public string HAIKI_NAME_CD { get; set; }

    //    /// <summary>検索条件  :処分方法CD</summary>
    //    public string SHOBUN_HOUHOU_CD { get; set; }
    //}

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
    /// 単位名称検索
    /// </summary>
    public class GetUnitDtoCls
    {
        /// <summary>検索条件  :単位CD</summary>
        public string UNIT_CD { get; set; }
    }

    ///// <summary>
    ///// 業者取得区分検索
    ///// </summary>
    //public class GetKbnGyoushaDtoCls
    //{
    //    /// <summary>検索条件  :業者CD</summary>
    //    public string GYOUSHA_CD { get; set; }
    //}

    ///// <summary>
    ///// 現場取得区分検索
    ///// </summary>
    //public class GetKbnJigyoubaDtoCls
    //{
    //    /// <summary>検索条件  :業者CD</summary>
    //    public string GYOUSHA_CD { get; set; }

    //    /// <summary>検索条件  :現場CD</summary>
    //    public string GENBA_CD { get; set; }
    //}

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

    ///// <summary>
    ///// 業者取得住所検索
    ///// </summary>
    //public class GetAddressGyoushaDtoCls
    //{
    //    /// <summary>検索条件  :業者CD</summary>
    //    public string GYOUSHA_CD { get; set; }
    //}

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

    // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 start‏
    /// <summary>
    /// 機能間連携検索
    /// </summary>
    public class GetKenpaiDataDtoCls
    {
        /// <summary>連携番号</summary>
        public decimal RENKEI_NUMBER { get; set; }
        /// <summary>連携行番号</summary>
        public int? RENKEI_ROW_NO { get; set; }
        /// <summary>マニ区分</summary>
        public int RENKEI_MANI_KBN { get; set; }
        /// <summary>検索条件  :チェックモード区分</summary>
        public int CHK_MODE_KBN { get; set; }
    }
    // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 end‏

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
