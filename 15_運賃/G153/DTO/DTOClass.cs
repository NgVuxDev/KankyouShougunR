using System.Collections.Generic;
using System.Data.SqlTypes;
using r_framework.Entity;

namespace Shougun.Core.Carriage.UnchinNyuuRyoku
{
    internal class DTOClass
    {
        /// <summary>
        /// M_SYS_INFO
        /// </summary>
        internal M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// T_UNCHIN_ENTRY用のEntity
        /// </summary>
        internal T_UNCHIN_ENTRY Entry;

        /// <summary>
        /// T_UNCHIN_DETAIL用のEntity
        /// </summary>
        internal T_UNCHIN_DETAIL[] detailEntity;

        /// <summary>
        /// M_TORIHIKISAKI_ENTRY用のEntity
        /// </summary>
        internal M_TORIHIKISAKI torihikisakiEntry;

        /// <summary>
        /// M_TORIHIKISAKI_SEIKYUU
        /// </summary>
        internal M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuEntity;

        /// <summary>
        /// M_TORIHIKISAKI_SHIHARAI
        /// </summary>
        internal M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiEntity;

        /// <summary>
        /// M_GYOUSHA_ENTRY用のEntity
        /// </summary>
        internal M_GYOUSHA gyoushaEntry;

        /// <summary>
        /// M_GENBA_ENTRY用のEntity
        /// </summary>
        internal M_GENBA genbaEntry;

        /// <summary>
        /// M_HIKIAI_TORIHIKISAKI_ENTRY用のEntity
        /// </summary>
        internal M_HIKIAI_TORIHIKISAKI hikiaitorihikisakiEntry;

        /// <summary>
        /// M_HIKIAI_GYOUSHA_ENTRY用のEntity
        /// </summary>
        internal M_HIKIAI_GYOUSHA hikiaiGyoushaEntry;        
        
        /// <summary>
        /// M_HIKIAI_GENBA_ENTRY用のEntity
        /// </summary>
        internal M_HIKIAI_GENBA hikiaiGenbaEntry;

        /// <summary>
        /// M_KYOTEN
        /// </summary>
        internal M_KYOTEN kyotenEntity;

        /// <summary>
        /// M_KYOTEN
        /// </summary>
        internal M_KYOTEN kyotenEntity1;

        /// <summary>
        /// M_KYOTEN
        /// </summary>
        internal M_KYOTEN kyotenEntity2;

        /// <summary>
        /// M_CORP_INFO
        /// </summary>
        internal M_CORP_INFO corpEntity;

        public DTOClass()
        {
            this.sysInfoEntity = new M_SYS_INFO();
            this.Entry = new T_UNCHIN_ENTRY();
            this.detailEntity = new T_UNCHIN_DETAIL[] { new T_UNCHIN_DETAIL() };
            this.torihikisakiEntry = new M_TORIHIKISAKI();
            this.gyoushaEntry = new M_GYOUSHA();
            this.genbaEntry = new M_GENBA();
            this.hikiaitorihikisakiEntry = new M_HIKIAI_TORIHIKISAKI();
            this.hikiaiGyoushaEntry = new M_HIKIAI_GYOUSHA();
            this.hikiaiGenbaEntry = new M_HIKIAI_GENBA();
            this.torihikisakiSeikyuuEntity = new M_TORIHIKISAKI_SEIKYUU();
            this.torihikisakiShiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
            this.kyotenEntity = new M_KYOTEN(); 
            this.kyotenEntity1 = new M_KYOTEN();
            this.kyotenEntity2 = new M_KYOTEN();
            this.corpEntity = new M_CORP_INFO();
        }

        /// <summary>
        /// Dtoの中身をコピーする
        /// T_UNCHIN_ENTRYとT_UNCHIN_DETAILのみデータコピーをする。
        /// それ以外は参照渡し。
        /// もし上記以外の値コピーをしたい場合は適宜追加。
        /// </summary>
        /// <returns></returns>
        public DTOClass Clone()
        {

            DTOClass returnDto = new DTOClass();
            returnDto.Entry = this.UnchinEntityClone();
            List<T_UNCHIN_DETAIL> copyList = new List<T_UNCHIN_DETAIL>();
            if (this.detailEntity != null)
            {
                foreach (var targetEntity in this.detailEntity)
                {
                    copyList.Add(this.UnchinDetailClone(targetEntity));
                }

            }
            returnDto.detailEntity = copyList.ToArray();

            returnDto.sysInfoEntity = this.sysInfoEntity;
            returnDto.torihikisakiEntry = this.torihikisakiEntry;
            returnDto.gyoushaEntry = this.gyoushaEntry;
            returnDto.genbaEntry = this.genbaEntry;
            returnDto.hikiaitorihikisakiEntry = this.hikiaitorihikisakiEntry;
            returnDto.hikiaiGyoushaEntry = this.hikiaiGyoushaEntry;
            returnDto.hikiaiGenbaEntry = this.hikiaiGenbaEntry;
            returnDto.torihikisakiSeikyuuEntity = this.torihikisakiSeikyuuEntity;
            returnDto.torihikisakiShiharaiEntity = this.torihikisakiShiharaiEntity;
            returnDto.kyotenEntity = this.kyotenEntity;
            returnDto.kyotenEntity1 = this.kyotenEntity1;
            returnDto.kyotenEntity2 = this.kyotenEntity2; 
            returnDto.corpEntity = this.corpEntity;
            return returnDto;
        }

