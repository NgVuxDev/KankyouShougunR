using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data;
using System.Data.SqlTypes;

namespace Shougun.Core.ElectronicManifest.DenshiCSVTorikomu
{
    /// <summary>
    /// マニフェスト情報クラス
    /// </summary>
    public class DenshiManifestInfoCls
    {
        /// <summary>電子マニフェスト情報</summary>          
        public DT_R18 dt_r18 { get; set; }
        public DT_R18 dt_r18Old { get; set; }

        /// <summary>目次情報</summary> 
        public DT_MF_TOC dt_mf_toc { get; set; }                //目次情報
        /// <summary>加入者番号情報</summary> 
        public DT_MF_MEMBER dt_mf_member { get; set; }          //加入者番号情報
        public DT_MF_MEMBER dt_mf_memberOld { get; set; }          //加入者番号情報

        /// <summary>運搬情報</summary> 
        public List<DT_R19> lstDT_R19 { get; set; }             //運搬情報
        public List<DT_R19> lstDT_R19Old { get; set; }             //運搬情報

        /// <summary>有害物質情報</summary> 
        public List<DT_R02> lstDT_R02 { get; set; }             //有害物質情報
        public List<DT_R02> lstDT_R02Old { get; set; }             //有害物質情報

        /// <summary>最終処分事業場（予定）情報</summary> 
        public List<DT_R04> lstDT_R04 { get; set; }             //最終処分事業場（予定）情報
        public List<DT_R04> lstDT_R04Old { get; set; }             //最終処分事業場（予定）情報

        /// <summary>連絡番号情報</summary> 
        public List<DT_R05> lstDT_R05 { get; set; }             //連絡番号情報
        public List<DT_R05> lstDT_R05Old { get; set; }             //連絡番号情報

        /// <summary>備考情報</summary> 
        public List<DT_R06> lstDT_R06 { get; set; }             //備考情報
        public List<DT_R06> lstDT_R06Old { get; set; }             //備考情報

        /// <summary>最終処分終了日・事業場情報</summary> 
        public List<DT_R13> lstDT_R13 { get; set; }             //最終処分終了日・事業場情報
        public List<DT_R13> lstDT_R13Old { get; set; }             //最終処分終了日・事業場情報

        /// <summary>電子基本拡張[既存データ]</summary> 
        public DT_R18_EX dt_r18Ex { get; set; }              //電子基本拡張[既存データ]
        public DT_R18_EX dt_r18ExOld { get; set; }              //電子基本拡張[既存データ]

        /// <summary>電子運搬拡張</summary> 
        public List<DT_R19_EX> lstDT_R19_EX { get; set; }       //電子運搬拡張
        public List<DT_R19_EX> lstDT_R19_EXOld { get; set; }       //電子運搬拡張

        /// <summary>電子最終処分(予定)拡張</summary> 
        public List<DT_R04_EX> lstDT_R04_EX { get; set; }       //電子最終処分(予定)拡張
        public List<DT_R04_EX> lstDT_R04_EXOld { get; set; }       //電子最終処分(予定)拡張

        /// <summary>電子最終処分拡張</summary> 
        public List<DT_R13_EX> lstDT_R13_EX { get; set; }       //電子最終処分拡張
        public List<DT_R13_EX> lstDT_R13_EXOld { get; set; }       //電子最終処分拡張

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenshiManifestInfoCls()
        {
            dt_r18 = new DT_R18();
            dt_mf_toc = new DT_MF_TOC();
            //dt_mf_member = new DT_MF_MEMBER();
            lstDT_R19 = new List<DT_R19>();
            lstDT_R02 = new List<DT_R02>();
            lstDT_R04 = new List<DT_R04>();
            lstDT_R05 = new List<DT_R05>();
            lstDT_R06 = new List<DT_R06>();
            lstDT_R13 = new List<DT_R13>();
            lstDT_R19_EX = new List<DT_R19_EX>();
            lstDT_R04_EX = new List<DT_R04_EX>();
            lstDT_R13_EX = new List<DT_R13_EX>();

            //dt_r18Old = new DT_R18();
            //dt_mf_memberOld = new DT_MF_MEMBER();
            lstDT_R19Old = new List<DT_R19>();
            lstDT_R02Old = new List<DT_R02>();
            lstDT_R04Old = new List<DT_R04>();
            lstDT_R05Old = new List<DT_R05>();
            lstDT_R06Old = new List<DT_R06>();
            lstDT_R13Old = new List<DT_R13>();
            //dt_r18ExOld = new DT_R18_EX();
            lstDT_R19_EXOld = new List<DT_R19_EX>();
            lstDT_R04_EXOld = new List<DT_R04_EX>();
            lstDT_R13_EXOld = new List<DT_R13_EX>();
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

        /// <summary>検索条件  :単位CD</summary>
        public string UNIT_CD { get; set; }
        /// <summary>検索条件  :荷姿CD</summary>
        public string NISUGATA_CD { get; set; }

        /// <summary>検索条件  :廃棄物名称</summary>
        public string HAIKI_NAME { get; set; }
        /// <summary>検索条件  :処分方法CD</summary>
        public string SHOBUN_HOUHOU_CD { get; set; }

        /// <summary>検索条件  :加入者番号リスト</summary>
        public string[] EDI_MEMBER_IDAry { get; set; }

    }

    /// <summary>
    /// 車輌マスタ
    /// </summary>
    public class M_SHARYOUDTOCls
    {
        /// <summary>
        /// 車輌名
        /// </summary>
        public String SHARYOU_NAME { get; set; }
        /// <summary>
        /// 加入者番号
        /// </summary>
        public String EDI_MEMBER_ID { get; set; }
    }


    public class MasterDataCls
    {
        //電子事業者マスタ
        public DataTable denshiJgyosyaTb { get; set; }
        //電子事業場マスタ
        public DataTable denshiJgyoujoTb { get; set; }
        //電子担当者マスタ
        public DataTable denshiTantousyaTb { get; set; }
        //電子廃棄物名称マスタ
        public DataTable denshiHakkiNameTb { get; set; }
        //電子有害物質マスタ
        public DataTable denshiYugaibushituTb { get; set; }
        //運搬方法マスタ
        public DataTable unpanHouhouTb { get; set; }
        //荷姿マスタ
        public DataTable nisugataTb { get; set; }
        //単位マスタ
        public DataTable unitTb { get; set; }
        //処分方法マスタ
        public DataTable syoubunHouhouTb { get; set; }
        //車両マスタ
        public DataTable sharryouTb { get; set; }
        //加入者情報マスタ
        public DataTable msJwnetMember { get; set; }

        public MasterDataCls()
        {
            denshiJgyosyaTb = new DataTable();
            denshiJgyoujoTb = new DataTable();
            denshiTantousyaTb = new DataTable();
            denshiHakkiNameTb = new DataTable();
            denshiYugaibushituTb = new DataTable();
            unpanHouhouTb = new DataTable();
            nisugataTb = new DataTable();
            unitTb = new DataTable();
            syoubunHouhouTb = new DataTable();
            sharryouTb = new DataTable();
            msJwnetMember = new DataTable();
        }

    }
}
