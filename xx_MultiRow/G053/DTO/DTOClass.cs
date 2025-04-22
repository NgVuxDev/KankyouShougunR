using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using GrapeCity.Win.MultiRow;
using r_framework.Entity;
using Shougun.Core.Inspection.KenshuMeisaiNyuryoku;

namespace Shougun.Core.SalesPayment.SyukkaNyuuryoku
{
    /// <summary>
    /// 出荷入力用DTO
    /// </summary>
    internal class DTOClass
    {
        /// <summary>
        /// T_SHUKKA_ENTRY用のEntity
        /// </summary>
        internal T_SHUKKA_ENTRY entryEntity;

        /// <summary>
        /// T_SHUKKA_DETAIL
        /// </summary>
        internal T_SHUKKA_DETAIL[] detailEntity;

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

        internal Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_SHUKKA_DETAIL>> detailZaikoShukkaDetails;
        internal Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>> detailZaikoHinmeiHuriwakes;
        // Dictionary関連修正
        internal Dictionary<Row, List<T_ZAIKO_SHUKKA_DETAIL>> rowZaikoShukkaDetails;
        internal Dictionary<Row, List<T_ZAIKO_HINMEI_HURIWAKE>> rowZaikoHinmeiHuriwakes;

        /// <summary>検収入力画面用DTO</summary>
        internal KenshuNyuuryokuDTOClass kenshuNyuuryokuDto;

        public DTOClass()
        {
            this.entryEntity = new T_SHUKKA_ENTRY();
            this.detailEntity = new T_SHUKKA_DETAIL[] { new T_SHUKKA_DETAIL() };
            this.sysInfoEntity = new M_SYS_INFO();
            this.torihikisakiSeikyuuEntity = new M_TORIHIKISAKI_SEIKYUU();
            this.torihikisakiShiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
            this.manifestShuruiEntity = new M_MANIFEST_SHURUI();
            this.manifestTehaiEntity = new M_MANIFEST_TEHAI();
            this.kyotenEntity = new M_KYOTEN();
            this.numberDay = new S_NUMBER_DAY();
            this.numberYear = new S_NUMBER_YEAR();
            this.numberReceipt = new S_NUMBER_RECEIPT();
            this.numberReceiptYear = new S_NUMBER_RECEIPT_YEAR();
            this.manifestEntrys = new DataTable();
            this.keitaiKbnEntity = new M_KEITAI_KBN();
            //this.detailZaikoShukkaDetails = new List<List<T_ZAIKO_SHUKKA_DETAIL>>();
            this.detailZaikoShukkaDetails = new Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_SHUKKA_DETAIL>>();
            //Dictionary関連修正
            this.rowZaikoShukkaDetails = new Dictionary<Row, List<T_ZAIKO_SHUKKA_DETAIL>>();
            // No.4578-->
            // 20150415 go 在庫品名振分処理追加(修正後のG051からコピー) Start
            this.detailZaikoHinmeiHuriwakes = new Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>>();
            this.rowZaikoHinmeiHuriwakes = new Dictionary<Row, List<T_ZAIKO_HINMEI_HURIWAKE>>();
            // 20150415 go 在庫品名振分処理追加(修正後のG051からコピー) End
            // No.4578<--
            this.kenshuNyuuryokuDto = new KenshuNyuuryokuDTOClass();
        }

