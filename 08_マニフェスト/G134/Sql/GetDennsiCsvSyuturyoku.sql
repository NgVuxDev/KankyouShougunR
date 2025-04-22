
       --連絡番号１
SELECT R05_1.RENRAKU_ID AS RENRAKU_ID_1,
       --連絡番号２
       R05_2.RENRAKU_ID AS RENRAKU_ID_2,
       --連絡番号３
       R05_3.RENRAKU_ID AS RENRAKU_ID_3,
       --引渡し日
	   R18.HIKIWATASHI_DATE,
	   --予約/ﾏﾆﾌｪｽﾄ区分
	   R18.MANIFEST_KBN,
	   --中間処理産業廃棄物登録区分
	   R18.FIRST_MANIFEST_FLAG,
       --排出事業場コード
	   JIGYOUJOU_HS.GENBA_CD AS JIGYOUJOU_CD_HS,
       --引渡し担当者
	   R18.HIKIWATASHI_TAN_NAME,
       --登録担当者
	   R18.REGI_TAN,
       --廃棄物の種類コード
	   R18.HAIKI_DAI_CODE,
	   R18.HAIKI_CHU_CODE,
	   R18.HAIKI_SHO_CODE,
	   R18.HAIKI_SAI_CODE,
       --廃棄物の名称
	   R18.HAIKI_NAME,
       --廃棄物の数量
	   R18.HAIKI_SUU,
       --廃棄物の数量単位コード
	   R18.HAIKI_UNIT_CODE,
       --荷姿コード
	   R18.NISUGATA_CODE,
       --荷姿の数量
	   R18.NISUGATA_SUU,
       --数量の確定者コード
	   R18.SUU_KAKUTEI_CODE,
       --有害物質１コード
	   R02_1.YUUGAI_CODE AS YUUGAI_CODE_1,
       --有害物質２コード
	   R02_2.YUUGAI_CODE AS YUUGAI_CODE_2,
       --有害物質３コード
	   R02_3.YUUGAI_CODE AS YUUGAI_CODE_3,
       --有害物質４コード
	   R02_4.YUUGAI_CODE AS YUUGAI_CODE_4,
       --有害物質５コード
	   R02_5.YUUGAI_CODE AS YUUGAI_CODE_5,
       --有害物質６コード
	   R02_6.YUUGAI_CODE AS YUUGAI_CODE_6,
       --[区間１]収集運搬業者加入者番号
	   R19_UPN_1.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID_1,
       --[区間１]再委託収集運搬業者加入者番号
	   R19_UPN_1.SAI_UPN_SHA_EDI_MEMBER_ID AS SAI_UPN_SHA_EDI_MEMBER_ID_1,
       --[区間１]運搬方法コード
	   R19_UPN_1.UPN_WAY_CODE AS UPN_WAY_CODE_1,
       --[区間１]車両番号
	   R19_UPN_1.CAR_NO AS CAR_NO_1,
       --[区間１]運搬担当者
	   R19_UPN_1.UPN_TAN_NAME AS UPN_TAN_NAME_1,
       --[区間１]運搬先事業場加入者番号
	   R19_UPN_1.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID_1,
       --[区間１]運搬先事業場コード
	   --JIGYOUJOU_SU_1.GENBA_CD AS JIGYOUJOU_CD_1,
       CASE JIGYOUJOU_SU_1.GENBA_CD
		WHEN NULL THEN ''
		ELSE RIGHT('000' + JIGYOUJOU_SU_1.GENBA_CD, 3)
	   END AS JIGYOUJOU_CD_1,
	   --報告不要区分
	   JIGYOUSHA_SU_1.HOUKOKU_HUYOU_KBN AS HOUKOKU_HUYOU_KBN_1,
	   --運搬先事業場番号
	   --R19_UPN_1.UPNSAKI_JOU_ID AS UPNSAKI_JOU_ID_1,
       CASE R19_UPN_1.UPNSAKI_JOU_ID
		WHEN NULL THEN ''
		ELSE RIGHT('000' + CONVERT(VARCHAR(3), R19_UPN_1.UPNSAKI_JOU_ID), 3)
	   END AS UPNSAKI_JOU_ID_1,
	   -- 区間番号
	   R19_UPN_1.UPN_ROUTE_NO AS UPN_ROUTE_NO_1,

       --[区間２]収集運搬業者加入者番号
	   R19_UPN_2.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID_2,
       --[区間２]再委託収集運搬業者加入者番号
	   R19_UPN_2.SAI_UPN_SHA_EDI_MEMBER_ID AS SAI_UPN_SHA_EDI_MEMBER_ID_2,
       --[区間２]運搬方法コード
	   R19_UPN_2.UPN_WAY_CODE AS UPN_WAY_CODE_2,
       --[区間２]車両番号
	   R19_UPN_2.CAR_NO AS CAR_NO_2,
       --[区間２]運搬担当者
	   R19_UPN_2.UPN_TAN_NAME AS UPN_TAN_NAME_2,
       --[区間２]運搬先事業場加入者番号
	   R19_UPN_2.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID_2,
       --[区間２]運搬先事業場コード
	   --JIGYOUJOU_SU_2.GENBA_CD AS JIGYOUJOU_CD_2,
       CASE JIGYOUJOU_SU_2.GENBA_CD
		WHEN NULL THEN ''
		ELSE RIGHT('000' + JIGYOUJOU_SU_2.GENBA_CD, 3)
	   END AS JIGYOUJOU_CD_2,
	   --報告不要区分
	   JIGYOUSHA_SU_2.HOUKOKU_HUYOU_KBN AS HOUKOKU_HUYOU_KBN_2,
	   --運搬先事業場番号
	   --R19_UPN_2.UPNSAKI_JOU_ID AS UPNSAKI_JOU_ID_2,
       CASE R19_UPN_2.UPNSAKI_JOU_ID
		WHEN NULL THEN ''
		ELSE RIGHT('000' + CONVERT(VARCHAR(3), R19_UPN_2.UPNSAKI_JOU_ID), 3)
	   END AS UPNSAKI_JOU_ID_2,
	   -- 区間番号
	   R19_UPN_2.UPN_ROUTE_NO AS UPN_ROUTE_NO_2,

       --[区間３]収集運搬業者加入者番号
	   R19_UPN_3.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID_3,
       --[区間３]再委託収集運搬業者加入者番号
	   R19_UPN_3.SAI_UPN_SHA_EDI_MEMBER_ID AS SAI_UPN_SHA_EDI_MEMBER_ID_3,
       --[区間３]運搬方法コード
	   R19_UPN_3.UPN_WAY_CODE AS UPN_WAY_CODE_3,
       --[区間３]車両番号
	   R19_UPN_3.CAR_NO AS CAR_NO_3,
       --[区間３]運搬担当者
	   R19_UPN_3.UPN_TAN_NAME AS UPN_TAN_NAME_3,
       --[区間３]運搬先事業場加入者番号
	   R19_UPN_3.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID_3,
       --[区間３]運搬先事業場コード
	   --JIGYOUJOU_SU_3.GENBA_CD AS JIGYOUJOU_CD_3,
       CASE JIGYOUJOU_SU_3.GENBA_CD
		WHEN NULL THEN ''
		ELSE RIGHT('000' + JIGYOUJOU_SU_3.GENBA_CD, 3)
	   END AS JIGYOUJOU_CD_3,
	   --報告不要区分
	   JIGYOUSHA_SU_3.HOUKOKU_HUYOU_KBN AS HOUKOKU_HUYOU_KBN_3,
	   --運搬先事業場番号
	   --R19_UPN_3.UPNSAKI_JOU_ID AS UPNSAKI_JOU_ID_3,
       CASE R19_UPN_3.UPNSAKI_JOU_ID
		WHEN NULL THEN ''
		ELSE RIGHT('000' + CONVERT(VARCHAR(3), R19_UPN_3.UPNSAKI_JOU_ID), 3)
	   END AS UPNSAKI_JOU_ID_3,
	   -- 区間番号
	   R19_UPN_3.UPN_ROUTE_NO AS UPN_ROUTE_NO_3,

       --[区間４]収集運搬業者加入者番号
	   R19_UPN_4.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID_4,
       --[区間４]再委託収集運搬業者加入者番号
	   R19_UPN_4.SAI_UPN_SHA_EDI_MEMBER_ID AS SAI_UPN_SHA_EDI_MEMBER_ID_4,
       --[区間４]運搬方法コード
	   R19_UPN_4.UPN_WAY_CODE AS UPN_WAY_CODE_4,
       --[区間４]車両番号
	   R19_UPN_4.CAR_NO AS CAR_NO_4,
       --[区間４]運搬担当者
	   R19_UPN_4.UPN_TAN_NAME AS UPN_TAN_NAME_4,
       --[区間４]運搬先事業場加入者番号
	   R19_UPN_4.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID_4,
       --[区間４]運搬先事業場コード
	   --JIGYOUJOU_SU_4.GENBA_CD AS JIGYOUJOU_CD_4,
       CASE JIGYOUJOU_SU_4.GENBA_CD
		WHEN NULL THEN ''
		ELSE RIGHT('000' + JIGYOUJOU_SU_4.GENBA_CD, 3)
	   END AS JIGYOUJOU_CD_4,
	   --報告不要区分
	   JIGYOUSHA_SU_4.HOUKOKU_HUYOU_KBN AS HOUKOKU_HUYOU_KBN_4,
	   --運搬先事業場番号
	   --R19_UPN_4.UPNSAKI_JOU_ID AS UPNSAKI_JOU_ID_4,
       CASE R19_UPN_4.UPNSAKI_JOU_ID
		WHEN NULL THEN ''
		ELSE RIGHT('000' + CONVERT(VARCHAR(3), R19_UPN_4.UPNSAKI_JOU_ID), 3)
	   END AS UPNSAKI_JOU_ID_4,
	   -- 区間番号
	   R19_UPN_4.UPN_ROUTE_NO AS UPN_ROUTE_NO_4,

       --[区間５]収集運搬業者加入者番号
	   R19_UPN_5.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID_5,
       --[区間５]再委託収集運搬業者加入者番号
	   R19_UPN_5.SAI_UPN_SHA_EDI_MEMBER_ID AS SAI_UPN_SHA_EDI_MEMBER_ID_5,
       --[区間５]運搬方法コード
	   R19_UPN_5.UPN_WAY_CODE AS UPN_WAY_CODE_5,
       --[区間５]車両番号
	   R19_UPN_5.CAR_NO AS CAR_NO_5,
       --[区間５]運搬担当者
	   R19_UPN_5.UPN_TAN_NAME AS UPN_TAN_NAME_5,
       --[区間５]運搬先事業場加入者番号
	   R19_UPN_5.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID_5,
       --[区間５]運搬先事業場コード
	   --JIGYOUJOU_SU_5.GENBA_CD AS JIGYOUJOU_CD_5,
       CASE JIGYOUJOU_SU_5.GENBA_CD
		WHEN NULL THEN ''
		ELSE RIGHT('000' + JIGYOUJOU_SU_5.GENBA_CD, 3)
	   END AS JIGYOUJOU_CD_5,
	   --報告不要区分
	   JIGYOUSHA_SU_5.HOUKOKU_HUYOU_KBN AS HOUKOKU_HUYOU_KBN_5,
	   --運搬先事業場番号
	   --R19_UPN_5.UPNSAKI_JOU_ID AS UPNSAKI_JOU_ID_5,
       CASE R19_UPN_5.UPNSAKI_JOU_ID
		WHEN NULL THEN ''
		ELSE RIGHT('000' + CONVERT(VARCHAR(3), R19_UPN_5.UPNSAKI_JOU_ID), 3)
	   END AS UPNSAKI_JOU_ID_5,
	   -- 区間番号
	   R19_UPN_5.UPN_ROUTE_NO AS UPN_ROUTE_NO_5,

       --処分業者加入者番号
	   R18.SBN_SHA_MEMBER_ID,
       --再委託処分業者加入者番号
	   R18.SAI_SBN_SHA_MEMBER_ID,
       --処分事業場コード
       CASE JIGYOUJOU_SB.JIGYOUJOU_CD
		WHEN NULL THEN ''
		ELSE RIGHT('000' + JIGYOUJOU_SB.JIGYOUJOU_CD, 3)
	   END AS JIGYOUJOU_CD_SB,
       --処分方法コード
	   R18.SBN_WAY_CODE,
       --最終処分事業場登録区分
	   R18.LAST_SBN_JOU_KISAI_FLAG,
       --最終処分事業場１コード
	   JIGYOUJOU_SBYT_1.GENBA_CD AS JIGYOUJOU_CD_SBYT_1,
       --最終処分事業場２コード
	   JIGYOUJOU_SBYT_2.GENBA_CD AS JIGYOUJOU_CD_SBYT_2,
       --最終処分事業場３コード
	   JIGYOUJOU_SBYT_3.GENBA_CD AS JIGYOUJOU_CD_SBYT_3,
       --最終処分事業場４コード
	   JIGYOUJOU_SBYT_4.GENBA_CD AS JIGYOUJOU_CD_SBYT_4,
       --最終処分事業場５コード
	   JIGYOUJOU_SBYT_5.GENBA_CD AS JIGYOUJOU_CD_SBYT_5,
       --最終処分事業場６コード
	   JIGYOUJOU_SBYT_6.GENBA_CD AS JIGYOUJOU_CD_SBYT_6,
       --最終処分事業場７コード
	   JIGYOUJOU_SBYT_7.GENBA_CD AS JIGYOUJOU_CD_SBYT_7,
       --最終処分事業場８コード
	   JIGYOUJOU_SBYT_8.GENBA_CD AS JIGYOUJOU_CD_SBYT_8,
       --最終処分事業場９コード
	   JIGYOUJOU_SBYT_9.GENBA_CD AS JIGYOUJOU_CD_SBYT_9,
       --最終処分事業場１０コード
	   JIGYOUJOU_SBYT_10.GENBA_CD AS JIGYOUJOU_CD_SBYT_10,
       --備考１
	   R06_1.BIKOU AS BIKOU_1,
       --備考２
	   R06_2.BIKOU AS BIKOU_2,
       --備考３
	   R06_3.BIKOU AS BIKOU_3,
       --備考４
	   R06_4.BIKOU AS BIKOU_4,
       --備考５
	   R06_5.BIKOU AS BIKOU_5,

	   --電子/紙区分
	   R08_1.MEDIA_TYPE AS MEDIA_TYPE_01,
	   --マニフェスト番号／交付番号
	   R08_1.MANIFEST_ID AS MANIFEST_ID_01,
	   --交付年月日
	   R08_1.KOUHU_DATE AS KOUHU_DATE_01,
	   --連絡番号
	   R08_1.RENRAKU_ID AS RENRAKU_ID_01,
	   --処分終了日
	   R08_1.SBN_END_DATE AS SBN_END_DATE_01,
	   --排出事業者
	   R08_1.HST_SHA_NAME AS HST_SHA_NAME_01,
	   --排出事業場
	   R08_1.HST_JOU_NAME AS HST_JOU_NAME_01,
	   --廃棄物の種類
	   R08_1.HAIKI_SHURUI AS HAIKI_SHURUI_01,
	   --廃棄物の数量
	   R08_1.HAIKI_SUU AS HAIKI_SUU_01,
	   --廃棄物の数量単位コード
	   R08_1.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_01,

	   --電子/紙区分
	   R08_2.MEDIA_TYPE AS MEDIA_TYPE_02,
	   --マニフェスト番号／交付番号
	   R08_2.MANIFEST_ID AS MANIFEST_ID_02,
	   --交付年月日
	   R08_2.KOUHU_DATE AS KOUHU_DATE_02,
	   --連絡番号
	   R08_2.RENRAKU_ID AS RENRAKU_ID_02,
	   --処分終了日
	   R08_2.SBN_END_DATE AS SBN_END_DATE_02,
	   --排出事業者
	   R08_2.HST_SHA_NAME AS HST_SHA_NAME_02,
	   --排出事業場
	   R08_2.HST_JOU_NAME AS HST_JOU_NAME_02,
	   --廃棄物の種類
	   R08_2.HAIKI_SHURUI AS HAIKI_SHURUI_02,
	   --廃棄物の数量
	   R08_2.HAIKI_SUU AS HAIKI_SUU_02,
	   --廃棄物の数量単位コード
	   R08_2.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_02,

	   --電子/紙区分
	   R08_3.MEDIA_TYPE AS MEDIA_TYPE_03,
	   --マニフェスト番号／交付番号
	   R08_3.MANIFEST_ID AS MANIFEST_ID_03,
	   --交付年月日
	   R08_3.KOUHU_DATE AS KOUHU_DATE_03,
	   --連絡番号
	   R08_3.RENRAKU_ID AS RENRAKU_ID_03,
	   --処分終了日
	   R08_3.SBN_END_DATE AS SBN_END_DATE_03,
	   --排出事業者
	   R08_3.HST_SHA_NAME AS HST_SHA_NAME_03,
	   --排出事業場
	   R08_3.HST_JOU_NAME AS HST_JOU_NAME_03,
	   --廃棄物の種類
	   R08_3.HAIKI_SHURUI AS HAIKI_SHURUI_03,
	   --廃棄物の数量
	   R08_3.HAIKI_SUU AS HAIKI_SUU_03,
	   --廃棄物の数量単位コード
	   R08_3.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_03,

	   --電子/紙区分
	   R08_4.MEDIA_TYPE AS MEDIA_TYPE_04,
	   --マニフェスト番号／交付番号
	   R08_4.MANIFEST_ID AS MANIFEST_ID_04,
	   --交付年月日
	   R08_4.KOUHU_DATE AS KOUHU_DATE_04,
	   --連絡番号
	   R08_4.RENRAKU_ID AS RENRAKU_ID_04,
	   --処分終了日
	   R08_4.SBN_END_DATE AS SBN_END_DATE_04,
	   --排出事業者
	   R08_4.HST_SHA_NAME AS HST_SHA_NAME_04,
	   --排出事業場
	   R08_4.HST_JOU_NAME AS HST_JOU_NAME_04,
	   --廃棄物の種類
	   R08_4.HAIKI_SHURUI AS HAIKI_SHURUI_04,
	   --廃棄物の数量
	   R08_4.HAIKI_SUU AS HAIKI_SUU_04,
	   --廃棄物の数量単位コード
	   R08_4.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_04,

	   --電子/紙区分
	   R08_5.MEDIA_TYPE AS MEDIA_TYPE_05,
	   --マニフェスト番号／交付番号
	   R08_5.MANIFEST_ID AS MANIFEST_ID_05,
	   --交付年月日
	   R08_5.KOUHU_DATE AS KOUHU_DATE_05,
	   --連絡番号
	   R08_5.RENRAKU_ID AS RENRAKU_ID_05,
	   --処分終了日
	   R08_5.SBN_END_DATE AS SBN_END_DATE_05,
	   --排出事業者
	   R08_5.HST_SHA_NAME AS HST_SHA_NAME_05,
	   --排出事業場
	   R08_5.HST_JOU_NAME AS HST_JOU_NAME_05,
	   --廃棄物の種類
	   R08_5.HAIKI_SHURUI AS HAIKI_SHURUI_05,
	   --廃棄物の数量
	   R08_5.HAIKI_SUU AS HAIKI_SUU_05,
	   --廃棄物の数量単位コード
	   R08_5.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_05,

	   --電子/紙区分
	   R08_6.MEDIA_TYPE AS MEDIA_TYPE_06,
	   --マニフェスト番号／交付番号
	   R08_6.MANIFEST_ID AS MANIFEST_ID_06,
	   --交付年月日
	   R08_6.KOUHU_DATE AS KOUHU_DATE_06,
	   --連絡番号
	   R08_6.RENRAKU_ID AS RENRAKU_ID_06,
	   --処分終了日
	   R08_6.SBN_END_DATE AS SBN_END_DATE_06,
	   --排出事業者
	   R08_6.HST_SHA_NAME AS HST_SHA_NAME_06,
	   --排出事業場
	   R08_6.HST_JOU_NAME AS HST_JOU_NAME_06,
	   --廃棄物の種類
	   R08_6.HAIKI_SHURUI AS HAIKI_SHURUI_06,
	   --廃棄物の数量
	   R08_6.HAIKI_SUU AS HAIKI_SUU_06,
	   --廃棄物の数量単位コード
	   R08_6.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_06,

	   --電子/紙区分
	   R08_7.MEDIA_TYPE AS MEDIA_TYPE_07,
	   --マニフェスト番号／交付番号
	   R08_7.MANIFEST_ID AS MANIFEST_ID_07,
	   --交付年月日
	   R08_7.KOUHU_DATE AS KOUHU_DATE_07,
	   --連絡番号
	   R08_7.RENRAKU_ID AS RENRAKU_ID_07,
	   --処分終了日
	   R08_7.SBN_END_DATE AS SBN_END_DATE_07,
	   --排出事業者
	   R08_7.HST_SHA_NAME AS HST_SHA_NAME_07,
	   --排出事業場
	   R08_7.HST_JOU_NAME AS HST_JOU_NAME_07,
	   --廃棄物の種類
	   R08_7.HAIKI_SHURUI AS HAIKI_SHURUI_07,
	   --廃棄物の数量
	   R08_7.HAIKI_SUU AS HAIKI_SUU_07,
	   --廃棄物の数量単位コード
	   R08_7.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_07,

	   --電子/紙区分
	   R08_8.MEDIA_TYPE AS MEDIA_TYPE_08,
	   --マニフェスト番号／交付番号
	   R08_8.MANIFEST_ID AS MANIFEST_ID_08,
	   --交付年月日
	   R08_8.KOUHU_DATE AS KOUHU_DATE_08,
	   --連絡番号
	   R08_8.RENRAKU_ID AS RENRAKU_ID_08,
	   --処分終了日
	   R08_8.SBN_END_DATE AS SBN_END_DATE_08,
	   --排出事業者
	   R08_8.HST_SHA_NAME AS HST_SHA_NAME_08,
	   --排出事業場
	   R08_8.HST_JOU_NAME AS HST_JOU_NAME_08,
	   --廃棄物の種類
	   R08_8.HAIKI_SHURUI AS HAIKI_SHURUI_08,
	   --廃棄物の数量
	   R08_8.HAIKI_SUU AS HAIKI_SUU_08,
	   --廃棄物の数量単位コード
	   R08_8.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_08,

	   --電子/紙区分
	   R08_9.MEDIA_TYPE AS MEDIA_TYPE_09,
	   --マニフェスト番号／交付番号
	   R08_9.MANIFEST_ID AS MANIFEST_ID_09,
	   --交付年月日
	   R08_9.KOUHU_DATE AS KOUHU_DATE_09,
	   --連絡番号
	   R08_9.RENRAKU_ID AS RENRAKU_ID_09,
	   --処分終了日
	   R08_9.SBN_END_DATE AS SBN_END_DATE_09,
	   --排出事業者
	   R08_9.HST_SHA_NAME AS HST_SHA_NAME_09,
	   --排出事業場
	   R08_9.HST_JOU_NAME AS HST_JOU_NAME_09,
	   --廃棄物の種類
	   R08_9.HAIKI_SHURUI AS HAIKI_SHURUI_09,
	   --廃棄物の数量
	   R08_9.HAIKI_SUU AS HAIKI_SUU_09,
	   --廃棄物の数量単位コード
	   R08_9.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_09,

	   --電子/紙区分
	   R08_10.MEDIA_TYPE AS MEDIA_TYPE_10,
	   --マニフェスト番号／交付番号
	   R08_10.MANIFEST_ID AS MANIFEST_ID_10,
	   --交付年月日
	   R08_10.KOUHU_DATE AS KOUHU_DATE_10,
	   --連絡番号
	   R08_10.RENRAKU_ID AS RENRAKU_ID_10,
	   --処分終了日
	   R08_10.SBN_END_DATE AS SBN_END_DATE_10,
	   --排出事業者
	   R08_10.HST_SHA_NAME AS HST_SHA_NAME_10,
	   --排出事業場
	   R08_10.HST_JOU_NAME AS HST_JOU_NAME_10,
	   --廃棄物の種類
	   R08_10.HAIKI_SHURUI AS HAIKI_SHURUI_10,
	   --廃棄物の数量
	   R08_10.HAIKI_SUU AS HAIKI_SUU_10,
	   --廃棄物の数量単位コード
	   R08_10.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_10,

	   --電子/紙区分
	   R08_11.MEDIA_TYPE AS MEDIA_TYPE_11,
	   --マニフェスト番号／交付番号
	   R08_11.MANIFEST_ID AS MANIFEST_ID_11,
	   --交付年月日
	   R08_11.KOUHU_DATE AS KOUHU_DATE_11,
	   --連絡番号
	   R08_11.RENRAKU_ID AS RENRAKU_ID_11,
	   --処分終了日
	   R08_11.SBN_END_DATE AS SBN_END_DATE_11,
	   --排出事業者
	   R08_11.HST_SHA_NAME AS HST_SHA_NAME_11,
	   --排出事業場
	   R08_11.HST_JOU_NAME AS HST_JOU_NAME_11,
	   --廃棄物の種類
	   R08_11.HAIKI_SHURUI AS HAIKI_SHURUI_11,
	   --廃棄物の数量
	   R08_11.HAIKI_SUU AS HAIKI_SUU_11,
	   --廃棄物の数量単位コード
	   R08_11.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_11,

	   --電子/紙区分
	   R08_12.MEDIA_TYPE AS MEDIA_TYPE_12,
	   --マニフェスト番号／交付番号
	   R08_12.MANIFEST_ID AS MANIFEST_ID_12,
	   --交付年月日
	   R08_12.KOUHU_DATE AS KOUHU_DATE_12,
	   --連絡番号
	   R08_12.RENRAKU_ID AS RENRAKU_ID_12,
	   --処分終了日
	   R08_12.SBN_END_DATE AS SBN_END_DATE_12,
	   --排出事業者
	   R08_12.HST_SHA_NAME AS HST_SHA_NAME_12,
	   --排出事業場
	   R08_12.HST_JOU_NAME AS HST_JOU_NAME_12,
	   --廃棄物の種類
	   R08_12.HAIKI_SHURUI AS HAIKI_SHURUI_12,
	   --廃棄物の数量
	   R08_12.HAIKI_SUU AS HAIKI_SUU_12,
	   --廃棄物の数量単位コード
	   R08_12.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_12,

	   --電子/紙区分
	   R08_13.MEDIA_TYPE AS MEDIA_TYPE_13,
	   --マニフェスト番号／交付番号
	   R08_13.MANIFEST_ID AS MANIFEST_ID_13,
	   --交付年月日
	   R08_13.KOUHU_DATE AS KOUHU_DATE_13,
	   --連絡番号
	   R08_13.RENRAKU_ID AS RENRAKU_ID_13,
	   --処分終了日
	   R08_13.SBN_END_DATE AS SBN_END_DATE_13,
	   --排出事業者
	   R08_13.HST_SHA_NAME AS HST_SHA_NAME_13,
	   --排出事業場
	   R08_13.HST_JOU_NAME AS HST_JOU_NAME_13,
	   --廃棄物の種類
	   R08_13.HAIKI_SHURUI AS HAIKI_SHURUI_13,
	   --廃棄物の数量
	   R08_13.HAIKI_SUU AS HAIKI_SUU_13,
	   --廃棄物の数量単位コード
	   R08_13.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_13,

	   --電子/紙区分
	   R08_14.MEDIA_TYPE AS MEDIA_TYPE_14,
	   --マニフェスト番号／交付番号
	   R08_14.MANIFEST_ID AS MANIFEST_ID_14,
	   --交付年月日
	   R08_14.KOUHU_DATE AS KOUHU_DATE_14,
	   --連絡番号
	   R08_14.RENRAKU_ID AS RENRAKU_ID_14,
	   --処分終了日
	   R08_14.SBN_END_DATE AS SBN_END_DATE_14,
	   --排出事業者
	   R08_14.HST_SHA_NAME AS HST_SHA_NAME_14,
	   --排出事業場
	   R08_14.HST_JOU_NAME AS HST_JOU_NAME_14,
	   --廃棄物の種類
	   R08_14.HAIKI_SHURUI AS HAIKI_SHURUI_14,
	   --廃棄物の数量
	   R08_14.HAIKI_SUU AS HAIKI_SUU_14,
	   --廃棄物の数量単位コード
	   R08_14.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_14,

	   --電子/紙区分
	   R08_15.MEDIA_TYPE AS MEDIA_TYPE_15,
	   --マニフェスト番号／交付番号
	   R08_15.MANIFEST_ID AS MANIFEST_ID_15,
	   --交付年月日
	   R08_15.KOUHU_DATE AS KOUHU_DATE_15,
	   --連絡番号
	   R08_15.RENRAKU_ID AS RENRAKU_ID_15,
	   --処分終了日
	   R08_15.SBN_END_DATE AS SBN_END_DATE_15,
	   --排出事業者
	   R08_15.HST_SHA_NAME AS HST_SHA_NAME_15,
	   --排出事業場
	   R08_15.HST_JOU_NAME AS HST_JOU_NAME_15,
	   --廃棄物の種類
	   R08_15.HAIKI_SHURUI AS HAIKI_SHURUI_15,
	   --廃棄物の数量
	   R08_15.HAIKI_SUU AS HAIKI_SUU_15,
	   --廃棄物の数量単位コード
	   R08_15.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_15,

	   --電子/紙区分
	   R08_16.MEDIA_TYPE AS MEDIA_TYPE_16,
	   --マニフェスト番号／交付番号
	   R08_16.MANIFEST_ID AS MANIFEST_ID_16,
	   --交付年月日
	   R08_16.KOUHU_DATE AS KOUHU_DATE_16,
	   --連絡番号
	   R08_16.RENRAKU_ID AS RENRAKU_ID_16,
	   --処分終了日
	   R08_16.SBN_END_DATE AS SBN_END_DATE_16,
	   --排出事業者
	   R08_16.HST_SHA_NAME AS HST_SHA_NAME_16,
	   --排出事業場
	   R08_16.HST_JOU_NAME AS HST_JOU_NAME_16,
	   --廃棄物の種類
	   R08_16.HAIKI_SHURUI AS HAIKI_SHURUI_16,
	   --廃棄物の数量
	   R08_16.HAIKI_SUU AS HAIKI_SUU_16,
	   --廃棄物の数量単位コード
	   R08_16.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_16,

	   --電子/紙区分
	   R08_17.MEDIA_TYPE AS MEDIA_TYPE_17,
	   --マニフェスト番号／交付番号
	   R08_17.MANIFEST_ID AS MANIFEST_ID_17,
	   --交付年月日
	   R08_17.KOUHU_DATE AS KOUHU_DATE_17,
	   --連絡番号
	   R08_17.RENRAKU_ID AS RENRAKU_ID_17,
	   --処分終了日
	   R08_17.SBN_END_DATE AS SBN_END_DATE_17,
	   --排出事業者
	   R08_17.HST_SHA_NAME AS HST_SHA_NAME_17,
	   --排出事業場
	   R08_17.HST_JOU_NAME AS HST_JOU_NAME_17,
	   --廃棄物の種類
	   R08_17.HAIKI_SHURUI AS HAIKI_SHURUI_17,
	   --廃棄物の数量
	   R08_17.HAIKI_SUU AS HAIKI_SUU_17,
	   --廃棄物の数量単位コード
	   R08_17.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_17,

	   --電子/紙区分
	   R08_18.MEDIA_TYPE AS MEDIA_TYPE_18,
	   --マニフェスト番号／交付番号
	   R08_18.MANIFEST_ID AS MANIFEST_ID_18,
	   --交付年月日
	   R08_18.KOUHU_DATE AS KOUHU_DATE_18,
	   --連絡番号
	   R08_18.RENRAKU_ID AS RENRAKU_ID_18,
	   --処分終了日
	   R08_18.SBN_END_DATE AS SBN_END_DATE_18,
	   --排出事業者
	   R08_18.HST_SHA_NAME AS HST_SHA_NAME_18,
	   --排出事業場
	   R08_18.HST_JOU_NAME AS HST_JOU_NAME_18,
	   --廃棄物の種類
	   R08_18.HAIKI_SHURUI AS HAIKI_SHURUI_18,
	   --廃棄物の数量
	   R08_18.HAIKI_SUU AS HAIKI_SUU_18,
	   --廃棄物の数量単位コード
	   R08_18.HAIKI_SUU_UNIT AS HAIKI_SUU_UNIT_18,

	   --修正許可
	   R18.KENGEN_CODE

        --マニフェスト目次情報
  FROM  DT_MF_TOC TOC

