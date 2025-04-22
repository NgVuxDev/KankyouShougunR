using System;
using System.Collections.Generic;
using System.Data;
using r_framework.Entity;
using Shougun.Core.SalesPayment.DenpyouHakou;

namespace Shougun.Core.Scale.KeiryouNyuuryoku
{
    /// <summary>
    /// 計量入力DTO
    /// </summary>
    internal class DTOClass
    {
        /// <summary>
        /// T_KEIRYOU_ENTRY用のEntity
        /// </summary>
        internal T_KEIRYOU_ENTRY entryEntity;

        /// <summary>
        /// T_KEIRYOU_DETAIL
        /// </summary>
        internal T_KEIRYOU_DETAIL[] detailEntity;

        /// <summary>
        /// T_UKEIRE_JISSEKI_ENTRY
        /// </summary>
        internal T_UKEIRE_JISSEKI_ENTRY JentryEntity;

        /// <summary>
        /// T_UKEIRE_JISSEKI_DETAIL
        /// </summary>
        internal T_UKEIRE_JISSEKI_DETAIL[] JdetailEntity;

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
        /// M_CONTENA_JOUKYOU
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

        internal M_FILE_LINK_UKEIRE_JISSEKI_ENTRY[] fileLinkUJEList;

        internal T_FILE_DATA[] fileDataList;

        /// <summary>
        ///	領収書/仕切書_売上)課税金額	:	R_KAZEI_KINGAKU	
        /// </summary>		
        public String R_KAZEI_KINGAKU { get; set; }
        /// <summary>
        ///	領収書/仕切書_売上)非課税金額	:	R_HIKAZEI_KINGAKU	
        /// </summary>		
        public String R_HIKAZEI_KINGAKU { get; set; }
        /// <summary>
        ///	領収書/仕切書_売上)課税消費税	:	R_KAZEI_SHOUHIZEI	
        /// </summary>		
        public String R_KAZEI_SHOUHIZEI { get; set; }

        public DTOClass()
        {
            this.entryEntity = new T_KEIRYOU_ENTRY();
            this.detailEntity = new T_KEIRYOU_DETAIL[] { new T_KEIRYOU_DETAIL() };
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
            this.JentryEntity = new T_UKEIRE_JISSEKI_ENTRY();
            this.JdetailEntity = new T_UKEIRE_JISSEKI_DETAIL[] { new T_UKEIRE_JISSEKI_DETAIL() };
            this.fileLinkUJEList = new M_FILE_LINK_UKEIRE_JISSEKI_ENTRY[] { new M_FILE_LINK_UKEIRE_JISSEKI_ENTRY() };
            this.fileDataList = new T_FILE_DATA[] { new T_FILE_DATA() };

        }

        /// <summary>
        /// Dtoの中身をコピーする
        /// T_KEIRYOU_ENTRYとT_KEIRYOU_DETAILのみデータコピーをする。
        /// それ以外は参照渡し。
        /// もし上記以外の値コピーをしたい場合は適宜追加。
        /// </summary>
        /// <returns></returns>
        public DTOClass Clone()
        {
            DTOClass returnDto = new DTOClass();
            returnDto.entryEntity = this.KeiryouEntryClone();

            List<T_KEIRYOU_DETAIL> copyList = new List<T_KEIRYOU_DETAIL>();
            if (this.detailEntity != null)
            {
                foreach (var targetEntity in this.detailEntity)
                {
                    copyList.Add(this.KeiryouDetailClone(targetEntity));
                }
            }
            returnDto.detailEntity = copyList.ToArray();

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
            returnDto.JentryEntity = this.UkeireJissekiEntryClone();

            List<T_UKEIRE_JISSEKI_DETAIL> copyList2 = new List<T_UKEIRE_JISSEKI_DETAIL>();
            if (this.JdetailEntity != null)
            {
                foreach (var targetEntity in this.JdetailEntity)
                {
                    copyList2.Add(this.UkeireJissekiDetailClone(targetEntity));
                }
            }
            returnDto.JdetailEntity = copyList2.ToArray();



            return returnDto;
        }

