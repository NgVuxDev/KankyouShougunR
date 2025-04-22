using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using r_framework.Utility;
using System.Data.SqlTypes;
using r_framework.Logic;
using System.Reflection;
using System.IO;
using System.Data;
using Shougun.Function.ShougunCSCommon.Utility;

namespace Shougun.Core.Carriage.UnchinNyuuRyoku
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

        /// <summary>
        /// IM_UNCHIN_TANKADao
        /// </summary>
        r_framework.Dao.IM_UNCHIN_TANKADao hinmeiTankaDao;

        /// <summary>
        /// IM_UNCHIN_HINMEIDao
        /// </summary>
        r_framework.Dao.IM_UNCHIN_HINMEIDao hinmeiDao;

        /// <summary>
        /// IS_NUMBER_SYSTEMDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// IS_NUMBER_DENSHUDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_DENSHUDao numberDenshuDao;


        /// <summary>
        /// IM_KYOTENDao
        /// </summary>
        r_framework.Dao.IM_KYOTENDao kyotenDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        r_framework.Dao.IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        r_framework.Dao.IM_GENBADao genbaDao;

        /// <summary>
        /// IM_SHASHUDao
        /// </summary>
        r_framework.Dao.IM_SHASHUDao shashuDao;

       

        /// <summary>
        /// IM_SHOUHIZEIDao
        /// </summary>
        r_framework.Dao.IM_SHOUHIZEIDao shouhizeiDao;

        /// <summary>
        /// IM_SHAINDao
        /// </summary>
        r_framework.Dao.IM_SHAINDao shainDao;

        /// <summary>
        /// IM_YOUKIDao
        /// </summary>
        r_framework.Dao.IM_YOUKIDao youkiDao;

        /// <summary>
        /// IM_TORIHIKISAKIDao
        /// </summary>
        r_framework.Dao.IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// IM_TORIHIKISAKI_SEIKYUUDao
        /// </summary>
        r_framework.Dao.IM_TORIHIKISAKI_SEIKYUUDao torihikisakiSeikyuuDao;

        /// <summary>
        /// IM_TORIHIKISAKI_SHIHARAIDao
        /// </summary>
        r_framework.Dao.IM_TORIHIKISAKI_SHIHARAIDao torihikisakiShiharaiDao;

        /// <summary>
        /// IM_MANIFEST_SHURUIDao
        /// </summary>
        r_framework.Dao.IM_MANIFEST_SHURUIDao manifestShuruiDao;

        /// <summary>
        /// IM_MANIFEST_TEHAIDao
        /// </summary>
        r_framework.Dao.IM_MANIFEST_TEHAIDao manifestTehaiDao;

        /// <summary>
        /// IM_SHARYOUDao
        /// </summary>
        r_framework.Dao.IM_SHARYOUDao sharyouDao;

        /// <summary>
        /// IM_UNITDao
        /// </summary>
        r_framework.Dao.IM_UNITDao unitDao;

        /// <summary>
        /// IM_KOBETSU_HINMEI_TANKADao
        /// </summary>
        r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao kobetsuHinmeiTankaDao;

        /// <summary>
        /// 
        /// </summary>
        r_framework.Dao.IM_KIHON_HINMEI_TANKADao kihonHinmeiTankaDao;

        /// <summary>
        /// IM_CONTENA_JOUKYOUDao
        /// </summary>
        r_framework.Dao.IM_CONTENA_JOUKYOUDao contenaJoukyouDao;

        /// <summary>
        /// IM_DENSHU_KBNDao
        /// </summary>
        r_framework.Dao.IM_DENSHU_KBNDao denshuKbnDao;

        /// <summary>
        /// IM_DENPYOU_KBNDao
        /// </summary>
        r_framework.Dao.IM_DENPYOU_KBNDao denpyouKbnDao;

        /// <summary>
        /// IM_KEITAI_KBNDao
        /// </summary>
        r_framework.Dao.IM_KEITAI_KBNDao keitaiKbnDao;

        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // スタートアッププロジェクトのDiconに情報が設定されていることを必ず確認
            this.sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNCHIN_HINMEIDao>();
            this.hinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNCHIN_TANKADao>();
            
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();
            this.shouhizeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHOUHIZEIDao>();
            this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();
            this.youkiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_YOUKIDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
            this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKI_SEIKYUUDao>();
            this.torihikisakiShiharaiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKI_SHIHARAIDao>();
            this.manifestShuruiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_SHURUIDao>();
            this.manifestTehaiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_TEHAIDao>();
            this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();
            this.unitDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNITDao>();
            this.kobetsuHinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao>();
            this.kihonHinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KIHON_HINMEI_TANKADao>();
            this.contenaJoukyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CONTENA_JOUKYOUDao>();
            this.denpyouKbnDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_DENPYOU_KBNDao>();
            this.denshuKbnDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_DENSHU_KBNDao>();
            this.keitaiKbnDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KEITAI_KBNDao>();
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

        ///// <summary>
        ///// 売上/支払入力を取得
        ///// </summary>
        ///// <param name="ukeireNumber">売上/支払番号</param>
        ///// <returns></returns>
        //public T_UNCHIN_ENTRY[] GetUnchinEntry(SqlInt64 denyouNumber)
        //{
        //    T_UNCHIN_ENTRY entity = new T_UNCHIN_ENTRY();
        //    entity.DENPYOU_NUMBER = denyouNumber;
        //    return this.urshEntryDao.GetDataForEntity(entity);
        //}

        ///// <summary>
        ///// 売上/支払入力の登録
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public int InsertUrshEntry(T_UR_SH_ENTRY entity)
        //{
        //    return this.urshEntryDao.Insert(entity);
        //}

        ///// <summary>
        ///// 売上/支払入力の更新
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public int UpdateUrshEntry(T_UR_SH_ENTRY entity)
        //{
        //    return this.urshEntryDao.Update(entity);
        //}

        ///// <summary>
        ///// 売上/支払明細を取得
        ///// </summary>
        ///// <param name="entrySysId"></param>
        ///// <param name="entrySeq"></param>
        ///// <returns></returns>
        //public T_UR_SH_DETAIL[] GetUrshDetail(SqlInt64 entrySysId, SqlInt32 entrySeq)
        //{
        //    T_UR_SH_DETAIL entity = new T_UR_SH_DETAIL();
        //    entity.SYSTEM_ID = entrySysId;
        //    entity.SEQ = entrySeq;
        //    return urshDetailDao.GetDataForEntity(entity);
        //}

        ///// <summary>
        ///// 売上/支払明細を登録
        ///// </summary>
        ///// <param name="entitys"></param>
        ///// <returns></returns>
        //public int InsertUrshDetail(T_UR_SH_DETAIL[] entitys)
        //{
        //    int returnint = 0;

        //    foreach (var ukeireDetail in entitys)
        //    {
        //        returnint = returnint + this.urshDetailDao.Insert(ukeireDetail);
        //    }

        //    return returnint;
        //}

        ///// <summary>
        ///// 売上/支払明細を更新
        ///// </summary>
        ///// <param name="entitys"></param>
        ///// <returns></returns>
        //public int UpdateUrshDetail(T_UR_SH_DETAIL[] entitys)
        //{
        //    int returnint = 0;

        //    foreach (var ukeireDetail in entitys)
        //    {
        //        returnint = returnint + this.urshDetailDao.Update(ukeireDetail);
        //    }

        //    return returnint;
        //}

        ///// <summary>
        ///// 年連番のデータを取得
        ///// </summary>
        ///// <param name="numberedYear">年度</param>
        ///// <param name="denshuKbnCd">伝種区分CD</param>
        ///// <param name="kyotenCd">拠点CD</param>
        ///// <returns></returns>
        //public S_NUMBER_YEAR[] GetNumberYear(SqlInt32 numberedYear, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        //{
        //    S_NUMBER_YEAR entity = new S_NUMBER_YEAR();
        //    entity.NUMBERED_YEAR = numberedYear;
        //    entity.DENSHU_KBN_CD = denshuKbnCd;
        //    entity.KYOTEN_CD = kyotenCd;
        //    return this.numberYearDao.GetDataForEntity(entity);
        //}

        ///// <summary>
        ///// 年連番を登録
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public int InsertNumberYear(S_NUMBER_YEAR entity)
        //{
        //    return this.numberYearDao.Insert(entity);
        //}

        ///// <summary>
        ///// 年連番を更新
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public int UpdateNumberYear(S_NUMBER_YEAR entity)
        //{
        //    return this.numberYearDao.Update(entity);
        //}

        ///// <summary>
        ///// 日連番のデータを取得
        ///// </summary>
        ///// <param name="numberedDay">日付</param>
        ///// <param name="denshuKbnCd">伝種区分CD</param>
        ///// <param name="kyotenCd">拠点CD</param>
        ///// <returns></returns>
        //public S_NUMBER_DAY[] GetNumberDay(DateTime numberedDay, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        //{
        //    S_NUMBER_DAY entity = new S_NUMBER_DAY();
        //    entity.NUMBERED_DAY = numberedDay.Date;
        //    entity.DENSHU_KBN_CD = denshuKbnCd;
        //    entity.KYOTEN_CD = kyotenCd;
        //    return this.numberDayDao.GetDataForEntity(entity);
        //}

        ///// <summary>
        ///// 日連番を登録
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public int InsertNumberDay(S_NUMBER_DAY entity)
        //{
        //    return this.numberDayDao.Insert(entity);
        //}

        ///// <summary>
        ///// 日連番を更新
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public int UpdateNumberDay(S_NUMBER_DAY entity)
        //{
        //    return this.numberDayDao.Update(entity);
        //}

        ///// <summary>
        ///// 受入入力用のSYSTEM_ID採番処理
        ///// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        ///// 最新のID + 1の値を返す
        ///// </summary>
        ///// <returns>採番した数値</returns>
        //public SqlInt64 createSystemIdForUrsh()
        //{
        //    SqlInt64 returnInt = 1;

        //    var entity = new S_NUMBER_SYSTEM();
        //    entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;

        //    var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
        //    returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

        //    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
        //    {
        //        updateEntity = new S_NUMBER_SYSTEM();
        //        updateEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
        //        updateEntity.CURRENT_NUMBER = returnInt;
        //        updateEntity.DELETE_FLG = false;
        //        var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
        //        dataBinderEntry.SetSystemProperty(updateEntity, false);

        //        this.numberSystemDao.Insert(updateEntity);
        //    }
        //    else
        //    {
        //        updateEntity.CURRENT_NUMBER = returnInt;
        //        this.numberSystemDao.Update(updateEntity);
        //    }

        //    return returnInt;
        //}

        //public SqlInt64 createUrshNumber()
        //{
        //    SqlInt64 returnInt = -1;

        //    var entity = new S_NUMBER_DENSHU();
        //    entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE;

        //    var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
        //    returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

        //    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
        //    {
        //        updateEntity = new S_NUMBER_DENSHU();
        //        updateEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE;
        //        updateEntity.CURRENT_NUMBER = returnInt;
        //        updateEntity.DELETE_FLG = false;
        //        var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
        //        dataBinderEntry.SetSystemProperty(updateEntity, false);

        //        this.numberDenshuDao.Insert(updateEntity);
        //    }
        //    else
        //    {
        //        updateEntity.CURRENT_NUMBER = returnInt;
        //        this.numberDenshuDao.Update(updateEntity);
        //    }

        //    return returnInt;
        //}

        /// <summary>
        /// 品名テーブルの情報を取得
        /// 適用開始日、終了日、削除フラグについては
        /// 有効なものだけを検索します
        /// </summary>
        /// <param name="key">品名CD</param>
        /// <returns></returns>
        public M_UNCHIN_HINMEI[] GetAllValidHinmeiData(string key = null)
        {
            M_UNCHIN_HINMEI keyEntity = new M_UNCHIN_HINMEI();
            if (!string.IsNullOrEmpty(key))
            {
                keyEntity.UNCHIN_HINMEI_CD = key;
            }

            return this.hinmeiDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 拠点を取得
        /// 適用開始日、適用終了日、削除フラグを考慮せず指定されたCDのデータを取得する
        /// </summary>
        /// <param name="kyotenCd">KYOTEN_CD</param>
        /// <returns></returns>
        public M_KYOTEN[] GetAllDataByCodeForKyoten(short kyotenCd)
        {
            if (kyotenCd < 0)
            {
                return null;
            }

            M_KYOTEN keyEntity = new M_KYOTEN();
            keyEntity.KYOTEN_CD = kyotenCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = SqlBoolean.True.Value;
            return this.kyotenDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 拠点を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="kyotenCd">KYOTEN_CD</param>
        /// <returns></returns>
        public M_KYOTEN[] GetDataByCodeForKyoten(short kyotenCd)
        {
            if (kyotenCd < 0)
            {
                return null;
            }

            M_KYOTEN keyEntity = new M_KYOTEN();
            keyEntity.KYOTEN_CD = kyotenCd;
            return this.kyotenDao.GetAllValidData(keyEntity);
        }

        ///// <summary>
        ///// 荷降業者を取得
        ///// 適用開始日、適用終了日、削除フラグについては
        ///// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        ///// </summary>
        ///// <param name="gyoushaCd">GYOSHA_CD</param>
        ///// <returns></returns>
        //public M_GYOUSHA[] GetNisumiGyousya(string gyoushaCd)
        //{

        //    M_GYOUSHA[] gyoushas = null;

        //    if (string.IsNullOrEmpty(gyoushaCd))
        //    {
        //        return gyoushas;
        //    }

        //    // SQL文作成
        //    DataTable dt = new DataTable();
        //    string whereStr = " AND GYOUSHA_CD = '" + gyoushaCd + "'";

        //    var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
        //    using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.Gyousha.NizumiGyoushaCondition.sql"))
        //    {
        //        using (var sqlStr = new StreamReader(resourceStream))
        //        {
        //            dt = this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

        //        }
        //    }

        //    if (dt == null || dt.Rows.Count < 1)
        //    {
        //        return gyoushas;
        //    }

        //    var dataBinderUtil = new DataBinderUtility<M_GYOUSHA>();

        //    gyoushas = dataBinderUtil.CreateEntityForDataTable(dt);

        //    return gyoushas;
        //}

        ///// <summary>
        ///// 荷降現場を取得
        ///// 適用開始日、適用終了日、削除フラグについては
        ///// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        ///// </summary>
        ///// <param name="genbaCd">GYOUSHA_CD</param>
        ///// <param name="gyoushaCd">GENBA_CD</param>
        ///// <returns></returns>
        //public M_GENBA[] GetNisumiGenba(string gyoushaCd, string genbaCd)
        //{

        //    M_GENBA[] genbas = null;

        //    if (string.IsNullOrEmpty(genbaCd))
        //    {
        //        return genbas;
        //    }

        //    // SQL文作成
        //    DataTable dt = new DataTable();
        //    string whereStr = "AND (GYOUSHA_CD = '" + gyoushaCd + "' AND GENBA_CD = '" + genbaCd + "')";

        //    var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
        //    using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.Genba.NizumiGenbaCondition.sql"))
        //    {
        //        using (var sqlStr = new StreamReader(resourceStream))
        //        {
        //            dt = this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

        //        }
        //    }

        //    if (dt == null || dt.Rows.Count < 1)
        //    {
        //        return genbas;
        //    }

        //    var dataBinderUtil = new DataBinderUtility<M_GENBA>();

        //    genbas = dataBinderUtil.CreateEntityForDataTable(dt);

        //    return genbas;
        //}

        ///// <summary>
        ///// 運搬業者を取得
        ///// 適用開始日、適用終了日、削除フラグについては
        ///// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        ///// </summary>
        ///// <param name="gyoushaCd">GYOSHA_CD</param>
        ///// <returns></returns>
        //public M_GYOUSHA[] GetUnpanGyousya(string gyoushaCd)
        //{

        //    M_GYOUSHA[] gyoushas = null;

        //    if (string.IsNullOrEmpty(gyoushaCd))
        //    {
        //        return gyoushas;
        //    }

        //    // SQL文作成
        //    DataTable dt = new DataTable();
        //    string whereStr = " AND GYOUSHA_CD = '" + gyoushaCd + "'";

        //    var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
        //    using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.Gyousha.UnpanGyoushaCondition.sql"))
        //    {
        //        using (var sqlStr = new StreamReader(resourceStream))
        //        {
        //            dt = this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

        //        }
        //    }

        //    if (dt == null || dt.Rows.Count < 1)
        //    {
        //        return gyoushas;
        //    }

        //    var dataBinderUtil = new DataBinderUtility<M_GYOUSHA>();

        //    gyoushas = dataBinderUtil.CreateEntityForDataTable(dt);

        //    return gyoushas;
        //}

        ///// <summary>
        ///// マニフェスト取得
        ///// 取得後、UrshDetailのキーでさらに絞り込んで使用してもらうため、
        ///// あえてDataTableで返しています。
        ///// </summary>
        ///// <param name="ukeireDetails">連携しているT_UR_SH_DETAIL</param>
        ///// <returns>必要なキーが設定されていない場合はNullを返します。</returns>
        //public DataTable GetManifestEntry(T_UR_SH_DETAIL[] urshDetails)
        //{
        //    if (urshDetails == null || urshDetails.Length < 1)
        //    {
        //        return null;
        //    }

        //    if (urshDetails[0].SYSTEM_ID.IsNull)
        //    {
        //        // 念のため先頭のキー値チェック
        //        return null;
        //    }

        //    // SQL文作成
        //    string whereStr = " AND RENKEI_DENSHU_KBN_CD = '" + SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI + "' AND RENKEI_SYSTEM_ID = '" + urshDetails[0].SYSTEM_ID + "' AND RENKEI_MEISAI_SYSTEM_ID IN ( ";

        //    // RENKEI_MEISAI_SYSTEM_ID用の条件句作成
        //    bool existRenkeiMeisaiId = false;
        //    StringBuilder sb = new StringBuilder();

        //    for (int i = 0; i < urshDetails.Length; i++)
        //    {
        //        if (urshDetails[i].DETAIL_SYSTEM_ID.IsNull)
        //        {
        //            continue;
        //        }

        //        if (i == 0)
        //        {
        //            existRenkeiMeisaiId = true;
        //            sb.Append("'" + urshDetails[i].DETAIL_SYSTEM_ID + "'");
        //        }
        //        else
        //        {
        //            existRenkeiMeisaiId = true;
        //            sb.Append(", '" + urshDetails[i].DETAIL_SYSTEM_ID + "'");
        //        }
        //    }

        //    if (!existRenkeiMeisaiId)
        //    {
        //        return null;    // 明細IDが無いと一意に識別できないためreturn
        //    }

        //    whereStr = whereStr + sb.ToString() + ")";

        //    var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
        //    using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.ManifestEntry.RenkeiDataCondition.sql"))
        //    {
        //        using (var sqlStr = new StreamReader(resourceStream))
        //        {
        //            return this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

        //        }
        //    }
        //}

        ///// <summary>
        ///// 消費税率を取得
        ///// </summary>
        ///// <param name="denpyouHiduke">適用期間条件</param>
        ///// <returns></returns>
        //public M_SHOUHIZEI GetShouhizeiRate(DateTime denpyouHiduke)
        //{
        //    M_SHOUHIZEI returnEntity = null;

        //    if (denpyouHiduke == null)
        //    {
        //        return returnEntity;
        //    }

        //    // SQL文作成(冗長にならないためsqlファイルで管理しない)
        //    DataTable dt = new DataTable();
        //    string selectStr = "SELECT * FROM M_SHOUHIZEI";
        //    string whereStr = " WHERE DELETE_FLG = 0";

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(" AND");
        //    sb.Append(" (");
        //    sb.Append("  (");
        //    sb.Append("  TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) <= TEKIYOU_END");
        //    sb.Append("  )");
        //    sb.Append(" )");

        //    whereStr = whereStr + sb.ToString();

        //    dt = this.gyoushaDao.GetDateForStringSql(selectStr + whereStr);

        //    if (dt == null || dt.Rows.Count < 1)
        //    {
        //        return returnEntity;
        //    }

        //    var dataBinderUtil = new DataBinderUtility<M_SHOUHIZEI>();

        //    var shouhizeis = dataBinderUtil.CreateEntityForDataTable(dt);
        //    returnEntity = shouhizeis[0];

        //    return returnEntity;
        //}

        /// <summary>
        /// 社員取得
        /// </summary>
        /// <param name="shainCd"></param>
        /// <returns></returns>
        public M_SHAIN GetShain(string shainCd)
        {
            if (string.IsNullOrEmpty(shainCd))
            {
                return null;
            }

            return this.shainDao.GetDataByCd(shainCd);
        }

        /// <summary>
        /// 品名取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_UNCHIN_HINMEI GetHinmei(string hinmeiCd)
        {
            if (string.IsNullOrEmpty(hinmeiCd))
            {
                return null;
            }

            M_UNCHIN_HINMEI keyEntity = new M_UNCHIN_HINMEI();
            keyEntity.UNCHIN_HINMEI_CD = hinmeiCd;
            var hinmei = this.hinmeiDao.GetAllValidData(keyEntity);

            if (hinmei == null || hinmei.Length < 1)
            {
                return null;
            }
            else
            {
                return hinmei[0];
            }
        }

        /// <summary>
        /// 品名単価取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_UNCHIN_TANKA GetHinmeiTanka(string gyoushaCd, string hinmeiCd, SqlInt16 unitCd, string shashuCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(hinmeiCd) || unitCd.IsNull)
            {
                return null;
            }

            M_UNCHIN_TANKA keyEntity = new M_UNCHIN_TANKA();
            keyEntity.UNPAN_GYOUSHA_CD = gyoushaCd;
            keyEntity.UNCHIN_HINMEI_CD = hinmeiCd;
            keyEntity.UNIT_CD = unitCd;
            keyEntity.SHASHU_CD = shashuCd;
            var hinmei = this.hinmeiTankaDao.GetAllValidData(keyEntity);

            if (string.IsNullOrEmpty(shashuCd))
            {
                hinmei = (from temp in hinmei
                          where string.IsNullOrEmpty(temp.SHASHU_CD)
                          select temp).ToArray();
            }
            if (hinmei == null || hinmei.Length < 1)
            {
                return null;
            }
            else
            {
                return hinmei[0];
            }
        }
        /// <summary>
        /// 単位名取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_DENSHU_KBN GetdenshuKbn(SqlInt16 Cd)
        {
            if (Cd < 0)
            {
                return null;
            }

            M_DENSHU_KBN keyEntity = new M_DENSHU_KBN();
            keyEntity.DENSHU_KBN_CD = Cd;
            var denshuKbn = this.denshuKbnDao.GetAllValidData(keyEntity);

            if (denshuKbn == null || denshuKbn.Length < 1)
            {
                return null;
            }
            else
            {
                return denshuKbn[0];
            }
        }
        /// <summary>
        /// 単位名取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_DENPYOU_KBN GetdenpyouKbn(SqlInt16 denpyouKbnCd)
        {
            if (denpyouKbnCd < 0)
            {
                return null;
            }

            M_DENPYOU_KBN keyEntity = new M_DENPYOU_KBN();
            keyEntity.DENPYOU_KBN_CD = denpyouKbnCd;
            var denpyouKbn = this.denpyouKbnDao.GetAllValidData(keyEntity);

            if (denpyouKbn == null || denpyouKbn.Length < 1)
            {
                return null;
            }
            else
            {
                return denpyouKbn[0];
            }
        }
        /// <summary>
        /// 単位名取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_UNIT GetUnit(SqlInt16 unitCd)
        {
            if (unitCd < 0)
            {
                return null;
            }

            M_UNIT keyEntity = new M_UNIT();
            keyEntity.UNIT_CD = unitCd;
            var unit = this.unitDao.GetAllValidData(keyEntity);

            if (unit == null || unit.Length < 1)
            {
                return null;
            }
            else
            {
                return unit[0];
            }
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return null;
            }

            return this.gyoushaDao.GetDataByCd(gyoushaCd);
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenba(string genbaCd)
        {
            if (string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GENBA_CD = genbaCd;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            return genba;
        }

        /// <summary>
        /// 消費税率を取得
        /// </summary>
        /// <param name="denpyouHiduke">適用期間条件</param>
        /// <returns></returns>
        public M_SHOUHIZEI GetShouhizeiRate(DateTime denpyouHiduke)
        {
            M_SHOUHIZEI returnEntity = null;

            if (denpyouHiduke == null)
            {
                return returnEntity;
            }

            // SQL文作成(冗長にならないためsqlファイルで管理しない)
            DataTable dt = new DataTable();
            string selectStr = "SELECT top 1 * FROM M_SHOUHIZEI";
            string whereStr = " WHERE DELETE_FLG = 0";

            StringBuilder sb = new StringBuilder();
            //sb.Append(" AND");
            //sb.Append(" (");
            //sb.Append("  (");
            //sb.Append("  TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) <= TEKIYOU_END");
            //sb.Append("  )");
            //sb.Append(" )");

            sb.Append(" AND");
            sb.Append(" (");
            sb.Append("  (");
            sb.Append("  ((TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + (DateTime)denpyouHiduke + "', 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, '" + (DateTime)denpyouHiduke + "', 111), 120) <= TEKIYOU_END) ");
            sb.Append("  or (TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + (DateTime)denpyouHiduke + "', 111), 120) and TEKIYOU_END IS NULL) ");
            sb.Append("  or (TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, '" + (DateTime)denpyouHiduke + "', 111), 120) <= TEKIYOU_END) or (TEKIYOU_BEGIN IS NULL and TEKIYOU_END IS NULL))");
            sb.Append("  )");
            sb.Append(" )");


            whereStr = whereStr + sb.ToString();

            dt = this.gyoushaDao.GetDateForStringSql(selectStr + whereStr);

            if (dt == null || dt.Rows.Count < 1)
            {
                return returnEntity;
            }

            var dataBinderUtil = new DataBinderUtility<M_SHOUHIZEI>();

            var shouhizeis = dataBinderUtil.CreateEntityForDataTable(dt);
            returnEntity = shouhizeis[0];

            return returnEntity;
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            return this.genbaDao.GetDataByCd(keyEntity);
        }

        /// <summary>
        /// 容器取得
        /// </summary>
        /// <param name="youkiCd"></param>
        /// <returns></returns>
        public M_YOUKI GetYouki(string youkiCd)
        {
            if (string.IsNullOrEmpty(youkiCd))
            {
                return null;
            }

            M_YOUKI keyEntity = new M_YOUKI();
            keyEntity.YOUKI_CD = youkiCd;
            var youki = this.youkiDao.GetAllValidData(keyEntity);
            if (youki == null || youki.Length < 1)
            {
                return null;
            }

            return youki[0];
        }

        /// <summary>
        /// 取引先取得
        /// </summary>
        /// <param name="tirihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd)
        {
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return null;
            }

            M_TORIHIKISAKI keyEntity = new M_TORIHIKISAKI();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity);
            if (torihikisaki == null || torihikisaki.Length < 1)
            {
                return null;
            }

            return torihikisaki[0];
        }

        /// <summary>
        /// 取引先_請求情報取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuu(string torihikisakiCd)
        {
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return null;
            }

            M_TORIHIKISAKI_SEIKYUU keyEntity = new M_TORIHIKISAKI_SEIKYUU();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            var torihikisakiSeikyuu = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
            if (torihikisakiSeikyuu == null)
            {
                return null;
            }

            return torihikisakiSeikyuu;
        }

        /// <summary>
        /// 取引先_支払情報取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI_SHIHARAI GetTorihikisakiShiharai(string torihikisakiCd)
        {
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return null;
            }

            M_TORIHIKISAKI_SHIHARAI keyEntity = new M_TORIHIKISAKI_SHIHARAI();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            var torihikisakiShiharai = this.torihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
            if (torihikisakiShiharai == null)
            {
                return null;
            }

            return torihikisakiShiharai;
        }

        /// <summary>
        /// マニフェスト種類取得
        /// </summary>
        /// <param name="manifestShuruiCd"></param>
        /// <returns></returns>
        public M_MANIFEST_SHURUI GetManifestShurui(SqlInt16 manifestShuruiCd)
        {
            if (manifestShuruiCd.IsNull)
            {
                return null;
            }

            M_MANIFEST_SHURUI keyEntity = new M_MANIFEST_SHURUI();
            keyEntity.MANIFEST_SHURUI_CD = manifestShuruiCd;
            var manifestShuruiList = this.manifestShuruiDao.GetAllValidData(keyEntity);
            if (manifestShuruiList == null || manifestShuruiList.Length < 1)
            {
                return null;
            }

            // PK指定のためデータ1件
            return manifestShuruiList[0];
        }

        /// <summary>
        /// マニフェスト手配取得
        /// </summary>
        /// <param name="manifestTehaiCd"></param>
        /// <returns></returns>
        public M_MANIFEST_TEHAI GetManifestTehai(SqlInt16 manifestTehaiCd)
        {
            if (manifestTehaiCd.IsNull)
            {
                return null;
            }

            M_MANIFEST_TEHAI keyEntity = new M_MANIFEST_TEHAI();
            keyEntity.MANIFEST_TEHAI_CD = manifestTehaiCd;
            var manifestTehaiList = this.manifestTehaiDao.GetAllValidData(keyEntity);
            if (manifestTehaiList == null || manifestTehaiList.Length < 1)
            {
                return null;
            }

            // PK指定のためにデータ1件
            return manifestTehaiList[0];
        }

        ///// <summary>
        ///// 車輌取得
        ///// </summary>
        ///// <param name="sharyouCd"></param>
        ///// <returns></returns>
        //public M_SHARYOU[] GetSharyou(string sharyouCd)
        //{
        //    if (string.IsNullOrEmpty(sharyouCd))
        //    {
        //        return null;
        //    }

        //    M_SHARYOU keyEntity = new M_SHARYOU();
        //    keyEntity.SHARYOU_CD = sharyouCd;
        //    var sharyous = this.sharyouDao.GetAllValidData(keyEntity);
        //    if (sharyous == null || sharyous.Length < 1)
        //    {
        //        return null;
        //    }

        //    return sharyous;
        //}

        /// <summary>
        /// 車輌取得
        /// </summary>
        /// <param name="sharyouCd"></param>
        /// <returns></returns>
        public M_SHARYOU[] GetSharyou(string sharyouCd, string gyoushaCd, string shasyuCd, string shainCd, SqlDateTime tekiyouDate)
        {
            if (string.IsNullOrEmpty(sharyouCd))
            {
                return null;
            }

            M_SHARYOU keyEntity = new M_SHARYOU();
            keyEntity.SHARYOU_CD = sharyouCd;
            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                keyEntity.GYOUSHA_CD = gyoushaCd;
            }
            if (!string.IsNullOrEmpty(shasyuCd))
            {
                keyEntity.SHASYU_CD = shasyuCd;
            }
            if (!string.IsNullOrEmpty(shainCd))
            {
                keyEntity.SHAIN_CD = shainCd;
            }

            var sharyous = this.sharyouDao.GetAllValidDataForGyoushaKbn(keyEntity, "9", tekiyouDate, true, false, false);
            if (sharyous == null || sharyous.Length < 1)
            {
                return null;
            }

            return sharyous;
        }
        /// <summary>
        /// 単位区分取得
        /// </summary>
        /// <param name="unitCd">単位区分CD</param>
        /// <returns></returns>
        public M_UNIT[] GetUnit(short unitCd)
        {
            if (unitCd < 0)
            {
                return null;
            }

            M_UNIT keyEntity = new M_UNIT();
            keyEntity.UNIT_CD = unitCd;
            var units = this.unitDao.GetAllValidData(keyEntity);
            if (units == null || units.Length < 1)
            {
                return null;
            }

            return units;
        }

        public M_KOBETSU_HINMEI_TANKA GetKobetsuhinmeiTanka(short denpyouKBNCd, string torihikisakiCd, string gyoushaCd,
            string genbaCd, string unpanGyoushaCd, string nioroshiGyoushaCd, string nioroshiGenbaCd, string hinmeiCd, short unitCd)
        {
            M_KOBETSU_HINMEI_TANKA keyEntity = new M_KOBETSU_HINMEI_TANKA();
            M_KOBETSU_HINMEI_TANKA[] kobetsuHinmeiTankas = null;

            // 伝票区分CDは動的に変更されないため最初に設定
            keyEntity.DENPYOU_KBN_CD = denpyouKBNCd;

            // すべて値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者, 現場, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && !string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 6; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者＞現場＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 4:
                            keyEntity.GENBA_CD = null;
                            break;

                        case 5:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸業者, 運搬業者, 現場, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && !string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 5; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞運搬業者＞現場＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.GENBA_CD = null;
                            break;

                        case 4:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 運搬業者, 現場, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && !string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 4; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 運搬業者＞現場＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.GENBA_CD = null;
                            break;

                        case 3:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者, 現場, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && !string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 5; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞現場＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.GENBA_CD = null;
                            break;

                        case 4:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者, 現場, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && !string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 4; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞現場＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.GENBA_CD = null;
                            break;

                        case 3:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 5; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 4:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸業者, 運搬業者, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 4; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞運搬業者＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 4:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 運搬業者, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 3; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 運搬業者＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 4; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸業者, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 3; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 4; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸業者, 運搬業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 3; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 運搬業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 2; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷降現場，荷卸業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 3; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 2; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                {
                    return kobetsuHinmeiTankas[0];
                }

            }

            // 何も取得できなかったとき
            return null;
        }

        /// <summary>
        /// 基本品名単価を検索する
        /// </summary>
        /// <param name="denpyouKBNCd">伝種区分CD</param>
        /// <param name="unpanGyoushaCd">運搬業者CD</param>
        /// <param name="nioroshiGyoushaCd">荷降業者CD</param>
        /// <param name="nioroshiGenbaCd">荷降現場CD</param>
        /// <param name="hinmeiCd">品名CD</param>
        /// <param name="unitCd">単位CD</param>
        /// <returns></returns>
        public M_KIHON_HINMEI_TANKA GetKihonHinmeitanka(short denpyouKBNCd, string unpanGyoushaCd,
            string nioroshiGyoushaCd, string nioroshiGenbaCd, string hinmeiCd, short unitCd)
        {
            M_KIHON_HINMEI_TANKA keyEntity = new M_KIHON_HINMEI_TANKA();
            M_KIHON_HINMEI_TANKA[] kihonHinmeiTankas = null;

            // すべて値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd))
            {
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;

                for (int i = 0; i < 4; i++)
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    {
                        return kihonHinmeiTankas[0];
                    }
                }
            }

            // 値あり：品名, 単位, 荷卸業者, 運搬業者
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd))
            {
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;

                for (int i = 0; i < 3; i++)
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    {
                        return kihonHinmeiTankas[0];
                    }
                }
            }

            // 値あり：品名, 単位, 運搬業者
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd))
            {
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;

                for (int i = 0; i < 2; i++)
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    {
                        return kihonHinmeiTankas[0];
                    }
                }
            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd))
            {
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;

                for (int i = 0; i < 3; i++)
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    {
                        return kihonHinmeiTankas[0];
                    }
                }
            }

            // 値あり：品名, 単位, 荷卸業者
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd))
            {
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;

                for (int i = 0; i < 2; i++)
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    {
                        return kihonHinmeiTankas[0];
                    }
                }
            }

            // 何も取得できなかった場合
            return null;
        }

        /// <summary>
        /// コンテナ状況取得
        /// </summary>
        /// <param name="contenaJoukyouCd"></param>
        /// <returns></returns>
        public M_CONTENA_JOUKYOU GetContenaJoukyou(short contenaJoukyouCd)
        {
            if (contenaJoukyouCd < 0)
            {
                return null;
            }

            M_CONTENA_JOUKYOU keyEntity = new M_CONTENA_JOUKYOU();
            keyEntity.CONTENA_JOUKYOU_CD = contenaJoukyouCd;
            var result = this.contenaJoukyouDao.GetAllValidData(keyEntity);
            if (result == null || result.Length < 1)
            {
                return null;
            }

            return result[0];
        }

        /// <summary>
        /// 伝票区分一覧取得
        /// </summary>
        /// <returns></returns>
        public M_DENPYOU_KBN[] GetAllDenpyouKbn()
        {
            M_DENPYOU_KBN keyEntity = new M_DENPYOU_KBN();
            return this.denpyouKbnDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 容器一覧取得
        /// </summary>
        /// <returns></returns>
        public M_YOUKI[] GetAllYouki()
        {
            M_YOUKI keyEntity = new M_YOUKI();
            return this.youkiDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 単位一覧取得
        /// </summary>
        /// <returns></returns>
        public M_UNIT[] GetAllUnit()
        {
            M_UNIT keyEntity = new M_UNIT();
            return this.unitDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 形態区分取得
        /// </summary>
        /// <param name="keitaiCbnCd"></param>
        /// <returns></returns>
        public M_KEITAI_KBN GetkeitaiKbn(short keitaiCbnCd)
        {
            if (keitaiCbnCd < 0)
            {
                return null;
            }

            return this.keitaiKbnDao.GetDataByCd(keitaiCbnCd.ToString());
        }

        ///// <summary>
        ///// 請求明細取得(受入入力用)
        ///// </summary>
        ///// <param name="systemId">システムID</param>
        ///// <param name="seq">枝番</param>
        ///// <param name="detailSystemId">明細システムID</param>
        ///// <param name="torihikisakiCd">取引先CD</param>
        ///// <returns></returns>
        //public DataTable GetSeikyuMeisaiData(long systemId, int seq, long detailSystemId, string torihikisakiCd)
        //{
        //    T_SEIKYUU_DETAIL keyEntity = new T_SEIKYUU_DETAIL();
        //    // 伝種区分：受入
        //    keyEntity.DENPYOU_SHURUI_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
        //    keyEntity.DENPYOU_SYSTEM_ID = systemId;
        //    keyEntity.DENPYOU_SEQ = seq;
        //    if (0 <= detailSystemId)
        //    {
        //        keyEntity.DETAIL_SYSTEM_ID = detailSystemId;
        //    }
        //    keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

        //    return this.seikyuuDetail.GetDataForEntity(keyEntity);
        //}

        ///// <summary>
        ///// 清算明細取得(受入入力用)
        ///// </summary>
        ///// <param name="systemId">システムID</param>
        ///// <param name="seq">枝番</param>
        ///// <param name="detailSystemId">明細システムID</param>
        ///// <param name="torihikisakiCd">取引先CD</param>
        ///// <returns></returns>
        //public DataTable GetSeisanMeisaiData(long systemId, int seq, long detailSystemId, string torihikisakiCd)
        //{
        //    T_SEISAN_DETAIL keyEntity = new T_SEISAN_DETAIL();
        //    // 伝種区分：受入
        //    keyEntity.DENPYOU_SHURUI_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
        //    keyEntity.DENPYOU_SYSTEM_ID = systemId;
        //    keyEntity.DENPYOU_SEQ = seq;
        //    if (0 <= detailSystemId)
        //    {
        //        keyEntity.DETAIL_SYSTEM_ID = detailSystemId;
        //    }
        //    keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

        //    return this.seisanDetail.GetDataForEntity(keyEntity);
        //}
        ///// <summary>
        ///// 領収書管理取得
        ///// </summary>
        ///// <param name="hiduke">日付</param>
        ///// <param name="densyuKbnCd">伝種区分</param>
        ///// <param name="kyotenCd">拠点CD</param>
        ///// <returns></returns>
        //public S_NUMBER_RECEIPT GetNumberReceipt(DateTime hiduke, short densyuKbnCd, short kyotenCd)
        //{
        //    S_NUMBER_RECEIPT keyEntity = new S_NUMBER_RECEIPT();
        //    if (hiduke == null)
        //    {
        //        return null;
        //    }
        //    keyEntity.NUMBERED_DAY = hiduke.Date;
        //    keyEntity.DENSHU_KBN_CD = (SqlInt16)densyuKbnCd;
        //    keyEntity.KYOTEN_CD = (SqlInt16)kyotenCd;

        //    var numberReceips = this.numberReceiptDao.GetDataForEntity(keyEntity);
        //    if (numberReceips == null
        //        || numberReceips.Length < 1)
        //    {
        //        return null;
        //    }

        //    return numberReceips[0];
 
        //}

        ///// <summary>
        ///// 領収書管理更新
        ///// </summary>
        ///// <param name="taergetEntity"></param>
        //public void InsertNumberReceipt(S_NUMBER_RECEIPT targetEntity)
        //{
        //    // キーチェック
        //    if (targetEntity == null
        //        || targetEntity.NUMBERED_DAY.IsNull
        //        || targetEntity.DENSHU_KBN_CD.IsNull
        //        || targetEntity.KYOTEN_CD.IsNull)
        //    {
        //        return;
        //    }
        //    this.numberReceiptDao.Insert(targetEntity);
        //}

        ///// <summary>
        ///// 領収書管理更新
        ///// </summary>
        ///// <param name="targetEntity"></param>
        //public void UpdateNumberReceipt(S_NUMBER_RECEIPT targetEntity)
        //{
        //    // キーチェック
        //    if (targetEntity == null
        //        || targetEntity.NUMBERED_DAY.IsNull
        //        || targetEntity.DENSHU_KBN_CD.IsNull
        //        || targetEntity.KYOTEN_CD.IsNull)
        //    {
        //        return;
        //    }
        //    this.numberReceiptDao.Update(targetEntity);
        //}

        /// <summary>
        /// 車種情報取得
        /// </summary>
        /// <param name="targetEntity"></param>
        public M_SHASHU GetShashu(string strShashuCd)
        {
            if (string.IsNullOrEmpty(strShashuCd))
            {
                return null;
            }

            return this.shashuDao.GetDataByCd(strShashuCd);
        }

        ///// <summary>
        ///// コンテナ情報取得
        ///// </summary>
        ///// <param name="sysId"></param>
        //internal T_CONTENA_RESULT[] GetContena(string sysId)
        //{
        //    //キーチェック
        //    if (sysId == null)
        //    {
        //        return null;
        //    }

        //    return this.tcrDao.GetContena(sysId);
        //}

        ///// <summary>
        ///// 取引先区分（請求情報）取得
        ///// </summary>
        ///// <param name="torihikisakiCD"></param>
        //internal string GetTrihikisakiKBN_Seikyuu(string torihikisakiCD)
        //{
        //    //キーチェック
        //    if (torihikisakiCD == null)
        //    {
        //        return null;
        //    }

        //    return this.mtSeiDao.GetTorihikisakiKBN_Seikyuu(torihikisakiCD);
        //}

        ///// <summary>
        ///// 取引先区分（支払情報）取得
        ///// </summary>
        ///// <param name="torihikisakiCD"></param>
        //internal string GetTrihikisakiKBN_Shiharai(string torihikisakiCD)
        //{
        //    //キーチェック
        //    if (torihikisakiCD == null)
        //    {
        //        return null;
        //    }

        //    return this.mtShihaDao.GetTorihikisakiKBN_Shiharai(torihikisakiCD);
        //}

        ///// <summary>
        ///// 受付（収集）テーブル存在チェック
        ///// </summary>
        ///// <param name="uketsukeNum"></param>
        ///// <returns></returns>
        //internal Boolean UketsukeSSCheck(string uketsukeNum)
        //{
        //    if (uketsukeNum == null || uketsukeNum == "")
        //    {
        //        return false;
        //    }
        //    T_UKETSUKE_SS_ENTRY keyEntity = new T_UKETSUKE_SS_ENTRY();
        //    keyEntity.UKETSUKE_NUMBER = SqlInt64.Parse(uketsukeNum);
        //    if (this.tuSSeDao.GetDataForEntity(keyEntity) == null)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 受付（出荷）テーブル存在チェック
        ///// </summary>
        ///// <param name="uketsukeNum"></param>
        ///// <returns></returns>
        //internal Boolean UketsukeSKCheck(string uketsukeNum)
        //{
        //    if (uketsukeNum == null || uketsukeNum == "")
        //    {
        //        return false;
        //    }
        //    T_UKETSUKE_SK_ENTRY keyEntity = new T_UKETSUKE_SK_ENTRY();
        //    keyEntity.UKETSUKE_NUMBER = SqlInt64.Parse(uketsukeNum);
        //    if (this.tuSSeDao.GetDataForEntity(keyEntity) == null)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        #endregion

        #region Utility
        #endregion
    }
}