--INNER JOIN R18 マニフェスト情報
--  ON マニフェスト目次情報.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND マニフェスト目次情報.最新SEQ　＝　R18 マニフェスト情報.枝番
  INNER JOIN DT_R18 R18 ON TOC.KANRI_ID = R18.KANRI_ID
                       AND TOC.LATEST_SEQ = R18.SEQ

--LEFT JOIN 電子事業場マスタ（排出）
--  ON 電子事業場マスタ（排出）.加入者番号　＝　R18 マニフェスト情報.排出事業者の加入者番号
--  AND 電子事業場マスタ（排出）．事業場名　＝　R18 マニフェスト情報.排出事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_HS ON JIGYOUJOU_HS.EDI_MEMBER_ID = R18.HST_SHA_EDI_MEMBER_ID
                                            AND JIGYOUJOU_HS.JIGYOUJOU_NAME = R18.HST_JOU_NAME
											AND ISNULL(JIGYOUJOU_HS.JIGYOUJOU_ADDRESS1, '') +
											    ISNULL(JIGYOUJOU_HS.JIGYOUJOU_ADDRESS2, '') +
											    ISNULL(JIGYOUJOU_HS.JIGYOUJOU_ADDRESS3, '') +
											    ISNULL(JIGYOUJOU_HS.JIGYOUJOU_ADDRESS4, '')
											=   ISNULL(R18.HST_JOU_ADDRESS1, '') +
											    ISNULL(R18.HST_JOU_ADDRESS2, '') +
												ISNULL(R18.HST_JOU_ADDRESS3, '') +
												ISNULL(R18.HST_JOU_ADDRESS4, '')
                                            --AND JIGYOUJOU_HS.JIGYOUJOU_ADDRESS1 = R18.HST_SHA_ADDRESS1
                                            --AND JIGYOUJOU_HS.JIGYOUJOU_ADDRESS2 = R18.HST_SHA_ADDRESS2
                                            --AND JIGYOUJOU_HS.JIGYOUJOU_ADDRESS3 = R18.HST_SHA_ADDRESS3
                                            --AND JIGYOUJOU_HS.JIGYOUJOU_ADDRESS4 = R18.HST_SHA_ADDRESS4