        /// <summary>
        /// Dtoの中身をコピーする
        /// T_SHUKKA_ENTRYとT_SHUKKA_DETAILのみデータコピーをする。
        /// それ以外は参照渡し
        /// もし上記以外の値コピーをしたい場合は適宜追加。
        /// </summary>
        /// <returns></returns>
        public DTOClass Clone()
        {
            DTOClass returnDto = new DTOClass();
            returnDto.entryEntity = this.ShukkaEntryClone(false);
            List<T_SHUKKA_DETAIL> copyList = new List<T_SHUKKA_DETAIL>();
            if (this.detailEntity != null)
            {
                foreach (var targetEntity in this.detailEntity)
                {
                    copyList.Add(this.ShukkaDetailClone(targetEntity));
                }
            }
            returnDto.detailEntity = copyList.ToArray();

            // 在庫明細のList
            var detailZaikoShukkaDetailsCopy = new Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_SHUKKA_DETAIL>>();
            foreach (var targetDetails in this.detailZaikoShukkaDetails)
            {
                var zaikoShukkaDetailsCopy = new List<T_ZAIKO_SHUKKA_DETAIL>();
                foreach (var targetEntity in targetDetails.Value)
                {
                    zaikoShukkaDetailsCopy.Add(this.ZaikoShukkaDetailClone(targetEntity));
                }
                detailZaikoShukkaDetailsCopy[targetDetails.Key] = zaikoShukkaDetailsCopy;
            }
            returnDto.detailZaikoShukkaDetails = detailZaikoShukkaDetailsCopy;
            // Dictionary関連修正
            // 在庫明細のDictionary
            var rowZaikoShukkaDetailsCopy = new Dictionary<Row, List<T_ZAIKO_SHUKKA_DETAIL>>();
            foreach (var targetDetails in this.rowZaikoShukkaDetails)
            {
                var zaikoShukkaDetailsCopy = new List<T_ZAIKO_SHUKKA_DETAIL>();
                foreach (var targetEntity in targetDetails.Value)
                {
                    zaikoShukkaDetailsCopy.Add(this.ZaikoShukkaDetailClone(targetEntity));
                }
                rowZaikoShukkaDetailsCopy[targetDetails.Key] = zaikoShukkaDetailsCopy;
            }
            returnDto.rowZaikoShukkaDetails = rowZaikoShukkaDetailsCopy;

            // No.4578-->
            // 20150415 go 在庫品名振分処理追加(修正後のG051からコピー) Start
            var detailZaikoHinmeiHuriwakesCopy = new Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>>();
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
            // 20150415 go 在庫品名振分処理追加(修正後のG051からコピー) End
            // No.4578<--

            returnDto.sysInfoEntity = this.sysInfoEntity;
            returnDto.torihikisakiSeikyuuEntity = this.torihikisakiSeikyuuEntity;
            returnDto.torihikisakiShiharaiEntity = this.torihikisakiShiharaiEntity;
            returnDto.manifestShuruiEntity = this.manifestShuruiEntity;
            returnDto.manifestTehaiEntity = this.manifestTehaiEntity;
            returnDto.kyotenEntity = this.kyotenEntity;
            returnDto.numberDay = this.numberDay;
            returnDto.numberYear = this.numberYear;
            returnDto.numberReceipt = this.numberReceipt;
            returnDto.numberReceiptYear = this.numberReceiptYear;
            returnDto.manifestEntrys = this.manifestEntrys;
            returnDto.keitaiKbnEntity = this.keitaiKbnEntity;
            // 検収明細
            returnDto.kenshuNyuuryokuDto = this.kenshuNyuuryokuDto.Clone();

            return returnDto;
        }

