using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using r_framework.Entity;

namespace Shougun.Core.Inspection.KenshuMeisaiNyuryoku
{
	/// <summary>
	/// 検収入力用DTO
	/// </summary>
	public class KenshuNyuuryokuDTOClass
	{
		/// <summary>
		/// T_KENSHU_DETAILのList
		/// </summary>
		public List<T_KENSHU_DETAIL> kenshuDetailList;

		/// <summary>
		/// T_SHUKKA_ENTRYのEntity
		/// </summary>
		public T_SHUKKA_ENTRY shukkaEntryEntity;

		/// <summary>
		/// T_SHUKKA_DETAILのList
		/// </summary>
		public List<T_SHUKKA_DETAIL> shukkaDetailList;

        /// <summary>
        /// コンストラクタ
        /// </summary>
		public KenshuNyuuryokuDTOClass()
        {
			this.kenshuDetailList = new List<T_KENSHU_DETAIL>();
			this.shukkaEntryEntity = new T_SHUKKA_ENTRY();
			this.shukkaDetailList = new List<T_SHUKKA_DETAIL>();
        }

		/// <summary>
		/// KenshuNyuuryokuDTOClassのCloneを作成
		/// </summary>
		public KenshuNyuuryokuDTOClass Clone()
		{
			var dto = new KenshuNyuuryokuDTOClass();

			// T_KENSHU_DETAILListのClone作成
			if(this.kenshuDetailList.Count > 0)
			{
				foreach(var kenshuDetail in this.kenshuDetailList)
				{
					var entity = new T_KENSHU_DETAIL();
					entity.SYSTEM_ID = kenshuDetail.SYSTEM_ID;
					entity.SEQ = kenshuDetail.SEQ;
					entity.DETAIL_SYSTEM_ID = kenshuDetail.DETAIL_SYSTEM_ID;
					entity.KENSHU_SYSTEM_ID = kenshuDetail.KENSHU_SYSTEM_ID;
					entity.SHUKKA_NUMBER = kenshuDetail.SHUKKA_NUMBER;
					entity.ROW_NO = kenshuDetail.ROW_NO;
					entity.KENSHU_ROW_NO = kenshuDetail.KENSHU_ROW_NO;
					entity.HINMEI_CD = kenshuDetail.HINMEI_CD;
					entity.HINMEI_NAME = kenshuDetail.HINMEI_NAME;
					entity.SHUKKA_NET = kenshuDetail.SHUKKA_NET;
					entity.BUBIKI = kenshuDetail.BUBIKI;
					entity.KENSHU_NET = kenshuDetail.KENSHU_NET;
					entity.SUURYOU = kenshuDetail.SUURYOU;
					entity.UNIT_CD = kenshuDetail.UNIT_CD;
					entity.TANKA = kenshuDetail.TANKA;
					entity.KINGAKU = kenshuDetail.KINGAKU;
					entity.TAX_SOTO = kenshuDetail.TAX_SOTO;
					entity.TAX_UCHI = kenshuDetail.TAX_UCHI;
					entity.HINMEI_ZEI_KBN_CD = kenshuDetail.HINMEI_ZEI_KBN_CD;
					entity.HINMEI_KINGAKU = kenshuDetail.HINMEI_KINGAKU;
					entity.HINMEI_TAX_SOTO = kenshuDetail.HINMEI_TAX_SOTO;
					entity.HINMEI_TAX_UCHI = kenshuDetail.HINMEI_TAX_UCHI;
					entity.DENPYOU_KBN_CD = kenshuDetail.DENPYOU_KBN_CD;
					dto.kenshuDetailList.Add(entity);
				}
			}

			// T_SHUKKA_ENTRYEntityのClone作成
			dto.shukkaEntryEntity.SYSTEM_ID = this.shukkaEntryEntity.SYSTEM_ID;
			dto.shukkaEntryEntity.SEQ = this.shukkaEntryEntity.SEQ;
			dto.shukkaEntryEntity.TAIRYUU_KBN = this.shukkaEntryEntity.TAIRYUU_KBN;
			dto.shukkaEntryEntity.KYOTEN_CD = this.shukkaEntryEntity.KYOTEN_CD;
			dto.shukkaEntryEntity.SHUKKA_NUMBER = this.shukkaEntryEntity.SHUKKA_NUMBER;
			dto.shukkaEntryEntity.DATE_NUMBER = this.shukkaEntryEntity.DATE_NUMBER;
			dto.shukkaEntryEntity.YEAR_NUMBER = this.shukkaEntryEntity.YEAR_NUMBER;
			dto.shukkaEntryEntity.KAKUTEI_KBN = this.shukkaEntryEntity.KAKUTEI_KBN;
			dto.shukkaEntryEntity.DENPYOU_DATE = this.shukkaEntryEntity.DENPYOU_DATE;
			dto.shukkaEntryEntity.SEARCH_DENPYOU_DATE = this.shukkaEntryEntity.SEARCH_DENPYOU_DATE;
			dto.shukkaEntryEntity.URIAGE_DATE = this.shukkaEntryEntity.URIAGE_DATE;
			dto.shukkaEntryEntity.SEARCH_URIAGE_DATE = this.shukkaEntryEntity.SEARCH_URIAGE_DATE;
			dto.shukkaEntryEntity.SHIHARAI_DATE = this.shukkaEntryEntity.SHIHARAI_DATE;
			dto.shukkaEntryEntity.SEARCH_SHIHARAI_DATE = this.shukkaEntryEntity.SEARCH_SHIHARAI_DATE;
			dto.shukkaEntryEntity.TORIHIKISAKI_CD = this.shukkaEntryEntity.TORIHIKISAKI_CD;
			dto.shukkaEntryEntity.TORIHIKISAKI_NAME = this.shukkaEntryEntity.TORIHIKISAKI_NAME;
			dto.shukkaEntryEntity.GYOUSHA_CD = this.shukkaEntryEntity.GYOUSHA_CD;
			dto.shukkaEntryEntity.GYOUSHA_NAME = this.shukkaEntryEntity.GYOUSHA_NAME;
			dto.shukkaEntryEntity.GENBA_CD = this.shukkaEntryEntity.GENBA_CD;
			dto.shukkaEntryEntity.GENBA_NAME = this.shukkaEntryEntity.GENBA_NAME;
			dto.shukkaEntryEntity.NIZUMI_GYOUSHA_CD = this.shukkaEntryEntity.NIZUMI_GYOUSHA_CD;
			dto.shukkaEntryEntity.NIZUMI_GYOUSHA_NAME = this.shukkaEntryEntity.NIZUMI_GYOUSHA_NAME;
			dto.shukkaEntryEntity.NIZUMI_GENBA_CD = this.shukkaEntryEntity.NIZUMI_GENBA_CD;
			dto.shukkaEntryEntity.NIZUMI_GENBA_NAME = this.shukkaEntryEntity.NIZUMI_GENBA_NAME;
			dto.shukkaEntryEntity.EIGYOU_TANTOUSHA_CD = this.shukkaEntryEntity.EIGYOU_TANTOUSHA_CD;
			dto.shukkaEntryEntity.EIGYOU_TANTOUSHA_NAME = this.shukkaEntryEntity.EIGYOU_TANTOUSHA_NAME;
			dto.shukkaEntryEntity.NYUURYOKU_TANTOUSHA_CD = this.shukkaEntryEntity.NYUURYOKU_TANTOUSHA_CD;
			dto.shukkaEntryEntity.NYUURYOKU_TANTOUSHA_NAME = this.shukkaEntryEntity.NYUURYOKU_TANTOUSHA_NAME;
			dto.shukkaEntryEntity.SHARYOU_CD = this.shukkaEntryEntity.SHARYOU_CD;
			dto.shukkaEntryEntity.SHARYOU_NAME = this.shukkaEntryEntity.SHARYOU_NAME;
			dto.shukkaEntryEntity.SHASHU_CD = this.shukkaEntryEntity.SHASHU_CD;
			dto.shukkaEntryEntity.SHASHU_NAME = this.shukkaEntryEntity.SHASHU_NAME;
			dto.shukkaEntryEntity.UNPAN_GYOUSHA_CD = this.shukkaEntryEntity.UNPAN_GYOUSHA_CD;
			dto.shukkaEntryEntity.UNPAN_GYOUSHA_NAME = this.shukkaEntryEntity.UNPAN_GYOUSHA_NAME;
			dto.shukkaEntryEntity.UNTENSHA_CD = this.shukkaEntryEntity.UNTENSHA_CD;
			dto.shukkaEntryEntity.UNTENSHA_NAME = this.shukkaEntryEntity.UNTENSHA_NAME;
			dto.shukkaEntryEntity.NINZUU_CNT = this.shukkaEntryEntity.NINZUU_CNT;
			dto.shukkaEntryEntity.KEITAI_KBN_CD = this.shukkaEntryEntity.KEITAI_KBN_CD;
			dto.shukkaEntryEntity.DAIKAN_KBN = this.shukkaEntryEntity.DAIKAN_KBN;
			dto.shukkaEntryEntity.CONTENA_SOUSA_CD = this.shukkaEntryEntity.CONTENA_SOUSA_CD;
			dto.shukkaEntryEntity.MANIFEST_SHURUI_CD = this.shukkaEntryEntity.MANIFEST_SHURUI_CD;
			dto.shukkaEntryEntity.MANIFEST_TEHAI_CD = this.shukkaEntryEntity.MANIFEST_TEHAI_CD;
			dto.shukkaEntryEntity.DENPYOU_BIKOU = this.shukkaEntryEntity.DENPYOU_BIKOU;
			dto.shukkaEntryEntity.TAIRYUU_BIKOU = this.shukkaEntryEntity.TAIRYUU_BIKOU;
			dto.shukkaEntryEntity.UKETSUKE_NUMBER = this.shukkaEntryEntity.UKETSUKE_NUMBER;
			dto.shukkaEntryEntity.KEIRYOU_NUMBER = this.shukkaEntryEntity.KEIRYOU_NUMBER;
			dto.shukkaEntryEntity.RECEIPT_NUMBER = this.shukkaEntryEntity.RECEIPT_NUMBER;
			dto.shukkaEntryEntity.NET_TOTAL = this.shukkaEntryEntity.NET_TOTAL;
			dto.shukkaEntryEntity.URIAGE_SHOUHIZEI_RATE = this.shukkaEntryEntity.URIAGE_SHOUHIZEI_RATE;
			dto.shukkaEntryEntity.URIAGE_AMOUNT_TOTAL = this.shukkaEntryEntity.URIAGE_AMOUNT_TOTAL;
			dto.shukkaEntryEntity.URIAGE_TAX_SOTO = this.shukkaEntryEntity.URIAGE_TAX_SOTO;
			dto.shukkaEntryEntity.URIAGE_TAX_UCHI = this.shukkaEntryEntity.URIAGE_TAX_UCHI;
			dto.shukkaEntryEntity.URIAGE_TAX_SOTO_TOTAL = this.shukkaEntryEntity.URIAGE_TAX_SOTO_TOTAL;
			dto.shukkaEntryEntity.URIAGE_TAX_UCHI_TOTAL = this.shukkaEntryEntity.URIAGE_TAX_UCHI_TOTAL;
			dto.shukkaEntryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = this.shukkaEntryEntity.HINMEI_URIAGE_KINGAKU_TOTAL;
			dto.shukkaEntryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = this.shukkaEntryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL;
			dto.shukkaEntryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = this.shukkaEntryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL;
			dto.shukkaEntryEntity.SHIHARAI_SHOUHIZEI_RATE = this.shukkaEntryEntity.SHIHARAI_SHOUHIZEI_RATE;
			dto.shukkaEntryEntity.SHIHARAI_AMOUNT_TOTAL = this.shukkaEntryEntity.SHIHARAI_AMOUNT_TOTAL;
			dto.shukkaEntryEntity.SHIHARAI_TAX_SOTO = this.shukkaEntryEntity.SHIHARAI_TAX_SOTO;
			dto.shukkaEntryEntity.SHIHARAI_TAX_UCHI = this.shukkaEntryEntity.SHIHARAI_TAX_UCHI;
			dto.shukkaEntryEntity.SHIHARAI_TAX_SOTO_TOTAL = this.shukkaEntryEntity.SHIHARAI_TAX_SOTO_TOTAL;
			dto.shukkaEntryEntity.SHIHARAI_TAX_UCHI_TOTAL = this.shukkaEntryEntity.SHIHARAI_TAX_UCHI_TOTAL;
			dto.shukkaEntryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = this.shukkaEntryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL;
			dto.shukkaEntryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = this.shukkaEntryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL;
			dto.shukkaEntryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = this.shukkaEntryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL;
			dto.shukkaEntryEntity.URIAGE_ZEI_KEISAN_KBN_CD = this.shukkaEntryEntity.URIAGE_ZEI_KEISAN_KBN_CD;
			dto.shukkaEntryEntity.URIAGE_ZEI_KBN_CD = this.shukkaEntryEntity.URIAGE_ZEI_KBN_CD;
			dto.shukkaEntryEntity.URIAGE_TORIHIKI_KBN_CD = this.shukkaEntryEntity.URIAGE_TORIHIKI_KBN_CD;
			dto.shukkaEntryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = this.shukkaEntryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD;
			dto.shukkaEntryEntity.SHIHARAI_ZEI_KBN_CD = this.shukkaEntryEntity.SHIHARAI_ZEI_KBN_CD;
			dto.shukkaEntryEntity.SHIHARAI_TORIHIKI_KBN_CD = this.shukkaEntryEntity.SHIHARAI_TORIHIKI_KBN_CD;
			dto.shukkaEntryEntity.KENSHU_DATE = this.shukkaEntryEntity.KENSHU_DATE;
			dto.shukkaEntryEntity.SHUKKA_NET_TOTAL = this.shukkaEntryEntity.SHUKKA_NET_TOTAL;
			dto.shukkaEntryEntity.KENSHU_NET_TOTAL = this.shukkaEntryEntity.KENSHU_NET_TOTAL;
			dto.shukkaEntryEntity.SABUN = this.shukkaEntryEntity.SABUN;
			dto.shukkaEntryEntity.SHUKKA_KINGAKU_TOTAL = this.shukkaEntryEntity.SHUKKA_KINGAKU_TOTAL;
			dto.shukkaEntryEntity.KENSHU_KINGAKU_TOTAL = this.shukkaEntryEntity.KENSHU_KINGAKU_TOTAL;
			dto.shukkaEntryEntity.SAGAKU = this.shukkaEntryEntity.SAGAKU;
			dto.shukkaEntryEntity.KENSHU_MUST_KBN = this.shukkaEntryEntity.KENSHU_MUST_KBN;
			dto.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE = this.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE;
			dto.shukkaEntryEntity.KENSHU_URIAGE_AMOUNT_TOTAL = this.shukkaEntryEntity.KENSHU_URIAGE_AMOUNT_TOTAL;
			dto.shukkaEntryEntity.KENSHU_URIAGE_TAX_SOTO = this.shukkaEntryEntity.KENSHU_URIAGE_TAX_SOTO;
			dto.shukkaEntryEntity.KENSHU_URIAGE_TAX_UCHI = this.shukkaEntryEntity.KENSHU_URIAGE_TAX_UCHI;
			dto.shukkaEntryEntity.KENSHU_URIAGE_TAX_SOTO_TOTAL = this.shukkaEntryEntity.KENSHU_URIAGE_TAX_SOTO_TOTAL;
			dto.shukkaEntryEntity.KENSHU_URIAGE_TAX_UCHI_TOTAL = this.shukkaEntryEntity.KENSHU_URIAGE_TAX_UCHI_TOTAL;
			dto.shukkaEntryEntity.KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL = this.shukkaEntryEntity.KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL;
			dto.shukkaEntryEntity.KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL = this.shukkaEntryEntity.KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL;
			dto.shukkaEntryEntity.KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL = this.shukkaEntryEntity.KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL;
			dto.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE = this.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE;
			dto.shukkaEntryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL = this.shukkaEntryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL;
			dto.shukkaEntryEntity.KENSHU_SHIHARAI_TAX_SOTO = this.shukkaEntryEntity.KENSHU_SHIHARAI_TAX_SOTO;
			dto.shukkaEntryEntity.KENSHU_SHIHARAI_TAX_UCHI = this.shukkaEntryEntity.KENSHU_SHIHARAI_TAX_UCHI;
			dto.shukkaEntryEntity.KENSHU_SHIHARAI_TAX_SOTO_TOTAL = this.shukkaEntryEntity.KENSHU_SHIHARAI_TAX_SOTO_TOTAL;
			dto.shukkaEntryEntity.KENSHU_SHIHARAI_TAX_UCHI_TOTAL = this.shukkaEntryEntity.KENSHU_SHIHARAI_TAX_UCHI_TOTAL;
			dto.shukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL = this.shukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL;
			dto.shukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL = this.shukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL;
			dto.shukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL = this.shukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL;
			dto.shukkaEntryEntity.KENSHU_URIAGE_DATE = this.shukkaEntryEntity.KENSHU_URIAGE_DATE;
			dto.shukkaEntryEntity.KENSHU_SHIHARAI_DATE = this.shukkaEntryEntity.KENSHU_SHIHARAI_DATE;
			dto.shukkaEntryEntity.DELETE_FLG = this.shukkaEntryEntity.DELETE_FLG;

			// T_SHUKKA_DETAILListのClone作成
			if(this.shukkaDetailList.Count > 0)
			{
				foreach(var shukkaDetail in this.shukkaDetailList)
				{
					var entity = new T_SHUKKA_DETAIL();
					entity.SYSTEM_ID = shukkaDetail.SYSTEM_ID;
					entity.SEQ = shukkaDetail.SEQ;
					entity.DETAIL_SYSTEM_ID = shukkaDetail.DETAIL_SYSTEM_ID;
					entity.SHUKKA_NUMBER = shukkaDetail.SHUKKA_NUMBER;
					entity.ROW_NO = shukkaDetail.ROW_NO;
					entity.KAKUTEI_KBN = shukkaDetail.KAKUTEI_KBN;
					entity.URIAGESHIHARAI_DATE = shukkaDetail.URIAGESHIHARAI_DATE;
					entity.SEARCH_URIAGESHIHARAI_DATE = shukkaDetail.SEARCH_URIAGESHIHARAI_DATE;
					entity.STACK_JYUURYOU = shukkaDetail.STACK_JYUURYOU;
					entity.EMPTY_JYUURYOU = shukkaDetail.EMPTY_JYUURYOU;
					entity.WARIFURI_JYUURYOU = shukkaDetail.WARIFURI_JYUURYOU;
					entity.WARIFURI_PERCENT = shukkaDetail.WARIFURI_PERCENT;
					entity.CHOUSEI_JYUURYOU = shukkaDetail.CHOUSEI_JYUURYOU;
					entity.CHOUSEI_PERCENT = shukkaDetail.CHOUSEI_PERCENT;
					entity.YOUKI_CD = shukkaDetail.YOUKI_CD;
					entity.YOUKI_SUURYOU = shukkaDetail.YOUKI_SUURYOU;
					entity.YOUKI_JYUURYOU = shukkaDetail.YOUKI_JYUURYOU;
					entity.DENPYOU_KBN_CD = shukkaDetail.DENPYOU_KBN_CD;
					entity.HINMEI_CD = shukkaDetail.HINMEI_CD;
					entity.HINMEI_NAME = shukkaDetail.HINMEI_NAME;
					entity.NET_JYUURYOU = shukkaDetail.NET_JYUURYOU;
					entity.SUURYOU = shukkaDetail.SUURYOU;
					entity.UNIT_CD = shukkaDetail.UNIT_CD;
					entity.TANKA = shukkaDetail.TANKA;
					entity.KINGAKU = shukkaDetail.KINGAKU;
					entity.TAX_SOTO = shukkaDetail.TAX_SOTO;
					entity.TAX_UCHI = shukkaDetail.TAX_UCHI;
					entity.HINMEI_ZEI_KBN_CD = shukkaDetail.HINMEI_ZEI_KBN_CD;
					entity.HINMEI_KINGAKU = shukkaDetail.HINMEI_KINGAKU;
					entity.HINMEI_TAX_SOTO = shukkaDetail.HINMEI_TAX_SOTO;
					entity.HINMEI_TAX_UCHI = shukkaDetail.HINMEI_TAX_UCHI;
					entity.MEISAI_BIKOU = shukkaDetail.MEISAI_BIKOU;
					entity.NISUGATA_SUURYOU = shukkaDetail.NISUGATA_SUURYOU;
					entity.NISUGATA_UNIT_CD = shukkaDetail.NISUGATA_UNIT_CD;
					dto.shukkaDetailList.Add(entity);
				}
			}

			return dto;
		}
	}
}
