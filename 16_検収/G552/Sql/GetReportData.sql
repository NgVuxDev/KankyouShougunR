SELECT
	tse.SHUKKA_NUMBER,
	tse.DENPYOU_DATE,
	tse.KENSHU_DATE,
	tse.TORIHIKISAKI_CD,
	tse.TORIHIKISAKI_NAME,
	tse.GYOUSHA_CD,
	tse.GYOUSHA_NAME,
	tse.GENBA_CD,
	tse.GENBA_NAME,
	tsd.HINMEI_CD SHUKKA_DETAIL_HINMEI_CD, 
	tsd.HINMEI_NAME SHUKKA_DETAIL_HINMEI_NAME,
	tsd.NET_JYUURYOU,
	tsd.SUURYOU SHUKKA_DETAIL_SUURYOU,
	(
		SELECT top 1 UNIT_NAME_RYAKU
		FROM M_UNIT u
		WHERE u.UNIT_CD = tsd.UNIT_CD
	) SHUKKA_DETAIL_UNIT_NAME_RYAKU,
	tsd.TANKA SHUKKA_DETAIL_TANKA,
	ISNULL(tsd.KINGAKU, 0) + ISNULL(tsd.HINMEI_KINGAKU, 0) SHUKKA_DETAIL_KINGAKU,
	tkd.HINMEI_CD KENSYU_DETAIL_HINMEI_CD,
	tkd.HINMEI_NAME  KENSYU_DETAIL_HINMEI_NAME,
	tkd.KENSHU_NET,
	tkd.SUURYOU KENSYU_DETAIL_SUURYOU,
	tkd.BUBIKI,
	(
		SELECT top 1 UNIT_NAME_RYAKU
		FROM M_UNIT u
		WHERE u.UNIT_CD = tkd.UNIT_CD
	) KENSYU_DETAIL_UNIT_NAME_RYAKU,
	tkd.TANKA KENSYU_DETAIL_TANKA,
	ISNULL(tkd.KINGAKU, 0) + ISNULL(tkd.HINMEI_KINGAKU, 0) KENSYU_DETAIL_KINGAKU
	,tse.KYOTEN_CD
	,tsd.DETAIL_SYSTEM_ID
	,tsd.SYSTEM_ID
	,tsd.SEQ
	,tse.NIZUMI_GYOUSHA_CD
	,tse.NIZUMI_GENBA_CD
	,tse.URIAGE_DATE,
	(
		SELECT top 1 DENPYOU_KBN_NAME_RYAKU
		FROM M_DENPYOU_KBN denKbn
		WHERE denKbn.DENPYOU_KBN_CD = tsd.DENPYOU_KBN_CD
	) DETAIL_DENPYOU_KBN_NAME_RYAKU,
	tkd.DENPYOU_KBN_CD AS KENSHU_DENPYOU_KBN_CD,
	(
		SELECT top 1 DENPYOU_KBN_NAME_RYAKU
		FROM M_DENPYOU_KBN denKbn
		WHERE denKbn.DENPYOU_KBN_CD = tkd.DENPYOU_KBN_CD
	) KENSYU_DENPYOU_KBN_NAME_RYAKU,
	tkd.KENSHU_ROW_NO

FROM
	T_SHUKKA_ENTRY AS tse
	LEFT JOIN T_SHUKKA_DETAIL AS tsd
		ON tse.SYSTEM_ID = tsd.SYSTEM_ID
		AND tse.SEQ = tsd.SEQ

	LEFT JOIN T_KENSHU_DETAIL AS tkd
		ON tkd.SYSTEM_ID = tsd.SYSTEM_ID
		AND tkd.SEQ = tsd.SEQ
		AND tkd.DETAIL_SYSTEM_ID = tsd.DETAIL_SYSTEM_ID

