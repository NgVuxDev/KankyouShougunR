using System.Collections.Generic;
using r_framework.Entity;
using r_framework.Utility;
using System;

namespace Shougun.Core.ReceiptPayManagement.Syukinnyuryoku
{
    /// <summary>
    /// G090 出金入力 DTOクラス
    /// </summary>
    internal class DTOClass
    {
        #region プロパティ  

        /// <summary>
        /// 出金入力エンティティリストを取得・設定します
        /// </summary>
        internal T_SHUKKIN_ENTRY ShukkinEntry { get; set; }

        /// <summary>
        /// 出金明細エンティティリストを取得・設定します
        /// </summary>
        internal List<T_SHUKKIN_DETAIL> ShukkinDetailList { get; set; }

        /// <summary>
        /// 出金消込エンティティリストを取得・設定します
        /// </summary>
        internal List<T_SHUKKIN_KESHIKOMI> ShukkinKeshikomiList { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal DTOClass()
        {
            this.ShukkinEntry = new T_SHUKKIN_ENTRY();
            this.ShukkinDetailList = new List<T_SHUKKIN_DETAIL>();
            this.ShukkinKeshikomiList = new List<T_SHUKKIN_KESHIKOMI>();
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
            ret.ShukkinEntry = this.CloneShukkinEntry(this.ShukkinEntry);
            ret.ShukkinDetailList = this.CloneShukkinDetailList(this.ShukkinDetailList);
            ret.ShukkinKeshikomiList = this.CloneShukkinKeshikomiList(this.ShukkinKeshikomiList);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        /// <summary>
        /// 出金入力エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_SHUKKIN_ENTRY CloneShukkinEntry(T_SHUKKIN_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_SHUKKIN_ENTRY();
            ret.SYSTEM_ID = entity.SYSTEM_ID;
            ret.SEQ = entity.SEQ;
            ret.KYOTEN_CD = entity.KYOTEN_CD;
            ret.SHUKKIN_NUMBER = entity.SHUKKIN_NUMBER;
            ret.DENPYOU_DATE = entity.DENPYOU_DATE;
            ret.TORIHIKISAKI_CD = entity.TORIHIKISAKI_CD;
            ret.SHUKKINSAKI_CD = entity.SHUKKINSAKI_CD;
            ret.EIGYOU_TANTOUSHA_CD = entity.EIGYOU_TANTOUSHA_CD;
            ret.DENPYOU_BIKOU = entity.DENPYOU_BIKOU;
            ret.SHUKKIN_AMOUNT_TOTAL = entity.SHUKKIN_AMOUNT_TOTAL;
            ret.CHOUSEI_AMOUNT_TOTAL = entity.CHOUSEI_AMOUNT_TOTAL;
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
        /// 出金明細エンティティリストを複製します
        /// </summary>
        /// <param name="entityList">複製元エンティティリスト</param>
        /// <returns>複製したエンティティリスト</returns>
        internal List<T_SHUKKIN_DETAIL> CloneShukkinDetailList(List<T_SHUKKIN_DETAIL> entityList)
        {
            LogUtility.DebugMethodStart(entityList);

            var ret = new List<T_SHUKKIN_DETAIL>();
            foreach (var entity in entityList)
            {
                ret.Add(this.CloneShukkinDetail(entity));
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 出金明細エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_SHUKKIN_DETAIL CloneShukkinDetail(T_SHUKKIN_DETAIL entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_SHUKKIN_DETAIL();
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
        /// 出金消込エンティティリストを複製します
        /// </summary>
        /// <param name="entityList">複製元エンティティリスト</param>
        /// <returns>複製したエンティティリスト</returns>
        internal List<T_SHUKKIN_KESHIKOMI> CloneShukkinKeshikomiList(List<T_SHUKKIN_KESHIKOMI> entityList)
        {
            LogUtility.DebugMethodStart(entityList);

            var ret = new List<T_SHUKKIN_KESHIKOMI>();
            foreach (var entity in entityList)
            {
                ret.Add(this.CloneShukkinKeshikomi(entity));
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 出金消込エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_SHUKKIN_KESHIKOMI CloneShukkinKeshikomi(T_SHUKKIN_KESHIKOMI entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_SHUKKIN_KESHIKOMI();
            ret.SYSTEM_ID = entity.SYSTEM_ID;
            ret.SEISAN_NUMBER = entity.SEISAN_NUMBER;
            ret.KESHIKOMI_SEQ = entity.KESHIKOMI_SEQ;
            ret.SHUKKIN_NUMBER = entity.SHUKKIN_NUMBER;
            ret.TORIHIKISAKI_CD = entity.TORIHIKISAKI_CD;
            ret.KESHIKOMI_GAKU = entity.KESHIKOMI_GAKU;
            ret.KESHIKOMI_BIKOU = entity.KESHIKOMI_BIKOU;
            ret.SHUKKIN_SEQ = entity.SHUKKIN_SEQ;
            ret.DELETE_FLG = entity.DELETE_FLG;
            ret.TIME_STAMP = entity.TIME_STAMP;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion
    }
    /// <summary>
    /// 伝票精算締処理⇒出金入力 DTO
    /// </summary>
    internal class ShiharaiDTOClass
    {
        /// <summary>
        /// 取引先CD
        /// </summary>
        public string TorihikisakiCd { get; set; }
        /// <summary>
        /// 出金区分CD
        /// </summary>
        public Int16 NyuushukinCd { get; set; }
        /// <summary>
        /// 精算日付
        /// </summary>
        public DateTime SeisanDate { get; set; }
        /// <summary>
        /// 精算伝票
        /// </summary>
        public List<Int64> SeisanNumbers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ShiharaiDTOClass()
        {
            this.SeisanNumbers = new List<Int64>();
        }
    }
}
