SELECT
    TNSE.KYOTEN_CD,
    TNSE.NYUUKIN_NUMBER,
    TNSE.DENPYOU_DATE,
    '' AS TORIHIKISAKI_CD,
    '' AS TORIHIKISAKI_NAME_RYAKU,
    '' AS TORIHIKISAKI_FURIGANA,
    TNSE.NYUUKINSAKI_CD,
    (SELECT
        N.NYUUKINSAKI_NAME_RYAKU
    FROM M_NYUUKINSAKI AS N
    WHERE N.NYUUKINSAKI_CD = TNSE.NYUUKINSAKI_CD)
    AS NYUUKINSAKI_NAME_RYAKU,
    (SELECT
        N.NYUUKINSAKI_FURIGANA
    FROM M_NYUUKINSAKI AS N
    WHERE N.NYUUKINSAKI_CD = TNSE.NYUUKINSAKI_CD)
    AS NYUUKINSAKI_FURIGANA,
    TNSE.BANK_CD,
    TNSE.BANK_SHITEN_CD,
    TNSE.UPDATE_DATE,
    TNSD.ROW_NUMBER,
    TNSD.NYUUSHUKKIN_KBN_CD,
    (SELECT
        N.NYUUSHUKKIN_KBN_NAME_RYAKU
    FROM M_NYUUSHUKKIN_KBN AS N
    WHERE N.NYUUSHUKKIN_KBN_CD = TNSD.NYUUSHUKKIN_KBN_CD)
    AS NYUUSHUKKIN_KBN_NAME_RYAKU,
    TNSD.KINGAKU,
    TNSD.MEISAI_BIKOU
FROM T_NYUUKIN_SUM_ENTRY AS TNSE
JOIN T_NYUUKIN_SUM_DETAIL AS TNSD
    ON TNSE.SYSTEM_ID = TNSD.SYSTEM_ID
    AND TNSE.SEQ = TNSD.SEQ
WHERE 1 = 1
AND TNSD.NYUUSHUKKIN_KBN_CD<> 51--CD:51 仮受金を使用したものは仮受金は入金明細表に反映させない。 #13105
/*IF dto.KyotenCd != 99*/AND TNSE.KYOTEN_CD = /*dto.KyotenCd*/0/*END*/
/*IF dto.DateShuruiCd == 1*/
/*IF dto.DateFrom != null && dto.DateFrom != ''*/AND CONVERT(varchar, TNSE.DENPYOU_DATE, 112) >= CONVERT(varchar, CONVERT(datetime, /*dto.DateFrom*/'2014/01/01 00:00:00'), 112)/*END*/
/*IF dto.DateTo != null && dto.DateTo != ''*/AND CONVERT(varchar, TNSE.DENPYOU_DATE, 112) <= CONVERT(varchar, CONVERT(datetime, /*dto.DateTo*/'2014/12/31 00:00:00'), 112)/*END*/
/*END*/
/*IF dto.DateShuruiCd == 2*/
/*IF dto.DateFrom != null && dto.DateFrom != ''*/AND CONVERT(varchar, TNSE.UPDATE_DATE, 112) >= CONVERT(varchar, CONVERT(datetime, /*dto.DateFrom*/'2014/01/01 00:00:00'), 112)/*END*/
/*IF dto.DateTo != null && dto.DateTo != ''*/AND CONVERT(varchar, TNSE.UPDATE_DATE, 112) <= CONVERT(varchar, CONVERT(datetime, /*dto.DateTo*/'2014/12/31 00:00:00'), 112)/*END*/
/*END*/

/*IF dto.NyuukinsakiCdFrom != null && dto.NyuukinsakiCdTo != null && (dto.NyuukinsakiCdFrom != '' || dto.NyuukinsakiCdTo != '')*/
/*IF dto.NyuukinsakiCdFrom != null*/AND TNSE.NYUUKINSAKI_CD >= /*dto.NyuukinsakiCdFrom*/'000000'/*END*/
/*IF dto.NyuukinsakiCdTo != null*/AND TNSE.NYUUKINSAKI_CD <= /*dto.NyuukinsakiCdTo*/'999999'/*END*/
/*END*/

/*IF dto.BankCdFrom != null && dto.BankCdFrom != ''*/AND TNSE.BANK_CD >= /*dto.BankCdFrom*/'0000'/*END*/
/*IF dto.BankCdTo != null && dto.BankCdTo != ''*/AND TNSE.BANK_CD <= /*dto.BankCdTo*/'9999'/*END*/

/*IF dto.BankShitenCdFrom != null && dto.BankShitenCdFrom != ''*/AND TNSE.BANK_SHITEN_CD >= /*dto.BankShitenCdFrom*/'000'/*END*/
/*IF dto.BankShitenCdTo != null && dto.BankShitenCdTo != ''*/AND TNSE.BANK_SHITEN_CD <= /*dto.BankShitenCdTo*/'999'/*END*/

AND TNSE.DELETE_FLG = 0
/*IF dto.Sort1 == 1 && dto.Sort2 == 1*/
-- 入金先・コード順
ORDER BY TNSE.NYUUKINSAKI_CD, TNSE.DENPYOU_DATE, TNSE.NYUUKIN_NUMBER, TNSD.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 1 && dto.Sort2 == 2*/
-- 入金先・フリガナ順
ORDER BY NYUUKINSAKI_FURIGANA, TNSE.NYUUKINSAKI_CD, TNSE.DENPYOU_DATE, TNSE.NYUUKIN_NUMBER, TNSD.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 1 && dto.Sort2 == 3*/
-- 入金先・伝票日付順
ORDER BY TNSE.DENPYOU_DATE, TNSE.NYUUKIN_NUMBER, TNSE.NYUUKINSAKI_CD, TNSD.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 1 && dto.Sort2 == 4*/
-- 入金先・伝票番号順
ORDER BY TNSE.NYUUKIN_NUMBER, TNSE.DENPYOU_DATE, TNSE.NYUUKINSAKI_CD, TNSD.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 1 && dto.Sort2 == 5*/
-- 入金先・入金区分順
ORDER BY TNSD.NYUUSHUKKIN_KBN_CD, TNSE.DENPYOU_DATE, TNSE.NYUUKIN_NUMBER, TNSE.NYUUKINSAKI_CD, TNSD.ROW_NUMBER
/*END*/