        /// <summary>
        /// 自身のT_SHUKKA_ENTRYを複製する
        /// </summary>
        /// <returns></returns>
        internal T_SHUKKA_ENTRY ShukkaEntryClone(bool copy)
        {
            var returnEntity = new T_SHUKKA_ENTRY();

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
            returnEntity.NIZUMI_GENBA_CD = this.entryEntity.NIZUMI_GENBA_CD;
            returnEntity.NIZUMI_GENBA_NAME = this.entryEntity.NIZUMI_GENBA_NAME;
            returnEntity.NIZUMI_GYOUSHA_CD = this.entryEntity.NIZUMI_GYOUSHA_CD;
            returnEntity.NIZUMI_GYOUSHA_NAME = this.entryEntity.NIZUMI_GYOUSHA_NAME;
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
            returnEntity.SHIHARAI_AMOUNT_TOTAL = this.entryEntity.SHIHARAI_AMOUNT_TOTAL;
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
            returnEntity.SHUKKA_NUMBER = this.entryEntity.SHUKKA_NUMBER;
            returnEntity.UKETSUKE_NUMBER = this.entryEntity.UKETSUKE_NUMBER;
            returnEntity.UNPAN_GYOUSHA_CD = this.entryEntity.UNPAN_GYOUSHA_CD;
            returnEntity.UNPAN_GYOUSHA_NAME = this.entryEntity.UNPAN_GYOUSHA_NAME;
            returnEntity.UNTENSHA_CD = this.entryEntity.UNTENSHA_CD;
            returnEntity.UNTENSHA_NAME = this.entryEntity.UNTENSHA_NAME;
            returnEntity.URIAGE_DATE = this.entryEntity.URIAGE_DATE;
            returnEntity.URIAGE_AMOUNT_TOTAL = this.entryEntity.URIAGE_AMOUNT_TOTAL;
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
            returnEntity.KENSHU_MUST_KBN = this.entryEntity.KENSHU_MUST_KBN;
            // 複製時は検収情報を読み込ませない
            if (!copy)
            {
                returnEntity.KENSHU_URIAGE_SHOUHIZEI_RATE = this.entryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE;
                returnEntity.KENSHU_URIAGE_AMOUNT_TOTAL = this.entryEntity.KENSHU_URIAGE_AMOUNT_TOTAL;
                returnEntity.KENSHU_URIAGE_TAX_SOTO = this.entryEntity.KENSHU_URIAGE_TAX_SOTO;
                returnEntity.KENSHU_URIAGE_TAX_UCHI = this.entryEntity.KENSHU_URIAGE_TAX_UCHI;
                returnEntity.KENSHU_URIAGE_TAX_SOTO_TOTAL = this.entryEntity.KENSHU_URIAGE_TAX_SOTO_TOTAL;
                returnEntity.KENSHU_URIAGE_TAX_UCHI_TOTAL = this.entryEntity.KENSHU_URIAGE_TAX_UCHI_TOTAL;
                returnEntity.KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL = this.entryEntity.KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL;
                returnEntity.KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL = this.entryEntity.KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL;
                returnEntity.KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL = this.entryEntity.KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL;
                returnEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE = this.entryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE;
                returnEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL = this.entryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL;
                returnEntity.KENSHU_SHIHARAI_TAX_SOTO = this.entryEntity.KENSHU_SHIHARAI_TAX_SOTO;
                returnEntity.KENSHU_SHIHARAI_TAX_UCHI = this.entryEntity.KENSHU_SHIHARAI_TAX_UCHI;
                returnEntity.KENSHU_SHIHARAI_TAX_SOTO_TOTAL = this.entryEntity.KENSHU_SHIHARAI_TAX_SOTO_TOTAL;
                returnEntity.KENSHU_SHIHARAI_TAX_UCHI_TOTAL = this.entryEntity.KENSHU_SHIHARAI_TAX_UCHI_TOTAL;
                returnEntity.KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL = this.entryEntity.KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL;
                returnEntity.KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL = this.entryEntity.KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL;
                returnEntity.KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL = this.entryEntity.KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL;
                returnEntity.KENSHU_DATE = this.entryEntity.KENSHU_DATE;
                returnEntity.KENSHU_URIAGE_DATE = this.entryEntity.KENSHU_URIAGE_DATE;
                returnEntity.KENSHU_SHIHARAI_DATE = this.entryEntity.KENSHU_SHIHARAI_DATE;
                returnEntity.KENSHU_NET_TOTAL = this.entryEntity.KENSHU_NET_TOTAL;
            }
            returnEntity.STACK_JYUURYOU = this.entryEntity.STACK_JYUURYOU;
            returnEntity.STACK_KEIRYOU_TIME = this.entryEntity.STACK_KEIRYOU_TIME;
            returnEntity.EMPTY_JYUURYOU = this.entryEntity.EMPTY_JYUURYOU;
            returnEntity.EMPTY_KEIRYOU_TIME = this.entryEntity.EMPTY_KEIRYOU_TIME;
            returnEntity.KINGAKU_TOTAL = this.entryEntity.KINGAKU_TOTAL;

            return returnEntity;
        }