--LEFT JOIN 電子事業場マスタ（処分）
--  ON 電子事業場マスタ（処分）.加入者番号　＝　R18 マニフェスト情報.処分業者加入者番号
--  AND 電子事業場マスタ（処分）．事業場名　＝　R18 マニフェスト情報.排出事業場名称
   --LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SB ON JIGYOUJOU_SB.EDI_MEMBER_ID = R18.SBN_SHA_MEMBER_ID
   --                                         AND JIGYOUJOU_SB.JIGYOUJOU_NAME = R18.HST_JOU_NAME
   --                                         AND JIGYOUJOU_SB.JIGYOUJOU_ADDRESS1 = R18.HST_SHA_ADDRESS1
   --                                         AND JIGYOUJOU_SB.JIGYOUJOU_ADDRESS2 = R18.HST_SHA_ADDRESS2
   --                                         AND JIGYOUJOU_SB.JIGYOUJOU_ADDRESS3 = R18.HST_SHA_ADDRESS3
   --                                         AND JIGYOUJOU_SB.JIGYOUJOU_ADDRESS4 = R18.HST_SHA_ADDRESS4
	LEFT JOIN DT_R19 DT_R19_LAST ON TOC.KANRI_ID = DT_R19_LAST.KANRI_ID 
											AND TOC.LATEST_SEQ = DT_R19_LAST.SEQ
											AND DT_R19_LAST.UPN_ROUTE_NO = (SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 R19_TMP WHERE TOC.KANRI_ID = R19_TMP.KANRI_ID AND TOC.LATEST_SEQ = R19_TMP.SEQ)
	LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SB ON JIGYOUJOU_SB.EDI_MEMBER_ID = R18.SBN_SHA_MEMBER_ID
											AND JIGYOUJOU_SB.JIGYOUJOU_NAME = DT_R19_LAST.UPNSAKI_JOU_NAME
											AND ISNULL(JIGYOUJOU_SB.JIGYOUJOU_ADDRESS1, '') +
											    ISNULL(JIGYOUJOU_SB.JIGYOUJOU_ADDRESS2, '') +
											    ISNULL(JIGYOUJOU_SB.JIGYOUJOU_ADDRESS3, '') +
											    ISNULL(JIGYOUJOU_SB.JIGYOUJOU_ADDRESS4, '')
											=   ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS1, '') +
											    ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS2, '') +
												ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS3, '') +
												ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS4, '')

