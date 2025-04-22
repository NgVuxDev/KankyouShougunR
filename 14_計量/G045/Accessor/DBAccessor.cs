using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using r_framework.Utility;
using System.Data.SqlTypes;
using Shougun.Core.Scale.Keiryou.Const;
using r_framework.Logic;
using r_framework.Const;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.IO;
using Shougun.Core.Scale.Keiryou.Utility;

namespace Shougun.Core.Scale.Keiryou.Accessor
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
        /// IM_HINMEIDao
        /// </summary>
        r_framework.Dao.IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// IS_NUMBER_SYSTEMDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// IS_NUMBER_DENSHUDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// IT_UKETSUKE_SS_DETAILDao
        /// </summary>
        Shougun.Core.Scale.Keiryou.Dao.IT_UKETSUKE_SS_DETAILDao uketukeDetailDao;

        /// <summary>
        /// IT_UKETSUKE_ENTRYDao
        /// </summary>
        public Shougun.Core.Scale.Keiryou.Dao.IT_UKETSUKE_SS_ENTRYDao uketukeEntryDao;

        /// <summary>
        /// IT_KEIRYOU_ENTRYDao
        /// </summary>
        public Shougun.Core.Scale.Keiryou.Dao.IT_KEIRYOU_ENTRYDao keiryouEntryDao;

        /// <summary>
        /// IT_KEIRYOU_DETAILDao
        /// </summary>
        Shougun.Core.Scale.Keiryou.Dao.IT_KEIRYOU_DETAILDao keiryouDetailDao;

        /// <summary>
        /// IS_NUMBER_YEARDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao numberYearDao;

        /// <summary>
        /// IS_NUMBER_DAYDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao numberDayDao;

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
        /// IT_MANIFEST_ENTRYDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_MANIFEST_ENTRYDao manifestEntryDao;


        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_RECEIPTDao numberReceiptDao;

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
        /// IM_DENPYOU_KBNDao
        /// </summary>
        r_framework.Dao.IM_DENPYOU_KBNDao denpyouKbnDao;

        /// <summary>
        /// IM_KEITAI_KBNDao
        /// </summary>
        r_framework.Dao.IM_KEITAI_KBNDao keitaiKbnDao;

        /// <summary>
        /// M_NISUGATADao
        /// </summary>
        r_framework.Dao.IM_NISUGATADao nisugataDao;

        /// <summary>
        /// M_CORP_INFODao
        /// </summary>
        r_framework.Dao.IM_CORP_INFODao corpInfoDao;

        /// <summary>
        /// IM_SHASHUDao
        /// </summary>
        r_framework.Dao.IM_SHASHUDao shashuDao;

        /// <summary>
        /// IT_UKETSUKE_SK_ENTRYDao
        /// </summary>
        Shougun.Core.Scale.Keiryou.Dao.IT_UKETSUKE_SK_ENTRYDao uketukeSKEntryDao;

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        r_framework.Dao.IM_KOBETSU_HINMEIDao kobetsuHinmeiDao;
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // スタートアッププロジェクトのDiconに情報が設定されていることを必ず確認
            this.sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_HINMEIDao>();
            this.keiryouEntryDao = DaoInitUtility.GetComponent<Shougun.Core.Scale.Keiryou.Dao.IT_KEIRYOU_ENTRYDao>();
            this.keiryouDetailDao = DaoInitUtility.GetComponent<Shougun.Core.Scale.Keiryou.Dao.IT_KEIRYOU_DETAILDao>();
            this.uketukeEntryDao = DaoInitUtility.GetComponent<Shougun.Core.Scale.Keiryou.Dao.IT_UKETSUKE_SS_ENTRYDao>();
            this.uketukeDetailDao = DaoInitUtility.GetComponent<Shougun.Core.Scale.Keiryou.Dao.IT_UKETSUKE_SS_DETAILDao>();
            this.numberYearDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao>();
            this.numberDayDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.manifestEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_MANIFEST_ENTRYDao>();
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
            this.keitaiKbnDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KEITAI_KBNDao>();
            this.numberReceiptDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_RECEIPTDao>();
            this.nisugataDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_NISUGATADao>();
            this.corpInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CORP_INFODao>();
            this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();

            this.uketukeSKEntryDao = DaoInitUtility.GetComponent<Shougun.Core.Scale.Keiryou.Dao.IT_UKETSUKE_SK_ENTRYDao>();
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.kobetsuHinmeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEIDao>();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
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

        /// 計量入力を取得
        /// </summary>
        /// <param name="keiryouNumber">計量番号</param>
        /// <returns></returns>
        public T_KEIRYOU_ENTRY[] GetKeiryouEntry(SqlInt64 keiryouNumber, string SEQ)
        {
            T_KEIRYOU_ENTRY entity = new T_KEIRYOU_ENTRY();
            entity.KEIRYOU_NUMBER = keiryouNumber;
            entity.SEQ = SqlInt32.Parse(SEQ);
            return this.keiryouEntryDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 計量入力の登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertKeiryouEntry(T_KEIRYOU_ENTRY entity)
        {
            return this.keiryouEntryDao.Insert(entity);
        }

        /// <summary>
        /// 計量入力の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateKeiryouEntry(T_KEIRYOU_ENTRY entity)
        {
            return this.keiryouEntryDao.Update(entity);
        }

        /// 受付入力を取得
        /// </summary>
        /// <param name="uketukeNumber">受付番号</param>
        /// <returns></returns>
        public T_UKETSUKE_SS_ENTRY[] GetUketukeEntry(SqlInt64 uketukeNumber)
        {
            T_UKETSUKE_SS_ENTRY entity = new T_UKETSUKE_SS_ENTRY();
            entity.UKETSUKE_NUMBER = uketukeNumber;
            return this.uketukeEntryDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 受付明細を取得
        /// </summary>
        /// <param name="uketukeNumber">受付番号</param>
        /// <returns></returns>
        public T_UKETSUKE_SS_DETAIL[] GetUketsukeDetail(SqlInt64 uketukeNumber)
        {
            T_UKETSUKE_SS_DETAIL entity = new T_UKETSUKE_SS_DETAIL();
            entity.UKETSUKE_NUMBER = uketukeNumber;
            return uketukeDetailDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 受付明細を取得(出荷)
        /// </summary>
        /// <param name="uketukeNumber">受付番号</param>
        /// <returns></returns>
        public T_UKETSUKE_SK_ENTRY[] GetUketsukeEntry(SqlInt64 uketukeNumber)
        {
            long number = 0;
            
            long.TryParse(uketukeNumber.ToString(), out number);
            return uketukeSKEntryDao.GetDataByCd(number);
        }

        /// <summary>
        /// 計量明細を取得
        /// </summary>
        /// <param name="entrySysId"></param>
        /// <param name="entrySeq"></param>
        /// <returns></returns>
        public T_KEIRYOU_DETAIL[] GetKeiryouDetail(SqlInt64 entrySysId, SqlInt32 entrySeq)
        {
            T_KEIRYOU_DETAIL entity = new T_KEIRYOU_DETAIL();
            entity.SYSTEM_ID = entrySysId;
            entity.SEQ = entrySeq;
            return keiryouDetailDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 計量明細を登録
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int InsertKeiryouDetail(T_KEIRYOU_DETAIL[] entitys)
        {
            int returnint = 0;

            foreach (var ukeiryouDetail in entitys)
            {
                returnint = returnint + this.keiryouDetailDao.Insert(ukeiryouDetail);
            }

            return returnint;
        }

        /// <summary>
        /// 計量明細を更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int UpdateKeiryouDetail(T_KEIRYOU_DETAIL[] entitys)
        {
            int returnint = 0;

            foreach (var ukeiryouDetail in entitys)
            {
                returnint = returnint + this.keiryouDetailDao.Update(ukeiryouDetail);
            }

            return returnint;
        }

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
            entity.NUMBERED_DAY = numberedDay.Date;
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

        /// <summary>
        /// 計量入力用のSYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemIdForKeiryou()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = (short)DENSHU_KBN.KEIRYOU;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = (short)DENSHU_KBN.KEIRYOU;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else 
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }

        public SqlInt64 createKeiryouNumber()
        {
            SqlInt64 returnInt = -1;

            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = (short)DENSHU_KBN.KEIRYOU;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = (short)DENSHU_KBN.KEIRYOU;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberDenshuDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberDenshuDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 品名テーブルの情報を取得
        /// 適用開始日、終了日、削除フラグについては
        /// 有効なものだけを検索します
        /// </summary>
        /// <param name="key">品名CD</param>
        /// <returns></returns>
        public M_HINMEI[] GetAllValidHinmeiData(string key = null)
        {
            M_HINMEI keyEntity = new M_HINMEI();
            if (!string.IsNullOrEmpty(key))
            {
                keyEntity.HINMEI_CD = key;
            }

            return this.hinmeiDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 品名テーブルの情報を取得
        /// </summary>
        /// <param name="key">品名CD</param>
        /// <returns></returns>
        public M_HINMEI GetHinmeiDataByCd(string key)
        {
            M_HINMEI keyEntity = new M_HINMEI();
            if (!string.IsNullOrEmpty(key))
            {
                keyEntity.HINMEI_CD = key;
            }

            M_HINMEI returnValue = new M_HINMEI();
            M_HINMEI[] hinmeis = this.hinmeiDao.GetAllValidData(keyEntity);

            if (hinmeis != null && hinmeis.Length > 0)
            {
                returnValue = hinmeis[0];
            }

            return returnValue;

        }

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        /// <summary>
        /// 品名テーブルの情報を取得
        /// </summary>
        /// <param name="key">品名CD</param>
        /// <returns></returns>
        public M_KOBETSU_HINMEI GetKobetsuHinmeiDataByCd(M_KOBETSU_HINMEI keyEntity)
        {
            M_KOBETSU_HINMEI returnValue = new M_KOBETSU_HINMEI();
            M_KOBETSU_HINMEI[] hinmeis = this.kobetsuHinmeiDao.GetAllValidData(keyEntity);

            if (hinmeis != null && hinmeis.Length > 0)
            {
                returnValue = hinmeis[0];
            }

            return returnValue;

        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        /// <summary>
        /// 拠点を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="kyotenCd">KYOTEN_CD</param>
        /// <returns></returns>
        public M_KYOTEN[] GetDataByCodeForKyoten(short kyotenCd)
        {
            M_KYOTEN keyEntity = new M_KYOTEN();
            keyEntity.KYOTEN_CD = kyotenCd;
            return this.kyotenDao.GetAllValidData(keyEntity);
        }

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

            M_SHASHU keyEntity = new M_SHASHU();
            keyEntity.SHASHU_CD = strShashuCd;
            var shashu = this.shashuDao.GetAllValidData(keyEntity);

            if (shashu == null || shashu.Length < 1)
            {
                return null;
            }
            else
            {
                return shashu[0];
            }
        }
        /// <summary>
        /// 荷降業者を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="gyoushaCd">GYOSHA_CD</param>
        /// <returns></returns>
        public M_GYOUSHA[] GetNisumiGyousya(string gyoushaCd)
        {

            M_GYOUSHA[] gyoushas = null;

            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return gyoushas;
            }

            // SQL文作成
            DataTable dt = new DataTable();
            string whereStr = " AND GYOUSHA_CD = '" + gyoushaCd + "'";

            var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
            using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.Gyousha.NizumiGyoushaCondition.sql"))
            {
                using (var sqlStr = new StreamReader(resourceStream))
                {
                    dt = this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

                }
            }

            if (dt == null || dt.Rows.Count < 1)
            {
                return gyoushas;
            }

            var dataBinderUtil = new DataBinderUtility<M_GYOUSHA>();

            gyoushas = dataBinderUtil.CreateEntityForDataTable(dt);

            return gyoushas;
        }

        /// <summary>
        /// 荷降現場を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="genbaCd">GYOUSHA_CD</param>
        /// <param name="gyoushaCd">GENBA_CD</param>
        /// <returns></returns>
        public M_GENBA[] GetNisumiGenba(string gyoushaCd, string genbaCd)
        {

            M_GENBA[] genbas = null;

            if (string.IsNullOrEmpty(genbaCd))
            {
                return genbas;
            }

            // SQL文作成
            DataTable dt = new DataTable();
            string whereStr = "AND (GYOUSHA_CD = '" + gyoushaCd + "' AND GENBA_CD = '" + genbaCd + "')";

            var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
            using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.ManifestEntry.RenkeiDataCondition.sql"))
            {
                using (var sqlStr = new StreamReader(resourceStream))
                {
                    dt = this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

                }
            }

            if (dt == null || dt.Rows.Count < 1)
            {
                return genbas;
            }

            var dataBinderUtil = new DataBinderUtility<M_GENBA>();

            genbas = dataBinderUtil.CreateEntityForDataTable(dt);

            return genbas;
        }

        /// <summary>
        /// 運搬業者を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="gyoushaCd">GYOSHA_CD</param>
        /// <returns></returns>
        public M_GYOUSHA[] GetUnpanGyousya(string gyoushaCd)
        {

            M_GYOUSHA[] gyoushas = null;

            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return gyoushas;
            }

            // SQL文作成
            DataTable dt = new DataTable();
            string whereStr = " AND GYOUSHA_CD = '" + gyoushaCd + "'";

            var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
            using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.Gyousha.UnpanGyoushaCondition.sql"))
            {
                using (var sqlStr = new StreamReader(resourceStream))
                {
                    dt = this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

                }
            }

            if (dt == null || dt.Rows.Count < 1)
            {
                return gyoushas;
            }

            var dataBinderUtil = new DataBinderUtility<M_GYOUSHA>();

            gyoushas = dataBinderUtil.CreateEntityForDataTable(dt);

            return gyoushas;
        }

        /// <summary>
        /// マニフェスト取得
        /// 取得後、KeiryouDetailのキーでさらに絞り込んで使用してもらうため、
        /// あえてDataTableで返しています。
        /// </summary>
        /// <param name="keiryouDetails">連携しているT_KEIRYOU_DETAIL</param>
        /// <returns>必要なキーが設定されていない場合はNullを返します。</returns>
        public DataTable GetManifestEntry(T_KEIRYOU_DETAIL[] keiryouDetails)
        {
            if (keiryouDetails == null || keiryouDetails.Length < 1)
            {
                return null;
            }

            if (keiryouDetails[0].SYSTEM_ID.IsNull)
            {
                // 念のため先頭のキー値チェック
                return null;
            }

            // SQL文作成
            string whereStr = " AND RENKEI_DENSHU_KBN_CD = '" + (short)DENSHU_KBN.KEIRYOU + "' AND RENKEI_SYSTEM_ID = '" + keiryouDetails[0].SYSTEM_ID + "' AND RENKEI_MEISAI_SYSTEM_ID IN ( ";

            // RENKEI_MEISAI_SYSTEM_ID用の条件句作成
            bool existRenkeiMeisaiId = false;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < keiryouDetails.Length; i++)
            {
                if (keiryouDetails[i].DETAIL_SYSTEM_ID.IsNull)
                {
                    continue;
                }

                if (i == 0)
                {
                    existRenkeiMeisaiId = true;
                    sb.Append("'" + keiryouDetails[i].DETAIL_SYSTEM_ID + "'");
                }
                else
                {
                    existRenkeiMeisaiId = true;
                    sb.Append(", '" + keiryouDetails[i].DETAIL_SYSTEM_ID + "'");
                }
            }

            if (!existRenkeiMeisaiId)
            {
                return null;    // 明細IDが無いと一意に識別できないためreturn
            }

            whereStr = whereStr + sb.ToString() + ")";

            var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
            using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.ManifestEntry.RenkeiDataCondition.sql"))
            {
                using (var sqlStr = new StreamReader(resourceStream))
                {
                    return this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

                }
            }
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
            string selectStr = "SELECT * FROM M_SHOUHIZEI";
            string whereStr = " WHERE DELETE_FLG = 0";

            StringBuilder sb = new StringBuilder();
            sb.Append(" AND");
            sb.Append(" (");
            sb.Append("  (");
            sb.Append("  TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) <= TEKIYOU_END");
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

            M_SHAIN keyEntity = new M_SHAIN();
            keyEntity.SHAIN_CD = shainCd;
            var shain = this.shainDao.GetAllValidData(keyEntity);

            if (shain == null || shain.Length < 1)
            {
                return null;
            }
            else
            {
                return shain[0];
            }
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd, object strDenpyouDate, object sysDate)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return null;
            }

            M_GYOUSHA keyEntity = new M_GYOUSHA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

            if (gyousha == null || gyousha.Length < 1)
            {
                return null;
            }
            if (null != gyousha && gyousha.Length > 0)
            {
                string strBegin = gyousha[0].TEKIYOU_BEGIN.ToString();
                string strEnd = gyousha[0].TEKIYOU_END.ToString();
                string sagyobi = string.Empty;
                if (strDenpyouDate != null)
                {
                    sagyobi = strDenpyouDate.ToString();
                }
                else
                {
                    sagyobi = sysDate.ToString();
                }

                if (gyousha[0].TEKIYOU_BEGIN.IsNull)
                {
                    strBegin = "0001/01/01 00:00:01";
                }

                if (gyousha[0].TEKIYOU_END.IsNull)
                {
                    strEnd = "9999/12/31 23:59:59";
                }

                if (!string.IsNullOrEmpty(sagyobi))
                {
                    //作業日は適用期間より範囲外の場合
                    if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                    {
                        return null;
                    }
                }
            }

            return gyousha[0];

        }


        /// <summary>
        /// 業者取得（削除済、適用期間外も含む）
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return null;
            }

            M_GYOUSHA keyEntity = new M_GYOUSHA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

            if (gyousha == null || gyousha.Length < 1)
            {
                return null;
            }
            else
            {
                return gyousha[0];
            }

        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenba(string genbaCd, object strDenpyouDate, object sysDate)
        {
            if (string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            List<M_GENBA> retlist = new List<M_GENBA>();
            M_GENBA keyEntity = new M_GENBA();

            keyEntity.GENBA_CD = genbaCd;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            for (int i = 0; i < genba.Length; i++)
            {
                string strBegin = genba[i].TEKIYOU_BEGIN.ToString();
                string strEnd = genba[i].TEKIYOU_END.ToString();
                string sagyobi = string.Empty;
                if (strDenpyouDate != null)
                {
                    sagyobi = strDenpyouDate.ToString();
                }
                else
                {
                    sagyobi = sysDate.ToString();
                }

                if (genba[i].TEKIYOU_BEGIN.IsNull)
                {
                    strBegin = "0001/01/01 00:00:01";
                }

                if (genba[i].TEKIYOU_END.IsNull)
                {
                    strEnd = "9999/12/31 23:59:59";
                }

                if (!string.IsNullOrEmpty(sagyobi))
                {
                    //作業日は適用期間より範囲外の場合
                    if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                    {
                        continue;
                    }
                    else
                    {
                        retlist.Add(genba[i]);
                    }
                }
            }

            return retlist.ToArray();
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd, object strDenpyouDate, object sysDate)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            if (null != genba && genba.Length > 0)
            {
                string strBegin = genba[0].TEKIYOU_BEGIN.ToString();
                string strEnd = genba[0].TEKIYOU_END.ToString();
                string sagyobi = string.Empty;
                if (strDenpyouDate != null)
                {
                    sagyobi = strDenpyouDate.ToString();
                }
                else
                {
                    sagyobi = sysDate.ToString();
                }

                if (genba[0].TEKIYOU_BEGIN.IsNull)
                {
                    strBegin = "0001/01/01 00:00:01";
                }

                if (genba[0].TEKIYOU_END.IsNull)
                {
                    strEnd = "9999/12/31 23:59:59";
                }

                if (!string.IsNullOrEmpty(sagyobi))
                {
                    //作業日は適用期間より範囲外の場合
                    if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                    {
                        return null;
                    }
                }
            }

            // PK指定のため1件
            return genba[0];
        }

        /// <summary>
        /// 現場取得（削除済、適用期間外も含む）
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
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            // PK指定のため1件
            return genba[0];
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
        public M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, object strDenpyouDate, object sysDate)
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

            if (null != torihikisaki && torihikisaki.Length > 0)
            {
                string strBegin = torihikisaki[0].TEKIYOU_BEGIN.ToString();
                string strEnd = torihikisaki[0].TEKIYOU_END.ToString();
                string sagyobi = string.Empty;
                if (strDenpyouDate != null)
                {
                    sagyobi = strDenpyouDate.ToString();
                }
                else
                {
                    sagyobi = sysDate.ToString();
                }

                if (torihikisaki[0].TEKIYOU_BEGIN.IsNull)
                {
                    strBegin = "0001/01/01 00:00:01";
                }

                if (torihikisaki[0].TEKIYOU_END.IsNull)
                {
                    strEnd = "9999/12/31 23:59:59";
                }

                if (!string.IsNullOrEmpty(sagyobi))
                {
                    //作業日は適用期間より範囲外の場合
                    if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                    {
                        return null;
                    }
                }
            }

            return torihikisaki[0];
        }

        /// <summary>
        /// 取引先取得（削除済、適用期間外も含む）
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd)
        {
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return null;
            }

            M_TORIHIKISAKI keyEntity = new M_TORIHIKISAKI();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity);
            if (torihikisaki == null || torihikisaki.Length < 1)
            {
                return null;
            }
            else
            {
                return torihikisaki[0];
            }
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

        /// <summary>
        /// 車輌取得
        /// </summary>
        /// <param name="sharyouCd"></param>
        /// <returns></returns>
        public M_SHARYOU[] GetSharyou(string sharyouCd, string gyoushaCd, string shasyuCd, string shainCd)
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

            var sharyous = this.sharyouDao.GetAllValidData(keyEntity);
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

                for ( int i = 0; i < 4; i++ )
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

                for ( int i = 0; i < 3; i++ )
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

                for ( int i = 0; i < 2; i++ )
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

                for ( int i = 0; i < 3; i++ )
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

                for ( int i = 0; i < 2; i++ )
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
                return new M_CONTENA_JOUKYOU();

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

            M_KEITAI_KBN keyEntity = new M_KEITAI_KBN();
            keyEntity.KEITAI_KBN_CD = keitaiCbnCd;
            var result = this.keitaiKbnDao.GetAllValidData(keyEntity);
            if (result == null || result.Length < 1)
            {
                return new M_KEITAI_KBN();
            }

            return result[0];
        }
        /// <summary>
        /// 単位を取得
        /// </summary>
        /// <param name="kyotenCd">UNIT_CD</param>
        /// <returns></returns>
        public M_UNIT[] GetDataByCodeForUnit(short unitCd)
        {
            if (unitCd < 1)
            {
                return null;
            }

            M_UNIT keyEntity = new M_UNIT();
            keyEntity.UNIT_CD = unitCd;
            return this.unitDao.GetAllValidData(keyEntity);
        }
        /// <summary>
        /// 領収書管理取得
        /// </summary>
        /// <param name="hiduke">日付</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        public S_NUMBER_RECEIPT GetNumberReceipt(DateTime hiduke, short kyotenCd)
        {
            S_NUMBER_RECEIPT keyEntity = new S_NUMBER_RECEIPT();
            if (hiduke == null)
            {
                return null;
            }
            keyEntity.NUMBERED_DAY = hiduke.Date;
            keyEntity.KYOTEN_CD = (SqlInt16)kyotenCd;

            var numberReceips = this.numberReceiptDao.GetDataForEntity(keyEntity);
            if (numberReceips == null
                || numberReceips.Length < 1)
            {
                return null;
            }

            return numberReceips[0];
 
        }

        /// <summary>
        /// 領収書管理追加
        /// </summary>
        /// <param name="taergetEntity"></param>
        public void InsertNumberReceipt(S_NUMBER_RECEIPT targetEntity)
        {
            this.numberReceiptDao.Insert(targetEntity);
        }

        /// <summary>
        /// 領収書管理更新
        /// </summary>
        /// <param name="targetEntity"></param>
        public void UpdateNumberReceipt(S_NUMBER_RECEIPT targetEntity)
        {
            this.numberReceiptDao.Update(targetEntity);
        }

        /// <summary>
        /// 荷姿単位取得
        /// </summary>
        /// <returns></returns>
        public M_NISUGATA[] GetAllNisugata()
        {
            M_NISUGATA keyEntity = new M_NISUGATA();
            return this.nisugataDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 会社情報取得
        /// </summary>
        /// <param name="keitaiCbnCd"></param>
        /// <returns></returns>
        public M_CORP_INFO GetCorpInfo(SqlInt16 SYS_ID)
        {
            if (SYS_ID < 0)
            {
                return null;
            }

            M_CORP_INFO keyEntity = new M_CORP_INFO();
            keyEntity.SYS_ID = SYS_ID;
            var result = this.corpInfoDao.GetAllValidData(keyEntity);
            if (result == null || result.Length < 1)
            {
                return new M_CORP_INFO();
            }

            return result[0];
        }
        #endregion

        #region Utility
        #endregion
    }
}
