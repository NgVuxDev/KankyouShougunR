using System.Collections.Generic;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2
{
    /// <summary>
    /// G459 入金入力 DTOクラス
    /// </summary>
    internal class DTOClass
    {
        #region プロパティ
        /// <summary>
        /// 入金一括入力エンティティを取得・設定します
        /// </summary>
        internal T_NYUUKIN_SUM_ENTRY NyuukinSumEntry { get; set; }

        /// <summary>
        /// 入金一括明細エンティティリストを取得・設定します
        /// </summary>
        internal List<T_NYUUKIN_SUM_DETAIL> NyuukinSumDetailList { get; set; }

        /// <summary>
        /// 入金入力エンティティリストを取得・設定します
        /// </summary>
        internal List<T_NYUUKIN_ENTRY> NyuukinEntryList { get; set; }

        /// <summary>
        /// 入金明細エンティティリストを取得・設定します
        /// </summary>
        internal List<T_NYUUKIN_DETAIL> NyuukinDetailList { get; set; }

        /// <summary>
        /// 入金消込エンティティリストを取得・設定します
        /// </summary>
        internal List<T_NYUUKIN_KESHIKOMI> NyuukinKeshikomiList { get; set; }

        /// <summary>
        /// 仮受金調整エンティティを取得・設定します
        /// </summary>
        internal T_KARIUKE_CHOUSEI KariukeChousei { get; set; }

        /// <summary>
        /// 仮受金管理エンティティを取得・設定します
        /// </summary>
        internal T_KARIUKE_CONTROL KariukeControl { get; set; }

        /// <summary>
        /// 入金先変更前の仮受金管理エンティティを取得・設定します
        /// </summary>
        internal T_KARIUKE_CONTROL BeforeKariukeControl { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal DTOClass()
        {
            this.NyuukinSumEntry = new T_NYUUKIN_SUM_ENTRY();
            this.NyuukinSumDetailList = new List<T_NYUUKIN_SUM_DETAIL>();
            this.NyuukinEntryList = new List<T_NYUUKIN_ENTRY>();
            this.NyuukinDetailList = new List<T_NYUUKIN_DETAIL>();
            this.NyuukinKeshikomiList = new List<T_NYUUKIN_KESHIKOMI>();
            this.KariukeChousei = new T_KARIUKE_CHOUSEI();
            this.KariukeControl = new T_KARIUKE_CONTROL();

            // 新規のときは変更前がないので、nullで初期化
            this.BeforeKariukeControl = null;
        }
        #endregion

        #region 複製メソッド
        /// <summary>
        /// DTOを複製します
        /// </summary>
        /// <param name="dto">複製元DTO</param>
        /// <returns>複製したDTO</returns>
        internal DTOClass CloneDto(DTOClass dto)
        {
            LogUtility.DebugMethodStart(dto);

            var ret = new DTOClass();
            ret.NyuukinSumEntry = this.CloneNyuukinSumEntry(this.NyuukinSumEntry);
            ret.NyuukinSumDetailList = this.CloneNyuukinSumDetailList(this.NyuukinSumDetailList);
            ret.NyuukinEntryList = this.CloneNyuukinEntryList(this.NyuukinEntryList);
            ret.NyuukinDetailList = this.CloneNyuukinDetailList(this.NyuukinDetailList);
            ret.NyuukinKeshikomiList = this.CloneNyuukinKeshikomiList(this.NyuukinKeshikomiList);
            ret.KariukeChousei = this.CloneKariukeChousei(this.KariukeChousei);
            ret.KariukeControl = this.CloneKariukeControl(this.KariukeControl);
            ret.BeforeKariukeControl = this.CloneKariukeControl(this.BeforeKariukeControl);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金一括入力エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_NYUUKIN_SUM_ENTRY CloneNyuukinSumEntry(T_NYUUKIN_SUM_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_NYUUKIN_SUM_ENTRY();
            ret.SYSTEM_ID = entity.SYSTEM_ID;
            ret.SEQ = entity.SEQ;
            ret.KYOTEN_CD = entity.KYOTEN_CD;
            ret.NYUUKIN_NUMBER = entity.NYUUKIN_NUMBER;
            ret.DENPYOU_DATE = entity.DENPYOU_DATE;
            ret.NYUUKINSAKI_CD = entity.NYUUKINSAKI_CD;
            ret.BANK_CD = entity.BANK_CD;
            ret.BANK_SHITEN_CD = entity.BANK_SHITEN_CD;
            ret.KOUZA_SHURUI = entity.KOUZA_SHURUI;
            ret.KOUZA_NO = entity.KOUZA_NO;
            ret.KOUZA_NAME = entity.KOUZA_NAME;
            ret.EIGYOU_TANTOUSHA_CD = entity.EIGYOU_TANTOUSHA_CD;
            ret.DENPYOU_BIKOU = entity.DENPYOU_BIKOU;
            ret.NYUUKIN_AMOUNT_TOTAL = entity.NYUUKIN_AMOUNT_TOTAL;
            ret.CHOUSEI_AMOUNT_TOTAL = entity.CHOUSEI_AMOUNT_TOTAL;
            ret.KARIUKEKIN_WARIATE_TOTAL = entity.KARIUKEKIN_WARIATE_TOTAL;
            ret.SEISAN_SOUSAI_CREATE_KBN = entity.SEISAN_SOUSAI_CREATE_KBN;
            ret.CREATE_USER = entity.CREATE_USER;
            ret.CREATE_DATE = entity.CREATE_DATE;
            ret.CREATE_PC = entity.CREATE_PC;
            ret.UPDATE_USER = entity.UPDATE_USER;
            ret.UPDATE_DATE = entity.UPDATE_DATE;
            ret.UPDATE_PC = entity.UPDATE_PC;
            ret.DELETE_FLG = entity.DELETE_FLG;
            ret.TIME_STAMP = entity.TIME_STAMP;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金一括明細エンティティリストを複製します
        /// </summary>
        /// <param name="entityList">複製元エンティティリスト</param>
        /// <returns>複製したエンティティリスト</returns>
        internal List<T_NYUUKIN_SUM_DETAIL> CloneNyuukinSumDetailList(List<T_NYUUKIN_SUM_DETAIL> entityList)
        {
            LogUtility.DebugMethodStart(entityList);

            var ret = new List<T_NYUUKIN_SUM_DETAIL>();
            foreach (var entity in entityList)
            {
                ret.Add(this.CloneNyuukinSumDetail(entity));
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金一括明細エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_NYUUKIN_SUM_DETAIL CloneNyuukinSumDetail(T_NYUUKIN_SUM_DETAIL entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_NYUUKIN_SUM_DETAIL();
            ret.SYSTEM_ID = entity.SYSTEM_ID;
            ret.SEQ = entity.SEQ;
            ret.DETAIL_SYSTEM_ID = entity.DETAIL_SYSTEM_ID;
            ret.ROW_NUMBER = entity.ROW_NUMBER;
            ret.NYUUSHUKKIN_KBN_CD = entity.NYUUSHUKKIN_KBN_CD;
            ret.KINGAKU = entity.KINGAKU;
            ret.MEISAI_BIKOU = entity.MEISAI_BIKOU;
            ret.TIME_STAMP = entity.TIME_STAMP;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金入力エンティティリストを複製します
        /// </summary>
        /// <param name="entityList">複製元エンティティリスト</param>
        /// <returns>複製したエンティティリスト</returns>
        internal List<T_NYUUKIN_ENTRY> CloneNyuukinEntryList(List<T_NYUUKIN_ENTRY> entityList)
        {
            LogUtility.DebugMethodStart(entityList);

            var ret = new List<T_NYUUKIN_ENTRY>();
            foreach (var entity in entityList)
            {
                ret.Add(this.CloneNyuukinEntry(entity));
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金入力エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_NYUUKIN_ENTRY CloneNyuukinEntry(T_NYUUKIN_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_NYUUKIN_ENTRY();
            ret.SYSTEM_ID = entity.SYSTEM_ID;
            ret.SEQ = entity.SEQ;
            ret.KYOTEN_CD = entity.KYOTEN_CD;
            ret.NYUUKIN_SUM_SYSTEM_ID = entity.NYUUKIN_SUM_SYSTEM_ID;
            ret.NYUUKIN_NUMBER = entity.NYUUKIN_NUMBER;
            ret.DENPYOU_DATE = entity.DENPYOU_DATE;
            ret.TORIHIKISAKI_CD = entity.TORIHIKISAKI_CD;
            ret.NYUUKINSAKI_CD = entity.NYUUKINSAKI_CD;
            ret.BANK_CD = entity.BANK_CD;
            ret.BANK_SHITEN_CD = entity.BANK_SHITEN_CD;
            ret.KOUZA_SHURUI = entity.KOUZA_SHURUI;
            ret.KOUZA_NO = entity.KOUZA_NO;
            ret.KOUZA_NAME = entity.KOUZA_NAME;
            ret.EIGYOU_TANTOUSHA_CD = entity.EIGYOU_TANTOUSHA_CD;
            ret.KARIUKEKIN = entity.KARIUKEKIN;
            ret.DENPYOU_BIKOU = entity.DENPYOU_BIKOU;
            ret.NYUUKIN_AMOUNT_TOTAL = entity.NYUUKIN_AMOUNT_TOTAL;
            ret.CHOUSEI_AMOUNT_TOTAL = entity.CHOUSEI_AMOUNT_TOTAL;
            ret.KARIUKEKIN_WARIATE_TOTAL = entity.KARIUKEKIN_WARIATE_TOTAL;
            ret.CHOUSEI_DENPYOU_KBN = entity.CHOUSEI_DENPYOU_KBN;
            ret.CREATE_USER = entity.CREATE_USER;
            ret.CREATE_DATE = entity.CREATE_DATE;
            ret.CREATE_PC = entity.CREATE_PC;
            ret.UPDATE_USER = entity.UPDATE_USER;
            ret.UPDATE_DATE = entity.UPDATE_DATE;
            ret.UPDATE_PC = entity.UPDATE_PC;
            ret.DELETE_FLG = entity.DELETE_FLG;
            ret.TIME_STAMP = entity.TIME_STAMP;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金明細エンティティリストを複製します
        /// </summary>
        /// <param name="entityList">複製元エンティティリスト</param>
        /// <returns>複製したエンティティリスト</returns>
        internal List<T_NYUUKIN_DETAIL> CloneNyuukinDetailList(List<T_NYUUKIN_DETAIL> entityList)
        {
            LogUtility.DebugMethodStart(entityList);

            var ret = new List<T_NYUUKIN_DETAIL>();
            foreach (var entity in entityList)
            {
                ret.Add(this.CloneNyuukinDetail(entity));
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金明細エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_NYUUKIN_DETAIL CloneNyuukinDetail(T_NYUUKIN_DETAIL entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_NYUUKIN_DETAIL();
            ret.SYSTEM_ID = entity.SYSTEM_ID;
            ret.SEQ = entity.SEQ;
            ret.DETAIL_SYSTEM_ID = entity.DETAIL_SYSTEM_ID;
            ret.ROW_NUMBER = entity.ROW_NUMBER;
            ret.NYUUSHUKKIN_KBN_CD = entity.NYUUSHUKKIN_KBN_CD;
            ret.KINGAKU = entity.KINGAKU;
            ret.MEISAI_BIKOU = entity.MEISAI_BIKOU;
            ret.TIME_STAMP = entity.TIME_STAMP;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金消込エンティティリストを複製します
        /// </summary>
        /// <param name="entityList">複製元エンティティリスト</param>
        /// <returns>複製したエンティティリスト</returns>
        internal List<T_NYUUKIN_KESHIKOMI> CloneNyuukinKeshikomiList(List<T_NYUUKIN_KESHIKOMI> entityList)
        {
            LogUtility.DebugMethodStart(entityList);

            var ret = new List<T_NYUUKIN_KESHIKOMI>();
            foreach (var entity in entityList)
            {
                ret.Add(this.CloneNyuukinKeshikomi(entity));
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金消込エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_NYUUKIN_KESHIKOMI CloneNyuukinKeshikomi(T_NYUUKIN_KESHIKOMI entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_NYUUKIN_KESHIKOMI();
            ret.SYSTEM_ID = entity.SYSTEM_ID;
            ret.SEIKYUU_NUMBER = entity.SEIKYUU_NUMBER;
            ret.KESHIKOMI_SEQ = entity.KESHIKOMI_SEQ;
            ret.NYUUKIN_NUMBER = entity.NYUUKIN_NUMBER;
            ret.TORIHIKISAKI_CD = entity.TORIHIKISAKI_CD;
            ret.KESHIKOMI_GAKU = entity.KESHIKOMI_GAKU;
            ret.KESHIKOMI_BIKOU = entity.KESHIKOMI_BIKOU;
            ret.NYUUKIN_SEQ = entity.NYUUKIN_SEQ;
            ret.DELETE_FLG = entity.DELETE_FLG;
            ret.TIME_STAMP = entity.TIME_STAMP;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 仮受金調整エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_KARIUKE_CHOUSEI CloneKariukeChousei(T_KARIUKE_CHOUSEI entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_KARIUKE_CHOUSEI();

            if (null != entity)
            {
                ret.SYSTEM_ID = entity.SYSTEM_ID;
                ret.SEQ = entity.SEQ;
                ret.NYUUKIN_NUMBER = entity.NYUUKIN_NUMBER;
                ret.NYUUKINSAKI_CD = entity.NYUUKINSAKI_CD;
                ret.KINGAKU = entity.KINGAKU;
                ret.DELETE_FLG = entity.DELETE_FLG;
                ret.TIME_STAMP = entity.TIME_STAMP;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 仮受金管理エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_KARIUKE_CONTROL CloneKariukeControl(T_KARIUKE_CONTROL entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_KARIUKE_CONTROL();

            if (null != entity)
            {
                ret.NYUUKINSAKI_CD = entity.NYUUKINSAKI_CD;
                ret.KARIUKE_TOTAL_KINGAKU = entity.KARIUKE_TOTAL_KINGAKU;
                ret.TIME_STAMP = entity.TIME_STAMP;
            }
            else
            {
                ret = null;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion
    }
}
