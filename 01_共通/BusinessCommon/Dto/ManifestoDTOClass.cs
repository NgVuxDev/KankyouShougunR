using System.Data.SqlTypes;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class CommonSerchParameterDtoCls
    {
        /// <summary>検索条件  :システムID</summary>
        public string SYSTEM_ID { get; set; }

        /// <summary>検索条件  :枝番</summary>
        public string SEQ { get; set; }

        /// <summary>検索条件  :廃棄区分CD</summary>
        public string HAIKI_KBN_CD { get; set; }

        /// <summary>検索条件  :交付番号</summary>
        public string MANIFEST_ID { get; set; }

        /// <summary>検索条件  :連携伝種区分CD</summary>
        public string RENKEI_DENSHU_KBN_CD { get; set; }

        /// <summary>検索条件  :連携システムID</summary>
        public string RENKEI_SYSTEM_ID { get; set; }

        /// <summary>検索条件  :連携明細システムID</summary>
        public string RENKEI_MEISAI_SYSTEM_ID { get; set; }
    }

    /// <summary>
    /// 業者マスタ検索
    /// </summary>
    public class CommonGyoushaDtoCls
    {
        /// <summary>検索条件  :業者CD</summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>検索条件  :業者区分マニ</summary>
        public string GYOUSHAKBN_MANI { get; set; }

        /// <summary>検索条件  :排出事業者/荷積業者区分</summary>
        public string HAISHUTSU_NIZUMI_GYOUSHA_KBN { get; set; }

        /// <summary>検索条件  :処分受託者/荷降業者区分</summary>
        public string SHOBUN_NIOROSHI_GYOUSHA_KBN { get; set; }

        /// <summary>検索条件  :運搬受託者/運搬会社区分</summary>
        public string UNPAN_JUTAKUSHA_KAISHA_KBN { get; set; }

        //積替えだけは現場と結合
        /// <summary>検索条件  :積替保管区分</summary>
        public string TSUMIKAEHOKAN_KBN { get; set; }

        //最終処分だけは現場と結合
        /// <summary>検索条件  :最終処分事業場区分</summary>
        public string SAISHUU_SHOBUNJOU_KBN { get; set; }

        public SqlBoolean ISNOT_NEED_DELETE_FLG { get; set; }
    }

    /// <summary>
    /// 現場マスタ検索
    /// </summary>
    public class CommonGenbaDtoCls
    {
        //業者マスタ項目
        /// <summary>検索条件  :業者CD</summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>検索条件  :業者区分マニ</summary>
        public string GYOUSHAKBN_MANI { get; set; }

        /// <summary>検索条件  :排出事業者/荷積業者区分</summary>
        public string HAISHUTSU_NIZUMI_GYOUSHA_KBN { get; set; }

        /// <summary>検索条件  :処分受託者/荷降業者区分</summary>
        public string SHOBUN_NIOROSHI_GYOUSHA_KBN { get; set; }

        //現場マスタ項目
        /// <summary>検索条件  :現場CD</summary>
        public string GENBA_CD { get; set; }

        /// <summary>検索条件  :排出事業場/荷積現場区分</summary>
        public string HAISHUTSU_NIZUMI_GENBA_KBN { get; set; }

        /// <summary>検索条件  :最終処分場区分</summary>
        public string SAISHUU_SHOBUNJOU_KBN { get; set; }

        /// <summary>検索条件  :処分事業場/荷降現場区分</summary>
        public string SHOBUN_NIOROSHI_GENBA_KBN { get; set; }

        /// <summary>検索条件  :積替保管区分</summary>
        public string TSUMIKAEHOKAN_KBN { get; set; }

        public SqlBoolean ISNOT_NEED_DELETE_FLG { get; set; }
    }

    /// <summary>
    /// 混合廃棄物マスタ
    /// </summary>
    public class CommonKongouHaikibutsuDtoCls
    {
        /// <summary>廃棄物区分CD</summary>
        public string HAIKI_KBN_CD { get; set; }

        /// <summary>混合種類CD</summary>
        public string KONGOU_SHURUI_CD { get; set; }
    }

    /// <summary>
    /// 単位マスタ
    /// </summary>
    public class CommonUnitDtoCls
    {
        /// <summary>単位CD</summary>
        public string UNIT_CD { get; set; }

        /// <summary>紙利用区分</summary>
        public string KAMI_USE_KBN { get; set; }
    }

    /// <summary>
    /// 処分担当者
    /// </summary>
    public class CommonShobunTantoushaDtoCls
    {
        /// <summary>社員CD</summary>
        public string SHAIN_CD { get; set; }
    }

    /// <summary>
    /// マニフェスト換算値 検索条件
    /// マニフェスト減容値 検索条件
    /// </summary>
    public class CommonKanSanDtoCls
    {
        /// <summary>廃棄物区分CD</summary>
        public string HAIKI_KBN_CD { get; set; }

        /// <summary>廃棄物種類CD</summary>
        public string HAIKI_SHURUI_CD { get; set; }

        /// <summary>廃棄物名称CD</summary>
        public string HAIKI_NAME_CD { get; set; }

        /// <summary>荷姿CD</summary>
        public string NISUGATA_CD { get; set; }

        /// <summary>数量</summary>
        public string HAIKI_SUU { get; set; }

        /// <summary>単位CD</summary>
        public string UNIT_CD { get; set; }

        /// <summary>換算後数量</summary>
        public string KANSAN_SUU { get; set; }

        /// <summary>処分方法CD</summary>
        public string SHOBUN_HOUHOU_CD { get; set; }
    }

    /// <summary>
    /// マニフェスト紐付対象検索
    /// </summary>
    public class GetManifestRelationDtoCls
    {
        /// <summary>マニフェストのシステムID</summary>
        public string MANIFEST_SYSTEM_ID { get; set; }

        /// <summary>二次マニの廃棄区分CD(1:直行、2:建廃、3:積替、4;電子)</summary>
        public int NEXT_HAIKI_KBN_CD { get; set; }
    }
}