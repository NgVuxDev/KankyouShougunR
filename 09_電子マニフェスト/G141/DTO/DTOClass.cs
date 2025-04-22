using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku
{
    /// <summary>
    /// マニフェスト情報クラス
    /// </summary>
    public class DenshiManifestInfoCls
    {
        /// <summary>データがパタンから呼出フラグ</summary>
        public bool bIsFromPattern { get; set; }
        /// <summary>自動モードフラグ</summary>
        public bool bIsAutoMode { get; set; }
        /// <summary>保留登録フラグ</summary>
        public bool bHouryuFlg { get; set; }
        /// <summary>電子マニフェスト情報</summary>          
        public DT_R18 dt_r18 { get; set; }
        /// <summary>キュー情報</summary>             
        public QUE_INFO que_Info { get; set; }                  //キュー情報
        /// <summary>目次情報</summary> 
        public DT_MF_TOC dt_mf_toc { get; set; }                //目次情報
        /// <summary>加入者番号情報</summary> 
        public DT_MF_MEMBER dt_mf_member { get; set; }          //加入者番号情報
        /// <summary>運搬情報</summary> 
        public List<DT_R19> lstDT_R19 { get; set; }             //運搬情報
        /// <summary>有害物質情報</summary> 
        public List<DT_R02> lstDT_R02 { get; set; }             //有害物質情報
        /// <summary>最終処分事業場（予定）情報</summary> 
        public List<DT_R04> lstDT_R04 { get; set; }             //最終処分事業場（予定）情報
        /// <summary>連絡番号情報</summary> 
        public List<DT_R05> lstDT_R05 { get; set; }             //連絡番号情報
        /// <summary>備考情報</summary> 
        public List<DT_R06> lstDT_R06 { get; set; }             //備考情報
        /// <summary>最終処分終了日・事業場情報</summary> 
        public List<DT_R13> lstDT_R13 { get; set; }             //最終処分終了日・事業場情報

        /// <summary>
        /// 電子マニフェスト基本拡張エンティティ
        /// </summary>
        public DT_R18_EX dt_r18Ex { get; set; }

        /// <summary>電子基本拡張[既存データ]</summary> 
        public DT_R18_EX dt_r18ExOld { get; set; }              //電子基本拡張[既存データ]
        /// <summary>電子運搬拡張</summary> 
        public List<DT_R19_EX> lstDT_R19_EX { get; set; }       //電子運搬拡張
        /// <summary>電子最終処分(予定)拡張</summary> 
        public List<DT_R04_EX> lstDT_R04_EX { get; set; }       //電子最終処分(予定)拡張
        /// <summary>電子最終処分拡張</summary> 
        public List<DT_R13_EX> lstDT_R13_EX { get; set; }       //電子最終処分拡張
        
        /// <summary>1次マニフェスト情報</summary> 
        public List<DT_R08> lstDT_R08 { get; set; }
        /// <summary>電子マニフェスト一次マニフェスト情報拡張</summary> 
        public List<DT_R08_EX> lstDT_R08_EX { get; set; }
        /// <summary>マニフェスト紐付</summary> 
        public List<T_MANIFEST_RELATION> lstT_MANIFEST_RELATION { get; set; }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenshiManifestInfoCls()
        {
            bIsFromPattern = false;//パタン呼出なくて
            bIsAutoMode = true;//自動
            bHouryuFlg = false;//JWNET登録
            dt_r18 = new DT_R18();
            que_Info = new QUE_INFO();
            dt_mf_toc = new DT_MF_TOC();
            dt_mf_member = new DT_MF_MEMBER();
            lstDT_R19 = new List<DT_R19>();
            lstDT_R02 = new List<DT_R02>();
            lstDT_R04 = new List<DT_R04>();
            lstDT_R05 = new List<DT_R05>();
            lstDT_R06 = new List<DT_R06>();
            lstDT_R13 = new List<DT_R13>();
            dt_r18Ex = new DT_R18_EX();
            dt_r18ExOld = new DT_R18_EX();
            lstDT_R19_EX = new List<DT_R19_EX>();
            lstDT_R04_EX = new List<DT_R04_EX>();
            lstDT_R13_EX = new List<DT_R13_EX>();
            lstDT_R08 = new List<DT_R08>();
            lstDT_R08_EX = new List<DT_R08_EX>();
            lstT_MANIFEST_RELATION = new List<T_MANIFEST_RELATION>();
        }
    }

    /// <summary>
    /// マニフェストパタン情報クラス
    /// </summary>
    public class DenshiManifestPatternInfoCls
    {
        /// <summary>電子マニフェストパターン有害物質</summary> 
        public List<DT_PT_R02> lstDT_PT_R02 { get; set; }             //電子マニフェストパターン有害物質
        /// <summary>電子マニフェストパターン最終処分(予定)</summary> 
        public List<DT_PT_R04> lstDT_PT_R04 { get; set; }             //電子マニフェストパターン最終処分(予定)
        /// <summary>電子マニフェストパターン連絡番号</summary> 
        public List<DT_PT_R05> lstDT_PT_R05 { get; set; }             //電子マニフェストパターン連絡番号
        /// <summary>電子マニフェストパターン備考</summary> 
        public List<DT_PT_R06> lstDT_PT_R06 { get; set; }             //電子マニフェストパターン備考
        /// <summary>電子マニフェストパターン最終処分</summary> 
        public List<DT_PT_R13> lstDT_PT_R13 { get; set; }             //電子マニフェストパターン最終処分
        /// <summary>電子マニフェストパターン</summary> 
        public DT_PT_R18 dt_PT_R18 { get; set; }                      //電子マニフェストパターン
        /// <summary>電子マニフェストパターン収集運搬</summary> 
        public List<DT_PT_R19> lstDT_PT_R19 { get; set; }             //電子マニフェストパターン収集運搬
        /// <summary>電子マニフェストパターン</summary> 
        public List<DT_PT_R18_EX> lstDT_PT_R18_EX { get; set; }       //電子マニフェストパターン拡張
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenshiManifestPatternInfoCls()
        {
            dt_PT_R18 = new DT_PT_R18();
            lstDT_PT_R02 = new List<DT_PT_R02>();
            lstDT_PT_R04 = new List<DT_PT_R04>();
            lstDT_PT_R05 = new List<DT_PT_R05>();
            lstDT_PT_R06 = new List<DT_PT_R06>();
            lstDT_PT_R13 = new List<DT_PT_R13>();
            lstDT_PT_R19 = new List<DT_PT_R19>();
            lstDT_PT_R18_EX = new List<DT_PT_R18_EX>();
        }
    }
    /// <summary>
    /// 存在するチェック検索条件DTO
    /// </summary>
    public class SearchMasterDataDTOCls
    {
        /// <summary>検索条件  :システムID</summary>
        public string SYSTEM_ID { get; set; }
        /// <summary>検索条件  :システム枝番号</summary>
        public string SYSTEM_SEQ { get; set; }
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
        /// <summary>検索条件  :電子事業場CD</summary>
        public string JIGYOUJOU_CD { get; set; }
        /// <summary>検索条件  :将軍業者CD</summary>
        public string GYOUSHA_CD { get; set; }
        /// <summary>検索条件  :将軍現場CD</summary>
        public string GENBA_CD { get; set; }
        /// <summary> 検索条件：電子排出業者区分 </summary>
        public bool HST_KBN { get; set; }
        /// <summary> 検索条件：電子運搬業者区分 </summary>
        public bool UPN_KBN { get; set; }
        /// <summary> 検索条件：電子処分業者区分 </summary>
        public bool SBN_KBN { get; set; }
        /// <summary> 検索条件：電子報告不要区分 </summary>
        public bool HOUKOKU_HUYOU_KBN { get; set; }
        /// <summary> 検索条件：電子事業者区分 </summary>
        public string JIGYOUSHA_KBN { get; set; }//1:排出；2:運搬;3:処分
        /// <summary> 検索条件：電子事業場区分 </summary>
        public string JIGYOUJOU_KBN { get; set; }//1:排出；2:運搬;3:処分
        /// <summary>検索条件  :担当者区分</summary>
        public string TANTOUSHA_KBN { get; set; }
        /// <summary>検索条件  :担当者CD</summary>
        public string TANTOUSHA_CD { get; set; }
        /// <summary>検索条件  :有害物質CD</summary>
        public string YUUGAI_BUSSHITSU_CD { get; set; }
        ///[換算式換算値取得ため]
        /// <summary>検索条件  :単位CD</summary>
        public string UNIT_CD { get; set; }
        /// <summary>検索条件  :荷姿CD</summary>
        public string NISUGATA_CD { get; set; }
        /// <summary>検索条件  :マニフェストCD</summary>
        public string MANIFEST_ID { get; set; }
        /// <summary>検索条件  :処分方法CD</summary>
        public string SHOBUN_HOUHOU_CD { get; set; }
    }

    /// <summary>
    /// 運搬区間の報告情報(手動モード場合のみ)
    /// </summary>
    public class UnpanHoukokuDataDTOCls
    {
        /// <summary>
        /// 運搬終了日(yyyyMMdd)
        /// </summary>
        public String cdtp_UnpanEndDate { get; set; }
        /// <summary>
        ///  運搬担当者CD
        /// </summary>
        public String cantxt_UnpanTantoushaCd { get; set; }
        /// <summary>
        ///  運搬担当者名称
        /// </summary>
        public String ctxt_UnpanTantoushaName { get; set; }
        /// <summary>
        ///  運搬量
        /// </summary>
        public SqlDecimal cntxt_UnpanRyo { get; set; }
        /// <summary>
        /// 運搬量単位CD
        /// </summary>
        public String cantxt_UnpanRyoUnitCd { get; set; }
        /// <summary>
        /// 運搬量単位名称
        /// </summary>
        public String ctxt_UnitName { get; set; }
        /// <summary>
        /// 車輌No
        /// </summary>
        public String cantxt_SyaryoNo { get; set; }
        /// <summary>
        /// 車輌名称
        /// </summary>
        public String ctxt_UnpanSyaryoName { get; set; }
        /// <summary>
        /// 報告担当者CD
        /// </summary>
        public String cantxt_HoukokuTantoushaCD { get; set; }
        /// <summary>
        /// 報告担当者名称
        /// </summary>
        public String ctxt_HoukokuTantoushaName { get; set; }
        /// <summary>
        /// 有価物収拾量
        /// </summary>
        public SqlDecimal cntxt_YukabutuRyo { get; set; }
        /// <summary>
        /// 有価物収拾量単位
        /// </summary>
        public String cantxt_YukabutuRyoUnitCd { get; set; }
        /// <summary>
        /// 有価物収拾量名称
        /// </summary>
        public String cantxt_YukabutuRyoUnitName { get; set; }
        /// <summary>
        /// 運搬報告の備考
        /// </summary>
        public String ctxt_UnpanBikou { get; set; }
        /// <summary>
        /// DTOクラスで空白判断
        /// </summary>
        /// <returns></returns>
        public bool IsNotEmpty()
        {
            bool bRet = true;
            if (!string.IsNullOrEmpty(cdtp_UnpanEndDate)) { return bRet; }
            if (!string.IsNullOrEmpty(cantxt_UnpanTantoushaCd)) { return bRet; }
            if (!string.IsNullOrEmpty(ctxt_UnpanTantoushaName)) { return bRet; }
            if (cntxt_UnpanRyo!=0) { return bRet; }
            if (!string.IsNullOrEmpty(cantxt_UnpanRyoUnitCd)) { return bRet; }
            if (!string.IsNullOrEmpty(ctxt_UnitName)) { return bRet; }
            if (!string.IsNullOrEmpty(cantxt_SyaryoNo)) { return bRet; }
            if (!string.IsNullOrEmpty(ctxt_UnpanSyaryoName)) { return bRet; }
            if (!string.IsNullOrEmpty(cantxt_HoukokuTantoushaCD)) { return bRet; }
            if (!string.IsNullOrEmpty(ctxt_HoukokuTantoushaName)) { return bRet; }
            if (cntxt_YukabutuRyo!=0) { return bRet; }
            if (!string.IsNullOrEmpty(cantxt_YukabutuRyoUnitCd)) { return bRet; }
            if (!string.IsNullOrEmpty(cantxt_YukabutuRyoUnitName)) { return bRet; }
            if (!string.IsNullOrEmpty(ctxt_UnpanBikou)) { return bRet; }

            return false;
        }

    }
}
