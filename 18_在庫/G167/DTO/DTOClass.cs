using System.Collections.Generic;
using r_framework.Entity;
using r_framework.Utility;
using System;

namespace Shougun.Core.Stock.ZaikoTyouseiNyuuryoku
{
    /// <summary>
    /// G631 入金入力 DTOクラス
    /// </summary>
    internal class DTOClass
    {
        #region プロパティ
        /// <summary>
        /// 入金入力エンティティリストを取得・設定します
        /// </summary>
        internal T_ZAIKO_TYOUSEI_ENTRY Entry { get; set; }

        /// <summary>
        /// 入金明細エンティティリストを取得・設定します
        /// </summary>
        internal List<T_ZAIKO_TYOUSEI_DETAIL> DetailList { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal DTOClass()
        {
            this.Entry = new T_ZAIKO_TYOUSEI_ENTRY();
            this.DetailList = new List<T_ZAIKO_TYOUSEI_DETAIL>();
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
            ret.Entry = this.CloneZaikoTyouseiEntry(this.Entry);
            ret.DetailList = this.CloneZaikoTyouseiDetailList(this.DetailList);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 在庫移動入力エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_ZAIKO_TYOUSEI_ENTRY CloneZaikoTyouseiEntry(T_ZAIKO_TYOUSEI_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_ZAIKO_TYOUSEI_ENTRY();
            ret.SYSTEM_ID = entity.SYSTEM_ID;
            ret.SEQ = entity.SEQ;
            ret.TYOUSEI_NUMBER = entity.TYOUSEI_NUMBER;
            ret.TYOUSEI_DATE = entity.TYOUSEI_DATE;
            ret.GYOUSHA_CD = entity.GYOUSHA_CD;
            ret.GYOUSHA_NAME = entity.GYOUSHA_NAME;
            ret.GENBA_CD = entity.GENBA_CD;
            ret.GENBA_NAME = entity.GENBA_NAME;
            ret.TYOUSEI_BIKOU1 = entity.TYOUSEI_BIKOU1;
            ret.TYOUSEI_BIKOU2 = entity.TYOUSEI_BIKOU2;
            ret.TYOUSEI_BIKOU3 = entity.TYOUSEI_BIKOU3;
            ret.TYOUSEI_BEFORE_GOUKEI = entity.TYOUSEI_BEFORE_GOUKEI;
            ret.TYOUSEI_AFTER_GOUKEI = entity.TYOUSEI_AFTER_GOUKEI;
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
        /// 在庫移動明細エンティティリストを複製します
        /// </summary>
        /// <param name="entityList">複製元エンティティリスト</param>
        /// <returns>複製したエンティティリスト</returns>
        internal List<T_ZAIKO_TYOUSEI_DETAIL> CloneZaikoTyouseiDetailList(List<T_ZAIKO_TYOUSEI_DETAIL> entityList)
        {
            LogUtility.DebugMethodStart(entityList);

            var ret = new List<T_ZAIKO_TYOUSEI_DETAIL>();
            foreach (var entity in entityList)
            {
                ret.Add(this.CloneZaikoTyouseiDetail(entity));
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 在庫移動明細エンティティを複製します
        /// </summary>
        /// <param name="entity">複製元エンティティ</param>
        /// <returns>複製したエンティティ</returns>
        internal T_ZAIKO_TYOUSEI_DETAIL CloneZaikoTyouseiDetail(T_ZAIKO_TYOUSEI_DETAIL entity)
        {
            LogUtility.DebugMethodStart(entity);

            var ret = new T_ZAIKO_TYOUSEI_DETAIL();
            ret.SYSTEM_ID = entity.SYSTEM_ID;
            ret.SEQ = entity.SEQ;
            ret.DETAIL_SYSTEM_ID = entity.DETAIL_SYSTEM_ID;
            ret.TYOUSEI_NUMBER = entity.TYOUSEI_NUMBER;
            ret.ROW_NO = entity.ROW_NO;
            ret.ZAIKO_HINMEI_CD = entity.ZAIKO_HINMEI_CD;
            ret.ZAIKO_HINMEI_NAME = entity.ZAIKO_HINMEI_NAME;
            ret.TYOUSEI_BEFORE_ZAIKO_RYOU = entity.TYOUSEI_BEFORE_ZAIKO_RYOU;
            ret.TYOUSEI_RYOU = entity.TYOUSEI_RYOU;
            ret.TYOUSEI_AFTER_ZAIKO_RYOU = entity.TYOUSEI_AFTER_ZAIKO_RYOU;
            ret.TIME_STAMP = entity.TIME_STAMP;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion
    }

    internal class GetsujiDTO
    {
        #region プロパティ
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string ZAIKO_HINMEI_CD { get; set; }
        public DateTime DATE_FROM { get; set; }
        public DateTime DATE_TO { get; set; }
        #endregion
    }
}
