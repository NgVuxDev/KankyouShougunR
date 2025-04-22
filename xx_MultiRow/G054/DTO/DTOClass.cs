using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;
using System.Data;

namespace Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku
{
    /// <summary>
    /// 売上/支払用DTO
    /// </summary>
    internal class DTOClass
    {
        /// <summary>
        /// T_UR_SH_ENTRY用のEntity
        /// </summary>
        internal T_UR_SH_ENTRY entryEntity;

        /// <summary>
        /// T_UR_SH_DETAIL
        /// </summary>
        internal T_UR_SH_DETAIL[] detailEntity;

        /// <summary>
        /// M_SYS_INFO
        /// </summary>
        internal M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// M_TORIHIKISAKI
        /// </summary>
        internal M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuEntity;

        /// <summary>
        /// M_TORIHIKISAKI_SHIHARAI
        /// </summary>
        internal M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiEntity;

        /// <summary>
        /// M_CONTENA
        /// </summary>
        internal M_CONTENA_JOUKYOU contenaEntity;

        /// <summary>
        /// M_MANIFEST_SHURUI
        /// </summary>
        internal M_MANIFEST_SHURUI manifestShuruiEntity;

        /// <summary>
        /// M_MANIFEST_TEHAI
        /// </summary>
        internal M_MANIFEST_TEHAI manifestTehaiEntity;

        /// <summary>
        /// M_KYOTEN
        /// </summary>
        internal M_KYOTEN kyotenEntity;

        /// <summary>
        /// S_NUMBER_DAY
        /// </summary>
        internal S_NUMBER_DAY numberDay;

        /// <summary>
        /// S_NUMBER_YEAR
        /// </summary>
        internal S_NUMBER_YEAR numberYear;

        /// <summary>
        /// S_NUMBER_RECEIPT
        /// </summary>
        internal S_NUMBER_RECEIPT numberReceipt;

        /// <summary>
        /// S_NUMBER_RECEIPT_YEAR
        /// </summary>
        internal S_NUMBER_RECEIPT_YEAR numberReceiptYear;

        /// <summary>
        /// マニフェスト
        /// </summary>
        internal DataTable manifestEntrys;

        /// <summary>
        /// M_KEITAI_KBN
        /// </summary>
        internal M_KEITAI_KBN keitaiKbnEntity;

        internal List<T_CONTENA_RESULT> contenaResultList;

        internal List<T_CONTENA_RESERVE> contenaReserveList;

        internal List<M_CONTENA> contenaMasterList;

        public DTOClass()
        {
            this.entryEntity = new T_UR_SH_ENTRY();
            this.detailEntity = new T_UR_SH_DETAIL[] { new T_UR_SH_DETAIL() };
            this.sysInfoEntity = new M_SYS_INFO();
            this.torihikisakiSeikyuuEntity = new M_TORIHIKISAKI_SEIKYUU();
            this.torihikisakiShiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
            this.contenaEntity = new M_CONTENA_JOUKYOU();
            this.manifestShuruiEntity = new M_MANIFEST_SHURUI();
            this.manifestTehaiEntity = new M_MANIFEST_TEHAI();
            this.kyotenEntity = new M_KYOTEN();
            this.numberDay = new S_NUMBER_DAY();
            this.numberYear = new S_NUMBER_YEAR();
            this.numberReceipt = new S_NUMBER_RECEIPT();
            this.numberReceiptYear = new S_NUMBER_RECEIPT_YEAR();
            this.manifestEntrys = new DataTable();
            this.keitaiKbnEntity = new M_KEITAI_KBN();
            this.contenaResultList = new List<T_CONTENA_RESULT>();
            this.contenaReserveList = new List<T_CONTENA_RESERVE>();
            this.contenaMasterList = new List<M_CONTENA>();
        }