--LEFT JOIN R02 有害物質情報1
--  ON R02 有害物質情報1.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R02 有害物質情報1.枝番　＝　R18 マニフェスト情報.枝番
--  AND R02 有害物質情報1.レコード連番　＝　1
   LEFT JOIN DT_R02 R02_1 ON R02_1.KANRI_ID = R18.KANRI_ID
                         AND R02_1.SEQ = R18.SEQ
						 AND R02_1.REC_SEQ = 1

--LEFT JOIN R02 有害物質情報2
--  ON R02 有害物質情報2.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R02 有害物質情報2.枝番　＝　R18 マニフェスト情報.枝番
--  AND R02 有害物質情報2.レコード連番　＝　2
   LEFT JOIN DT_R02 R02_2 ON R02_2.KANRI_ID = R18.KANRI_ID
                         AND R02_2.SEQ = R18.SEQ
						 AND R02_2.REC_SEQ = 2

--LEFT JOIN R02 有害物質情報3
--  ON R02 有害物質情報3.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R02 有害物質情報3.枝番　＝　R18 マニフェスト情報.枝番
--  AND R02 有害物質情報3.レコード連番　＝　3
   LEFT JOIN DT_R02 R02_3 ON R02_3.KANRI_ID = R18.KANRI_ID
                         AND R02_3.SEQ = R18.SEQ
						 AND R02_3.REC_SEQ = 3

--LEFT JOIN R02 有害物質情報4
--  ON R02 有害物質情報4.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R02 有害物質情報4.枝番　＝　R18 マニフェスト情報.枝番
--  AND R02 有害物質情報4.レコード連番　＝　4
   LEFT JOIN DT_R02 R02_4 ON R02_4.KANRI_ID = R18.KANRI_ID
                         AND R02_4.SEQ = R18.SEQ
						 AND R02_4.REC_SEQ = 4

--LEFT JOIN　R02 有害物質情報5
--  ON R02 有害物質情報5.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R02 有害物質情報5.枝番　＝　R18 マニフェスト情報.枝番
--  AND R02 有害物質情報5.レコード連番　＝　5
   LEFT JOIN DT_R02 R02_5 ON R02_5.KANRI_ID = R18.KANRI_ID
                         AND R02_5.SEQ = R18.SEQ
						 AND R02_5.REC_SEQ = 5

--LEFT JOIN R02 有害物質情報6
--  ON R02 有害物質情報6.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R02 有害物質情報6.枝番　＝　R18 マニフェスト情報.枝番
--  AND R02 有害物質情報6.レコード連番　＝　6
   LEFT JOIN DT_R02 R02_6 ON R02_6.KANRI_ID = R18.KANRI_ID
                         AND R02_6.SEQ = R18.SEQ
						 AND R02_6.REC_SEQ = 6

--LEFT JOIN 収集運搬情報(区間１)
--  ON R18 収集運搬情報(区間１).管理番号　＝　R18 マニフェスト情報.管理番号
--  AND 収集運搬情報(区間１).最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND 収集運搬情報(区間１).区間番号　＝　1
   LEFT JOIN DT_R19 R19_UPN_1 ON R19_UPN_1.KANRI_ID = R18.KANRI_ID
                             AND R19_UPN_1.SEQ = R18.SEQ
						     AND R19_UPN_1.UPN_ROUTE_NO = 1