WHERE
	tse.DELETE_FLG = 0
	AND tse.KENSHU_MUST_KBN = 1

	/*IF data.Shukka_Entry_KENSHU_JYOUKYOU == 1 */
		AND tse.KENSHU_DATE IS NULL
	/*END*/
	/*IF data.Shukka_Entry_KENSHU_JYOUKYOU == 2 */
		AND tse.KENSHU_DATE IS NOT NULL
	/*END*/

	/*IF !data.Shukka_Entry_Denpyou_Date_Begin.IsNull*/AND DENPYOU_DATE >= /*data.Shukka_Entry_Denpyou_Date_Begin*//*END*/
	/*IF !data.Shukka_Entry_Denpyou_Date_End.IsNull*/and  DENPYOU_DATE <= /*data.Shukka_Entry_Denpyou_Date_End*//*END*/
	/*IF !data.Shukka_Entry_Kenshu_Date_Begin.IsNull*/and  KENSHU_DATE >= /*data.Shukka_Entry_Kenshu_Date_Begin*//*END*/
	/*IF !data.Shukka_Entry_Kenshu_Date_End.IsNull*/ and  KENSHU_DATE <= /*data.Shukka_Entry_Kenshu_Date_End*//*END*/

	/*IF !data.Shukka_Entry_KYOTEN_CD.IsNull && data.Shukka_Entry_KYOTEN_CD != 99*/AND tse.KYOTEN_CD = /*data.Shukka_Entry_KYOTEN_CD*//*END*/

	/*IF data.Shukka_Entry_Torihikisaki_Cd_1 != null && data.Shukka_Entry_Torihikisaki_Cd_1 != ''*/AND tse.TORIHIKISAKI_CD >= /*data.Shukka_Entry_Torihikisaki_Cd_1*//*END*/
	/*IF data.Shukka_Entry_Torihikisaki_Cd_2 != null && data.Shukka_Entry_Torihikisaki_Cd_2 != ''*/AND tse.TORIHIKISAKI_CD <= /*data.Shukka_Entry_Torihikisaki_Cd_2*//*END*/

	/*IF data.Shukka_Entry_Nizumi_Gyousha_Cd_1 != null && data.Shukka_Entry_Nizumi_Gyousha_Cd_1 != ''*/AND tse.NIZUMI_GYOUSHA_CD >= /*data.Shukka_Entry_Nizumi_Gyousha_Cd_1*//*END*/
	/*IF data.Shukka_Entry_Nizumi_Gyousha_Cd_2 != null && data.Shukka_Entry_Nizumi_Gyousha_Cd_2 != ''*/AND tse.NIZUMI_GYOUSHA_CD <= /*data.Shukka_Entry_Nizumi_Gyousha_Cd_2*//*END*/
	/*IF data.Shukka_Entry_Nizumi_Genba_Cd_1 != null && data.Shukka_Entry_Nizumi_Genba_Cd_1 != ''*/AND tse.NIZUMI_GENBA_CD >= /*data.Shukka_Entry_Nizumi_Genba_Cd_1*//*END*/
	/*IF data.Shukka_Entry_Nizumi_Genba_Cd_2 != null && data.Shukka_Entry_Nizumi_Genba_Cd_2 != ''*/AND tse.NIZUMI_GENBA_CD <= /*data.Shukka_Entry_Nizumi_Genba_Cd_2*//*END*/

	/*IF data.Shukka_Entry_Gyousha_Cd_1 != null && data.Shukka_Entry_Gyousha_Cd_1 != ''*/AND tse.GYOUSHA_CD >= /*data.Shukka_Entry_Gyousha_Cd_1*//*END*/
	/*IF data.Shukka_Entry_Gyousha_Cd_2 != null && data.Shukka_Entry_Gyousha_Cd_2 != ''*/AND tse.GYOUSHA_CD <= /*data.Shukka_Entry_Gyousha_Cd_2*//*END*/
	/*IF data.Shukka_Entry_Genba_Cd_1 != null && data.Shukka_Entry_Genba_Cd_1 != ''*/AND tse.GENBA_CD >= /*data.Shukka_Entry_Genba_Cd_1*//*END*/
	/*IF data.Shukka_Entry_Genba_Cd_2 != null && data.Shukka_Entry_Genba_Cd_2 != ''*/AND tse.GENBA_CD <= /*data.Shukka_Entry_Genba_Cd_2*//*END*/

	/*IF data.Shukka_Detail_Hinmei_Cd_1 != null && data.Shukka_Detail_Hinmei_Cd_1 != ''*/AND tsd.HINMEI_CD >= /*data.Shukka_Detail_Hinmei_Cd_1*//*END*/
	/*IF data.Shukka_Detail_Hinmei_Cd_2 != null && data.Shukka_Detail_Hinmei_Cd_2 != ''*/AND tsd.HINMEI_CD <= /*data.Shukka_Detail_Hinmei_Cd_2*//*END*/
	/*IF data.Shukka_Entry_KENSHUHINMEI_CD_1 != null && data.Shukka_Entry_KENSHUHINMEI_CD_1 != ''*/AND tkd.HINMEI_CD >= /*data.Shukka_Entry_KENSHUHINMEI_CD_1*//*END*/
	/*IF data.Shukka_Entry_KENSHUHINMEI_CD_2 != null && data.Shukka_Entry_KENSHUHINMEI_CD_2 != ''*/AND tkd.HINMEI_CD <= /*data.Shukka_Entry_KENSHUHINMEI_CD_2*//*END*/