        /// <summary>
        /// Dtoの中身をコピーする
        /// T_UR_SH_ENTRYとT_UR_SH_DETAILのみデータコピーをする。
        /// それ以外は参照渡し
        /// </summary>
        /// <returns></returns>
        public DTOClass Clone()
        {
            DTOClass returnDto = new DTOClass();
            returnDto.entryEntity = this.urshEntityClone();
            List<T_UR_SH_DETAIL> copyList = new List<T_UR_SH_DETAIL>();
            foreach (var targetEntity in this.detailEntity)
            {
                copyList.Add(this.urshDetailClone(targetEntity));
            }
            returnDto.detailEntity = copyList.ToArray();
            //2次
            //コンテナ稼働実績
            List<T_CONTENA_RESULT> contenaCopyList = new List<T_CONTENA_RESULT>();
            foreach (var targetEntity in this.contenaResultList)
            {
                contenaCopyList.Add(this.ContenaClone(targetEntity));
            }
            returnDto.contenaResultList = contenaCopyList;

            returnDto.sysInfoEntity = this.sysInfoEntity;
            returnDto.torihikisakiSeikyuuEntity = this.torihikisakiSeikyuuEntity;
            returnDto.torihikisakiShiharaiEntity = this.torihikisakiShiharaiEntity;
            returnDto.contenaEntity = this.contenaEntity;
            returnDto.manifestShuruiEntity = this.manifestShuruiEntity;
            returnDto.manifestTehaiEntity = this.manifestTehaiEntity;
            returnDto.kyotenEntity = this.kyotenEntity;
            returnDto.numberDay = this.numberDay;
            returnDto.numberYear = this.numberYear;
            returnDto.numberReceipt = this.numberReceipt;
            returnDto.numberReceiptYear = this.numberReceiptYear;
            returnDto.manifestEntrys = this.manifestEntrys;
            returnDto.keitaiKbnEntity = this.keitaiKbnEntity;


            return returnDto;
        }