--LEFT JOIN 収集運搬情報(区間２)
--  ON R18 収集運搬情報(区間２).管理番号　＝　R18 マニフェスト情報.管理番号
--  AND 収集運搬情報(区間２).最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND 収集運搬情報(区間２).区間番号　＝　2
   LEFT JOIN DT_R19 R19_UPN_2 ON R19_UPN_2.KANRI_ID = R18.KANRI_ID
                             AND R19_UPN_2.SEQ = R18.SEQ
						     AND R19_UPN_2.UPN_ROUTE_NO = 2

--LEFT JOIN 収集運搬情報(区間３)
--  ON R18 収集運搬情報(区間３).管理番号　＝　R18 マニフェスト情報.管理番号
--  AND 収集運搬情報(区間３).最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND 収集運搬情報(区間３).区間番号　＝　3
   LEFT JOIN DT_R19 R19_UPN_3 ON R19_UPN_3.KANRI_ID = R18.KANRI_ID
                             AND R19_UPN_3.SEQ = R18.SEQ
						     AND R19_UPN_3.UPN_ROUTE_NO = 3

--LEFT JOIN 収集運搬情報(区間４)
--  ON R18 収集運搬情報(区間４).管理番号　＝　R18 マニフェスト情報.管理番号
--  AND 収集運搬情報(区間４).最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND 収集運搬情報(区間４).区間番号　＝　4
   LEFT JOIN DT_R19 R19_UPN_4 ON R19_UPN_4.KANRI_ID = R18.KANRI_ID
                             AND R19_UPN_4.SEQ = R18.SEQ
						     AND R19_UPN_4.UPN_ROUTE_NO = 4

--LEFT JOIN 収集運搬情報(区間５)
--  ON R18 収集運搬情報(区間５).管理番号　＝　R18 マニフェスト情報.管理番号
--  AND 収集運搬情報(区間５).最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND 収集運搬情報(区間５).区間番号　＝　5
   LEFT JOIN DT_R19 R19_UPN_5 ON R19_UPN_5.KANRI_ID = R18.KANRI_ID
                             AND R19_UPN_5.SEQ = R18.SEQ
						     AND R19_UPN_5.UPN_ROUTE_NO = 5

--LEFT JOIN 電子事業者マスタ（収運区間１）
--  ON 電子事業者マスタ（収運区間１）.加入者番号　＝　収集運搬情報(区間1).運搬先加入者番号
   LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU_1 ON JIGYOUSHA_SU_1.EDI_MEMBER_ID = R19_UPN_1.UPNSAKI_EDI_MEMBER_ID

--LEFT JOIN 電子事業者マスタ（収運区間２）
--  ON 電子事業者マスタ（収運区間２）.加入者番号　＝　収集運搬情報(区間２).運搬先加入者番号
   LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU_2 ON JIGYOUSHA_SU_2.EDI_MEMBER_ID = R19_UPN_2.UPNSAKI_EDI_MEMBER_ID

--LEFT JOIN 電子事業者マスタ（収運区間３）
--  ON 電子事業者マスタ（収運区間３）.加入者番号　＝　収集運搬情報(区間３).運搬先加入者番号
   LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU_3 ON JIGYOUSHA_SU_3.EDI_MEMBER_ID = R19_UPN_3.UPNSAKI_EDI_MEMBER_ID

--LEFT JOIN 電子事業者マスタ（収運区間４）
--  ON 電子事業者マスタ（収運区間４）.加入者番号　＝　収集運搬情報(区間４).運搬先加入者番号
   LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU_4 ON JIGYOUSHA_SU_4.EDI_MEMBER_ID = R19_UPN_4.UPNSAKI_EDI_MEMBER_ID

--LEFT JOIN 電子事業者マスタ（収運区間５）
--  ON 電子事業者マスタ（収運区間５）.加入者番号　＝　収集運搬情報(区間３).運搬先加入者番号
   LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU_5 ON JIGYOUSHA_SU_5.EDI_MEMBER_ID = R19_UPN_5.UPNSAKI_EDI_MEMBER_ID

--LEFT JOIN 電子事業場マスタ（収運区間１）
--  ON 電子事業場マスタ（収運区間１）.加入者番号　＝　R19 収集運搬情報(区間１).運搬先加入者番号
--  AND 電子事業場マスタ（収運区間１）．事業場名　＝　収集運搬情報(区間1).運搬先事業場名
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SU_1 ON JIGYOUJOU_SU_1.EDI_MEMBER_ID = R19_UPN_1.UPNSAKI_EDI_MEMBER_ID
                                              AND JIGYOUJOU_SU_1.JIGYOUJOU_NAME = R19_UPN_1.UPNSAKI_JOU_NAME
                                            --AND JIGYOUJOU_SU_1.JIGYOUJOU_ADDRESS1 = R19_UPN_1.UPN_SHA_ADDRESS1
                                            --AND JIGYOUJOU_SU_1.JIGYOUJOU_ADDRESS2 = R19_UPN_1.UPN_SHA_ADDRESS2
                                            --AND JIGYOUJOU_SU_1.JIGYOUJOU_ADDRESS3 = R19_UPN_1.UPN_SHA_ADDRESS3
                                            --AND JIGYOUJOU_SU_1.JIGYOUJOU_ADDRESS4 = R19_UPN_1.UPN_SHA_ADDRESS4
											AND ISNULL(JIGYOUJOU_SU_1.JIGYOUJOU_ADDRESS1, '') +
											    ISNULL(JIGYOUJOU_SU_1.JIGYOUJOU_ADDRESS2, '') +
											    ISNULL(JIGYOUJOU_SU_1.JIGYOUJOU_ADDRESS3, '') +
											    ISNULL(JIGYOUJOU_SU_1.JIGYOUJOU_ADDRESS4, '')
											=   ISNULL(R19_UPN_1.UPNSAKI_JOU_ADDRESS1, '') +
											    ISNULL(R19_UPN_1.UPNSAKI_JOU_ADDRESS2, '') +
												ISNULL(R19_UPN_1.UPNSAKI_JOU_ADDRESS3, '') +
												ISNULL(R19_UPN_1.UPNSAKI_JOU_ADDRESS4, '')

--LEFT JOIN 電子事業場マスタ（収運区間２）
--  ON 電子事業場マスタ（収運区間２）.加入者番号　＝　R19 収集運搬情報(区間２).運搬先加入者番号
--  AND 電子事業場マスタ（収運区間２）．事業場名　＝　収集運搬情報(区間２).運搬先事業場名
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SU_2 ON JIGYOUJOU_SU_2.EDI_MEMBER_ID = R19_UPN_2.UPNSAKI_EDI_MEMBER_ID
                                              AND JIGYOUJOU_SU_2.JIGYOUJOU_NAME = R19_UPN_2.UPNSAKI_JOU_NAME
                                            --AND JIGYOUJOU_SU_2.JIGYOUJOU_ADDRESS1 = R19_UPN_2.UPN_SHA_ADDRESS1
                                            --AND JIGYOUJOU_SU_2.JIGYOUJOU_ADDRESS2 = R19_UPN_2.UPN_SHA_ADDRESS2
                                            --AND JIGYOUJOU_SU_2.JIGYOUJOU_ADDRESS3 = R19_UPN_2.UPN_SHA_ADDRESS3
                                            --AND JIGYOUJOU_SU_2.JIGYOUJOU_ADDRESS4 = R19_UPN_2.UPN_SHA_ADDRESS4
											AND ISNULL(JIGYOUJOU_SU_2.JIGYOUJOU_ADDRESS1, '') +
											    ISNULL(JIGYOUJOU_SU_2.JIGYOUJOU_ADDRESS2, '') +
											    ISNULL(JIGYOUJOU_SU_2.JIGYOUJOU_ADDRESS3, '') +
											    ISNULL(JIGYOUJOU_SU_2.JIGYOUJOU_ADDRESS4, '')
											=   ISNULL(R19_UPN_2.UPNSAKI_JOU_ADDRESS1, '') +
											    ISNULL(R19_UPN_2.UPNSAKI_JOU_ADDRESS2, '') +
												ISNULL(R19_UPN_2.UPNSAKI_JOU_ADDRESS3, '') +
												ISNULL(R19_UPN_2.UPNSAKI_JOU_ADDRESS4, '')

--LEFT JOIN 電子事業場マスタ（収運区間３）
--  ON 電子事業場マスタ（収運区間３）.加入者番号　＝　R19 収集運搬情報(区間３).運搬先加入者番号
--  AND 電子事業場マスタ（収運区間３）．事業場名　＝　収集運搬情報(区間３).運搬先事業場名
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SU_3 ON JIGYOUJOU_SU_3.EDI_MEMBER_ID = R19_UPN_3.UPNSAKI_EDI_MEMBER_ID
                                              AND JIGYOUJOU_SU_3.JIGYOUJOU_NAME = R19_UPN_3.UPNSAKI_JOU_NAME
                                            --AND JIGYOUJOU_SU_3.JIGYOUJOU_ADDRESS1 = R19_UPN_3.UPN_SHA_ADDRESS1
                                            --AND JIGYOUJOU_SU_3.JIGYOUJOU_ADDRESS2 = R19_UPN_3.UPN_SHA_ADDRESS2
                                            --AND JIGYOUJOU_SU_3.JIGYOUJOU_ADDRESS3 = R19_UPN_3.UPN_SHA_ADDRESS3
                                            --AND JIGYOUJOU_SU_3.JIGYOUJOU_ADDRESS4 = R19_UPN_3.UPN_SHA_ADDRESS4
											AND ISNULL(JIGYOUJOU_SU_3.JIGYOUJOU_ADDRESS1, '') +
											    ISNULL(JIGYOUJOU_SU_3.JIGYOUJOU_ADDRESS2, '') +
											    ISNULL(JIGYOUJOU_SU_3.JIGYOUJOU_ADDRESS3, '') +
											    ISNULL(JIGYOUJOU_SU_3.JIGYOUJOU_ADDRESS4, '')
											=   ISNULL(R19_UPN_3.UPNSAKI_JOU_ADDRESS1, '') +
											    ISNULL(R19_UPN_3.UPNSAKI_JOU_ADDRESS2, '') +
												ISNULL(R19_UPN_3.UPNSAKI_JOU_ADDRESS3, '') +
												ISNULL(R19_UPN_3.UPNSAKI_JOU_ADDRESS4, '')