        /// <summary>
        /// 自身のT_UNCHIN_ENTRYを複製する
        /// </summary>
        /// <returns></returns>
        private T_UNCHIN_ENTRY UnchinEntityClone()
        {
            var returnEntity = new T_UNCHIN_ENTRY();
            returnEntity.DENSHU_KBN_CD = this.Entry.DENSHU_KBN_CD;
            returnEntity.RENKEI_NUMBER = this.Entry.RENKEI_NUMBER;
            returnEntity.DENPYOU_DATE = this.Entry.DENPYOU_DATE;
            returnEntity.DENPYOU_NUMBER = this.Entry.DENPYOU_NUMBER;
            returnEntity.UNPAN_GYOUSHA_CD = this.Entry.UNPAN_GYOUSHA_CD;
            returnEntity.UNPAN_GYOUSHA_NAME = this.Entry.UNPAN_GYOUSHA_NAME;
            returnEntity.SHARYOU_CD = this.Entry.SHARYOU_CD;
            returnEntity.SHARYOU_NAME = this.Entry.SHARYOU_NAME;
            returnEntity.SHASHU_CD = this.Entry.SHASHU_CD;
            returnEntity.SHASHU_NAME = this.Entry.SHASHU_NAME;
            returnEntity.UNTENSHA_CD = this.Entry.UNTENSHA_CD;
            returnEntity.UNTENSHA_NAME = this.Entry.UNTENSHA_NAME;
            returnEntity.KEITAI_KBN_CD = this.Entry.KEITAI_KBN_CD;
            returnEntity.KEITAI_KBN_CD = this.Entry.KEITAI_KBN_CD;
            returnEntity.NIZUMI_GYOUSHA_CD = this.Entry.NIZUMI_GYOUSHA_CD;
            returnEntity.NIZUMI_GYOUSHA_NAME = this.Entry.NIZUMI_GYOUSHA_NAME;
            returnEntity.NIZUMI_GENBA_CD = this.Entry.NIZUMI_GENBA_CD;
            returnEntity.NIZUMI_GENBA_NAME = this.Entry.NIZUMI_GENBA_NAME;
            returnEntity.NIOROSHI_GYOUSHA_CD = this.Entry.NIOROSHI_GYOUSHA_CD;
            returnEntity.NIOROSHI_GYOUSHA_NAME = this.Entry.NIOROSHI_GYOUSHA_NAME;
            returnEntity.NIOROSHI_GENBA_CD = this.Entry.NIOROSHI_GENBA_CD;
            returnEntity.NIOROSHI_GENBA_NAME = this.Entry.NIOROSHI_GENBA_NAME;
            returnEntity.DENPYOU_BIKOU = this.Entry.DENPYOU_BIKOU;
            returnEntity.NET_TOTAL = this.Entry.NET_TOTAL;
            returnEntity.KINGAKU_TOTAL = this.Entry.KINGAKU_TOTAL;
            returnEntity.CREATE_USER = this.Entry.CREATE_USER;
            returnEntity.CREATE_DATE = this.Entry.CREATE_DATE;
            returnEntity.CREATE_PC = this.Entry.CREATE_PC;
            returnEntity.UPDATE_USER = this.Entry.UPDATE_USER;
            returnEntity.UPDATE_DATE = this.Entry.UPDATE_DATE;
            returnEntity.UPDATE_PC = this.Entry.UPDATE_PC;
            returnEntity.DELETE_FLG = this.Entry.DELETE_FLG;
            returnEntity.TIME_STAMP = this.Entry.TIME_STAMP;
            return returnEntity;
        }

        /// <summary>
        /// 自身のT_UNCHIN_DETAILを複製する
        /// </summary>
        /// <returns></returns>
        public T_UNCHIN_DETAIL UnchinDetailClone(T_UNCHIN_DETAIL copyTarget)
        {
            var returnEntity = new T_UNCHIN_DETAIL();
            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.DENPYOU_NUMBER = copyTarget.DENPYOU_NUMBER;
            returnEntity.ROW_NO = copyTarget.ROW_NO;
            returnEntity.UNCHIN_HINMEI_CD = copyTarget.UNCHIN_HINMEI_CD;
            returnEntity.UNCHIN_HINMEI_NAME = copyTarget.UNCHIN_HINMEI_NAME;
            returnEntity.NET_JYUURYOU = copyTarget.NET_JYUURYOU;
            returnEntity.SUURYOU = copyTarget.SUURYOU;
            returnEntity.UNIT_CD = copyTarget.UNIT_CD;
            returnEntity.TANKA = copyTarget.TANKA;
            returnEntity.KINGAKU = copyTarget.KINGAKU;
            returnEntity.MEISAI_BIKOU = copyTarget.MEISAI_BIKOU;
            returnEntity.TIME_STAMP = copyTarget.TIME_STAMP;
            return returnEntity;
        }

        #region Utility
        /// <summary>
        /// 自身のT_UNCHIN_DETAILのDETAIL_SYSTEM_IDのリストを取得する
        /// </summary>
        /// <returns>T_UNCHIN_DETAILが一件もない</returns>
        public List<SqlInt64> getDetailSysIds()
        {
            List<SqlInt64> returnList = new List<SqlInt64>();

            if (this.detailEntity == null || this.detailEntity.Length < 1)
            {
                return returnList;
            }

            foreach (T_UNCHIN_DETAIL dtail in this.detailEntity)
            {
                returnList.Add(dtail.DETAIL_SYSTEM_ID);
            }

            return returnList;
        }
        #endregion
    }
}
