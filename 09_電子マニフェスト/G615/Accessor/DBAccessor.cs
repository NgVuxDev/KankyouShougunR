// $Id: DBAccessor.cs 42593 2015-02-18 02:29:13Z takeda $
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuJoukyouIchiran
{
    /// <summary>
    /// 混合廃棄物状況一覧Accessor
    /// </summary>
    public class DBAccessor
    {
        #region - Field -
        /// <summary>混合廃棄物状況一覧DAO</summary>
        DAOClass dao;
        /// <summary>業者DAO</summary>
        IM_GYOUSHADao gyoushaDao;
        /// <summary>現場DAO</summary>
        IM_GENBADao genbaDao;
        /// <summary>報告書分類DAO</summary>
        IM_HOUKOKUSHO_BUNRUIDao bunruiDao;
        /// <summary>電子廃棄物種類DAO</summary>
        IM_DENSHI_HAIKI_SHURUIDao denshiHaikiShuruiDao;
        /// <summary>電子事業者DAO</summary>
        IM_DENSHI_JIGYOUSHADao denshiGyoushaDao;
        /// <summary>電子事業場DAO</summary>
        IM_DENSHI_JIGYOUJOUDao denshiGenbaDao;
        /// <summary>マニフェスト紐付DAO</summary>
        GetManifestRelationDaoCls maniRelDao;

        #endregion - Field -

        #region - Constructor -
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // フィールドの初期化
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.bunruiDao = DaoInitUtility.GetComponent<IM_HOUKOKUSHO_BUNRUIDao>();
            this.denshiHaikiShuruiDao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUIDao>();
            this.denshiGyoushaDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
            this.denshiGenbaDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
            this.maniRelDao = DaoInitUtility.GetComponent<GetManifestRelationDaoCls>();
        }

        #endregion - Constructor -

        #region - Utility -
        /// <summary>
        /// 排出事業者情報取得
        /// </summary>
        /// <param name="gyoushaCd">キーとなる業者CD</param>
        /// <returns name="M_GYOUSHA">業者Entity 該当無しの場合はnullを返却</returns>
        internal M_GYOUSHA getHstGyoushaEntity(string gyoushaCd)
        {
            M_GYOUSHA retEntity = null;

            if(false == string.IsNullOrEmpty(gyoushaCd))
            {
                // 排出事業者情報取得
                var findEntity = new M_GYOUSHA();
                findEntity.GYOUSHA_CD = gyoushaCd;
                // 20151022 BUNN #12040 STR
                findEntity.GYOUSHAKBN_MANI = SqlBoolean.True;
                findEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN = SqlBoolean.True;
                // 20151022 BUNN #12040 END
                findEntity.ISNOT_NEED_DELETE_FLG = true;
                retEntity = this.getGyoushaEntity(findEntity);
            }

            return retEntity;
        }

        /// <summary>
        /// 排出事業場情報取得
        /// </summary>
        /// <param name="gyoushaCd">キーとなる業者CD</param>
        /// <param name="genbaCd">キーとなる現場CD</param>
        /// <returns name="M_GENBA">現場Entity 該当無しの場合はnullを返却</returns>
        internal M_GENBA getHstGenbaEntity(string gyoushaCd, string genbaCd)
        {
            M_GENBA retEntity = null;

            if((false == string.IsNullOrEmpty(gyoushaCd)) && (false == string.IsNullOrEmpty(genbaCd)))
            {
                // 排出事業場情報取得
                var findEntity = new M_GENBA();
                findEntity.GYOUSHA_CD = gyoushaCd;
                findEntity.GENBA_CD = genbaCd;
                // 20151022 BUNN #12040 STR
                findEntity.HAISHUTSU_NIZUMI_GENBA_KBN = SqlBoolean.True;
                // 20151022 BUNN #12040 END
                findEntity.ISNOT_NEED_DELETE_FLG = true;
                retEntity = this.getGenbaEntity(findEntity);
            }

            return retEntity;
        }

        /// <summary>
        /// 処分受託者情報取得
        /// </summary>
        /// <param name="gyoushaCd">キーとなる業者CD</param>
        /// <returns name="M_GYOUSHA">業者Entity 該当無しの場合はnullを返却</returns>
        internal M_GYOUSHA getSbnGyoushaEntity(string gyoushaCd)
        {
            M_GYOUSHA retEntity = null;

            if(false == string.IsNullOrEmpty(gyoushaCd))
            {
                // 処分受託者情報取得
                var findEntity = new M_GYOUSHA();
                findEntity.GYOUSHA_CD = gyoushaCd;
                // 20151022 BUNN #12040 STR
                findEntity.GYOUSHAKBN_MANI = SqlBoolean.True;
                findEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN = SqlBoolean.True;
                // 20151022 BUNN #12040 END
                findEntity.ISNOT_NEED_DELETE_FLG = true;
                retEntity = this.getGyoushaEntity(findEntity);
            }

            return retEntity;
        }

        /// <summary>
        /// 運搬先の事業場情報取得
        /// </summary>
        /// <param name="gyoushaCd">キーとなる業者CD</param>
        /// <param name="genbaCd">キーとなる現場CD</param>
        /// <returns name="M_GENBA">現場Entity 該当無しの場合はnullを返却</returns>
        internal M_GENBA getUpnSakiGenbaEntity(string gyoushaCd, string genbaCd)
        {
            M_GENBA retEntity = null;

            if((false == string.IsNullOrEmpty(gyoushaCd)) && (false == string.IsNullOrEmpty(genbaCd)))
            {
                // 運搬先の事業場情報取得(積替保管区分=Trueのもの)
                var findEntity = new M_GENBA();
                findEntity.GYOUSHA_CD = gyoushaCd;
                findEntity.GENBA_CD = genbaCd;
                findEntity.TSUMIKAEHOKAN_KBN = SqlBoolean.True;
                findEntity.ISNOT_NEED_DELETE_FLG = true;
                retEntity = this.getGenbaEntity(findEntity);
                if(retEntity == null)
                {
                    // 運搬先の事業場情報取得(処分事業場区分=Trueのもの)
                    findEntity = new M_GENBA();
                    findEntity.GYOUSHA_CD = gyoushaCd;
                    findEntity.GENBA_CD = genbaCd;
                    findEntity.SHOBUN_NIOROSHI_GENBA_KBN = SqlBoolean.True;
                    findEntity.ISNOT_NEED_DELETE_FLG = true;
                    retEntity = this.getGenbaEntity(findEntity);
                }
            }

            return retEntity;
        }

        /// <summary>
        /// 運搬受託者情報取得
        /// </summary>
        /// <param name="gyoushaCd">キーとなる業者CD</param>
        /// <returns name="M_GYOUSHA">業者Entity 該当無しの場合はnullを返却</returns>
        internal M_GYOUSHA getUpnJyutakushaEntity(string gyoushaCd)
        {
            M_GYOUSHA retEntity = null;

            if(false == string.IsNullOrEmpty(gyoushaCd))
            {
                // 運搬受託者情報取得(運搬受託者区分=Trueのもの)
                var findEntity = new M_GYOUSHA();
                findEntity.GYOUSHA_CD = gyoushaCd;
                // 20151022 BUNN #12040 STR
                findEntity.GYOUSHAKBN_MANI = SqlBoolean.True;
                findEntity.UNPAN_JUTAKUSHA_KAISHA_KBN = SqlBoolean.True;
                // 20151022 BUNN #12040 END
                findEntity.ISNOT_NEED_DELETE_FLG = true;
                retEntity = this.getGyoushaEntity(findEntity);
            }

            return retEntity;
        }

        /// <summary>
        /// 報告書分類情報取得
        /// </summary>
        /// <param name="bunruiCd">キーとなる分類CD</param>
        /// <returns name="M_GYOUSHA">報告書分類Entity 該当無しの場合はnullを返却</returns>
        internal M_HOUKOKUSHO_BUNRUI getHoukokushoBunruiEntity(string bunruiCd)
        {
            M_HOUKOKUSHO_BUNRUI retEntity = null;

            if(false == string.IsNullOrEmpty(bunruiCd))
            {
                // 報告書分類情報取得
                var findEntity = new M_HOUKOKUSHO_BUNRUI();
                findEntity.HOUKOKUSHO_BUNRUI_CD = bunruiCd;
                findEntity.ISNOT_NEED_DELETE_FLG = true;
                var entitys = this.bunruiDao.GetAllValidData(findEntity);
                if((entitys != null) && (entitys.Length > 0))
                {
                    // 該当情報を返却
                    // ※キーにて検索するため該当情報は唯一
                    retEntity = entitys[0];
                }
            }

            return retEntity;
        }

        /// <summary>
        /// 電子廃棄物種類情報取得
        /// </summary>
        /// <param name="shuruiCd">キーとなる種類CD</param>
        /// <returns name="M_DENSHI_HAIKI_SHURUI">電子廃棄物種類Entity 該当無しの場合はnullを返却</returns>
        internal M_DENSHI_HAIKI_SHURUI getDenshiHaikiShuruiEntity(string shuruiCd)
        {
            M_DENSHI_HAIKI_SHURUI retEntity = null;

            if(false == string.IsNullOrEmpty(shuruiCd))
            {
                // 電子廃棄物種類情報取得
                var findEntity = new M_DENSHI_HAIKI_SHURUI();
                findEntity.HAIKI_SHURUI_CD = shuruiCd;
                findEntity.ISNOT_NEED_DELETE_FLG = true;
                var entitys = this.denshiHaikiShuruiDao.GetAllValidData(findEntity);
                if((entitys != null) && (entitys.Length > 0))
                {
                    // 該当情報を返却
                    // ※キーにて検索するため該当情報は唯一
                    retEntity = entitys[0];
                }
            }

            return retEntity;
        }

        /// <summary>
        /// 一覧表示用Data取得
        /// </summary>
        /// <param name="condition">抽出条件</param>
        /// <returns name="DataTable">一覧表示用DataTable 該当無しの場合はnullを返却</returns>
        internal DataTable getIchiranData(findConditionDTO condition)
        {
            // 一覧表示用Data取得
            var table = this.dao.getIchiranData(condition);

            // 該当無しの場合はnullを返却
            if(table.Rows.Count <= 0)
            {
                table = null;
            }

            return table;
        }

        /// <summary>
        /// 電マニデータ存在チェック
        /// </summary>
        /// <param name="kanriID">管理番号</param>
        /// <returns name="bool">TRUE:存在する, FALSE:存在しない</returns>
        internal bool denManiDataExistCheck(string kanriID)
        {
            bool bRet = false;

            // マニフェスト目次情報(DT_MF_TOC)より情報取得
            var sql = "SELECT * FROM DT_MF_TOC WHERE DT_MF_TOC.KANRI_ID = '" + kanriID + "'";
            var table = this.dao.GetDateForStringSql(sql);
            if(table.Rows.Count > 0)
            {
                // 該当情報あり
                bRet = true;
            }

            return bRet;
        }

        /// <summary>
        /// 対象の一次マニがすでに紐付済かどうか
        /// </summary>
        /// <param name="firstSysId">一次マニのSYSTEM_ID</param>
        /// <param name="fristHaikiKbnCd">一次マニの廃棄区分CD</param>
        /// <returns>true:紐付済, false:未紐付</returns>
        internal bool isExistRelatiionData(string firstSysId, string firstHaikiKbnCd)
        {
            bool returnVal = false;

            if (string.IsNullOrEmpty(firstSysId) || string.IsNullOrEmpty(firstHaikiKbnCd))
            {
                return returnVal;
            }

            long targetFirstSysId = -1;
            short targetFirstHaikiKbnCd = -1;

            if (long.TryParse(firstSysId, out targetFirstSysId)
                && short.TryParse(firstHaikiKbnCd, out targetFirstHaikiKbnCd))
            {
                var targetManiRel = new T_MANIFEST_RELATION();
                targetManiRel.FIRST_SYSTEM_ID = targetFirstSysId;
                targetManiRel.FIRST_HAIKI_KBN_CD = targetFirstHaikiKbnCd;

                var result = maniRelDao.GetAllValidData(targetManiRel);
                returnVal = (result != null && result.Length > 0) ? true : false;
            }

            return returnVal;
        }
        #endregion - Utility -

        #region - PrivateUtility -
        /// <summary>
        /// 業者Entityの取得
        /// </summary>
        /// <param name="findEntity">検索条件</param>
        /// <returns name="M_GYOUSHA">業者Entity 該当無しの場合はnullを返却</returns>
        private M_GYOUSHA getGyoushaEntity(M_GYOUSHA findEntity)
        {
            M_GYOUSHA retEntity = null;

            // 業者情報取得
            var entitys = this.gyoushaDao.GetAllValidData(findEntity);
            if((entitys != null) && (entitys.Length > 0))
            {
                // 該当情報を返却
                // ※キーにて検索するため該当情報は唯一
                retEntity = entitys[0];
            }

            return retEntity;
        }

        /// <summary>
        /// 現場Entityの取得
        /// </summary>
        /// <param name="findEntity">検索条件</param>
        /// <returns name="M_GENBA">現場Entity 該当無しの場合はnullを返却</returns>
        private M_GENBA getGenbaEntity(M_GENBA findEntity)
        {
            M_GENBA retEntity = null;

            // 現場情報取得
            var entitys = this.genbaDao.GetAllValidData(findEntity);
            if((entitys != null) && (entitys.Length > 0))
            {
                // 該当情報を返却
                // ※キーにて検索するため該当情報は唯一
                retEntity = entitys[0];
            }

            return retEntity;
        }

        #endregion - PrivateUtility -
    }
}