--LEFT JOIN 電子事業場マスタ（収運区間４）
--  ON 電子事業場マスタ（収運区間４）.加入者番号　＝　R19 収集運搬情報(区間４).運搬先加入者番号
--  AND 電子事業場マスタ（収運区間４）．事業場名　＝　収集運搬情報(区間４).運搬先事業場名
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SU_4 ON JIGYOUJOU_SU_4.EDI_MEMBER_ID = R19_UPN_4.UPNSAKI_EDI_MEMBER_ID
                                              AND JIGYOUJOU_SU_4.JIGYOUJOU_NAME = R19_UPN_4.UPNSAKI_JOU_NAME
                                            --AND JIGYOUJOU_SU_4.JIGYOUJOU_ADDRESS1 = R19_UPN_4.UPN_SHA_ADDRESS1
                                            --AND JIGYOUJOU_SU_4.JIGYOUJOU_ADDRESS2 = R19_UPN_4.UPN_SHA_ADDRESS2
                                            --AND JIGYOUJOU_SU_4.JIGYOUJOU_ADDRESS3 = R19_UPN_4.UPN_SHA_ADDRESS3
                                            --AND JIGYOUJOU_SU_4.JIGYOUJOU_ADDRESS4 = R19_UPN_4.UPN_SHA_ADDRESS4
											AND ISNULL(JIGYOUJOU_SU_4.JIGYOUJOU_ADDRESS1, '') +
											    ISNULL(JIGYOUJOU_SU_4.JIGYOUJOU_ADDRESS2, '') +
											    ISNULL(JIGYOUJOU_SU_4.JIGYOUJOU_ADDRESS3, '') +
											    ISNULL(JIGYOUJOU_SU_4.JIGYOUJOU_ADDRESS4, '')
											=   ISNULL(R19_UPN_4.UPNSAKI_JOU_ADDRESS1, '') +
											    ISNULL(R19_UPN_4.UPNSAKI_JOU_ADDRESS2, '') +
												ISNULL(R19_UPN_4.UPNSAKI_JOU_ADDRESS3, '') +
												ISNULL(R19_UPN_4.UPNSAKI_JOU_ADDRESS4, '')

--LEFT JOIN 電子事業場マスタ（収運区間５）
--  ON 電子事業場マスタ（収運区間５）.加入者番号　＝　R19 収集運搬情報(区間５).運搬先加入者番号
--  AND 電子事業場マスタ（収運区間５）．事業場名　＝　収集運搬情報(区間５).運搬先事業場名
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SU_5 ON JIGYOUJOU_SU_5.EDI_MEMBER_ID = R19_UPN_5.UPNSAKI_EDI_MEMBER_ID
                                              AND JIGYOUJOU_SU_5.JIGYOUJOU_NAME = R19_UPN_5.UPNSAKI_JOU_NAME
                                            --AND JIGYOUJOU_SU_5.JIGYOUJOU_ADDRESS1 = R19_UPN_5.UPN_SHA_ADDRESS1
                                            --AND JIGYOUJOU_SU_5.JIGYOUJOU_ADDRESS2 = R19_UPN_5.UPN_SHA_ADDRESS2
                                            --AND JIGYOUJOU_SU_5.JIGYOUJOU_ADDRESS3 = R19_UPN_5.UPN_SHA_ADDRESS3
                                            --AND JIGYOUJOU_SU_5.JIGYOUJOU_ADDRESS4 = R19_UPN_5.UPN_SHA_ADDRESS4
											AND ISNULL(JIGYOUJOU_SU_5.JIGYOUJOU_ADDRESS1, '') +
											    ISNULL(JIGYOUJOU_SU_5.JIGYOUJOU_ADDRESS2, '') +
											    ISNULL(JIGYOUJOU_SU_5.JIGYOUJOU_ADDRESS3, '') +
											    ISNULL(JIGYOUJOU_SU_5.JIGYOUJOU_ADDRESS4, '')
											=   ISNULL(R19_UPN_5.UPNSAKI_JOU_ADDRESS1, '') +
											    ISNULL(R19_UPN_5.UPNSAKI_JOU_ADDRESS2, '') +
												ISNULL(R19_UPN_5.UPNSAKI_JOU_ADDRESS3, '') +
												ISNULL(R19_UPN_5.UPNSAKI_JOU_ADDRESS4, '')

--   LEFT JOIN R04 最終処分事業場（予定）情報１
--  ON R04 最終処分事業場（予定）情報１.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R04 最終処分事業場（予定）情報１.枝番　＝　R18 マニフェスト情報.枝番
--  AND R04 最終処分事業場（予定）情報１.レコード連番　＝　1
   LEFT JOIN DT_R04 R04_1 ON R04_1.KANRI_ID = R18.KANRI_ID
                         AND R04_1.SEQ = R18.SEQ
						 AND R04_1.REC_SEQ = 1

--LEFT JOIN R04 最終処分事業場（予定）情報２
--  ON R04 最終処分事業場（予定）情報２.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R04 最終処分事業場（予定）情報２.枝番　＝　R18 マニフェスト情報.枝番
--  AND R04 最終処分事業場（予定）情報２.レコード連番　＝　2
   LEFT JOIN DT_R04 R04_2 ON R04_2.KANRI_ID = R18.KANRI_ID
                         AND R04_2.SEQ = R18.SEQ
						 AND R04_2.REC_SEQ = 2

--LEFT JOIN R04 最終処分事業場（予定）情報３
--  ON R04 最終処分事業場（予定）情報３.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R04 最終処分事業場（予定）情報３.枝番　＝　R18 マニフェスト情報.枝番
--  AND R04 最終処分事業場（予定）情報３.レコード連番　＝　3
   LEFT JOIN DT_R04 R04_3 ON R04_3.KANRI_ID = R18.KANRI_ID
                         AND R04_3.SEQ = R18.SEQ
						 AND R04_3.REC_SEQ = 3

--LEFT JOIN R04 最終処分事業場（予定）情報４
--  ON R04 最終処分事業場（予定）情報４.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R04 最終処分事業場（予定）情報４.枝番　＝　R18 マニフェスト情報.枝番
--  AND R04 最終処分事業場（予定）情報４.レコード連番　＝　4
   LEFT JOIN DT_R04 R04_4 ON R04_4.KANRI_ID = R18.KANRI_ID
                         AND R04_4.SEQ = R18.SEQ
						 AND R04_4.REC_SEQ = 4

--LEFT JOIN R04 最終処分事業場（予定）情報５
--  ON R04 最終処分事業場（予定）情報５.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R04 最終処分事業場（予定）情報５.枝番　＝　R18 マニフェスト情報.枝番
--  AND R04 最終処分事業場（予定）情報５.レコード連番　＝　5
   LEFT JOIN DT_R04 R04_5 ON R04_5.KANRI_ID = R18.KANRI_ID
                         AND R04_5.SEQ = R18.SEQ
						 AND R04_5.REC_SEQ = 5

--LEFT JOIN R04 最終処分事業場（予定）情報６
--  ON R04 最終処分事業場（予定）情報６.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R04 最終処分事業場（予定）情報６.枝番　＝　R18 マニフェスト情報.枝番
--  AND R04 最終処分事業場（予定）情報６.レコード連番　＝　6
   LEFT JOIN DT_R04 R04_6 ON R04_6.KANRI_ID = R18.KANRI_ID
                         AND R04_6.SEQ = R18.SEQ
						 AND R04_6.REC_SEQ = 6

--LEFT JOIN R04 最終処分事業場（予定）情報７
--  ON R04 最終処分事業場（予定）情報７.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R04 最終処分事業場（予定）情報７.枝番　＝　R18 マニフェスト情報.枝番
--  AND R04 最終処分事業場（予定）情報７.レコード連番　＝　7
   LEFT JOIN DT_R04 R04_7 ON R04_7.KANRI_ID = R18.KANRI_ID
                         AND R04_7.SEQ = R18.SEQ
						 AND R04_7.REC_SEQ = 7

--LEFT JOIN R04 最終処分事業場（予定）情報８
--  ON R04 最終処分事業場（予定）情報８.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R04 最終処分事業場（予定）情報８.枝番　＝　R18 マニフェスト情報.枝番
--  AND R04 最終処分事業場（予定）情報８.レコード連番　＝　8
   LEFT JOIN DT_R04 R04_8 ON R04_8.KANRI_ID = R18.KANRI_ID
                         AND R04_8.SEQ = R18.SEQ
						 AND R04_8.REC_SEQ = 8

--LEFT JOIN R04 最終処分事業場（予定）情報９
--  ON R04 最終処分事業場（予定）情報９.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R04 最終処分事業場（予定）情報９.枝番　＝　R18 マニフェスト情報.枝番
--  AND R04 最終処分事業場（予定）情報９.レコード連番　＝　9
   LEFT JOIN DT_R04 R04_9 ON R04_9.KANRI_ID = R18.KANRI_ID
                         AND R04_9.SEQ = R18.SEQ
						 AND R04_9.REC_SEQ = 9

--LEFT JOIN R04 最終処分事業場（予定）情報１０
--  ON R04 最終処分事業場（予定）情報１０.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R04 最終処分事業場（予定）情報１０.枝番　＝　R18 マニフェスト情報.枝番
--  AND R04 最終処分事業場（予定）情報１０.レコード連番　＝　10
   LEFT JOIN DT_R04 R04_10 ON R04_10.KANRI_ID = R18.KANRI_ID
                         AND R04_10.SEQ = R18.SEQ
						 AND R04_10.REC_SEQ = 10

--LEFT JOIN 電子事業場マスタ（最終処分予定１）
--  ON 電子事業場マスタ（最終処分予定１）.事業場名　＝　R04 最終処分事業場（予定）情報１.最終処分事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SBYT_1 ON JIGYOUJOU_SBYT_1.JIGYOUJOU_NAME = R04_1.LAST_SBN_JOU_NAME
                                            AND JIGYOUJOU_SBYT_1.JIGYOUJOU_ADDRESS1 = R04_1.LAST_SBN_JOU_ADDRESS1
                                            AND JIGYOUJOU_SBYT_1.JIGYOUJOU_ADDRESS2 = R04_1.LAST_SBN_JOU_ADDRESS2
                                            AND JIGYOUJOU_SBYT_1.JIGYOUJOU_ADDRESS3 = R04_1.LAST_SBN_JOU_ADDRESS3
                                            AND JIGYOUJOU_SBYT_1.JIGYOUJOU_ADDRESS4 = R04_1.LAST_SBN_JOU_ADDRESS4