        internal T_SHUKKA_DETAIL ShukkaDetailClone(T_SHUKKA_DETAIL copyTarget)
        {
            var returnEntity = new T_SHUKKA_DETAIL();

            returnEntity.YOUKI_SUURYOU = copyTarget.YOUKI_SUURYOU;
            returnEntity.YOUKI_JYUURYOU = copyTarget.YOUKI_JYUURYOU;
            returnEntity.YOUKI_CD = copyTarget.YOUKI_CD;
            returnEntity.WARIFURI_PERCENT = copyTarget.WARIFURI_PERCENT;
            returnEntity.WARIFURI_JYUURYOU = copyTarget.WARIFURI_JYUURYOU;
            returnEntity.URIAGESHIHARAI_DATE = copyTarget.URIAGESHIHARAI_DATE;
            returnEntity.UNIT_CD = copyTarget.UNIT_CD;
            returnEntity.SHUKKA_NUMBER = copyTarget.SHUKKA_NUMBER;
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
            returnEntity.TIME_STAMP = copyTarget.TIME_STAMP;
            returnEntity.DELETE_FLG = copyTarget.DELETE_FLG;

            return returnEntity;
        }

        private T_ZAIKO_SHUKKA_DETAIL ZaikoShukkaDetailClone(T_ZAIKO_SHUKKA_DETAIL copyTarget)
        {
            T_ZAIKO_SHUKKA_DETAIL returnEntity = new T_ZAIKO_SHUKKA_DETAIL();

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
        // 20150415 go 在庫品名振分処理追加(修正後のG051からコピー) Start
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
        // 20150415 go 在庫品名振分処理追加(修正後のG051からコピー) End
        // No.4578<--

        private T_KENSHU_DETAIL KenshuClone(T_KENSHU_DETAIL copyTarget)
        {
            T_KENSHU_DETAIL returnEntity = new T_KENSHU_DETAIL();

            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.KENSHU_SYSTEM_ID = copyTarget.KENSHU_SYSTEM_ID;
            returnEntity.SHUKKA_NUMBER = copyTarget.SHUKKA_NUMBER;
            returnEntity.ROW_NO = copyTarget.ROW_NO;
            returnEntity.KENSHU_ROW_NO = copyTarget.KENSHU_ROW_NO;
            returnEntity.HINMEI_CD = copyTarget.HINMEI_CD;
            returnEntity.HINMEI_NAME = copyTarget.HINMEI_NAME;
            returnEntity.SHUKKA_NET = copyTarget.SHUKKA_NET;
            returnEntity.BUBIKI = copyTarget.BUBIKI;
            returnEntity.KENSHU_NET = copyTarget.KENSHU_NET;
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
            returnEntity.TIME_STAMP = copyTarget.TIME_STAMP;

            return returnEntity;
        }

        /// <summary>
        /// 取得済み在庫明細情報からキーに該当する要素を取得する
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="detailSystemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public List<T_ZAIKO_SHUKKA_DETAIL> GetZaikoShukkaListByDetail(SqlInt64 systemId, SqlInt64 detailSystemId, SqlInt32 seq)
        {
            //foreach (List<T_ZAIKO_SHUKKA_DETAIL> details in this.detailZaikoShukkaDetails)
            foreach (var details in this.detailZaikoShukkaDetails)
            {
                //foreach (T_ZAIKO_SHUKKA_DETAIL detail in details)
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
        // 20150415 go 在庫品名振分処理追加(修正後のG051からコピー) Start
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
        // 20150415 go 在庫品名振分処理追加(修正後のG051からコピー) End
        // No.4578<--

        #region Utility
        /// <summary>
        /// 自身のT_SHUKKA_DETAILのDETAIL_SYSTEM_IDのリストを取得する
        /// </summary>
        /// <returns>T_SHUKKA_DETAILが一件もない</returns>
        public List<SqlInt64> GetDetailSysIds()
        {
            List<SqlInt64> returnList = new List<SqlInt64>();

            if (this.detailEntity == null || this.detailEntity.Length < 1)
            {
                return returnList;
            }

            foreach (T_SHUKKA_DETAIL detail in this.detailEntity)
            {
                returnList.Add(detail.DETAIL_SYSTEM_ID);
            }

            return returnList;
        }
        #endregion
    }
}