        /// <summary>
        /// 自身のT_UR_SH_ENTRYを複製する
        /// </summary>
        /// <returns></returns>
        private T_UR_SH_ENTRY urshEntityClone()
        {
            var returnEntity = new T_UR_SH_ENTRY();

            returnEntity.SYSTEM_ID = this.entryEntity.SYSTEM_ID;
            returnEntity.SEQ = this.entryEntity.SEQ;
            returnEntity.KYOTEN_CD = this.entryEntity.KYOTEN_CD;
            returnEntity.UR_SH_NUMBER = this.entryEntity.UR_SH_NUMBER;
            returnEntity.DATE_NUMBER = this.entryEntity.DATE_NUMBER;
            returnEntity.YEAR_NUMBER = this.entryEntity.YEAR_NUMBER;
            returnEntity.KAKUTEI_KBN = this.entryEntity.KAKUTEI_KBN;
            returnEntity.DENPYOU_DATE = this.entryEntity.DENPYOU_DATE;
            returnEntity.SEARCH_DENPYOU_DATE = this.entryEntity.SEARCH_DENPYOU_DATE;
            returnEntity.URIAGE_DATE = this.entryEntity.URIAGE_DATE;
            returnEntity.SEARCH_URIAGE_DATE = this.entryEntity.SEARCH_URIAGE_DATE;
            returnEntity.SHIHARAI_DATE = this.entryEntity.SHIHARAI_DATE;
            returnEntity.SEARCH_SHIHARAI_DATE = this.entryEntity.SEARCH_SHIHARAI_DATE;
            returnEntity.TORIHIKISAKI_CD = this.entryEntity.TORIHIKISAKI_CD;
            returnEntity.TORIHIKISAKI_NAME = this.entryEntity.TORIHIKISAKI_NAME;
            returnEntity.GYOUSHA_CD = this.entryEntity.GYOUSHA_CD;
            returnEntity.GYOUSHA_NAME = this.entryEntity.GYOUSHA_NAME;
            returnEntity.GENBA_CD = this.entryEntity.GENBA_CD;
            returnEntity.GENBA_NAME = this.entryEntity.GENBA_NAME;
            returnEntity.NIZUMI_GYOUSHA_CD = this.entryEntity.NIZUMI_GYOUSHA_CD;
            returnEntity.NIZUMI_GYOUSHA_NAME = this.entryEntity.NIZUMI_GYOUSHA_NAME;
            returnEntity.NIZUMI_GENBA_CD = this.entryEntity.NIZUMI_GENBA_CD;
            returnEntity.NIZUMI_GENBA_NAME = this.entryEntity.NIZUMI_GENBA_NAME;
            returnEntity.NIOROSHI_GYOUSHA_CD = this.entryEntity.NIOROSHI_GYOUSHA_CD;
            returnEntity.NIOROSHI_GYOUSHA_NAME = this.entryEntity.NIOROSHI_GYOUSHA_NAME;
            returnEntity.NIOROSHI_GENBA_CD = this.entryEntity.NIOROSHI_GENBA_CD;
            returnEntity.NIOROSHI_GENBA_NAME = this.entryEntity.NIOROSHI_GENBA_NAME;
            returnEntity.EIGYOU_TANTOUSHA_CD = this.entryEntity.EIGYOU_TANTOUSHA_CD;
            returnEntity.EIGYOU_TANTOUSHA_NAME = this.entryEntity.EIGYOU_TANTOUSHA_NAME;
            returnEntity.NYUURYOKU_TANTOUSHA_CD = this.entryEntity.NYUURYOKU_TANTOUSHA_CD;
            returnEntity.NYUURYOKU_TANTOUSHA_NAME = this.entryEntity.NYUURYOKU_TANTOUSHA_NAME;
            returnEntity.SHARYOU_CD = this.entryEntity.SHARYOU_CD;
            returnEntity.SHARYOU_NAME = this.entryEntity.SHARYOU_NAME;
            returnEntity.SHASHU_CD = this.entryEntity.SHASHU_CD;
            returnEntity.SHASHU_NAME = this.entryEntity.SHASHU_NAME;
            returnEntity.UNPAN_GYOUSHA_CD = this.entryEntity.UNPAN_GYOUSHA_CD;
            returnEntity.UNPAN_GYOUSHA_NAME = this.entryEntity.UNPAN_GYOUSHA_NAME;
            returnEntity.UNTENSHA_CD = this.entryEntity.UNTENSHA_CD;
            returnEntity.UNTENSHA_NAME = this.entryEntity.UNTENSHA_NAME;
            returnEntity.NINZUU_CNT = this.entryEntity.NINZUU_CNT;
            returnEntity.KEITAI_KBN_CD = this.entryEntity.KEITAI_KBN_CD;
            returnEntity.CONTENA_SOUSA_CD = this.entryEntity.CONTENA_SOUSA_CD;
            returnEntity.MANIFEST_SHURUI_CD = this.entryEntity.MANIFEST_SHURUI_CD;
            returnEntity.MANIFEST_TEHAI_CD = this.entryEntity.MANIFEST_TEHAI_CD;
            returnEntity.DENPYOU_BIKOU = this.entryEntity.DENPYOU_BIKOU;
            returnEntity.UKETSUKE_NUMBER = this.entryEntity.UKETSUKE_NUMBER;
            returnEntity.RECEIPT_NUMBER = this.entryEntity.RECEIPT_NUMBER;
            returnEntity.RECEIPT_NUMBER_YEAR = this.entryEntity.RECEIPT_NUMBER_YEAR;
            returnEntity.URIAGE_SHOUHIZEI_RATE = this.entryEntity.URIAGE_SHOUHIZEI_RATE;
            returnEntity.URIAGE_AMOUNT_TOTAL = this.entryEntity.URIAGE_AMOUNT_TOTAL;
            returnEntity.URIAGE_TAX_SOTO = this.entryEntity.URIAGE_TAX_SOTO;
            returnEntity.URIAGE_TAX_UCHI = this.entryEntity.URIAGE_TAX_UCHI;
            returnEntity.URIAGE_TAX_SOTO_TOTAL = this.entryEntity.URIAGE_TAX_SOTO_TOTAL;
            returnEntity.URIAGE_TAX_UCHI_TOTAL = this.entryEntity.URIAGE_TAX_UCHI_TOTAL;
            returnEntity.HINMEI_URIAGE_KINGAKU_TOTAL = this.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL;
            returnEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = this.entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL;
            returnEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = this.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL;
            returnEntity.SHIHARAI_SHOUHIZEI_RATE = this.entryEntity.SHIHARAI_SHOUHIZEI_RATE;
            returnEntity.SHIHARAI_AMOUNT_TOTAL = this.entryEntity.SHIHARAI_AMOUNT_TOTAL;
            returnEntity.SHIHARAI_TAX_SOTO = this.entryEntity.SHIHARAI_TAX_SOTO;
            returnEntity.SHIHARAI_TAX_UCHI = this.entryEntity.SHIHARAI_TAX_UCHI;
            returnEntity.SHIHARAI_TAX_SOTO_TOTAL = this.entryEntity.SHIHARAI_TAX_SOTO_TOTAL;
            returnEntity.SHIHARAI_TAX_UCHI_TOTAL = this.entryEntity.SHIHARAI_TAX_UCHI_TOTAL;
            returnEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = this.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL;
            returnEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = this.entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL;
            returnEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = this.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL;
            returnEntity.URIAGE_ZEI_KEISAN_KBN_CD = this.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD;
            returnEntity.URIAGE_ZEI_KBN_CD = this.entryEntity.URIAGE_ZEI_KBN_CD;
            returnEntity.URIAGE_TORIHIKI_KBN_CD = this.entryEntity.URIAGE_TORIHIKI_KBN_CD;
            returnEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = this.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD;
            returnEntity.SHIHARAI_ZEI_KBN_CD = this.entryEntity.SHIHARAI_ZEI_KBN_CD;
            returnEntity.SHIHARAI_TORIHIKI_KBN_CD = this.entryEntity.SHIHARAI_TORIHIKI_KBN_CD;
            returnEntity.TSUKI_CREATE_KBN = this.entryEntity.TSUKI_CREATE_KBN;
            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
            returnEntity.CREATE_DATE = this.entryEntity.CREATE_DATE;
            returnEntity.CREATE_PC = this.entryEntity.CREATE_PC;
            returnEntity.CREATE_USER = this.entryEntity.CREATE_USER;
            returnEntity.UPDATE_DATE = this.entryEntity.UPDATE_DATE;
            returnEntity.UPDATE_PC = this.entryEntity.UPDATE_PC;
            returnEntity.UPDATE_USER = this.entryEntity.UPDATE_USER;
            /// 20141118 Houkakou 「更新日、登録日の見直し」　end
            returnEntity.DELETE_FLG = this.entryEntity.DELETE_FLG;

            return returnEntity;
        }