        /// <summary>
        /// 自身のT_KEIRYOU_ENTRYを複製する
        /// </summary>
        /// <returns></returns>
        private T_KEIRYOU_ENTRY KeiryouEntryClone()
        {
            var returnEntity = new T_KEIRYOU_ENTRY();

            returnEntity.SYSTEM_ID = this.entryEntity.SYSTEM_ID;
            returnEntity.SEQ = this.entryEntity.SEQ;
            returnEntity.TAIRYUU_KBN = this.entryEntity.TAIRYUU_KBN;
            returnEntity.KYOTEN_CD = this.entryEntity.KYOTEN_CD;
            returnEntity.KEIRYOU_NUMBER = this.entryEntity.KEIRYOU_NUMBER;
            returnEntity.DATE_NUMBER = this.entryEntity.DATE_NUMBER;
            returnEntity.YEAR_NUMBER = this.entryEntity.YEAR_NUMBER;
            returnEntity.KIHON_KEIRYOU = this.entryEntity.KIHON_KEIRYOU;
            returnEntity.DENPYOU_DATE = this.entryEntity.DENPYOU_DATE;
            returnEntity.SEARCH_DENPYOU_DATE = this.entryEntity.SEARCH_DENPYOU_DATE;
            returnEntity.KEIRYOU_KBN = this.entryEntity.KEIRYOU_KBN;
            returnEntity.GYOUSHA_CD = this.entryEntity.GYOUSHA_CD;
            returnEntity.GYOUSHA_NAME = this.entryEntity.GYOUSHA_NAME;
            returnEntity.GENBA_CD = this.entryEntity.GENBA_CD;
            returnEntity.GENBA_NAME = this.entryEntity.GENBA_NAME;
            returnEntity.UNPAN_GYOUSHA_CD = this.entryEntity.UNPAN_GYOUSHA_CD;
            returnEntity.UNPAN_GYOUSHA_NAME = this.entryEntity.UNPAN_GYOUSHA_NAME;
            returnEntity.NYUURYOKU_TANTOUSHA_CD = this.entryEntity.NYUURYOKU_TANTOUSHA_CD;
            returnEntity.NYUURYOKU_TANTOUSHA_NAME = this.entryEntity.NYUURYOKU_TANTOUSHA_NAME;
            returnEntity.SHARYOU_CD = this.entryEntity.SHARYOU_CD;
            returnEntity.SHARYOU_NAME = this.entryEntity.SHARYOU_NAME;
            returnEntity.SHASHU_CD = this.entryEntity.SHASHU_CD;
            returnEntity.SHASHU_NAME = this.entryEntity.SHASHU_NAME;
            returnEntity.UNTENSHA_CD = this.entryEntity.UNTENSHA_CD;
            returnEntity.UNTENSHA_NAME = this.entryEntity.UNTENSHA_NAME;
            returnEntity.STACK_JYUURYOU = this.entryEntity.STACK_JYUURYOU;
            returnEntity.STACK_KEIRYOU_TIME = this.entryEntity.STACK_KEIRYOU_TIME;
            returnEntity.EMPTY_JYUURYOU = this.entryEntity.EMPTY_JYUURYOU;
            returnEntity.EMPTY_KEIRYOU_TIME = this.entryEntity.EMPTY_KEIRYOU_TIME;
            returnEntity.DENPYOU_BIKOU = this.entryEntity.DENPYOU_BIKOU;
            returnEntity.TAIRYUU_BIKOU = this.entryEntity.TAIRYUU_BIKOU;
            returnEntity.RECEIPT_NUMBER = this.entryEntity.RECEIPT_NUMBER;
            returnEntity.RECEIPT_NUMBER_YEAR = this.entryEntity.RECEIPT_NUMBER_YEAR;
            returnEntity.HAIKI_KBN_CD = this.entryEntity.HAIKI_KBN_CD;
            returnEntity.HST_GYOUSHA_CD = this.entryEntity.HST_GYOUSHA_CD;
            returnEntity.HST_GENBA_CD = this.entryEntity.HST_GENBA_CD;
            returnEntity.SBN_GYOUSHA_CD = this.entryEntity.SBN_GYOUSHA_CD;
            returnEntity.SBN_GENBA_CD = this.entryEntity.SBN_GENBA_CD;
            returnEntity.LAST_SBN_GYOUSHA_CD = this.entryEntity.LAST_SBN_GYOUSHA_CD;
            returnEntity.LAST_SBN_GENBA_CD = this.entryEntity.LAST_SBN_GENBA_CD;
            returnEntity.EIGYOU_TANTOUSHA_CD = this.entryEntity.EIGYOU_TANTOUSHA_CD;
            returnEntity.EIGYOU_TANTOUSHA_NAME = this.entryEntity.EIGYOU_TANTOUSHA_NAME;
            returnEntity.NIZUMI_GYOUSHA_CD = this.entryEntity.NIZUMI_GYOUSHA_CD;
            returnEntity.NIZUMI_GYOUSHA_NAME = this.entryEntity.NIZUMI_GYOUSHA_NAME;
            returnEntity.NIZUMI_GENBA_CD = this.entryEntity.NIZUMI_GENBA_CD;
            returnEntity.NIZUMI_GENBA_NAME = this.entryEntity.NIZUMI_GENBA_NAME;
            returnEntity.NIOROSHI_GYOUSHA_CD = this.entryEntity.NIOROSHI_GYOUSHA_CD;
            returnEntity.NIOROSHI_GYOUSHA_NAME = this.entryEntity.NIOROSHI_GYOUSHA_NAME;
            returnEntity.NIOROSHI_GENBA_CD = this.entryEntity.NIOROSHI_GENBA_CD;
            returnEntity.NIOROSHI_GENBA_NAME = this.entryEntity.NIOROSHI_GENBA_NAME;
            returnEntity.DAIKAN_KBN = this.entryEntity.DAIKAN_KBN;
            returnEntity.KEITAI_KBN_CD = this.entryEntity.KEITAI_KBN_CD;
            returnEntity.MANIFEST_SHURUI_CD = this.entryEntity.MANIFEST_SHURUI_CD;
            returnEntity.MANIFEST_TEHAI_CD = this.entryEntity.MANIFEST_TEHAI_CD;
            returnEntity.TORIHIKISAKI_CD = this.entryEntity.TORIHIKISAKI_CD;
            returnEntity.TORIHIKISAKI_NAME = this.entryEntity.TORIHIKISAKI_NAME;
            returnEntity.URIAGE_ZEI_KEISAN_KBN_CD = this.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD;
            returnEntity.URIAGE_ZEI_KBN_CD = this.entryEntity.URIAGE_ZEI_KBN_CD;
            returnEntity.URIAGE_TORIHIKI_KBN_CD = this.entryEntity.URIAGE_TORIHIKI_KBN_CD;
            returnEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = this.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD;
            returnEntity.SHIHARAI_ZEI_KBN_CD = this.entryEntity.SHIHARAI_ZEI_KBN_CD;
            returnEntity.SHIHARAI_TORIHIKI_KBN_CD = this.entryEntity.SHIHARAI_TORIHIKI_KBN_CD;
            returnEntity.NET_TOTAL = this.entryEntity.NET_TOTAL;
            returnEntity.URIAGE_SHOUHIZEI_RATE = this.entryEntity.URIAGE_SHOUHIZEI_RATE;
            returnEntity.URIAGE_KINGAKU_TOTAL = this.entryEntity.URIAGE_KINGAKU_TOTAL;
            returnEntity.URIAGE_TAX_SOTO = this.entryEntity.URIAGE_TAX_SOTO;
            returnEntity.URIAGE_TAX_UCHI = this.entryEntity.URIAGE_TAX_UCHI;
            returnEntity.URIAGE_TAX_SOTO_TOTAL = this.entryEntity.URIAGE_TAX_SOTO_TOTAL;
            returnEntity.URIAGE_TAX_UCHI_TOTAL = this.entryEntity.URIAGE_TAX_UCHI_TOTAL;
            returnEntity.HINMEI_URIAGE_KINGAKU_TOTAL = this.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL;
            returnEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = this.entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL;
            returnEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = this.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL;
            returnEntity.SHIHARAI_SHOUHIZEI_RATE = this.entryEntity.SHIHARAI_SHOUHIZEI_RATE;
            returnEntity.SHIHARAI_KINGAKU_TOTAL = this.entryEntity.SHIHARAI_KINGAKU_TOTAL;
            returnEntity.SHIHARAI_TAX_SOTO = this.entryEntity.SHIHARAI_TAX_SOTO;
            returnEntity.SHIHARAI_TAX_UCHI = this.entryEntity.SHIHARAI_TAX_UCHI;
            returnEntity.SHIHARAI_TAX_SOTO_TOTAL = this.entryEntity.SHIHARAI_TAX_SOTO_TOTAL;
            returnEntity.SHIHARAI_TAX_UCHI_TOTAL = this.entryEntity.SHIHARAI_TAX_UCHI_TOTAL;
            returnEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = this.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL;
            returnEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = this.entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL;
            returnEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = this.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL;
            returnEntity.CREATE_USER = this.entryEntity.CREATE_USER;
            returnEntity.CREATE_DATE = this.entryEntity.CREATE_DATE;
            returnEntity.CREATE_PC = this.entryEntity.CREATE_PC;
            returnEntity.UPDATE_USER = this.entryEntity.UPDATE_USER;
            returnEntity.UPDATE_DATE = this.entryEntity.UPDATE_DATE;
            returnEntity.UPDATE_PC = this.entryEntity.UPDATE_PC;

            return returnEntity;
        }

