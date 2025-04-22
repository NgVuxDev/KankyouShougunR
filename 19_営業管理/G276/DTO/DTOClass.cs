using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;
using System.Data;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku
{
    internal class DTOClass
    {
        /// <summary>
        /// M_SYS_INFO
        /// </summary>
        internal M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// T_MITSUMORI_ENTRY用のEntity
        /// </summary>
        internal T_MITSUMORI_ENTRY entryEntity;

        /// <summary>
        /// T_MITSUMORI_DETAIL_ENTRY用のEntity
        /// </summary>
        internal T_MITSUMORI_DETAIL[] detailEntity;

        //20250416
        internal T_MITSUMORI_DETAIL_2[] detailEntity_2;

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
        internal M_HIKIAI_TORIHIKISAKI hikiaiTorihikisakiEntry;

        /// <summary>
        /// M_HIKIAI_TORIHIKISAKI_SEIKYUU用のEntity
        /// </summary>
        internal M_HIKIAI_TORIHIKISAKI_SEIKYUU hikiaiTorihikisakiSeikyuuEntity;

        /// <summary>
        /// M_HIKIAI_TORIHIKISAKI_SHIHARAI用のEntity
        /// </summary>
        internal M_HIKIAI_TORIHIKISAKI_SHIHARAI hikiaiTorihikisakiShiharaiEntity;

        /// <summary>
        /// M_HIKIAI_GYOUSHA_ENTRY用のEntity
        /// </summary>
        internal M_HIKIAI_GYOUSHA hikiaiGyoushaEntry;        
        
        /// <summary>
        /// M_HIKIAI_GENBA_ENTRY用のEntity
        /// </summary>
        internal M_HIKIAI_GENBA hikiaiGenbaEntry;

        /// <summary>
        /// M_BUSHO
        /// </summary>
        internal M_BUSHO bushoEntity;

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
            this.entryEntity = new T_MITSUMORI_ENTRY();
            this.detailEntity = new T_MITSUMORI_DETAIL[] { new T_MITSUMORI_DETAIL() };

            //20250416
            this.detailEntity_2 = new T_MITSUMORI_DETAIL_2[] { new T_MITSUMORI_DETAIL_2() };

            this.torihikisakiEntry = new M_TORIHIKISAKI();
            this.gyoushaEntry = new M_GYOUSHA();
            this.genbaEntry = new M_GENBA();
            this.hikiaiTorihikisakiEntry = new M_HIKIAI_TORIHIKISAKI();
            this.hikiaiTorihikisakiSeikyuuEntity = new M_HIKIAI_TORIHIKISAKI_SEIKYUU();
            this.hikiaiTorihikisakiShiharaiEntity = new M_HIKIAI_TORIHIKISAKI_SHIHARAI();
            this.hikiaiGyoushaEntry = new M_HIKIAI_GYOUSHA();
            this.hikiaiGenbaEntry = new M_HIKIAI_GENBA();
            this.torihikisakiSeikyuuEntity = new M_TORIHIKISAKI_SEIKYUU();
            this.torihikisakiShiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
            this.bushoEntity = new M_BUSHO();
            this.kyotenEntity = new M_KYOTEN(); 
            this.kyotenEntity1 = new M_KYOTEN();
            this.kyotenEntity2 = new M_KYOTEN();
            this.corpEntity = new M_CORP_INFO();
        }

        /// <summary>
        /// Dtoの中身をコピーする
        /// T_MITSUMORI_ENTRYとT_MITSUMORI_DETAILのみデータコピーをする。
        /// それ以外は参照渡し。
        /// もし上記以外の値コピーをしたい場合は適宜追加。
        /// </summary>
        /// <returns></returns>
        public DTOClass Clone()
        {

            DTOClass returnDto = new DTOClass();
            returnDto.entryEntity = this.MitsumoriEntityClone();
            List<T_MITSUMORI_DETAIL> copyList = new List<T_MITSUMORI_DETAIL>();
            if (this.detailEntity != null)
            {
                foreach (var targetEntity in this.detailEntity)
                {
                    copyList.Add(this.MitsumoriDetailClone(targetEntity));
                }

            }
            returnDto.detailEntity = copyList.ToArray();

            //20250416
            List<T_MITSUMORI_DETAIL_2> copyList_2 = new List<T_MITSUMORI_DETAIL_2>();
            if (this.detailEntity_2 != null)
            {
                foreach (var targetEntity_2 in this.detailEntity_2)
                {
                    copyList_2.Add(this.BikoDetailClone(targetEntity_2));
                }
            }
            returnDto.detailEntity_2 = copyList_2.ToArray();

            returnDto.sysInfoEntity = this.sysInfoEntity;
            returnDto.torihikisakiEntry = this.torihikisakiEntry;
            returnDto.gyoushaEntry = this.gyoushaEntry;
            returnDto.genbaEntry = this.genbaEntry;
            returnDto.hikiaiTorihikisakiEntry = this.hikiaiTorihikisakiEntry;
            returnDto.hikiaiTorihikisakiSeikyuuEntity = this.hikiaiTorihikisakiSeikyuuEntity;
            returnDto.hikiaiTorihikisakiShiharaiEntity = this.hikiaiTorihikisakiShiharaiEntity;
            returnDto.hikiaiGyoushaEntry = this.hikiaiGyoushaEntry;
            returnDto.hikiaiGenbaEntry = this.hikiaiGenbaEntry;
            returnDto.torihikisakiSeikyuuEntity = this.torihikisakiSeikyuuEntity;
            returnDto.torihikisakiShiharaiEntity = this.torihikisakiShiharaiEntity;
            returnDto.bushoEntity = this.bushoEntity;
            returnDto.kyotenEntity = this.kyotenEntity;
            returnDto.kyotenEntity1 = this.kyotenEntity1;
            returnDto.kyotenEntity2 = this.kyotenEntity2; 
            returnDto.corpEntity = this.corpEntity;
            return returnDto;
        }

        /// <summary>
        /// 自身のT_MITSUMORI_ENTRYを複製する
        /// </summary>
        /// <returns></returns>
        private T_MITSUMORI_ENTRY MitsumoriEntityClone()
        {
            var returnEntity = new T_MITSUMORI_ENTRY();

            returnEntity.SYSTEM_ID =this.entryEntity.SYSTEM_ID;
            returnEntity.SEQ =this.entryEntity.SEQ;
            returnEntity.KYOTEN_CD =this.entryEntity.KYOTEN_CD;
            returnEntity.MITSUMORI_SHOSHIKI_KBN =this.entryEntity.MITSUMORI_SHOSHIKI_KBN;
            returnEntity.PEGE_TOTAL =this.entryEntity.PEGE_TOTAL;
            returnEntity.JOKYO_FLG =this.entryEntity.JOKYO_FLG;
            returnEntity.SINKOU_DATE =this.entryEntity.SINKOU_DATE;
            returnEntity.JUCHU_DATE =this.entryEntity.JUCHU_DATE;
            returnEntity.SICHU_DATE =this.entryEntity.SICHU_DATE;
            returnEntity.MITSUMORI_NUMBER =this.entryEntity.MITSUMORI_NUMBER;
            returnEntity.MITSUMORI_DATE =this.entryEntity.MITSUMORI_DATE;
            returnEntity.INJI_KYOTEN1_CD =this.entryEntity.INJI_KYOTEN1_CD;
            returnEntity.INJI_KYOTEN2_CD =this.entryEntity.INJI_KYOTEN2_CD;
            returnEntity.HIKIAI_TORIHIKISAKI_FLG =this.entryEntity.HIKIAI_TORIHIKISAKI_FLG;
            returnEntity.TORIHIKISAKI_CD =this.entryEntity.TORIHIKISAKI_CD;
            returnEntity.TORIHIKISAKI_NAME =this.entryEntity.TORIHIKISAKI_NAME;
            returnEntity.TORIHIKISAKI_INJI =this.entryEntity.TORIHIKISAKI_INJI;
            returnEntity.HIKIAI_GYOUSHA_FLG =this.entryEntity.HIKIAI_GYOUSHA_FLG;
            returnEntity.GYOUSHA_CD =this.entryEntity.GYOUSHA_CD;
            returnEntity.GYOUSHA_NAME =this.entryEntity.GYOUSHA_NAME;
            returnEntity.GYOUSHA_INJI =this.entryEntity.GYOUSHA_INJI;
            returnEntity.HIKIAI_GENBA_FLG =this.entryEntity.HIKIAI_GENBA_FLG;
            returnEntity.GENBA_CD =this.entryEntity.GENBA_CD;
            returnEntity.GENBA_NAME =this.entryEntity.GENBA_NAME;
            returnEntity.GENBA_INJI =this.entryEntity.GENBA_INJI;
            returnEntity.SHAIN_CD =this.entryEntity.SHAIN_CD;
            returnEntity.SHAIN_NAME =this.entryEntity.SHAIN_NAME;
            returnEntity.TORIHIKISAKI_KEISHOU =this.entryEntity.TORIHIKISAKI_KEISHOU;
            returnEntity.GYOUSHA_KEISHOU =this.entryEntity.GYOUSHA_KEISHOU;
            returnEntity.GENBA_KEISHOU =this.entryEntity.GENBA_KEISHOU;
            returnEntity.KENMEI =this.entryEntity.KENMEI;
            returnEntity.MITSUMORI_1 =this.entryEntity.MITSUMORI_1;
            returnEntity.MITSUMORI_2 =this.entryEntity.MITSUMORI_2;
            returnEntity.MITSUMORI_3 =this.entryEntity.MITSUMORI_3;
            returnEntity.MITSUMORI_4 =this.entryEntity.MITSUMORI_4;
            returnEntity.BIKOU_1 =this.entryEntity.BIKOU_1;
            returnEntity.BIKOU_2 =this.entryEntity.BIKOU_2;
            returnEntity.BIKOU_3 =this.entryEntity.BIKOU_3;
            returnEntity.BIKOU_4 =this.entryEntity.BIKOU_4;
            returnEntity.BIKOU_5 =this.entryEntity.BIKOU_5;
            returnEntity.MITSUMORI_INJI_DATE =this.entryEntity.MITSUMORI_INJI_DATE;
            returnEntity.SHANAI_BIKOU =this.entryEntity.SHANAI_BIKOU;
            returnEntity.ZEI_KEISAN_KBN_CD =this.entryEntity.ZEI_KEISAN_KBN_CD;
            returnEntity.ZEI_KBN_CD =this.entryEntity.ZEI_KBN_CD;
            returnEntity.KINGAKU_TOTAL =this.entryEntity.KINGAKU_TOTAL;
            returnEntity.SHOUHIZEI_RATE =this.entryEntity.SHOUHIZEI_RATE;
            returnEntity.TAX_SOTO =this.entryEntity.TAX_SOTO;
            returnEntity.TAX_UCHI =this.entryEntity.TAX_UCHI;
            returnEntity.TAX_SOTO_TOTAL =this.entryEntity.TAX_SOTO_TOTAL;
            returnEntity.TAX_UCHI_TOTAL =this.entryEntity.TAX_UCHI_TOTAL;
            returnEntity.SHOUHIZEI_TOTAL =this.entryEntity.SHOUHIZEI_TOTAL;
            returnEntity.GOUKEI_KINGAKU_TOTAL =this.entryEntity.GOUKEI_KINGAKU_TOTAL;
            returnEntity.CREATE_USER =this.entryEntity.CREATE_USER;
            returnEntity.CREATE_DATE =this.entryEntity.CREATE_DATE;
            returnEntity.CREATE_PC =this.entryEntity.CREATE_PC;
            returnEntity.UPDATE_USER =this.entryEntity.UPDATE_USER;
            returnEntity.UPDATE_DATE =this.entryEntity.UPDATE_DATE;
            returnEntity.UPDATE_PC =this.entryEntity.UPDATE_PC;
            returnEntity.DELETE_FLG =this.entryEntity.DELETE_FLG;
            returnEntity.TIME_STAMP = this.entryEntity.TIME_STAMP;

            //20250414
            returnEntity.BIKO_KBN_CD = this.entryEntity.BIKO_KBN_CD;
            returnEntity.BIKO_NAME_RYAKU = this.entryEntity.BIKO_NAME_RYAKU;

            return returnEntity;
        }

        /// <summary>
        /// 自身のT_MITSUMORI_DETAILを複製する
        /// </summary>
        /// <returns></returns>
        public T_MITSUMORI_DETAIL MitsumoriDetailClone(T_MITSUMORI_DETAIL copyTarget)
        {
            var returnEntity = new T_MITSUMORI_DETAIL();

            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.DENPYOU_NUMBER = copyTarget.DENPYOU_NUMBER;
            returnEntity.PAGE_NUMBER = copyTarget.PAGE_NUMBER;
            returnEntity.ROW_NO = copyTarget.ROW_NO;
            returnEntity.SHOUKEI_FLG = copyTarget.SHOUKEI_FLG;
            returnEntity.DENPYOU_KBN_CD = copyTarget.DENPYOU_KBN_CD;
            returnEntity.HINMEI_CD = copyTarget.HINMEI_CD;
            returnEntity.HINMEI_NAME = copyTarget.HINMEI_NAME;
            returnEntity.SUURYOU = copyTarget.SUURYOU;
            returnEntity.UNIT_CD = copyTarget.UNIT_CD;
            returnEntity.TANKA = copyTarget.TANKA;
            returnEntity.KINGAKU = copyTarget.KINGAKU;
            returnEntity.TAX_SOTO = copyTarget.TAX_SOTO;
            returnEntity.TAX_UCHI = copyTarget.TAX_UCHI;
            returnEntity.HINMEI_ZEI_KBN_CD = copyTarget.HINMEI_ZEI_KBN_CD;
            returnEntity.HINMEI_KINGAKU = copyTarget.HINMEI_KINGAKU;
            returnEntity.HINMEI_TAX_SOTO = copyTarget.HINMEI_TAX_SOTO;
            returnEntity.HINMEI_TAX_UCHI = copyTarget.HINMEI_TAX_UCHI;
            returnEntity.MEISAI_BIKOU = copyTarget.MEISAI_BIKOU;
            returnEntity.MEISAI_TEKIYO = copyTarget.MEISAI_TEKIYO;
            returnEntity.TIME_STAMP = copyTarget.TIME_STAMP;

            return returnEntity;
        }

        //20250416
        public T_MITSUMORI_DETAIL_2 BikoDetailClone(T_MITSUMORI_DETAIL_2 copyTarget)
        {
            var returnEntity = new T_MITSUMORI_DETAIL_2();

            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.BIKO_CD = copyTarget.BIKO_CD;
            returnEntity.BIKO_NOTE = copyTarget.BIKO_NOTE;

            return returnEntity;
        }

        #region Utility
        /// <summary>
        /// 自身のT_MITSUMORI_DETAILのDETAIL_SYSTEM_IDのリストを取得する
        /// </summary>
        /// <returns>T_MITSUMORI_DETAILが一件もない</returns>
        public List<SqlInt64> getDetailSysIds()
        {
            List<SqlInt64> returnList = new List<SqlInt64>();

            if (this.detailEntity == null || this.detailEntity.Length < 1)
            {
                return returnList;
            }

            foreach (T_MITSUMORI_DETAIL dtail in this.detailEntity)
            {
                returnList.Add(dtail.DETAIL_SYSTEM_ID);
            }

            return returnList;
        }
        #endregion
    }
}