--LEFT JOIN 電子事業場マスタ（最終処分予定２）
--  ON 電子事業場マスタ（最終処分予定２）.事業場名　＝　R04 最終処分事業場（予定）情報２.最終処分事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SBYT_2 ON JIGYOUJOU_SBYT_2.JIGYOUJOU_NAME = R04_2.LAST_SBN_JOU_NAME
                                            AND JIGYOUJOU_SBYT_2.JIGYOUJOU_ADDRESS1 = R04_2.LAST_SBN_JOU_ADDRESS1
                                            AND JIGYOUJOU_SBYT_2.JIGYOUJOU_ADDRESS2 = R04_2.LAST_SBN_JOU_ADDRESS2
                                            AND JIGYOUJOU_SBYT_2.JIGYOUJOU_ADDRESS3 = R04_2.LAST_SBN_JOU_ADDRESS3
                                            AND JIGYOUJOU_SBYT_2.JIGYOUJOU_ADDRESS4 = R04_2.LAST_SBN_JOU_ADDRESS4

--LEFT JOIN 電子事業場マスタ（最終処分予定３）
--  ON 電子事業場マスタ（最終処分予定３）.事業場名　＝　R04 最終処分事業場（予定）情報３.最終処分事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SBYT_3 ON JIGYOUJOU_SBYT_3.JIGYOUJOU_NAME = R04_3.LAST_SBN_JOU_NAME
                                            AND JIGYOUJOU_SBYT_3.JIGYOUJOU_ADDRESS1 = R04_3.LAST_SBN_JOU_ADDRESS1
                                            AND JIGYOUJOU_SBYT_3.JIGYOUJOU_ADDRESS2 = R04_3.LAST_SBN_JOU_ADDRESS2
                                            AND JIGYOUJOU_SBYT_3.JIGYOUJOU_ADDRESS3 = R04_3.LAST_SBN_JOU_ADDRESS3
                                            AND JIGYOUJOU_SBYT_3.JIGYOUJOU_ADDRESS4 = R04_3.LAST_SBN_JOU_ADDRESS4

--LEFT JOIN 電子事業場マスタ（最終処分予定４）
--  ON 電子事業場マスタ（最終処分予定４）.事業場名　＝　R04 最終処分事業場（予定）情報４.最終処分事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SBYT_4 ON JIGYOUJOU_SBYT_4.JIGYOUJOU_NAME = R04_4.LAST_SBN_JOU_NAME
                                            AND JIGYOUJOU_SBYT_4.JIGYOUJOU_ADDRESS1 = R04_4.LAST_SBN_JOU_ADDRESS1
                                            AND JIGYOUJOU_SBYT_4.JIGYOUJOU_ADDRESS2 = R04_4.LAST_SBN_JOU_ADDRESS2
                                            AND JIGYOUJOU_SBYT_4.JIGYOUJOU_ADDRESS3 = R04_4.LAST_SBN_JOU_ADDRESS3
                                            AND JIGYOUJOU_SBYT_4.JIGYOUJOU_ADDRESS4 = R04_4.LAST_SBN_JOU_ADDRESS4

--LEFT JOIN 電子事業場マスタ（最終処分予定５）
--  ON 電子事業場マスタ（最終処分予定５）.事業場名　＝　R04 最終処分事業場（予定）情報５.最終処分事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SBYT_5 ON JIGYOUJOU_SBYT_5.JIGYOUJOU_NAME = R04_5.LAST_SBN_JOU_NAME
                                            AND JIGYOUJOU_SBYT_5.JIGYOUJOU_ADDRESS1 = R04_5.LAST_SBN_JOU_ADDRESS1
                                            AND JIGYOUJOU_SBYT_5.JIGYOUJOU_ADDRESS2 = R04_5.LAST_SBN_JOU_ADDRESS2
                                            AND JIGYOUJOU_SBYT_5.JIGYOUJOU_ADDRESS3 = R04_5.LAST_SBN_JOU_ADDRESS3
                                            AND JIGYOUJOU_SBYT_5.JIGYOUJOU_ADDRESS4 = R04_5.LAST_SBN_JOU_ADDRESS4

--LEFT JOIN 電子事業場マスタ（最終処分予定６）
--  ON 電子事業場マスタ（最終処分予定６）.事業場名　＝　R04 最終処分事業場（予定）情報６.最終処分事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SBYT_6 ON JIGYOUJOU_SBYT_6.JIGYOUJOU_NAME = R04_6.LAST_SBN_JOU_NAME
                                            AND JIGYOUJOU_SBYT_6.JIGYOUJOU_ADDRESS1 = R04_6.LAST_SBN_JOU_ADDRESS1
                                            AND JIGYOUJOU_SBYT_6.JIGYOUJOU_ADDRESS2 = R04_6.LAST_SBN_JOU_ADDRESS2
                                            AND JIGYOUJOU_SBYT_6.JIGYOUJOU_ADDRESS3 = R04_6.LAST_SBN_JOU_ADDRESS3
                                            AND JIGYOUJOU_SBYT_6.JIGYOUJOU_ADDRESS4 = R04_6.LAST_SBN_JOU_ADDRESS4

--LEFT JOIN 電子事業場マスタ（最終処分予定７）
--  ON 電子事業場マスタ（最終処分予定７）.事業場名　＝　R04 最終処分事業場（予定）情報７.最終処分事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SBYT_7 ON JIGYOUJOU_SBYT_7.JIGYOUJOU_NAME = R04_7.LAST_SBN_JOU_NAME
                                            AND JIGYOUJOU_SBYT_7.JIGYOUJOU_ADDRESS1 = R04_7.LAST_SBN_JOU_ADDRESS1
                                            AND JIGYOUJOU_SBYT_7.JIGYOUJOU_ADDRESS2 = R04_7.LAST_SBN_JOU_ADDRESS2
                                            AND JIGYOUJOU_SBYT_7.JIGYOUJOU_ADDRESS3 = R04_7.LAST_SBN_JOU_ADDRESS3
                                            AND JIGYOUJOU_SBYT_7.JIGYOUJOU_ADDRESS4 = R04_7.LAST_SBN_JOU_ADDRESS4

--LEFT JOIN 電子事業場マスタ（最終処分予定８）
--  ON 電子事業場マスタ（最終処分予定８）.事業場名　＝　R04 最終処分事業場（予定）情報８.最終処分事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SBYT_8 ON JIGYOUJOU_SBYT_8.JIGYOUJOU_NAME = R04_8.LAST_SBN_JOU_NAME
                                            AND JIGYOUJOU_SBYT_8.JIGYOUJOU_ADDRESS1 = R04_8.LAST_SBN_JOU_ADDRESS1
                                            AND JIGYOUJOU_SBYT_8.JIGYOUJOU_ADDRESS2 = R04_8.LAST_SBN_JOU_ADDRESS2
                                            AND JIGYOUJOU_SBYT_8.JIGYOUJOU_ADDRESS3 = R04_8.LAST_SBN_JOU_ADDRESS3
                                            AND JIGYOUJOU_SBYT_8.JIGYOUJOU_ADDRESS4 = R04_8.LAST_SBN_JOU_ADDRESS4

--LEFT JOIN 電子事業場マスタ（最終処分予定９）
--  ON 電子事業場マスタ（最終処分予定９）.事業場名　＝　R04 最終処分事業場（予定）情報９.最終処分事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SBYT_9 ON JIGYOUJOU_SBYT_9.JIGYOUJOU_NAME = R04_9.LAST_SBN_JOU_NAME
                                            AND JIGYOUJOU_SBYT_9.JIGYOUJOU_ADDRESS1 = R04_9.LAST_SBN_JOU_ADDRESS1
                                            AND JIGYOUJOU_SBYT_9.JIGYOUJOU_ADDRESS2 = R04_9.LAST_SBN_JOU_ADDRESS2
                                            AND JIGYOUJOU_SBYT_9.JIGYOUJOU_ADDRESS3 = R04_9.LAST_SBN_JOU_ADDRESS3
                                            AND JIGYOUJOU_SBYT_9.JIGYOUJOU_ADDRESS4 = R04_9.LAST_SBN_JOU_ADDRESS4

--LEFT JOIN 電子事業場マスタ（最終処分予定１０）
--  ON 電子事業場マスタ（最終処分予定１０）.事業場名　＝　R04 最終処分事業場（予定）情報１０.最終処分事業場名称
   LEFT JOIN M_DENSHI_JIGYOUJOU JIGYOUJOU_SBYT_10 ON JIGYOUJOU_SBYT_10.JIGYOUJOU_NAME = R04_10.LAST_SBN_JOU_NAME
                                            AND JIGYOUJOU_SBYT_10.JIGYOUJOU_ADDRESS1 = R04_10.LAST_SBN_JOU_ADDRESS1
                                            AND JIGYOUJOU_SBYT_10.JIGYOUJOU_ADDRESS2 = R04_10.LAST_SBN_JOU_ADDRESS2
                                            AND JIGYOUJOU_SBYT_10.JIGYOUJOU_ADDRESS3 = R04_10.LAST_SBN_JOU_ADDRESS3
                                            AND JIGYOUJOU_SBYT_10.JIGYOUJOU_ADDRESS4 = R04_10.LAST_SBN_JOU_ADDRESS4

--LEFT JOIN R05 連絡番号情報1
--  ON R05 連絡番号情報1.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R05 連絡番号情報1.枝番　＝　R18 マニフェスト情報.枝番
--  AND R05 連絡番号情報1.連絡番号No　＝　1
   LEFT JOIN DT_R05 R05_1 ON R05_1.KANRI_ID = R18.KANRI_ID
                         AND R05_1.SEQ = R18.SEQ
						 AND R05_1.RENRAKU_ID_NO = 1

--LEFT JOIN R05 連絡番号情報2
--  ON R05 連絡番号情報2.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R05 連絡番号情報2.枝番　＝　R18 マニフェスト情報.枝番
--  AND R05 連絡番号情報2.連絡番号No　＝　2
   LEFT JOIN DT_R05 R05_2 ON R05_2.KANRI_ID = R18.KANRI_ID
                         AND R05_2.SEQ = R18.SEQ
						 AND R05_2.RENRAKU_ID_NO = 2