        private T_KEIRYOU_DETAIL KeiryouDetailClone(T_KEIRYOU_DETAIL copyTarget)
        {
            var returnEntity = new T_KEIRYOU_DETAIL();
            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.KEIRYOU_NUMBER = copyTarget.KEIRYOU_NUMBER;
            returnEntity.ROW_NO = copyTarget.ROW_NO;
            returnEntity.DENPYOU_DATE = copyTarget.DENPYOU_DATE;
            returnEntity.STACK_JYUURYOU = copyTarget.STACK_JYUURYOU;
            returnEntity.EMPTY_JYUURYOU = copyTarget.EMPTY_JYUURYOU;
            returnEntity.CHOUSEI_JYUURYOU = copyTarget.CHOUSEI_JYUURYOU;
            returnEntity.CHOUSEI_PERCENT = copyTarget.CHOUSEI_PERCENT;
            returnEntity.YOUKI_CD = copyTarget.YOUKI_CD;
            returnEntity.YOUKI_SUURYOU = copyTarget.YOUKI_SUURYOU;
            returnEntity.YOUKI_JYUURYOU = copyTarget.YOUKI_JYUURYOU;
            returnEntity.DENPYOU_KBN_CD = copyTarget.DENPYOU_KBN_CD;
            returnEntity.HINMEI_CD = copyTarget.HINMEI_CD;
            returnEntity.HINMEI_NAME = copyTarget.HINMEI_NAME;
            returnEntity.NET_JYUURYOU = copyTarget.NET_JYUURYOU;
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
            returnEntity.KEIRYOU_TIME = copyTarget.KEIRYOU_TIME;

            return returnEntity;
        }


