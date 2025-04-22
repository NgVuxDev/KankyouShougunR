using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using GrapeCity.Win.MultiRow;
using r_framework.Entity;

namespace Shougun.Core.SalesPayment.UkeireNyuuryoku
{
    /// <summary>
    /// 受入入力DTO
    /// </summary>
    internal class DTOClass
    {
        /// <summary>
        /// T_UKEIRE_ENTRY用のEntity
        /// </summary>
        internal T_UKEIRE_ENTRY entryEntity;

        /// <summary>
        /// T_UKEIRE_DETAIL
        /// </summary>
        internal T_UKEIRE_DETAIL[] detailEntity;

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

        internal List<T_CONTENA_RESULT> contenaResultList;

        internal List<T_CONTENA_RESERVE> contenaReserveList;

        internal List<M_CONTENA> contenaMasterList;

        internal Dictionary<T_UKEIRE_DETAIL, List<T_ZAIKO_UKEIRE_DETAIL>> detailZaikoUkeireDetails;
        internal Dictionary<T_UKEIRE_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>> detailZaikoHinmeiHuriwakes;
        //Dictionary関連修正
        internal Dictionary<Row, List<T_ZAIKO_UKEIRE_DETAIL>> rowZaikoUkeireDetails;
        internal Dictionary<Row, List<T_ZAIKO_HINMEI_HURIWAKE>> rowZaikoHinmeiHuriwakes;

        //internal List<String> contenaTimeStamp;
        //internal List<String> zaikoTimeStamp;

        internal M_FILE_LINK_UKEIRE_JISSEKI_ENTRY[] fileLinkUJEList;

        internal T_FILE_DATA[] fileDataList;

        public DTOClass()
        {
            this.entryEntity = new T_UKEIRE_ENTRY();
            this.detailEntity = new T_UKEIRE_DETAIL[] { new T_UKEIRE_DETAIL() };
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
            //this.contenaTimeStamp = new List<String>();
            //this.zaikoTimeStamp = new List<String>();
            //this.detailZaikoUkeireDetails = new List<List<T_ZAIKO_UKEIRE_DETAIL>>();
            this.detailZaikoUkeireDetails = new Dictionary<T_UKEIRE_DETAIL, List<T_ZAIKO_UKEIRE_DETAIL>>();
            //Dictionary関連修正
            this.rowZaikoUkeireDetails = new Dictionary<Row, List<T_ZAIKO_UKEIRE_DETAIL>>();
            // No.4578-->
            // 20150408 go 在庫品名振分処理追加 Start
            this.detailZaikoHinmeiHuriwakes = new Dictionary<T_UKEIRE_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>>();
            this.rowZaikoHinmeiHuriwakes = new Dictionary<Row, List<T_ZAIKO_HINMEI_HURIWAKE>>();
            // 20150409 go 在庫品名振分処理追加 End
            // No.4578<--
            this.JentryEntity = new T_UKEIRE_JISSEKI_ENTRY();
            this.JdetailEntity = new T_UKEIRE_JISSEKI_DETAIL[] { new T_UKEIRE_JISSEKI_DETAIL() };
            this.fileLinkUJEList = new M_FILE_LINK_UKEIRE_JISSEKI_ENTRY[] { new M_FILE_LINK_UKEIRE_JISSEKI_ENTRY() };
            this.fileDataList = new T_FILE_DATA[] { new T_FILE_DATA() };
        }

