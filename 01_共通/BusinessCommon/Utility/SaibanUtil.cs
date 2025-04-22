using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using r_framework.Logic;
using r_framework.Utility;
using dao = Shougun.Function.ShougunCSCommon.Dao;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    public class SaibanUtil
    {
        /// <summary>
        /// 伝種区分
        /// </summary>
        public enum DenshuKubunCD
        {
            //受入
            DENSHU_KBN_CD_UKEIRE = 1,
            //出荷
            DENSHU_KBN_CD_SHUKKA = 2,
            //売上支払
            DENSHU_KBN_CD_URIAGESHIHARAI = 3,
            //共通
            DENSHU_KBN_CD_KYOTU = 9,
            //入金
            DENSHU_KBN_CD_NYUUKIN = 10,
            //出金
            DENSHU_KBN_CD_SHUKKIN = 20,
            //紙マニフェスト
            DENSHU_KBN_CD_KAMI_MANIFEST = 80,
            //電子マニフェスト
            DENSHU_KBN_CD_DENSHI_MANIFEST = 90,
            //定期実績
            DENSHU_KBN_CD_TEIKI_JISSEKI = 130,
            //計量
            DENSHU_KBN_CD_KEIRYOU = 140,
            //在庫
            DENSHU_KBN_CD_ZAIKO = 150,
            //運賃
            DENSHU_KBN_CD_UNCHIN = 160,
            //見積
            DENSHU_KBN_CD_MITSUMORI = 180,
            //受注目標
            DENSHU_KBN_CD_JYUCYUU_MOKUHYOU = 181,
            //営業予算
            DENSHU_KBN_CD_EIGYOU_YOSAN = 182,
            //実績報告書
            DENSHU_KBN_CD_JISSEKI_HOUKOKUSHO = 400,
            //汎用帳票
            DENSHU_KBN_CD_HANYOU_CHOUHYOU = 912,
            //一覧出力項目選択
            DENSHU_KBN_CD_ICHIRANSYUTSURYOKU_KOUMOKU = 920,
            //一覧出力項目選択(伝票紐付一覧用)
            DENSHU_KBN_CD_ICHIRANSYUTSURYOKU_KOUMOKU_DENPYOU = 922
        }

        /// <summary>
        /// 年連番のデータを取得
        /// </summary>
        /// <param name="numberedYear">年度</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        public static S_NUMBER_YEAR[] GetNumberYear(SqlInt32 numberedYear, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        {
            dao.IS_NUMBER_YEARDao numberYearDao = DaoInitUtility.GetComponent<dao.IS_NUMBER_YEARDao>();
            S_NUMBER_YEAR entity = new S_NUMBER_YEAR();
            entity.NUMBERED_YEAR = numberedYear;
            entity.DENSHU_KBN_CD = denshuKbnCd;
            entity.KYOTEN_CD = kyotenCd;
            return numberYearDao.GetDataForEntity(entity);
        }

        #region "CongBinh 20150714 追加"
        /// <summary>
        /// CURRENT_NUMBER年連番処理     
        /// S_NUMBER_YEARから指定された伝種区分CDと年度と拠点CDの最新のID + 1の値を返す
        /// </summary>
        /// <param name="numberedYear">年度</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns>年連番した数値</returns>
        public static SqlInt32 CreateNumberYear(SqlInt32 numberedYear, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        {
            SqlInt32 returnInt = 1;            
            // 年連番のデータを取得する
            S_NUMBER_YEAR[] numberYears = GetNumberYear(numberedYear, denshuKbnCd, kyotenCd);

            // 年連番DAO
            IS_NUMBER_YEARDao numberYearDao = DaoInitUtility.GetComponent<IS_NUMBER_YEARDao>();

            S_NUMBER_YEAR updateEntity = new S_NUMBER_YEAR();
            updateEntity.NUMBERED_YEAR = numberedYear;
            updateEntity.DENSHU_KBN_CD = denshuKbnCd;
            updateEntity.KYOTEN_CD = kyotenCd;
            updateEntity.DELETE_FLG = false;

            // 年連番へデータを変換・設定
            var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_YEAR>(updateEntity);
            dataBinderNumberDay.SetSystemProperty(updateEntity, false);

            if (numberYears == null || numberYears.Length < 1)
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                // 登録場合
                numberYearDao.Insert(updateEntity);
            }
            else
            {
                returnInt = numberYears[0].CURRENT_NUMBER + 1;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.TIME_STAMP = numberYears[0].TIME_STAMP;
                // 更新場合
                numberYearDao.Update(updateEntity);
            }

            return returnInt;
        }
        #endregion

        /// <summary>
        /// 日連番のデータを取得
        /// </summary>
        /// <param name="numberedDay">日付</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        public static S_NUMBER_DAY[] GetNumberDay(DateTime numberedDay, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        {
           dao.IS_NUMBER_DAYDao numberDayDao = DaoInitUtility.GetComponent<dao.IS_NUMBER_DAYDao>();
            S_NUMBER_DAY entity = new S_NUMBER_DAY();
            entity.NUMBERED_DAY = numberedDay;
            entity.DENSHU_KBN_CD = denshuKbnCd;
            entity.KYOTEN_CD = kyotenCd;
            return numberDayDao.GetDataForEntity(entity);
        }

        #region "CongBinh 20150714 追加"
        /// <summary>
        /// CURRENT_NUMBER日連番処理     
        /// S_NUMBER_DAYから指定された伝種区分CDと日付と拠点CDの最新のID + 1の値を返す
        /// </summary>
        /// <param name="numberedDay">日付</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns>日連番した数値</returns>
        public static SqlInt32 CreateNumberDay(DateTime numberedDay, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        {
            SqlInt32 returnInt = 1;

            // 日連番のデータを取得する
            S_NUMBER_DAY[] numberDays = GetNumberDay(numberedDay, denshuKbnCd, kyotenCd);

            // 日連番DAO
            IS_NUMBER_DAYDao numberDayDao = DaoInitUtility.GetComponent<IS_NUMBER_DAYDao>();

            S_NUMBER_DAY updateEntity = new S_NUMBER_DAY();
            updateEntity.NUMBERED_DAY = numberedDay.Date;
            updateEntity.DENSHU_KBN_CD = denshuKbnCd;
            updateEntity.KYOTEN_CD = kyotenCd;
            updateEntity.DELETE_FLG = false;

            // 日連番へデータを変換・設定
            var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(updateEntity);
            dataBinderNumberDay.SetSystemProperty(updateEntity, false);

            if (numberDays == null || numberDays.Length < 1)
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                // 登録場合
                numberDayDao.Insert(updateEntity);
            }
            else
            {
                returnInt = numberDays[0].CURRENT_NUMBER + 1;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.TIME_STAMP = numberDays[0].TIME_STAMP;
                // 更新場合
                numberDayDao.Update(updateEntity);

            }

            return returnInt;
        }
        #endregion

        /// <summary>
        /// SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// S_NUMBER_SYSTEMから指定された伝種区分CDの最新のID + 1の値を返す
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns>採番した数値</returns>
        public static SqlInt64 createSystemId(SqlInt16 denshuKbnCd)
        {
            SqlInt64 returnInt = 1;
            IS_NUMBER_SYSTEMDao numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = denshuKbnCd;

            var updateEntity = numberSystemDao.GetNumberSystemData(entity);
            returnInt = numberSystemDao.GetMaxPlusKey(entity);

            //データがない場合
            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = denshuKbnCd;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                numberSystemDao.Insert(updateEntity);
            }
            //データがある場合
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 伝種区分別番号採番処理
        /// S_NUMBER_DENSHUから指定された伝種区分CDの最新の番号 + 1の値を返す
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns>採番した数値</returns>
        public static SqlInt64 createDenshuNumber(SqlInt16 denshuKbnCd)
        {
            SqlInt64 returnInt = -1;
            IS_NUMBER_DENSHUDao numberDenshuDao = DaoInitUtility.GetComponent<IS_NUMBER_DENSHUDao>();

            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = denshuKbnCd;

            var updateEntity = numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = numberDenshuDao.GetMaxPlusKey(entity);

            //データがない場合
            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = denshuKbnCd;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                numberDenshuDao.Insert(updateEntity);
            }
            //データがある場合
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                numberDenshuDao.Update(updateEntity);
            }

            return returnInt;
        }
    }
}