--LEFT JOIN R05 連絡番号情報3
--  ON R05 連絡番号情報3.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R05 連絡番号情報3.枝番　＝　R18 マニフェスト情報.枝番
--  AND R05 連絡番号情報3.連絡番号No　＝　3
   LEFT JOIN DT_R05 R05_3 ON R05_3.KANRI_ID = R18.KANRI_ID
                         AND R05_3.SEQ = R18.SEQ
						 AND R05_3.RENRAKU_ID_NO = 3

--LEFT JOIN R06 備考情報１
--  ON R06 備考情報１.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R06 備考情報１.枝番　＝　R18 マニフェスト情報.枝番
--  AND R06 備考情報１.連絡番号No　＝　1
   LEFT JOIN DT_R06 R06_1 ON R06_1.KANRI_ID = R18.KANRI_ID
                         AND R06_1.SEQ = R18.SEQ
						 AND R06_1.BIKOU_NO = 1

--LEFT JOIN R06 備考情報２
--  ON R06 備考情報２.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R06 備考情報２.枝番　＝　R18 マニフェスト情報.枝番
--  AND R06 備考情報２.連絡番号No　＝　2
   LEFT JOIN DT_R06 R06_2 ON R06_2.KANRI_ID = R18.KANRI_ID
                         AND R06_2.SEQ = R18.SEQ
						 AND R06_2.BIKOU_NO = 2

--LEFT JOIN R06 備考情報３
--  ON R06 備考情報３.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R06 備考情報３.枝番　＝　R18 マニフェスト情報.枝番
--  AND R06 備考情報３.連絡番号No　＝　3
   LEFT JOIN DT_R06 R06_3 ON R06_3.KANRI_ID = R18.KANRI_ID
                         AND R06_3.SEQ = R18.SEQ
						 AND R06_3.BIKOU_NO = 3

--LEFT JOIN R06 備考情報４
--  ON R06 備考情報４.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R06 備考情報４.枝番　＝　R18 マニフェスト情報.枝番
--  AND R06 備考情報４.連絡番号No　＝　4
   LEFT JOIN DT_R06 R06_4 ON R06_4.KANRI_ID = R18.KANRI_ID
                         AND R06_4.SEQ = R18.SEQ
						 AND R06_4.BIKOU_NO = 4

--LEFT JOIN R06 備考情報５
--  ON R06 備考情報５.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R06 備考情報５.枝番　＝　R18 マニフェスト情報.枝番
--  AND R06 備考情報５.連絡番号No　＝　5
   LEFT JOIN DT_R06 R06_5 ON R06_5.KANRI_ID = R18.KANRI_ID
                         AND R06_5.SEQ = R18.SEQ
						 AND R06_5.BIKOU_NO = 5

--LEFT JOIN R08 1次マニフェスト情報1
--  ON R08 1次マニフェスト情報1.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報1.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08枝 1次マニフェスト情報1.レコード番　＝　1
   LEFT JOIN DT_R08 R08_1 ON R08_1.KANRI_ID = R18.KANRI_ID
                         AND R08_1.SEQ = R18.SEQ
						 AND R08_1.REC_SEQ = 1

--LEFT JOIN R08 1次マニフェスト情報2
--  ON R08 1次マニフェスト情報2.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報2.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報2.レコード枝番　＝　2
   LEFT JOIN DT_R08 R08_2 ON R08_2.KANRI_ID = R18.KANRI_ID
                         AND R08_2.SEQ = R18.SEQ
						 AND R08_2.REC_SEQ = 2

--LEFT JOIN R08 1次マニフェスト情報3
--  ON R08 1次マニフェスト情報3.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報3.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報3.レコード枝番　＝　3
   LEFT JOIN DT_R08 R08_3 ON R08_3.KANRI_ID = R18.KANRI_ID
                         AND R08_3.SEQ = R18.SEQ
						 AND R08_3.REC_SEQ = 3

--LEFT JOIN R08 1次マニフェスト情報4
--  ON R08 1次マニフェスト情報4.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報4.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報4.レコード枝番　＝　4
   LEFT JOIN DT_R08 R08_4 ON R08_4.KANRI_ID = R18.KANRI_ID
                         AND R08_4.SEQ = R18.SEQ
						 AND R08_4.REC_SEQ = 4

--LEFT JOIN R08 1次マニフェスト情報5
--  ON R08 1次マニフェスト情報5.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報5.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報5.レコード枝番　＝　5
   LEFT JOIN DT_R08 R08_5 ON R08_5.KANRI_ID = R18.KANRI_ID
                         AND R08_5.SEQ = R18.SEQ
						 AND R08_5.REC_SEQ = 5

--LEFT JOIN R08 1次マニフェスト情報6
--  ON R08 1次マニフェスト情報6.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報6.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報6.レコード枝番　＝　6
   LEFT JOIN DT_R08 R08_6 ON R08_6.KANRI_ID = R18.KANRI_ID
                         AND R08_6.SEQ = R18.SEQ
						 AND R08_6.REC_SEQ = 6

--LEFT JOIN R08 1次マニフェスト情報7
--  ON R08 1次マニフェスト情報7.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報7.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報7.レコード枝番　＝　7
   LEFT JOIN DT_R08 R08_7 ON R08_7.KANRI_ID = R18.KANRI_ID
                         AND R08_7.SEQ = R18.SEQ
						 AND R08_7.REC_SEQ = 7

--LEFT JOIN R08 1次マニフェスト情報8
--  ON R08 1次マニフェスト情報8.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報8.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報8.レコード枝番　＝　8
   LEFT JOIN DT_R08 R08_8 ON R08_8.KANRI_ID = R18.KANRI_ID
                         AND R08_8.SEQ = R18.SEQ
						 AND R08_8.REC_SEQ = 8

--LEFT JOIN R08 1次マニフェスト情報9
--  ON R08 1次マニフェスト情報9.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報9.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報9.レコード枝番　＝　9
   LEFT JOIN DT_R08 R08_9 ON R08_9.KANRI_ID = R18.KANRI_ID
                         AND R08_9.SEQ = R18.SEQ
						 AND R08_9.REC_SEQ = 9

--LEFT JOIN R08 1次マニフェスト情報10
--  ON R08 1次マニフェスト情報10.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報10.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報10.レコード枝番　＝　10
   LEFT JOIN DT_R08 R08_10 ON R08_10.KANRI_ID = R18.KANRI_ID
                         AND R08_10.SEQ = R18.SEQ
						 AND R08_10.REC_SEQ = 10

--LEFT JOIN R08 1次マニフェスト情報11
--  ON R08 1次マニフェスト情報11.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報11.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報11.レコード枝番　＝　11
   LEFT JOIN DT_R08 R08_11 ON R08_11.KANRI_ID = R18.KANRI_ID
                         AND R08_11.SEQ = R18.SEQ
						 AND R08_11.REC_SEQ = 11

--LEFT JOIN R08 1次マニフェスト情報12
--  ON R08 1次マニフェスト情報12.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報12.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報12.レコード枝番　＝　12
   LEFT JOIN DT_R08 R08_12 ON R08_12.KANRI_ID = R18.KANRI_ID
                         AND R08_12.SEQ = R18.SEQ
						 AND R08_12.REC_SEQ = 12

--LEFT JOIN R08 1次マニフェスト情報13
--  ON R08 1次マニフェスト情報13.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報13.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報13.レコード枝番　＝　13
   LEFT JOIN DT_R08 R08_13 ON R08_13.KANRI_ID = R18.KANRI_ID
                         AND R08_13.SEQ = R18.SEQ
						 AND R08_13.REC_SEQ = 13

--LEFT JOIN R08 1次マニフェスト情報14
--  ON R08 1次マニフェスト情報14.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報14.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報14.レコード枝番　＝　14
   LEFT JOIN DT_R08 R08_14 ON R08_14.KANRI_ID = R18.KANRI_ID
                         AND R08_14.SEQ = R18.SEQ
						 AND R08_14.REC_SEQ = 14

--LEFT JOIN R08 1次マニフェスト情報15
--  ON R08 1次マニフェスト情報15.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報15.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報15.レコード枝番　＝　15
   LEFT JOIN DT_R08 R08_15 ON R08_15.KANRI_ID = R18.KANRI_ID
                         AND R08_15.SEQ = R18.SEQ
						 AND R08_15.REC_SEQ = 15

--LEFT JOIN R08 1次マニフェスト情報16
--  ON R08 1次マニフェスト情報16.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報16.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報16.レコード枝番　＝　16
   LEFT JOIN DT_R08 R08_16 ON R08_16.KANRI_ID = R18.KANRI_ID
                         AND R08_16.SEQ = R18.SEQ
						 AND R08_16.REC_SEQ = 16

--LEFT JOIN R08 1次マニフェスト情報17
--  ON R08 1次マニフェスト情報17.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報17.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報17.レコード枝番　＝　17
   LEFT JOIN DT_R08 R08_17 ON R08_17.KANRI_ID = R18.KANRI_ID
                         AND R08_17.SEQ = R18.SEQ
						 AND R08_17.REC_SEQ = 17

--LEFT JOIN R08 1次マニフェスト情報18
--  ON R08 1次マニフェスト情報18.管理番号　＝　R18 マニフェスト情報.管理番号
--  AND R08 1次マニフェスト情報18.最新SEQ　＝　R18 マニフェスト情報.枝番
--  AND R08 1次マニフェスト情報18.レコード枝番　＝　18
   LEFT JOIN DT_R08 R08_18 ON R08_18.KANRI_ID = R18.KANRI_ID
                         AND R08_18.SEQ = R18.SEQ
						 AND R08_18.REC_SEQ = 18

--マニフェスト目次情報.種類　＝　4(電子)
--マニフェスト目次情報.管理番号　IN　画面でチェックした行の管理番号
--マニフェスト目次情報.状態フラグ　<> 1:削除
 WHERE (TOC.KIND = 4 OR TOC.KIND = 5 OR TOC.KIND IS NULL)
   AND TOC.KANRI_ID IN /*data.KANRI_ID*/('123') 
   AND TOC.STATUS_FLAG <> 9
 ORDER BY R18.MANIFEST_ID