        /// <summary>
        /// Dtoの中身をコピーする
        /// T_UKEIRE_ENTRYとT_UKEIRE_DETAILのみデータコピーをする。
        /// それ以外は参照渡し。
        /// もし上記以外の値コピーをしたい場合は適宜追加。
        /// </summary>
        /// <returns></returns>
        public DTOClass Clone()
        {
            DTOClass returnDto = new DTOClass();
            returnDto.entryEntity = this.UkeireEntryClone();

            List<T_UKEIRE_DETAIL> copyList = new List<T_UKEIRE_DETAIL>();
            if (this.detailEntity != null)
            {
                foreach (var targetEntity in this.detailEntity)
                {
                    copyList.Add(this.UkeireDetailClone(targetEntity));
                }
            }
            returnDto.detailEntity = copyList.ToArray();

            // 2次
            // コンテナ稼働実績
            List<T_CONTENA_RESULT> contenaCopyList = new List<T_CONTENA_RESULT>();
            foreach (var targetEntity in this.contenaResultList)
            {
                contenaCopyList.Add(this.ContenaClone(targetEntity));
            }
            returnDto.contenaResultList = contenaCopyList;

            // 在庫明細のList
            var detailZaikoUkeireDetailsCopy = new Dictionary<T_UKEIRE_DETAIL, List<T_ZAIKO_UKEIRE_DETAIL>>();
            foreach (var targetDetails in this.detailZaikoUkeireDetails)
            {
                var zaikoUkeireDetailsCopy = new List<T_ZAIKO_UKEIRE_DETAIL>();
                foreach (var targetEntity in targetDetails.Value)
                {
                    zaikoUkeireDetailsCopy.Add(this.ZaikoUkeireDetailClone(targetEntity));
                }
                detailZaikoUkeireDetailsCopy[targetDetails.Key] = zaikoUkeireDetailsCopy;
            }
            returnDto.detailZaikoUkeireDetails = detailZaikoUkeireDetailsCopy;
            // Dictionary関連修正
            // 在庫明細のDictionary
            var rowZaikoUkeireDetailsCopy = new Dictionary<Row, List<T_ZAIKO_UKEIRE_DETAIL>>();
            foreach (var targetDetails in this.rowZaikoUkeireDetails)
            {
                var zaikoUkeireDetailsCopy = new List<T_ZAIKO_UKEIRE_DETAIL>();
                foreach (var targetEntity in targetDetails.Value)
                {
                    zaikoUkeireDetailsCopy.Add(this.ZaikoUkeireDetailClone(targetEntity));
                }
                rowZaikoUkeireDetailsCopy[targetDetails.Key] = zaikoUkeireDetailsCopy;
            }
            returnDto.rowZaikoUkeireDetails = rowZaikoUkeireDetailsCopy;

            // No.4578-->
            // 20150410 go 在庫品名振分処理追加 Start
            var detailZaikoHinmeiHuriwakesCopy = new Dictionary<T_UKEIRE_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>>();
            foreach (var targetDetails in this.detailZaikoHinmeiHuriwakes)
            {
                var zaikoHinmeiHuriwakesCopy = new List<T_ZAIKO_HINMEI_HURIWAKE>();
                foreach (var targetEntity in targetDetails.Value)
                {
                    zaikoHinmeiHuriwakesCopy.Add(this.ZaikoHinmeiHuriwakeClone(targetEntity));
                }
                detailZaikoHinmeiHuriwakesCopy[targetDetails.Key] = zaikoHinmeiHuriwakesCopy;
            }
            returnDto.detailZaikoHinmeiHuriwakes = detailZaikoHinmeiHuriwakesCopy;

            var rowZaikoHinmeiHuriwakesCopy = new Dictionary<Row, List<T_ZAIKO_HINMEI_HURIWAKE>>();
            foreach (var targetDetails in this.rowZaikoHinmeiHuriwakes)
            {
                var zaikoHinmeiHuriwakesCopy = new List<T_ZAIKO_HINMEI_HURIWAKE>();
                foreach (var targetEntity in targetDetails.Value)
                {
                    zaikoHinmeiHuriwakesCopy.Add(this.ZaikoHinmeiHuriwakeClone(targetEntity));
                }
                rowZaikoHinmeiHuriwakesCopy[targetDetails.Key] = zaikoHinmeiHuriwakesCopy;
            }
            returnDto.rowZaikoHinmeiHuriwakes = rowZaikoHinmeiHuriwakesCopy;
            // 20150410 go 在庫品名振分処理追加 End
            // No.4578<--

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
        /// 自身のT_UKEIRE_ENTRYを複製する
        /// </summary>
        /// <returns></returns>
        private T_UKEIRE_ENTRY UkeireEntryClone()
        {
            var returnEntity = new T_UKEIRE_ENTRY();

            returnEntity.CONTENA_SOUSA_CD = this.entryEntity.CONTENA_SOUSA_CD;
            returnEntity.DAIKAN_KBN = this.entryEntity.DAIKAN_KBN;
            returnEntity.DATE_NUMBER = this.entryEntity.DATE_NUMBER;
            returnEntity.DELETE_FLG = this.entryEntity.DELETE_FLG;
            returnEntity.DENPYOU_BIKOU = this.entryEntity.DENPYOU_BIKOU;
            returnEntity.DENPYOU_DATE = this.entryEntity.DENPYOU_DATE;
            returnEntity.EIGYOU_TANTOUSHA_CD = this.entryEntity.EIGYOU_TANTOUSHA_CD;
            returnEntity.EIGYOU_TANTOUSHA_NAME = this.entryEntity.EIGYOU_TANTOUSHA_NAME;
            returnEntity.GENBA_CD = this.entryEntity.GENBA_CD;
            returnEntity.GENBA_NAME = this.entryEntity.GENBA_NAME;
            returnEntity.GYOUSHA_CD = this.entryEntity.GYOUSHA_CD;
            returnEntity.GYOUSHA_NAME = this.entryEntity.GYOUSHA_NAME;
            returnEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = this.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL;
            returnEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = this.entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL;
            returnEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = this.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL;
            returnEntity.HINMEI_URIAGE_KINGAKU_TOTAL = this.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL;
            returnEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = this.entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL;
            returnEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = this.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL;
            returnEntity.KAKUTEI_KBN = this.entryEntity.KAKUTEI_KBN;
            returnEntity.KEIRYOU_NUMBER = this.entryEntity.KEIRYOU_NUMBER;
            returnEntity.KEITAI_KBN_CD = this.entryEntity.KEITAI_KBN_CD;
            returnEntity.KYOTEN_CD = this.entryEntity.KYOTEN_CD;
            returnEntity.MANIFEST_SHURUI_CD = this.entryEntity.MANIFEST_SHURUI_CD;
            returnEntity.MANIFEST_TEHAI_CD = this.entryEntity.MANIFEST_TEHAI_CD;
            returnEntity.NET_TOTAL = this.entryEntity.NET_TOTAL;
            returnEntity.NINZUU_CNT = this.entryEntity.NINZUU_CNT;
            returnEntity.NIOROSHI_GENBA_CD = this.entryEntity.NIOROSHI_GENBA_CD;
            returnEntity.NIOROSHI_GENBA_NAME = this.entryEntity.NIOROSHI_GENBA_NAME;
            returnEntity.NIOROSHI_GYOUSHA_CD = this.entryEntity.NIOROSHI_GYOUSHA_CD;
            returnEntity.NIOROSHI_GYOUSHA_NAME = this.entryEntity.NIOROSHI_GYOUSHA_NAME;
            returnEntity.NYUURYOKU_TANTOUSHA_CD = this.entryEntity.NYUURYOKU_TANTOUSHA_CD;
            returnEntity.NYUURYOKU_TANTOUSHA_NAME = this.entryEntity.NYUURYOKU_TANTOUSHA_NAME;
            returnEntity.RECEIPT_NUMBER = this.entryEntity.RECEIPT_NUMBER;
            returnEntity.RECEIPT_NUMBER_YEAR = this.entryEntity.RECEIPT_NUMBER_YEAR;
            returnEntity.SEARCH_DENPYOU_DATE = this.entryEntity.SEARCH_DENPYOU_DATE;
            returnEntity.SEARCH_SHIHARAI_DATE = this.entryEntity.SEARCH_SHIHARAI_DATE;
            returnEntity.SEARCH_URIAGE_DATE = this.entryEntity.SEARCH_URIAGE_DATE;
            returnEntity.SEQ = this.entryEntity.SEQ;
            returnEntity.SHARYOU_CD = this.entryEntity.SHARYOU_CD;
            returnEntity.SHARYOU_NAME = this.entryEntity.SHARYOU_NAME;
            returnEntity.SHASHU_CD = this.entryEntity.SHASHU_CD;
            returnEntity.SHASHU_NAME = this.entryEntity.SHASHU_NAME;
            returnEntity.SHIHARAI_DATE = this.entryEntity.SHIHARAI_DATE;
            returnEntity.SHIHARAI_KINGAKU_TOTAL = this.entryEntity.SHIHARAI_KINGAKU_TOTAL;
            returnEntity.SHIHARAI_SHOUHIZEI_RATE = this.entryEntity.SHIHARAI_SHOUHIZEI_RATE;
            returnEntity.SHIHARAI_TAX_SOTO = this.entryEntity.SHIHARAI_TAX_SOTO;
            returnEntity.SHIHARAI_TAX_SOTO_TOTAL = this.entryEntity.SHIHARAI_TAX_SOTO_TOTAL;
            returnEntity.SHIHARAI_TAX_UCHI = this.entryEntity.SHIHARAI_TAX_UCHI;
            returnEntity.SHIHARAI_TAX_UCHI_TOTAL = this.entryEntity.SHIHARAI_TAX_UCHI_TOTAL;
            returnEntity.SHIHARAI_TORIHIKI_KBN_CD = this.entryEntity.SHIHARAI_TORIHIKI_KBN_CD;
            returnEntity.SHIHARAI_ZEI_KBN_CD = this.entryEntity.SHIHARAI_ZEI_KBN_CD;
            returnEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = this.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD;
            returnEntity.SYSTEM_ID = this.entryEntity.SYSTEM_ID;
            returnEntity.TAIRYUU_BIKOU = this.entryEntity.TAIRYUU_BIKOU;
            returnEntity.TAIRYUU_KBN = this.entryEntity.TAIRYUU_KBN;
            returnEntity.TORIHIKISAKI_CD = this.entryEntity.TORIHIKISAKI_CD;
            returnEntity.TORIHIKISAKI_NAME = this.entryEntity.TORIHIKISAKI_NAME;
            returnEntity.UKEIRE_NUMBER = this.entryEntity.UKEIRE_NUMBER;
            returnEntity.UKETSUKE_NUMBER = this.entryEntity.UKETSUKE_NUMBER;
            returnEntity.UNPAN_GYOUSHA_CD = this.entryEntity.UNPAN_GYOUSHA_CD;
            returnEntity.UNPAN_GYOUSHA_NAME = this.entryEntity.UNPAN_GYOUSHA_NAME;
            returnEntity.UNTENSHA_CD = this.entryEntity.UNTENSHA_CD;
            returnEntity.UNTENSHA_NAME = this.entryEntity.UNTENSHA_NAME;
            returnEntity.URIAGE_DATE = this.entryEntity.URIAGE_DATE;
            returnEntity.URIAGE_KINGAKU_TOTAL = this.entryEntity.URIAGE_KINGAKU_TOTAL;
            returnEntity.URIAGE_SHOUHIZEI_RATE = this.entryEntity.URIAGE_SHOUHIZEI_RATE;
            returnEntity.URIAGE_TAX_SOTO = this.entryEntity.URIAGE_TAX_SOTO;
            returnEntity.URIAGE_TAX_SOTO_TOTAL = this.entryEntity.URIAGE_TAX_SOTO_TOTAL;
            returnEntity.URIAGE_TAX_UCHI = this.entryEntity.URIAGE_TAX_UCHI;
            returnEntity.URIAGE_TAX_UCHI_TOTAL = this.entryEntity.URIAGE_TAX_UCHI_TOTAL;
            returnEntity.URIAGE_TORIHIKI_KBN_CD = this.entryEntity.URIAGE_TORIHIKI_KBN_CD;
            returnEntity.URIAGE_ZEI_KBN_CD = this.entryEntity.URIAGE_ZEI_KBN_CD;
            returnEntity.URIAGE_ZEI_KEISAN_KBN_CD = this.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD;
            returnEntity.YEAR_NUMBER = this.entryEntity.YEAR_NUMBER;
            returnEntity.CREATE_USER = this.entryEntity.CREATE_USER;
            returnEntity.CREATE_DATE = this.entryEntity.CREATE_DATE;
            returnEntity.CREATE_PC = this.entryEntity.CREATE_PC;
            returnEntity.UPDATE_USER = this.entryEntity.UPDATE_USER;
            returnEntity.UPDATE_DATE = this.entryEntity.UPDATE_DATE;
            returnEntity.UPDATE_PC = this.entryEntity.UPDATE_PC;
            returnEntity.STACK_JYUURYOU = this.entryEntity.STACK_JYUURYOU;
            returnEntity.STACK_KEIRYOU_TIME = this.entryEntity.STACK_KEIRYOU_TIME;
            returnEntity.EMPTY_JYUURYOU = this.entryEntity.EMPTY_JYUURYOU;
            returnEntity.EMPTY_KEIRYOU_TIME = this.entryEntity.EMPTY_KEIRYOU_TIME;
            returnEntity.KINGAKU_TOTAL = this.entryEntity.KINGAKU_TOTAL;

            return returnEntity;
        }

        private T_UKEIRE_DETAIL UkeireDetailClone(T_UKEIRE_DETAIL copyTarget)
        {
            var returnEntity = new T_UKEIRE_DETAIL();

            returnEntity.YOUKI_SUURYOU = copyTarget.YOUKI_SUURYOU;
            returnEntity.YOUKI_JYUURYOU = copyTarget.YOUKI_JYUURYOU;
            returnEntity.YOUKI_CD = copyTarget.YOUKI_CD;
            returnEntity.WARIFURI_PERCENT = copyTarget.WARIFURI_PERCENT;
            returnEntity.WARIFURI_JYUURYOU = copyTarget.WARIFURI_JYUURYOU;
            returnEntity.URIAGESHIHARAI_DATE = copyTarget.URIAGESHIHARAI_DATE;
            returnEntity.UNIT_CD = copyTarget.UNIT_CD;
            returnEntity.UKEIRE_NUMBER = copyTarget.UKEIRE_NUMBER;
            returnEntity.TAX_UCHI = copyTarget.TAX_UCHI;
            returnEntity.TAX_SOTO = copyTarget.TAX_SOTO;
            returnEntity.TANKA = copyTarget.TANKA;
            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.SUURYOU = copyTarget.SUURYOU;
            returnEntity.STACK_JYUURYOU = copyTarget.STACK_JYUURYOU;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.SEARCH_URIAGESHIHARAI_DATE = copyTarget.SEARCH_URIAGESHIHARAI_DATE;
            returnEntity.ROW_NO = copyTarget.ROW_NO;
            returnEntity.NISUGATA_UNIT_CD = copyTarget.NISUGATA_UNIT_CD;
            returnEntity.NISUGATA_SUURYOU = copyTarget.NISUGATA_SUURYOU;
            returnEntity.NET_JYUURYOU = copyTarget.NET_JYUURYOU;
            returnEntity.MEISAI_BIKOU = copyTarget.MEISAI_BIKOU;
            returnEntity.KINGAKU = copyTarget.KINGAKU;
            returnEntity.KAKUTEI_KBN = copyTarget.KAKUTEI_KBN;
            returnEntity.HINMEI_ZEI_KBN_CD = copyTarget.HINMEI_ZEI_KBN_CD;
            returnEntity.HINMEI_TAX_UCHI = copyTarget.HINMEI_TAX_UCHI;
            returnEntity.HINMEI_TAX_SOTO = copyTarget.HINMEI_TAX_SOTO;
            returnEntity.HINMEI_NAME = copyTarget.HINMEI_NAME;
            returnEntity.HINMEI_KINGAKU = copyTarget.HINMEI_KINGAKU;
            returnEntity.HINMEI_CD = copyTarget.HINMEI_CD;
            returnEntity.EMPTY_JYUURYOU = copyTarget.EMPTY_JYUURYOU;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.DENPYOU_KBN_CD = copyTarget.DENPYOU_KBN_CD;
            returnEntity.CHOUSEI_PERCENT = copyTarget.CHOUSEI_PERCENT;
            returnEntity.CHOUSEI_JYUURYOU = copyTarget.CHOUSEI_JYUURYOU;

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
            returnEntity.DELETE_FLG = copyTarget.DELETE_FLG;
            returnEntity.TIME_STAMP = copyTarget.TIME_STAMP;

            return returnEntity;
        }

        private T_ZAIKO_UKEIRE_DETAIL ZaikoUkeireDetailClone(T_ZAIKO_UKEIRE_DETAIL copyTarget)
        {
            T_ZAIKO_UKEIRE_DETAIL returnEntity = new T_ZAIKO_UKEIRE_DETAIL();

            returnEntity.DENSHU_KBN_CD = copyTarget.DENSHU_KBN_CD;
            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.ROW_NO = copyTarget.ROW_NO;
            returnEntity.GYOUSHA_CD = copyTarget.GYOUSHA_CD;
            returnEntity.GENBA_CD = copyTarget.GENBA_CD;
            returnEntity.ZAIKO_HINMEI_CD = copyTarget.ZAIKO_HINMEI_CD;
            returnEntity.ZAIKO_RITSU = copyTarget.ZAIKO_RITSU;
            returnEntity.JYUURYOU = copyTarget.JYUURYOU;
            returnEntity.TANKA = copyTarget.TANKA;
            returnEntity.KINGAKU = copyTarget.KINGAKU;
            returnEntity.TIME_STAMP = copyTarget.TIME_STAMP;
            returnEntity.DELETE_FLG = copyTarget.DELETE_FLG;

            return returnEntity;
        }

        // No.4578-->
        // 20150410 go 在庫品名振分処理追加 Start
        private T_ZAIKO_HINMEI_HURIWAKE ZaikoHinmeiHuriwakeClone(T_ZAIKO_HINMEI_HURIWAKE copyTarget)
        {
            T_ZAIKO_HINMEI_HURIWAKE returnEntity = new T_ZAIKO_HINMEI_HURIWAKE();

            returnEntity.DENSHU_KBN_CD = copyTarget.DENSHU_KBN_CD;
            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.ZAIKO_HINMEI_CD = copyTarget.ZAIKO_HINMEI_CD;
            returnEntity.ZAIKO_HINMEI_NAME = copyTarget.ZAIKO_HINMEI_NAME;
            returnEntity.ZAIKO_HIRITSU = copyTarget.ZAIKO_HIRITSU;
            returnEntity.ZAIKO_RYOU = copyTarget.ZAIKO_RYOU;
            returnEntity.ZAIKO_TANKA = copyTarget.ZAIKO_TANKA;
            returnEntity.ZAIKO_KINGAKU = copyTarget.ZAIKO_KINGAKU;
            returnEntity.TIME_STAMP = copyTarget.TIME_STAMP;

            return returnEntity;
        }
        // 20150410 go 在庫品名振分処理追加 End
        // No.4578<--

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

        /// <summary>
        /// 取得済み在庫明細情報からキーに該当する要素を取得する
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="detailSystemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public List<T_ZAIKO_UKEIRE_DETAIL> GetZaikoUkeireListByDetail(SqlInt64 systemId, SqlInt64 detailSystemId, SqlInt32 seq)
        {
            //foreach (List<T_ZAIKO_UKEIRE_DETAIL> details in this.detailZaikoUkeireDetails)
            foreach (var details in this.detailZaikoUkeireDetails)
            {
                //foreach (T_ZAIKO_UKEIRE_DETAIL detail in details)
                //{
                //if (detail.SYSTEM_ID == systemid && detail.DETAIL_SYSTEM_ID == detailSystemId && detail.SEQ == seq)
                if (details.Key.SYSTEM_ID == systemId && details.Key.DETAIL_SYSTEM_ID == detailSystemId && details.Key.SEQ == seq)
                {
                    return details.Value;
                }
                //}
            }

            return null;
        }

        // No.4578-->
        // 20150409 go 在庫品名振分処理追加 Start
        /// <summary>
        ///
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="detailSystemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public List<T_ZAIKO_HINMEI_HURIWAKE> GetZaikoHinmeiHuriwakeListByDetail(SqlInt64 systemId, SqlInt64 detailSystemId, SqlInt32 seq)
        {
            foreach (var hinmeis in this.detailZaikoHinmeiHuriwakes)
            {
                if (hinmeis.Key.SYSTEM_ID == systemId && hinmeis.Key.DETAIL_SYSTEM_ID == detailSystemId && hinmeis.Key.SEQ == seq)
                {
                    return hinmeis.Value;
                }
            }

            return null;
        }
        // 20150409 go 在庫品名振分処理追加 End
        // No.4578<--

        #region Utility
        /// <summary>
        /// 自身のT_UKEIRE_DETAILのDETAIL_SYSTEM_IDのリストを取得する
        /// </summary>
        /// <returns>T_UKEIRE_DETAILが一件もない</returns>
        public List<SqlInt64> GetDetailSysIds()
        {
            List<SqlInt64> returnList = new List<SqlInt64>();

            if (this.detailEntity == null || this.detailEntity.Length < 1)
            {
                return returnList;
            }

            foreach (T_UKEIRE_DETAIL detail in this.detailEntity)
            {
                returnList.Add(detail.DETAIL_SYSTEM_ID);
            }

            return returnList;
        }
        #endregion
    }
}