SELECT
    TNE.KYOTEN_CD,
    TNE.NYUUKIN_NUMBER,
    TNE.DENPYOU_DATE,
    ISNULL(TNE.TORIHIKISAKI_CD, '') AS TORIHIKISAKI_CD,
    (SELECT
        T.TORIHIKISAKI_NAME_RYAKU
    FROM M_TORIHIKISAKI AS T
    WHERE T.TORIHIKISAKI_CD = TNE.TORIHIKISAKI_CD)
    AS TORIHIKISAKI_NAME_RYAKU,
    (SELECT
        T.TORIHIKISAKI_FURIGANA
    FROM M_TORIHIKISAKI AS T
    WHERE T.TORIHIKISAKI_CD = TNE.TORIHIKISAKI_CD)
    AS TORIHIKISAKI_FURIGANA,
    TNE.NYUUKINSAKI_CD,
    (SELECT
        N.NYUUKINSAKI_NAME_RYAKU
    FROM M_NYUUKINSAKI AS N
    WHERE N.NYUUKINSAKI_CD = TNE.NYUUKINSAKI_CD)
    AS NYUUKINSAKI_NAME_RYAKU,
    (SELECT
        N.NYUUKINSAKI_FURIGANA
    FROM M_NYUUKINSAKI AS N
    WHERE N.NYUUKINSAKI_CD = TNE.NYUUKINSAKI_CD)
    AS NYUUKINSAKI_FURIGANA,
    TNE.BANK_CD,
    TNE.BANK_SHITEN_CD,
    TNE.UPDATE_DATE,
    TND.ROW_NUMBER,
    TND.NYUUSHUKKIN_KBN_CD,
    (SELECT
        N.NYUUSHUKKIN_KBN_NAME_RYAKU
    FROM M_NYUUSHUKKIN_KBN AS N
    WHERE N.NYUUSHUKKIN_KBN_CD = TND.NYUUSHUKKIN_KBN_CD)
    AS NYUUSHUKKIN_KBN_NAME_RYAKU,
    TND.KINGAKU,
    TND.MEISAI_BIKOU
FROM T_NYUUKIN_ENTRY AS TNE
JOIN T_NYUUKIN_DETAIL AS TND
    ON TNE.SYSTEM_ID = TND.SYSTEM_ID
    AND TNE.SEQ = TND.SEQ
    AND TND.NYUUSHUKKIN_KBN_CD IS NOT NULL
WHERE 1 = 1
/*IF dto.KyotenCd != 99*/AND TNE.KYOTEN_CD = /*dto.KyotenCd*/0/*END*/
/*IF dto.DateShuruiCd == 1*/
/*IF dto.DateFrom != null && dto.DateFrom != ''*/AND CONVERT(varchar, TNE.DENPYOU_DATE, 112) >= CONVERT(varchar, CONVERT(datetime, /*dto.DateFrom*/'2014/01/01 00:00:00'), 112)/*END*/
/*IF dto.DateTo != null && dto.DateTo != ''*/AND CONVERT(varchar, TNE.DENPYOU_DATE, 112) <= CONVERT(varchar, CONVERT(datetime, /*dto.DateTo*/'2014/12/31 00:00:00'), 112)/*END*/
/*END*/
/*IF dto.DateShuruiCd == 2*/
/*IF dto.DateFrom != null && dto.DateFrom != ''*/AND CONVERT(varchar, TNE.UPDATE_DATE, 112) >= CONVERT(varchar, CONVERT(datetime, /*dto.DateFrom*/'2014/01/01 00:00:00'), 112)/*END*/
/*IF dto.DateTo != null && dto.DateTo != ''*/AND CONVERT(varchar, TNE.UPDATE_DATE, 112) <= CONVERT(varchar, CONVERT(datetime, /*dto.DateTo*/'2014/12/31 00:00:00'), 112)/*END*/
/*END*/

/*IF dto.NyuukinsakiCdFrom != null && dto.NyuukinsakiCdTo != null && (dto.NyuukinsakiCdFrom != '' || dto.NyuukinsakiCdTo != '')*/
/*IF dto.NyuukinsakiCdFrom != null*/AND TNE.NYUUKINSAKI_CD >= /*dto.NyuukinsakiCdFrom*/'000000'/*END*/
/*IF dto.NyuukinsakiCdTo != null*/AND TNE.NYUUKINSAKI_CD <= /*dto.NyuukinsakiCdTo*/'999999'/*END*/
/*END*/

/*IF dto.TorihikisakiCdFrom != null && dto.TorihikisakiCdTo != null && (dto.TorihikisakiCdFrom != '' || dto.TorihikisakiCdTo != '')*/
/*IF dto.TorihikisakiCdFrom != null*/AND TNE.TORIHIKISAKI_CD >= /*dto.TorihikisakiCdFrom*/''/*END*/
/*IF dto.TorihikisakiCdTo != null*/AND TNE.TORIHIKISAKI_CD <= /*dto.TorihikisakiCdTo*/'999999'/*END*/
/*END*/

/*IF dto.BankCdFrom != null && dto.BankCdFrom != ''*/AND TNE.BANK_CD >= /*dto.BankCdFrom*/'0000'/*END*/
/*IF dto.BankCdTo != null && dto.BankCdTo != ''*/AND TNE.BANK_CD <= /*dto.BankCdTo*/'9999'/*END*/

/*IF dto.BankShitenCdFrom != null && dto.BankShitenCdFrom != ''*/AND TNE.BANK_SHITEN_CD >= /*dto.BankShitenCdFrom*/'000'/*END*/
/*IF dto.BankShitenCdTo != null && dto.BankShitenCdTo != ''*/AND TNE.BANK_SHITEN_CD <= /*dto.BankShitenCdTo*/'999'/*END*/

AND TNE.DELETE_FLG = 0
/*IF dto.Sort1 == 2 && dto.Sort2 == 1*/
-- 取引先・コード順
ORDER BY TNE.TORIHIKISAKI_CD, TNE.NYUUKINSAKI_CD, TNE.DENPYOU_DATE, TNE.NYUUKIN_NUMBER, TND.DETAIL_SYSTEM_ID, TND.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 2 && dto.Sort2 == 2*/
-- 取引先・フリガナ順
ORDER BY TORIHIKISAKI_FURIGANA, TNE.TORIHIKISAKI_CD, TNE.NYUUKINSAKI_CD, TNE.DENPYOU_DATE, TNE.NYUUKIN_NUMBER, TND.DETAIL_SYSTEM_ID, TND.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 2 && dto.Sort2 == 3*/
-- 取引先・伝票日付順
ORDER BY TNE.DENPYOU_DATE, TNE.NYUUKIN_NUMBER, TNE.TORIHIKISAKI_CD, TNE.NYUUKINSAKI_CD, TND.DETAIL_SYSTEM_ID, TND.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 2 && dto.Sort2 == 4*/
-- 取引先・伝票番号順
ORDER BY TNE.NYUUKIN_NUMBER, TNE.DENPYOU_DATE, TNE.TORIHIKISAKI_CD, TNE.NYUUKINSAKI_CD, TND.DETAIL_SYSTEM_ID, TND.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 2 && dto.Sort2 == 5*/
-- 取引先・入金区分順
ORDER BY TND.NYUUSHUKKIN_KBN_CD, TNE.DENPYOU_DATE, TNE.NYUUKIN_NUMBER, TNE.TORIHIKISAKI_CD, TNE.NYUUKINSAKI_CD, TND.DETAIL_SYSTEM_ID, TND.ROW_NUMBER
/*END*/