        private T_UR_SH_DETAIL urshDetailClone(T_UR_SH_DETAIL copyTarget)
        {
            var returnEntity = new T_UR_SH_DETAIL();

            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.UR_SH_NUMBER = copyTarget.UR_SH_NUMBER;
            returnEntity.ROW_NO = copyTarget.ROW_NO;
            returnEntity.KAKUTEI_KBN = copyTarget.KAKUTEI_KBN;
            returnEntity.URIAGESHIHARAI_DATE = copyTarget.URIAGESHIHARAI_DATE;
            returnEntity.SEARCH_URIAGESHIHARAI_DATE = copyTarget.SEARCH_URIAGESHIHARAI_DATE;
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
            returnEntity.NISUGATA_SUURYOU = copyTarget.NISUGATA_SUURYOU;
            returnEntity.NISUGATA_UNIT_CD = copyTarget.NISUGATA_UNIT_CD;

            return returnEntity;
        }

        private T_CONTENA_RESULT ContenaClone(T_CONTENA_RESULT copyTarget)
        {
            T_CONTENA_RESULT returnEntity = new T_CONTENA_RESULT();

            returnEntity.DENSHU_KBN_CD = copyTarget.DENSHU_KBN_CD;
            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.CONTENA_SET_KBN = copyTarget.CONTENA_SET_KBN;
            returnEntity.CONTENA_SHURUI_CD = copyTarget.CONTENA_SHURUI_CD;
            returnEntity.CONTENA_CD = copyTarget.CONTENA_CD;
            returnEntity.DAISUU_CNT = copyTarget.DAISUU_CNT;
            returnEntity.TIME_STAMP = copyTarget.TIME_STAMP;
            returnEntity.DELETE_FLG = copyTarget.DELETE_FLG;

            return returnEntity;
        }

        #region Utility
        /// <summary>
        /// 自身のT_UR_SH_DETAILのDETAIL_SYSTEM_IDのリストを取得する
        /// </summary>
        /// <returns>T_UR_SH_DETAILが一件もない</returns>
        public List<SqlInt64> getDetailSysIds()
        {
            List<SqlInt64> returnList = new List<SqlInt64>();

            if (this.detailEntity == null || this.detailEntity.Length < 1)
            {
                return returnList;
            }

            foreach (T_UR_SH_DETAIL dtail in this.detailEntity)
            {
                returnList.Add(dtail.DETAIL_SYSTEM_ID);
            }

            return returnList;
        }
        #endregion
    }
}