        /// <summary>
        /// 自身のT_UKEIRE_JISSEKI_ENTRYを複製する
        /// </summary>
        /// <returns></returns>
        private T_UKEIRE_JISSEKI_ENTRY UkeireJissekiEntryClone()
        {
            var returnEntity = new T_UKEIRE_JISSEKI_ENTRY();

            returnEntity.DENPYOU_SHURUI = this.JentryEntity.DENPYOU_SHURUI;
            returnEntity.DENPYOU_SYSTEM_ID = this.JentryEntity.DENPYOU_SYSTEM_ID;
            returnEntity.SEQ = this.JentryEntity.SEQ;
            returnEntity.SAGYOU_DATE = this.JentryEntity.SAGYOU_DATE;
            returnEntity.SAGYOU_TIME = this.JentryEntity.SAGYOU_TIME;
            returnEntity.SAGYOUSHA_CD = this.JentryEntity.SAGYOUSHA_CD;
            returnEntity.SAGYOUSHA_NAME = this.JentryEntity.SAGYOUSHA_NAME;
            returnEntity.SAGYOU_BIKOU = this.JentryEntity.SAGYOU_BIKOU;
            returnEntity.CREATE_USER = this.JentryEntity.CREATE_USER;
            returnEntity.CREATE_DATE = this.JentryEntity.CREATE_DATE;
            returnEntity.CREATE_PC = this.JentryEntity.CREATE_PC;
            returnEntity.UPDATE_USER = this.JentryEntity.UPDATE_USER;
            returnEntity.UPDATE_DATE = this.JentryEntity.UPDATE_DATE;
            returnEntity.UPDATE_PC = this.JentryEntity.UPDATE_PC;

            return returnEntity;
        }

        private T_UKEIRE_JISSEKI_DETAIL UkeireJissekiDetailClone(T_UKEIRE_JISSEKI_DETAIL copyTarget)
        {
            var returnEntity = new T_UKEIRE_JISSEKI_DETAIL();
            returnEntity.DENPYOU_SHURUI = copyTarget.DENPYOU_SHURUI;
            returnEntity.DENPYOU_SYSTEM_ID = copyTarget.DENPYOU_SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.DETAIL_SEQ = copyTarget.DETAIL_SEQ;
            returnEntity.HINMEI_CD = copyTarget.HINMEI_CD;
            returnEntity.SUURYOU_WARIAI = copyTarget.SUURYOU_WARIAI;
            returnEntity.CREATE_USER = copyTarget.CREATE_USER;
            returnEntity.CREATE_DATE = copyTarget.CREATE_DATE;
            returnEntity.CREATE_PC = copyTarget.CREATE_PC;
            returnEntity.UPDATE_USER = copyTarget.UPDATE_USER;
            returnEntity.UPDATE_DATE = copyTarget.UPDATE_DATE;
            returnEntity.UPDATE_PC = copyTarget.UPDATE_PC;
            return returnEntity;
        }

    }
}