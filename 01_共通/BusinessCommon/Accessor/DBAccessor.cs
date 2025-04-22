using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Common.BusinessCommon
{
    /// <summary>
    /// DBAccessするためのクラス
    /// 
    /// FW側と業務側とでDaoが点在するため、
    /// 本クラスで呼び出すDaoをコントロールする
    /// </summary>
    public class DBAccessor
    {
        #region フィールド

        /// <summary>
        /// IM_SYS_INFODao
        /// </summary>
        r_framework.Dao.IM_SYS_INFODao sysInfoDao;

        /*
        /// <summary>
        /// IS_NUMBER_YEARDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao numberYearDao;

        /// <summary>
        /// IS_NUMBER_DAYDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao numberDayDao;
        */

        /// <summary>
        /// IS_NUMBER_SYSTEMDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// IS_NUMBER_DENSHUDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// IM_KOBETSU_HINMEI_TANKADao
        /// </summary>
        r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao kobetsuHinmeiTankaDao;

        /// <summary>
        /// IM_KIHON_HINMEI_TANKADao
        /// </summary>
        r_framework.Dao.IM_KIHON_HINMEI_TANKADao kihonHinmeiTankaDao;

        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // スタートアッププロジェクトのDiconに情報が設定されていることを必ず確認
            this.sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            //this.numberYearDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao>();
            //this.numberDayDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            this.kobetsuHinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao>();
            this.kihonHinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KIHON_HINMEI_TANKADao>();
        }
        #endregion

        #region DBアクセッサ

        /// <summary>
        /// SYS_INFOを取得する
        /// </summary>
        /// <returns></returns>
        public M_SYS_INFO GetSysInfo()
        {
            // TODO: ログイン時に共通メンバでSYS_INFOの情報を保持する可能性があるため、
            //       その場合、このメソッドは必要なくなる。
            M_SYS_INFO[] returnEntity = sysInfoDao.GetAllData();
            return returnEntity[0];
        }

        /*
        /// <summary>
        /// 年連番のデータを取得
        /// </summary>
        /// <param name="numberedYear">年度</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        public S_NUMBER_YEAR[] GetNumberYear(SqlInt32 numberedYear, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        {
            S_NUMBER_YEAR entity = new S_NUMBER_YEAR();
            entity.NUMBERED_YEAR = numberedYear;
            entity.DENSHU_KBN_CD = denshuKbnCd;
            entity.KYOTEN_CD = kyotenCd;
            return this.numberYearDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 年連番を登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertNumberYear(S_NUMBER_YEAR entity)
        {
            return this.numberYearDao.Insert(entity);
        }

        /// <summary>
        /// 年連番を更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateNumberYear(S_NUMBER_YEAR entity)
        {
            return this.numberYearDao.Update(entity);
        }

        /// <summary>
        /// 日連番のデータを取得
        /// </summary>
        /// <param name="numberedDay">日付</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        public S_NUMBER_DAY[] GetNumberDay(DateTime numberedDay, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        {
            S_NUMBER_DAY entity = new S_NUMBER_DAY();
            entity.NUMBERED_DAY = numberedDay;
            entity.DENSHU_KBN_CD = denshuKbnCd;
            entity.KYOTEN_CD = kyotenCd;
            return this.numberDayDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 日連番を登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertNumberDay(S_NUMBER_DAY entity)
        {
            return this.numberDayDao.Insert(entity);
        }

        /// <summary>
        /// 日連番を更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateNumberDay(S_NUMBER_DAY entity)
        {
            return this.numberDayDao.Update(entity);
        }
        */

        /// <summary>
        /// SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// S_NUMBER_SYSTEMから指定された伝種区分CDの最新のID + 1の値を返す
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemId(SqlInt16 denshuKbnCd)
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = denshuKbnCd;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = denshuKbnCd;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// [TableLock付 Transaction有] SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// S_NUMBER_SYSTEMから指定された伝種区分CDの最新のID + 1の値を返す
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemIdWithTableLock(SqlInt16 denshuKbnCd)
        {
            SqlInt64 returnInt = 1;

            try
            {
                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = denshuKbnCd;

                using (Transaction tran = new Transaction())
                {
                    var updateEntity = this.numberSystemDao.GetNumberSystemDataWithTableLock(entity);
                    returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

                    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                    {
                        updateEntity = new S_NUMBER_SYSTEM();
                        updateEntity.DENSHU_KBN_CD = denshuKbnCd;
                        updateEntity.CURRENT_NUMBER = returnInt;
                        updateEntity.DELETE_FLG = false;
                        var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                        dataBinderEntry.SetSystemProperty(updateEntity, false);

                        this.numberSystemDao.Insert(updateEntity);
                    }
                    else
                    {
                        updateEntity.CURRENT_NUMBER = returnInt;
                        var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                        dataBinderEntry.SetSystemProperty(updateEntity, false);

                        this.numberSystemDao.Update(updateEntity);
                    }

                    tran.Commit();
                }

                return returnInt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// [TableLock付 Transaction無] SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// S_NUMBER_SYSTEMから指定された伝種区分CDの最新のID + 1の値を返す
        /// 
        /// 【注意】テーブルロックをするが本メソッドではTransactionがないので使用元画面でTransactionが有る場合のみ使用すること。
        /// Transactionが無いと実質テーブルロックがされないので注意。
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemIdWithTableLockNoTransaction(SqlInt16 denshuKbnCd)
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = denshuKbnCd;

            var updateEntity = this.numberSystemDao.GetNumberSystemDataWithTableLock(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = denshuKbnCd;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 伝種区分別番号採番処理
        /// S_NUMBER_DENSHUから指定された伝種区分CDの最新の番号 + 1の値を返す
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns>採番した数値</returns>
        public SqlInt64 createDenshuNumber(SqlInt16 denshuKbnCd)
        {
            SqlInt64 returnInt = -1;

            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = denshuKbnCd;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = denshuKbnCd;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberDenshuDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberDenshuDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// [TableLock付] 伝種区分別番号採番処理
        /// S_NUMBER_DENSHUから指定された伝種区分CDの最新の番号 + 1の値を返す
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns>採番した数値</returns>
        public SqlInt64 createDenshuNumberWithTableLock(SqlInt16 denshuKbnCd)
        {
            SqlInt64 returnInt = -1;

            using (Transaction tran = new Transaction())
            {
                var entity = new S_NUMBER_DENSHU();
                entity.DENSHU_KBN_CD = denshuKbnCd;

                var updateEntity = this.numberDenshuDao.GetNumberDenshuDataWithTableLock(entity);
                returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_DENSHU();
                    updateEntity.DENSHU_KBN_CD = denshuKbnCd;
                    updateEntity.CURRENT_NUMBER = returnInt;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberDenshuDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnInt;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberDenshuDao.Update(updateEntity);
                }

                tran.Commit();
            }

            return returnInt;
        }

        #region 個別品名単価検索
        /// <summary>
        /// 個別品名単価を取得
        /// 仕様は(G051)受入入力の[共通動作・単価決定]シートを参照
        /// ※2013/12/19 /R-Project/参考資料/単価仕変/単価の引き方_Ver1.1.xls の仕様を反映
        /// </summary>
        /// <param name="denpyouKBNCd">伝票区分</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="unpanGyoushaCd">運搬業者CD</param>
        /// <param name="nioroshiGyoushaCd">荷降業者CD</param>
        /// <param name="nioroshiGenbaCd">荷降現場CD</param>
        /// <param name="hinmeiCd">品名CD</param>
        /// <param name="unitCd">単位CD</param>
        /// <returns></returns>
        public M_KOBETSU_HINMEI_TANKA GetKobetsuhinmeiTanka(short denpyouKBNCd, string torihikisakiCd,
            string gyoushaCd, string genbaCd, string unpanGyoushaCd, string nioroshiGyoushaCd, string nioroshiGenbaCd,
            string hinmeiCd, short unitCd)
        {
            LogUtility.DebugMethodStart(denpyouKBNCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nioroshiGyoushaCd, nioroshiGenbaCd, hinmeiCd, unitCd);

            var param = new KOBETSU_HINMEI_TANKA_PARAM(hinmeiCd, unitCd, nioroshiGenbaCd, nioroshiGyoushaCd, unpanGyoushaCd, genbaCd, gyoushaCd, torihikisakiCd, denpyouKBNCd, 0);

            // ----20150714 速度改善ため、条件に合ってる品名単価情報を一回で取得するように修正 Start
            M_KOBETSU_HINMEI_TANKA ret = null;
            List<M_KOBETSU_HINMEI_TANKA> ents = null;
            foreach (var rule in KOBETSU_HINMEI_TANKA_RULE.RULES())
            {
                if (this.GetKobetsuhinmeiTanka(param, rule, ref ret, ref ents))
                // 20150714 速度改善ため、条件に合ってる品名単価情報を一回で取得するように修正 End
                {
                    //データあり
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
            }

            //データ無
            LogUtility.DebugMethodEnd(null);
            return null;
        }

        /// <summary>
        /// 個別品名単価を取得
        /// </summary>
        /// <param name="denshuKBNCd">伝種区分</param>
        /// <param name="denpyouKBNCd">伝票区分</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="unpanGyoushaCd">運搬業者CD</param>
        /// <param name="nioroshiGyoushaCd">荷降業者CD</param>
        /// <param name="nioroshiGenbaCd">荷降現場CD</param>
        /// <param name="hinmeiCd">品名CD</param>
        /// <param name="unitCd">単位CD</param>
        /// <param name="referenceDate">基準日(この引数が無い場合は現在日付を参照）</param>
        /// <returns></returns>
        public M_KOBETSU_HINMEI_TANKA GetKobetsuhinmeiTanka(short denshuKBNCd, short denpyouKBNCd, string torihikisakiCd,
            string gyoushaCd, string genbaCd, string unpanGyoushaCd, string nioroshiGyoushaCd, string nioroshiGenbaCd,
            string hinmeiCd, short unitCd, string referenceDate = null)
        {
            LogUtility.DebugMethodStart(denshuKBNCd, denpyouKBNCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nioroshiGyoushaCd, nioroshiGenbaCd, hinmeiCd, unitCd, referenceDate);

            var param = new KOBETSU_HINMEI_TANKA_PARAM(hinmeiCd, unitCd, nioroshiGenbaCd, nioroshiGyoushaCd, unpanGyoushaCd, genbaCd, gyoushaCd, torihikisakiCd, denpyouKBNCd, denshuKBNCd);

            // ----20150714 速度改善ため、条件に合ってる品名単価情報を一回で取得するように修正 Start
            M_KOBETSU_HINMEI_TANKA ret = null;
            List<M_KOBETSU_HINMEI_TANKA> ents = null;
            foreach (var rule in KOBETSU_HINMEI_TANKA_RULE.RULES())
            {
                if (GetKobetsuhinmeiTanka(param, rule, ref ret, ref ents, referenceDate))
                // 20150714 速度改善ため、条件に合ってる品名単価情報を一回で取得するように修正 End
                {
                    //データあり
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
            }

            //データ無
            LogUtility.DebugMethodEnd(null);
            return null;
        }

        //仕様変更 資料は以下
        // //R-Project/参考資料/単価仕変/単価の引き方_Ver1.1.xls
        /// <summary>
        /// 単価決定ルール構造体
        /// </summary>
        private struct KOBETSU_HINMEI_TANKA_RULE
        {
            public bool useHINMEI_CD;
            public bool useUNIT_CD;
            public bool useNIOROSHI_GENBA_CD;
            public bool useNIOROSHI_GYOUSHA_CD;
            public bool useUNPAN_GYOUSHA_CD;
            public bool useGENBA_CD;
            public bool useGYOUSHA_CD;
            public bool useTORIHIKISAKI_CD;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="useHINMEI_CD">品名CDを検索条件に利用するか</param>
            /// <param name="useUNIT_CD">単位を検索条件に利用するか</param>
            /// <param name="useNIOROSHI_GENBA_CD">荷卸現場CDを検索条件に利用するか</param>
            /// <param name="useNIOROSHI_GYOUSHA_CD">荷卸業者CDを検索条件に利用するか</param>
            /// <param name="useUNPAN_GYOUSHA_CD">運搬業者CDを検索条件に利用するか</param>
            /// <param name="useGENBA_CD">現場CDを検索条件に利用するか</param>
            /// <param name="useGYOUSHA_CD">業者CDを検索条件に利用するか</param>
            /// <param name="useTORIHIKISAKI_CD">取引先CDを検索条件に利用するか</param>
            public KOBETSU_HINMEI_TANKA_RULE(bool useHINMEI_CD, bool useUNIT_CD, bool useNIOROSHI_GENBA_CD,
                               bool useNIOROSHI_GYOUSHA_CD, bool useUNPAN_GYOUSHA_CD, bool useGENBA_CD,
                               bool useGYOUSHA_CD, bool useTORIHIKISAKI_CD)
            {
                this.useHINMEI_CD = useHINMEI_CD;
                this.useUNIT_CD = useUNIT_CD;
                this.useNIOROSHI_GENBA_CD = useNIOROSHI_GENBA_CD;
                this.useNIOROSHI_GYOUSHA_CD = useNIOROSHI_GYOUSHA_CD;
                this.useUNPAN_GYOUSHA_CD = useUNPAN_GYOUSHA_CD;
                this.useGENBA_CD = useGENBA_CD;
                this.useGYOUSHA_CD = useGYOUSHA_CD;
                this.useTORIHIKISAKI_CD = useTORIHIKISAKI_CD;
            }

            /// <summary>
            /// 優先ルール一覧を返します
            /// </summary>
            /// <returns></returns>
            static public KOBETSU_HINMEI_TANKA_RULE[] RULES()
            {
                //HACK:仕様変更時は 資料の「共通動作・単価決定」シートのマトリクスを転記すればOK
                return new KOBETSU_HINMEI_TANKA_RULE[] {
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  true,  true,  true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  true,  true,  true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true,  true,  true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  false, true,  true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  false, true,  true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, true,  true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  true,  false, true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  true,  false, true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true,  false, true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  false, false, true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  false, false, true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, false, true, true  ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  true,  true,  true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  true,  true,  true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true,  true,  true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  false, true,  true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  false, true,  true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, true,  true, false ),
                    //MOD NHU 20211007 #157932 - >157937 S
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true, true, true, false,  false, true ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true, true, false,  false, true ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true, false,  false, true ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true, true, false, false,  false, true ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true, false, false,  false, true ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, false,  false, true ),
                    //MOD NHU 20211007 #157932 - >157937 E
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  true,  false, true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  true,  false, true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true,  false, true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  false, false, true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  false, false, true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, false, true, false )
               };
            }

            //デバッグログ用
            public override string ToString()
            {
                return string.Format("{0}{1} [ useHINMEI_CD = {2}, useUNIT_CD = {3}, useNIOROSHI_GENBA_CD = {4}, useNIOROSHI_GYOUSHA_CD = {5}, useUNPAN_GYOUSHA_CD = {6}, useGENBA_CD = {7}, useGYOUSHA_CD = {8}, useTORIHIKISAKI_CD = {9} ]",
                                   "", ""/*ダミー*/, useHINMEI_CD, useUNIT_CD, useNIOROSHI_GENBA_CD, useNIOROSHI_GYOUSHA_CD, useUNPAN_GYOUSHA_CD, useGENBA_CD, useGYOUSHA_CD, useTORIHIKISAKI_CD);
            }
        }

        /// <summary>
        /// 検索条件をまとめただけの構造体
        /// </summary>
        private struct KOBETSU_HINMEI_TANKA_PARAM
        {
            public string HINMEI_CD;
            public short UNIT_CD;
            public string NIOROSHI_GENBA_CD;
            public string NIOROSHI_GYOUSHA_CD;
            public string UNPAN_GYOUSHA_CD;
            public string GENBA_CD;
            public string GYOUSHA_CD;
            public string TORIHIKISAKI_CD;

            public short DENPYOU_KBN_CD;
            public short DENSHU_KBN_CD;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="HINMEI_CD">品名CD</param>
            /// <param name="UNIT_CD">単位(0は未設定とみなす)</param>
            /// <param name="NIOROSHI_GENBA_CD">荷卸現場CD</param>
            /// <param name="NIOROSHI_GYOUSHA_CD">荷卸業者CD</param>
            /// <param name="UNPAN_GYOUSHA_CD">運搬業者CD</param>
            /// <param name="GENBA_CD">現場CD</param>
            /// <param name="GYOUSHA_CD">業者CD</param>
            /// <param name="TORIHIKISAKI_CD">取引先CD</param>
            /// <param name="DENPYOU_KBN_CD">伝票区分(値チェックしていません。そのまま検索に利用します)</param>
            /// <param name="DENSHU_KBN_CD">伝種区分</param>
            public KOBETSU_HINMEI_TANKA_PARAM(string HINMEI_CD, short UNIT_CD, string NIOROSHI_GENBA_CD,
                               string NIOROSHI_GYOUSHA_CD, string UNPAN_GYOUSHA_CD, string GENBA_CD,
                               string GYOUSHA_CD, string TORIHIKISAKI_CD, short DENPYOU_KBN_CD, short DENSHU_KBN_CD)
            {
                this.HINMEI_CD = HINMEI_CD;
                this.UNIT_CD = UNIT_CD;
                this.NIOROSHI_GENBA_CD = NIOROSHI_GENBA_CD;
                this.NIOROSHI_GYOUSHA_CD = NIOROSHI_GYOUSHA_CD;
                this.UNPAN_GYOUSHA_CD = UNPAN_GYOUSHA_CD;
                this.GENBA_CD = GENBA_CD;
                this.GYOUSHA_CD = GYOUSHA_CD;
                this.TORIHIKISAKI_CD = TORIHIKISAKI_CD;
                this.DENPYOU_KBN_CD = DENPYOU_KBN_CD;
                this.DENSHU_KBN_CD = DENSHU_KBN_CD;
            }

            //デバッグログ用
            public override string ToString()
            {
                return string.Format("{0} [ DENPYOU_KBN_CD = {1}, HINMEI_CD = \"{2}\", UNIT_CD = {3}, NIOROSHI_GENBA_CD = \"{4}\", NIOROSHI_GYOUSHA_CD = \"{5}\", UNPAN_GYOUSHA_CD = \"{6}\", GENBA_CD = \"{7}\", GYOUSHA_CD = \"{8}\", TORIHIKISAKI_CD = \"{9}\" ]",
                                     ""/*ダミー*/, DENPYOU_KBN_CD, HINMEI_CD, UNIT_CD, NIOROSHI_GENBA_CD, NIOROSHI_GYOUSHA_CD, UNPAN_GYOUSHA_CD, GENBA_CD, GYOUSHA_CD, TORIHIKISAKI_CD);
            }
        }

        /// <summary>
        /// 個別品名単価取得（単一ルール）
        /// </summary>
        /// <param name="param">入力された条件</param>
        /// <param name="rule">検索ルール</param>
        /// <param name="ent">trueの場合、見つかった場合エンティティを返す。falseの場合null</param>
        /// <returns>true:データあり/false:データ無</returns>
        /// <returns>基準日(この引数が無い場合は現在日付を参照）</returns>
        private bool GetKobetsuhinmeiTanka(KOBETSU_HINMEI_TANKA_PARAM param, KOBETSU_HINMEI_TANKA_RULE rule, ref M_KOBETSU_HINMEI_TANKA ent, ref List<M_KOBETSU_HINMEI_TANKA> ents, string referenceDate = null)
        {
            LogUtility.DebugMethodStart(param, rule, ent, ents, referenceDate);

            // パラメータ必須チェック(使うものがnull等の場合即終了)
            if (rule.useHINMEI_CD && string.IsNullOrEmpty(param.HINMEI_CD)) return false;
            if (rule.useUNIT_CD && param.UNIT_CD < 0) return false;
            if (rule.useNIOROSHI_GENBA_CD && string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)) return false;
            if (rule.useNIOROSHI_GYOUSHA_CD && string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)) return false;
            if (rule.useUNPAN_GYOUSHA_CD && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD)) return false;
            if (rule.useGENBA_CD && string.IsNullOrEmpty(param.GENBA_CD)) return false;
            if (rule.useGYOUSHA_CD && string.IsNullOrEmpty(param.GYOUSHA_CD)) return false;
            if (rule.useTORIHIKISAKI_CD && string.IsNullOrEmpty(param.TORIHIKISAKI_CD)) return false;

            // 20150714 速度改善ため、条件に合ってる品名単価情報を一回で取得するように修正 Start
            // DBアクセス削減のため、一回初期検索のみ行う、nullではない場合、検索済みと見なし、再検索しない
            if (ents == null)
            {
                // 必要条件洗出し
                var ruleRequired = new KOBETSU_HINMEI_TANKA_RULE(
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useHINMEI_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useUNIT_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useNIOROSHI_GENBA_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useNIOROSHI_GYOUSHA_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useUNPAN_GYOUSHA_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useGENBA_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useGYOUSHA_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useTORIHIKISAKI_CD)
                    );

                //検索キー設定
                var keyRequired = new M_KOBETSU_HINMEI_TANKA();
                keyRequired.DENPYOU_KBN_CD = param.DENPYOU_KBN_CD;

                if (ruleRequired.useHINMEI_CD) keyRequired.HINMEI_CD = param.HINMEI_CD;
                if (ruleRequired.useUNIT_CD) keyRequired.UNIT_CD = param.UNIT_CD;
                if (ruleRequired.useNIOROSHI_GENBA_CD) keyRequired.NIOROSHI_GENBA_CD = param.NIOROSHI_GENBA_CD;
                if (ruleRequired.useNIOROSHI_GYOUSHA_CD) keyRequired.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
                if (ruleRequired.useUNPAN_GYOUSHA_CD) keyRequired.UNPAN_GYOUSHA_CD = param.UNPAN_GYOUSHA_CD;
                if (ruleRequired.useGENBA_CD) keyRequired.GENBA_CD = param.GENBA_CD;
                if (ruleRequired.useGYOUSHA_CD) keyRequired.GYOUSHA_CD = param.GYOUSHA_CD;
                if (ruleRequired.useTORIHIKISAKI_CD) keyRequired.TORIHIKISAKI_CD = param.TORIHIKISAKI_CD;

                // 初期化、結果が有るか問わず検索済みと見なし、再検索不要
                ents = new List<M_KOBETSU_HINMEI_TANKA>();

                // 適用日と比較する基準日が引数にあれば取得
                var d = new DateTime();
                var date = new SqlDateTime();
                if(DateTime.TryParse(referenceDate, out d))
                {
                    date = (SqlDateTime)d;
                }

                // 個別品名検索
                // 指定伝種区分
                if (param.DENSHU_KBN_CD > 0) keyRequired.DENSHU_KBN_CD = param.DENSHU_KBN_CD;
                if (date.IsNull)
                {
                    ents.AddRange(this.kobetsuHinmeiTankaDao.GetAllValidData(keyRequired));
                }
                else
                {
                    ents.AddRange(this.kobetsuHinmeiTankaDao.GetAllValidDataSpecifyDate(keyRequired, date));
                }

                // 「共通」伝種区分
                keyRequired.DENSHU_KBN_CD = Convert.ToInt16(DENSHU_KBN.KYOUTSUU);
                if (date.IsNull)
                {
                    ents.AddRange(this.kobetsuHinmeiTankaDao.GetAllValidData(keyRequired));
                }
                else
                {
                    ents.AddRange(this.kobetsuHinmeiTankaDao.GetAllValidDataSpecifyDate(keyRequired, date));
                }
                // 上記検索に、param.DENSHU_KBN_CD<=0の場合、伝種条件が入力しないので、
                // 検索結果に共通伝種検索と被るの可能性があるが、後続処理に影響がない
            }
            // 20150714 速度改善ため、条件に合ってる品名単価情報を一回で取得するように修正 Start

            // 20150714 速度改善ため、再検索でなく、上記キャッシュを利用するように修正 Start
            // 指定伝種と共通伝種は全部含むので、別々で共通を検索する必要ない
            // 検索実行(フィルター適用)
            var results = ents.
                // 条件を使う時は条件に合って、使わない時は必ず空白/null
                // 元下部のループに有ったの判断条件もここに組込む
                Where(o =>
                    ((rule.useHINMEI_CD && o.HINMEI_CD == param.HINMEI_CD) || (!rule.useHINMEI_CD && string.IsNullOrEmpty(o.HINMEI_CD))) &&
                    ((rule.useUNIT_CD && !o.UNIT_CD.IsNull && o.UNIT_CD.Value == param.UNIT_CD) || (!rule.useUNIT_CD && o.UNIT_CD.IsNull)) &&
                    ((rule.useNIOROSHI_GENBA_CD && o.NIOROSHI_GENBA_CD == param.NIOROSHI_GENBA_CD) || (!rule.useNIOROSHI_GENBA_CD && string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD))) &&
                    ((rule.useNIOROSHI_GYOUSHA_CD && o.NIOROSHI_GYOUSHA_CD == param.NIOROSHI_GYOUSHA_CD) || (!rule.useNIOROSHI_GYOUSHA_CD && string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD))) &&
                    ((rule.useUNPAN_GYOUSHA_CD && o.UNPAN_GYOUSHA_CD == param.UNPAN_GYOUSHA_CD) || (!rule.useUNPAN_GYOUSHA_CD && string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD))) &&
                    ((rule.useGENBA_CD && o.GENBA_CD == param.GENBA_CD) || (!rule.useGENBA_CD && string.IsNullOrEmpty(o.GENBA_CD))) &&
                    ((rule.useGYOUSHA_CD && o.GYOUSHA_CD == param.GYOUSHA_CD) || (!rule.useGYOUSHA_CD && string.IsNullOrEmpty(o.GYOUSHA_CD))) &&
                    ((rule.useTORIHIKISAKI_CD && o.TORIHIKISAKI_CD == param.TORIHIKISAKI_CD) || (!rule.useTORIHIKISAKI_CD && string.IsNullOrEmpty(o.TORIHIKISAKI_CD)))
                ).
                // ソート順(伝種区分：伝種は1~4及び100以上(運賃など)が可能ので、直接利用ではなく、0に変換し9より優先にする)
                OrderBy(o => (o.DENSHU_KBN_CD.IsNull || o.DENSHU_KBN_CD.Value != Convert.ToInt16(DENSHU_KBN.KYOUTSUU)) ? 0 : 9);
            if (results != null && 0 < results.Count())
            {
                // 20140714 LINQを利用するので、ループの代わり、LINQのFirstOrDefaultメソッドを使う Start
                //foreach (var result in results)
                //{
                // 20150714 キャッシュを利用するので、元の判断条件を上記LINQ句に含む
                // データあり
                ent = results.FirstOrDefault();
                LogUtility.DebugMethodEnd(true, ent, ents);
                return true;
                //}
                // 20140714 LINQを利用するので、ループの代わり、LINQのFirstOrDefaultメソッドを使う End
            }
            // 20150714 速度改善ため、再検索でなく、上記キャッシュを利用するように修正 End

            //データ無
            LogUtility.DebugMethodEnd(false, ent, ents);
            return false;
        }
        #endregion

        #region 基本品名単価検索
        /// <summary>
        /// 基本品名単価を取得
        /// 仕様は(G051)受入入力の[共通動作・単価決定]シートを参照
        /// </summary>
        /// <param name="denpyouKBNCd">伝票区分CD</param>
        /// <param name="unpanGyoushaCd">運搬業者CD</param>
        /// <param name="nioroshiGyoushaCd">荷降業者CD</param>
        /// <param name="nioroshiGenbaCd">荷降現場CD</param>
        /// <param name="hinmeiCd">品名CD</param>
        /// <param name="unitCd">単位CD</param>
        /// <returns></returns>
        public M_KIHON_HINMEI_TANKA GetKihonHinmeitanka(short denpyouKBNCd,
            string unpanGyoushaCd, string nioroshiGyoushaCd, string nioroshiGenbaCd,
            string hinmeiCd, short unitCd)
        {
            LogUtility.DebugMethodStart(denpyouKBNCd, unpanGyoushaCd, nioroshiGyoushaCd, nioroshiGenbaCd, hinmeiCd, unitCd);

            M_KIHON_HINMEI_TANKA keyEntity = new M_KIHON_HINMEI_TANKA();

            // ----20140715 DBアクセス削減ので、品名と単位をキーとして、基本品名一回のみ検索にする Start
            // TODO: 伝票区分は必要ない？
            if (!string.IsNullOrEmpty(hinmeiCd) && 0 < unitCd)
            {
                if (denpyouKBNCd != 9)
                {
                    keyEntity.DENPYOU_KBN_CD = denpyouKBNCd;
                }
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;

                var kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity).AsEnumerable();
                // 20140715 DBアクセス削減ので、品名と単位をキーとして、基本品名一回のみ検索にする End

                // 20140715 上記の検索キャッシュを利用するため、各ロジックを、検索処理からLINQフィルター処理に変更 Start
                // TODO: 下記条件の組合せは、個別品名のように、ルール化で順番処理可能のはず...
                // 値あり：品名, 単位
                if (string.IsNullOrEmpty(nioroshiGenbaCd)
                    && string.IsNullOrEmpty(nioroshiGyoushaCd)
                    && string.IsNullOrEmpty(unpanGyoushaCd))
                {
                    //keyEntity.HINMEI_CD = hinmeiCd;
                    //keyEntity.UNIT_CD = unitCd;
                    //keyEntity.NIOROSHI_GYOUSHA_CD = string.Empty;
                    //keyEntity.NIOROSHI_GENBA_CD = string.Empty;
                    //keyEntity.UNPAN_GYOUSHA_CD = string.Empty;

                    //kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    var resEntities000 = kihonHinmeiTankas.
                        // 条件空白ではない時は条件に合って、空白時は必ず空白/null
                        Where(o => string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD)).
                        Where(o => string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD)).
                        Where(o => string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD));
                    if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities000.FirstOrDefault());
                        return resEntities000.FirstOrDefault();
                    }
                }

                // すべて値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者
                if (!string.IsNullOrEmpty(nioroshiGenbaCd)
                    && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                    && !string.IsNullOrEmpty(unpanGyoushaCd))
                {
                    //keyEntity.HINMEI_CD = hinmeiCd;
                    //keyEntity.UNIT_CD = unitCd;
                    //keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                    //keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                    //keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;

                    //for (int i = 0; i < 4; i++)
                    //{
                    //    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    //    // 荷卸現場＞荷卸業者＞運搬業者
                    //    switch (i)
                    //    {
                    //        case 0:
                    //            break;
                    //        case 1:
                    //            keyEntity.NIOROSHI_GENBA_CD = null;
                    //            break;
                    //        case 2:
                    //            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                    //            break;
                    //        case 3:
                    //            keyEntity.UNPAN_GYOUSHA_CD = null;
                    //            break;
                    //        default:
                    //            break;
                    //    }

                    //    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    //    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    //    {
                    //        LogUtility.DebugMethodEnd(kihonHinmeiTankas[0]);
                    //        return kihonHinmeiTankas[0];
                    //    }
                    //}
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない
                    var resEntities001 = resEntities000.Where(o => o.UNPAN_GYOUSHA_CD == unpanGyoushaCd); // 運搬業者フィルター追加
                    var resEntities011 = resEntities001.Where(o => o.NIOROSHI_GYOUSHA_CD == nioroshiGyoushaCd); // 更に荷卸業者フィルター追加
                    var resEntities111 = resEntities011.Where(o => o.NIOROSHI_GENBA_CD == nioroshiGenbaCd); // 更に荷卸現場フィルター追加

                    // 荷卸現場と荷卸業者と運搬業者条件合ったら、優先
                    if (resEntities111 != null && resEntities111.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities111.FirstOrDefault());
                        return resEntities111.FirstOrDefault();
                    }
                    // 荷卸現場条件外す
                    else if (resEntities011 != null && resEntities011.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities011.FirstOrDefault());
                        return resEntities011.FirstOrDefault();
                    }
                    // 更に荷卸業者条件外す
                    else if (resEntities001 != null && resEntities001.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities001.FirstOrDefault());
                        return resEntities001.FirstOrDefault();
                    }
                    // 更に運搬業者条件外す(全条件外す)
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities000.FirstOrDefault());
                        return resEntities000.FirstOrDefault();
                    }
                }

                // 値あり：品名, 単位, 荷卸業者, 運搬業者
                if (string.IsNullOrEmpty(nioroshiGenbaCd)
                    && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                    && !string.IsNullOrEmpty(unpanGyoushaCd))
                {
                    //keyEntity.HINMEI_CD = hinmeiCd;
                    //keyEntity.UNIT_CD = unitCd;
                    //keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                    //keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;

                    //for (int i = 0; i < 3; i++)
                    //{
                    //    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    //    // 荷卸現場＞荷卸業者＞運搬業者
                    //    switch (i)
                    //    {
                    //        case 0:
                    //            break;
                    //        case 1:
                    //            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                    //            break;
                    //        case 2:
                    //            keyEntity.UNPAN_GYOUSHA_CD = null;
                    //            break;
                    //        default:
                    //            break;
                    //    }

                    //    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    //    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    //    {
                    //        LogUtility.DebugMethodEnd(kihonHinmeiTankas[0]);
                    //        return kihonHinmeiTankas[0];
                    //    }
                    //}
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞運搬業者
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない
                    var resEntities001 = resEntities000.Where(o => o.UNPAN_GYOUSHA_CD == unpanGyoushaCd); // 運搬業者フィルター追加
                    var resEntities011 = resEntities001.Where(o => o.NIOROSHI_GYOUSHA_CD == nioroshiGyoushaCd); // 更に荷卸業者フィルター追加

                    // 荷卸業者と運搬業者条件合ったら、優先
                    if (resEntities011 != null && resEntities011.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities011.FirstOrDefault());
                        return resEntities011.FirstOrDefault();
                    }
                    // 荷卸業者条件外す
                    else if (resEntities001 != null && resEntities001.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities001.FirstOrDefault());
                        return resEntities001.FirstOrDefault();
                    }
                    // 更に運搬業者条件外す(全条件外す)
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities000.FirstOrDefault());
                        return resEntities000.FirstOrDefault();
                    }
                }

                // 値あり：品名, 単位, 運搬業者
                if (string.IsNullOrEmpty(nioroshiGenbaCd)
                    && string.IsNullOrEmpty(nioroshiGyoushaCd)
                    && !string.IsNullOrEmpty(unpanGyoushaCd))
                {
                    //keyEntity.HINMEI_CD = hinmeiCd;
                    //keyEntity.UNIT_CD = unitCd;
                    //keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;

                    //for (int i = 0; i < 2; i++)
                    //{
                    //    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    //    // 荷卸現場＞荷卸業者＞運搬業者
                    //    switch (i)
                    //    {
                    //        case 0:
                    //            break;
                    //        case 1:
                    //            keyEntity.UNPAN_GYOUSHA_CD = null;
                    //            break;
                    //        default:
                    //            break;
                    //    }

                    //    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    //    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    //    {
                    //        LogUtility.DebugMethodEnd(kihonHinmeiTankas[0]);
                    //        return kihonHinmeiTankas[0];
                    //    }
                    //}
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 運搬業者
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない
                    var resEntities001 = resEntities000.Where(o => o.UNPAN_GYOUSHA_CD == unpanGyoushaCd); // 運搬業者フィルター追加

                    // 運搬業者条件合ったら、優先
                    if (resEntities001 != null && resEntities001.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities001.FirstOrDefault());
                        return resEntities001.FirstOrDefault();
                    }
                    // 運搬業者条件外す(全条件外す)
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities000.FirstOrDefault());
                        return resEntities000.FirstOrDefault();
                    }
                }

                // 値あり：品名, 単位, 荷卸現場, 荷卸業者
                if (!string.IsNullOrEmpty(nioroshiGenbaCd)
                    && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                    && string.IsNullOrEmpty(unpanGyoushaCd))
                {
                    //keyEntity.HINMEI_CD = hinmeiCd;
                    //keyEntity.UNIT_CD = unitCd;
                    //keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                    //keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;

                    //for (int i = 0; i < 3; i++)
                    //{
                    //    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    //    // 荷卸現場＞荷卸業者＞運搬業者
                    //    switch (i)
                    //    {
                    //        case 0:
                    //            break;
                    //        case 1:
                    //            keyEntity.NIOROSHI_GENBA_CD = null;
                    //            break;
                    //        case 2:
                    //            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                    //            break;
                    //        default:
                    //            break;
                    //    }

                    //    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    //    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    //    {
                    //        LogUtility.DebugMethodEnd(kihonHinmeiTankas[0]);
                    //        return kihonHinmeiTankas[0];
                    //    }
                    //}
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない
                    var resEntities010 = resEntities000.Where(o => o.NIOROSHI_GYOUSHA_CD == nioroshiGyoushaCd); // 荷卸業者フィルター追加
                    var resEntities110 = resEntities010.Where(o => o.NIOROSHI_GENBA_CD == nioroshiGenbaCd); // 更に荷卸現場フィルター追加

                    // 荷卸現場と荷卸業者条件合ったら、優先
                    if (resEntities110 != null && resEntities110.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities110.FirstOrDefault());
                        return resEntities110.FirstOrDefault();
                    }
                    // 荷卸現場条件外す
                    else if (resEntities010 != null && resEntities010.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities010.FirstOrDefault());
                        return resEntities010.FirstOrDefault();
                    }
                    // 更に荷卸業者条件外す(全条件外す)
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities000.FirstOrDefault());
                        return resEntities000.FirstOrDefault();
                    }
                }

                // 値あり：品名, 単位, 荷卸業者
                if (string.IsNullOrEmpty(nioroshiGenbaCd)
                    && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                    && string.IsNullOrEmpty(unpanGyoushaCd))
                {
                    //keyEntity.HINMEI_CD = hinmeiCd;
                    //keyEntity.UNIT_CD = unitCd;
                    //keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;

                    //for (int i = 0; i < 2; i++)
                    //{
                    //    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    //    // 荷卸現場＞荷卸業者＞運搬業者
                    //    switch (i)
                    //    {
                    //        case 0:
                    //            break;
                    //        case 1:
                    //            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                    //            break;
                    //        default:
                    //            break;
                    //    }

                    //    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    //    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    //    {
                    //        LogUtility.DebugMethodEnd(kihonHinmeiTankas[0]);
                    //        return kihonHinmeiTankas[0];
                    //    }
                    //}
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない
                    var resEntities010 = resEntities000.Where(o => o.NIOROSHI_GYOUSHA_CD == nioroshiGyoushaCd); // 荷卸業者フィルター追加

                    // 荷卸業者条件合ったら、優先
                    if (resEntities010 != null && resEntities010.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities010.FirstOrDefault());
                        return resEntities010.FirstOrDefault();
                    }
                    // 荷卸業者条件外す(全条件外す)
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(resEntities000.FirstOrDefault());
                        return resEntities000.FirstOrDefault();
                    }
                }
                // 20140715 上記の検索キャッシュを利用するため、各ロジックを、検索処理からLINQフィルター処理に変更 Start
            }

            // 何も取得できなかった場合
            LogUtility.DebugMethodEnd(null);
            return null;
        }

        /// <summary>
        /// 基本品名単価を取得
        /// 仕様は(G051)受入入力の[共通動作・単価決定]シートを参照
        /// </summary>
        /// <param name="denshuKBNCd">伝種区分CD</param>
        /// <param name="denpyouKBNCd">伝票区分CD</param>
        /// <param name="unpanGyoushaCd">運搬業者CD</param>
        /// <param name="nioroshiGyoushaCd">荷降業者CD</param>
        /// <param name="nioroshiGenbaCd">荷降現場CD</param>
        /// <param name="hinmeiCd">品名CD</param>
        /// <param name="unitCd">単位CD</param>
        /// <param name="unitCd">基準日(この引数が無い場合は現在日付を参照）</param>
        /// <returns></returns>
        public M_KIHON_HINMEI_TANKA GetKihonHinmeitanka(short denshuKBNCd, short denpyouKBNCd,
            string unpanGyoushaCd, string nioroshiGyoushaCd, string nioroshiGenbaCd,
            string hinmeiCd, short unitCd, string referenceDate = null)
        {
            LogUtility.DebugMethodStart(denshuKBNCd, denpyouKBNCd, unpanGyoushaCd, nioroshiGyoushaCd, nioroshiGenbaCd, hinmeiCd, unitCd, referenceDate);

            M_KIHON_HINMEI_TANKA keyEntity = new M_KIHON_HINMEI_TANKA();

            // ----20140715 DBアクセス削減ので、品名と単位をキーとして、基本品名一回のみ検索にする Start
            if (!string.IsNullOrEmpty(hinmeiCd) && 0 < unitCd)
            {
                // 適用日と比較する基準日が引数にあれば取得
                var d = new DateTime();
                var date = new SqlDateTime();
                if(DateTime.TryParse(referenceDate, out d))
                {
                    date = (SqlDateTime)d;
                }

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;

                M_KIHON_HINMEI_TANKA[] entitys;
                if (date.IsNull)
                {
                    entitys = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                }
                else
                {
                    entitys = this.kihonHinmeiTankaDao.GetAllValidDataSpecifyDate(keyEntity, date);
                }

                var kihonHinmeiTankas = entitys.AsEnumerable().
                    // 予備のため、先に伝票区分と伝種区分は合った又は共通のデータをフィルターする
                    Where(o => !o.DENPYOU_KBN_CD.IsNull && (o.DENPYOU_KBN_CD.Value == denpyouKBNCd || o.DENPYOU_KBN_CD.Value == (short)DENSHU_KBN.KYOUTSUU)).
                    Where(o => !o.DENSHU_KBN_CD.IsNull && (o.DENSHU_KBN_CD.Value == denshuKBNCd || o.DENSHU_KBN_CD.Value == (short)DENSHU_KBN.KYOUTSUU));
                // 20140715 DBアクセス削減ので、品名と単位をキーとして、基本品名一回のみ検索にする End

                // 20140715 上記の検索キャッシュを利用するため、各ロジックを、検索処理からLINQフィルター処理に変更 Start
                // TODO: 下記条件の組合せは、個別品名のように、ルール化で順番処理可能のはず...
                // 値あり：品名, 単位
                if (string.IsNullOrEmpty(nioroshiGenbaCd)
                    && string.IsNullOrEmpty(nioroshiGyoushaCd)
                    && string.IsNullOrEmpty(unpanGyoushaCd))
                {
                    var resEntities000 = kihonHinmeiTankas.
                        // 条件空白ではない時は条件に合って、空白時は必ず空白/null
                        Where(o => string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD)).
                        Where(o => string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD)).
                        Where(o => string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD));
                    if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        resEntities000 = resEntities000.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                            OrderBy(o => o.DENSHU_KBN_CD.Value == denshuKBNCd ? 0 : 9). // 伝種区分順追加(伝種は1~4及び100以上(運賃など)が可能ので、直接利用ではなく、0に変換し9より優先にする)
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == denpyouKBNCd ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(resEntities000.FirstOrDefault());
                        return resEntities000.FirstOrDefault();
                    }
                }
                // すべて値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者
                else
                {
                    // 「環境将軍R_単価取得標準ルール.doc」の【基本品名単価の引き方】に準拠
                    // TODO : 個別品名単価と同様の処理に「KOBETSU_HINMEI_TANKA_RULE」を使う形にしたいが影響範囲が大きいため今回は諦める

                    // 荷卸現場と荷卸業者と運搬業者
                    var resEntities111 = kihonHinmeiTankas.Where(o =>
                            (!string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD) && o.NIOROSHI_GYOUSHA_CD == nioroshiGyoushaCd)
                            && (!string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD) && o.NIOROSHI_GENBA_CD == nioroshiGenbaCd)
                            && (!string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD) && o.UNPAN_GYOUSHA_CD == unpanGyoushaCd)
                        );

                    // 荷卸業者と運搬業者
                    var resEntities011 = kihonHinmeiTankas.Where(o =>
                            (!string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD) && o.NIOROSHI_GYOUSHA_CD == nioroshiGyoushaCd)
                            && (string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD))
                            && (!string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD) && o.UNPAN_GYOUSHA_CD == unpanGyoushaCd)
                        );

                    // 運搬業者
                    var resEntities001 = kihonHinmeiTankas.Where(o =>
                            (string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD))
                            && (string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD))
                            && (!string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD) && o.UNPAN_GYOUSHA_CD == unpanGyoushaCd)
                        );

                    // 荷卸現場と荷卸業者
                    var resEntities110 = kihonHinmeiTankas.Where(o =>
                            (!string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD) && o.NIOROSHI_GYOUSHA_CD == nioroshiGyoushaCd)
                            && (!string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD) && o.NIOROSHI_GENBA_CD == nioroshiGenbaCd)
                            && (string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD))
                        );

                    // 荷卸業者
                    var resEntities010 = kihonHinmeiTankas.Where(o =>
                            (!string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD) && o.NIOROSHI_GYOUSHA_CD == nioroshiGyoushaCd)
                            && (string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD))
                            && (string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD))
                        );
                    // 全てブランク
                    var resEntities000 = kihonHinmeiTankas.Where(o =>
                            (string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD))
                            && (string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD))
                            && (string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD))
                        );

                    // 荷卸現場と荷卸業者と運搬業者条件合ったら、優先
                    if (resEntities111 != null && resEntities111.Count() > 0)
                    {
                        resEntities111 = resEntities111.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                            OrderBy(o => o.DENSHU_KBN_CD.Value == denshuKBNCd ? 0 : 9). // 伝種区分順追加
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == denpyouKBNCd ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(resEntities111.FirstOrDefault());
                        return resEntities111.FirstOrDefault();
                    }
                    // 荷卸現場条件外す
                    else if (resEntities011 != null && resEntities011.Count() > 0)
                    {
                        resEntities011 = resEntities011.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                            OrderBy(o => o.DENSHU_KBN_CD.Value == denshuKBNCd ? 0 : 9). // 伝種区分順追加
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == denpyouKBNCd ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(resEntities011.FirstOrDefault());
                        return resEntities011.FirstOrDefault();
                    }
                    // 更に荷卸業者条件外す
                    else if (resEntities001 != null && resEntities001.Count() > 0)
                    {
                        resEntities001 = resEntities001.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                            OrderBy(o => o.DENSHU_KBN_CD.Value == denshuKBNCd ? 0 : 9). // 伝種区分順追加
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == denpyouKBNCd ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(resEntities001.FirstOrDefault());
                        return resEntities001.FirstOrDefault();
                    }
                    else if (resEntities110 != null && resEntities110.Count() > 0)
                    {
                        resEntities110 = resEntities110.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                            OrderBy(o => o.DENSHU_KBN_CD.Value == denshuKBNCd ? 0 : 9). // 伝種区分順追加
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == denpyouKBNCd ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(resEntities110.FirstOrDefault());
                        return resEntities110.FirstOrDefault();
                    }
                    else if (resEntities010 != null && resEntities010.Count() > 0)
                    {
                        resEntities010 = resEntities010.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                            OrderBy(o => o.DENSHU_KBN_CD.Value == denshuKBNCd ? 0 : 9). // 伝種区分順追加
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == denpyouKBNCd ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(resEntities010.FirstOrDefault());
                        return resEntities010.FirstOrDefault();
                    }
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        resEntities000 = resEntities000.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                            OrderBy(o => o.DENSHU_KBN_CD.Value == denshuKBNCd ? 0 : 9). // 伝種区分順追加
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == denpyouKBNCd ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(resEntities000.FirstOrDefault());
                        return resEntities000.FirstOrDefault();
                    }
                }
                // 20140715 上記の検索キャッシュを利用するため、各ロジックを、検索処理からLINQフィルター処理に変更 End
            }

            // 何も取得できなかった場合
            LogUtility.DebugMethodEnd(null);
            return null;
        }
        #endregion

        #endregion

        #region Utility
        #endregion
    }
}
